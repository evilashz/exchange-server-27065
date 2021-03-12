using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007E1 RID: 2017
	public class GetMSERVEntryCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x06006495 RID: 25749 RVA: 0x00099E76 File Offset: 0x00098076
		private GetMSERVEntryCommand() : base("Get-MSERVEntry")
		{
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00099E83 File Offset: 0x00098083
		public GetMSERVEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x00099E92 File Offset: 0x00098092
		public virtual GetMSERVEntryCommand SetParameters(GetMSERVEntryCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x00099E9C File Offset: 0x0009809C
		public virtual GetMSERVEntryCommand SetParameters(GetMSERVEntryCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00099EA6 File Offset: 0x000980A6
		public virtual GetMSERVEntryCommand SetParameters(GetMSERVEntryCommand.AddressParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007E2 RID: 2018
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x1700408C RID: 16524
			// (set) Token: 0x0600649A RID: 25754 RVA: 0x00099EB0 File Offset: 0x000980B0
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x1700408D RID: 16525
			// (set) Token: 0x0600649B RID: 25755 RVA: 0x00099EC8 File Offset: 0x000980C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700408E RID: 16526
			// (set) Token: 0x0600649C RID: 25756 RVA: 0x00099EE0 File Offset: 0x000980E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700408F RID: 16527
			// (set) Token: 0x0600649D RID: 25757 RVA: 0x00099EF8 File Offset: 0x000980F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004090 RID: 16528
			// (set) Token: 0x0600649E RID: 25758 RVA: 0x00099F10 File Offset: 0x00098110
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007E3 RID: 2019
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x17004091 RID: 16529
			// (set) Token: 0x060064A0 RID: 25760 RVA: 0x00099F30 File Offset: 0x00098130
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17004092 RID: 16530
			// (set) Token: 0x060064A1 RID: 25761 RVA: 0x00099F43 File Offset: 0x00098143
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004093 RID: 16531
			// (set) Token: 0x060064A2 RID: 25762 RVA: 0x00099F5B File Offset: 0x0009815B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004094 RID: 16532
			// (set) Token: 0x060064A3 RID: 25763 RVA: 0x00099F73 File Offset: 0x00098173
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004095 RID: 16533
			// (set) Token: 0x060064A4 RID: 25764 RVA: 0x00099F8B File Offset: 0x0009818B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020007E4 RID: 2020
		public class AddressParameterSetParameters : ParametersBase
		{
			// Token: 0x17004096 RID: 16534
			// (set) Token: 0x060064A6 RID: 25766 RVA: 0x00099FAB File Offset: 0x000981AB
			public virtual string Address
			{
				set
				{
					base.PowerSharpParameters["Address"] = value;
				}
			}

			// Token: 0x17004097 RID: 16535
			// (set) Token: 0x060064A7 RID: 25767 RVA: 0x00099FBE File Offset: 0x000981BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004098 RID: 16536
			// (set) Token: 0x060064A8 RID: 25768 RVA: 0x00099FD6 File Offset: 0x000981D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004099 RID: 16537
			// (set) Token: 0x060064A9 RID: 25769 RVA: 0x00099FEE File Offset: 0x000981EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700409A RID: 16538
			// (set) Token: 0x060064AA RID: 25770 RVA: 0x0009A006 File Offset: 0x00098206
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
