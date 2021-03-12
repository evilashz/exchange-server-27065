using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008B2 RID: 2226
	[AttributeUsage(AttributeTargets.All)]
	[ComVisible(false)]
	internal sealed class DecoratedNameAttribute : Attribute
	{
		// Token: 0x06005CBA RID: 23738 RVA: 0x00144E6F File Offset: 0x0014306F
		public DecoratedNameAttribute(string decoratedName)
		{
		}
	}
}
