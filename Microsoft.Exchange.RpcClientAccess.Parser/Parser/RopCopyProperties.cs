using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200028B RID: 651
	internal sealed class RopCopyProperties : DualInputRop
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0002A814 File Offset: 0x00028A14
		internal override RopId RopId
		{
			get
			{
				return RopId.CopyProperties;
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0002A818 File Offset: 0x00028A18
		internal static Rop CreateRop()
		{
			return new RopCopyProperties();
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0002A81F File Offset: 0x00028A1F
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = CopyPropertiesResultFactory.Parse(reader);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0002A835 File Offset: 0x00028A35
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, bool reportProgress, CopyPropertiesFlags copyPropertiesFlags, PropertyTag[] propertyTags)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex);
			this.reportProgress = reportProgress;
			this.copyPropertiesFlags = copyPropertiesFlags;
			this.propertyTags = propertyTags;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0002A858 File Offset: 0x00028A58
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.reportProgress);
			writer.WriteByte((byte)this.copyPropertiesFlags);
			writer.WriteCountAndPropertyTagArray(this.propertyTags, FieldLength.WordSize);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002A887 File Offset: 0x00028A87
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new CopyPropertiesResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0002A89A File Offset: 0x00028A9A
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0002A8AF File Offset: 0x00028AAF
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.reportProgress = reader.ReadBool();
			this.copyPropertiesFlags = (CopyPropertiesFlags)reader.ReadByte();
			this.propertyTags = reader.ReadCountAndPropertyTagArray(FieldLength.WordSize);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002A8E0 File Offset: 0x00028AE0
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			CopyPropertiesResultFactory resultFactory = new CopyPropertiesResultFactory(base.LogonIndex, (uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.CopyProperties(sourceServerObject, destinationServerObject, this.reportProgress, this.copyPropertiesFlags, this.propertyTags, resultFactory);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002A920 File Offset: 0x00028B20
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.copyPropertiesFlags);
			stringBuilder.Append(" Progress=").Append(this.reportProgress);
			stringBuilder.Append(" Tags=[");
			Util.AppendToString<PropertyTag>(stringBuilder, this.propertyTags);
			stringBuilder.Append("]");
		}

		// Token: 0x04000748 RID: 1864
		private const RopId RopType = RopId.CopyProperties;

		// Token: 0x04000749 RID: 1865
		private bool reportProgress;

		// Token: 0x0400074A RID: 1866
		private CopyPropertiesFlags copyPropertiesFlags;

		// Token: 0x0400074B RID: 1867
		private PropertyTag[] propertyTags;
	}
}
