using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000072 RID: 114
	public class CurrentOperationCounter : ICurrentOperationCounter, IDisposable
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		public CurrentOperationCounter(ExPerformanceCounter counter, bool autoIncrement = true)
		{
			if (counter == null)
			{
				throw new ArgumentNullException("counter");
			}
			this.counter = counter;
			if (autoIncrement)
			{
				this.Increment();
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000CBEE File Offset: 0x0000ADEE
		public void Increment()
		{
			this.counter.Increment();
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public void Decrement()
		{
			this.counter.Decrement();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		void IDisposable.Dispose()
		{
			this.Decrement();
		}

		// Token: 0x04000293 RID: 659
		private ExPerformanceCounter counter;
	}
}
