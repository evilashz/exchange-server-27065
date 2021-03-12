using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D1 RID: 2513
	internal class IStringableHelper
	{
		// Token: 0x060063D6 RID: 25558 RVA: 0x001531CC File Offset: 0x001513CC
		internal static string ToString(object obj)
		{
			IStringable stringable = obj as IStringable;
			if (stringable != null)
			{
				return stringable.ToString();
			}
			return obj.ToString();
		}
	}
}
