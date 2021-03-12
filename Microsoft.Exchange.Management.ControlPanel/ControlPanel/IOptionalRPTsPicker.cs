using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000340 RID: 832
	[ServiceContract(Namespace = "ECP", Name = "OptionalRPTsPicker")]
	public interface IOptionalRPTsPicker : IGetListService<AllRPTsFilter, OptionalRetentionPolicyTagRow>
	{
	}
}
