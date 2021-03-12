using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200072A RID: 1834
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookWorkHistoryEntry : IExtensibleDataObject
	{
		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000479C0 File Offset: 0x00045BC0
		// (set) Token: 0x060022FF RID: 8959 RVA: 0x000479C8 File Offset: 0x00045BC8
		[DataMember(Name = "employer")]
		public FacebookEmployer Employer { get; set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002300 RID: 8960 RVA: 0x000479D1 File Offset: 0x00045BD1
		// (set) Token: 0x06002301 RID: 8961 RVA: 0x000479D9 File Offset: 0x00045BD9
		[DataMember(Name = "position")]
		public FacebookWorkPosition Position { get; set; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x000479E2 File Offset: 0x00045BE2
		// (set) Token: 0x06002303 RID: 8963 RVA: 0x000479EA File Offset: 0x00045BEA
		[DataMember(Name = "start_date")]
		public string StartDate { get; set; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x000479F3 File Offset: 0x00045BF3
		// (set) Token: 0x06002305 RID: 8965 RVA: 0x000479FB File Offset: 0x00045BFB
		[DataMember(Name = "end_date")]
		public string EndDate { get; set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x00047A04 File Offset: 0x00045C04
		// (set) Token: 0x06002307 RID: 8967 RVA: 0x00047A0C File Offset: 0x00045C0C
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
