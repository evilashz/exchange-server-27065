using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000255 RID: 597
	internal sealed class SuccessfulGetMessageStatusResult : RopResult
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002814F File Offset: 0x0002634F
		internal SuccessfulGetMessageStatusResult(MessageStatusFlags messageStatus) : base(RopId.SetMessageStatus, ErrorCode.None, null)
		{
			this.messageStatus = messageStatus;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00028162 File Offset: 0x00026362
		internal SuccessfulGetMessageStatusResult(Reader reader) : base(reader)
		{
			this.messageStatus = (MessageStatusFlags)reader.ReadUInt32();
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x00028177 File Offset: 0x00026377
		internal MessageStatusFlags MessageStatus
		{
			get
			{
				return this.messageStatus;
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002817F File Offset: 0x0002637F
		internal static SuccessfulGetMessageStatusResult Parse(Reader reader)
		{
			return new SuccessfulGetMessageStatusResult(reader);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00028187 File Offset: 0x00026387
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.messageStatus);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002819C File Offset: 0x0002639C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Status=").Append(this.messageStatus);
		}

		// Token: 0x040006F6 RID: 1782
		private readonly MessageStatusFlags messageStatus;
	}
}
