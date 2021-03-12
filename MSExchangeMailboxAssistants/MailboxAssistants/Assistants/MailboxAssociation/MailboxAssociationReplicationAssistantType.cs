using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.GroupMailbox;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxAssociation
{
	// Token: 0x02000233 RID: 563
	internal sealed class MailboxAssociationReplicationAssistantType : StoreAssistantType, ITimeBasedAssistantType, IAssistantType
	{
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000794A8 File Offset: 0x000776A8
		public TimeSpan WorkCycle
		{
			get
			{
				return AssistantConfiguration.MailboxAssociationReplicationWorkCycle.Read();
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x000794B4 File Offset: 0x000776B4
		public TimeSpan WorkCycleCheckpoint
		{
			get
			{
				return AssistantConfiguration.MailboxAssociationReplicationWorkCycleCheckpoint.Read();
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x000794C0 File Offset: 0x000776C0
		public TimeBasedAssistantIdentifier Identifier
		{
			get
			{
				return TimeBasedAssistantIdentifier.MailboxAssociationReplicationAssistant;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000794C4 File Offset: 0x000776C4
		public LocalizedString Name
		{
			get
			{
				return Strings.mailboxAssociationReplicationAssistantName;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000794CB File Offset: 0x000776CB
		public string NonLocalizedName
		{
			get
			{
				return "MailboxAssociationReplicationAssistant";
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x000794D2 File Offset: 0x000776D2
		public WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.MailboxAssociationReplicationAssistant;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000794D6 File Offset: 0x000776D6
		public PropertyTagPropertyDefinition[] MailboxExtendedProperties
		{
			get
			{
				return MailboxAssociationReplicationAssistantType.ExtendedProperties;
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000794E0 File Offset: 0x000776E0
		public bool IsMailboxInteresting(MailboxInformation mailboxInformation)
		{
			MailboxAssociationReplicationAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "MailboxAssociationReplicationAssistantType.IsMailboxInteresting");
			if (!mailboxInformation.IsGroupMailbox())
			{
				MailboxAssociationReplicationAssistantType.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "[{0}]: mailbox is not a group mailbox. No need to process it.", mailboxInformation.MailboxGuid);
				return false;
			}
			DateTime? nextReplicationTime = mailboxInformation.GetMailboxProperty(MailboxSchema.MailboxAssociationNextReplicationTime) as DateTime?;
			return MailboxAssociationReplicationAssistantType.IsMailboxNextSyncTimeDue(nextReplicationTime, mailboxInformation.MailboxGuid);
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x0007954B File Offset: 0x0007774B
		public PropertyTagPropertyDefinition ControlDataPropertyDefinition
		{
			get
			{
				return MailboxSchema.ControlDataForMailboxAssociationReplicationAssistant;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00079552 File Offset: 0x00077752
		public void OnWorkCycleCheckpoint()
		{
			MailboxAssociationReplicationAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "MailboxAssociationReplicationAssistantType.OnWorkCycleCheckpoint");
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x0007956A File Offset: 0x0007776A
		public ITimeBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			MailboxAssociationReplicationAssistantType.Tracer.TraceFunction((long)this.GetHashCode(), "MailboxAssociationReplicationAssistantType.CreateInstance");
			return new MailboxAssociationReplicationAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00079594 File Offset: 0x00077794
		internal static bool IsMailboxNextSyncTimeDue(DateTime? nextReplicationTime, Guid mailboxGuid)
		{
			if (nextReplicationTime == null)
			{
				MailboxAssociationReplicationAssistantType.Tracer.TraceDebug<Guid>(0L, "MailboxAssociationReplicationAssistantType.IsMailboxNextSyncTimeDue: Assuming mailbox is due for replication as couldn't find nextReplicationTime. Mailbox = {0}.", mailboxGuid);
				return true;
			}
			bool flag = nextReplicationTime < DateTime.UtcNow;
			MailboxAssociationReplicationAssistantType.Tracer.TraceDebug<Guid, DateTime?, bool>(0L, "MailboxAssociationReplicationAssistantType.IsMailboxNextSyncTimeDue. Mailbox = {0}, NextReplicationTime = {1}, DueForReplication = {2}.", mailboxGuid, nextReplicationTime, flag);
			return flag;
		}

		// Token: 0x04000CAB RID: 3243
		internal const string AssistantName = "MailboxAssociationReplicationAssistant";

		// Token: 0x04000CAC RID: 3244
		private static readonly Trace Tracer = ExTraceGlobals.AssociationReplicationAssistantTracer;

		// Token: 0x04000CAD RID: 3245
		internal static readonly PropertyTagPropertyDefinition[] ExtendedProperties = new PropertyTagPropertyDefinition[]
		{
			MailboxSchema.MailboxAssociationNextReplicationTime,
			MailboxSchema.MailboxAssociationProcessingFlags
		};
	}
}
