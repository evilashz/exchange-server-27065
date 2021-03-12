using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000721 RID: 1825
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class FacebookLocation : IExtensibleDataObject
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x00047400 File Offset: 0x00045600
		// (set) Token: 0x060022A3 RID: 8867 RVA: 0x00047408 File Offset: 0x00045608
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x00047411 File Offset: 0x00045611
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x00047419 File Offset: 0x00045619
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x00047422 File Offset: 0x00045622
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0004742A File Offset: 0x0004562A
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
