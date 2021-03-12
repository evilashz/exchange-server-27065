using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000234 RID: 564
	internal sealed class SuccessfulCreateMessageResult : RopResult
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x0002722C File Offset: 0x0002542C
		internal SuccessfulCreateMessageResult(IServerObject serverObject, StoreId? messageId) : base(RopId.CreateMessage, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.messageId = messageId;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0002724C File Offset: 0x0002544C
		internal SuccessfulCreateMessageResult(Reader reader) : base(reader)
		{
			bool flag = reader.ReadBool();
			if (flag)
			{
				this.messageId = new StoreId?(StoreId.Parse(reader));
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0002727B File Offset: 0x0002547B
		internal StoreId? MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00027283 File Offset: 0x00025483
		internal static RopResult Parse(Reader reader)
		{
			return new SuccessfulCreateMessageResult(reader);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002728C File Offset: 0x0002548C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.messageId != null);
			if (this.messageId != null)
			{
				this.messageId.Value.Serialize(writer);
			}
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000272DC File Offset: 0x000254DC
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" MID=").Append((this.messageId != null) ? this.messageId.ToString() : "null");
		}

		// Token: 0x040006CB RID: 1739
		private readonly StoreId? messageId;
	}
}
