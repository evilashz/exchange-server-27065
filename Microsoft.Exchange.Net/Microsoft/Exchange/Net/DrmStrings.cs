using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Security.RightsManagement.Protectors;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000CA RID: 202
	internal static class DrmStrings
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x00012F44 File Offset: 0x00011144
		static DrmStrings()
		{
			DrmStrings.stringIDs.Add(258986790U, "AlgorithmNotSupported");
			DrmStrings.stringIDs.Add(2776208532U, "RmExceptionActivationGenericMessage");
			DrmStrings.stringIDs.Add(1462171551U, "ReadAttachRenderingErrorOverflow");
			DrmStrings.stringIDs.Add(3575724597U, "ReadOutlookUnicodeStringErrorOverflow");
			DrmStrings.stringIDs.Add(1929574072U, "RmExceptionGenericMessage");
			DrmStrings.stringIDs.Add(3054250489U, "ReadUnicodeStringErrorBefore");
			DrmStrings.stringIDs.Add(4250413746U, "FederationCertificateAccessFailure");
			DrmStrings.stringIDs.Add(1496643996U, "ReadLicenseStringErrorOverflow");
			DrmStrings.stringIDs.Add(2927959246U, "BadDRMPropsSignature");
			DrmStrings.stringIDs.Add(3586924399U, "ReadAttachRenderingError");
			DrmStrings.stringIDs.Add(1879361533U, "ReadUTF8StringErrorBefore");
			DrmStrings.stringIDs.Add(685132634U, "ReadUTF8StringError");
			DrmStrings.stringIDs.Add(642568182U, "FailedToReadManifestFileLocation");
			DrmStrings.stringIDs.Add(1226314129U, "FailedToInitializeRMSEnvironment");
			DrmStrings.stringIDs.Add(2450801847U, "FailedToInstantiateProtectors");
			DrmStrings.stringIDs.Add(74411116U, "ReadUTF8StringErrorOverflow");
			DrmStrings.stringIDs.Add(2063837220U, "UnableToCreateEncryptorHandleWithoutEditRight");
			DrmStrings.stringIDs.Add(2614709070U, "ReadOutlookUnicodeStringErrorBefore");
			DrmStrings.stringIDs.Add(2206873980U, "ReadUnicodeStringErrorOverflow");
			DrmStrings.stringIDs.Add(1454821145U, "ReadLicenseStringErrorBefore");
			DrmStrings.stringIDs.Add(181941971U, "TemplateTypeDistributed");
			DrmStrings.stringIDs.Add(1786778634U, "ReadLicenseStringError");
			DrmStrings.stringIDs.Add(3623501452U, "FailedToDetermineMode");
			DrmStrings.stringIDs.Add(3997623939U, "FailedToLoadIconForAttachment");
			DrmStrings.stringIDs.Add(2213243530U, "ReadUnicodeStringError");
			DrmStrings.stringIDs.Add(508904078U, "TemplateTypeArchived");
			DrmStrings.stringIDs.Add(4021551436U, "FailedToGetTemplateIdFromLicense");
			DrmStrings.stringIDs.Add(1264615704U, "ReadOutlookAnsiStringErrorBefore");
			DrmStrings.stringIDs.Add(3358696673U, "TemplateTypeAll");
			DrmStrings.stringIDs.Add(2637835443U, "ReadOutlookAnsiStringErrorOverflow");
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000131D8 File Offset: 0x000113D8
		public static LocalizedString OpenStreamError(string streamName)
		{
			return new LocalizedString("OpenStreamError", "Ex6F3CFE", false, true, DrmStrings.ResourceManager, new object[]
			{
				streamName
			});
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00013208 File Offset: 0x00011408
		public static LocalizedString FailedToAcquireServerBoxRac(string url)
		{
			return new LocalizedString("FailedToAcquireServerBoxRac", "Ex37A49D", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00013238 File Offset: 0x00011438
		public static LocalizedString FailedToAcquireUseLicense(string url)
		{
			return new LocalizedString("FailedToAcquireUseLicense", "ExF607F6", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00013268 File Offset: 0x00011468
		public static LocalizedString InvalidResponseToServerLicensingRequest(string url)
		{
			return new LocalizedString("InvalidResponseToServerLicensingRequest", "Ex79D3F4", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00013297 File Offset: 0x00011497
		public static LocalizedString AlgorithmNotSupported
		{
			get
			{
				return new LocalizedString("AlgorithmNotSupported", "ExB2F97D", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000132B8 File Offset: 0x000114B8
		public static LocalizedString FailedAtInitializingProtector(Guid protector, int errorCode)
		{
			return new LocalizedString("FailedAtInitializingProtector", "ExC87957", false, true, DrmStrings.ResourceManager, new object[]
			{
				protector,
				errorCode
			});
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x000132F5 File Offset: 0x000114F5
		public static LocalizedString RmExceptionActivationGenericMessage
		{
			get
			{
				return new LocalizedString("RmExceptionActivationGenericMessage", "", false, false, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00013314 File Offset: 0x00011514
		public static LocalizedString ErrorUnprotectingFile(string filename, MsoIpiStatus status)
		{
			return new LocalizedString("ErrorUnprotectingFile", "Ex55B616", false, true, DrmStrings.ResourceManager, new object[]
			{
				filename,
				status
			});
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001334C File Offset: 0x0001154C
		public static LocalizedString EndpointNotFound(Uri url)
		{
			return new LocalizedString("EndpointNotFound", "Ex53012C", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001337C File Offset: 0x0001157C
		public static LocalizedString InvalidRpmsgFormat(string reason)
		{
			return new LocalizedString("InvalidRpmsgFormat", "ExAFD8DB", false, true, DrmStrings.ResourceManager, new object[]
			{
				reason
			});
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x000133AB File Offset: 0x000115AB
		public static LocalizedString ReadAttachRenderingErrorOverflow
		{
			get
			{
				return new LocalizedString("ReadAttachRenderingErrorOverflow", "Ex4AA34F", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000133CC File Offset: 0x000115CC
		public static LocalizedString InvalidResponseToPrelicensingRequest(string url)
		{
			return new LocalizedString("InvalidResponseToPrelicensingRequest", "Ex72A266", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000133FC File Offset: 0x000115FC
		public static LocalizedString InvalidRmsUrl(string s)
		{
			return new LocalizedString("InvalidRmsUrl", "Ex834753", false, true, DrmStrings.ResourceManager, new object[]
			{
				s
			});
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001342B File Offset: 0x0001162B
		public static LocalizedString ReadOutlookUnicodeStringErrorOverflow
		{
			get
			{
				return new LocalizedString("ReadOutlookUnicodeStringErrorOverflow", "ExE95540", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001344C File Offset: 0x0001164C
		public static LocalizedString InvalidResponseToTemplateInformationRequest(string url)
		{
			return new LocalizedString("InvalidResponseToTemplateInformationRequest", "Ex3929D9", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0001347B File Offset: 0x0001167B
		public static LocalizedString RmExceptionGenericMessage
		{
			get
			{
				return new LocalizedString("RmExceptionGenericMessage", "ExE0C0C9", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00013499 File Offset: 0x00011699
		public static LocalizedString ReadUnicodeStringErrorBefore
		{
			get
			{
				return new LocalizedString("ReadUnicodeStringErrorBefore", "ExDCEA42", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000134B7 File Offset: 0x000116B7
		public static LocalizedString FederationCertificateAccessFailure
		{
			get
			{
				return new LocalizedString("FederationCertificateAccessFailure", "ExCD539A", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000134D5 File Offset: 0x000116D5
		public static LocalizedString ReadLicenseStringErrorOverflow
		{
			get
			{
				return new LocalizedString("ReadLicenseStringErrorOverflow", "ExF58C3C", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000134F4 File Offset: 0x000116F4
		public static LocalizedString FailedToGetServerInfo(string url)
		{
			return new LocalizedString("FailedToGetServerInfo", "ExF77EE1", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00013524 File Offset: 0x00011724
		public static LocalizedString FailedToFindServiceLocation(string url)
		{
			return new LocalizedString("FailedToFindServiceLocation", "ExBE4692", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x00013553 File Offset: 0x00011753
		public static LocalizedString BadDRMPropsSignature
		{
			get
			{
				return new LocalizedString("BadDRMPropsSignature", "Ex33E912", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00013574 File Offset: 0x00011774
		public static LocalizedString FailedToAcquireTemplateInformation(string url)
		{
			return new LocalizedString("FailedToAcquireTemplateInformation", "Ex5DC656", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000135A4 File Offset: 0x000117A4
		public static LocalizedString InvalidResponseToTemplateRequest(string url)
		{
			return new LocalizedString("InvalidResponseToTemplateRequest", "Ex7D60C6", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000135D3 File Offset: 0x000117D3
		public static LocalizedString ReadAttachRenderingError
		{
			get
			{
				return new LocalizedString("ReadAttachRenderingError", "Ex3E5F16", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000135F4 File Offset: 0x000117F4
		public static LocalizedString ExternalRmsServerFailure(Uri url, string ecode)
		{
			return new LocalizedString("ExternalRmsServerFailure", "ExAB4BF5", false, true, DrmStrings.ResourceManager, new object[]
			{
				url,
				ecode
			});
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00013627 File Offset: 0x00011827
		public static LocalizedString ReadUTF8StringErrorBefore
		{
			get
			{
				return new LocalizedString("ReadUTF8StringErrorBefore", "Ex4D2772", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00013648 File Offset: 0x00011848
		public static LocalizedString FailedToAcquireTemplates(string url)
		{
			return new LocalizedString("FailedToAcquireTemplates", "ExE6B8FF", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00013677 File Offset: 0x00011877
		public static LocalizedString ReadUTF8StringError
		{
			get
			{
				return new LocalizedString("ReadUTF8StringError", "Ex76AC31", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00013698 File Offset: 0x00011898
		public static LocalizedString InvalidTemplateInformationResponse(string url)
		{
			return new LocalizedString("InvalidTemplateInformationResponse", "ExD2F49C", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000136C8 File Offset: 0x000118C8
		public static LocalizedString InvalidResponseToServerBoxRacRequest(string url)
		{
			return new LocalizedString("InvalidResponseToServerBoxRacRequest", "Ex6F874F", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000136F7 File Offset: 0x000118F7
		public static LocalizedString FailedToReadManifestFileLocation
		{
			get
			{
				return new LocalizedString("FailedToReadManifestFileLocation", "ExABABE8", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00013718 File Offset: 0x00011918
		public static LocalizedString ErrorProtectingFile(string filename, MsoIpiStatus status)
		{
			return new LocalizedString("ErrorProtectingFile", "ExC78DC4", false, true, DrmStrings.ResourceManager, new object[]
			{
				filename,
				status
			});
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00013750 File Offset: 0x00011950
		public static LocalizedString FailedToGetUnboundLicenseObject(string useLicense)
		{
			return new LocalizedString("FailedToGetUnboundLicenseObject", "Ex0E78A4", false, true, DrmStrings.ResourceManager, new object[]
			{
				useLicense
			});
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00013780 File Offset: 0x00011980
		public static LocalizedString FailedAtGettingStatusOfProtection(string filename, int errorCode)
		{
			return new LocalizedString("FailedAtGettingStatusOfProtection", "Ex0027F4", false, true, DrmStrings.ResourceManager, new object[]
			{
				filename,
				errorCode
			});
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000137B8 File Offset: 0x000119B8
		public static LocalizedString FailedToAcquireClc(string url)
		{
			return new LocalizedString("FailedToAcquireClc", "Ex52FA00", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x000137E7 File Offset: 0x000119E7
		public static LocalizedString FailedToInitializeRMSEnvironment
		{
			get
			{
				return new LocalizedString("FailedToInitializeRMSEnvironment", "ExCD190B", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00013805 File Offset: 0x00011A05
		public static LocalizedString FailedToInstantiateProtectors
		{
			get
			{
				return new LocalizedString("FailedToInstantiateProtectors", "Ex310A03", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00013823 File Offset: 0x00011A23
		public static LocalizedString ReadUTF8StringErrorOverflow
		{
			get
			{
				return new LocalizedString("ReadUTF8StringErrorOverflow", "Ex67DE75", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00013841 File Offset: 0x00011A41
		public static LocalizedString UnableToCreateEncryptorHandleWithoutEditRight
		{
			get
			{
				return new LocalizedString("UnableToCreateEncryptorHandleWithoutEditRight", "Ex13F009", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0001385F File Offset: 0x00011A5F
		public static LocalizedString ReadOutlookUnicodeStringErrorBefore
		{
			get
			{
				return new LocalizedString("ReadOutlookUnicodeStringErrorBefore", "Ex119BD8", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0001387D File Offset: 0x00011A7D
		public static LocalizedString ReadUnicodeStringErrorOverflow
		{
			get
			{
				return new LocalizedString("ReadUnicodeStringErrorOverflow", "Ex0B704C", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0001389B File Offset: 0x00011A9B
		public static LocalizedString ReadLicenseStringErrorBefore
		{
			get
			{
				return new LocalizedString("ReadLicenseStringErrorBefore", "ExD2F9D4", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x000138B9 File Offset: 0x00011AB9
		public static LocalizedString TemplateTypeDistributed
		{
			get
			{
				return new LocalizedString("TemplateTypeDistributed", "", false, false, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x000138D8 File Offset: 0x00011AD8
		public static LocalizedString MessageSecurityError(Uri url)
		{
			return new LocalizedString("MessageSecurityError", "Ex37CBAB", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00013908 File Offset: 0x00011B08
		public static LocalizedString InvalidUseLicenseResponse(string url)
		{
			return new LocalizedString("InvalidUseLicenseResponse", "Ex78FDD1", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00013938 File Offset: 0x00011B38
		public static LocalizedString FailedToProtectFile(string filename, int errorCode)
		{
			return new LocalizedString("FailedToProtectFile", "Ex33EEB6", false, true, DrmStrings.ResourceManager, new object[]
			{
				filename,
				errorCode
			});
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00013970 File Offset: 0x00011B70
		public static LocalizedString ReadLicenseStringError
		{
			get
			{
				return new LocalizedString("ReadLicenseStringError", "Ex5BE727", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00013990 File Offset: 0x00011B90
		public static LocalizedString FailedToRequestDelegationToken(Uri url, Uri targetUrl)
		{
			return new LocalizedString("FailedToRequestDelegationToken", "Ex805543", false, true, DrmStrings.ResourceManager, new object[]
			{
				url,
				targetUrl
			});
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000139C3 File Offset: 0x00011BC3
		public static LocalizedString FailedToDetermineMode
		{
			get
			{
				return new LocalizedString("FailedToDetermineMode", "ExE5BA02", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000139E4 File Offset: 0x00011BE4
		public static LocalizedString InvalidResponseToClcRequest(string url)
		{
			return new LocalizedString("InvalidResponseToClcRequest", "Ex3F83DE", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00013A14 File Offset: 0x00011C14
		public static LocalizedString FailedToUnprotectFile(string filename, int errorCode)
		{
			return new LocalizedString("FailedToUnprotectFile", "Ex15268E", false, true, DrmStrings.ResourceManager, new object[]
			{
				filename,
				errorCode
			});
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00013A4C File Offset: 0x00011C4C
		public static LocalizedString ActionNotSupported(Uri url)
		{
			return new LocalizedString("ActionNotSupported", "ExF01168", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013A7C File Offset: 0x00011C7C
		public static LocalizedString TimeoutError(Uri url)
		{
			return new LocalizedString("TimeoutError", "ExB5AA9B", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00013AAB File Offset: 0x00011CAB
		public static LocalizedString FailedToLoadIconForAttachment
		{
			get
			{
				return new LocalizedString("FailedToLoadIconForAttachment", "ExC58448", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00013AC9 File Offset: 0x00011CC9
		public static LocalizedString ReadUnicodeStringError
		{
			get
			{
				return new LocalizedString("ReadUnicodeStringError", "ExC431E0", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00013AE8 File Offset: 0x00011CE8
		public static LocalizedString CommunicationError(Uri url)
		{
			return new LocalizedString("CommunicationError", "Ex8FE8AA", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00013B17 File Offset: 0x00011D17
		public static LocalizedString TemplateTypeArchived
		{
			get
			{
				return new LocalizedString("TemplateTypeArchived", "", false, false, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x00013B35 File Offset: 0x00011D35
		public static LocalizedString FailedToGetTemplateIdFromLicense
		{
			get
			{
				return new LocalizedString("FailedToGetTemplateIdFromLicense", "Ex90AF73", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00013B53 File Offset: 0x00011D53
		public static LocalizedString ReadOutlookAnsiStringErrorBefore
		{
			get
			{
				return new LocalizedString("ReadOutlookAnsiStringErrorBefore", "ExD8E10D", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00013B71 File Offset: 0x00011D71
		public static LocalizedString TemplateTypeAll
		{
			get
			{
				return new LocalizedString("TemplateTypeAll", "", false, false, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00013B90 File Offset: 0x00011D90
		public static LocalizedString FailedToAcquirePreLicense(string url)
		{
			return new LocalizedString("FailedToAcquirePreLicense", "ExDB1526", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00013BC0 File Offset: 0x00011DC0
		public static LocalizedString FailedToOpenManifestFile(string location)
		{
			return new LocalizedString("FailedToOpenManifestFile", "Ex4B2F45", false, true, DrmStrings.ResourceManager, new object[]
			{
				location
			});
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00013BF0 File Offset: 0x00011DF0
		public static LocalizedString FailedToParseUnboundLicense(string useLicense)
		{
			return new LocalizedString("FailedToParseUnboundLicense", "Ex69EEAA", false, true, DrmStrings.ResourceManager, new object[]
			{
				useLicense
			});
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00013C20 File Offset: 0x00011E20
		public static LocalizedString FailedAtSettingProtectorLanguageId(Guid protector, int errorCode)
		{
			return new LocalizedString("FailedAtSettingProtectorLanguageId", "ExB2FE84", false, true, DrmStrings.ResourceManager, new object[]
			{
				protector,
				errorCode
			});
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013C60 File Offset: 0x00011E60
		public static LocalizedString OpenStorageError(string storageName)
		{
			return new LocalizedString("OpenStorageError", "ExF1A762", false, true, DrmStrings.ResourceManager, new object[]
			{
				storageName
			});
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00013C8F File Offset: 0x00011E8F
		public static LocalizedString ReadOutlookAnsiStringErrorOverflow
		{
			get
			{
				return new LocalizedString("ReadOutlookAnsiStringErrorOverflow", "Ex3492A0", false, true, DrmStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public static LocalizedString FailedToEnumerateProtectors(string registryKey)
		{
			return new LocalizedString("FailedToEnumerateProtectors", "Ex7D4517", false, true, DrmStrings.ResourceManager, new object[]
			{
				registryKey
			});
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00013CE0 File Offset: 0x00011EE0
		public static LocalizedString InvalidResponseToCertificationRequest(string url)
		{
			return new LocalizedString("InvalidResponseToCertificationRequest", "ExC10E85", false, true, DrmStrings.ResourceManager, new object[]
			{
				url
			});
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00013D0F File Offset: 0x00011F0F
		public static LocalizedString GetLocalizedString(DrmStrings.IDs key)
		{
			return new LocalizedString(DrmStrings.stringIDs[(uint)key], DrmStrings.ResourceManager, new object[0]);
		}

		// Token: 0x04000418 RID: 1048
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(30);

		// Token: 0x04000419 RID: 1049
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Net.DrmStrings", typeof(DrmStrings).GetTypeInfo().Assembly);

		// Token: 0x020000CB RID: 203
		public enum IDs : uint
		{
			// Token: 0x0400041B RID: 1051
			AlgorithmNotSupported = 258986790U,
			// Token: 0x0400041C RID: 1052
			RmExceptionActivationGenericMessage = 2776208532U,
			// Token: 0x0400041D RID: 1053
			ReadAttachRenderingErrorOverflow = 1462171551U,
			// Token: 0x0400041E RID: 1054
			ReadOutlookUnicodeStringErrorOverflow = 3575724597U,
			// Token: 0x0400041F RID: 1055
			RmExceptionGenericMessage = 1929574072U,
			// Token: 0x04000420 RID: 1056
			ReadUnicodeStringErrorBefore = 3054250489U,
			// Token: 0x04000421 RID: 1057
			FederationCertificateAccessFailure = 4250413746U,
			// Token: 0x04000422 RID: 1058
			ReadLicenseStringErrorOverflow = 1496643996U,
			// Token: 0x04000423 RID: 1059
			BadDRMPropsSignature = 2927959246U,
			// Token: 0x04000424 RID: 1060
			ReadAttachRenderingError = 3586924399U,
			// Token: 0x04000425 RID: 1061
			ReadUTF8StringErrorBefore = 1879361533U,
			// Token: 0x04000426 RID: 1062
			ReadUTF8StringError = 685132634U,
			// Token: 0x04000427 RID: 1063
			FailedToReadManifestFileLocation = 642568182U,
			// Token: 0x04000428 RID: 1064
			FailedToInitializeRMSEnvironment = 1226314129U,
			// Token: 0x04000429 RID: 1065
			FailedToInstantiateProtectors = 2450801847U,
			// Token: 0x0400042A RID: 1066
			ReadUTF8StringErrorOverflow = 74411116U,
			// Token: 0x0400042B RID: 1067
			UnableToCreateEncryptorHandleWithoutEditRight = 2063837220U,
			// Token: 0x0400042C RID: 1068
			ReadOutlookUnicodeStringErrorBefore = 2614709070U,
			// Token: 0x0400042D RID: 1069
			ReadUnicodeStringErrorOverflow = 2206873980U,
			// Token: 0x0400042E RID: 1070
			ReadLicenseStringErrorBefore = 1454821145U,
			// Token: 0x0400042F RID: 1071
			TemplateTypeDistributed = 181941971U,
			// Token: 0x04000430 RID: 1072
			ReadLicenseStringError = 1786778634U,
			// Token: 0x04000431 RID: 1073
			FailedToDetermineMode = 3623501452U,
			// Token: 0x04000432 RID: 1074
			FailedToLoadIconForAttachment = 3997623939U,
			// Token: 0x04000433 RID: 1075
			ReadUnicodeStringError = 2213243530U,
			// Token: 0x04000434 RID: 1076
			TemplateTypeArchived = 508904078U,
			// Token: 0x04000435 RID: 1077
			FailedToGetTemplateIdFromLicense = 4021551436U,
			// Token: 0x04000436 RID: 1078
			ReadOutlookAnsiStringErrorBefore = 1264615704U,
			// Token: 0x04000437 RID: 1079
			TemplateTypeAll = 3358696673U,
			// Token: 0x04000438 RID: 1080
			ReadOutlookAnsiStringErrorOverflow = 2637835443U
		}

		// Token: 0x020000CC RID: 204
		private enum ParamIDs
		{
			// Token: 0x0400043A RID: 1082
			OpenStreamError,
			// Token: 0x0400043B RID: 1083
			FailedToAcquireServerBoxRac,
			// Token: 0x0400043C RID: 1084
			FailedToAcquireUseLicense,
			// Token: 0x0400043D RID: 1085
			InvalidResponseToServerLicensingRequest,
			// Token: 0x0400043E RID: 1086
			FailedAtInitializingProtector,
			// Token: 0x0400043F RID: 1087
			ErrorUnprotectingFile,
			// Token: 0x04000440 RID: 1088
			EndpointNotFound,
			// Token: 0x04000441 RID: 1089
			InvalidRpmsgFormat,
			// Token: 0x04000442 RID: 1090
			InvalidResponseToPrelicensingRequest,
			// Token: 0x04000443 RID: 1091
			InvalidRmsUrl,
			// Token: 0x04000444 RID: 1092
			InvalidResponseToTemplateInformationRequest,
			// Token: 0x04000445 RID: 1093
			FailedToGetServerInfo,
			// Token: 0x04000446 RID: 1094
			FailedToFindServiceLocation,
			// Token: 0x04000447 RID: 1095
			FailedToAcquireTemplateInformation,
			// Token: 0x04000448 RID: 1096
			InvalidResponseToTemplateRequest,
			// Token: 0x04000449 RID: 1097
			ExternalRmsServerFailure,
			// Token: 0x0400044A RID: 1098
			FailedToAcquireTemplates,
			// Token: 0x0400044B RID: 1099
			InvalidTemplateInformationResponse,
			// Token: 0x0400044C RID: 1100
			InvalidResponseToServerBoxRacRequest,
			// Token: 0x0400044D RID: 1101
			ErrorProtectingFile,
			// Token: 0x0400044E RID: 1102
			FailedToGetUnboundLicenseObject,
			// Token: 0x0400044F RID: 1103
			FailedAtGettingStatusOfProtection,
			// Token: 0x04000450 RID: 1104
			FailedToAcquireClc,
			// Token: 0x04000451 RID: 1105
			MessageSecurityError,
			// Token: 0x04000452 RID: 1106
			InvalidUseLicenseResponse,
			// Token: 0x04000453 RID: 1107
			FailedToProtectFile,
			// Token: 0x04000454 RID: 1108
			FailedToRequestDelegationToken,
			// Token: 0x04000455 RID: 1109
			InvalidResponseToClcRequest,
			// Token: 0x04000456 RID: 1110
			FailedToUnprotectFile,
			// Token: 0x04000457 RID: 1111
			ActionNotSupported,
			// Token: 0x04000458 RID: 1112
			TimeoutError,
			// Token: 0x04000459 RID: 1113
			CommunicationError,
			// Token: 0x0400045A RID: 1114
			FailedToAcquirePreLicense,
			// Token: 0x0400045B RID: 1115
			FailedToOpenManifestFile,
			// Token: 0x0400045C RID: 1116
			FailedToParseUnboundLicense,
			// Token: 0x0400045D RID: 1117
			FailedAtSettingProtectorLanguageId,
			// Token: 0x0400045E RID: 1118
			OpenStorageError,
			// Token: 0x0400045F RID: 1119
			FailedToEnumerateProtectors,
			// Token: 0x04000460 RID: 1120
			InvalidResponseToCertificationRequest
		}
	}
}
