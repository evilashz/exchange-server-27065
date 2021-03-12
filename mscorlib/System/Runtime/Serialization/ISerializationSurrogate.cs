using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000709 RID: 1801
	[ComVisible(true)]
	public interface ISerializationSurrogate
	{
		// Token: 0x060050A2 RID: 20642
		[SecurityCritical]
		void GetObjectData(object obj, SerializationInfo info, StreamingContext context);

		// Token: 0x060050A3 RID: 20643
		[SecurityCritical]
		object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector);
	}
}
