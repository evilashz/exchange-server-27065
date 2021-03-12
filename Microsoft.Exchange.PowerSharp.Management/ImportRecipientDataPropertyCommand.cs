using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D45 RID: 3397
	public class ImportRecipientDataPropertyCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x0600B3D2 RID: 46034 RVA: 0x0010312A File Offset: 0x0010132A
		private ImportRecipientDataPropertyCommand() : base("Import-RecipientDataProperty")
		{
		}

		// Token: 0x0600B3D3 RID: 46035 RVA: 0x00103137 File Offset: 0x00101337
		public ImportRecipientDataPropertyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B3D4 RID: 46036 RVA: 0x00103146 File Offset: 0x00101346
		public virtual ImportRecipientDataPropertyCommand SetParameters(ImportRecipientDataPropertyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B3D5 RID: 46037 RVA: 0x00103150 File Offset: 0x00101350
		public virtual ImportRecipientDataPropertyCommand SetParameters(ImportRecipientDataPropertyCommand.ImportPictureParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B3D6 RID: 46038 RVA: 0x0010315A File Offset: 0x0010135A
		public virtual ImportRecipientDataPropertyCommand SetParameters(ImportRecipientDataPropertyCommand.ImportSpokenNameParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D46 RID: 3398
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008501 RID: 34049
			// (set) Token: 0x0600B3D7 RID: 46039 RVA: 0x00103164 File Offset: 0x00101364
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxUserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008502 RID: 34050
			// (set) Token: 0x0600B3D8 RID: 46040 RVA: 0x00103182 File Offset: 0x00101382
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17008503 RID: 34051
			// (set) Token: 0x0600B3D9 RID: 46041 RVA: 0x0010319A File Offset: 0x0010139A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008504 RID: 34052
			// (set) Token: 0x0600B3DA RID: 46042 RVA: 0x001031AD File Offset: 0x001013AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008505 RID: 34053
			// (set) Token: 0x0600B3DB RID: 46043 RVA: 0x001031C5 File Offset: 0x001013C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008506 RID: 34054
			// (set) Token: 0x0600B3DC RID: 46044 RVA: 0x001031DD File Offset: 0x001013DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008507 RID: 34055
			// (set) Token: 0x0600B3DD RID: 46045 RVA: 0x001031F5 File Offset: 0x001013F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008508 RID: 34056
			// (set) Token: 0x0600B3DE RID: 46046 RVA: 0x0010320D File Offset: 0x0010140D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D47 RID: 3399
		public class ImportPictureParameters : ParametersBase
		{
			// Token: 0x17008509 RID: 34057
			// (set) Token: 0x0600B3E0 RID: 46048 RVA: 0x0010322D File Offset: 0x0010142D
			public virtual SwitchParameter Picture
			{
				set
				{
					base.PowerSharpParameters["Picture"] = value;
				}
			}

			// Token: 0x1700850A RID: 34058
			// (set) Token: 0x0600B3E1 RID: 46049 RVA: 0x00103245 File Offset: 0x00101445
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxUserContactIdParameter(value) : null);
				}
			}

			// Token: 0x1700850B RID: 34059
			// (set) Token: 0x0600B3E2 RID: 46050 RVA: 0x00103263 File Offset: 0x00101463
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x1700850C RID: 34060
			// (set) Token: 0x0600B3E3 RID: 46051 RVA: 0x0010327B File Offset: 0x0010147B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700850D RID: 34061
			// (set) Token: 0x0600B3E4 RID: 46052 RVA: 0x0010328E File Offset: 0x0010148E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700850E RID: 34062
			// (set) Token: 0x0600B3E5 RID: 46053 RVA: 0x001032A6 File Offset: 0x001014A6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700850F RID: 34063
			// (set) Token: 0x0600B3E6 RID: 46054 RVA: 0x001032BE File Offset: 0x001014BE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008510 RID: 34064
			// (set) Token: 0x0600B3E7 RID: 46055 RVA: 0x001032D6 File Offset: 0x001014D6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17008511 RID: 34065
			// (set) Token: 0x0600B3E8 RID: 46056 RVA: 0x001032EE File Offset: 0x001014EE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000D48 RID: 3400
		public class ImportSpokenNameParameters : ParametersBase
		{
			// Token: 0x17008512 RID: 34066
			// (set) Token: 0x0600B3EA RID: 46058 RVA: 0x0010330E File Offset: 0x0010150E
			public virtual SwitchParameter SpokenName
			{
				set
				{
					base.PowerSharpParameters["SpokenName"] = value;
				}
			}

			// Token: 0x17008513 RID: 34067
			// (set) Token: 0x0600B3EB RID: 46059 RVA: 0x00103326 File Offset: 0x00101526
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxUserContactIdParameter(value) : null);
				}
			}

			// Token: 0x17008514 RID: 34068
			// (set) Token: 0x0600B3EC RID: 46060 RVA: 0x00103344 File Offset: 0x00101544
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x17008515 RID: 34069
			// (set) Token: 0x0600B3ED RID: 46061 RVA: 0x0010335C File Offset: 0x0010155C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008516 RID: 34070
			// (set) Token: 0x0600B3EE RID: 46062 RVA: 0x0010336F File Offset: 0x0010156F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008517 RID: 34071
			// (set) Token: 0x0600B3EF RID: 46063 RVA: 0x00103387 File Offset: 0x00101587
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008518 RID: 34072
			// (set) Token: 0x0600B3F0 RID: 46064 RVA: 0x0010339F File Offset: 0x0010159F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008519 RID: 34073
			// (set) Token: 0x0600B3F1 RID: 46065 RVA: 0x001033B7 File Offset: 0x001015B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700851A RID: 34074
			// (set) Token: 0x0600B3F2 RID: 46066 RVA: 0x001033CF File Offset: 0x001015CF
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
