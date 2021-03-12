using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002BD RID: 701
	[ServiceContract(Namespace = "ECP", Name = "ImportContactList")]
	public interface IImportContactList : IImportObjectService<ImportContactsResult, ImportContactListParameters>
	{
	}
}
