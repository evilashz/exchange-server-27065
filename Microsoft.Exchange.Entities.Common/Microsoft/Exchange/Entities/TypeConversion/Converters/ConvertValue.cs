using System;

namespace Microsoft.Exchange.Entities.TypeConversion.Converters
{
	// Token: 0x0200005D RID: 93
	// (Invoke) Token: 0x0600020E RID: 526
	public delegate TDestination ConvertValue<in TSource, out TDestination>(TSource value);
}
