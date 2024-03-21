using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ViewModel : MonoBehaviour
{
    void Connect()
    {
        AsyncServer().Wait();
    }

    async Task AsyncServer()
    {
        //listener
    }
}