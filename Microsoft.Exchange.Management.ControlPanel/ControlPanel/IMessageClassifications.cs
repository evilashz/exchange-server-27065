using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000423 RID: 1059
	[ServiceContract(Namespace = "ECP", Name = "MessageClassifications")]
	public interface IMessageClassifications : IGetListService<MessageClassificationFilter, MessageClassification>
	{
	}
}
