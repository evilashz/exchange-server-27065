using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000252 RID: 594
	internal sealed class SuccessfulGetHierarchyTableResult : RopResult
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00027FC0 File Offset: 0x000261C0
		internal SuccessfulGetHierarchyTableResult(IServerObject serverObject, int rowCount) : base(RopId.GetHierarchyTable, ErrorCode.None, serverObject)
		{
			if (serverObject == null)
			{
				throw new ArgumentNullException("serverObject");
			}
			this.rowCount = rowCount;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00027FE0 File Offset: 0x000261E0
		internal SuccessfulGetHierarchyTableResult(Reader reader) : base(reader)
		{
			this.rowCount = reader.ReadInt32();
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00027FF5 File Offset: 0x000261F5
		internal int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00027FFD File Offset: 0x000261FD
		public static SuccessfulGetHierarchyTableResult Parse(Reader reader)
		{
			return new SuccessfulGetHierarchyTableResult(reader);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00028005 File Offset: 0x00026205
		internal override void Serialize(Writer writer)
		{
			base.Serialize(writer);
			writer.WriteInt32(this.rowCount);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002801A File Offset: 0x0002621A
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" RowCount=").Append(this.rowCount);
		}

		// Token: 0x040006F3 RID: 1779
		private readonly int rowCount;
	}
}
