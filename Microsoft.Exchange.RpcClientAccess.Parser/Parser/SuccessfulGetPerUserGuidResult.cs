using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200025A RID: 602
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class SuccessfulGetPerUserGuidResult : RopResult
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x00028414 File Offset: 0x00026614
		internal SuccessfulGetPerUserGuidResult(Guid databaseGuid) : base(RopId.GetPerUserGuid, ErrorCode.None, null)
		{
			this.databaseGuid = databaseGuid;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00028427 File Offset: 0x00026627
		internal SuccessfulGetPerUserGuidResult(Reader reader) : base(reader)
		{
			this.databaseGuid = reader.ReadGuid();
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002843C File Offset: 0x0002663C
		internal Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00028444 File Offset: 0x00026644
		internal static SuccessfulGetPerUserGuidResult Parse(Reader reader)
		{
			return new SuccessfulGetPerUserGuidResult(reader);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002844C File Offset: 0x0002664C
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteGuid(this.DatabaseGuid);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00028461 File Offset: 0x00026661
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" DatabaseGuid=[").Append(this.DatabaseGuid).Append("]");
		}

		// Token: 0x040006FC RID: 1788
		private readonly Guid databaseGuid;
	}
}
