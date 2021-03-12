using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000008 RID: 8
	internal sealed class TimeSpanAppSettingsEntry : AppSettingsEntry<TimeSpan>
	{
		// Token: 0x06000014 RID: 20 RVA: 0x0000238C File Offset: 0x0000058C
		public TimeSpanAppSettingsEntry(string name, TimeSpanUnit unit, TimeSpan defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
			this.unit = unit;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023A0 File Offset: 0x000005A0
		protected override bool TryParseValue(string inputValue, out TimeSpan outputValue)
		{
			int num;
			if (int.TryParse(inputValue, out num))
			{
				switch (this.unit)
				{
				case TimeSpanUnit.Seconds:
					outputValue = TimeSpan.FromSeconds((double)num);
					return true;
				case TimeSpanUnit.Minutes:
					outputValue = TimeSpan.FromMinutes((double)num);
					return true;
				case TimeSpanUnit.Hours:
					outputValue = TimeSpan.FromHours((double)num);
					return true;
				case TimeSpanUnit.Days:
					outputValue = TimeSpan.FromDays((double)num);
					return true;
				}
			}
			outputValue = TimeSpan.Zero;
			return false;
		}

		// Token: 0x04000007 RID: 7
		private TimeSpanUnit unit;
	}
}
