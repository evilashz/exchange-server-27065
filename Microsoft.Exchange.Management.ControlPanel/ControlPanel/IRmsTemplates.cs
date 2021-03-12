using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000428 RID: 1064
	[ServiceContract(Namespace = "ECP", Name = "RmsTemplates")]
	public interface IRmsTemplates : IGetListService<RmsTemplateFilter, RmsTemplate>
	{
	}
}
