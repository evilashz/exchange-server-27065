using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B2E RID: 2862
	[Cmdlet("Get", "OutboundConnector", DefaultParameterSetName = "Identity")]
	public class GetOutboundConnector : GetMultitenancySystemConfigurationObjectTask<OutboundConnectorIdParameter, TenantOutboundConnector>
	{
		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x060066E8 RID: 26344 RVA: 0x001A94C8 File Offset: 0x001A76C8
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x060066E9 RID: 26345 RVA: 0x001A94CB File Offset: 0x001A76CB
		// (set) Token: 0x060066EA RID: 26346 RVA: 0x001A94E2 File Offset: 0x001A76E2
		[Parameter(Mandatory = false)]
		public bool IsTransportRuleScoped
		{
			get
			{
				return (bool)base.Fields["IsTransportRuleScoped"];
			}
			set
			{
				base.Fields["IsTransportRuleScoped"] = value;
			}
		}

		// Token: 0x060066EB RID: 26347 RVA: 0x001A94FA File Offset: 0x001A76FA
		protected override void WriteResult(IConfigurable dataObject)
		{
			if (base.Fields.IsModified("IsTransportRuleScoped"))
			{
				if (((TenantOutboundConnector)dataObject).IsTransportRuleScoped == this.IsTransportRuleScoped)
				{
					base.WriteResult(dataObject);
					return;
				}
			}
			else
			{
				base.WriteResult(dataObject);
			}
		}
	}
}
