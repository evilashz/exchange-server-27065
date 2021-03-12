using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A5D RID: 2653
	[DataContract]
	internal class PhonebookServicesWebResponse
	{
		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x001056B4 File Offset: 0x001038B4
		// (set) Token: 0x06004B49 RID: 19273 RVA: 0x001056BC File Offset: 0x001038BC
		[DataMember]
		public PhonebookServicesWebResponse.PhonebookResultSet Phonebook { get; set; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x001056C5 File Offset: 0x001038C5
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x001056CD File Offset: 0x001038CD
		[DataMember]
		public PhonebookServicesWebResponse.ErrorResult[] Errors { get; set; }

		// Token: 0x02000A5E RID: 2654
		[DataContract]
		internal class ErrorResult : IBingError
		{
			// Token: 0x17001139 RID: 4409
			// (get) Token: 0x06004B4D RID: 19277 RVA: 0x001056DE File Offset: 0x001038DE
			// (set) Token: 0x06004B4E RID: 19278 RVA: 0x001056E6 File Offset: 0x001038E6
			[DataMember]
			public string Code { get; set; }

			// Token: 0x1700113A RID: 4410
			// (get) Token: 0x06004B4F RID: 19279 RVA: 0x001056EF File Offset: 0x001038EF
			// (set) Token: 0x06004B50 RID: 19280 RVA: 0x001056F7 File Offset: 0x001038F7
			[DataMember]
			public string Message { get; set; }
		}

		// Token: 0x02000A5F RID: 2655
		[DataContract]
		internal class PhonebookResultSet
		{
			// Token: 0x1700113B RID: 4411
			// (get) Token: 0x06004B52 RID: 19282 RVA: 0x00105708 File Offset: 0x00103908
			// (set) Token: 0x06004B53 RID: 19283 RVA: 0x00105710 File Offset: 0x00103910
			[DataMember]
			public PhonebookServicesWebResponse.PhonebookResultSet.PhonebookResult[] Results { get; set; }

			// Token: 0x02000A60 RID: 2656
			[DataContract(Name = "Results")]
			internal class PhonebookResult : IBingResult
			{
				// Token: 0x1700113C RID: 4412
				// (get) Token: 0x06004B55 RID: 19285 RVA: 0x00105721 File Offset: 0x00103921
				// (set) Token: 0x06004B56 RID: 19286 RVA: 0x00105729 File Offset: 0x00103929
				[DataMember(Name = "Title")]
				public string Name { get; set; }

				// Token: 0x1700113D RID: 4413
				// (get) Token: 0x06004B57 RID: 19287 RVA: 0x00105732 File Offset: 0x00103932
				// (set) Token: 0x06004B58 RID: 19288 RVA: 0x0010573A File Offset: 0x0010393A
				[DataMember]
				public string PhoneNumber { get; set; }

				// Token: 0x1700113E RID: 4414
				// (get) Token: 0x06004B59 RID: 19289 RVA: 0x00105743 File Offset: 0x00103943
				// (set) Token: 0x06004B5A RID: 19290 RVA: 0x0010574B File Offset: 0x0010394B
				[DataMember(Name = "Address")]
				public string StreetAddress { get; set; }

				// Token: 0x1700113F RID: 4415
				// (get) Token: 0x06004B5B RID: 19291 RVA: 0x00105754 File Offset: 0x00103954
				// (set) Token: 0x06004B5C RID: 19292 RVA: 0x0010575C File Offset: 0x0010395C
				[DataMember]
				public string City { get; set; }

				// Token: 0x17001140 RID: 4416
				// (get) Token: 0x06004B5D RID: 19293 RVA: 0x00105765 File Offset: 0x00103965
				// (set) Token: 0x06004B5E RID: 19294 RVA: 0x0010576D File Offset: 0x0010396D
				[DataMember(Name = "StateOrProvince")]
				public string State { get; set; }

				// Token: 0x17001141 RID: 4417
				// (get) Token: 0x06004B5F RID: 19295 RVA: 0x00105776 File Offset: 0x00103976
				// (set) Token: 0x06004B60 RID: 19296 RVA: 0x0010577E File Offset: 0x0010397E
				[DataMember(Name = "CountryOrRegion")]
				public string Country { get; set; }

				// Token: 0x17001142 RID: 4418
				// (get) Token: 0x06004B61 RID: 19297 RVA: 0x00105787 File Offset: 0x00103987
				// (set) Token: 0x06004B62 RID: 19298 RVA: 0x0010578F File Offset: 0x0010398F
				[DataMember(Name = "PostalCode")]
				private string OriginalPostalCode { get; set; }

				// Token: 0x17001143 RID: 4419
				// (get) Token: 0x06004B63 RID: 19299 RVA: 0x00105798 File Offset: 0x00103998
				public string PostalCode
				{
					get
					{
						if (this.Country == "US")
						{
							return null;
						}
						return this.OriginalPostalCode;
					}
				}

				// Token: 0x17001144 RID: 4420
				// (get) Token: 0x06004B64 RID: 19300 RVA: 0x001057B4 File Offset: 0x001039B4
				// (set) Token: 0x06004B65 RID: 19301 RVA: 0x001057BC File Offset: 0x001039BC
				[DataMember]
				public double Latitude { get; set; }

				// Token: 0x17001145 RID: 4421
				// (get) Token: 0x06004B66 RID: 19302 RVA: 0x001057C5 File Offset: 0x001039C5
				// (set) Token: 0x06004B67 RID: 19303 RVA: 0x001057CD File Offset: 0x001039CD
				[DataMember]
				public double Longitude { get; set; }

				// Token: 0x17001146 RID: 4422
				// (get) Token: 0x06004B68 RID: 19304 RVA: 0x001057D6 File Offset: 0x001039D6
				// (set) Token: 0x06004B69 RID: 19305 RVA: 0x001057DE File Offset: 0x001039DE
				[DataMember(Name = "DisplayUrl")]
				public string LocalHomePage { get; set; }

				// Token: 0x17001147 RID: 4423
				// (get) Token: 0x06004B6A RID: 19306 RVA: 0x001057E7 File Offset: 0x001039E7
				// (set) Token: 0x06004B6B RID: 19307 RVA: 0x001057EF File Offset: 0x001039EF
				[DataMember(Name = "BusinessUrl")]
				public string BusinessHomePage { get; set; }

				// Token: 0x17001148 RID: 4424
				// (get) Token: 0x06004B6C RID: 19308 RVA: 0x001057F8 File Offset: 0x001039F8
				public LocationSource LocationSource
				{
					get
					{
						return LocationSource.PhonebookServices;
					}
				}

				// Token: 0x17001149 RID: 4425
				// (get) Token: 0x06004B6D RID: 19309 RVA: 0x001057FB File Offset: 0x001039FB
				// (set) Token: 0x06004B6E RID: 19310 RVA: 0x00105803 File Offset: 0x00103A03
				[DataMember(Name = "UniqueId")]
				public string LocationUri { get; set; }

				// Token: 0x04002AD0 RID: 10960
				private const string CountryWithPostalCodeRestriction = "US";
			}
		}
	}
}
