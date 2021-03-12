using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization
{
	// Token: 0x02000707 RID: 1799
	[ComVisible(true)]
	public interface IObjectReference
	{
		// Token: 0x060050A0 RID: 20640
		[SecurityCritical]
		object GetRealObject(StreamingContext context);
	}
}
