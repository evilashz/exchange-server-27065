using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.GroupMailbox
{
	// Token: 0x02000230 RID: 560
	internal sealed class GroupMailboxAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001515 RID: 5397 RVA: 0x0007885E File Offset: 0x00076A5E
		public GroupMailboxAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			GroupMailboxAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "GroupMailboxAssistant.GroupMailboxAssistant");
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0007887F File Offset: 0x00076A7F
		public void OnWorkCycleCheckpoint()
		{
			GroupMailboxAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "GroupMailboxAssistant.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00078898 File Offset: 0x00076A98
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			mailboxSession.Mailbox.Load(GroupMailboxAssistantType.ExtendedProperties);
			if (mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox)
			{
				GroupMailboxAssistant.Tracer.TraceError((long)this.GetHashCode(), "ActivityId : {0} ; Mailbox : TypeDetail {1} , SmtpAddress {2}, ExchangeGuid {3} cannot be processed by GroupMailboxAssistant, Parameters {4}", new object[]
				{
					invokeArgs.ActivityId,
					mailboxSession.MailboxOwner.RecipientTypeDetails,
					mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid,
					invokeArgs.Parameters
				});
				customDataToLog.Add(new KeyValuePair<string, object>("GroupMailboxAssistantInfo", "Mailbox cannot be processed by GroupMailboxAssistant"));
				return;
			}
			this.UpdateGroupMailboxMembership(mailboxSession);
			IRecipientSession adrecipientSession = mailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent);
			ADUser adUser = adrecipientSession.FindADUserByObjectId(mailboxSession.MailboxOwner.ObjectId);
			this.UploadDefaultPhoto(adrecipientSession, adUser, invokeArgs, customDataToLog);
			this.PublishExchangeResourcesToAAD(adrecipientSession, adUser, invokeArgs, customDataToLog);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000789A8 File Offset: 0x00076BA8
		private void PublishExchangeResourcesToAAD(IRecipientSession adSession, ADUser adUser, InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			try
			{
				int? valueOrDefault = mailboxSession.Mailbox.GetValueOrDefault<int?>(MailboxSchema.GroupMailboxExchangeResourcesPublishedVersion, null);
				if (GroupMailboxAssistantType.IsGroupMailboxExchangeResourcesVersionOutdated(valueOrDefault, invokeArgs.MailboxData.MailboxGuid))
				{
					if (new GroupMailboxExchangeResourcesPublisher(adUser, default(Guid)).Publish(valueOrDefault))
					{
						mailboxSession.Mailbox[MailboxSchema.GroupMailboxExchangeResourcesPublishedVersion] = 1;
						mailboxSession.Mailbox.Save();
						mailboxSession.Mailbox.Load();
					}
				}
			}
			catch (LocalizedException ex)
			{
				GroupMailboxAssistant.Tracer.TraceError((long)this.GetHashCode(), "ActivityId : {0} ; Mailbox : TypeDetail {1} , SmtpAddress {2}, ExchangeGuid {3} cannot publish exchange resources, Parameters {4}. Exception: {5}", new object[]
				{
					invokeArgs.ActivityId,
					mailboxSession.MailboxOwner.RecipientTypeDetails,
					mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid,
					invokeArgs.Parameters,
					ex
				});
				customDataToLog.Add(new KeyValuePair<string, object>("PublishExchangeResourcesToAAD", string.Format("Cannot publish exchange resources to AAD for mailbox {0} exception {1}", adUser.ExternalDirectoryObjectId, ex)));
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x00078B00 File Offset: 0x00076D00
		private void UpdateGroupMailboxMembership(MailboxSession mailboxSession)
		{
			int? valueOrDefault = mailboxSession.Mailbox.GetValueOrDefault<int?>(MailboxSchema.GroupMailboxPermissionsVersion, null);
			if (!GroupMailboxAssistantType.IsGroupMailboxPermissionsVersionOutdated(valueOrDefault, mailboxSession.MailboxGuid))
			{
				return;
			}
			GroupMailboxMembershipUpdater groupMailboxMembershipUpdater = new GroupMailboxMembershipUpdater(mailboxSession);
			groupMailboxMembershipUpdater.Update();
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00078B44 File Offset: 0x00076D44
		private void UploadDefaultPhoto(IRecipientSession adSession, ADUser adUser, InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (!GroupMailboxDefaultPhotoUploader.IsFlightEnabled(mailboxSession))
			{
				return;
			}
			try
			{
				new GroupMailboxDefaultPhotoUploader(adSession, mailboxSession, adUser).UploadIfOutdated();
			}
			catch (LocalizedException ex)
			{
				GroupMailboxAssistant.Tracer.TraceError((long)this.GetHashCode(), "ActivityId : {0} ; Mailbox : TypeDetail {1} , SmtpAddress {2}, ExchangeGuid {3} cannot upload default photo for group, Parameters {4}. Exception: {5}", new object[]
				{
					invokeArgs.ActivityId,
					mailboxSession.MailboxOwner.RecipientTypeDetails,
					mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid,
					invokeArgs.Parameters,
					ex
				});
				customDataToLog.Add(new KeyValuePair<string, object>("GroupMailboxDefaultPhotoUploader", string.Format("A photo can not be auto-generated for Group {0}. Exception: {1}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress, ex)));
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00078C50 File Offset: 0x00076E50
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00078C58 File Offset: 0x00076E58
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00078C60 File Offset: 0x00076E60
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000CA5 RID: 3237
		private static readonly Trace Tracer = ExTraceGlobals.GroupMailboxAssistantTracer;
	}
}
