using System;
using System.Collections;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorDefinitions;
using Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorHelpers;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxIssueDetectors
{
	// Token: 0x02000236 RID: 566
	internal sealed class MailboxLockedDetector : IMailboxIssueDetector, IMailboxProcessor
	{
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00079634 File Offset: 0x00077834
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x0007963C File Offset: 0x0007783C
		bool IMailboxProcessor.IsEnabled { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00079645 File Offset: 0x00077845
		PropTag[] IMailboxProcessor.RequiredMailboxTableProperties
		{
			get
			{
				return MailboxLockedDetector.requiredMailboxTableProperties;
			}
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0007964C File Offset: 0x0007784C
		void IMailboxProcessor.OnStartWorkcycle()
		{
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0007964E File Offset: 0x0007784E
		void IMailboxProcessor.OnStopWorkcycle()
		{
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00079650 File Offset: 0x00077850
		void IMailboxProcessor.ProcessSingleMailbox(MailboxData mailboxData)
		{
			MailboxProcessorMailboxData mailboxData2 = mailboxData as MailboxProcessorMailboxData;
			if (((IMailboxIssueDetector)this).IsMailboxProblemDetected(mailboxData2))
			{
				((IMailboxIssueDetector)this).SubmitToRepair(((IMailboxIssueDetector)this).GetMailboxInformation(mailboxData2));
				return;
			}
			this.RemoveMailboxFromStatesCache(mailboxData2);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00079684 File Offset: 0x00077884
		bool IMailboxIssueDetector.IsMailboxProblemDetected(MailboxProcessorMailboxData mailboxData)
		{
			if (!mailboxData.IsMoveDestination)
			{
				return false;
			}
			ADUser aduserFromMailboxGuid;
			try
			{
				aduserFromMailboxGuid = ADHelper.GetADUserFromMailboxGuid(mailboxData.MailboxGuid, mailboxData.TenantPartitionHint);
			}
			catch (NonUniqueRecipientException)
			{
				MailboxProcessorAssistantType.Tracer.TraceError<Guid>((long)this.GetHashCode(), "Got AD duplicate by GUID: {0}", mailboxData.MailboxGuid);
				return false;
			}
			catch (ADTransientException)
			{
				return false;
			}
			if (aduserFromMailboxGuid == null || (mailboxData.IsArchive && aduserFromMailboxGuid.ArchiveDatabase == null) || aduserFromMailboxGuid.Database == null)
			{
				return false;
			}
			Guid a = mailboxData.IsArchive ? aduserFromMailboxGuid.ArchiveDatabase.ObjectGuid : aduserFromMailboxGuid.Database.ObjectGuid;
			if (a != mailboxData.DatabaseGuid)
			{
				MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox {0} is on the different database than AD user {1}. Not eligible for unlocking.", new object[]
				{
					mailboxData.MailboxGuid,
					aduserFromMailboxGuid.Guid
				});
				return false;
			}
			if (aduserFromMailboxGuid.MailboxMoveStatus != RequestStatus.Completed && aduserFromMailboxGuid.MailboxMoveStatus != RequestStatus.CompletedWithWarning && aduserFromMailboxGuid.MailboxMoveStatus != RequestStatus.None && aduserFromMailboxGuid.MailboxMoveStatus != RequestStatus.Failed)
			{
				MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox with GUID {0} is being moved and not yet completed or not yet failed.", new object[]
				{
					mailboxData.MailboxGuid
				});
				return false;
			}
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromMailboxGuid(aduserFromMailboxGuid.Session.SessionSettings, mailboxData.MailboxGuid, mailboxData.DatabaseGuid, RemotingOptions.LocalConnectionsOnly, null, false);
			try
			{
				string clientInfoString = string.Format("{0};Action={1}", "Client=TBA", base.GetType().Name);
				using (MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, clientInfoString, true))
				{
					MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Mailbox {0} is not locked. No repair needed.", new object[]
					{
						mailboxData.MailboxGuid
					});
				}
			}
			catch (MailboxInTransitException)
			{
				MailboxProcessorAssistantType.TraceInformation((long)this.GetHashCode(), "Found illegally locked mailbox with GUID {0}", new object[]
				{
					mailboxData.MailboxGuid
				});
				this.AddMailboxToStatesCache(mailboxData);
				return true;
			}
			catch (LocalizedException ex)
			{
				MailboxProcessorAssistantType.Tracer.TraceError((long)this.GetHashCode(), "{0} threw an exception of type {1} with message {2}. Was processing mailbox {3} on database {4}.", new object[]
				{
					base.GetType().ToString(),
					ex.GetType().ToString(),
					ex.Message,
					mailboxData.MailboxGuid,
					mailboxData.DatabaseName
				});
			}
			return false;
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00079920 File Offset: 0x00077B20
		MailboxProcessorNotificationEntry IMailboxIssueDetector.GetMailboxInformation(MailboxProcessorMailboxData mailboxData)
		{
			Guid externalDirectoryOrganizationId = (mailboxData.TenantPartitionHint != null) ? mailboxData.TenantPartitionHint.GetExternalDirectoryOrganizationId() : TenantPartitionHint.ExternalDirectoryOrganizationIdForRootOrg;
			return new MailboxProcessorNotificationEntry(mailboxData.MailboxGuid, mailboxData.DatabaseGuid, externalDirectoryOrganizationId, this.GetMailboxRetyCountFromStateCache(mailboxData));
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00079961 File Offset: 0x00077B61
		void IMailboxIssueDetector.SubmitToRepair(MailboxProcessorNotificationEntry detectedMailbox)
		{
			detectedMailbox.CreateMailboxProcessorEventNotificationItem(ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration.Name, "MailboxLocked").Publish(false);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00079988 File Offset: 0x00077B88
		public static XElement GetLockedMailboxesDiagnosticInfo()
		{
			XElement xelement = new XElement("MailboxLockedDetector");
			Hashtable hashtable;
			if (AssistantsService.MailboxesStates.TryGetValue(WorkloadType.MailboxProcessorAssistant, out hashtable))
			{
				foreach (object obj in hashtable.Keys)
				{
					XElement xelement2 = new XElement("LockedMailbox");
					xelement2.Add(new XElement("MailboxGuid", obj));
					xelement2.Add(new XElement("LockedDetectionCounter", hashtable[obj]));
					xelement.Add(xelement2);
				}
			}
			return xelement;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00079A4C File Offset: 0x00077C4C
		private void AddMailboxToStatesCache(MailboxProcessorMailboxData mailboxData)
		{
			int num = 1;
			Hashtable hashtable;
			if (AssistantsService.MailboxesStates.TryGetValue(WorkloadType.MailboxProcessorAssistant, out hashtable))
			{
				if (hashtable.ContainsKey(mailboxData.MailboxGuid))
				{
					num = (int)hashtable[mailboxData.MailboxGuid];
					num++;
				}
			}
			else
			{
				AssistantsService.MailboxesStates.Add(WorkloadType.MailboxProcessorAssistant, new Hashtable());
			}
			AssistantsService.MailboxesStates[WorkloadType.MailboxProcessorAssistant][mailboxData.MailboxGuid] = num;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00079AD0 File Offset: 0x00077CD0
		private void RemoveMailboxFromStatesCache(MailboxProcessorMailboxData mailboxData)
		{
			Hashtable hashtable;
			if (AssistantsService.MailboxesStates.TryGetValue(WorkloadType.MailboxProcessorAssistant, out hashtable) && hashtable.ContainsKey(mailboxData.MailboxGuid))
			{
				AssistantsService.MailboxesStates[WorkloadType.MailboxProcessorAssistant].Remove(mailboxData.MailboxGuid);
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00079B1C File Offset: 0x00077D1C
		private int GetMailboxRetyCountFromStateCache(MailboxProcessorMailboxData mailboxData)
		{
			int result = 0;
			Hashtable hashtable;
			if (AssistantsService.MailboxesStates.TryGetValue(WorkloadType.MailboxProcessorAssistant, out hashtable) && hashtable.ContainsKey(mailboxData.MailboxGuid))
			{
				result = (int)hashtable[mailboxData.MailboxGuid];
			}
			return result;
		}

		// Token: 0x04000CAE RID: 3246
		private static readonly PropTag[] requiredMailboxTableProperties = new PropTag[]
		{
			PropTag.MailboxNumber,
			PropTag.UserGuid,
			PropTag.MailboxMiscFlags,
			PropTag.PersistableTenantPartitionHint
		};
	}
}
