using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	internal interface IMailboxReplicationProxyServiceChannel : IMailboxReplicationProxyService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
	}
}
