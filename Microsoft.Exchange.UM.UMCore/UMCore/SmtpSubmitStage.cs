using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020002E5 RID: 741
	internal class SmtpSubmitStage : SubmitStage, IUMNetworkResource
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005FCE8 File Offset: 0x0005DEE8
		internal override PipelineDispatcher.PipelineResourceType ResourceType
		{
			get
			{
				return PipelineDispatcher.PipelineResourceType.NetworkBound;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x0005FCEB File Offset: 0x0005DEEB
		public string NetworkResourceId
		{
			get
			{
				return "70cb6c39-83d9-4123-8013-d95aadda7712";
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0005FCF2 File Offset: 0x0005DEF2
		internal override TimeSpan ExpectedRunTime
		{
			get
			{
				return TimeSpan.FromMinutes(5.0);
			}
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0005FD02 File Offset: 0x0005DF02
		internal override void Initialize(PipelineWorkItem workItem)
		{
			base.Initialize(workItem);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x0005FD0C File Offset: 0x0005DF0C
		protected override void InternalDoSynchronousWork()
		{
			IUMCAMessage iumcamessage = base.WorkItem.Message as IUMCAMessage;
			ExAssert.RetailAssert(iumcamessage != null, "SmtpSubmitStage must operate on PipelineContext which implements IUMCAMessage");
			UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = null;
			try
			{
				UMRecipient camessageRecipient = iumcamessage.CAMessageRecipient;
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(camessageRecipient.ADRecipient.OrganizationId);
				MicrosoftExchangeRecipient microsoftExchangeRecipient = iadsystemConfigurationLookup.GetMicrosoftExchangeRecipient();
				if (base.MessageToSubmit != null)
				{
					VoiceMessagePipelineContext voiceMessagePipelineContext = base.WorkItem.Message as VoiceMessagePipelineContext;
					if (voiceMessagePipelineContext != null && voiceMessagePipelineContext.IsThisAProtectedMessage)
					{
						mailboxSessionLock = ((UMMailboxRecipient)voiceMessagePipelineContext.CAMessageRecipient).CreateSessionLock();
						if (mailboxSessionLock.Session == null || mailboxSessionLock.UnderlyingStoreRPCSessionDisconnected)
						{
							throw new WorkItemNeedsToBeRequeuedException();
						}
						base.MessageToSubmit.Load(StoreObjectSchema.ContentConversionProperties);
					}
					AcceptedDomain defaultAcceptedDomain = Utils.GetDefaultAcceptedDomain(camessageRecipient.ADRecipient);
					OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(defaultAcceptedDomain.DomainName.ToString());
					outboundConversionOptions.UserADSession = ADRecipientLookupFactory.CreateFromUmUser(camessageRecipient).ScopedRecipientSession;
					SmtpSubmissionHelper.SubmitMessage(base.MessageToSubmit, microsoftExchangeRecipient.PrimarySmtpAddress.ToString(), base.WorkItem.Message.TenantGuid, camessageRecipient.MailAddress, outboundConversionOptions, Path.GetFileNameWithoutExtension(base.WorkItem.HeaderFilename));
				}
			}
			finally
			{
				if (mailboxSessionLock != null)
				{
					mailboxSessionLock.Dispose();
				}
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x0005FE58 File Offset: 0x0005E058
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SmtpSubmitStage>(this);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0005FE60 File Offset: 0x0005E060
		protected override StageRetryDetails InternalGetRetrySchedule()
		{
			return new StageRetryDetails(StageRetryDetails.FinalAction.DropMessage, TimeSpan.FromSeconds(30.0), TimeSpan.FromDays(1.0));
		}
	}
}
