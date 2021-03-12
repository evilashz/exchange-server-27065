using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200001E RID: 30
	public interface ITextComparer : ISupportTextComparer
	{
		// Token: 0x060001B3 RID: 435
		string Format(object item, ICustomFormatter customFormatter, IFormatProvider formatProvider, string formatString, string defaultEmptyText);
	}
}
