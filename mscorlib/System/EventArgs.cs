using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000DF RID: 223
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class EventArgs
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x0002CC17 File Offset: 0x0002AE17
		[__DynamicallyInvokable]
		public EventArgs()
		{
		}

		// Token: 0x0400057C RID: 1404
		[__DynamicallyInvokable]
		public static readonly EventArgs Empty = new EventArgs();
	}
}
