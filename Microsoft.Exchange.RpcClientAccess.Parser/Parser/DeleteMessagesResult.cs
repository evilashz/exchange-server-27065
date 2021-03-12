using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000236 RID: 566
	internal sealed class DeleteMessagesResult : RopResult
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x00027399 File Offset: 0x00025599
		internal DeleteMessagesResult(ErrorCode errorCode, bool isPartiallyCompleted) : base(RopId.DeleteMessages, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000273AC File Offset: 0x000255AC
		internal DeleteMessagesResult(Reader reader) : base(reader)
		{
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x000273C1 File Offset: 0x000255C1
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000273C9 File Offset: 0x000255C9
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000273DE File Offset: 0x000255DE
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial Completion=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x040006CD RID: 1741
		private readonly bool isPartiallyCompleted;
	}
}
