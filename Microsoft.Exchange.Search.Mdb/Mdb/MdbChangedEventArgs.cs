using System;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x0200001B RID: 27
	internal sealed class MdbChangedEventArgs : EventArgs
	{
		// Token: 0x0600008C RID: 140 RVA: 0x000066AF File Offset: 0x000048AF
		internal MdbChangedEventArgs(MdbChangedEntry[] mdbChangedEntries)
		{
			this.changedDatabases = mdbChangedEntries;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000066BE File Offset: 0x000048BE
		internal MdbChangedEntry[] ChangedDatabases
		{
			get
			{
				return this.changedDatabases;
			}
		}

		// Token: 0x04000062 RID: 98
		private readonly MdbChangedEntry[] changedDatabases;
	}
}
