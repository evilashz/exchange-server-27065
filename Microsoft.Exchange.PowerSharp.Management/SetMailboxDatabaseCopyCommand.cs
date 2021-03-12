using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000602 RID: 1538
	public class SetMailboxDatabaseCopyCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseCopy>
	{
		// Token: 0x06004F04 RID: 20228 RVA: 0x0007DB80 File Offset: 0x0007BD80
		private SetMailboxDatabaseCopyCommand() : base("Set-MailboxDatabaseCopy")
		{
		}

		// Token: 0x06004F05 RID: 20229 RVA: 0x0007DB8D File Offset: 0x0007BD8D
		public SetMailboxDatabaseCopyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004F06 RID: 20230 RVA: 0x0007DB9C File Offset: 0x0007BD9C
		public virtual SetMailboxDatabaseCopyCommand SetParameters(SetMailboxDatabaseCopyCommand.ClearHostServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004F07 RID: 20231 RVA: 0x0007DBA6 File Offset: 0x0007BDA6
		public virtual SetMailboxDatabaseCopyCommand SetParameters(SetMailboxDatabaseCopyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004F08 RID: 20232 RVA: 0x0007DBB0 File Offset: 0x0007BDB0
		public virtual SetMailboxDatabaseCopyCommand SetParameters(SetMailboxDatabaseCopyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000603 RID: 1539
		public class ClearHostServerParameters : ParametersBase
		{
			// Token: 0x17002EB9 RID: 11961
			// (set) Token: 0x06004F09 RID: 20233 RVA: 0x0007DBBA File Offset: 0x0007BDBA
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002EBA RID: 11962
			// (set) Token: 0x06004F0A RID: 20234 RVA: 0x0007DBCD File Offset: 0x0007BDCD
			public virtual SwitchParameter ClearHostServer
			{
				set
				{
					base.PowerSharpParameters["ClearHostServer"] = value;
				}
			}

			// Token: 0x17002EBB RID: 11963
			// (set) Token: 0x06004F0B RID: 20235 RVA: 0x0007DBE5 File Offset: 0x0007BDE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002EBC RID: 11964
			// (set) Token: 0x06004F0C RID: 20236 RVA: 0x0007DBF8 File Offset: 0x0007BDF8
			public virtual EnhancedTimeSpan ReplayLagTime
			{
				set
				{
					base.PowerSharpParameters["ReplayLagTime"] = value;
				}
			}

			// Token: 0x17002EBD RID: 11965
			// (set) Token: 0x06004F0D RID: 20237 RVA: 0x0007DC10 File Offset: 0x0007BE10
			public virtual EnhancedTimeSpan TruncationLagTime
			{
				set
				{
					base.PowerSharpParameters["TruncationLagTime"] = value;
				}
			}

			// Token: 0x17002EBE RID: 11966
			// (set) Token: 0x06004F0E RID: 20238 RVA: 0x0007DC28 File Offset: 0x0007BE28
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x17002EBF RID: 11967
			// (set) Token: 0x06004F0F RID: 20239 RVA: 0x0007DC40 File Offset: 0x0007BE40
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002EC0 RID: 11968
			// (set) Token: 0x06004F10 RID: 20240 RVA: 0x0007DC58 File Offset: 0x0007BE58
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002EC1 RID: 11969
			// (set) Token: 0x06004F11 RID: 20241 RVA: 0x0007DC70 File Offset: 0x0007BE70
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002EC2 RID: 11970
			// (set) Token: 0x06004F12 RID: 20242 RVA: 0x0007DC88 File Offset: 0x0007BE88
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002EC3 RID: 11971
			// (set) Token: 0x06004F13 RID: 20243 RVA: 0x0007DCA0 File Offset: 0x0007BEA0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000604 RID: 1540
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002EC4 RID: 11972
			// (set) Token: 0x06004F15 RID: 20245 RVA: 0x0007DCC0 File Offset: 0x0007BEC0
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002EC5 RID: 11973
			// (set) Token: 0x06004F16 RID: 20246 RVA: 0x0007DCD3 File Offset: 0x0007BED3
			public virtual uint ActivationPreference
			{
				set
				{
					base.PowerSharpParameters["ActivationPreference"] = value;
				}
			}

			// Token: 0x17002EC6 RID: 11974
			// (set) Token: 0x06004F17 RID: 20247 RVA: 0x0007DCEB File Offset: 0x0007BEEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002EC7 RID: 11975
			// (set) Token: 0x06004F18 RID: 20248 RVA: 0x0007DCFE File Offset: 0x0007BEFE
			public virtual EnhancedTimeSpan ReplayLagTime
			{
				set
				{
					base.PowerSharpParameters["ReplayLagTime"] = value;
				}
			}

			// Token: 0x17002EC8 RID: 11976
			// (set) Token: 0x06004F19 RID: 20249 RVA: 0x0007DD16 File Offset: 0x0007BF16
			public virtual EnhancedTimeSpan TruncationLagTime
			{
				set
				{
					base.PowerSharpParameters["TruncationLagTime"] = value;
				}
			}

			// Token: 0x17002EC9 RID: 11977
			// (set) Token: 0x06004F1A RID: 20250 RVA: 0x0007DD2E File Offset: 0x0007BF2E
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x17002ECA RID: 11978
			// (set) Token: 0x06004F1B RID: 20251 RVA: 0x0007DD46 File Offset: 0x0007BF46
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002ECB RID: 11979
			// (set) Token: 0x06004F1C RID: 20252 RVA: 0x0007DD5E File Offset: 0x0007BF5E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002ECC RID: 11980
			// (set) Token: 0x06004F1D RID: 20253 RVA: 0x0007DD76 File Offset: 0x0007BF76
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002ECD RID: 11981
			// (set) Token: 0x06004F1E RID: 20254 RVA: 0x0007DD8E File Offset: 0x0007BF8E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002ECE RID: 11982
			// (set) Token: 0x06004F1F RID: 20255 RVA: 0x0007DDA6 File Offset: 0x0007BFA6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000605 RID: 1541
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002ECF RID: 11983
			// (set) Token: 0x06004F21 RID: 20257 RVA: 0x0007DDC6 File Offset: 0x0007BFC6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002ED0 RID: 11984
			// (set) Token: 0x06004F22 RID: 20258 RVA: 0x0007DDD9 File Offset: 0x0007BFD9
			public virtual EnhancedTimeSpan ReplayLagTime
			{
				set
				{
					base.PowerSharpParameters["ReplayLagTime"] = value;
				}
			}

			// Token: 0x17002ED1 RID: 11985
			// (set) Token: 0x06004F23 RID: 20259 RVA: 0x0007DDF1 File Offset: 0x0007BFF1
			public virtual EnhancedTimeSpan TruncationLagTime
			{
				set
				{
					base.PowerSharpParameters["TruncationLagTime"] = value;
				}
			}

			// Token: 0x17002ED2 RID: 11986
			// (set) Token: 0x06004F24 RID: 20260 RVA: 0x0007DE09 File Offset: 0x0007C009
			public virtual DatabaseCopyAutoActivationPolicyType DatabaseCopyAutoActivationPolicy
			{
				set
				{
					base.PowerSharpParameters["DatabaseCopyAutoActivationPolicy"] = value;
				}
			}

			// Token: 0x17002ED3 RID: 11987
			// (set) Token: 0x06004F25 RID: 20261 RVA: 0x0007DE21 File Offset: 0x0007C021
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002ED4 RID: 11988
			// (set) Token: 0x06004F26 RID: 20262 RVA: 0x0007DE39 File Offset: 0x0007C039
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002ED5 RID: 11989
			// (set) Token: 0x06004F27 RID: 20263 RVA: 0x0007DE51 File Offset: 0x0007C051
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002ED6 RID: 11990
			// (set) Token: 0x06004F28 RID: 20264 RVA: 0x0007DE69 File Offset: 0x0007C069
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002ED7 RID: 11991
			// (set) Token: 0x06004F29 RID: 20265 RVA: 0x0007DE81 File Offset: 0x0007C081
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
