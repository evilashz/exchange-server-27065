using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000723 RID: 1827
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal sealed class FacebookPicture : IExtensibleDataObject
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x0004745D File Offset: 0x0004565D
		// (set) Token: 0x060022AC RID: 8876 RVA: 0x00047465 File Offset: 0x00045665
		[DataMember(Name = "data")]
		public FacebookPictureData Data { get; set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0004746E File Offset: 0x0004566E
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x00047476 File Offset: 0x00045676
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
