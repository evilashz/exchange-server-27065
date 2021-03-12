using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x020000B2 RID: 178
	[DataContract(Name = "DocumentSharingLocationCollectionSetting", Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	public class DocumentSharingLocationCollectionSetting : UserSetting
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0001814C File Offset: 0x0001634C
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00018154 File Offset: 0x00016354
		[DataMember(Name = "DocumentSharingLocations", IsRequired = true)]
		public DocumentSharingLocationCollection DocumentSharingLocations { get; set; }
	}
}
