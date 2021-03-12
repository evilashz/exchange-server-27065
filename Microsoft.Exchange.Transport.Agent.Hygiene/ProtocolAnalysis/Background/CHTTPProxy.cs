using System;
using System.Net;
using System.Text;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000049 RID: 73
	internal class CHTTPProxy : TransportConnection, IDataConnection
	{
		// Token: 0x0600023F RID: 575 RVA: 0x0000F416 File Offset: 0x0000D616
		public CHTTPProxy(bool fPost, ProxyChain proxyChain) : base(proxyChain)
		{
			this.fPost = fPost;
			this.connectRequest = new StringBuilder();
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000F434 File Offset: 0x0000D634
		public override void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpCxn, NetworkCredential authInfo)
		{
			this.tcpCxn = tcpCxn;
			if (this.fPost)
			{
				this.connectRequest.AppendFormat("POST http://{0}:{1}/ HTTP/1.0\\r\\nContent-Type: text/plain\\r\\nContent-Length: 6\\r\\n\\r\\nRSET\\r\\n", remoteEndpoint.Address.ToString(), remoteEndpoint.Port);
			}
			else
			{
				this.connectRequest.AppendFormat("CONNECT {0}:{1} HTTP/1.0\\r\\n\\r\\n", remoteEndpoint.Address.ToString(), remoteEndpoint.Port);
			}
			try
			{
				this.tcpCxn.SendString(this.connectRequest.ToString());
				this.state = CHTTPProxy.HTTPProxyState.RequestSent;
			}
			catch (AtsException)
			{
				base.ProxyChain.OnDisconnected();
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		public int OnDataReceived(byte[] dataReceived, int offset, int length)
		{
			int num = 0;
			while (length - num > 0)
			{
				CHTTPProxy.HTTPProxyState httpproxyState = this.state;
				if (httpproxyState != CHTTPProxy.HTTPProxyState.RequestSent)
				{
					return num;
				}
				string @string = Encoding.ASCII.GetString(dataReceived, offset + num, length - num);
				int num2 = @string.IndexOf("\r\n\r\n", StringComparison.OrdinalIgnoreCase);
				if (num2 == -1)
				{
					return num;
				}
				bool flag = false;
				this.state = CHTTPProxy.HTTPProxyState.Finished;
				string[] array = @string.Split(CHTTPProxy.delimiter, 3);
				if (array.Length > 1)
				{
					try
					{
						int num3 = int.Parse(array[1], null);
						if (num3 >= 200 && num3 < 300)
						{
							flag = true;
						}
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
				}
				num += num2 + 4;
				if (flag)
				{
					num += base.ProxyChain.OnConnected(dataReceived, offset + num, length - num);
				}
				else
				{
					base.ProxyChain.OnDisconnected();
				}
			}
			return num;
		}

		// Token: 0x04000196 RID: 406
		private static char[] delimiter = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x04000197 RID: 407
		private StringBuilder connectRequest;

		// Token: 0x04000198 RID: 408
		private bool fPost;

		// Token: 0x04000199 RID: 409
		private TcpConnection tcpCxn;

		// Token: 0x0400019A RID: 410
		private CHTTPProxy.HTTPProxyState state;

		// Token: 0x0200004A RID: 74
		private enum HTTPProxyState
		{
			// Token: 0x0400019C RID: 412
			Invalid,
			// Token: 0x0400019D RID: 413
			RequestSent,
			// Token: 0x0400019E RID: 414
			Finished
		}
	}
}
