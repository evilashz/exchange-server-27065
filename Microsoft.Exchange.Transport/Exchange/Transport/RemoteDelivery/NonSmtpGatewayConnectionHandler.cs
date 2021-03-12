using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003D0 RID: 976
	internal class NonSmtpGatewayConnectionHandler : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002CB0 RID: 11440 RVA: 0x000B2221 File Offset: 0x000B0421
		public static ExEventLog EventLogger
		{
			get
			{
				return NonSmtpGatewayConnectionHandler.eventLogger;
			}
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000B2228 File Offset: 0x000B0428
		public static void HandleConnection(NextHopConnection connection)
		{
			ExTraceGlobals.QueuingTracer.TraceDebug<string>(0L, "Invoking NonSmtpGatewayConnection for {0}", connection.Key.NextHopDomain);
			ThreadPool.QueueUserWorkItem(new WaitCallback(NonSmtpGatewayConnectionHandler.DeliveryCallback), connection);
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000B2267 File Offset: 0x000B0467
		public void Load()
		{
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000B2269 File Offset: 0x000B0469
		public void Unload()
		{
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x000B226B File Offset: 0x000B046B
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x000B226E File Offset: 0x000B046E
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000B2270 File Offset: 0x000B0470
		public void Pause()
		{
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000B2272 File Offset: 0x000B0472
		public void Continue()
		{
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000B2274 File Offset: 0x000B0474
		public void Stop()
		{
			ExTraceGlobals.QueuingTracer.TraceDebug(0L, "Shutdown called for Gateway Connection Handler");
			lock (NonSmtpGatewayConnectionHandler.syncObject)
			{
				foreach (NonSmtpGatewayConnection nonSmtpGatewayConnection in NonSmtpGatewayConnectionHandler.connections)
				{
					nonSmtpGatewayConnection.Retire();
				}
				goto IL_6B;
			}
			IL_61:
			Thread.Sleep(1000);
			IL_6B:
			if (NonSmtpGatewayConnectionHandler.connections.Count <= 0)
			{
				return;
			}
			goto IL_61;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000B2318 File Offset: 0x000B0518
		public string CurrentState
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(256);
				stringBuilder.Append("Connection count=");
				stringBuilder.AppendLine(NonSmtpGatewayConnectionHandler.connections.Count.ToString());
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000B235C File Offset: 0x000B055C
		private static void DeliveryCallback(object connection)
		{
			NextHopConnection nextHopConnection = connection as NextHopConnection;
			NonSmtpGatewayConnection nonSmtpGatewayConnection = null;
			try
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<string>(0L, "Initiating new outbound non SMTP Gateway connection for {0}", nextHopConnection.Key.NextHopDomain);
				lock (NonSmtpGatewayConnectionHandler.syncObject)
				{
					nonSmtpGatewayConnection = new NonSmtpGatewayConnection(nextHopConnection);
					NonSmtpGatewayConnectionHandler.connections.Add(nonSmtpGatewayConnection);
				}
				nonSmtpGatewayConnection.StartConnection();
			}
			catch (LocalizedException arg)
			{
				nextHopConnection.AckConnection(AckStatus.Retry, AckReason.UnexpectedException, null);
				ExTraceGlobals.QueuingTracer.TraceError<LocalizedException>(0L, "Unexpected exception while starting a non SMTP gateway connection . Exception text: {0}", arg);
			}
			finally
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<string>(0L, "Stop non smtp gateway delivery for connection to {0}", nextHopConnection.Key.NextHopDomain);
				lock (NonSmtpGatewayConnectionHandler.syncObject)
				{
					if (nonSmtpGatewayConnection != null)
					{
						NonSmtpGatewayConnectionHandler.connections.Remove(nonSmtpGatewayConnection);
					}
				}
			}
		}

		// Token: 0x04001651 RID: 5713
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.QueuingTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04001652 RID: 5714
		private static List<NonSmtpGatewayConnection> connections = new List<NonSmtpGatewayConnection>();

		// Token: 0x04001653 RID: 5715
		private static object syncObject = new object();
	}
}
