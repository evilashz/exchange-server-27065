using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000AE RID: 174
	[ServiceContract(Namespace = "ECP", Name = "ResourceBase")]
	public interface IResourceBase<O, U> : IEditObjectService<O, U>, IGetObjectService<O> where O : ResourceConfigurationBase where U : SetResourceConfigurationBase
	{
	}
}
