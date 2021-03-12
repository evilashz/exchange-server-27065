using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x02000727 RID: 1831
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FacebookUser : IExtensibleDataObject
	{
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x0004787E File Offset: 0x00045A7E
		// (set) Token: 0x060022D9 RID: 8921 RVA: 0x00047886 File Offset: 0x00045A86
		[DataMember(Name = "activities")]
		public FacebookActivityList ActivitiesList { get; set; }

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x0004788F File Offset: 0x00045A8F
		// (set) Token: 0x060022DB RID: 8923 RVA: 0x00047897 File Offset: 0x00045A97
		[DataMember(Name = "birthday")]
		public string Birthday { get; set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060022DC RID: 8924 RVA: 0x000478A0 File Offset: 0x00045AA0
		// (set) Token: 0x060022DD RID: 8925 RVA: 0x000478A8 File Offset: 0x00045AA8
		[DataMember(Name = "education")]
		public List<FacebookEducationHistoryEntry> EducationHistory { get; set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x000478B1 File Offset: 0x00045AB1
		// (set) Token: 0x060022DF RID: 8927 RVA: 0x000478B9 File Offset: 0x00045AB9
		[DataMember(Name = "email")]
		public string EmailAddress { get; set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x000478C2 File Offset: 0x00045AC2
		// (set) Token: 0x060022E1 RID: 8929 RVA: 0x000478CA File Offset: 0x00045ACA
		[DataMember(Name = "id")]
		public string Id { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x000478D3 File Offset: 0x00045AD3
		// (set) Token: 0x060022E3 RID: 8931 RVA: 0x000478DB File Offset: 0x00045ADB
		[DataMember(Name = "interests")]
		public FacebookInterestList InterestsList { get; set; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000478E4 File Offset: 0x00045AE4
		// (set) Token: 0x060022E5 RID: 8933 RVA: 0x000478EC File Offset: 0x00045AEC
		[DataMember(Name = "updated_time")]
		public string UpdatedTime { get; set; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000478F5 File Offset: 0x00045AF5
		// (set) Token: 0x060022E7 RID: 8935 RVA: 0x000478FD File Offset: 0x00045AFD
		[DataMember(Name = "first_name")]
		public string FirstName { get; set; }

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x00047906 File Offset: 0x00045B06
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x0004790E File Offset: 0x00045B0E
		[DataMember(Name = "last_name")]
		public string LastName { get; set; }

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x00047917 File Offset: 0x00045B17
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x0004791F File Offset: 0x00045B1F
		[DataMember(Name = "location")]
		public FacebookLocation Location { get; set; }

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00047928 File Offset: 0x00045B28
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x00047930 File Offset: 0x00045B30
		[DataMember(Name = "mobile_phone")]
		public string MobilePhoneNumber { get; set; }

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00047939 File Offset: 0x00045B39
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x00047941 File Offset: 0x00045B41
		[DataMember(Name = "picture")]
		public FacebookPicture Picture { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0004794A File Offset: 0x00045B4A
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x00047952 File Offset: 0x00045B52
		[DataMember(Name = "link")]
		public string ProfilePageUrl { get; set; }

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0004795B File Offset: 0x00045B5B
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x00047963 File Offset: 0x00045B63
		[DataMember(Name = "website")]
		public string Website { get; set; }

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0004796C File Offset: 0x00045B6C
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x00047974 File Offset: 0x00045B74
		[DataMember(Name = "work")]
		public List<FacebookWorkHistoryEntry> WorkHistory { get; set; }

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x0004797D File Offset: 0x00045B7D
		// (set) Token: 0x060022F7 RID: 8951 RVA: 0x00047985 File Offset: 0x00045B85
		public ExtensionDataObject ExtensionData { get; set; }
	}
}
