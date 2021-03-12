using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C6 RID: 966
	[ServiceContract(Namespace = "ECP", Name = "ExportMailboxChanges")]
	public interface IExportMailboxChanges : INewObjectService<ExportMailboxChangesRow, ExportMailboxChangesParameters>
	{
	}
}
