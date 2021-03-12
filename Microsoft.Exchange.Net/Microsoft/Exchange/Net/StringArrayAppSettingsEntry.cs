using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000005 RID: 5
	internal sealed class StringArrayAppSettingsEntry : AppSettingsEntry<string[]>
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000022DC File Offset: 0x000004DC
		public StringArrayAppSettingsEntry(string name, string[] defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022E7 File Offset: 0x000004E7
		protected override bool TryParseValue(string inputValue, out string[] outputValue)
		{
			outputValue = inputValue.Split(StringArrayAppSettingsEntry.separator, StringSplitOptions.RemoveEmptyEntries);
			return true;
		}

		// Token: 0x04000006 RID: 6
		private static char[] separator = new char[]
		{
			','
		};
	}
}
