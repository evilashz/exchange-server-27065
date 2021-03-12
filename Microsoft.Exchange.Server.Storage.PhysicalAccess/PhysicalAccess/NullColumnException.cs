using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000027 RID: 39
	public class NullColumnException : Exception
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public NullColumnException(Column column) : base(string.Format("Column {0} was null so a read only stream cannot be opened", column.Name))
		{
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		public NullColumnException(Column column, Exception innerException) : base(string.Format("Column {0} was null so a read only stream cannot be opened", column.Name), innerException)
		{
		}

		// Token: 0x040000B5 RID: 181
		private const string NullColumnMessage = "Column {0} was null so a read only stream cannot be opened";
	}
}
