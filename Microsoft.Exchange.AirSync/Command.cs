using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.XSO;
using Microsoft.Exchange.AirSync.SyncStateConverter;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000042 RID: 66
	internal abstract class Command : IDisposeTrackable, IDisposable, ITask, ITaskTimeout
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00016CE9 File Offset: 0x00014EE9
		private static Stack<Command> CommandStack
		{
			get
			{
				if (Command.commandStack == null)
				{
					Command.commandStack = new Stack<Command>();
				}
				return Command.commandStack;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00016D01 File Offset: 0x00014F01
		internal static OrganizationId CurrentOrganizationId
		{
			get
			{
				return Command.CurrentCommand.User.OrganizationId;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00016D12 File Offset: 0x00014F12
		internal static Command CurrentCommand
		{
			get
			{
				if (Command.CommandStack.Count != 0)
				{
					return Command.CommandStack.Peek();
				}
				return null;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00016D2C File Offset: 0x00014F2C
		protected static string MachineName
		{
			get
			{
				return Command.machineName;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00016D34 File Offset: 0x00014F34
		public static void DetermineDeviceAccessState(IOrganizationSettingsData organizationSettingsData, string deviceType, string deviceModel, string userAgent, string deviceOS, out DeviceAccessState deviceAccessState, out DeviceAccessStateReason accessStateReason, out ADObjectId deviceAccessControlRule)
		{
			if (organizationSettingsData == null)
			{
				throw new ArgumentNullException("organizationSettingsData");
			}
			if (deviceType == null)
			{
				throw new ArgumentNullException("deviceType");
			}
			if (deviceModel == null)
			{
				throw new ArgumentNullException("deviceModel");
			}
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "Determine access state for device type '{0}', model '{1}', OS '{2}', UserAgent '{3}'.", new object[]
			{
				deviceType,
				deviceModel,
				deviceOS,
				userAgent
			});
			if (!organizationSettingsData.IsRulesListEmpty)
			{
				DeviceAccessRuleData deviceAccessRuleData;
				if (!string.IsNullOrEmpty(userAgent))
				{
					deviceAccessRuleData = organizationSettingsData.EvaluateDevice(DeviceAccessCharacteristic.UserAgent, userAgent);
					if (deviceAccessRuleData != null)
					{
						accessStateReason = DeviceAccessStateReason.DeviceRule;
						deviceAccessControlRule = deviceAccessRuleData.Identity;
						deviceAccessState = deviceAccessRuleData.AccessState;
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] UserAgent: '{0}' - Org Settings Data evaluated to true.  AccessState: '{1}', Rule: '{2}', Reason: '{3}'", new object[]
						{
							userAgent,
							deviceAccessState,
							deviceAccessControlRule,
							accessStateReason
						});
						return;
					}
				}
				if (!string.IsNullOrEmpty(deviceOS))
				{
					deviceAccessRuleData = organizationSettingsData.EvaluateDevice(DeviceAccessCharacteristic.DeviceOS, deviceOS);
					if (deviceAccessRuleData != null)
					{
						accessStateReason = DeviceAccessStateReason.DeviceRule;
						deviceAccessControlRule = deviceAccessRuleData.Identity;
						deviceAccessState = deviceAccessRuleData.AccessState;
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] DeviceOS: '{0}' - Org Settings Data evaluated to true.  AccessState: '{1}', Rule: '{2}', Reason: '{3}'", new object[]
						{
							deviceOS,
							deviceAccessState,
							deviceAccessControlRule,
							accessStateReason
						});
						return;
					}
				}
				deviceAccessRuleData = organizationSettingsData.EvaluateDevice(DeviceAccessCharacteristic.DeviceModel, deviceModel);
				if (deviceAccessRuleData != null)
				{
					accessStateReason = DeviceAccessStateReason.DeviceRule;
					deviceAccessControlRule = deviceAccessRuleData.Identity;
					deviceAccessState = deviceAccessRuleData.AccessState;
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] DeviceModel: '{0}' - Org Settings Data evaluated to true.  AccessState: '{1}', Rule: '{2}', Reason: '{3}'", new object[]
					{
						deviceModel,
						deviceAccessState,
						deviceAccessControlRule,
						accessStateReason
					});
					return;
				}
				deviceAccessRuleData = organizationSettingsData.EvaluateDevice(DeviceAccessCharacteristic.DeviceType, deviceType);
				if (deviceAccessRuleData != null)
				{
					accessStateReason = DeviceAccessStateReason.DeviceRule;
					deviceAccessControlRule = deviceAccessRuleData.Identity;
					deviceAccessState = deviceAccessRuleData.AccessState;
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] DeviceType: '{0}' - Org Settings Data evaluated to true.  AccessState: '{1}', Rule: '{2}', Reason: '{3}'", new object[]
					{
						deviceType,
						deviceAccessState,
						deviceAccessControlRule,
						accessStateReason
					});
					return;
				}
			}
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] Check organization global device access.");
			deviceAccessControlRule = null;
			accessStateReason = DeviceAccessStateReason.Global;
			switch (organizationSettingsData.DefaultAccessLevel)
			{
			case DeviceAccessLevel.Allow:
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] Global allow due to org Settings Data.");
				deviceAccessState = DeviceAccessState.Allowed;
				return;
			case DeviceAccessLevel.Block:
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] Global block due to org Settings Data.");
				deviceAccessState = DeviceAccessState.Blocked;
				return;
			case DeviceAccessLevel.Quarantine:
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] Global quarantine due to org Settings Data.");
				deviceAccessState = DeviceAccessState.Quarantined;
				return;
			default:
				AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "[Command.DetermineDeviceAccessState] Global UNKNOWN due to org Settings Data.");
				deviceAccessState = DeviceAccessState.Unknown;
				if (Command.CurrentCommand != null && Command.CurrentCommand.ProtocolLogger != null)
				{
					Command.CurrentCommand.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.Error, "ABQGlobalUnknown");
				}
				return;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00016FDB File Offset: 0x000151DB
		internal static void ClearContextDataInTls()
		{
			Command.CommandStack.Pop();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00016FE8 File Offset: 0x000151E8
		internal static float GetFolderHierarchyICSPercentage()
		{
			long num = Command.numSkinnyICSFolderChecks;
			long num2 = Command.numFatDeepTraversalFolderChecks;
			double num3 = (double)(num + num2);
			if (num3 != 0.0)
			{
				return (float)((double)num / num3) * 100f;
			}
			return 0f;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00017024 File Offset: 0x00015224
		internal static void DetectPolicyChange(IPolicyData policyData, IGlobalInfo mbxInfo, int version, out bool policyIsCompatibleWithDevice)
		{
			policyIsCompatibleWithDevice = true;
			if (policyData == null)
			{
				if (mbxInfo.LastPolicyXMLHash != null)
				{
					mbxInfo.PolicyKeyNeeded = 0U;
					mbxInfo.PolicyKeyWaitingAck = Command.GenerateNewPolicyKey(mbxInfo);
					mbxInfo.LastPolicyXMLHash = null;
					mbxInfo.DevicePolicyApplied = null;
				}
				return;
			}
			policyIsCompatibleWithDevice = policyData.GetVersionCompatibility(version);
			int hashCode = policyData.GetHashCode(version);
			if (mbxInfo.LastPolicyXMLHash != null && hashCode == mbxInfo.LastPolicyXMLHash.Value)
			{
				return;
			}
			mbxInfo.PolicyKeyNeeded = Command.GenerateNewPolicyKey(mbxInfo);
			mbxInfo.PolicyKeyWaitingAck = Command.GenerateNewPolicyKey(mbxInfo);
			mbxInfo.LastPolicyXMLHash = new int?(hashCode);
			mbxInfo.DevicePolicyApplied = policyData.Identity;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000170D8 File Offset: 0x000152D8
		internal bool TryParseDeviceOSFromUserAgent(out string deviceOS)
		{
			deviceOS = null;
			if (!GlobalSettings.DeviceTypesToParseOSVersion.Contains(this.Context.DeviceIdentity.DeviceType.ToLower()))
			{
				return false;
			}
			AirSyncDiagnostics.TraceDebug<string, string>(ExTraceGlobals.RequestsTracer, this, "TryParseDeviceOSFromUserAgent:: deviceUserAgent:{0}, deviceType:{1}", this.EffectiveUserAgent, this.Context.DeviceIdentity.DeviceType);
			if (!string.IsNullOrEmpty(this.EffectiveUserAgent))
			{
				if (this.Context.DeviceIdentity.DeviceType.ToUpper().Contains("SAMSUNG"))
				{
					int num = this.EffectiveUserAgent.LastIndexOf(".");
					if (num > 0)
					{
						string text = this.EffectiveUserAgent.Substring(num + 1);
						int num2;
						if (int.TryParse(text, out num2))
						{
							deviceOS = string.Format("{0} {1}", "Android", text.Replace('0', '.'));
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "TryParseDeviceOSFromUserAgent:: deviceOSWithVersion:{0}", deviceOS);
							return true;
						}
					}
				}
				else
				{
					int num3 = this.EffectiveUserAgent.IndexOf("-");
					if (num3 > 0)
					{
						string text2 = this.EffectiveUserAgent.Substring(0, num3);
						deviceOS = text2.Replace("/", " ");
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "TryParseDeviceOSFromUserAgent:: deviceOSWithVersion:{0}", deviceOS);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001720E File Offset: 0x0001540E
		protected void SaveLatestIcsFolderHierarchySnapshot(FolderHierarchyChangeDetector.SyncHierarchyManifestState latestState)
		{
			if (latestState != null)
			{
				this.DeviceSyncStateMetadata.RecordLatestFolderHierarchySnapshot(latestState);
				return;
			}
			this.DeviceSyncStateMetadata.RecordLatestFolderHierarchySnapshot(this.MailboxSession, this.Context);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00017238 File Offset: 0x00015438
		protected Command.IcsFolderCheckResults PerformICSFolderHierarchyChangeCheck(ref SyncState folderIdMappingState, out FolderHierarchyChangeDetector.SyncHierarchyManifestState latestState)
		{
			Command.IcsFolderCheckResults icsFolderCheckResults = Command.IcsFolderCheckResults.ChangesNeedDeepCheck;
			latestState = null;
			Command.IcsFolderCheckResults icsFolderCheckResults2;
			try
			{
				FolderHierarchyChangeDetector.MailboxChangesManifest folderHierarchyICSChanges = this.DeviceSyncStateMetadata.GetFolderHierarchyICSChanges(this.MailboxSession, out latestState, this.Context);
				if (folderHierarchyICSChanges != null)
				{
					if (!folderHierarchyICSChanges.HasChanges)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[Command.PerformICSFolderHierarchyChangeCheck] Folder Hierarchy ICS check says state has not changed.");
						icsFolderCheckResults = Command.IcsFolderCheckResults.NoChanges;
						return icsFolderCheckResults;
					}
					if (folderHierarchyICSChanges.DeletedFolders.Count > 0)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[Command.PerformICSFolderHierarchyChangeCheck] Folder Hierarchy ICS check says folders were deleted.  Skipping deep traversal.");
						icsFolderCheckResults = Command.IcsFolderCheckResults.ChangesNoDeepCheck;
						return icsFolderCheckResults;
					}
					if (folderIdMappingState == null)
					{
						folderIdMappingState = this.SyncStateStorage.GetCustomSyncState(new FolderIdMappingSyncStateInfo(), new PropertyDefinition[0]);
					}
					if (folderIdMappingState == null)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[Command.PerformICSFolderHierarchyChangeCheck] FolderIdMapping sync state is missing.  Must do folder sync.");
						icsFolderCheckResults = Command.IcsFolderCheckResults.ChangesNoDeepCheck;
						return icsFolderCheckResults;
					}
					FolderTree folderTree = (FolderTree)folderIdMappingState[CustomStateDatumType.FullFolderTree];
					if (folderTree == null)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[Command.PerformICSFolderHierarchyChangeCheck] FullFolderTree data is missing.  Must do folder sync.");
						icsFolderCheckResults = Command.IcsFolderCheckResults.ChangesNoDeepCheck;
						return icsFolderCheckResults;
					}
					foreach (KeyValuePair<StoreObjectId, string> keyValuePair in folderHierarchyICSChanges.ChangedFolders)
					{
						MailboxSyncItemId folderId = MailboxSyncItemId.CreateForNewItem(keyValuePair.Key);
						if (!folderTree.Contains(folderId))
						{
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[Command.PerformICSFolderHierarchyChangeCheck] Folder '{0}' was an add.  Must do folder sync.", keyValuePair.Value);
							icsFolderCheckResults = Command.IcsFolderCheckResults.ChangesNoDeepCheck;
							return icsFolderCheckResults;
						}
					}
				}
				icsFolderCheckResults2 = icsFolderCheckResults;
			}
			finally
			{
				switch (icsFolderCheckResults)
				{
				case Command.IcsFolderCheckResults.NoChanges:
				case Command.IcsFolderCheckResults.ChangesNoDeepCheck:
					Interlocked.Increment(ref Command.numSkinnyICSFolderChecks);
					this.ProtocolLogger.SetValue(ProtocolLoggerData.QuickHierarchyChangeCheck, "T");
					break;
				case Command.IcsFolderCheckResults.ChangesNeedDeepCheck:
					Interlocked.Increment(ref Command.numFatDeepTraversalFolderChecks);
					this.ProtocolLogger.SetValue(ProtocolLoggerData.QuickHierarchyChangeCheck, "F");
					break;
				}
			}
			return icsFolderCheckResults2;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00017414 File Offset: 0x00015614
		private static uint GenerateNewPolicyKey(IGlobalInfo mbxInfo)
		{
			byte[] array = new byte[4];
			uint num2;
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				uint num = 0U;
				while (num == 0U || num == mbxInfo.PolicyKeyWaitingAck || num == mbxInfo.PolicyKeyNeeded)
				{
					rngcryptoServiceProvider.GetNonZeroBytes(array);
					num = BitConverter.ToUInt32(array, 0);
				}
				num2 = num;
			}
			return num2;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00017478 File Offset: 0x00015678
		private static string ReadStringFromStream(Stream stream, Encoding encoding)
		{
			AirSyncDiagnostics.Assert(stream != null);
			AirSyncDiagnostics.Assert(encoding != null);
			if (!stream.CanRead)
			{
				return string.Empty;
			}
			byte[] array = new byte[4096];
			int num = 0;
			for (;;)
			{
				int num2 = stream.Read(array, num, array.Length - num);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				if (num == array.Length)
				{
					byte[] array2 = new byte[array.Length * 2];
					array.CopyTo(array2, 0);
					array = array2;
				}
			}
			return encoding.GetString(array, 0, num);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x000174F5 File Offset: 0x000156F5
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x000174FD File Offset: 0x000156FD
		public AnnotationsManager RequestAnnotations { get; private set; }

		// Token: 0x060003FD RID: 1021 RVA: 0x00017508 File Offset: 0x00015708
		public Command()
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.WorkloadSettings = new WorkloadSettings(WorkloadType.Eas, false);
			this.RequestAnnotations = new AnnotationsManager();
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001756F File Offset: 0x0001576F
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x00017577 File Offset: 0x00015777
		public bool PerUserTracingEnabled
		{
			get
			{
				return this.perUserTracingEnabled;
			}
			set
			{
				this.perUserTracingEnabled = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00017580 File Offset: 0x00015780
		internal ProtocolLogger ProtocolLogger
		{
			get
			{
				return this.context.ProtocolLogger;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001758D File Offset: 0x0001578D
		internal DeviceIdentity DeviceIdentity
		{
			get
			{
				return this.context.DeviceIdentity;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0001759A File Offset: 0x0001579A
		internal XmlElement XmlRequest
		{
			get
			{
				return this.context.Request.CommandXml;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000175AC File Offset: 0x000157AC
		internal Stream InputStream
		{
			get
			{
				return this.context.Request.InputStream;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x000175BE File Offset: 0x000157BE
		internal string EffectiveUserAgent
		{
			get
			{
				if (this.GlobalInfo != null && string.IsNullOrEmpty(this.context.Request.UserAgent))
				{
					return this.GlobalInfo.UserAgent;
				}
				return this.context.Request.UserAgent;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x000175FB File Offset: 0x000157FB
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0001760D File Offset: 0x0001580D
		internal XmlDocument XmlResponse
		{
			get
			{
				return this.context.Response.XmlDocument;
			}
			set
			{
				this.context.Response.XmlDocument = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00017620 File Offset: 0x00015820
		internal Stream OutputStream
		{
			get
			{
				return this.context.Response.OutputStream;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x00017632 File Offset: 0x00015832
		internal IAirSyncRequest Request
		{
			get
			{
				return this.context.Request;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001763F File Offset: 0x0001583F
		internal int Version
		{
			get
			{
				return this.context.Request.Version;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x00017651 File Offset: 0x00015851
		internal bool HasOutlookExtensions
		{
			get
			{
				return this.context.Request.HasOutlookExtensions;
			}
		}

		// Token: 0x1700018E RID: 398
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x00017663 File Offset: 0x00015863
		internal LazyAsyncResult LazyAsyncResult
		{
			set
			{
				this.result = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001766C File Offset: 0x0001586C
		internal MailboxSession MailboxSession
		{
			get
			{
				if (this.securityContextAndSession != null)
				{
					return this.securityContextAndSession.MailboxSession;
				}
				return null;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00017683 File Offset: 0x00015883
		internal SyncStateStorage SyncStateStorage
		{
			get
			{
				return this.syncStateStorage;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001768B File Offset: 0x0001588B
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00017693 File Offset: 0x00015893
		internal ISyncStatusData SyncStatusSyncData
		{
			get
			{
				return this.syncStatusSyncData;
			}
			set
			{
				this.syncStatusSyncData = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001769C File Offset: 0x0001589C
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000176A4 File Offset: 0x000158A4
		internal GlobalInfo GlobalInfo { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000176AD File Offset: 0x000158AD
		internal UserSyncStateMetadata UserSyncStateMetadata
		{
			get
			{
				return UserSyncStateMetadataCache.Singleton.Get(this.MailboxSession, null);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000176C0 File Offset: 0x000158C0
		internal DeviceSyncStateMetadata DeviceSyncStateMetadata
		{
			get
			{
				return this.UserSyncStateMetadata.GetDevice(this.MailboxSession, this.DeviceIdentity, null);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000176DA File Offset: 0x000158DA
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000176E2 File Offset: 0x000158E2
		internal MeetingOrganizerSyncState MeetingOrganizerSyncState { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x000176EB File Offset: 0x000158EB
		internal MailboxLogger MailboxLogger
		{
			get
			{
				return this.mailboxLogger;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000176F3 File Offset: 0x000158F3
		internal bool MailboxLoggingEnabled
		{
			get
			{
				if (this.mailboxLoggingEnabled == null)
				{
					if (this.MailboxSession == null)
					{
						return false;
					}
					this.mailboxLoggingEnabled = new bool?(SyncStateStorage.GetMailboxLoggingEnabled(this.MailboxSession, this.Context));
				}
				return this.mailboxLoggingEnabled.Value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00017733 File Offset: 0x00015933
		internal int RequestId
		{
			get
			{
				return this.requestId;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001773B File Offset: 0x0001593B
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x00017743 File Offset: 0x00015943
		internal Stopwatch RequestWaitWatch
		{
			get
			{
				return this.requestWaitWatch;
			}
			set
			{
				this.requestWaitWatch = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0001774C File Offset: 0x0001594C
		internal virtual bool RequiresPolicyCheck
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001774F File Offset: 0x0001594F
		internal virtual bool ShouldOpenGlobalSyncState
		{
			get
			{
				return this.RequiresPolicyCheck;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00017757 File Offset: 0x00015957
		internal virtual int MinVersion
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001775B File Offset: 0x0001595B
		internal virtual int MaxVersion
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00017762 File Offset: 0x00015962
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0001776A File Offset: 0x0001596A
		public IAirSyncContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
				if (value != null)
				{
					this.budget = this.context.User.Budget;
				}
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001778C File Offset: 0x0001598C
		internal virtual bool ShouldSaveSyncStatus
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001778F File Offset: 0x0001598F
		internal Dictionary<StoreObjectId, Dictionary<AttachmentId, string>> InlineAttachmentContentIdLookUp
		{
			get
			{
				if (this.inlineAttachmentContentIdLookUp == null)
				{
					this.inlineAttachmentContentIdLookUp = new Dictionary<StoreObjectId, Dictionary<AttachmentId, string>>(1);
				}
				return this.inlineAttachmentContentIdLookUp;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000177AB File Offset: 0x000159AB
		protected virtual bool ShouldOpenSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000424 RID: 1060
		protected abstract string RootNodeName { get; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000177AE File Offset: 0x000159AE
		protected virtual string RootNodeNamespace
		{
			get
			{
				return this.RootNodeName + ":";
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000177C0 File Offset: 0x000159C0
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x000177C8 File Offset: 0x000159C8
		protected ExPerformanceCounter PerfCounter
		{
			get
			{
				return this.perfCounter;
			}
			set
			{
				this.perfCounter = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x000177D1 File Offset: 0x000159D1
		protected ExDateTime NextPolicyRefreshTime
		{
			get
			{
				return this.nextPolicyRefreshTime;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x000177D9 File Offset: 0x000159D9
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000177E1 File Offset: 0x000159E1
		protected bool SendServerUpgradeHeader
		{
			get
			{
				return this.sendServerUpgradeHeader;
			}
			set
			{
				this.sendServerUpgradeHeader = value;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000177EA File Offset: 0x000159EA
		protected internal IAirSyncUser User
		{
			get
			{
				return this.context.User;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000177F7 File Offset: 0x000159F7
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x000177FF File Offset: 0x000159FF
		protected internal bool PartialFailure { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00017808 File Offset: 0x00015A08
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00017810 File Offset: 0x00015A10
		private protected DeviceAccessState CurrentAccessState { protected get; private set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00017819 File Offset: 0x00015A19
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00017821 File Offset: 0x00015A21
		private protected DeviceAccessState PreviousAccessState { protected get; private set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001782A File Offset: 0x00015A2A
		protected bool IsInQuarantinedState
		{
			get
			{
				return this.CurrentAccessState == DeviceAccessState.Quarantined || this.CurrentAccessState == DeviceAccessState.DeviceDiscovery;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00017840 File Offset: 0x00015A40
		protected bool IsQuarantineMailAvailable
		{
			get
			{
				return this.IsInQuarantinedState && this.GlobalInfo != null && (this.GlobalInfo.ABQMailState == ABQMailState.MailPosted || this.GlobalInfo.ABQMailState == ABQMailState.MailSent);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00017872 File Offset: 0x00015A72
		protected SecurityContextAndSession SecurityContextAndSession
		{
			get
			{
				return this.securityContextAndSession;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001787A File Offset: 0x00015A7A
		internal virtual bool RightsManagementSupportFlag
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0001787D File Offset: 0x00015A7D
		private ADDeviceManager DeviceManager
		{
			get
			{
				if (this.deviceManager == null)
				{
					this.deviceManager = new ADDeviceManager(this.context);
				}
				return this.deviceManager;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001789E File Offset: 0x00015A9E
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<Command>(this);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000178A6 File Offset: 0x00015AA6
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000178BB File Offset: 0x00015ABB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000178CA File Offset: 0x00015ACA
		public void SetContextDataInTls()
		{
			Command.CommandStack.Push(this);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000178D7 File Offset: 0x00015AD7
		internal void RegisterDisposableData(IDisposable data)
		{
			if (data != null)
			{
				this.dataToBeDisposed.Add(data);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000178EC File Offset: 0x00015AEC
		internal void ReleaseDisposableData()
		{
			foreach (IDisposable disposable in this.dataToBeDisposed)
			{
				disposable.Dispose();
			}
			this.dataToBeDisposed.Clear();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001794C File Offset: 0x00015B4C
		internal virtual XmlDocument GetInvalidParametersXml()
		{
			return null;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00017950 File Offset: 0x00015B50
		internal void AddInteractiveCall()
		{
			IEasDeviceBudget easDeviceBudget = this.Budget as IEasDeviceBudget;
			if (easDeviceBudget != null)
			{
				easDeviceBudget.AddInteractiveCall();
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00017972 File Offset: 0x00015B72
		internal virtual void SetStateData(Command.StateData data)
		{
		}

		// Token: 0x06000440 RID: 1088
		internal abstract Command.ExecutionState ExecuteCommand();

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x00017974 File Offset: 0x00015B74
		protected virtual bool IsInteractiveCommand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00017977 File Offset: 0x00015B77
		protected virtual Validator GetValidator()
		{
			return new Validator(this.Version, this.HasOutlookExtensions);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001798C File Offset: 0x00015B8C
		internal virtual bool ValidateXml()
		{
			bool flag2;
			using (this.context.Tracker.Start(TimeId.CommandValidateXML))
			{
				Validator validator = this.GetValidator();
				bool flag = validator.ValidateXml(this.XmlRequest, this.RootNodeName);
				if (!flag)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, this.RootNodeName + " Command validation failed!");
					this.XmlResponse = this.GetValidationErrorXml();
					this.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.Error, "InvalidXml");
					if (validator.ValidationErrors.Count > 0)
					{
						for (int i = 0; i < validator.ValidationErrors.Count; i++)
						{
							Validator.XmlValidationError xmlValidationError = validator.ValidationErrors[i];
							this.Context.Response.AppendHeader("X-MS-ASError", xmlValidationError.ToString(), true);
						}
					}
				}
				flag2 = flag;
			}
			return flag2;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00017A78 File Offset: 0x00015C78
		internal virtual XmlDocument GetValidationErrorXml()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00017A80 File Offset: 0x00015C80
		internal XmlDocument GetCommandXmlStub()
		{
			string xml = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><{0} xmlns=\"{1}\"></{0}>", this.RootNodeName, this.RootNodeNamespace);
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(xml);
			return xmlDocument;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00017AB4 File Offset: 0x00015CB4
		protected virtual bool PreProcessRequest()
		{
			bool flag = true;
			if (this.Context.Request.IsEmpty && this.Context.Request.CommandType != CommandType.Options)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.EmptyRequest, 1);
			}
			return flag;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00017AF7 File Offset: 0x00015CF7
		protected virtual void LogRequestToMailboxLog(string requestToLog)
		{
			this.mailboxLogger.SetData(MailboxLogDataName.RequestBody, this.GetRequestDataToLog(requestToLog));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00017B0C File Offset: 0x00015D0C
		internal void WorkerThread()
		{
			if (this.perUserTracingEnabled)
			{
				AirSyncDiagnostics.SetThreadTracing();
			}
			else
			{
				AirSyncDiagnostics.ClearThreadTracing();
			}
			this.executionState = Command.ExecutionState.Invalid;
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Entry point of the worker thread", 30763);
			Guid serviceProviderRequestId = Microsoft.Exchange.Diagnostics.Trace.TraceCasStart(CasTraceEventType.ActiveSync);
			AirSyncSyncStateTypeFactory.EnsureSyncStateTypesRegistered();
			string action = null;
			try
			{
				this.SetContextDataInTls();
				ActivityContext.SetThreadScope(this.User.Context.ActivityScope);
				action = this.User.Context.ActivityScope.Action;
				this.User.Context.ActivityScope.Action = this.RootNodeName;
				this.User.Context.ActivityScope.Component = "ActiveSync";
				if (this.perfCounter != null)
				{
					this.perfCounter.Increment();
				}
				AirSyncDiagnostics.TracePfd<int, int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Checking AirSync version: {1}", 16427, this.Version);
				this.ProtocolLogger.SetValue(ProtocolLoggerData.ProtocolVersion, this.Version);
				AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Opening mailbox session", 24619);
				bool flag;
				this.GetOrCreateNotificationManager(out flag);
				if (flag)
				{
					this.executionState = Command.ExecutionState.Pending;
				}
				else
				{
					if (this.ShouldOpenSyncState)
					{
						this.OpenMailboxSession(this.User);
						AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Opening Sync Storage", 20523);
						AirSyncDiagnostics.TraceDebug<DeviceIdentity>(ExTraceGlobals.RequestsTracer, this, "DeviceIdentity: {0}.", this.DeviceIdentity);
						this.OpenSyncStorage(this.ShouldOpenGlobalSyncState);
						this.SetNotificationManagerMailboxLogging(this.MailboxLoggingEnabled);
					}
					bool flag2 = this.PreProcessRequest();
					if (flag2)
					{
						string text = (this.XmlRequest == null) ? Command.ReadStringFromStream(this.InputStream, this.context.Request.ContentEncoding) : AirSyncUtility.BuildOuterXml(this.XmlRequest.OwnerDocument, true);
						AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "Request: {0}", text);
						if (this.MailboxLoggingEnabled && this.mailboxLogger != null && this.mailboxLogger.Enabled)
						{
							this.mailboxLogger.SetData(MailboxLogDataName.RequestTime, ExDateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo));
							this.mailboxLogger.SetData(MailboxLogDataName.ServerName, Command.MachineName);
							this.mailboxLogger.SetData(MailboxLogDataName.AssemblyVersion, "15.00.1497.010");
							this.requestId = this.GetNextNumber(0, true);
							this.mailboxLogger.SetData(MailboxLogDataName.Identifier, this.requestId.ToString("X", CultureInfo.InvariantCulture.NumberFormat));
							this.mailboxLogger.LogRequestHeader(this.Request);
							this.LogRequestToMailboxLog(text);
						}
					}
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.TimeDeviceAccessCheckStarted, ExDateTime.UtcNow);
					if (!this.IsDeviceAccessAllowed())
					{
						this.executionState = Command.ExecutionState.Complete;
					}
					else
					{
						AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Checking policy", 28715);
						AirSyncDiagnostics.FaultInjectionTracer.TraceTest(3179687229U);
						this.context.ProtocolLogger.SetValue(ProtocolLoggerData.TimePolicyCheckStarted, ExDateTime.UtcNow);
						bool flag3;
						if (!this.IsPolicyCompliant(out this.nextPolicyRefreshTime, out flag3))
						{
							AirSyncDiagnostics.TraceInfo<string, DeviceIdentity>(ExTraceGlobals.RequestsTracer, this, "user {0}, device identity {1}, is blocked by policy.", this.User.Name, this.DeviceIdentity);
							this.executionState = Command.ExecutionState.Complete;
							if (!flag3 && this.GlobalInfo != null)
							{
								this.GlobalInfo.DeviceAccessState = DeviceAccessState.Blocked;
								this.GlobalInfo.DeviceAccessStateReason = DeviceAccessStateReason.Policy;
							}
						}
						else if (flag2)
						{
							AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Calling Execute method for the command", 20011);
							if (this.RootNodeName == "Invalid" || this.ValidateXml())
							{
								if (this.IsInteractiveCommand)
								{
									this.AddInteractiveCall();
								}
								this.context.ProtocolLogger.SetValue(ProtocolLoggerData.TimeExecuteStarted, ExDateTime.UtcNow);
								this.executionState = this.ExecuteCommand();
								this.context.ProtocolLogger.SetValue(ProtocolLoggerData.TimeExecuteFinished, ExDateTime.UtcNow);
							}
							else
							{
								this.executionState = Command.ExecutionState.Complete;
							}
						}
						else
						{
							this.XmlResponse = this.GetInvalidParametersXml();
							if (this.XmlResponse != null)
							{
								this.context.Response.HttpStatusCode = HttpStatusCode.OK;
								AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "[Command.WorkerThread] Returning InvalidParameters - we need them to reissue the request because it is invalid.");
							}
							else
							{
								AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "[Command.WorkerThread] GetInvalidParametersXml was not overridden and returned null.  Convering into HTTP 400 response.  Command Type: {0}", base.GetType().Name);
								this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidStoredRequest");
								this.Context.Response.SetErrorResponse(HttpStatusCode.BadRequest, StatusCode.InvalidStoredRequest);
							}
							this.executionState = Command.ExecutionState.Complete;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (this.mailboxLogger != null)
				{
					this.mailboxLogger.SetData(MailboxLogDataName.Command_WorkerThread_Exception, new AirSyncUtility.ExceptionToStringHelper(ex));
				}
				this.executionState = Command.ExecutionState.Complete;
				AirSyncUtility.ProcessException(ex, this, this.context);
			}
			finally
			{
				try
				{
					AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - Releasing the resources and Logging protocol data to the IIS logs", 28203);
					this.TraceStop(serviceProviderRequestId);
					switch (this.executionState)
					{
					case Command.ExecutionState.Pending:
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Command is pending.");
						ActiveSyncRequestData activeSyncRequestData = ActiveSyncRequestCache.Instance.Get(this.Context.ActivityScope.ActivityId);
						activeSyncRequestData.IsHanging = true;
						this.LogResponseToMailbox(true);
						if (this.GlobalInfo != null)
						{
							this.CompleteDeviceAccessProcessing();
							this.GlobalInfo.SaveToMailbox();
						}
						this.CommitSyncStatusSyncState();
						this.ReleaseResources();
						break;
					}
					case Command.ExecutionState.Complete:
						try
						{
							AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - The request has completed successfully", 24107);
							if (this.sendServerUpgradeHeader)
							{
								if (this.User.IsConsumerOrganizationUser)
								{
									this.AddHeadersForConsumerOrgUser();
								}
								else
								{
									this.AddHeadersForEnterpriseOrgUser();
								}
							}
							if (this.GlobalInfo != null)
							{
								this.GlobalInfo.DeviceActiveSyncVersion = this.Request.VersionString;
								this.CompleteDeviceAccessProcessing();
								if (!this.GlobalInfo.HaveSentBoostrapMailForWM61 && this.GlobalInfo.BootstrapMailForWM61TriggeredTime != null && this.GlobalInfo.BootstrapMailForWM61TriggeredTime + Command.bootstrapMailDeliveryDelay < ExDateTime.UtcNow)
								{
									OTABootstrapMail.SendBootstrapMailForWM61(this.User);
									this.GlobalInfo.HaveSentBoostrapMailForWM61 = true;
								}
								if (this.MailboxLoggingEnabled && this.MailboxLogger != null && this.MailboxLogger.Enabled)
								{
									this.MailboxLogger.SetData(MailboxLogDataName.AccessState, this.GlobalInfo.DeviceAccessState);
									this.MailboxLogger.SetData(MailboxLogDataName.AccessStateReason, this.GlobalInfo.DeviceAccessStateReason);
									if (this.GlobalInfo.DeviceAccessControlRule != null)
									{
										this.MailboxLogger.SetData(MailboxLogDataName.DeviceAccessControlRule, this.GlobalInfo.DeviceAccessControlRule);
									}
								}
								this.ProtocolLogger.SetTrimmedValue(ProtocolLoggerData.DeviceInfoOS, this.GlobalInfo.DeviceOS, 50);
								string value;
								if (this.GlobalInfo.DeviceAccessStateReason < DeviceAccessStateReason.UserAgentsChanges || this.GlobalInfo.DeviceAccessStateReason > DeviceAccessStateReason.CommandFrequency)
								{
									value = this.GlobalInfo.DeviceAccessState.ToString() + this.GlobalInfo.DeviceAccessStateReason.ToString()[0];
								}
								else
								{
									value = this.GlobalInfo.DeviceAccessState.ToString() + "AB" + this.GlobalInfo.DeviceAccessStateReason.ToString()[0];
								}
								this.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.AccessStateAndReason, value);
								this.GlobalInfo.SaveToMailbox();
							}
							if (this.MeetingOrganizerSyncState != null && this.MeetingOrganizerSyncState.MeetingOrganizerInfo.IsDirty)
							{
								this.MeetingOrganizerSyncState.IsDirty = true;
								this.MeetingOrganizerSyncState.SaveToMailbox();
							}
						}
						catch (Exception ex2)
						{
							if (this.mailboxLogger != null)
							{
								this.mailboxLogger.SetData(MailboxLogDataName.Command_WorkerThread_Exception, new AirSyncUtility.ExceptionToStringHelper(ex2));
							}
							AirSyncUtility.ProcessException(ex2, this, this.context);
						}
						finally
						{
							if (this.GlobalInfo != null)
							{
								this.GlobalInfo.Dispose();
								this.GlobalInfo = null;
							}
							if (this.MeetingOrganizerSyncState != null)
							{
								this.MeetingOrganizerSyncState.Dispose();
								this.MeetingOrganizerSyncState = null;
							}
						}
						if (this.context.Response.HttpStatusCode == HttpStatusCode.OK)
						{
							if (this.XmlResponse != null)
							{
								this.Context.Response.IssueWbXmlResponse();
							}
							else if (string.IsNullOrEmpty(this.Context.Response.ContentType) || string.Equals("text/html", this.Context.Response.ContentType, StringComparison.OrdinalIgnoreCase))
							{
								this.Context.Response.ContentType = "application/vnd.ms-sync.wbxml";
							}
						}
						this.CommitSyncStatusSyncState();
						AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "Response: {0}", (this.XmlResponse == null) ? "[No XmlResponse]" : AirSyncUtility.BuildOuterXml(this.XmlResponse, true));
						this.LogResponseToMailbox(false);
						this.ReleaseResources();
						break;
					default:
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Invalid execution state.");
						throw new InvalidOperationException();
					}
					if (this.perUserTracingEnabled)
					{
						AirSyncDiagnostics.ClearThreadTracing();
					}
				}
				catch (Exception ex3)
				{
					if (!AirSyncUtility.HandleNonCriticalException(ex3, true))
					{
						throw;
					}
				}
				finally
				{
					try
					{
						if (this is IAsyncCommand)
						{
							this.ProcessQueuedEvents();
						}
					}
					finally
					{
						Command.ClearContextDataInTls();
						ActivityContext.ClearThreadScope();
						if (this.User != null && this.User.Context != null && this.User.Context.ActivityScope != null)
						{
							this.User.Context.ActivityScope.Action = action;
						}
					}
				}
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001851C File Offset: 0x0001671C
		public void ExecuteWithCommandTls(Action action)
		{
			this.SetContextDataInTls();
			try
			{
				action();
			}
			finally
			{
				Command.ClearContextDataInTls();
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00018550 File Offset: 0x00016750
		internal void UpdateADDevice(GlobalInfo globalInfo)
		{
			if (globalInfo == null)
			{
				return;
			}
			if (this.User.IsConsumerOrganizationUser)
			{
				return;
			}
			if ((globalInfo.DeviceAccessStateReason >= DeviceAccessStateReason.UserAgentsChanges && globalInfo.DeviceAccessStateReason < DeviceAccessStateReason.ExternallyManaged && (!GlobalSettings.AutoBlockWriteToAd || ADNotificationManager.GetAutoBlockThreshold(this.GlobalInfo.DeviceAccessStateReason).DeviceBlockDuration == TimeSpan.Zero)) || (this.Context.DeviceBehavior != null && this.Context.DeviceBehavior.TimeToUpdateAD > ExDateTime.UtcNow))
			{
				return;
			}
			int num = GlobalInfo.ComputeADDeviceInfoHash(globalInfo);
			int? addeviceInfoHash = this.GlobalInfo.ADDeviceInfoHash;
			bool flag = this.GlobalInfo.ADCreationTime != null && this.GlobalInfo.ADCreationTime != null && this.GlobalInfo.ADCreationTime.Value.AddHours((double)GlobalSettings.ADDataSyncInterval) < ExDateTime.UtcNow;
			if (addeviceInfoHash != null && addeviceInfoHash.Value == num && ADObjectId.Equals(globalInfo.UserADObjectId, this.User.ADUser.OriginalId) && globalInfo.DeviceADObjectId != null && !flag)
			{
				AirSyncDiagnostics.TraceInfo<string, DeviceIdentity, bool>(ExTraceGlobals.RequestsTracer, this, "Skip updating AD device object for user {0}, device id {1}, ShouldForceUpdateAD: {2}", this.User.Name, this.DeviceIdentity, flag);
				return;
			}
			this.DeviceManager.GetActiveSyncDeviceContainer();
			MobileDevice mobileDevice = this.DeviceManager.GetMobileDevice();
			int num2 = num;
			if (flag)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Compare the Hash value from AD and Update is required.");
				num2 = GlobalInfo.ComputeADDeviceInfoHash(mobileDevice);
				this.GlobalInfo.ADCreationTime = new ExDateTime?(ExDateTime.UtcNow);
			}
			bool flag2 = false;
			if (mobileDevice != null && mobileDevice.MaximumSupportedExchangeObjectVersion.IsOlderThan(mobileDevice.ExchangeVersion))
			{
				flag2 = this.deviceManager.DeleteMobileDevice(mobileDevice);
				mobileDevice = null;
				AirSyncDiagnostics.TraceInfo<string, DeviceIdentity, bool>(ExTraceGlobals.RequestsTracer, this, "mobile device is readonly. UserName {0}, device identity {1}, ShouldCreateNewDevice {2} .", this.User.Name, this.DeviceIdentity, flag2);
			}
			if (mobileDevice == null)
			{
				AirSyncDiagnostics.TraceInfo<string, DeviceIdentity>(ExTraceGlobals.RequestsTracer, this, "No ActiveSyncDevice object found in AD for user {0}, device identity: {1}", this.User.Name, this.DeviceIdentity);
				if (!this.isNewSyncStateStorage && this.syncStateStorage.CreationTime.AddHours((double)GlobalSettings.ADDataSyncInterval) >= ExDateTime.UtcNow && this.GlobalInfo.ADCreationTime != null && this.GlobalInfo.ADCreationTime.Value.AddHours((double)GlobalSettings.ADDataSyncInterval) >= ExDateTime.UtcNow && ADObjectId.Equals(globalInfo.UserADObjectId, this.User.ADUser.OriginalId) && !flag2)
				{
					return;
				}
				AirSyncDiagnostics.TraceInfo<string, DeviceIdentity>(ExTraceGlobals.RequestsTracer, this, "Creating new ActiveSyncDevice object in AD for user {0}, device identity {1}.", this.User.Name, this.DeviceIdentity);
				ActiveSyncRequestData activeSyncRequestData = ActiveSyncRequestCache.Instance.Get(this.Context.ActivityScope.ActivityId);
				activeSyncRequestData.NewDeviceCreated = true;
				this.DeviceManager.CreateMobileDevice(globalInfo, this.syncStateStorage.CreationTime, true, null);
				this.GlobalInfo.ADCreationTime = new ExDateTime?(ExDateTime.UtcNow);
				mobileDevice = this.DeviceManager.GetMobileDevice();
				if (mobileDevice == null)
				{
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Creating new ActiveSyncDevice object in AD failed.");
					return;
				}
			}
			else if (addeviceInfoHash == null || addeviceInfoHash.Value != num || addeviceInfoHash.Value != num2)
			{
				AirSyncDiagnostics.TraceInfo<string, DeviceIdentity>(ExTraceGlobals.RequestsTracer, this, "Updating ActiveSyncDevice object in AD for user {0}, device identity {1}.", this.User.Name, this.DeviceIdentity);
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.ADWriteReason, string.Format("FU:{0},SSH:{1},ADH:{2},DHA:{3},SCD:{4}", new object[]
				{
					flag,
					(addeviceInfoHash == null) ? -1 : addeviceInfoHash.Value,
					num,
					num2,
					flag2
				}));
				this.DeviceManager.UpdateMobileDevice(mobileDevice, globalInfo);
			}
			globalInfo.ADDeviceInfoHash = new int?(num);
			globalInfo.DeviceADObjectId = mobileDevice.OriginalId;
			globalInfo.UserADObjectId = this.User.ADUser.OriginalId;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00018978 File Offset: 0x00016B78
		internal void InitializeSyncStatusSyncState()
		{
			if (this.syncStatusSyncData == null)
			{
				if (this.User.Features.IsEnabled(EasFeature.SyncStatusOnGlobalInfo))
				{
					this.syncStatusSyncData = NewSyncStatusData.Load(this.GlobalInfo, this.syncStateStorage);
				}
				else
				{
					this.syncStatusSyncData = SyncStatusData.Load(this.syncStateStorage);
				}
				Interlocked.Exchange(ref this.validToCommitSyncStatusSyncState, 1);
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000189D8 File Offset: 0x00016BD8
		internal bool IsDeviceAccessAllowed()
		{
			if (this.GlobalInfo != null)
			{
				if (this.GlobalInfo.RemoteWipeRequestedTime != null)
				{
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceWipeIsRequested");
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Device wipe is requested.");
					return true;
				}
				if (string.IsNullOrEmpty(this.GlobalInfo.DeviceModel))
				{
					this.GlobalInfo.DeviceModel = this.DeviceIdentity.DeviceType;
				}
				else if (this.GlobalInfo.IsSyncStateJustUpgraded)
				{
					this.GlobalInfo.DeviceInformationReceived = true;
				}
			}
			IOrganizationSettingsData organizationSettingsData = ADNotificationManager.GetOrganizationSettingsData(this.User);
			DeviceAccessStateReason deviceAccessStateReason;
			if (this.Context.User.Features.IsEnabled(EasFeature.CloudMDMEnrolled) || organizationSettingsData.IsIntuneManaged)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.ExternallyManaged, "T");
				bool isSupportedDevice = true;
				deviceAccessStateReason = DeviceAccessStateReason.ExternallyManaged;
				if (this.Request.CommandType == CommandType.Options)
				{
					deviceAccessStateReason = DeviceAccessStateReason.Global;
					this.CurrentAccessState = DeviceAccessState.Allowed;
				}
				else if (!this.GlobalInfo.DeviceInformationReceived && !this.GlobalInfo.DeviceInformationPromoted)
				{
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "We haven't received device information settings yet.");
					if (this.syncStateStorage.CreationTime.AddMinutes((double)GlobalSettings.DeviceDiscoveryPeriod) >= ExDateTime.UtcNow)
					{
						AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "DeviceDiscovery.");
						this.CurrentAccessState = DeviceAccessState.DeviceDiscovery;
					}
					else
					{
						this.context.ProtocolLogger.AppendValue(ProtocolLoggerData.Message, "FallBackToUnSupportedPlatform");
						AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "device information not received within configured Discovery period. fall back to unsupported platform.");
						isSupportedDevice = false;
					}
				}
				else if (!DeviceCapability.DeviceSupportedForMdm(this.GlobalInfo))
				{
					this.context.ProtocolLogger.AppendValue(ProtocolLoggerData.Message, "DeviceNotSupported");
					isSupportedDevice = false;
				}
				if (this.CurrentAccessState == DeviceAccessState.Unknown)
				{
					this.CurrentAccessState = this.IsCloudMDMPolicyCompliant(isSupportedDevice, organizationSettingsData, out deviceAccessStateReason);
					AirSyncDiagnostics.TraceInfo<string, DeviceIdentity>(ExTraceGlobals.RequestsTracer, this, "user {0}, device identity: {1} is blocked by cloudMDM policy.", this.User.Name, this.DeviceIdentity);
					this.context.ProtocolLogger.AppendValue(ProtocolLoggerData.Error, (!this.isManaged) ? "BlockedMDMManaged" : "BlockedMDMCompliant");
				}
			}
			else
			{
				this.CurrentAccessState = this.DetermineDeviceAccessState(out deviceAccessStateReason);
			}
			AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "DetermineDeviceAccessState for user '{0}', device identifier: '{1}', AccessState '{2}', AccessReason '{3}'", new object[]
			{
				this.User.Name,
				(this.Request.CommandType == CommandType.Options) ? null : this.DeviceIdentity.ToString(),
				this.CurrentAccessState,
				deviceAccessStateReason
			});
			bool flag = true;
			if (this.CurrentAccessState == DeviceAccessState.Blocked)
			{
				flag = false;
				if (deviceAccessStateReason < DeviceAccessStateReason.UserAgentsChanges || deviceAccessStateReason > DeviceAccessStateReason.CommandFrequency)
				{
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceIsBlockedForThisUser");
					this.context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.DeviceIsBlockedForThisUser);
				}
				else
				{
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceIsAutoBlocked");
					this.context.Response.SetErrorResponse(HttpStatusCode.ServiceUnavailable, StatusCode.None);
					IAsyncCommand asyncCommand = this as IAsyncCommand;
					uint heartbeatInterval;
					if (asyncCommand != null && (ulong)(heartbeatInterval = asyncCommand.GetHeartbeatInterval()) > (ulong)((long)GlobalSettings.ErrorResponseDelay))
					{
						this.context.Response.TimeToRespond = this.context.RequestTime.AddSeconds(heartbeatInterval);
					}
				}
				this.context.Response.AppendHeader("X-MS-ASThrottle", deviceAccessStateReason.ToString());
			}
			if (this.ShouldOpenSyncState)
			{
				if (this.CurrentAccessState == DeviceAccessState.Quarantined || this.CurrentAccessState == DeviceAccessState.DeviceDiscovery)
				{
					flag = this.HandleQuarantinedState();
				}
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Stamping access state and reason on GlobalInfo for user: {0}, device identity: {1}, AccessState: {2}, AccessReason: {3}", new object[]
				{
					this.User.Name,
					this.DeviceIdentity,
					this.CurrentAccessState,
					deviceAccessStateReason
				});
				this.PreviousAccessState = this.GlobalInfo.DeviceAccessState;
				this.GlobalInfo.DeviceAccessState = this.CurrentAccessState;
				this.GlobalInfo.DeviceAccessStateReason = deviceAccessStateReason;
				this.Context.SetDiagnosticValue(AirSyncConditionalHandlerSchema.DeviceAccessState, this.GlobalInfo.DeviceAccessState);
				this.Context.SetDiagnosticValue(AirSyncConditionalHandlerSchema.DeviceAccessStateReason, this.GlobalInfo.DeviceAccessStateReason);
			}
			return flag;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00018E24 File Offset: 0x00017024
		internal bool IsPolicyCompliant(out ExDateTime nextRefreshTime, out bool isPolicyRefresh)
		{
			AirSyncDiagnostics.TracePfd<int, IAirSyncContext, Command>(ExTraceGlobals.PfdInitTraceTracer, this, "PFD EAI {0} - IsPolicyCompliant - Context: {1}, Command: {2}", 32299, this.context, this);
			nextRefreshTime = ExDateTime.MaxValue;
			isPolicyRefresh = false;
			if (this.Context.User.Features.IsEnabled(EasFeature.CloudMDMEnrolled) || ADNotificationManager.GetOrganizationSettingsData(this.User).IsIntuneManaged)
			{
				AirSyncDiagnostics.TracePfd(ExTraceGlobals.RequestsTracer, this, "Skip Policy Check for Cloud MDM user.");
				return true;
			}
			if (this.User.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.MonitoringMailbox)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Bypassing provisioning for user {0}", this.User.Name);
				return true;
			}
			int version = this.Version;
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - the command requires policy check", 26155);
			if (!this.RequiresPolicyCheck && (version < 140 || !(this is PingCommand)))
			{
				return true;
			}
			uint? policyKey = this.Request.PolicyKey;
			if (policyKey != null)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.PolicyKeyReceived, policyKey.Value);
			}
			bool flag = false;
			TimeSpan timeSpan = TimeSpan.MaxValue;
			bool flag2 = false;
			PolicyData policyData = ADNotificationManager.GetPolicyData(this.User);
			if (policyData != null)
			{
				flag = policyData.AllowNonProvisionableDevices;
				flag2 = policyData.RequireStorageCardEncryption;
				if (!policyData.DevicePolicyRefreshInterval.IsUnlimited)
				{
					timeSpan = policyData.DevicePolicyRefreshInterval.Value;
				}
			}
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - the device is allowed to sync", 17963);
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - Sync State Storage is not null", 22059);
			if (this.SyncStateStorage == null)
			{
				AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_DeviceIdAndDeviceTypeMustBePresent, "DeviceIdAndDeviceTypeMustBePresent" + this.User.Name, new string[]
				{
					this.User.Name
				});
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "NoSyncStateWhileCheckingPolicy");
				this.context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.SyncStateNotFound);
				return false;
			}
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Read the provision information from the user's mailbox", 30251);
			GlobalInfo globalInfo = this.GlobalInfo;
			this.SetStateData(new Command.StateData
			{
				DevicePhoneNumberForSms = globalInfo.DevicePhoneNumberForSms,
				DeviceEnableOutboundSMS = globalInfo.DeviceEnableOutboundSMS
			});
			if (!this.RequiresPolicyCheck)
			{
				return true;
			}
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Detect if the user's policy has changed", 18987);
			PolicyData policyData2 = ADNotificationManager.GetPolicyData(this.User);
			bool flag3;
			Command.DetectPolicyChange(policyData2, globalInfo, this.Version, out flag3);
			if (!flag3)
			{
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceNotFullyProvisionable");
				this.context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.DeviceNotFullyProvisionable);
				return false;
			}
			AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - if remote wipe was requested", 27179);
			if (globalInfo.RemoteWipeRequestedTime != null)
			{
				if (policyKey == null)
				{
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "RemoteWipeRequested");
					this.context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.RemoteWipeRequested);
					return false;
				}
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "RemoteWipeRequested");
				this.context.Response.SetErrorResponse((HttpStatusCode)449, StatusCode.RemoteWipeRequested);
				return false;
			}
			else
			{
				AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - PolicyKeyNeeded and policyKeyHeader are set", 23083);
				if (globalInfo.PolicyKeyNeeded == 0U && (policyKey == null || policyKey == 0U))
				{
					return true;
				}
				AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - policyKeyHeader is set", 31275);
				if (policyKey == null)
				{
					if (flag)
					{
						return true;
					}
					AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_NonconformingDeviceError, "NonconformingDeviceError: " + this.User.Name, new string[]
					{
						this.User.Name,
						(this.DeviceIdentity == null) ? null : this.DeviceIdentity.DeviceId
					});
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "LegacyDeviceOnStrictPolicy");
					this.context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.LegacyDeviceOnStrictPolicy);
					return false;
				}
				else
				{
					if (flag2 && version < 120 && !flag)
					{
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "LegacyDeviceCannotEncrypt");
						this.context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.DeviceNotFullyProvisionable);
						return false;
					}
					AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - LastPolicyTime is set", 16939);
					if (globalInfo.LastPolicyTime == null)
					{
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "DeviceNotProvisioned");
						this.context.Response.SetErrorResponse((HttpStatusCode)449, StatusCode.DeviceNotProvisioned);
						return false;
					}
					AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - timeSinceLastProvision is greater than refreshInterval", 25131);
					if (ExDateTime.UtcNow - globalInfo.LastPolicyTime > timeSpan)
					{
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "PolicyRefresh");
						this.context.Response.SetErrorResponse((HttpStatusCode)449, StatusCode.PolicyRefresh);
						isPolicyRefresh = true;
						return false;
					}
					if (timeSpan != TimeSpan.MaxValue && ExDateTime.MaxValue - timeSpan > globalInfo.LastPolicyTime.Value)
					{
						nextRefreshTime = globalInfo.LastPolicyTime.Value + timeSpan;
					}
					AirSyncDiagnostics.TracePfd<int>(ExTraceGlobals.PfdInitTraceTracer, null, "PFD EAI {0} - Checking - if there is no policy mismatch", 21035);
					if (policyKey != globalInfo.PolicyKeyNeeded)
					{
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "InvalidPolicyKey");
						this.context.Response.SetErrorResponse((HttpStatusCode)449, StatusCode.InvalidPolicyKey);
						return false;
					}
					return true;
				}
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00019438 File Offset: 0x00017638
		internal DeviceAccessState IsCloudMDMPolicyCompliant(bool isSupportedDevice, IOrganizationSettingsData organizationSettingsData, out DeviceAccessStateReason accessStateReason)
		{
			accessStateReason = DeviceAccessStateReason.ExternallyManaged;
			if (this.User.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.MonitoringMailbox)
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Bypassing provisioning for user {0}", this.User.Name);
				return DeviceAccessState.Allowed;
			}
			if (this.Request.CommandType == CommandType.Options)
			{
				return DeviceAccessState.Allowed;
			}
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.PolicyEvaluationEnabled && !isSupportedDevice)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "user {0}, device id {1}, device type {2}, is {3} by cloudMDM policy of Unsupported platforms.", new object[]
				{
					this.User.Name,
					this.Request.DeviceIdentity.DeviceId,
					this.Request.DeviceIdentity.DeviceType,
					organizationSettingsData.AllowAccessForUnSupportedPlatform ? "Allowed" : "Blocked"
				});
				if (!organizationSettingsData.AllowAccessForUnSupportedPlatform)
				{
					return DeviceAccessState.Blocked;
				}
				return DeviceAccessState.Allowed;
			}
			else
			{
				bool deviceStatus;
				DeviceAccessState deviceAccessState;
				if (GlobalSettings.SkipAzureADCall)
				{
					deviceStatus = GraphApiHelper.GetDeviceStatus(this.User.OrganizationId, this.DeviceManager.GetMobileDevice(), this.User.ADUser.ExternalDirectoryObjectId, out this.isManaged, out this.isCompliant);
					deviceAccessState = ((this.isManaged && this.isCompliant) ? DeviceAccessState.Allowed : DeviceAccessState.Quarantined);
					if (deviceAccessState == DeviceAccessState.Quarantined)
					{
						accessStateReason = (this.isManaged ? DeviceAccessStateReason.ExternalCompliance : DeviceAccessStateReason.ExternalEnrollment);
					}
				}
				else if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.PolicyEvaluationEnabled)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Call GraphApi Helper GetDeviceStatus for device {0}", this.Request.DeviceIdentity.DeviceId);
					deviceStatus = GraphApiHelper.GetDeviceStatus(this.User.OrganizationId, this.Request.DeviceIdentity.DeviceId, this.User.ADUser.ExternalDirectoryObjectId, isSupportedDevice, out deviceAccessState, out accessStateReason);
				}
				else
				{
					deviceStatus = GraphApiHelper.GetDeviceStatus(this.User.OrganizationId, this.Request.DeviceIdentity.DeviceId, this.User.ADUser.ExternalDirectoryObjectId, out this.isManaged, out this.isCompliant);
					deviceAccessState = ((this.isManaged && this.isCompliant) ? DeviceAccessState.Allowed : DeviceAccessState.Quarantined);
					if (deviceAccessState == DeviceAccessState.Quarantined)
					{
						accessStateReason = (this.isManaged ? DeviceAccessStateReason.ExternalCompliance : DeviceAccessStateReason.ExternalEnrollment);
					}
				}
				if (!deviceStatus)
				{
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Message, "DeviceDisabledInAAD");
					throw new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.DeviceIsBlockedForThisUser, null, false);
				}
				return deviceAccessState;
			}
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001969C File Offset: 0x0001789C
		internal void BorrowSecurityContextAndSession(Command sourceCommand)
		{
			if (this.securityContextAndSession != null && !this.sessionBorrowed)
			{
				throw new InvalidOperationException(string.Format("[Command.BorrowSecurityContextAndSession] Command {0} already has a security context and session that is not borrowed. Cannot replace.", base.GetType().Name));
			}
			this.securityContextAndSession = sourceCommand.securityContextAndSession;
			this.syncStateStorage = sourceCommand.syncStateStorage;
			this.sessionBorrowed = true;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000196F4 File Offset: 0x000178F4
		internal bool GetOrCreateConversation(ConversationId conversationId, bool shouldLoadBodySummary, out Conversation conversation)
		{
			if (conversationId == null)
			{
				throw new ArgumentNullException("conversationId");
			}
			bool flag = false;
			if (this.cachedConversation == null || !this.cachedConversation.ConversationId.Equals(conversationId))
			{
				AirSyncDiagnostics.TraceDebug<ConversationId>(ExTraceGlobals.RequestsTracer, this, "Loading Conversation for id {0}.", conversationId);
				bool isIrmEnabled = false;
				if (this.Context.User.IrmEnabled && this.Context.Request.IsSecureConnection && this.RightsManagementSupportFlag)
				{
					isIrmEnabled = true;
				}
				this.cachedConversation = Conversation.Load(this.MailboxSession, conversationId, isIrmEnabled, new PropertyDefinition[0]);
				if (this.shouldWatsonWhenReloadingSameConversation)
				{
					if (this.openedConversationIdList.Contains(conversationId))
					{
						AirSyncDiagnostics.TraceError<ConversationId>(ExTraceGlobals.RequestsTracer, this, "Loading same conversation twice.", conversationId);
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ReloadingSameConversationTwice");
					}
					else
					{
						this.openedConversationIdList.Add(conversationId);
					}
				}
				bool flag2 = false;
				foreach (IConversationTreeNode conversationTreeNode in this.cachedConversation.ConversationTree)
				{
					flag2 = (bool)conversationTreeNode.StorePropertyBags[0].TryGetProperty(MessageItemSchema.MapiHasAttachment);
					if (flag2)
					{
						break;
					}
				}
				if (flag2)
				{
					this.cachedConversation.OnBeforeItemLoad += BodyConversionUtilities.OnBeforeItemLoadInConversationForceOpen;
				}
				else
				{
					this.cachedConversation.OnBeforeItemLoad += BodyConversionUtilities.OnBeforeItemLoadInConversation;
				}
				if (shouldLoadBodySummary)
				{
					this.cachedConversation.LoadBodySummaries();
				}
				flag = true;
			}
			conversation = this.cachedConversation;
			return flag;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00019884 File Offset: 0x00017A84
		internal void EnableConversationDoubleLoadCheck(bool shouldEnableCheck)
		{
			this.shouldWatsonWhenReloadingSameConversation = shouldEnableCheck;
			if (this.shouldWatsonWhenReloadingSameConversation)
			{
				this.openedConversationIdList.Clear();
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000198A0 File Offset: 0x00017AA0
		protected virtual void GetOrCreateNotificationManager(out bool notificationManagerWasTaken)
		{
			notificationManagerWasTaken = false;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000198A5 File Offset: 0x00017AA5
		protected virtual void SetNotificationManagerMailboxLogging(bool mailboxLogging)
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000198A7 File Offset: 0x00017AA7
		protected virtual void ProcessQueuedEvents()
		{
			throw new NotSupportedException("ProcessQueuedEvents");
		}

		// Token: 0x06000455 RID: 1109
		protected abstract bool HandleQuarantinedState();

		// Token: 0x06000456 RID: 1110 RVA: 0x000198B4 File Offset: 0x00017AB4
		protected internal void CommitSyncStatusSyncState()
		{
			if (this.syncStatusSyncData != null)
			{
				try
				{
					if (this.ShouldSaveSyncStatus)
					{
						if (Interlocked.CompareExchange(ref this.validToCommitSyncStatusSyncState, 0, 1) == 1)
						{
							this.syncStatusSyncData.SaveAndDispose();
							this.syncStatusSyncData = null;
						}
						else
						{
							AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "syncStatusSyncState has already been committed.");
						}
					}
				}
				catch (LocalizedException arg)
				{
					AirSyncDiagnostics.TraceError<LocalizedException>(ExTraceGlobals.RequestsTracer, this, "Failed to commit syncStatusSyncState: {0}", arg);
				}
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001992C File Offset: 0x00017B2C
		protected virtual void CompleteHttpRequest()
		{
			ExDateTime timeToRespond = this.context.Response.TimeToRespond;
			if (!this.context.Response.IsErrorResponse && this.PartialFailure)
			{
				timeToRespond.AddSeconds((double)GlobalSettings.ErrorResponseDelay);
			}
			if (!(this is OptionsCommand) && (this.XmlResponse == null || this.XmlResponse.FirstChild == null))
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.EmptyResponse, 1);
			}
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, (this.Context as AirSyncContext).GetParticipantCacheData());
			TimeSpan timeSpan = timeToRespond - ExDateTime.UtcNow;
			if (timeSpan <= Command.timeAllowedToCompleteEarly || (this.User != null && this.User.IsMonitoringTestUser))
			{
				this.CompleteHttpRequestCallback(null);
				return;
			}
			this.ProtocolLogger.SetValue(ProtocolLoggerData.CompletionOffset, (int)timeSpan.TotalMilliseconds);
			this.completionTimer = new Timer(new TimerCallback(this.CompleteHttpRequestCallback), this, timeSpan, Command.disablePeriodsTimespan);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00019A24 File Offset: 0x00017C24
		protected internal void OpenMailboxSession(IAirSyncUser user)
		{
			this.CheckDisposed();
			if (this.securityContextAndSession != null)
			{
				return;
			}
			lock (this.contextAndSessionLock)
			{
				this.CheckDisposed();
				if (this.securityContextAndSession != null)
				{
					return;
				}
				SecurityContextAndSession securityContextAndSession = null;
				this.User.Context.ActivityScope.ClientInfo = string.Format("Client=ActiveSync;UserAgent={0};Action={1}", this.EffectiveUserAgent, this.context.Request.PathAndQuery);
				if (MailboxSessionCache.TryGetAndRemoveValue(user.ExchangePrincipal.ObjectId.ObjectGuid, out securityContextAndSession))
				{
					this.securityContextAndSession = securityContextAndSession;
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "Reuse mailbox session for " + user.Name);
					this.securityContextAndSession.MailboxSession.Connect();
				}
				else
				{
					string clientInfoString = string.Format("Client=ActiveSync;User={0}", user.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
					MailboxSession mailboxSession = null;
					bool flag2 = false;
					try
					{
						if (this.context.Request.WasProxied)
						{
							if (user.ADUser == null)
							{
								this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ProxyUserNotFound");
								AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_NoMailboxRights, new string[]
								{
									user.Name
								});
								throw new AirSyncPermanentException(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater, null, false);
							}
							mailboxSession = MailboxSession.OpenWithBestAccess(user.ExchangePrincipal, user.ADUser.LegacyExchangeDN, user.ClientSecurityContextWrapper.ClientSecurityContext, this.context.Request.Culture, clientInfoString);
							if (!mailboxSession.CanActAsOwner)
							{
								this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "NoMailboxRights");
								AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_NoMailboxRights, new string[]
								{
									user.Name
								});
								throw new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.AccessDenied, null, false);
							}
						}
						else
						{
							mailboxSession = MailboxSession.Open(user.ExchangePrincipal, user.ClientSecurityContextWrapper.ClientSecurityContext, this.context.Request.Culture, clientInfoString);
						}
						this.securityContextAndSession = new SecurityContextAndSession(user.ClientSecurityContextWrapper, mailboxSession);
						this.ProtocolLogger.SetValue(ProtocolLoggerData.NewMailboxSession, 1);
						flag2 = true;
					}
					finally
					{
						if (!flag2 && mailboxSession != null)
						{
							mailboxSession.Dispose();
							mailboxSession = null;
						}
					}
				}
				this.sessionBorrowed = false;
				if (GlobalSettings.IsGCCEnabled)
				{
					if (GlobalSettings.AreGccStoredSecretKeysValid)
					{
						GccUtils.SetStoreSessionClientIPEndpointsFromHttpRequest(this.MailboxSession, this.context.Request.GetRawHttpRequest());
					}
					else
					{
						AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_NoGccStoredSecretKey, "NoGccStoredSecretKey", new string[0]);
						AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "No gcc stored secret key");
					}
				}
			}
			this.MailboxSession.ExTimeZone = ExTimeZone.UtcTimeZone;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00019CF8 File Offset: 0x00017EF8
		protected internal void UpdateRecipientInfoCache(RecipientCollection recipients, HashSet<RecipientId> originalRecipients)
		{
			using (RecipientInfoCache recipientInfoCache = RecipientInfoCache.Create(this.MailboxSession, "OWA.AutocompleteCache"))
			{
				List<RecipientInfoCacheEntry> list;
				try
				{
					list = recipientInfoCache.Load("AutoCompleteCache");
				}
				catch (CorruptDataException arg)
				{
					AirSyncDiagnostics.TraceDebug<CorruptDataException>(ExTraceGlobals.RequestsTracer, this, "Got a corrupt data exception! {0}", arg);
					list = new List<RecipientInfoCacheEntry>(recipients.Count);
				}
				Dictionary<string, RecipientInfoCacheEntry> dictionary = new Dictionary<string, RecipientInfoCacheEntry>(list.Count * 2 + recipients.Count, StringComparer.Ordinal);
				foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in list)
				{
					if (recipientInfoCacheEntry.SmtpAddress != null)
					{
						dictionary[recipientInfoCacheEntry.SmtpAddress] = recipientInfoCacheEntry;
					}
					if (recipientInfoCacheEntry.RoutingAddress != null)
					{
						dictionary[recipientInfoCacheEntry.RoutingAddress] = recipientInfoCacheEntry;
					}
				}
				foreach (Recipient recipient in recipients)
				{
					if (originalRecipients == null || !originalRecipients.Contains(recipient.Id))
					{
						RecipientInfoCacheEntry recipientInfoCacheEntry2 = null;
						string text = recipient.Participant.TryGetProperty(ParticipantSchema.SmtpAddress) as string;
						bool flag = false;
						if (text != null)
						{
							flag = dictionary.TryGetValue(text, out recipientInfoCacheEntry2);
						}
						if (!flag && recipient.Participant.EmailAddress != null)
						{
							flag = dictionary.TryGetValue(recipient.Participant.EmailAddress, out recipientInfoCacheEntry2);
						}
						if (!flag)
						{
							recipientInfoCacheEntry2 = new RecipientInfoCacheEntry(recipient.Participant.DisplayName, text, recipient.Participant.EmailAddress, null, recipient.Participant.RoutingType, AddressOrigin.OneOff, 0, null, EmailAddressIndex.Email1, null, null);
							if (recipientInfoCacheEntry2.SmtpAddress != null)
							{
								dictionary[recipientInfoCacheEntry2.SmtpAddress] = recipientInfoCacheEntry2;
							}
							if (recipientInfoCacheEntry2.RoutingAddress != null)
							{
								dictionary[recipientInfoCacheEntry2.RoutingAddress] = recipientInfoCacheEntry2;
							}
							list.Add(recipientInfoCacheEntry2);
						}
						else
						{
							recipientInfoCacheEntry2.UpdateTimeStamp();
							recipientInfoCacheEntry2.IncrementUsage();
						}
					}
				}
				recipientInfoCache.Save(list, "AutoCompleteCache", 100);
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00019F54 File Offset: 0x00018154
		protected void SetHttpStatusCodeForTerminatedAccount(AccountState accountState)
		{
			AirSyncDiagnostics.TraceInfo<string>(ExTraceGlobals.RequestsTracer, this, "SetHttpStatusCodeForTerminatedAccount {0}", accountState.ToString());
			switch (accountState)
			{
			case AccountState.AccountEnabled:
				throw new InvalidOperationException("SetHttpStatusCodeForTerminatedAccount called for an enabled acount!");
			case AccountState.AccountDisabled:
				this.Context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.AccountDisabled);
				return;
			case AccountState.PasswordExpired:
				this.Context.Response.SetErrorResponse(HttpStatusCode.Unauthorized, StatusCode.AccessDenied);
				this.Context.Response.AppendHeader("WWW-Authenticate", "Basic Realm=\"\"");
				return;
			case AccountState.AccountDeleted:
				this.Context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.AccessDenied);
				return;
			case AccountState.MailboxDisabled:
				this.Context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.UserHasNoMailbox);
				return;
			case AccountState.ProtocolDisabled:
				this.Context.Response.SetErrorResponse(HttpStatusCode.Forbidden, StatusCode.UserDisabledForSync);
				return;
			default:
				throw new InvalidOperationException("SetHttpStatusCodeForTerminatedAccount called with an unknown account state");
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001A06C File Offset: 0x0001826C
		private void CreateNewSyncStateOnFailedUpgrade(bool shouldOpenGlobalSyncState)
		{
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Sync state storage not found.");
			this.sendServerUpgradeHeader = true;
			this.ProtocolLogger.SetValue(ProtocolLoggerData.SyncStateNotFound, "T");
			this.syncStateStorage = SyncStateStorage.Create(this.MailboxSession, this.DeviceIdentity, StateStorageFeatures.ContentState, this.Context);
			this.syncStateStorage.SaveOnDirectItems = this.Context.User.Features.IsEnabled(EasFeature.SyncStateOnDirectItems);
			AirSyncDiagnostics.TraceDebug<bool>(ExTraceGlobals.RequestsTracer, this, "[Command.CreateNewSyncStateOnFailedUpgrade] SaveOnDirectItems? {0}", this.syncStateStorage.SaveOnDirectItems);
			this.isNewSyncStateStorage = true;
			try
			{
				if (this.User.ADUser != null && !this.User.IsConsumerOrganizationUser)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[Command.CreateNewSyncStateOnFailedUpgrade] Begin updating HasDevicePartnership, Found AdUser");
					IRecipientSession recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.User.OrganizationId), 3505, "CreateNewSyncStateOnFailedUpgrade", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\Command.cs");
					recipientSession.UseGlobalCatalog = false;
					ADObjectId adobjectId = (ADObjectId)this.User.ADUser.Identity;
					ADRecipient recipient = recipientSession.Read(adobjectId);
					this.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, recipientSession.LastUsedDc);
					ADUser aduser = recipient as ADUser;
					if (aduser != null)
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[Command.CreateNewSyncStateOnFailedUpgrade] Update HasDevicePartnership to true");
						aduser.MobileMailboxFlags |= MobileMailboxFlags.HasDevicePartnership;
						ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
						{
							recipientSession.Save(recipient);
						});
						if (!adoperationResult.Succeeded)
						{
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "Exception occurred during AD Operation. Message:{0}", adoperationResult.Exception.Message);
							this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "SetUserHasDeviceAdException");
						}
						this.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, recipientSession.LastUsedDc);
						this.Context.ProtocolLogger.SetValue(ProtocolLoggerData.UpdateUserHasPartnerships, "T");
					}
					else
					{
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ADUserNotFound");
						AirSyncDiagnostics.TraceDebug<ADObjectId>(ExTraceGlobals.RequestsTracer, null, "Could not find ADUser with ID {0} when trying to set MobileMailboxFlags.", adobjectId);
					}
				}
			}
			catch (ADOperationException)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "ADOperationException");
			}
			catch (InvalidObjectOperationException innerException)
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "UserObjectInvalid");
				throw new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.AccessDenied, innerException, false);
			}
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001A310 File Offset: 0x00018510
		private SyncStateUpgradeResult UpgradeSyncStateStorage(bool shouldOpenGlobalSyncState)
		{
			SyncStateUpgradeResult syncStateUpgradeResult = SyncStateUpgradeResult.NoTiSyncState;
			if (this.Version < 120)
			{
				try
				{
					syncStateUpgradeResult = SyncStateUpgrader.CheckAndUpgradeSyncStates(this.MailboxSession, this.DeviceIdentity);
				}
				catch
				{
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Ssu, "FailDuringUpgrade");
					throw;
				}
				switch (syncStateUpgradeResult)
				{
				case SyncStateUpgradeResult.UpgradeFailed:
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Ssu, "FailDuringUpgrade");
					break;
				case SyncStateUpgradeResult.UpgradeComplete:
					this.ProtocolLogger.SetValue(ProtocolLoggerData.Ssu, "2003");
					break;
				}
				if (syncStateUpgradeResult == SyncStateUpgradeResult.UpgradeComplete)
				{
					this.syncStateStorage = SyncStateStorage.Bind(this.MailboxSession, this.DeviceIdentity, this.Context);
					if (this.syncStateStorage == null)
					{
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Ssu, "FailToBindToFolder");
						syncStateUpgradeResult = SyncStateUpgradeResult.UpgradeFailed;
					}
					else
					{
						this.syncStateStorage.SaveOnDirectItems = this.Context.User.Features.IsEnabled(EasFeature.SyncStateOnDirectItems);
						AirSyncDiagnostics.TraceDebug<bool>(ExTraceGlobals.RequestsTracer, this, "[Command.UpgradeSyncStateStorage] SaveOnDirectItems? {0}", this.syncStateStorage.SaveOnDirectItems);
						this.ProtocolLogger.SetValue(ProtocolLoggerData.Ssu, "2003");
						this.isNewSyncStateStorage = true;
					}
				}
			}
			if (syncStateUpgradeResult != SyncStateUpgradeResult.UpgradeComplete)
			{
				this.CreateNewSyncStateOnFailedUpgrade(shouldOpenGlobalSyncState);
			}
			return syncStateUpgradeResult;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001A448 File Offset: 0x00018648
		internal void LoadMeetingOrganizerSyncState()
		{
			this.CheckDisposed();
			if (this.MeetingOrganizerSyncState != null)
			{
				return;
			}
			lock (this.contextAndSessionLock)
			{
				this.CheckDisposed();
				if (this.MeetingOrganizerSyncState == null)
				{
					if (this.syncStateStorage == null)
					{
						throw new InvalidOperationException("Sync State Storage must be opened before opening meeting organizer sync state.");
					}
					this.MeetingOrganizerSyncState = MeetingOrganizerSyncState.LoadFromMailbox(this.MailboxSession, this.syncStateStorage, this.ProtocolLogger);
				}
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001A4D0 File Offset: 0x000186D0
		protected internal void OpenSyncStorage(bool shouldOpenGlobalSyncState)
		{
			this.CheckDisposed();
			SyncStateUpgradeResult syncStateUpgradeResult = SyncStateUpgradeResult.NoTiSyncState;
			if (this.syncStateStorage == null)
			{
				lock (this.contextAndSessionLock)
				{
					this.CheckDisposed();
					if (this.syncStateStorage == null)
					{
						this.syncStateStorage = SyncStateStorage.Bind(this.MailboxSession, this.DeviceIdentity, this.Context);
						if (this.syncStateStorage == null)
						{
							syncStateUpgradeResult = this.UpgradeSyncStateStorage(shouldOpenGlobalSyncState);
						}
						else
						{
							this.syncStateStorage.SaveOnDirectItems = this.Context.User.Features.IsEnabled(EasFeature.SyncStateOnDirectItems);
							AirSyncDiagnostics.TraceDebug<bool>(ExTraceGlobals.RequestsTracer, this, "[Command.OpenSyncStorage] SaveOnDirectItems? {0}", this.syncStateStorage.SaveOnDirectItems);
						}
					}
					goto IL_AD;
				}
				return;
				IL_AD:
				AirSyncDiagnostics.FaultInjectionTracer.TraceTest(2242260285U);
				bool flag2 = false;
				if (shouldOpenGlobalSyncState && this.GlobalInfo == null)
				{
					this.GlobalInfo = GlobalInfo.LoadFromMailbox(this.MailboxSession, this.SyncStateStorage, this.ProtocolLogger, out flag2);
					if (syncStateUpgradeResult != SyncStateUpgradeResult.NoTiSyncState)
					{
						this.GlobalInfo.SyncStateUpgradeTime = new ExDateTime?(ExDateTime.UtcNow);
					}
				}
				if (flag2 && !this.User.IsConsumerOrganizationUser)
				{
					DeviceInfo.UpdateDeviceHasPartnership(this.MailboxSession, true);
				}
				if (this.ShouldSaveSyncStatus && this.syncStatusSyncData == null)
				{
					this.InitializeSyncStatusSyncState();
				}
				if (this.MailboxLoggingEnabled)
				{
					if (this.mailboxLogger == null)
					{
						bool flag3 = false;
						AirSyncDiagnostics.FaultInjectionTracer.TraceTest<bool>(3622186301U, ref flag3);
						if (!flag3 && this.GlobalInfo != null)
						{
							ExDateTime? nextTimeToClearMailboxLogs = this.GlobalInfo.NextTimeToClearMailboxLogs;
							flag3 = (nextTimeToClearMailboxLogs == null || ExDateTime.UtcNow > nextTimeToClearMailboxLogs.Value);
						}
						this.mailboxLogger = new MailboxLogger(this.MailboxSession, this.DeviceIdentity, flag3);
						if (!this.mailboxLogger.Enabled)
						{
							this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "MailboxLoggerError");
							AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, this, "MailboxLogger failed to initialize.  Exception message: {0}\nStack trace: {1}", this.mailboxLogger.LastError.Message, this.mailboxLogger.LastError.StackTrace);
						}
						else if (flag3 && this.GlobalInfo != null)
						{
							this.GlobalInfo.NextTimeToClearMailboxLogs = new ExDateTime?(ExDateTime.UtcNow.AddHours(6.0));
						}
					}
					if (this.mailboxLogger.MailboxSession == null)
					{
						this.mailboxLogger.MailboxSession = this.MailboxSession;
					}
				}
				return;
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001A738 File Offset: 0x00018938
		protected virtual void ReleaseResources()
		{
			if (this.mailboxLogger != null)
			{
				this.mailboxLogger.MailboxSession = null;
			}
			if (this.syncStatusSyncData != null)
			{
				IDisposable disposable = this.syncStatusSyncData as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.syncStatusSyncData = null;
			}
			if (this.Context != null && this.Context.DeviceBehavior != null)
			{
				this.Context.DeviceBehavior.Owner = null;
				this.Context.DeviceBehavior.ProtocolLogger = null;
				this.Context.DeviceBehavior = null;
			}
			if (this.GlobalInfo != null)
			{
				this.GlobalInfo.Dispose();
				this.GlobalInfo = null;
			}
			if (this.MeetingOrganizerSyncState != null)
			{
				this.MeetingOrganizerSyncState.Dispose();
				this.MeetingOrganizerSyncState = null;
			}
			if (!this.sessionBorrowed)
			{
				if (this.syncStateStorage != null)
				{
					this.syncStateStorage.Dispose();
					this.syncStateStorage = null;
				}
				if (this.securityContextAndSession != null)
				{
					bool flag = false;
					if (!this.PartialFailure && !this.context.Response.IsErrorResponse)
					{
						MailboxSession mailboxSession = this.MailboxSession;
						if (mailboxSession.IsConnected)
						{
							mailboxSession.Disconnect();
						}
						flag = MailboxSessionCache.AddOrReplace(this.User.ExchangePrincipal.ObjectId.ObjectGuid, this.securityContextAndSession);
					}
					if (!flag)
					{
						MailboxSessionCache.IncrementDiscardedSessions();
						this.securityContextAndSession.Dispose();
					}
					this.securityContextAndSession = null;
				}
			}
			if (this.deviceManager != null)
			{
				this.deviceManager = null;
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001A89C File Offset: 0x00018A9C
		protected int GetNextNumber(int number)
		{
			return this.GetNextNumber(number, false);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		protected int GetNextNumber(int number, bool alwaysGetRandom)
		{
			int num = 0;
			if (!alwaysGetRandom)
			{
				return number + 1;
			}
			lock (Command.randomGenerator)
			{
				while (num == 0 || num == number)
				{
					num = Command.randomGenerator.Next();
				}
			}
			AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.RequestsTracer, this, "Previous number: {0}, New GetNextNumber: {1}", number, num);
			return num;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001A914 File Offset: 0x00018B14
		internal void DecodeIrmMessage(Item mailboxItem, bool acquireLicense)
		{
			RightsManagedMessageItem rightsManagedMessageItem = mailboxItem as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null)
			{
				if (!this.Context.User.IrmEnabled)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "User {0} is not IrmEnabled or the client access server is not IrmEnable. Ignoring the Irm decoding of messages.", this.User.DisplayName);
					return;
				}
				if (!this.Context.Request.IsSecureConnection)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "User {0} sent request on non SSL channnel. Ignoring the Irm decoding of messages.", this.User.DisplayName);
					return;
				}
				if (!rightsManagedMessageItem.IsRestricted)
				{
					AirSyncDiagnostics.TraceError<VersionedId>(ExTraceGlobals.RequestsTracer, null, "Rights managed item {0} is not restricted", mailboxItem.Id);
					return;
				}
				if (!rightsManagedMessageItem.CanDecode)
				{
					AirSyncDiagnostics.TraceDebug<VersionedId>(ExTraceGlobals.RequestsTracer, null, "Rights managed item {0} can not be decoded on server", mailboxItem.Id);
					return;
				}
				if (rightsManagedMessageItem.IsDecoded)
				{
					return;
				}
				RightsManagedMessageDecryptionStatus rightsManagedMessageDecryptionStatus = rightsManagedMessageItem.TryDecode(AirSyncUtility.GetOutboundConversionOptions(), acquireLicense);
				if (rightsManagedMessageDecryptionStatus.Failed)
				{
					AirSyncDiagnostics.TraceError<Exception>(ExTraceGlobals.XsoTracer, null, "Failed to decode the message. Exception {0}", rightsManagedMessageDecryptionStatus.Exception);
					if (this.MailboxLogger != null)
					{
						this.MailboxLogger.SetData(MailboxLogDataName.IRM_Exception, rightsManagedMessageDecryptionStatus.Exception);
						RightsManagementPermanentException ex = rightsManagedMessageDecryptionStatus.Exception as RightsManagementPermanentException;
						if (ex != null)
						{
							this.MailboxLogger.SetData(MailboxLogDataName.IRM_FailureCode, ex.FailureCode);
						}
					}
				}
			}
			AirSyncCounters.NumberOfIRMMailsDownloads.Increment();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001AA50 File Offset: 0x00018C50
		internal void SaveLicense(Item item)
		{
			if (BodyConversionUtilities.IsMessageRestrictedAndDecoded(item))
			{
				RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
				try
				{
					rightsManagedMessageItem.SaveUseLicense();
				}
				catch (AccessDeniedException)
				{
					AirSyncDiagnostics.TraceError<VersionedId, string>(ExTraceGlobals.XsoTracer, null, "Failed to write the license back for the messageItem {0} for user {1}", item.Id, this.Context.User.DisplayName);
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001AAB0 File Offset: 0x00018CB0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this.disposed)
			{
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
				this.ReleaseDisposableData();
				lock (this.contextAndSessionLock)
				{
					this.ReleaseResources();
					this.disposed = true;
				}
				if (this.completionTimer != null)
				{
					this.completionTimer.Dispose();
					this.completionTimer = null;
				}
				if (this.mailboxLogger != null)
				{
					this.mailboxLogger.Dispose();
					this.mailboxLogger = null;
				}
				if (this.budget != null)
				{
					try
					{
						this.budget.Dispose();
					}
					catch (FailFastException arg)
					{
						AirSyncDiagnostics.TraceError<FailFastException>(ExTraceGlobals.RequestsTracer, null, "Budget.Dispose failed with exception: {0}", arg);
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001AB98 File Offset: 0x00018D98
		protected bool IsInboxFolder(StoreObjectId folderId)
		{
			return this.MailboxSession != null && folderId != null && folderId.Equals(this.MailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox));
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001ABBC File Offset: 0x00018DBC
		private void CompleteHttpRequestCallback(object state)
		{
			this.ProtocolLogger.SetValue(ProtocolLoggerData.FinalElapsedTime, (int)ExDateTime.UtcNow.Subtract(this.Context.RequestTime).TotalMilliseconds);
			this.context.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.TimeCompleted, ExDateTime.UtcNow);
			this.result.InvokeCallback();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001AC1C File Offset: 0x00018E1C
		private void LogResponseToMailbox(bool pending)
		{
			try
			{
				if (this.MailboxLoggingEnabled && this.mailboxLogger != null && this.mailboxLogger.Enabled)
				{
					if (!pending)
					{
						this.mailboxLogger.LogResponseHead(this.context.Response);
						if (!this.mailboxLogger.DataExists(MailboxLogDataName.ResponseBody))
						{
							this.mailboxLogger.SetData(MailboxLogDataName.ResponseBody, (this.XmlResponse == null) ? "[No XmlResponse]" : AirSyncUtility.BuildOuterXml(this.XmlResponse, !GlobalSettings.EnableMailboxLoggingVerboseMode));
						}
						this.mailboxLogger.SetData(MailboxLogDataName.ResponseTime, ExDateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo));
					}
					this.mailboxLogger.SaveLogToMailbox();
				}
			}
			catch (LocalizedException arg)
			{
				AirSyncDiagnostics.TraceError<LocalizedException>(ExTraceGlobals.RequestsTracer, this, "Failed to LogResponseToMailbox: {0}", arg);
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001ACF0 File Offset: 0x00018EF0
		private void TraceStop(Guid serviceProviderRequestId)
		{
			Microsoft.Exchange.Diagnostics.Trace.TraceCasStop(CasTraceEventType.ActiveSync, serviceProviderRequestId, this.Request.ContentLength, 0, Command.MachineName, this.User.Name, "WorkerThread", this.Request.PathAndQuery, string.Empty);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001AD38 File Offset: 0x00018F38
		private DeviceAccessState DetermineDeviceAccessState(out DeviceAccessStateReason accessStateReason)
		{
			DeviceAccessState deviceAccessState = DeviceAccessState.Unknown;
			accessStateReason = DeviceAccessStateReason.Unknown;
			DeviceAccessState deviceAccessState2;
			try
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "DetermineDeviceAccessState: Enter");
				if (this.User.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.MonitoringMailbox)
				{
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Monitoring mailboxes are always allowed.");
					accessStateReason = DeviceAccessStateReason.Global;
					deviceAccessState = DeviceAccessState.Allowed;
					deviceAccessState2 = deviceAccessState;
				}
				else if (this.Request.CommandType == CommandType.Options)
				{
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "OPTIONS is always allowed.");
					accessStateReason = DeviceAccessStateReason.Global;
					deviceAccessState = DeviceAccessState.Allowed;
					deviceAccessState2 = deviceAccessState;
				}
				else
				{
					if (this.GlobalInfo == null)
					{
						throw new InvalidOperationException("GlobalInfo should not be null at this point!");
					}
					this.GlobalInfo.DeviceAccessControlRule = null;
					string item = (this.Request.CommandType == CommandType.Options) ? null : this.DeviceIdentity.DeviceId;
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "1. Check individual user blocked list");
					MultiValuedProperty<string> activeSyncBlockedDeviceIDs = this.User.ADUser.ActiveSyncBlockedDeviceIDs;
					if (activeSyncBlockedDeviceIDs.Contains(item))
					{
						accessStateReason = DeviceAccessStateReason.Individual;
						deviceAccessState = DeviceAccessState.Blocked;
						deviceAccessState2 = deviceAccessState;
					}
					else
					{
						this.Context.DeviceBehavior = DeviceBehavior.GetDeviceBehavior(this.User.ADUser.OriginalId.ObjectGuid, this.DeviceIdentity, this.GlobalInfo, this, this.ProtocolLogger);
						AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "User agent is " + this.EffectiveUserAgent);
						TimeSpan t;
						DeviceAccessStateReason deviceAccessStateReason = this.Context.DeviceBehavior.IsDeviceAutoBlocked(this.EffectiveUserAgent, out t);
						if (deviceAccessStateReason != DeviceAccessStateReason.Unknown)
						{
							if (t > TimeSpan.Zero)
							{
								AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Device is still blocked, no need to re-check");
							}
							this.context.Response.AppendHeader("Retry-After", t.TotalSeconds.ToString(), false);
							accessStateReason = deviceAccessStateReason;
							deviceAccessState = DeviceAccessState.Blocked;
							deviceAccessState2 = deviceAccessState;
						}
						else
						{
							this.Context.DeviceBehavior.RecordNewUserAgent(this.Request.UserAgent);
							if (this.Request.CommandType != CommandType.Sync && this.Request.CommandType != CommandType.Ping)
							{
								this.Context.DeviceBehavior.RecordCommand(this);
							}
							AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "1.1 Check auto block.");
							deviceAccessStateReason = this.Context.DeviceBehavior.IsDeviceAutoBlocked(this.Context.RequestTime, out t);
							if (deviceAccessStateReason != DeviceAccessStateReason.Unknown && t > TimeSpan.Zero)
							{
								this.context.Response.AppendHeader("Retry-After", t.TotalSeconds.ToString(), false);
								accessStateReason = deviceAccessStateReason;
								deviceAccessState = DeviceAccessState.Blocked;
								deviceAccessState2 = deviceAccessState;
							}
							else
							{
								if (this.User.OrganizationId != OrganizationId.ForestWideOrgId)
								{
									IOrganizationSettingsData organizationSettingsData = ADNotificationManager.GetOrganizationSettingsData(OrganizationId.ForestWideOrgId, this.User.Context);
									if (organizationSettingsData == null)
									{
										AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Cannot find OrganizationSetting for forest wide org");
										AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_NoOrgSettings, new string[]
										{
											this.User.OrganizationId.ToString()
										});
										throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, null, false)
										{
											ErrorStringForProtocolLogger = "ForestWideOrgSettingsNotFound"
										};
									}
									AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "2.1 Check forest-wide rules.");
									ADObjectId adobjectId;
									Command.DetermineDeviceAccessState(organizationSettingsData, this.DeviceIdentity.DeviceType, this.GlobalInfo.DeviceModel, this.EffectiveUserAgent, this.GlobalInfo.DeviceOS, out deviceAccessState, out accessStateReason, out adobjectId);
									if (adobjectId != null)
									{
										AirSyncDiagnostics.TraceInfo<ADObjectId>(ExTraceGlobals.RequestsTracer, this, "Found matching rule. {0}", adobjectId);
										this.GlobalInfo.DeviceAccessControlRule = adobjectId;
										return deviceAccessState;
									}
								}
								AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "3. Check individual user allowed list.");
								MultiValuedProperty<string> activeSyncAllowedDeviceIDs = this.User.ADUser.ActiveSyncAllowedDeviceIDs;
								if (activeSyncAllowedDeviceIDs.Contains(item))
								{
									accessStateReason = DeviceAccessStateReason.Individual;
									deviceAccessState = DeviceAccessState.Allowed;
									deviceAccessState2 = deviceAccessState;
								}
								else
								{
									AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4. Check device access rule settings.");
									IOrganizationSettingsData organizationSettingsData2 = ADNotificationManager.GetOrganizationSettingsData(this.User);
									if (organizationSettingsData2 == null)
									{
										AirSyncDiagnostics.TraceInfo<OrganizationId>(ExTraceGlobals.RequestsTracer, this, "Cannot find OrganizationSetting for org {0}", this.User.OrganizationId);
										AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_NoOrgSettings, new string[]
										{
											this.User.OrganizationId.ToString()
										});
										throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, null, false)
										{
											ErrorStringForProtocolLogger = "OrgSettingsNotFound"
										};
									}
									if (!organizationSettingsData2.IsRulesListEmpty)
									{
										AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.1 Did we receive <DeviceInformationSettings> already?");
										if (!this.GlobalInfo.DeviceInformationReceived && !this.GlobalInfo.DeviceInformationPromoted)
										{
											AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.1.1 We haven't received device information settings yet.");
											if (this.GlobalInfo.SyncStateUpgradeTime != null && this.GlobalInfo.SyncStateUpgradeTime.Value.AddMinutes((double)GlobalSettings.UpgradeGracePeriod) >= ExDateTime.UtcNow)
											{
												AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.1.1.1 We are in upgrade grace period.");
												accessStateReason = DeviceAccessStateReason.Upgrade;
												deviceAccessState = DeviceAccessState.Allowed;
												return deviceAccessState;
											}
											if (this.syncStateStorage.CreationTime.AddMinutes((double)GlobalSettings.DeviceDiscoveryPeriod) >= ExDateTime.UtcNow)
											{
												AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.1.1.2 DeviceDiscovery.");
												accessStateReason = DeviceAccessStateReason.DeviceRule;
												deviceAccessState = DeviceAccessState.DeviceDiscovery;
												return deviceAccessState;
											}
										}
									}
									AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "3.1.2 We have device information or we have passed upgrade grace period and DeviceDiscovery check.");
									AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.2 Check rules and global state.");
									ADObjectId adobjectId;
									Command.DetermineDeviceAccessState(organizationSettingsData2, this.DeviceIdentity.DeviceType, this.GlobalInfo.DeviceModel, this.EffectiveUserAgent, this.GlobalInfo.DeviceOS, out deviceAccessState, out accessStateReason, out adobjectId);
									if (adobjectId != null)
									{
										AirSyncDiagnostics.TraceInfo<ADObjectId>(ExTraceGlobals.RequestsTracer, this, "Found matching rule. {0}", adobjectId);
										this.GlobalInfo.DeviceAccessControlRule = adobjectId;
										deviceAccessState2 = deviceAccessState;
									}
									else
									{
										if (accessStateReason == DeviceAccessStateReason.Global && deviceAccessState == DeviceAccessState.Quarantined)
										{
											AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.3 Global access level is quarantine.");
											if (this.GlobalInfo.SyncStateUpgradeTime != null && this.GlobalInfo.SyncStateUpgradeTime.Value.AddMinutes((double)GlobalSettings.UpgradeGracePeriod) >= ExDateTime.UtcNow)
											{
												AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.3.1 We are in upgrade grace period.");
												accessStateReason = DeviceAccessStateReason.Upgrade;
												deviceAccessState = DeviceAccessState.Allowed;
												return deviceAccessState;
											}
											if (this.GlobalInfo.DeviceInformationReceived)
											{
												AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.3.2 We have received device information.");
												accessStateReason = DeviceAccessStateReason.Global;
												deviceAccessState = DeviceAccessState.Quarantined;
												return deviceAccessState;
											}
											if (this.GlobalInfo.DeviceInformationPromoted)
											{
												this.ProtocolLogger.SetValue(ProtocolLoggerData.AccessStateAndReason, "DeviceInformationPromoted");
												AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Device type has been promoted to device model.");
												accessStateReason = DeviceAccessStateReason.Global;
												deviceAccessState = DeviceAccessState.Quarantined;
												return deviceAccessState;
											}
											if (this.syncStateStorage.CreationTime.AddMinutes((double)GlobalSettings.DeviceDiscoveryPeriod) >= ExDateTime.UtcNow)
											{
												AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.3.3 DeviceDiscovery.");
												accessStateReason = DeviceAccessStateReason.Global;
												deviceAccessState = DeviceAccessState.DeviceDiscovery;
												return deviceAccessState;
											}
										}
										AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "4.4 Everything else...");
										deviceAccessState2 = deviceAccessState;
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				AirSyncDiagnostics.TraceInfo<DeviceAccessState, DeviceAccessStateReason>(ExTraceGlobals.RequestsTracer, this, "Result: State:{0}, Reason {1}", deviceAccessState, accessStateReason);
			}
			return deviceAccessState2;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001B440 File Offset: 0x00019640
		private bool ShouldSendMDMComplianceNotificationMail()
		{
			if ((!this.Context.User.Features.IsEnabled(EasFeature.CloudMDMEnrolled) && !ADNotificationManager.GetOrganizationSettingsData(this.User).IsIntuneManaged) || !DeviceCapability.DeviceSupportedForMdm(this.GlobalInfo))
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Skip Mdm email because not does not qualify.");
				return false;
			}
			if (this.GlobalInfo.DeviceAccessState == this.PreviousAccessState)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Skip Mdm email device is in previous state.");
				return false;
			}
			if (this.GlobalInfo.DeviceAccessState != DeviceAccessState.Quarantined || this.GlobalInfo.DeviceAccessStateReason <= DeviceAccessStateReason.ExternallyManaged)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Skip Mdm email device is not externally managed.");
				return false;
			}
			return true;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001B4EC File Offset: 0x000196EC
		private bool ShouldSendABQNotificationMail()
		{
			return this.GlobalInfo.DeviceAccessStateReason != DeviceAccessStateReason.Policy && this.GlobalInfo.DeviceAccessState != this.PreviousAccessState && (this.GlobalInfo.DeviceAccessState == DeviceAccessState.Quarantined || this.GlobalInfo.DeviceAccessState == DeviceAccessState.Blocked) && this.GlobalInfo.DeviceAccessStateReason < DeviceAccessStateReason.UserAgentsChanges;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001B550 File Offset: 0x00019750
		private bool ShouldSendAutoBlockNotificationMail(out TimeSpan blockTime)
		{
			blockTime = TimeSpan.Zero;
			return this.GlobalInfo.DeviceAccessStateReason >= DeviceAccessStateReason.UserAgentsChanges && this.GlobalInfo.DeviceAccessState != this.PreviousAccessState && ADNotificationManager.GetAutoBlockThreshold(AutoblockThresholdType.EnableNotificationEmail).BehaviorTypeIncidenceLimit != 0 && !(ADNotificationManager.GetAutoBlockThreshold(this.GlobalInfo.DeviceAccessStateReason).DeviceBlockDuration == TimeSpan.Zero) && this.Context.DeviceBehavior.IsDeviceAutoBlocked(this.Context.RequestTime, out blockTime) != DeviceAccessStateReason.Unknown && !(blockTime <= TimeSpan.Zero);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001B5F0 File Offset: 0x000197F0
		private object GetRequestDataToLog(string requestToLog)
		{
			if (GlobalSettings.EnableMailboxLoggingVerboseMode)
			{
				return requestToLog;
			}
			string value = Regex.Match(requestToLog, "<Mime>([\\s\\S]*)</Mime>", RegexOptions.IgnoreCase).Value;
			return Regex.Replace(requestToLog, "<Mime>([\\s\\S]*)</Mime>", string.Format("<Mime>[Mime Removed] BytesCount = {0}</Mime>", Encoding.Default.GetByteCount(value).ToString()), RegexOptions.IgnoreCase);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001B641 File Offset: 0x00019841
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("Cannot open disposable state on a command after the command has been disposed of.");
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001B658 File Offset: 0x00019858
		private void CompleteDeviceAccessProcessing()
		{
			if (string.IsNullOrEmpty(this.GlobalInfo.DeviceModel))
			{
				this.GlobalInfo.DeviceModel = this.DeviceIdentity.DeviceType;
			}
			if (!string.IsNullOrEmpty(this.Request.UserAgent) && !string.Equals(this.GlobalInfo.UserAgent, this.Request.UserAgent, StringComparison.OrdinalIgnoreCase))
			{
				this.GlobalInfo.UserAgent = this.Request.UserAgent;
			}
			string deviceOS;
			if ((string.IsNullOrEmpty(this.GlobalInfo.DeviceOS) || string.Equals(this.GlobalInfo.DeviceOS, "Android", StringComparison.OrdinalIgnoreCase)) && this.TryParseDeviceOSFromUserAgent(out deviceOS))
			{
				this.GlobalInfo.DeviceOS = deviceOS;
			}
			this.UpdateADDevice(this.GlobalInfo);
			if (!string.IsNullOrEmpty(this.DeviceIdentity.DeviceType) && !string.IsNullOrEmpty(this.GlobalInfo.DeviceModel) && !this.User.IsConsumerOrganizationUser)
			{
				DeviceClassCache.Instance.Add(this.User.OrganizationId, this.DeviceIdentity.DeviceType, this.GlobalInfo.DeviceModel);
			}
			if (this.ShouldSendABQNotificationMail())
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Sending notification mail for user: '{0}', device identity: '{1}', AccessState: {2}, Previous AccessState: {3}, PreviousAccessStateReason: {4}", new object[]
				{
					this.User.Name,
					this.DeviceIdentity,
					this.CurrentAccessState,
					this.GlobalInfo.DeviceAccessState,
					this.GlobalInfo.DeviceAccessStateReason
				});
				IOrganizationSettingsData organizationSettingsData = ADNotificationManager.GetOrganizationSettingsData(this.User);
				ABQMailHelper abqmailHelper = new ABQMailHelper(this.GlobalInfo, this.Context, organizationSettingsData);
				abqmailHelper.SendABQNotificationMail();
				this.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.MailSent, "ABQ");
			}
			TimeSpan blockTime;
			if (this.ShouldSendAutoBlockNotificationMail(out blockTime))
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Sending auto-block notification mail for user: '{0}', device identifier: '{1}', AccessState: {2}, Previous AccessState: {3}, PreviousAccessStateReason: {4}", new object[]
				{
					this.User.Name,
					this.DeviceIdentity,
					this.CurrentAccessState,
					this.GlobalInfo.DeviceAccessState,
					this.GlobalInfo.DeviceAccessStateReason
				});
				IOrganizationSettingsData organizationSettingsData2 = ADNotificationManager.GetOrganizationSettingsData(OrganizationId.ForestWideOrgId, this.Context);
				ABQMailHelper abqmailHelper2 = new ABQMailHelper(this.GlobalInfo, this.Context, organizationSettingsData2);
				abqmailHelper2.SendAutoBlockNotificationMail(blockTime, ADNotificationManager.GetAutoBlockThreshold(this.Context.DeviceBehavior.AutoBlockReason).AdminEmailInsert);
				this.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.MailSent, "B");
			}
			if (this.ShouldSendMDMComplianceNotificationMail())
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "Sending MDM Quarantine email for user: '{0}', Device Identity: '{1}', AccessState: {2}, Previous AccessState: {3}, PreviousAccessStateReason: {4}, IsManaged: {5}, isCompliant: {6}", new object[]
				{
					this.User.Name,
					this.DeviceIdentity,
					this.CurrentAccessState,
					this.GlobalInfo.DeviceAccessState,
					this.GlobalInfo.DeviceAccessStateReason,
					this.isManaged,
					this.isCompliant
				});
				IOrganizationSettingsData organizationSettingsData3 = ADNotificationManager.GetOrganizationSettingsData(this.User);
				ABQMailHelper abqmailHelper3 = new ABQMailHelper(this.GlobalInfo, this.Context, organizationSettingsData3);
				abqmailHelper3.SendMdmQuarantineEmail(this.GlobalInfo.DeviceAccessStateReason != DeviceAccessStateReason.ExternalEnrollment);
				this.ProtocolLogger.SetValueIfNotSet(ProtocolLoggerData.MailSent, (this.GlobalInfo.DeviceAccessStateReason == DeviceAccessStateReason.ExternalEnrollment) ? "M" : "C");
			}
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001B9F0 File Offset: 0x00019BF0
		internal bool ScheduleTask()
		{
			string action = null;
			try
			{
				ActivityContext.SetThreadScope(this.context.ActivityScope);
				action = this.User.Context.ActivityScope.Action;
				this.User.Context.ActivityScope.Action = this.RootNodeName;
				this.maxExecutionTime = this.context.RequestTime + TimeSpan.FromSeconds((double)GlobalSettings.MaxThrottlingDelay) - ExDateTime.UtcNow;
				if (this.maxExecutionTime <= TimeSpan.Zero)
				{
					AirSyncDiagnostics.TraceError<ExDateTime, int, ExDateTime>(ExTraceGlobals.RequestsTracer, this, "Request is over max delay! requestTime: {0}, MaxThrottlingDelay: {1}, utcNow: {2}", this.context.RequestTime, GlobalSettings.MaxThrottlingDelay, ExDateTime.UtcNow);
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "RequestOverMaxDelay");
					this.context.Response.IssueErrorResponse(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater);
					this.CompleteHttpRequest();
					return false;
				}
			}
			finally
			{
				ActivityContext.ClearThreadScope();
				if (this.User != null && this.User.Context != null && this.User.Context.ActivityScope != null)
				{
					this.User.Context.ActivityScope.Action = action;
				}
			}
			TimeSpan timeSpan = ExDateTime.UtcNow - this.context.RequestTime;
			this.context.SetDiagnosticValue(ConditionalHandlerSchema.PreWlmElapsed, timeSpan);
			AirSyncDiagnostics.TraceInfo<TimeSpan>(ExTraceGlobals.RequestsTracer, this, "[Command.ScheduleTask] Submitting task to WLM.  Pre-WLM time spent: {0}", timeSpan);
			bool flag = UserWorkloadManager.Singleton.TrySubmitNewTask(this);
			if (!flag)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "TrySubmitNewTask failure!");
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "TrySubmitNewTaskFailure");
				this.context.Response.IssueErrorResponse(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater);
				this.CompleteHttpRequest();
			}
			return flag;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001BBC8 File Offset: 0x00019DC8
		public IActivityScope GetActivityScope()
		{
			if (this.User != null && this.User.Context != null)
			{
				return this.User.Context.ActivityScope;
			}
			return null;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001BCBC File Offset: 0x00019EBC
		public TaskExecuteResult Execute(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			ExDateTime utcNow = ExDateTime.UtcNow;
			this.ExecuteWithCommandTls(delegate
			{
				AirSyncDiagnostics.TraceInfo<TimeSpan, TimeSpan>(ExTraceGlobals.RequestsTracer, this, "[Command.Execute] ITask.Execute called.  QueueAndDelayTime: {0}, TotalTime: {1}", queueAndDelayTime, totalTime);
				this.ProtocolLogger.SetValue(ProtocolLoggerData.ThrottledTime, (int)queueAndDelayTime.TotalMilliseconds);
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.TimeStarted, ExDateTime.UtcNow);
				using (ExPerfTrace.RelatedActivity(this.GetTraceActivityId()))
				{
					this.WorkerThread();
				}
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.TimeFinished, ExDateTime.UtcNow);
			});
			this.context.SetDiagnosticValue(ConditionalHandlerSchema.CommandElapsed, ExDateTime.UtcNow - utcNow);
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001BD9C File Offset: 0x00019F9C
		public void Complete(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.ExecuteWithCommandTls(delegate
			{
				AirSyncDiagnostics.TraceInfo<TimeSpan, TimeSpan, Command.ExecutionState>(ExTraceGlobals.RequestsTracer, this, "[Command.Complete] ITask.Complete called.  QueueAndDelayTime: {0}, TotalTime: {1}.  ExecutionState: {2}", queueAndDelayTime, totalTime, this.executionState);
				if (this.executionState != Command.ExecutionState.Pending)
				{
					this.context.SetDiagnosticValue(ConditionalHandlerIntermediateSchema.PostWlmStartTime, ExDateTime.UtcNow);
					this.CompleteHttpRequest();
				}
			});
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001BE3E File Offset: 0x0001A03E
		public void Cancel()
		{
			this.ExecuteWithCommandTls(delegate
			{
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "CancelFromWLM");
				this.context.SetDiagnosticValue(ConditionalHandlerIntermediateSchema.PostWlmStartTime, ExDateTime.UtcNow);
				this.context.Response.SetErrorResponse(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater);
				this.context.Response.IssueWbXmlResponse();
				this.CompleteHttpRequest();
			});
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001BE52 File Offset: 0x0001A052
		public IBudget Budget
		{
			get
			{
				return this.budget;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001BE5A File Offset: 0x0001A05A
		public TimeSpan MaxExecutionTime
		{
			get
			{
				return this.maxExecutionTime;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001BF2C File Offset: 0x0001A12C
		public void Timeout(TimeSpan queueAndDelayTime, TimeSpan totalTime)
		{
			this.context.SetDiagnosticValue(ConditionalHandlerSchema.WlmQueueElapsed, queueAndDelayTime);
			this.ExecuteWithCommandTls(delegate
			{
				AirSyncDiagnostics.TraceInfo<TimeSpan, TimeSpan>(ExTraceGlobals.RequestsTracer, this, "[Command.Timeout] ITask.Timeout called.  QueueAndDelayTime: {0}, TotalTime: {1}", queueAndDelayTime, totalTime);
				this.context.SetDiagnosticValue(ConditionalHandlerIntermediateSchema.PostWlmStartTime, ExDateTime.UtcNow);
				this.ProtocolLogger.SetValue(ProtocolLoggerData.ThrottledTime, (int)queueAndDelayTime.TotalMilliseconds);
				this.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "WLMTimeout");
				this.context.Response.SetErrorResponse(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater);
				this.context.Response.IssueWbXmlResponse();
				this.CompleteHttpRequest();
			});
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
		public TaskExecuteResult CancelStep(LocalizedException exception)
		{
			this.ExecuteWithCommandTls(delegate
			{
				AirSyncDiagnostics.TraceInfo<LocalizedException>(ExTraceGlobals.RequestsTracer, this, "[Command.Execute] ITask.CancelStep called.  Exception: {0}", exception);
				AirSyncUtility.ProcessException(exception, this, this.context);
				this.context.Response.IssueWbXmlResponse();
			});
			return TaskExecuteResult.ProcessingComplete;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001C01C File Offset: 0x0001A21C
		public ResourceKey[] GetResources()
		{
			return null;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001C01F File Offset: 0x0001A21F
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0001C027 File Offset: 0x0001A227
		public WorkloadSettings WorkloadSettings { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001C030 File Offset: 0x0001A230
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001C033 File Offset: 0x0001A233
		public object State
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001C035 File Offset: 0x0001A235
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001C042 File Offset: 0x0001A242
		public string Description
		{
			get
			{
				return this.context.TaskDescription;
			}
			set
			{
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001C044 File Offset: 0x0001A244
		public TimeSpan GetActionTimeout(CostType costType)
		{
			return Microsoft.Exchange.Data.Directory.Budget.GetMaxActionTime(costType);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001C04C File Offset: 0x0001A24C
		protected Guid GetTraceActivityId()
		{
			IActivityScope activityScope = this.GetActivityScope();
			if (activityScope != null)
			{
				return activityScope.ActivityId;
			}
			return Guid.NewGuid();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001C070 File Offset: 0x0001A270
		protected void AddHeadersForEnterpriseOrgUser()
		{
			AirSyncDiagnostics.Assert(!this.User.IsConsumerOrganizationUser, "User does not belong to the enterprise org.", new object[0]);
			string value;
			if (this.Context.Request.CommandType == CommandType.Options)
			{
				value = (this.Context.User.Features.IsEnabled(EasFeature.EnableV160) ? Constants.ProtocolExperimantalVersionsHeaderValue : Constants.ProtocolVersionsHeaderValue);
				if (this.Version == 120)
				{
					this.Context.Response.AppendHeader("MS-ASSeamlessUpgradeVersions", "12.1");
				}
			}
			else
			{
				value = Constants.ProtocolVersionsHeaderValue;
				this.Context.Response.AppendHeader("X-MS-RP", value);
			}
			this.Context.Response.AppendHeader("MS-ASProtocolVersions", value);
			this.Context.Response.AppendHeader("MS-ASProtocolCommands", "Sync,SendMail,SmartForward,SmartReply,GetAttachment,GetHierarchy,CreateCollection,DeleteCollection,MoveCollection,FolderSync,FolderCreate,FolderDelete,FolderUpdate,MoveItems,GetItemEstimate,MeetingResponse,Search,Settings,Ping,ItemOperations,Provision,ResolveRecipients,ValidateCert");
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001C148 File Offset: 0x0001A348
		protected void AddHeadersForConsumerOrgUser()
		{
			AirSyncDiagnostics.Assert(this.User.IsConsumerOrganizationUser, "User does not belong to the consumer org.", new object[0]);
			string value;
			string value2;
			if (DeviceFilterManager.ContactsOnly)
			{
				value = "14.0";
				value2 = "Sync,FolderSync,GetItemEstimate,Ping";
			}
			else if (DeviceFilterManager.V25OnlyInOptions)
			{
				value = "2.5";
				value2 = "Sync,SendMail,SmartForward,SmartReply,GetAttachment,FolderSync,FolderCreate,FolderDelete,FolderUpdate,MoveItems,GetItemEstimate,MeetingResponse,Ping";
			}
			else
			{
				value = "2.5,14.0";
				value2 = "Sync,SendMail,SmartForward,SmartReply,GetAttachment,FolderSync,FolderCreate,FolderDelete,FolderUpdate,MoveItems,GetItemEstimate,MeetingResponse,Search,Settings,Ping,ItemOperations";
			}
			if (this.Context.Request.CommandType != CommandType.Options)
			{
				this.Context.Response.AppendHeader("X-MS-RP", value);
			}
			this.Context.Response.AppendHeader("MS-ASProtocolVersions", value);
			this.Context.Response.AppendHeader("MS-ASProtocolCommands", value2);
			this.Context.Response.AppendHeader("X-OLK-Extensions", "1=0E47");
		}

		// Token: 0x040002D8 RID: 728
		private const int ExpectedPropertyGroupMappingId = 514;

		// Token: 0x040002D9 RID: 729
		private const string AndroidBaseOSString = "Android";

		// Token: 0x040002DA RID: 730
		private const string regexToMatchMimeTag = "<Mime>([\\s\\S]*)</Mime>";

		// Token: 0x040002DB RID: 731
		private const string replacedMimeTagText = "<Mime>[Mime Removed] BytesCount = {0}</Mime>";

		// Token: 0x040002DC RID: 732
		private static long numSkinnyICSFolderChecks = 0L;

		// Token: 0x040002DD RID: 733
		private static long numFatDeepTraversalFolderChecks = 0L;

		// Token: 0x040002DE RID: 734
		private static readonly string machineName = Environment.MachineName;

		// Token: 0x040002DF RID: 735
		private static readonly TimeSpan timeAllowedToCompleteEarly = new TimeSpan(0, 0, 0, 0, GlobalSettings.EarlyCompletionTolerance);

		// Token: 0x040002E0 RID: 736
		private static readonly TimeSpan disablePeriodsTimespan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x040002E1 RID: 737
		private static readonly TimeSpan bootstrapMailDeliveryDelay = new TimeSpan(0, 0, GlobalSettings.BootstrapMailDeliveryDelay);

		// Token: 0x040002E2 RID: 738
		[ThreadStatic]
		private static Stack<Command> commandStack;

		// Token: 0x040002E3 RID: 739
		private static Random randomGenerator = new Random();

		// Token: 0x040002E4 RID: 740
		private IStandardBudget budget;

		// Token: 0x040002E5 RID: 741
		private bool? mailboxLoggingEnabled;

		// Token: 0x040002E6 RID: 742
		private bool disposed;

		// Token: 0x040002E7 RID: 743
		private bool sessionBorrowed;

		// Token: 0x040002E8 RID: 744
		private bool isManaged = true;

		// Token: 0x040002E9 RID: 745
		private bool isCompliant = true;

		// Token: 0x040002EA RID: 746
		protected int validToCommitSyncStatusSyncState;

		// Token: 0x040002EB RID: 747
		private Stopwatch requestWaitWatch;

		// Token: 0x040002EC RID: 748
		private IAirSyncContext context;

		// Token: 0x040002ED RID: 749
		private bool sendServerUpgradeHeader;

		// Token: 0x040002EE RID: 750
		private ISyncStatusData syncStatusSyncData;

		// Token: 0x040002EF RID: 751
		private ExPerformanceCounter perfCounter;

		// Token: 0x040002F0 RID: 752
		private int requestId;

		// Token: 0x040002F1 RID: 753
		private bool perUserTracingEnabled;

		// Token: 0x040002F2 RID: 754
		private MailboxLogger mailboxLogger;

		// Token: 0x040002F3 RID: 755
		private SecurityContextAndSession securityContextAndSession;

		// Token: 0x040002F4 RID: 756
		private object contextAndSessionLock = new object();

		// Token: 0x040002F5 RID: 757
		private SyncStateStorage syncStateStorage;

		// Token: 0x040002F6 RID: 758
		private LazyAsyncResult result;

		// Token: 0x040002F7 RID: 759
		private DisposeTracker disposeTracker;

		// Token: 0x040002F8 RID: 760
		private ADDeviceManager deviceManager;

		// Token: 0x040002F9 RID: 761
		private ExDateTime nextPolicyRefreshTime;

		// Token: 0x040002FA RID: 762
		private Timer completionTimer;

		// Token: 0x040002FB RID: 763
		private bool isNewSyncStateStorage;

		// Token: 0x040002FC RID: 764
		private HashSet<IDisposable> dataToBeDisposed = new HashSet<IDisposable>();

		// Token: 0x040002FD RID: 765
		private Conversation cachedConversation;

		// Token: 0x040002FE RID: 766
		private Dictionary<StoreObjectId, Dictionary<AttachmentId, string>> inlineAttachmentContentIdLookUp;

		// Token: 0x040002FF RID: 767
		private HashSet<ConversationId> openedConversationIdList = new HashSet<ConversationId>();

		// Token: 0x04000300 RID: 768
		private TimeSpan maxExecutionTime;

		// Token: 0x04000301 RID: 769
		private Command.ExecutionState executionState;

		// Token: 0x04000302 RID: 770
		private bool shouldWatsonWhenReloadingSameConversation;

		// Token: 0x02000043 RID: 67
		protected enum IcsFolderCheckResults
		{
			// Token: 0x0400030B RID: 779
			NoChanges,
			// Token: 0x0400030C RID: 780
			ChangesNoDeepCheck,
			// Token: 0x0400030D RID: 781
			ChangesNeedDeepCheck
		}

		// Token: 0x02000044 RID: 68
		internal enum ExecutionState
		{
			// Token: 0x0400030F RID: 783
			Invalid,
			// Token: 0x04000310 RID: 784
			Pending,
			// Token: 0x04000311 RID: 785
			Complete
		}

		// Token: 0x02000045 RID: 69
		public class StateData
		{
			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001C27A File Offset: 0x0001A47A
			// (set) Token: 0x06000487 RID: 1159 RVA: 0x0001C282 File Offset: 0x0001A482
			public string DevicePhoneNumberForSms { get; set; }

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001C28B File Offset: 0x0001A48B
			// (set) Token: 0x06000489 RID: 1161 RVA: 0x0001C293 File Offset: 0x0001A493
			public bool DeviceEnableOutboundSMS { get; set; }
		}
	}
}
