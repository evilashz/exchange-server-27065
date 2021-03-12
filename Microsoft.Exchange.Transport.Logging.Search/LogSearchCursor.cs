using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000029 RID: 41
	public class LogSearchCursor : IDisposable
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00007734 File Offset: 0x00005934
		public LogSearchCursor(CsvTable table, string server, ServerVersion version, string log, LogQuery query, IProgressReport progressReport)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version must not be null.");
			}
			this.searchStream = new LogSearchStream(server, version, log, query, progressReport);
			this.cursor = new CsvFieldCache(table, this.searchStream, 32768);
			this.responseContainsSchema = (version >= LogSearchConstants.LowestModernInterfaceBuildVersion);
			this.serverSchemaMatchesLocalSchema = (CsvFieldCache.LocalVersion == version);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000077B4 File Offset: 0x000059B4
		public bool MoveNext()
		{
			if (!this.cursor.MoveNext(false))
			{
				return false;
			}
			if (this.responseContainsSchema && !this.firstRowHasBeenProcessed)
			{
				if (!this.serverSchemaMatchesLocalSchema)
				{
					this.cursor.SetMappingTableBasedOnCurrentRecord();
				}
				this.firstRowHasBeenProcessed = true;
				return this.cursor.MoveNext(false);
			}
			return true;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00007809 File Offset: 0x00005A09
		public object GetField(int idx)
		{
			return this.cursor.GetField(idx);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00007817 File Offset: 0x00005A17
		public void Cancel()
		{
			this.searchStream.Cancel();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007824 File Offset: 0x00005A24
		public void Close()
		{
			this.searchStream.Close();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00007831 File Offset: 0x00005A31
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x04000084 RID: 132
		private LogSearchStream searchStream;

		// Token: 0x04000085 RID: 133
		private CsvFieldCache cursor;

		// Token: 0x04000086 RID: 134
		private bool serverSchemaMatchesLocalSchema;

		// Token: 0x04000087 RID: 135
		private bool responseContainsSchema;

		// Token: 0x04000088 RID: 136
		private bool firstRowHasBeenProcessed;
	}
}
