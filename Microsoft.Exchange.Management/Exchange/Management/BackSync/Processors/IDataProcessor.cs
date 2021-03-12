using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000A7 RID: 167
	internal interface IDataProcessor
	{
		// Token: 0x0600059E RID: 1438
		void Process(PropertyBag propertyBag);

		// Token: 0x0600059F RID: 1439
		void Flush(Func<byte[]> getCookieDelegate, bool moreData);
	}
}
