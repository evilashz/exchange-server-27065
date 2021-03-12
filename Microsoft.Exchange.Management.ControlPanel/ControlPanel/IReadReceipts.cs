using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A4 RID: 164
	[ServiceContract(Namespace = "ECP", Name = "ReadReceipts")]
	public interface IReadReceipts : IMessagingBase<ReadReceiptsConfiguration, SetReadReceiptsConfiguration>, IEditObjectService<ReadReceiptsConfiguration, SetReadReceiptsConfiguration>, IGetObjectService<ReadReceiptsConfiguration>
	{
	}
}
