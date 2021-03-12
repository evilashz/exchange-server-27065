using System;
using System.Text;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002A4 RID: 676
	internal abstract class RopEmptyFolderBase : InputRop
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0002BDD8 File Offset: 0x00029FD8
		protected bool ReportProgress
		{
			get
			{
				return this.reportProgress;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0002BDE0 File Offset: 0x00029FE0
		protected EmptyFolderFlags EmptyFolderFlags
		{
			get
			{
				return this.emptyFolderFlags;
			}
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002BDE8 File Offset: 0x00029FE8
		internal void SetInput(byte logonIndex, byte handleTableIndex, bool reportProgress, EmptyFolderFlags emptyFolderFlags)
		{
			base.SetCommonInput(logonIndex, handleTableIndex);
			this.reportProgress = reportProgress;
			this.emptyFolderFlags = emptyFolderFlags;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002BE01 File Offset: 0x0002A001
		protected override void InternalSerializeInput(Writer writer, Encoding string8Encoding)
		{
			base.InternalSerializeInput(writer, string8Encoding);
			writer.WriteBool(this.reportProgress);
			writer.WriteByte((byte)this.emptyFolderFlags);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002BE23 File Offset: 0x0002A023
		protected override void InternalParseInput(Reader reader, ServerObjectHandleTable serverObjectHandleTable)
		{
			base.InternalParseInput(reader, serverObjectHandleTable);
			this.reportProgress = reader.ReadBool();
			this.emptyFolderFlags = (EmptyFolderFlags)reader.ReadByte();
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002BE45 File Offset: 0x0002A045
		internal override void AppendToString(StringBuilder stringBuilder)
		{
			base.AppendToString(stringBuilder);
			stringBuilder.Append(" Flags=").Append(this.EmptyFolderFlags);
			stringBuilder.Append(" Progress=").Append(this.ReportProgress);
		}

		// Token: 0x0400079E RID: 1950
		private bool reportProgress;

		// Token: 0x0400079F RID: 1951
		private EmptyFolderFlags emptyFolderFlags;
	}
}
