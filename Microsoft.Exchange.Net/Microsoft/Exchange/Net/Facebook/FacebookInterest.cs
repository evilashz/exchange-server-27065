using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200071E RID: 1822
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookInterest : IExtensibleDataObject
	{
		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x0004734F File Offset: 0x0004554F
		// (set) Token: 0x0600228E RID: 8846 RVA: 0x00047357 File Offset: 0x00045557
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x00047360 File Offset: 0x00045560
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x00047368 File Offset: 0x00045568
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x00047371 File Offset: 0x00045571
		// (set) Token: 0x06002292 RID: 8850 RVA: 0x00047379 File Offset: 0x00045579
		[DataMember(Name = "category")]
		public string Category { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x00047382 File Offset: 0x00045582
		// (set) Token: 0x06002294 RID: 8852 RVA: 0x0004738A File Offset: 0x0004558A
		[DataMember(Name = "created_time")]
		public string CreatedTime { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x00047393 File Offset: 0x00045593
		// (set) Token: 0x06002296 RID: 8854 RVA: 0x0004739B File Offset: 0x0004559B
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
