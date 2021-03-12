using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000004 RID: 4
	internal sealed class StringAppSettingsEntry : AppSettingsEntry<string>
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000022CB File Offset: 0x000004CB
		public StringAppSettingsEntry(string name, string defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022D6 File Offset: 0x000004D6
		protected override bool TryParseValue(string inputValue, out string outputValue)
		{
			outputValue = inputValue;
			return true;
		}
	}
}
