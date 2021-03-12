using System;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000189 RID: 393
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationWorkflowPosition : IMigrationSerializable
	{
		// Token: 0x0600125D RID: 4701 RVA: 0x0004DD5B File Offset: 0x0004BF5B
		public MigrationWorkflowPosition(MigrationStep step, MigrationStage stage)
		{
			this.Step = step;
			this.Stage = stage;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0004DD71 File Offset: 0x0004BF71
		private MigrationWorkflowPosition()
		{
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0004DD79 File Offset: 0x0004BF79
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0004DD81 File Offset: 0x0004BF81
		public MigrationStep Step { get; private set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0004DD8A File Offset: 0x0004BF8A
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0004DD92 File Offset: 0x0004BF92
		public MigrationStage Stage { get; private set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0004DD9B File Offset: 0x0004BF9B
		public PropertyDefinition[] PropertyDefinitions
		{
			get
			{
				return MigrationWorkflowPosition.MigrationWorkflowPositionProperties;
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x0004DDA2 File Offset: 0x0004BFA2
		public bool ReadFromMessageItem(IMigrationStoreObject message)
		{
			this.Stage = MigrationHelper.GetEnumProperty<MigrationStage>(message, MigrationBatchMessageSchema.MigrationStage);
			this.Step = MigrationHelper.GetEnumProperty<MigrationStep>(message, MigrationBatchMessageSchema.MigrationStep);
			return true;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004DDC7 File Offset: 0x0004BFC7
		public void WriteToMessageItem(IMigrationStoreObject message, bool loaded)
		{
			message[MigrationBatchMessageSchema.MigrationStep] = this.Step;
			message[MigrationBatchMessageSchema.MigrationStage] = this.Stage;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004DDF8 File Offset: 0x0004BFF8
		public XElement GetDiagnosticInfo(IMigrationDataProvider dataProvider, MigrationDiagnosticArgument argument)
		{
			XElement xelement = new XElement("MigrationWorkflowPosition");
			xelement.Add(new object[]
			{
				new XElement("Step", this.Step),
				new XElement("Stage", this.Stage)
			});
			return xelement;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0004DE5E File Offset: 0x0004C05E
		public override string ToString()
		{
			return this.Step + "," + this.Stage;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0004DE80 File Offset: 0x0004C080
		internal static MigrationWorkflowPosition CreateFromMessage(IMigrationStoreObject message)
		{
			MigrationWorkflowPosition migrationWorkflowPosition = new MigrationWorkflowPosition();
			migrationWorkflowPosition.ReadFromMessageItem(message);
			return migrationWorkflowPosition;
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0004DE9C File Offset: 0x0004C09C
		internal static IStepHandler CreateStepHandler(MigrationWorkflowPosition position, IMigrationDataProvider dataProvider, MigrationJob migrationJob)
		{
			MigrationStep step = position.Step;
			if (step <= MigrationStep.Provisioning)
			{
				if (step == MigrationStep.Initialization)
				{
					return new InitializationStepHandler(dataProvider);
				}
				if (step == MigrationStep.Provisioning)
				{
					return new ProvisioningStepHandler(dataProvider);
				}
			}
			else
			{
				if (step == MigrationStep.ProvisioningUpdate)
				{
					return new ProvisioningUpdateStepHandler(dataProvider);
				}
				if (step == MigrationStep.DataMigration)
				{
					return new DataMigrationStepHandler(dataProvider, migrationJob.MigrationType, migrationJob.JobName);
				}
			}
			throw new NotSupportedException(string.Format("Step {0} not yet supported", position.Step));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0004DF0C File Offset: 0x0004C10C
		internal static ISnapshotId GetStepSnapshotId(MigrationWorkflowPosition position, MigrationJobItem jobItem)
		{
			MigrationStep step = position.Step;
			if (step <= MigrationStep.Provisioning)
			{
				if (step == MigrationStep.Initialization)
				{
					goto IL_2F;
				}
				if (step != MigrationStep.Provisioning)
				{
					goto IL_2F;
				}
			}
			else if (step != MigrationStep.ProvisioningUpdate)
			{
				if (step != MigrationStep.DataMigration)
				{
					goto IL_2F;
				}
				return jobItem.SubscriptionId;
			}
			return jobItem.ProvisioningId;
			IL_2F:
			return null;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004DF4C File Offset: 0x0004C14C
		internal MigrationUserStatus GetInitialStatus()
		{
			MigrationStep step = this.Step;
			if (step <= MigrationStep.Provisioning)
			{
				if (step == MigrationStep.Initialization)
				{
					return MigrationUserStatus.Validating;
				}
				if (step == MigrationStep.Provisioning)
				{
					return MigrationUserStatus.Provisioning;
				}
			}
			else
			{
				if (step == MigrationStep.ProvisioningUpdate)
				{
					return MigrationUserStatus.ProvisionUpdating;
				}
				if (step == MigrationStep.DataMigration)
				{
					return MigrationUserStatus.Syncing;
				}
			}
			throw new NotSupportedException(string.Format("Step {0} not yet supported", this.Step));
		}

		// Token: 0x04000660 RID: 1632
		internal static readonly PropertyDefinition[] MigrationWorkflowPositionProperties = new PropertyDefinition[]
		{
			MigrationBatchMessageSchema.MigrationStep,
			MigrationBatchMessageSchema.MigrationStage
		};
	}
}
