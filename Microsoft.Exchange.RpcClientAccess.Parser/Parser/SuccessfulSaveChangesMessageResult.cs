using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000365 RID: 869
	internal sealed class SuccessfulSaveChangesMessageResult : RopResult
	{
		// Token: 0x06001541 RID: 5441 RVA: 0x000373A0 File Offset: 0x000355A0
		internal SuccessfulSaveChangesMessageResult(byte realHandleTableIndex, StoreId messageId) : base(RopId.SaveChangesMessage, ErrorCode.None, null)
		{
			this.realHandleTableIndex = realHandleTableIndex;
			this.messageId = messageId;
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000373BA File Offset: 0x000355BA
		internal SuccessfulSaveChangesMessageResult(Reader reader) : base(reader)
		{
			this.realHandleTableIndex = reader.ReadByte();
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000373DB File Offset: 0x000355DB
		public StoreId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x000373E3 File Offset: 0x000355E3
		internal static SuccessfulSaveChangesMessageResult Parse(Reader reader)
		{
			return new SuccessfulSaveChangesMessageResult(reader);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x000373EC File Offset: 0x000355EC
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(this.realHandleTableIndex);
			this.messageId.Serialize(writer);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0003741C File Offset: 0x0003561C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append(this.messageId.ToString());
		}

		// Token: 0x04000B28 RID: 2856
		private readonly StoreId messageId;

		// Token: 0x04000B29 RID: 2857
		private readonly byte realHandleTableIndex;
	}
}
