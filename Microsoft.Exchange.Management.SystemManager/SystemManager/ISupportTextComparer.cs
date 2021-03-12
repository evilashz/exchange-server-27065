using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200001D RID: 29
	public interface ISupportTextComparer
	{
		// Token: 0x060001B2 RID: 434
		int Compare(object x, object y, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText);
	}
}
