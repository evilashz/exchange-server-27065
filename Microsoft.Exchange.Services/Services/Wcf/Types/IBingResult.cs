using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A52 RID: 2642
	internal interface IBingResult
	{
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06004AF1 RID: 19185
		string Name { get; }

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06004AF2 RID: 19186
		double Latitude { get; }

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06004AF3 RID: 19187
		double Longitude { get; }

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06004AF4 RID: 19188
		string StreetAddress { get; }

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06004AF5 RID: 19189
		string City { get; }

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06004AF6 RID: 19190
		string State { get; }

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06004AF7 RID: 19191
		string Country { get; }

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06004AF8 RID: 19192
		string PostalCode { get; }

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06004AF9 RID: 19193
		LocationSource LocationSource { get; }

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06004AFA RID: 19194
		string LocationUri { get; }

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06004AFB RID: 19195
		string PhoneNumber { get; }

		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06004AFC RID: 19196
		string LocalHomePage { get; }

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004AFD RID: 19197
		string BusinessHomePage { get; }
	}
}
