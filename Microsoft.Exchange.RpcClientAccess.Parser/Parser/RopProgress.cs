using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000313 RID: 787
	internal sealed class RopProgress : InputRop
	{
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x000327F4 File Offset: 0x000309F4
		internal override RopId RopId
		{
			get
			{
				return RopId.Progress;
			}
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000327F8 File Offset: 0x000309F8
		internal static Rop CreateRop()
		{
			return new RopProgress();
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000327FF File Offset: 0x000309FF
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" WantCancel=").Append(this.wantCancel);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0003281F File Offset: 0x00030A1F
		internal void SetInput(byte logonIndex, byte handleTableIndex, bool wantCancel)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.wantCancel = wantCancel;
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00032830 File Offset: 0x00030A30
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.wantCancel, 1);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00032847 File Offset: 0x00030A47
		protected override void InternalParseOutput(Reader reader, Encoding string8Encoding)
		{
			base.InternalParseOutput(reader, string8Encoding);
			this.result = ProgressResultFactory.Parse(reader);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0003285D File Offset: 0x00030A5D
		protected override IResultFactory GetDefaultResultFactory(IConnectionInformation connection, ArraySegment<byte> outputBuffer)
		{
			return RopProgress.resultFactory;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00032864 File Offset: 0x00030A64
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.wantCancel = reader.ReadBool();
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0003287A File Offset: 0x00030A7A
		protected override void InternalSerializeOutput(Writer writer)
		{
			base.InternalSerializeOutput(writer);
			this.result.Serialize(writer);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003288F File Offset: 0x00030A8F
		protected override void InternalExecute(IServerObject serverObject, IRopHandler ropHandler, ArraySegment<byte> outputBuffer)
		{
			this.result = ropHandler.Progress(serverObject, this.wantCancel, RopProgress.resultFactory);
		}

		// Token: 0x040009F0 RID: 2544
		private const RopId RopType = RopId.Progress;

		// Token: 0x040009F1 RID: 2545
		private static ProgressResultFactory resultFactory = new ProgressResultFactory();

		// Token: 0x040009F2 RID: 2546
		private bool wantCancel;
	}
}
