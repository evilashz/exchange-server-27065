using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200009B RID: 155
	[ServiceContract(Namespace = "ECP", Name = "MessageOptions")]
	public interface IMessageOptions : IMessagingBase<MessageOptionsConfiguration, SetMessageOptionsConfiguration>, IEditObjectService<MessageOptionsConfiguration, SetMessageOptionsConfiguration>, IGetObjectService<MessageOptionsConfiguration>
	{
	}
}
