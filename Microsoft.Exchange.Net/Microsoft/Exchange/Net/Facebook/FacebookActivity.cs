using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000711 RID: 1809
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookActivity : IExtensibleDataObject
	{
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000462B6 File Offset: 0x000444B6
		// (set) Token: 0x06002222 RID: 8738 RVA: 0x000462BE File Offset: 0x000444BE
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000462C7 File Offset: 0x000444C7
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x000462CF File Offset: 0x000444CF
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x000462D8 File Offset: 0x000444D8
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x000462E0 File Offset: 0x000444E0
		[DataMember(Name = "category")]
		public string Category { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x000462E9 File Offset: 0x000444E9
		// (set) Token: 0x06002228 RID: 8744 RVA: 0x000462F1 File Offset: 0x000444F1
		[DataMember(Name = "created_time")]
		public string CreatedTime { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000462FA File Offset: 0x000444FA
		// (set) Token: 0x0600222A RID: 8746 RVA: 0x00046302 File Offset: 0x00044502
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
