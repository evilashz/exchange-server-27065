using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000C9 RID: 201
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public interface IUpgradeHandlerChannel : IUpgradeHandler, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
	}
}
