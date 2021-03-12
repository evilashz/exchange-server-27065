using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002ED RID: 749
	internal sealed class RopIncrementalConfig : InputOutputRop
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x000301F8 File Offset: 0x0002E3F8
		internal override RopId RopId
		{
			get
			{
				return RopId.IncrementalConfig;
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000301FC File Offset: 0x0002E3FC
		internal static Rop CreateRop()
		{
			return new RopIncrementalConfig();
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00030204 File Offset: 0x0002E404
		internal void SetInput(byte logonIndex, byte handleTableIndex, byte returnHandleTableIndex, IncrementalConfigOption configOptions, FastTransferSendOption sendOptions, SyncFlag syncFlags, Restriction restriction, SyncExtraFlag extraFlags, PropertyTag[] propertyTags, StoreId[] messageIds)
		{
			base.SetCommonInput(logonIndex, handleTableIndex, returnHandleTableIndex);
			this.configOptions = configOptions;
			this.sendOptions = sendOptions;
			this.syncFlags = syncFlags;
			this.restriction = restriction;
			this.extraFlags = extraFlags;
			this.propertyTags = propertyTags;
			this.messageIds = messageIds;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00030254 File Offset: 0x0002E454
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteByte((byte)this.configOptions);
			writer.WriteByte((byte)this.sendOptions);
			writer.WriteUInt16((ushort)this.syncFlags);
			writer.WriteSizedRestriction(this.restriction, string8Encoding, WireFormatStyle.Rop);
			writer.WriteUInt32((uint)this.extraFlags);
			writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
			if ((ushort)(this.syncFlags & SyncFlag.MessageSelective) != 0)
			{
				writer.WriteCountedStoreIds(this.messageIds);
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000302CF File Offset: 0x0002E4CF
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulIncrementalConfigResult.Parse), new RopResult.ResultParserDelegate(StandardRopResult.ParseFailResult));
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000302FD File Offset: 0x0002E4FD
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopIncrementalConfig.resultFactory;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00030304 File Offset: 0x0002E504
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.configOptions = (IncrementalConfigOption)reader.ReadByte();
			this.sendOptions = (FastTransferSendOption)reader.ReadByte();
			this.syncFlags = (SyncFlag)reader.ReadUInt16();
			this.restriction = reader.ReadSizeAndRestriction(WireFormatStyle.Rop);
			this.extraFlags = (SyncExtraFlag)reader.ReadUInt32();
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
			if ((ushort)(this.syncFlags & SyncFlag.MessageSelective) != 0)
			{
				this.messageIds = reader.ReadSizeAndStoreIdArray();
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0003037E File Offset: 0x0002E57E
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00030393 File Offset: 0x0002E593
		internal override void ResolveString8Values(Encoding string8Encoding)
		{
			base.ResolveString8Values(string8Encoding);
			if (this.restriction != null)
			{
				this.restriction.ResolveString8Values(string8Encoding);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000303B0 File Offset: 0x0002E5B0
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.IncrementalConfig(serverObject, this.configOptions, this.sendOptions, this.syncFlags, this.restriction, this.extraFlags, this.propertyTags, this.messageIds, RopIncrementalConfig.resultFactory);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000303FC File Offset: 0x0002E5FC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" icsOptions=").Append(this.configOptions.ToString());
			stringBuilder.Append(" fxOptions=").Append(this.sendOptions.ToString());
			stringBuilder.Append(" syncFlags=").Append(this.syncFlags.ToString());
			stringBuilder.Append(" extraSyncFlags=").Append(this.extraFlags.ToString());
			stringBuilder.Append(" restriction=[");
			if (this.restriction != null)
			{
				this.restriction.AppendToString(stringBuilder);
			}
			stringBuilder.Append("]");
			stringBuilder.Append(" propertyTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
			stringBuilder.Append(" messageIds=[");
			Util.AppendToString<StoreId>(stringBuilder, this.messageIds);
			stringBuilder.Append("]");
		}

		// Token: 0x0400093F RID: 2367
		private const RopId RopType = RopId.IncrementalConfig;

		// Token: 0x04000940 RID: 2368
		private static IncrementalConfigResultFactory resultFactory = new IncrementalConfigResultFactory();

		// Token: 0x04000941 RID: 2369
		private IncrementalConfigOption configOptions;

		// Token: 0x04000942 RID: 2370
		private FastTransferSendOption sendOptions;

		// Token: 0x04000943 RID: 2371
		private SyncFlag syncFlags;

		// Token: 0x04000944 RID: 2372
		private Restriction restriction;

		// Token: 0x04000945 RID: 2373
		private SyncExtraFlag extraFlags;

		// Token: 0x04000946 RID: 2374
		private PropertyTag[] propertyTags;

		// Token: 0x04000947 RID: 2375
		private StoreId[] messageIds;
	}
}
