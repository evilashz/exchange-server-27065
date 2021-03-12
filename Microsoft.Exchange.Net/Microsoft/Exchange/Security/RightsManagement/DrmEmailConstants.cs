using System;
using System.IO;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000981 RID: 2433
	internal static class DrmEmailConstants
	{
		// Token: 0x04002C9C RID: 11420
		public const int MajorStgVersion = 2;

		// Token: 0x04002C9D RID: 11421
		public const int MinorStgVersion = 2;

		// Token: 0x04002C9E RID: 11422
		public const int CurrentProtection = 0;

		// Token: 0x04002C9F RID: 11423
		public const uint StgCookie = 366883359U;

		// Token: 0x04002CA0 RID: 11424
		public const string AttachmentList = "Attachment List";

		// Token: 0x04002CA1 RID: 11425
		public const string AttachmentListInfo = "Attachment Info";

		// Token: 0x04002CA2 RID: 11426
		public const string Attachment = "MailAttachment {0}";

		// Token: 0x04002CA3 RID: 11427
		public const string BodyInfo = "OutlookBodyStreamInfo";

		// Token: 0x04002CA4 RID: 11428
		public const string RTFBody = "BodyRTF";

		// Token: 0x04002CA5 RID: 11429
		public const string PTHTMLBody = "BodyPT-HTML";

		// Token: 0x04002CA6 RID: 11430
		public const string BodyPTAsHTML = "BodyPTAsHTML";

		// Token: 0x04002CA7 RID: 11431
		public const string DRMStorageInformation = "RpmsgStorageInfo";

		// Token: 0x04002CA8 RID: 11432
		public const string MailStreamInOleAttachment = "\u0003MailStream";

		// Token: 0x04002CA9 RID: 11433
		public const string MailAttachment = "\u0003MailAttachment";

		// Token: 0x04002CAA RID: 11434
		public const string AttachDesc = "AttachDesc";

		// Token: 0x04002CAB RID: 11435
		public const string AttachPres = "AttachPres";

		// Token: 0x04002CAC RID: 11436
		public const string AttachContents = "AttachContents";

		// Token: 0x04002CAD RID: 11437
		public const string StgMessage = "MAPIMessage";

		// Token: 0x04002CAE RID: 11438
		public const ushort AttachDescVersion = 515;

		// Token: 0x04002CAF RID: 11439
		public const ushort AttachPresVersion = 256;

		// Token: 0x04002CB0 RID: 11440
		public const string DRMContent = "\tDRMContent";

		// Token: 0x04002CB1 RID: 11441
		public const string DataSpaces = "\u0006DataSpaces";

		// Token: 0x04002CB2 RID: 11442
		public const string Version = "Version";

		// Token: 0x04002CB3 RID: 11443
		public const string DataSpaceMap = "DataSpaceMap";

		// Token: 0x04002CB4 RID: 11444
		public const string DataSpaceInfo = "DataSpaceInfo";

		// Token: 0x04002CB5 RID: 11445
		public const string DRMDataSpace = "\tDRMDataSpace";

		// Token: 0x04002CB6 RID: 11446
		public const string TransformInfo = "TransformInfo";

		// Token: 0x04002CB7 RID: 11447
		public const string DRMTransform = "\tDRMTransform";

		// Token: 0x04002CB8 RID: 11448
		public const string Primary = "\u0006Primary";

		// Token: 0x04002CB9 RID: 11449
		public const string VersionFeature = "Microsoft.Container.DataSpaces";

		// Token: 0x04002CBA RID: 11450
		public const string DRMTransformFeature = "Microsoft.Metadata.DRMTransform";

		// Token: 0x04002CBB RID: 11451
		public const string DRMTransformClass = "{C73DFACD-061F-43B0-8B64-0C620D2A8B50}";

		// Token: 0x04002CBC RID: 11452
		public const string RACPrefix = "GIC";

		// Token: 0x04002CBD RID: 11453
		public const string CLCPrefix = "CLC";

		// Token: 0x04002CBE RID: 11454
		public const string DrmExtension = "drm";

		// Token: 0x04002CBF RID: 11455
		public const string RacDrmFilePattern = "GIC-*.drm";

		// Token: 0x04002CC0 RID: 11456
		public const string ClcDrmFilePattern = "CLC-*.drm";

		// Token: 0x04002CC1 RID: 11457
		public const string VersionDataValueMin = "1.1.0.0";

		// Token: 0x04002CC2 RID: 11458
		public const string VersionDataValueMax = "1.1.0.0";

		// Token: 0x04002CC3 RID: 11459
		public const int AcquireRmsTemplateMax = 25;

		// Token: 0x04002CC4 RID: 11460
		public const int MaxSupportedRmsTemplates = 200;

		// Token: 0x04002CC5 RID: 11461
		public const string ReachPackageVersionDataValueMin = "1.2.0.0";

		// Token: 0x04002CC6 RID: 11462
		public const string ReachPackageVersionDataValueMax = "1.2.0.0";

		// Token: 0x04002CC7 RID: 11463
		public const string RmsSvcPipelineMaxVersion = "1.0.0.0";

		// Token: 0x04002CC8 RID: 11464
		public const string RmsSvcPipelineMinVersion = "1.0.0.0";

		// Token: 0x04002CC9 RID: 11465
		public const string TemplateDistributionPipeline = "_wmcs/licensing/templatedistribution.asmx";

		// Token: 0x04002CCA RID: 11466
		public const string ServerCertificationPipeline = "_wmcs/certification/servercertification.asmx";

		// Token: 0x04002CCB RID: 11467
		public const string PublishPipeline = "_wmcs/licensing/publish.asmx";

		// Token: 0x04002CCC RID: 11468
		public const string LicensePipeline = "_wmcs/licensing/license.asmx";

		// Token: 0x04002CCD RID: 11469
		public const string LicenseServerPipeline = "_wmcs/licensing/server.asmx";

		// Token: 0x04002CCE RID: 11470
		public const string CertificationServerPipeline = "_wmcs/certification/server.asmx";

		// Token: 0x04002CCF RID: 11471
		public const string LicensingLocation = "_wmcs/licensing";

		// Token: 0x04002CD0 RID: 11472
		public const string CertificationLocation = "_wmcs/certification";

		// Token: 0x04002CD1 RID: 11473
		public const string MexSpecifier = "/mex";

		// Token: 0x04002CD2 RID: 11474
		public const string WsdlSpecifier = "?wsdl";

		// Token: 0x04002CD3 RID: 11475
		public const string XmlVersionTag = "<?xml version=\"1.0\"?>";

		// Token: 0x04002CD4 RID: 11476
		public const string XrmlEndTag = "</XrML>";

		// Token: 0x04002CD5 RID: 11477
		public const string ContentType = "Microsoft Office Document";

		// Token: 0x04002CD6 RID: 11478
		public const string ContentName = "Microsoft Office Document";

		// Token: 0x04002CD7 RID: 11479
		public const string ContentIdType = "MS-GUID";

		// Token: 0x04002CD8 RID: 11480
		public const string IssueRights = "ISSUE";

		// Token: 0x04002CD9 RID: 11481
		public const string MainRightsGroup = "Main-Rights";

		// Token: 0x04002CDA RID: 11482
		public const string WindowsAuthProvider = "WindowsAuthProvider";

		// Token: 0x04002CDB RID: 11483
		public const string ExchangeRecipientOrganizationExtractAllowedName = "ExchangeRecipientOrganizationExtractAllowed";

		// Token: 0x04002CDC RID: 11484
		public const string ExchangeRecipientOrganizationExtractAllowedValue = "true";

		// Token: 0x04002CDD RID: 11485
		public const string TestSetupRegKeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Exchange_Test\\v15\\Setup";

		// Token: 0x04002CDE RID: 11486
		public const string TestSetupRegValueName = "MsiInstallPath";

		// Token: 0x04002CDF RID: 11487
		public const string NoLicenseCacheName = "NOLICCACHE";

		// Token: 0x04002CE0 RID: 11488
		public const string NoLicenseCacheValue = "1";

		// Token: 0x04002CE1 RID: 11489
		public const string XmlDeclarationStart = "<?xml";

		// Token: 0x04002CE2 RID: 11490
		public const string TemplateTypeAndXrmlDelimiter = ":";

		// Token: 0x04002CE3 RID: 11491
		public const string XmlVersionTemplateAndTypeTag = ":<?xml";

		// Token: 0x04002CE4 RID: 11492
		public static readonly Guid FileAttachmentObjectGuid = new Guid(454705, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

		// Token: 0x04002CE5 RID: 11493
		public static readonly Guid MessageAttachmentObjectGuid = new Guid(454706, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

		// Token: 0x04002CE6 RID: 11494
		public static readonly Guid MsgAttGuid = new Guid(134409, 0, 0, 192, 0, 0, 0, 0, 0, 0, 70);

		// Token: 0x04002CE7 RID: 11495
		public static readonly byte[] Null22Bytes = new byte[22];

		// Token: 0x04002CE8 RID: 11496
		public static readonly RmsTemplate[] EmptyTemplateArray = new RmsTemplate[0];

		// Token: 0x04002CE9 RID: 11497
		public static readonly string LicenseStorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Microsoft\\DRM\\Server");

		// Token: 0x04002CEA RID: 11498
		public static byte[] CompressedDRMHeader = new byte[]
		{
			118,
			232,
			4,
			96,
			196,
			17,
			227,
			134
		};

		// Token: 0x04002CEB RID: 11499
		public static uint MagicDrmHeaderChecksum = 4000U;
	}
}
