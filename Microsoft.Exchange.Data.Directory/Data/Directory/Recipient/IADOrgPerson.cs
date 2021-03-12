using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E8 RID: 488
	public interface IADOrgPerson : IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060016B4 RID: 5812
		// (set) Token: 0x060016B5 RID: 5813
		string C { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060016B6 RID: 5814
		// (set) Token: 0x060016B7 RID: 5815
		string City { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060016B8 RID: 5816
		// (set) Token: 0x060016B9 RID: 5817
		string Co { get; set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060016BA RID: 5818
		// (set) Token: 0x060016BB RID: 5819
		string Company { get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060016BC RID: 5820
		// (set) Token: 0x060016BD RID: 5821
		int CountryCode { get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060016BE RID: 5822
		string CountryOrRegionDisplayName { get; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060016BF RID: 5823
		// (set) Token: 0x060016C0 RID: 5824
		CountryInfo CountryOrRegion { get; set; }

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060016C1 RID: 5825
		// (set) Token: 0x060016C2 RID: 5826
		string Department { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060016C3 RID: 5827
		MultiValuedProperty<ADObjectId> DirectReports { get; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060016C4 RID: 5828
		// (set) Token: 0x060016C5 RID: 5829
		string Fax { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060016C6 RID: 5830
		// (set) Token: 0x060016C7 RID: 5831
		string FirstName { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060016C8 RID: 5832
		// (set) Token: 0x060016C9 RID: 5833
		string HomePhone { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060016CA RID: 5834
		MultiValuedProperty<string> IndexedPhoneNumbers { get; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060016CB RID: 5835
		// (set) Token: 0x060016CC RID: 5836
		string Initials { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060016CD RID: 5837
		// (set) Token: 0x060016CE RID: 5838
		MultiValuedProperty<CultureInfo> Languages { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060016CF RID: 5839
		// (set) Token: 0x060016D0 RID: 5840
		string LastName { get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060016D1 RID: 5841
		// (set) Token: 0x060016D2 RID: 5842
		ADObjectId Manager { get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060016D3 RID: 5843
		// (set) Token: 0x060016D4 RID: 5844
		string MobilePhone { get; set; }

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060016D5 RID: 5845
		// (set) Token: 0x060016D6 RID: 5846
		string Office { get; set; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060016D7 RID: 5847
		// (set) Token: 0x060016D8 RID: 5848
		MultiValuedProperty<string> OtherFax { get; set; }

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060016D9 RID: 5849
		// (set) Token: 0x060016DA RID: 5850
		MultiValuedProperty<string> OtherHomePhone { get; set; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060016DB RID: 5851
		// (set) Token: 0x060016DC RID: 5852
		MultiValuedProperty<string> OtherTelephone { get; set; }

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060016DD RID: 5853
		// (set) Token: 0x060016DE RID: 5854
		string Pager { get; set; }

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060016DF RID: 5855
		// (set) Token: 0x060016E0 RID: 5856
		string Phone { get; set; }

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060016E1 RID: 5857
		// (set) Token: 0x060016E2 RID: 5858
		string PostalCode { get; set; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060016E3 RID: 5859
		// (set) Token: 0x060016E4 RID: 5860
		MultiValuedProperty<string> PostOfficeBox { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060016E5 RID: 5861
		string RtcSipLine { get; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060016E6 RID: 5862
		MultiValuedProperty<string> SanitizedPhoneNumbers { get; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060016E7 RID: 5863
		// (set) Token: 0x060016E8 RID: 5864
		string StateOrProvince { get; set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060016E9 RID: 5865
		// (set) Token: 0x060016EA RID: 5866
		string StreetAddress { get; set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060016EB RID: 5867
		// (set) Token: 0x060016EC RID: 5868
		string TelephoneAssistant { get; set; }

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060016ED RID: 5869
		// (set) Token: 0x060016EE RID: 5870
		string Title { get; set; }

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060016EF RID: 5871
		// (set) Token: 0x060016F0 RID: 5872
		MultiValuedProperty<string> UMCallingLineIds { get; set; }

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060016F1 RID: 5873
		// (set) Token: 0x060016F2 RID: 5874
		MultiValuedProperty<string> VoiceMailSettings { get; set; }

		// Token: 0x060016F3 RID: 5875
		object[][] GetManagementChainView(bool getPeers, params PropertyDefinition[] returnProperties);

		// Token: 0x060016F4 RID: 5876
		object[][] GetDirectReportsView(params PropertyDefinition[] returnProperties);

		// Token: 0x060016F5 RID: 5877
		void PopulateDtmfMap(bool create);
	}
}
