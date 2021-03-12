using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E5E RID: 3678
	public class GetSendAddressCommand : SyntheticCommandWithPipelineInput<SendAddress, SendAddress>
	{
		// Token: 0x0600D9B3 RID: 55731 RVA: 0x00134FC7 File Offset: 0x001331C7
		private GetSendAddressCommand() : base("Get-SendAddress")
		{
		}

		// Token: 0x0600D9B4 RID: 55732 RVA: 0x00134FD4 File Offset: 0x001331D4
		public GetSendAddressCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D9B5 RID: 55733 RVA: 0x00134FE3 File Offset: 0x001331E3
		public virtual GetSendAddressCommand SetParameters(GetSendAddressCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D9B6 RID: 55734 RVA: 0x00134FED File Offset: 0x001331ED
		public virtual GetSendAddressCommand SetParameters(GetSendAddressCommand.LookUpIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D9B7 RID: 55735 RVA: 0x00134FF7 File Offset: 0x001331F7
		public virtual GetSendAddressCommand SetParameters(GetSendAddressCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E5F RID: 3679
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A8B0 RID: 43184
			// (set) Token: 0x0600D9B8 RID: 55736 RVA: 0x00135001 File Offset: 0x00133201
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8B1 RID: 43185
			// (set) Token: 0x0600D9B9 RID: 55737 RVA: 0x0013501F File Offset: 0x0013321F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SendAddressIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8B2 RID: 43186
			// (set) Token: 0x0600D9BA RID: 55738 RVA: 0x0013503D File Offset: 0x0013323D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8B3 RID: 43187
			// (set) Token: 0x0600D9BB RID: 55739 RVA: 0x00135050 File Offset: 0x00133250
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8B4 RID: 43188
			// (set) Token: 0x0600D9BC RID: 55740 RVA: 0x00135068 File Offset: 0x00133268
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8B5 RID: 43189
			// (set) Token: 0x0600D9BD RID: 55741 RVA: 0x00135080 File Offset: 0x00133280
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8B6 RID: 43190
			// (set) Token: 0x0600D9BE RID: 55742 RVA: 0x00135098 File Offset: 0x00133298
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E60 RID: 3680
		public class LookUpIdParameters : ParametersBase
		{
			// Token: 0x1700A8B7 RID: 43191
			// (set) Token: 0x0600D9C0 RID: 55744 RVA: 0x001350B8 File Offset: 0x001332B8
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8B8 RID: 43192
			// (set) Token: 0x0600D9C1 RID: 55745 RVA: 0x001350D6 File Offset: 0x001332D6
			public virtual string AddressId
			{
				set
				{
					base.PowerSharpParameters["AddressId"] = value;
				}
			}

			// Token: 0x1700A8B9 RID: 43193
			// (set) Token: 0x0600D9C2 RID: 55746 RVA: 0x001350E9 File Offset: 0x001332E9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8BA RID: 43194
			// (set) Token: 0x0600D9C3 RID: 55747 RVA: 0x001350FC File Offset: 0x001332FC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8BB RID: 43195
			// (set) Token: 0x0600D9C4 RID: 55748 RVA: 0x00135114 File Offset: 0x00133314
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8BC RID: 43196
			// (set) Token: 0x0600D9C5 RID: 55749 RVA: 0x0013512C File Offset: 0x0013332C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8BD RID: 43197
			// (set) Token: 0x0600D9C6 RID: 55750 RVA: 0x00135144 File Offset: 0x00133344
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000E61 RID: 3681
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A8BE RID: 43198
			// (set) Token: 0x0600D9C8 RID: 55752 RVA: 0x00135164 File Offset: 0x00133364
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8BF RID: 43199
			// (set) Token: 0x0600D9C9 RID: 55753 RVA: 0x00135177 File Offset: 0x00133377
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8C0 RID: 43200
			// (set) Token: 0x0600D9CA RID: 55754 RVA: 0x0013518F File Offset: 0x0013338F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8C1 RID: 43201
			// (set) Token: 0x0600D9CB RID: 55755 RVA: 0x001351A7 File Offset: 0x001333A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8C2 RID: 43202
			// (set) Token: 0x0600D9CC RID: 55756 RVA: 0x001351BF File Offset: 0x001333BF
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
