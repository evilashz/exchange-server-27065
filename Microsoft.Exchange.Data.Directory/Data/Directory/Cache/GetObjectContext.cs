using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000A9 RID: 169
	[DataContract]
	internal class GetObjectContext : CacheResponseContext
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x00029676 File Offset: 0x00027876
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0002967E File Offset: 0x0002787E
		[DataMember]
		internal SimpleADObject Object { get; set; }
	}
}
