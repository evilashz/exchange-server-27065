using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000369 RID: 873
	internal sealed class SuccessfulSetMessageStatusResult : RopResult
	{
		// Token: 0x0600155B RID: 5467 RVA: 0x00037672 File Offset: 0x00035872
		internal SuccessfulSetMessageStatusResult(MessageStatusFlags oldStatus) : base(RopId.SetMessageStatus, ErrorCode.None, null)
		{
			this.oldStatus = oldStatus;
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00037685 File Offset: 0x00035885
		internal SuccessfulSetMessageStatusResult(Reader reader) : base(reader)
		{
			this.oldStatus = (MessageStatusFlags)reader.ReadUInt32();
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0003769A File Offset: 0x0003589A
		internal MessageStatusFlags OldStatus
		{
			get
			{
				return this.oldStatus;
			}
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000376A2 File Offset: 0x000358A2
		internal static SuccessfulSetMessageStatusResult Parse(Reader reader)
		{
			return new SuccessfulSetMessageStatusResult(reader);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000376AA File Offset: 0x000358AA
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteUInt32((uint)this.oldStatus);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000376BF File Offset: 0x000358BF
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Old Status=").Append(this.oldStatus);
		}

		// Token: 0x04000B2F RID: 2863
		private readonly MessageStatusFlags oldStatus;
	}
}
