using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000737 RID: 1847
	internal static class SerTrace
	{
		// Token: 0x060051F3 RID: 20979 RVA: 0x0011F1F4 File Offset: 0x0011D3F4
		[Conditional("_LOGGING")]
		internal static void InfoLog(params object[] messages)
		{
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x0011F1F6 File Offset: 0x0011D3F6
		[Conditional("SER_LOGGING")]
		internal static void Log(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			messages[0] = messages[0] + " ";
		}
	}
}
