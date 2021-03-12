using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BD3 RID: 3027
	public class GetCASMailboxPlanCommand : SyntheticCommandWithPipelineInput<MailboxPlanIdParameter, MailboxPlanIdParameter>
	{
		// Token: 0x06009209 RID: 37385 RVA: 0x000D53F8 File Offset: 0x000D35F8
		private GetCASMailboxPlanCommand() : base("Get-CASMailboxPlan")
		{
		}

		// Token: 0x0600920A RID: 37386 RVA: 0x000D5405 File Offset: 0x000D3605
		public GetCASMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600920B RID: 37387 RVA: 0x000D5414 File Offset: 0x000D3614
		public virtual GetCASMailboxPlanCommand SetParameters(GetCASMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600920C RID: 37388 RVA: 0x000D541E File Offset: 0x000D361E
		public virtual GetCASMailboxPlanCommand SetParameters(GetCASMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BD4 RID: 3028
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700661C RID: 26140
			// (set) Token: 0x0600920D RID: 37389 RVA: 0x000D5428 File Offset: 0x000D3628
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700661D RID: 26141
			// (set) Token: 0x0600920E RID: 37390 RVA: 0x000D543B File Offset: 0x000D363B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700661E RID: 26142
			// (set) Token: 0x0600920F RID: 37391 RVA: 0x000D5459 File Offset: 0x000D3659
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700661F RID: 26143
			// (set) Token: 0x06009210 RID: 37392 RVA: 0x000D546C File Offset: 0x000D366C
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006620 RID: 26144
			// (set) Token: 0x06009211 RID: 37393 RVA: 0x000D547F File Offset: 0x000D367F
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006621 RID: 26145
			// (set) Token: 0x06009212 RID: 37394 RVA: 0x000D549D File Offset: 0x000D369D
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006622 RID: 26146
			// (set) Token: 0x06009213 RID: 37395 RVA: 0x000D54B5 File Offset: 0x000D36B5
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006623 RID: 26147
			// (set) Token: 0x06009214 RID: 37396 RVA: 0x000D54C8 File Offset: 0x000D36C8
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006624 RID: 26148
			// (set) Token: 0x06009215 RID: 37397 RVA: 0x000D54E0 File Offset: 0x000D36E0
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006625 RID: 26149
			// (set) Token: 0x06009216 RID: 37398 RVA: 0x000D54F8 File Offset: 0x000D36F8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006626 RID: 26150
			// (set) Token: 0x06009217 RID: 37399 RVA: 0x000D550B File Offset: 0x000D370B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006627 RID: 26151
			// (set) Token: 0x06009218 RID: 37400 RVA: 0x000D5523 File Offset: 0x000D3723
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006628 RID: 26152
			// (set) Token: 0x06009219 RID: 37401 RVA: 0x000D553B File Offset: 0x000D373B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006629 RID: 26153
			// (set) Token: 0x0600921A RID: 37402 RVA: 0x000D5553 File Offset: 0x000D3753
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BD5 RID: 3029
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700662A RID: 26154
			// (set) Token: 0x0600921C RID: 37404 RVA: 0x000D5573 File Offset: 0x000D3773
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700662B RID: 26155
			// (set) Token: 0x0600921D RID: 37405 RVA: 0x000D5591 File Offset: 0x000D3791
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700662C RID: 26156
			// (set) Token: 0x0600921E RID: 37406 RVA: 0x000D55A4 File Offset: 0x000D37A4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700662D RID: 26157
			// (set) Token: 0x0600921F RID: 37407 RVA: 0x000D55C2 File Offset: 0x000D37C2
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700662E RID: 26158
			// (set) Token: 0x06009220 RID: 37408 RVA: 0x000D55D5 File Offset: 0x000D37D5
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x1700662F RID: 26159
			// (set) Token: 0x06009221 RID: 37409 RVA: 0x000D55E8 File Offset: 0x000D37E8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006630 RID: 26160
			// (set) Token: 0x06009222 RID: 37410 RVA: 0x000D5606 File Offset: 0x000D3806
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006631 RID: 26161
			// (set) Token: 0x06009223 RID: 37411 RVA: 0x000D561E File Offset: 0x000D381E
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006632 RID: 26162
			// (set) Token: 0x06009224 RID: 37412 RVA: 0x000D5631 File Offset: 0x000D3831
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006633 RID: 26163
			// (set) Token: 0x06009225 RID: 37413 RVA: 0x000D5649 File Offset: 0x000D3849
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006634 RID: 26164
			// (set) Token: 0x06009226 RID: 37414 RVA: 0x000D5661 File Offset: 0x000D3861
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006635 RID: 26165
			// (set) Token: 0x06009227 RID: 37415 RVA: 0x000D5674 File Offset: 0x000D3874
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006636 RID: 26166
			// (set) Token: 0x06009228 RID: 37416 RVA: 0x000D568C File Offset: 0x000D388C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006637 RID: 26167
			// (set) Token: 0x06009229 RID: 37417 RVA: 0x000D56A4 File Offset: 0x000D38A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006638 RID: 26168
			// (set) Token: 0x0600922A RID: 37418 RVA: 0x000D56BC File Offset: 0x000D38BC
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
