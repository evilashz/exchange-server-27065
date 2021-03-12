using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200028E RID: 654
	internal sealed class RopCopyToStream : DualInputRop
	{
		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0002ACE3 File Offset: 0x00028EE3
		internal override RopId RopId
		{
			get
			{
				return RopId.CopyToStream;
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0002ACE7 File Offset: 0x00028EE7
		internal static Rop CreateRop()
		{
			return new RopCopyToStream();
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0002ACEE File Offset: 0x00028EEE
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = CopyToStreamResult.Parse(reader);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0002AD04 File Offset: 0x00028F04
		internal void SetInput(byte logonIndex, byte sourceHandleTableIndex, byte destinationHandleTableIndex, ulong bytesToCopy)
		{
			base.SetCommonInput(logonIndex, sourceHandleTableIndex, destinationHandleTableIndex);
			this.bytesToCopy = bytesToCopy;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0002AD17 File Offset: 0x00028F17
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteUInt64(this.bytesToCopy);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002AD2D File Offset: 0x00028F2D
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return new CopyToStreamResultFactory((uint)base.DestinationHandleTableIndex);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0002AD3A File Offset: 0x00028F3A
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002AD4F File Offset: 0x00028F4F
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.bytesToCopy = reader.ReadUInt64();
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0002AD68 File Offset: 0x00028F68
		protected override void InternalExecute(IServerObject sourceServerObject, IServerObject destinationServerObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			CopyToStreamResultFactory resultFactory = new CopyToStreamResultFactory((uint)base.DestinationHandleTableIndex);
			this.result = ropHandler.CopyToStream(sourceServerObject, destinationServerObject, this.bytesToCopy, resultFactory);
		}

		// Token: 0x04000755 RID: 1877
		private const RopId RopType = RopId.CopyToStream;

		// Token: 0x04000756 RID: 1878
		private ulong bytesToCopy;
	}
}
