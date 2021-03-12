using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000372 RID: 882
	internal sealed class SuccessfulTransportDeliverMessage2Result : RopResult
	{
		// Token: 0x06001585 RID: 5509 RVA: 0x00037A82 File Offset: 0x00035C82
		internal SuccessfulTransportDeliverMessage2Result(StoreId messageId) : base(RopId.TransportDeliverMessage2, ErrorCode.None, null)
		{
			this.messageId = messageId;
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00037A98 File Offset: 0x00035C98
		internal SuccessfulTransportDeliverMessage2Result(Reader reader) : base(reader)
		{
			reader.ReadByte();
			this.messageId = StoreId.Parse(reader);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00037AB4 File Offset: 0x00035CB4
		internal static SuccessfulTransportDeliverMessage2Result Parse(Reader reader)
		{
			return new SuccessfulTransportDeliverMessage2Result(reader);
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00037ABC File Offset: 0x00035CBC
		public StoreId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00037AC4 File Offset: 0x00035CC4
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteByte(base.HandleTableIndex);
			this.messageId.Serialize(writer);
		}

		// Token: 0x04000B37 RID: 2871
		private StoreId messageId;
	}
}
