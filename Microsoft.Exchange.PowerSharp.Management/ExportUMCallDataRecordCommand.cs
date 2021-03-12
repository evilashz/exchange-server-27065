using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B2D RID: 2861
	public class ExportUMCallDataRecordCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06008BD8 RID: 35800 RVA: 0x000CD456 File Offset: 0x000CB656
		private ExportUMCallDataRecordCommand() : base("Export-UMCallDataRecord")
		{
		}

		// Token: 0x06008BD9 RID: 35801 RVA: 0x000CD463 File Offset: 0x000CB663
		public ExportUMCallDataRecordCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008BDA RID: 35802 RVA: 0x000CD472 File Offset: 0x000CB672
		public virtual ExportUMCallDataRecordCommand SetParameters(ExportUMCallDataRecordCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B2E RID: 2862
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006137 RID: 24887
			// (set) Token: 0x06008BDB RID: 35803 RVA: 0x000CD47C File Offset: 0x000CB67C
			public virtual string UMDialPlan
			{
				set
				{
					base.PowerSharpParameters["UMDialPlan"] = ((value != null) ? new UMDialPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17006138 RID: 24888
			// (set) Token: 0x06008BDC RID: 35804 RVA: 0x000CD49A File Offset: 0x000CB69A
			public virtual string UMIPGateway
			{
				set
				{
					base.PowerSharpParameters["UMIPGateway"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x17006139 RID: 24889
			// (set) Token: 0x06008BDD RID: 35805 RVA: 0x000CD4B8 File Offset: 0x000CB6B8
			public virtual ExDateTime Date
			{
				set
				{
					base.PowerSharpParameters["Date"] = value;
				}
			}

			// Token: 0x1700613A RID: 24890
			// (set) Token: 0x06008BDE RID: 35806 RVA: 0x000CD4D0 File Offset: 0x000CB6D0
			public virtual Stream ClientStream
			{
				set
				{
					base.PowerSharpParameters["ClientStream"] = value;
				}
			}

			// Token: 0x1700613B RID: 24891
			// (set) Token: 0x06008BDF RID: 35807 RVA: 0x000CD4E3 File Offset: 0x000CB6E3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700613C RID: 24892
			// (set) Token: 0x06008BE0 RID: 35808 RVA: 0x000CD501 File Offset: 0x000CB701
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700613D RID: 24893
			// (set) Token: 0x06008BE1 RID: 35809 RVA: 0x000CD514 File Offset: 0x000CB714
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700613E RID: 24894
			// (set) Token: 0x06008BE2 RID: 35810 RVA: 0x000CD52C File Offset: 0x000CB72C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700613F RID: 24895
			// (set) Token: 0x06008BE3 RID: 35811 RVA: 0x000CD544 File Offset: 0x000CB744
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006140 RID: 24896
			// (set) Token: 0x06008BE4 RID: 35812 RVA: 0x000CD55C File Offset: 0x000CB75C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006141 RID: 24897
			// (set) Token: 0x06008BE5 RID: 35813 RVA: 0x000CD574 File Offset: 0x000CB774
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
