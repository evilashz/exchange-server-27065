using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200006F RID: 111
	public sealed class DeviceInfo
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00022E24 File Offset: 0x00021024
		internal DeviceInfo()
		{
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00022E2C File Offset: 0x0002102C
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00022E34 File Offset: 0x00021034
		internal DeviceIdentity DeviceIdentity { get; private set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00022E3D File Offset: 0x0002103D
		internal string DeviceModel
		{
			get
			{
				return this.deviceModel;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00022E45 File Offset: 0x00021045
		internal string DeviceImei
		{
			get
			{
				return this.deviceImei;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00022E4D File Offset: 0x0002104D
		internal string DeviceFriendlyName
		{
			get
			{
				return this.deviceFriendlyName;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00022E55 File Offset: 0x00021055
		internal string DeviceOS
		{
			get
			{
				return this.deviceOS;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x00022E5D File Offset: 0x0002105D
		internal string DeviceOSLanguage
		{
			get
			{
				return this.deviceOSLanguage;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00022E65 File Offset: 0x00021065
		internal string DevicePhoneNumber
		{
			get
			{
				return this.devicePhoneNumber;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00022E6D File Offset: 0x0002106D
		internal bool DeviceEnableOutboundSMS
		{
			get
			{
				return this.deviceEnableOutboundSMS;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00022E75 File Offset: 0x00021075
		internal string DeviceMobileOperator
		{
			get
			{
				return this.deviceMobileOperator;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00022E7D File Offset: 0x0002107D
		internal ExDateTime? FirstSyncTime
		{
			get
			{
				return this.firstSyncTime;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00022E85 File Offset: 0x00021085
		internal ExDateTime? LastSyncAttemptTime
		{
			get
			{
				return this.lastSyncAttemptTime;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00022E8D File Offset: 0x0002108D
		internal ExDateTime? LastSyncSuccessTime
		{
			get
			{
				return this.lastSyncSuccessTime;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00022E95 File Offset: 0x00021095
		internal string UserAgent
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00022E9D File Offset: 0x0002109D
		internal ExDateTime? WipeRequestTime
		{
			get
			{
				return this.wipeRequestTime;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00022EA5 File Offset: 0x000210A5
		internal ExDateTime? WipeSentTime
		{
			get
			{
				return this.wipeSentTime;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x00022EAD File Offset: 0x000210AD
		internal ExDateTime? WipeAckTime
		{
			get
			{
				return this.wipeAckTime;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00022EB5 File Offset: 0x000210B5
		internal string[] RemoteWipeConfirmationAddresses
		{
			get
			{
				return this.remoteWipeConfirmationAddresses;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x00022EBD File Offset: 0x000210BD
		internal ExDateTime? LastPolicyUpdateTime
		{
			get
			{
				return this.lastPolicyUpdateTime;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00022EC5 File Offset: 0x000210C5
		internal uint? LastPingHeartbeat
		{
			get
			{
				return this.lastPingHeartbeat;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00022ECD File Offset: 0x000210CD
		internal string RecoveryPassword
		{
			get
			{
				return this.recoveryPassword;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x00022ED5 File Offset: 0x000210D5
		internal bool IsRemoteWipeSupported
		{
			get
			{
				return this.remoteWipeSupported;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00022EDD File Offset: 0x000210DD
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00022EE5 File Offset: 0x000210E5
		internal DeviceAccessState DeviceAccessState { get; private set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00022EEE File Offset: 0x000210EE
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00022EF6 File Offset: 0x000210F6
		internal DeviceAccessStateReason DeviceAccessStateReason { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00022EFF File Offset: 0x000210FF
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00022F07 File Offset: 0x00021107
		internal ExDateTime? SSUpgradeDateTime { get; private set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00022F10 File Offset: 0x00021110
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00022F18 File Offset: 0x00021118
		internal bool HaveSentBoostrapMailForWM61 { get; private set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00022F21 File Offset: 0x00021121
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00022F29 File Offset: 0x00021129
		internal ADObjectId DevicePolicyApplied { get; private set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00022F32 File Offset: 0x00021132
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x00022F3A File Offset: 0x0002113A
		internal DevicePolicyApplicationStatus DevicePolicyApplicationStatus { get; private set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00022F43 File Offset: 0x00021143
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00022F4B File Offset: 0x0002114B
		internal ADObjectId DeviceAccessControlRule { get; private set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00022F54 File Offset: 0x00021154
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x00022F5C File Offset: 0x0002115C
		internal string LastDeviceWipeRequestor { get; private set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00022F65 File Offset: 0x00021165
		// (set) Token: 0x060005F7 RID: 1527 RVA: 0x00022F6D File Offset: 0x0002116D
		internal string ClientVersion { get; private set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00022F76 File Offset: 0x00021176
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00022F7E File Offset: 0x0002117E
		internal int NumberOfFoldersSynced { get; private set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00022F87 File Offset: 0x00021187
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00022F8F File Offset: 0x0002118F
		internal ADObjectId DeviceADObjectId { get; private set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00022F98 File Offset: 0x00021198
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00022FA0 File Offset: 0x000211A0
		internal ADObjectId UserADObjectId { get; private set; }

		// Token: 0x060005FE RID: 1534 RVA: 0x00022FAC File Offset: 0x000211AC
		internal static DeviceInfo GetDeviceInfo(MailboxSession mailboxSession, DeviceIdentity deviceIdentity)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("deviceIdentity", deviceIdentity);
			if (deviceIdentity.IsDnMangled)
			{
				AirSyncDiagnostics.TraceDebug<DeviceIdentity>(ExTraceGlobals.RequestsTracer, null, "[DeviceInfo.RemoveDeviceFromMailbox] Device Id was mangled due to naming conflicts.  Cannot remove device since we cannot generate the composite sync state name.  Device identity: '{0}'", deviceIdentity);
				return null;
			}
			DeviceInfo result;
			using (SyncStateStorage syncStateStorage = SyncStateStorage.Bind(mailboxSession, deviceIdentity, null))
			{
				result = ((syncStateStorage == null) ? null : DeviceInfo.GetDeviceInfo(syncStateStorage));
			}
			return result;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00023020 File Offset: 0x00021220
		internal static bool CleanUpMobileDevice(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, TimeSpan inactivityPeriod)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("deviceIdentity", deviceIdentity);
			bool result;
			using (SyncStateStorage syncStateStorage = SyncStateStorage.Bind(mailboxSession, deviceIdentity, null))
			{
				if (syncStateStorage == null)
				{
					result = true;
				}
				else
				{
					DeviceInfo deviceInfo = DeviceInfo.GetDeviceInfo(syncStateStorage);
					if (deviceInfo == null)
					{
						result = true;
					}
					else
					{
						ExDateTime? exDateTime = (deviceInfo.LastSyncAttemptTime != null) ? deviceInfo.LastSyncAttemptTime : deviceInfo.LastSyncSuccessTime;
						if (exDateTime == null || ExDateTime.UtcNow.Subtract(exDateTime.Value) > inactivityPeriod)
						{
							AirSyncDiagnostics.TraceDebug<DeviceIdentity>(ExTraceGlobals.RequestsTracer, null, "Cleaning up device with device identity: '{0}'", deviceInfo.DeviceIdentity);
							DeviceInfo.RemoveDeviceFromMailbox(mailboxSession, syncStateStorage, deviceIdentity);
							result = true;
						}
						else
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000230EC File Offset: 0x000212EC
		internal static DeviceInfo[] GetAllDeviceInfo(MailboxSession mailboxSession, bool loadPreE14SyncStates = false)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			List<DeviceInfo> list = new List<DeviceInfo>(4);
			IEnumerator enumerator = SyncStateStorage.GetEnumerator(mailboxSession, null);
			using (enumerator as IDisposable)
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					SyncStateStorage syncStateStorage = (SyncStateStorage)obj;
					if (string.Equals(syncStateStorage.DeviceIdentity.Protocol, "AirSync", StringComparison.OrdinalIgnoreCase) || string.Equals(syncStateStorage.DeviceIdentity.Protocol, "MOWA", StringComparison.OrdinalIgnoreCase))
					{
						SyncStatusSyncStateInfo syncStateInfo = new SyncStatusSyncStateInfo();
						using (CustomSyncState customSyncState = syncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]))
						{
							bool flag = SyncStatusSyncStateInfo.IsPreE14SyncState(customSyncState);
							AirSyncDiagnostics.TraceDebug<bool, DeviceIdentity>(ExTraceGlobals.RequestsTracer, null, "DeviceInfo::GetAllDeviceInfo- isPreE14SyncState: {0}, device identity: {1}", flag, syncStateStorage.DeviceIdentity);
							if (!flag || loadPreE14SyncStates)
							{
								list.Add(DeviceInfo.GetDeviceInfo(syncStateStorage, customSyncState, false));
							}
						}
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0002327C File Offset: 0x0002147C
		internal static DeviceInfo[] GetAllDeviceInfo(MailboxSession mailboxSession, MobileClientType mobileClientType)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			List<DeviceInfo> deviceInfos = new List<DeviceInfo>(4);
			IEnumerator enumerator = SyncStateStorage.GetEnumerator(mailboxSession, null);
			using (enumerator as IDisposable)
			{
				while (enumerator.MoveNext())
				{
					SyncStateStorage syncStateStorage = (SyncStateStorage)enumerator.Current;
					if ((syncStateStorage.DeviceIdentity.IsProtocol("AirSync") && (mobileClientType & MobileClientType.EAS) == MobileClientType.EAS) || (syncStateStorage.DeviceIdentity.IsProtocol("MOWA") && (mobileClientType & MobileClientType.MOWA) == MobileClientType.MOWA))
					{
						DeviceInfo.CorruptionSafeAction("GetAllDeviceInfo(MailboxSession)", delegate
						{
							SyncStatusSyncStateInfo syncStateInfo = new SyncStatusSyncStateInfo();
							using (CustomSyncState customSyncState = syncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]))
							{
								if (!SyncStatusSyncStateInfo.IsPreE14SyncState(customSyncState))
								{
									DeviceInfo deviceInfo = DeviceInfo.GetDeviceInfo(syncStateStorage, customSyncState, false);
									if (deviceInfo != null)
									{
										deviceInfos.Add(deviceInfo);
									}
								}
							}
						});
					}
					else
					{
						AirSyncDiagnostics.TraceDebug<DeviceIdentity>(ExTraceGlobals.ValidationTracer, null, "[DeviceInfo.GetAllDeviceInfo] Ignoring device '{0}' because it is not an AirSync device.", syncStateStorage.DeviceIdentity);
					}
				}
			}
			if (deviceInfos.Count != 0)
			{
				return deviceInfos.ToArray();
			}
			return null;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00023394 File Offset: 0x00021594
		internal static DeviceInfo GetDeviceInfo(SyncStateStorage syncStateStorage)
		{
			return DeviceInfo.GetDeviceInfo(syncStateStorage, null, true);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000238C4 File Offset: 0x00021AC4
		internal static DeviceInfo GetDeviceInfo(SyncStateStorage syncStateStorage, CustomSyncState syncStatusSyncState, bool shouldDisposeSyncState)
		{
			DeviceInfo currentInfo = new DeviceInfo();
			currentInfo.firstSyncTime = new ExDateTime?(syncStateStorage.CreationTime);
			currentInfo.DeviceIdentity = syncStateStorage.DeviceIdentity;
			if (!DeviceInfo.CorruptionSafeAction("GetDeviceInfo", delegate
			{
				SyncStatusSyncStateInfo syncStatusSyncStateInfo = new SyncStatusSyncStateInfo();
				syncStatusSyncStateInfo.ReadOnly = true;
				if (syncStatusSyncState == null)
				{
					syncStatusSyncState = syncStateStorage.GetCustomSyncState(syncStatusSyncStateInfo, new PropertyDefinition[0]);
				}
				if (syncStatusSyncState != null)
				{
					currentInfo.userAgent = DeviceInfo.GetObjectProperty<ConstStringData, string>(syncStatusSyncState, CustomStateDatumType.UserAgent, null);
					currentInfo.lastSyncAttemptTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(syncStatusSyncState, CustomStateDatumType.LastSyncAttemptTime, null);
					currentInfo.lastSyncSuccessTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(syncStatusSyncState, CustomStateDatumType.LastSyncSuccessTime, null);
					if (shouldDisposeSyncState)
					{
						syncStatusSyncState.Dispose();
					}
				}
				using (AutdStatusData autdStatusData = AutdStatusData.Load(syncStateStorage, true, false))
				{
					if (autdStatusData != null)
					{
						currentInfo.lastPingHeartbeat = ((autdStatusData.LastPingHeartbeat != null) ? new uint?((uint)autdStatusData.LastPingHeartbeat.Value) : null);
					}
				}
				GlobalSyncStateInfo globalSyncStateInfo = new GlobalSyncStateInfo();
				globalSyncStateInfo.ReadOnly = true;
				using (CustomSyncState customSyncState = syncStateStorage.GetCustomSyncState(globalSyncStateInfo, new PropertyDefinition[0]))
				{
					if (customSyncState != null)
					{
						currentInfo.wipeRequestTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.WipeRequestTime, null);
						currentInfo.wipeSentTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.WipeSendTime, null);
						currentInfo.wipeAckTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.WipeAckTime, null);
						currentInfo.remoteWipeConfirmationAddresses = DeviceInfo.GetObjectProperty<ArrayData<StringData, string>, string[]>(customSyncState, CustomStateDatumType.WipeConfirmationAddresses, null);
						currentInfo.lastPolicyUpdateTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.LastPolicyTime, null);
						currentInfo.recoveryPassword = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.RecoveryPassword, null);
						currentInfo.deviceModel = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceModel, null);
						currentInfo.deviceImei = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceImei, null);
						currentInfo.deviceFriendlyName = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceFriendlyName, null);
						currentInfo.deviceOS = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceOS, null);
						currentInfo.deviceOSLanguage = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceOSLanguage, null);
						currentInfo.devicePhoneNumber = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DevicePhoneNumber, null);
						string objectProperty = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.UserAgent, null);
						if (!string.IsNullOrEmpty(objectProperty))
						{
							currentInfo.userAgent = objectProperty;
						}
						currentInfo.deviceEnableOutboundSMS = DeviceInfo.GetValueProperty<BooleanData, bool>(customSyncState, CustomStateDatumType.DeviceEnableOutboundSMS, new bool?(false)).Value;
						currentInfo.deviceMobileOperator = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceMobileOperator, null);
						currentInfo.ClientVersion = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.DeviceActiveSyncVersion, null);
						double num = -1.0;
						if (!double.TryParse(currentInfo.ClientVersion, out num))
						{
							num = -1.0;
						}
						currentInfo.remoteWipeSupported = (num >= 14.0 || customSyncState.GetData<BooleanData, bool>(CustomStateDatumType.ProvisionSupported, false));
						currentInfo.SSUpgradeDateTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.SSUpgradeDateTime, null);
						currentInfo.HaveSentBoostrapMailForWM61 = DeviceInfo.GetValueProperty<BooleanData, bool>(customSyncState, CustomStateDatumType.HaveSentBoostrapMailForWM61, new bool?(false)).Value;
						currentInfo.DeviceAccessState = (DeviceAccessState)DeviceInfo.GetValueProperty<Int32Data, int>(customSyncState, CustomStateDatumType.DeviceAccessState, new int?(0)).Value;
						currentInfo.DeviceAccessStateReason = (DeviceAccessStateReason)DeviceInfo.GetValueProperty<Int32Data, int>(customSyncState, CustomStateDatumType.DeviceAccessStateReason, new int?(0)).Value;
						currentInfo.DevicePolicyApplied = DeviceInfo.GetObjectProperty<ADObjectIdData, ADObjectId>(customSyncState, CustomStateDatumType.DevicePolicyApplied, null);
						currentInfo.DevicePolicyApplicationStatus = (DevicePolicyApplicationStatus)DeviceInfo.GetValueProperty<Int32Data, int>(customSyncState, CustomStateDatumType.DevicePolicyApplicationStatus, new int?(1)).Value;
						currentInfo.LastDeviceWipeRequestor = DeviceInfo.GetObjectProperty<StringData, string>(customSyncState, CustomStateDatumType.LastDeviceWipeRequestor, null);
						currentInfo.DeviceAccessControlRule = DeviceInfo.GetObjectProperty<ADObjectIdData, ADObjectId>(customSyncState, CustomStateDatumType.DeviceAccessControlRule, null);
						currentInfo.DeviceADObjectId = DeviceInfo.GetObjectProperty<ADObjectIdData, ADObjectId>(customSyncState, CustomStateDatumType.DeviceADObjectId, null);
						currentInfo.UserADObjectId = DeviceInfo.GetObjectProperty<ADObjectIdData, ADObjectId>(customSyncState, CustomStateDatumType.UserADObjectId, null);
						if (currentInfo.lastSyncAttemptTime != null)
						{
							ExDateTime value = currentInfo.lastSyncAttemptTime.Value;
						}
						else
						{
							currentInfo.lastSyncAttemptTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.LastSyncAttemptTime, null);
							currentInfo.lastSyncSuccessTime = DeviceInfo.GetValueProperty<DateTimeData, ExDateTime>(customSyncState, CustomStateDatumType.LastSyncSuccessTime, null);
						}
					}
					currentInfo.NumberOfFoldersSynced = syncStateStorage.CountFolderSyncStates();
				}
			}))
			{
				return null;
			}
			return currentInfo;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002394C File Offset: 0x00021B4C
		internal static void SendMeMailboxLog(MailboxSession mailboxSession, DeviceIdentity deviceIdentity)
		{
			DeviceInfo deviceInfo = DeviceInfo.GetDeviceInfo(mailboxSession, deviceIdentity);
			if (deviceInfo != null)
			{
				List<string> list = new List<string>(1);
				list.Add(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				bool flag;
				DeviceInfo.SendMailboxLog(mailboxSession, mailboxSession.MailboxOwner.Alias, new DeviceInfo[]
				{
					deviceInfo
				}, list, out flag);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000239C4 File Offset: 0x00021BC4
		internal static bool SendMailboxLog(MailboxSession mailboxSession, string userName, DeviceInfo[] deviceInfos, List<string> addresses, out bool logsRetrieved)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (deviceInfos == null)
			{
				throw new ArgumentNullException("deviceInfos");
			}
			if (addresses == null)
			{
				throw new ArgumentNullException("addresses");
			}
			bool flag = false;
			MessageItem messageItem = null;
			byte[] array = null;
			logsRetrieved = false;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				CultureInfo preferedCulture = mailboxSession.PreferedCulture;
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Drafts);
				messageItem = MessageItem.Create(mailboxSession, defaultFolderId);
				messageItem.ClassName = "IPM.Note.Exchange.ActiveSync.MailboxLog";
				for (int i = 0; i < deviceInfos.Length; i++)
				{
					using (MailboxLogger mailboxLogger = new MailboxLogger(mailboxSession, deviceInfos[i].DeviceIdentity))
					{
						if (mailboxLogger.LogsExist)
						{
							logsRetrieved = true;
							string text = string.Format(CultureInfo.InvariantCulture, "EASMailboxLog_{0}_{1}_{2}.txt", new object[]
							{
								userName,
								deviceInfos[i].DeviceIdentity.DeviceType,
								deviceInfos[i].DeviceIdentity.DeviceId
							});
							stringBuilder.Append(AirSyncUtility.HtmlEncode(string.Format(CultureInfo.InvariantCulture, Strings.DeviceType.ToString(preferedCulture), new object[]
							{
								deviceInfos[i].DeviceIdentity.DeviceType
							}), false));
							stringBuilder.Append("<br>");
							stringBuilder.Append(AirSyncUtility.HtmlEncode(string.Format(CultureInfo.InvariantCulture, Strings.DeviceId.ToString(preferedCulture), new object[]
							{
								deviceInfos[i].DeviceIdentity.DeviceId
							}), false));
							stringBuilder.Append("<br><br>");
							using (StreamAttachment streamAttachment = (StreamAttachment)messageItem.AttachmentCollection.Create(AttachmentType.Stream))
							{
								streamAttachment.FileName = text;
								using (Stream contentStream = streamAttachment.GetContentStream())
								{
									if (deviceInfos[i].mailboxLogReport == null)
									{
										mailboxLogger.GenerateReport(contentStream);
									}
									else
									{
										if (array == null)
										{
											array = new byte[4100];
										}
										string text2 = deviceInfos[i].mailboxLogReport;
										int num;
										for (int j = 0; j < text2.Length; j += num)
										{
											num = ((text2.Length - j > 1024) ? 1024 : (text2.Length - j));
											int bytes = Encoding.UTF8.GetBytes(text2, j, num, array, 0);
											contentStream.Write(array, 0, bytes);
										}
									}
									if (contentStream.Length > 5242880L)
									{
										streamAttachment.FileName = string.Format(CultureInfo.InvariantCulture, "EASMailboxLog_{0}_{1}_{2}.gz", new object[]
										{
											userName,
											deviceInfos[i].DeviceIdentity.DeviceType,
											deviceInfos[i].DeviceIdentity.DeviceId
										});
										using (MemoryStream memoryStream = new MemoryStream((int)(contentStream.Length / 10L)))
										{
											byte[] array2 = new byte[]
											{
												31,
												139,
												8,
												8,
												0,
												0,
												0,
												0,
												4,
												0
											};
											memoryStream.Write(array2, 0, array2.Length);
											byte[] bytes2 = Encoding.UTF8.GetBytes(text);
											memoryStream.Write(bytes2, 0, bytes2.Length);
											memoryStream.WriteByte(0);
											long length = contentStream.Length;
											uint num2 = 0U;
											using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
											{
												StreamHelper.CopyStream(contentStream, deflateStream, (int)length, out num2);
											}
											memoryStream.WriteByte((byte)(num2 & 255U));
											memoryStream.WriteByte((byte)(num2 >> 8 & 255U));
											memoryStream.WriteByte((byte)(num2 >> 16 & 255U));
											memoryStream.WriteByte((byte)(num2 >> 24 & 255U));
											memoryStream.WriteByte((byte)(length & 255L));
											memoryStream.WriteByte((byte)(length >> 8 & 255L));
											memoryStream.WriteByte((byte)(length >> 16 & 255L));
											memoryStream.WriteByte((byte)(length >> 24 & 255L));
											contentStream.Flush();
											contentStream.Dispose();
											using (Stream contentStream2 = streamAttachment.GetContentStream(PropertyOpenMode.Create))
											{
												StreamHelper.CopyStream(memoryStream, contentStream2, (int)memoryStream.Length);
											}
										}
									}
								}
								streamAttachment.Save();
							}
						}
					}
				}
				if (logsRetrieved)
				{
					messageItem.Subject = string.Format(CultureInfo.InvariantCulture, Strings.DeviceStatisticsTaskMailboxLogSubject.ToString(preferedCulture), new object[]
					{
						userName
					});
					using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextHtml))
					{
						string input = string.Format(CultureInfo.InvariantCulture, Strings.Date.ToString(preferedCulture), new object[]
						{
							ExDateTime.Now.ToString(preferedCulture)
						});
						string input2 = string.Format(CultureInfo.InvariantCulture, Strings.UserName.ToString(preferedCulture), new object[]
						{
							userName.ToString(preferedCulture)
						});
						textWriter.Write("\r\n            <html>\r\n                {0}\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <p>\r\n                        <br>\r\n                        {2}\r\n                        <br><br>\r\n                        {3}\r\n                        <br><br>\r\n                        {4}\r\n                    </p>\r\n                    <font color=\"red\">\r\n                    {5}\r\n                    </font>\r\n                </body>\r\n            </html>\r\n            ", new object[]
						{
							"\r\n                <style>\r\n                    body\r\n                    {\r\n                        font-family: Tahoma;\r\n                        background-color: rgb(255,255,255);\r\n                        color: #000000;\r\n                        font-size:x-small;\r\n                        width: 600px\r\n                    }\r\n                    p\r\n                    {\r\n                        margin:0in;\r\n                    }\r\n                    h1\r\n                    {\r\n                        font-family: Arial;\r\n                        color: #000066;\r\n                        margin: 0in;\r\n                        font-size: medium; font-weight:bold\r\n                    }\r\n                </style>\r\n                ",
							AirSyncUtility.HtmlEncode(messageItem.Subject, false),
							AirSyncUtility.HtmlEncode(input, false),
							AirSyncUtility.HtmlEncode(input2, false),
							stringBuilder.ToString(),
							AirSyncUtility.HtmlEncode(Strings.DeviceStatisticsTaskMailboxLogAttachmentNote.ToString(preferedCulture), false)
						});
					}
					messageItem.From = null;
					if (addresses.Count > 0)
					{
						for (int k = 0; k < addresses.Count; k++)
						{
							Participant participant = new Participant(null, addresses[k], "SMTP");
							messageItem.Recipients.Add(participant, RecipientItemType.To);
						}
						messageItem.Send();
						flag = true;
					}
				}
			}
			finally
			{
				if (messageItem != null)
				{
					if (!flag)
					{
						messageItem.Load();
						if (messageItem.Id != null)
						{
							AggregateOperationResult aggregateOperationResult = mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
							{
								messageItem.Id.ObjectId
							});
							if (OperationResult.Succeeded != aggregateOperationResult.OperationResult)
							{
								AirSyncDiagnostics.TraceDebug<MessageItem>(ExTraceGlobals.RequestsTracer, null, "Failed to delete {0}", messageItem);
							}
						}
					}
					messageItem.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00024088 File Offset: 0x00022288
		internal static void RemoveDevice(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, bool updateThrottlingData = true)
		{
			if (mailboxSession == null)
			{
				return;
			}
			ActiveSyncDevices activeSyncDevices = null;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId), 1312, "RemoveDevice", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\DeviceInfo.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			if (updateThrottlingData && DeviceInfo.IsThrottlingLimitsExceeded(tenantOrTopologyConfigurationSession, mailboxSession.MailboxOwner, out activeSyncDevices))
			{
				return;
			}
			DeviceInfo.RemoveDeviceFromMailbox(mailboxSession, deviceIdentity);
			MobileDevice[] array = DeviceInfo.FindAllADDevice(tenantOrTopologyConfigurationSession, mailboxSession.MailboxOwner.ObjectId, deviceIdentity);
			if (array == null)
			{
				return;
			}
			foreach (MobileDevice instance in array)
			{
				tenantOrTopologyConfigurationSession.Delete(instance);
			}
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, tenantOrTopologyConfigurationSession.LastUsedDc);
			}
			if (updateThrottlingData)
			{
				DeviceInfo.UpdateThrottlingData(tenantOrTopologyConfigurationSession, activeSyncDevices);
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00024158 File Offset: 0x00022358
		internal static bool RemoveDeviceFromMailbox(MailboxSession mailboxSession, DeviceIdentity deviceIdentity)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("deviceIdentity", deviceIdentity);
			if (deviceIdentity.IsDnMangled)
			{
				AirSyncDiagnostics.TraceDebug<DeviceIdentity>(ExTraceGlobals.RequestsTracer, null, "[DeviceInfo.RemoveDeviceFromMailbox] Device Id was mangled due to naming conflicts.  Cannot remove device since we cannot generate the composite sync state name.  Device Identity: '{0}'", deviceIdentity);
				return false;
			}
			bool result;
			using (SyncStateStorage syncStateStorage = SyncStateStorage.Bind(mailboxSession, deviceIdentity, null))
			{
				result = DeviceInfo.RemoveDeviceFromMailbox(mailboxSession, syncStateStorage, deviceIdentity);
			}
			return result;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x000241C8 File Offset: 0x000223C8
		internal static bool RemoveDeviceFromMailbox(MailboxSession mailboxSession, SyncStateStorage syncStateStorage, DeviceIdentity deviceIdentity)
		{
			ArgumentValidator.ThrowIfNull("deviceIdentity", deviceIdentity);
			if (syncStateStorage != null)
			{
				DeviceInfo.ResetMobileServiceSelector(mailboxSession, syncStateStorage);
				bool flag = SyncStateStorage.DeleteSyncStateStorage(mailboxSession, syncStateStorage.FolderId, deviceIdentity, null);
				AirSyncDiagnostics.TraceDebug<DeviceIdentity, bool>(ExTraceGlobals.RequestsTracer, null, "[DeviceInfo.RemoveDeviceFromMailbox] DeletedSyncStateStorage {0}, AnyOtherDevices:{1}", deviceIdentity, flag);
				DeviceInfo.UpdateDeviceHasPartnership(mailboxSession, flag);
				return true;
			}
			return false;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00024230 File Offset: 0x00022430
		internal static void UpdateDeviceHasPartnership(MailboxSession mailboxSession, bool hasDevicePartnership)
		{
			IRecipientSession recipientSession = mailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent);
			recipientSession.UseGlobalCatalog = false;
			ADRecipient recipient = recipientSession.Read(mailboxSession.MailboxOwner.ObjectId);
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, recipientSession.LastUsedDc);
			}
			ADUser aduser = (ADUser)recipient;
			bool flag = (aduser.MobileMailboxFlags & MobileMailboxFlags.HasDevicePartnership) != MobileMailboxFlags.None;
			if (flag != hasDevicePartnership)
			{
				if (hasDevicePartnership)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "[DeviceInfo.RemoveDeviceFromMailbox] Update HasDevicePartnership to true");
					aduser.MobileMailboxFlags |= MobileMailboxFlags.HasDevicePartnership;
				}
				else
				{
					aduser.MobileMailboxFlags &= ~MobileMailboxFlags.HasDevicePartnership;
				}
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					recipientSession.Save(recipient);
				});
				if (!adoperationResult.Succeeded)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "Exception occurred during AD Operation. Message:{0}", adoperationResult.Exception.Message);
					if (Command.CurrentCommand != null)
					{
						Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "UpdateUserHasDeviceADException");
					}
				}
				if (Command.CurrentCommand != null)
				{
					Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, recipientSession.LastUsedDc);
					Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.UpdateUserHasPartnerships, hasDevicePartnership ? "T" : "F");
				}
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00024404 File Offset: 0x00022604
		internal static bool CancelRemoteWipe(SyncStateStorage syncStateStorage)
		{
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			bool success = false;
			bool flag = DeviceInfo.CorruptionSafeAction("CancelRemoteWipe", delegate
			{
				using (CustomSyncState orCreateGlobalSyncState = AirSyncUtility.GetOrCreateGlobalSyncState(syncStateStorage))
				{
					success = DeviceInfo.CancelRemoteWipeFromMailbox(orCreateGlobalSyncState);
					if (success)
					{
						orCreateGlobalSyncState.Commit();
					}
				}
			});
			return success && flag;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002445A File Offset: 0x0002265A
		internal static bool CancelRemoteWipeFromMailbox(CustomSyncState globalSyncState)
		{
			if (globalSyncState == null)
			{
				throw new ArgumentNullException("globalSyncState");
			}
			if (globalSyncState[CustomStateDatumType.WipeAckTime] != null || globalSyncState[CustomStateDatumType.WipeSendTime] != null)
			{
				return false;
			}
			globalSyncState[CustomStateDatumType.WipeRequestTime] = null;
			return true;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00024493 File Offset: 0x00022693
		internal static void StartRemoteWipe(SyncStateStorage syncStateStorage, ExDateTime wipeRequestTime, string requestorSMTP)
		{
			DeviceInfo.StartRemoteWipe(syncStateStorage, wipeRequestTime, null, requestorSMTP);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00024504 File Offset: 0x00022704
		internal static void StartRemoteWipe(SyncStateStorage syncStateStorage, ExDateTime wipeRequestTime, IList<string> additionalAddresses, string requestorSMTP)
		{
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			DeviceInfo.CorruptionSafeAction("StartRemoteWipe", delegate
			{
				using (CustomSyncState orCreateGlobalSyncState = AirSyncUtility.GetOrCreateGlobalSyncState(syncStateStorage))
				{
					DeviceInfo.StartRemoteWipeFromMailbox(syncStateStorage, orCreateGlobalSyncState, wipeRequestTime, additionalAddresses, requestorSMTP);
					orCreateGlobalSyncState.Commit();
				}
			});
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00024560 File Offset: 0x00022760
		internal static void StartRemoteWipeFromMailbox(SyncStateStorage syncStateStorage, CustomSyncState globalSyncState, ExDateTime wipeRequestTime, IList<string> additionalAddresses, string requestorSMTP)
		{
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			if (globalSyncState == null)
			{
				throw new ArgumentNullException("globalSyncState");
			}
			globalSyncState[CustomStateDatumType.WipeRequestTime] = new DateTimeData(wipeRequestTime);
			globalSyncState[CustomStateDatumType.WipeSendTime] = null;
			globalSyncState[CustomStateDatumType.WipeAckTime] = null;
			globalSyncState[CustomStateDatumType.LastDeviceWipeRequestor] = new StringData(requestorSMTP);
			if (additionalAddresses != null)
			{
				string[] array = new string[additionalAddresses.Count];
				additionalAddresses.CopyTo(array, 0);
				globalSyncState[CustomStateDatumType.WipeConfirmationAddresses] = new ArrayData<StringData, string>(array);
			}
			else
			{
				globalSyncState[CustomStateDatumType.WipeConfirmationAddresses] = null;
			}
			AutdTriggerSyncStateInfo syncStateInfo = new AutdTriggerSyncStateInfo();
			CustomSyncState customSyncState = syncStateStorage.GetCustomSyncState(syncStateInfo, new PropertyDefinition[0]);
			if (customSyncState == null)
			{
				customSyncState = syncStateStorage.CreateCustomSyncState(syncStateInfo);
			}
			customSyncState.Dispose();
			syncStateStorage.DeleteCustomSyncState(syncStateInfo);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00024628 File Offset: 0x00022828
		internal static void ResetMobileServiceSelector(MailboxSession mailboxSession, SyncStateStorage syncStateStorage)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (syncStateStorage == null)
			{
				throw new ArgumentNullException("syncStateStorage");
			}
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(mailboxSession))
			{
				TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(mailboxSession.MailboxOwner.ObjectId);
				if (textMessagingAccount != null && textMessagingAccount.EasEnabled)
				{
					DeviceInfo deviceInfo = DeviceInfo.GetDeviceInfo(syncStateStorage);
					if (deviceInfo != null)
					{
						E164Number b = null;
						E164Number.TryParse(deviceInfo.DevicePhoneNumber, out b);
						if (textMessagingAccount.EasPhoneNumber == b)
						{
							IRecipientSession adrecipientSession = mailboxSession.GetADRecipientSession(false, ConsistencyMode.FullyConsistent);
							ADRecipient adrecipient = adrecipientSession.Read(mailboxSession.MailboxOwner.ObjectId);
							if (Command.CurrentCommand != null)
							{
								Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, adrecipientSession.LastUsedDc);
							}
							if (adrecipient == null)
							{
								AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "Error:  Could not find the AD person correlated to text messaging account");
							}
							else
							{
								textMessagingAccount.SetEasDisabled();
								TextMessagingHelper.SaveTextMessagingAccount(textMessagingAccount, versionedXmlDataProvider, adrecipient, adrecipientSession);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00024734 File Offset: 0x00022934
		internal static string ObfuscatePhoneNumber(string phoneNumber)
		{
			if (phoneNumber == null)
			{
				return null;
			}
			char[] array = phoneNumber.Trim().ToCharArray();
			for (int i = 0; i < array.Length - 4; i++)
			{
				array[i] = '*';
			}
			return new string(array);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00024770 File Offset: 0x00022970
		internal static bool IsThrottlingLimitsExceeded(IConfigurationSession session, IExchangePrincipal exchangePrincipal, out ActiveSyncDevices activeSyncDevices)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			activeSyncDevices = null;
			using (IBudget budget = StandardBudget.Acquire(exchangePrincipal.Sid, BudgetType.Eas, exchangePrincipal.MailboxInfo.OrganizationId.ToADSessionSettings()))
			{
				if (budget != null)
				{
					IThrottlingPolicy throttlingPolicy = budget.ThrottlingPolicy;
					if (throttlingPolicy != null && !throttlingPolicy.EasMaxDeviceDeletesPerMonth.IsUnlimited)
					{
						ExDateTime utcNow = ExDateTime.UtcNow;
						ADObjectId rootId = MobileDevice.GetRootId(exchangePrincipal.ObjectId);
						activeSyncDevices = session.Read<ActiveSyncDevices>(rootId);
						if (activeSyncDevices == null)
						{
							AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "Error: Cannot load ActiveSyncDevices! Skipping delete count check.");
						}
						else if ((long)activeSyncDevices.ObjectsDeletedThisPeriod >= (long)((ulong)throttlingPolicy.EasMaxDeviceDeletesPerMonth.Value) && activeSyncDevices.DeletionPeriod != null && activeSyncDevices.DeletionPeriod.Value.Year == utcNow.Year && activeSyncDevices.DeletionPeriod.Value.Month == utcNow.Month)
						{
							AirSyncDiagnostics.TraceDebug<int, Unlimited<uint>, DateTime>(ExTraceGlobals.RequestsTracer, null, "Error: EASMaxDeviceDeletesPerMonth exceeded. ObjectsDeletedThisPeriod {0}, MaxDeviceDeletesPerMonth {1}, DeletionPeriod {2}", activeSyncDevices.ObjectsDeletedThisPeriod, throttlingPolicy.EasMaxDeviceDeletesPerMonth, activeSyncDevices.DeletionPeriod.Value);
							return true;
						}
					}
					else
					{
						AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "Error: Not throttling value set! Skipping delete count check.");
					}
				}
				else
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "Error: Cannot load budget! Skipping delete count check.");
				}
			}
			return false;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0002490C File Offset: 0x00022B0C
		internal static void UpdateThrottlingData(IConfigurationSession session, ActiveSyncDevices activeSyncDevices)
		{
			if (session != null && activeSyncDevices != null)
			{
				ExDateTime utcNow = ExDateTime.UtcNow;
				if (activeSyncDevices.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
				{
					activeSyncDevices.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
				}
				if (activeSyncDevices.DeletionPeriod != null && activeSyncDevices.DeletionPeriod.Value.Year == utcNow.Year && activeSyncDevices.DeletionPeriod.Value.Month == utcNow.Month)
				{
					activeSyncDevices.ObjectsDeletedThisPeriod++;
				}
				else
				{
					activeSyncDevices.ObjectsDeletedThisPeriod = 1;
					activeSyncDevices.DeletionPeriod = new DateTime?((DateTime)utcNow.ToUtc());
				}
				session.Save(activeSyncDevices);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000249D0 File Offset: 0x00022BD0
		internal string GetOrCreateMailboxLogReport(MailboxSession mailboxSession)
		{
			if (this.mailboxLogReport != null)
			{
				return this.mailboxLogReport;
			}
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			using (MailboxLogger mailboxLogger = new MailboxLogger(mailboxSession, this.DeviceIdentity))
			{
				this.mailboxLogReport = mailboxLogger.GenerateReport();
			}
			return this.mailboxLogReport;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00024A38 File Offset: 0x00022C38
		internal bool IsMailboxLogAvailable(MailboxSession mailboxSession)
		{
			if (this.mailboxLogAvailable != null)
			{
				return this.mailboxLogAvailable.Value;
			}
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			using (MailboxLogger mailboxLogger = new MailboxLogger(mailboxSession, this.DeviceIdentity))
			{
				this.mailboxLogAvailable = new bool?(mailboxLogger.LogsExist);
			}
			return this.mailboxLogAvailable.Value;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00024AB4 File Offset: 0x00022CB4
		private static RawT GetObjectProperty<T, RawT>(SyncState syncState, string name, RawT defaultProperty) where T : ComponentData<RawT>, new() where RawT : class
		{
			return syncState.GetData<T, RawT>(name, defaultProperty);
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00024AC0 File Offset: 0x00022CC0
		private static RawT? GetValueProperty<T, RawT>(SyncState syncState, string name, RawT? defaultProperty) where T : ComponentData<RawT>, new() where RawT : struct
		{
			ICustomSerializable customSerializable = syncState[name];
			if (customSerializable != null)
			{
				if (customSerializable is T)
				{
					T t = (T)((object)customSerializable);
					return new RawT?(t.Data);
				}
				if (customSerializable is NullableData<T, RawT>)
				{
					return ((NullableData<T, RawT>)customSerializable).Data;
				}
			}
			return defaultProperty;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00024B10 File Offset: 0x00022D10
		private static MobileDevice[] FindAllADDevice(IConfigurationSession session, ADObjectId mailboxOwnerADObjectId, DeviceIdentity deviceIdentity)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, MobileDeviceSchema.DeviceId, deviceIdentity.DeviceId);
			QueryFilter queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, MobileDeviceSchema.DeviceType, deviceIdentity.DeviceType);
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				queryFilter,
				queryFilter2
			});
			MobileDevice[] array = session.Find<MobileDevice>(MobileDevice.GetRootId(mailboxOwnerADObjectId), QueryScope.OneLevel, filter, null, 2);
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, session.LastUsedDc);
			}
			if (array.Length == 0)
			{
				AirSyncDiagnostics.TraceInfo<string, string>(ExTraceGlobals.ValidationTracer, null, "No ActiveSyncDevice object found in AD for user {0}, device id {1}.", mailboxOwnerADObjectId.Rdn.UnescapedName, deviceIdentity.DeviceId);
				return null;
			}
			return array;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00024BB8 File Offset: 0x00022DB8
		private static bool CorruptionSafeAction(string info, Action action)
		{
			bool result = false;
			try
			{
				action();
				result = true;
			}
			catch (CorruptSyncStateException arg)
			{
				AirSyncDiagnostics.TraceError<string, CorruptSyncStateException>(ExTraceGlobals.ValidationTracer, null, "[DeviceInfo.{0}] Sync state exception was caught: {1}", info, arg);
			}
			catch (InvalidSyncStateVersionException arg2)
			{
				AirSyncDiagnostics.TraceError<string, InvalidSyncStateVersionException>(ExTraceGlobals.ValidationTracer, null, "[DeviceInfo.{0}] Sync state exception was caught: {1}", info, arg2);
			}
			return result;
		}

		// Token: 0x04000410 RID: 1040
		private const string ActiveSyncLogReportBody = "\r\n            <html>\r\n                {0}\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <p>\r\n                        <br>\r\n                        {2}\r\n                        <br><br>\r\n                        {3}\r\n                        <br><br>\r\n                        {4}\r\n                    </p>\r\n                    <font color=\"red\">\r\n                    {5}\r\n                    </font>\r\n                </body>\r\n            </html>\r\n            ";

		// Token: 0x04000411 RID: 1041
		private const string MailboxLogFileName = "ActiveSyncMailboxLog.txt";

		// Token: 0x04000412 RID: 1042
		private const string MailboxLogReportStyle = "\r\n                <style>\r\n                    body\r\n                    {\r\n                        font-family: Tahoma;\r\n                        background-color: rgb(255,255,255);\r\n                        color: #000000;\r\n                        font-size:x-small;\r\n                        width: 600px\r\n                    }\r\n                    p\r\n                    {\r\n                        margin:0in;\r\n                    }\r\n                    h1\r\n                    {\r\n                        font-family: Arial;\r\n                        color: #000066;\r\n                        margin: 0in;\r\n                        font-size: medium; font-weight:bold\r\n                    }\r\n                </style>\r\n                ";

		// Token: 0x04000413 RID: 1043
		private const string MailboxLogReportBody = "\r\n            <html>\r\n                {0}\r\n                <body>\r\n                    <h1>{1}</h1>\r\n                    <br>\r\n                    <p>\r\n                    {2}\r\n                    </p>\r\n                </body>\r\n            </html>\r\n            ";

		// Token: 0x04000414 RID: 1044
		private const int MinLogSizeToStartCompress = 5242880;

		// Token: 0x04000415 RID: 1045
		private string deviceModel;

		// Token: 0x04000416 RID: 1046
		private string deviceImei;

		// Token: 0x04000417 RID: 1047
		private string deviceFriendlyName;

		// Token: 0x04000418 RID: 1048
		private string deviceOS;

		// Token: 0x04000419 RID: 1049
		private string deviceOSLanguage;

		// Token: 0x0400041A RID: 1050
		private string devicePhoneNumber;

		// Token: 0x0400041B RID: 1051
		private bool deviceEnableOutboundSMS;

		// Token: 0x0400041C RID: 1052
		private string deviceMobileOperator;

		// Token: 0x0400041D RID: 1053
		private ExDateTime? firstSyncTime;

		// Token: 0x0400041E RID: 1054
		private ExDateTime? lastSyncAttemptTime;

		// Token: 0x0400041F RID: 1055
		private ExDateTime? lastSyncSuccessTime;

		// Token: 0x04000420 RID: 1056
		private string userAgent;

		// Token: 0x04000421 RID: 1057
		private ExDateTime? wipeRequestTime;

		// Token: 0x04000422 RID: 1058
		private ExDateTime? wipeSentTime;

		// Token: 0x04000423 RID: 1059
		private ExDateTime? wipeAckTime;

		// Token: 0x04000424 RID: 1060
		private string[] remoteWipeConfirmationAddresses;

		// Token: 0x04000425 RID: 1061
		private ExDateTime? lastPolicyUpdateTime;

		// Token: 0x04000426 RID: 1062
		private uint? lastPingHeartbeat;

		// Token: 0x04000427 RID: 1063
		private string recoveryPassword;

		// Token: 0x04000428 RID: 1064
		private bool? mailboxLogAvailable;

		// Token: 0x04000429 RID: 1065
		private string mailboxLogReport;

		// Token: 0x0400042A RID: 1066
		private bool remoteWipeSupported;
	}
}
