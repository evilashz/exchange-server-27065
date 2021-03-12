using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F59 RID: 3929
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecommendedGroupInfo : IRecommendedGroupInfo
	{
		// Token: 0x060086A2 RID: 34466 RVA: 0x0024ED4A File Offset: 0x0024CF4A
		public RecommendedGroupInfo()
		{
			this.ID = Guid.Empty;
			this.Members = new List<string>();
			this.Words = new List<string>();
		}

		// Token: 0x170023AD RID: 9133
		// (get) Token: 0x060086A3 RID: 34467 RVA: 0x0024ED73 File Offset: 0x0024CF73
		// (set) Token: 0x060086A4 RID: 34468 RVA: 0x0024ED7B File Offset: 0x0024CF7B
		public Guid ID { get; set; }

		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x060086A5 RID: 34469 RVA: 0x0024ED84 File Offset: 0x0024CF84
		// (set) Token: 0x060086A6 RID: 34470 RVA: 0x0024ED8C File Offset: 0x0024CF8C
		public List<string> Members { get; set; }

		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x060086A7 RID: 34471 RVA: 0x0024ED95 File Offset: 0x0024CF95
		// (set) Token: 0x060086A8 RID: 34472 RVA: 0x0024ED9D File Offset: 0x0024CF9D
		public List<string> Words { get; set; }

		// Token: 0x060086A9 RID: 34473 RVA: 0x0024EDA8 File Offset: 0x0024CFA8
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.ID.ToByteArray());
			writer.Write(this.Members.Count);
			foreach (string value in this.Members)
			{
				writer.Write(value);
			}
			writer.Write(this.Words.Count);
			foreach (string value2 in this.Words)
			{
				writer.Write(value2);
			}
		}

		// Token: 0x060086AA RID: 34474 RVA: 0x0024EE74 File Offset: 0x0024D074
		public void Read(BinaryReader reader)
		{
			Guid id = new Guid(reader.ReadBytes(16));
			this.ID = id;
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = reader.ReadString();
				this.Members.Add(item);
			}
			int num2 = reader.ReadInt32();
			for (int j = 0; j < num2; j++)
			{
				string item2 = reader.ReadString();
				this.Words.Add(item2);
			}
		}

		// Token: 0x04005A14 RID: 23060
		private const int GuidBytesLength = 16;
	}
}
