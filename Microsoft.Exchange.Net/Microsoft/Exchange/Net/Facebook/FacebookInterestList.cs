using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200071F RID: 1823
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookInterestList
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x000473AC File Offset: 0x000455AC
		// (set) Token: 0x06002299 RID: 8857 RVA: 0x000473B4 File Offset: 0x000455B4
		[DataMember(Name = "data")]
		public List<FacebookInterest> Interests { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x000473BD File Offset: 0x000455BD
		// (set) Token: 0x0600229B RID: 8859 RVA: 0x000473C5 File Offset: 0x000455C5
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
