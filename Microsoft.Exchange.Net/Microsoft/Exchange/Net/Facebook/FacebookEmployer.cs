using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200071D RID: 1821
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookEmployer : IExtensibleDataObject
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00047314 File Offset: 0x00045514
		// (set) Token: 0x06002287 RID: 8839 RVA: 0x0004731C File Offset: 0x0004551C
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00047325 File Offset: 0x00045525
		// (set) Token: 0x06002289 RID: 8841 RVA: 0x0004732D File Offset: 0x0004552D
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x00047336 File Offset: 0x00045536
		// (set) Token: 0x0600228B RID: 8843 RVA: 0x0004733E File Offset: 0x0004553E
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
