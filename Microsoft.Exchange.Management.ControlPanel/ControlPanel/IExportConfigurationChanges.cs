using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003C2 RID: 962
	[ServiceContract(Namespace = "ECP", Name = "ExportConfigurationChanges")]
	public interface IExportConfigurationChanges : INewObjectService<ExportConfigurationChangesRow, ExportConfigurationChangesParameters>
	{
	}
}
