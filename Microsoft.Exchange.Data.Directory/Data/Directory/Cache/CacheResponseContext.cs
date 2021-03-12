using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x02000096 RID: 150
	[DataContract]
	internal class CacheResponseContext
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00027161 File Offset: 0x00025361
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00027169 File Offset: 0x00025369
		[DataMember]
		internal long BeginOperationLatency { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00027172 File Offset: 0x00025372
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0002717A File Offset: 0x0002537A
		[DataMember]
		internal ADCacheResultState ResultState { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00027183 File Offset: 0x00025383
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0002718B File Offset: 0x0002538B
		[DataMember]
		internal long EndOperationLatency { get; set; }
	}
}
