using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Messages;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000003 RID: 3
	internal sealed class CheckExchangeRpcServiceResponsive : BaseObject
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020FC File Offset: 0x000002FC
		public CheckExchangeRpcServiceResponsive(ExEventLog eventLog)
		{
			this.eventLog = eventLog;
			this.wasExchangeRpcServiceResponsive = true;
			this.localIpAddresses = Dns.GetHostAddresses(Dns.GetHostName());
			this.ipAddressFamily = this.GetAddressFamily();
			this.periodicCheckIfExchangeRpcServiceIsResponsiveTimer = new MaintenanceJobTimer(new Action(this.CheckIfExchangeRpcServiceIsResponsive), () => Configuration.ServiceConfiguration.WaitBetweenTcpConnectToFindIfRpcServiceResponsive != TimeSpan.Zero, Configuration.ServiceConfiguration.WaitBetweenTcpConnectToFindIfRpcServiceResponsive, TimeSpan.Zero);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000217C File Offset: 0x0000037C
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CheckExchangeRpcServiceResponsive>(this);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002184 File Offset: 0x00000384
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.periodicCheckIfExchangeRpcServiceIsResponsiveTimer);
			base.InternalDispose();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		private void CheckIfExchangeRpcServiceIsResponsive()
		{
			bool flag = this.TcpConnect(this.localIpAddresses, 6001, this.ipAddressFamily);
			if (!flag && !this.wasExchangeRpcServiceResponsive)
			{
				ExTraceGlobals.ConnectRpcTracer.TraceDebug(Activity.TraceId, "Restart ExchangeRpcService process because a possible deadlock is detected");
				this.eventLog.LogEvent(RpcClientAccessServiceEventLogConstants.Tuple_RpcClientAccessServiceDeadlocked, string.Empty, new object[]
				{
					string.Empty
				});
				Environment.Exit(-559034354);
			}
			this.wasExchangeRpcServiceResponsive = flag;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002214 File Offset: 0x00000414
		private bool TcpConnect(IPAddress[] localIpAddresses, int port, AddressFamily addressFamily)
		{
			TcpClient tcpClient = new TcpClient(addressFamily);
			bool result;
			try
			{
				tcpClient.Connect(localIpAddresses, port);
				result = true;
			}
			catch (SocketException)
			{
				result = false;
			}
			catch (ObjectDisposedException)
			{
				result = false;
			}
			finally
			{
				tcpClient.Close();
			}
			return result;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002270 File Offset: 0x00000470
		private AddressFamily GetAddressFamily()
		{
			bool flag = false;
			foreach (IPAddress ipaddress in this.localIpAddresses)
			{
				if (AddressFamily.InterNetworkV6 == ipaddress.AddressFamily)
				{
					flag = true;
					break;
				}
				AddressFamily addressFamily = ipaddress.AddressFamily;
			}
			if (!flag)
			{
				return AddressFamily.InterNetwork;
			}
			return AddressFamily.InterNetworkV6;
		}

		// Token: 0x04000001 RID: 1
		private readonly ExEventLog eventLog;

		// Token: 0x04000002 RID: 2
		private readonly MaintenanceJobTimer periodicCheckIfExchangeRpcServiceIsResponsiveTimer;

		// Token: 0x04000003 RID: 3
		private readonly IPAddress[] localIpAddresses;

		// Token: 0x04000004 RID: 4
		private readonly AddressFamily ipAddressFamily;

		// Token: 0x04000005 RID: 5
		private bool wasExchangeRpcServiceResponsive;
	}
}
