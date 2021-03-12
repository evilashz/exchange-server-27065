using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.GroupMailbox;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxAssociation
{
	// Token: 0x02000232 RID: 562
	internal sealed class MailboxAssociationReplicationAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x0600152F RID: 5423 RVA: 0x00078F0C File Offset: 0x0007710C
		public MailboxAssociationReplicationAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			MailboxAssociationReplicationAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "MailboxAssociationReplicationAssistant.MailboxAssociationReplicationAssistant");
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00078F2D File Offset: 0x0007712D
		public void OnWorkCycleCheckpoint()
		{
			MailboxAssociationReplicationAssistant.Tracer.TraceFunction((long)this.GetHashCode(), "MailboxAssociationReplicationAssistant.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00078F48 File Offset: 0x00077148
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			mailboxSession.Mailbox.Load(MailboxAssociationReplicationAssistantType.ExtendedProperties);
			if ((mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.UserMailbox && mailboxSession.MailboxOwner.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox) || mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				MailboxAssociationReplicationAssistant.Tracer.TraceError((long)this.GetHashCode(), "ActivityId : {0} ; Mailbox : TypeDetail {1} , SmtpAddress {2}, ExchangeGuid {3} cannot be processed by MailboxAssociationReplicationAssistant, Parameters {4}", new object[]
				{
					invokeArgs.ActivityId,
					mailboxSession.MailboxOwner.RecipientTypeDetails,
					mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
					mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid,
					invokeArgs.Parameters
				});
				customDataToLog.Add(new KeyValuePair<string, object>("ReplicationAssistantInfo", "Mailbox cannot be processed by MailboxAssociationReplicationAssistant"));
				return;
			}
			this.PerformDataReplication(mailboxSession, invokeArgs);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0007904A File Offset: 0x0007724A
		private static string GetOperationDescription(string parameters)
		{
			if (!string.IsNullOrWhiteSpace(parameters))
			{
				return "MailboxAssociationReplicationAssistant.RunNow";
			}
			return "MailboxAssociationReplicationAssistant.WorkCycle";
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00079060 File Offset: 0x00077260
		private static ICollection<IMailboxLocator> GetAssociationsToReplicate(string parameters, IRecipientSession adSession)
		{
			if (!string.IsNullOrEmpty(parameters))
			{
				RpcAssociationReplicatorRunNowParameters rpcAssociationReplicatorRunNowParameters = RpcAssociationReplicatorRunNowParameters.Parse(parameters, adSession);
				return rpcAssociationReplicatorRunNowParameters.SlaveMailboxes;
			}
			return null;
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00079274 File Offset: 0x00077474
		private void PerformDataReplication(MailboxSession mailboxSession, InvokeArgs invokeArgs)
		{
			IRecipientSession adSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, mailboxSession.GetADSessionSettings(), 139, "PerformDataReplication", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\MailboxAssociation\\MailboxAssociationReplicationAssistant.cs");
			string operationDescription = MailboxAssociationReplicationAssistant.GetOperationDescription(invokeArgs.Parameters);
			GroupMailboxAccessLayer.Execute(operationDescription, adSession, mailboxSession, delegate(GroupMailboxAccessLayer accessLayer)
			{
				ADObjectId objectId = invokeArgs.StoreSession.MailboxOwner.ObjectId;
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "MailboxAssociationReplicationAssistant.InvokeInternal - Replicating associations via MailboxAssistant for mailbox with ID {0}", objectId);
				ADUser aduser = adSession.FindADUserByObjectId(objectId);
				if (aduser == null)
				{
					string text = string.Format("MailboxAssociationReplicationAssistant.InvokeInternal - Couldn't find AdUser with AdObjectId {0}.", objectId);
					MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), text);
					LocalAssociationStore.SaveMailboxSyncStatus(mailboxSession, new ExDateTime?(ExDateTime.Now.Add(MailboxAssociationReplicationAssistant.TimeToWaitForAdReplication)), null);
					accessLayer.Logger.LogEvent(new SchemaBasedLogEvent<MailboxAssociationLogSchema.Error>
					{
						{
							MailboxAssociationLogSchema.Error.Context,
							"MailboxAssociationReplicationAssistant"
						},
						{
							MailboxAssociationLogSchema.Error.Exception,
							text
						}
					});
					return;
				}
				if (aduser.RecipientTypeDetails != RecipientTypeDetails.GroupMailbox && aduser.RecipientTypeDetails != RecipientTypeDetails.UserMailbox)
				{
					MailboxAssociationReplicationAssistant.Tracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "Skipping mailbox with guid {0} and display name {1} since this is a {2} and not a GroupMailbox or UserMailbox", mailboxSession.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.DisplayName, mailboxSession.MailboxOwner.RecipientTypeDetails.ToString());
					return;
				}
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "MailboxAssociationReplicationAssistant.InvokeInternal - Master Mailbox: ExternalID {0}, LegDN {1}", aduser.ExternalDirectoryObjectId, aduser.LegacyExchangeDN);
				ICollection<IMailboxLocator> associationsToReplicate = MailboxAssociationReplicationAssistant.GetAssociationsToReplicate(invokeArgs.Parameters, adSession);
				if (associationsToReplicate == null)
				{
					MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "MailboxAssociationReplicationAssistant.InvokeInternal - No locators provided in RunNow parameters, replicating all out-of-sync associations");
					this.ReplicateOutOfSyncAssociations(mailboxSession, aduser, adSession, accessLayer);
					return;
				}
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "MailboxAssociationReplicationAssistant.InvokeInternal - Found slave locators in RunNow parameters, replicating only provided associations");
				this.ReplicateAssociations(aduser, adSession, associationsToReplicate, accessLayer);
			});
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000792F8 File Offset: 0x000774F8
		private void ReplicateOutOfSyncAssociations(MailboxSession mailboxSession, ADUser masterAdUser, IRecipientSession adSession, GroupMailboxAccessLayer accessLayer)
		{
			mailboxSession.Mailbox.GetValueOrDefault<MailboxAssociationProcessingFlags>(MailboxSchema.MailboxAssociationProcessingFlags, MailboxAssociationProcessingFlags.None);
			if (masterAdUser.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Replicating associations for GroupMailbox via MailboxAssistant WorkCycle");
				GroupMailboxLocator masterLocator = GroupMailboxLocator.Instantiate(adSession, masterAdUser);
				accessLayer.ReplicateOutOfSyncAssociation(masterLocator);
				return;
			}
			if (masterAdUser.RecipientTypeDetails == RecipientTypeDetails.UserMailbox)
			{
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Replicating associations for UserMailbox via MailboxAssistant WorkCycle");
				UserMailboxLocator masterLocator2 = UserMailboxLocator.Instantiate(adSession, masterAdUser);
				accessLayer.ReplicateOutOfSyncAssociation(masterLocator2);
				return;
			}
			string message = string.Format("Replication of Mailbox Associations is not yet supported for mailbox of type {0}", masterAdUser.RecipientTypeDetails);
			MailboxAssociationReplicationAssistant.Tracer.TraceError((long)this.GetHashCode(), message);
			throw new NotImplementedException(message);
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000793B0 File Offset: 0x000775B0
		private void ReplicateAssociations(ADUser masterAdUser, IRecipientSession adSession, ICollection<IMailboxLocator> itemsToReplicate, GroupMailboxAccessLayer accessLayer)
		{
			if (masterAdUser.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox)
			{
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Replicating associations for GroupMailbox via MailboxAssistant.RunNow");
				GroupMailboxLocator masterLocator = GroupMailboxLocator.Instantiate(adSession, masterAdUser);
				UserMailboxLocator[] slaveLocators = itemsToReplicate.Cast<UserMailboxLocator>().ToArray<UserMailboxLocator>();
				accessLayer.ReplicateOutOfSyncAssociation(masterLocator, slaveLocators);
				return;
			}
			if (masterAdUser.RecipientTypeDetails == RecipientTypeDetails.UserMailbox)
			{
				MailboxAssociationReplicationAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "Replicating associations for UserMailbox via MailboxAssistant.RunNow");
				UserMailboxLocator masterLocator2 = UserMailboxLocator.Instantiate(adSession, masterAdUser);
				GroupMailboxLocator[] slaveLocators2 = itemsToReplicate.Cast<GroupMailboxLocator>().ToArray<GroupMailboxLocator>();
				accessLayer.ReplicateOutOfSyncAssociation(masterLocator2, slaveLocators2);
				return;
			}
			string message = string.Format("Replication of Mailbox Associations is not yet supported for mailbox of type {0}", masterAdUser.RecipientTypeDetails);
			MailboxAssociationReplicationAssistant.Tracer.TraceError((long)this.GetHashCode(), message);
			throw new NotImplementedException(message);
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00079490 File Offset: 0x00077690
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00079498 File Offset: 0x00077698
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000794A0 File Offset: 0x000776A0
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000CA9 RID: 3241
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationAssistantTracer;

		// Token: 0x04000CAA RID: 3242
		private static readonly TimeSpan TimeToWaitForAdReplication = TimeSpan.FromMinutes(15.0);
	}
}
