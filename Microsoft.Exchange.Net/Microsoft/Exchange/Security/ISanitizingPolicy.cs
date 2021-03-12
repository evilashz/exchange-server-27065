using System;
using System.IO;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A3E RID: 2622
	public interface ISanitizingPolicy
	{
		// Token: 0x0600390D RID: 14605
		string Sanitize(string str);

		// Token: 0x0600390E RID: 14606
		void Sanitize(TextWriter writer, string str);

		// Token: 0x0600390F RID: 14607
		string SanitizeFormat(IFormatProvider formatProvider, string format, params object[] args);
	}
}
