using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x0200011F RID: 287
	internal interface IDataLoader : IDisposeTrackable, IDisposable
	{
		// Token: 0x0600093F RID: 2367
		PhoneNumber GetCallerId();

		// Token: 0x06000940 RID: 2368
		string GetDiversionForCall();

		// Token: 0x06000941 RID: 2369
		void GetUserOofSettings(out UserOofSettings owaOof, out bool telOof);

		// Token: 0x06000942 RID: 2370
		void GetFreeBusyInformation(out FreeBusyStatusEnum freeBusy);

		// Token: 0x06000943 RID: 2371
		WorkingHours GetWorkingHours();

		// Token: 0x06000944 RID: 2372
		PersonalContactInfo[] GetMatchingPersonalContacts(PhoneNumber callerId);

		// Token: 0x06000945 RID: 2373
		ADContactInfo GetMatchingADContact(PhoneNumber callerId);

		// Token: 0x06000946 RID: 2374
		List<string> GetMatchingPersonaEmails();

		// Token: 0x06000947 RID: 2375
		ExTimeZone GetUserTimeZone();

		// Token: 0x06000948 RID: 2376
		UMSubscriber GetUMSubscriber();

		// Token: 0x06000949 RID: 2377
		bool TryIsWithinCompanyWorkingHours(out bool withinWorkingHours);
	}
}
