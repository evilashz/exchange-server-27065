using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001D2 RID: 466
	public sealed class TimeSpanAppSettingsReader : AppSettingsReader<TimeSpan>
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x00036F84 File Offset: 0x00035184
		public TimeSpanAppSettingsReader(string name, TimeSpanUnits unit, TimeSpan defaultValue) : base(name, defaultValue)
		{
			this.unit = unit;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00036F98 File Offset: 0x00035198
		public static bool TryParseStringValue(string inputValue, TimeSpanUnits unit, out TimeSpan outputValue)
		{
			int num;
			if (int.TryParse(inputValue, out num))
			{
				switch (unit)
				{
				case TimeSpanUnits.Seconds:
					outputValue = TimeSpan.FromSeconds((double)num);
					return true;
				case TimeSpanUnits.Minutes:
					outputValue = TimeSpan.FromMinutes((double)num);
					return true;
				case TimeSpanUnits.Hours:
					outputValue = TimeSpan.FromHours((double)num);
					return true;
				case TimeSpanUnits.Days:
					outputValue = TimeSpan.FromDays((double)num);
					return true;
				}
			}
			outputValue = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00037011 File Offset: 0x00035211
		protected override bool TryParseValue(string inputValue, out TimeSpan outputValue)
		{
			return TimeSpanAppSettingsReader.TryParseStringValue(inputValue, this.unit, out outputValue);
		}

		// Token: 0x040009AA RID: 2474
		private TimeSpanUnits unit;
	}
}
