using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Win32;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001F3 RID: 499
	public sealed class OwaRegistryKeys
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x00064836 File Offset: 0x00062A36
		internal static string DefaultTempFolderLocation
		{
			get
			{
				return OwaRegistryKeys.defaultTempFolderLocation;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x0006483D File Offset: 0x00062A3D
		public static string OwaBasicVersion
		{
			get
			{
				return (string)OwaRegistryKeys.GetValue(OwaRegistryKeys.owaBasicVersionKey);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x0006484E File Offset: 0x00062A4E
		public static bool AllowInternalUntrustedCerts
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.allowInternalUntrustedCertsKey);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x0006485F File Offset: 0x00062A5F
		public static bool AllowProxyingWithoutSsl
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.allowProxyingWithoutSslKey);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00064870 File Offset: 0x00062A70
		public static int MaxRecipientsPerMessage
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.maxRecipientsPerMessageKey);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00064881 File Offset: 0x00062A81
		public static int WebReadyDocumentViewingRecycleByConversions
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingRecycleByConversionsKey);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00064892 File Offset: 0x00062A92
		public static int WebReadyDocumentViewingExcelRowsPerPage
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingExcelRowsPerPageKey);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x000648A3 File Offset: 0x00062AA3
		public static int WebReadyDocumentViewingMaxDocumentInputSize
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingMaxDocumentInputSizeKey);
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x000648B4 File Offset: 0x00062AB4
		public static int WebReadyDocumentViewingMaxDocumentOutputSize
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingMaxDocumentOutputSizeKey);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x000648C5 File Offset: 0x00062AC5
		public static bool WebReadyDocumentViewingWithInlineImage
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingWithInlineImageKey);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x000648D6 File Offset: 0x00062AD6
		public static string WebReadyDocumentViewingTempFolderLocation
		{
			get
			{
				return (string)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingTempFolderLocationKey);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x000648E7 File Offset: 0x00062AE7
		public static int WebReadyDocumentViewingCacheDiskQuota
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingCacheDiskQuotaKey);
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x000648F8 File Offset: 0x00062AF8
		public static int WebReadyDocumentViewingConversionTimeout
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingConversionTimeoutKey);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x00064909 File Offset: 0x00062B09
		public static int WebReadyDocumentViewingMemoryLimitInMB
		{
			get
			{
				return (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.webReadyDocumentViewingMemoryLimitInMBKey);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0006491A File Offset: 0x00062B1A
		public static bool ForceSMimeClientUpgrade
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.forceSMimeClientUpgradeKey);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0006492B File Offset: 0x00062B2B
		public static bool CheckCRLOnSend
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.checkCRLOnSendKey);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0006493C File Offset: 0x00062B3C
		public static int DLExpansionTimeout
		{
			get
			{
				int num = (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.dlExpansionTimeoutKey);
				if (num < 0)
				{
					return 0;
				}
				return num;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x00064960 File Offset: 0x00062B60
		public static bool UseSecondaryProxiesWhenFindingCertificates
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.useSecondaryProxiesWhenFindingCertificatesKey);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x00064974 File Offset: 0x00062B74
		public static int CRLConnectionTimeout
		{
			get
			{
				int num = (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.clrConnectionTimeoutKey);
				if (num < 5000)
				{
					return 5000;
				}
				return num;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x000649A0 File Offset: 0x00062BA0
		public static int CRLRetrievalTimeout
		{
			get
			{
				int num = (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.clrRetrievalTimeoutKey);
				if (num < 0)
				{
					return 5000;
				}
				return num;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x000649C8 File Offset: 0x00062BC8
		public static bool DisableCRLCheck
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.disableCRLCheckKey);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000649D9 File Offset: 0x00062BD9
		public static bool AlwaysSign
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.alwaysSignKey);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000649EA File Offset: 0x00062BEA
		public static bool AlwaysEncrypt
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.alwaysEncryptKey);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x000649FB File Offset: 0x00062BFB
		public static bool ClearSign
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.clearSignKey);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00064A0C File Offset: 0x00062C0C
		public static bool IncludeCertificateChainWithoutRootCertificate
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.includeCertificateChainWithoutRootCertificateKey);
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x00064A1D File Offset: 0x00062C1D
		public static bool IncludeCertificateChainAndRootCertificate
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.includeCertificateChainAndRootCertificateKey);
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00064A2E File Offset: 0x00062C2E
		public static bool EncryptTemporaryBuffers
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.encryptTemporaryBuffersKey);
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00064A3F File Offset: 0x00062C3F
		public static bool SignedEmailCertificateInclusion
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.signedEmailCertificateInclusionKey);
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00064A50 File Offset: 0x00062C50
		public static int BccEncryptedEmailForking
		{
			get
			{
				int num = (int)OwaRegistryKeys.GetValue(OwaRegistryKeys.bccEncryptedEmailForkingKey);
				if (num < 0 || num > 2)
				{
					return (int)OwaRegistryKeys.bccEncryptedEmailForkingKey.DefaultValue;
				}
				return num;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x00064A86 File Offset: 0x00062C86
		public static bool IncludeSMIMECapabilitiesInMessage
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.includeSMIMECapabilitiesInMessageKey);
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00064A97 File Offset: 0x00062C97
		public static bool CopyRecipientHeaders
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.copyRecipientHeadersKey);
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00064AA8 File Offset: 0x00062CA8
		public static bool OnlyUseSmartCard
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.onlyUseSmartCardKey);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00064AB9 File Offset: 0x00062CB9
		public static bool TripleWrapSignedEncryptedMail
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.tripleWrapSignedEncryptedMailKey);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00064ACA File Offset: 0x00062CCA
		public static bool UseKeyIdentifier
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.useKeyIdentifierKey);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00064ADB File Offset: 0x00062CDB
		public static string EncryptionAlgorithms
		{
			get
			{
				return (string)OwaRegistryKeys.GetValue(OwaRegistryKeys.encryptionAlgorithmsKey);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00064AEC File Offset: 0x00062CEC
		public static string SigningAlgorithms
		{
			get
			{
				return (string)OwaRegistryKeys.GetValue(OwaRegistryKeys.signingAlgorithmsKey);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00064AFD File Offset: 0x00062CFD
		public static bool AllowUserChoiceOfSigningCertificate
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.allowUserChoiceOfSigningCertificateKey);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00064B0E File Offset: 0x00062D0E
		public static string SenderCertificateAttributesToDisplay
		{
			get
			{
				return (string)OwaRegistryKeys.GetValue(OwaRegistryKeys.senderCertificateAttributesToDisplayKey);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00064B1F File Offset: 0x00062D1F
		public static bool UseEmbeddedMessageFileNameAsAttachmentName
		{
			get
			{
				return (bool)OwaRegistryKeys.GetValue(OwaRegistryKeys.useEmbeddedMessageFileNameAsAttachmentNameKey);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00064B30 File Offset: 0x00062D30
		public static string IMImplementationDLLPath
		{
			get
			{
				return (string)OwaRegistryKeys.GetValue(OwaRegistryKeys.implementationDLLPathKey);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00064B41 File Offset: 0x00062D41
		public static string IMImplementationDLLPathKey
		{
			get
			{
				return OwaRegistryKeys.implementationDLLPathKey.Name;
			}
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x00064B50 File Offset: 0x00062D50
		public static void Initialize()
		{
			ExTraceGlobals.CoreCallTracer.TraceDebug(0L, "OwaRegistryKeys.Initialize");
			for (int i = 0; i < OwaRegistryKeys.keyPaths.Length; i++)
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(OwaRegistryKeys.keyPaths[i]))
				{
					if (registryKey != null)
					{
						foreach (OwaRegistryKey owaRegistryKey in OwaRegistryKeys.keys[i])
						{
							OwaRegistryKeys.keyValueCache[owaRegistryKey] = OwaRegistryKeys.ReadKeyValue(registryKey, owaRegistryKey);
						}
					}
				}
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00064BE4 File Offset: 0x00062DE4
		private static object ReadKeyValue(RegistryKey keyContainer, OwaRegistryKey owaKey)
		{
			ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Reading registry key \"{0}\"", owaKey.Name);
			object obj;
			if (owaKey.Type == typeof(int))
			{
				obj = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (obj.GetType() != typeof(int))
				{
					obj = null;
				}
			}
			else if (owaKey.Type == typeof(string))
			{
				obj = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (obj.GetType() != typeof(string))
				{
					obj = null;
				}
			}
			else
			{
				if (!(owaKey.Type == typeof(bool)))
				{
					return null;
				}
				object value = keyContainer.GetValue(owaKey.Name, owaKey.DefaultValue);
				if (value.GetType() != typeof(int))
				{
					obj = null;
				}
				else
				{
					obj = ((int)value != 0);
				}
			}
			if (obj == null)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "Couldn't find key or key format/type was incorrect, using default value");
				obj = owaKey.DefaultValue;
			}
			ExTraceGlobals.CoreTracer.TraceDebug<string, object>(0L, "Configuration registry key \"{0}\" read with value=\"{1}\"", owaKey.Name, obj);
			return obj;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00064D2C File Offset: 0x00062F2C
		private static object GetValue(OwaRegistryKey key)
		{
			object result = null;
			if (OwaRegistryKeys.keyValueCache.TryGetValue(key, out result))
			{
				return result;
			}
			return key.DefaultValue;
		}

		// Token: 0x04000AE8 RID: 2792
		internal const int DefaultRecycleByConversions = 150;

		// Token: 0x04000AE9 RID: 2793
		internal const int DefaultExcelRowsPerPage = 200;

		// Token: 0x04000AEA RID: 2794
		internal const int DefaultMaxDocumentInputSize = 5000;

		// Token: 0x04000AEB RID: 2795
		internal const int DefaultMaxDocumentOutputSize = 5000;

		// Token: 0x04000AEC RID: 2796
		internal const bool DefaultWithInlineImage = true;

		// Token: 0x04000AED RID: 2797
		internal const int DefaultMemoryLimitInMB = 200;

		// Token: 0x04000AEE RID: 2798
		internal const int DefaultCacheDiskQuota = 1000;

		// Token: 0x04000AEF RID: 2799
		internal const int DefaultConversionTimeout = 20;

		// Token: 0x04000AF0 RID: 2800
		private static readonly string smimeKeyPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA\\SMime";

		// Token: 0x04000AF1 RID: 2801
		internal static readonly string IMKeyPath = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA\\InstantMessaging";

		// Token: 0x04000AF2 RID: 2802
		private static readonly string OwaSetupInstallKeyPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x04000AF3 RID: 2803
		private static readonly string[] keyPaths = new string[]
		{
			"SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA",
			"SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA\\WebReadyDocumentViewing",
			OwaRegistryKeys.IMKeyPath,
			OwaRegistryKeys.smimeKeyPath,
			OwaRegistryKeys.OwaSetupInstallKeyPath
		};

		// Token: 0x04000AF4 RID: 2804
		private static string defaultTempFolderLocation = Path.GetTempPath();

		// Token: 0x04000AF5 RID: 2805
		private static OwaRegistryKey allowInternalUntrustedCertsKey = new OwaRegistryKey("AllowInternalUntrustedCerts", typeof(bool), true);

		// Token: 0x04000AF6 RID: 2806
		private static OwaRegistryKey allowProxyingWithoutSslKey = new OwaRegistryKey("AllowProxyingWithoutSsl", typeof(bool), false);

		// Token: 0x04000AF7 RID: 2807
		private static OwaRegistryKey maxRecipientsPerMessageKey = new OwaRegistryKey("MaxRecipientsPerMessage", typeof(int), 2000);

		// Token: 0x04000AF8 RID: 2808
		private static OwaRegistryKey webReadyDocumentViewingRecycleByConversionsKey = new OwaRegistryKey("RecycleByConversions", typeof(int), 150);

		// Token: 0x04000AF9 RID: 2809
		private static OwaRegistryKey webReadyDocumentViewingExcelRowsPerPageKey = new OwaRegistryKey("ExcelRowsPerPage", typeof(int), 200);

		// Token: 0x04000AFA RID: 2810
		private static OwaRegistryKey webReadyDocumentViewingMaxDocumentInputSizeKey = new OwaRegistryKey("MaxDocumentInputSize", typeof(int), 5000);

		// Token: 0x04000AFB RID: 2811
		private static OwaRegistryKey webReadyDocumentViewingMaxDocumentOutputSizeKey = new OwaRegistryKey("MaxDocumentOutputSize", typeof(int), 5000);

		// Token: 0x04000AFC RID: 2812
		private static OwaRegistryKey webReadyDocumentViewingWithInlineImageKey = new OwaRegistryKey("WebReadyDocumentViewingWithInlineImage", typeof(bool), true);

		// Token: 0x04000AFD RID: 2813
		private static OwaRegistryKey webReadyDocumentViewingTempFolderLocationKey = new OwaRegistryKey("TempFolderLocation", typeof(string), OwaRegistryKeys.DefaultTempFolderLocation);

		// Token: 0x04000AFE RID: 2814
		private static OwaRegistryKey webReadyDocumentViewingCacheDiskQuotaKey = new OwaRegistryKey("CacheDiskQuota", typeof(int), 1000);

		// Token: 0x04000AFF RID: 2815
		private static OwaRegistryKey webReadyDocumentViewingConversionTimeoutKey = new OwaRegistryKey("ConversionTimeout", typeof(int), 20);

		// Token: 0x04000B00 RID: 2816
		private static OwaRegistryKey webReadyDocumentViewingMemoryLimitInMBKey = new OwaRegistryKey("MemoryLimitInMB", typeof(int), 200);

		// Token: 0x04000B01 RID: 2817
		private static OwaRegistryKey forceSMimeClientUpgradeKey = new OwaRegistryKey("ForceSMimeClientUpgrade", typeof(bool), true);

		// Token: 0x04000B02 RID: 2818
		private static OwaRegistryKey checkCRLOnSendKey = new OwaRegistryKey("CheckCRLOnSend", typeof(bool), false);

		// Token: 0x04000B03 RID: 2819
		private static OwaRegistryKey dlExpansionTimeoutKey = new OwaRegistryKey("DLExpansionTimeout", typeof(int), 60000);

		// Token: 0x04000B04 RID: 2820
		private static OwaRegistryKey useSecondaryProxiesWhenFindingCertificatesKey = new OwaRegistryKey("UseSecondaryProxiesWhenFindingCertificates", typeof(bool), true);

		// Token: 0x04000B05 RID: 2821
		private static OwaRegistryKey clrConnectionTimeoutKey = new OwaRegistryKey("CRLConnectionTimeout", typeof(int), 60000);

		// Token: 0x04000B06 RID: 2822
		private static OwaRegistryKey clrRetrievalTimeoutKey = new OwaRegistryKey("CRLRetrievalTimeout", typeof(int), 10000);

		// Token: 0x04000B07 RID: 2823
		private static OwaRegistryKey disableCRLCheckKey = new OwaRegistryKey("DisableCRLCheck", typeof(bool), false);

		// Token: 0x04000B08 RID: 2824
		private static OwaRegistryKey alwaysSignKey = new OwaRegistryKey("AlwaysSign", typeof(bool), false);

		// Token: 0x04000B09 RID: 2825
		private static OwaRegistryKey alwaysEncryptKey = new OwaRegistryKey("AlwaysEncrypt", typeof(bool), false);

		// Token: 0x04000B0A RID: 2826
		private static OwaRegistryKey clearSignKey = new OwaRegistryKey("ClearSign", typeof(bool), true);

		// Token: 0x04000B0B RID: 2827
		private static OwaRegistryKey includeCertificateChainWithoutRootCertificateKey = new OwaRegistryKey("IncludeCertificateChainWithoutRootCertificate", typeof(bool), false);

		// Token: 0x04000B0C RID: 2828
		private static OwaRegistryKey includeCertificateChainAndRootCertificateKey = new OwaRegistryKey("IncludeCertificateChainAndRootCertificate", typeof(bool), false);

		// Token: 0x04000B0D RID: 2829
		private static OwaRegistryKey encryptTemporaryBuffersKey = new OwaRegistryKey("EncryptTemporaryBuffers", typeof(bool), true);

		// Token: 0x04000B0E RID: 2830
		private static OwaRegistryKey signedEmailCertificateInclusionKey = new OwaRegistryKey("SignedEmailCertificateInclusion", typeof(bool), true);

		// Token: 0x04000B0F RID: 2831
		private static OwaRegistryKey bccEncryptedEmailForkingKey = new OwaRegistryKey("BccEncryptedEmailForking", typeof(int), 0);

		// Token: 0x04000B10 RID: 2832
		private static OwaRegistryKey includeSMIMECapabilitiesInMessageKey = new OwaRegistryKey("IncludeSMIMECapabilitiesInMessage", typeof(bool), false);

		// Token: 0x04000B11 RID: 2833
		private static OwaRegistryKey copyRecipientHeadersKey = new OwaRegistryKey("CopyRecipientHeaders", typeof(bool), false);

		// Token: 0x04000B12 RID: 2834
		private static OwaRegistryKey onlyUseSmartCardKey = new OwaRegistryKey("OnlyUseSmartCard", typeof(bool), false);

		// Token: 0x04000B13 RID: 2835
		private static OwaRegistryKey tripleWrapSignedEncryptedMailKey = new OwaRegistryKey("TripleWrapSignedEncryptedMail", typeof(bool), true);

		// Token: 0x04000B14 RID: 2836
		private static OwaRegistryKey useKeyIdentifierKey = new OwaRegistryKey("UseKeyIdentifier", typeof(bool), false);

		// Token: 0x04000B15 RID: 2837
		private static OwaRegistryKey encryptionAlgorithmsKey = new OwaRegistryKey("EncryptionAlgorithms", typeof(string), string.Empty);

		// Token: 0x04000B16 RID: 2838
		private static OwaRegistryKey signingAlgorithmsKey = new OwaRegistryKey("SigningAlgorithms", typeof(string), string.Empty);

		// Token: 0x04000B17 RID: 2839
		private static OwaRegistryKey allowUserChoiceOfSigningCertificateKey = new OwaRegistryKey("AllowUserChoiceOfSigningCertificate", typeof(bool), false);

		// Token: 0x04000B18 RID: 2840
		private static OwaRegistryKey senderCertificateAttributesToDisplayKey = new OwaRegistryKey("SenderCertificateAttributesToDisplay", typeof(string), string.Empty);

		// Token: 0x04000B19 RID: 2841
		private static OwaRegistryKey useEmbeddedMessageFileNameAsAttachmentNameKey = new OwaRegistryKey("UseEmbeddedMessageFileNameAsAttachmentName", typeof(bool), false);

		// Token: 0x04000B1A RID: 2842
		private static OwaRegistryKey implementationDLLPathKey = new OwaRegistryKey("ImplementationDLLPath", typeof(string), string.Empty);

		// Token: 0x04000B1B RID: 2843
		private static OwaRegistryKey owaBasicVersionKey = new OwaRegistryKey("OwaBasicVersion", typeof(string), string.Empty);

		// Token: 0x04000B1C RID: 2844
		private static OwaRegistryKey[] owaKeys = new OwaRegistryKey[]
		{
			OwaRegistryKeys.allowInternalUntrustedCertsKey,
			OwaRegistryKeys.allowProxyingWithoutSslKey,
			OwaRegistryKeys.maxRecipientsPerMessageKey,
			OwaRegistryKeys.owaBasicVersionKey
		};

		// Token: 0x04000B1D RID: 2845
		private static OwaRegistryKey[] owaWebReadyDocumentViewingKeys = new OwaRegistryKey[]
		{
			OwaRegistryKeys.webReadyDocumentViewingRecycleByConversionsKey,
			OwaRegistryKeys.webReadyDocumentViewingExcelRowsPerPageKey,
			OwaRegistryKeys.webReadyDocumentViewingMaxDocumentInputSizeKey,
			OwaRegistryKeys.webReadyDocumentViewingMaxDocumentOutputSizeKey,
			OwaRegistryKeys.webReadyDocumentViewingWithInlineImageKey,
			OwaRegistryKeys.webReadyDocumentViewingTempFolderLocationKey,
			OwaRegistryKeys.webReadyDocumentViewingCacheDiskQuotaKey,
			OwaRegistryKeys.webReadyDocumentViewingConversionTimeoutKey,
			OwaRegistryKeys.webReadyDocumentViewingMemoryLimitInMBKey
		};

		// Token: 0x04000B1E RID: 2846
		private static OwaRegistryKey[] owaSMimeKeys = new OwaRegistryKey[]
		{
			OwaRegistryKeys.forceSMimeClientUpgradeKey,
			OwaRegistryKeys.checkCRLOnSendKey,
			OwaRegistryKeys.dlExpansionTimeoutKey,
			OwaRegistryKeys.useSecondaryProxiesWhenFindingCertificatesKey,
			OwaRegistryKeys.clrConnectionTimeoutKey,
			OwaRegistryKeys.clrRetrievalTimeoutKey,
			OwaRegistryKeys.disableCRLCheckKey,
			OwaRegistryKeys.alwaysSignKey,
			OwaRegistryKeys.alwaysEncryptKey,
			OwaRegistryKeys.clearSignKey,
			OwaRegistryKeys.includeCertificateChainWithoutRootCertificateKey,
			OwaRegistryKeys.includeCertificateChainAndRootCertificateKey,
			OwaRegistryKeys.encryptTemporaryBuffersKey,
			OwaRegistryKeys.signedEmailCertificateInclusionKey,
			OwaRegistryKeys.bccEncryptedEmailForkingKey,
			OwaRegistryKeys.includeSMIMECapabilitiesInMessageKey,
			OwaRegistryKeys.copyRecipientHeadersKey,
			OwaRegistryKeys.onlyUseSmartCardKey,
			OwaRegistryKeys.tripleWrapSignedEncryptedMailKey,
			OwaRegistryKeys.useKeyIdentifierKey,
			OwaRegistryKeys.encryptionAlgorithmsKey,
			OwaRegistryKeys.signingAlgorithmsKey,
			OwaRegistryKeys.allowUserChoiceOfSigningCertificateKey,
			OwaRegistryKeys.useEmbeddedMessageFileNameAsAttachmentNameKey,
			OwaRegistryKeys.senderCertificateAttributesToDisplayKey
		};

		// Token: 0x04000B1F RID: 2847
		private static OwaRegistryKey[] owaIMKeys = new OwaRegistryKey[]
		{
			OwaRegistryKeys.implementationDLLPathKey
		};

		// Token: 0x04000B20 RID: 2848
		private static readonly OwaRegistryKey[] owaSetupKeys = new OwaRegistryKey[]
		{
			OwaRegistryKeys.owaBasicVersionKey
		};

		// Token: 0x04000B21 RID: 2849
		private static OwaRegistryKey[][] keys = new OwaRegistryKey[][]
		{
			OwaRegistryKeys.owaKeys,
			OwaRegistryKeys.owaWebReadyDocumentViewingKeys,
			OwaRegistryKeys.owaIMKeys,
			OwaRegistryKeys.owaSMimeKeys,
			OwaRegistryKeys.owaSetupKeys
		};

		// Token: 0x04000B22 RID: 2850
		private static Dictionary<OwaRegistryKey, object> keyValueCache = new Dictionary<OwaRegistryKey, object>(OwaRegistryKeys.keys.Length);
	}
}
