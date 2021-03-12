using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001F2 RID: 498
	public class GetMailboxAssociationCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06002881 RID: 10369 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
		private GetMailboxAssociationCommand() : base("Get-MailboxAssociation")
		{
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0004C5B5 File Offset: 0x0004A7B5
		public GetMailboxAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0004C5C4 File Offset: 0x0004A7C4
		public virtual GetMailboxAssociationCommand SetParameters(GetMailboxAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001F3 RID: 499
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001056 RID: 4182
			// (set) Token: 0x06002884 RID: 10372 RVA: 0x0004C5CE File Offset: 0x0004A7CE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x17001057 RID: 4183
			// (set) Token: 0x06002885 RID: 10373 RVA: 0x0004C5EC File Offset: 0x0004A7EC
			public virtual SwitchParameter IncludeNotPromotedProperties
			{
				set
				{
					base.PowerSharpParameters["IncludeNotPromotedProperties"] = value;
				}
			}

			// Token: 0x17001058 RID: 4184
			// (set) Token: 0x06002886 RID: 10374 RVA: 0x0004C604 File Offset: 0x0004A804
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17001059 RID: 4185
			// (set) Token: 0x06002887 RID: 10375 RVA: 0x0004C617 File Offset: 0x0004A817
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700105A RID: 4186
			// (set) Token: 0x06002888 RID: 10376 RVA: 0x0004C62F File Offset: 0x0004A82F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700105B RID: 4187
			// (set) Token: 0x06002889 RID: 10377 RVA: 0x0004C647 File Offset: 0x0004A847
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700105C RID: 4188
			// (set) Token: 0x0600288A RID: 10378 RVA: 0x0004C65A File Offset: 0x0004A85A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700105D RID: 4189
			// (set) Token: 0x0600288B RID: 10379 RVA: 0x0004C672 File Offset: 0x0004A872
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700105E RID: 4190
			// (set) Token: 0x0600288C RID: 10380 RVA: 0x0004C68A File Offset: 0x0004A88A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700105F RID: 4191
			// (set) Token: 0x0600288D RID: 10381 RVA: 0x0004C6A2 File Offset: 0x0004A8A2
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
