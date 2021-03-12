using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000343 RID: 835
	internal sealed class RopSetSearchCriteria : InputRop
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x000355AF File Offset: 0x000337AF
		internal override RopId RopId
		{
			get
			{
				return RopId.SetSearchCriteria;
			}
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x000355B3 File Offset: 0x000337B3
		internal static Rop CreateRop()
		{
			return new RopSetSearchCriteria();
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x000355BA File Offset: 0x000337BA
		internal void SetInput(byte logonIndex, byte handleTableIndex, Restriction restriction, StoreId[] folderIds, SetSearchCriteriaFlags setSearchCriteriaFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.restriction = restriction;
			this.folderIds = folderIds;
			this.setSearchCriteriaFlags = setSearchCriteriaFlags;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x000355DB File Offset: 0x000337DB
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteSizedRestriction(this.restriction, string8Encoding, WireFormatStyle.Rop);
			writer.WriteCountedStoreIds(this.folderIds);
			writer.WriteUInt32((uint)this.setSearchCriteriaFlags);
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0003560B File Offset: 0x0003380B
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(StandardRopResult.ParseSuccessResult), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00035639 File Offset: 0x00033839
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopSetSearchCriteria.resultFactory;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00035640 File Offset: 0x00033840
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.restriction = reader.ReadSizeAndRestriction(WireFormatStyle.Rop);
			this.folderIds = reader.ReadSizeAndStoreIdArray();
			this.setSearchCriteriaFlags = (SetSearchCriteriaFlags)reader.ReadUInt32();
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0003566F File Offset: 0x0003386F
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00035684 File Offset: 0x00033884
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.restriction != null)
			{
				this.restriction.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x000356A1 File Offset: 0x000338A1
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.SetSearchCriteria(serverObject, this.restriction, this.folderIds, this.setSearchCriteriaFlags, RopSetSearchCriteria.resultFactory);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x000356C8 File Offset: 0x000338C8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.setSearchCriteriaFlags);
			if (this.folderIds != null)
			{
				stringBuilder.Append(" FIDs=[");
				Util.AppendToString<StoreId>(stringBuilder, this.folderIds);
				stringBuilder.Append("]");
			}
			if (this.restriction != null)
			{
				stringBuilder.Append(" Restriction=[");
				this.restriction.AppendToString(stringBuilder);
				stringBuilder.Append("]");
			}
		}

		// Token: 0x04000AA3 RID: 2723
		private const RopId RopType = RopId.SetSearchCriteria;

		// Token: 0x04000AA4 RID: 2724
		private static SetSearchCriteriaResultFactory resultFactory = new SetSearchCriteriaResultFactory();

		// Token: 0x04000AA5 RID: 2725
		private Restriction restriction;

		// Token: 0x04000AA6 RID: 2726
		private StoreId[] folderIds;

		// Token: 0x04000AA7 RID: 2727
		private SetSearchCriteriaFlags setSearchCriteriaFlags;
	}
}
