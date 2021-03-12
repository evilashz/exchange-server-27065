using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x02000014 RID: 20
	public interface ILogWriter
	{
		// Token: 0x0600009C RID: 156
		void Flush();

		// Token: 0x0600009D RID: 157
		void Close();

		// Token: 0x0600009E RID: 158
		void Append(IEnumerable<LogRowFormatter> rows, int timestampField);

		// Token: 0x0600009F RID: 159
		void Append(LogRowFormatter row, int timestampField);

		// Token: 0x060000A0 RID: 160
		void Append(LogRowFormatter row, int timestampField, DateTime timeStamp);
	}
}
