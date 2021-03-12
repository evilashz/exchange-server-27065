using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema
{
	// Token: 0x020001C8 RID: 456
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface ISyncContact : ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000CDA RID: 3290
		ExDateTime? BirthDate { get; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000CDB RID: 3291
		ExDateTime? BirthDateLocal { get; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000CDC RID: 3292
		string BusinessAddressCity { get; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000CDD RID: 3293
		string BusinessAddressCountry { get; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000CDE RID: 3294
		string BusinessAddressPostalCode { get; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000CDF RID: 3295
		string BusinessAddressState { get; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000CE0 RID: 3296
		string BusinessAddressStreet { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000CE1 RID: 3297
		string BusinessFaxNumber { get; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000CE2 RID: 3298
		string BusinessTelephoneNumber { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000CE3 RID: 3299
		string CompanyName { get; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000CE4 RID: 3300
		string DisplayName { get; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000CE5 RID: 3301
		string Email1Address { get; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000CE6 RID: 3302
		string Email2Address { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000CE7 RID: 3303
		string Email3Address { get; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000CE8 RID: 3304
		string FileAs { get; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000CE9 RID: 3305
		string FirstName { get; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000CEA RID: 3306
		string Hobbies { get; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000CEB RID: 3307
		string HomeAddressCity { get; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000CEC RID: 3308
		string HomeAddressCountry { get; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000CED RID: 3309
		string HomeAddressPostalCode { get; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000CEE RID: 3310
		string HomeAddressState { get; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000CEF RID: 3311
		string HomeAddressStreet { get; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000CF0 RID: 3312
		string HomeTelephoneNumber { get; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000CF1 RID: 3313
		string IMAddress { get; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000CF2 RID: 3314
		string JobTitle { get; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000CF3 RID: 3315
		string LastName { get; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000CF4 RID: 3316
		string Location { get; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000CF5 RID: 3317
		string MiddleName { get; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000CF6 RID: 3318
		string MobileTelephoneNumber { get; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000CF7 RID: 3319
		string OtherTelephoneNumber { get; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000CF8 RID: 3320
		byte[] OscContactSources { get; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000CF9 RID: 3321
		string PartnerNetworkContactType { get; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000CFA RID: 3322
		string PartnerNetworkId { get; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000CFB RID: 3323
		string PartnerNetworkProfilePhotoUrl { get; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000CFC RID: 3324
		string PartnerNetworkThumbnailPhotoUrl { get; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06000CFD RID: 3325
		string PartnerNetworkUserId { get; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06000CFE RID: 3326
		ExDateTime? PeopleConnectionCreationTime { get; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06000CFF RID: 3327
		string ProtectedEmailAddress { get; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000D00 RID: 3328
		string ProtectedPhoneNumber { get; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000D01 RID: 3329
		string Schools { get; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000D02 RID: 3330
		string Webpage { get; }
	}
}
