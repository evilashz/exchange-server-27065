using System;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x0200000E RID: 14
	internal sealed class StoreDriver : IStoreDriver, IDiagnosable
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003ACF File Offset: 0x00001CCF
		public static IPHostEntry LocalIP
		{
			get
			{
				return StoreDriver.localIp;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003AD6 File Offset: 0x00001CD6
		public static IPAddress LocalIPAddress
		{
			get
			{
				return StoreDriver.localIp.AddressList[0];
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003AE4 File Offset: 0x00001CE4
		public static string ReceivedHeaderTcpInfo
		{
			get
			{
				return StoreDriver.receivedHeaderTcpInfo;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003AEB File Offset: 0x00001CEB
		public static IStoreDriver CreateStoreDriver()
		{
			StoreDriver.InitializeIPInfo();
			return null;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003AF3 File Offset: 0x00001CF3
		public static string FormatIPAddress(IPAddress address)
		{
			return "[" + address.ToString() + "]";
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003B0A File Offset: 0x00001D0A
		public void Retire()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003B0C File Offset: 0x00001D0C
		public void Start(bool initiallyPaused)
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003B0E File Offset: 0x00001D0E
		public void Stop()
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B10 File Offset: 0x00001D10
		public void Pause()
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003B12 File Offset: 0x00001D12
		public void Continue()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003B14 File Offset: 0x00001D14
		public void DoLocalDelivery(NextHopConnection connection)
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003B16 File Offset: 0x00001D16
		public void ExpireOldSubmissionConnections()
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003B18 File Offset: 0x00001D18
		public string CurrentState
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003B1F File Offset: 0x00001D1F
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return string.Empty;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003B26 File Offset: 0x00001D26
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return null;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003B80 File Offset: 0x00001D80
		private static void InitializeIPInfo()
		{
			lock (StoreDriver.syncObject)
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					try
					{
						StoreDriver.localIp = Dns.GetHostEntry(Dns.GetHostName());
					}
					catch (SocketException ex)
					{
						throw new TransportComponentLoadFailedException(ex.Message, ex);
					}
					StoreDriver.receivedHeaderTcpInfo = StoreDriver.FormatIPAddress(StoreDriver.localIp.AddressList[0]);
				}, 1);
			}
		}

		// Token: 0x04000049 RID: 73
		private static IPHostEntry localIp;

		// Token: 0x0400004A RID: 74
		private static string receivedHeaderTcpInfo;

		// Token: 0x0400004B RID: 75
		private static StoreDriver instance = new StoreDriver();

		// Token: 0x0400004C RID: 76
		private static object syncObject = new object();
	}
}
