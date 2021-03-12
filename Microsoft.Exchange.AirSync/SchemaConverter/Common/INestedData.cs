using System;
using System.Collections;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x02000184 RID: 388
	internal interface INestedData
	{
		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060010BA RID: 4282
		IDictionary SubProperties { get; }

		// Token: 0x060010BB RID: 4283
		void Clear();
	}
}
