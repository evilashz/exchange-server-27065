using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Exchange.Compliance.Serialization.Formatters;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F7 RID: 503
	internal class TopNData
	{
		// Token: 0x06000EBD RID: 3773 RVA: 0x00042568 File Offset: 0x00040768
		protected TopNData(TopNData.CachedData cached, TopNData.ITopNWords topNWords, OffensiveWordsFilter offensiveWordsFilter)
		{
			this.cached = cached;
			this.offensiveWordsFilter = offensiveWordsFilter;
			if (this.cached.ContainsWords && this.cached.ScanTime == topNWords.LastScanTime)
			{
				this.isFiltered = true;
				this.isCached = true;
				return;
			}
			this.isFiltered = false;
			this.isCached = false;
			this.rawList = topNWords.WordList;
			this.rawScanTime = new ExDateTime?(topNWords.LastScanTime);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000425E8 File Offset: 0x000407E8
		public static TopNData Create(UMSubscriber subscriber, OffensiveWordsFilter offensiveWordsFilter)
		{
			TopNData result;
			using (subscriber.CreateSessionLock())
			{
				TopNData.CachedData cachedData = new TopNData.MailboxCachedData(subscriber);
				TopNData.ITopNWords topNWords = new TopNData.EmptyTopNWords();
				result = new TopNData(cachedData, topNWords, offensiveWordsFilter);
			}
			return result;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00042630 File Offset: 0x00040830
		public List<KeyValuePair<string, int>> GetFilteredList(BaseUMOfflineTranscriber transcriber)
		{
			List<KeyValuePair<string, int>> list = null;
			if (this.isFiltered)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "TopNData::Filter has no work to do because the list is already filtered", new object[0]);
				list = this.cached.WordList;
			}
			else if (this.rawList == null || this.rawList.Count == 0 || this.rawScanTime == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "TopNData::Filter has no work to do because the raw topn list is empty", new object[0]);
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "TopNData::Filter running on a list of count '{0}'", new object[]
				{
					this.rawList.Count
				});
				Exception ex = null;
				try
				{
					list = transcriber.FilterWordsInLexion(this.rawList, 2500);
					list = this.offensiveWordsFilter.Filter(list);
					this.cached.WordList = list;
					this.cached.ScanTime = this.rawScanTime.Value;
					this.isFiltered = true;
					this.isCached = false;
				}
				catch (InvalidOperationException ex2)
				{
					ex = ex2;
				}
				catch (COMException ex3)
				{
					ex = ex3;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "Filter ending with error='{0}'", new object[]
				{
					ex
				});
			}
			if (list == null)
			{
				list = new List<KeyValuePair<string, int>>(0);
			}
			if (list.Count > 2500)
			{
				list = list.GetRange(0, 2500);
			}
			return list;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000427C0 File Offset: 0x000409C0
		public bool TryCache()
		{
			Exception ex = null;
			try
			{
				if (!this.isCached && this.isFiltered)
				{
					this.cached.Save();
					this.isCached = true;
				}
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "TopNData::TryCache completed with error = '{0}'", new object[]
			{
				ex
			});
			return this.isCached;
		}

		// Token: 0x04000B10 RID: 2832
		public const int MaximumFilteredWords = 2500;

		// Token: 0x04000B11 RID: 2833
		private readonly TopNData.CachedData cached;

		// Token: 0x04000B12 RID: 2834
		private readonly OffensiveWordsFilter offensiveWordsFilter;

		// Token: 0x04000B13 RID: 2835
		private List<KeyValuePair<string, int>> rawList;

		// Token: 0x04000B14 RID: 2836
		private ExDateTime? rawScanTime;

		// Token: 0x04000B15 RID: 2837
		private bool isFiltered;

		// Token: 0x04000B16 RID: 2838
		private bool isCached;

		// Token: 0x020001F8 RID: 504
		protected abstract class CachedData
		{
			// Token: 0x17000395 RID: 917
			// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00042848 File Offset: 0x00040A48
			// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x00042850 File Offset: 0x00040A50
			public ExDateTime ScanTime { get; set; }

			// Token: 0x17000396 RID: 918
			// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00042859 File Offset: 0x00040A59
			// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x00042861 File Offset: 0x00040A61
			public List<KeyValuePair<string, int>> WordList { get; set; }

			// Token: 0x17000397 RID: 919
			// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0004286A File Offset: 0x00040A6A
			public bool ContainsWords
			{
				get
				{
					return this.WordList != null && this.WordList.Count > 0;
				}
			}

			// Token: 0x06000EC6 RID: 3782
			public abstract void Save();
		}

		// Token: 0x020001F9 RID: 505
		private class MailboxCachedData : TopNData.CachedData
		{
			// Token: 0x06000EC8 RID: 3784 RVA: 0x0004288C File Offset: 0x00040A8C
			public MailboxCachedData(UMSubscriber subscriber)
			{
				this.subscriber = subscriber;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
				{
					using (UserConfiguration config = this.GetConfig(mailboxSessionLock.Session))
					{
						this.ReadDictionary(config);
						this.ReadStream(config);
					}
				}
			}

			// Token: 0x06000EC9 RID: 3785 RVA: 0x00042904 File Offset: 0x00040B04
			public override void Save()
			{
				if (base.ContainsWords)
				{
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
					{
						using (UserConfiguration config = this.GetConfig(mailboxSessionLock.Session))
						{
							this.SaveDictionary(config);
							this.SaveStream(config);
							config.Save();
						}
					}
				}
			}

			// Token: 0x06000ECA RID: 3786 RVA: 0x0004297C File Offset: 0x00040B7C
			private void ReadDictionary(UserConfiguration config)
			{
				IDictionary dictionary = config.GetDictionary();
				object obj = dictionary["ScanTime"];
				if (obj == null)
				{
					obj = ExDateTime.MinValue;
				}
				else if (!(obj is ExDateTime))
				{
					using (this.RebuildConfiguration())
					{
					}
					obj = ExDateTime.MinValue;
				}
				base.ScanTime = (ExDateTime)obj;
			}

			// Token: 0x06000ECB RID: 3787 RVA: 0x000429F0 File Offset: 0x00040BF0
			private void SaveDictionary(UserConfiguration config)
			{
				IDictionary dictionary = config.GetDictionary();
				dictionary["ScanTime"] = base.ScanTime;
			}

			// Token: 0x06000ECC RID: 3788 RVA: 0x00042A1C File Offset: 0x00040C1C
			private void ReadStream(UserConfiguration config)
			{
				using (Stream stream = config.GetStream())
				{
					Exception ex = null;
					List<KeyValuePair<string, int>> list = null;
					try
					{
						list = (List<KeyValuePair<string, int>>)TypedBinaryFormatter.DeserializeObject(stream, new Type[]
						{
							Type.GetType("System.Collections.Generic.List`1[[System.Collections.Generic.KeyValuePair`2[[System.String, mscorlib],[System.Int32, mscorlib]], mscorlib]]")
						}, null, true);
					}
					catch (ArgumentNullException ex2)
					{
						ex = ex2;
					}
					catch (SerializationException ex3)
					{
						ex = ex3;
					}
					finally
					{
						base.WordList = (list ?? new List<KeyValuePair<string, int>>());
						if (ex != null)
						{
							CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "Error reading cached word list '{0}'", new object[]
							{
								ex
							});
						}
					}
				}
			}

			// Token: 0x06000ECD RID: 3789 RVA: 0x00042AE8 File Offset: 0x00040CE8
			private void SaveStream(UserConfiguration config)
			{
				using (Stream stream = config.GetStream())
				{
					IFormatter formatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					Exception ex = null;
					try
					{
						formatter.Serialize(stream, base.WordList);
					}
					catch (ArgumentNullException ex2)
					{
						ex = ex2;
					}
					catch (SerializationException ex3)
					{
						ex = ex3;
					}
					if (ex != null)
					{
						CallIdTracer.TraceDebug(ExTraceGlobals.XsoTracer, this.GetHashCode(), "Error saving cached word list '{0}'", new object[]
						{
							ex
						});
					}
				}
			}

			// Token: 0x06000ECE RID: 3790 RVA: 0x00042B80 File Offset: 0x00040D80
			private UserConfiguration GetConfig(MailboxSession s)
			{
				UserConfiguration result = null;
				StoreId defaultFolderId = s.GetDefaultFolderId(DefaultFolderType.Inbox);
				try
				{
					result = s.UserConfigurationManager.GetFolderConfiguration("TopNWords.Data.OutOfGrammar", UserConfigurationTypes.Stream | UserConfigurationTypes.Dictionary, defaultFolderId);
				}
				catch (ObjectNotFoundException)
				{
					result = s.UserConfigurationManager.CreateFolderConfiguration("TopNWords.Data.OutOfGrammar", UserConfigurationTypes.Stream | UserConfigurationTypes.Dictionary, defaultFolderId);
				}
				catch (CorruptDataException)
				{
					result = this.RebuildConfiguration();
				}
				catch (InvalidOperationException)
				{
					result = this.RebuildConfiguration();
				}
				return result;
			}

			// Token: 0x06000ECF RID: 3791 RVA: 0x00042C00 File Offset: 0x00040E00
			private UserConfiguration RebuildConfiguration()
			{
				UserConfiguration result;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.subscriber.CreateSessionLock())
				{
					StoreId defaultFolderId = mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Inbox);
					mailboxSessionLock.Session.UserConfigurationManager.DeleteMailboxConfigurations(new string[]
					{
						"TopNWords.Data.OutOfGrammar"
					});
					result = mailboxSessionLock.Session.UserConfigurationManager.CreateFolderConfiguration("TopNWords.Data.OutOfGrammar", UserConfigurationTypes.Stream | UserConfigurationTypes.Dictionary, defaultFolderId);
				}
				return result;
			}

			// Token: 0x04000B19 RID: 2841
			private const string MessageClass = "TopNWords.Data.OutOfGrammar";

			// Token: 0x04000B1A RID: 2842
			private const string ScanTimeName = "ScanTime";

			// Token: 0x04000B1B RID: 2843
			private UMSubscriber subscriber;
		}

		// Token: 0x020001FA RID: 506
		protected interface ITopNWords
		{
			// Token: 0x17000398 RID: 920
			// (get) Token: 0x06000ED0 RID: 3792
			ExDateTime LastScanTime { get; }

			// Token: 0x17000399 RID: 921
			// (get) Token: 0x06000ED1 RID: 3793
			List<KeyValuePair<string, int>> WordList { get; }
		}

		// Token: 0x020001FB RID: 507
		protected class EmptyTopNWords : TopNData.ITopNWords
		{
			// Token: 0x06000ED2 RID: 3794 RVA: 0x00042C7C File Offset: 0x00040E7C
			public EmptyTopNWords()
			{
				this.WordList = new List<KeyValuePair<string, int>>();
				this.LastScanTime = ExDateTime.Now.AddMinutes(-1.0);
			}

			// Token: 0x1700039A RID: 922
			// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00042CB6 File Offset: 0x00040EB6
			// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00042CBE File Offset: 0x00040EBE
			public ExDateTime LastScanTime { get; private set; }

			// Token: 0x1700039B RID: 923
			// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00042CC7 File Offset: 0x00040EC7
			// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00042CCF File Offset: 0x00040ECF
			public List<KeyValuePair<string, int>> WordList { get; private set; }
		}
	}
}
