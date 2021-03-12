using System;
using System.Net;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverSubmission;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200001B RID: 27
	internal class MapiSubmissionItem : SubmissionItem
	{
		// Token: 0x0600011A RID: 282 RVA: 0x00008C5A File Offset: 0x00006E5A
		public MapiSubmissionItem(MapiSubmissionInfo mapiSubmissionInfo, MailItemSubmitter context, IStoreDriverTracer storeDriverTracer) : base("mapi", context, mapiSubmissionInfo, storeDriverTracer)
		{
			if (mapiSubmissionInfo == null)
			{
				throw new ArgumentNullException("mapiSubmissionInfo");
			}
			this.mapiSubmissionInfo = mapiSubmissionInfo;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00008C7F File Offset: 0x00006E7F
		public override bool Done
		{
			get
			{
				return this.done;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00008C87 File Offset: 0x00006E87
		public override string SourceServerFqdn
		{
			get
			{
				return this.mapiSubmissionInfo.MailboxFqdn;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00008C94 File Offset: 0x00006E94
		public override IPAddress SourceServerNetworkAddress
		{
			get
			{
				return this.mapiSubmissionInfo.NetworkAddress;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00008CA1 File Offset: 0x00006EA1
		public override DateTime OriginalCreateTime
		{
			get
			{
				return this.mapiSubmissionInfo.OriginalCreateTime;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008CAE File Offset: 0x00006EAE
		public override Exception DoneWithMessage()
		{
			this.done = true;
			return StoreProvider.CallDoneWithMessageWithRetry(base.Session, base.Item, 6, base.Context);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00008CD0 File Offset: 0x00006ED0
		public override uint LoadFromStore()
		{
			uint result = 0U;
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2615553341U);
			try
			{
				this.OpenStore();
				base.StoreDriverTracer.StoreDriverSubmissionTracer.TracePass(base.StoreDriverTracer.MessageProbeActivityId, 0L, "Bind to message item");
				StoreId storeId = StoreObjectId.FromProviderSpecificId(this.mapiSubmissionInfo.EntryId);
				ExDateTime dt = (base.Context == null) ? default(ExDateTime) : ExDateTime.UtcNow;
				try
				{
					base.Item = StoreProvider.GetMessageItem(base.Session, storeId, StoreObjectSchema.ContentConversionProperties);
				}
				finally
				{
					if (base.Context != null)
					{
						TimeSpan additionalLatency = ExDateTime.UtcNow - dt;
						base.Context.AddRpcLatency(additionalLatency, "Bind message");
					}
				}
				if (!base.IsSubmitMessage)
				{
					base.StoreDriverTracer.StoreDriverSubmissionTracer.TracePass(base.StoreDriverTracer.MessageProbeActivityId, 0L, "Notification recieved for a message that wasn't submitted");
					base.Dispose();
					result = 9U;
				}
			}
			catch (ObjectNotFoundException)
			{
				base.StoreDriverTracer.StoreDriverSubmissionTracer.TracePass(base.StoreDriverTracer.MessageProbeActivityId, 0L, "Message was deleted prior to MailSubmitted event processing completed");
				result = 6U;
			}
			catch (VirusMessageDeletedException)
			{
				base.StoreDriverTracer.StoreDriverSubmissionTracer.TracePass(base.StoreDriverTracer.MessageProbeActivityId, 0L, "Item was marked for deletion by a virus scanner.");
				result = 8U;
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00008E2C File Offset: 0x0000702C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					base.DisposeMessageItem();
					if (base.Session != null)
					{
						if (base.Context != null)
						{
							LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession, base.Context.LatencyTracker);
						}
						try
						{
							base.DisposeStoreSession();
						}
						finally
						{
							if (base.Context != null)
							{
								TimeSpan additionalLatency = LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreDisposeSession, base.Context.LatencyTracker);
								base.Context.AddRpcLatency(additionalLatency, "Session dispose");
							}
						}
					}
					if (base.Context != null)
					{
						MapiSubmissionInfo mapiSubmissionInfo = (MapiSubmissionInfo)base.Info;
						base.StoreDriverTracer.MapiStoreDriverSubmissionTracer.TracePass(base.StoreDriverTracer.MessageProbeActivityId, 0L, "Event {0}, Mailbox {1}, Mdb {2}, RPC latency {4}", new object[]
						{
							mapiSubmissionInfo.EventCounter,
							mapiSubmissionInfo.MailboxGuid,
							mapiSubmissionInfo.MdbGuid,
							base.Context.RpcLatency
						});
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00008F40 File Offset: 0x00007140
		private void OpenStore()
		{
			if (this.mapiSubmissionInfo.IsPublicFolder)
			{
				base.StoreDriverTracer.StoreDriverSubmissionTracer.TracePfdPass<int, string>(base.StoreDriverTracer.MessageProbeActivityId, 0L, "PFD ESD {0} Opening public store on {1}", 29595, this.mapiSubmissionInfo.MailboxServerDN);
				try
				{
					if (base.Context != null)
					{
						LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession, base.Context.LatencyTracker);
					}
					base.Session = StoreProvider.OpenStore(this.mapiSubmissionInfo.GetOrganizationId(), this.mapiSubmissionInfo.MailboxGuid);
					return;
				}
				finally
				{
					if (base.Context != null)
					{
						TimeSpan additionalLatency = LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionStoreOpenSession, base.Context.LatencyTracker);
						base.Context.AddRpcLatency(additionalLatency, "Open session");
					}
				}
			}
			base.StoreDriverTracer.StoreDriverSubmissionTracer.TracePfdPass(base.StoreDriverTracer.MessageProbeActivityId, 0L, "PFD ESD {0} Opening mailbox {1} on {2},{3}", new object[]
			{
				17307,
				this.mapiSubmissionInfo.MailboxGuid,
				this.mapiSubmissionInfo.MdbGuid,
				this.mapiSubmissionInfo.MailboxFqdn
			});
			ExDateTime dt = (base.Context == null) ? default(ExDateTime) : ExDateTime.UtcNow;
			try
			{
				base.Session = StoreProvider.OpenStore(this.mapiSubmissionInfo.GetOrganizationId(), "DummyName", this.mapiSubmissionInfo.MailboxFqdn, this.mapiSubmissionInfo.MailboxServerDN, this.mapiSubmissionInfo.MailboxGuid, this.mapiSubmissionInfo.MdbGuid, this.mapiSubmissionInfo.GetSenderLocales(), this.mapiSubmissionInfo.GetAggregatedMailboxGuids());
			}
			finally
			{
				if (base.Context != null)
				{
					TimeSpan additionalLatency2 = ExDateTime.UtcNow - dt;
					base.Context.AddRpcLatency(additionalLatency2, "Open session");
				}
			}
			base.SetSessionTimeZone();
		}

		// Token: 0x0400008D RID: 141
		private const string DummyDisplayName = "DummyName";

		// Token: 0x0400008E RID: 142
		private const int MaxRetry = 6;

		// Token: 0x0400008F RID: 143
		private static readonly Trace diag = ExTraceGlobals.StoreDriverSubmissionTracer;

		// Token: 0x04000090 RID: 144
		private MapiSubmissionInfo mapiSubmissionInfo;

		// Token: 0x04000091 RID: 145
		private bool done;
	}
}
