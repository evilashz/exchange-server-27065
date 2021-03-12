using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200036C RID: 876
	internal sealed class SetReadFlagsResult : RopResult
	{
		// Token: 0x0600156C RID: 5484 RVA: 0x00037879 File Offset: 0x00035A79
		internal SetReadFlagsResult(ErrorCode errorCode, bool isPartiallyCompleted) : base(RopId.SetReadFlags, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0003788C File Offset: 0x00035A8C
		internal SetReadFlagsResult(Reader reader) : base(reader)
		{
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000378A1 File Offset: 0x00035AA1
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000378A9 File Offset: 0x00035AA9
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x04000B34 RID: 2868
		private readonly bool isPartiallyCompleted;
	}
}
