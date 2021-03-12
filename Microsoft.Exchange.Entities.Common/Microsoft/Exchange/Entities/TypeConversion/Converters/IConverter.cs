using System;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x02000057 RID: 87
	public interface IConverter<in TSource, out TDestination>
	{
		// Token: 0x060001CE RID: 462
		TDestination Convert(TSource value);
	}
}
