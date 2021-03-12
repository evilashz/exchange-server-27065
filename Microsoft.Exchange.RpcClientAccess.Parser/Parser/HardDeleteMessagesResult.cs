using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000263 RID: 611
	internal sealed class HardDeleteMessagesResult : RopResult
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x0002898C File Offset: 0x00026B8C
		internal HardDeleteMessagesResult(ErrorCode errorCode, bool isPartiallyCompleted) : base(RopId.HardDeleteMessages, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x000289A2 File Offset: 0x00026BA2
		internal HardDeleteMessagesResult(Reader reader) : base(reader)
		{
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000289B7 File Offset: 0x00026BB7
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000289BF File Offset: 0x00026BBF
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000289D4 File Offset: 0x00026BD4
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial Completion=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x04000705 RID: 1797
		private readonly bool isPartiallyCompleted;
	}
}
