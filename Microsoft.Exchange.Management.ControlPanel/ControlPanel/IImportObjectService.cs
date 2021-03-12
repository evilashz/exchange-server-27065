using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002BC RID: 700
	[ServiceContract(Namespace = "ECP", Name = "IImportObjectService")]
	public interface IImportObjectService<O, U> where O : BaseRow
	{
		// Token: 0x06002C14 RID: 11284
		[OperationContract]
		PowerShellResults<O> ImportObject(Identity identity, U properties);
	}
}
