using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200041F RID: 1055
	[ServiceContract(Namespace = "ECP", Name = "MessageCategories")]
	public interface IMessageCategories : IGetListService<MessageCategoryFilter, MessageCategory>
	{
	}
}
