using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x0200099F RID: 2463
	[SecurityCritical(SecurityCriticalScope.Everything)]
	[StructLayout(LayoutKind.Sequential)]
	internal class SystemTime
	{
		// Token: 0x0600356C RID: 13676 RVA: 0x00087240 File Offset: 0x00085440
		internal SystemTime(DateTime dateTime)
		{
			this.year = (ushort)dateTime.Year;
			this.month = (ushort)dateTime.Month;
			this.dayOfWeek = (ushort)dateTime.DayOfWeek;
			this.day = (ushort)dateTime.Day;
			this.hour = (ushort)dateTime.Hour;
			this.minute = (ushort)dateTime.Minute;
			this.second = (ushort)dateTime.Second;
			this.milliseconds = (ushort)dateTime.Millisecond;
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000872C3 File Offset: 0x000854C3
		internal static uint Size
		{
			get
			{
				return 16U;
			}
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000872C8 File Offset: 0x000854C8
		internal SystemTime(byte[] dataBuffer)
		{
			this.year = BitConverter.ToUInt16(dataBuffer, 0);
			this.month = BitConverter.ToUInt16(dataBuffer, 2);
			this.dayOfWeek = BitConverter.ToUInt16(dataBuffer, 4);
			this.day = BitConverter.ToUInt16(dataBuffer, 6);
			this.hour = BitConverter.ToUInt16(dataBuffer, 8);
			this.minute = BitConverter.ToUInt16(dataBuffer, 10);
			this.second = BitConverter.ToUInt16(dataBuffer, 12);
			this.milliseconds = BitConverter.ToUInt16(dataBuffer, 14);
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x00087348 File Offset: 0x00085548
		internal DateTime GetDateTime(DateTime defaultValue)
		{
			if (this.year == 0 && this.month == 0 && this.day == 0 && this.hour == 0 && this.minute == 0 && this.second == 0 && this.milliseconds == 0)
			{
				return defaultValue;
			}
			return new DateTime((int)this.year, (int)this.month, (int)this.day, (int)this.hour, (int)this.minute, (int)this.second, (int)this.milliseconds);
		}

		// Token: 0x04002DA2 RID: 11682
		private ushort year;

		// Token: 0x04002DA3 RID: 11683
		private ushort month;

		// Token: 0x04002DA4 RID: 11684
		private ushort dayOfWeek;

		// Token: 0x04002DA5 RID: 11685
		private ushort day;

		// Token: 0x04002DA6 RID: 11686
		private ushort hour;

		// Token: 0x04002DA7 RID: 11687
		private ushort minute;

		// Token: 0x04002DA8 RID: 11688
		private ushort second;

		// Token: 0x04002DA9 RID: 11689
		private ushort milliseconds;
	}
}
