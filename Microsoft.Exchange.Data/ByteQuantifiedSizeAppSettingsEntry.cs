using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200005B RID: 91
	internal sealed class ByteQuantifiedSizeAppSettingsEntry : AppSettingsEntry<ByteQuantifiedSize>
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0000CDAB File Offset: 0x0000AFAB
		public ByteQuantifiedSizeAppSettingsEntry(string name, ByteQuantifiedSize defaultValue, Trace tracer) : base(name, defaultValue, tracer)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000CDB6 File Offset: 0x0000AFB6
		protected override bool TryParseValue(string inputValue, out ByteQuantifiedSize outputValue)
		{
			return ByteQuantifiedSize.TryParse(inputValue, out outputValue);
		}
	}
}
