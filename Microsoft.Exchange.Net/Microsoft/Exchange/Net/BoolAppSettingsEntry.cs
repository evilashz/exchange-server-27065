using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000006 RID: 6
	internal sealed class BoolAppSettingsEntry : AppSettingsEntry<bool>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002317 File Offset: 0x00000517
		public BoolAppSettingsEntry(string name, bool defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002324 File Offset: 0x00000524
		protected override bool TryParseValue(string inputValue, out bool outputValue)
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
	}
}
