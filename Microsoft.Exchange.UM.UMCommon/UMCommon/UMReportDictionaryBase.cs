using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000141 RID: 321
	internal abstract class UMReportDictionaryBase : Dictionary<DateTime, UMReportRawCounters>
	{
		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A2B RID: 2603
		public abstract int MaxItemsInDictionary { get; }
	}
}
