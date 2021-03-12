using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200083F RID: 2111
	internal interface ISerializationRootObject
	{
		// Token: 0x06005AA9 RID: 23209
		[SecurityCritical]
		void RootSetObjectData(SerializationInfo info, StreamingContext ctx);
	}
}
