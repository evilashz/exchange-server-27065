using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Inference.Common.Diagnostics
{
	// Token: 0x02000002 RID: 2
	public interface IDataLogger : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		IList<string> ColumnNames { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		IList<Type> ColumnTypes { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		string[] RecentlyLoggedRows { get; }

		// Token: 0x06000004 RID: 4
		void Log(IList<object> values);

		// Token: 0x06000005 RID: 5
		void Flush();
	}
}
