using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D2B RID: 3371
	public class GetMailboxPermissionCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600B299 RID: 45721 RVA: 0x00101784 File Offset: 0x000FF984
		private GetMailboxPermissionCommand() : base("Get-MailboxPermission")
		{
		}

		// Token: 0x0600B29A RID: 45722 RVA: 0x00101791 File Offset: 0x000FF991
		public GetMailboxPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B29B RID: 45723 RVA: 0x001017A0 File Offset: 0x000FF9A0
		public virtual GetMailboxPermissionCommand SetParameters(GetMailboxPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B29C RID: 45724 RVA: 0x001017AA File Offset: 0x000FF9AA
		public virtual GetMailboxPermissionCommand SetParameters(GetMailboxPermissionCommand.AccessRightsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B29D RID: 45725 RVA: 0x001017B4 File Offset: 0x000FF9B4
		public virtual GetMailboxPermissionCommand SetParameters(GetMailboxPermissionCommand.OwnerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D2C RID: 3372
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170083FC RID: 33788
			// (set) Token: 0x0600B29E RID: 45726 RVA: 0x001017BE File Offset: 0x000FF9BE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170083FD RID: 33789
			// (set) Token: 0x0600B29F RID: 45727 RVA: 0x001017DC File Offset: 0x000FF9DC
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170083FE RID: 33790
			// (set) Token: 0x0600B2A0 RID: 45728 RVA: 0x001017EF File Offset: 0x000FF9EF
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170083FF RID: 33791
			// (set) Token: 0x0600B2A1 RID: 45729 RVA: 0x00101807 File Offset: 0x000FFA07
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008400 RID: 33792
			// (set) Token: 0x0600B2A2 RID: 45730 RVA: 0x0010181F File Offset: 0x000FFA1F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008401 RID: 33793
			// (set) Token: 0x0600B2A3 RID: 45731 RVA: 0x00101832 File Offset: 0x000FFA32
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008402 RID: 33794
			// (set) Token: 0x0600B2A4 RID: 45732 RVA: 0x0010184A File Offset: 0x000FFA4A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008403 RID: 33795
			// (set) Token: 0x0600B2A5 RID: 45733 RVA: 0x00101862 File Offset: 0x000FFA62
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008404 RID: 33796
			// (set) Token: 0x0600B2A6 RID: 45734 RVA: 0x0010187A File Offset: 0x000FFA7A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D2D RID: 3373
		public class AccessRightsParameters : ParametersBase
		{
			// Token: 0x17008405 RID: 33797
			// (set) Token: 0x0600B2A8 RID: 45736 RVA: 0x0010189A File Offset: 0x000FFA9A
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new SecurityPrincipalIdParameter(value) : null);
				}
			}

			// Token: 0x17008406 RID: 33798
			// (set) Token: 0x0600B2A9 RID: 45737 RVA: 0x001018B8 File Offset: 0x000FFAB8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008407 RID: 33799
			// (set) Token: 0x0600B2AA RID: 45738 RVA: 0x001018D6 File Offset: 0x000FFAD6
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008408 RID: 33800
			// (set) Token: 0x0600B2AB RID: 45739 RVA: 0x001018E9 File Offset: 0x000FFAE9
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008409 RID: 33801
			// (set) Token: 0x0600B2AC RID: 45740 RVA: 0x00101901 File Offset: 0x000FFB01
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700840A RID: 33802
			// (set) Token: 0x0600B2AD RID: 45741 RVA: 0x00101919 File Offset: 0x000FFB19
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700840B RID: 33803
			// (set) Token: 0x0600B2AE RID: 45742 RVA: 0x0010192C File Offset: 0x000FFB2C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700840C RID: 33804
			// (set) Token: 0x0600B2AF RID: 45743 RVA: 0x00101944 File Offset: 0x000FFB44
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700840D RID: 33805
			// (set) Token: 0x0600B2B0 RID: 45744 RVA: 0x0010195C File Offset: 0x000FFB5C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700840E RID: 33806
			// (set) Token: 0x0600B2B1 RID: 45745 RVA: 0x00101974 File Offset: 0x000FFB74
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D2E RID: 3374
		public class OwnerParameters : ParametersBase
		{
			// Token: 0x1700840F RID: 33807
			// (set) Token: 0x0600B2B3 RID: 45747 RVA: 0x00101994 File Offset: 0x000FFB94
			public virtual SwitchParameter Owner
			{
				set
				{
					base.PowerSharpParameters["Owner"] = value;
				}
			}

			// Token: 0x17008410 RID: 33808
			// (set) Token: 0x0600B2B4 RID: 45748 RVA: 0x001019AC File Offset: 0x000FFBAC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17008411 RID: 33809
			// (set) Token: 0x0600B2B5 RID: 45749 RVA: 0x001019CA File Offset: 0x000FFBCA
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008412 RID: 33810
			// (set) Token: 0x0600B2B6 RID: 45750 RVA: 0x001019DD File Offset: 0x000FFBDD
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008413 RID: 33811
			// (set) Token: 0x0600B2B7 RID: 45751 RVA: 0x001019F5 File Offset: 0x000FFBF5
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008414 RID: 33812
			// (set) Token: 0x0600B2B8 RID: 45752 RVA: 0x00101A0D File Offset: 0x000FFC0D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008415 RID: 33813
			// (set) Token: 0x0600B2B9 RID: 45753 RVA: 0x00101A20 File Offset: 0x000FFC20
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008416 RID: 33814
			// (set) Token: 0x0600B2BA RID: 45754 RVA: 0x00101A38 File Offset: 0x000FFC38
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008417 RID: 33815
			// (set) Token: 0x0600B2BB RID: 45755 RVA: 0x00101A50 File Offset: 0x000FFC50
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008418 RID: 33816
			// (set) Token: 0x0600B2BC RID: 45756 RVA: 0x00101A68 File Offset: 0x000FFC68
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
