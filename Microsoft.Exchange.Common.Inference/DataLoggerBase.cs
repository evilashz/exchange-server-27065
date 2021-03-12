using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000003 RID: 3
	public abstract class DataLoggerBase : IDataLogger, IDisposable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		public DataLoggerBase(IList<string> columnNames, IList<Type> columnTypes)
		{
			ArgumentValidator.ThrowIfNull("columnNames", columnNames);
			ArgumentValidator.ThrowIfNull("columnTypes", columnTypes);
			this.ColumnNames = columnNames;
			this.ColumnTypes = columnTypes;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002104 File Offset: 0x00000304
		public IList<string> ColumnNames { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210D File Offset: 0x0000030D
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002115 File Offset: 0x00000315
		public IList<Type> ColumnTypes { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11
		public abstract string[] RecentlyLoggedRows { get; }

		// Token: 0x0600000C RID: 12
		public abstract void Log(IList<object> values);

		// Token: 0x0600000D RID: 13
		public abstract void Flush();

		// Token: 0x0600000E RID: 14
		public abstract void Dispose();
	}
}
