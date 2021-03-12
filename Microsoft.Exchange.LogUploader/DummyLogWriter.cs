using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000021 RID: 33
	public class DummyLogWriter : ILogWriter
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000942D File Offset: 0x0000762D
		public void Flush()
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000942F File Offset: 0x0000762F
		public void Close()
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009431 File Offset: 0x00007631
		public void Append(IEnumerable<LogRowFormatter> rows, int timestampField)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009433 File Offset: 0x00007633
		public void Append(LogRowFormatter row, int timestampField)
		{
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00009435 File Offset: 0x00007635
		public void Append(LogRowFormatter row, int timestampField, DateTime timeStamp)
		{
		}
	}
}
