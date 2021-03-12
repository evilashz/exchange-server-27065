using System;
using System.Runtime.CompilerServices;

namespace System.StubHelpers
{
	// Token: 0x02000578 RID: 1400
	internal static class WinRTTypeNameConverter
	{
		// Token: 0x06004204 RID: 16900
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ConvertToWinRTTypeName(Type managedType, out bool isPrimitive);

		// Token: 0x06004205 RID: 16901
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type GetTypeFromWinRTTypeName(string typeName, out bool isPrimitive);
	}
}
