using System;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000C7D RID: 3197
	internal abstract class DebugSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060046C6 RID: 18118 RVA: 0x000BE459 File Offset: 0x000BC659
		internal DebugSafeHandle() : base(true)
		{
		}
	}
}
