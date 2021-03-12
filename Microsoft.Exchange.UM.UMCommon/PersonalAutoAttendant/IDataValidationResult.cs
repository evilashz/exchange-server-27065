using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x020000EF RID: 239
	internal interface IDataValidationResult
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007DC RID: 2012
		PAAValidationResult PAAValidationResult { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007DD RID: 2013
		ADRecipient ADRecipient { get; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007DE RID: 2014
		PhoneNumber PhoneNumber { get; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060007DF RID: 2015
		PersonalContactInfo PersonalContactInfo { get; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060007E0 RID: 2016
		PersonaType PersonaContactInfo { get; }
	}
}
