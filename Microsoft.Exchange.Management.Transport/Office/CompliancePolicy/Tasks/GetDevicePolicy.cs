using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.UnifiedPolicy;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Tasks
{
	// Token: 0x020000F6 RID: 246
	[Cmdlet("Get", "DevicePolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetDevicePolicy : GetDevicePolicyBase
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002931C File Offset: 0x0002751C
		protected override IEnumerable<PolicyStorage> GetPagedData()
		{
			QueryFilter internalFilter = this.InternalFilter;
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(PolicyStorage), internalFilter, this.RootId, this.DeepSearch));
			IEnumerable<PolicyStorage> source = base.DataSession.FindPaged<PolicyStorage>(internalFilter, this.RootId, this.DeepSearch, this.InternalSortBy, this.PageSize);
			return from dataObj in source
			where dataObj.Scenario == PolicyScenario.DeviceSettings || dataObj.Scenario == PolicyScenario.DeviceConditionalAccess || dataObj.Scenario == PolicyScenario.DeviceTenantConditionalAccess
			select dataObj;
		}
	}
}
