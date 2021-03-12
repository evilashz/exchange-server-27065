using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200000F RID: 15
	internal class RegistryCrashRepository : ICrashRepository
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000428D File Offset: 0x0000248D
		public RegistryCrashRepository(string poisonRegistryEntryLocation, IStoreDriverTracer storeDriverTracer)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("poisonRegistryEntryLocation", poisonRegistryEntryLocation);
			ArgumentValidator.ThrowIfNull("storeDriverTracer", storeDriverTracer);
			this.poisonRegistryEntryLocation = poisonRegistryEntryLocation;
			this.storeDriverTracer = storeDriverTracer;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000042C9 File Offset: 0x000024C9
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000042D1 File Offset: 0x000024D1
		protected SortedSet<KeyValuePair<SubmissionPoisonContext, DateTime>> PurgablePoisonDataSet
		{
			get
			{
				return this.purgablePoisonDataSet;
			}
			set
			{
				this.purgablePoisonDataSet = value;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000042DC File Offset: 0x000024DC
		public List<Guid> GetAllResourceIDs()
		{
			List<Guid> list = new List<Guid>();
			List<Guid> result;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(this.poisonRegistryEntryLocation, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						foreach (string text in registryKey.GetSubKeyNames())
						{
							Guid item;
							if (!Guid.TryParse(text, out item))
							{
								this.storeDriverTracer.GeneralTracer.TraceFail<string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Invalid Resource Guid {0}. Deleting it.", text);
								registryKey.DeleteSubKeyTree(text);
							}
							else
							{
								list.Add(item);
							}
						}
					}
				}
				result = list;
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_PoisonMessageLoadFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new CrashRepositoryAccessException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000043C8 File Offset: 0x000025C8
		public bool GetQuarantineInfoContext(Guid resourceGuid, TimeSpan quarantineExpiryWindow, out QuarantineInfoContext quarantineInfoContext)
		{
			ArgumentValidator.ThrowIfEmpty("resourceGuid", resourceGuid);
			quarantineInfoContext = null;
			bool result;
			try
			{
				string text = resourceGuid.ToString();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(RegistryCrashRepository.resourceSubKeyFromRoot, this.poisonRegistryEntryLocation, text), RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					DateTime quarantineStartTime;
					if (registryKey != null && this.GetQuarantineStartTime(registryKey, text, quarantineExpiryWindow, out quarantineStartTime))
					{
						quarantineInfoContext = new QuarantineInfoContext(quarantineStartTime);
						return true;
					}
				}
				result = false;
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_PoisonMessageLoadFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new CrashRepositoryAccessException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000448C File Offset: 0x0000268C
		public bool GetResourceCrashInfoData(Guid resourceGuid, TimeSpan crashExpiryWindow, out Dictionary<long, ResourceEventCounterCrashInfo> resourceCrashData, out SortedSet<DateTime> allCrashTimes)
		{
			ArgumentValidator.ThrowIfEmpty("resourceGuid", resourceGuid);
			string text = resourceGuid.ToString();
			resourceCrashData = new Dictionary<long, ResourceEventCounterCrashInfo>();
			allCrashTimes = new SortedSet<DateTime>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(RegistryCrashRepository.poisonInfoKeyLocationFromRoot, this.poisonRegistryEntryLocation, text), RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						foreach (string text2 in registryKey.GetValueNames())
						{
							if (string.IsNullOrEmpty(text2))
							{
								registryKey.DeleteValue(text2, false);
							}
							else
							{
								ResourceEventCounterCrashInfo resourceEventCounterCrashInfo = null;
								long num;
								if (this.ProcessEventCounterData(text2, registryKey, text, out num))
								{
									string[] eventCounterCrashInfo = null;
									if (this.GetEventCounterCrashInfoValue(text2, registryKey, text, out eventCounterCrashInfo) && this.ProcessSingleEventCounterCrashInfoValue(eventCounterCrashInfo, text2, registryKey, text, crashExpiryWindow, out resourceEventCounterCrashInfo))
									{
										resourceCrashData.Add(num, resourceEventCounterCrashInfo);
										allCrashTimes.UnionWith(resourceEventCounterCrashInfo.CrashTimes);
										this.AddDataToPurgeDictionary(resourceGuid, num, resourceEventCounterCrashInfo.CrashTimes.Max);
									}
								}
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_PoisonMessageLoadFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new CrashRepositoryAccessException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
			return resourceCrashData.Count != 0;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000045E0 File Offset: 0x000027E0
		public void PersistCrashInfo(Guid resourceGuid, long eventCounter, ResourceEventCounterCrashInfo resourceEventCounterCrashInfo, int maxCrashEntries)
		{
			ArgumentValidator.ThrowIfEmpty("resourceGuid", resourceGuid);
			ArgumentValidator.ThrowIfNull("resourceEventCounterCrashInfo", resourceEventCounterCrashInfo);
			ArgumentValidator.ThrowIfZeroOrNegative("maxCrashEntries", maxCrashEntries);
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(string.Format(RegistryCrashRepository.poisonInfoKeyLocationFromRoot, this.poisonRegistryEntryLocation, resourceGuid)))
				{
					this.PersistCrashInfoToRegistry(registryKey, resourceGuid, eventCounter, resourceEventCounterCrashInfo, maxCrashEntries);
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_PoisonMessageSaveFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new CrashRepositoryAccessException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004694 File Offset: 0x00002894
		public bool PersistQuarantineInfo(Guid resourceGuid, QuarantineInfoContext quarantineInfoContext, bool overrideExisting = false)
		{
			ArgumentValidator.ThrowIfEmpty("resourceGuid", resourceGuid);
			ArgumentValidator.ThrowIfNull("quarantineInfoContext", quarantineInfoContext);
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(this.poisonRegistryEntryLocation + "\\" + resourceGuid.ToString()))
				{
					if (registryKey.GetValue("QuarantineStart") == null || overrideExisting)
					{
						registryKey.SetValue("QuarantineStart", quarantineInfoContext.QuarantineStartTime);
						return true;
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_PoisonMessageSaveFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new CrashRepositoryAccessException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
			return false;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004764 File Offset: 0x00002964
		public void PurgeResourceData(Guid resourceGuid)
		{
			ArgumentValidator.ThrowIfEmpty("resourceGuid", resourceGuid);
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(this.poisonRegistryEntryLocation, RegistryKeyPermissionCheck.ReadWriteSubTree))
				{
					if (registryKey != null)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey(resourceGuid.ToString()))
						{
							if (registryKey2 != null && registryKey2.GetValueNames().Length == 0)
							{
								registryKey.DeleteSubKeyTree(resourceGuid.ToString(), false);
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				StoreDriverSubmission.LogEvent(MSExchangeStoreDriverSubmissionEventLogConstants.Tuple_PoisonMessageSaveFailedRegistryAccessDenied, null, new object[]
				{
					ex.Message
				});
				throw new CrashRepositoryAccessException(Strings.PoisonMessageRegistryAccessFailed, ex);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004838 File Offset: 0x00002A38
		protected void PersistCrashInfoToRegistry(RegistryKey resourcePoisonKey, Guid resourceGuid, long eventCounter, ResourceEventCounterCrashInfo resourceEventCounterCrashInfo, int maxCrashEntries)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (DateTime dateTime in resourceEventCounterCrashInfo.CrashTimes)
			{
				stringBuilder.Append(dateTime + ";");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			string text = eventCounter.ToString();
			if (this.PurgeRegistryEntriesIfNecessary(resourceGuid.ToString(), text, maxCrashEntries))
			{
				this.AddDataToPurgeDictionary(resourceGuid, eventCounter, resourceEventCounterCrashInfo.CrashTimes.Max);
			}
			resourcePoisonKey.SetValue(text, new string[]
			{
				stringBuilder.ToString(),
				resourceEventCounterCrashInfo.IsPoisonNdrSent.ToString()
			}, RegistryValueKind.MultiString);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004920 File Offset: 0x00002B20
		protected bool PurgeRegistryEntriesIfNecessary(string resourceBeingAdded, string eventCounterString, int maxCrashEntries)
		{
			if (this.purgablePoisonDataSet.Count == 0 || this.purgablePoisonDataSet.Count < maxCrashEntries)
			{
				return false;
			}
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(RegistryCrashRepository.poisonInfoKeyLocationFromRoot, this.poisonRegistryEntryLocation, resourceBeingAdded)))
			{
				if (registryKey != null && registryKey.GetValue(eventCounterString) != null)
				{
					return false;
				}
			}
			KeyValuePair<SubmissionPoisonContext, DateTime> min = this.purgablePoisonDataSet.Min;
			using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(string.Format(RegistryCrashRepository.poisonInfoKeyLocationFromRoot, this.poisonRegistryEntryLocation, min.Key.ResourceGuid), RegistryKeyPermissionCheck.ReadWriteSubTree))
			{
				if (registryKey2 != null)
				{
					registryKey2.DeleteValue(min.Key.MapiEventCounter.ToString(), false);
					this.purgablePoisonDataSet.Remove(min);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004A1C File Offset: 0x00002C1C
		protected void AddDataToPurgeDictionary(Guid resourceGuid, long eventCounter, DateTime crashTime)
		{
			this.purgablePoisonDataSet.Add(new KeyValuePair<SubmissionPoisonContext, DateTime>(new SubmissionPoisonContext(resourceGuid, eventCounter), crashTime));
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004A37 File Offset: 0x00002C37
		protected bool ProcessEventCounterData(string eventCounterString, RegistryKey poisonKey, string resourceKeyString, out long eventCounter)
		{
			if (!long.TryParse(eventCounterString, out eventCounter))
			{
				this.storeDriverTracer.GeneralTracer.TraceFail<long, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Invalid event counter {0} in {1} registry key. Deleting it.", eventCounter, resourceKeyString);
				poisonKey.DeleteValue(eventCounterString, false);
				return false;
			}
			return true;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004A74 File Offset: 0x00002C74
		protected bool GetEventCounterCrashInfoValue(string eventCounterString, RegistryKey poisonKey, string resourceKeyString, out string[] eventCounterCrashInfo)
		{
			eventCounterCrashInfo = null;
			if (poisonKey.GetValueKind(eventCounterString) != RegistryValueKind.MultiString)
			{
				this.storeDriverTracer.GeneralTracer.TraceFail<string, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Event Counter Value is not multi string for event counter {0} inside resource {1}", eventCounterString, resourceKeyString);
				poisonKey.DeleteValue(eventCounterString, false);
				return false;
			}
			eventCounterCrashInfo = (string[])poisonKey.GetValue(eventCounterString);
			if (eventCounterCrashInfo.Length != 2)
			{
				this.storeDriverTracer.GeneralTracer.TraceFail<string[], string, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Invalid value {0} for event counter {1} in {2} registry key. Deleting it.", eventCounterCrashInfo, eventCounterString, resourceKeyString);
				poisonKey.DeleteValue(eventCounterString, false);
				eventCounterCrashInfo = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004B10 File Offset: 0x00002D10
		protected bool ProcessSingleEventCounterCrashInfoValue(string[] eventCounterCrashInfo, string eventCounterString, RegistryKey poisonKey, string resourceKeyString, TimeSpan crashExpiryWindow, out ResourceEventCounterCrashInfo resourceEventCounterCrashInfo)
		{
			ArgumentValidator.ThrowIfNull("eventCounterCrashInfo", eventCounterCrashInfo);
			ArgumentValidator.ThrowIfInvalidValue<int>("eventCounterCrashInfo", eventCounterCrashInfo.Length, (int length) => length == 2);
			resourceEventCounterCrashInfo = null;
			bool isPoisonNdrSent;
			SortedSet<DateTime> crashTimes;
			if (this.ParseSingleEventCounterNdrSentData(eventCounterCrashInfo[1], eventCounterString, poisonKey, resourceKeyString, out isPoisonNdrSent) && this.ProcessSingleEventCounterCrashTimeData(eventCounterCrashInfo[0], eventCounterString, poisonKey, resourceKeyString, crashExpiryWindow, out crashTimes))
			{
				resourceEventCounterCrashInfo = new ResourceEventCounterCrashInfo(crashTimes, isPoisonNdrSent);
				return true;
			}
			return false;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00004B87 File Offset: 0x00002D87
		protected bool ParseSingleEventCounterNdrSentData(string eventCounterCrashInfoNdrValue, string eventCounterString, RegistryKey poisonKey, string resourceKeyString, out bool isNdrSent)
		{
			if (!bool.TryParse(eventCounterCrashInfoNdrValue, out isNdrSent))
			{
				this.storeDriverTracer.GeneralTracer.TraceFail<string, string, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Invalid bool value {0} for event counter {1} in {2} registry key. Deleting it.", eventCounterCrashInfoNdrValue, eventCounterString, resourceKeyString);
				poisonKey.DeleteValue(eventCounterString, false);
				return false;
			}
			return true;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004BC4 File Offset: 0x00002DC4
		protected bool ProcessSingleEventCounterCrashTimeData(string eventCounterCrashInfoCrashTimes, string eventCounterString, RegistryKey poisonKey, string resourceKeyString, TimeSpan crashExpiryWindow, out SortedSet<DateTime> eventCounterCrashTimeSet)
		{
			eventCounterCrashTimeSet = null;
			string[] array = eventCounterCrashInfoCrashTimes.Split(";".ToCharArray());
			eventCounterCrashTimeSet = new SortedSet<DateTime>();
			foreach (string text in array)
			{
				DateTime dateTime;
				if (!DateTime.TryParse(text, out dateTime))
				{
					this.storeDriverTracer.GeneralTracer.TraceFail<string, string, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Invalid Crash Time value {0} for event counter {1} in {2} registry key. Deleting it.", text, eventCounterString, resourceKeyString);
					poisonKey.DeleteValue(eventCounterString, false);
					eventCounterCrashTimeSet = null;
					return false;
				}
				if (!this.IsCrashTimeExpired(dateTime, eventCounterString, resourceKeyString, crashExpiryWindow))
				{
					eventCounterCrashTimeSet.Add(dateTime);
				}
			}
			if (eventCounterCrashTimeSet.Count == 0)
			{
				eventCounterCrashTimeSet = null;
				poisonKey.DeleteValue(eventCounterString, false);
				return false;
			}
			return true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004C7D File Offset: 0x00002E7D
		protected bool IsCrashTimeExpired(DateTime crashTime, string eventCounterString, string resourceKeyString, TimeSpan crashExpiryWindow)
		{
			if (StoreDriverUtils.CheckIfDateTimeExceedsThreshold(crashTime, DateTime.UtcNow, crashExpiryWindow))
			{
				this.storeDriverTracer.GeneralTracer.TracePass<DateTime, string, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Crash time {0} has expired for Event Counter {1} inside resource {2}.", crashTime, eventCounterString, resourceKeyString);
				return true;
			}
			return false;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004CB8 File Offset: 0x00002EB8
		protected bool GetQuarantineStartTime(RegistryKey resourceKey, string resource, TimeSpan quarantineExpiryWindow, out DateTime quarantineStartTime)
		{
			quarantineStartTime = DateTime.MinValue;
			object value = resourceKey.GetValue("QuarantineStart");
			if (value == null)
			{
				resourceKey.DeleteValue("QuarantineStart", false);
				return false;
			}
			if (!DateTime.TryParse(value.ToString(), out quarantineStartTime))
			{
				this.storeDriverTracer.GeneralTracer.TraceFail<object, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Failed to parse start time of Quarantine specfied by string {0} for resource {1}. Will delete it", value, resource);
				resourceKey.DeleteValue("QuarantineStart", false);
				return false;
			}
			if (StoreDriverUtils.CheckIfDateTimeExceedsThreshold(quarantineStartTime, DateTime.UtcNow, quarantineExpiryWindow))
			{
				this.storeDriverTracer.GeneralTracer.TracePass<DateTime, TimeSpan, string>(this.storeDriverTracer.MessageProbeActivityId, 0L, "Date time {0} has expired when compared using TimeSpan {1} for resource {2}. Entry will be removed from registry", quarantineStartTime, quarantineExpiryWindow, resource);
				resourceKey.DeleteValue("QuarantineStart", false);
				return false;
			}
			return true;
		}

		// Token: 0x0400001D RID: 29
		private const string PoisonSubKeyName = "PoisonInfo";

		// Token: 0x0400001E RID: 30
		private const string CrashTimesSeparator = ";";

		// Token: 0x0400001F RID: 31
		private static string poisonInfoKeyLocationFromRoot = "{0}\\{1}\\PoisonInfo";

		// Token: 0x04000020 RID: 32
		private static string resourceSubKeyFromRoot = "{0}\\{1}";

		// Token: 0x04000021 RID: 33
		private readonly string poisonRegistryEntryLocation;

		// Token: 0x04000022 RID: 34
		private readonly IStoreDriverTracer storeDriverTracer;

		// Token: 0x04000023 RID: 35
		private SortedSet<KeyValuePair<SubmissionPoisonContext, DateTime>> purgablePoisonDataSet = new SortedSet<KeyValuePair<SubmissionPoisonContext, DateTime>>(new PoisonDataComparer());
	}
}
