using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Diagnostics.CmdletInfra
{
	// Token: 0x02000102 RID: 258
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GenericCmdletInfoDataLogger : IPerformanceDataLogger
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0001DF28 File Offset: 0x0001C128
		private GenericCmdletInfoDataLogger()
		{
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001DF30 File Offset: 0x0001C130
		public void Log(string marker, string counter, TimeSpan value)
		{
			CmdletLogger.SafeAppendGenericInfo(string.Format("{0}.{1}", marker, counter), ((long)value.TotalMilliseconds).ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001DF63 File Offset: 0x0001C163
		public void Log(string marker, string counter, uint value)
		{
			CmdletLogger.SafeAppendGenericInfo(string.Format("{0}.{1}", marker, counter), value.ToString(NumberFormatInfo.InvariantInfo));
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001DF82 File Offset: 0x0001C182
		public void Log(string marker, string counter, string value)
		{
			CmdletLogger.SafeAppendGenericInfo(string.Format("{0}.{1}", marker, counter), value);
		}

		// Token: 0x040004BC RID: 1212
		public static readonly IPerformanceDataLogger Instance = new GenericCmdletInfoDataLogger();
	}
}
