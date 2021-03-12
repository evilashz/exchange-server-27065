using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000110 RID: 272
	internal interface IPersonalContactCache
	{
		// Token: 0x06000909 RID: 2313
		void BuildCache();

		// Token: 0x0600090A RID: 2314
		bool IsContactValid(StoreObjectId contactId);

		// Token: 0x0600090B RID: 2315
		PersonalContactInfo GetContact(StoreObjectId contactId);
	}
}
