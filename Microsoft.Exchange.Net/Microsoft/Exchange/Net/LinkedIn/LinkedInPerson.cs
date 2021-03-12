using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.LinkedIn
{
	// Token: 0x02000750 RID: 1872
	[DataContract]
	public class LinkedInPerson : IExtensibleDataObject
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0004CAFB File Offset: 0x0004ACFB
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x0004CB03 File Offset: 0x0004AD03
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x0004CB0C File Offset: 0x0004AD0C
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0004CB14 File Offset: 0x0004AD14
		[DataMember(Name = "firstName")]
		public string FirstName { get; set; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x0004CB1D File Offset: 0x0004AD1D
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x0004CB25 File Offset: 0x0004AD25
		[DataMember(Name = "lastName")]
		public string LastName { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x0004CB2E File Offset: 0x0004AD2E
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x0004CB36 File Offset: 0x0004AD36
		[DataMember(Name = "headline")]
		public string Headline { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x0004CB3F File Offset: 0x0004AD3F
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x0004CB47 File Offset: 0x0004AD47
		[DataMember(Name = "emailAddress")]
		public string EmailAddress { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x0004CB50 File Offset: 0x0004AD50
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x0004CB58 File Offset: 0x0004AD58
		[DataMember(Name = "threeCurrentPositions")]
		public LinkedInPositionsList ThreeCurrentPositions { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x0004CB61 File Offset: 0x0004AD61
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x0004CB69 File Offset: 0x0004AD69
		[DataMember(Name = "phoneNumbers")]
		public LinkedInPhoneNumberList PhoneNumbers { get; set; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x0004CB72 File Offset: 0x0004AD72
		// (set) Token: 0x0600249D RID: 9373 RVA: 0x0004CB7A File Offset: 0x0004AD7A
		[DataMember(Name = "pictureUrl")]
		public string PictureUrl { get; set; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x0004CB83 File Offset: 0x0004AD83
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x0004CB8B File Offset: 0x0004AD8B
		[DataMember(Name = "imAccounts")]
		public LinkedInIMAccounts IMAccounts { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x0004CB94 File Offset: 0x0004AD94
		// (set) Token: 0x060024A1 RID: 9377 RVA: 0x0004CB9C File Offset: 0x0004AD9C
		[DataMember(Name = "dateOfBirth")]
		public LinkedInBirthDate Birthdate { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x0004CBA5 File Offset: 0x0004ADA5
		// (set) Token: 0x060024A3 RID: 9379 RVA: 0x0004CBAD File Offset: 0x0004ADAD
		[DataMember(Name = "educations")]
		public LinkedInSchoolList SchoolList { get; set; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0004CBB6 File Offset: 0x0004ADB6
		// (set) Token: 0x060024A5 RID: 9381 RVA: 0x0004CBBE File Offset: 0x0004ADBE
		[DataMember(Name = "publicProfileUrl")]
		public string PublicProfileUrl { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x0004CBC7 File Offset: 0x0004ADC7
		// (set) Token: 0x060024A7 RID: 9383 RVA: 0x0004CBCF File Offset: 0x0004ADCF
		[DataMember(Name = "pictureUrls")]
		public LinkedInPictureUrls PictureUrls { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x0004CBD8 File Offset: 0x0004ADD8
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x0004CBE0 File Offset: 0x0004ADE0
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
