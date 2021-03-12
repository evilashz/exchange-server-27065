using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000720 RID: 1824
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookImportContactsResult : IExtensibleDataObject
	{
		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x000473D6 File Offset: 0x000455D6
		// (set) Token: 0x0600229E RID: 8862 RVA: 0x000473DE File Offset: 0x000455DE
		[DataMember(Name = "count", IsRequired = false)]
		public int ProcessedContacts { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x000473E7 File Offset: 0x000455E7
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x000473EF File Offset: 0x000455EF
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
