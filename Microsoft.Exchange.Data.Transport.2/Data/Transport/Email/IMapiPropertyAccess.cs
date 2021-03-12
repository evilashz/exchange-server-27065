using System;
using Microsoft.Exchange.Data.ContentTypes.Tnef;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000DC RID: 220
	internal interface IMapiPropertyAccess
	{
		// Token: 0x0600051E RID: 1310
		object GetProperty(TnefPropertyTag tag);

		// Token: 0x0600051F RID: 1311
		object GetProperty(TnefNameTag nameId);

		// Token: 0x06000520 RID: 1312
		void SetProperty(TnefPropertyTag tag, object value);

		// Token: 0x06000521 RID: 1313
		void SetProperty(TnefNameTag nameId, object value);
	}
}
