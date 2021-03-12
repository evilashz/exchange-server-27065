using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B2 RID: 178
	[ServiceContract(Namespace = "ECP", Name = "ResponseMessage")]
	public interface IResponseMessage : IResourceBase<ResponseMessageConfiguration, SetResponseMessageConfiguration>, IEditObjectService<ResponseMessageConfiguration, SetResponseMessageConfiguration>, IGetObjectService<ResponseMessageConfiguration>
	{
	}
}
