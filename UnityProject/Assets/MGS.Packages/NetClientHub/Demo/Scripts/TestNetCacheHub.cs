/*************************************************************************
 *  Copyright © 2022 Mogoson. All rights reserved.
 *------------------------------------------------------------------------
 *  File         :  TestNetCacheHub.cs
 *  Description  :  Ignore.
 *------------------------------------------------------------------------
 *  Author       :  Mogoson
 *  Version      :  1.0
 *  Date         :  7/20/2022
 *  Description  :  Initial development version.
 *************************************************************************/

using UnityEngine;

namespace MGS.Net.Demo
{
    public class TestNetCacheHub : MonoBehaviour
    {
        public string url;
        INetClient client;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (client == null)
                {
                    client = NetClientHubAPI.handler.Put(url, 1000);
                    if (client is NetCacheClient)
                    {
                        Debug.LogFormat("Respond from cache.");
                    }
                    else
                    {
                        Debug.LogFormat("Request to server.");
                    }
                }
            }

            if (client != null)
            {
                if (!client.IsDone)
                {
                    Debug.LogFormat("progress is {0}", client.Progress);
                    Debug.LogFormat("Speed is {0} kb", client.Speed / 1024);
                }
                else
                {
                    if (client.Error == null)
                    {
                        Debug.LogFormat("result is {0}", client.Result);
                    }
                    else
                    {
                        Debug.LogErrorFormat("error is {0}", client.Error.Message);
                    }

                    client.Close();
                    client = null;
                }
            }
        }

        private void OnDestroy()
        {
            NetClientHubAPI.handler.Dispose();
            client = null;
        }
    }
}