using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IFastTransferReader : IFastTransferDataInterface, IDisposable
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000711 RID: 1809
		bool IsDataAvailable { get; }

		// Token: 0x06000712 RID: 1810
		bool TryPeekMarker(out PropertyTag propertyTag);

		// Token: 0x06000713 RID: 1811
		void ReadMarker(PropertyTag expectedMarker);

		// Token: 0x06000714 RID: 1812
		PropertyTag ReadPropertyInfo(out NamedProperty namedProperty, out int codePage);

		// Token: 0x06000715 RID: 1813
		PropertyValue ReadAndParseFixedSizeValue(PropertyTag propertyTag);

		// Token: 0x06000716 RID: 1814
		int ReadLength(int maxValue);

		// Token: 0x06000717 RID: 1815
		ArraySegment<byte> ReadVariableSizeValue(int maxToRead);
	}
}
