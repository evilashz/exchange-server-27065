using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E0 RID: 480
	// (Invoke) Token: 0x060014E0 RID: 5344
	internal delegate void MultiPropertyParsingMethod(BufferString value, FormatConverter formatConverter, PropertyId basePropertyId, Property[] outputProperties, out int parsedPropertiesCount);
}
