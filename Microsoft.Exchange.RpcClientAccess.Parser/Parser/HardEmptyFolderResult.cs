using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000264 RID: 612
	internal sealed class HardEmptyFolderResult : RopResult
	{
		// Token: 0x06000D39 RID: 3385 RVA: 0x000289F4 File Offset: 0x00026BF4
		internal HardEmptyFolderResult(ErrorCode errorCode, bool isPartiallyCompleted) : base(RopId.HardEmptyFolder, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00028A0A File Offset: 0x00026C0A
		internal HardEmptyFolderResult(Reader reader) : base(reader)
		{
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x00028A1F File Offset: 0x00026C1F
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00028A27 File Offset: 0x00026C27
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00028A3C File Offset: 0x00026C3C
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x04000706 RID: 1798
		private readonly bool isPartiallyCompleted;
	}
}
