using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A91 RID: 2705
	[Serializable]
	public sealed class PublicFolderMailboxDiagnosticsInfo : ConfigurableObject
	{
		// Token: 0x06006308 RID: 25352 RVA: 0x001A20F0 File Offset: 0x001A02F0
		public PublicFolderMailboxDiagnosticsInfo(string displayName) : base(new SimplePropertyBag(SimpleProviderObjectSchema.Identity, SimpleProviderObjectSchema.ObjectState, SimpleProviderObjectSchema.ExchangeVersion))
		{
			if (string.IsNullOrEmpty(displayName))
			{
				throw new ArgumentNullException("displayName");
			}
			this[this.propertyBag.ObjectIdentityPropertyDefinition] = new PublicFolderMailboxDiagnosticsInfoId();
			this.DisplayName = displayName;
			this.propertyBag.ResetChangeTracking();
		}

		// Token: 0x17001B73 RID: 7027
		// (get) Token: 0x06006309 RID: 25353 RVA: 0x001A2152 File Offset: 0x001A0352
		// (set) Token: 0x0600630A RID: 25354 RVA: 0x001A2164 File Offset: 0x001A0364
		public string DisplayName
		{
			get
			{
				return (string)this[PublicFolderDiagnosticsInfoSchema.DisplayName];
			}
			private set
			{
				this[PublicFolderDiagnosticsInfoSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001B74 RID: 7028
		// (get) Token: 0x0600630B RID: 25355 RVA: 0x001A2172 File Offset: 0x001A0372
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return PublicFolderMailboxDiagnosticsInfo.schema;
			}
		}

		// Token: 0x17001B75 RID: 7029
		// (get) Token: 0x0600630C RID: 25356 RVA: 0x001A2179 File Offset: 0x001A0379
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001B76 RID: 7030
		// (get) Token: 0x0600630D RID: 25357 RVA: 0x001A2180 File Offset: 0x001A0380
		// (set) Token: 0x0600630E RID: 25358 RVA: 0x001A2192 File Offset: 0x001A0392
		public PublicFolderMailboxSynchronizerInfo SyncInfo
		{
			get
			{
				return (PublicFolderMailboxSynchronizerInfo)this[PublicFolderDiagnosticsInfoSchema.SyncInfo];
			}
			set
			{
				this[PublicFolderDiagnosticsInfoSchema.SyncInfo] = value;
			}
		}

		// Token: 0x17001B77 RID: 7031
		// (get) Token: 0x0600630F RID: 25359 RVA: 0x001A21A0 File Offset: 0x001A03A0
		// (set) Token: 0x06006310 RID: 25360 RVA: 0x001A21B2 File Offset: 0x001A03B2
		public PublicFolderMailboxAssistantInfo AssistantInfo
		{
			get
			{
				return (PublicFolderMailboxAssistantInfo)this[PublicFolderDiagnosticsInfoSchema.AssistantInfo];
			}
			set
			{
				this[PublicFolderDiagnosticsInfoSchema.AssistantInfo] = value;
			}
		}

		// Token: 0x17001B78 RID: 7032
		// (get) Token: 0x06006311 RID: 25361 RVA: 0x001A21C0 File Offset: 0x001A03C0
		// (set) Token: 0x06006312 RID: 25362 RVA: 0x001A21D2 File Offset: 0x001A03D2
		public PublicFolderMailboxDumpsterInfo DumpsterInfo
		{
			get
			{
				return (PublicFolderMailboxDumpsterInfo)this[PublicFolderDiagnosticsInfoSchema.DumpsterInfo];
			}
			set
			{
				this[PublicFolderDiagnosticsInfoSchema.DumpsterInfo] = value;
			}
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x06006313 RID: 25363 RVA: 0x001A21E0 File Offset: 0x001A03E0
		// (set) Token: 0x06006314 RID: 25364 RVA: 0x001A21F2 File Offset: 0x001A03F2
		public PublicFolderMailboxHierarchyInfo HierarchyInfo
		{
			get
			{
				return (PublicFolderMailboxHierarchyInfo)this[PublicFolderDiagnosticsInfoSchema.HierarchyInfo];
			}
			set
			{
				this[PublicFolderDiagnosticsInfoSchema.HierarchyInfo] = value;
			}
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x001A2200 File Offset: 0x001A0400
		internal static PublicFolderMailboxDiagnosticsInfo Load(OrganizationId organizationId, Guid contentMailboxGuid, DiagnosticsLoadFlags loadFlags, Action<LocalizedString, LocalizedString, int> writeProgress)
		{
			PublicFolderMailboxDiagnosticsInfo result;
			using (PublicFolderSession publicFolderSession = PublicFolderSession.OpenAsAdmin(organizationId, null, contentMailboxGuid, null, CultureInfo.CurrentCulture, "Client=Management;Action=Get-PublicFolderMailboxDiagnostics", null))
			{
				result = PublicFolderMailboxDiagnosticsInfo.Load(publicFolderSession, loadFlags, writeProgress);
			}
			return result;
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x001A2248 File Offset: 0x001A0448
		internal static PublicFolderMailboxDiagnosticsInfo Load(PublicFolderSession session, DiagnosticsLoadFlags loadFlags, Action<LocalizedString, LocalizedString, int> writeProgress)
		{
			PublicFolderMailboxDiagnosticsInfo publicFolderMailboxDiagnosticsInfo = new PublicFolderMailboxDiagnosticsInfo("Public Folder Diagnostics Information");
			publicFolderMailboxDiagnosticsInfo.SyncInfo = (PublicFolderMailboxDiagnosticsInfo.LoadMailboxInfo<PublicFolderMailboxSynchronizerInfo>(session, "PublicFolderSyncInfo", "PublicFolderLastSyncCylceLog") as PublicFolderMailboxSynchronizerInfo);
			publicFolderMailboxDiagnosticsInfo.AssistantInfo = (PublicFolderMailboxDiagnosticsInfo.LoadMailboxInfo<PublicFolderMailboxAssistantInfo>(session, "PublicFolderAssistantInfo", "PublicFolderLastAssistantCycleLog") as PublicFolderMailboxAssistantInfo);
			if ((loadFlags & DiagnosticsLoadFlags.DumpsterInfo) != DiagnosticsLoadFlags.Default)
			{
				publicFolderMailboxDiagnosticsInfo.DumpsterInfo = PublicFolderMailboxDumpsterInfo.LoadInfo(session, writeProgress);
			}
			if ((loadFlags & DiagnosticsLoadFlags.HierarchyInfo) != DiagnosticsLoadFlags.Default)
			{
				publicFolderMailboxDiagnosticsInfo.HierarchyInfo = PublicFolderMailboxHierarchyInfo.LoadInfo(session, writeProgress);
			}
			return publicFolderMailboxDiagnosticsInfo;
		}

		// Token: 0x06006317 RID: 25367 RVA: 0x001A22BC File Offset: 0x001A04BC
		private static PublicFolderMailboxMonitoringInfo LoadMailboxInfo<TValue>(PublicFolderSession session, string stateInfoConfigurationName, string logInfoConfigurationName) where TValue : PublicFolderMailboxMonitoringInfo, new()
		{
			TValue tvalue = Activator.CreateInstance<TValue>();
			using (Folder folder = Folder.Bind(session, session.GetTombstonesRootFolderId()))
			{
				using (UserConfiguration configuration = UserConfiguration.GetConfiguration(folder, new UserConfigurationName(stateInfoConfigurationName, ConfigurationNameKind.Name), UserConfigurationTypes.Dictionary))
				{
					tvalue.LastAttemptedSyncTime = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "LastAttemptedSyncTime") as ExDateTime?);
					tvalue.LastFailedSyncTime = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "LastFailedSyncTime") as ExDateTime?);
					tvalue.LastSuccessfulSyncTime = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "LastSuccessfulSyncTime") as ExDateTime?);
					tvalue.LastSyncFailure = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "LastSyncFailure") as string);
					tvalue.NumberofAttemptsAfterLastSuccess = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "NumberofAttemptsAfterLastSuccess") as int?);
					tvalue.FirstFailedSyncTimeAfterLastSuccess = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "FirstFailedSyncTimeAfterLastSuccess") as ExDateTime?);
					PublicFolderMailboxSynchronizerInfo publicFolderMailboxSynchronizerInfo = tvalue as PublicFolderMailboxSynchronizerInfo;
					if (publicFolderMailboxSynchronizerInfo != null)
					{
						publicFolderMailboxSynchronizerInfo.NumberOfBatchesExecuted = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "NumberOfBatchesExecuted") as int?);
						publicFolderMailboxSynchronizerInfo.NumberOfFoldersToBeSynced = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "NumberOfFoldersToBeSynced") as int?);
						publicFolderMailboxSynchronizerInfo.NumberOfFoldersSynced = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "NumberOfFoldersSynced") as int?);
						publicFolderMailboxSynchronizerInfo.BatchSize = (PublicFolderMailboxDiagnosticsInfo.GetMetadataValue(configuration, "BatchSize") as int?);
					}
				}
				using (UserConfiguration configuration2 = UserConfiguration.GetConfiguration(folder, new UserConfigurationName(logInfoConfigurationName, ConfigurationNameKind.Name), UserConfigurationTypes.Stream))
				{
					using (Stream stream = configuration2.GetStream())
					{
						using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress, true))
						{
							using (MemoryStream memoryStream = new MemoryStream())
							{
								gzipStream.CopyTo(memoryStream);
								tvalue.LastSyncCycleLog = Encoding.ASCII.GetString(memoryStream.ToArray());
							}
						}
					}
				}
			}
			return tvalue;
		}

		// Token: 0x06006318 RID: 25368 RVA: 0x001A256C File Offset: 0x001A076C
		private static object GetMetadataValue(UserConfiguration metadata, string name)
		{
			IDictionary dictionary = metadata.GetDictionary();
			if (dictionary.Contains(name))
			{
				return dictionary[name];
			}
			return null;
		}

		// Token: 0x04003800 RID: 14336
		private static readonly PublicFolderDiagnosticsInfoSchema schema = ObjectSchema.GetInstance<PublicFolderDiagnosticsInfoSchema>();
	}
}
