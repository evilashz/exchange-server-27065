using System;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Agent.Hygiene;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200004C RID: 76
	internal sealed class ProxyChain : IDataConnection
	{
		// Token: 0x06000244 RID: 580 RVA: 0x0000F5F1 File Offset: 0x0000D7F1
		public ProxyChain(ProxyEndPoint[] remote, ProxyTest proxyTest, string greetingBanner)
		{
			this.proxyTest = proxyTest;
			this.proxyEndpoints = remote;
			this.currentPos = 0;
			this.matchingBanner = greetingBanner;
			this.detectionTimeout = new Timer(new TimerCallback(ProxyChain.OpenProxyDetectionTimeout), this, -1, -1);
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000F62F File Offset: 0x0000D82F
		public IPEndPoint RemoteEndpoint
		{
			get
			{
				return this.proxyEndpoints[this.proxyEndpoints.Length - 2].Endpoint;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000F648 File Offset: 0x0000D848
		public int OnDataReceived(byte[] buffer, int offset, int size)
		{
			if (size < this.matchingBanner.Length)
			{
				return 0;
			}
			OPDetectionResult result = OPDetectionResult.NotOpenProxy;
			string @string = Encoding.ASCII.GetString(buffer, offset, size);
			if (@string.StartsWith(this.matchingBanner, StringComparison.OrdinalIgnoreCase))
			{
				result = OPDetectionResult.IsOpenProxy;
			}
			this.DetectionComplete(result);
			return size;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000F690 File Offset: 0x0000D890
		public int OnConnected(byte[] dataReceived, int offset, int length)
		{
			if (this.currentPos < this.proxyEndpoints.Length)
			{
				int num = this.currentPos;
				TcpConnection tcpConnection = null;
				lock (this)
				{
					this.currentPos++;
					this.dataCxn = this.CreateDataCxn(this.proxyEndpoints[num].Type);
					if (this.tcpCxn != null)
					{
						this.tcpCxn.DataCxn = this.dataCxn;
					}
					tcpConnection = this.tcpCxn;
				}
				if (tcpConnection != null)
				{
					((TransportConnection)this.dataCxn).AsyncConnect(this.proxyEndpoints[num].Endpoint, this.tcpCxn, this.proxyEndpoints[num].AuthInfo);
				}
			}
			else
			{
				lock (this)
				{
					if (this.tcpCxn != null)
					{
						this.tcpCxn.DataCxn = this;
					}
				}
				if (dataReceived != null)
				{
					return this.OnDataReceived(dataReceived, offset, length);
				}
			}
			return 0;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public void OnDisconnected()
		{
			this.DetectionComplete(OPDetectionResult.NotOpenProxy);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public void DetectOpenProxy(int timeoutPeriod)
		{
			if (this.proxyEndpoints[0].Type != ProxyType.None)
			{
				throw new LocalizedException(AgentStrings.InvalidProxyChain);
			}
			this.detectionTimeout.Change(timeoutPeriod, -1);
			this.tcpCxn = new TcpConnection(this);
			this.currentPos++;
			this.tcpCxn.DataCxn = this;
			this.tcpCxn.AsyncConnect(this.proxyEndpoints[0].Endpoint, null, this.proxyEndpoints[0].AuthInfo);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000F838 File Offset: 0x0000DA38
		private static void OpenProxyDetectionTimeout(object state)
		{
			ProxyChain proxyChain = (ProxyChain)state;
			OPDetectionResult result = OPDetectionResult.Unknown;
			lock (proxyChain)
			{
				if (proxyChain.tcpCxn != null && proxyChain.tcpCxn.RemoteReachable)
				{
					result = OPDetectionResult.NotOpenProxy;
				}
			}
			proxyChain.DetectionComplete(result);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000F894 File Offset: 0x0000DA94
		private IDataConnection CreateDataCxn(ProxyType type)
		{
			switch (type)
			{
			case ProxyType.Socks4:
				return new CSocks4Proxy(this);
			case ProxyType.Socks5:
				return new CSocks5Proxy(this);
			case ProxyType.HttpConnect:
				return new CHTTPProxy(false, this);
			case ProxyType.HttpPost:
				return new CHTTPProxy(true, this);
			case ProxyType.Telnet:
				return new CTelnetProxy(false, this);
			case ProxyType.Cisco:
				return new CTelnetProxy(true, this);
			case ProxyType.Wingate:
				return new CWinGateProxy(this);
			default:
				throw new LocalizedException(AgentStrings.InvalidOpenProxyType);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000F908 File Offset: 0x0000DB08
		private void DetectionComplete(OPDetectionResult result)
		{
			bool flag = false;
			lock (this)
			{
				if (this.detectionTimeout != null)
				{
					try
					{
						this.detectionTimeout.Change(-1, -1);
						this.tcpCxn.Shutdown(false);
					}
					finally
					{
						this.detectionTimeout.Dispose();
						this.detectionTimeout = null;
						this.tcpCxn.Dispose();
						this.tcpCxn = null;
					}
					flag = true;
				}
			}
			if (flag)
			{
				this.proxyTest.DetectionChainResult(result, this.proxyEndpoints[this.proxyEndpoints.Length - 1].Type, this.proxyEndpoints[this.proxyEndpoints.Length - 2].Endpoint.Port);
			}
		}

		// Token: 0x0400019F RID: 415
		private TcpConnection tcpCxn;

		// Token: 0x040001A0 RID: 416
		private IDataConnection dataCxn;

		// Token: 0x040001A1 RID: 417
		private ProxyTest proxyTest;

		// Token: 0x040001A2 RID: 418
		private string matchingBanner;

		// Token: 0x040001A3 RID: 419
		private Timer detectionTimeout;

		// Token: 0x040001A4 RID: 420
		private ProxyEndPoint[] proxyEndpoints;

		// Token: 0x040001A5 RID: 421
		private int currentPos;
	}
}
