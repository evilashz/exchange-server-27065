using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000022 RID: 34
	[OutputType(new Type[]
	{
		typeof(DynamicDistributionGroup)
	})]
	[Cmdlet("Get", "DynamicDistributionGroup", DefaultParameterSetName = "Identity")]
	public sealed class GetDynamicDistributionGroup : GetDynamicDistributionGroupBase
	{
	}
}
