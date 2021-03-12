using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x020000BA RID: 186
	public abstract class TcpConnection : IDisposable
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x00025CE0 File Offset: 0x00023EE0
		public TcpConnection(EndPoint targetEndpoint)
		{
			bool flag = false;
			Socket socket = null;
			try
			{
				socket = new Socket(targetEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				socket.Connect(targetEndpoint);
				this.connection = new NetworkConnection(socket, 4096);
				this.connection.SendTimeout = 120;
				this.connection.ReceiveTimeout = 120;
				this.data = new byte[4096];
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (this.connection != null)
					{
						this.connection.Dispose();
					}
					else if (socket != null)
					{
						try
						{
							if (socket.Connected)
							{
								socket.Shutdown(SocketShutdown.Both);
							}
						}
						finally
						{
							socket.Close();
						}
					}
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x00025D9C File Offset: 0x00023F9C
		public IPEndPoint LocalEndPoint
		{
			get
			{
				return this.connection.LocalEndPoint;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x00025DA9 File Offset: 0x00023FA9
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.connection.RemoteEndPoint;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00025DB6 File Offset: 0x00023FB6
		internal ChannelBindingToken ChannelBindingToken
		{
			get
			{
				return this.connection.ChannelBindingToken;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00025DC3 File Offset: 0x00023FC3
		protected byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00025DCC File Offset: 0x00023FCC
		public void SendRawData(byte[] data, int offset, int length)
		{
			IAsyncResult asyncResult = this.connection.BeginWrite(data, offset, length, null, null);
			if (!asyncResult.AsyncWaitHandle.WaitOne(120000, false))
			{
				throw new TimeoutException(string.Format("No data written in {0} seconds.", 120));
			}
			object obj;
			this.connection.EndWrite(asyncResult, out obj);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00025E22 File Offset: 0x00024022
		public void SendData(byte[] data)
		{
			this.SendData(data, 0, data.Length);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00025E2F File Offset: 0x0002402F
		public void SendData(byte[] data, int offset, int length)
		{
			if (data[length - 2] != TcpConnection.byteCrLf[0] && data[length - 1] != TcpConnection.byteCrLf[1])
			{
				throw new ArgumentException("SendData must end with CRLF.");
			}
			this.SendRawData(data, offset, length);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00025E64 File Offset: 0x00024064
		public void SendData(string request)
		{
			string text = request;
			if (!text.EndsWith("\r\n"))
			{
				text = string.Format("{0}{1}", text, "\r\n");
			}
			byte[] array = new byte[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				array[i] = (byte)(text[i] & 'ÿ');
			}
			this.SendData(array);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00025EC6 File Offset: 0x000240C6
		public TcpResponse SendDataWithResponse(string data)
		{
			this.SendData(data);
			return this.GetResponse(120, null, false);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00025ED9 File Offset: 0x000240D9
		public TcpResponse GetResponse()
		{
			return this.GetResponse(120, null, false);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00025EE8 File Offset: 0x000240E8
		public TcpResponse GetResponse(int timeout, string expectedTag, bool multiLine)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string responseString = string.Empty;
			do
			{
				byte[] rawBytes = this.GetRawBytes(timeout);
				if (rawBytes.Length == 0)
				{
					break;
				}
				stringBuilder.Append(Encoding.ASCII.GetString(rawBytes, 0, rawBytes.Length));
				responseString = stringBuilder.ToString();
			}
			while (!this.LastLineReceived(responseString, expectedTag, multiLine));
			return this.CreateResponse(responseString);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00025F40 File Offset: 0x00024140
		public string GetRawString()
		{
			byte[] rawBytes = this.GetRawBytes(120);
			string result;
			if (rawBytes.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				result = Encoding.ASCII.GetString(rawBytes, 0, rawBytes.Length);
			}
			return result;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00025F74 File Offset: 0x00024174
		public IAsyncResult BeginRead()
		{
			return this.connection.BeginRead(null, null);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00025F84 File Offset: 0x00024184
		public string EndRead(IAsyncResult asyncResult)
		{
			byte[] array;
			int srcOffset;
			int num;
			object obj;
			this.connection.EndRead(asyncResult, out array, out srcOffset, out num, out obj);
			if (obj != null)
			{
				throw new ApplicationException("EndRead() resulted in non-null error code: " + obj.ToString());
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, srcOffset, array2, 0, num);
			string result;
			if (array.Length == 0)
			{
				result = string.Empty;
			}
			else
			{
				result = Encoding.ASCII.GetString(array2, 0, array2.Length);
			}
			return result;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00025FF4 File Offset: 0x000241F4
		public bool IsConnected()
		{
			return false;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00025FF8 File Offset: 0x000241F8
		public bool IsDisconnected()
		{
			int num = 0;
			while (this.IsConnected() && num < 100)
			{
				Thread.Sleep(100);
				num++;
			}
			return !this.IsConnected();
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0002602C File Offset: 0x0002422C
		public void Close()
		{
			this.connection.Dispose();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00026039 File Offset: 0x00024239
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00026044 File Offset: 0x00024244
		public void NegotiateSSL()
		{
			IAsyncResult asyncResult = this.connection.BeginNegotiateTlsAsClient(null, null);
			if (!asyncResult.AsyncWaitHandle.WaitOne(50000))
			{
				throw new InvalidOperationException("Negotiate SSL process timed out");
			}
			object obj;
			this.connection.EndNegotiateTlsAsClient(asyncResult, out obj);
			if (obj != null)
			{
				throw new InvalidOperationException("TcpConnection Errorcode was not null");
			}
		}

		// Token: 0x06000664 RID: 1636
		public abstract bool LastLineReceived(string responseString, string expectedTag, bool multiLine);

		// Token: 0x06000665 RID: 1637
		public abstract TcpResponse CreateResponse(string responseString);

		// Token: 0x06000666 RID: 1638 RVA: 0x0002609C File Offset: 0x0002429C
		private byte[] GetRawBytes(int timeout)
		{
			IAsyncResult asyncResult = this.connection.BeginRead(null, null);
			if (!asyncResult.AsyncWaitHandle.WaitOne(timeout * 1000, false))
			{
				throw new ApplicationException(string.Format("No data received in {0} seconds while initializing tcp connection.", timeout));
			}
			byte[] src;
			int srcOffset;
			int num;
			object obj;
			this.connection.EndRead(asyncResult, out src, out srcOffset, out num, out obj);
			if (obj != null)
			{
				throw new ApplicationException("EndRead() resulted in non-null error code: " + obj.ToString());
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(src, srcOffset, array, 0, num);
			return array;
		}

		// Token: 0x04000402 RID: 1026
		public const int DefaultTimeout = 120;

		// Token: 0x04000403 RID: 1027
		public const int DefaultNegotiateSslTimeout = 50000;

		// Token: 0x04000404 RID: 1028
		protected const string StrCrLf = "\r\n";

		// Token: 0x04000405 RID: 1029
		private static byte[] byteCrLf = Encoding.ASCII.GetBytes("\r\n");

		// Token: 0x04000406 RID: 1030
		private NetworkConnection connection;

		// Token: 0x04000407 RID: 1031
		private byte[] data;
	}
}
