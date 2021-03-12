using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E2 RID: 738
	internal class SearchFolderVerificationStage : SynchronousPipelineStageBase, IUMNetworkResource
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0005F7A8 File Offset: 0x0005D9A8
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x0005F7AB File Offset: 0x0005D9AB
		public string NetworkResourceId
		{
			get
			{
				return base.WorkItem.Message.GetMailboxServerId();
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0005F7BD File Offset: 0x0005D9BD
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0005F7CD File Offset: 0x0005D9CD
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.SkipStage);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0005F7D8 File Offset: 0x0005D9D8
		protected override void InternalDoSynchronousWork()
		{
			try
			{
				IUMCAMessage iumcamessage = base.WorkItem.Message as IUMCAMessage;
				ExAssert.RetailAssert(iumcamessage != null, "SearchFolderVerificationStage must operate only on PipelineContext which implements IUMCAMessage");
				UMSubscriber umsubscriber = iumcamessage.CAMessageRecipient as UMSubscriber;
				if (umsubscriber != null)
				{
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = umsubscriber.CreateSessionLock())
					{
						using (UMSearchFolder umsearchFolder = UMSearchFolder.Get(mailboxSessionLock.Session, UMSearchFolder.Type.VoiceMail))
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Invoking Get SearchFolder Criteria= {0}", new object[]
							{
								umsearchFolder.SearchFolder.GetSearchCriteria().ToString()
							});
						}
						using (UMSearchFolder umsearchFolder2 = UMSearchFolder.Get(mailboxSessionLock.Session, UMSearchFolder.Type.Fax))
						{
							if (umsearchFolder2.SearchFolderId != null)
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "Invoking Get SearchFolder Criteria= {0}", new object[]
								{
									umsearchFolder2.SearchFolder.GetSearchCriteria().ToString()
								});
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "SearchFolderVerificationStage exception = {0}", new object[]
				{
					ex
				});
				throw;
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0005F940 File Offset: 0x0005DB40
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SearchFolderVerificationStage>(this);
		}
	}
}
