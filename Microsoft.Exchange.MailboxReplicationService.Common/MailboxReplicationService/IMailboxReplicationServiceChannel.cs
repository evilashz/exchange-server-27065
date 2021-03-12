using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200014E RID: 334
	internal interface IMailboxReplicationServiceChannel : IMailboxReplicationService, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
	}
}
