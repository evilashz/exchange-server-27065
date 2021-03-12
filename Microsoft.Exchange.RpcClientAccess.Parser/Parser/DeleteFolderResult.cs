using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000235 RID: 565
	internal sealed class DeleteFolderResult : RopResult
	{
		// Token: 0x06000C5A RID: 3162 RVA: 0x0002732C File Offset: 0x0002552C
		internal DeleteFolderResult(ErrorCode errorCode, bool isPartiallyCompleted) : base(RopId.DeleteFolder, errorCode, null)
		{
			this.isPartiallyCompleted = isPartiallyCompleted;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002733F File Offset: 0x0002553F
		internal DeleteFolderResult(Reader reader) : base(reader)
		{
			this.isPartiallyCompleted = reader.ReadBool();
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00027354 File Offset: 0x00025554
		internal bool PartiallyCompleted
		{
			get
			{
				return this.isPartiallyCompleted;
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002735C File Offset: 0x0002555C
		internal static RopResult Parse(Reader reader)
		{
			return new DeleteFolderResult(reader);
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00027364 File Offset: 0x00025564
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteBool(this.isPartiallyCompleted);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00027379 File Offset: 0x00025579
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Partial=").Append(this.isPartiallyCompleted);
		}

		// Token: 0x040006CC RID: 1740
		private readonly bool isPartiallyCompleted;
	}
}
