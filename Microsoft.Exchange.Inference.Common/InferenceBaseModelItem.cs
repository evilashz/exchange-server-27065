using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000031 RID: 49
	[DataContract(Name = "InferenceBaseModelData")]
	[Serializable]
	internal abstract class InferenceBaseModelItem
	{
		// Token: 0x060000DA RID: 218 RVA: 0x0000340B File Offset: 0x0000160B
		public InferenceBaseModelItem()
		{
			this.CreationTime = DateTime.UtcNow;
			this.LastModifiedTime = DateTime.UtcNow;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003429 File Offset: 0x00001629
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00003431 File Offset: 0x00001631
		[DataMember]
		public Version Version { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000343A File Offset: 0x0000163A
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003442 File Offset: 0x00001642
		[DataMember]
		public DateTime CreationTime { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000344B File Offset: 0x0000164B
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00003453 File Offset: 0x00001653
		[DataMember]
		public DateTime LastModifiedTime { get; set; }
	}
}
