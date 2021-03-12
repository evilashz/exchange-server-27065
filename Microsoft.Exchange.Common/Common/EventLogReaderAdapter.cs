using System;
using System.Diagnostics.Eventing.Reader;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000041 RID: 65
	internal class EventLogReaderAdapter : IDisposable
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00007749 File Offset: 0x00005949
		internal EventLogReaderAdapter(EventLogQuery query)
		{
			this.reader = new EventLogReader(query);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000775D File Offset: 0x0000595D
		internal virtual EventRecord ReadEvent()
		{
			return this.reader.ReadEvent();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000776A File Offset: 0x0000596A
		public void Dispose()
		{
			this.reader.Dispose();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007777 File Offset: 0x00005977
		protected virtual void Dispose(bool disposing)
		{
			this.reader.Dispose();
		}

		// Token: 0x04000169 RID: 361
		protected EventLogReader reader;
	}
}
