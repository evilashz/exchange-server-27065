using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	[DataContract]
	internal sealed class MSInternal1 : ItemPropertiesBase
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002DAA File Offset: 0x00000FAA
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002DB2 File Offset: 0x00000FB2
		[DataMember]
		public MSInternal2[] Field1 { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002DBB File Offset: 0x00000FBB
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002DC3 File Offset: 0x00000FC3
		[DataMember]
		public MSInternal4[] Field2 { get; set; }
	}
}
