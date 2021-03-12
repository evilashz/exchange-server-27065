using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002D6 RID: 726
	internal sealed class SuccessfulGetReceiveFolderResult : RopResult
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x0002F1B3 File Offset: 0x0002D3B3
		internal StoreId ReceiveFolderId
		{
			get
			{
				return this.receiveFolderId;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0002F1BB File Offset: 0x0002D3BB
		internal string MessageClass
		{
			get
			{
				return this.messageClass;
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0002F1C3 File Offset: 0x0002D3C3
		internal SuccessfulGetReceiveFolderResult(StoreId receiveFolderId, string messageClass) : base(RopId.GetReceiveFolder, ErrorCode.None, null)
		{
			this.receiveFolderId = receiveFolderId;
			this.messageClass = messageClass;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0002F1DD File Offset: 0x0002D3DD
		internal SuccessfulGetReceiveFolderResult(Reader reader) : base(reader)
		{
			this.receiveFolderId = StoreId.Parse(reader);
			this.messageClass = reader.ReadAsciiString(StringFlags.IncludeNull);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0002F1FF File Offset: 0x0002D3FF
		internal static SuccessfulGetReceiveFolderResult Parse(Reader reader)
		{
			return new SuccessfulGetReceiveFolderResult(reader);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0002F208 File Offset: 0x0002D408
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			this.receiveFolderId.Serialize(writer);
			writer.WriteAsciiString(this.messageClass, StringFlags.IncludeNull);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0002F238 File Offset: 0x0002D438
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Folder=").Append(this.receiveFolderId.ToString());
			stringBuilder.Append(" Message Class=[").Append(this.messageClass).Append("]");
		}

		// Token: 0x04000843 RID: 2115
		private readonly StoreId receiveFolderId;

		// Token: 0x04000844 RID: 2116
		private readonly string messageClass;
	}
}
