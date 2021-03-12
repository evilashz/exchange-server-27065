using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000024 RID: 36
	internal class DeliveryThrottlingAgent : SmtpReceiveAgent
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000A734 File Offset: 0x00008934
		internal DeliveryThrottlingAgent()
		{
			this.defaultConnectionWait = DeliveryConfiguration.Instance.Throttling.AcquireConnectionTimeout;
			base.OnConnect += this.ConnectHandler;
			base.OnEndOfData += this.EndOfDataEventHandler;
			base.OnXSessionParamsCommand += this.XSessionParamsCommandHandler;
			base.OnRcptCommand += this.RcptCommandHandler;
			base.OnDisconnect += this.DisconnectHandler;
			base.OnMailCommand += this.MailCommandHandler;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A7C8 File Offset: 0x000089C8
		private void ConnectHandler(ConnectEventSource source, ConnectEventArgs args)
		{
			DeliveryThrottlingAgent.Diag.TraceDebug(0, (long)this.GetHashCode(), "OnConnectHandler started");
			MSExchangeStoreDriver.LocalDeliveryCalls.Increment();
			if (!DeliveryThrottling.Instance.CheckAndTrackThrottleServer(args.SmtpSession.SessionId))
			{
				MSExchangeStoreDriver.DeliveryRetry.Increment();
				source.RejectConnection(AckReason.MailboxServerThreadLimitExceeded);
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000A824 File Offset: 0x00008A24
		private void XSessionParamsCommandHandler(ReceiveCommandEventSource source, XSessionParamsCommandEventArgs args)
		{
			DeliveryThrottlingAgent.Diag.TraceDebug(0, (long)this.GetHashCode(), "XSessionParamsCommandHandler started");
			if (DeliveryConfiguration.Instance.Throttling.DynamicMailboxDatabaseThrottlingEnabled)
			{
				DeliveryThrottling.Instance.ResetSession(args.SmtpSession.SessionId);
				this.destinationMdbGuid = args.DestinationMdbGuid;
				this.connectionManager = DeliveryThrottling.Instance.MailboxDatabaseCollectionManager.GetConnectionManager(args.DestinationMdbGuid);
				if (!DeliveryThrottling.Instance.CheckAndTrackDynamicThrottleMDBPendingConnections(this.destinationMdbGuid, this.connectionManager, args.SmtpSession.SessionId, args.SmtpSession.RemoteEndPoint.Address, out this.mdbHealthMonitors))
				{
					MSExchangeStoreDriver.DeliveryRetry.Increment();
					source.RejectCommand(AckReason.DynamicMailboxDatabaseThrottlingLimitExceeded);
					return;
				}
			}
			else if (!DeliveryThrottling.Instance.CheckAndTrackThrottleMDB(args.DestinationMdbGuid, args.SmtpSession.SessionId, out this.mdbHealthMonitors))
			{
				MSExchangeStoreDriver.DeliveryRetry.Increment();
				source.RejectCommand(AckReason.MailboxDatabaseThreadLimitExceeded);
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000A924 File Offset: 0x00008B24
		private void MailCommandHandler(ReceiveCommandEventSource source, MailCommandEventArgs args)
		{
			DeliveryThrottling.Instance.SetSessionMessageSize(args.Size, args.SmtpSession.SessionId);
			if (!DeliveryConfiguration.Instance.Throttling.DynamicMailboxDatabaseThrottlingEnabled)
			{
				return;
			}
			if (this.destinationMdbGuid == Guid.Empty || this.connectionManager == null)
			{
				DeliveryThrottlingAgent.Diag.TraceDebug<long>(0, (long)this.GetHashCode(), "Dynamic Throttling: No destination mailbox database specified for this connection, no throttling applied. SessionId {0}", args.SmtpSession.SessionId);
				return;
			}
			if (this.mailboxDatabaseConnectionInfo != null)
			{
				DeliveryThrottlingAgent.Diag.TraceDebug<long>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection was previously acquired. Being released before attempt to reacquire. SessionId {0}", args.SmtpSession.SessionId);
				this.connectionManager.Release(ref this.mailboxDatabaseConnectionInfo);
			}
			if (!DeliveryThrottling.Instance.CheckAndTrackDynamicThrottleMDBTimeout(this.destinationMdbGuid, this.mailboxDatabaseConnectionInfo, this.connectionManager, args.SmtpSession.SessionId, args.SmtpSession.RemoteEndPoint.Address, this.defaultConnectionWait, this.mdbHealthMonitors))
			{
				DeliveryThrottling.Instance.DecrementCurrentMessageSize(args.SmtpSession.SessionId);
				source.RejectCommand(AckReason.DynamicMailboxDatabaseThrottlingLimitExceeded);
			}
			this.connectionManager.UpdateLastActivityTime(args.SmtpSession.SessionId);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000AA54 File Offset: 0x00008C54
		private void RcptCommandHandler(ReceiveCommandEventSource source, RcptCommandEventArgs args)
		{
			DeliveryThrottlingAgent.Diag.TraceDebug(0, (long)this.GetHashCode(), "RcptCommandHandler started");
			if (!DeliveryThrottling.Instance.CheckAndTrackThrottleRecipient(args.RecipientAddress, args.SmtpSession.SessionId, this.destinationMdbGuid.ToString(), args.MailItem.TenantId))
			{
				MSExchangeStoreDriver.DeliveryRetry.Increment();
				source.RejectCommand(AckReason.RecipientThreadLimitExceeded);
			}
			if (this.connectionManager != null)
			{
				this.connectionManager.UpdateLastActivityTime(args.SmtpSession.SessionId);
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		private void DisconnectHandler(DisconnectEventSource source, DisconnectEventArgs args)
		{
			DeliveryThrottlingAgent.Diag.TraceDebug(0, (long)this.GetHashCode(), "DisconnectHandler started");
			DeliveryThrottling.Instance.ClearSession(args.SmtpSession.SessionId);
			if (DeliveryConfiguration.Instance.Throttling.DynamicMailboxDatabaseThrottlingEnabled && this.connectionManager != null)
			{
				if (this.mailboxDatabaseConnectionInfo != null)
				{
					DeliveryThrottlingAgent.Diag.TraceDebug<long>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection lock was previously acquired. Being released during disconnect. SessionId {0}", args.SmtpSession.SessionId);
					if (!this.connectionManager.Release(ref this.mailboxDatabaseConnectionInfo))
					{
						DeliveryThrottlingAgent.Diag.TraceWarning<long>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection lock was previously acquired but release returned false during disconnect. SessionId {0}", args.SmtpSession.SessionId);
					}
				}
				bool flag = this.connectionManager.RemoveConnection(args.SmtpSession.SessionId, args.SmtpSession.RemoteEndPoint.Address);
				this.connectionManager = null;
				if (flag)
				{
					DeliveryThrottlingAgent.Diag.TraceDebug<Guid, long, IPAddress>(0, (long)this.GetHashCode(), "Dynamic Throttling: Connection removed. MDB {0} SessionId {1} IP {2}", this.destinationMdbGuid, args.SmtpSession.SessionId, args.SmtpSession.RemoteEndPoint.Address);
					return;
				}
				DeliveryThrottlingAgent.Diag.TraceWarning<Guid, long, IPAddress>(0, (long)this.GetHashCode(), "Dynamic Throttling: Remove Connection returned false during disconnect. MDB {0} SessionId {1} IP {2}", this.destinationMdbGuid, args.SmtpSession.SessionId, args.SmtpSession.RemoteEndPoint.Address);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AC44 File Offset: 0x00008E44
		private void EndOfDataEventHandler(ReceiveMessageEventSource source, EndOfDataEventArgs args)
		{
			if (!DeliveryThrottling.Instance.CheckAndTrackThrottleConcurrentMessageSizeLimit(args.SmtpSession.SessionId, args.MailItem.Recipients.Count))
			{
				MSExchangeStoreDriver.DeliveryRetry.Increment();
				source.RejectMessage(AckReason.MaxConcurrentMessageSizeLimitExceeded);
				return;
			}
			string internetMessageId = args.MailItem.InternetMessageId;
			if (string.IsNullOrEmpty(internetMessageId))
			{
				DeliveryThrottlingAgent.Diag.TraceWarning(0, (long)this.GetHashCode(), "MessageId header is missing. Poison handling is disabled");
				return;
			}
			int crashCount = 0;
			if (DeliveryConfiguration.Instance.PoisonHandler.VerifyPoisonMessage(internetMessageId, out crashCount))
			{
				DeliveryThrottlingAgent.Diag.TraceError<string>(0, (long)this.GetHashCode(), "Poison message identified. Message ID: {0}", internetMessageId);
				source.RejectMessage(AckReason.InboundPoisonMessage(crashCount));
				return;
			}
			PoisonHandler<DeliveryPoisonContext>.Context = new DeliveryPoisonContext(internetMessageId);
		}

		// Token: 0x040000CC RID: 204
		private static readonly Trace Diag = ExTraceGlobals.StoreDriverDeliveryTracer;

		// Token: 0x040000CD RID: 205
		private readonly TimeSpan defaultConnectionWait;

		// Token: 0x040000CE RID: 206
		private Guid destinationMdbGuid;

		// Token: 0x040000CF RID: 207
		private List<KeyValuePair<string, double>> mdbHealthMonitors;

		// Token: 0x040000D0 RID: 208
		private IMailboxDatabaseConnectionManager connectionManager;

		// Token: 0x040000D1 RID: 209
		private IMailboxDatabaseConnectionInfo mailboxDatabaseConnectionInfo;
	}
}
