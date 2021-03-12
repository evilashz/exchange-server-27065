using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.MailboxProcessors;

namespace Microsoft.Exchange.MailboxLoadBalance.Logging
{
	// Token: 0x020000A6 RID: 166
	internal class ProvisioningConstraintFixStateLog : ObjectLog<ProvisioningConstraintFixStateLogEntry>
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x0000F6E6 File Offset: 0x0000D8E6
		private ProvisioningConstraintFixStateLog() : base(new ProvisioningConstraintFixStateLog.ProvisioningConstraintsFixStateLogSchema(), new LoadBalanceLoggingConfig("ProvisioningConstraintFixStates"))
		{
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000F700 File Offset: 0x0000D900
		public static void Write(DirectoryMailbox mailbox, DirectoryDatabase sourceDatabase, MoveInfo moveInfo)
		{
			ProvisioningConstraintFixStateLogEntry provisioningConstraintFixStateLogEntry = new ProvisioningConstraintFixStateLogEntry();
			provisioningConstraintFixStateLogEntry.MailboxGuid = mailbox.Guid;
			provisioningConstraintFixStateLogEntry.MailboxProvisioningHardConstraint = mailbox.MailboxProvisioningConstraints.HardConstraint;
			provisioningConstraintFixStateLogEntry.SourceDatabaseGuid = sourceDatabase.Guid;
			provisioningConstraintFixStateLogEntry.SourceDatabaseProvisioningAttributes = sourceDatabase.MailboxProvisioningAttributes;
			provisioningConstraintFixStateLogEntry.ExistingMoveStatus = moveInfo.Status;
			provisioningConstraintFixStateLogEntry.ExistingMoveRequestGuid = moveInfo.MoveRequestGuid;
			ProvisioningConstraintFixStateLog.Instance.LogObject(provisioningConstraintFixStateLogEntry);
		}

		// Token: 0x040001FC RID: 508
		private static readonly ProvisioningConstraintFixStateLog Instance = new ProvisioningConstraintFixStateLog();

		// Token: 0x020000A7 RID: 167
		private class ProvisioningConstraintFixStateLogData : ConfigurableObject
		{
			// Token: 0x060005D8 RID: 1496 RVA: 0x0000F777 File Offset: 0x0000D977
			public ProvisioningConstraintFixStateLogData(PropertyBag propertyBag) : base(propertyBag)
			{
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000F780 File Offset: 0x0000D980
			internal override ObjectSchema ObjectSchema
			{
				get
				{
					return new DummyObjectSchema();
				}
			}
		}

		// Token: 0x020000A8 RID: 168
		private class ProvisioningConstraintsFixStateLogSchema : ConfigurableObjectLogSchema<ProvisioningConstraintFixStateLog.ProvisioningConstraintFixStateLogData, DummyObjectSchema>
		{
			// Token: 0x170001ED RID: 493
			// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000F787 File Offset: 0x0000D987
			public override string LogType
			{
				get
				{
					return "Provisioning Constraint Fix States";
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000F78E File Offset: 0x0000D98E
			public override string Software
			{
				get
				{
					return "Mailbox Load Balancing";
				}
			}

			// Token: 0x040001FD RID: 509
			public static readonly ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry> MailboxGuid = new ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry>("MailboxGuid", (ProvisioningConstraintFixStateLogEntry r) => r.MailboxGuid);

			// Token: 0x040001FE RID: 510
			public static readonly ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry> ExistingMoveRequestGuid = new ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry>("ExistingMoveRequestGuid", (ProvisioningConstraintFixStateLogEntry r) => r.ExistingMoveRequestGuid);

			// Token: 0x040001FF RID: 511
			public static readonly ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry> ExistingMoveStatus = new ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry>("ExistingMoveStatus", (ProvisioningConstraintFixStateLogEntry r) => r.ExistingMoveStatus);

			// Token: 0x04000200 RID: 512
			public static readonly ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry> SourceDatabaseGuid = new ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry>("SourceDatabaseGuid", (ProvisioningConstraintFixStateLogEntry r) => r.SourceDatabaseGuid);

			// Token: 0x04000201 RID: 513
			public static readonly ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry> SourceDatabaseProvisioningAttributes = new ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry>("SourceDatabaseProvisioningAttributes", (ProvisioningConstraintFixStateLogEntry r) => r.SourceDatabaseProvisioningAttributes);

			// Token: 0x04000202 RID: 514
			public static readonly ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry> MailboxProvisioningHardConstraint = new ObjectLogSimplePropertyDefinition<ProvisioningConstraintFixStateLogEntry>("MailboxProvisioningHardConstraint", (ProvisioningConstraintFixStateLogEntry r) => r.MailboxProvisioningHardConstraint);
		}
	}
}
