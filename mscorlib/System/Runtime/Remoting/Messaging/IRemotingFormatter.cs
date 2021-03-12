using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000830 RID: 2096
	[ComVisible(true)]
	public interface IRemotingFormatter : IFormatter
	{
		// Token: 0x06005979 RID: 22905
		object Deserialize(Stream serializationStream, HeaderHandler handler);

		// Token: 0x0600597A RID: 22906
		void Serialize(Stream serializationStream, object graph, Header[] headers);
	}
}
