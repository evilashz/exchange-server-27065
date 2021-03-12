using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200071C RID: 1820
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookEducationHistoryEntry : IExtensibleDataObject
	{
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x000472D9 File Offset: 0x000454D9
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x000472E1 File Offset: 0x000454E1
		[DataMember(Name = "school")]
		public FacebookSchool School { get; set; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x000472EA File Offset: 0x000454EA
		// (set) Token: 0x06002282 RID: 8834 RVA: 0x000472F2 File Offset: 0x000454F2
		[DataMember(Name = "type")]
		public string Type { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x000472FB File Offset: 0x000454FB
		// (set) Token: 0x06002284 RID: 8836 RVA: 0x00047303 File Offset: 0x00045503
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
