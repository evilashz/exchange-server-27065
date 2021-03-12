using System;
using System.IO;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200013F RID: 319
	internal interface IMIMEDataProperty : IMIMERelatedProperty, IProperty
	{
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000F97 RID: 3991
		// (set) Token: 0x06000F98 RID: 3992
		Stream MIMEData { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000F99 RID: 3993
		// (set) Token: 0x06000F9A RID: 3994
		long MIMESize { get; set; }
	}
}
