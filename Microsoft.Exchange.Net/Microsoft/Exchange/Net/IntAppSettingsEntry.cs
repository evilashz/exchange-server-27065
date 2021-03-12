using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000003 RID: 3
	internal sealed class IntAppSettingsEntry : AppSettingsEntry<int>
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022B7 File Offset: 0x000004B7
		public IntAppSettingsEntry(string name, int defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022C2 File Offset: 0x000004C2
		protected override bool TryParseValue(string inputValue, out int outputValue)
		{
			return int.TryParse(inputValue, out outputValue);
		}
	}
}
