using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.OCS;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002C3 RID: 707
	internal class OCSPipelineContext : PipelineContext, IUMCAMessage, IUMResolveCaller
	{
		// Token: 0x0600156F RID: 5487 RVA: 0x0005BAC0 File Offset: 0x00059CC0
		internal OCSPipelineContext(string xmlData)
		{
			this.xmlData = xmlData;
			base.MessageType = "OCSNotification";
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0005BADC File Offset: 0x00059CDC
		protected OCSPipelineContext(UserNotificationEvent evt, string xmlData) : base(new SubmissionHelper(evt.CallId, evt.CallerId, evt.Subscriber.ADRecipient.Guid, evt.Subscriber.DisplayName, evt.Subscriber.TelephonyCulture.ToString(), null, null, evt.TenantGuid))
		{
			this.notifEvent = evt;
			base.MessageType = "OCSNotification";
			this.xmlData = xmlData;
			base.MessageID = Util.GenerateMessageIdFromSeed(evt.CallId);
			base.SentTime = evt.Time;
			base.InitializeCallerIdAndTryGetDialPlan(this.CAMessageRecipient);
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0005BB76 File Offset: 0x00059D76
		public UMRecipient CAMessageRecipient
		{
			get
			{
				if (this.notifEvent == null)
				{
					return null;
				}
				return this.notifEvent.Subscriber;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0005BB8D File Offset: 0x00059D8D
		public bool CollectMessageForAnalysis
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0005BB90 File Offset: 0x00059D90
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x0005BB98 File Offset: 0x00059D98
		public ContactInfo ContactInfo
		{
			get
			{
				return this.contactInfo;
			}
			set
			{
				this.contactInfo = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0005BBA1 File Offset: 0x00059DA1
		internal override Pipeline Pipeline
		{
			get
			{
				return TextPipeline.Instance;
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0005BBA8 File Offset: 0x00059DA8
		public override void PrepareUnProtectedMessage()
		{
			if (this.notifEvent.Subscriber.IsMissedCallNotificationEnabled)
			{
				base.PrepareUnProtectedMessage();
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0005BBC2 File Offset: 0x00059DC2
		public override string GetMailboxServerId()
		{
			return base.GetMailboxServerIdHelper();
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0005BBCA File Offset: 0x00059DCA
		public override string GetRecipientIdForThrottling()
		{
			return base.GetRecipientIdHelper();
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0005BBD4 File Offset: 0x00059DD4
		public override void PostCompletion()
		{
			if (this.notifEvent.Subscriber.IsMissedCallNotificationEnabled)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "OCSPipelineContext.PostCompletion - Incrementing OCS user event notifications counter", new object[0]);
				Util.IncrementCounter(GeneralCounters.OCSUserEventNotifications);
			}
			base.PostCompletion();
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0005BC24 File Offset: 0x00059E24
		internal static OCSPipelineContext Deserialize(string xmlData)
		{
			OCSPipelineContext result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				UserNotificationEvent userNotificationEvent = UserNotificationEvent.Deserialize(xmlData);
				disposeGuard.Add<UserNotificationEvent>(userNotificationEvent);
				OCSPipelineContext ocspipelineContext = new OCSPipelineContext(userNotificationEvent, xmlData);
				disposeGuard.Add<OCSPipelineContext>(ocspipelineContext);
				PipelineSubmitStatus pipelineSubmitStatus = PipelineDispatcher.Instance.CanSubmitWorkItem(userNotificationEvent.Subscriber.ADUser.ServerLegacyDN, userNotificationEvent.Subscriber.ADUser.DistinguishedName, PipelineDispatcher.ThrottledWorkItemType.NonCDRWorkItem);
				if (pipelineSubmitStatus != PipelineSubmitStatus.Ok)
				{
					string distinguishedName = userNotificationEvent.Subscriber.ADUser.DistinguishedName;
					throw new PipelineFullException(distinguishedName);
				}
				disposeGuard.Success();
				result = ocspipelineContext;
			}
			return result;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0005BCD0 File Offset: 0x00059ED0
		internal override void WriteCustomHeaderFields(StreamWriter headerStream)
		{
			headerStream.WriteLine("OCSNotificationData : " + this.xmlData);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0005BCE8 File Offset: 0x00059EE8
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this.GetHashCode(), "OCSPipelineContext.Dispose() called", new object[0]);
					if (this.notifEvent != null)
					{
						this.notifEvent.Dispose();
						this.notifEvent = null;
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0005BD4C File Offset: 0x00059F4C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OCSPipelineContext>(this);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0005BD54 File Offset: 0x00059F54
		protected override void SetMessageProperties()
		{
			base.SetMessageProperties();
			this.notifEvent.RenderMessage(base.MessageToSubmit, this.ContactInfo);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0005BD73 File Offset: 0x00059F73
		protected override void WriteCommonHeaderFields(StreamWriter headerStream)
		{
		}

		// Token: 0x04000CDA RID: 3290
		private string xmlData;

		// Token: 0x04000CDB RID: 3291
		private UserNotificationEvent notifEvent;

		// Token: 0x04000CDC RID: 3292
		private ContactInfo contactInfo;
	}
}
