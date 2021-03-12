using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002A9 RID: 681
	// (Invoke) Token: 0x06001886 RID: 6278
	public delegate T PiiRedactor<T>(T value, out string rawPii, out string redactedPii);
}
