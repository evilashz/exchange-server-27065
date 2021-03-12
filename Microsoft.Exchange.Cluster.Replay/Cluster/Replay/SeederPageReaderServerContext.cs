using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000136 RID: 310
	internal class SeederPageReaderServerContext
	{
		// Token: 0x06000BB6 RID: 2998 RVA: 0x00034730 File Offset: 0x00032930
		public SeederPageReaderServerContext(IEseDatabaseReader localEseDatabaseReader, string targetServerName)
		{
			this.m_localEseDatabaseReader = localEseDatabaseReader;
			this.m_targetServerName = targetServerName;
			ReplayCrimsonEvents.IncReseedSourceBegin.Log<Guid, string, string, string>(this.m_localEseDatabaseReader.DatabaseGuid, this.m_localEseDatabaseReader.DatabaseName, this.m_targetServerName, string.Empty);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0003477C File Offset: 0x0003297C
		public IEseDatabaseReader DatabaseReader
		{
			get
			{
				return this.m_localEseDatabaseReader;
			}
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00034784 File Offset: 0x00032984
		public void Close()
		{
			if (this.m_localEseDatabaseReader != null)
			{
				this.m_localEseDatabaseReader.Dispose();
				ReplayCrimsonEvents.IncReseedSourceEnd.Log<Guid, string, string, string>(this.m_localEseDatabaseReader.DatabaseGuid, this.m_localEseDatabaseReader.DatabaseName, this.m_targetServerName, string.Empty);
			}
			this.m_localEseDatabaseReader = null;
		}

		// Token: 0x04000517 RID: 1303
		private string m_targetServerName;

		// Token: 0x04000518 RID: 1304
		private IEseDatabaseReader m_localEseDatabaseReader;
	}
}
