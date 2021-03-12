using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E65 RID: 3685
	public class ImportContactListCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600D9E9 RID: 55785 RVA: 0x00135419 File Offset: 0x00133619
		private ImportContactListCommand() : base("Import-ContactList")
		{
		}

		// Token: 0x0600D9EA RID: 55786 RVA: 0x00135426 File Offset: 0x00133626
		public ImportContactListCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D9EB RID: 55787 RVA: 0x00135435 File Offset: 0x00133635
		public virtual ImportContactListCommand SetParameters(ImportContactListCommand.DataParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D9EC RID: 55788 RVA: 0x0013543F File Offset: 0x0013363F
		public virtual ImportContactListCommand SetParameters(ImportContactListCommand.StreamParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D9ED RID: 55789 RVA: 0x00135449 File Offset: 0x00133649
		public virtual ImportContactListCommand SetParameters(ImportContactListCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E66 RID: 3686
		public class DataParameters : ParametersBase
		{
			// Token: 0x1700A8D8 RID: 43224
			// (set) Token: 0x0600D9EE RID: 55790 RVA: 0x00135453 File Offset: 0x00133653
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8D9 RID: 43225
			// (set) Token: 0x0600D9EF RID: 55791 RVA: 0x00135471 File Offset: 0x00133671
			public virtual SwitchParameter CSV
			{
				set
				{
					base.PowerSharpParameters["CSV"] = value;
				}
			}

			// Token: 0x1700A8DA RID: 43226
			// (set) Token: 0x0600D9F0 RID: 55792 RVA: 0x00135489 File Offset: 0x00133689
			public virtual byte CSVData
			{
				set
				{
					base.PowerSharpParameters["CSVData"] = value;
				}
			}

			// Token: 0x1700A8DB RID: 43227
			// (set) Token: 0x0600D9F1 RID: 55793 RVA: 0x001354A1 File Offset: 0x001336A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8DC RID: 43228
			// (set) Token: 0x0600D9F2 RID: 55794 RVA: 0x001354B4 File Offset: 0x001336B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8DD RID: 43229
			// (set) Token: 0x0600D9F3 RID: 55795 RVA: 0x001354CC File Offset: 0x001336CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8DE RID: 43230
			// (set) Token: 0x0600D9F4 RID: 55796 RVA: 0x001354E4 File Offset: 0x001336E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8DF RID: 43231
			// (set) Token: 0x0600D9F5 RID: 55797 RVA: 0x001354FC File Offset: 0x001336FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8E0 RID: 43232
			// (set) Token: 0x0600D9F6 RID: 55798 RVA: 0x00135514 File Offset: 0x00133714
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E67 RID: 3687
		public class StreamParameters : ParametersBase
		{
			// Token: 0x1700A8E1 RID: 43233
			// (set) Token: 0x0600D9F8 RID: 55800 RVA: 0x00135534 File Offset: 0x00133734
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8E2 RID: 43234
			// (set) Token: 0x0600D9F9 RID: 55801 RVA: 0x00135552 File Offset: 0x00133752
			public virtual SwitchParameter CSV
			{
				set
				{
					base.PowerSharpParameters["CSV"] = value;
				}
			}

			// Token: 0x1700A8E3 RID: 43235
			// (set) Token: 0x0600D9FA RID: 55802 RVA: 0x0013556A File Offset: 0x0013376A
			public virtual Stream CSVStream
			{
				set
				{
					base.PowerSharpParameters["CSVStream"] = value;
				}
			}

			// Token: 0x1700A8E4 RID: 43236
			// (set) Token: 0x0600D9FB RID: 55803 RVA: 0x0013557D File Offset: 0x0013377D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8E5 RID: 43237
			// (set) Token: 0x0600D9FC RID: 55804 RVA: 0x00135590 File Offset: 0x00133790
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8E6 RID: 43238
			// (set) Token: 0x0600D9FD RID: 55805 RVA: 0x001355A8 File Offset: 0x001337A8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8E7 RID: 43239
			// (set) Token: 0x0600D9FE RID: 55806 RVA: 0x001355C0 File Offset: 0x001337C0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8E8 RID: 43240
			// (set) Token: 0x0600D9FF RID: 55807 RVA: 0x001355D8 File Offset: 0x001337D8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8E9 RID: 43241
			// (set) Token: 0x0600DA00 RID: 55808 RVA: 0x001355F0 File Offset: 0x001337F0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E68 RID: 3688
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A8EA RID: 43242
			// (set) Token: 0x0600DA02 RID: 55810 RVA: 0x00135610 File Offset: 0x00133810
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8EB RID: 43243
			// (set) Token: 0x0600DA03 RID: 55811 RVA: 0x00135623 File Offset: 0x00133823
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8EC RID: 43244
			// (set) Token: 0x0600DA04 RID: 55812 RVA: 0x0013563B File Offset: 0x0013383B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8ED RID: 43245
			// (set) Token: 0x0600DA05 RID: 55813 RVA: 0x00135653 File Offset: 0x00133853
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8EE RID: 43246
			// (set) Token: 0x0600DA06 RID: 55814 RVA: 0x0013566B File Offset: 0x0013386B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8EF RID: 43247
			// (set) Token: 0x0600DA07 RID: 55815 RVA: 0x00135683 File Offset: 0x00133883
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
