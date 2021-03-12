using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200028C RID: 652
	internal sealed class RopCopyTo : DualInputRop
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0002A993 File Offset: 0x00028B93
		internal override RopId RopId
		{
			get
			{
				return RopId.CopyTo;
			}
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002A997 File Offset: 0x00028B97
		internal static Rop CreateRop()
		{
			return new RopCopyTo();
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0002A99E File Offset: 0x00028B9E
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = CopyToResultFactory.Parse(reader);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x0002A9B4 File Offset: 0x00028BB4
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, bool reportProgress, bool copySubObjects, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] excludePropertyTags)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex);
			this.reportProgress = reportProgress;
			this.copySubObjects = copySubObjects;
			this.copyPropertiesFlags = copyPropertiesFlags;
			this.excludePropertyTags = excludePropertyTags;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002A9DF File Offset: 0x00028BDF
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.reportProgress);
			writer.WriteBool(this.copySubObjects);
			writer.WriteByte((byte)this.copyPropertiesFlags);
			writer.WriteCountAndPropertyTagArray(this.excludePropertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0002AA1A File Offset: 0x00028C1A
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new CopyToResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002AA2D File Offset: 0x00028C2D
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0002AA42 File Offset: 0x00028C42
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.reportProgress = reader.ReadBool();
			this.copySubObjects = reader.ReadBool();
			this.copyPropertiesFlags = (CopyPropertiesFlags)reader.ReadByte();
			this.excludePropertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0002AA80 File Offset: 0x00028C80
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			CopyToResultFactory resultFactory = new CopyToResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.CopyTo(sourceServerObject, destinationServerObject, this.reportProgress, this.copySubObjects, this.copyPropertiesFlags, this.excludePropertyTags, resultFactory);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.copyPropertiesFlags);
			stringBuilder.Append(" CopySubObjects=").Append(this.copySubObjects);
			stringBuilder.Append(" Progress=").Append(this.reportProgress);
			stringBuilder.Append(" ExcludeTags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.excludePropertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x0400074C RID: 1868
		private const RopId RopType = RopId.CopyTo;

		// Token: 0x0400074D RID: 1869
		private bool reportProgress;

		// Token: 0x0400074E RID: 1870
		private bool copySubObjects;

		// Token: 0x0400074F RID: 1871
		private CopyPropertiesFlags copyPropertiesFlags;

		// Token: 0x04000750 RID: 1872
		private PropertyTag[] excludePropertyTags;
	}
}
