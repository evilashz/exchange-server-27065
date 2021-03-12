using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000999 RID: 2457
	internal static class NativeConstants
	{
		// Token: 0x04002D52 RID: 11602
		public const uint DrmCallbackVersion = 1U;

		// Token: 0x04002D53 RID: 11603
		internal const string TAGASCII = "ASCII Tag";

		// Token: 0x04002D54 RID: 11604
		internal const string TAGXRML = "XrML Tag";

		// Token: 0x04002D55 RID: 11605
		internal const string TAGFILENAME = "filename";

		// Token: 0x04002D56 RID: 11606
		internal const string TAGMSGUID = "MS-GUID";

		// Token: 0x04002D57 RID: 11607
		internal const string PLUGSTANDARDENABLINGPRINCIPAL = "UDStdPlg Enabling Principal";

		// Token: 0x04002D58 RID: 11608
		internal const string PLUGSTANDARDRIGHTSINTERPRETER = "XrMLv2a";

		// Token: 0x04002D59 RID: 11609
		internal const string PLUGSTANDARDEBDECRYPTOR = "UDStdPlg Enabling Bits Decryptor";

		// Token: 0x04002D5A RID: 11610
		internal const string PLUGSTANDARDEBENCRYPTOR = "UDStdPlg Enabling Bits Encryptor";

		// Token: 0x04002D5B RID: 11611
		internal const string PLUGSTANDARDEBCRYPTOPROVIDER = "UDStdPlg Enabling Bits Crypto Provider";

		// Token: 0x04002D5C RID: 11612
		internal const string PLUGSTANDARDLIBRARY = "UDStdPlg";

		// Token: 0x04002D5D RID: 11613
		internal const string ALGORITHMIDDES = "DES";

		// Token: 0x04002D5E RID: 11614
		internal const string ALGORITHMIDCOCKTAIL = "COCKTAIL";

		// Token: 0x04002D5F RID: 11615
		internal const string ALGORITHMIDAES = "AES";

		// Token: 0x04002D60 RID: 11616
		internal const string ALGORITHMIDRC4 = "RC4";

		// Token: 0x04002D61 RID: 11617
		internal const string QUERYOBJECTIDTYPE = "object-id-type";

		// Token: 0x04002D62 RID: 11618
		internal const string QUERYOBJECTID = "object-id";

		// Token: 0x04002D63 RID: 11619
		internal const string QUERYNAME = "name";

		// Token: 0x04002D64 RID: 11620
		internal const string QUERYCONTENTIDTYPE = "content-id-type";

		// Token: 0x04002D65 RID: 11621
		internal const string QUERYCONTENTIDVALUE = "content-id-value";

		// Token: 0x04002D66 RID: 11622
		internal const string QUERYCONTENTSKUTYPE = "content-sku-type";

		// Token: 0x04002D67 RID: 11623
		internal const string QUERYCONTENTSKUVALUE = "content-sku-value";

		// Token: 0x04002D68 RID: 11624
		internal const string QUERYMANIFESTSOURCE = "manifest-xrml";

		// Token: 0x04002D69 RID: 11625
		internal const string QUERYMACHINECERTSOURCE = "machine-certificate-xrml";

		// Token: 0x04002D6A RID: 11626
		internal const string QUERYAPIVERSION = "api-version";

		// Token: 0x04002D6B RID: 11627
		internal const string QUERYSECREPVERSION = "secrep-version";

		// Token: 0x04002D6C RID: 11628
		internal const string QUERYBLOCKSIZE = "block-size";

		// Token: 0x04002D6D RID: 11629
		internal const string QUERYSYMMETRICKEYTYPE = "symmetric-key-type";

		// Token: 0x04002D6E RID: 11630
		internal const string QUERYACCESSCONDITION = "access-condition";

		// Token: 0x04002D6F RID: 11631
		internal const string QUERYADDRESSTYPE = "address-type";

		// Token: 0x04002D70 RID: 11632
		internal const string QUERYADDRESSVALUE = "address-value";

		// Token: 0x04002D71 RID: 11633
		internal const string QUERYAPPDATANAME = "appdata-name";

		// Token: 0x04002D72 RID: 11634
		internal const string QUERYAPPDATAVALUE = "appdata-value";

		// Token: 0x04002D73 RID: 11635
		internal const string QUERYCONDITIONLIST = "condition-list";

		// Token: 0x04002D74 RID: 11636
		internal const string QUERYDISTRIBUTIONPOINT = "distribution-point";

		// Token: 0x04002D75 RID: 11637
		internal const string QUERYOBJECTTYPE = "object-type";

		// Token: 0x04002D76 RID: 11638
		internal const string QUERYENABLINGPRINCIPALIDTYPE = "enabling-principal-id-type";

		// Token: 0x04002D77 RID: 11639
		internal const string QUERYENABLINGPRINCIPALIDVALUE = "enabling-principal-id-value";

		// Token: 0x04002D78 RID: 11640
		internal const string QUERYGROUPIDENTITYPRINCIPAL = "group-identity-principal";

		// Token: 0x04002D79 RID: 11641
		internal const string QUERYFIRSTUSETAG = "first-use-tag";

		// Token: 0x04002D7A RID: 11642
		internal const string QUERYFROMTIME = "from-time";

		// Token: 0x04002D7B RID: 11643
		internal const string QUERYIDTYPE = "id-type";

		// Token: 0x04002D7C RID: 11644
		internal const string QUERYIDVALUE = "id-value";

		// Token: 0x04002D7D RID: 11645
		internal const string QUERYISSUEDPRINCIPAL = "issued-principal";

		// Token: 0x04002D7E RID: 11646
		internal const string QUERYISSUEDTIME = "issued-time";

		// Token: 0x04002D7F RID: 11647
		internal const string QUERYISSUER = "issuer";

		// Token: 0x04002D80 RID: 11648
		internal const string QUERYOWNER = "owner";

		// Token: 0x04002D81 RID: 11649
		internal const string QUERYPRINCIPAL = "principal";

		// Token: 0x04002D82 RID: 11650
		internal const string QUERYPRINCIPALIDVALUE = "principal-id-value";

		// Token: 0x04002D83 RID: 11651
		internal const string QUERYPRINCIPALIDTYPE = "principal-id-type";

		// Token: 0x04002D84 RID: 11652
		internal const string QUERYRANGETIMECONDITION = "rangetime-condition";

		// Token: 0x04002D85 RID: 11653
		internal const string QUERYOSEXCLUSIONCONDITION = "os-exclusion-condition";

		// Token: 0x04002D86 RID: 11654
		internal const string QUERYINTERVALTIMECONDITION = "intervaltime-condition";

		// Token: 0x04002D87 RID: 11655
		internal const string QUERYINTERVALTIMEINTERVAL = "intervaltime-interval";

		// Token: 0x04002D88 RID: 11656
		internal const string QUERYMAXVERSION = "max-version";

		// Token: 0x04002D89 RID: 11657
		internal const string QUERYMINVERSION = "min-version";

		// Token: 0x04002D8A RID: 11658
		internal const string QUERYREFRESHPERIOD = "refresh-period";

		// Token: 0x04002D8B RID: 11659
		internal const string QUERYREVOCATIONCONDITION = "revocation-condition";

		// Token: 0x04002D8C RID: 11660
		internal const string QUERYRIGHT = "right";

		// Token: 0x04002D8D RID: 11661
		internal const string QUERYRIGHTSGROUP = "rights-group";

		// Token: 0x04002D8E RID: 11662
		internal const string QUERYRIGHTSPARAMETERNAME = "rights-parameter-name";

		// Token: 0x04002D8F RID: 11663
		internal const string QUERYRIGHTSPARAMETERVALUE = "rights-parameter-value";

		// Token: 0x04002D90 RID: 11664
		internal const string QUERYSKUTYPE = "sku-type";

		// Token: 0x04002D91 RID: 11665
		internal const string QUERYSKUVALUE = "sku-value";

		// Token: 0x04002D92 RID: 11666
		internal const string QUERYTIMEINTERVAL = "time-interval";

		// Token: 0x04002D93 RID: 11667
		internal const string QUERYUNTILTIME = "until-time";

		// Token: 0x04002D94 RID: 11668
		internal const string QUERYVALIDITYFROMTIME = "valid-from";

		// Token: 0x04002D95 RID: 11669
		internal const string QUERYVALIDITYUNTILTIME = "valid-until";

		// Token: 0x04002D96 RID: 11670
		internal const string QUERYWORK = "work";
	}
}
