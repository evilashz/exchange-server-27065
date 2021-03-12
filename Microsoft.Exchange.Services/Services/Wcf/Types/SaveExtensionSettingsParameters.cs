using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A01 RID: 2561
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SaveExtensionSettingsParameters
	{
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06004857 RID: 18519 RVA: 0x0010169E File Offset: 0x000FF89E
		// (set) Token: 0x06004858 RID: 18520 RVA: 0x001016A6 File Offset: 0x000FF8A6
		[DataMember(IsRequired = true, Order = 1)]
		public string ExtensionId { get; set; }

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x001016AF File Offset: 0x000FF8AF
		// (set) Token: 0x0600485A RID: 18522 RVA: 0x001016B7 File Offset: 0x000FF8B7
		[DataMember(IsRequired = true, Order = 2)]
		public string ExtensionVersion { get; set; }

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600485B RID: 18523 RVA: 0x001016C0 File Offset: 0x000FF8C0
		// (set) Token: 0x0600485C RID: 18524 RVA: 0x001016C8 File Offset: 0x000FF8C8
		[DataMember(IsRequired = true, Order = 3)]
		public string Settings { get; set; }
	}
}
