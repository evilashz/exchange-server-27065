using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000064 RID: 100
	public class GetAuditLogSearchCommand : SyntheticCommandWithPipelineInput<AuditLogSearchBase, AuditLogSearchBase>
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x00036416 File Offset: 0x00034616
		private GetAuditLogSearchCommand() : base("Get-AuditLogSearch")
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00036423 File Offset: 0x00034623
		public GetAuditLogSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00036432 File Offset: 0x00034632
		public virtual GetAuditLogSearchCommand SetParameters(GetAuditLogSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000065 RID: 101
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700027C RID: 636
			// (set) Token: 0x0600178E RID: 6030 RVA: 0x0003643C File Offset: 0x0003463C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700027D RID: 637
			// (set) Token: 0x0600178F RID: 6031 RVA: 0x0003645A File Offset: 0x0003465A
			public virtual string Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x1700027E RID: 638
			// (set) Token: 0x06001790 RID: 6032 RVA: 0x0003646D File Offset: 0x0003466D
			public virtual ExDateTime? CreatedAfter
			{
				set
				{
					base.PowerSharpParameters["CreatedAfter"] = value;
				}
			}

			// Token: 0x1700027F RID: 639
			// (set) Token: 0x06001791 RID: 6033 RVA: 0x00036485 File Offset: 0x00034685
			public virtual ExDateTime? CreatedBefore
			{
				set
				{
					base.PowerSharpParameters["CreatedBefore"] = value;
				}
			}

			// Token: 0x17000280 RID: 640
			// (set) Token: 0x06001792 RID: 6034 RVA: 0x0003649D File Offset: 0x0003469D
			public virtual int ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17000281 RID: 641
			// (set) Token: 0x06001793 RID: 6035 RVA: 0x000364B5 File Offset: 0x000346B5
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AuditLogSearchIdParameter(value) : null);
				}
			}

			// Token: 0x17000282 RID: 642
			// (set) Token: 0x06001794 RID: 6036 RVA: 0x000364D3 File Offset: 0x000346D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000283 RID: 643
			// (set) Token: 0x06001795 RID: 6037 RVA: 0x000364EB File Offset: 0x000346EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000284 RID: 644
			// (set) Token: 0x06001796 RID: 6038 RVA: 0x00036503 File Offset: 0x00034703
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000285 RID: 645
			// (set) Token: 0x06001797 RID: 6039 RVA: 0x0003651B File Offset: 0x0003471B
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
