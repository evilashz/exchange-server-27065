using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200072B RID: 1835
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookWorkPosition : IExtensibleDataObject
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x00047A1D File Offset: 0x00045C1D
		// (set) Token: 0x0600230A RID: 8970 RVA: 0x00047A25 File Offset: 0x00045C25
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x00047A2E File Offset: 0x00045C2E
		// (set) Token: 0x0600230C RID: 8972 RVA: 0x00047A36 File Offset: 0x00045C36
		[DataMember(Name = "name")]
		public string Name { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x00047A3F File Offset: 0x00045C3F
		// (set) Token: 0x0600230E RID: 8974 RVA: 0x00047A47 File Offset: 0x00045C47
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
