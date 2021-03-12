using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000708 RID: 1800
	[ComVisible(true)]
	public interface ISerializable
	{
		// Token: 0x060050A1 RID: 20641
		[SecurityCritical]
		void GetObjectData(SerializationInfo info, StreamingContext context);
	}
}
