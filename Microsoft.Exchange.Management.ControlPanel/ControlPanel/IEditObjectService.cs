using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000065 RID: 101
	[ServiceContract(Namespace = "ECP", Name = "IEditObjectService")]
	public interface IEditObjectService<O, U> : IGetObjectService<O> where O : BaseRow
	{
		// Token: 0x06001A7F RID: 6783
		[OperationContract]
		PowerShellResults<O> SetObject(Identity identity, U properties);
	}
}
