using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000007 RID: 7
	internal sealed class EnumAppSettingsEntry<T> : AppSettingsEntry<T> where T : struct
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002359 File Offset: 0x00000559
		public EnumAppSettingsEntry(string name, T defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002364 File Offset: 0x00000564
		protected override bool TryParseValue(string inputValue, out T outputValue)
		{
			T t;
			if (Enum.TryParse<T>(inputValue, out t))
			{
				outputValue = t;
				return true;
			}
			outputValue = default(T);
			return false;
		}
	}
}
