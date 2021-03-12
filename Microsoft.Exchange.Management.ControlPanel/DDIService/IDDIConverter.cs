using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200011E RID: 286
	public interface IDDIConverter
	{
		// Token: 0x0600203D RID: 8253
		bool CanConvert(object sourceValue);

		// Token: 0x0600203E RID: 8254
		object Convert(object sourceValue);
	}
}
