using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000065 RID: 101
	internal class DeviceClassCache
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00020CB1 File Offset: 0x0001EEB1
		protected DeviceClassCache()
		{
			this.cache = new Dictionary<OrganizationId, DeviceClassCache.DeviceClassDataSet>();
			this.timerStartTime = ExDateTime.MinValue;
			this.realTimeRefresh = false;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00020CE1 File Offset: 0x0001EEE1
		public static DeviceClassCache Instance
		{
			get
			{
				return DeviceClassCache.instance;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public bool Started
		{
			get
			{
				return this.timerStartTime != ExDateTime.MinValue;
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00020CFA File Offset: 0x0001EEFA
		public static string NormalizeDeviceClass(string deviceClass)
		{
			if (string.IsNullOrEmpty(deviceClass))
			{
				return deviceClass;
			}
			if (deviceClass.StartsWith("IMEI", StringComparison.OrdinalIgnoreCase))
			{
				return "IMEI";
			}
			return deviceClass;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public void Start()
		{
			if (this.Started)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "DeviceClassCache is already started.");
				return;
			}
			if (GlobalSettings.DeviceClassCachePerOrgRefreshInterval == 0)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "DeviceClassCache is turned off.");
				return;
			}
			TimeSpan startDelay = DeviceClassCache.GetStartDelay();
			this.timerStartTime = ExDateTime.UtcNow + startDelay;
			this.refreshTimer = new GuardedTimer(new TimerCallback(this.Refresh), null, startDelay, TimeSpan.FromSeconds((double)DeviceClassCache.TimerKickinInterval));
			AirSyncDiagnostics.TraceDebug<ExDateTime, ExDateTime>(ExTraceGlobals.RequestsTracer, this, "DeviceClassCache is started at '{0}-UTC'.  The internal timer will be started at '{1}-UTC'.", ExDateTime.UtcNow, this.timerStartTime);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00020DB0 File Offset: 0x0001EFB0
		public void Stop()
		{
			if (this.refreshTimer != null)
			{
				this.refreshTimer.Dispose(true);
				this.refreshTimer = null;
			}
			if (this.cache != null)
			{
				foreach (DeviceClassCache.DeviceClassDataSet deviceClassDataSet in this.cache.Values)
				{
					deviceClassDataSet.Dispose();
				}
				this.cache.Clear();
			}
			this.timerStartTime = ExDateTime.MinValue;
			AirSyncDiagnostics.TraceDebug<ExDateTime>(ExTraceGlobals.RequestsTracer, this, "DeviceClassCache is stoped at '{0}-UTC'.", ExDateTime.UtcNow);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00020E58 File Offset: 0x0001F058
		public void Add(OrganizationId organizationId, string deviceType, string deviceModel)
		{
			AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Adding device class: orgId={0}, deviceType={1}, deviceModel={2}, started={3}.", new object[]
			{
				organizationId,
				deviceType,
				deviceModel,
				this.Started
			});
			if (!this.Started)
			{
				return;
			}
			if (string.IsNullOrEmpty(deviceType) || deviceType.Length > 32)
			{
				throw new ArgumentException("Invalid deviceType: " + deviceType);
			}
			if (string.IsNullOrEmpty(deviceModel))
			{
				throw new ArgumentNullException("deviceModel");
			}
			try
			{
				DeviceClassCache.DeviceClassData data = new DeviceClassCache.DeviceClassData(DeviceClassCache.EnforceLengthLimit(deviceType, DeviceClassCache.ADPropertyConstraintLength.MaxDeviceTypeLength, false), DeviceClassCache.EnforceLengthLimit(deviceModel, DeviceClassCache.ADPropertyConstraintLength.MaxDeviceModelLength, false));
				lock (this.thisLock)
				{
					DeviceClassCache.DeviceClassDataSet deviceClassDataSet;
					if (this.cache.TryGetValue(organizationId, out deviceClassDataSet))
					{
						if (!deviceClassDataSet.Contains(data))
						{
							if (deviceClassDataSet.Count >= GlobalSettings.DeviceClassPerOrgMaxADCount)
							{
								AirSyncDiagnostics.TraceDebug<OrganizationId, int>(ExTraceGlobals.RequestsTracer, this, "Device class will not be added to the cache since it already reaches the cap:orgId={0}, count={1}.", organizationId, deviceClassDataSet.Count);
							}
							else
							{
								deviceClassDataSet.Add(data);
								AirSyncDiagnostics.TraceDebug<OrganizationId>(ExTraceGlobals.RequestsTracer, this, "New device class is added to the existing org '{0}'.", organizationId);
							}
						}
					}
					else if (this.cache.Count >= GlobalSettings.ADCacheMaxOrgCount)
					{
						AirSyncDiagnostics.TraceDebug<OrganizationId, int>(ExTraceGlobals.RequestsTracer, this, "Device class set will not be added to the cache since it already reaches the cap:orgId={0}, count={1}.", organizationId, this.cache.Count);
					}
					else
					{
						deviceClassDataSet = new DeviceClassCache.DeviceClassDataSet(organizationId);
						deviceClassDataSet.Add(data);
						this.cache.Add(organizationId, deviceClassDataSet);
						AirSyncDiagnostics.TraceDebug<OrganizationId>(ExTraceGlobals.RequestsTracer, this, "New device class is added to the new org '{0}'.", organizationId);
					}
				}
			}
			finally
			{
				AirSyncDiagnostics.FaultInjectionTracer.TraceTest<bool>(2359700797U, ref this.realTimeRefresh);
				if (this.realTimeRefresh)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "Calling Refresh real time.");
					this.Refresh(null);
					this.realTimeRefresh = false;
				}
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002102C File Offset: 0x0001F22C
		private static string EnforceLengthLimit(string deviceClassString, int constraintLength, bool generateUniqueName)
		{
			if (deviceClassString.Length <= constraintLength)
			{
				return deviceClassString;
			}
			if (generateUniqueName)
			{
				string text = Guid.NewGuid().ToString("N");
				if (constraintLength >= text.Length)
				{
					return deviceClassString.Substring(0, constraintLength - text.Length) + text;
				}
			}
			return deviceClassString.Substring(0, constraintLength);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00021084 File Offset: 0x0001F284
		private static TimeSpan GetStartDelay()
		{
			int seed;
			try
			{
				seed = Environment.MachineName.GetHashCode();
			}
			catch
			{
				seed = Environment.TickCount;
			}
			Random random = new Random(seed);
			return TimeSpan.FromSeconds((double)random.Next(0, (int)GlobalSettings.DeviceClassCacheMaxStartDelay.TotalSeconds));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x000210DC File Offset: 0x0001F2DC
		private static void ProcessForADAdds(IConfigurationSession scopedSession, DeviceClassCache.DeviceClassDataSet localDataSet, DeviceClassCache.DeviceClassDataSet dataSetFromAD, ref int totalADWriteCount, int perOrgDeleteCount)
		{
			int num = 0;
			foreach (DeviceClassCache.DeviceClassData deviceClassData in localDataSet)
			{
				if (totalADWriteCount >= GlobalSettings.DeviceClassCacheMaxADUploadCount)
				{
					AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, null, "2. Stop updating AD because the cap is reached: adUpdateCount={0}", totalADWriteCount);
					break;
				}
				if (!dataSetFromAD.Contains(deviceClassData))
				{
					if (dataSetFromAD.Count + num - perOrgDeleteCount >= GlobalSettings.DeviceClassPerOrgMaxADCount)
					{
						AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.RequestsTracer, null, "Stop adding new device class node to AD because the cap is reached: dataSetFromAD.Count={0}, DeviceClassPerOrgMaxADCount={1}", dataSetFromAD.Count, GlobalSettings.DeviceClassPerOrgMaxADCount);
						break;
					}
					if (dataSetFromAD.Count + num >= DeviceClassCache.ADCapWarningThreshold)
					{
						AirSyncDiagnostics.TraceDebug<int, int>(ExTraceGlobals.RequestsTracer, null, "Still adding new device class node to AD but the cap is close to be reached: dataSetFromAD.Count={0}, DeviceClassPerOrgMaxADCount={1}", dataSetFromAD.Count, DeviceClassCache.ADCapWarningThreshold);
						AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_TooManyDeviceClassNodes, localDataSet.OrganizationId.ToString(), new string[]
						{
							dataSetFromAD.Count.ToString(),
							localDataSet.OrganizationId.ToString(),
							GlobalSettings.DeviceClassPerOrgMaxADCount.ToString()
						});
					}
					DeviceClassCache.CreateOrUpdateActiveSyncDeviceClass(scopedSession, deviceClassData, localDataSet.OrganizationId);
					num++;
					totalADWriteCount++;
				}
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00021220 File Offset: 0x0001F420
		private static void ProcessForADCleanup(IConfigurationSession scopedSession, DeviceClassCache.DeviceClassDataSet localDataSet, DeviceClassCache.DeviceClassDataSet dataSetFromAD, ref int totalADWriteCount, ref int perOrgDeleteCount)
		{
			foreach (DeviceClassCache.DeviceClassData deviceClassData in dataSetFromAD)
			{
				if (totalADWriteCount >= GlobalSettings.DeviceClassCacheMaxADUploadCount)
				{
					AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, null, "3. Stop updating AD because the cap is reached: adUpdateCount={0}", totalADWriteCount);
					break;
				}
				if (localDataSet.Contains(deviceClassData))
				{
					if ((ExDateTime.UtcNow - deviceClassData.LastUpdateTime).Days >= TimeSpan.FromDays((double)GlobalSettings.DeviceClassCacheADCleanupInterval).Days)
					{
						AirSyncDiagnostics.TraceDebug<ExDateTime, int>(ExTraceGlobals.RequestsTracer, null, "Update or Create DeviceClass in AD. LastUpdateTime :{0}. adUpdateCount: {1}", deviceClassData.LastUpdateTime, totalADWriteCount);
						DeviceClassCache.CreateOrUpdateActiveSyncDeviceClass(scopedSession, deviceClassData, localDataSet.OrganizationId);
						totalADWriteCount++;
					}
				}
				else if ((ExDateTime.UtcNow - deviceClassData.LastUpdateTime).Days > TimeSpan.FromDays((double)GlobalSettings.DeviceClassCacheADCleanupInterval).Days * 2)
				{
					AirSyncDiagnostics.TraceDebug<ExDateTime, int>(ExTraceGlobals.RequestsTracer, null, "Delete DeviceClass in AD. LastUpdateTime :{0}, adUpdateCount: {1} ", deviceClassData.LastUpdateTime, totalADWriteCount);
					DeviceClassCache.DeleteActiveSyncDeviceClass(scopedSession, deviceClassData, localDataSet.OrganizationId);
					perOrgDeleteCount++;
					totalADWriteCount++;
				}
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00021394 File Offset: 0x0001F594
		private static ActiveSyncDeviceClasses GetActiveSyncDeviceClassContainer(IConfigurationSession scopedSession, OrganizationId orgId)
		{
			ActiveSyncDeviceClasses[] deviceClasses = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				deviceClasses = scopedSession.Find<ActiveSyncDeviceClasses>(orgId.ConfigurationUnit, QueryScope.SubTree, DeviceClassCache.deviceClassesFilter, DeviceClassCache.deviceClassesSortOrder, 0);
			});
			DeviceClassCache.UpdateProtocolLogLastUsedDC(scopedSession);
			if (deviceClasses == null)
			{
				AirSyncDiagnostics.TraceDebug<OrganizationId>(ExTraceGlobals.RequestsTracer, null, "Oragnization \"{0}\" has no DeviceClass container in AD.", orgId);
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_NoDeviceClassContainer, new string[]
				{
					orgId.ToString()
				});
				return null;
			}
			AirSyncDiagnostics.TraceDebug<OrganizationId, int>(ExTraceGlobals.RequestsTracer, null, "Oragnization '{0}' has '{1}' DeviceClass container in AD.", orgId, deviceClasses.Length);
			if (deviceClasses.Length == 1)
			{
				return deviceClasses[0];
			}
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "TooManyDeviceClassContainers");
			}
			return DeviceClassCache.CleanMangledObjects(scopedSession, deviceClasses, "ExchangeDeviceClasses") as ActiveSyncDeviceClasses;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x000214B0 File Offset: 0x0001F6B0
		private static void CreateOrUpdateActiveSyncDeviceClass(IConfigurationSession scopedSession, DeviceClassCache.DeviceClassData deviceClassData, OrganizationId orgId)
		{
			try
			{
				ActiveSyncDeviceClass deviceClass = deviceClassData.ToActiveSyncDeviceClass(scopedSession, orgId);
				deviceClass.LastUpdateTime = new DateTime?(DateTime.UtcNow);
				ADNotificationAdapter.RunADOperation(delegate()
				{
					scopedSession.Save(deviceClass);
				});
				DeviceClassCache.UpdateProtocolLogLastUsedDC(scopedSession);
				AirSyncDiagnostics.TraceDebug<OrganizationId, string, string>(ExTraceGlobals.RequestsTracer, null, "Created DeviceClassData in AD: orgId={0}, deviceType={1}, deviceModel={2}", orgId, deviceClassData.DeviceType, deviceClassData.DeviceModel);
			}
			catch (LocalizedException ex)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, null, "Failed to create DeviceClassData: orgId={0}, deviceType={1}, deviceModel={2}, exception=\n\r{3}", new object[]
				{
					orgId,
					deviceClassData.DeviceType,
					deviceClassData.DeviceModel,
					ex
				});
			}
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00021584 File Offset: 0x0001F784
		private static void DeleteActiveSyncDeviceClass(IConfigurationSession scopedSession, DeviceClassCache.DeviceClassData deviceClassData, OrganizationId orgId)
		{
			ActiveSyncDeviceClass adObject = deviceClassData.ToActiveSyncDeviceClass(scopedSession, orgId);
			DeviceClassCache.DeleteObject(scopedSession, adObject);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x000215A2 File Offset: 0x0001F7A2
		private static void UpdateProtocolLogLastUsedDC(IConfigurationSession session)
		{
			if (Command.CurrentCommand != null)
			{
				Command.CurrentCommand.Context.ProtocolLogger.SetValue(ProtocolLoggerData.DomainController, session.LastUsedDc);
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x000215C8 File Offset: 0x0001F7C8
		private static ADConfigurationObject CleanMangledObjects(IConfigurationSession scopedSession, ADConfigurationObject[] objects, string rdnShouldBe)
		{
			int num = -1;
			for (int i = 0; i < objects.Length; i++)
			{
				if (num == -1 && !DeviceClassCache.DnIsMangled(objects[i], rdnShouldBe))
				{
					num = i;
				}
				else
				{
					DeviceClassCache.DeleteObject(scopedSession, objects[i]);
				}
			}
			if (num != -1)
			{
				return objects[num];
			}
			return null;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00021628 File Offset: 0x0001F828
		private static void DeleteObject(IConfigurationSession scopedSession, ADConfigurationObject adObject)
		{
			try
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					scopedSession.Delete(adObject);
				});
				DeviceClassCache.UpdateProtocolLogLastUsedDC(scopedSession);
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "Deleted object: {0}", adObject.Id.DistinguishedName);
			}
			catch (LocalizedException ex)
			{
				AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, null, "Failed to delete object {0} because: {1}", adObject.Id.DistinguishedName, ex.Message);
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000216C8 File Offset: 0x0001F8C8
		private static bool DnIsMangled(ADConfigurationObject adObject, string rdnShouldBe)
		{
			string escapedName = adObject.Id.Rdn.EscapedName;
			if (!escapedName.Equals(rdnShouldBe, StringComparison.Ordinal))
			{
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "Found mangled device object DN: {0}", adObject.Id.DistinguishedName);
				return true;
			}
			return false;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00021738 File Offset: 0x0001F938
		private void Refresh(object state)
		{
			try
			{
				AirSyncDiagnostics.TraceDebug<ExDateTime>(ExTraceGlobals.RequestsTracer, this, "Refresh is being call at '{0}-UTC'.", ExDateTime.UtcNow);
				AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, this, "DeviceClassCache contains '{0}' elements.", this.cache.Values.Count);
				List<DeviceClassCache.DeviceClassDataSet> list;
				lock (this.thisLock)
				{
					if (this.cache.Values.Count < 1)
					{
						return;
					}
					list = new List<DeviceClassCache.DeviceClassDataSet>(this.cache.Values);
				}
				int num = 0;
				foreach (DeviceClassCache.DeviceClassDataSet deviceClassDataSet in list)
				{
					if (this.realTimeRefresh || !(ExDateTime.UtcNow - deviceClassDataSet.StartTime < TimeSpan.FromSeconds((double)GlobalSettings.DeviceClassCachePerOrgRefreshInterval)))
					{
						if (num >= GlobalSettings.DeviceClassCacheMaxADUploadCount)
						{
							AirSyncDiagnostics.TraceDebug<int>(ExTraceGlobals.RequestsTracer, this, "1. Stop updating AD because the cap is reached: adUpdateCount={0}", num);
							break;
						}
						lock (this.thisLock)
						{
							if (!this.cache.ContainsKey(deviceClassDataSet.OrganizationId))
							{
								AirSyncDiagnostics.TraceDebug<OrganizationId>(ExTraceGlobals.RequestsTracer, this, "Organization {0} is already removed from the cache by another thread", deviceClassDataSet.OrganizationId);
								continue;
							}
							this.cache.Remove(deviceClassDataSet.OrganizationId);
						}
						using (deviceClassDataSet)
						{
							IConfigurationSession scopedSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(deviceClassDataSet.OrganizationId), 930, "Refresh", "f:\\15.00.1497\\sources\\dev\\AirSync\\src\\AirSync\\DeviceClassCache.cs");
							ActiveSyncDeviceClasses deviceClassContainer = DeviceClassCache.GetActiveSyncDeviceClassContainer(scopedSession, deviceClassDataSet.OrganizationId);
							if (deviceClassContainer != null)
							{
								ADPagedReader<ActiveSyncDeviceClass> deviceClassReader = null;
								ADNotificationAdapter.RunADOperation(delegate()
								{
									deviceClassReader = scopedSession.FindPaged<ActiveSyncDeviceClass>(deviceClassContainer.Id, QueryScope.OneLevel, null, null, 0);
								});
								DeviceClassCache.UpdateProtocolLogLastUsedDC(scopedSession);
								if (deviceClassReader != null)
								{
									using (DeviceClassCache.DeviceClassDataSet deviceClassDataSet3 = new DeviceClassCache.DeviceClassDataSet(deviceClassDataSet.OrganizationId))
									{
										foreach (ActiveSyncDeviceClass activeSyncDeviceClass in deviceClassReader)
										{
											if (!string.IsNullOrEmpty(activeSyncDeviceClass.DeviceType) && !string.IsNullOrEmpty(activeSyncDeviceClass.DeviceModel) && activeSyncDeviceClass.LastUpdateTime != null)
											{
												string commonName = ActiveSyncDeviceClass.GetCommonName(activeSyncDeviceClass.DeviceType, activeSyncDeviceClass.DeviceModel);
												if (DeviceClassCache.DnIsMangled(activeSyncDeviceClass, commonName))
												{
													AirSyncDiagnostics.TraceDebug<ADObjectId>(ExTraceGlobals.RequestsTracer, this, "Delete the Mangled DeviceClassCache {0}.", activeSyncDeviceClass.Id);
													DeviceClassCache.DeleteObject(scopedSession, activeSyncDeviceClass);
													num++;
												}
												else
												{
													deviceClassDataSet3.Add(new DeviceClassCache.DeviceClassData(activeSyncDeviceClass));
												}
											}
											else
											{
												AirSyncDiagnostics.TraceDebug<string, string, DateTime?>(ExTraceGlobals.RequestsTracer, this, "Delete the DeviceClassCache. Either DeviceType, DeviceModel or LastupdatTime is null. DeviceType:{0}, DeviceModel:{1}, LastUpdateTime:{2}.", activeSyncDeviceClass.DeviceType, activeSyncDeviceClass.DeviceModel, activeSyncDeviceClass.LastUpdateTime);
												DeviceClassCache.DeleteObject(scopedSession, activeSyncDeviceClass);
												num++;
											}
										}
										DeviceClassCache.UpdateProtocolLogLastUsedDC(scopedSession);
										AirSyncDiagnostics.TraceDebug<int, OrganizationId>(ExTraceGlobals.RequestsTracer, this, "'{0}' device classes are loaded from AD for org '{1}'", deviceClassDataSet3.Count, deviceClassDataSet.OrganizationId);
										int perOrgDeleteCount = 0;
										DeviceClassCache.ProcessForADCleanup(scopedSession, deviceClassDataSet, deviceClassDataSet3, ref num, ref perOrgDeleteCount);
										DeviceClassCache.ProcessForADAdds(scopedSession, deviceClassDataSet, deviceClassDataSet3, ref num, perOrgDeleteCount);
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				AirSyncUtility.ProcessException(ex);
			}
		}

		// Token: 0x040003DE RID: 990
		private static readonly int ADCapWarningThreshold = GlobalSettings.DeviceClassPerOrgMaxADCount * 9 / 10;

		// Token: 0x040003DF RID: 991
		private static readonly int TimerKickinInterval = (GlobalSettings.DeviceClassCachePerOrgRefreshInterval / 10 > 0) ? (GlobalSettings.DeviceClassCachePerOrgRefreshInterval / 10) : 1;

		// Token: 0x040003E0 RID: 992
		private static readonly SortBy deviceClassesSortOrder = new SortBy(ADObjectSchema.WhenCreated, SortOrder.Ascending);

		// Token: 0x040003E1 RID: 993
		private static readonly ComparisonFilter deviceClassesFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, "ExchangeDeviceClasses");

		// Token: 0x040003E2 RID: 994
		private static InternDataPool<string> internStringPool = new InternDataPool<string>(100);

		// Token: 0x040003E3 RID: 995
		private static DeviceClassCache instance = new DeviceClassCache();

		// Token: 0x040003E4 RID: 996
		private object thisLock = new object();

		// Token: 0x040003E5 RID: 997
		private Dictionary<OrganizationId, DeviceClassCache.DeviceClassDataSet> cache;

		// Token: 0x040003E6 RID: 998
		private ExDateTime timerStartTime;

		// Token: 0x040003E7 RID: 999
		private GuardedTimer refreshTimer;

		// Token: 0x040003E8 RID: 1000
		private bool realTimeRefresh;

		// Token: 0x02000066 RID: 102
		private struct ADPropertyConstraintLength
		{
			// Token: 0x1700022A RID: 554
			// (get) Token: 0x0600059B RID: 1435 RVA: 0x00021BCF File Offset: 0x0001FDCF
			public static int MaxDeviceModelLength
			{
				get
				{
					if (DeviceClassCache.ADPropertyConstraintLength.maxDeviceModelLength == -1)
					{
						DeviceClassCache.ADPropertyConstraintLength.maxDeviceModelLength = MobileDevice.GetStringConstraintLength(ActiveSyncDeviceClass.StaticSchema, ActiveSyncDeviceClassSchema.DeviceModel);
					}
					return DeviceClassCache.ADPropertyConstraintLength.maxDeviceModelLength;
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x0600059C RID: 1436 RVA: 0x00021BF2 File Offset: 0x0001FDF2
			public static int MaxDeviceTypeLength
			{
				get
				{
					if (DeviceClassCache.ADPropertyConstraintLength.maxDeviceTypeLength == -1)
					{
						DeviceClassCache.ADPropertyConstraintLength.maxDeviceTypeLength = MobileDevice.GetStringConstraintLength(ActiveSyncDeviceClass.StaticSchema, ActiveSyncDeviceClassSchema.DeviceType);
					}
					return DeviceClassCache.ADPropertyConstraintLength.maxDeviceTypeLength;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x0600059D RID: 1437 RVA: 0x00021C15 File Offset: 0x0001FE15
			public static int MaxDeviceClassNameLength
			{
				get
				{
					if (DeviceClassCache.ADPropertyConstraintLength.maxDeviceClassNameLength == -1)
					{
						DeviceClassCache.ADPropertyConstraintLength.maxDeviceClassNameLength = MobileDevice.GetStringConstraintLength(ActiveSyncDeviceClass.StaticSchema, ADObjectSchema.Name);
					}
					return DeviceClassCache.ADPropertyConstraintLength.maxDeviceClassNameLength;
				}
			}

			// Token: 0x040003E9 RID: 1001
			private static int maxDeviceModelLength = -1;

			// Token: 0x040003EA RID: 1002
			private static int maxDeviceTypeLength = -1;

			// Token: 0x040003EB RID: 1003
			private static int maxDeviceClassNameLength = -1;
		}

		// Token: 0x02000067 RID: 103
		private struct DeviceClassData : IComparable<DeviceClassCache.DeviceClassData>
		{
			// Token: 0x0600059F RID: 1439 RVA: 0x00021C4C File Offset: 0x0001FE4C
			public DeviceClassData(string deviceType, string deviceModel)
			{
				this = new DeviceClassCache.DeviceClassData(deviceType, deviceModel, ExDateTime.UtcNow);
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00021C5C File Offset: 0x0001FE5C
			public DeviceClassData(ActiveSyncDeviceClass deviceClassFromAD)
			{
				this = new DeviceClassCache.DeviceClassData(deviceClassFromAD.DeviceType, deviceClassFromAD.DeviceModel, new ExDateTime(ExTimeZone.UtcTimeZone, deviceClassFromAD.LastUpdateTime.Value));
				this.deviceClassFromAD = deviceClassFromAD;
			}

			// Token: 0x060005A1 RID: 1441 RVA: 0x00021C9A File Offset: 0x0001FE9A
			public DeviceClassData(string deviceType, string deviceModel, ExDateTime lastUpdateTime)
			{
				this.deviceType = DeviceClassCache.internStringPool.Intern(deviceType);
				this.deviceModel = DeviceClassCache.internStringPool.Intern(deviceModel);
				this.lastUpdateTime = lastUpdateTime;
				this.deviceClassFromAD = null;
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00021CCC File Offset: 0x0001FECC
			public string DeviceType
			{
				get
				{
					return this.deviceType;
				}
			}

			// Token: 0x1700022E RID: 558
			// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00021CD4 File Offset: 0x0001FED4
			public string DeviceModel
			{
				get
				{
					return this.deviceModel;
				}
			}

			// Token: 0x1700022F RID: 559
			// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00021CDC File Offset: 0x0001FEDC
			// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00021CE4 File Offset: 0x0001FEE4
			public ExDateTime LastUpdateTime
			{
				get
				{
					return this.lastUpdateTime;
				}
				set
				{
					this.lastUpdateTime = value;
				}
			}

			// Token: 0x17000230 RID: 560
			// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00021CED File Offset: 0x0001FEED
			public ActiveSyncDeviceClass DeviceClassFromAD
			{
				get
				{
					return this.deviceClassFromAD;
				}
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x00021CF8 File Offset: 0x0001FEF8
			public ActiveSyncDeviceClass ToActiveSyncDeviceClass(IConfigurationSession scopedSession, OrganizationId orgId)
			{
				if (this.DeviceClassFromAD != null)
				{
					this.DeviceClassFromAD.DeviceType = this.DeviceType;
					this.DeviceClassFromAD.DeviceModel = this.DeviceModel;
					this.DeviceClassFromAD.LastUpdateTime = new DateTime?((DateTime)this.LastUpdateTime);
					return this.DeviceClassFromAD;
				}
				ActiveSyncDeviceClass activeSyncDeviceClass = new ActiveSyncDeviceClass
				{
					DeviceType = this.DeviceType,
					DeviceModel = this.DeviceModel,
					LastUpdateTime = new DateTime?((DateTime)this.LastUpdateTime),
					OrganizationId = orgId
				};
				activeSyncDeviceClass.Name = DeviceClassCache.EnforceLengthLimit(activeSyncDeviceClass.GetCommonName(), DeviceClassCache.ADPropertyConstraintLength.MaxDeviceClassNameLength, true);
				activeSyncDeviceClass.SetId(scopedSession, activeSyncDeviceClass.Name);
				return activeSyncDeviceClass;
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00021DB4 File Offset: 0x0001FFB4
			public override int GetHashCode()
			{
				int hashCode = this.deviceType.GetHashCode();
				int hashCode2 = this.deviceModel.GetHashCode();
				if (hashCode == hashCode2)
				{
					return hashCode;
				}
				return hashCode ^ hashCode2;
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x00021DE4 File Offset: 0x0001FFE4
			public override bool Equals(object obj)
			{
				DeviceClassCache.DeviceClassData deviceClassData = (DeviceClassCache.DeviceClassData)obj;
				return this.DeviceType == deviceClassData.DeviceType && this.DeviceModel == deviceClassData.DeviceModel;
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00021E20 File Offset: 0x00020020
			public int CompareTo(DeviceClassCache.DeviceClassData other)
			{
				int num = this.DeviceType.CompareTo(other.DeviceType);
				if (num == 0)
				{
					num = this.DeviceModel.CompareTo(other.DeviceModel);
				}
				return num;
			}

			// Token: 0x040003EC RID: 1004
			private string deviceType;

			// Token: 0x040003ED RID: 1005
			private string deviceModel;

			// Token: 0x040003EE RID: 1006
			private ExDateTime lastUpdateTime;

			// Token: 0x040003EF RID: 1007
			private ActiveSyncDeviceClass deviceClassFromAD;
		}

		// Token: 0x02000068 RID: 104
		private class DeviceClassDataSet : DisposeTrackableBase, IEnumerable<DeviceClassCache.DeviceClassData>, IEnumerable
		{
			// Token: 0x060005AB RID: 1451 RVA: 0x00021E57 File Offset: 0x00020057
			public DeviceClassDataSet(OrganizationId orgId)
			{
				this.OrganizationId = orgId;
				this.StartTime = ExDateTime.UtcNow;
				this.cache = new HashSet<DeviceClassCache.DeviceClassData>();
			}

			// Token: 0x17000231 RID: 561
			// (get) Token: 0x060005AC RID: 1452 RVA: 0x00021E7C File Offset: 0x0002007C
			// (set) Token: 0x060005AD RID: 1453 RVA: 0x00021E84 File Offset: 0x00020084
			public ExDateTime StartTime { get; private set; }

			// Token: 0x17000232 RID: 562
			// (get) Token: 0x060005AE RID: 1454 RVA: 0x00021E8D File Offset: 0x0002008D
			// (set) Token: 0x060005AF RID: 1455 RVA: 0x00021E95 File Offset: 0x00020095
			public OrganizationId OrganizationId { get; private set; }

			// Token: 0x17000233 RID: 563
			// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00021E9E File Offset: 0x0002009E
			public int Count
			{
				get
				{
					return this.cache.Count;
				}
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x00021EAB File Offset: 0x000200AB
			public bool Add(DeviceClassCache.DeviceClassData data)
			{
				return this.cache.Add(data);
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x00021EB9 File Offset: 0x000200B9
			public bool Contains(DeviceClassCache.DeviceClassData data)
			{
				return this.cache.Contains(data);
			}

			// Token: 0x060005B3 RID: 1459 RVA: 0x00021EC7 File Offset: 0x000200C7
			public IEnumerator<DeviceClassCache.DeviceClassData> GetEnumerator()
			{
				return this.cache.GetEnumerator();
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x00021ED9 File Offset: 0x000200D9
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x00021EE4 File Offset: 0x000200E4
			protected override void InternalDispose(bool isDisposing)
			{
				AirSyncDiagnostics.TraceDebug<OrganizationId>(ExTraceGlobals.RequestsTracer, this, "Disposing DeviceClassDataSet : orgId={0}", this.OrganizationId);
				if (isDisposing)
				{
					foreach (DeviceClassCache.DeviceClassData deviceClassData in this.cache)
					{
						DeviceClassCache.internStringPool.Release(deviceClassData.DeviceType);
						DeviceClassCache.internStringPool.Release(deviceClassData.DeviceModel);
					}
					this.cache.Clear();
				}
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x00021F78 File Offset: 0x00020178
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<DeviceClassCache.DeviceClassDataSet>(this);
			}

			// Token: 0x040003F0 RID: 1008
			private HashSet<DeviceClassCache.DeviceClassData> cache;
		}
	}
}
