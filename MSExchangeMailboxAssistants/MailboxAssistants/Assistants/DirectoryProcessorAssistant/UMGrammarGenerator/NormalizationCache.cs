using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using Microsoft.Exchange.Compliance.Serialization.Formatters;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001BE RID: 446
	internal class NormalizationCache
	{
		// Token: 0x06001150 RID: 4432 RVA: 0x000652C8 File Offset: 0x000634C8
		public NormalizationCache(CultureInfo culture, string fileNamePrefix, string grammarFolderPath, Logger logger, INormalizationCacheFileStore cacheFileStore)
		{
			ValidateArgument.NotNull(culture, "culture");
			ValidateArgument.NotNullOrEmpty(fileNamePrefix, "fileNamePrefix");
			ValidateArgument.NotNullOrEmpty(grammarFolderPath, "grammarFolderPath");
			ValidateArgument.NotNull(logger, "logger");
			this.logger = logger;
			this.logger.TraceDebug(this, "Entering NormalizationCache constructor culture='{0}', fileNamePrefix='{1}'", new object[]
			{
				culture,
				fileNamePrefix
			});
			this.culture = culture;
			this.fileNamePrefix = fileNamePrefix;
			this.normalizationCacheFolderPath = Path.Combine(grammarFolderPath, "NormalizationCache");
			this.versionedCacheFolderName = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
			{
				"1.0",
				typeof(Dictionary<string, bool>).Assembly.ImageRuntimeVersion
			});
			this.cacheFileStore = cacheFileStore;
			this.Initialize();
		}

		// Token: 0x17000466 RID: 1126
		public bool this[string word]
		{
			set
			{
				if (this.cache.Count < 200000)
				{
					this.cache.Add(word, value);
				}
			}
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x000653CF File Offset: 0x000635CF
		public bool TryGetValue(string word, out bool acceptWord)
		{
			return this.cache.TryGetValue(word, out acceptWord);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000653E0 File Offset: 0x000635E0
		public void UpdateDiskCache()
		{
			this.logger.TraceDebug(this, "Entering NormalizationCache.UpdateDiskCache filePath='{0}', cache.Count='{1}'", new object[]
			{
				this.filePath,
				this.cache.Count
			});
			try
			{
				if (this.ShouldUpdateDiskCache())
				{
					using (Stream stream = File.Open(this.filePath, FileMode.Create, FileAccess.Write))
					{
						BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
						binaryFormatter.Serialize(stream, this.cache);
					}
					this.logger.TraceDebug(this, "NormalizationCache.UpdateDiskCache - Saved cache to filePath='{0}'", new object[]
					{
						this.filePath
					});
					this.shouldUploadCache = true;
				}
				this.cache.Clear();
				this.cache = null;
				string[] directories = Directory.GetDirectories(this.normalizationCacheFolderPath);
				foreach (string text in directories)
				{
					string fileName = Path.GetFileName(text);
					if (string.Compare(fileName, this.versionedCacheFolderName, StringComparison.OrdinalIgnoreCase) != 0)
					{
						this.logger.TraceDebug(this, "Deleting normalization cache directory '{0}'", new object[]
						{
							text
						});
						Directory.Delete(text, true);
					}
				}
			}
			catch (Exception ex)
			{
				this.logger.TraceDebug(this, "NormalizationCache.UpdateDiskCache - Unable to update disk cache filePath='{0}', exception='{1}'", new object[]
				{
					this.filePath,
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SaveNormalizationCacheFailed, null, new object[]
				{
					this.logger.TenantId,
					this.filePath,
					this.culture,
					CommonUtil.ToEventLogString(ex)
				});
				if (!this.IsExpectedException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000655A8 File Offset: 0x000637A8
		public void UploadCacheFile(MailboxSession mbxSession)
		{
			if (this.shouldUploadCache && this.cacheFileStore != null)
			{
				this.cacheFileStore.UploadCache(this.filePath, this.fileNamePrefix, this.culture, this.versionedCacheFolderName, mbxSession);
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000655E0 File Offset: 0x000637E0
		private void Initialize()
		{
			try
			{
				this.logger.TraceDebug(this, "Entering NormalizationCache.Initialize normalizationCacheFolderPath='{0}', versionedCacheFolderName='{1}'", new object[]
				{
					this.normalizationCacheFolderPath,
					this.versionedCacheFolderName
				});
				string text = Path.Combine(this.normalizationCacheFolderPath, this.versionedCacheFolderName);
				Directory.CreateDirectory(text);
				string path = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
				{
					this.fileNamePrefix,
					this.culture.Name
				});
				this.filePath = Path.Combine(text, path);
				this.logger.TraceDebug(this, "NormalizationCache.Initialize filePath='{0}'", new object[]
				{
					this.filePath
				});
				if (this.CheckCacheFile())
				{
					using (Stream stream = File.Open(this.filePath, FileMode.Open, FileAccess.Read))
					{
						Type[] expectedTypes = new Type[]
						{
							Type.GetType("System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib], [System.Boolean, mscorlib]]"),
							Type.GetType("System.Collections.Generic.GenericEqualityComparer`1[[System.String, mscorlib]]"),
							typeof(Dictionary<string, bool>)
						};
						this.cache = (Dictionary<string, bool>)TypedBinaryFormatter.DeserializeObject(stream, expectedTypes, null, true);
						this.initialCacheCount = this.cache.Count;
					}
					this.logger.TraceDebug(this, "NormalizationCache.Initialize - Loaded cache from filePath='{0}', initial cache count = '{1}'", new object[]
					{
						this.filePath,
						this.initialCacheCount
					});
				}
				else
				{
					this.logger.TraceDebug(this, "NormalizationCache.Initialize - Cache does not exist or could not be downloaded, filePath='{0}'", new object[]
					{
						this.filePath
					});
				}
			}
			catch (Exception ex)
			{
				this.logger.TraceDebug(this, "NormalizationCache.Initialize - Unable to load cache from filePath='{0}', exception='{1}'", new object[]
				{
					this.filePath,
					ex
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_LoadNormalizationCacheFailed, null, new object[]
				{
					this.logger.TenantId,
					this.filePath,
					this.culture,
					CommonUtil.ToEventLogString(ex)
				});
				if (!this.IsExpectedException(ex))
				{
					throw;
				}
			}
			if (this.cache == null)
			{
				this.logger.TraceDebug(this, "NormalizationCache.Initialize - Creating empty cache", new object[0]);
				this.cache = new Dictionary<string, bool>(1000);
				this.initialCacheCount = 0;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00065868 File Offset: 0x00063A68
		private bool ShouldUpdateDiskCache()
		{
			bool result = false;
			if (this.cache.Count > 0)
			{
				int num = (this.cache.Count - this.initialCacheCount) * 100 / this.cache.Count;
				this.logger.TraceDebug(this, "NormalizationCache.ShouldUpdateDiskCache - initialCacheCount='{0}', cache.Count='{1}', percentageAdditions='{2}'", new object[]
				{
					this.initialCacheCount,
					this.cache.Count,
					num
				});
				if (num >= 10)
				{
					result = true;
				}
			}
			else
			{
				this.logger.TraceDebug(this, "Skip disk update, cache count='{0}'", new object[]
				{
					this.cache.Count
				});
			}
			return result;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00065920 File Offset: 0x00063B20
		private bool CheckCacheFile()
		{
			bool flag = false;
			if (File.Exists(this.filePath))
			{
				this.logger.TraceDebug(this, "NormalizationCache.CheckCacheFile - Found cache file at filePath='{0}'", new object[]
				{
					this.filePath
				});
				DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(this.filePath);
				if (DateTime.UtcNow - lastWriteTimeUtc > this.MaxCacheAge)
				{
					this.logger.TraceDebug(this, "NormalizationCache.CheckCacheFile - Cache is stale (lastWriteTimeUtc='{0}'), Deleting cache from filePath='{1}'", new object[]
					{
						lastWriteTimeUtc,
						this.filePath
					});
					File.Delete(this.filePath);
				}
				else
				{
					this.logger.TraceDebug(this, "NormalizationCache.CheckCacheFile - Found usable cache at filePath='{0}'", new object[]
					{
						this.filePath
					});
					flag = true;
				}
			}
			if (!flag && this.cacheFileStore != null)
			{
				flag = this.cacheFileStore.DownloadCache(this.filePath, this.fileNamePrefix, this.culture, this.versionedCacheFolderName);
				this.logger.TraceDebug(this, "NormalizationCache.CheckCacheFile - Download of cache to filePath='{0}', success='{1}'", new object[]
				{
					this.filePath,
					flag
				});
			}
			return flag;
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00065A43 File Offset: 0x00063C43
		private bool IsExpectedException(Exception e)
		{
			return e is IOException || e is UnauthorizedAccessException || e is SecurityException || e is SerializationException;
		}

		// Token: 0x04000ACB RID: 2763
		private const string NormalizationCacheFolderName = "NormalizationCache";

		// Token: 0x04000ACC RID: 2764
		private const string CacheVersion = "1.0";

		// Token: 0x04000ACD RID: 2765
		private const string VersionedCacheFolderNameFormat = "{0}-{1}";

		// Token: 0x04000ACE RID: 2766
		private const string FileNameFormat = "{0}-{1}";

		// Token: 0x04000ACF RID: 2767
		private const int InitialCacheSize = 1000;

		// Token: 0x04000AD0 RID: 2768
		private const int MaxCacheCount = 200000;

		// Token: 0x04000AD1 RID: 2769
		private const int MinPercentageAdded = 10;

		// Token: 0x04000AD2 RID: 2770
		private readonly TimeSpan MaxCacheAge = TimeSpan.FromDays(30.0);

		// Token: 0x04000AD3 RID: 2771
		private readonly CultureInfo culture;

		// Token: 0x04000AD4 RID: 2772
		private readonly string fileNamePrefix;

		// Token: 0x04000AD5 RID: 2773
		private readonly Logger logger;

		// Token: 0x04000AD6 RID: 2774
		private readonly INormalizationCacheFileStore cacheFileStore;

		// Token: 0x04000AD7 RID: 2775
		private readonly string normalizationCacheFolderPath;

		// Token: 0x04000AD8 RID: 2776
		private readonly string versionedCacheFolderName;

		// Token: 0x04000AD9 RID: 2777
		private string filePath;

		// Token: 0x04000ADA RID: 2778
		private int initialCacheCount;

		// Token: 0x04000ADB RID: 2779
		private Dictionary<string, bool> cache;

		// Token: 0x04000ADC RID: 2780
		private bool shouldUploadCache;
	}
}
