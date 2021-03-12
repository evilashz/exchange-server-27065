using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200034A RID: 842
	internal class SortOrder
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x00035A58 File Offset: 0x00033C58
		internal PropertyTag Tag
		{
			get
			{
				return this.propertyTag;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00035A60 File Offset: 0x00033C60
		internal SortOrderFlags Flags
		{
			get
			{
				return this.sortOrderFlags;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00035A68 File Offset: 0x00033C68
		internal SortOrder(PropertyTag propertyTag, SortOrderFlags sortOrderFlags)
		{
			this.propertyTag = propertyTag;
			this.sortOrderFlags = sortOrderFlags;
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x00035A80 File Offset: 0x00033C80
		internal static SortOrder Parse(Reader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			PropertyTag propertyTag = reader.ReadPropertyTag();
			SortOrderFlags sortOrderFlags = (SortOrderFlags)reader.ReadByte();
			return new SortOrder(propertyTag, sortOrderFlags);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00035AB0 File Offset: 0x00033CB0
		internal void Serialize(Writer writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WritePropertyTag(this.propertyTag);
			writer.WriteByte((byte)this.sortOrderFlags);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00035AD8 File Offset: 0x00033CD8
		public override string ToString()
		{
			return string.Format("SortOrder: [PropertyTag: {0}, SortOrderFlags: {1}]", this.propertyTag, this.sortOrderFlags);
		}

		// Token: 0x04000ABB RID: 2747
		private readonly PropertyTag propertyTag;

		// Token: 0x04000ABC RID: 2748
		private readonly SortOrderFlags sortOrderFlags;
	}
}
