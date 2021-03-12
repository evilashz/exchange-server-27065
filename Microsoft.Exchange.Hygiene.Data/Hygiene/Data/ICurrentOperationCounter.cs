using System;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000071 RID: 113
	public interface ICurrentOperationCounter : IDisposable
	{
		// Token: 0x06000448 RID: 1096
		void Increment();

		// Token: 0x06000449 RID: 1097
		void Decrement();
	}
}
