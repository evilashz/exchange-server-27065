using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F5A RID: 3930
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecommendedGroupsInfo
	{
		// Token: 0x060086AB RID: 34475 RVA: 0x0024EEEB File Offset: 0x0024D0EB
		public RecommendedGroupsInfo()
		{
			this.RecommendedGroups = new List<RecommendedGroupInfo>();
		}

		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x060086AC RID: 34476 RVA: 0x0024EEFE File Offset: 0x0024D0FE
		// (set) Token: 0x060086AD RID: 34477 RVA: 0x0024EF06 File Offset: 0x0024D106
		public List<RecommendedGroupInfo> RecommendedGroups { get; set; }

		// Token: 0x060086AE RID: 34478 RVA: 0x0024EF10 File Offset: 0x0024D110
		public void Write(BinaryWriter writer)
		{
			writer.Write((short)RecommendedGroupsInfo.serializationVersion);
			writer.Write(this.serializationFlags);
			writer.Write(this.RecommendedGroups.Count);
			foreach (RecommendedGroupInfo recommendedGroupInfo in this.RecommendedGroups)
			{
				recommendedGroupInfo.Write(writer);
			}
		}

		// Token: 0x060086AF RID: 34479 RVA: 0x0024EF8C File Offset: 0x0024D18C
		public void Read(BinaryReader reader)
		{
			int num = (int)reader.ReadInt16();
			if (num > RecommendedGroupsInfo.serializationVersion)
			{
				throw new SerializationException(string.Format("Recommended groups info was serialized using an unsupported serialization version. Recommended groups info serialization version: {0}, Current serialization version: {1}", num, RecommendedGroupsInfo.serializationVersion));
			}
			this.serializationFlags = reader.ReadInt64();
			int num2 = reader.ReadInt32();
			for (int i = 0; i < num2; i++)
			{
				RecommendedGroupInfo recommendedGroupInfo = new RecommendedGroupInfo();
				recommendedGroupInfo.Read(reader);
				this.RecommendedGroups.Add(recommendedGroupInfo);
			}
		}

		// Token: 0x04005A18 RID: 23064
		public const string Name = "Inference.RecommendedGroups";

		// Token: 0x04005A19 RID: 23065
		private static readonly int serializationVersion = 1;

		// Token: 0x04005A1A RID: 23066
		private long serializationFlags;
	}
}
