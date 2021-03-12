using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000233 RID: 563
	internal sealed class SuccessfulCreateMessageExtendedResult : RopResult
	{
		// Token: 0x06000C4E RID: 3150 RVA: 0x00027127 File Offset: 0x00025327
		internal SuccessfulCreateMessageExtendedResult(IServerObject serverObject, StoreId? messageId) : base(RopId.CreateMessageExtended, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.messageId = messageId;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0002714C File Offset: 0x0002534C
		internal SuccessfulCreateMessageExtendedResult(Reader reader) : base(reader)
		{
			bool flag = reader.ReadBool();
			if (flag)
			{
				this.messageId = new StoreId?(StoreId.Parse(reader));
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0002717B File Offset: 0x0002537B
		internal StoreId? MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00027183 File Offset: 0x00025383
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulCreateMessageExtendedResult(reader);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002718C File Offset: 0x0002538C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.messageId != null);
			if (this.messageId != null)
			{
				this.messageId.Value.Serialize(writer);
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000271DC File Offset: 0x000253DC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append((this.messageId != null) ? this.messageId.ToString() : "null");
		}

		// Token: 0x040006CA RID: 1738
		private readonly StoreId? messageId;
	}
}
