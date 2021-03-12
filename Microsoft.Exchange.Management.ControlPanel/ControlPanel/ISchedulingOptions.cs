using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B7 RID: 183
	[ServiceContract(Namespace = "ECP", Name = "SchedulingOptions")]
	public interface ISchedulingOptions : IResourceBase<SchedulingOptionsConfiguration, SetSchedulingOptionsConfiguration>, IEditObjectService<SchedulingOptionsConfiguration, SetSchedulingOptionsConfiguration>, IGetObjectService<SchedulingOptionsConfiguration>
	{
	}
}
