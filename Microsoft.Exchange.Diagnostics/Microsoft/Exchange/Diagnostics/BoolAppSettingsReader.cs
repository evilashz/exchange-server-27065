using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020001CB RID: 459
	public sealed class BoolAppSettingsReader : AppSettingsReader<bool>
	{
		// Token: 0x06000CDA RID: 3290 RVA: 0x0002F5D0 File Offset: 0x0002D7D0
		public BoolAppSettingsReader(string name, bool defaultValue) : base(name, defaultValue)
		{
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		public static bool TryParseStringValue(string inputValue, out bool outputValue)
		{
			int num;
			if (int.TryParse(inputValue, out num))
			{
				outputValue = (num != 0);
				return true;
			}
			bool flag;
			if (bool.TryParse(inputValue, out flag))
			{
				outputValue = flag;
				return true;
			}
			outputValue = false;
			return false;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002F611 File Offset: 0x0002D811
		protected override bool TryParseValue(string inputValue, out bool outputValue)
		{
			return BoolAppSettingsReader.TryParseStringValue(inputValue, out outputValue);
		}
	}
}
