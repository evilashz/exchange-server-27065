using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000140 RID: 320
	public class SetFailedMSOSyncObjectCommand : SyntheticCommandWithPipelineInputNoOutput<FailedMSOSyncObjectPresentationObject>
	{
		// Token: 0x06002070 RID: 8304 RVA: 0x00041B49 File Offset: 0x0003FD49
		private SetFailedMSOSyncObjectCommand() : base("Set-FailedMSOSyncObject")
		{
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x00041B56 File Offset: 0x0003FD56
		public SetFailedMSOSyncObjectCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x00041B65 File Offset: 0x0003FD65
		public virtual SetFailedMSOSyncObjectCommand SetParameters(SetFailedMSOSyncObjectCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00041B6F File Offset: 0x0003FD6F
		public virtual SetFailedMSOSyncObjectCommand SetParameters(SetFailedMSOSyncObjectCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000141 RID: 321
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170009A9 RID: 2473
			// (set) Token: 0x06002074 RID: 8308 RVA: 0x00041B79 File Offset: 0x0003FD79
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new FailedMSOSyncObjectIdParameter(value) : null);
				}
			}

			// Token: 0x170009AA RID: 2474
			// (set) Token: 0x06002075 RID: 8309 RVA: 0x00041B97 File Offset: 0x0003FD97
			public virtual bool IsIgnoredInHaltCondition
			{
				set
				{
					base.PowerSharpParameters["IsIgnoredInHaltCondition"] = value;
				}
			}

			// Token: 0x170009AB RID: 2475
			// (set) Token: 0x06002076 RID: 8310 RVA: 0x00041BAF File Offset: 0x0003FDAF
			public virtual bool IsRetriable
			{
				set
				{
					base.PowerSharpParameters["IsRetriable"] = value;
				}
			}

			// Token: 0x170009AC RID: 2476
			// (set) Token: 0x06002077 RID: 8311 RVA: 0x00041BC7 File Offset: 0x0003FDC7
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170009AD RID: 2477
			// (set) Token: 0x06002078 RID: 8312 RVA: 0x00041BDA File Offset: 0x0003FDDA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170009AE RID: 2478
			// (set) Token: 0x06002079 RID: 8313 RVA: 0x00041BF2 File Offset: 0x0003FDF2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170009AF RID: 2479
			// (set) Token: 0x0600207A RID: 8314 RVA: 0x00041C0A File Offset: 0x0003FE0A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170009B0 RID: 2480
			// (set) Token: 0x0600207B RID: 8315 RVA: 0x00041C22 File Offset: 0x0003FE22
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170009B1 RID: 2481
			// (set) Token: 0x0600207C RID: 8316 RVA: 0x00041C3A File Offset: 0x0003FE3A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000142 RID: 322
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170009B2 RID: 2482
			// (set) Token: 0x0600207E RID: 8318 RVA: 0x00041C5A File Offset: 0x0003FE5A
			public virtual bool IsIgnoredInHaltCondition
			{
				set
				{
					base.PowerSharpParameters["IsIgnoredInHaltCondition"] = value;
				}
			}

			// Token: 0x170009B3 RID: 2483
			// (set) Token: 0x0600207F RID: 8319 RVA: 0x00041C72 File Offset: 0x0003FE72
			public virtual bool IsRetriable
			{
				set
				{
					base.PowerSharpParameters["IsRetriable"] = value;
				}
			}

			// Token: 0x170009B4 RID: 2484
			// (set) Token: 0x06002080 RID: 8320 RVA: 0x00041C8A File Offset: 0x0003FE8A
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170009B5 RID: 2485
			// (set) Token: 0x06002081 RID: 8321 RVA: 0x00041C9D File Offset: 0x0003FE9D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170009B6 RID: 2486
			// (set) Token: 0x06002082 RID: 8322 RVA: 0x00041CB5 File Offset: 0x0003FEB5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170009B7 RID: 2487
			// (set) Token: 0x06002083 RID: 8323 RVA: 0x00041CCD File Offset: 0x0003FECD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170009B8 RID: 2488
			// (set) Token: 0x06002084 RID: 8324 RVA: 0x00041CE5 File Offset: 0x0003FEE5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170009B9 RID: 2489
			// (set) Token: 0x06002085 RID: 8325 RVA: 0x00041CFD File Offset: 0x0003FEFD
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
