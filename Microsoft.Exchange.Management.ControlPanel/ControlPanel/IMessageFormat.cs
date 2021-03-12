using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000097 RID: 151
	[ServiceContract(Namespace = "ECP", Name = "MessageFormat")]
	public interface IMessageFormat : IMessagingBase<MessageFormatConfiguration, SetMessageFormatConfiguration>, IEditObjectService<MessageFormatConfiguration, SetMessageFormatConfiguration>, IGetObjectService<MessageFormatConfiguration>
	{
	}
}
