using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007BB RID: 1979
	public class HttpPingServer
	{
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x00055DE4 File Offset: 0x00053FE4
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x00055DEC File Offset: 0x00053FEC
		public static HttpPingServer Instance
		{
			get
			{
				return HttpPingServer.theInstance;
			}
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00055DF3 File Offset: 0x00053FF3
		private HttpPingServer()
		{
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00055E08 File Offset: 0x00054008
		public void Initialize()
		{
			if (this.initialized)
			{
				return;
			}
			lock (this)
			{
				if (!this.initialized)
				{
					while (this.connectRetryCount < 5)
					{
						Exception ex = this.Reconnect();
						if (ex == null)
						{
							this.initialized = true;
							return;
						}
						this.TrackError(ex);
						this.connectRetryCount++;
					}
					this.initialized = false;
				}
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00055E88 File Offset: 0x00054088
		private void TrackError(Exception e)
		{
			if (this.errors.Count > 10)
			{
				string text;
				this.errors.TryPop(out text);
			}
			this.errors.Push(e.ToString());
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00055EC4 File Offset: 0x000540C4
		private Exception Reconnect()
		{
			if (this.mainSocket != null)
			{
				this.mainSocket.Dispose();
			}
			this.port = -1;
			try
			{
				this.localIps = Dns.GetHostEntry("localhost");
				IPAddress ipaddress = this.localIps.AddressList[0];
				this.mainSocket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.mainSocket.Bind(new IPEndPoint(ipaddress, 0));
				this.mainSocket.Listen(200);
				this.port = ((IPEndPoint)this.mainSocket.LocalEndPoint).Port;
				this.mainSocket.BeginAccept(new AsyncCallback(this.NewConnectionReceived), null);
			}
			catch (Exception result)
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x00055F8C File Offset: 0x0005418C
		private void NewConnectionReceived(IAsyncResult asyncResult)
		{
			Socket socket;
			try
			{
				socket = this.mainSocket.EndAccept(asyncResult);
				this.connectRetryCount = 0;
			}
			catch (Exception e)
			{
				this.HandleSocketDisconnection(e);
				return;
			}
			try
			{
				this.mainSocket.BeginAccept(new AsyncCallback(this.NewConnectionReceived), null);
				if (!this.IsLocalConnection(socket))
				{
					socket.Close();
					socket.Dispose();
				}
				else
				{
					using (NetworkStream networkStream = new NetworkStream(socket, true))
					{
						using (StreamReader streamReader = new StreamReader(networkStream))
						{
							string text;
							do
							{
								text = streamReader.ReadLine();
							}
							while (text != null && text.Trim() != string.Empty);
							using (StreamWriter streamWriter = new StreamWriter(networkStream))
							{
								streamWriter.Write("HTTP/1.1 200 OK\r\nContent-Type: text/plain; charset=UTF-8\r\nContent-Length: 0\r\nX-PingServer: 1\r\nConnection: Close\r\n\r\n");
								streamWriter.Flush();
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				this.HandleIndividualSocketException(e2);
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000560AC File Offset: 0x000542AC
		private void HandleIndividualSocketException(Exception e)
		{
			this.TrackError(e);
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000560B5 File Offset: 0x000542B5
		private void HandleSocketDisconnection(Exception e)
		{
			this.TrackError(e);
			this.connectRetryCount++;
			this.initialized = false;
			this.Initialize();
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000560DC File Offset: 0x000542DC
		private bool IsLocalConnection(Socket newSocket)
		{
			IPEndPoint ipendPoint = (IPEndPoint)newSocket.RemoteEndPoint;
			foreach (IPAddress ipaddress in this.localIps.AddressList)
			{
				if (ipaddress.Equals(ipendPoint.Address))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002427 RID: 9255
		private const string OkResponse = "HTTP/1.1 200 OK\r\nContent-Type: text/plain; charset=UTF-8\r\nContent-Length: 0\r\nX-PingServer: 1\r\nConnection: Close\r\n\r\n";

		// Token: 0x04002428 RID: 9256
		private const int MaxReconnectAttempts = 5;

		// Token: 0x04002429 RID: 9257
		private const int MaxErrorsToTrack = 10;

		// Token: 0x0400242A RID: 9258
		private static HttpPingServer theInstance = new HttpPingServer();

		// Token: 0x0400242B RID: 9259
		private Socket mainSocket;

		// Token: 0x0400242C RID: 9260
		private int port;

		// Token: 0x0400242D RID: 9261
		private bool initialized;

		// Token: 0x0400242E RID: 9262
		private IPHostEntry localIps;

		// Token: 0x0400242F RID: 9263
		private int connectRetryCount;

		// Token: 0x04002430 RID: 9264
		private ConcurrentStack<string> errors = new ConcurrentStack<string>();
	}
}
