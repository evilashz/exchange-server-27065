using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ProvisioningAssistant
{
	// Token: 0x02000131 RID: 305
	internal sealed class ProvisioningAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000C35 RID: 3125 RVA: 0x0004F4B8 File Offset: 0x0004D6B8
		public ProvisioningAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0004F4C3 File Offset: 0x0004D6C3
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxCreated) != (MapiEventTypeFlags)0 || (mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0004F4E4 File Offset: 0x0004D6E4
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			try
			{
				ADUser aduser;
				ProvisioningAssistant.UMProvisioningRequest provisioningRequest;
				if (!this.TryFindUser(mapiEvent, itemStore, out aduser))
				{
					ExTraceGlobals.ProvisioningAssistantTracer.TraceWarning<Guid>((long)this.GetHashCode(), "ProvisioningAssistant.HandleEventInternal. Could not find AD user with mailbox guid '{0}'.", mapiEvent.MailboxGuid);
				}
				else if (!this.TryCreateUMProvisioningRequest(mapiEvent, aduser, itemStore, out provisioningRequest))
				{
					ExTraceGlobals.ProvisioningAssistantTracer.TraceDebug<ADUser, Guid>((long)this.GetHashCode(), "ProvisioningAssistant.HandleEventInternal. AD user '{0}' with mailbox guid '{1}' doesn't need to be provisioned.", aduser, mapiEvent.MailboxGuid);
				}
				else
				{
					this.ProcessMailbox(provisioningRequest);
				}
			}
			catch (Exception error)
			{
				if (!this.TryHandleException(mapiEvent.MailboxGuid.ToString(), error))
				{
					throw;
				}
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0004F584 File Offset: 0x0004D784
		protected override void OnStartInternal(EventBasedStartInfo startInfo)
		{
			ExTraceGlobals.ProvisioningAssistantTracer.TraceDebug((long)this.GetHashCode(), "ProvisioningAssistant.OnStartInternal. ProvisioningAssistant starting.");
			base.OnStartInternal(startInfo);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0004F5A3 File Offset: 0x0004D7A3
		protected override void OnShutdownInternal()
		{
			ExTraceGlobals.ProvisioningAssistantTracer.TraceDebug((long)this.GetHashCode(), "ProvisioningAssistant.OnShutdownInternal. ProvisioningAssistant is stopping.");
			base.OnShutdownInternal();
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0004F5C4 File Offset: 0x0004D7C4
		private bool TryFindUser(MapiEvent mapiEvent, MailboxSession itemStore, out ADUser user)
		{
			IRecipientSession adrecipientSession = itemStore.GetADRecipientSession(false, ConsistencyMode.IgnoreInvalid);
			user = (adrecipientSession.FindByExchangeGuid(mapiEvent.MailboxGuid) as ADUser);
			return user != null;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0004F5F5 File Offset: 0x0004D7F5
		private bool TryCreateUMProvisioningRequest(MapiEvent mapiEvent, ADUser user, MailboxSession itemStore, out ProvisioningAssistant.UMProvisioningRequest provisioningRequest)
		{
			if (this.IsUMProvisioningRequested(mapiEvent, user))
			{
				provisioningRequest = new ProvisioningAssistant.MailboxCreatedUMProvisioningRequest(user, itemStore);
			}
			else if (this.IsUMPostMigrationProvisioningRequired(mapiEvent, user))
			{
				provisioningRequest = new ProvisioningAssistant.PostMigrationUMProvisioningRequest(user, itemStore);
			}
			else
			{
				provisioningRequest = null;
			}
			return provisioningRequest != null;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0004F630 File Offset: 0x0004D830
		private bool IsUMProvisioningRequested(MapiEvent mapiEvent, ADUser user)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxCreated) != (MapiEventTypeFlags)0 && user.UMEnabled && user.UMProvisioningRequested && user.RecipientTypeDetails != RecipientTypeDetails.MailboxPlan;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0004F663 File Offset: 0x0004D863
		private bool IsUMPostMigrationProvisioningRequired(MapiEvent mapiEvent, ADUser user)
		{
			return (mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0 && user.UMProvisioningRequested && user.RecipientTypeDetails != RecipientTypeDetails.MailboxPlan;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0004F690 File Offset: 0x0004D890
		private void ProcessMailbox(ProvisioningAssistant.UMProvisioningRequest provisioningRequest)
		{
			ExTraceGlobals.ProvisioningAssistantTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "ProvisioningAssistant.ProcessMailbox: Starting to process mailbox guid = {0} and display name = {1} in mbx db = {2}", provisioningRequest.MailboxSession.MailboxGuid, provisioningRequest.User.DisplayName, base.DatabaseInfo.DisplayName);
			provisioningRequest.DoWork();
			ExTraceGlobals.ProvisioningAssistantTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "ProvisioningAssistant.ProcessMailbox: Finished processing mailbox with guid = {0} and display name = {1} in mbx db = {2}", provisioningRequest.MailboxSession.MailboxGuid, provisioningRequest.User.DisplayName, base.DatabaseInfo.DisplayName);
			Globals.ProvisioningAssistantLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_ProvisioningAssitantProvisionedMailbox, null, new object[]
			{
				provisioningRequest.MailboxSession.MailboxGuid,
				provisioningRequest.User.DisplayName,
				base.DatabaseInfo.DisplayName
			});
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x0004F759 File Offset: 0x0004D959
		private bool ShouldLogException(Exception exception)
		{
			return !(exception is ObjectNotFoundException);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x0004F768 File Offset: 0x0004D968
		private bool TryHandleException(string mailboxGuid, Exception error)
		{
			ExTraceGlobals.ProvisioningAssistantTracer.TraceError<string, string, Exception>((long)this.GetHashCode(), "ProvisioningAssistant.TryHandleException: Mailbox guid = {0}, DB display name = {1} and Exception = {2}", mailboxGuid, base.DatabaseInfo.DisplayName, error);
			if (error is StorageTransientException || error is StoragePermanentException || error is LocalServerException || error is DataValidationException || error is DataSourceTransientException || error is DataSourceOperationException || error is UnableToGenerateValidPassword || error is UMRecipientValidationException || error is UmUserException || error is ADUMUserInvalidUMMailboxPolicyException)
			{
				if (this.ShouldLogException(error))
				{
					Globals.ProvisioningAssistantLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_ProvisioningAssistantFailedToProvisionMailbox, null, new object[]
					{
						mailboxGuid,
						base.DatabaseInfo.DisplayName,
						error
					});
				}
				throw new TransientMailboxException(error);
			}
			if (GrayException.IsGrayException(error))
			{
				ExWatson.SendReport(error, ReportOptions.None, null);
				return true;
			}
			return false;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0004F83A File Offset: 0x0004DA3A
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0004F842 File Offset: 0x0004DA42
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0004F84A File Offset: 0x0004DA4A
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x02000132 RID: 306
		private abstract class UMProvisioningRequest
		{
			// Token: 0x06000C44 RID: 3140 RVA: 0x0004F852 File Offset: 0x0004DA52
			public UMProvisioningRequest(ADUser user, MailboxSession mailboxSession)
			{
				this.user = user;
				this.mailboxSession = mailboxSession;
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0004F868 File Offset: 0x0004DA68
			public ADUser User
			{
				get
				{
					return this.user;
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0004F870 File Offset: 0x0004DA70
			public MailboxSession MailboxSession
			{
				get
				{
					return this.mailboxSession;
				}
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x0004F878 File Offset: 0x0004DA78
			public void DoWork()
			{
				this.InternalDoWork();
				this.User.UMProvisioningRequested = false;
				this.User.Session.Save(this.user);
			}

			// Token: 0x06000C48 RID: 3144
			protected abstract void InternalDoWork();

			// Token: 0x04000791 RID: 1937
			private readonly ADUser user;

			// Token: 0x04000792 RID: 1938
			private readonly MailboxSession mailboxSession;
		}

		// Token: 0x02000133 RID: 307
		private class MailboxCreatedUMProvisioningRequest : ProvisioningAssistant.UMProvisioningRequest
		{
			// Token: 0x06000C49 RID: 3145 RVA: 0x0004F8A2 File Offset: 0x0004DAA2
			public MailboxCreatedUMProvisioningRequest(ADUser user, MailboxSession mailboxSession) : base(user, mailboxSession)
			{
			}

			// Token: 0x06000C4A RID: 3146 RVA: 0x0004F8AC File Offset: 0x0004DAAC
			protected override void InternalDoWork()
			{
				byte[] validPassword = Utils.GetValidPassword(base.User);
				if (validPassword == null)
				{
					throw new UnableToGenerateValidPassword(base.User.DisplayName);
				}
				string @string = Encoding.ASCII.GetString(validPassword);
				Utils.SetUserPassword(base.MailboxSession, base.User, @string, true, false);
				Utils.InitUMMailbox(base.MailboxSession, base.User);
			}
		}

		// Token: 0x02000134 RID: 308
		private class PostMigrationUMProvisioningRequest : ProvisioningAssistant.UMProvisioningRequest
		{
			// Token: 0x06000C4B RID: 3147 RVA: 0x0004F90A File Offset: 0x0004DB0A
			public PostMigrationUMProvisioningRequest(ADUser user, MailboxSession mailboxSession) : base(user, mailboxSession)
			{
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x0004F914 File Offset: 0x0004DB14
			protected override void InternalDoWork()
			{
				if (base.User.UMEnabled)
				{
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.User.OrganizationId);
					UMDialPlan dialPlanFromId = iadsystemConfigurationLookup.GetDialPlanFromId(base.User.UMRecipientDialPlanId);
					string[] accessNumbers = ProvisioningAssistant.PostMigrationUMProvisioningRequest.GetAccessNumbers(dialPlanFromId);
					string extension = this.GetExtension(dialPlanFromId);
					Utils.SendWelcomeMail(base.User, accessNumbers, extension, Strings.UnchangedPIN, base.User.PrimarySmtpAddress.ToString(), base.MailboxSession);
					return;
				}
				Utils.ResetUMMailbox(base.User, true, base.MailboxSession);
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x0004F9AC File Offset: 0x0004DBAC
			private static string[] GetAccessNumbers(UMDialPlan dialPlan)
			{
				string[] result = null;
				if (dialPlan.AccessTelephoneNumbers != null && dialPlan.AccessTelephoneNumbers.Count > 0)
				{
					result = dialPlan.AccessTelephoneNumbers.ToArray();
				}
				return result;
			}

			// Token: 0x06000C4E RID: 3150 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
			private string GetExtension(UMDialPlan dialPlan)
			{
				string result = string.Empty;
				if (dialPlan.URIType == UMUriType.TelExtn)
				{
					result = base.User.UMExtension;
				}
				else
				{
					foreach (string text in base.User.Extensions)
					{
						PhoneNumber phoneNumber;
						if (PhoneNumber.TryParse(text, out phoneNumber) && phoneNumber.UriType == UMUriType.TelExtn)
						{
							result = text;
							break;
						}
					}
				}
				return result;
			}
		}
	}
}
