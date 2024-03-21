using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

public class Client : MonoBehaviour
{
    [SerializeField]RawImage image;
    Socket client;
    public string Ip;
    public int Port;

    private void Awake()
    {
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    private void Start()
    {
        // Connect �Լ��� ����(127.0.0.1)�� ��Ʈ ��ȣ 9999�� ��� ���� socket�� �����Ѵ�.
        client.Connect(new IPEndPoint(IPAddress.Parse(Ip), Port));
        // ���� �޽����� UTF8Ÿ���� byte �迭�� ��ȯ�Ѵ�.
        var data = Encoding.UTF8.GetBytes("this message is sent from C# client.");

        // big��������� ������ ���̸� ��ȯ�ϰ� ������ ���� �������� ���̸� ������. (4byte)
        client.Send(BitConverter.GetBytes(data.Length));
        // �����͸� �����Ѵ�.
        client.Send(data);
    }

    private void Update()
    {
        ServerConnet();
    }

    void ServerConnet()
    {
        // �������� ���̸� �����ϱ� ���� �迭�� �����Ѵ�. (4byte)
        var data = new byte[4];
        // �������� ���̸� �����Ѵ�.
        client.Receive(data, data.Length, SocketFlags.None);
        // server���� big��������� ������ �ߴµ��� little ��������� �´�. big������ little������� �迭�� ������ �ݴ��̹Ƿ� reverse�Ѵ�.
        Array.Reverse(data);
        // �������� ���̸�ŭ byte �迭�� �����Ѵ�.
        data = new byte[BitConverter.ToInt32(data, 0)];
        // �����͸� �����Ѵ�.
        client.Receive(data, data.Length, SocketFlags.None);

        //byte�� base64�� ����
        ChangeByteToBase64(data);

        // ���ŵ� �����͸� UTF8���ڵ����� string Ÿ������ ��ȯ �Ŀ� �ֿܼ� ����Ѵ�.
        Debug.Log(Encoding.UTF8.GetString(data));
    }

    void ChangeByteToBase64(byte[] data)
    {
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data);
        image.GetComponent<Renderer>().material.mainTexture = tex;
        Texture tex2 = tex;
        image.GetComponent<MeshRenderer>().material.mainTexture = tex;
        image.GetComponent<RawImage>().texture = tex as Texture;
    }
}