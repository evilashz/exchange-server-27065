using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000C3 RID: 195
	internal class AmDbTrace
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00026F40 File Offset: 0x00025140
		internal AmDbTrace(IADDatabase db)
		{
			this.m_db = db;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00026F4F File Offset: 0x0002514F
		internal void Debug(string format, params object[] args)
		{
			AmTrace.Debug(this.PrefixDatabase(format), args);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00026F5E File Offset: 0x0002515E
		internal void Info(string format, params object[] args)
		{
			AmTrace.Info(this.PrefixDatabase(format), args);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00026F6D File Offset: 0x0002516D
		internal void Warning(string format, params object[] args)
		{
			AmTrace.Warning(this.PrefixDatabase(format), args);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00026F7C File Offset: 0x0002517C
		internal void Error(string format, params object[] args)
		{
			AmTrace.Error(this.PrefixDatabase(format), args);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00026F8B File Offset: 0x0002518B
		internal void Entering(string format, params object[] args)
		{
			AmTrace.Entering(this.PrefixDatabase(format), args);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00026F9A File Offset: 0x0002519A
		internal void Leaving(string format, params object[] args)
		{
			AmTrace.Leaving(this.PrefixDatabase(format), args);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00026FAC File Offset: 0x000251AC
		private string PrefixDatabase(string format)
		{
			string result;
			if (this.m_db != null)
			{
				result = string.Format("{0} [DbGuid={1}]", format, this.m_db.Guid);
			}
			else
			{
				result = string.Format("{0} [DbGuid=<unknown>]", format);
			}
			return result;
		}

		// Token: 0x04000385 RID: 901
		private IADDatabase m_db;
	}
}
