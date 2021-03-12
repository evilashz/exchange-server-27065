using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Configuration;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000C6 RID: 198
	internal class AttachmentPolicy
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x0001A370 File Offset: 0x00018570
		internal AttachmentPolicy(string[] blockFileTypes, string[] blockMimeTypes, string[] forceSaveFileTypes, string[] forceSaveMimeTypes, string[] allowFileTypes, string[] allowMimeTypes, AttachmentPolicyLevel treatUnknownTypeAs, bool directFileAccessOnPublicComputersEnabled, bool directFileAccessOnPrivateComputersEnabled, bool forceWacViewingFirstOnPublicComputers, bool forceWacViewingFirstOnPrivateComputers, bool wacViewingOnPublicComputersEnabled, bool wacViewingOnPrivateComputersEnabled, bool forceWebReadyDocumentViewingFirstOnPublicComputers, bool forceWebReadyDocumentViewingFirstOnPrivateComputers, bool webReadyDocumentViewingOnPublicComputersEnabled, bool webReadyDocumentViewingOnPrivateComputersEnabled, string[] webReadyFileTypes, string[] webReadyMimeTypes, string[] webReadyDocumentViewingSupportedFileTypes, string[] webReadyDocumentViewingSupportedMimeTypes, bool webReadyDocumentViewingForAllSupportedTypes)
		{
			this.treatUnknownTypeAs = treatUnknownTypeAs;
			this.directFileAccessOnPublicComputersEnabled = directFileAccessOnPublicComputersEnabled;
			this.directFileAccessOnPrivateComputersEnabled = directFileAccessOnPrivateComputersEnabled;
			this.forceWacViewingFirstOnPublicComputers = forceWacViewingFirstOnPublicComputers;
			this.forceWacViewingFirstOnPrivateComputers = forceWacViewingFirstOnPrivateComputers;
			this.wacViewingOnPublicComputersEnabled = wacViewingOnPublicComputersEnabled;
			this.wacViewingOnPrivateComputersEnabled = wacViewingOnPrivateComputersEnabled;
			this.forceWebReadyDocumentViewingFirstOnPublicComputers = forceWebReadyDocumentViewingFirstOnPublicComputers;
			this.forceWebReadyDocumentViewingFirstOnPrivateComputers = forceWebReadyDocumentViewingFirstOnPrivateComputers;
			this.webReadyDocumentViewingOnPublicComputersEnabled = webReadyDocumentViewingOnPublicComputersEnabled;
			this.webReadyDocumentViewingOnPrivateComputersEnabled = webReadyDocumentViewingOnPrivateComputersEnabled;
			this.webReadyDocumentViewingForAllSupportedTypes = webReadyDocumentViewingForAllSupportedTypes;
			this.webReadyFileTypes = webReadyFileTypes;
			Array.Sort<string>(this.webReadyFileTypes);
			this.webReadyMimeTypes = webReadyMimeTypes;
			Array.Sort<string>(this.webReadyMimeTypes);
			this.webReadyDocumentViewingSupportedFileTypes = webReadyDocumentViewingSupportedFileTypes;
			Array.Sort<string>(this.webReadyDocumentViewingSupportedFileTypes);
			this.webReadyDocumentViewingSupportedMimeTypes = webReadyDocumentViewingSupportedMimeTypes;
			Array.Sort<string>(this.webReadyDocumentViewingSupportedMimeTypes);
			this.fileTypeLevels = AttachmentPolicy.LoadDictionary(blockFileTypes, forceSaveFileTypes, allowFileTypes);
			this.mimeTypeLevels = AttachmentPolicy.LoadDictionary(blockMimeTypes, forceSaveMimeTypes, allowMimeTypes);
			this.blockedFileTypes = blockFileTypes;
			this.blockedMimeTypes = blockMimeTypes;
			this.forceSaveFileTypes = forceSaveFileTypes;
			this.forceSaveMimeTypes = forceSaveMimeTypes;
			this.allowedFileTypes = allowFileTypes;
			this.allowedMimeTypes = allowMimeTypes;
			this.policyData = new OwaAttachmentPolicyData
			{
				AllowFileTypes = this.allowedFileTypes,
				AllowMimeTypes = this.allowedMimeTypes,
				BlockFileTypes = this.blockedFileTypes,
				BlockMimeTypes = this.blockedMimeTypes,
				DirectFileAccessOnPrivateComputersEnabled = directFileAccessOnPrivateComputersEnabled,
				DirectFileAccessOnPublicComputersEnabled = directFileAccessOnPublicComputersEnabled,
				ForceSaveFileTypes = forceSaveFileTypes,
				ForceSaveMimeTypes = forceSaveMimeTypes,
				ForceWacViewingFirstOnPrivateComputers = forceWacViewingFirstOnPrivateComputers,
				ForceWacViewingFirstOnPublicComputers = forceWacViewingFirstOnPublicComputers,
				ForceWebReadyDocumentViewingFirstOnPrivateComputers = forceWebReadyDocumentViewingFirstOnPrivateComputers,
				ForceWebReadyDocumentViewingFirstOnPublicComputers = forceWebReadyDocumentViewingFirstOnPublicComputers,
				TreatUnknownTypeAs = treatUnknownTypeAs.ToString(),
				WacViewingOnPrivateComputersEnabled = wacViewingOnPrivateComputersEnabled,
				WacViewingOnPublicComputersEnabled = wacViewingOnPublicComputersEnabled,
				WebReadyDocumentViewingForAllSupportedTypes = webReadyDocumentViewingForAllSupportedTypes,
				WebReadyDocumentViewingOnPrivateComputersEnabled = webReadyDocumentViewingOnPrivateComputersEnabled,
				WebReadyDocumentViewingOnPublicComputersEnabled = webReadyDocumentViewingOnPublicComputersEnabled,
				WebReadyDocumentViewingSupportedFileTypes = webReadyDocumentViewingSupportedFileTypes,
				WebReadyDocumentViewingSupportedMimeTypes = webReadyDocumentViewingSupportedMimeTypes,
				WebReadyFileTypes = webReadyFileTypes,
				WebReadyMimeTypes = webReadyMimeTypes
			};
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x0001A551 File Offset: 0x00018751
		internal AttachmentPolicyLevel TreatUnknownTypeAs
		{
			get
			{
				return this.treatUnknownTypeAs;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0001A559 File Offset: 0x00018759
		internal OwaAttachmentPolicyData PolicyData
		{
			get
			{
				return this.policyData;
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001A561 File Offset: 0x00018761
		internal bool GetDirectFileAccessEnabled(bool isPublicLogon)
		{
			if (isPublicLogon)
			{
				return this.directFileAccessOnPublicComputersEnabled;
			}
			return this.directFileAccessOnPrivateComputersEnabled;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001A573 File Offset: 0x00018773
		internal bool GetWacViewingEnabled(bool isPublicLogon)
		{
			if (isPublicLogon)
			{
				return this.wacViewingOnPublicComputersEnabled;
			}
			return this.wacViewingOnPrivateComputersEnabled;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001A585 File Offset: 0x00018785
		internal bool GetForceWacViewingFirstEnabled(bool isPublicLogon)
		{
			if (isPublicLogon)
			{
				return this.forceWacViewingFirstOnPublicComputers;
			}
			return this.forceWacViewingFirstOnPrivateComputers;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001A598 File Offset: 0x00018798
		internal AttachmentPolicyLevel GetLevel(string attachmentType, AttachmentPolicy.TypeSignifier typeSignifier)
		{
			AttachmentPolicyLevel result = AttachmentPolicyLevel.Unknown;
			switch (typeSignifier)
			{
			case AttachmentPolicy.TypeSignifier.File:
				result = AttachmentPolicy.FindLevel(this.fileTypeLevels, attachmentType);
				break;
			case AttachmentPolicy.TypeSignifier.Mime:
				result = AttachmentPolicy.FindLevel(this.mimeTypeLevels, attachmentType);
				break;
			}
			return result;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001A5D8 File Offset: 0x000187D8
		internal AttachmentPolicyType CreateAttachmentPolicyType(UserContext userContext, UserAgent userAgent, WacConfigData wacData)
		{
			AttachmentPolicyType attachmentPolicyType = new AttachmentPolicyType();
			attachmentPolicyType.DirectFileAccessOnPublicComputersEnabled = this.directFileAccessOnPublicComputersEnabled;
			attachmentPolicyType.DirectFileAccessOnPrivateComputersEnabled = this.directFileAccessOnPrivateComputersEnabled;
			attachmentPolicyType.AllowedFileTypes = this.allowedFileTypes;
			attachmentPolicyType.AllowedMimeTypes = this.allowedMimeTypes;
			attachmentPolicyType.BlockedFileTypes = this.blockedFileTypes;
			attachmentPolicyType.BlockedMimeTypes = this.blockedMimeTypes;
			attachmentPolicyType.ForceSaveFileTypes = this.forceSaveFileTypes;
			attachmentPolicyType.ForceSaveMimeTypes = this.forceSaveMimeTypes;
			attachmentPolicyType.ActionForUnknownFileAndMIMETypes = this.treatUnknownTypeAs.ToString();
			if (userAgent != null && (string.Equals(userAgent.Platform, "iPhone", StringComparison.OrdinalIgnoreCase) || string.Equals(userAgent.Platform, "iPad", StringComparison.OrdinalIgnoreCase)))
			{
				string[] collection = new string[]
				{
					".dotm",
					".ppsm",
					".pptm",
					".xlsb",
					".xlsm",
					".wma"
				};
				List<string> list = new List<string>();
				list.AddRange(this.blockedFileTypes);
				list.AddRange(collection);
				attachmentPolicyType.BlockedFileTypes = list.ToArray();
			}
			attachmentPolicyType.AttachmentDataProviderAvailable = AttachmentPolicy.IsAttachmentDataProviderAvailable(wacData);
			userContext.SetWacEditingEnabled(wacData);
			attachmentPolicyType.WacViewableFileTypes = wacData.WacViewableFileTypes;
			attachmentPolicyType.WacEditableFileTypes = (userContext.IsWacEditingEnabled ? wacData.WacEditableFileTypes : new string[0]);
			attachmentPolicyType.WacViewingOnPublicComputersEnabled = this.wacViewingOnPublicComputersEnabled;
			attachmentPolicyType.WacViewingOnPrivateComputersEnabled = this.wacViewingOnPrivateComputersEnabled;
			attachmentPolicyType.ForceWacViewingFirstOnPublicComputers = this.forceWacViewingFirstOnPublicComputers;
			attachmentPolicyType.ForceWacViewingFirstOnPrivateComputers = this.forceWacViewingFirstOnPrivateComputers;
			attachmentPolicyType.ForceWebReadyDocumentViewingFirstOnPublicComputers = this.forceWebReadyDocumentViewingFirstOnPublicComputers;
			attachmentPolicyType.ForceWebReadyDocumentViewingFirstOnPrivateComputers = this.forceWebReadyDocumentViewingFirstOnPrivateComputers;
			attachmentPolicyType.WebReadyDocumentViewingOnPublicComputersEnabled = this.webReadyDocumentViewingOnPublicComputersEnabled;
			attachmentPolicyType.WebReadyDocumentViewingOnPrivateComputersEnabled = this.webReadyDocumentViewingOnPrivateComputersEnabled;
			attachmentPolicyType.WebReadyFileTypes = this.webReadyFileTypes;
			attachmentPolicyType.WebReadyMimeTypes = this.webReadyMimeTypes;
			attachmentPolicyType.WebReadyDocumentViewingSupportedFileTypes = this.webReadyDocumentViewingSupportedFileTypes;
			attachmentPolicyType.WebReadyDocumentViewingSupportedMimeTypes = this.webReadyDocumentViewingSupportedMimeTypes;
			attachmentPolicyType.WebReadyDocumentViewingForAllSupportedTypes = this.webReadyDocumentViewingForAllSupportedTypes;
			attachmentPolicyType.DirectFileAccessOnPrivateComputersEnabled = this.directFileAccessOnPrivateComputersEnabled;
			attachmentPolicyType.DirectFileAccessOnPublicComputersEnabled = this.directFileAccessOnPublicComputersEnabled;
			return attachmentPolicyType;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001A7DB File Offset: 0x000189DB
		internal static bool IsAttachmentDataProviderAvailable(WacConfigData wacData)
		{
			return !string.IsNullOrEmpty(wacData.OneDriveDocumentsUrl) || AttachmentDataProviderManager.IsMockDataProviderEnabled();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001A808 File Offset: 0x00018A08
		internal static WacConfigData ReadAggregatedWacData(UserContext userContext, UserConfigurationManager.IAggregationContext ctx)
		{
			return UserContextUtilities.ReadAggregatedType<WacConfigData>(ctx, "OWA.WacData", () => AttachmentPolicy.GetWacConfigData(userContext));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001A83C File Offset: 0x00018A3C
		private static WacConfigData GetWacConfigData(UserContext userContext)
		{
			WacConfigData wacConfigData = new WacConfigData();
			if (userContext == null)
			{
				throw new NullReferenceException("Retail assert: UserContext is null.");
			}
			if (userContext.FeaturesManager == null)
			{
				throw new NullReferenceException("Retail assert: UserContext.FeaturesManager is null.");
			}
			if (WacConfiguration.Instance == null)
			{
				throw new NullReferenceException("Retail assert: WacConfiguration.Instance is null.");
			}
			if (WacDiscoveryManager.Instance == null)
			{
				throw new NullReferenceException("Retail assert: WacDiscoveryManager.Instance is null.");
			}
			if (WacDiscoveryManager.Instance.WacDiscoveryResult == null)
			{
				throw new NullReferenceException("Retail assert: WacDiscoveryManager.Instance.WacDiscoveryResult is null.");
			}
			if (!WacConfiguration.Instance.BlockWacViewingThroughUI)
			{
				if (userContext.FeaturesManager.ClientServerSettings.DocCollab.Enabled || userContext.FeaturesManager.ClientServerSettings.ModernAttachments.Enabled)
				{
					wacConfigData.IsWacEditingEnabled = WacConfiguration.Instance.EditingEnabled;
				}
				else
				{
					wacConfigData.IsWacEditingEnabled = false;
				}
				try
				{
					wacConfigData.WacViewableFileTypes = WacDiscoveryManager.Instance.WacDiscoveryResult.WacViewableFileTypes;
					wacConfigData.WacEditableFileTypes = WacDiscoveryManager.Instance.WacDiscoveryResult.WacEditableFileTypes;
					wacConfigData.WacDiscoverySucceeded = true;
				}
				catch (WacDiscoveryFailureException ex)
				{
					OwaDiagnostics.PublishMonitoringEventNotification(ExchangeComponent.OwaDependency.Name, "DocCollab", ex.Message, ResultSeverityLevel.Error);
					wacConfigData.WacViewableFileTypes = new string[0];
					wacConfigData.WacEditableFileTypes = new string[0];
					wacConfigData.WacDiscoverySucceeded = false;
					wacConfigData.IsWacEditingEnabled = false;
				}
			}
			if (userContext.IsBposUser)
			{
				string text;
				string text2;
				wacConfigData.OneDriveDiscoverySucceeded = OneDriveProAttachmentDataProvider.TryGetBposDocumentsInfoFromBox(userContext, CallContext.Current, out text, out text2);
				wacConfigData.OneDriveDocumentsUrl = (text ?? string.Empty);
				wacConfigData.OneDriveDocumentsDisplayName = (text2 ?? string.Empty);
			}
			else
			{
				wacConfigData.OneDriveDiscoverySucceeded = true;
				wacConfigData.OneDriveDocumentsUrl = string.Empty;
				wacConfigData.OneDriveDocumentsDisplayName = string.Empty;
			}
			return wacConfigData;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		private static SortedDictionary<string, AttachmentPolicyLevel> LoadDictionary(string[] block, string[] forceSave, string[] allow)
		{
			string[][] array = new string[3][];
			AttachmentPolicyLevel[] array2 = new AttachmentPolicyLevel[3];
			array[1] = block;
			array[2] = forceSave;
			array[0] = allow;
			array2[1] = AttachmentPolicyLevel.Block;
			array2[2] = AttachmentPolicyLevel.ForceSave;
			array2[0] = AttachmentPolicyLevel.Allow;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					array[i] = new string[0];
				}
			}
			SortedDictionary<string, AttachmentPolicyLevel> sortedDictionary = new SortedDictionary<string, AttachmentPolicyLevel>(StringComparer.OrdinalIgnoreCase);
			for (int j = 0; j <= 2; j++)
			{
				for (int k = 0; k < array[j].Length; k++)
				{
					string key = array[j][k];
					if (!sortedDictionary.ContainsKey(key))
					{
						sortedDictionary.Add(key, array2[j]);
					}
				}
			}
			return sortedDictionary;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001AA90 File Offset: 0x00018C90
		private static AttachmentPolicyLevel FindLevel(SortedDictionary<string, AttachmentPolicyLevel> dictionary, string attachmentType)
		{
			AttachmentPolicyLevel result;
			if (!dictionary.TryGetValue(attachmentType, out result))
			{
				return AttachmentPolicyLevel.Unknown;
			}
			return result;
		}

		// Token: 0x0400045B RID: 1115
		private readonly OwaAttachmentPolicyData policyData;

		// Token: 0x0400045C RID: 1116
		private readonly AttachmentPolicyLevel treatUnknownTypeAs;

		// Token: 0x0400045D RID: 1117
		private readonly bool directFileAccessOnPrivateComputersEnabled;

		// Token: 0x0400045E RID: 1118
		private readonly bool directFileAccessOnPublicComputersEnabled;

		// Token: 0x0400045F RID: 1119
		private readonly bool forceWacViewingFirstOnPrivateComputers;

		// Token: 0x04000460 RID: 1120
		private readonly bool forceWacViewingFirstOnPublicComputers;

		// Token: 0x04000461 RID: 1121
		private readonly bool wacViewingOnPrivateComputersEnabled;

		// Token: 0x04000462 RID: 1122
		private readonly bool wacViewingOnPublicComputersEnabled;

		// Token: 0x04000463 RID: 1123
		private readonly bool forceWebReadyDocumentViewingFirstOnPublicComputers;

		// Token: 0x04000464 RID: 1124
		private readonly bool forceWebReadyDocumentViewingFirstOnPrivateComputers;

		// Token: 0x04000465 RID: 1125
		private readonly bool webReadyDocumentViewingOnPublicComputersEnabled;

		// Token: 0x04000466 RID: 1126
		private readonly bool webReadyDocumentViewingOnPrivateComputersEnabled;

		// Token: 0x04000467 RID: 1127
		private readonly bool webReadyDocumentViewingForAllSupportedTypes;

		// Token: 0x04000468 RID: 1128
		private string[] webReadyFileTypes;

		// Token: 0x04000469 RID: 1129
		private string[] webReadyMimeTypes;

		// Token: 0x0400046A RID: 1130
		private string[] webReadyDocumentViewingSupportedFileTypes;

		// Token: 0x0400046B RID: 1131
		private string[] webReadyDocumentViewingSupportedMimeTypes;

		// Token: 0x0400046C RID: 1132
		private string[] allowedFileTypes;

		// Token: 0x0400046D RID: 1133
		private string[] allowedMimeTypes;

		// Token: 0x0400046E RID: 1134
		private string[] forceSaveFileTypes;

		// Token: 0x0400046F RID: 1135
		private string[] forceSaveMimeTypes;

		// Token: 0x04000470 RID: 1136
		private string[] blockedFileTypes;

		// Token: 0x04000471 RID: 1137
		private string[] blockedMimeTypes;

		// Token: 0x04000472 RID: 1138
		private SortedDictionary<string, AttachmentPolicyLevel> fileTypeLevels;

		// Token: 0x04000473 RID: 1139
		private SortedDictionary<string, AttachmentPolicyLevel> mimeTypeLevels;

		// Token: 0x020000C7 RID: 199
		public enum TypeSignifier
		{
			// Token: 0x04000475 RID: 1141
			File,
			// Token: 0x04000476 RID: 1142
			Mime
		}

		// Token: 0x020000C8 RID: 200
		private enum LevelPrecedence
		{
			// Token: 0x04000478 RID: 1144
			First,
			// Token: 0x04000479 RID: 1145
			Allow = 0,
			// Token: 0x0400047A RID: 1146
			Block,
			// Token: 0x0400047B RID: 1147
			ForceSave,
			// Token: 0x0400047C RID: 1148
			Last = 2
		}
	}
}
