using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002DC RID: 732
	internal sealed class SuccessfulGetSearchCriteriaResult : RopResult
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x0002F612 File Offset: 0x0002D812
		internal SuccessfulGetSearchCriteriaResult(Restriction restriction, byte logonIndex, StoreId[] folderIds, SearchState searchState) : base(RopId.GetSearchCriteria, ErrorCode.None, null)
		{
			this.restriction = restriction;
			this.logonIndex = logonIndex;
			this.folderIds = folderIds;
			this.searchState = searchState;
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0002F63C File Offset: 0x0002D83C
		internal SuccessfulGetSearchCriteriaResult(Reader reader, Encoding string8Encoding) : base(reader)
		{
			this.restriction = reader.ReadSizeAndRestriction(WireFormatStyle.Rop);
			if (this.restriction != null)
			{
				this.restriction.ResolveString8Values(string8Encoding);
			}
			this.logonIndex = reader.ReadByte();
			this.folderIds = reader.ReadSizeAndStoreIdArray();
			this.searchState = (SearchState)reader.ReadUInt32();
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0002F695 File Offset: 0x0002D895
		internal StoreId[] FolderIds
		{
			get
			{
				return this.folderIds;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0002F69D File Offset: 0x0002D89D
		internal Restriction Restriction
		{
			get
			{
				return this.restriction;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0002F6A5 File Offset: 0x0002D8A5
		internal SearchState SearchState
		{
			get
			{
				return this.searchState;
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0002F6AD File Offset: 0x0002D8AD
		internal static SuccessfulGetSearchCriteriaResult Parse(Reader reader, Encoding string8Encoding)
		{
			return new SuccessfulGetSearchCriteriaResult(reader, string8Encoding);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0002F6B6 File Offset: 0x0002D8B6
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteSizedRestriction(this.restriction, base.String8Encoding, WireFormatStyle.Rop);
			writer.WriteByte(this.logonIndex);
			writer.WriteCountedStoreIds(this.folderIds);
			writer.WriteUInt32((uint)this.searchState);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			if (this.restriction != null)
			{
				stringBuilder.Append(" Restriction=[");
				this.restriction.AppendToString(stringBuilder);
				stringBuilder.Append("]");
			}
			stringBuilder.Append(" Folders=[");
			Util.AppendToString<StoreId>(stringBuilder, this.folderIds);
			stringBuilder.Append("] SearchState=").Append(this.searchState);
		}

		// Token: 0x04000864 RID: 2148
		private readonly Restriction restriction;

		// Token: 0x04000865 RID: 2149
		private readonly StoreId[] folderIds;

		// Token: 0x04000866 RID: 2150
		private readonly byte logonIndex;

		// Token: 0x04000867 RID: 2151
		private readonly SearchState searchState;
	}
}
