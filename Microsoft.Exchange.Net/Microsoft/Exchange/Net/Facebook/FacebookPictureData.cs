using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000724 RID: 1828
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal sealed class FacebookPictureData : IExtensibleDataObject
	{
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00047487 File Offset: 0x00045687
		// (set) Token: 0x060022B1 RID: 8881 RVA: 0x0004748F File Offset: 0x0004568F
		[DataMember(Name = "url")]
		public string Url { get; set; }

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00047498 File Offset: 0x00045698
		// (set) Token: 0x060022B3 RID: 8883 RVA: 0x000474A0 File Offset: 0x000456A0
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
