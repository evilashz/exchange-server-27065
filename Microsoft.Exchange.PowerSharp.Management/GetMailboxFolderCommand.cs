using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000488 RID: 1160
	public class GetMailboxFolderCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x0600418A RID: 16778 RVA: 0x0006CCB2 File Offset: 0x0006AEB2
		private GetMailboxFolderCommand() : base("Get-MailboxFolder")
		{
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x0006CCBF File Offset: 0x0006AEBF
		public GetMailboxFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x0006CCCE File Offset: 0x0006AECE
		public virtual GetMailboxFolderCommand SetParameters(GetMailboxFolderCommand.RecurseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0006CCD8 File Offset: 0x0006AED8
		public virtual GetMailboxFolderCommand SetParameters(GetMailboxFolderCommand.GetChildrenParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0006CCE2 File Offset: 0x0006AEE2
		public virtual GetMailboxFolderCommand SetParameters(GetMailboxFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0006CCEC File Offset: 0x0006AEEC
		public virtual GetMailboxFolderCommand SetParameters(GetMailboxFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000489 RID: 1161
		public class RecurseParameters : ParametersBase
		{
			// Token: 0x17002433 RID: 9267
			// (set) Token: 0x06004190 RID: 16784 RVA: 0x0006CCF6 File Offset: 0x0006AEF6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002434 RID: 9268
			// (set) Token: 0x06004191 RID: 16785 RVA: 0x0006CD14 File Offset: 0x0006AF14
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x17002435 RID: 9269
			// (set) Token: 0x06004192 RID: 16786 RVA: 0x0006CD2C File Offset: 0x0006AF2C
			public virtual SwitchParameter MailFolderOnly
			{
				set
				{
					base.PowerSharpParameters["MailFolderOnly"] = value;
				}
			}

			// Token: 0x17002436 RID: 9270
			// (set) Token: 0x06004193 RID: 16787 RVA: 0x0006CD44 File Offset: 0x0006AF44
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002437 RID: 9271
			// (set) Token: 0x06004194 RID: 16788 RVA: 0x0006CD5C File Offset: 0x0006AF5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002438 RID: 9272
			// (set) Token: 0x06004195 RID: 16789 RVA: 0x0006CD6F File Offset: 0x0006AF6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002439 RID: 9273
			// (set) Token: 0x06004196 RID: 16790 RVA: 0x0006CD87 File Offset: 0x0006AF87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700243A RID: 9274
			// (set) Token: 0x06004197 RID: 16791 RVA: 0x0006CD9F File Offset: 0x0006AF9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700243B RID: 9275
			// (set) Token: 0x06004198 RID: 16792 RVA: 0x0006CDB7 File Offset: 0x0006AFB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200048A RID: 1162
		public class GetChildrenParameters : ParametersBase
		{
			// Token: 0x1700243C RID: 9276
			// (set) Token: 0x0600419A RID: 16794 RVA: 0x0006CDD7 File Offset: 0x0006AFD7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x1700243D RID: 9277
			// (set) Token: 0x0600419B RID: 16795 RVA: 0x0006CDF5 File Offset: 0x0006AFF5
			public virtual SwitchParameter GetChildren
			{
				set
				{
					base.PowerSharpParameters["GetChildren"] = value;
				}
			}

			// Token: 0x1700243E RID: 9278
			// (set) Token: 0x0600419C RID: 16796 RVA: 0x0006CE0D File Offset: 0x0006B00D
			public virtual SwitchParameter MailFolderOnly
			{
				set
				{
					base.PowerSharpParameters["MailFolderOnly"] = value;
				}
			}

			// Token: 0x1700243F RID: 9279
			// (set) Token: 0x0600419D RID: 16797 RVA: 0x0006CE25 File Offset: 0x0006B025
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002440 RID: 9280
			// (set) Token: 0x0600419E RID: 16798 RVA: 0x0006CE3D File Offset: 0x0006B03D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002441 RID: 9281
			// (set) Token: 0x0600419F RID: 16799 RVA: 0x0006CE50 File Offset: 0x0006B050
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002442 RID: 9282
			// (set) Token: 0x060041A0 RID: 16800 RVA: 0x0006CE68 File Offset: 0x0006B068
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002443 RID: 9283
			// (set) Token: 0x060041A1 RID: 16801 RVA: 0x0006CE80 File Offset: 0x0006B080
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002444 RID: 9284
			// (set) Token: 0x060041A2 RID: 16802 RVA: 0x0006CE98 File Offset: 0x0006B098
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200048B RID: 1163
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002445 RID: 9285
			// (set) Token: 0x060041A4 RID: 16804 RVA: 0x0006CEB8 File Offset: 0x0006B0B8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002446 RID: 9286
			// (set) Token: 0x060041A5 RID: 16805 RVA: 0x0006CED6 File Offset: 0x0006B0D6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002447 RID: 9287
			// (set) Token: 0x060041A6 RID: 16806 RVA: 0x0006CEE9 File Offset: 0x0006B0E9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002448 RID: 9288
			// (set) Token: 0x060041A7 RID: 16807 RVA: 0x0006CF01 File Offset: 0x0006B101
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002449 RID: 9289
			// (set) Token: 0x060041A8 RID: 16808 RVA: 0x0006CF19 File Offset: 0x0006B119
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700244A RID: 9290
			// (set) Token: 0x060041A9 RID: 16809 RVA: 0x0006CF31 File Offset: 0x0006B131
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200048C RID: 1164
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700244B RID: 9291
			// (set) Token: 0x060041AB RID: 16811 RVA: 0x0006CF51 File Offset: 0x0006B151
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700244C RID: 9292
			// (set) Token: 0x060041AC RID: 16812 RVA: 0x0006CF64 File Offset: 0x0006B164
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700244D RID: 9293
			// (set) Token: 0x060041AD RID: 16813 RVA: 0x0006CF7C File Offset: 0x0006B17C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700244E RID: 9294
			// (set) Token: 0x060041AE RID: 16814 RVA: 0x0006CF94 File Offset: 0x0006B194
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700244F RID: 9295
			// (set) Token: 0x060041AF RID: 16815 RVA: 0x0006CFAC File Offset: 0x0006B1AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
