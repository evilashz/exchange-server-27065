using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000044 RID: 68
	internal class CacheCleaner : IUMAsyncComponent
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000C920 File Offset: 0x0000AB20
		public static IUMAsyncComponent Instance
		{
			get
			{
				return CacheCleaner.instance;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000C927 File Offset: 0x0000AB27
		public AutoResetEvent StoppedEvent
		{
			get
			{
				return this.syncEvent;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000C92F File Offset: 0x0000AB2F
		public bool IsInitialized
		{
			get
			{
				return this.initialized;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000C937 File Offset: 0x0000AB37
		public string Name
		{
			get
			{
				return "Local Disk Cache Cleaner";
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000C940 File Offset: 0x0000AB40
		private string CleanupSentinelPath
		{
			get
			{
				string path = Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging");
				return Path.Combine(path, "CacheCleanup.bin");
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C968 File Offset: 0x0000AB68
		internal static void TouchUpDirectory(DirectoryInfo dir)
		{
			try
			{
				Directory.SetLastAccessTimeUtc(dir.FullName, (DateTime)ExDateTime.UtcNow);
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public void StartNow(StartupStage stage)
		{
			if (stage == StartupStage.WPActivation)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.StartNow", new object[0]);
				this.cleanupTimer = new Timer(new TimerCallback(this.CleanupProcedure), null, this.GetTimeToCleanup(), this.cleanupInterval);
			}
			this.initialized = true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000CA04 File Offset: 0x0000AC04
		public void StopAsync()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.StopAsync", new object[0]);
			lock (this.lockObj)
			{
				this.shuttingDown = true;
				if (!this.cleaning)
				{
					this.syncEvent.Set();
				}
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public void CleanupAfterStopped()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.CleanupAfterStopped", new object[0]);
			if (this.cleanupTimer != null)
			{
				this.cleanupTimer.Dispose();
			}
			if (this.syncEvent != null)
			{
				this.syncEvent.Close();
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		internal void TestHookSetCleanupParameters(TimeSpan interval, string consumerName, ulong limit)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.TestHookSetCleanupParameters", new object[0]);
			foreach (CacheCleaner.IDiskCacheConsumer diskCacheConsumer in this.cacheConsumers)
			{
				if (diskCacheConsumer.Name.Equals(consumerName, StringComparison.InvariantCultureIgnoreCase))
				{
					diskCacheConsumer.CacheSizeLimit = limit;
				}
			}
			this.cleanupInterval = interval;
			this.cleanupTimer.Change(interval, interval);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CB18 File Offset: 0x0000AD18
		private static bool TrySafeDeleteDirectory(DateTime utcLastUsed, string directoryPath)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, null, "TrySafeDeleteDirectory", new object[0]);
			bool result = false;
			try
			{
				string fullName = Directory.GetParent(directoryPath).FullName;
				string text = Path.Combine(fullName, Guid.NewGuid().ToString());
				Directory.Move(directoryPath, text);
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, null, "Moved '{0}' to '{1}'", new object[]
				{
					directoryPath,
					text
				});
				Directory.SetLastAccessTimeUtc(text, utcLastUsed);
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, null, "Updated access time", new object[0]);
				Directory.Delete(text, true);
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, null, "Deleted", new object[0]);
				result = true;
			}
			catch (IOException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, null, "TrySafeDeleteDirectory exception='{0}'", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000CC00 File Offset: 0x0000AE00
		private void CleanupProcedure(object state)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.CleanupProcedure", new object[0]);
			lock (this.lockObj)
			{
				this.cleaning = true;
			}
			this.bailedEarly = false;
			try
			{
				foreach (CacheCleaner.IDiskCacheConsumer diskCacheConsumer in this.cacheConsumers)
				{
					List<CacheCleaner.LRUInformation> lru = null;
					if (this.shuttingDown)
					{
						this.bailedEarly = true;
						break;
					}
					ulong num = diskCacheConsumer.CustomCleanupAndBuildLRU(out lru);
					if (num > diskCacheConsumer.CacheSizeLimit)
					{
						this.LruKick(lru, num, diskCacheConsumer.CacheSizeLimit);
					}
				}
				if (!this.bailedEarly)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "updating sentinel", new object[0]);
					using (File.Create(this.CleanupSentinelPath))
					{
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Cleanup procedure encountered an exception='{0}'", new object[]
				{
					ex
				});
			}
			catch (IOException ex2)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Cleanup procedure encountered an exception='{0}'", new object[]
				{
					ex2
				});
			}
			finally
			{
				lock (this.lockObj)
				{
					this.cleaning = false;
					this.bailedEarly = false;
					if (this.shuttingDown)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "Cleanup procedure setting shutdown flag", new object[0]);
						this.syncEvent.Set();
					}
				}
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		private void LruKick(List<CacheCleaner.LRUInformation> lru, ulong totalBytes, ulong cacheLimitBytes)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.LruKick", new object[0]);
			if (this.shuttingDown)
			{
				this.bailedEarly = true;
				return;
			}
			lru.Sort();
			ulong num = totalBytes;
			ulong num2 = (ulong)(cacheLimitBytes * 0.8);
			foreach (CacheCleaner.LRUInformation lruinformation in lru)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "TargetBytes='{0}', UpdatedBytes='{1}'", new object[]
				{
					num2,
					num
				});
				if (this.shuttingDown)
				{
					this.bailedEarly = true;
					break;
				}
				if (num < num2)
				{
					break;
				}
				if (CacheCleaner.TrySafeDeleteDirectory((DateTime)lruinformation.UtcLastUsed, lruinformation.Path))
				{
					num -= lruinformation.Bytes;
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		private TimeSpan GetTimeToCleanup()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "CacheCleaner.GetTimeToCleanup", new object[0]);
			DateTime creationTimeUtc = File.GetCreationTimeUtc(this.CleanupSentinelPath);
			TimeSpan timeSpan = this.cleanupInterval - (DateTime.UtcNow - creationTimeUtc);
			if (timeSpan < TimeSpan.Zero)
			{
				timeSpan = TimeSpan.FromMinutes(1.0);
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "TimeToCleanup='{0}'", new object[]
			{
				timeSpan
			});
			return timeSpan;
		}

		// Token: 0x040000E0 RID: 224
		public const string SentinelFileExtension = ".delete";

		// Token: 0x040000E1 RID: 225
		private static CacheCleaner instance = new CacheCleaner();

		// Token: 0x040000E2 RID: 226
		private bool initialized;

		// Token: 0x040000E3 RID: 227
		private bool shuttingDown;

		// Token: 0x040000E4 RID: 228
		private bool cleaning;

		// Token: 0x040000E5 RID: 229
		private bool bailedEarly;

		// Token: 0x040000E6 RID: 230
		private TimeSpan cleanupInterval = TimeSpan.FromHours(24.0);

		// Token: 0x040000E7 RID: 231
		private AutoResetEvent syncEvent = new AutoResetEvent(false);

		// Token: 0x040000E8 RID: 232
		private object lockObj = new object();

		// Token: 0x040000E9 RID: 233
		private Timer cleanupTimer;

		// Token: 0x040000EA RID: 234
		private CacheCleaner.IDiskCacheConsumer[] cacheConsumers = new CacheCleaner.IDiskCacheConsumer[]
		{
			new CacheCleaner.CustomPromptDiskCacheConsumer(),
			new CacheCleaner.LargeGrammarsDiskCacheConsumer()
		};

		// Token: 0x02000045 RID: 69
		internal class LRUInformation : IComparable
		{
			// Token: 0x060002DC RID: 732 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
			public LRUInformation(string path, ulong bytes, ExDateTime utcLastUsed)
			{
				this.Path = path;
				this.Bytes = bytes;
				this.UtcLastUsed = utcLastUsed;
			}

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060002DD RID: 733 RVA: 0x0000CFC5 File Offset: 0x0000B1C5
			// (set) Token: 0x060002DE RID: 734 RVA: 0x0000CFCD File Offset: 0x0000B1CD
			public string Path { get; set; }

			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060002DF RID: 735 RVA: 0x0000CFD6 File Offset: 0x0000B1D6
			// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000CFDE File Offset: 0x0000B1DE
			public ulong Bytes { get; set; }

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000CFE7 File Offset: 0x0000B1E7
			// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000CFEF File Offset: 0x0000B1EF
			public ExDateTime UtcLastUsed { get; set; }

			// Token: 0x060002E3 RID: 739 RVA: 0x0000CFF8 File Offset: 0x0000B1F8
			public int CompareTo(object obj)
			{
				CacheCleaner.LRUInformation lruinformation = obj as CacheCleaner.LRUInformation;
				if (lruinformation != null)
				{
					return ExDateTime.Compare(lruinformation.UtcLastUsed, this.UtcLastUsed);
				}
				throw new ArgumentException("Object is not LRUInformation");
			}
		}

		// Token: 0x02000046 RID: 70
		private interface IDiskCacheConsumer
		{
			// Token: 0x060002E4 RID: 740
			ulong CustomCleanupAndBuildLRU(out List<CacheCleaner.LRUInformation> lru);

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060002E5 RID: 741
			// (set) Token: 0x060002E6 RID: 742
			ulong CacheSizeLimit { get; set; }

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060002E7 RID: 743
			string Name { get; }
		}

		// Token: 0x02000047 RID: 71
		private class CustomPromptDiskCacheConsumer : CacheCleaner.IDiskCacheConsumer
		{
			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000D02B File Offset: 0x0000B22B
			// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000D033 File Offset: 0x0000B233
			public ulong CacheSizeLimit
			{
				get
				{
					return this.cacheSizeLimit;
				}
				set
				{
					this.cacheSizeLimit = value;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060002EA RID: 746 RVA: 0x0000D03C File Offset: 0x0000B23C
			public string Name
			{
				get
				{
					return base.GetType().Name;
				}
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000D049 File Offset: 0x0000B249
			public CustomPromptDiskCacheConsumer()
			{
				this.cachePath = Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\Prompts\\Cache");
				this.cacheSizeLimit = 17179869184UL;
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000D078 File Offset: 0x0000B278
			public ulong CustomCleanupAndBuildLRU(out List<CacheCleaner.LRUInformation> lru)
			{
				lru = new List<CacheCleaner.LRUInformation>();
				ulong num = 0UL;
				DirectoryInfo directoryInfo = new DirectoryInfo(this.cachePath);
				if (directoryInfo.Exists)
				{
					DirectoryInfo[] directories = directoryInfo.GetDirectories();
					foreach (DirectoryInfo directoryInfo2 in directories)
					{
						DirectoryInfo[] directories2 = directoryInfo2.GetDirectories();
						DateTime t = DateTime.MinValue;
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "computing newest directory", new object[0]);
						foreach (DirectoryInfo directoryInfo3 in directories2)
						{
							if (directoryInfo3.CreationTimeUtc > t)
							{
								t = directoryInfo3.CreationTimeUtc;
							}
						}
						CallIdTracer.TraceDebug(ExTraceGlobals.PromptProvisioningTracer, this, "removing aged directories", new object[0]);
						foreach (DirectoryInfo directoryInfo4 in directories2)
						{
							if (directoryInfo4.CreationTimeUtc >= t || !CacheCleaner.TrySafeDeleteDirectory(directoryInfo4.LastAccessTimeUtc, directoryInfo4.FullName))
							{
								ulong num2 = 0UL;
								foreach (FileInfo fileInfo in directoryInfo4.GetFiles())
								{
									num += (ulong)fileInfo.Length;
									num2 += (ulong)fileInfo.Length;
								}
								lru.Add(new CacheCleaner.LRUInformation(directoryInfo4.FullName, num2, new ExDateTime(ExTimeZone.UtcTimeZone, directoryInfo4.LastAccessTimeUtc)));
							}
						}
					}
				}
				return num;
			}

			// Token: 0x040000EE RID: 238
			private readonly string cachePath;

			// Token: 0x040000EF RID: 239
			private ulong cacheSizeLimit;
		}

		// Token: 0x02000048 RID: 72
		internal class LargeGrammarsDiskCacheConsumer : CacheCleaner.IDiskCacheConsumer
		{
			// Token: 0x170000A2 RID: 162
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000D1EE File Offset: 0x0000B3EE
			// (set) Token: 0x060002EE RID: 750 RVA: 0x0000D1F6 File Offset: 0x0000B3F6
			public ulong CacheSizeLimit
			{
				get
				{
					return this.cacheSizeLimit;
				}
				set
				{
					this.cacheSizeLimit = value;
				}
			}

			// Token: 0x170000A3 RID: 163
			// (get) Token: 0x060002EF RID: 751 RVA: 0x0000D1FF File Offset: 0x0000B3FF
			public string Name
			{
				get
				{
					return base.GetType().Name;
				}
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x0000D20C File Offset: 0x0000B40C
			public LargeGrammarsDiskCacheConsumer()
			{
				this.cachePath = Path.Combine(Utils.GetExchangeDirectory(), "UnifiedMessaging\\grammars");
				this.cacheSizeLimit = 9223372036854775807UL;
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000D238 File Offset: 0x0000B438
			public ulong CustomCleanupAndBuildLRU(out List<CacheCleaner.LRUInformation> lru)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Entering LargeGrammarsDiskCacheConsumer.CustomCleanupAndBuildLRU", new object[0]);
				lru = new List<CacheCleaner.LRUInformation>();
				DirectoryInfo directoryInfo = new DirectoryInfo(this.cachePath);
				if (directoryInfo.Exists)
				{
					DirectoryInfo[] directories = directoryInfo.GetDirectories("Cache", SearchOption.AllDirectories);
					foreach (DirectoryInfo directoryInfo2 in directories)
					{
						DirectoryInfo[] directories2 = directoryInfo2.GetDirectories();
						foreach (DirectoryInfo directoryInfo3 in directories2)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Checking tenant dir='{0}'", new object[]
							{
								directoryInfo3.FullName
							});
							FileInfo[] files = directoryInfo3.GetFiles("*.delete");
							foreach (FileInfo fileInfo in files)
							{
								if (DateTime.UtcNow - fileInfo.CreationTimeUtc > CacheCleaner.LargeGrammarsDiskCacheConsumer.CompiledGrammarExpiration)
								{
									string fullName = fileInfo.FullName;
									CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Sentinel file='{0}' has expired", new object[]
									{
										fullName
									});
									string text = fullName.Substring(0, fullName.Length - ".delete".Length);
									CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Deleting sentinel file='{0}' and grammarFile='{1}'", new object[]
									{
										fullName,
										text
									});
									File.Delete(fullName);
									if (File.Exists(text))
									{
										File.Delete(text);
									}
								}
							}
						}
					}
				}
				return 0UL;
			}

			// Token: 0x040000F0 RID: 240
			private static readonly TimeSpan CompiledGrammarExpiration = TimeSpan.FromDays(1.0);

			// Token: 0x040000F1 RID: 241
			private readonly string cachePath;

			// Token: 0x040000F2 RID: 242
			private ulong cacheSizeLimit;
		}
	}
}
