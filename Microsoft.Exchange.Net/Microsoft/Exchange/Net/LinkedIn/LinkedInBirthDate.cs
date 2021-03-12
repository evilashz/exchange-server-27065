using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000747 RID: 1863
	[DataContract]
	public class LinkedInBirthDate : IExtensibleDataObject
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x0004C216 File Offset: 0x0004A416
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x0004C21E File Offset: 0x0004A41E
		[DataMember(Name = "day")]
		public int Day { get; set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0004C227 File Offset: 0x0004A427
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x0004C22F File Offset: 0x0004A42F
		[DataMember(Name = "month")]
		public int Month { get; set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0004C238 File Offset: 0x0004A438
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x0004C240 File Offset: 0x0004A440
		[DataMember(Name = "year")]
		public int Year { get; set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x0004C249 File Offset: 0x0004A449
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x0004C251 File Offset: 0x0004A451
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
