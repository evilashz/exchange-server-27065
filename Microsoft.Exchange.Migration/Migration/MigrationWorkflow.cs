using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000188 RID: 392
	public sealed class MigrationWorkflow : XMLSerializableBase
	{
		// Token: 0x06001251 RID: 4689 RVA: 0x0004DA27 File Offset: 0x0004BC27
		public MigrationWorkflow(MigrationWorkflowStep[] workflowSteps)
		{
			MigrationUtil.ThrowOnCollectionEmptyArgument(workflowSteps, "workflowSteps");
			this.Steps = workflowSteps;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0004DA41 File Offset: 0x0004BC41
		public MigrationWorkflow()
		{
			this.Steps = null;
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0004DA50 File Offset: 0x0004BC50
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x0004DA58 File Offset: 0x0004BC58
		[XmlArray("Workflow")]
		[XmlArrayItem("Step")]
		public MigrationWorkflowStep[] Steps { get; set; }

		// Token: 0x06001255 RID: 4693 RVA: 0x0004DA61 File Offset: 0x0004BC61
		internal static MigrationWorkflow Deserialize(string content)
		{
			return XMLSerializableBase.Deserialize<MigrationWorkflow>(content, true);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0004DA6A File Offset: 0x0004BC6A
		internal MigrationWorkflowPosition GetInitialPosition()
		{
			if (this.Steps == null || this.Steps.Length < 1)
			{
				return null;
			}
			return new MigrationWorkflowPosition(this.Steps[0].Step, this.Steps[0].AllowedStages[0]);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
		internal MigrationWorkflowPosition GetNextPosition(MigrationWorkflowPosition position, MigrationStep[] supportedSteps)
		{
			if (this.Steps == null || this.Steps.Length < 1)
			{
				return null;
			}
			bool flag = false;
			foreach (MigrationWorkflowStep migrationWorkflowStep in this.Steps)
			{
				if (supportedSteps.Contains(migrationWorkflowStep.Step))
				{
					if (flag || migrationWorkflowStep.Step == position.Step)
					{
						foreach (MigrationStage migrationStage in migrationWorkflowStep.AllowedStages)
						{
							if (flag)
							{
								return new MigrationWorkflowPosition(migrationWorkflowStep.Step, migrationStage);
							}
							if (migrationStage == position.Stage)
							{
								flag = true;
							}
						}
						flag = true;
					}
				}
				else
				{
					MigrationLogger.Log(MigrationEventType.Verbose, "skipping step {0} which isn't supported for this job-item", new object[]
					{
						migrationWorkflowStep.Step
					});
				}
			}
			return null;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004DB78 File Offset: 0x0004BD78
		internal MigrationWorkflowPosition GetRestartPosition(MigrationWorkflowPosition position)
		{
			MigrationUtil.ThrowOnNullArgument(position, "position");
			if (this.Steps == null || this.Steps.Length < 1)
			{
				return position;
			}
			foreach (MigrationWorkflowStep migrationWorkflowStep in this.Steps)
			{
				if (migrationWorkflowStep.Step == position.Step)
				{
					return new MigrationWorkflowPosition(position.Step, migrationWorkflowStep.AllowedStages[0]);
				}
			}
			return position;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x0004DBE8 File Offset: 0x0004BDE8
		internal bool ShouldDelay(MigrationWorkflowPosition position, MigrationJob job)
		{
			return position.Step == MigrationStep.ProvisioningUpdate && job.GetItemCount(new MigrationUserStatus[]
			{
				MigrationUserStatus.Validating,
				MigrationUserStatus.Provisioning
			}) > 0;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0004DC20 File Offset: 0x0004BE20
		[Conditional("DEBUG")]
		internal void ValidateWorkflowSteps(MigrationWorkflowStep[] workflowSteps)
		{
			HashSet<MigrationStep> hashSet = new HashSet<MigrationStep>();
			foreach (MigrationWorkflowStep migrationWorkflowStep in workflowSteps)
			{
				MigrationUtil.AssertOrThrow(!hashSet.Contains(migrationWorkflowStep.Step), "Workflow Step {0} is repeated!", new object[]
				{
					migrationWorkflowStep.Step
				});
				hashSet.Add(migrationWorkflowStep.Step);
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0004DC88 File Offset: 0x0004BE88
		internal List<MigrationStep> GetRemainingSteps(MigrationWorkflowPosition position)
		{
			List<MigrationStep> list = new List<MigrationStep>();
			bool flag = false;
			foreach (MigrationWorkflowStep migrationWorkflowStep in this.Steps)
			{
				if (migrationWorkflowStep.Step == position.Step)
				{
					flag = true;
				}
				if (flag)
				{
					list.Add(migrationWorkflowStep.Step);
				}
			}
			return list;
		}

		// Token: 0x0400065C RID: 1628
		internal static readonly MigrationWorkflowStep[] DefaultProvisionAndMigrateWorkflowSteps = new MigrationWorkflowStep[]
		{
			new MigrationWorkflowStep(MigrationStep.Initialization),
			new MigrationWorkflowStep(MigrationStep.Provisioning),
			new MigrationWorkflowStep(MigrationStep.ProvisioningUpdate),
			new MigrationWorkflowStep(MigrationStep.DataMigration)
		};

		// Token: 0x0400065D RID: 1629
		internal static readonly MigrationWorkflowStep[] DefaultMigrationWorkflowSteps = new MigrationWorkflowStep[]
		{
			new MigrationWorkflowStep(MigrationStep.Initialization),
			new MigrationWorkflowStep(MigrationStep.DataMigration)
		};

		// Token: 0x0400065E RID: 1630
		internal static readonly MigrationWorkflowStep[] DefaultProvisioningWorkflowSteps = new MigrationWorkflowStep[]
		{
			new MigrationWorkflowStep(MigrationStep.Initialization),
			new MigrationWorkflowStep(MigrationStep.Provisioning)
		};
	}
}
