using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018A RID: 394
	public sealed class MigrationWorkflowStep : XMLSerializableBase
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x0004DFCA File Offset: 0x0004C1CA
		public MigrationWorkflowStep(MigrationStep step)
		{
			this.Step = step;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004DFD9 File Offset: 0x0004C1D9
		public MigrationWorkflowStep()
		{
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600126F RID: 4719 RVA: 0x0004DFE1 File Offset: 0x0004C1E1
		// (set) Token: 0x06001270 RID: 4720 RVA: 0x0004DFE9 File Offset: 0x0004C1E9
		[XmlIgnore]
		public MigrationStep Step { get; set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0004DFF2 File Offset: 0x0004C1F2
		// (set) Token: 0x06001272 RID: 4722 RVA: 0x0004DFFA File Offset: 0x0004C1FA
		[XmlElement("Step")]
		public int StepInt
		{
			get
			{
				return (int)this.Step;
			}
			set
			{
				this.Step = (MigrationStep)value;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001273 RID: 4723 RVA: 0x0004E004 File Offset: 0x0004C204
		[XmlIgnore]
		public MigrationStage[] AllowedStages
		{
			get
			{
				MigrationStep step = this.Step;
				if (step <= MigrationStep.Provisioning)
				{
					if (step == MigrationStep.Initialization)
					{
						return InitializationStepHandler.AllowedStages;
					}
					if (step != MigrationStep.Provisioning)
					{
						goto IL_33;
					}
				}
				else if (step != MigrationStep.ProvisioningUpdate)
				{
					if (step != MigrationStep.DataMigration)
					{
						goto IL_33;
					}
					return DataMigrationStepHandler.AllowedStages;
				}
				return ProvisioningStepHandlerBase.AllowedStages;
				IL_33:
				throw new NotSupportedException("Don't know step " + this.Step);
			}
		}
	}
}
