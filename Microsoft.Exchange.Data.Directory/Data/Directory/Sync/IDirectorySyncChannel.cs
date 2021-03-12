using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000980 RID: 2432
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public interface IDirectorySyncChannel : IDirectorySync, IClientChannel, IContextChannel, IChannel, ICommunicationObject, IExtensibleObject<IContextChannel>, IDisposable
	{
	}
}
