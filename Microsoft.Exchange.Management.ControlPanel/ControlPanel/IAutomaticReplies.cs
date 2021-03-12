using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200025F RID: 607
	[ServiceContract(Namespace = "ECP", Name = "AutomaticReplies")]
	public interface IAutomaticReplies : IEditObjectService<AutoReplyConfiguration, SetAutoReplyConfiguration>, IGetObjectService<AutoReplyConfiguration>
	{
	}
}
