using System;

namespace Microsoft.Exchange.Server.Storage.PropTags
{
	// Token: 0x02000017 RID: 23
	public static class NamedPropInfo
	{
		// Token: 0x02000018 RID: 24
		public static class Unnamed
		{
			// Token: 0x04001796 RID: 6038
			public static readonly Guid NamespaceGuid = new Guid("00020328-0000-0000-C000-000000000046");
		}

		// Token: 0x02000019 RID: 25
		public static class PublicStrings
		{
			// Token: 0x04001797 RID: 6039
			public static readonly Guid NamespaceGuid = new Guid("00020329-0000-0000-C000-000000000046");

			// Token: 0x04001798 RID: 6040
			public static readonly StoreNamedPropInfo TelURI = new StoreNamedPropInfo("TelURI", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "TelURI"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001799 RID: 6041
			public static readonly StoreNamedPropInfo PeopleConnectionCreationTime = new StoreNamedPropInfo("PeopleConnectionCreationTime", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "PeopleConnectionCreationTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400179A RID: 6042
			public static readonly StoreNamedPropInfo Categories = new StoreNamedPropInfo("Categories", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, 9000U), PropertyType.Invalid, 9223372036854775808UL, new PropertyCategories(18));

			// Token: 0x0400179B RID: 6043
			public static readonly StoreNamedPropInfo CategoriesStr = new StoreNamedPropInfo("CategoriesStr", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, 9001U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400179C RID: 6044
			public static readonly StoreNamedPropInfo AddLevel1 = new StoreNamedPropInfo("AddLevel1", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AddLevel1"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400179D RID: 6045
			public static readonly StoreNamedPropInfo AddLevel2 = new StoreNamedPropInfo("AddLevel2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AddLevel2"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400179E RID: 6046
			public static readonly StoreNamedPropInfo AllowOleAct = new StoreNamedPropInfo("AllowOleAct", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AllowOleAct"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400179F RID: 6047
			public static readonly StoreNamedPropInfo AllowOlePack = new StoreNamedPropInfo("AllowOlePack", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AllowOlePack"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A0 RID: 6048
			public static readonly StoreNamedPropInfo AllowOneOffs = new StoreNamedPropInfo("AllowOneOffs", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AllowOneOffs"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A1 RID: 6049
			public static readonly StoreNamedPropInfo AllowUserAttachSetting = new StoreNamedPropInfo("AllowUserAttachSetting", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AllowUserAttachSetting"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A2 RID: 6050
			public static readonly StoreNamedPropInfo AllUsers = new StoreNamedPropInfo("AllUsers", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AllUsers"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A3 RID: 6051
			public static readonly StoreNamedPropInfo AppName = new StoreNamedPropInfo("AppName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AppName"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A4 RID: 6052
			public static readonly StoreNamedPropInfo AttachClosePrompt = new StoreNamedPropInfo("AttachClosePrompt", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AttachClosePrompt"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A5 RID: 6053
			public static readonly StoreNamedPropInfo AttachSendPrompt = new StoreNamedPropInfo("AttachSendPrompt", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "AttachSendPrompt"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A6 RID: 6054
			public static readonly StoreNamedPropInfo Author = new StoreNamedPropInfo("Author", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Author"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A7 RID: 6055
			public static readonly StoreNamedPropInfo ByteCount = new StoreNamedPropInfo("ByteCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ByteCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A8 RID: 6056
			public static readonly StoreNamedPropInfo Category = new StoreNamedPropInfo("Category", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Category"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017A9 RID: 6057
			public static readonly StoreNamedPropInfo CharCount = new StoreNamedPropInfo("CharCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "CharCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017AA RID: 6058
			public static readonly StoreNamedPropInfo Comments = new StoreNamedPropInfo("Comments", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Comments"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017AB RID: 6059
			public static readonly StoreNamedPropInfo Company = new StoreNamedPropInfo("Company", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Company"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017AC RID: 6060
			public static readonly StoreNamedPropInfo CreateDtmRo = new StoreNamedPropInfo("CreateDtmRo", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "CreateDtmRo"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017AD RID: 6061
			public static readonly StoreNamedPropInfo DocParts = new StoreNamedPropInfo("DocParts", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "DocParts"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017AE RID: 6062
			public static readonly StoreNamedPropInfo DoFormLookup = new StoreNamedPropInfo("DoFormLookup", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "doformlookup"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017AF RID: 6063
			public static readonly StoreNamedPropInfo DRMLicense = new StoreNamedPropInfo("DRMLicense", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "DRMLicense"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B0 RID: 6064
			public static readonly StoreNamedPropInfo EditTime = new StoreNamedPropInfo("EditTime", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "EditTime"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B1 RID: 6065
			public static readonly StoreNamedPropInfo FormControlProp = new StoreNamedPropInfo("FormControlProp", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "FormControlProp"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B2 RID: 6066
			public static readonly StoreNamedPropInfo FormVersion = new StoreNamedPropInfo("FormVersion", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "FormVersion"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B3 RID: 6067
			public static readonly StoreNamedPropInfo HeadingPair = new StoreNamedPropInfo("HeadingPair", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "HeadingPair"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B4 RID: 6068
			public static readonly StoreNamedPropInfo HeadingsPair = new StoreNamedPropInfo("HeadingsPair", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "HeadingsPair"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B5 RID: 6069
			public static readonly StoreNamedPropInfo HiddenCount = new StoreNamedPropInfo("HiddenCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "HiddenCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B6 RID: 6070
			public static readonly StoreNamedPropInfo OnlineMeetingExternalLink = new StoreNamedPropInfo("OnlineMeetingExternalLink", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "OnlineMeetingExternalLink"), PropertyType.Unicode, 16384UL, default(PropertyCategories));

			// Token: 0x040017B7 RID: 6071
			public static readonly StoreNamedPropInfo OnlineMeetingConfLink = new StoreNamedPropInfo("OnlineMeetingConfLink", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "OnlineMeetingConfLink"), PropertyType.Unicode, 16384UL, default(PropertyCategories));

			// Token: 0x040017B8 RID: 6072
			public static readonly StoreNamedPropInfo Age = new StoreNamedPropInfo("Age", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/age/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017B9 RID: 6073
			public static readonly StoreNamedPropInfo AstrologySign = new StoreNamedPropInfo("AstrologySign", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/astrologysign/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017BA RID: 6074
			public static readonly StoreNamedPropInfo BloodType = new StoreNamedPropInfo("BloodType", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/bloodtype/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017BB RID: 6075
			public static readonly StoreNamedPropInfo Custom1 = new StoreNamedPropInfo("Custom1", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom1/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017BC RID: 6076
			public static readonly StoreNamedPropInfo Custom2 = new StoreNamedPropInfo("Custom2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom2/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017BD RID: 6077
			public static readonly StoreNamedPropInfo Custom3 = new StoreNamedPropInfo("Custom3", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom3/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017BE RID: 6078
			public static readonly StoreNamedPropInfo Custom4 = new StoreNamedPropInfo("Custom4", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom4/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017BF RID: 6079
			public static readonly StoreNamedPropInfo Custom5 = new StoreNamedPropInfo("Custom5", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom5/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C0 RID: 6080
			public static readonly StoreNamedPropInfo Custom6 = new StoreNamedPropInfo("Custom6", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom6/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C1 RID: 6081
			public static readonly StoreNamedPropInfo Custom7 = new StoreNamedPropInfo("Custom7", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom7/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C2 RID: 6082
			public static readonly StoreNamedPropInfo Custom8 = new StoreNamedPropInfo("Custom8", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/custom8/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C3 RID: 6083
			public static readonly StoreNamedPropInfo CustomDate1 = new StoreNamedPropInfo("CustomDate1", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/customdate1/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C4 RID: 6084
			public static readonly StoreNamedPropInfo CustomDate2 = new StoreNamedPropInfo("CustomDate2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/customdate2/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C5 RID: 6085
			public static readonly StoreNamedPropInfo CustomPhone1 = new StoreNamedPropInfo("CustomPhone1", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/customphone1/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C6 RID: 6086
			public static readonly StoreNamedPropInfo CustomPhone2 = new StoreNamedPropInfo("CustomPhone2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/customphone2/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C7 RID: 6087
			public static readonly StoreNamedPropInfo CustomPhone3 = new StoreNamedPropInfo("CustomPhone3", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/customphone3/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C8 RID: 6088
			public static readonly StoreNamedPropInfo CustomPhone4 = new StoreNamedPropInfo("CustomPhone4", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/customphone4/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017C9 RID: 6089
			public static readonly StoreNamedPropInfo EMail10OriginalDisplayName = new StoreNamedPropInfo("EMail10OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email10originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017CA RID: 6090
			public static readonly StoreNamedPropInfo EMail11OriginalDisplayName = new StoreNamedPropInfo("EMail11OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email11originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017CB RID: 6091
			public static readonly StoreNamedPropInfo EMail12OriginalDisplayName = new StoreNamedPropInfo("EMail12OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email12originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017CC RID: 6092
			public static readonly StoreNamedPropInfo EMail3OriginalDisplayName = new StoreNamedPropInfo("EMail3OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email13originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017CD RID: 6093
			public static readonly StoreNamedPropInfo EMail4OriginalDisplayName = new StoreNamedPropInfo("EMail4OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email4originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017CE RID: 6094
			public static readonly StoreNamedPropInfo EMail5OriginalDisplayName = new StoreNamedPropInfo("EMail5OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email5originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017CF RID: 6095
			public static readonly StoreNamedPropInfo EMail6OriginalDisplayName = new StoreNamedPropInfo("EMail6OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email6originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D0 RID: 6096
			public static readonly StoreNamedPropInfo EMail7OriginalDisplayName = new StoreNamedPropInfo("EMail7OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email7originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D1 RID: 6097
			public static readonly StoreNamedPropInfo EMail8OriginalDisplayName = new StoreNamedPropInfo("EMail8OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email8originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D2 RID: 6098
			public static readonly StoreNamedPropInfo EMail9OriginalDisplayName = new StoreNamedPropInfo("EMail9OriginalDisplayName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/email9originaldisplayname/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D3 RID: 6099
			public static readonly StoreNamedPropInfo EmailLabel1 = new StoreNamedPropInfo("EmailLabel1", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel1/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D4 RID: 6100
			public static readonly StoreNamedPropInfo EmailLabel10 = new StoreNamedPropInfo("EmailLabel10", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel10/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D5 RID: 6101
			public static readonly StoreNamedPropInfo EmailLabel11 = new StoreNamedPropInfo("EmailLabel11", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel11/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D6 RID: 6102
			public static readonly StoreNamedPropInfo EmailLabel12 = new StoreNamedPropInfo("EmailLabel12", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel12/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D7 RID: 6103
			public static readonly StoreNamedPropInfo EmailLabel13 = new StoreNamedPropInfo("EmailLabel13", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel13/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D8 RID: 6104
			public static readonly StoreNamedPropInfo EmailLabel2 = new StoreNamedPropInfo("EmailLabel2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel2/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017D9 RID: 6105
			public static readonly StoreNamedPropInfo EmailLabel3 = new StoreNamedPropInfo("EmailLabel3", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel3/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017DA RID: 6106
			public static readonly StoreNamedPropInfo EmailLabel4 = new StoreNamedPropInfo("EmailLabel4", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel4/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017DB RID: 6107
			public static readonly StoreNamedPropInfo EmailLabel5 = new StoreNamedPropInfo("EmailLabel5", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel5/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017DC RID: 6108
			public static readonly StoreNamedPropInfo EmailLabel6 = new StoreNamedPropInfo("EmailLabel6", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel6/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017DD RID: 6109
			public static readonly StoreNamedPropInfo EmailLabel7 = new StoreNamedPropInfo("EmailLabel7", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel7/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017DE RID: 6110
			public static readonly StoreNamedPropInfo EmailLabel8 = new StoreNamedPropInfo("EmailLabel8", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel8/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017DF RID: 6111
			public static readonly StoreNamedPropInfo EmailLabel9 = new StoreNamedPropInfo("EmailLabel9", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/emaillabel9/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E0 RID: 6112
			public static readonly StoreNamedPropInfo Flagged = new StoreNamedPropInfo("Flagged", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/flagged/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E1 RID: 6113
			public static readonly StoreNamedPropInfo JapaneseContact = new StoreNamedPropInfo("JapaneseContact", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/japanesecontact/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E2 RID: 6114
			public static readonly StoreNamedPropInfo JapaneseFormat = new StoreNamedPropInfo("JapaneseFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/japaneseformat/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E3 RID: 6115
			public static readonly StoreNamedPropInfo SpouseFurigana = new StoreNamedPropInfo("SpouseFurigana", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/entourage/spousefurigana/"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E4 RID: 6116
			public static readonly StoreNamedPropInfo ABLuMultiLine = new StoreNamedPropInfo("ABLuMultiLine", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/ablumultiline"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E5 RID: 6117
			public static readonly StoreNamedPropInfo ABLuPreview = new StoreNamedPropInfo("ABLuPreview", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/ablupreview"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E6 RID: 6118
			public static readonly StoreNamedPropInfo ABPKmultiLine = new StoreNamedPropInfo("ABPKmultiLine", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/abpkmultiline"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E7 RID: 6119
			public static readonly StoreNamedPropInfo ANRContactsFirst = new StoreNamedPropInfo("ANRContactsFirst", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/anrcontactsfirst"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E8 RID: 6120
			public static readonly StoreNamedPropInfo Archive = new StoreNamedPropInfo("Archive", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/archive"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017E9 RID: 6121
			public static readonly StoreNamedPropInfo AutoAddSignature = new StoreNamedPropInfo("AutoAddSignature", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/autoaddsignature"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017EA RID: 6122
			public static readonly StoreNamedPropInfo BlockExternalContent = new StoreNamedPropInfo("BlockExternalContent", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/blockexternalcontent"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017EB RID: 6123
			public static readonly StoreNamedPropInfo CalViewType = new StoreNamedPropInfo("CalViewType", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/calviewtype"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017EC RID: 6124
			public static readonly StoreNamedPropInfo ComposeFontColor = new StoreNamedPropInfo("ComposeFontColor", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/composefontcolor"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017ED RID: 6125
			public static readonly StoreNamedPropInfo ComposeFontFlags = new StoreNamedPropInfo("ComposeFontFlags", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/composefontflags"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017EE RID: 6126
			public static readonly StoreNamedPropInfo ComposeFontName = new StoreNamedPropInfo("ComposeFontName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/composefontname"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017EF RID: 6127
			public static readonly StoreNamedPropInfo ComposeFontSize = new StoreNamedPropInfo("ComposeFontSize", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/composefontsize"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F0 RID: 6128
			public static readonly StoreNamedPropInfo DailyViewDays = new StoreNamedPropInfo("DailyViewDays", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/dailyviewdays"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F1 RID: 6129
			public static readonly StoreNamedPropInfo DeliverAndRedirect = new StoreNamedPropInfo("DeliverAndRedirect", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/deliverandredirect"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F2 RID: 6130
			public static readonly StoreNamedPropInfo EnableReminders = new StoreNamedPropInfo("EnableReminders", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/enablereminders"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F3 RID: 6131
			public static readonly StoreNamedPropInfo FBOldBusyStatus = new StoreNamedPropInfo("FBOldBusyStatus", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fboldbusystatus"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F4 RID: 6132
			public static readonly StoreNamedPropInfo FBOldEnd = new StoreNamedPropInfo("FBOldEnd", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fboldend"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F5 RID: 6133
			public static readonly StoreNamedPropInfo FBoldStart = new StoreNamedPropInfo("FBoldStart", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fboldstart"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F6 RID: 6134
			public static readonly StoreNamedPropInfo FBQueryEnd = new StoreNamedPropInfo("FBQueryEnd", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fbqueryend"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F7 RID: 6135
			public static readonly StoreNamedPropInfo FBQueryInterval = new StoreNamedPropInfo("FBQueryInterval", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fbqueryinterval"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F8 RID: 6136
			public static readonly StoreNamedPropInfo FBQueryStart = new StoreNamedPropInfo("FBQueryStart", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fbquerystart"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017F9 RID: 6137
			public static readonly StoreNamedPropInfo FBRecursDirty = new StoreNamedPropInfo("FBRecursDirty", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/fbrecurisdirty"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017FA RID: 6138
			public static readonly StoreNamedPropInfo FirstWeekOfYear = new StoreNamedPropInfo("FirstWeekOfYear", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/firstweekofyear"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017FB RID: 6139
			public static readonly StoreNamedPropInfo ForwardingAddress = new StoreNamedPropInfo("ForwardingAddress", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/forwardingaddress"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017FC RID: 6140
			public static readonly StoreNamedPropInfo JunkMailMoveStamp = new StoreNamedPropInfo("JunkMailMoveStamp", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/junkemailmovestamp"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017FD RID: 6141
			public static readonly StoreNamedPropInfo LongDateFormat = new StoreNamedPropInfo("LongDateFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/longdateformat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017FE RID: 6142
			public static readonly StoreNamedPropInfo MSExchEmbAcceptedDevices = new StoreNamedPropInfo("MSExchEmbAcceptedDevices", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembaccepteddevices"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040017FF RID: 6143
			public static readonly StoreNamedPropInfo MSExchEmbCultureInfo = new StoreNamedPropInfo("MSExchEmbCultureInfo", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembcultureinfo"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001800 RID: 6144
			public static readonly StoreNamedPropInfo MSExchEmbDateFormat = new StoreNamedPropInfo("MSExchEmbDateFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembdateformat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001801 RID: 6145
			public static readonly StoreNamedPropInfo MSExchEmbDefaultMailFolder = new StoreNamedPropInfo("MSExchEmbDefaultMailFolder", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembdefaultmailfolder"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001802 RID: 6146
			public static readonly StoreNamedPropInfo MSExchEmbDefaultMailFolderType = new StoreNamedPropInfo("MSExchEmbDefaultMailFolderType", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembdefaultmailfoldertype"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001803 RID: 6147
			public static readonly StoreNamedPropInfo MSExchEmbMarkRead = new StoreNamedPropInfo("MSExchEmbMarkRead", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembmarkread"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001804 RID: 6148
			public static readonly StoreNamedPropInfo MSExchEmbTimeFormat = new StoreNamedPropInfo("MSExchEmbTimeFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembtimeformat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001805 RID: 6149
			public static readonly StoreNamedPropInfo MSExchEmbTimeZone = new StoreNamedPropInfo("MSExchEmbTimeZone", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/msexchembtimezone"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001806 RID: 6150
			public static readonly StoreNamedPropInfo NewMailNotify = new StoreNamedPropInfo("NewMailNotify", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/newmailnotify"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001807 RID: 6151
			public static readonly StoreNamedPropInfo NextSel = new StoreNamedPropInfo("NextSel", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/nextsel"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001808 RID: 6152
			public static readonly StoreNamedPropInfo OtherProxyAddress = new StoreNamedPropInfo("OtherProxyAddress", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/otherproxyaddresses"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001809 RID: 6153
			public static readonly StoreNamedPropInfo Preview = new StoreNamedPropInfo("Preview", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/preview"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400180A RID: 6154
			public static readonly StoreNamedPropInfo PreviewMarkAsRead = new StoreNamedPropInfo("PreviewMarkAsRead", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/previewmarkasread"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400180B RID: 6155
			public static readonly StoreNamedPropInfo PreviewMultiDay = new StoreNamedPropInfo("PreviewMultiDay", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/previewMultiDay"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400180C RID: 6156
			public static readonly StoreNamedPropInfo PrevReadDelayTime = new StoreNamedPropInfo("PrevReadDelayTime", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/prevreaddelaytime"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400180D RID: 6157
			public static readonly StoreNamedPropInfo QuickLinks = new StoreNamedPropInfo("QuickLinks", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/quicklinks"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400180E RID: 6158
			public static readonly StoreNamedPropInfo ReadReceipt = new StoreNamedPropInfo("ReadReceipt", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/readreceipt"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400180F RID: 6159
			public static readonly StoreNamedPropInfo ReminderInterval = new StoreNamedPropInfo("ReminderInterval", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/reminderinterval"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001810 RID: 6160
			public static readonly StoreNamedPropInfo RunAt = new StoreNamedPropInfo("RunAt", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/runat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001811 RID: 6161
			public static readonly StoreNamedPropInfo SchemaVersion = new StoreNamedPropInfo("SchemaVersion", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/schemaversion"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001812 RID: 6162
			public static readonly StoreNamedPropInfo ShortDateFormat = new StoreNamedPropInfo("ShortDateFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/shortdateformat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001813 RID: 6163
			public static readonly StoreNamedPropInfo ShowRulePont = new StoreNamedPropInfo("ShowRulePont", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/showrulepont"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001814 RID: 6164
			public static readonly StoreNamedPropInfo SignatureHTML = new StoreNamedPropInfo("SignatureHTML", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/signaturehtml"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001815 RID: 6165
			public static readonly StoreNamedPropInfo SignatureText = new StoreNamedPropInfo("SignatureText", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/signaturetext"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001816 RID: 6166
			public static readonly StoreNamedPropInfo SMIMEEncrypt = new StoreNamedPropInfo("SMIMEEncrypt", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/smimeencrypt"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001817 RID: 6167
			public static readonly StoreNamedPropInfo SMIMESign = new StoreNamedPropInfo("SMIMESign", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/smimesign"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001818 RID: 6168
			public static readonly StoreNamedPropInfo SpellingCheckBeforeSend = new StoreNamedPropInfo("SpellingCheckBeforeSend", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/spellingcheckbeforesend"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001819 RID: 6169
			public static readonly StoreNamedPropInfo SpellingDictionaryLanguage = new StoreNamedPropInfo("SpellingDictionaryLanguage", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/spellingdictionarylanguage"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400181A RID: 6170
			public static readonly StoreNamedPropInfo SpellingIgnoreMixedDigits = new StoreNamedPropInfo("SpellingIgnoreMixedDigits", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/spellingignoremixeddigits"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400181B RID: 6171
			public static readonly StoreNamedPropInfo SpellingIgnoreUpperCase = new StoreNamedPropInfo("SpellingIgnoreUpperCase", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/spellingignoreuppercase"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400181C RID: 6172
			public static readonly StoreNamedPropInfo ThemeId = new StoreNamedPropInfo("ThemeId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/themeid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400181D RID: 6173
			public static readonly StoreNamedPropInfo TimeFormat = new StoreNamedPropInfo("TimeFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/timeformat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400181E RID: 6174
			public static readonly StoreNamedPropInfo TimeZone = new StoreNamedPropInfo("TimeZone", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/timezone"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400181F RID: 6175
			public static readonly StoreNamedPropInfo ViewRowCount = new StoreNamedPropInfo("ViewRowCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/viewrowcount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001820 RID: 6176
			public static readonly StoreNamedPropInfo VWFlt = new StoreNamedPropInfo("VWFlt", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/vwflt"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001821 RID: 6177
			public static readonly StoreNamedPropInfo WcMultiLine = new StoreNamedPropInfo("WcMultiLine", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/wcmultiline"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001822 RID: 6178
			public static readonly StoreNamedPropInfo WcSortColumn = new StoreNamedPropInfo("WcSortColumn", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/wcsortcolumn"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001823 RID: 6179
			public static readonly StoreNamedPropInfo WcSortOrder = new StoreNamedPropInfo("WcSortOrder", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/wcsortorder"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001824 RID: 6180
			public static readonly StoreNamedPropInfo WCViewHeight = new StoreNamedPropInfo("WCViewHeight", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/wcviewheight"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001825 RID: 6181
			public static readonly StoreNamedPropInfo WCViewWidth = new StoreNamedPropInfo("WCViewWidth", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/wcviewwidth"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001826 RID: 6182
			public static readonly StoreNamedPropInfo WebClientLastUsedSortCols = new StoreNamedPropInfo("WebClientLastUsedSortCols", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/webclientlastusedsortcols"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001827 RID: 6183
			public static readonly StoreNamedPropInfo WebClientLastUsedView = new StoreNamedPropInfo("WebClientLastUsedView", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/webclientlastusedview"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001828 RID: 6184
			public static readonly StoreNamedPropInfo WebClientNavBarWidth = new StoreNamedPropInfo("WebClientNavBarWidth", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/webclientnavbarwidth"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001829 RID: 6185
			public static readonly StoreNamedPropInfo WebClientShowHierarchy = new StoreNamedPropInfo("WebClientShowHierarchy", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/webclientshowhierarchy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400182A RID: 6186
			public static readonly StoreNamedPropInfo WeekStartDay = new StoreNamedPropInfo("WeekStartDay", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/weekstartday"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400182B RID: 6187
			public static readonly StoreNamedPropInfo WorkDayEndTime = new StoreNamedPropInfo("WorkDayEndTime", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/workdayendtime"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400182C RID: 6188
			public static readonly StoreNamedPropInfo WorkDays = new StoreNamedPropInfo("WorkDays", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/workdays"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400182D RID: 6189
			public static readonly StoreNamedPropInfo WordDayStartTime = new StoreNamedPropInfo("WordDayStartTime", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/exchange/workdaystarttime"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400182E RID: 6190
			public static readonly StoreNamedPropInfo PhishingStamp = new StoreNamedPropInfo("PhishingStamp", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/outlook/phishingstamp"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400182F RID: 6191
			public static readonly StoreNamedPropInfo SpoofingStamp = new StoreNamedPropInfo("SpoofingStamp", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "http://schemas.microsoft.com/outlook/spoofingstamp"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001830 RID: 6192
			public static readonly StoreNamedPropInfo Keywords = new StoreNamedPropInfo("Keywords", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Keywords"), PropertyType.MVUnicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001831 RID: 6193
			public static readonly StoreNamedPropInfo LastAuthor = new StoreNamedPropInfo("LastAuthor", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "LastAuthor"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001832 RID: 6194
			public static readonly StoreNamedPropInfo LastPrinted = new StoreNamedPropInfo("LastPrinted", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "LastPrinted"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001833 RID: 6195
			public static readonly StoreNamedPropInfo LastSaveDtm = new StoreNamedPropInfo("LastSaveDtm", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "LastSaveDtm"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001834 RID: 6196
			public static readonly StoreNamedPropInfo LineCount = new StoreNamedPropInfo("LineCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "LineCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001835 RID: 6197
			public static readonly StoreNamedPropInfo LinksDirty = new StoreNamedPropInfo("LinksDirty", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "LinksDirty"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001836 RID: 6198
			public static readonly StoreNamedPropInfo Manager = new StoreNamedPropInfo("Manager", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Manager"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001837 RID: 6199
			public static readonly StoreNamedPropInfo MMClipCount = new StoreNamedPropInfo("MMClipCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MMClipCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001838 RID: 6200
			public static readonly StoreNamedPropInfo NoteCount = new StoreNamedPropInfo("NoteCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "NoteCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001839 RID: 6201
			public static readonly StoreNamedPropInfo NumElements = new StoreNamedPropInfo("NumElements", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "NumElements"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400183A RID: 6202
			public static readonly StoreNamedPropInfo PageCount = new StoreNamedPropInfo("PageCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "PageCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400183B RID: 6203
			public static readonly StoreNamedPropInfo ParCount = new StoreNamedPropInfo("ParCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ParCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400183C RID: 6204
			public static readonly StoreNamedPropInfo PresFormat = new StoreNamedPropInfo("PresFormat", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "PresFormat"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400183D RID: 6205
			public static readonly StoreNamedPropInfo ProgAddrBookCDO = new StoreNamedPropInfo("ProgAddrBookCDO", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgAddrBookCDO"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400183E RID: 6206
			public static readonly StoreNamedPropInfo ProgAddrBookOOM = new StoreNamedPropInfo("ProgAddrBookOOM", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgAddrBookOOM"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400183F RID: 6207
			public static readonly StoreNamedPropInfo ProgAddrBookSMAPI = new StoreNamedPropInfo("ProgAddrBookSMAPI", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgAddrBookSMAPI"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001840 RID: 6208
			public static readonly StoreNamedPropInfo ProgAddrInfoCDO = new StoreNamedPropInfo("ProgAddrInfoCDO", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgAddrInfoCDO"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001841 RID: 6209
			public static readonly StoreNamedPropInfo ProgAddrInfoOOM = new StoreNamedPropInfo("ProgAddrInfoOOM", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgAddrInfoOOM"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001842 RID: 6210
			public static readonly StoreNamedPropInfo ProgAddrInfoSMAPI = new StoreNamedPropInfo("ProgAddrInfoSMAPI", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgAddrInfoSMAPI"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001843 RID: 6211
			public static readonly StoreNamedPropInfo ProgCustVerb = new StoreNamedPropInfo("ProgCustVerb", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgCustVerb"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001844 RID: 6212
			public static readonly StoreNamedPropInfo ProgRespond = new StoreNamedPropInfo("ProgRespond", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgRespond"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001845 RID: 6213
			public static readonly StoreNamedPropInfo ProgSaveAs = new StoreNamedPropInfo("ProgSaveAs", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgSaveAs"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001846 RID: 6214
			public static readonly StoreNamedPropInfo ProgSearch = new StoreNamedPropInfo("ProgSearch", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgSearch"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001847 RID: 6215
			public static readonly StoreNamedPropInfo ProgSendCDO = new StoreNamedPropInfo("ProgSendCDO", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgSendCDO"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001848 RID: 6216
			public static readonly StoreNamedPropInfo ProgSendOOM = new StoreNamedPropInfo("ProgSendOOM", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgSendOOM"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001849 RID: 6217
			public static readonly StoreNamedPropInfo ProgSendSMAPI = new StoreNamedPropInfo("ProgSendSMAPI", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ProgSendSMAPI"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400184A RID: 6218
			public static readonly StoreNamedPropInfo QuarantineOriginalSender = new StoreNamedPropInfo("QuarantineOriginalSender", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "quarantine-original-sender"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400184B RID: 6219
			public static readonly StoreNamedPropInfo RemoveLevel1 = new StoreNamedPropInfo("RemoveLevel1", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "RemoveLevel1"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400184C RID: 6220
			public static readonly StoreNamedPropInfo RemoveLevel2 = new StoreNamedPropInfo("RemoveLevel2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "RemoveLevel2"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400184D RID: 6221
			public static readonly StoreNamedPropInfo RevNumber = new StoreNamedPropInfo("RevNumber", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "RevNumber"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400184E RID: 6222
			public static readonly StoreNamedPropInfo Scale = new StoreNamedPropInfo("Scale", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Scale"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400184F RID: 6223
			public static readonly StoreNamedPropInfo Security = new StoreNamedPropInfo("Security", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Security"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001850 RID: 6224
			public static readonly StoreNamedPropInfo ShowAllAttach = new StoreNamedPropInfo("ShowAllAttach", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ShowAllAttach"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001851 RID: 6225
			public static readonly StoreNamedPropInfo SlideCount = new StoreNamedPropInfo("SlideCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "SlideCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001852 RID: 6226
			public static readonly StoreNamedPropInfo STSAttachIdTable = new StoreNamedPropInfo("STSAttachIdTable", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_AttachIDTable"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001853 RID: 6227
			public static readonly StoreNamedPropInfo STSBaseUrl = new StoreNamedPropInfo("STSBaseUrl", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_BaseURL"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001854 RID: 6228
			public static readonly StoreNamedPropInfo STSEventType = new StoreNamedPropInfo("STSEventType", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_EventType"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001855 RID: 6229
			public static readonly StoreNamedPropInfo STSEventUid = new StoreNamedPropInfo("STSEventUid", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_EventUID"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001856 RID: 6230
			public static readonly StoreNamedPropInfo STSFooterInfo = new StoreNamedPropInfo("STSFooterInfo", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_FooterInfo"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001857 RID: 6231
			public static readonly StoreNamedPropInfo STSHeaderInfo = new StoreNamedPropInfo("STSHeaderInfo", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_HeaderInfo"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001858 RID: 6232
			public static readonly StoreNamedPropInfo STSHiddenVer = new StoreNamedPropInfo("STSHiddenVer", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_HiddenVer"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001859 RID: 6233
			public static readonly StoreNamedPropInfo STSId = new StoreNamedPropInfo("STSId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_Id"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400185A RID: 6234
			public static readonly StoreNamedPropInfo STSIDTable = new StoreNamedPropInfo("STSIDTable", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_IDTable"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400185B RID: 6235
			public static readonly StoreNamedPropInfo STSLastSync = new StoreNamedPropInfo("STSLastSync", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_LastSync"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400185C RID: 6236
			public static readonly StoreNamedPropInfo STSListFriendlyName = new StoreNamedPropInfo("STSListFriendlyName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_ListFriendlyName"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400185D RID: 6237
			public static readonly StoreNamedPropInfo STSListGuid = new StoreNamedPropInfo("STSListGuid", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_ListGUID"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400185E RID: 6238
			public static readonly StoreNamedPropInfo STSListUrl = new StoreNamedPropInfo("STSListUrl", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_ListURL"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400185F RID: 6239
			public static readonly StoreNamedPropInfo STSRecurrenceId = new StoreNamedPropInfo("STSRecurrenceId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_RecurrenceID"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001860 RID: 6240
			public static readonly StoreNamedPropInfo STSRecurXml = new StoreNamedPropInfo("STSRecurXml", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_RecurXml"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001861 RID: 6241
			public static readonly StoreNamedPropInfo STSSharePointFolder = new StoreNamedPropInfo("STSSharePointFolder", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_SharePointFolder"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001862 RID: 6242
			public static readonly StoreNamedPropInfo STSSiteName = new StoreNamedPropInfo("STSSiteName", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_SiteName"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001863 RID: 6243
			public static readonly StoreNamedPropInfo STSTimeStamp = new StoreNamedPropInfo("STSTimeStamp", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_TimeStamp"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001864 RID: 6244
			public static readonly StoreNamedPropInfo STSTimeStamp2 = new StoreNamedPropInfo("STSTimeStamp2", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_TimeStamp2"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001865 RID: 6245
			public static readonly StoreNamedPropInfo STSUserId = new StoreNamedPropInfo("STSUserId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "STS_UserId"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001866 RID: 6246
			public static readonly StoreNamedPropInfo Subject = new StoreNamedPropInfo("Subject", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Subject"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001867 RID: 6247
			public static readonly StoreNamedPropInfo Template = new StoreNamedPropInfo("Template", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Template"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001868 RID: 6248
			public static readonly StoreNamedPropInfo Thumbnail = new StoreNamedPropInfo("Thumbnail", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Thumbnail"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001869 RID: 6249
			public static readonly StoreNamedPropInfo ThumbNail = new StoreNamedPropInfo("ThumbNail", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "ThumbNail"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400186A RID: 6250
			public static readonly StoreNamedPropInfo Title = new StoreNamedPropInfo("Title", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "Title"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400186B RID: 6251
			public static readonly StoreNamedPropInfo TrustedCode = new StoreNamedPropInfo("TrustedCode", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "TrustedCode"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400186C RID: 6252
			public static readonly StoreNamedPropInfo AttendeeRole = new StoreNamedPropInfo("AttendeeRole", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:attendeerole"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400186D RID: 6253
			public static readonly StoreNamedPropInfo Contact = new StoreNamedPropInfo("Contact", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:contact"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400186E RID: 6254
			public static readonly StoreNamedPropInfo ContactURL = new StoreNamedPropInfo("ContactURL", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:contacturl"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400186F RID: 6255
			public static readonly StoreNamedPropInfo DescriptionURL = new StoreNamedPropInfo("DescriptionURL", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:descriptionurl"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001870 RID: 6256
			public static readonly StoreNamedPropInfo ExRule = new StoreNamedPropInfo("ExRule", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:exrule"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001871 RID: 6257
			public static readonly StoreNamedPropInfo GeoLatitude = new StoreNamedPropInfo("GeoLatitude", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:geolatitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001872 RID: 6258
			public static readonly StoreNamedPropInfo GeoLongitude = new StoreNamedPropInfo("GeoLongitude", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:geolongitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001873 RID: 6259
			public static readonly StoreNamedPropInfo IsOrganizer = new StoreNamedPropInfo("IsOrganizer", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:isorganizer"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001874 RID: 6260
			public static readonly StoreNamedPropInfo LocationUrl = new StoreNamedPropInfo("LocationUrl", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:locationurl"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001875 RID: 6261
			public static readonly StoreNamedPropInfo ProdId = new StoreNamedPropInfo("ProdId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:prodid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001876 RID: 6262
			public static readonly StoreNamedPropInfo RDate = new StoreNamedPropInfo("RDate", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:rdate"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001877 RID: 6263
			public static readonly StoreNamedPropInfo RecurrenceIdRange = new StoreNamedPropInfo("RecurrenceIdRange", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:recurrenceidrange"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001878 RID: 6264
			public static readonly StoreNamedPropInfo Resources = new StoreNamedPropInfo("Resources", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:resources"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001879 RID: 6265
			public static readonly StoreNamedPropInfo RSVP = new StoreNamedPropInfo("RSVP", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:rsvp"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400187A RID: 6266
			public static readonly StoreNamedPropInfo TimezoneId = new StoreNamedPropInfo("TimezoneId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:timezoneid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400187B RID: 6267
			public static readonly StoreNamedPropInfo Transparent = new StoreNamedPropInfo("Transparent", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:calendar:transparent"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400187C RID: 6268
			public static readonly StoreNamedPropInfo AlternateRecipient = new StoreNamedPropInfo("AlternateRecipient", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:alternaterecipient"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400187D RID: 6269
			public static readonly StoreNamedPropInfo ContactC = new StoreNamedPropInfo("ContactC", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:c"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400187E RID: 6270
			public static readonly StoreNamedPropInfo HomeLatitude = new StoreNamedPropInfo("HomeLatitude", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:homelatitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400187F RID: 6271
			public static readonly StoreNamedPropInfo HomeLongitude = new StoreNamedPropInfo("HomeLongitude", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:homelongitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001880 RID: 6272
			public static readonly StoreNamedPropInfo HomeTimeZone = new StoreNamedPropInfo("HomeTimeZone", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:hometimezone"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001881 RID: 6273
			public static readonly StoreNamedPropInfo OtherCountryCode = new StoreNamedPropInfo("OtherCountryCode", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:othercountrycode"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001882 RID: 6274
			public static readonly StoreNamedPropInfo OtherPager = new StoreNamedPropInfo("OtherPager", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:otherpager"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001883 RID: 6275
			public static readonly StoreNamedPropInfo OtherTimeZone = new StoreNamedPropInfo("OtherTimeZone", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:othertimezone"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001884 RID: 6276
			public static readonly StoreNamedPropInfo ContactsPSRDate = new StoreNamedPropInfo("ContactsPSRDate", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:rdate"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001885 RID: 6277
			public static readonly StoreNamedPropInfo RRule = new StoreNamedPropInfo("RRule", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:rrule"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001886 RID: 6278
			public static readonly StoreNamedPropInfo SecretaryURL = new StoreNamedPropInfo("SecretaryURL", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:secretaryurl"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001887 RID: 6279
			public static readonly StoreNamedPropInfo SourceURL = new StoreNamedPropInfo("SourceURL", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:sourceurl"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001888 RID: 6280
			public static readonly StoreNamedPropInfo FileAs = new StoreNamedPropInfo("FileAs", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:contacts:fileas"), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001889 RID: 6281
			public static readonly StoreNamedPropInfo SaveDestination = new StoreNamedPropInfo("SaveDestination", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:httpmail:savedestination"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400188A RID: 6282
			public static readonly StoreNamedPropInfo SaveInSent = new StoreNamedPropInfo("SaveInSent", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:httpmail:saveinsent"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400188B RID: 6283
			public static readonly StoreNamedPropInfo Special = new StoreNamedPropInfo("Special", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas:httpmail:special"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400188C RID: 6284
			public static readonly StoreNamedPropInfo Type = new StoreNamedPropInfo("Type", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:datatypes#type"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400188D RID: 6285
			public static readonly StoreNamedPropInfo ClosedExpectedContentClasses = new StoreNamedPropInfo("ClosedExpectedContentClasses", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:closedexpectedcontentclasses"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400188E RID: 6286
			public static readonly StoreNamedPropInfo CodeBase = new StoreNamedPropInfo("CodeBase", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:codebase"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400188F RID: 6287
			public static readonly StoreNamedPropInfo ComClassId = new StoreNamedPropInfo("ComClassId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:comclassid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001890 RID: 6288
			public static readonly StoreNamedPropInfo Comprogid = new StoreNamedPropInfo("Comprogid", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:comprogid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001891 RID: 6289
			public static readonly StoreNamedPropInfo Default = new StoreNamedPropInfo("Default", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:default"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001892 RID: 6290
			public static readonly StoreNamedPropInfo Dictionary = new StoreNamedPropInfo("Dictionary", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:dictionary"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001893 RID: 6291
			public static readonly StoreNamedPropInfo Form = new StoreNamedPropInfo("Form", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:form"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001894 RID: 6292
			public static readonly StoreNamedPropInfo IsContentIndexed = new StoreNamedPropInfo("IsContentIndexed", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:iscontentindexed"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001895 RID: 6293
			public static readonly StoreNamedPropInfo IsIndexed = new StoreNamedPropInfo("IsIndexed", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:isindexed"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001896 RID: 6294
			public static readonly StoreNamedPropInfo IsMultiValued = new StoreNamedPropInfo("IsMultiValued", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:ismultivalued"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001897 RID: 6295
			public static readonly StoreNamedPropInfo IsReadOnly = new StoreNamedPropInfo("IsReadOnly", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:isreadonly"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001898 RID: 6296
			public static readonly StoreNamedPropInfo IsRequired = new StoreNamedPropInfo("IsRequired", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:isrequired"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001899 RID: 6297
			public static readonly StoreNamedPropInfo IsVisible = new StoreNamedPropInfo("IsVisible", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:isvisible"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400189A RID: 6298
			public static readonly StoreNamedPropInfo PropertyDef = new StoreNamedPropInfo("PropertyDef", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:propertydef"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400189B RID: 6299
			public static readonly StoreNamedPropInfo Synchronize = new StoreNamedPropInfo("Synchronize", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:synchronize"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400189C RID: 6300
			public static readonly StoreNamedPropInfo Version = new StoreNamedPropInfo("Version", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:exch-data:version"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400189D RID: 6301
			public static readonly StoreNamedPropInfo DesignerMembership = new StoreNamedPropInfo("DesignerMembership", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:designer#Membership"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400189E RID: 6302
			public static readonly StoreNamedPropInfo TemplateDescription = new StoreNamedPropInfo("TemplateDescription", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:designer#templatedescription"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400189F RID: 6303
			public static readonly StoreNamedPropInfo OutlookAllOrNoneMtgUpdatedLG = new StoreNamedPropInfo("OutlookAllOrNoneMtgUpdatedLG", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#allornonemtgupdatedlg"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A0 RID: 6304
			public static readonly StoreNamedPropInfo OutlookDoCoercion = new StoreNamedPropInfo("OutlookDoCoercion", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#docoercion"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A1 RID: 6305
			public static readonly StoreNamedPropInfo OutlookFixupFBFolder = new StoreNamedPropInfo("OutlookFixupFBFolder", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#fixupfbfolder"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A2 RID: 6306
			public static readonly StoreNamedPropInfo OutlookHideFilterTab = new StoreNamedPropInfo("OutlookHideFilterTab", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#hidefiltertab"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A3 RID: 6307
			public static readonly StoreNamedPropInfo OutlookIsHidden = new StoreNamedPropInfo("OutlookIsHidden", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#ishidden"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A4 RID: 6308
			public static readonly StoreNamedPropInfo OutlookNoAclUI = new StoreNamedPropInfo("OutlookNoAclUI", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#noaclui"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A5 RID: 6309
			public static readonly StoreNamedPropInfo OutlookNoDuplicateDocuments = new StoreNamedPropInfo("OutlookNoDuplicateDocuments", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#noduplicatedocuments"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A6 RID: 6310
			public static readonly StoreNamedPropInfo OutlookServerFolderSizes = new StoreNamedPropInfo("OutlookServerFolderSizes", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#serverfoldersizes"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A7 RID: 6311
			public static readonly StoreNamedPropInfo OutlookShowForward = new StoreNamedPropInfo("OutlookShowForward", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#showforward"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A8 RID: 6312
			public static readonly StoreNamedPropInfo OutlookShowReply = new StoreNamedPropInfo("OutlookShowReply", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#showreply"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018A9 RID: 6313
			public static readonly StoreNamedPropInfo OutlookStoreTypePrivate = new StoreNamedPropInfo("OutlookStoreTypePrivate", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:office:outlook#storetypeprivate"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018AA RID: 6314
			public static readonly StoreNamedPropInfo Element = new StoreNamedPropInfo("Element", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:xml-data#element"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018AB RID: 6315
			public static readonly StoreNamedPropInfo Extends = new StoreNamedPropInfo("Extends", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:xml-data#extends"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018AC RID: 6316
			public static readonly StoreNamedPropInfo Name = new StoreNamedPropInfo("Name", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "urn:schemas-microsoft-com:xml-data#name"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018AD RID: 6317
			public static readonly StoreNamedPropInfo UserFormula = new StoreNamedPropInfo("UserFormula", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "UserFormula"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018AE RID: 6318
			public static readonly StoreNamedPropInfo WordCount = new StoreNamedPropInfo("WordCount", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "WordCount"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018AF RID: 6319
			public static readonly StoreNamedPropInfo DRMServerLicenseCompressed = new StoreNamedPropInfo("DRMServerLicenseCompressed", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "DRMServerLicenseCompressed"), PropertyType.Binary, 1024UL, default(PropertyCategories));

			// Token: 0x040018B0 RID: 6320
			public static readonly StoreNamedPropInfo DRMServerLicense = new StoreNamedPropInfo("DRMServerLicense", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "DRMServerLicense"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B1 RID: 6321
			public static readonly StoreNamedPropInfo MonitoringUniqueId = new StoreNamedPropInfo("MonitoringUniqueId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringUniqueId"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B2 RID: 6322
			public static readonly StoreNamedPropInfo MonitoringEventSource = new StoreNamedPropInfo("MonitoringEventSource", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringEventSource"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B3 RID: 6323
			public static readonly StoreNamedPropInfo MonitoringEventInstanceId = new StoreNamedPropInfo("MonitoringEventInstanceId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringEventInstanceId"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B4 RID: 6324
			public static readonly StoreNamedPropInfo MonitoringInsertionStrings = new StoreNamedPropInfo("MonitoringInsertionStrings", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringInsertionStrings"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B5 RID: 6325
			public static readonly StoreNamedPropInfo MonitoringCreationTimeUtc = new StoreNamedPropInfo("MonitoringCreationTimeUtc", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringCreationTimeUtc"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B6 RID: 6326
			public static readonly StoreNamedPropInfo MonitoringNotificationEmailSent = new StoreNamedPropInfo("MonitoringNotificationEmailSent", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringNotificationEmailSent"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B7 RID: 6327
			public static readonly StoreNamedPropInfo MonitoringEventEntryType = new StoreNamedPropInfo("MonitoringEventEntryType", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringEventEntryType"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B8 RID: 6328
			public static readonly StoreNamedPropInfo MonitoringNotificationRecipients = new StoreNamedPropInfo("MonitoringNotificationRecipients", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringNotificationRecipients"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018B9 RID: 6329
			public static readonly StoreNamedPropInfo MonitoringEventCategoryId = new StoreNamedPropInfo("MonitoringEventCategoryId", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringEventCategoryId"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018BA RID: 6330
			public static readonly StoreNamedPropInfo MonitoringEventTimeUtc = new StoreNamedPropInfo("MonitoringEventTimeUtc", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringEventTimeUtc"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018BB RID: 6331
			public static readonly StoreNamedPropInfo MonitoringNotificationMessageIds = new StoreNamedPropInfo("MonitoringNotificationMessageIds", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringNotificationMessageIds"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018BC RID: 6332
			public static readonly StoreNamedPropInfo MonitoringEventPeriodicKey = new StoreNamedPropInfo("MonitoringEventPeriodicKey", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, "MonitoringEventPeriodicKey"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018BD RID: 6333
			public static readonly StoreNamedPropInfo OutlookContactLinkDateTime = new StoreNamedPropInfo("OutlookContactLinkDateTime", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, 32993U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018BE RID: 6334
			public static readonly StoreNamedPropInfo OutlookContactLinkVersion = new StoreNamedPropInfo("OutlookContactLinkVersion", new StorePropName(NamedPropInfo.PublicStrings.NamespaceGuid, 32999U), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200001A RID: 26
		public static class Calendar
		{
			// Token: 0x040018BF RID: 6335
			public static readonly Guid NamespaceGuid = new Guid("00062001-0000-0000-C000-000000000046");
		}

		// Token: 0x0200001B RID: 27
		public static class Appointment
		{
			// Token: 0x040018C0 RID: 6336
			public static readonly Guid NamespaceGuid = new Guid("00062002-0000-0000-C000-000000000046");

			// Token: 0x040018C1 RID: 6337
			public static readonly StoreNamedPropInfo SendMtgAsICAL = new StoreNamedPropInfo("SendMtgAsICAL", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33280U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018C2 RID: 6338
			public static readonly StoreNamedPropInfo ApptSequence = new StoreNamedPropInfo("ApptSequence", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33281U), PropertyType.Int32, 2048UL, default(PropertyCategories));

			// Token: 0x040018C3 RID: 6339
			public static readonly StoreNamedPropInfo ApptSeqTime = new StoreNamedPropInfo("ApptSeqTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33282U), PropertyType.SysTime, 2048UL, default(PropertyCategories));

			// Token: 0x040018C4 RID: 6340
			public static readonly StoreNamedPropInfo ApptLastSequence = new StoreNamedPropInfo("ApptLastSequence", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33283U), PropertyType.Int32, 2048UL, default(PropertyCategories));

			// Token: 0x040018C5 RID: 6341
			public static readonly StoreNamedPropInfo ChangeHighlight = new StoreNamedPropInfo("ChangeHighlight", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33284U), PropertyType.Int32, 2048UL, default(PropertyCategories));

			// Token: 0x040018C6 RID: 6342
			public static readonly StoreNamedPropInfo BusyStatus = new StoreNamedPropInfo("BusyStatus", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33285U), PropertyType.Int32, 72057594038190080UL, default(PropertyCategories));

			// Token: 0x040018C7 RID: 6343
			public static readonly StoreNamedPropInfo FExceptionalBody = new StoreNamedPropInfo("FExceptionalBody", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33286U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018C8 RID: 6344
			public static readonly StoreNamedPropInfo ApptAuxFlags = new StoreNamedPropInfo("ApptAuxFlags", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33287U), PropertyType.Int32, 72057594038190080UL, default(PropertyCategories));

			// Token: 0x040018C9 RID: 6345
			public static readonly StoreNamedPropInfo Location = new StoreNamedPropInfo("Location", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33288U), PropertyType.Unicode, 2305843009213825024UL, default(PropertyCategories));

			// Token: 0x040018CA RID: 6346
			public static readonly StoreNamedPropInfo MWSURL = new StoreNamedPropInfo("MWSURL", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33289U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018CB RID: 6347
			public static readonly StoreNamedPropInfo FwrdInstance = new StoreNamedPropInfo("FwrdInstance", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33290U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018CC RID: 6348
			public static readonly StoreNamedPropInfo LinkedTaskItems = new StoreNamedPropInfo("LinkedTaskItems", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33292U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018CD RID: 6349
			public static readonly StoreNamedPropInfo ApptStartWhole = new StoreNamedPropInfo("ApptStartWhole", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33293U), PropertyType.SysTime, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018CE RID: 6350
			public static readonly StoreNamedPropInfo ApptEndWhole = new StoreNamedPropInfo("ApptEndWhole", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33294U), PropertyType.SysTime, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018CF RID: 6351
			public static readonly StoreNamedPropInfo ApptStartTime = new StoreNamedPropInfo("ApptStartTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33295U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018D0 RID: 6352
			public static readonly StoreNamedPropInfo ApptEndTime = new StoreNamedPropInfo("ApptEndTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33296U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018D1 RID: 6353
			public static readonly StoreNamedPropInfo ApptEndDate = new StoreNamedPropInfo("ApptEndDate", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33297U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018D2 RID: 6354
			public static readonly StoreNamedPropInfo ApptStartDate = new StoreNamedPropInfo("ApptStartDate", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33298U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018D3 RID: 6355
			public static readonly StoreNamedPropInfo ApptDuration = new StoreNamedPropInfo("ApptDuration", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33299U), PropertyType.Int32, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018D4 RID: 6356
			public static readonly StoreNamedPropInfo ApptColor = new StoreNamedPropInfo("ApptColor", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33300U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018D5 RID: 6357
			public static readonly StoreNamedPropInfo ApptSubType = new StoreNamedPropInfo("ApptSubType", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33301U), PropertyType.Boolean, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018D6 RID: 6358
			public static readonly StoreNamedPropInfo ApptRecur = new StoreNamedPropInfo("ApptRecur", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33302U), PropertyType.Binary, 72057594037993472UL, default(PropertyCategories));

			// Token: 0x040018D7 RID: 6359
			public static readonly StoreNamedPropInfo ApptStateFlags = new StoreNamedPropInfo("ApptStateFlags", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33303U), PropertyType.Int32, 72057594038190080UL, default(PropertyCategories));

			// Token: 0x040018D8 RID: 6360
			public static readonly StoreNamedPropInfo ResponseStatus = new StoreNamedPropInfo("ResponseStatus", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33304U), PropertyType.Int32, 8192UL, default(PropertyCategories));

			// Token: 0x040018D9 RID: 6361
			public static readonly StoreNamedPropInfo ApptReplyTime = new StoreNamedPropInfo("ApptReplyTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33312U), PropertyType.SysTime, 8192UL, default(PropertyCategories));

			// Token: 0x040018DA RID: 6362
			public static readonly StoreNamedPropInfo Recurring = new StoreNamedPropInfo("Recurring", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33315U), PropertyType.Boolean, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018DB RID: 6363
			public static readonly StoreNamedPropInfo IntendedBusyStatus = new StoreNamedPropInfo("IntendedBusyStatus", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33316U), PropertyType.Int32, 72057594038190080UL, default(PropertyCategories));

			// Token: 0x040018DC RID: 6364
			public static readonly StoreNamedPropInfo DeleteAssocRequest = new StoreNamedPropInfo("DeleteAssocRequest", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33317U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018DD RID: 6365
			public static readonly StoreNamedPropInfo ApptUpdateTime = new StoreNamedPropInfo("ApptUpdateTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33318U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018DE RID: 6366
			public static readonly StoreNamedPropInfo DirtyTimesOrStatus = new StoreNamedPropInfo("DirtyTimesOrStatus", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33319U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018DF RID: 6367
			public static readonly StoreNamedPropInfo ExceptionReplaceTime = new StoreNamedPropInfo("ExceptionReplaceTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33320U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018E0 RID: 6368
			public static readonly StoreNamedPropInfo FInvited = new StoreNamedPropInfo("FInvited", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33321U), PropertyType.Boolean, 2048UL, default(PropertyCategories));

			// Token: 0x040018E1 RID: 6369
			public static readonly StoreNamedPropInfo OrganizerExceptionReplaceTime = new StoreNamedPropInfo("OrganizerExceptionReplaceTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33322U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018E2 RID: 6370
			public static readonly StoreNamedPropInfo FExceptionalAttendees = new StoreNamedPropInfo("FExceptionalAttendees", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33323U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018E3 RID: 6371
			public static readonly StoreNamedPropInfo FDirtyTimes = new StoreNamedPropInfo("FDirtyTimes", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33324U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018E4 RID: 6372
			public static readonly StoreNamedPropInfo FDirtyLocation = new StoreNamedPropInfo("FDirtyLocation", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33325U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018E5 RID: 6373
			public static readonly StoreNamedPropInfo OwnerName = new StoreNamedPropInfo("OwnerName", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33326U), PropertyType.Unicode, 72057594038190080UL, default(PropertyCategories));

			// Token: 0x040018E6 RID: 6374
			public static readonly StoreNamedPropInfo FOthersAppt = new StoreNamedPropInfo("FOthersAppt", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33327U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018E7 RID: 6375
			public static readonly StoreNamedPropInfo ApptReplyName = new StoreNamedPropInfo("ApptReplyName", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33328U), PropertyType.Unicode, 8192UL, default(PropertyCategories));

			// Token: 0x040018E8 RID: 6376
			public static readonly StoreNamedPropInfo RecurType = new StoreNamedPropInfo("RecurType", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33329U), PropertyType.Int32, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018E9 RID: 6377
			public static readonly StoreNamedPropInfo RecurPattern = new StoreNamedPropInfo("RecurPattern", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33330U), PropertyType.Unicode, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018EA RID: 6378
			public static readonly StoreNamedPropInfo TimeZoneStruct = new StoreNamedPropInfo("TimeZoneStruct", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33331U), PropertyType.Binary, 524288UL, default(PropertyCategories));

			// Token: 0x040018EB RID: 6379
			public static readonly StoreNamedPropInfo TimeZoneDesc = new StoreNamedPropInfo("TimeZoneDesc", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33332U), PropertyType.Unicode, 524288UL, default(PropertyCategories));

			// Token: 0x040018EC RID: 6380
			public static readonly StoreNamedPropInfo ClipStart = new StoreNamedPropInfo("ClipStart", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33333U), PropertyType.SysTime, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018ED RID: 6381
			public static readonly StoreNamedPropInfo ClipEnd = new StoreNamedPropInfo("ClipEnd", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33334U), PropertyType.SysTime, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x040018EE RID: 6382
			public static readonly StoreNamedPropInfo OrigStoreEid = new StoreNamedPropInfo("OrigStoreEid", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33335U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018EF RID: 6383
			public static readonly StoreNamedPropInfo AllAttendeesString = new StoreNamedPropInfo("AllAttendeesString", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33336U), PropertyType.Unicode, 16UL, default(PropertyCategories));

			// Token: 0x040018F0 RID: 6384
			public static readonly StoreNamedPropInfo FMtgDataChanged = new StoreNamedPropInfo("FMtgDataChanged", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33337U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F1 RID: 6385
			public static readonly StoreNamedPropInfo AutoFillLocation = new StoreNamedPropInfo("AutoFillLocation", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33338U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F2 RID: 6386
			public static readonly StoreNamedPropInfo ToAttendeesString = new StoreNamedPropInfo("ToAttendeesString", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33339U), PropertyType.Unicode, 16UL, default(PropertyCategories));

			// Token: 0x040018F3 RID: 6387
			public static readonly StoreNamedPropInfo CCAttendeesString = new StoreNamedPropInfo("CCAttendeesString", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33340U), PropertyType.Unicode, 16UL, default(PropertyCategories));

			// Token: 0x040018F4 RID: 6388
			public static readonly StoreNamedPropInfo SowFBFlags = new StoreNamedPropInfo("SowFBFlags", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33341U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F5 RID: 6389
			public static readonly StoreNamedPropInfo TrustRecipHighlights = new StoreNamedPropInfo("TrustRecipHighlights", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33342U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F6 RID: 6390
			public static readonly StoreNamedPropInfo ConfCheckChanged = new StoreNamedPropInfo("ConfCheckChanged", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33343U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F7 RID: 6391
			public static readonly StoreNamedPropInfo ConfCheck = new StoreNamedPropInfo("ConfCheck", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33344U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F8 RID: 6392
			public static readonly StoreNamedPropInfo ConfType = new StoreNamedPropInfo("ConfType", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33345U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018F9 RID: 6393
			public static readonly StoreNamedPropInfo Directory = new StoreNamedPropInfo("Directory", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33346U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018FA RID: 6394
			public static readonly StoreNamedPropInfo OrgAlias = new StoreNamedPropInfo("OrgAlias", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33347U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018FB RID: 6395
			public static readonly StoreNamedPropInfo AutoStartCheck = new StoreNamedPropInfo("AutoStartCheck", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33348U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018FC RID: 6396
			public static readonly StoreNamedPropInfo AutoStartWhen = new StoreNamedPropInfo("AutoStartWhen", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33349U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018FD RID: 6397
			public static readonly StoreNamedPropInfo AllowExternCheck = new StoreNamedPropInfo("AllowExternCheck", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33350U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018FE RID: 6398
			public static readonly StoreNamedPropInfo CollaborateDoc = new StoreNamedPropInfo("CollaborateDoc", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33351U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040018FF RID: 6399
			public static readonly StoreNamedPropInfo NetShowURL = new StoreNamedPropInfo("NetShowURL", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33352U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001900 RID: 6400
			public static readonly StoreNamedPropInfo OnlinePassword = new StoreNamedPropInfo("OnlinePassword", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33353U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001901 RID: 6401
			public static readonly StoreNamedPropInfo ApptProposedStartWhole = new StoreNamedPropInfo("ApptProposedStartWhole", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33360U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001902 RID: 6402
			public static readonly StoreNamedPropInfo ApptProposedEndWhole = new StoreNamedPropInfo("ApptProposedEndWhole", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33361U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001903 RID: 6403
			public static readonly StoreNamedPropInfo ApptProposedStartTime = new StoreNamedPropInfo("ApptProposedStartTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33362U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001904 RID: 6404
			public static readonly StoreNamedPropInfo ApptProposedEndTime = new StoreNamedPropInfo("ApptProposedEndTime", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33363U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001905 RID: 6405
			public static readonly StoreNamedPropInfo ApptProposedStartDate = new StoreNamedPropInfo("ApptProposedStartDate", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33364U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001906 RID: 6406
			public static readonly StoreNamedPropInfo ApptProposedEndDate = new StoreNamedPropInfo("ApptProposedEndDate", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33365U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001907 RID: 6407
			public static readonly StoreNamedPropInfo ApptProposedDuration = new StoreNamedPropInfo("ApptProposedDuration", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33366U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001908 RID: 6408
			public static readonly StoreNamedPropInfo ApptCounterProposal = new StoreNamedPropInfo("ApptCounterProposal", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33367U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001909 RID: 6409
			public static readonly StoreNamedPropInfo ApptProposalNum = new StoreNamedPropInfo("ApptProposalNum", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33369U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400190A RID: 6410
			public static readonly StoreNamedPropInfo ApptNotAllowPropose = new StoreNamedPropInfo("ApptNotAllowPropose", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33370U), PropertyType.Boolean, 16384UL, default(PropertyCategories));

			// Token: 0x0400190B RID: 6411
			public static readonly StoreNamedPropInfo ApptOpenViewProposal = new StoreNamedPropInfo("ApptOpenViewProposal", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33371U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400190C RID: 6412
			public static readonly StoreNamedPropInfo ApptProposedLocation = new StoreNamedPropInfo("ApptProposedLocation", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33372U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400190D RID: 6413
			public static readonly StoreNamedPropInfo ApptUnsendableRecips = new StoreNamedPropInfo("ApptUnsendableRecips", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33373U), PropertyType.Binary, 16UL, default(PropertyCategories));

			// Token: 0x0400190E RID: 6414
			public static readonly StoreNamedPropInfo ApptTZDefStartDisplay = new StoreNamedPropInfo("ApptTZDefStartDisplay", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33374U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400190F RID: 6415
			public static readonly StoreNamedPropInfo ApptTZDefEndDisplay = new StoreNamedPropInfo("ApptTZDefEndDisplay", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33375U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001910 RID: 6416
			public static readonly StoreNamedPropInfo ApptTZDefRecur = new StoreNamedPropInfo("ApptTZDefRecur", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33376U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001911 RID: 6417
			public static readonly StoreNamedPropInfo SeriesCreationHash = new StoreNamedPropInfo("SeriesCreationHash", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "SeriesCreationHash"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001912 RID: 6418
			public static readonly StoreNamedPropInfo SeriesMasterId = new StoreNamedPropInfo("SeriesMasterId", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "SeriesMasterId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001913 RID: 6419
			public static readonly StoreNamedPropInfo InstanceCreationIndex = new StoreNamedPropInfo("InstanceCreationIndex", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "InstanceCreationIndex"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001914 RID: 6420
			public static readonly StoreNamedPropInfo EventClientId = new StoreNamedPropInfo("EventClientId", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "EventClientId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001915 RID: 6421
			public static readonly StoreNamedPropInfo SeriesId = new StoreNamedPropInfo("SeriesId", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "SeriesId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001916 RID: 6422
			public static readonly StoreNamedPropInfo SeriesReminderIsSet = new StoreNamedPropInfo("SeriesReminderIsSet", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "SeriesReminderIsSet"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001917 RID: 6423
			public static readonly StoreNamedPropInfo IsHiddenFromLegacyClients = new StoreNamedPropInfo("IsHiddenFromLegacyClients", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "IsHiddenFromLegacyClients"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001918 RID: 6424
			public static readonly StoreNamedPropInfo PropertyChangeMetadataRaw = new StoreNamedPropInfo("PropertyChangeMetadataRaw", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, "PropertyChangeMetadataRaw"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001919 RID: 6425
			public static readonly StoreNamedPropInfo ForwardNotificationRecipients = new StoreNamedPropInfo("ForwardNotificationRecipients", new StorePropName(NamedPropInfo.Appointment.NamespaceGuid, 33377U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200001C RID: 28
		public static class Task
		{
			// Token: 0x0400191A RID: 6426
			public static readonly Guid NamespaceGuid = new Guid("00062003-0000-0000-C000-000000000046");

			// Token: 0x0400191B RID: 6427
			public static readonly StoreNamedPropInfo TaskStatus = new StoreNamedPropInfo("TaskStatus", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33025U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400191C RID: 6428
			public static readonly StoreNamedPropInfo PercentComplete = new StoreNamedPropInfo("PercentComplete", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33026U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400191D RID: 6429
			public static readonly StoreNamedPropInfo TeamTask = new StoreNamedPropInfo("TeamTask", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33027U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400191E RID: 6430
			public static readonly StoreNamedPropInfo TaskStartDate = new StoreNamedPropInfo("TaskStartDate", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33028U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400191F RID: 6431
			public static readonly StoreNamedPropInfo TaskDueDate = new StoreNamedPropInfo("TaskDueDate", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33029U), PropertyType.SysTime, 64UL, default(PropertyCategories));

			// Token: 0x04001920 RID: 6432
			public static readonly StoreNamedPropInfo TaskDuration = new StoreNamedPropInfo("TaskDuration", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33030U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001921 RID: 6433
			public static readonly StoreNamedPropInfo TaskResetReminder = new StoreNamedPropInfo("TaskResetReminder", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33031U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001922 RID: 6434
			public static readonly StoreNamedPropInfo TaskAccepted = new StoreNamedPropInfo("TaskAccepted", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33032U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001923 RID: 6435
			public static readonly StoreNamedPropInfo TaskDeadOccur = new StoreNamedPropInfo("TaskDeadOccur", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33033U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001924 RID: 6436
			public static readonly StoreNamedPropInfo IntegerTest = new StoreNamedPropInfo("IntegerTest", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33037U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001925 RID: 6437
			public static readonly StoreNamedPropInfo FloatTest = new StoreNamedPropInfo("FloatTest", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33038U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001926 RID: 6438
			public static readonly StoreNamedPropInfo TaskDateCompleted = new StoreNamedPropInfo("TaskDateCompleted", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33039U), PropertyType.SysTime, 64UL, default(PropertyCategories));

			// Token: 0x04001927 RID: 6439
			public static readonly StoreNamedPropInfo TaskActualEffort = new StoreNamedPropInfo("TaskActualEffort", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33040U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001928 RID: 6440
			public static readonly StoreNamedPropInfo TaskEstimatedEffort = new StoreNamedPropInfo("TaskEstimatedEffort", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33041U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001929 RID: 6441
			public static readonly StoreNamedPropInfo TaskVersion = new StoreNamedPropInfo("TaskVersion", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33042U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400192A RID: 6442
			public static readonly StoreNamedPropInfo TaskState = new StoreNamedPropInfo("TaskState", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33043U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400192B RID: 6443
			public static readonly StoreNamedPropInfo TaskLastUpdate = new StoreNamedPropInfo("TaskLastUpdate", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33045U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400192C RID: 6444
			public static readonly StoreNamedPropInfo TaskRecur = new StoreNamedPropInfo("TaskRecur", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33046U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400192D RID: 6445
			public static readonly StoreNamedPropInfo TaskMyDelegators = new StoreNamedPropInfo("TaskMyDelegators", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33047U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400192E RID: 6446
			public static readonly StoreNamedPropInfo TaskSOC = new StoreNamedPropInfo("TaskSOC", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33049U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400192F RID: 6447
			public static readonly StoreNamedPropInfo TaskHistory = new StoreNamedPropInfo("TaskHistory", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33050U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001930 RID: 6448
			public static readonly StoreNamedPropInfo TaskUpdates = new StoreNamedPropInfo("TaskUpdates", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33051U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001931 RID: 6449
			public static readonly StoreNamedPropInfo TaskComplete = new StoreNamedPropInfo("TaskComplete", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33052U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001932 RID: 6450
			public static readonly StoreNamedPropInfo TaskOriginalRecurring = new StoreNamedPropInfo("TaskOriginalRecurring", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33053U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001933 RID: 6451
			public static readonly StoreNamedPropInfo TaskFCreator = new StoreNamedPropInfo("TaskFCreator", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33054U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001934 RID: 6452
			public static readonly StoreNamedPropInfo TaskOwner = new StoreNamedPropInfo("TaskOwner", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33055U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001935 RID: 6453
			public static readonly StoreNamedPropInfo TaskMultRecips = new StoreNamedPropInfo("TaskMultRecips", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33056U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001936 RID: 6454
			public static readonly StoreNamedPropInfo TaskDelegator = new StoreNamedPropInfo("TaskDelegator", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33057U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001937 RID: 6455
			public static readonly StoreNamedPropInfo TaskLastUser = new StoreNamedPropInfo("TaskLastUser", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33058U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001938 RID: 6456
			public static readonly StoreNamedPropInfo TaskOrdinal = new StoreNamedPropInfo("TaskOrdinal", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33059U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001939 RID: 6457
			public static readonly StoreNamedPropInfo TaskNoCompute = new StoreNamedPropInfo("TaskNoCompute", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33060U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400193A RID: 6458
			public static readonly StoreNamedPropInfo TaskLastDelegate = new StoreNamedPropInfo("TaskLastDelegate", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33061U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400193B RID: 6459
			public static readonly StoreNamedPropInfo TaskFRecur = new StoreNamedPropInfo("TaskFRecur", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33062U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400193C RID: 6460
			public static readonly StoreNamedPropInfo TaskRole = new StoreNamedPropInfo("TaskRole", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33063U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400193D RID: 6461
			public static readonly StoreNamedPropInfo TaskOwnership = new StoreNamedPropInfo("TaskOwnership", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33065U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400193E RID: 6462
			public static readonly StoreNamedPropInfo TaskDelegValue = new StoreNamedPropInfo("TaskDelegValue", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33066U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400193F RID: 6463
			public static readonly StoreNamedPropInfo TaskCardData = new StoreNamedPropInfo("TaskCardData", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33067U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001940 RID: 6464
			public static readonly StoreNamedPropInfo TaskFFixOffline = new StoreNamedPropInfo("TaskFFixOffline", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33068U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001941 RID: 6465
			public static readonly StoreNamedPropInfo TaskSchdPrio = new StoreNamedPropInfo("TaskSchdPrio", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33071U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001942 RID: 6466
			public static readonly StoreNamedPropInfo TaskFormURN = new StoreNamedPropInfo("TaskFormURN", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33074U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001943 RID: 6467
			public static readonly StoreNamedPropInfo TaskWebUrl = new StoreNamedPropInfo("TaskWebUrl", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33076U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001944 RID: 6468
			public static readonly StoreNamedPropInfo TaskCustomStatus = new StoreNamedPropInfo("TaskCustomStatus", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33079U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001945 RID: 6469
			public static readonly StoreNamedPropInfo TaskCustomPriority = new StoreNamedPropInfo("TaskCustomPriority", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33080U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001946 RID: 6470
			public static readonly StoreNamedPropInfo TaskCustomFlags = new StoreNamedPropInfo("TaskCustomFlags", new StorePropName(NamedPropInfo.Task.NamespaceGuid, 33081U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001947 RID: 6471
			public static readonly StoreNamedPropInfo DoItTime = new StoreNamedPropInfo("DoItTime", new StorePropName(NamedPropInfo.Task.NamespaceGuid, "DoItTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200001D RID: 29
		public static class Address
		{
			// Token: 0x04001948 RID: 6472
			public static readonly Guid NamespaceGuid = new Guid("00062004-0000-0000-C000-000000000046");

			// Token: 0x04001949 RID: 6473
			public static readonly StoreNamedPropInfo FSendPlainText = new StoreNamedPropInfo("FSendPlainText", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32769U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400194A RID: 6474
			public static readonly StoreNamedPropInfo FPostalAddress = new StoreNamedPropInfo("FPostalAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32770U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400194B RID: 6475
			public static readonly StoreNamedPropInfo FileUnder = new StoreNamedPropInfo("FileUnder", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32773U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x0400194C RID: 6476
			public static readonly StoreNamedPropInfo FileUnderId = new StoreNamedPropInfo("FileUnderId", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32774U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400194D RID: 6477
			public static readonly StoreNamedPropInfo ContactItemData = new StoreNamedPropInfo("ContactItemData", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32775U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400194E RID: 6478
			public static readonly StoreNamedPropInfo SelectedEmailAddress = new StoreNamedPropInfo("SelectedEmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32776U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400194F RID: 6479
			public static readonly StoreNamedPropInfo SelectedOriginalDisplayName = new StoreNamedPropInfo("SelectedOriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32777U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001950 RID: 6480
			public static readonly StoreNamedPropInfo SelectedOriginalEntryID = new StoreNamedPropInfo("SelectedOriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32778U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001951 RID: 6481
			public static readonly StoreNamedPropInfo ContactItemData2 = new StoreNamedPropInfo("ContactItemData2", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32779U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001952 RID: 6482
			public static readonly StoreNamedPropInfo ChildrenStr = new StoreNamedPropInfo("ChildrenStr", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32780U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001953 RID: 6483
			public static readonly StoreNamedPropInfo ReferredBy = new StoreNamedPropInfo("ReferredBy", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32782U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001954 RID: 6484
			public static readonly StoreNamedPropInfo Department = new StoreNamedPropInfo("Department", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32784U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001955 RID: 6485
			public static readonly StoreNamedPropInfo HasPicture = new StoreNamedPropInfo("HasPicture", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32789U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001956 RID: 6486
			public static readonly StoreNamedPropInfo UserCertificateStr = new StoreNamedPropInfo("UserCertificateStr", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32790U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001957 RID: 6487
			public static readonly StoreNamedPropInfo LastNameAndFirstName = new StoreNamedPropInfo("LastNameAndFirstName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32791U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001958 RID: 6488
			public static readonly StoreNamedPropInfo CompanyAndFullName = new StoreNamedPropInfo("CompanyAndFullName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32792U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001959 RID: 6489
			public static readonly StoreNamedPropInfo FullNameAndCompany = new StoreNamedPropInfo("FullNameAndCompany", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32793U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400195A RID: 6490
			public static readonly StoreNamedPropInfo HomeAddress = new StoreNamedPropInfo("HomeAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32794U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x0400195B RID: 6491
			public static readonly StoreNamedPropInfo WorkAddress = new StoreNamedPropInfo("WorkAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32795U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x0400195C RID: 6492
			public static readonly StoreNamedPropInfo OtherAddress = new StoreNamedPropInfo("OtherAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32796U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x0400195D RID: 6493
			public static readonly StoreNamedPropInfo PostalAddressId = new StoreNamedPropInfo("PostalAddressId", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32802U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400195E RID: 6494
			public static readonly StoreNamedPropInfo ContactCharSet = new StoreNamedPropInfo("ContactCharSet", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32803U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400195F RID: 6495
			public static readonly StoreNamedPropInfo AutoLog = new StoreNamedPropInfo("AutoLog", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32805U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001960 RID: 6496
			public static readonly StoreNamedPropInfo FileUnderList = new StoreNamedPropInfo("FileUnderList", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32806U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001961 RID: 6497
			public static readonly StoreNamedPropInfo EmailList = new StoreNamedPropInfo("EmailList", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32807U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001962 RID: 6498
			public static readonly StoreNamedPropInfo ABPEmailList = new StoreNamedPropInfo("ABPEmailList", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32808U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001963 RID: 6499
			public static readonly StoreNamedPropInfo ABPArrayType = new StoreNamedPropInfo("ABPArrayType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32809U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001964 RID: 6500
			public static readonly StoreNamedPropInfo DontAgeLog = new StoreNamedPropInfo("DontAgeLog", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32810U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001965 RID: 6501
			public static readonly StoreNamedPropInfo HTML = new StoreNamedPropInfo("HTML", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32811U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001966 RID: 6502
			public static readonly StoreNamedPropInfo YomiFirstName = new StoreNamedPropInfo("YomiFirstName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32812U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001967 RID: 6503
			public static readonly StoreNamedPropInfo YomiLastName = new StoreNamedPropInfo("YomiLastName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32813U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001968 RID: 6504
			public static readonly StoreNamedPropInfo YomiCompanyName = new StoreNamedPropInfo("YomiCompanyName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32814U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001969 RID: 6505
			public static readonly StoreNamedPropInfo LastFirstNoSpace = new StoreNamedPropInfo("LastFirstNoSpace", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32816U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400196A RID: 6506
			public static readonly StoreNamedPropInfo LastFirstSpaceOnly = new StoreNamedPropInfo("LastFirstSpaceOnly", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32817U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400196B RID: 6507
			public static readonly StoreNamedPropInfo CompanyLastFirstNoSpace = new StoreNamedPropInfo("CompanyLastFirstNoSpace", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32818U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400196C RID: 6508
			public static readonly StoreNamedPropInfo CompanyLastFirstSpaceOnly = new StoreNamedPropInfo("CompanyLastFirstSpaceOnly", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32819U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400196D RID: 6509
			public static readonly StoreNamedPropInfo LastFirstNoSpaceCompany = new StoreNamedPropInfo("LastFirstNoSpaceCompany", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32820U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400196E RID: 6510
			public static readonly StoreNamedPropInfo LastFirstSpaceOnlyCompany = new StoreNamedPropInfo("LastFirstSpaceOnlyCompany", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32821U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400196F RID: 6511
			public static readonly StoreNamedPropInfo LastFirstAndSuffix = new StoreNamedPropInfo("LastFirstAndSuffix", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32822U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001970 RID: 6512
			public static readonly StoreNamedPropInfo FirstMiddleLastSuffix = new StoreNamedPropInfo("FirstMiddleLastSuffix", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32823U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001971 RID: 6513
			public static readonly StoreNamedPropInfo LastFirstNoSpaceAndSuffix = new StoreNamedPropInfo("LastFirstNoSpaceAndSuffix", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32824U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001972 RID: 6514
			public static readonly StoreNamedPropInfo BCDisplayDefinition = new StoreNamedPropInfo("BCDisplayDefinition", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32832U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001973 RID: 6515
			public static readonly StoreNamedPropInfo BCCardPicture = new StoreNamedPropInfo("BCCardPicture", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32833U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001974 RID: 6516
			public static readonly StoreNamedPropInfo InterConnectBizcard = new StoreNamedPropInfo("InterConnectBizcard", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32834U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001975 RID: 6517
			public static readonly StoreNamedPropInfo InterConnectBizcardFlag = new StoreNamedPropInfo("InterConnectBizcardFlag", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32835U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001976 RID: 6518
			public static readonly StoreNamedPropInfo InterConnectBizcardLastUpdate = new StoreNamedPropInfo("InterConnectBizcardLastUpdate", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32836U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001977 RID: 6519
			public static readonly StoreNamedPropInfo WorkAddressStreet = new StoreNamedPropInfo("WorkAddressStreet", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32837U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001978 RID: 6520
			public static readonly StoreNamedPropInfo WorkAddressCity = new StoreNamedPropInfo("WorkAddressCity", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32838U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001979 RID: 6521
			public static readonly StoreNamedPropInfo WorkAddressState = new StoreNamedPropInfo("WorkAddressState", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32839U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x0400197A RID: 6522
			public static readonly StoreNamedPropInfo WorkAddressPostalCode = new StoreNamedPropInfo("WorkAddressPostalCode", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32840U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x0400197B RID: 6523
			public static readonly StoreNamedPropInfo WorkAddressCountry = new StoreNamedPropInfo("WorkAddressCountry", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32841U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x0400197C RID: 6524
			public static readonly StoreNamedPropInfo WorkAddressPostOfficeBox = new StoreNamedPropInfo("WorkAddressPostOfficeBox", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32842U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400197D RID: 6525
			public static readonly StoreNamedPropInfo DLCountMembers = new StoreNamedPropInfo("DLCountMembers", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32843U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400197E RID: 6526
			public static readonly StoreNamedPropInfo DLChecksum = new StoreNamedPropInfo("DLChecksum", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32844U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400197F RID: 6527
			public static readonly StoreNamedPropInfo BirthdayEventEID = new StoreNamedPropInfo("BirthdayEventEID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32845U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001980 RID: 6528
			public static readonly StoreNamedPropInfo AnniversaryEventEID = new StoreNamedPropInfo("AnniversaryEventEID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32846U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001981 RID: 6529
			public static readonly StoreNamedPropInfo ContactUserField1 = new StoreNamedPropInfo("ContactUserField1", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32847U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001982 RID: 6530
			public static readonly StoreNamedPropInfo ContactUserField2 = new StoreNamedPropInfo("ContactUserField2", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32848U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001983 RID: 6531
			public static readonly StoreNamedPropInfo ContactUserField3 = new StoreNamedPropInfo("ContactUserField3", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32849U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001984 RID: 6532
			public static readonly StoreNamedPropInfo ContactUserField4 = new StoreNamedPropInfo("ContactUserField4", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32850U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001985 RID: 6533
			public static readonly StoreNamedPropInfo DLName = new StoreNamedPropInfo("DLName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32851U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001986 RID: 6534
			public static readonly StoreNamedPropInfo DLOneOffMembers = new StoreNamedPropInfo("DLOneOffMembers", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32852U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001987 RID: 6535
			public static readonly StoreNamedPropInfo DLMembers = new StoreNamedPropInfo("DLMembers", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32853U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001988 RID: 6536
			public static readonly StoreNamedPropInfo ConfServerNames = new StoreNamedPropInfo("ConfServerNames", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32854U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001989 RID: 6537
			public static readonly StoreNamedPropInfo ConfDefServerIndex = new StoreNamedPropInfo("ConfDefServerIndex", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32855U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400198A RID: 6538
			public static readonly StoreNamedPropInfo ConfBackupServerIndex = new StoreNamedPropInfo("ConfBackupServerIndex", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32856U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400198B RID: 6539
			public static readonly StoreNamedPropInfo ConfEmailIndex = new StoreNamedPropInfo("ConfEmailIndex", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32857U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400198C RID: 6540
			public static readonly StoreNamedPropInfo MoreAddressType = new StoreNamedPropInfo("MoreAddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32859U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400198D RID: 6541
			public static readonly StoreNamedPropInfo MoreEmailAddress = new StoreNamedPropInfo("MoreEmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32860U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400198E RID: 6542
			public static readonly StoreNamedPropInfo ContactEmailAddressesStr = new StoreNamedPropInfo("ContactEmailAddressesStr", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32861U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400198F RID: 6543
			public static readonly StoreNamedPropInfo ConfServerNamesStr = new StoreNamedPropInfo("ConfServerNamesStr", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32862U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001990 RID: 6544
			public static readonly StoreNamedPropInfo ConfAliasDisplay = new StoreNamedPropInfo("ConfAliasDisplay", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32863U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001991 RID: 6545
			public static readonly StoreNamedPropInfo ConfServerDisplay = new StoreNamedPropInfo("ConfServerDisplay", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32864U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001992 RID: 6546
			public static readonly StoreNamedPropInfo MeFlag = new StoreNamedPropInfo("MeFlag", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32865U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001993 RID: 6547
			public static readonly StoreNamedPropInfo InstMsg = new StoreNamedPropInfo("InstMsg", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32866U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001994 RID: 6548
			public static readonly StoreNamedPropInfo NetMeetingOrganizerAlias = new StoreNamedPropInfo("NetMeetingOrganizerAlias", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32867U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001995 RID: 6549
			public static readonly StoreNamedPropInfo AddressSelection = new StoreNamedPropInfo("AddressSelection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32872U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001996 RID: 6550
			public static readonly StoreNamedPropInfo EmailSelection = new StoreNamedPropInfo("EmailSelection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32873U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001997 RID: 6551
			public static readonly StoreNamedPropInfo Phone1Selection = new StoreNamedPropInfo("Phone1Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32874U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001998 RID: 6552
			public static readonly StoreNamedPropInfo Phone2Selection = new StoreNamedPropInfo("Phone2Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32875U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001999 RID: 6553
			public static readonly StoreNamedPropInfo Phone3Selection = new StoreNamedPropInfo("Phone3Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32876U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400199A RID: 6554
			public static readonly StoreNamedPropInfo Phone4Selection = new StoreNamedPropInfo("Phone4Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32877U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400199B RID: 6555
			public static readonly StoreNamedPropInfo Phone5Selection = new StoreNamedPropInfo("Phone5Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32878U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400199C RID: 6556
			public static readonly StoreNamedPropInfo Phone6Selection = new StoreNamedPropInfo("Phone6Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32879U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400199D RID: 6557
			public static readonly StoreNamedPropInfo Phone7Selection = new StoreNamedPropInfo("Phone7Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32880U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400199E RID: 6558
			public static readonly StoreNamedPropInfo Phone8Selection = new StoreNamedPropInfo("Phone8Selection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32881U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x0400199F RID: 6559
			public static readonly StoreNamedPropInfo SelectedAddress = new StoreNamedPropInfo("SelectedAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32884U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A0 RID: 6560
			public static readonly StoreNamedPropInfo SelectedPhone1 = new StoreNamedPropInfo("SelectedPhone1", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32886U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A1 RID: 6561
			public static readonly StoreNamedPropInfo SelectedPhone2 = new StoreNamedPropInfo("SelectedPhone2", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32887U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A2 RID: 6562
			public static readonly StoreNamedPropInfo EmailDisplayName = new StoreNamedPropInfo("EmailDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32896U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019A3 RID: 6563
			public static readonly StoreNamedPropInfo EmailEntryID = new StoreNamedPropInfo("EmailEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32897U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A4 RID: 6564
			public static readonly StoreNamedPropInfo EmailAddressType = new StoreNamedPropInfo("EmailAddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32898U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A5 RID: 6565
			public static readonly StoreNamedPropInfo EmailEmailAddress = new StoreNamedPropInfo("EmailEmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32899U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019A6 RID: 6566
			public static readonly StoreNamedPropInfo EmailOriginalDisplayName = new StoreNamedPropInfo("EmailOriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32900U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019A7 RID: 6567
			public static readonly StoreNamedPropInfo EmailOriginalEntryID = new StoreNamedPropInfo("EmailOriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32901U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A8 RID: 6568
			public static readonly StoreNamedPropInfo Email1RTF = new StoreNamedPropInfo("Email1RTF", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32902U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019A9 RID: 6569
			public static readonly StoreNamedPropInfo EmailEmailType = new StoreNamedPropInfo("EmailEmailType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32903U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019AA RID: 6570
			public static readonly StoreNamedPropInfo SelectedPhone3 = new StoreNamedPropInfo("SelectedPhone3", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32904U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019AB RID: 6571
			public static readonly StoreNamedPropInfo SelectedPhone4 = new StoreNamedPropInfo("SelectedPhone4", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32905U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019AC RID: 6572
			public static readonly StoreNamedPropInfo SelectedPhone5 = new StoreNamedPropInfo("SelectedPhone5", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32906U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019AD RID: 6573
			public static readonly StoreNamedPropInfo SelectedPhone6 = new StoreNamedPropInfo("SelectedPhone6", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32907U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019AE RID: 6574
			public static readonly StoreNamedPropInfo SelectedPhone7 = new StoreNamedPropInfo("SelectedPhone7", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32908U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019AF RID: 6575
			public static readonly StoreNamedPropInfo SelectedPhone8 = new StoreNamedPropInfo("SelectedPhone8", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32909U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019B0 RID: 6576
			public static readonly StoreNamedPropInfo Email2DisplayName = new StoreNamedPropInfo("Email2DisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32912U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019B1 RID: 6577
			public static readonly StoreNamedPropInfo Email2EntryID = new StoreNamedPropInfo("Email2EntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32913U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019B2 RID: 6578
			public static readonly StoreNamedPropInfo Email2AddressType = new StoreNamedPropInfo("Email2AddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32914U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019B3 RID: 6579
			public static readonly StoreNamedPropInfo Email2EmailAddress = new StoreNamedPropInfo("Email2EmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32915U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019B4 RID: 6580
			public static readonly StoreNamedPropInfo Email2OriginalDisplayName = new StoreNamedPropInfo("Email2OriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32916U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019B5 RID: 6581
			public static readonly StoreNamedPropInfo Email2OriginalEntryID = new StoreNamedPropInfo("Email2OriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32917U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019B6 RID: 6582
			public static readonly StoreNamedPropInfo Email2RTF = new StoreNamedPropInfo("Email2RTF", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32918U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019B7 RID: 6583
			public static readonly StoreNamedPropInfo Email2EmailType = new StoreNamedPropInfo("Email2EmailType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32919U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019B8 RID: 6584
			public static readonly StoreNamedPropInfo Email3DisplayName = new StoreNamedPropInfo("Email3DisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32928U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019B9 RID: 6585
			public static readonly StoreNamedPropInfo Email3EntryID = new StoreNamedPropInfo("Email3EntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32929U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019BA RID: 6586
			public static readonly StoreNamedPropInfo Email3AddressType = new StoreNamedPropInfo("Email3AddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32930U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019BB RID: 6587
			public static readonly StoreNamedPropInfo Email3EmailAddress = new StoreNamedPropInfo("Email3EmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32931U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019BC RID: 6588
			public static readonly StoreNamedPropInfo Email3OriginalDisplayName = new StoreNamedPropInfo("Email3OriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32932U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x040019BD RID: 6589
			public static readonly StoreNamedPropInfo Email3OriginalEntryID = new StoreNamedPropInfo("Email3OriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32933U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019BE RID: 6590
			public static readonly StoreNamedPropInfo Email3RTF = new StoreNamedPropInfo("Email3RTF", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32934U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019BF RID: 6591
			public static readonly StoreNamedPropInfo Email3EmailType = new StoreNamedPropInfo("Email3EmailType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32935U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C0 RID: 6592
			public static readonly StoreNamedPropInfo Fax1DisplayName = new StoreNamedPropInfo("Fax1DisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32944U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C1 RID: 6593
			public static readonly StoreNamedPropInfo Fax1EntryID = new StoreNamedPropInfo("Fax1EntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32945U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C2 RID: 6594
			public static readonly StoreNamedPropInfo Fax1AddressType = new StoreNamedPropInfo("Fax1AddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32946U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C3 RID: 6595
			public static readonly StoreNamedPropInfo Fax1EmailAddress = new StoreNamedPropInfo("Fax1EmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32947U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C4 RID: 6596
			public static readonly StoreNamedPropInfo Fax1OriginalDisplayName = new StoreNamedPropInfo("Fax1OriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32948U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C5 RID: 6597
			public static readonly StoreNamedPropInfo Fax1OriginalEntryID = new StoreNamedPropInfo("Fax1OriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32949U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C6 RID: 6598
			public static readonly StoreNamedPropInfo Fax1RTF = new StoreNamedPropInfo("Fax1RTF", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32950U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C7 RID: 6599
			public static readonly StoreNamedPropInfo Fax1EmailType = new StoreNamedPropInfo("Fax1EmailType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32951U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C8 RID: 6600
			public static readonly StoreNamedPropInfo Fax2DisplayName = new StoreNamedPropInfo("Fax2DisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32960U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019C9 RID: 6601
			public static readonly StoreNamedPropInfo Fax2EntryID = new StoreNamedPropInfo("Fax2EntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32961U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019CA RID: 6602
			public static readonly StoreNamedPropInfo Fax2AddressType = new StoreNamedPropInfo("Fax2AddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32962U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019CB RID: 6603
			public static readonly StoreNamedPropInfo Fax2EmailAddress = new StoreNamedPropInfo("Fax2EmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32963U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019CC RID: 6604
			public static readonly StoreNamedPropInfo Fax2OriginalDisplayName = new StoreNamedPropInfo("Fax2OriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32964U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019CD RID: 6605
			public static readonly StoreNamedPropInfo Fax2OriginalEntryID = new StoreNamedPropInfo("Fax2OriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32965U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019CE RID: 6606
			public static readonly StoreNamedPropInfo Fax2RTF = new StoreNamedPropInfo("Fax2RTF", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32966U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019CF RID: 6607
			public static readonly StoreNamedPropInfo Fax2EmailType = new StoreNamedPropInfo("Fax2EmailType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32967U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D0 RID: 6608
			public static readonly StoreNamedPropInfo Fax3DisplayName = new StoreNamedPropInfo("Fax3DisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32976U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D1 RID: 6609
			public static readonly StoreNamedPropInfo Fax3EntryID = new StoreNamedPropInfo("Fax3EntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32977U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D2 RID: 6610
			public static readonly StoreNamedPropInfo Fax3AddressType = new StoreNamedPropInfo("Fax3AddressType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32978U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D3 RID: 6611
			public static readonly StoreNamedPropInfo Fax3EmailAddress = new StoreNamedPropInfo("Fax3EmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32979U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D4 RID: 6612
			public static readonly StoreNamedPropInfo Fax3OriginalDisplayName = new StoreNamedPropInfo("Fax3OriginalDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32980U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D5 RID: 6613
			public static readonly StoreNamedPropInfo Fax3OriginalEntryID = new StoreNamedPropInfo("Fax3OriginalEntryID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32981U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D6 RID: 6614
			public static readonly StoreNamedPropInfo Fax3RTF = new StoreNamedPropInfo("Fax3RTF", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32982U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D7 RID: 6615
			public static readonly StoreNamedPropInfo Fax3EmailType = new StoreNamedPropInfo("Fax3EmailType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32983U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D8 RID: 6616
			public static readonly StoreNamedPropInfo FreeBusyLocation = new StoreNamedPropInfo("FreeBusyLocation", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32984U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019D9 RID: 6617
			public static readonly StoreNamedPropInfo EMSAbX509Cert = new StoreNamedPropInfo("EMSAbX509Cert", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32985U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019DA RID: 6618
			public static readonly StoreNamedPropInfo HomeAddressCountryCode = new StoreNamedPropInfo("HomeAddressCountryCode", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32986U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019DB RID: 6619
			public static readonly StoreNamedPropInfo WorkAddressCountryCode = new StoreNamedPropInfo("WorkAddressCountryCode", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32987U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019DC RID: 6620
			public static readonly StoreNamedPropInfo OtherAddressCountryCode = new StoreNamedPropInfo("OtherAddressCountryCode", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32988U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019DD RID: 6621
			public static readonly StoreNamedPropInfo AddressCountryCode = new StoreNamedPropInfo("AddressCountryCode", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32989U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019DE RID: 6622
			public static readonly StoreNamedPropInfo AddressLinked = new StoreNamedPropInfo("AddressLinked", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32992U), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019DF RID: 6623
			public static readonly StoreNamedPropInfo AddressBookEntryId = new StoreNamedPropInfo("AddressBookEntryId", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32994U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E0 RID: 6624
			public static readonly StoreNamedPropInfo SmtpAddressCache = new StoreNamedPropInfo("SmtpAddressCache", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32995U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E1 RID: 6625
			public static readonly StoreNamedPropInfo LinkRejectHistoryRaw = new StoreNamedPropInfo("LinkRejectHistoryRaw", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32997U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E2 RID: 6626
			public static readonly StoreNamedPropInfo GALLinkState = new StoreNamedPropInfo("GALLinkState", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32998U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E3 RID: 6627
			public static readonly StoreNamedPropInfo ContactGALLinkID = new StoreNamedPropInfo("ContactGALLinkID", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33000U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E4 RID: 6628
			public static readonly StoreNamedPropInfo Schools = new StoreNamedPropInfo("Schools", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33001U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E5 RID: 6629
			public static readonly StoreNamedPropInfo InternalPersonType = new StoreNamedPropInfo("InternalPersonType", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33002U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E6 RID: 6630
			public static readonly StoreNamedPropInfo UserApprovedLink = new StoreNamedPropInfo("UserApprovedLink", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33003U), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E7 RID: 6631
			public static readonly StoreNamedPropInfo DisplayNameFirstLast = new StoreNamedPropInfo("DisplayNameFirstLast", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "DisplayNameFirstLast"), PropertyType.Unicode, 11529215046068469760UL, default(PropertyCategories));

			// Token: 0x040019E8 RID: 6632
			public static readonly StoreNamedPropInfo DisplayNameLastFirst = new StoreNamedPropInfo("DisplayNameLastFirst", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "DisplayNameLastFirst"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019E9 RID: 6633
			public static readonly StoreNamedPropInfo DisplayNamePriority = new StoreNamedPropInfo("DisplayNamePriority", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "DisplayNamePriority"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019EA RID: 6634
			public static readonly StoreNamedPropInfo ContactOtherPhone2 = new StoreNamedPropInfo("ContactOtherPhone2", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "ContactOtherPhone2"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019EB RID: 6635
			public static readonly StoreNamedPropInfo MobileTelephoneNumber2 = new StoreNamedPropInfo("MobileTelephoneNumber2", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "ContactMobilePhone2"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019EC RID: 6636
			public static readonly StoreNamedPropInfo BirthdayLocal = new StoreNamedPropInfo("BirthdayLocal", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32990U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019ED RID: 6637
			public static readonly StoreNamedPropInfo WeddingAnniversaryLocal = new StoreNamedPropInfo("WeddingAnniversaryLocal", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 32991U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019EE RID: 6638
			public static readonly StoreNamedPropInfo ProtectedEmailAddress = new StoreNamedPropInfo("ProtectedEmailAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33008U), PropertyType.Unicode, 9223372036854775808UL, new PropertyCategories(0, 1, 2, 8));

			// Token: 0x040019EF RID: 6639
			public static readonly StoreNamedPropInfo ProtectedPhoneNumber = new StoreNamedPropInfo("ProtectedPhoneNumber", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33010U), PropertyType.Unicode, 9223372036854775808UL, new PropertyCategories(0, 1, 2, 8));

			// Token: 0x040019F0 RID: 6640
			public static readonly StoreNamedPropInfo NetMeetingDocPathName = new StoreNamedPropInfo("NetMeetingDocPathName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33345U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F1 RID: 6641
			public static readonly StoreNamedPropInfo NetMeetingConferenceServerAllowExternal = new StoreNamedPropInfo("NetMeetingConferenceServerAllowExternal", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33350U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F2 RID: 6642
			public static readonly StoreNamedPropInfo NetMeetingConferenceSerPassword = new StoreNamedPropInfo("NetMeetingConferenceSerPassword", new StorePropName(NamedPropInfo.Address.NamespaceGuid, 33353U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F3 RID: 6643
			public static readonly StoreNamedPropInfo ImContactSipUriAddress = new StoreNamedPropInfo("ImContactSipUriAddress", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "ImContactSipUriAddress"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F4 RID: 6644
			public static readonly StoreNamedPropInfo IsFavorite = new StoreNamedPropInfo("IsFavorite", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "IsFavorite"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F5 RID: 6645
			public static readonly StoreNamedPropInfo BirthdayContactAttributionDisplayName = new StoreNamedPropInfo("BirthdayContactAttributionDisplayName", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "BirthdayContactAttributionDisplayName"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F6 RID: 6646
			public static readonly StoreNamedPropInfo BirthdayContactEntryId = new StoreNamedPropInfo("BirthdayContactEntryId", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "BirthdayContactEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F7 RID: 6647
			public static readonly StoreNamedPropInfo IsBirthdayContactWritable = new StoreNamedPropInfo("IsBirthdayContactWritable", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "IsBirthdayContactWritable"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F8 RID: 6648
			public static readonly StoreNamedPropInfo MyContactsFolderEntryId = new StoreNamedPropInfo("MyContactsFolderEntryId", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "MyContactsFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019F9 RID: 6649
			public static readonly StoreNamedPropInfo MyContactsExtendedFolderEntryId = new StoreNamedPropInfo("MyContactsExtendedFolderEntryId", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "MyContactsExtendedFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019FA RID: 6650
			public static readonly StoreNamedPropInfo MyContactsFolders = new StoreNamedPropInfo("MyContactsFolders", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "MyContactsFolders"), PropertyType.MVBinary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019FB RID: 6651
			public static readonly StoreNamedPropInfo LinkChangeHistory = new StoreNamedPropInfo("LinkChangeHistory", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "LinkChangeHistory"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019FC RID: 6652
			public static readonly StoreNamedPropInfo PeopleIKnowEmailAddressCollection = new StoreNamedPropInfo("PeopleIKnowEmailAddressCollection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "PeopleIKnowEmailAddressCollection"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019FD RID: 6653
			public static readonly StoreNamedPropInfo PeopleIKnowEmailAddressRelevanceScoreCollection = new StoreNamedPropInfo("PeopleIKnowEmailAddressRelevanceScoreCollection", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "PeopleIKnowEmailAddressRelevanceScoreCollection"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x040019FE RID: 6654
			public static readonly StoreNamedPropInfo SenderRelevanceScore = new StoreNamedPropInfo("SenderRelevanceScore", new StorePropName(NamedPropInfo.Address.NamespaceGuid, "SenderRelevanceScore"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200001E RID: 30
		public static class Recipient
		{
			// Token: 0x040019FF RID: 6655
			public static readonly Guid NamespaceGuid = new Guid("00062005-0000-0000-C000-000000000046");
		}

		// Token: 0x0200001F RID: 31
		public static class Delegation
		{
			// Token: 0x04001A00 RID: 6656
			public static readonly Guid NamespaceGuid = new Guid("00062006-0000-0000-C000-000000000046");
		}

		// Token: 0x02000020 RID: 32
		public static class Common
		{
			// Token: 0x04001A01 RID: 6657
			public static readonly Guid NamespaceGuid = new Guid("00062008-0000-0000-C000-000000000046");

			// Token: 0x04001A02 RID: 6658
			public static readonly StoreNamedPropInfo DayOfMonth = new StoreNamedPropInfo("DayOfMonth", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4096U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A03 RID: 6659
			public static readonly StoreNamedPropInfo DayOfWeekMask = new StoreNamedPropInfo("DayOfWeekMask", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4097U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A04 RID: 6660
			public static readonly StoreNamedPropInfo EndRecurDate = new StoreNamedPropInfo("EndRecurDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4098U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A05 RID: 6661
			public static readonly StoreNamedPropInfo Instance = new StoreNamedPropInfo("Instance", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4099U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A06 RID: 6662
			public static readonly StoreNamedPropInfo Interval = new StoreNamedPropInfo("Interval", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4100U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A07 RID: 6663
			public static readonly StoreNamedPropInfo Occurrences = new StoreNamedPropInfo("Occurrences", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4101U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A08 RID: 6664
			public static readonly StoreNamedPropInfo MonthOfYear = new StoreNamedPropInfo("MonthOfYear", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4102U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A09 RID: 6665
			public static readonly StoreNamedPropInfo RecurrenceType = new StoreNamedPropInfo("RecurrenceType", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4103U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A0A RID: 6666
			public static readonly StoreNamedPropInfo StartRecurDate = new StoreNamedPropInfo("StartRecurDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4104U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A0B RID: 6667
			public static readonly StoreNamedPropInfo FSliding = new StoreNamedPropInfo("FSliding", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4106U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A0C RID: 6668
			public static readonly StoreNamedPropInfo FNoEndDate = new StoreNamedPropInfo("FNoEndDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4107U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A0D RID: 6669
			public static readonly StoreNamedPropInfo EndRecurTime = new StoreNamedPropInfo("EndRecurTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4108U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A0E RID: 6670
			public static readonly StoreNamedPropInfo RecurDuration = new StoreNamedPropInfo("RecurDuration", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4109U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A0F RID: 6671
			public static readonly StoreNamedPropInfo Exceptions = new StoreNamedPropInfo("Exceptions", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4110U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A10 RID: 6672
			public static readonly StoreNamedPropInfo FEndByOcc = new StoreNamedPropInfo("FEndByOcc", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 4111U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A11 RID: 6673
			public static readonly StoreNamedPropInfo ApptStickerID = new StoreNamedPropInfo("ApptStickerID", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 33305U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A12 RID: 6674
			public static readonly StoreNamedPropInfo ApptExtractVersion = new StoreNamedPropInfo("ApptExtractVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 33324U), PropertyType.Int32, 32768UL, default(PropertyCategories));

			// Token: 0x04001A13 RID: 6675
			public static readonly StoreNamedPropInfo ApptExtractTime = new StoreNamedPropInfo("ApptExtractTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 33325U), PropertyType.SysTime, 32768UL, default(PropertyCategories));

			// Token: 0x04001A14 RID: 6676
			public static readonly StoreNamedPropInfo ReminderDelta = new StoreNamedPropInfo("ReminderDelta", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34049U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A15 RID: 6677
			public static readonly StoreNamedPropInfo ReminderTime = new StoreNamedPropInfo("ReminderTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34050U), PropertyType.SysTime, 4611686018427387968UL, default(PropertyCategories));

			// Token: 0x04001A16 RID: 6678
			public static readonly StoreNamedPropInfo ReminderSet = new StoreNamedPropInfo("ReminderSet", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34051U), PropertyType.Boolean, 4611686018427387968UL, default(PropertyCategories));

			// Token: 0x04001A17 RID: 6679
			public static readonly StoreNamedPropInfo ReminderTimeTime = new StoreNamedPropInfo("ReminderTimeTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34052U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A18 RID: 6680
			public static readonly StoreNamedPropInfo ReminderTimeDate = new StoreNamedPropInfo("ReminderTimeDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34053U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A19 RID: 6681
			public static readonly StoreNamedPropInfo Private = new StoreNamedPropInfo("Private", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34054U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A1A RID: 6682
			public static readonly StoreNamedPropInfo AgingDontAgeMe = new StoreNamedPropInfo("AgingDontAgeMe", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34062U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A1B RID: 6683
			public static readonly StoreNamedPropInfo FormStorage = new StoreNamedPropInfo("FormStorage", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34063U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A1C RID: 6684
			public static readonly StoreNamedPropInfo SideEffects = new StoreNamedPropInfo("SideEffects", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34064U), PropertyType.Int32, 4UL, default(PropertyCategories));

			// Token: 0x04001A1D RID: 6685
			public static readonly StoreNamedPropInfo RemoteStatus = new StoreNamedPropInfo("RemoteStatus", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34065U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A1E RID: 6686
			public static readonly StoreNamedPropInfo PageDirStream = new StoreNamedPropInfo("PageDirStream", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34067U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A1F RID: 6687
			public static readonly StoreNamedPropInfo SmartNoAttach = new StoreNamedPropInfo("SmartNoAttach", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34068U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A20 RID: 6688
			public static readonly StoreNamedPropInfo CustomPages = new StoreNamedPropInfo("CustomPages", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34069U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A21 RID: 6689
			public static readonly StoreNamedPropInfo CommonStart = new StoreNamedPropInfo("CommonStart", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34070U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A22 RID: 6690
			public static readonly StoreNamedPropInfo CommonEnd = new StoreNamedPropInfo("CommonEnd", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34071U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A23 RID: 6691
			public static readonly StoreNamedPropInfo TaskMode = new StoreNamedPropInfo("TaskMode", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34072U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A24 RID: 6692
			public static readonly StoreNamedPropInfo TaskGlobalObjId = new StoreNamedPropInfo("TaskGlobalObjId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34073U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A25 RID: 6693
			public static readonly StoreNamedPropInfo SniffState = new StoreNamedPropInfo("SniffState", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34074U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A26 RID: 6694
			public static readonly StoreNamedPropInfo FormPropStream = new StoreNamedPropInfo("FormPropStream", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34075U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A27 RID: 6695
			public static readonly StoreNamedPropInfo ReminderOverride = new StoreNamedPropInfo("ReminderOverride", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34076U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A28 RID: 6696
			public static readonly StoreNamedPropInfo ReminderType = new StoreNamedPropInfo("ReminderType", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34077U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A29 RID: 6697
			public static readonly StoreNamedPropInfo ReminderPlaySound = new StoreNamedPropInfo("ReminderPlaySound", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34078U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A2A RID: 6698
			public static readonly StoreNamedPropInfo ReminderFileParam = new StoreNamedPropInfo("ReminderFileParam", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34079U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A2B RID: 6699
			public static readonly StoreNamedPropInfo VerbStream = new StoreNamedPropInfo("VerbStream", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34080U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A2C RID: 6700
			public static readonly StoreNamedPropInfo VerbResponse = new StoreNamedPropInfo("VerbResponse", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34084U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A2D RID: 6701
			public static readonly StoreNamedPropInfo Request = new StoreNamedPropInfo("Request", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34096U), PropertyType.Unicode, 512UL, default(PropertyCategories));

			// Token: 0x04001A2E RID: 6702
			public static readonly StoreNamedPropInfo Mileage = new StoreNamedPropInfo("Mileage", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34100U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A2F RID: 6703
			public static readonly StoreNamedPropInfo Billing = new StoreNamedPropInfo("Billing", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34101U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001A30 RID: 6704
			public static readonly StoreNamedPropInfo NonSendableTo = new StoreNamedPropInfo("NonSendableTo", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34102U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A31 RID: 6705
			public static readonly StoreNamedPropInfo NonSendableCC = new StoreNamedPropInfo("NonSendableCC", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34103U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A32 RID: 6706
			public static readonly StoreNamedPropInfo NonSendableBCC = new StoreNamedPropInfo("NonSendableBCC", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34104U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A33 RID: 6707
			public static readonly StoreNamedPropInfo Companies = new StoreNamedPropInfo("Companies", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34105U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001A34 RID: 6708
			public static readonly StoreNamedPropInfo Contacts = new StoreNamedPropInfo("Contacts", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34106U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001A35 RID: 6709
			public static readonly StoreNamedPropInfo CompaniesStr = new StoreNamedPropInfo("CompaniesStr", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34107U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A36 RID: 6710
			public static readonly StoreNamedPropInfo ContactsStr = new StoreNamedPropInfo("ContactsStr", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34108U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A37 RID: 6711
			public static readonly StoreNamedPropInfo Url = new StoreNamedPropInfo("Url", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34109U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A38 RID: 6712
			public static readonly StoreNamedPropInfo HtmlForm = new StoreNamedPropInfo("HtmlForm", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34110U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A39 RID: 6713
			public static readonly StoreNamedPropInfo PropDefStream = new StoreNamedPropInfo("PropDefStream", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34112U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A3A RID: 6714
			public static readonly StoreNamedPropInfo ScriptStream = new StoreNamedPropInfo("ScriptStream", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34113U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A3B RID: 6715
			public static readonly StoreNamedPropInfo CustomFlag = new StoreNamedPropInfo("CustomFlag", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34114U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A3C RID: 6716
			public static readonly StoreNamedPropInfo NonSendToTrackStatus = new StoreNamedPropInfo("NonSendToTrackStatus", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34115U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A3D RID: 6717
			public static readonly StoreNamedPropInfo NonSendCcTrackStatus = new StoreNamedPropInfo("NonSendCcTrackStatus", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34116U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A3E RID: 6718
			public static readonly StoreNamedPropInfo NonSendBccTrackStatus = new StoreNamedPropInfo("NonSendBccTrackStatus", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34117U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A3F RID: 6719
			public static readonly StoreNamedPropInfo RecallTime = new StoreNamedPropInfo("RecallTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34121U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A40 RID: 6720
			public static readonly StoreNamedPropInfo AttachStripped = new StoreNamedPropInfo("AttachStripped", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34122U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A41 RID: 6721
			public static readonly StoreNamedPropInfo MinReadVersion = new StoreNamedPropInfo("MinReadVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34128U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A42 RID: 6722
			public static readonly StoreNamedPropInfo MinWriteVersion = new StoreNamedPropInfo("MinWriteVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34129U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A43 RID: 6723
			public static readonly StoreNamedPropInfo CurrentVersion = new StoreNamedPropInfo("CurrentVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34130U), PropertyType.Int32, 2UL, default(PropertyCategories));

			// Token: 0x04001A44 RID: 6724
			public static readonly StoreNamedPropInfo CurrentVersionName = new StoreNamedPropInfo("CurrentVersionName", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34132U), PropertyType.Unicode, 2UL, default(PropertyCategories));

			// Token: 0x04001A45 RID: 6725
			public static readonly StoreNamedPropInfo ReminderNextTime = new StoreNamedPropInfo("ReminderNextTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34144U), PropertyType.SysTime, 4611686018427387968UL, default(PropertyCategories));

			// Token: 0x04001A46 RID: 6726
			public static readonly StoreNamedPropInfo ImapDeleted = new StoreNamedPropInfo("ImapDeleted", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34160U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A47 RID: 6727
			public static readonly StoreNamedPropInfo MarkedForDownload = new StoreNamedPropInfo("MarkedForDownload", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34161U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A48 RID: 6728
			public static readonly StoreNamedPropInfo HeaderItem = new StoreNamedPropInfo("HeaderItem", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34168U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A49 RID: 6729
			public static readonly StoreNamedPropInfo InetAcctName = new StoreNamedPropInfo("InetAcctName", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34176U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A4A RID: 6730
			public static readonly StoreNamedPropInfo InetAcctStamp = new StoreNamedPropInfo("InetAcctStamp", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34177U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A4B RID: 6731
			public static readonly StoreNamedPropInfo UseTNEF = new StoreNamedPropInfo("UseTNEF", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34178U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A4C RID: 6732
			public static readonly StoreNamedPropInfo LastAuthorClass = new StoreNamedPropInfo("LastAuthorClass", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34179U), PropertyType.Unicode, 4UL, default(PropertyCategories));

			// Token: 0x04001A4D RID: 6733
			public static readonly StoreNamedPropInfo ContactLinkSearchKey = new StoreNamedPropInfo("ContactLinkSearchKey", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34180U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A4E RID: 6734
			public static readonly StoreNamedPropInfo ContactLinkEntry = new StoreNamedPropInfo("ContactLinkEntry", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34181U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A4F RID: 6735
			public static readonly StoreNamedPropInfo ContactLinkName = new StoreNamedPropInfo("ContactLinkName", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34182U), PropertyType.Unicode, 512UL, default(PropertyCategories));

			// Token: 0x04001A50 RID: 6736
			public static readonly StoreNamedPropInfo DocObjWordmail = new StoreNamedPropInfo("DocObjWordmail", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34183U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A51 RID: 6737
			public static readonly StoreNamedPropInfo StampedAccount = new StoreNamedPropInfo("StampedAccount", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34184U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A52 RID: 6738
			public static readonly StoreNamedPropInfo UseInternetZone = new StoreNamedPropInfo("UseInternetZone", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34185U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A53 RID: 6739
			public static readonly StoreNamedPropInfo FramesetBody = new StoreNamedPropInfo("FramesetBody", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34186U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A54 RID: 6740
			public static readonly StoreNamedPropInfo SharingEnabled = new StoreNamedPropInfo("SharingEnabled", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34188U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A55 RID: 6741
			public static readonly StoreNamedPropInfo UberGroup = new StoreNamedPropInfo("UberGroup", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34189U), PropertyType.Unicode, 512UL, default(PropertyCategories));

			// Token: 0x04001A56 RID: 6742
			public static readonly StoreNamedPropInfo SharingServerUrl = new StoreNamedPropInfo("SharingServerUrl", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34190U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A57 RID: 6743
			public static readonly StoreNamedPropInfo SharingTitle = new StoreNamedPropInfo("SharingTitle", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34191U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A58 RID: 6744
			public static readonly StoreNamedPropInfo SharingAutoPane = new StoreNamedPropInfo("SharingAutoPane", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34192U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A59 RID: 6745
			public static readonly StoreNamedPropInfo SharingFooterID = new StoreNamedPropInfo("SharingFooterID", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34193U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A5A RID: 6746
			public static readonly StoreNamedPropInfo ImgAttchmtsCompressLevel = new StoreNamedPropInfo("ImgAttchmtsCompressLevel", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34195U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A5B RID: 6747
			public static readonly StoreNamedPropInfo ImgAttchmtsPreviewSize = new StoreNamedPropInfo("ImgAttchmtsPreviewSize", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34196U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A5C RID: 6748
			public static readonly StoreNamedPropInfo ConflictItems = new StoreNamedPropInfo("ConflictItems", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34197U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A5D RID: 6749
			public static readonly StoreNamedPropInfo SharingWebUrl = new StoreNamedPropInfo("SharingWebUrl", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34198U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A5E RID: 6750
			public static readonly StoreNamedPropInfo SyncFailures = new StoreNamedPropInfo("SyncFailures", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34199U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A5F RID: 6751
			public static readonly StoreNamedPropInfo ContentClass = new StoreNamedPropInfo("ContentClass", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34200U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A60 RID: 6752
			public static readonly StoreNamedPropInfo ContentType = new StoreNamedPropInfo("ContentType", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34201U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A61 RID: 6753
			public static readonly StoreNamedPropInfo SharingServerStatus = new StoreNamedPropInfo("SharingServerStatus", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34202U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A62 RID: 6754
			public static readonly StoreNamedPropInfo IsIPFax = new StoreNamedPropInfo("IsIPFax", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34203U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A63 RID: 6755
			public static readonly StoreNamedPropInfo SpamOriginalFolder = new StoreNamedPropInfo("SpamOriginalFolder", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34204U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A64 RID: 6756
			public static readonly StoreNamedPropInfo DrmAttachmentNumber = new StoreNamedPropInfo("DrmAttachmentNumber", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34205U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A65 RID: 6757
			public static readonly StoreNamedPropInfo IsInfoMailPost = new StoreNamedPropInfo("IsInfoMailPost", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34207U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A66 RID: 6758
			public static readonly StoreNamedPropInfo ToDoOrdinalDate = new StoreNamedPropInfo("ToDoOrdinalDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34208U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A67 RID: 6759
			public static readonly StoreNamedPropInfo ToDoSubOrdinal = new StoreNamedPropInfo("ToDoSubOrdinal", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34209U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A68 RID: 6760
			public static readonly StoreNamedPropInfo ToDoTitle = new StoreNamedPropInfo("ToDoTitle", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34212U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001A69 RID: 6761
			public static readonly StoreNamedPropInfo FShouldTNEF = new StoreNamedPropInfo("FShouldTNEF", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34213U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A6A RID: 6762
			public static readonly StoreNamedPropInfo AutoSaveOriginalItemInfo = new StoreNamedPropInfo("AutoSaveOriginalItemInfo", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34216U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A6B RID: 6763
			public static readonly StoreNamedPropInfo InfoPathFormType = new StoreNamedPropInfo("InfoPathFormType", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34225U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A6C RID: 6764
			public static readonly StoreNamedPropInfo LinkedApptItems = new StoreNamedPropInfo("LinkedApptItems", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34226U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A6D RID: 6765
			public static readonly StoreNamedPropInfo ApptStartTimes = new StoreNamedPropInfo("ApptStartTimes", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34227U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A6E RID: 6766
			public static readonly StoreNamedPropInfo Classified = new StoreNamedPropInfo("Classified", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34229U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A6F RID: 6767
			public static readonly StoreNamedPropInfo Classification = new StoreNamedPropInfo("Classification", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34230U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A70 RID: 6768
			public static readonly StoreNamedPropInfo ClassDesc = new StoreNamedPropInfo("ClassDesc", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34231U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A71 RID: 6769
			public static readonly StoreNamedPropInfo ClassGuid = new StoreNamedPropInfo("ClassGuid", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34232U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A72 RID: 6770
			public static readonly StoreNamedPropInfo OfflineStatus = new StoreNamedPropInfo("OfflineStatus", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34233U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A73 RID: 6771
			public static readonly StoreNamedPropInfo ClassKeep = new StoreNamedPropInfo("ClassKeep", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34234U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A74 RID: 6772
			public static readonly StoreNamedPropInfo Creator = new StoreNamedPropInfo("Creator", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34236U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A75 RID: 6773
			public static readonly StoreNamedPropInfo ReferenceEID = new StoreNamedPropInfo("ReferenceEID", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34237U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A76 RID: 6774
			public static readonly StoreNamedPropInfo StsContentTypeId = new StoreNamedPropInfo("StsContentTypeId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34238U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A77 RID: 6775
			public static readonly StoreNamedPropInfo ValidFlagStringProof = new StoreNamedPropInfo("ValidFlagStringProof", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34239U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A78 RID: 6776
			public static readonly StoreNamedPropInfo FlagStringEnum = new StoreNamedPropInfo("FlagStringEnum", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34240U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A79 RID: 6777
			public static readonly StoreNamedPropInfo SharedItemOwner = new StoreNamedPropInfo("SharedItemOwner", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34241U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A7A RID: 6778
			public static readonly StoreNamedPropInfo ThemeDataXML = new StoreNamedPropInfo("ThemeDataXML", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34242U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A7B RID: 6779
			public static readonly StoreNamedPropInfo ColorSchemeMappingXML = new StoreNamedPropInfo("ColorSchemeMappingXML", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34243U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A7C RID: 6780
			public static readonly StoreNamedPropInfo PendingStateforTMDocument = new StoreNamedPropInfo("PendingStateforTMDocument", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 34272U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A7D RID: 6781
			public static readonly StoreNamedPropInfo AllowedFlagString = new StoreNamedPropInfo("AllowedFlagString", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 61624U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A7E RID: 6782
			public static readonly StoreNamedPropInfo ToDoTitleOM = new StoreNamedPropInfo("ToDoTitleOM", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 64543U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A7F RID: 6783
			public static readonly StoreNamedPropInfo StartRecurTime = new StoreNamedPropInfo("StartRecurTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, 1009U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A80 RID: 6784
			public static readonly StoreNamedPropInfo ArchiveSourseSupportMask = new StoreNamedPropInfo("ArchiveSourseSupportMask", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "ArchiveSourceSupportMask"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A81 RID: 6785
			public static readonly StoreNamedPropInfo CrawlSourceSupportMask = new StoreNamedPropInfo("CrawlSourceSupportMask", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "CrawlSourceSupportMask"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A82 RID: 6786
			public static readonly StoreNamedPropInfo MailboxAssociationExternalId = new StoreNamedPropInfo("MailboxAssociationExternalId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationExternalId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A83 RID: 6787
			public static readonly StoreNamedPropInfo MailboxAssociationLegacyDN = new StoreNamedPropInfo("MailboxAssociationLegacyDN", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationLegacyDN"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A84 RID: 6788
			public static readonly StoreNamedPropInfo MailboxAssociationIsMember = new StoreNamedPropInfo("MailboxAssociationIsMember", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationIsMember"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A85 RID: 6789
			public static readonly StoreNamedPropInfo MailboxAssociationJoinedBy = new StoreNamedPropInfo("MailboxAssociationJoinedBy", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationJoinedBy"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A86 RID: 6790
			public static readonly StoreNamedPropInfo MailboxAssociationIsPin = new StoreNamedPropInfo("MailboxAssociationIsPin", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationIsPin"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A87 RID: 6791
			public static readonly StoreNamedPropInfo MailboxAssociationShouldEscalate = new StoreNamedPropInfo("MailboxAssociationShouldEscalate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationShouldEscalate"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A88 RID: 6792
			public static readonly StoreNamedPropInfo MailboxAssociationIsAutoSubscribed = new StoreNamedPropInfo("MailboxAssociationIsAutoSubscribed", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationIsAutoSubscribed"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A89 RID: 6793
			public static readonly StoreNamedPropInfo MailboxAssociationSmtpAddress = new StoreNamedPropInfo("MailboxAssociationSmtpAddress", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationSmtpAddress"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A8A RID: 6794
			public static readonly StoreNamedPropInfo MailboxAssociationJoinDate = new StoreNamedPropInfo("MailboxAssociationJoinDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationJoinDate"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A8B RID: 6795
			public static readonly StoreNamedPropInfo MailboxAssociationPinDate = new StoreNamedPropInfo("MailboxAssociationPinDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationPinDate"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A8C RID: 6796
			public static readonly StoreNamedPropInfo MailboxAssociationLastVisitedDate = new StoreNamedPropInfo("MailboxAssociationLastVisitedDate", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationLastVisitedDate"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A8D RID: 6797
			public static readonly StoreNamedPropInfo MailboxAssociationCurrentVersion = new StoreNamedPropInfo("MailboxAssociationCurrentVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationCurrentVersion"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A8E RID: 6798
			public static readonly StoreNamedPropInfo MailboxAssociationSyncedVersion = new StoreNamedPropInfo("MailboxAssociationSyncedVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationSyncedVersion"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A8F RID: 6799
			public static readonly StoreNamedPropInfo MailboxAssociationLastSyncError = new StoreNamedPropInfo("MailboxAssociationLastSyncError", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationLastSyncError"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A90 RID: 6800
			public static readonly StoreNamedPropInfo MailboxAssociationSyncAttempts = new StoreNamedPropInfo("MailboxAssociationSyncAttempts", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationSyncAttempts"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A91 RID: 6801
			public static readonly StoreNamedPropInfo MailboxAssociationSyncedSchemaVersion = new StoreNamedPropInfo("MailboxAssociationSyncedSchemaVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationSyncedSchemaVersion"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A92 RID: 6802
			public static readonly StoreNamedPropInfo MailboxAssociationSyncedIdentityHash = new StoreNamedPropInfo("MailboxAssociationSyncedIdentityHash", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationSyncedIdentityHash"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A93 RID: 6803
			public static readonly StoreNamedPropInfo HierarchySyncLastAttemptedSyncTime = new StoreNamedPropInfo("HierarchySyncLastAttemptedSyncTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncLastAttemptedSyncTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A94 RID: 6804
			public static readonly StoreNamedPropInfo HierarchySyncLastFailedSyncTime = new StoreNamedPropInfo("HierarchySyncLastFailedSyncTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncLastFailedSyncTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A95 RID: 6805
			public static readonly StoreNamedPropInfo HierarchySyncLastSuccessfulSyncTime = new StoreNamedPropInfo("HierarchySyncLastSuccessfulSyncTime", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncLastSuccessfulSyncTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A96 RID: 6806
			public static readonly StoreNamedPropInfo HierarchySyncFirstFailedSyncTimeAfterLastSuccess = new StoreNamedPropInfo("HierarchySyncFirstFailedSyncTimeAfterLastSuccess", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncFirstFailedSyncTimeAfterLastSuccess"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A97 RID: 6807
			public static readonly StoreNamedPropInfo HierarchySyncLastSyncFailure = new StoreNamedPropInfo("HierarchySyncLastSyncFailure", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncLastSyncFailure"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A98 RID: 6808
			public static readonly StoreNamedPropInfo HierarchySyncNumberOfAttemptsAfterLastSuccess = new StoreNamedPropInfo("HierarchySyncNumberOfAttemptsAfterLastSuccess", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncNumberOfAttemptsAfterLastSuccess"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A99 RID: 6809
			public static readonly StoreNamedPropInfo HierarchySyncNumberOfBatchesExecuted = new StoreNamedPropInfo("HierarchySyncNumberOfBatchesExecuted", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncNumberOfBatchesExecuted"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A9A RID: 6810
			public static readonly StoreNamedPropInfo HierarchySyncNumberOfFoldersSynced = new StoreNamedPropInfo("HierarchySyncNumberOfFoldersSynced", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncNumberOfFoldersSynced"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A9B RID: 6811
			public static readonly StoreNamedPropInfo HierarchySyncNumberOfFoldersToBeSynced = new StoreNamedPropInfo("HierarchySyncNumberOfFoldersToBeSynced", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncNumberOfFoldersToBeSynced"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A9C RID: 6812
			public static readonly StoreNamedPropInfo HierarchySyncBatchSize = new StoreNamedPropInfo("HierarchySyncBatchSize", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "HierarchySyncBatchSize"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A9D RID: 6813
			public static readonly StoreNamedPropInfo UserPhotoHR648x648 = new StoreNamedPropInfo("UserPhotoHR648x648", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR648x648"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A9E RID: 6814
			public static readonly StoreNamedPropInfo UserPhotoHR504x504 = new StoreNamedPropInfo("UserPhotoHR504x504", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR504x504"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001A9F RID: 6815
			public static readonly StoreNamedPropInfo UserPhotoHR432x432 = new StoreNamedPropInfo("UserPhotoHR432x432", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR432x432"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA0 RID: 6816
			public static readonly StoreNamedPropInfo UserPhotoHR360x360 = new StoreNamedPropInfo("UserPhotoHR360x360", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR360x360"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA1 RID: 6817
			public static readonly StoreNamedPropInfo UserPhotoHR240x240 = new StoreNamedPropInfo("UserPhotoHR240x240", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR240x240"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA2 RID: 6818
			public static readonly StoreNamedPropInfo UserPhotoHR120x120 = new StoreNamedPropInfo("UserPhotoHR120x120", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR120x120"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA3 RID: 6819
			public static readonly StoreNamedPropInfo UserPhotoHR96x96 = new StoreNamedPropInfo("UserPhotoHR96x96", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR96x96"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA4 RID: 6820
			public static readonly StoreNamedPropInfo UserPhotoHR64x64 = new StoreNamedPropInfo("UserPhotoHR64x64", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR64x64"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA5 RID: 6821
			public static readonly StoreNamedPropInfo UserPhotoHR48x48 = new StoreNamedPropInfo("UserPhotoHR48x48", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "UserPhotoHR48x48"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA6 RID: 6822
			public static readonly StoreNamedPropInfo PeopleHubSortGroupPriority = new StoreNamedPropInfo("PeopleHubSortGroupPriority", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "PeopleHubSortGroupPriority"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA7 RID: 6823
			public static readonly StoreNamedPropInfo PeopleHubSortGroupPriorityVersion = new StoreNamedPropInfo("PeopleHubSortGroupPriorityVersion", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "PeopleHubSortGroupPriorityVersion"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA8 RID: 6824
			public static readonly StoreNamedPropInfo IsPeopleConnectSyncFolder = new StoreNamedPropInfo("IsPeopleConnectSyncFolder", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "IsPeopleConnectSyncFolder"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AA9 RID: 6825
			public static readonly StoreNamedPropInfo TemporarySavesFolderEntryId = new StoreNamedPropInfo("TemporarySavesFolderEntryId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "TemporarySavesFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AAA RID: 6826
			public static readonly StoreNamedPropInfo BirthdayCalendarFolderEntryId = new StoreNamedPropInfo("BirthdayCalendarFolderEntryId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "BirthdayCalendarFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AAB RID: 6827
			public static readonly StoreNamedPropInfo SnackyAppsFolderEntryId = new StoreNamedPropInfo("SnackyAppsFolderEntryId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "SnackyAppsFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AAC RID: 6828
			public static readonly StoreNamedPropInfo PropertyExistenceTracker = new StoreNamedPropInfo("PropertyExistenceTracker", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "PropertyExistenceTracker"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AAD RID: 6829
			public static readonly StoreNamedPropInfo MailboxAssociationFolderEntryId = new StoreNamedPropInfo("MailboxAssociationFolderEntryId", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "MailboxAssociationFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AAE RID: 6830
			public static readonly StoreNamedPropInfo ExchangeApplicationFlags = new StoreNamedPropInfo("ExchangeApplicationFlags", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "ExchangeApplicationFlags"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AAF RID: 6831
			public static readonly StoreNamedPropInfo ItemMovedByRule = new StoreNamedPropInfo("ItemMovedByRule", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "ItemMovedByRule"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AB0 RID: 6832
			public static readonly StoreNamedPropInfo ItemMovedByConversationAction = new StoreNamedPropInfo("ItemMovedByConversationAction", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "ItemMovedByConversationAction"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AB1 RID: 6833
			public static readonly StoreNamedPropInfo ItemSenderClass = new StoreNamedPropInfo("ItemSenderClass", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "ItemSenderClass"), PropertyType.Int16, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AB2 RID: 6834
			public static readonly StoreNamedPropInfo ItemCurrentFolderReason = new StoreNamedPropInfo("ItemCurrentFolderReason", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "ItemCurrentFolderReason"), PropertyType.Int16, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AB3 RID: 6835
			public static readonly StoreNamedPropInfo OfficeGraphLocation = new StoreNamedPropInfo("OfficeGraphLocation", new StorePropName(NamedPropInfo.Common.NamespaceGuid, "OfficeGraphLocation"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000021 RID: 33
		public static class Mail
		{
			// Token: 0x04001AB4 RID: 6836
			public static readonly Guid NamespaceGuid = new Guid("00062009-0000-0000-C000-000000000046");
		}

		// Token: 0x02000022 RID: 34
		public static class Log
		{
			// Token: 0x04001AB5 RID: 6837
			public static readonly Guid NamespaceGuid = new Guid("0006200A-0000-0000-C000-000000000046");

			// Token: 0x04001AB6 RID: 6838
			public static readonly StoreNamedPropInfo LogType = new StoreNamedPropInfo("LogType", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34560U), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001AB7 RID: 6839
			public static readonly StoreNamedPropInfo LogStartDate = new StoreNamedPropInfo("LogStartDate", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34564U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AB8 RID: 6840
			public static readonly StoreNamedPropInfo LogStartTime = new StoreNamedPropInfo("LogStartTime", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34565U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AB9 RID: 6841
			public static readonly StoreNamedPropInfo LogStart = new StoreNamedPropInfo("LogStart", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34566U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ABA RID: 6842
			public static readonly StoreNamedPropInfo LogDuration = new StoreNamedPropInfo("LogDuration", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34567U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ABB RID: 6843
			public static readonly StoreNamedPropInfo LogEnd = new StoreNamedPropInfo("LogEnd", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34568U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ABC RID: 6844
			public static readonly StoreNamedPropInfo LogFlags = new StoreNamedPropInfo("LogFlags", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34572U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ABD RID: 6845
			public static readonly StoreNamedPropInfo LogContactLog = new StoreNamedPropInfo("LogContactLog", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34573U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ABE RID: 6846
			public static readonly StoreNamedPropInfo LogDocPrinted = new StoreNamedPropInfo("LogDocPrinted", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34574U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ABF RID: 6847
			public static readonly StoreNamedPropInfo LogDocSaved = new StoreNamedPropInfo("LogDocSaved", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34575U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AC0 RID: 6848
			public static readonly StoreNamedPropInfo LogDocRouted = new StoreNamedPropInfo("LogDocRouted", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34576U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AC1 RID: 6849
			public static readonly StoreNamedPropInfo LogDocPosted = new StoreNamedPropInfo("LogDocPosted", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34577U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AC2 RID: 6850
			public static readonly StoreNamedPropInfo LogTypeDesc = new StoreNamedPropInfo("LogTypeDesc", new StorePropName(NamedPropInfo.Log.NamespaceGuid, 34578U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000023 RID: 35
		public static class Tracking
		{
			// Token: 0x04001AC3 RID: 6851
			public static readonly Guid NamespaceGuid = new Guid("0006200B-0000-0000-C000-000000000046");

			// Token: 0x04001AC4 RID: 6852
			public static readonly StoreNamedPropInfo FHaveWrittenTracking = new StoreNamedPropInfo("FHaveWrittenTracking", new StorePropName(NamedPropInfo.Tracking.NamespaceGuid, 34824U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AC5 RID: 6853
			public static readonly StoreNamedPropInfo UnifiedTracking = new StoreNamedPropInfo("UnifiedTracking", new StorePropName(NamedPropInfo.Tracking.NamespaceGuid, 34825U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000024 RID: 36
		public static class MAPIExtra
		{
			// Token: 0x04001AC6 RID: 6854
			public static readonly Guid NamespaceGuid = new Guid("0006200C-0000-0000-C000-000000000046");
		}

		// Token: 0x02000025 RID: 37
		public static class Exception
		{
			// Token: 0x04001AC7 RID: 6855
			public static readonly Guid NamespaceGuid = new Guid("0006200D-0000-0000-C000-000000000046");
		}

		// Token: 0x02000026 RID: 38
		public static class RenStore
		{
			// Token: 0x04001AC8 RID: 6856
			public static readonly Guid NamespaceGuid = new Guid("0006200F-0000-0000-C000-000000000046");
		}

		// Token: 0x02000027 RID: 39
		public static class System
		{
			// Token: 0x04001AC9 RID: 6857
			public static readonly Guid NamespaceGuid = new Guid("00062010-0000-0000-C000-000000000046");
		}

		// Token: 0x02000028 RID: 40
		public static class AddrPersonal
		{
			// Token: 0x04001ACA RID: 6858
			public static readonly Guid NamespaceGuid = new Guid("00062012-0000-0000-C000-000000000046");
		}

		// Token: 0x02000029 RID: 41
		public static class Report
		{
			// Token: 0x04001ACB RID: 6859
			public static readonly Guid NamespaceGuid = new Guid("00062013-0000-0000-C000-000000000046");

			// Token: 0x04001ACC RID: 6860
			public static readonly StoreNamedPropInfo ResendTime = new StoreNamedPropInfo("ResendTime", new StorePropName(NamedPropInfo.Report.NamespaceGuid, 36096U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200002A RID: 42
		public static class Remote
		{
			// Token: 0x04001ACD RID: 6861
			public static readonly Guid NamespaceGuid = new Guid("00062014-0000-0000-C000-000000000046");

			// Token: 0x04001ACE RID: 6862
			public static readonly StoreNamedPropInfo RemoteEID = new StoreNamedPropInfo("RemoteEID", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36609U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ACF RID: 6863
			public static readonly StoreNamedPropInfo RemoteMsgClass = new StoreNamedPropInfo("RemoteMsgClass", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36610U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AD0 RID: 6864
			public static readonly StoreNamedPropInfo RemoteXP = new StoreNamedPropInfo("RemoteXP", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36611U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AD1 RID: 6865
			public static readonly StoreNamedPropInfo RemoteXferTime = new StoreNamedPropInfo("RemoteXferTime", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36612U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AD2 RID: 6866
			public static readonly StoreNamedPropInfo RemoteXferSize = new StoreNamedPropInfo("RemoteXferSize", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36613U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AD3 RID: 6867
			public static readonly StoreNamedPropInfo RemoteSearchKey = new StoreNamedPropInfo("RemoteSearchKey", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36614U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AD4 RID: 6868
			public static readonly StoreNamedPropInfo RemoteAttachment = new StoreNamedPropInfo("RemoteAttachment", new StorePropName(NamedPropInfo.Remote.NamespaceGuid, 36615U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200002B RID: 43
		public static class FATSystem
		{
			// Token: 0x04001AD5 RID: 6869
			public static readonly Guid NamespaceGuid = new Guid("00062015-0000-0000-C000-000000000046");
		}

		// Token: 0x0200002C RID: 44
		public static class FATCommon
		{
			// Token: 0x04001AD6 RID: 6870
			public static readonly Guid NamespaceGuid = new Guid("00062016-0000-0000-C000-000000000046");
		}

		// Token: 0x0200002D RID: 45
		public static class FATCustom
		{
			// Token: 0x04001AD7 RID: 6871
			public static readonly Guid NamespaceGuid = new Guid("00062017-0000-0000-C000-000000000046");
		}

		// Token: 0x0200002E RID: 46
		public static class News
		{
			// Token: 0x04001AD8 RID: 6872
			public static readonly Guid NamespaceGuid = new Guid("00062018-0000-0000-C000-000000000046");
		}

		// Token: 0x0200002F RID: 47
		public static class Sharing
		{
			// Token: 0x04001AD9 RID: 6873
			public static readonly Guid NamespaceGuid = new Guid("00062040-0000-0000-C000-000000000046");

			// Token: 0x04001ADA RID: 6874
			public static readonly StoreNamedPropInfo SharingStatus = new StoreNamedPropInfo("SharingStatus", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35328U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ADB RID: 6875
			public static readonly StoreNamedPropInfo SharingProviderGuid = new StoreNamedPropInfo("SharingProviderGuid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35329U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ADC RID: 6876
			public static readonly StoreNamedPropInfo SharingProviderName = new StoreNamedPropInfo("SharingProviderName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35330U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ADD RID: 6877
			public static readonly StoreNamedPropInfo SharingProviderUrl = new StoreNamedPropInfo("SharingProviderUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35331U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001ADE RID: 6878
			public static readonly StoreNamedPropInfo SharingRemotePath = new StoreNamedPropInfo("SharingRemotePath", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35332U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001ADF RID: 6879
			public static readonly StoreNamedPropInfo SharingRemoteName = new StoreNamedPropInfo("SharingRemoteName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35333U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001AE0 RID: 6880
			public static readonly StoreNamedPropInfo SharingRemoteUid = new StoreNamedPropInfo("SharingRemoteUid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35334U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE1 RID: 6881
			public static readonly StoreNamedPropInfo SharingInitiatorName = new StoreNamedPropInfo("SharingInitiatorName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35335U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001AE2 RID: 6882
			public static readonly StoreNamedPropInfo SharingInitiatorSmtp = new StoreNamedPropInfo("SharingInitiatorSmtp", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35336U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE3 RID: 6883
			public static readonly StoreNamedPropInfo SharingInitiatorEid = new StoreNamedPropInfo("SharingInitiatorEid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35337U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE4 RID: 6884
			public static readonly StoreNamedPropInfo SharingFlags = new StoreNamedPropInfo("SharingFlags", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35338U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE5 RID: 6885
			public static readonly StoreNamedPropInfo SharingProviderExtension = new StoreNamedPropInfo("SharingProviderExtension", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35339U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE6 RID: 6886
			public static readonly StoreNamedPropInfo SharingRemoteUser = new StoreNamedPropInfo("SharingRemoteUser", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35340U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE7 RID: 6887
			public static readonly StoreNamedPropInfo SharingRemotePass = new StoreNamedPropInfo("SharingRemotePass", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35341U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE8 RID: 6888
			public static readonly StoreNamedPropInfo SharingLocalPath = new StoreNamedPropInfo("SharingLocalPath", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35342U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AE9 RID: 6889
			public static readonly StoreNamedPropInfo SharingLocalName = new StoreNamedPropInfo("SharingLocalName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35343U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001AEA RID: 6890
			public static readonly StoreNamedPropInfo SharingLocalUid = new StoreNamedPropInfo("SharingLocalUid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35344U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AEB RID: 6891
			public static readonly StoreNamedPropInfo SharingFilter = new StoreNamedPropInfo("SharingFilter", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35347U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AEC RID: 6892
			public static readonly StoreNamedPropInfo SharingLocalType = new StoreNamedPropInfo("SharingLocalType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35348U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AED RID: 6893
			public static readonly StoreNamedPropInfo SharingFolderEid = new StoreNamedPropInfo("SharingFolderEid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35349U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AEE RID: 6894
			public static readonly StoreNamedPropInfo SharingCaps = new StoreNamedPropInfo("SharingCaps", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35351U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AEF RID: 6895
			public static readonly StoreNamedPropInfo SharingFlavor = new StoreNamedPropInfo("SharingFlavor", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35352U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF0 RID: 6896
			public static readonly StoreNamedPropInfo SharingAnonymity = new StoreNamedPropInfo("SharingAnonymity", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35353U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF1 RID: 6897
			public static readonly StoreNamedPropInfo SharingReciprocation = new StoreNamedPropInfo("SharingReciprocation", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35354U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF2 RID: 6898
			public static readonly StoreNamedPropInfo SharingPermissions = new StoreNamedPropInfo("SharingPermissions", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35355U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF3 RID: 6899
			public static readonly StoreNamedPropInfo SharingInstanceGuid = new StoreNamedPropInfo("SharingInstanceGuid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35356U), PropertyType.Guid, 128UL, default(PropertyCategories));

			// Token: 0x04001AF4 RID: 6900
			public static readonly StoreNamedPropInfo SharingRemoteType = new StoreNamedPropInfo("SharingRemoteType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35357U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF5 RID: 6901
			public static readonly StoreNamedPropInfo SharingParticipants = new StoreNamedPropInfo("SharingParticipants", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35358U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF6 RID: 6902
			public static readonly StoreNamedPropInfo SharingLastSync = new StoreNamedPropInfo("SharingLastSync", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35359U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF7 RID: 6903
			public static readonly StoreNamedPropInfo SharingRssHash = new StoreNamedPropInfo("SharingRssHash", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35360U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF8 RID: 6904
			public static readonly StoreNamedPropInfo SharingExtXml = new StoreNamedPropInfo("SharingExtXml", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35361U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AF9 RID: 6905
			public static readonly StoreNamedPropInfo SharingRemoteLastMod = new StoreNamedPropInfo("SharingRemoteLastMod", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35362U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AFA RID: 6906
			public static readonly StoreNamedPropInfo SharingLocalLastMod = new StoreNamedPropInfo("SharingLocalLastMod", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35363U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AFB RID: 6907
			public static readonly StoreNamedPropInfo SharingConfigUrl = new StoreNamedPropInfo("SharingConfigUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35364U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AFC RID: 6908
			public static readonly StoreNamedPropInfo SharingStart = new StoreNamedPropInfo("SharingStart", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35365U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AFD RID: 6909
			public static readonly StoreNamedPropInfo SharingStop = new StoreNamedPropInfo("SharingStop", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35366U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AFE RID: 6910
			public static readonly StoreNamedPropInfo SharingResponseType = new StoreNamedPropInfo("SharingResponseType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35367U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001AFF RID: 6911
			public static readonly StoreNamedPropInfo SharingResponseTime = new StoreNamedPropInfo("SharingResponseTime", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35368U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B00 RID: 6912
			public static readonly StoreNamedPropInfo SharingOriginalMessageEid = new StoreNamedPropInfo("SharingOriginalMessageEid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35369U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B01 RID: 6913
			public static readonly StoreNamedPropInfo SharingSyncInterval = new StoreNamedPropInfo("SharingSyncInterval", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35370U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B02 RID: 6914
			public static readonly StoreNamedPropInfo SharingDetail = new StoreNamedPropInfo("SharingDetail", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35371U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B03 RID: 6915
			public static readonly StoreNamedPropInfo SharingTimeToLive = new StoreNamedPropInfo("SharingTimeToLive", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35372U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B04 RID: 6916
			public static readonly StoreNamedPropInfo SharingBindingEid = new StoreNamedPropInfo("SharingBindingEid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35373U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B05 RID: 6917
			public static readonly StoreNamedPropInfo SharingIndexEid = new StoreNamedPropInfo("SharingIndexEid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35374U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B06 RID: 6918
			public static readonly StoreNamedPropInfo SharingRemoteComment = new StoreNamedPropInfo("SharingRemoteComment", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35375U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001B07 RID: 6919
			public static readonly StoreNamedPropInfo SharingWssVer = new StoreNamedPropInfo("SharingWssVer", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35376U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B08 RID: 6920
			public static readonly StoreNamedPropInfo SharingWssCmd = new StoreNamedPropInfo("SharingWssCmd", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35377U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B09 RID: 6921
			public static readonly StoreNamedPropInfo SharingWssListRelUrl = new StoreNamedPropInfo("SharingWssListRelUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35378U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B0A RID: 6922
			public static readonly StoreNamedPropInfo SharingWssSiteName = new StoreNamedPropInfo("SharingWssSiteName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35379U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B0B RID: 6923
			public static readonly StoreNamedPropInfo XSharingConfigUrl = new StoreNamedPropInfo("XSharingConfigUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35381U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B0C RID: 6924
			public static readonly StoreNamedPropInfo XSharingRemotePath = new StoreNamedPropInfo("XSharingRemotePath", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35382U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B0D RID: 6925
			public static readonly StoreNamedPropInfo XSharingRemoteName = new StoreNamedPropInfo("XSharingRemoteName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35383U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B0E RID: 6926
			public static readonly StoreNamedPropInfo XSharingRemoteUid = new StoreNamedPropInfo("XSharingRemoteUid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35384U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B0F RID: 6927
			public static readonly StoreNamedPropInfo XSharingRemoteType = new StoreNamedPropInfo("XSharingRemoteType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35385U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B10 RID: 6928
			public static readonly StoreNamedPropInfo XSharingInstanceGuid = new StoreNamedPropInfo("XSharingInstanceGuid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35386U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B11 RID: 6929
			public static readonly StoreNamedPropInfo XSharingCapabilities = new StoreNamedPropInfo("XSharingCapabilities", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35387U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B12 RID: 6930
			public static readonly StoreNamedPropInfo XSharingFlavor = new StoreNamedPropInfo("XSharingFlavor", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35388U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B13 RID: 6931
			public static readonly StoreNamedPropInfo XSharingProviderGuid = new StoreNamedPropInfo("XSharingProviderGuid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35389U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B14 RID: 6932
			public static readonly StoreNamedPropInfo XSharingProviderName = new StoreNamedPropInfo("XSharingProviderName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35390U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B15 RID: 6933
			public static readonly StoreNamedPropInfo XSharingProviderUrl = new StoreNamedPropInfo("XSharingProviderUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35391U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B16 RID: 6934
			public static readonly StoreNamedPropInfo SharingWorkingHoursStart = new StoreNamedPropInfo("SharingWorkingHoursStart", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35392U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B17 RID: 6935
			public static readonly StoreNamedPropInfo SharingWorkingHoursEnd = new StoreNamedPropInfo("SharingWorkingHoursEnd", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35393U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B18 RID: 6936
			public static readonly StoreNamedPropInfo SharingWorkingHoursDays = new StoreNamedPropInfo("SharingWorkingHoursDays", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35394U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B19 RID: 6937
			public static readonly StoreNamedPropInfo SharingWorkingHoursTZ = new StoreNamedPropInfo("SharingWorkingHoursTZ", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35395U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B1A RID: 6938
			public static readonly StoreNamedPropInfo SharingDataRangeStart = new StoreNamedPropInfo("SharingDataRangeStart", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35396U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B1B RID: 6939
			public static readonly StoreNamedPropInfo SharingDataRangeEnd = new StoreNamedPropInfo("SharingDataRangeEnd", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35397U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B1C RID: 6940
			public static readonly StoreNamedPropInfo SharingRangeStart = new StoreNamedPropInfo("SharingRangeStart", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35398U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B1D RID: 6941
			public static readonly StoreNamedPropInfo SharingRangeEnd = new StoreNamedPropInfo("SharingRangeEnd", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35399U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B1E RID: 6942
			public static readonly StoreNamedPropInfo SharingRemoteStoreUid = new StoreNamedPropInfo("SharingRemoteStoreUid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35400U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B1F RID: 6943
			public static readonly StoreNamedPropInfo SharingLocalStoreUid = new StoreNamedPropInfo("SharingLocalStoreUid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35401U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B20 RID: 6944
			public static readonly StoreNamedPropInfo XSharingRemoteStoreUid = new StoreNamedPropInfo("XSharingRemoteStoreUid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35402U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B21 RID: 6945
			public static readonly StoreNamedPropInfo SharingRemoteByteSize = new StoreNamedPropInfo("SharingRemoteByteSize", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35403U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B22 RID: 6946
			public static readonly StoreNamedPropInfo SharingRemoteCrc = new StoreNamedPropInfo("SharingRemoteCrc", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35404U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B23 RID: 6947
			public static readonly StoreNamedPropInfo SharingLocalComment = new StoreNamedPropInfo("SharingLocalComment", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35405U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B24 RID: 6948
			public static readonly StoreNamedPropInfo SharingRoamLog = new StoreNamedPropInfo("SharingRoamLog", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35406U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B25 RID: 6949
			public static readonly StoreNamedPropInfo SharingRemoteMsgCount = new StoreNamedPropInfo("SharingRemoteMsgCount", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35407U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B26 RID: 6950
			public static readonly StoreNamedPropInfo XSharingLocalType = new StoreNamedPropInfo("XSharingLocalType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35408U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B27 RID: 6951
			public static readonly StoreNamedPropInfo SharingBrowseUrl = new StoreNamedPropInfo("SharingBrowseUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35409U), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001B28 RID: 6952
			public static readonly StoreNamedPropInfo XSharingBrowseUrl = new StoreNamedPropInfo("XSharingBrowseUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35410U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B29 RID: 6953
			public static readonly StoreNamedPropInfo SharingLastAutoSync = new StoreNamedPropInfo("SharingLastAutoSync", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35413U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B2A RID: 6954
			public static readonly StoreNamedPropInfo SharingTimeToLiveAuto = new StoreNamedPropInfo("SharingTimeToLiveAuto", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35414U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B2B RID: 6955
			public static readonly StoreNamedPropInfo SharingWssFolderRelUrl = new StoreNamedPropInfo("SharingWssFolderRelUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35415U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B2C RID: 6956
			public static readonly StoreNamedPropInfo SharingWssFileRelUrl = new StoreNamedPropInfo("SharingWssFileRelUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35416U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B2D RID: 6957
			public static readonly StoreNamedPropInfo SharingWssPrevFolderRelUrls = new StoreNamedPropInfo("SharingWssPrevFolderRelUrls", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35417U), PropertyType.MVUnicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B2E RID: 6958
			public static readonly StoreNamedPropInfo SharingWssAlternateUrls = new StoreNamedPropInfo("SharingWssAlternateUrls", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35418U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B2F RID: 6959
			public static readonly StoreNamedPropInfo SharingRemoteVersion = new StoreNamedPropInfo("SharingRemoteVersion", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35419U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B30 RID: 6960
			public static readonly StoreNamedPropInfo SharingParentBindingEid = new StoreNamedPropInfo("SharingParentBindingEid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35420U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B31 RID: 6961
			public static readonly StoreNamedPropInfo SharingWssCachedSchema = new StoreNamedPropInfo("SharingWssCachedSchema", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35421U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B32 RID: 6962
			public static readonly StoreNamedPropInfo SharingSavedSession = new StoreNamedPropInfo("SharingSavedSession", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35422U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B33 RID: 6963
			public static readonly StoreNamedPropInfo SharingWssServerRelUrl = new StoreNamedPropInfo("SharingWssServerRelUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35423U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B34 RID: 6964
			public static readonly StoreNamedPropInfo SharingSyncFlags = new StoreNamedPropInfo("SharingSyncFlags", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35424U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B35 RID: 6965
			public static readonly StoreNamedPropInfo SharingWssFolderID = new StoreNamedPropInfo("SharingWssFolderID", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35425U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B36 RID: 6966
			public static readonly StoreNamedPropInfo SharingWssAllFolderIDs = new StoreNamedPropInfo("SharingWssAllFolderIDs", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35426U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B37 RID: 6967
			public static readonly StoreNamedPropInfo SharingRemoteEwsId = new StoreNamedPropInfo("SharingRemoteEwsId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35429U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B38 RID: 6968
			public static readonly StoreNamedPropInfo SharingLocalEwsId = new StoreNamedPropInfo("SharingLocalEwsId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35430U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B39 RID: 6969
			public static readonly StoreNamedPropInfo SharingDetailedStatus = new StoreNamedPropInfo("SharingDetailedStatus", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35488U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B3A RID: 6970
			public static readonly StoreNamedPropInfo SharingLastSuccessSyncTime = new StoreNamedPropInfo("SharingLastSuccessSyncTime", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35492U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B3B RID: 6971
			public static readonly StoreNamedPropInfo SharingSyncRange = new StoreNamedPropInfo("SharingSyncRange", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35493U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B3C RID: 6972
			public static readonly StoreNamedPropInfo SharingAggregationStatus = new StoreNamedPropInfo("SharingAggregationStatus", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35494U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B3D RID: 6973
			public static readonly StoreNamedPropInfo SharingWlidAuthPolicy = new StoreNamedPropInfo("SharingWlidAuthPolicy", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35504U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B3E RID: 6974
			public static readonly StoreNamedPropInfo SharingWlidUserPuid = new StoreNamedPropInfo("SharingWlidUserPuid", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35505U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B3F RID: 6975
			public static readonly StoreNamedPropInfo SharingWlidAuthToken = new StoreNamedPropInfo("SharingWlidAuthToken", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35506U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B40 RID: 6976
			public static readonly StoreNamedPropInfo SharingWlidAuthTokenExpireTime = new StoreNamedPropInfo("SharingWlidAuthTokenExpireTime", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35507U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B41 RID: 6977
			public static readonly StoreNamedPropInfo SharingMinSyncPollInterval = new StoreNamedPropInfo("SharingMinSyncPollInterval", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35520U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B42 RID: 6978
			public static readonly StoreNamedPropInfo SharingMinSettingPollInterval = new StoreNamedPropInfo("SharingMinSettingPollInterval", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35521U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B43 RID: 6979
			public static readonly StoreNamedPropInfo SharingSyncMultiplier = new StoreNamedPropInfo("SharingSyncMultiplier", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35522U), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B44 RID: 6980
			public static readonly StoreNamedPropInfo SharingMaxObjectsInSync = new StoreNamedPropInfo("SharingMaxObjectsInSync", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35523U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B45 RID: 6981
			public static readonly StoreNamedPropInfo SharingMaxNumberOfEmails = new StoreNamedPropInfo("SharingMaxNumberOfEmails", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35524U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B46 RID: 6982
			public static readonly StoreNamedPropInfo SharingMaxNumberOfFolders = new StoreNamedPropInfo("SharingMaxNumberOfFolders", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35525U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B47 RID: 6983
			public static readonly StoreNamedPropInfo SharingMaxAttachments = new StoreNamedPropInfo("SharingMaxAttachments", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35526U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B48 RID: 6984
			public static readonly StoreNamedPropInfo SharingMaxMessageSize = new StoreNamedPropInfo("SharingMaxMessageSize", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35527U), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B49 RID: 6985
			public static readonly StoreNamedPropInfo SharingMaxRecipients = new StoreNamedPropInfo("SharingMaxRecipients", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35528U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B4A RID: 6986
			public static readonly StoreNamedPropInfo SharingMigrationState = new StoreNamedPropInfo("SharingMigrationState", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35529U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B4B RID: 6987
			public static readonly StoreNamedPropInfo SharingDiagnostics = new StoreNamedPropInfo("SharingDiagnostics", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35530U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B4C RID: 6988
			public static readonly StoreNamedPropInfo SharingPoisonCallstack = new StoreNamedPropInfo("SharingPoisonCallstack", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35531U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B4D RID: 6989
			public static readonly StoreNamedPropInfo SharingAggregationType = new StoreNamedPropInfo("SharingAggregationType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35532U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B4E RID: 6990
			public static readonly StoreNamedPropInfo SharingSubscriptionConfiguration = new StoreNamedPropInfo("SharingSubscriptionConfiguration", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35584U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B4F RID: 6991
			public static readonly StoreNamedPropInfo SharingSharingAggregationProtocolVersion = new StoreNamedPropInfo("SharingSharingAggregationProtocolVersion", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35585U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B50 RID: 6992
			public static readonly StoreNamedPropInfo SharingAggregationProtocolName = new StoreNamedPropInfo("SharingAggregationProtocolName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35586U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B51 RID: 6993
			public static readonly StoreNamedPropInfo SharingSubscriptionName = new StoreNamedPropInfo("SharingSubscriptionName", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35587U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B52 RID: 6994
			public static readonly StoreNamedPropInfo SharingSubscriptions = new StoreNamedPropInfo("SharingSubscriptions", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35840U), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B53 RID: 6995
			public static readonly StoreNamedPropInfo SharingAdjustedLastSuccessfulSyncTime = new StoreNamedPropInfo("SharingAdjustedLastSuccessfulSyncTime", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35841U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B54 RID: 6996
			public static readonly StoreNamedPropInfo SharingOutageDetectionDiagnostics = new StoreNamedPropInfo("SharingOutageDetectionDiagnostics", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35842U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B55 RID: 6997
			public static readonly StoreNamedPropInfo SharingEwsUri = new StoreNamedPropInfo("SharingEwsUri", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingEwsUri"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B56 RID: 6998
			public static readonly StoreNamedPropInfo SharingRemoteExchangeVersion = new StoreNamedPropInfo("SharingRemoteExchangeVersion", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingRemoteExchangeVersion"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B57 RID: 6999
			public static readonly StoreNamedPropInfo SharingRemoteUserDomain = new StoreNamedPropInfo("SharingRemoteUserDomain", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingRemoteUserDomain"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B58 RID: 7000
			public static readonly StoreNamedPropInfo SharingSendAsState = new StoreNamedPropInfo("SharingSendAsState", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSendAsState"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B59 RID: 7001
			public static readonly StoreNamedPropInfo SharingSendAsSubmissionUrl = new StoreNamedPropInfo("SharingSendAsSubmissionUrl", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSendAsSubmissionUrl"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B5A RID: 7002
			public static readonly StoreNamedPropInfo SharingSendAsValidatedEmail = new StoreNamedPropInfo("SharingSendAsValidatedEmail", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSendAsValidatedEmail"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B5B RID: 7003
			public static readonly StoreNamedPropInfo SharingSubscriptionVersion = new StoreNamedPropInfo("SharingSubscriptionVersion", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionVersion"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B5C RID: 7004
			public static readonly StoreNamedPropInfo SharingSubscriptionCreationType = new StoreNamedPropInfo("SharingSubscriptionCreationType", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionCreationType"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B5D RID: 7005
			public static readonly StoreNamedPropInfo SharingSubscriptionSyncPhase = new StoreNamedPropInfo("SharingSubscriptionSyncPhase", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionSyncPhase"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B5E RID: 7006
			public static readonly StoreNamedPropInfo SharingSubscriptionExclusionFolders = new StoreNamedPropInfo("SharingSubscriptionExclusionFolders", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionExclusionFolders"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B5F RID: 7007
			public static readonly StoreNamedPropInfo SharingSendAsVerificationEmailState = new StoreNamedPropInfo("SharingSendAsVerificationEmailState", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSendAsVerificationEmailState"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B60 RID: 7008
			public static readonly StoreNamedPropInfo SharingSendAsVerificationMessageId = new StoreNamedPropInfo("SharingSendAsVerificationMessageId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSendAsVerificationMessageId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B61 RID: 7009
			public static readonly StoreNamedPropInfo SharingSendAsVerificationTimestamp = new StoreNamedPropInfo("SharingSendAsVerificationTimestamp", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSendAsVerificationTimestamp"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B62 RID: 7010
			public static readonly StoreNamedPropInfo SharingSubscriptionEvents = new StoreNamedPropInfo("SharingSubscriptionEvents", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionEvents"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B63 RID: 7011
			public static readonly StoreNamedPropInfo SharingImapPathPrefix = new StoreNamedPropInfo("SharingImapPathPrefix", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingImapPathPrefix"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B64 RID: 7012
			public static readonly StoreNamedPropInfo SharingSubscriptionItemsSynced = new StoreNamedPropInfo("SharingSubscriptionItemsSynced", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionItemsSynced"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B65 RID: 7013
			public static readonly StoreNamedPropInfo SharingSubscriptionItemsSkipped = new StoreNamedPropInfo("SharingSubscriptionItemsSkipped", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionItemsSkipped"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B66 RID: 7014
			public static readonly StoreNamedPropInfo SharingSubscriptionTotalItemsInSourceMailbox = new StoreNamedPropInfo("SharingSubscriptionTotalItemsInSourceMailbox", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionTotalItemsInSourceMailbox"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B67 RID: 7015
			public static readonly StoreNamedPropInfo SharingSubscriptionTotalSizeOfSourceMailbox = new StoreNamedPropInfo("SharingSubscriptionTotalSizeOfSourceMailbox", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingSubscriptionTotalSizeOfSourceMailbox"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B68 RID: 7016
			public static readonly StoreNamedPropInfo SharingLastSyncNowRequest = new StoreNamedPropInfo("SharingLastSyncNowRequest", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "SharingLastSyncNowRequest"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B69 RID: 7017
			public static readonly StoreNamedPropInfo MigrationJobId = new StoreNamedPropInfo("MigrationJobId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35601U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B6A RID: 7018
			public static readonly StoreNamedPropInfo MigrationJobItemId = new StoreNamedPropInfo("MigrationJobItemId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35719U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B6B RID: 7019
			public static readonly StoreNamedPropInfo MigrationJobItemStatus = new StoreNamedPropInfo("MigrationJobItemStatus", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35604U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B6C RID: 7020
			public static readonly StoreNamedPropInfo MigrationJobItemEmailAddress = new StoreNamedPropInfo("MigrationJobItemEmailAddress", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35612U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B6D RID: 7021
			public static readonly StoreNamedPropInfo MigrationJobItemLocalMailboxIdentifier = new StoreNamedPropInfo("MigrationJobItemLocalMailboxIdentifier", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35791U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B6E RID: 7022
			public static readonly StoreNamedPropInfo MigrationJobItemMailboxId = new StoreNamedPropInfo("MigrationJobItemMailboxId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35617U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B6F RID: 7023
			public static readonly StoreNamedPropInfo MigrationJobItemMailboxLegacyDN = new StoreNamedPropInfo("MigrationJobItemMailboxLegacyDN", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35619U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B70 RID: 7024
			public static readonly StoreNamedPropInfo MigrationJobItemSubscriptionStateLastChecked = new StoreNamedPropInfo("MigrationJobItemSubscriptionStateLastChecked", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35630U), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B71 RID: 7025
			public static readonly StoreNamedPropInfo MigrationJobItemMRSId = new StoreNamedPropInfo("MigrationJobItemMRSId", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35668U), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B72 RID: 7026
			public static readonly StoreNamedPropInfo MigrationJobStatisticsEnabled = new StoreNamedPropInfo("MigrationJobStatisticsEnabled", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35671U), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B73 RID: 7027
			public static readonly StoreNamedPropInfo MigrationJobItemItemsSynced = new StoreNamedPropInfo("MigrationJobItemItemsSynced", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35672U), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B74 RID: 7028
			public static readonly StoreNamedPropInfo MigrationJobItemItemsSkipped = new StoreNamedPropInfo("MigrationJobItemItemsSkipped", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35673U), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B75 RID: 7029
			public static readonly StoreNamedPropInfo MigrationLastSuccessfulSyncTime = new StoreNamedPropInfo("MigrationLastSuccessfulSyncTime", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, 35709U), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B76 RID: 7030
			public static readonly StoreNamedPropInfo OWANavigationNodeCalendarTypeFromOlderExchange = new StoreNamedPropInfo("OWANavigationNodeCalendarTypeFromOlderExchange", new StorePropName(NamedPropInfo.Sharing.NamespaceGuid, "OWA-NavigationNodeCalendarTypeFromOlderExchange"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000030 RID: 48
		public static class PostRss
		{
			// Token: 0x04001B77 RID: 7031
			public static readonly Guid NamespaceGuid = new Guid("00062041-0000-0000-C000-000000000046");

			// Token: 0x04001B78 RID: 7032
			public static readonly StoreNamedPropInfo PostRssChannelLink = new StoreNamedPropInfo("PostRssChannelLink", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35072U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B79 RID: 7033
			public static readonly StoreNamedPropInfo PostRssItemLink = new StoreNamedPropInfo("PostRssItemLink", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35073U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B7A RID: 7034
			public static readonly StoreNamedPropInfo PostRssItemHash = new StoreNamedPropInfo("PostRssItemHash", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35074U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B7B RID: 7035
			public static readonly StoreNamedPropInfo PostRssItemGuid = new StoreNamedPropInfo("PostRssItemGuid", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35075U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B7C RID: 7036
			public static readonly StoreNamedPropInfo PostRssChannel = new StoreNamedPropInfo("PostRssChannel", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35076U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B7D RID: 7037
			public static readonly StoreNamedPropInfo PostRssItemXml = new StoreNamedPropInfo("PostRssItemXml", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35077U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B7E RID: 7038
			public static readonly StoreNamedPropInfo PostRssSubscription = new StoreNamedPropInfo("PostRssSubscription", new StorePropName(NamedPropInfo.PostRss.NamespaceGuid, 35078U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000031 RID: 49
		public static class Note
		{
			// Token: 0x04001B7F RID: 7039
			public static readonly Guid NamespaceGuid = new Guid("00062008-000E-0000-C000-000000000046");

			// Token: 0x04001B80 RID: 7040
			public static readonly StoreNamedPropInfo NoteColor = new StoreNamedPropInfo("NoteColor", new StorePropName(NamedPropInfo.Note.NamespaceGuid, 35584U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B81 RID: 7041
			public static readonly StoreNamedPropInfo NoteOnTop = new StoreNamedPropInfo("NoteOnTop", new StorePropName(NamedPropInfo.Note.NamespaceGuid, 35585U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B82 RID: 7042
			public static readonly StoreNamedPropInfo NoteWidth = new StoreNamedPropInfo("NoteWidth", new StorePropName(NamedPropInfo.Note.NamespaceGuid, 35586U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B83 RID: 7043
			public static readonly StoreNamedPropInfo NoteHeight = new StoreNamedPropInfo("NoteHeight", new StorePropName(NamedPropInfo.Note.NamespaceGuid, 35587U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B84 RID: 7044
			public static readonly StoreNamedPropInfo NoteX = new StoreNamedPropInfo("NoteX", new StorePropName(NamedPropInfo.Note.NamespaceGuid, 35588U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B85 RID: 7045
			public static readonly StoreNamedPropInfo NoteY = new StoreNamedPropInfo("NoteY", new StorePropName(NamedPropInfo.Note.NamespaceGuid, 35589U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000032 RID: 50
		public static class OutlineView
		{
			// Token: 0x04001B86 RID: 7046
			public static readonly Guid NamespaceGuid = new Guid("00062061-0000-0000-C000-000000000046");
		}

		// Token: 0x02000033 RID: 51
		public static class MessageLookup
		{
			// Token: 0x04001B87 RID: 7047
			public static readonly Guid NamespaceGuid = new Guid("00062062-0000-0000-C000-000000000046");
		}

		// Token: 0x02000034 RID: 52
		public static class GenericView
		{
			// Token: 0x04001B88 RID: 7048
			public static readonly Guid NamespaceGuid = new Guid("00062063-0000-0000-C000-000000000046");
		}

		// Token: 0x02000035 RID: 53
		public static class InternetHeaders
		{
			// Token: 0x04001B89 RID: 7049
			public static readonly Guid NamespaceGuid = new Guid("00020386-0000-0000-C000-000000000046");

			// Token: 0x04001B8A RID: 7050
			public static readonly StoreNamedPropInfo AcceptLanguage = new StoreNamedPropInfo("AcceptLanguage", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "AcceptLanguage"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B8B RID: 7051
			public static readonly StoreNamedPropInfo Approved = new StoreNamedPropInfo("Approved", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Approved"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B8C RID: 7052
			public static readonly StoreNamedPropInfo Bcc = new StoreNamedPropInfo("Bcc", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Bcc"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B8D RID: 7053
			public static readonly StoreNamedPropInfo Cc = new StoreNamedPropInfo("Cc", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Cc"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B8E RID: 7054
			public static readonly StoreNamedPropInfo Comment = new StoreNamedPropInfo("Comment", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Comment"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B8F RID: 7055
			public static readonly StoreNamedPropInfo ContentBase = new StoreNamedPropInfo("ContentBase", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-Base"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B90 RID: 7056
			public static readonly StoreNamedPropInfo ContentDisposition = new StoreNamedPropInfo("ContentDisposition", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-Disposition"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B91 RID: 7057
			public static readonly StoreNamedPropInfo ContentId = new StoreNamedPropInfo("ContentId", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-ID"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B92 RID: 7058
			public static readonly StoreNamedPropInfo ContentLanguage = new StoreNamedPropInfo("ContentLanguage", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-Language"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B93 RID: 7059
			public static readonly StoreNamedPropInfo ContentLocation = new StoreNamedPropInfo("ContentLocation", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-Location"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B94 RID: 7060
			public static readonly StoreNamedPropInfo ContentTransferEncoding = new StoreNamedPropInfo("ContentTransferEncoding", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-Transfer-Encoding"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B95 RID: 7061
			public static readonly StoreNamedPropInfo ContentType = new StoreNamedPropInfo("ContentType", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Content-Type"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B96 RID: 7062
			public static readonly StoreNamedPropInfo Control = new StoreNamedPropInfo("Control", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Control"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B97 RID: 7063
			public static readonly StoreNamedPropInfo Disposition = new StoreNamedPropInfo("Disposition", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Disposition"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B98 RID: 7064
			public static readonly StoreNamedPropInfo DispositionNotificationOptions = new StoreNamedPropInfo("DispositionNotificationOptions", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Disposition-Notification-Options"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B99 RID: 7065
			public static readonly StoreNamedPropInfo DispositionNotificationTo = new StoreNamedPropInfo("DispositionNotificationTo", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Disposition-Notification-To"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B9A RID: 7066
			public static readonly StoreNamedPropInfo Distribution = new StoreNamedPropInfo("Distribution", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Distribution"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B9B RID: 7067
			public static readonly StoreNamedPropInfo Expires = new StoreNamedPropInfo("Expires", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Expires"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B9C RID: 7068
			public static readonly StoreNamedPropInfo ExpiryDate = new StoreNamedPropInfo("ExpiryDate", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Expiry-Date"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B9D RID: 7069
			public static readonly StoreNamedPropInfo FollowupTo = new StoreNamedPropInfo("FollowupTo", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Followup-To"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B9E RID: 7070
			public static readonly StoreNamedPropInfo From = new StoreNamedPropInfo("From", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "From"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001B9F RID: 7071
			public static readonly StoreNamedPropInfo Importance = new StoreNamedPropInfo("Importance", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Importance"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA0 RID: 7072
			public static readonly StoreNamedPropInfo InReplyTo = new StoreNamedPropInfo("InReplyTo", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "In-Reply-To"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA1 RID: 7073
			public static readonly StoreNamedPropInfo Keywords = new StoreNamedPropInfo("Keywords", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Keywords"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA2 RID: 7074
			public static readonly StoreNamedPropInfo MessageId = new StoreNamedPropInfo("MessageId", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Message-ID"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA3 RID: 7075
			public static readonly StoreNamedPropInfo MimeVersion = new StoreNamedPropInfo("MimeVersion", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Mime-Version"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA4 RID: 7076
			public static readonly StoreNamedPropInfo Newsgroups = new StoreNamedPropInfo("Newsgroups", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Newsgroups"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA5 RID: 7077
			public static readonly StoreNamedPropInfo NNTPPostingHost = new StoreNamedPropInfo("NNTPPostingHost", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "NNTP-Posting-Host"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA6 RID: 7078
			public static readonly StoreNamedPropInfo NNTPPostingUser = new StoreNamedPropInfo("NNTPPostingUser", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "NNTP-Posting-User"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA7 RID: 7079
			public static readonly StoreNamedPropInfo Organization = new StoreNamedPropInfo("Organization", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Organization"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA8 RID: 7080
			public static readonly StoreNamedPropInfo OriginalRecipient = new StoreNamedPropInfo("OriginalRecipient", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Original-Recipient"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BA9 RID: 7081
			public static readonly StoreNamedPropInfo Path = new StoreNamedPropInfo("Path", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Path"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BAA RID: 7082
			public static readonly StoreNamedPropInfo PostingVersion = new StoreNamedPropInfo("PostingVersion", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Posting-Version"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BAB RID: 7083
			public static readonly StoreNamedPropInfo Priority = new StoreNamedPropInfo("Priority", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Priority"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BAC RID: 7084
			public static readonly StoreNamedPropInfo Received = new StoreNamedPropInfo("Received", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Received"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BAD RID: 7085
			public static readonly StoreNamedPropInfo References = new StoreNamedPropInfo("References", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "References"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BAE RID: 7086
			public static readonly StoreNamedPropInfo RelayVersion = new StoreNamedPropInfo("RelayVersion", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Relay-Version"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BAF RID: 7087
			public static readonly StoreNamedPropInfo ReplyBy = new StoreNamedPropInfo("ReplyBy", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Reply-By"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB0 RID: 7088
			public static readonly StoreNamedPropInfo ReplyTo = new StoreNamedPropInfo("ReplyTo", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Reply-To"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB1 RID: 7089
			public static readonly StoreNamedPropInfo ReturnPath = new StoreNamedPropInfo("ReturnPath", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Return-Path"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB2 RID: 7090
			public static readonly StoreNamedPropInfo ReturnReceiptTo = new StoreNamedPropInfo("ReturnReceiptTo", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Return-Receipt-To"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB3 RID: 7091
			public static readonly StoreNamedPropInfo Sender = new StoreNamedPropInfo("Sender", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Sender"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB4 RID: 7092
			public static readonly StoreNamedPropInfo Sensitivity = new StoreNamedPropInfo("Sensitivity", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Sensitivity"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB5 RID: 7093
			public static readonly StoreNamedPropInfo Subject = new StoreNamedPropInfo("Subject", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Subject"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB6 RID: 7094
			public static readonly StoreNamedPropInfo Summary = new StoreNamedPropInfo("Summary", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Summary"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB7 RID: 7095
			public static readonly StoreNamedPropInfo ThreadIndex = new StoreNamedPropInfo("ThreadIndex", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Thread-Index"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB8 RID: 7096
			public static readonly StoreNamedPropInfo ThreadTopic = new StoreNamedPropInfo("ThreadTopic", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Thread-Topic"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BB9 RID: 7097
			public static readonly StoreNamedPropInfo To = new StoreNamedPropInfo("To", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "To"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BBA RID: 7098
			public static readonly StoreNamedPropInfo AttachmentOrder = new StoreNamedPropInfo("AttachmentOrder", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-attachmentorder"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BBB RID: 7099
			public static readonly StoreNamedPropInfo CallId = new StoreNamedPropInfo("CallId", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-callid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BBC RID: 7100
			public static readonly StoreNamedPropInfo CallingTelephone = new StoreNamedPropInfo("CallingTelephone", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-callingtelephonenumber"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BBD RID: 7101
			public static readonly StoreNamedPropInfo FaxNumberOfPages = new StoreNamedPropInfo("FaxNumberOfPages", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-faxnumberofpages"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BBE RID: 7102
			public static readonly StoreNamedPropInfo XListUnsubscribe = new StoreNamedPropInfo("XListUnsubscribe", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-List-Unsubscribe"), PropertyType.Invalid, 9223372036854775808UL, new PropertyCategories(1, 2));

			// Token: 0x04001BBF RID: 7103
			public static readonly StoreNamedPropInfo XMailer = new StoreNamedPropInfo("XMailer", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Mailer"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC0 RID: 7104
			public static readonly StoreNamedPropInfo MessageCompleted = new StoreNamedPropInfo("MessageCompleted", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Message-Completed"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC1 RID: 7105
			public static readonly StoreNamedPropInfo XMessageFlag = new StoreNamedPropInfo("XMessageFlag", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Message-Flag"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC2 RID: 7106
			public static readonly StoreNamedPropInfo AuthAs = new StoreNamedPropInfo("AuthAs", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-AuthAs"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC3 RID: 7107
			public static readonly StoreNamedPropInfo AuthDomain = new StoreNamedPropInfo("AuthDomain", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-AuthDomain"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC4 RID: 7108
			public static readonly StoreNamedPropInfo AuthMechanism = new StoreNamedPropInfo("AuthMechanism", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-AuthMechanism"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC5 RID: 7109
			public static readonly StoreNamedPropInfo AuthSource = new StoreNamedPropInfo("AuthSource", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-AuthSource"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC6 RID: 7110
			public static readonly StoreNamedPropInfo JournalReport = new StoreNamedPropInfo("JournalReport", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Journal-Report"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC7 RID: 7111
			public static readonly StoreNamedPropInfo TNEFCorrelator = new StoreNamedPropInfo("TNEFCorrelator", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-TNEF-Correlator"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC8 RID: 7112
			public static readonly StoreNamedPropInfo Xref = new StoreNamedPropInfo("Xref", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Xref"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BC9 RID: 7113
			public static readonly StoreNamedPropInfo SharingBrowseUrl = new StoreNamedPropInfo("SharingBrowseUrl", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Browse-Url"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BCA RID: 7114
			public static readonly StoreNamedPropInfo SharingCapacilities = new StoreNamedPropInfo("SharingCapacilities", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Capabilities"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BCB RID: 7115
			public static readonly StoreNamedPropInfo SharingConfigUrl = new StoreNamedPropInfo("SharingConfigUrl", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Config-Url"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BCC RID: 7116
			public static readonly StoreNamedPropInfo ExtendedCaps = new StoreNamedPropInfo("ExtendedCaps", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Exended-Caps"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BCD RID: 7117
			public static readonly StoreNamedPropInfo SharingFlavor = new StoreNamedPropInfo("SharingFlavor", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Flavor"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BCE RID: 7118
			public static readonly StoreNamedPropInfo SharingInstanceGuid = new StoreNamedPropInfo("SharingInstanceGuid", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Instance-Guid"), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BCF RID: 7119
			public static readonly StoreNamedPropInfo SharingLocalType = new StoreNamedPropInfo("SharingLocalType", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Local-Type"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD0 RID: 7120
			public static readonly StoreNamedPropInfo SharingProviderGuid = new StoreNamedPropInfo("SharingProviderGuid", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Provider-Guid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD1 RID: 7121
			public static readonly StoreNamedPropInfo SharingProviderName = new StoreNamedPropInfo("SharingProviderName", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Provider-Name"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD2 RID: 7122
			public static readonly StoreNamedPropInfo SharingProviderUrl = new StoreNamedPropInfo("SharingProviderUrl", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Provider-Url"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD3 RID: 7123
			public static readonly StoreNamedPropInfo SharingRemoteName = new StoreNamedPropInfo("SharingRemoteName", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Remote-Name"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD4 RID: 7124
			public static readonly StoreNamedPropInfo SharingRemotePath = new StoreNamedPropInfo("SharingRemotePath", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Remote-Path"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD5 RID: 7125
			public static readonly StoreNamedPropInfo SharingRemoteStoreUid = new StoreNamedPropInfo("SharingRemoteStoreUid", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Remote-Store-Uid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD6 RID: 7126
			public static readonly StoreNamedPropInfo SharingRemoteType = new StoreNamedPropInfo("SharingRemoteType", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Remote-Type"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD7 RID: 7127
			public static readonly StoreNamedPropInfo SharingRemoteUid = new StoreNamedPropInfo("SharingRemoteUid", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Sharing-Remote-Uid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD8 RID: 7128
			public static readonly StoreNamedPropInfo XUnsent = new StoreNamedPropInfo("XUnsent", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-Unsent"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BD9 RID: 7129
			public static readonly StoreNamedPropInfo VoiceMessageDuration = new StoreNamedPropInfo("VoiceMessageDuration", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-voicemessageduration"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BDA RID: 7130
			public static readonly StoreNamedPropInfo VoiceMessageSenderName = new StoreNamedPropInfo("VoiceMessageSenderName", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-voicemessagesendername"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BDB RID: 7131
			public static readonly StoreNamedPropInfo MsgSubmitClientIp = new StoreNamedPropInfo("MsgSubmitClientIp", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-OriginalClientIPAddress"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BDC RID: 7132
			public static readonly StoreNamedPropInfo XMsExchOrganizationOriginalServerIPAddress = new StoreNamedPropInfo("XMsExchOrganizationOriginalServerIPAddress", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-OriginalServerIPAddress"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BDD RID: 7133
			public static readonly StoreNamedPropInfo XRequireProtectedPlayOnPhone = new StoreNamedPropInfo("XRequireProtectedPlayOnPhone", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-requireprotectedplayonphone"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BDE RID: 7134
			public static readonly StoreNamedPropInfo ContentClass = new StoreNamedPropInfo("ContentClass", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "content-class"), PropertyType.Unicode, 128UL, default(PropertyCategories));

			// Token: 0x04001BDF RID: 7135
			public static readonly StoreNamedPropInfo Lines = new StoreNamedPropInfo("Lines", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "Lines"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE0 RID: 7136
			public static readonly StoreNamedPropInfo XMSExchangeOrganizationApprovalAllowedDecisionMakers = new StoreNamedPropInfo("XMSExchangeOrganizationApprovalAllowedDecisionMakers", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-Approval-Allowed-Decision-Makers"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE1 RID: 7137
			public static readonly StoreNamedPropInfo XMSExchangeOrganizationApprovalRequestor = new StoreNamedPropInfo("XMSExchangeOrganizationApprovalRequestor", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-Approval-Requestor"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE2 RID: 7138
			public static readonly StoreNamedPropInfo XMSExchangeOrganizationRightsProtectMessage = new StoreNamedPropInfo("XMSExchangeOrganizationRightsProtectMessage", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "X-MS-Exchange-Organization-RightsProtectMessage"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE3 RID: 7139
			public static readonly StoreNamedPropInfo XMSHasAttach = new StoreNamedPropInfo("XMSHasAttach", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-ms-has-attach"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE4 RID: 7140
			public static readonly StoreNamedPropInfo XMSExchOrganizationAuthDomain = new StoreNamedPropInfo("XMSExchOrganizationAuthDomain", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "x-ms-exchange-organization-authdomain"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE5 RID: 7141
			public static readonly StoreNamedPropInfo DlpSenderOverride = new StoreNamedPropInfo("DlpSenderOverride", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "DlpSenderOverride"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE6 RID: 7142
			public static readonly StoreNamedPropInfo DlpFalsePositive = new StoreNamedPropInfo("DlpFalsePositive", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "DlpFalsePositive"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BE7 RID: 7143
			public static readonly StoreNamedPropInfo DlpDetectedClassifications = new StoreNamedPropInfo("DlpDetectedClassifications", new StorePropName(NamedPropInfo.InternetHeaders.NamespaceGuid, "DlpDetectedClassifications"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000036 RID: 54
		public static class ExternalSharing
		{
			// Token: 0x04001BE8 RID: 7144
			public static readonly Guid NamespaceGuid = new Guid("F52A8693-C34D-4980-9E20-9D4C1EABB6A7");

			// Token: 0x04001BE9 RID: 7145
			public static readonly StoreNamedPropInfo DataType = new StoreNamedPropInfo("DataType", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingDataType"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BEA RID: 7146
			public static readonly StoreNamedPropInfo IsPrimary = new StoreNamedPropInfo("IsPrimary", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingIsPrimary"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BEB RID: 7147
			public static readonly StoreNamedPropInfo LastSuccessfulSyncTime = new StoreNamedPropInfo("LastSuccessfulSyncTime", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingLastSuccessfulSyncTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BEC RID: 7148
			public static readonly StoreNamedPropInfo LevelOfDetails = new StoreNamedPropInfo("LevelOfDetails", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingLevelOfDetails"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BED RID: 7149
			public static readonly StoreNamedPropInfo LocalFolderId = new StoreNamedPropInfo("LocalFolderId", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingLocalFolderId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BEE RID: 7150
			public static readonly StoreNamedPropInfo MasterId = new StoreNamedPropInfo("MasterId", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingMasterId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BEF RID: 7151
			public static readonly StoreNamedPropInfo RemoteFolderId = new StoreNamedPropInfo("RemoteFolderId", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingRemoteFolderId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF0 RID: 7152
			public static readonly StoreNamedPropInfo RemoteFolderName = new StoreNamedPropInfo("RemoteFolderName", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingRemoteFolderName"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF1 RID: 7153
			public static readonly StoreNamedPropInfo SharerIdentity = new StoreNamedPropInfo("SharerIdentity", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingSharerIdentity"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF2 RID: 7154
			public static readonly StoreNamedPropInfo SharerIdentityFederationUri = new StoreNamedPropInfo("SharerIdentityFederationUri", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingSharerIdentityFederationUri"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF3 RID: 7155
			public static readonly StoreNamedPropInfo SharerName = new StoreNamedPropInfo("SharerName", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingSharerName"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF4 RID: 7156
			public static readonly StoreNamedPropInfo SharingKey = new StoreNamedPropInfo("SharingKey", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingSharingKey"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF5 RID: 7157
			public static readonly StoreNamedPropInfo SubscriberIdentity = new StoreNamedPropInfo("SubscriberIdentity", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingSubscriberIdentity"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF6 RID: 7158
			public static readonly StoreNamedPropInfo SyncState = new StoreNamedPropInfo("SyncState", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingSyncState"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BF7 RID: 7159
			public static readonly StoreNamedPropInfo Url = new StoreNamedPropInfo("Url", new StorePropName(NamedPropInfo.ExternalSharing.NamespaceGuid, "ExternalSharingUrl"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000037 RID: 55
		public static class Meeting
		{
			// Token: 0x04001BF8 RID: 7160
			public static readonly Guid NamespaceGuid = new Guid("6ED8DA90-450B-101B-98DA-00AA003F1305");

			// Token: 0x04001BF9 RID: 7161
			public static readonly StoreNamedPropInfo AttendeeCriticalChange = new StoreNamedPropInfo("AttendeeCriticalChange", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 1U), PropertyType.SysTime, 8192UL, default(PropertyCategories));

			// Token: 0x04001BFA RID: 7162
			public static readonly StoreNamedPropInfo Where = new StoreNamedPropInfo("Where", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 2U), PropertyType.Unicode, 131072UL, default(PropertyCategories));

			// Token: 0x04001BFB RID: 7163
			public static readonly StoreNamedPropInfo GlobalObjId = new StoreNamedPropInfo("GlobalObjId", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 3U), PropertyType.Binary, 16384UL, default(PropertyCategories));

			// Token: 0x04001BFC RID: 7164
			public static readonly StoreNamedPropInfo IsSilent = new StoreNamedPropInfo("IsSilent", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BFD RID: 7165
			public static readonly StoreNamedPropInfo IsRecurring = new StoreNamedPropInfo("IsRecurring", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 5U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BFE RID: 7166
			public static readonly StoreNamedPropInfo RequiredAttendees = new StoreNamedPropInfo("RequiredAttendees", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 6U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001BFF RID: 7167
			public static readonly StoreNamedPropInfo OptionalAttendees = new StoreNamedPropInfo("OptionalAttendees", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 7U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C00 RID: 7168
			public static readonly StoreNamedPropInfo ResourceAttendees = new StoreNamedPropInfo("ResourceAttendees", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 8U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C01 RID: 7169
			public static readonly StoreNamedPropInfo DelegateMail = new StoreNamedPropInfo("DelegateMail", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 9U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C02 RID: 7170
			public static readonly StoreNamedPropInfo IsException = new StoreNamedPropInfo("IsException", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 10U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C03 RID: 7171
			public static readonly StoreNamedPropInfo SingleInvite = new StoreNamedPropInfo("SingleInvite", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 11U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C04 RID: 7172
			public static readonly StoreNamedPropInfo TimeZone = new StoreNamedPropInfo("TimeZone", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 12U), PropertyType.Int32, 524288UL, default(PropertyCategories));

			// Token: 0x04001C05 RID: 7173
			public static readonly StoreNamedPropInfo StartRecurDate = new StoreNamedPropInfo("StartRecurDate", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 13U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C06 RID: 7174
			public static readonly StoreNamedPropInfo StartRecurTime = new StoreNamedPropInfo("StartRecurTime", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 14U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C07 RID: 7175
			public static readonly StoreNamedPropInfo EndRecurDate = new StoreNamedPropInfo("EndRecurDate", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 15U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C08 RID: 7176
			public static readonly StoreNamedPropInfo EndRecurTime = new StoreNamedPropInfo("EndRecurTime", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 16U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C09 RID: 7177
			public static readonly StoreNamedPropInfo DayInterval = new StoreNamedPropInfo("DayInterval", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 17U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C0A RID: 7178
			public static readonly StoreNamedPropInfo WeekInterval = new StoreNamedPropInfo("WeekInterval", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 18U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C0B RID: 7179
			public static readonly StoreNamedPropInfo MonthInterval = new StoreNamedPropInfo("MonthInterval", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 19U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C0C RID: 7180
			public static readonly StoreNamedPropInfo YearInterval = new StoreNamedPropInfo("YearInterval", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 20U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C0D RID: 7181
			public static readonly StoreNamedPropInfo DowMask = new StoreNamedPropInfo("DowMask", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 21U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C0E RID: 7182
			public static readonly StoreNamedPropInfo DomMask = new StoreNamedPropInfo("DomMask", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 22U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C0F RID: 7183
			public static readonly StoreNamedPropInfo MoyMask = new StoreNamedPropInfo("MoyMask", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 23U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C10 RID: 7184
			public static readonly StoreNamedPropInfo MtgRecurType = new StoreNamedPropInfo("MtgRecurType", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 24U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C11 RID: 7185
			public static readonly StoreNamedPropInfo DowPref = new StoreNamedPropInfo("DowPref", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 25U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C12 RID: 7186
			public static readonly StoreNamedPropInfo OwnerCriticalChange = new StoreNamedPropInfo("OwnerCriticalChange", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 26U), PropertyType.SysTime, 2048UL, default(PropertyCategories));

			// Token: 0x04001C13 RID: 7187
			public static readonly StoreNamedPropInfo LID_WANT_SILENT_RESP = new StoreNamedPropInfo("LID_WANT_SILENT_RESP", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 27U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C14 RID: 7188
			public static readonly StoreNamedPropInfo CalendarType = new StoreNamedPropInfo("CalendarType", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 28U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C15 RID: 7189
			public static readonly StoreNamedPropInfo AllAttendeesList = new StoreNamedPropInfo("AllAttendeesList", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 29U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C16 RID: 7190
			public static readonly StoreNamedPropInfo ResponseState = new StoreNamedPropInfo("ResponseState", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 33U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C17 RID: 7191
			public static readonly StoreNamedPropInfo WhenProp = new StoreNamedPropInfo("WhenProp", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 34U), PropertyType.Unicode, 144115188075872256UL, default(PropertyCategories));

			// Token: 0x04001C18 RID: 7192
			public static readonly StoreNamedPropInfo CleanGlobalObjId = new StoreNamedPropInfo("CleanGlobalObjId", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 35U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C19 RID: 7193
			public static readonly StoreNamedPropInfo ApptMessageClass = new StoreNamedPropInfo("ApptMessageClass", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 36U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C1A RID: 7194
			public static readonly StoreNamedPropInfo ProposedWhenProp = new StoreNamedPropInfo("ProposedWhenProp", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 37U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C1B RID: 7195
			public static readonly StoreNamedPropInfo MeetingType = new StoreNamedPropInfo("MeetingType", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 38U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C1C RID: 7196
			public static readonly StoreNamedPropInfo OldWhen = new StoreNamedPropInfo("OldWhen", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 39U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C1D RID: 7197
			public static readonly StoreNamedPropInfo OldLocation = new StoreNamedPropInfo("OldLocation", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 40U), PropertyType.Unicode, 131072UL, default(PropertyCategories));

			// Token: 0x04001C1E RID: 7198
			public static readonly StoreNamedPropInfo OldWhenStartWhole = new StoreNamedPropInfo("OldWhenStartWhole", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 41U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C1F RID: 7199
			public static readonly StoreNamedPropInfo OldWhenEndWhole = new StoreNamedPropInfo("OldWhenEndWhole", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 42U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C20 RID: 7200
			public static readonly StoreNamedPropInfo ProsedWhenProp = new StoreNamedPropInfo("ProsedWhenProp", new StorePropName(NamedPropInfo.Meeting.NamespaceGuid, 55U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000038 RID: 56
		public static class IMAPStore
		{
			// Token: 0x04001C21 RID: 7201
			public static readonly Guid NamespaceGuid = new Guid("29F3AB55-554D-11D0-A97C-00A0C911F50A");

			// Token: 0x04001C22 RID: 7202
			public static readonly StoreNamedPropInfo Acct = new StoreNamedPropInfo("Acct", new StorePropName(NamedPropInfo.IMAPStore.NamespaceGuid, 41072U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C23 RID: 7203
			public static readonly StoreNamedPropInfo Prefix = new StoreNamedPropInfo("Prefix", new StorePropName(NamedPropInfo.IMAPStore.NamespaceGuid, 41073U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C24 RID: 7204
			public static readonly StoreNamedPropInfo OfflineMsg = new StoreNamedPropInfo("OfflineMsg", new StorePropName(NamedPropInfo.IMAPStore.NamespaceGuid, 41074U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C25 RID: 7205
			public static readonly StoreNamedPropInfo OfflineChgNum = new StoreNamedPropInfo("OfflineChgNum", new StorePropName(NamedPropInfo.IMAPStore.NamespaceGuid, 41075U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C26 RID: 7206
			public static readonly StoreNamedPropInfo OfflineFldrs = new StoreNamedPropInfo("OfflineFldrs", new StorePropName(NamedPropInfo.IMAPStore.NamespaceGuid, 41076U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000039 RID: 57
		public static class IMAPMsg
		{
			// Token: 0x04001C27 RID: 7207
			public static readonly Guid NamespaceGuid = new Guid("29F3AB53-554D-11D0-A97C-00A0C911F50A");

			// Token: 0x04001C28 RID: 7208
			public static readonly StoreNamedPropInfo UID = new StoreNamedPropInfo("UID", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, 41024U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C29 RID: 7209
			public static readonly StoreNamedPropInfo Flags = new StoreNamedPropInfo("Flags", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, 41025U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C2A RID: 7210
			public static readonly StoreNamedPropInfo State = new StoreNamedPropInfo("State", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, 41027U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C2B RID: 7211
			public static readonly StoreNamedPropInfo GUID = new StoreNamedPropInfo("GUID", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, 41028U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C2C RID: 7212
			public static readonly StoreNamedPropInfo OfflineChgs = new StoreNamedPropInfo("OfflineChgs", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, 41029U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C2D RID: 7213
			public static readonly StoreNamedPropInfo Headers = new StoreNamedPropInfo("Headers", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, 41030U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C2E RID: 7214
			public static readonly StoreNamedPropInfo ImapMIMEOptions = new StoreNamedPropInfo("ImapMIMEOptions", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, "PopImap:ImapMIMEOptions"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C2F RID: 7215
			public static readonly StoreNamedPropInfo ImapMIMESize = new StoreNamedPropInfo("ImapMIMESize", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, "PopImap:ImapMIMESize"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C30 RID: 7216
			public static readonly StoreNamedPropInfo PopMIMEOptions = new StoreNamedPropInfo("PopMIMEOptions", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, "PopImap:PopMIMEOptions"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C31 RID: 7217
			public static readonly StoreNamedPropInfo PopMIMESize = new StoreNamedPropInfo("PopMIMESize", new StorePropName(NamedPropInfo.IMAPMsg.NamespaceGuid, "PopImap:PopMIMESize"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200003A RID: 58
		public static class IMAPFold
		{
			// Token: 0x04001C32 RID: 7218
			public static readonly Guid NamespaceGuid = new Guid("29F3AB52-554D-11D0-A97C-00A0C911F50A");

			// Token: 0x04001C33 RID: 7219
			public static readonly StoreNamedPropInfo UIDValidity = new StoreNamedPropInfo("UIDValidity", new StorePropName(NamedPropInfo.IMAPFold.NamespaceGuid, 40993U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C34 RID: 7220
			public static readonly StoreNamedPropInfo NextUID = new StoreNamedPropInfo("NextUID", new StorePropName(NamedPropInfo.IMAPFold.NamespaceGuid, 40994U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C35 RID: 7221
			public static readonly StoreNamedPropInfo Sep = new StoreNamedPropInfo("Sep", new StorePropName(NamedPropInfo.IMAPFold.NamespaceGuid, 40996U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C36 RID: 7222
			public static readonly StoreNamedPropInfo Flags = new StoreNamedPropInfo("Flags", new StorePropName(NamedPropInfo.IMAPFold.NamespaceGuid, 40997U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C37 RID: 7223
			public static readonly StoreNamedPropInfo PendingAppend = new StoreNamedPropInfo("PendingAppend", new StorePropName(NamedPropInfo.IMAPFold.NamespaceGuid, 40998U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200003B RID: 59
		public static class Proxy
		{
			// Token: 0x04001C38 RID: 7224
			public static readonly Guid NamespaceGuid = new Guid("29F3AB56-554D-11D0-A97C-00A0C911F50A");

			// Token: 0x04001C39 RID: 7225
			public static readonly StoreNamedPropInfo ObjType = new StoreNamedPropInfo("ObjType", new StorePropName(NamedPropInfo.Proxy.NamespaceGuid, 40960U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200003C RID: 60
		public static class AirSync
		{
			// Token: 0x04001C3A RID: 7226
			public static readonly Guid NamespaceGuid = new Guid("71035549-0739-4DCB-9163-00F0580DBBDF");

			// Token: 0x04001C3B RID: 7227
			public static readonly StoreNamedPropInfo ASDeletedCountTotal = new StoreNamedPropInfo("ASDeletedCountTotal", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncDeletedCountTotal"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C3C RID: 7228
			public static readonly StoreNamedPropInfo ASFilter = new StoreNamedPropInfo("ASFilter", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncFilter"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C3D RID: 7229
			public static readonly StoreNamedPropInfo ASLastSyncDay = new StoreNamedPropInfo("ASLastSyncDay", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLastSyncDay"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C3E RID: 7230
			public static readonly StoreNamedPropInfo ASLocalCommitTimeMax = new StoreNamedPropInfo("ASLocalCommitTimeMax", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLocalCommitTimeMax"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C3F RID: 7231
			public static readonly StoreNamedPropInfo ASStoreObjectId = new StoreNamedPropInfo("ASStoreObjectId", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncStoreObectIdProperty"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C40 RID: 7232
			public static readonly StoreNamedPropInfo ASSyncKey = new StoreNamedPropInfo("ASSyncKey", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncSynckey"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C41 RID: 7233
			public static readonly StoreNamedPropInfo ASIMAddress2 = new StoreNamedPropInfo("ASIMAddress2", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:IMAddress2"), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001C42 RID: 7234
			public static readonly StoreNamedPropInfo ASIMAddress3 = new StoreNamedPropInfo("ASIMAddress3", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:IMAddress3"), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001C43 RID: 7235
			public static readonly StoreNamedPropInfo ASMMS = new StoreNamedPropInfo("ASMMS", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:MMS"), PropertyType.Unicode, 256UL, default(PropertyCategories));

			// Token: 0x04001C44 RID: 7236
			public static readonly StoreNamedPropInfo ASMaxItems = new StoreNamedPropInfo("ASMaxItems", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncMaxItems"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C45 RID: 7237
			public static readonly StoreNamedPropInfo ASConversationMode = new StoreNamedPropInfo("ASConversationMode", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncConversationMode"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C46 RID: 7238
			public static readonly StoreNamedPropInfo ASSettingsHash = new StoreNamedPropInfo("ASSettingsHash", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncSettingsHash"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C47 RID: 7239
			public static readonly StoreNamedPropInfo ASLastSyncTime = new StoreNamedPropInfo("ASLastSyncTime", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLastSyncTime"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C48 RID: 7240
			public static readonly StoreNamedPropInfo ASClientCategoryList = new StoreNamedPropInfo("ASClientCategoryList", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncClientCategoryList"), PropertyType.MVInt32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C49 RID: 7241
			public static readonly StoreNamedPropInfo ASLastSeenClientIds = new StoreNamedPropInfo("ASLastSeenClientIds", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLastSeenClientIds"), PropertyType.MVUnicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C4A RID: 7242
			public static readonly StoreNamedPropInfo ASLastSyncAttemptTime = new StoreNamedPropInfo("ASLastSyncAttemptTime", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLastSyncAttemptTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C4B RID: 7243
			public static readonly StoreNamedPropInfo ASLastSyncSuccessTime = new StoreNamedPropInfo("ASLastSyncSuccessTime", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLastSyncSuccessTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C4C RID: 7244
			public static readonly StoreNamedPropInfo ASLastSyncUserAgent = new StoreNamedPropInfo("ASLastSyncUserAgent", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:AirSyncLastSyncUserAgent"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C4D RID: 7245
			public static readonly StoreNamedPropInfo ASLastPingHeartbeatInterval = new StoreNamedPropInfo("ASLastPingHeartbeatInterval", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:LastPingHeartbeatInterval"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C4E RID: 7246
			public static readonly StoreNamedPropInfo ASDeviceBlockedUntil = new StoreNamedPropInfo("ASDeviceBlockedUntil", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:DeviceBlockedUntil"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C4F RID: 7247
			public static readonly StoreNamedPropInfo ASDeviceBlockedAt = new StoreNamedPropInfo("ASDeviceBlockedAt", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:DeviceBlockedAt"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C50 RID: 7248
			public static readonly StoreNamedPropInfo ASDeviceBlockedReason = new StoreNamedPropInfo("ASDeviceBlockedReason", new StorePropName(NamedPropInfo.AirSync.NamespaceGuid, "AirSync:DeviceBlockedReason"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200003D RID: 61
		public static class UnifiedMessaging
		{
			// Token: 0x04001C51 RID: 7249
			public static readonly Guid NamespaceGuid = new Guid("4442858E-A9E3-4E80-B900-317A210CC15B");

			// Token: 0x04001C52 RID: 7250
			public static readonly StoreNamedPropInfo MsExchangeUMPartnerContent = new StoreNamedPropInfo("MsExchangeUMPartnerContent", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "x-ms-exchange-um-partnercontent"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C53 RID: 7251
			public static readonly StoreNamedPropInfo MsExchangeUMPartnerContext = new StoreNamedPropInfo("MsExchangeUMPartnerContext", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "x-ms-exchange-um-partnercontext"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C54 RID: 7252
			public static readonly StoreNamedPropInfo MsExchangeUMPartnerStatus = new StoreNamedPropInfo("MsExchangeUMPartnerStatus", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "x-ms-exchange-um-partnerstatus"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C55 RID: 7253
			public static readonly StoreNamedPropInfo MsExchangeUMPartnerAssignedID = new StoreNamedPropInfo("MsExchangeUMPartnerAssignedID", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "x-ms-exchange-um-partnerassignedid"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C56 RID: 7254
			public static readonly StoreNamedPropInfo MsExchangeUMDialPlanLanguage = new StoreNamedPropInfo("MsExchangeUMDialPlanLanguage", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "x-ms-exchange-um-dialplanlanguage"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C57 RID: 7255
			public static readonly StoreNamedPropInfo MsExchangeUMCallerInformedOfAnalysis = new StoreNamedPropInfo("MsExchangeUMCallerInformedOfAnalysis", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "x-ms-exchange-um-callerinformedofanalysis"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C58 RID: 7256
			public static readonly StoreNamedPropInfo UnifiedMessagingOptions = new StoreNamedPropInfo("UnifiedMessagingOptions", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, 1U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C59 RID: 7257
			public static readonly StoreNamedPropInfo OfficeCommunicatorOptions = new StoreNamedPropInfo("OfficeCommunicatorOptions", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, 2U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C5A RID: 7258
			public static readonly StoreNamedPropInfo UMAudioNotes = new StoreNamedPropInfo("UMAudioNotes", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "UMAudioNotes"), PropertyType.Unicode, 2305843009213694208UL, default(PropertyCategories));

			// Token: 0x04001C5B RID: 7259
			public static readonly StoreNamedPropInfo OutlookProtectionRuleAddinVersion = new StoreNamedPropInfo("OutlookProtectionRuleAddinVersion", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Addin-Version"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C5C RID: 7260
			public static readonly StoreNamedPropInfo OutlookProtectionRuleConfigTimestamp = new StoreNamedPropInfo("OutlookProtectionRuleConfigTimestamp", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Config-Timestamp"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C5D RID: 7261
			public static readonly StoreNamedPropInfo OutlookProtectionRuleOverridden = new StoreNamedPropInfo("OutlookProtectionRuleOverridden", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "X-MS-Exchange-Organization-Outlook-Protection-Rule-Overridden"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C5E RID: 7262
			public static readonly StoreNamedPropInfo IsVoiceReminderEnabled = new StoreNamedPropInfo("IsVoiceReminderEnabled", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "IsVoiceReminderEnabled"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C5F RID: 7263
			public static readonly StoreNamedPropInfo VoiceReminderPhoneNumber = new StoreNamedPropInfo("VoiceReminderPhoneNumber", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "VoiceReminderPhoneNumber"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C60 RID: 7264
			public static readonly StoreNamedPropInfo PstnCallbackTelephoneNumber = new StoreNamedPropInfo("PstnCallbackTelephoneNumber", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "PstnCallbackTelephoneNumber"), PropertyType.Unicode, 128UL, default(PropertyCategories));

			// Token: 0x04001C61 RID: 7265
			public static readonly StoreNamedPropInfo XMSExchangeUMPartnerContent = new StoreNamedPropInfo("XMSExchangeUMPartnerContent", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "X-MS-Exchange-UM-PartnerContent"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C62 RID: 7266
			public static readonly StoreNamedPropInfo XMSExchangeUMPartnerStatus = new StoreNamedPropInfo("XMSExchangeUMPartnerStatus", new StorePropName(NamedPropInfo.UnifiedMessaging.NamespaceGuid, "X-MS-Exchange-UM-PartnerStatus"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200003E RID: 62
		public static class Elc
		{
			// Token: 0x04001C63 RID: 7267
			public static readonly Guid NamespaceGuid = new Guid("C7A4569B-F7AE-4DC2-9279-A8FE2F3CAF89");

			// Token: 0x04001C64 RID: 7268
			public static readonly StoreNamedPropInfo ElcFolderLocalizedName = new StoreNamedPropInfo("ElcFolderLocalizedName", new StorePropName(NamedPropInfo.Elc.NamespaceGuid, "ElcFolderLocalizedName"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C65 RID: 7269
			public static readonly StoreNamedPropInfo ElcExplicitPolicyTag = new StoreNamedPropInfo("ElcExplicitPolicyTag", new StorePropName(NamedPropInfo.Elc.NamespaceGuid, "ExplicitPolicyTag"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C66 RID: 7270
			public static readonly StoreNamedPropInfo ElcExplicitArchiveTag = new StoreNamedPropInfo("ElcExplicitArchiveTag", new StorePropName(NamedPropInfo.Elc.NamespaceGuid, "ExplicitArchiveTag"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200003F RID: 63
		public static class Attachment
		{
			// Token: 0x04001C67 RID: 7271
			public static readonly Guid NamespaceGuid = new Guid("96357F7F-59E1-47D0-99A7-46515C183B54");

			// Token: 0x04001C68 RID: 7272
			public static readonly StoreNamedPropInfo MacContentType = new StoreNamedPropInfo("MacContentType", new StorePropName(NamedPropInfo.Attachment.NamespaceGuid, "AttachmentMacContentType"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C69 RID: 7273
			public static readonly StoreNamedPropInfo MacInfo = new StoreNamedPropInfo("MacInfo", new StorePropName(NamedPropInfo.Attachment.NamespaceGuid, "AttachmentMacInfo"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C6A RID: 7274
			public static readonly StoreNamedPropInfo ProviderEndpointUrl = new StoreNamedPropInfo("ProviderEndpointUrl", new StorePropName(NamedPropInfo.Attachment.NamespaceGuid, "AttachmentProviderEndpointUrl"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C6B RID: 7275
			public static readonly StoreNamedPropInfo ProviderType = new StoreNamedPropInfo("ProviderType", new StorePropName(NamedPropInfo.Attachment.NamespaceGuid, "AttachmentProviderType"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000040 RID: 64
		public static class CalendarAssistant
		{
			// Token: 0x04001C6C RID: 7276
			public static readonly Guid NamespaceGuid = new Guid("11000E07-B51B-40D6-AF21-CAA85EDAB1D0");

			// Token: 0x04001C6D RID: 7277
			public static readonly StoreNamedPropInfo ViewStartTime = new StoreNamedPropInfo("ViewStartTime", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 33U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C6E RID: 7278
			public static readonly StoreNamedPropInfo ViewEndTime = new StoreNamedPropInfo("ViewEndTime", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 34U), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C6F RID: 7279
			public static readonly StoreNamedPropInfo CalendarFolderVersion = new StoreNamedPropInfo("CalendarFolderVersion", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 35U), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C70 RID: 7280
			public static readonly StoreNamedPropInfo CharmId = new StoreNamedPropInfo("CharmId", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 37U), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C71 RID: 7281
			public static readonly StoreNamedPropInfo OccurrencesExceptionalViewProperties = new StoreNamedPropInfo("OccurrencesExceptionalViewProperties", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "OccurrencesExceptionalViewProperties"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C72 RID: 7282
			public static readonly StoreNamedPropInfo SeriesSequenceNumber = new StoreNamedPropInfo("SeriesSequenceNumber", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "SeriesSequenceNumber"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C73 RID: 7283
			public static readonly StoreNamedPropInfo CalendarInteropActionQueueInternal = new StoreNamedPropInfo("CalendarInteropActionQueueInternal", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "CalendarInteropActions"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C74 RID: 7284
			public static readonly StoreNamedPropInfo CalendarInteropActionQueueHasDataInternal = new StoreNamedPropInfo("CalendarInteropActionQueueHasDataInternal", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "CalendarInteropActionsAvailable"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C75 RID: 7285
			public static readonly StoreNamedPropInfo LastExecutedCalendarInteropAction = new StoreNamedPropInfo("LastExecutedCalendarInteropAction", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "LastExecutedCalendarInteropAction"), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C76 RID: 7286
			public static readonly StoreNamedPropInfo PropertyChangeMetadataProcessingFlags = new StoreNamedPropInfo("PropertyChangeMetadataProcessingFlags", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "PropertyChangeMetadataProcessingFlags"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C77 RID: 7287
			public static readonly StoreNamedPropInfo MasterGlobalObjectId = new StoreNamedPropInfo("MasterGlobalObjectId", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "MasterGlobalObjectId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C78 RID: 7288
			public static readonly StoreNamedPropInfo ParkedCorrelationId = new StoreNamedPropInfo("ParkedCorrelationId", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "ParkedCorrelationId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C79 RID: 7289
			public static readonly StoreNamedPropInfo CalendarProcessed = new StoreNamedPropInfo("CalendarProcessed", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 1U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C7A RID: 7290
			public static readonly StoreNamedPropInfo CalendarLastChangeAction = new StoreNamedPropInfo("CalendarLastChangeAction", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 2U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C7B RID: 7291
			public static readonly StoreNamedPropInfo ChangeList = new StoreNamedPropInfo("ChangeList", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 5U), PropertyType.Binary, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C7C RID: 7292
			public static readonly StoreNamedPropInfo CalendarLogTriggerAction = new StoreNamedPropInfo("CalendarLogTriggerAction", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 6U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C7D RID: 7293
			public static readonly StoreNamedPropInfo OriginalFolderId = new StoreNamedPropInfo("OriginalFolderId", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 7U), PropertyType.Binary, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C7E RID: 7294
			public static readonly StoreNamedPropInfo OriginalCreationTime = new StoreNamedPropInfo("OriginalCreationTime", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 8U), PropertyType.SysTime, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C7F RID: 7295
			public static readonly StoreNamedPropInfo OriginalLastModifiedTime = new StoreNamedPropInfo("OriginalLastModifiedTime", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 9U), PropertyType.SysTime, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C80 RID: 7296
			public static readonly StoreNamedPropInfo ResponsibleUserName = new StoreNamedPropInfo("ResponsibleUserName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 10U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C81 RID: 7297
			public static readonly StoreNamedPropInfo ClientInfoString = new StoreNamedPropInfo("ClientInfoString", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 11U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C82 RID: 7298
			public static readonly StoreNamedPropInfo ClientProcessName = new StoreNamedPropInfo("ClientProcessName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 12U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C83 RID: 7299
			public static readonly StoreNamedPropInfo ClientMachineName = new StoreNamedPropInfo("ClientMachineName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 13U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C84 RID: 7300
			public static readonly StoreNamedPropInfo ClientBuildVersion = new StoreNamedPropInfo("ClientBuildVersion", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 14U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C85 RID: 7301
			public static readonly StoreNamedPropInfo MiddleTierProcessName = new StoreNamedPropInfo("MiddleTierProcessName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 15U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C86 RID: 7302
			public static readonly StoreNamedPropInfo MiddleTierServerName = new StoreNamedPropInfo("MiddleTierServerName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 16U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C87 RID: 7303
			public static readonly StoreNamedPropInfo MiddleTierServerBuildVersion = new StoreNamedPropInfo("MiddleTierServerBuildVersion", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 17U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C88 RID: 7304
			public static readonly StoreNamedPropInfo ServerName = new StoreNamedPropInfo("ServerName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 18U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C89 RID: 7305
			public static readonly StoreNamedPropInfo ServerBuildVersion = new StoreNamedPropInfo("ServerBuildVersion", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 19U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C8A RID: 7306
			public static readonly StoreNamedPropInfo MailboxDatabaseName = new StoreNamedPropInfo("MailboxDatabaseName", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 20U), PropertyType.Unicode, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C8B RID: 7307
			public static readonly StoreNamedPropInfo ClientIntent = new StoreNamedPropInfo("ClientIntent", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 21U), PropertyType.Int32, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C8C RID: 7308
			public static readonly StoreNamedPropInfo ItemVersion = new StoreNamedPropInfo("ItemVersion", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 22U), PropertyType.Int32, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C8D RID: 7309
			public static readonly StoreNamedPropInfo OriginalEntryId = new StoreNamedPropInfo("OriginalEntryId", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, 23U), PropertyType.Binary, 8388608UL, default(PropertyCategories));

			// Token: 0x04001C8E RID: 7310
			public static readonly StoreNamedPropInfo ParkedMessagesFolderEntryId = new StoreNamedPropInfo("ParkedMessagesFolderEntryId", new StorePropName(NamedPropInfo.CalendarAssistant.NamespaceGuid, "ParkedMessagesFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000041 RID: 65
		public static class InboxFolderLazyStream
		{
			// Token: 0x04001C8F RID: 7311
			public static readonly Guid NamespaceGuid = new Guid("94FAEF10-F947-11D0-800E-0000C90DC8DB");

			// Token: 0x04001C90 RID: 7312
			public static readonly StoreNamedPropInfo AccociatedMessageEID = new StoreNamedPropInfo("AccociatedMessageEID", new StorePropName(NamedPropInfo.InboxFolderLazyStream.NamespaceGuid, 40960U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000042 RID: 66
		public static class Messaging
		{
			// Token: 0x04001C91 RID: 7313
			public static readonly Guid NamespaceGuid = new Guid("41F28F13-83F4-4114-A584-EEDB5A6B0BFF");

			// Token: 0x04001C92 RID: 7314
			public static readonly StoreNamedPropInfo DEPRECATED_IsGroupEscalationMessage = new StoreNamedPropInfo("DEPRECATED_IsGroupEscalationMessage", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "DEPRECATED_IsGroupEscalationMessage"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C93 RID: 7315
			public static readonly StoreNamedPropInfo AggregationSyncProgress = new StoreNamedPropInfo("AggregationSyncProgress", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "AggregationSyncProgress"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C94 RID: 7316
			public static readonly StoreNamedPropInfo TextMessagingDeliveryStatus = new StoreNamedPropInfo("TextMessagingDeliveryStatus", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "TextMessaging:DeliveryStatus"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C95 RID: 7317
			public static readonly StoreNamedPropInfo CloudId = new StoreNamedPropInfo("CloudId", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "CloudId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C96 RID: 7318
			public static readonly StoreNamedPropInfo CloudVersion = new StoreNamedPropInfo("CloudVersion", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "CloudVersion"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C97 RID: 7319
			public static readonly StoreNamedPropInfo OriginalSentTimeForEscalation = new StoreNamedPropInfo("OriginalSentTimeForEscalation", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "OriginalSentTimeForEscalation"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C98 RID: 7320
			public static readonly StoreNamedPropInfo MessageBccMe = new StoreNamedPropInfo("MessageBccMe", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "MessageBccMe"), PropertyType.Boolean, 128UL, default(PropertyCategories));

			// Token: 0x04001C99 RID: 7321
			public static readonly StoreNamedPropInfo Likers = new StoreNamedPropInfo("Likers", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "Likers"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C9A RID: 7322
			public static readonly StoreNamedPropInfo DlpDetectedClassificationObjects = new StoreNamedPropInfo("DlpDetectedClassificationObjects", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "DlpDetectedClassificationObjects"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C9B RID: 7323
			public static readonly StoreNamedPropInfo HasDlpDetectedClassifications = new StoreNamedPropInfo("HasDlpDetectedClassifications", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "HasDlpDetectedClassifications"), PropertyType.String8, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C9C RID: 7324
			public static readonly StoreNamedPropInfo RecoveryOptions = new StoreNamedPropInfo("RecoveryOptions", new StorePropName(NamedPropInfo.Messaging.NamespaceGuid, "RecoveryOptions"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000043 RID: 67
		public static class Storage
		{
			// Token: 0x04001C9D RID: 7325
			public static readonly Guid NamespaceGuid = new Guid("B725F130-47EF-101A-A5F1-02608C9EEBAC");

			// Token: 0x04001C9E RID: 7326
			public static readonly StoreNamedPropInfo X_0009 = new StoreNamedPropInfo("X_0009", new StorePropName(NamedPropInfo.Storage.NamespaceGuid, 9U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001C9F RID: 7327
			public static readonly StoreNamedPropInfo PID_STG_NAME = new StoreNamedPropInfo("PID_STG_NAME", new StorePropName(NamedPropInfo.Storage.NamespaceGuid, 10U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000044 RID: 68
		public static class IConverterSession
		{
			// Token: 0x04001CA0 RID: 7328
			public static readonly Guid NamespaceGuid = new Guid("4E3A7680-B77A-11D0-9DA5-00C04FD65685");

			// Token: 0x04001CA1 RID: 7329
			public static readonly StoreNamedPropInfo Body = new StoreNamedPropInfo("Body", new StorePropName(NamedPropInfo.IConverterSession.NamespaceGuid, "Internet Charset Body"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CA2 RID: 7330
			public static readonly StoreNamedPropInfo PartialMessageId = new StoreNamedPropInfo("PartialMessageId", new StorePropName(NamedPropInfo.IConverterSession.NamespaceGuid, "Partial Message Id"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CA3 RID: 7331
			public static readonly StoreNamedPropInfo PartialMessageNumber = new StoreNamedPropInfo("PartialMessageNumber", new StorePropName(NamedPropInfo.IConverterSession.NamespaceGuid, "Partial Message Number"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CA4 RID: 7332
			public static readonly StoreNamedPropInfo PartialMessageTotal = new StoreNamedPropInfo("PartialMessageTotal", new StorePropName(NamedPropInfo.IConverterSession.NamespaceGuid, "Partial Message Total"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000045 RID: 69
		public static class PkmLinkInformation
		{
			// Token: 0x04001CA5 RID: 7333
			public static readonly Guid NamespaceGuid = new Guid("C82BF597-B831-11D0-B733-00AA00A1EBD2");

			// Token: 0x04001CA6 RID: 7334
			public static readonly StoreNamedPropInfo AHref = new StoreNamedPropInfo("AHref", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "A.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CA7 RID: 7335
			public static readonly StoreNamedPropInfo AppletCode = new StoreNamedPropInfo("AppletCode", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "APPLET.CODE"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CA8 RID: 7336
			public static readonly StoreNamedPropInfo AppletCodeBase = new StoreNamedPropInfo("AppletCodeBase", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "APPLET.CODEBASE"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CA9 RID: 7337
			public static readonly StoreNamedPropInfo AreaHref = new StoreNamedPropInfo("AreaHref", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "AREA.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CAA RID: 7338
			public static readonly StoreNamedPropInfo BaseHref = new StoreNamedPropInfo("BaseHref", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "BASE.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CAB RID: 7339
			public static readonly StoreNamedPropInfo BGSoundSrc = new StoreNamedPropInfo("BGSoundSrc", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "BGSOUND.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CAC RID: 7340
			public static readonly StoreNamedPropInfo BodyBgnd = new StoreNamedPropInfo("BodyBgnd", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "BODY.BACKGROUND"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CAD RID: 7341
			public static readonly StoreNamedPropInfo EmbedSrc = new StoreNamedPropInfo("EmbedSrc", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "EMBED.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CAE RID: 7342
			public static readonly StoreNamedPropInfo FrameSrc = new StoreNamedPropInfo("FrameSrc", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "FRAME.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CAF RID: 7343
			public static readonly StoreNamedPropInfo IframeSrc = new StoreNamedPropInfo("IframeSrc", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "IFRAME.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB0 RID: 7344
			public static readonly StoreNamedPropInfo ImgDynSrc = new StoreNamedPropInfo("ImgDynSrc", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "IMG.DYNSRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB1 RID: 7345
			public static readonly StoreNamedPropInfo ImgSrc = new StoreNamedPropInfo("ImgSrc", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "IMG.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB2 RID: 7346
			public static readonly StoreNamedPropInfo ImgUseMap = new StoreNamedPropInfo("ImgUseMap", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "IMG.USEMAP"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB3 RID: 7347
			public static readonly StoreNamedPropInfo LinkHref = new StoreNamedPropInfo("LinkHref", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "LINK.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB4 RID: 7348
			public static readonly StoreNamedPropInfo MetaUrl = new StoreNamedPropInfo("MetaUrl", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "META.URL"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB5 RID: 7349
			public static readonly StoreNamedPropInfo ObjectCodeBase = new StoreNamedPropInfo("ObjectCodeBase", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "OBJECT.CODEBASE"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB6 RID: 7350
			public static readonly StoreNamedPropInfo ObjectName = new StoreNamedPropInfo("ObjectName", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "OBJECT.NAME"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CB7 RID: 7351
			public static readonly StoreNamedPropInfo ObjectUseMap = new StoreNamedPropInfo("ObjectUseMap", new StorePropName(NamedPropInfo.PkmLinkInformation.NamespaceGuid, "OBJECT.USEMAP"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000046 RID: 70
		public static class PkmHTMLInformation
		{
			// Token: 0x04001CB8 RID: 7352
			public static readonly Guid NamespaceGuid = new Guid("70EB7A10-55D9-11CF-B75B-00AA0051FE20");

			// Token: 0x04001CB9 RID: 7353
			public static readonly StoreNamedPropInfo AHref = new StoreNamedPropInfo("AHref", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "A.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CBA RID: 7354
			public static readonly StoreNamedPropInfo AppletCode = new StoreNamedPropInfo("AppletCode", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "APPLET.CODE"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CBB RID: 7355
			public static readonly StoreNamedPropInfo AppletCodeBase = new StoreNamedPropInfo("AppletCodeBase", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "APPLET.CODEBASE"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CBC RID: 7356
			public static readonly StoreNamedPropInfo AreaHref = new StoreNamedPropInfo("AreaHref", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "AREA.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CBD RID: 7357
			public static readonly StoreNamedPropInfo BaseHref = new StoreNamedPropInfo("BaseHref", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "BASE.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CBE RID: 7358
			public static readonly StoreNamedPropInfo BGSoundSrc = new StoreNamedPropInfo("BGSoundSrc", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "BGSOUND.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CBF RID: 7359
			public static readonly StoreNamedPropInfo BodyBgnd = new StoreNamedPropInfo("BodyBgnd", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "BODY.BACKGROUND"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC0 RID: 7360
			public static readonly StoreNamedPropInfo EmbedSrc = new StoreNamedPropInfo("EmbedSrc", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "EMBED.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC1 RID: 7361
			public static readonly StoreNamedPropInfo FrameSrc = new StoreNamedPropInfo("FrameSrc", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "FRAME.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC2 RID: 7362
			public static readonly StoreNamedPropInfo IframeSrc = new StoreNamedPropInfo("IframeSrc", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "IFRAME.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC3 RID: 7363
			public static readonly StoreNamedPropInfo ImgDynSrc = new StoreNamedPropInfo("ImgDynSrc", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "IMG.DYNSRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC4 RID: 7364
			public static readonly StoreNamedPropInfo ImgSrc = new StoreNamedPropInfo("ImgSrc", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "IMG.SRC"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC5 RID: 7365
			public static readonly StoreNamedPropInfo ImgUseMap = new StoreNamedPropInfo("ImgUseMap", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "IMG.USEMAP"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC6 RID: 7366
			public static readonly StoreNamedPropInfo LinkHref = new StoreNamedPropInfo("LinkHref", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "LINK.HREF"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC7 RID: 7367
			public static readonly StoreNamedPropInfo MetaUrl = new StoreNamedPropInfo("MetaUrl", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "META.URL"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC8 RID: 7368
			public static readonly StoreNamedPropInfo ObjectCodeBase = new StoreNamedPropInfo("ObjectCodeBase", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "OBJECT.CODEBASE"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CC9 RID: 7369
			public static readonly StoreNamedPropInfo ObjectName = new StoreNamedPropInfo("ObjectName", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "OBJECT.NAME"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CCA RID: 7370
			public static readonly StoreNamedPropInfo ObjectUseMap = new StoreNamedPropInfo("ObjectUseMap", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "OBJECT.USEMAP"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CCB RID: 7371
			public static readonly StoreNamedPropInfo ImgAlt = new StoreNamedPropInfo("ImgAlt", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, "IMG.ALT"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CCC RID: 7372
			public static readonly StoreNamedPropInfo X_0003 = new StoreNamedPropInfo("X_0003", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, 3U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CCD RID: 7373
			public static readonly StoreNamedPropInfo X_0004 = new StoreNamedPropInfo("X_0004", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CCE RID: 7374
			public static readonly StoreNamedPropInfo X_0005 = new StoreNamedPropInfo("X_0005", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, 5U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CCF RID: 7375
			public static readonly StoreNamedPropInfo X_0006 = new StoreNamedPropInfo("X_0006", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, 6U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CD0 RID: 7376
			public static readonly StoreNamedPropInfo X_0007 = new StoreNamedPropInfo("X_0007", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, 7U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CD1 RID: 7377
			public static readonly StoreNamedPropInfo X_0008 = new StoreNamedPropInfo("X_0008", new StorePropName(NamedPropInfo.PkmHTMLInformation.NamespaceGuid, 8U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000047 RID: 71
		public static class PkmMetaInformation
		{
			// Token: 0x04001CD2 RID: 7378
			public static readonly Guid NamespaceGuid = new Guid("D1B5D3F0-C0B3-11CF-9A92-00A0C908DBF1");

			// Token: 0x04001CD3 RID: 7379
			public static readonly StoreNamedPropInfo Product = new StoreNamedPropInfo("Product", new StorePropName(NamedPropInfo.PkmMetaInformation.NamespaceGuid, "Product"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CD4 RID: 7380
			public static readonly StoreNamedPropInfo Topic = new StoreNamedPropInfo("Topic", new StorePropName(NamedPropInfo.PkmMetaInformation.NamespaceGuid, "Topic"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000048 RID: 72
		public static class PkmScriptInfo
		{
			// Token: 0x04001CD5 RID: 7381
			public static readonly Guid NamespaceGuid = new Guid("31F400A0-FD07-11CF-B9BD-00AA003DB18E");

			// Token: 0x04001CD6 RID: 7382
			public static readonly StoreNamedPropInfo JavaScript = new StoreNamedPropInfo("JavaScript", new StorePropName(NamedPropInfo.PkmScriptInfo.NamespaceGuid, "JavaScript"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CD7 RID: 7383
			public static readonly StoreNamedPropInfo VBScript = new StoreNamedPropInfo("VBScript", new StorePropName(NamedPropInfo.PkmScriptInfo.NamespaceGuid, "VBScript"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000049 RID: 73
		public static class PkmCharacterization
		{
			// Token: 0x04001CD8 RID: 7384
			public static readonly Guid NamespaceGuid = new Guid("560C36C0-503A-11CF-BAA1-00004C752A9A");

			// Token: 0x04001CD9 RID: 7385
			public static readonly StoreNamedPropInfo X_0002 = new StoreNamedPropInfo("X_0002", new StorePropName(NamedPropInfo.PkmCharacterization.NamespaceGuid, 2U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200004A RID: 74
		public static class PkmDocSummaryInformation
		{
			// Token: 0x04001CDA RID: 7386
			public static readonly Guid NamespaceGuid = new Guid("D5CDD502-2E9C-101B-9397-08002B2CF9AE");

			// Token: 0x04001CDB RID: 7387
			public static readonly StoreNamedPropInfo X_0002 = new StoreNamedPropInfo("X_0002", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 2U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CDC RID: 7388
			public static readonly StoreNamedPropInfo X_0003 = new StoreNamedPropInfo("X_0003", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 3U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CDD RID: 7389
			public static readonly StoreNamedPropInfo X_0004 = new StoreNamedPropInfo("X_0004", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CDE RID: 7390
			public static readonly StoreNamedPropInfo X_0005 = new StoreNamedPropInfo("X_0005", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 5U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CDF RID: 7391
			public static readonly StoreNamedPropInfo X_0006 = new StoreNamedPropInfo("X_0006", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 6U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE0 RID: 7392
			public static readonly StoreNamedPropInfo X_0007 = new StoreNamedPropInfo("X_0007", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 7U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE1 RID: 7393
			public static readonly StoreNamedPropInfo X_0008 = new StoreNamedPropInfo("X_0008", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 8U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE2 RID: 7394
			public static readonly StoreNamedPropInfo X_0009 = new StoreNamedPropInfo("X_0009", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 9U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE3 RID: 7395
			public static readonly StoreNamedPropInfo X_000A = new StoreNamedPropInfo("X_000A", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 10U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE4 RID: 7396
			public static readonly StoreNamedPropInfo X_000B = new StoreNamedPropInfo("X_000B", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 11U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE5 RID: 7397
			public static readonly StoreNamedPropInfo X_000C = new StoreNamedPropInfo("X_000C", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 12U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE6 RID: 7398
			public static readonly StoreNamedPropInfo X_000D = new StoreNamedPropInfo("X_000D", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 13U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE7 RID: 7399
			public static readonly StoreNamedPropInfo X_000E = new StoreNamedPropInfo("X_000E", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 14U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE8 RID: 7400
			public static readonly StoreNamedPropInfo X_000F = new StoreNamedPropInfo("X_000F", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 15U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CE9 RID: 7401
			public static readonly StoreNamedPropInfo X_0010 = new StoreNamedPropInfo("X_0010", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 16U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CEA RID: 7402
			public static readonly StoreNamedPropInfo X_0011 = new StoreNamedPropInfo("X_0011", new StorePropName(NamedPropInfo.PkmDocSummaryInformation.NamespaceGuid, 17U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200004B RID: 75
		public static class PkmGatherer
		{
			// Token: 0x04001CEB RID: 7403
			public static readonly Guid NamespaceGuid = new Guid("0B63E343-9CCC-11D0-BCDB-00805FCCCE04");

			// Token: 0x04001CEC RID: 7404
			public static readonly StoreNamedPropInfo X_0004 = new StoreNamedPropInfo("X_0004", new StorePropName(NamedPropInfo.PkmGatherer.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200004C RID: 76
		public static class PkmIndexServerQuery
		{
			// Token: 0x04001CED RID: 7405
			public static readonly Guid NamespaceGuid = new Guid("49691C90-7E17-101A-A91C-08002B2ECDA9");

			// Token: 0x04001CEE RID: 7406
			public static readonly StoreNamedPropInfo X_0002 = new StoreNamedPropInfo("X_0002", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 2U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CEF RID: 7407
			public static readonly StoreNamedPropInfo X_0004 = new StoreNamedPropInfo("X_0004", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CF0 RID: 7408
			public static readonly StoreNamedPropInfo X_0005 = new StoreNamedPropInfo("X_0005", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 5U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CF1 RID: 7409
			public static readonly StoreNamedPropInfo X_0006 = new StoreNamedPropInfo("X_0006", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 6U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CF2 RID: 7410
			public static readonly StoreNamedPropInfo X_0007 = new StoreNamedPropInfo("X_0007", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 7U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CF3 RID: 7411
			public static readonly StoreNamedPropInfo X_0009 = new StoreNamedPropInfo("X_0009", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 9U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CF4 RID: 7412
			public static readonly StoreNamedPropInfo X_000A = new StoreNamedPropInfo("X_000A", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 10U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CF5 RID: 7413
			public static readonly StoreNamedPropInfo X_000B = new StoreNamedPropInfo("X_000B", new StorePropName(NamedPropInfo.PkmIndexServerQuery.NamespaceGuid, 11U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200004D RID: 77
		public static class PkmNetLibraryInfo
		{
			// Token: 0x04001CF6 RID: 7414
			public static readonly Guid NamespaceGuid = new Guid("C82BF596-B831-11D0-B733-00AA00A1EBD2");

			// Token: 0x04001CF7 RID: 7415
			public static readonly StoreNamedPropInfo X_0003 = new StoreNamedPropInfo("X_0003", new StorePropName(NamedPropInfo.PkmNetLibraryInfo.NamespaceGuid, 3U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200004E RID: 78
		public static class PkmSummaryInformation
		{
			// Token: 0x04001CF8 RID: 7416
			public static readonly Guid NamespaceGuid = new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9");

			// Token: 0x04001CF9 RID: 7417
			public static readonly StoreNamedPropInfo X_0002 = new StoreNamedPropInfo("X_0002", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 2U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CFA RID: 7418
			public static readonly StoreNamedPropInfo X_0003 = new StoreNamedPropInfo("X_0003", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 3U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CFB RID: 7419
			public static readonly StoreNamedPropInfo X_0004 = new StoreNamedPropInfo("X_0004", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CFC RID: 7420
			public static readonly StoreNamedPropInfo X_0005 = new StoreNamedPropInfo("X_0005", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 5U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CFD RID: 7421
			public static readonly StoreNamedPropInfo X_0006 = new StoreNamedPropInfo("X_0006", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 6U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CFE RID: 7422
			public static readonly StoreNamedPropInfo X_0007 = new StoreNamedPropInfo("X_0007", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 7U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001CFF RID: 7423
			public static readonly StoreNamedPropInfo X_0008 = new StoreNamedPropInfo("X_0008", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 8U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D00 RID: 7424
			public static readonly StoreNamedPropInfo X_0009 = new StoreNamedPropInfo("X_0009", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 9U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D01 RID: 7425
			public static readonly StoreNamedPropInfo X_000A = new StoreNamedPropInfo("X_000A", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 10U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D02 RID: 7426
			public static readonly StoreNamedPropInfo X_000B = new StoreNamedPropInfo("X_000B", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 11U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D03 RID: 7427
			public static readonly StoreNamedPropInfo X_000C = new StoreNamedPropInfo("X_000C", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 12U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D04 RID: 7428
			public static readonly StoreNamedPropInfo X_000D = new StoreNamedPropInfo("X_000D", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 13U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D05 RID: 7429
			public static readonly StoreNamedPropInfo X_000E = new StoreNamedPropInfo("X_000E", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 14U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D06 RID: 7430
			public static readonly StoreNamedPropInfo X_000F = new StoreNamedPropInfo("X_000F", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 15U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D07 RID: 7431
			public static readonly StoreNamedPropInfo X_0010 = new StoreNamedPropInfo("X_0010", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 16U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D08 RID: 7432
			public static readonly StoreNamedPropInfo X_0011 = new StoreNamedPropInfo("X_0011", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 17U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D09 RID: 7433
			public static readonly StoreNamedPropInfo X_0012 = new StoreNamedPropInfo("X_0012", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 18U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D0A RID: 7434
			public static readonly StoreNamedPropInfo X_0013 = new StoreNamedPropInfo("X_0013", new StorePropName(NamedPropInfo.PkmSummaryInformation.NamespaceGuid, 19U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200004F RID: 79
		public static class Location
		{
			// Token: 0x04001D0B RID: 7435
			public static readonly Guid NamespaceGuid = new Guid("A719E259-2A9A-4FB8-BAB3-3A9F02970E4B");

			// Token: 0x04001D0C RID: 7436
			public static readonly StoreNamedPropInfo LocationRelevanceRank = new StoreNamedPropInfo("LocationRelevanceRank", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationRelevanceRank"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D0D RID: 7437
			public static readonly StoreNamedPropInfo LocationDisplayName = new StoreNamedPropInfo("LocationDisplayName", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationDisplayName"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D0E RID: 7438
			public static readonly StoreNamedPropInfo LocationAnnotation = new StoreNamedPropInfo("LocationAnnotation", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationAnnotation"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D0F RID: 7439
			public static readonly StoreNamedPropInfo LocationType = new StoreNamedPropInfo("LocationType", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationType"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D10 RID: 7440
			public static readonly StoreNamedPropInfo LocationSource = new StoreNamedPropInfo("LocationSource", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationSource"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D11 RID: 7441
			public static readonly StoreNamedPropInfo LocationUri = new StoreNamedPropInfo("LocationUri", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationUri"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D12 RID: 7442
			public static readonly StoreNamedPropInfo Latitude = new StoreNamedPropInfo("Latitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "Latitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D13 RID: 7443
			public static readonly StoreNamedPropInfo Longitude = new StoreNamedPropInfo("Longitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "Longitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D14 RID: 7444
			public static readonly StoreNamedPropInfo Accuracy = new StoreNamedPropInfo("Accuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "Accuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D15 RID: 7445
			public static readonly StoreNamedPropInfo Altitude = new StoreNamedPropInfo("Altitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "Altitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D16 RID: 7446
			public static readonly StoreNamedPropInfo AltitudeAccuracy = new StoreNamedPropInfo("AltitudeAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "AltitudeAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D17 RID: 7447
			public static readonly StoreNamedPropInfo StreetAddress = new StoreNamedPropInfo("StreetAddress", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "StreetAddress"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D18 RID: 7448
			public static readonly StoreNamedPropInfo LocationCity = new StoreNamedPropInfo("LocationCity", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationCity"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D19 RID: 7449
			public static readonly StoreNamedPropInfo LocationState = new StoreNamedPropInfo("LocationState", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationState"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D1A RID: 7450
			public static readonly StoreNamedPropInfo LocationCountry = new StoreNamedPropInfo("LocationCountry", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationCountry"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D1B RID: 7451
			public static readonly StoreNamedPropInfo LocationPostalCode = new StoreNamedPropInfo("LocationPostalCode", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "LocationPostalCode"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D1C RID: 7452
			public static readonly StoreNamedPropInfo WorkLatitude = new StoreNamedPropInfo("WorkLatitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkLatitude"), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D1D RID: 7453
			public static readonly StoreNamedPropInfo WorkLongitude = new StoreNamedPropInfo("WorkLongitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkLongitude"), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D1E RID: 7454
			public static readonly StoreNamedPropInfo WorkAccuracy = new StoreNamedPropInfo("WorkAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D1F RID: 7455
			public static readonly StoreNamedPropInfo WorkAltitude = new StoreNamedPropInfo("WorkAltitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkAltitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D20 RID: 7456
			public static readonly StoreNamedPropInfo WorkAltitudeAccuracy = new StoreNamedPropInfo("WorkAltitudeAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkAltitudeAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D21 RID: 7457
			public static readonly StoreNamedPropInfo WorkLocationSource = new StoreNamedPropInfo("WorkLocationSource", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkLocationSource"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D22 RID: 7458
			public static readonly StoreNamedPropInfo WorkLocationUri = new StoreNamedPropInfo("WorkLocationUri", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "WorkLocationUri"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D23 RID: 7459
			public static readonly StoreNamedPropInfo HomeLatitude = new StoreNamedPropInfo("HomeLatitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeLatitude"), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D24 RID: 7460
			public static readonly StoreNamedPropInfo HomeLongitude = new StoreNamedPropInfo("HomeLongitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeLongitude"), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D25 RID: 7461
			public static readonly StoreNamedPropInfo HomeAccuracy = new StoreNamedPropInfo("HomeAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D26 RID: 7462
			public static readonly StoreNamedPropInfo HomeAltitude = new StoreNamedPropInfo("HomeAltitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeAltitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D27 RID: 7463
			public static readonly StoreNamedPropInfo HomeAltitudeAccuracy = new StoreNamedPropInfo("HomeAltitudeAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeAltitudeAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D28 RID: 7464
			public static readonly StoreNamedPropInfo HomeLocationSource = new StoreNamedPropInfo("HomeLocationSource", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeLocationSource"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D29 RID: 7465
			public static readonly StoreNamedPropInfo HomeLocationUri = new StoreNamedPropInfo("HomeLocationUri", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "HomeLocationUri"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D2A RID: 7466
			public static readonly StoreNamedPropInfo OtherLatitude = new StoreNamedPropInfo("OtherLatitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherLatitude"), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D2B RID: 7467
			public static readonly StoreNamedPropInfo OtherLongitude = new StoreNamedPropInfo("OtherLongitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherLongitude"), PropertyType.Real64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D2C RID: 7468
			public static readonly StoreNamedPropInfo OtherAccuracy = new StoreNamedPropInfo("OtherAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D2D RID: 7469
			public static readonly StoreNamedPropInfo OtherAltitude = new StoreNamedPropInfo("OtherAltitude", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherAltitude"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D2E RID: 7470
			public static readonly StoreNamedPropInfo OtherAltitudeAccuracy = new StoreNamedPropInfo("OtherAltitudeAccuracy", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherAltitudeAccuracy"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D2F RID: 7471
			public static readonly StoreNamedPropInfo OtherLocationSource = new StoreNamedPropInfo("OtherLocationSource", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherLocationSource"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D30 RID: 7472
			public static readonly StoreNamedPropInfo OtherLocationUri = new StoreNamedPropInfo("OtherLocationUri", new StorePropName(NamedPropInfo.Location.NamespaceGuid, "OtherLocationUri"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000050 RID: 80
		public static class Search
		{
			// Token: 0x04001D31 RID: 7473
			public static readonly Guid NamespaceGuid = new Guid("0B63E350-9CCC-11D0-BCDB-00805FCCCE04");

			// Token: 0x04001D32 RID: 7474
			public static readonly StoreNamedPropInfo X_0004 = new StoreNamedPropInfo("X_0004", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 4U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D33 RID: 7475
			public static readonly StoreNamedPropInfo X_0005 = new StoreNamedPropInfo("X_0005", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 5U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D34 RID: 7476
			public static readonly StoreNamedPropInfo X_0006 = new StoreNamedPropInfo("X_0006", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 6U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D35 RID: 7477
			public static readonly StoreNamedPropInfo X_0007 = new StoreNamedPropInfo("X_0007", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 7U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D36 RID: 7478
			public static readonly StoreNamedPropInfo X_000A = new StoreNamedPropInfo("X_000A", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 10U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D37 RID: 7479
			public static readonly StoreNamedPropInfo X_000B = new StoreNamedPropInfo("X_000B", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 11U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D38 RID: 7480
			public static readonly StoreNamedPropInfo X_0014 = new StoreNamedPropInfo("X_0014", new StorePropName(NamedPropInfo.Search.NamespaceGuid, 20U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000051 RID: 81
		public static class Conversations
		{
			// Token: 0x04001D39 RID: 7481
			public static readonly Guid NamespaceGuid = new Guid("33EBA41F-7AA8-422E-BE7B-79E1A98E54B3");

			// Token: 0x04001D3A RID: 7482
			public static readonly StoreNamedPropInfo ConversationIndexTracking = new StoreNamedPropInfo("ConversationIndexTracking", new StorePropName(NamedPropInfo.Conversations.NamespaceGuid, "ConversationIndexTracking"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D3B RID: 7483
			public static readonly StoreNamedPropInfo ConversationIndexTrackingEx = new StoreNamedPropInfo("ConversationIndexTrackingEx", new StorePropName(NamedPropInfo.Conversations.NamespaceGuid, "ConversationIndexTrackingEx"), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000052 RID: 82
		public static class DAV
		{
			// Token: 0x04001D3C RID: 7484
			public static readonly Guid NamespaceGuid = new Guid("29F3AB60-554D-11D0-A97C-00A0C911F50A");

			// Token: 0x04001D3D RID: 7485
			public static readonly StoreNamedPropInfo X_A100 = new StoreNamedPropInfo("X_A100", new StorePropName(NamedPropInfo.DAV.NamespaceGuid, 41216U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D3E RID: 7486
			public static readonly StoreNamedPropInfo X_A101 = new StoreNamedPropInfo("X_A101", new StorePropName(NamedPropInfo.DAV.NamespaceGuid, 41217U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D3F RID: 7487
			public static readonly StoreNamedPropInfo X_A103 = new StoreNamedPropInfo("X_A103", new StorePropName(NamedPropInfo.DAV.NamespaceGuid, 41219U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D40 RID: 7488
			public static readonly StoreNamedPropInfo X_A104 = new StoreNamedPropInfo("X_A104", new StorePropName(NamedPropInfo.DAV.NamespaceGuid, 41220U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000053 RID: 83
		public static class Drm
		{
			// Token: 0x04001D41 RID: 7489
			public static readonly Guid NamespaceGuid = new Guid("AEAC19E4-89AE-4508-B9B7-BB867ABEE2ED");
		}

		// Token: 0x02000054 RID: 84
		public static class PushNotificationSubscription
		{
			// Token: 0x04001D42 RID: 7490
			public static readonly Guid NamespaceGuid = new Guid("BB8D823E-582D-4A68-B1FB-180B32E3B53E");

			// Token: 0x04001D43 RID: 7491
			public static readonly StoreNamedPropInfo PushNotificationFolderEntryId = new StoreNamedPropInfo("PushNotificationFolderEntryId", new StorePropName(NamedPropInfo.PushNotificationSubscription.NamespaceGuid, "PushNotificationFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D44 RID: 7492
			public static readonly StoreNamedPropInfo PushNotificationSubscriptionId = new StoreNamedPropInfo("PushNotificationSubscriptionId", new StorePropName(NamedPropInfo.PushNotificationSubscription.NamespaceGuid, "PushNotificationSubscriptionId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D45 RID: 7493
			public static readonly StoreNamedPropInfo PushNotificationSubscriptionLastUpdateTimeUTC = new StoreNamedPropInfo("PushNotificationSubscriptionLastUpdateTimeUTC", new StorePropName(NamedPropInfo.PushNotificationSubscription.NamespaceGuid, "PushNotificationSubscriptionLastUpdateTimeUTC"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D46 RID: 7494
			public static readonly StoreNamedPropInfo SerializedPushNotificationSubscription = new StoreNamedPropInfo("SerializedPushNotificationSubscription", new StorePropName(NamedPropInfo.PushNotificationSubscription.NamespaceGuid, "SerializedPushNotificationSubscription"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000055 RID: 85
		public static class GroupNotifications
		{
			// Token: 0x04001D47 RID: 7495
			public static readonly Guid NamespaceGuid = new Guid("4D240CD1-F947-44EE-8F8A-B0E5FF29C18A");

			// Token: 0x04001D48 RID: 7496
			public static readonly StoreNamedPropInfo GroupNotificationsFolderEntryId = new StoreNamedPropInfo("GroupNotificationsFolderEntryId", new StorePropName(NamedPropInfo.GroupNotifications.NamespaceGuid, "GroupNotificationsFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000056 RID: 86
		public static class AuditLogSearch
		{
			// Token: 0x04001D49 RID: 7497
			public static readonly Guid NamespaceGuid = new Guid("9CFF9E83-A0B3-4110-BCD8-617E9EA1E0FE");

			// Token: 0x04001D4A RID: 7498
			public static readonly StoreNamedPropInfo AuditLogSearchIdentity = new StoreNamedPropInfo("AuditLogSearchIdentity", new StorePropName(NamedPropInfo.AuditLogSearch.NamespaceGuid, 1U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000057 RID: 87
		public static class LawEnforcementData
		{
			// Token: 0x04001D4B RID: 7499
			public static readonly Guid NamespaceGuid = new Guid("F9DBDE22-ED1E-4059-B757-51053ED786B8");

			// Token: 0x04001D4C RID: 7500
			public static readonly StoreNamedPropInfo LawEnforcementDataIdentity = new StoreNamedPropInfo("LawEnforcementDataIdentity", new StorePropName(NamedPropInfo.LawEnforcementData.NamespaceGuid, 9U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D4D RID: 7501
			public static readonly StoreNamedPropInfo LawEnforcementDataInternalName = new StoreNamedPropInfo("LawEnforcementDataInternalName", new StorePropName(NamedPropInfo.LawEnforcementData.NamespaceGuid, 16U), PropertyType.Invalid, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000058 RID: 88
		public static class MRSLegacy1
		{
			// Token: 0x04001D4E RID: 7502
			public static readonly Guid NamespaceGuid = new Guid("A7B04B86-0B51-4511-A381-ABC6D4E3C1DB");

			// Token: 0x04001D4F RID: 7503
			public static readonly StoreNamedPropInfo MailboxMoveStatus = new StoreNamedPropInfo("MailboxMoveStatus", new StorePropName(NamedPropInfo.MRSLegacy1.NamespaceGuid, "MailboxMoveStatus"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000059 RID: 89
		public static class MRSLegacy2
		{
			// Token: 0x04001D50 RID: 7504
			public static readonly Guid NamespaceGuid = new Guid("686C4F8A-F3DC-40A9-B078-EC253E481166");

			// Token: 0x04001D51 RID: 7505
			public static readonly StoreNamedPropInfo MoveState = new StoreNamedPropInfo("MoveState", new StorePropName(NamedPropInfo.MRSLegacy2.NamespaceGuid, "MoveState"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200005A RID: 90
		public static class MRSLegacy3
		{
			// Token: 0x04001D52 RID: 7506
			public static readonly Guid NamespaceGuid = new Guid("7B307DDD-13BF-4EEB-B44C-D9C8384D4372");

			// Token: 0x04001D53 RID: 7507
			public static readonly StoreNamedPropInfo MoveServerName = new StoreNamedPropInfo("MoveServerName", new StorePropName(NamedPropInfo.MRSLegacy3.NamespaceGuid, "MoveServerName"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200005B RID: 91
		public static class MRSLegacy4
		{
			// Token: 0x04001D54 RID: 7508
			public static readonly Guid NamespaceGuid = new Guid("8A481DE6-1A0C-4896-A5DC-7E0E5C90C98D");

			// Token: 0x04001D55 RID: 7509
			public static readonly StoreNamedPropInfo AllowedToFinishMove = new StoreNamedPropInfo("AllowedToFinishMove", new StorePropName(NamedPropInfo.MRSLegacy4.NamespaceGuid, "AllowedToFinishMove"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200005C RID: 92
		public static class MRSLegacy5
		{
			// Token: 0x04001D56 RID: 7510
			public static readonly Guid NamespaceGuid = new Guid("55AEADFF-F6D7-4033-A4AC-8AD176E5D5A4");

			// Token: 0x04001D57 RID: 7511
			public static readonly StoreNamedPropInfo CancelMove = new StoreNamedPropInfo("CancelMove", new StorePropName(NamedPropInfo.MRSLegacy5.NamespaceGuid, "CancelMove"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200005D RID: 93
		public static class MRSLegacy6
		{
			// Token: 0x04001D58 RID: 7512
			public static readonly Guid NamespaceGuid = new Guid("E9A77F63-CEC9-469A-9B01-25740A1BA47A");

			// Token: 0x04001D59 RID: 7513
			public static readonly StoreNamedPropInfo ExchangeGuid = new StoreNamedPropInfo("ExchangeGuid", new StorePropName(NamedPropInfo.MRSLegacy6.NamespaceGuid, "ExchangeGuid"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200005E RID: 94
		public static class MRSLegacy7
		{
			// Token: 0x04001D5A RID: 7514
			public static readonly Guid NamespaceGuid = new Guid("D82803CC-312B-4CE1-BFB9-62FC6852987F");

			// Token: 0x04001D5B RID: 7515
			public static readonly StoreNamedPropInfo LastUpdateTimestamp = new StoreNamedPropInfo("LastUpdateTimestamp", new StorePropName(NamedPropInfo.MRSLegacy7.NamespaceGuid, "LastUpdateTimestamp"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200005F RID: 95
		public static class MRSLegacy8
		{
			// Token: 0x04001D5C RID: 7516
			public static readonly Guid NamespaceGuid = new Guid("1553C2E6-0E94-4805-A478-5781B04D83B5");

			// Token: 0x04001D5D RID: 7517
			public static readonly StoreNamedPropInfo CreationTimestamp = new StoreNamedPropInfo("CreationTimestamp", new StorePropName(NamedPropInfo.MRSLegacy8.NamespaceGuid, "CreationTimestamp"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000060 RID: 96
		public static class MRSLegacy9
		{
			// Token: 0x04001D5E RID: 7518
			public static readonly Guid NamespaceGuid = new Guid("32779377-B9A0-45C5-B45C-A71F2F7E2FD0");

			// Token: 0x04001D5F RID: 7519
			public static readonly StoreNamedPropInfo JobType = new StoreNamedPropInfo("JobType", new StorePropName(NamedPropInfo.MRSLegacy9.NamespaceGuid, "JobType"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000061 RID: 97
		public static class MRSLegacy10
		{
			// Token: 0x04001D60 RID: 7520
			public static readonly Guid NamespaceGuid = new Guid("920D906B-8F43-4ABE-9B32-B4197E0BEE8F");

			// Token: 0x04001D61 RID: 7521
			public static readonly StoreNamedPropInfo MailboxMoveFlags = new StoreNamedPropInfo("MailboxMoveFlags", new StorePropName(NamedPropInfo.MRSLegacy10.NamespaceGuid, "MailboxMoveFlags"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000062 RID: 98
		public static class MRSLegacy11
		{
			// Token: 0x04001D62 RID: 7522
			public static readonly Guid NamespaceGuid = new Guid("94216575-14A4-42AE-A19C-84A02166C7B8");

			// Token: 0x04001D63 RID: 7523
			public static readonly StoreNamedPropInfo MailboxMoveSourceMDB = new StoreNamedPropInfo("MailboxMoveSourceMDB", new StorePropName(NamedPropInfo.MRSLegacy11.NamespaceGuid, "MailboxMoveSourceMDB"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000063 RID: 99
		public static class MRSLegacy12
		{
			// Token: 0x04001D64 RID: 7524
			public static readonly Guid NamespaceGuid = new Guid("90EC3564-448C-4B89-9856-3BA248CD6E75");

			// Token: 0x04001D65 RID: 7525
			public static readonly StoreNamedPropInfo MailboxMoveTargetMDB = new StoreNamedPropInfo("MailboxMoveTargetMDB", new StorePropName(NamedPropInfo.MRSLegacy12.NamespaceGuid, "MailboxMoveTargetMDB"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000064 RID: 100
		public static class MRSLegacy13
		{
			// Token: 0x04001D66 RID: 7526
			public static readonly Guid NamespaceGuid = new Guid("18BE3CE8-7B58-47F1-802C-F678EC1F919F");

			// Token: 0x04001D67 RID: 7527
			public static readonly StoreNamedPropInfo DoNotPickUntilTimestamp = new StoreNamedPropInfo("DoNotPickUntilTimestamp", new StorePropName(NamedPropInfo.MRSLegacy13.NamespaceGuid, "DoNotPickUntilTimestamp"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000065 RID: 101
		public static class MRSLegacy14
		{
			// Token: 0x04001D68 RID: 7528
			public static readonly Guid NamespaceGuid = new Guid("42688E07-12BC-43A1-A5D0-301065DB781D");

			// Token: 0x04001D69 RID: 7529
			public static readonly StoreNamedPropInfo RequestType = new StoreNamedPropInfo("RequestType", new StorePropName(NamedPropInfo.MRSLegacy14.NamespaceGuid, "RequestType"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D6A RID: 7530
			public static readonly StoreNamedPropInfo TargetArchiveDatabase = new StoreNamedPropInfo("TargetArchiveDatabase", new StorePropName(NamedPropInfo.MRSLegacy14.NamespaceGuid, "TargetArchiveDatabase"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000066 RID: 102
		public static class MRSLegacy15
		{
			// Token: 0x04001D6B RID: 7531
			public static readonly Guid NamespaceGuid = new Guid("763F3EAB-D6FA-41FE-A317-7A971B529B92");

			// Token: 0x04001D6C RID: 7532
			public static readonly StoreNamedPropInfo SourceArchiveDatabase = new StoreNamedPropInfo("SourceArchiveDatabase", new StorePropName(NamedPropInfo.MRSLegacy15.NamespaceGuid, "SourceArchiveDatabase"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000067 RID: 103
		public static class MRSLegacy16
		{
			// Token: 0x04001D6D RID: 7533
			public static readonly Guid NamespaceGuid = new Guid("EFDBED05-C980-437F-A785-D7AB7F045451");

			// Token: 0x04001D6E RID: 7534
			public static readonly StoreNamedPropInfo Priority = new StoreNamedPropInfo("Priority", new StorePropName(NamedPropInfo.MRSLegacy16.NamespaceGuid, "Priority"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000068 RID: 104
		public static class MRSLegacy17
		{
			// Token: 0x04001D6F RID: 7535
			public static readonly Guid NamespaceGuid = new Guid("6D4203D5-0234-48BB-A2A2-38CB9A4051DF");

			// Token: 0x04001D70 RID: 7536
			public static readonly StoreNamedPropInfo SourceExchangeGuid = new StoreNamedPropInfo("SourceExchangeGuid", new StorePropName(NamedPropInfo.MRSLegacy17.NamespaceGuid, "SourceExchangeGuid"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000069 RID: 105
		public static class MRSLegacy18
		{
			// Token: 0x04001D71 RID: 7537
			public static readonly Guid NamespaceGuid = new Guid("CBEEF357-24DB-4730-9BDE-DA2081BB043B");

			// Token: 0x04001D72 RID: 7538
			public static readonly StoreNamedPropInfo TargetExchangeGuid = new StoreNamedPropInfo("TargetExchangeGuid", new StorePropName(NamedPropInfo.MRSLegacy18.NamespaceGuid, "TargetExchangeGuid"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200006A RID: 106
		public static class MigrationService
		{
			// Token: 0x04001D73 RID: 7539
			public static readonly Guid NamespaceGuid = new Guid("338D7E39-3D57-479E-A8BC-BBA17F0DF824");

			// Token: 0x04001D74 RID: 7540
			public static readonly StoreNamedPropInfo RehomeRequest = new StoreNamedPropInfo("RehomeRequest", new StorePropName(NamedPropInfo.MigrationService.NamespaceGuid, "RehomeRequest"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D75 RID: 7541
			public static readonly StoreNamedPropInfo InternalFlags = new StoreNamedPropInfo("InternalFlags", new StorePropName(NamedPropInfo.MigrationService.NamespaceGuid, "InternalFlags"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D76 RID: 7542
			public static readonly StoreNamedPropInfo OrganizationGuid = new StoreNamedPropInfo("OrganizationGuid", new StorePropName(NamedPropInfo.MigrationService.NamespaceGuid, "OrganizationGuid"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D77 RID: 7543
			public static readonly StoreNamedPropInfo PoisonCount = new StoreNamedPropInfo("PoisonCount", new StorePropName(NamedPropInfo.MigrationService.NamespaceGuid, "PoisonCount"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200006B RID: 107
		public static class Reminders
		{
			// Token: 0x04001D78 RID: 7544
			public static readonly Guid NamespaceGuid = new Guid("1A15A70E-6248-4CBA-9194-92AA60304A35");

			// Token: 0x04001D79 RID: 7545
			public static readonly StoreNamedPropInfo EventTimeBasedInboxReminders = new StoreNamedPropInfo("EventTimeBasedInboxReminders", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "EventTimeBasedInboxReminders"), PropertyType.Binary, 9241386435364257792UL, default(PropertyCategories));

			// Token: 0x04001D7A RID: 7546
			public static readonly StoreNamedPropInfo QuickCaptureReminders = new StoreNamedPropInfo("QuickCaptureReminders", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "QuickCaptureReminders"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D7B RID: 7547
			public static readonly StoreNamedPropInfo EventTimeBasedInboxRemindersState = new StoreNamedPropInfo("EventTimeBasedInboxRemindersState", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "EventTimeBasedInboxRemindersState"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D7C RID: 7548
			public static readonly StoreNamedPropInfo ModernReminders = new StoreNamedPropInfo("ModernReminders", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ModernReminders"), PropertyType.Binary, 9241386435364257792UL, default(PropertyCategories));

			// Token: 0x04001D7D RID: 7549
			public static readonly StoreNamedPropInfo ModernRemindersState = new StoreNamedPropInfo("ModernRemindersState", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ModernRemindersState"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D7E RID: 7550
			public static readonly StoreNamedPropInfo HasExceptionalInboxReminders = new StoreNamedPropInfo("HasExceptionalInboxReminders", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "HasExceptionalInboxReminders"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D7F RID: 7551
			public static readonly StoreNamedPropInfo ReminderId = new StoreNamedPropInfo("ReminderId", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ReminderId"), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D80 RID: 7552
			public static readonly StoreNamedPropInfo ReminderItemGlobalObjectId = new StoreNamedPropInfo("ReminderItemGlobalObjectId", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ReminderItemGlobalObjectId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D81 RID: 7553
			public static readonly StoreNamedPropInfo ReminderOccurrenceGlobalObjectId = new StoreNamedPropInfo("ReminderOccurrenceGlobalObjectId", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ReminderOccurrenceGlobalObjectId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D82 RID: 7554
			public static readonly StoreNamedPropInfo ScheduledReminderTime = new StoreNamedPropInfo("ScheduledReminderTime", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ScheduledReminderTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D83 RID: 7555
			public static readonly StoreNamedPropInfo ReminderText = new StoreNamedPropInfo("ReminderText", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ReminderText"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D84 RID: 7556
			public static readonly StoreNamedPropInfo ReminderStartTime = new StoreNamedPropInfo("ReminderStartTime", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ReminderStartTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D85 RID: 7557
			public static readonly StoreNamedPropInfo ReminderEndTime = new StoreNamedPropInfo("ReminderEndTime", new StorePropName(NamedPropInfo.Reminders.NamespaceGuid, "ReminderEndTime"), PropertyType.SysTime, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200006C RID: 108
		public static class Compliance
		{
			// Token: 0x04001D86 RID: 7558
			public static readonly Guid NamespaceGuid = new Guid("403FC56B-CD30-47C5-86F8-EDE9E35A022B");

			// Token: 0x04001D87 RID: 7559
			public static readonly StoreNamedPropInfo GroupExpansionRecipients = new StoreNamedPropInfo("GroupExpansionRecipients", new StorePropName(NamedPropInfo.Compliance.NamespaceGuid, "GroupExpansionRecipients"), PropertyType.Unicode, 11529215046068469760UL, default(PropertyCategories));

			// Token: 0x04001D88 RID: 7560
			public static readonly StoreNamedPropInfo NeedGroupExpansion = new StoreNamedPropInfo("NeedGroupExpansion", new StorePropName(NamedPropInfo.Compliance.NamespaceGuid, "NeedGroupExpansion"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200006D RID: 109
		public static class Inference
		{
			// Token: 0x04001D89 RID: 7561
			public static readonly Guid NamespaceGuid = new Guid("23239608-685D-4732-9C55-4C95CB4E8E33");

			// Token: 0x04001D8A RID: 7562
			public static readonly StoreNamedPropInfo InferenceProcessingNeeded = new StoreNamedPropInfo("InferenceProcessingNeeded", new StorePropName(NamedPropInfo.Inference.NamespaceGuid, "InferenceProcessingNeeded"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D8B RID: 7563
			public static readonly StoreNamedPropInfo InferenceProcessingActions = new StoreNamedPropInfo("InferenceProcessingActions", new StorePropName(NamedPropInfo.Inference.NamespaceGuid, "InferenceProcessingActions"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D8C RID: 7564
			public static readonly StoreNamedPropInfo InferenceActionTruth = new StoreNamedPropInfo("InferenceActionTruth", new StorePropName(NamedPropInfo.Inference.NamespaceGuid, "InferenceActionTruth"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D8D RID: 7565
			public static readonly StoreNamedPropInfo InferenceNeverClutterOverrideApplied = new StoreNamedPropInfo("InferenceNeverClutterOverrideApplied", new StorePropName(NamedPropInfo.Inference.NamespaceGuid, "InferenceNeverClutterOverrideApplied"), PropertyType.Boolean, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D8E RID: 7566
			public static readonly StoreNamedPropInfo InferenceUniqueActionLabelData = new StoreNamedPropInfo("InferenceUniqueActionLabelData", new StorePropName(NamedPropInfo.Inference.NamespaceGuid, "InferenceUniqueActionLabelData"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x0200006E RID: 110
		public static class PICW
		{
			// Token: 0x04001D8F RID: 7567
			public static readonly Guid NamespaceGuid = new Guid("CAF1337F-3C60-47C6-B8F9-3B50113D046B");
		}

		// Token: 0x0200006F RID: 111
		public static class WorkingSet
		{
			// Token: 0x04001D90 RID: 7568
			public static readonly Guid NamespaceGuid = new Guid("95A4668D-CFBE-4D15-B4AE-3E61B9EF078B");

			// Token: 0x04001D91 RID: 7569
			public static readonly StoreNamedPropInfo WorkingSetId = new StoreNamedPropInfo("WorkingSetId", new StorePropName(NamedPropInfo.WorkingSet.NamespaceGuid, "WorkingSetId"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D92 RID: 7570
			public static readonly StoreNamedPropInfo WorkingSetSource = new StoreNamedPropInfo("WorkingSetSource", new StorePropName(NamedPropInfo.WorkingSet.NamespaceGuid, "WorkingSetSource"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D93 RID: 7571
			public static readonly StoreNamedPropInfo WorkingSetSourcePartition = new StoreNamedPropInfo("WorkingSetSourcePartition", new StorePropName(NamedPropInfo.WorkingSet.NamespaceGuid, "WorkingSetSourcePartition"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D94 RID: 7572
			public static readonly StoreNamedPropInfo WorkingSetSourcePartitionInternal = new StoreNamedPropInfo("WorkingSetSourcePartitionInternal", new StorePropName(NamedPropInfo.WorkingSet.NamespaceGuid, "WorkingSetSourcePartitionInternal"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D95 RID: 7573
			public static readonly StoreNamedPropInfo WorkingSetFlags = new StoreNamedPropInfo("WorkingSetFlags", new StorePropName(NamedPropInfo.WorkingSet.NamespaceGuid, "WorkingSetFlags"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D96 RID: 7574
			public static readonly StoreNamedPropInfo WorkingSetFolderEntryId = new StoreNamedPropInfo("WorkingSetFolderEntryId", new StorePropName(NamedPropInfo.WorkingSet.NamespaceGuid, "WorkingSetFolderEntryId"), PropertyType.Binary, 9223372036854775808UL, default(PropertyCategories));
		}

		// Token: 0x02000070 RID: 112
		public static class ConsumerCalendar
		{
			// Token: 0x04001D97 RID: 7575
			public static readonly Guid NamespaceGuid = new Guid("58B6F260-0251-4293-9737-2EF23187F89D");

			// Token: 0x04001D98 RID: 7576
			public static readonly StoreNamedPropInfo ConsumerCalendarGuid = new StoreNamedPropInfo("ConsumerCalendarGuid", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarGuid"), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D99 RID: 7577
			public static readonly StoreNamedPropInfo ConsumerCalendarOwnerId = new StoreNamedPropInfo("ConsumerCalendarOwnerId", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarOwnerId"), PropertyType.Int64, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D9A RID: 7578
			public static readonly StoreNamedPropInfo ConsumerCalendarPrivateFreeBusyId = new StoreNamedPropInfo("ConsumerCalendarPrivateFreeBusyId", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarPrivateFreeBusyId"), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D9B RID: 7579
			public static readonly StoreNamedPropInfo ConsumerCalendarPrivateDetailId = new StoreNamedPropInfo("ConsumerCalendarPrivateDetailId", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarPrivateDetailId"), PropertyType.Guid, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D9C RID: 7580
			public static readonly StoreNamedPropInfo ConsumerCalendarPublishVisibility = new StoreNamedPropInfo("ConsumerCalendarPublishVisibility", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarPublishVisibility"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D9D RID: 7581
			public static readonly StoreNamedPropInfo ConsumerCalendarSharingInvitations = new StoreNamedPropInfo("ConsumerCalendarSharingInvitations", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarSharingInvitations"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D9E RID: 7582
			public static readonly StoreNamedPropInfo ConsumerCalendarPermissionLevel = new StoreNamedPropInfo("ConsumerCalendarPermissionLevel", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarPermissionLevel"), PropertyType.Int32, 9223372036854775808UL, default(PropertyCategories));

			// Token: 0x04001D9F RID: 7583
			public static readonly StoreNamedPropInfo ConsumerCalendarSynchronizationState = new StoreNamedPropInfo("ConsumerCalendarSynchronizationState", new StorePropName(NamedPropInfo.ConsumerCalendar.NamespaceGuid, "ConsumerCalendarSynchronizationState"), PropertyType.Unicode, 9223372036854775808UL, default(PropertyCategories));
		}
	}
}
