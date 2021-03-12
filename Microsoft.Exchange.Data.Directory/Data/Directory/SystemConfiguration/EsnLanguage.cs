using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000471 RID: 1137
	public enum EsnLanguage
	{
		// Token: 0x040022B9 RID: 8889
		[LocDescription(DirectoryStrings.IDs.EsnLangDefault)]
		Default,
		// Token: 0x040022BA RID: 8890
		[LocDescription(DirectoryStrings.IDs.EsnLangEnglish)]
		English,
		// Token: 0x040022BB RID: 8891
		[LocDescription(DirectoryStrings.IDs.EsnLangFrench)]
		French,
		// Token: 0x040022BC RID: 8892
		[LocDescription(DirectoryStrings.IDs.EsnLangGerman)]
		German,
		// Token: 0x040022BD RID: 8893
		[LocDescription(DirectoryStrings.IDs.EsnLangItalian)]
		Italian,
		// Token: 0x040022BE RID: 8894
		[LocDescription(DirectoryStrings.IDs.EsnLangJapanese)]
		Japanese,
		// Token: 0x040022BF RID: 8895
		[LocDescription(DirectoryStrings.IDs.EsnLangSpanish)]
		Spanish,
		// Token: 0x040022C0 RID: 8896
		[LocDescription(DirectoryStrings.IDs.EsnLangKorean)]
		Korean,
		// Token: 0x040022C1 RID: 8897
		[LocDescription(DirectoryStrings.IDs.EsnLangPortuguese)]
		Portuguese,
		// Token: 0x040022C2 RID: 8898
		[LocDescription(DirectoryStrings.IDs.EsnLangRussian)]
		Russian,
		// Token: 0x040022C3 RID: 8899
		[LocDescription(DirectoryStrings.IDs.EsnLangChineseSimplified)]
		ChineseSimplified,
		// Token: 0x040022C4 RID: 8900
		[LocDescription(DirectoryStrings.IDs.EsnLangChineseTraditional)]
		ChineseTraditional,
		// Token: 0x040022C5 RID: 8901
		[LocDescription(DirectoryStrings.IDs.EsnLangAmharic)]
		Amharic,
		// Token: 0x040022C6 RID: 8902
		[LocDescription(DirectoryStrings.IDs.EsnLangArabic)]
		Arabic,
		// Token: 0x040022C7 RID: 8903
		[LocDescription(DirectoryStrings.IDs.EsnLangBulgarian)]
		Bulgarian,
		// Token: 0x040022C8 RID: 8904
		[LocDescription(DirectoryStrings.IDs.EsnLangBengaliIndia)]
		BengaliIndia,
		// Token: 0x040022C9 RID: 8905
		[LocDescription(DirectoryStrings.IDs.EsnLangCatalan)]
		Catalan,
		// Token: 0x040022CA RID: 8906
		[LocDescription(DirectoryStrings.IDs.EsnLangCzech)]
		Czech,
		// Token: 0x040022CB RID: 8907
		[LocDescription(DirectoryStrings.IDs.EsnLangCyrillic)]
		Cyrillic,
		// Token: 0x040022CC RID: 8908
		[LocDescription(DirectoryStrings.IDs.EsnLangDanish)]
		Danish,
		// Token: 0x040022CD RID: 8909
		[LocDescription(DirectoryStrings.IDs.EsnLangGreek)]
		Greek,
		// Token: 0x040022CE RID: 8910
		[LocDescription(DirectoryStrings.IDs.EsnLangEstonian)]
		Estonian,
		// Token: 0x040022CF RID: 8911
		[LocDescription(DirectoryStrings.IDs.EsnLangBasque)]
		Basque,
		// Token: 0x040022D0 RID: 8912
		[LocDescription(DirectoryStrings.IDs.EsnLangFarsi)]
		Farsi,
		// Token: 0x040022D1 RID: 8913
		[LocDescription(DirectoryStrings.IDs.EsnLangFinnish)]
		Finnish,
		// Token: 0x040022D2 RID: 8914
		[LocDescription(DirectoryStrings.IDs.EsnLangFilipino)]
		Filipino,
		// Token: 0x040022D3 RID: 8915
		[LocDescription(DirectoryStrings.IDs.EsnLangGalician)]
		Galician,
		// Token: 0x040022D4 RID: 8916
		[LocDescription(DirectoryStrings.IDs.EsnLangGujarati)]
		Gujarati,
		// Token: 0x040022D5 RID: 8917
		[LocDescription(DirectoryStrings.IDs.EsnLangHebrew)]
		Hebrew,
		// Token: 0x040022D6 RID: 8918
		[LocDescription(DirectoryStrings.IDs.EsnLangHindi)]
		Hindi,
		// Token: 0x040022D7 RID: 8919
		[LocDescription(DirectoryStrings.IDs.EsnLangCroatian)]
		Croatian,
		// Token: 0x040022D8 RID: 8920
		[LocDescription(DirectoryStrings.IDs.EsnLangHungarian)]
		Hungarian,
		// Token: 0x040022D9 RID: 8921
		[LocDescription(DirectoryStrings.IDs.EsnLangIndonesian)]
		Indonesian,
		// Token: 0x040022DA RID: 8922
		[LocDescription(DirectoryStrings.IDs.EsnLangIcelandic)]
		Icelandic,
		// Token: 0x040022DB RID: 8923
		[LocDescription(DirectoryStrings.IDs.EsnLangKazakh)]
		Kazakh,
		// Token: 0x040022DC RID: 8924
		[LocDescription(DirectoryStrings.IDs.EsnLangKannada)]
		Kannada,
		// Token: 0x040022DD RID: 8925
		[LocDescription(DirectoryStrings.IDs.EsnLangLithuanian)]
		Lithuanian,
		// Token: 0x040022DE RID: 8926
		[LocDescription(DirectoryStrings.IDs.EsnLangLatvian)]
		Latvian,
		// Token: 0x040022DF RID: 8927
		[LocDescription(DirectoryStrings.IDs.EsnLangMalayalam)]
		Malayalam,
		// Token: 0x040022E0 RID: 8928
		[LocDescription(DirectoryStrings.IDs.EsnLangMarathi)]
		Marathi,
		// Token: 0x040022E1 RID: 8929
		[LocDescription(DirectoryStrings.IDs.EsnLangMalay)]
		Malay,
		// Token: 0x040022E2 RID: 8930
		[LocDescription(DirectoryStrings.IDs.EsnLangDutch)]
		Dutch,
		// Token: 0x040022E3 RID: 8931
		[LocDescription(DirectoryStrings.IDs.EsnLangNorwegianNynorsk)]
		NorwegianNynorsk,
		// Token: 0x040022E4 RID: 8932
		[LocDescription(DirectoryStrings.IDs.EsnLangNorwegian)]
		Norwegian,
		// Token: 0x040022E5 RID: 8933
		[LocDescription(DirectoryStrings.IDs.EsnLangOriya)]
		Oriya,
		// Token: 0x040022E6 RID: 8934
		[LocDescription(DirectoryStrings.IDs.EsnLangPolish)]
		Polish,
		// Token: 0x040022E7 RID: 8935
		[LocDescription(DirectoryStrings.IDs.EsnLangPortuguesePortugal)]
		PortuguesePortugal,
		// Token: 0x040022E8 RID: 8936
		[LocDescription(DirectoryStrings.IDs.EsnLangRomanian)]
		Romanian,
		// Token: 0x040022E9 RID: 8937
		[LocDescription(DirectoryStrings.IDs.EsnLangSlovak)]
		Slovak,
		// Token: 0x040022EA RID: 8938
		[LocDescription(DirectoryStrings.IDs.EsnLangSlovenian)]
		Slovenian,
		// Token: 0x040022EB RID: 8939
		[LocDescription(DirectoryStrings.IDs.EsnLangSerbianCyrillic)]
		SerbianCyrillic,
		// Token: 0x040022EC RID: 8940
		[LocDescription(DirectoryStrings.IDs.EsnLangSerbian)]
		Serbian,
		// Token: 0x040022ED RID: 8941
		[LocDescription(DirectoryStrings.IDs.EsnLangSwedish)]
		Swedish,
		// Token: 0x040022EE RID: 8942
		[LocDescription(DirectoryStrings.IDs.EsnLangSwahili)]
		Swahili,
		// Token: 0x040022EF RID: 8943
		[LocDescription(DirectoryStrings.IDs.EsnLangTamil)]
		Tamil,
		// Token: 0x040022F0 RID: 8944
		[LocDescription(DirectoryStrings.IDs.EsnLangTelugu)]
		Telugu,
		// Token: 0x040022F1 RID: 8945
		[LocDescription(DirectoryStrings.IDs.EsnLangThai)]
		Thai,
		// Token: 0x040022F2 RID: 8946
		[LocDescription(DirectoryStrings.IDs.EsnLangTurkish)]
		Turkish,
		// Token: 0x040022F3 RID: 8947
		[LocDescription(DirectoryStrings.IDs.EsnLangUkrainian)]
		Ukrainian,
		// Token: 0x040022F4 RID: 8948
		[LocDescription(DirectoryStrings.IDs.EsnLangUrdu)]
		Urdu,
		// Token: 0x040022F5 RID: 8949
		[LocDescription(DirectoryStrings.IDs.EsnLangVietnamese)]
		Vietnamese
	}
}
