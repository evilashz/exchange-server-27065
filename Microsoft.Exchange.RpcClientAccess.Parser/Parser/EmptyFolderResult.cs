using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200023B RID: 571
	internal sealed class EmptyFolderResult : RopResult
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x0002762D File Offset: 0x0002582D
		internal EmptyFolderResult(ErrorCode errorCode, bool isPartiallyCompleted) : base(RopId.EmptyFolder, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00027640 File Offset: 0x00025840
		internal EmptyFolderResult(Reader reader) : base(reader)
		{
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00027655 File Offset: 0x00025855
		internal bool IsPartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002765D File Offset: 0x0002585D
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00027672 File Offset: 0x00025872
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x040006D8 RID: 1752
		private readonly bool isPartiallyCompleted;
	}
}
