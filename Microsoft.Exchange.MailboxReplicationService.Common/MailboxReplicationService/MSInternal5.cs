using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000C RID: 12
	[DataContract]
	internal sealed class MSInternal5 : ItemPropertiesBase
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002DD4 File Offset: 0x00000FD4
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002DDC File Offset: 0x00000FDC
		[DataMember]
		public MSInternal6[] Field1 { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002DE5 File Offset: 0x00000FE5
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002DED File Offset: 0x00000FED
		[DataMember]
		public string Field2 { get; set; }
	}
}
