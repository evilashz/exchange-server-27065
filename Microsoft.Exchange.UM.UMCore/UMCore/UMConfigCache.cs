using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Exchange.Audio;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.Prompts.Provisioning;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000207 RID: 519
	internal class UMConfigCache
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x00044524 File Offset: 0x00042724
		internal string GetPrompt<T>(T config, string fileName) where T : ADConfigurationObject
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.GetPrompt {0}, {1}", new object[]
			{
				config,
				fileName
			});
			string text = null;
			if (!string.IsNullOrEmpty(fileName))
			{
				UMConfigCache.CacheEntry entry = this.GetEntry<T>(config.Id, config.OrganizationId);
				if (!entry.PromptsDownloaded)
				{
					entry.InitializeSessionCache();
				}
				text = entry.GetPrompt(fileName).FullName;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.GetPrompt returns {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000445BC File Offset: 0x000427BC
		internal string CheckIfFileExists<T>(T config, string fileName) where T : ADConfigurationObject
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.CheckIfFileExists {0}, {1}", new object[]
			{
				config,
				fileName
			});
			if (string.IsNullOrEmpty(fileName))
			{
				return null;
			}
			string text = null;
			try
			{
				text = this.GetPrompt<T>(config, fileName);
			}
			catch (FileNotFoundException)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.CheckIfFileExists - File presently not in PCMCache", new object[0]);
			}
			if (text == null)
			{
				UMConfigCache.CacheEntry entry = this.GetEntry<T>(config.Id, config.OrganizationId);
				text = entry.GetPossiblyUnReferencedPromptIntoCache(fileName).FullName;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.CheckIfFileExists returns {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0004467C File Offset: 0x0004287C
		internal void SetPrompt<T>(T config, string sourcePath, string selectedFileName) where T : ADConfigurationObject
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.SetPrompt {0}, {1}, {2}", new object[]
			{
				config,
				sourcePath,
				selectedFileName
			});
			UMConfigCache.CacheEntry entry = this.GetEntry<T>(config.Id, config.OrganizationId);
			entry.SetPrompt(sourcePath, selectedFileName);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000446DC File Offset: 0x000428DC
		internal T Find<T>(ADObjectId objectId, OrganizationId orgId) where T : ADConfigurationObject
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.Find '{0}' '{1}'", new object[]
			{
				objectId,
				orgId
			});
			T result = default(T);
			UMConfigCache.CacheEntry entry = this.GetEntry<T>(objectId, orgId);
			if (entry != null)
			{
				result = (T)((object)entry.Config);
			}
			return result;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0004472C File Offset: 0x0004292C
		private UMConfigCache.CacheEntry GetEntry<T>(ADObjectId key, OrganizationId orgId) where T : ADConfigurationObject
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "UMConfigCache.GetEntry '{0}' '{1}'", new object[]
			{
				key,
				orgId
			});
			UMConfigCache.CacheEntry cacheEntry;
			if (this.hashTable.ContainsKey(key))
			{
				cacheEntry = this.hashTable[key];
			}
			else if (typeof(T) == typeof(UMDialPlan))
			{
				cacheEntry = new UMConfigCache.UMDialPlanCacheEntry(key, orgId);
			}
			else
			{
				if (!(typeof(T) == typeof(UMAutoAttendant)))
				{
					throw new ArgumentException("key");
				}
				cacheEntry = new UMConfigCache.UMAutoAttendantCacheEntry(key, orgId);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Cache entry for '{0}' Found? = '{1}'", new object[]
			{
				key,
				cacheEntry.Config != null
			});
			UMConfigCache.CacheEntry result = null;
			if (cacheEntry.Config != null)
			{
				this.hashTable[key] = cacheEntry;
				result = cacheEntry;
			}
			return result;
		}

		// Token: 0x04000B3C RID: 2876
		private Dictionary<ADObjectId, UMConfigCache.CacheEntry> hashTable = new Dictionary<ADObjectId, UMConfigCache.CacheEntry>();

		// Token: 0x02000208 RID: 520
		private abstract class CacheEntry
		{
			// Token: 0x06000F30 RID: 3888 RVA: 0x0004482C File Offset: 0x00042A2C
			internal CacheEntry(ADObjectId key, OrganizationId orgId)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Constructing CacheEntry key='{0}', '{1}'", new object[]
				{
					key,
					orgId
				});
				this.config = this.ReadConfiguration(key, orgId);
			}

			// Token: 0x06000F31 RID: 3889 RVA: 0x00044870 File Offset: 0x00042A70
			internal void InitializeSessionCache()
			{
				if (this.config != null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Constructing CacheEntry config != null", new object[0]);
					bool flag = true;
					Exception ex = null;
					this.missingPromptList = new List<string>();
					FileStream fileStream = null;
					try
					{
						this.pcmCache = TempFileFactory.CreateTempDir();
						fileStream = this.EnsurePersistedCache();
						bool flag2 = null != fileStream;
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheEntry , usePersistedCache ={0}", new object[]
						{
							flag2
						});
						if (!flag2)
						{
							this.EnsureSessionCache();
						}
						if (!this.ApplyConfiguration(false))
						{
							string missingPromptsString = this.GetMissingPromptsString();
							if (!string.IsNullOrEmpty(missingPromptsString))
							{
								this.LogMissingPromptsEvent(missingPromptsString);
								flag = false;
								ex = null;
							}
						}
						if (!flag2)
						{
							this.PersistSessionCache();
						}
					}
					catch (PublishingPointException ex2)
					{
						flag = false;
						ex = ex2;
					}
					catch (IOException ex3)
					{
						flag = false;
						ex = ex3;
					}
					finally
					{
						if (fileStream != null)
						{
							fileStream.Dispose();
						}
					}
					if (!flag)
					{
						this.LogInvalidConfiguration(this.config.Id, ex);
						throw CallRejectedException.Create(Strings.ObjectPromptsNotConsistent(this.Config.Name), ex, CallEndingReason.ObjectPromptsNotConsistent, "Object: {0}.", new object[]
						{
							this.Config.Name
						});
					}
				}
			}

			// Token: 0x170003B0 RID: 944
			// (get) Token: 0x06000F32 RID: 3890 RVA: 0x000449BC File Offset: 0x00042BBC
			internal virtual ADConfigurationObject Config
			{
				get
				{
					return this.config;
				}
			}

			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000449C4 File Offset: 0x00042BC4
			internal bool PromptsDownloaded
			{
				get
				{
					return this.sessionCache != null;
				}
			}

			// Token: 0x170003B2 RID: 946
			// (get) Token: 0x06000F34 RID: 3892 RVA: 0x000449D4 File Offset: 0x00042BD4
			protected string CacheRootPath
			{
				get
				{
					string path = Path.Combine(GlobCfg.ExchangeDirectory, "UnifiedMessaging\\Prompts\\Cache");
					return Path.Combine(path, this.Config.Guid.ToString());
				}
			}

			// Token: 0x170003B3 RID: 947
			// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00044A14 File Offset: 0x00042C14
			protected string PersistedCachePath
			{
				get
				{
					return Path.Combine(this.CacheRootPath, this.ChangeKey);
				}
			}

			// Token: 0x170003B4 RID: 948
			// (get) Token: 0x06000F36 RID: 3894
			protected abstract string ChangeKey { get; }

			// Token: 0x170003B5 RID: 949
			// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00044A34 File Offset: 0x00042C34
			protected ITempFile PcmCache
			{
				get
				{
					return this.pcmCache;
				}
			}

			// Token: 0x06000F38 RID: 3896 RVA: 0x00044A3C File Offset: 0x00042C3C
			internal FileInfo GetPrompt(string fileName)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheEntry.GetPrompt filename='{0}'", new object[]
				{
					fileName
				});
				string text = Path.Combine(this.PcmCache.FilePath, fileName + ".wav");
				FileInfo fileInfo = new FileInfo(text);
				if (!fileInfo.Exists)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Did not find file '{0}' at path '{1}'", new object[]
					{
						fileName,
						text
					});
					throw CallRejectedException.Create(Strings.ObjectPromptsNotConsistent(this.Config.Name), null, CallEndingReason.ObjectPromptsNotConsistent, "Object: {0}.", new object[]
					{
						this.Config.Name
					});
				}
				return fileInfo;
			}

			// Token: 0x06000F39 RID: 3897 RVA: 0x00044AEC File Offset: 0x00042CEC
			internal FileInfo GetPossiblyUnReferencedPromptIntoCache(string fileName)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheEntry.GetPossiblyUnReferencedPromptIntoCache filename='{0}'", new object[]
				{
					fileName
				});
				this.ProcessPrompt(fileName, false);
				string text = Path.Combine(this.PcmCache.FilePath, fileName + ".wav");
				FileInfo fileInfo = new FileInfo(text);
				if (!fileInfo.Exists)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Did not find file '{0}' at path '{1}'", new object[]
					{
						fileName,
						text
					});
					throw CallRejectedException.Create(Strings.ObjectPromptsNotConsistent(this.Config.Name), null, CallEndingReason.ObjectPromptsNotConsistent, "Object: {0}.", new object[]
					{
						this.Config.Name
					});
				}
				return fileInfo;
			}

			// Token: 0x06000F3A RID: 3898 RVA: 0x00044BA4 File Offset: 0x00042DA4
			internal void SetPrompt(string sourcePath, string selectedFileName)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheEntry.SetPrompt sourcePath='{0}', selectedFileName='{1}'", new object[]
				{
					sourcePath,
					selectedFileName
				});
				string text = Path.Combine(this.PcmCache.FilePath, selectedFileName + ".wav");
				File.Copy(sourcePath, text, true);
				TempFileFactory.AddNetworkServiceReadAccess(text, false);
			}

			// Token: 0x06000F3B RID: 3899
			protected abstract void LogMissingPromptsEvent(string missing);

			// Token: 0x06000F3C RID: 3900
			protected abstract ADConfigurationObject ReadConfiguration(ADObjectId objectId, OrganizationId orgId);

			// Token: 0x06000F3D RID: 3901
			protected abstract bool ApplyConfiguration(bool whatif);

			// Token: 0x06000F3E RID: 3902
			protected abstract void LogInvalidConfiguration(ADObjectId id, Exception e);

			// Token: 0x06000F3F RID: 3903
			protected abstract void LogCacheUpdateEvent();

			// Token: 0x06000F40 RID: 3904 RVA: 0x00044BFC File Offset: 0x00042DFC
			protected bool ProcessPrompt(string fileName)
			{
				return this.ProcessPrompt(fileName, true);
			}

			// Token: 0x06000F41 RID: 3905 RVA: 0x00044C08 File Offset: 0x00042E08
			protected string GetMissingPromptsString()
			{
				string result = null;
				if (this.missingPromptList.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine();
					for (int i = 0; i < this.missingPromptList.Count; i++)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append(this.missingPromptList[i]);
					}
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
					result = stringBuilder.ToString();
				}
				return result;
			}

			// Token: 0x06000F42 RID: 3906 RVA: 0x00044C78 File Offset: 0x00042E78
			private bool ProcessPrompt(string fileName, bool logMissingPrompts)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheEntry.ProcessPrompt({0}).", new object[]
				{
					fileName
				});
				bool flag = false;
				Exception ex = null;
				try
				{
					if (string.IsNullOrEmpty(fileName))
					{
						flag = true;
					}
					else
					{
						string path = fileName + ".wma";
						string text = Path.Combine(this.sessionCache.FullName, path);
						if (File.Exists(text))
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "ProcessPrompt cacheFilePath({0}) exists.", new object[]
							{
								text
							});
							string text2 = Path.Combine(this.PcmCache.FilePath, fileName + ".wav");
							if (File.Exists(text2))
							{
								flag = true;
							}
							else
							{
								CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Extracting source='{0}' to destination='{1}'", new object[]
								{
									text,
									text2
								});
								using (PcmWriter pcmWriter = new PcmWriter(text2, WaveFormat.Pcm8WaveFormat))
								{
									using (WmaReader wmaReader = new WmaReader(text))
									{
										byte[] array = new byte[wmaReader.SampleSize * 2];
										int count;
										while ((count = wmaReader.Read(array, array.Length)) > 0)
										{
											pcmWriter.Write(array, count);
										}
									}
								}
								TempFileFactory.AddNetworkServiceReadAccess(text2, false);
								flag = true;
							}
						}
						else
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "ProcessPrompt cacheFilePath({0}) doesnt exists.", new object[]
							{
								text
							});
						}
					}
				}
				catch (InvalidWmaFormatException ex2)
				{
					ex = ex2;
				}
				catch (COMException ex3)
				{
					ex = ex3;
				}
				catch (IOException ex4)
				{
					ex = ex4;
				}
				if (!flag && logMissingPrompts)
				{
					this.missingPromptList.Add(fileName);
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "ProcessPrompt ret='{0}', error='{1}'.", new object[]
				{
					flag,
					ex
				});
				return flag;
			}

			// Token: 0x06000F43 RID: 3907 RVA: 0x00044EB0 File Offset: 0x000430B0
			private FileStream EnsurePersistedCache()
			{
				FileStream fileStream = this.LockPersistedCache();
				if (fileStream != null)
				{
					this.sessionCache = new DirectoryInfo(this.PersistedCachePath);
					if (!this.ApplyConfiguration(true))
					{
						fileStream.Dispose();
						fileStream = null;
						this.sessionCache.Delete(true);
						this.sessionCache = null;
					}
					else
					{
						CacheCleaner.TouchUpDirectory(this.sessionCache);
					}
				}
				return fileStream;
			}

			// Token: 0x06000F44 RID: 3908 RVA: 0x00044F0C File Offset: 0x0004310C
			private void EnsureSessionCache()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheEntry.EnsureSessionCache", new object[0]);
				this.sessionCache = new DirectoryInfo(Path.Combine(this.CacheRootPath, Guid.NewGuid().ToString()));
				Directory.CreateDirectory(this.sessionCache.FullName);
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Downloading prompts to {0}", new object[]
				{
					this.sessionCache
				});
				if (!this.ApplyConfiguration(true))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "executing DownloadAllAsWma for {0}", new object[]
					{
						this.Config.Name
					});
					using (IPublishingSession publishingSession = PublishingPoint.GetPublishingSession("UM", this.config))
					{
						publishingSession.DownloadAllAsWma(this.sessionCache);
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "completed DownloadAllAsWma for {0}", new object[]
					{
						this.Config.Name
					});
				}
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x0004501C File Offset: 0x0004321C
			private FileStream LockPersistedCache()
			{
				FileStream result = null;
				if (Directory.Exists(this.PersistedCachePath))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "LockPersistedCache , directory {0} exists", new object[]
					{
						this.PersistedCachePath
					});
					try
					{
						result = new FileStream(Path.Combine(this.PersistedCachePath, Guid.NewGuid().ToString()), FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8, FileOptions.DeleteOnClose);
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "LockPersistedCache , was able to open filestream", new object[0]);
					}
					catch (DirectoryNotFoundException ex)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CreateDirectoryFileLock exception='{0}'", new object[]
						{
							ex
						});
					}
				}
				return result;
			}

			// Token: 0x06000F46 RID: 3910 RVA: 0x000450D4 File Offset: 0x000432D4
			private void PersistSessionCache()
			{
				try
				{
					Directory.Move(this.sessionCache.FullName, this.PersistedCachePath);
					this.sessionCache = new DirectoryInfo(this.PersistedCachePath);
				}
				catch (IOException ex)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Unable to persist session cache e='{0}'", new object[]
					{
						ex
					});
				}
			}

			// Token: 0x04000B3D RID: 2877
			private ADConfigurationObject config;

			// Token: 0x04000B3E RID: 2878
			private ITempFile pcmCache;

			// Token: 0x04000B3F RID: 2879
			private List<string> missingPromptList;

			// Token: 0x04000B40 RID: 2880
			private DirectoryInfo sessionCache;
		}

		// Token: 0x02000209 RID: 521
		private class UMAutoAttendantCacheEntry : UMConfigCache.CacheEntry
		{
			// Token: 0x06000F47 RID: 3911 RVA: 0x0004513C File Offset: 0x0004333C
			internal UMAutoAttendantCacheEntry(ADObjectId key, OrganizationId orgId) : base(key, orgId)
			{
			}

			// Token: 0x170003B6 RID: 950
			// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00045148 File Offset: 0x00043348
			protected override string ChangeKey
			{
				get
				{
					return this.AutoAttendantConfig.PromptChangeKey.ToString("N");
				}
			}

			// Token: 0x170003B7 RID: 951
			// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0004516D File Offset: 0x0004336D
			private UMAutoAttendant AutoAttendantConfig
			{
				get
				{
					return (UMAutoAttendant)this.Config;
				}
			}

			// Token: 0x06000F4A RID: 3914 RVA: 0x0004517C File Offset: 0x0004337C
			protected override void LogMissingPromptsEvent(string missing)
			{
				if (!string.IsNullOrEmpty(missing))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AACustomPromptFileMissing, null, new object[]
					{
						this.Config.Id,
						CommonUtil.ToEventLogString(missing)
					});
				}
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x000451C4 File Offset: 0x000433C4
			protected override ADConfigurationObject ReadConfiguration(ADObjectId objectId, OrganizationId orgId)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "AACacheEntry.ReadConfiguration('{0}', '{1}').", new object[]
				{
					objectId,
					orgId
				});
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(orgId, false);
				return iadsystemConfigurationLookup.GetAutoAttendantFromId(objectId);
			}

			// Token: 0x06000F4C RID: 3916 RVA: 0x00045200 File Offset: 0x00043400
			protected override bool ApplyConfiguration(bool whatif)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "AACacheEntry.ApplyConfiguration().  Whatif={0}", new object[]
				{
					whatif
				});
				bool flag = true;
				flag &= base.ProcessPrompt(this.AutoAttendantConfig.AfterHoursMainMenuCustomPromptFilename);
				flag &= base.ProcessPrompt(this.AutoAttendantConfig.AfterHoursWelcomeGreetingFilename);
				flag &= base.ProcessPrompt(this.AutoAttendantConfig.BusinessHoursMainMenuCustomPromptFilename);
				flag &= base.ProcessPrompt(this.AutoAttendantConfig.BusinessHoursWelcomeGreetingFilename);
				flag &= base.ProcessPrompt(this.AutoAttendantConfig.InfoAnnouncementFilename);
				flag &= this.ProcessHolidaySchedules(this.AutoAttendantConfig);
				flag &= this.ProcessAfterHourKeyMappings(this.AutoAttendantConfig);
				flag &= this.ProcessBusinessHoursKeyMappings(this.AutoAttendantConfig);
				if (!whatif)
				{
					Util.IncrementCounter(AvailabilityCounters.PercentageCustomPromptDownloadFailures_Base, 1L);
					if (!flag)
					{
						Util.IncrementCounter(AvailabilityCounters.PercentageCustomPromptDownloadFailures, 1L);
					}
				}
				return flag;
			}

			// Token: 0x06000F4D RID: 3917 RVA: 0x000452E0 File Offset: 0x000434E0
			protected override void LogInvalidConfiguration(ADObjectId id, Exception e)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "AACacheEntry.LogInvalidConfiguration().", new object[0]);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AACustomPromptInvalid, null, new object[]
				{
					id,
					CommonUtil.ToEventLogString(e)
				});
			}

			// Token: 0x06000F4E RID: 3918 RVA: 0x0004532C File Offset: 0x0004352C
			protected override void LogCacheUpdateEvent()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_AutoAttendantCustomPromptCacheUpdate, null, new object[]
				{
					this.Config.Id
				});
			}

			// Token: 0x06000F4F RID: 3919 RVA: 0x00045360 File Offset: 0x00043560
			private bool ProcessHolidaySchedules(UMAutoAttendant newConfig)
			{
				bool result = true;
				if (newConfig.HolidaySchedule != null)
				{
					foreach (HolidaySchedule holidaySchedule in newConfig.HolidaySchedule)
					{
						if (!base.ProcessPrompt(holidaySchedule.Greeting))
						{
							result = false;
						}
					}
				}
				return result;
			}

			// Token: 0x06000F50 RID: 3920 RVA: 0x000453C8 File Offset: 0x000435C8
			private bool ProcessAfterHourKeyMappings(UMAutoAttendant newConfig)
			{
				bool result = true;
				if (newConfig.AfterHoursKeyMapping != null)
				{
					foreach (CustomMenuKeyMapping customMenuKeyMapping in newConfig.AfterHoursKeyMapping)
					{
						if (!base.ProcessPrompt(customMenuKeyMapping.PromptFileName))
						{
							result = false;
						}
					}
				}
				return result;
			}

			// Token: 0x06000F51 RID: 3921 RVA: 0x00045430 File Offset: 0x00043630
			private bool ProcessBusinessHoursKeyMappings(UMAutoAttendant newConfig)
			{
				bool result = true;
				if (newConfig.BusinessHoursKeyMapping != null)
				{
					foreach (CustomMenuKeyMapping customMenuKeyMapping in newConfig.BusinessHoursKeyMapping)
					{
						if (!base.ProcessPrompt(customMenuKeyMapping.PromptFileName))
						{
							result = false;
						}
					}
				}
				return result;
			}

			// Token: 0x04000B41 RID: 2881
			private static UMAutoAttendant defaultConfig = new UMAutoAttendant();
		}

		// Token: 0x0200020A RID: 522
		private class UMDialPlanCacheEntry : UMConfigCache.CacheEntry
		{
			// Token: 0x06000F53 RID: 3923 RVA: 0x000454A4 File Offset: 0x000436A4
			internal UMDialPlanCacheEntry(ADObjectId key, OrganizationId orgId) : base(key, orgId)
			{
			}

			// Token: 0x170003B8 RID: 952
			// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000454B0 File Offset: 0x000436B0
			protected override string ChangeKey
			{
				get
				{
					if (!string.IsNullOrEmpty(this.DialPlanConfig.PromptChangeKey))
					{
						return this.DialPlanConfig.PromptChangeKey;
					}
					return Guid.Empty.ToString("N");
				}
			}

			// Token: 0x170003B9 RID: 953
			// (get) Token: 0x06000F55 RID: 3925 RVA: 0x000454ED File Offset: 0x000436ED
			private UMDialPlan DialPlanConfig
			{
				get
				{
					return (UMDialPlan)this.Config;
				}
			}

			// Token: 0x06000F56 RID: 3926 RVA: 0x000454FC File Offset: 0x000436FC
			protected override void LogMissingPromptsEvent(string missing)
			{
				if (!string.IsNullOrEmpty(missing))
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DialPlanCustomPromptFileMissing, null, new object[]
					{
						this.Config.Id,
						CommonUtil.ToEventLogString(missing)
					});
				}
			}

			// Token: 0x06000F57 RID: 3927 RVA: 0x00045544 File Offset: 0x00043744
			protected override ADConfigurationObject ReadConfiguration(ADObjectId objectId, OrganizationId orgId)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "DPCacheEntry.ReadConfiguration('{0}', '{1}').", new object[]
				{
					objectId,
					orgId
				});
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(orgId, false);
				return iadsystemConfigurationLookup.GetDialPlanFromId(objectId);
			}

			// Token: 0x06000F58 RID: 3928 RVA: 0x00045580 File Offset: 0x00043780
			protected override bool ApplyConfiguration(bool whatif)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "DPCacheEntry.ApplyConfiguration().  Whatif={0}", new object[]
				{
					whatif
				});
				bool flag = true;
				flag &= base.ProcessPrompt(this.DialPlanConfig.WelcomeGreetingFilename);
				flag &= base.ProcessPrompt(this.DialPlanConfig.InfoAnnouncementFilename);
				if (!whatif)
				{
					Util.IncrementCounter(AvailabilityCounters.PercentageCustomPromptDownloadFailures_Base, 1L);
					if (!flag)
					{
						Util.IncrementCounter(AvailabilityCounters.PercentageCustomPromptDownloadFailures, 1L);
					}
				}
				return flag;
			}

			// Token: 0x06000F59 RID: 3929 RVA: 0x000455F8 File Offset: 0x000437F8
			protected override void LogCacheUpdateEvent()
			{
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DialPlanCustomPromptCacheUpdated, null, new object[]
				{
					this.Config.Id
				});
			}

			// Token: 0x06000F5A RID: 3930 RVA: 0x0004562C File Offset: 0x0004382C
			protected override void LogInvalidConfiguration(ADObjectId id, Exception e)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "DPCacheEntry.LogInvalidConfiguration().", new object[0]);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DialPlanCustomPromptInvalid, null, new object[]
				{
					id,
					CommonUtil.ToEventLogString(e)
				});
			}

			// Token: 0x04000B42 RID: 2882
			private static UMDialPlan defaultConfig = new UMDialPlan();
		}
	}
}
