using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DBE RID: 3518
	[Serializable]
	public sealed class SharedFolderDataRecipient
	{
		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x060078DA RID: 30938 RVA: 0x00215F2E File Offset: 0x0021412E
		// (set) Token: 0x060078DB RID: 30939 RVA: 0x00215F36 File Offset: 0x00214136
		[XmlElement]
		public string SmtpAddress { get; set; }

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x060078DC RID: 30940 RVA: 0x00215F3F File Offset: 0x0021413F
		// (set) Token: 0x060078DD RID: 30941 RVA: 0x00215F47 File Offset: 0x00214147
		[XmlElement]
		public string SharingKey { get; set; }
	}
}
