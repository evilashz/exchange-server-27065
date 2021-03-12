using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E1 RID: 737
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IReferenceAttachment : IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001F7B RID: 8059
		// (set) Token: 0x06001F7C RID: 8060
		string AttachLongPathName { get; set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001F7D RID: 8061
		// (set) Token: 0x06001F7E RID: 8062
		string ProviderEndpointUrl { get; set; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001F7F RID: 8063
		// (set) Token: 0x06001F80 RID: 8064
		string ProviderType { get; set; }
	}
}
