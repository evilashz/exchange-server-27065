using System;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x02000026 RID: 38
	public interface IFilterableConverter
	{
		// Token: 0x060001DF RID: 479
		IConvertible ToFilterable(object item);
	}
}
