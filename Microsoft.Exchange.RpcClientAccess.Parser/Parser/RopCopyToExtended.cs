using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200028D RID: 653
	internal sealed class RopCopyToExtended : DualInputRop
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0002AB52 File Offset: 0x00028D52
		internal override RopId RopId
		{
			get
			{
				return RopId.CopyToExtended;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0002AB59 File Offset: 0x00028D59
		internal static Rop CreateRop()
		{
			return new RopCopyToExtended();
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0002AB60 File Offset: 0x00028D60
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = RopResult.Parse(reader, new RopResult.ResultParserDelegate(SuccessfulCopyToExtendedResult.Parse), new RopResult.ResultParserDelegate(FailedCopyToExtendedResult.Parse));
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0002AB8E File Offset: 0x00028D8E
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex);
			this.copySubObjects = copySubObjects;
			this.copyPropertiesFlags = copyPropertiesFlags;
			this.excludePropertyTags = excludePropertyTags;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0002ABB1 File Offset: 0x00028DB1
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.copySubObjects);
			writer.WriteByte((byte)this.copyPropertiesFlags);
			writer.WriteCountAndPropertyTagArray(this.excludePropertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new CopyToExtendedResultFactory((uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0002ABED File Offset: 0x00028DED
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0002AC02 File Offset: 0x00028E02
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.copySubObjects = reader.ReadBool();
			this.copyPropertiesFlags = (CopyPropertiesFlags)reader.ReadByte();
			this.excludePropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0002AC34 File Offset: 0x00028E34
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			CopyToExtendedResultFactory resultFactory = new CopyToExtendedResultFactory((uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.CopyToExtended(sourceServerObject, destinationServerObject, this.copySubObjects, this.copyPropertiesFlags, this.excludePropertyTags, resultFactory);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0002AC70 File Offset: 0x00028E70
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.copyPropertiesFlags);
			stringBuilder.Append(" CopySubObjects=").Append(this.copySubObjects);
			stringBuilder.Append(" ExcludeTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.excludePropertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x04000751 RID: 1873
		private const RopId RopType = RopId.CopyToExtended;

		// Token: 0x04000752 RID: 1874
		private bool copySubObjects;

		// Token: 0x04000753 RID: 1875
		private CopyPropertiesFlags copyPropertiesFlags;

		// Token: 0x04000754 RID: 1876
		private PropertyTag[] excludePropertyTags;
	}
}
