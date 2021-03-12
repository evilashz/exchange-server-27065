using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200009F RID: 159
	[ServiceContract(Namespace = "ECP", Name = "ReadingPane")]
	public interface IReadingPane : IMessagingBase<ReadingPaneConfiguration, SetReadingPaneConfiguration>, IEditObjectService<ReadingPaneConfiguration, SetReadingPaneConfiguration>, IGetObjectService<ReadingPaneConfiguration>
	{
	}
}
