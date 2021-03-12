using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000113 RID: 275
	internal sealed class FolderMruCache : IComparer
	{
		// Token: 0x06000913 RID: 2323 RVA: 0x000416CC File Offset: 0x0003F8CC
		private FolderMruCache(UserContext userContext)
		{
			this.userContext = userContext;
			this.cacheEntries = new FolderMruCacheEntry[20];
			this.Load();
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000914 RID: 2324 RVA: 0x000416EE File Offset: 0x0003F8EE
		public FolderMruCacheEntry[] CacheEntries
		{
			get
			{
				return this.cacheEntries;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000416F6 File Offset: 0x0003F8F6
		public int CacheLength
		{
			get
			{
				return this.cacheLength;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00041700 File Offset: 0x0003F900
		public static FolderMruCache GetCacheInstance(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			FolderMruCache folderMruCache = null;
			if (!userContext.IsMruSessionStarted)
			{
				userContext.IsMruSessionStarted = true;
				folderMruCache = new FolderMruCache(userContext);
				folderMruCache.StartCacheSession();
				folderMruCache.Commit();
			}
			if (folderMruCache == null)
			{
				return new FolderMruCache(userContext);
			}
			return folderMruCache;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0004174C File Offset: 0x0003F94C
		public static void DeleteFromCache(StoreObjectId folderId, UserContext userContext)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("folderId");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			FolderMruCache cacheInstance = FolderMruCache.GetCacheInstance(userContext);
			int entryIndexByFolderId = cacheInstance.GetEntryIndexByFolderId(folderId);
			if (entryIndexByFolderId == -1)
			{
				return;
			}
			cacheInstance.ShiftBackEntries(entryIndexByFolderId + 1);
			cacheInstance.Commit();
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00041798 File Offset: 0x0003F998
		public static void FinishCacheSession(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			FolderMruCache cacheInstance = FolderMruCache.GetCacheInstance(userContext);
			cacheInstance.InternalFinishCacheSession();
			cacheInstance.Commit();
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000417C8 File Offset: 0x0003F9C8
		int IComparer.Compare(object x, object y)
		{
			FolderMruCacheEntry folderMruCacheEntry = x as FolderMruCacheEntry;
			FolderMruCacheEntry folderMruCacheEntry2 = y as FolderMruCacheEntry;
			if (folderMruCacheEntry == null && folderMruCacheEntry2 == null)
			{
				return 0;
			}
			if (folderMruCacheEntry == null)
			{
				return 1;
			}
			if (folderMruCacheEntry2 == null)
			{
				return -1;
			}
			if (folderMruCacheEntry2.UsageCount != folderMruCacheEntry.UsageCount)
			{
				return folderMruCacheEntry2.UsageCount.CompareTo(folderMruCacheEntry.UsageCount);
			}
			return folderMruCacheEntry2.LastAccessedDateTimeTicks.CompareTo(folderMruCacheEntry.LastAccessedDateTimeTicks);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0004182C File Offset: 0x0003FA2C
		public void Sort()
		{
			Array.Sort(this.cacheEntries, this);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0004183C File Offset: 0x0003FA3C
		private static int ComputeDecay(double percent, int setDecay, int usageCount)
		{
			int num = (int)Math.Round(percent * (double)usageCount);
			int result;
			if (num > setDecay)
			{
				result = num;
			}
			else
			{
				result = setDecay;
			}
			return result;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00041864 File Offset: 0x0003FA64
		public void AddEntry(FolderMruCacheEntry newEntry)
		{
			if (newEntry == null)
			{
				throw new ArgumentNullException("newEntry");
			}
			int entryIndexByFolderId = this.GetEntryIndexByFolderId(newEntry.FolderId);
			int num;
			if (entryIndexByFolderId == -1)
			{
				if (this.firstEmptyIndex == -1)
				{
					num = this.GetLeastUsageEntryIndex();
				}
				else
				{
					num = this.firstEmptyIndex;
				}
				newEntry.SetInitialUsage();
				this.cacheEntries[num] = newEntry;
				if (this.firstEmptyIndex != -1 && this.firstEmptyIndex < 19)
				{
					this.firstEmptyIndex++;
					this.cacheLength = this.firstEmptyIndex;
				}
				else
				{
					this.firstEmptyIndex = -1;
					this.cacheLength = 20;
				}
			}
			else
			{
				num = entryIndexByFolderId;
				this.UpdateEntry(newEntry, num);
			}
			this.cacheEntries[num].UpdateTimeStamp();
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00041914 File Offset: 0x0003FB14
		private void UpdateEntry(FolderMruCacheEntry newEntry, int oldEntryIndex)
		{
			FolderMruCacheEntry folderMruCacheEntry = this.cacheEntries[oldEntryIndex];
			folderMruCacheEntry.UsageCount++;
			folderMruCacheEntry.UsedInCurrentSession = true;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00041940 File Offset: 0x0003FB40
		private void ShiftBackEntries(int index)
		{
			int i = index;
			if (index < this.cacheEntries.Length)
			{
				while (i < this.cacheLength)
				{
					this.cacheEntries[i - 1] = this.cacheEntries[i];
					i++;
				}
			}
			this.cacheEntries[i - 1] = null;
			if (this.firstEmptyIndex > 0)
			{
				this.firstEmptyIndex--;
			}
			else if (this.firstEmptyIndex == -1)
			{
				this.firstEmptyIndex = 19;
			}
			this.cacheLength = this.firstEmptyIndex;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000419BC File Offset: 0x0003FBBC
		public int GetEntryIndexByFolderId(StoreObjectId folderId)
		{
			if (folderId == null)
			{
				return -1;
			}
			for (int i = 0; i < this.cacheLength; i++)
			{
				if (folderId.Equals(this.cacheEntries[i].FolderId))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000419F8 File Offset: 0x0003FBF8
		private int GetLeastUsageEntryIndex()
		{
			int num = 0;
			for (int i = 1; i < this.cacheLength; i++)
			{
				if (((IComparer)this).Compare(this.cacheEntries[i], this.cacheEntries[num]) > 0)
				{
					num = i;
				}
			}
			return num;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00041A38 File Offset: 0x0003FC38
		private void InternalFinishCacheSession()
		{
			int num = -1;
			int i = 0;
			while (i < this.cacheLength)
			{
				FolderMruCacheEntry folderMruCacheEntry = this.cacheEntries[i];
				if (!folderMruCacheEntry.UsedInCurrentSession)
				{
					folderMruCacheEntry.NumberOfSessionsSinceLastUse++;
				}
				else
				{
					folderMruCacheEntry.NumberOfSessionsSinceLastUse = 0;
				}
				int numberOfSessionsSinceLastUse = folderMruCacheEntry.NumberOfSessionsSinceLastUse;
				if (numberOfSessionsSinceLastUse <= 12)
				{
					if (numberOfSessionsSinceLastUse <= 6)
					{
						if (numberOfSessionsSinceLastUse != 3 && numberOfSessionsSinceLastUse != 6)
						{
							goto IL_E9;
						}
					}
					else if (numberOfSessionsSinceLastUse != 9)
					{
						if (numberOfSessionsSinceLastUse != 12)
						{
							goto IL_E9;
						}
						num = FolderMruCache.ComputeDecay(0.25, 2, folderMruCacheEntry.UsageCount);
						goto IL_FA;
					}
					num = 1;
				}
				else if (numberOfSessionsSinceLastUse <= 23)
				{
					if (numberOfSessionsSinceLastUse != 17)
					{
						if (numberOfSessionsSinceLastUse != 23)
						{
							goto IL_E9;
						}
						num = FolderMruCache.ComputeDecay(0.5, 4, folderMruCacheEntry.UsageCount);
					}
					else
					{
						num = FolderMruCache.ComputeDecay(0.25, 3, folderMruCacheEntry.UsageCount);
					}
				}
				else if (numberOfSessionsSinceLastUse != 26)
				{
					if (numberOfSessionsSinceLastUse != 31)
					{
						goto IL_E9;
					}
					num = 0;
					folderMruCacheEntry.UsageCount = 0;
				}
				else
				{
					num = FolderMruCache.ComputeDecay(0.75, 5, folderMruCacheEntry.UsageCount);
				}
				IL_FA:
				if (num > 0)
				{
					folderMruCacheEntry.DecayUsage(num);
				}
				i++;
				continue;
				IL_E9:
				if (folderMruCacheEntry.NumberOfSessionsSinceLastUse > 31)
				{
					folderMruCacheEntry.UsageCount = 0;
					goto IL_FA;
				}
				goto IL_FA;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00041B5C File Offset: 0x0003FD5C
		internal void StartCacheSession()
		{
			for (int i = 0; i < this.cacheLength; i++)
			{
				this.cacheEntries[i].UsedInCurrentSession = false;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00041B88 File Offset: 0x0003FD88
		public void ClearCache()
		{
			for (int i = 0; i < this.cacheLength; i++)
			{
				this.cacheEntries[i] = null;
			}
			this.firstEmptyIndex = 0;
			this.cacheLength = 0;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00041BC0 File Offset: 0x0003FDC0
		public void Commit()
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration())
			{
				using (Stream xmlStream = userConfiguration.GetXmlStream())
				{
					xmlStream.SetLength(0L);
					using (StreamWriter streamWriter = Utilities.CreateStreamWriter(xmlStream))
					{
						this.RenderXml(streamWriter);
					}
				}
				try
				{
					userConfiguration.Save();
				}
				catch (ObjectNotFoundException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "FolderMruCache.Commit: Failed. Exception: {0}", ex.Message);
				}
				catch (QuotaExceededException ex2)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "FolderMruCache.Commit: Failed. Exception: {0}", ex2.Message);
				}
				catch (SaveConflictException ex3)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "FolderMruCache.Commit: Failed. Exception: {0}", ex3.Message);
				}
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00041CBC File Offset: 0x0003FEBC
		public void Load()
		{
			using (UserConfiguration userConfiguration = this.GetUserConfiguration())
			{
				using (Stream xmlStream = userConfiguration.GetXmlStream())
				{
					if (xmlStream != null && xmlStream.Length > 0L)
					{
						using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(xmlStream))
						{
							this.Parse(xmlTextReader);
							goto IL_3B;
						}
					}
					this.ClearCache();
					IL_3B:;
				}
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00041D44 File Offset: 0x0003FF44
		private UserConfiguration GetUserConfiguration()
		{
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = this.userContext.MailboxSession.UserConfigurationManager.GetMailboxConfiguration("OWA.FolderMruCache", UserConfigurationTypes.XML);
			}
			catch (ObjectNotFoundException)
			{
				userConfiguration = this.userContext.MailboxSession.UserConfigurationManager.CreateMailboxConfiguration("OWA.FolderMruCache", UserConfigurationTypes.XML);
				try
				{
					userConfiguration.Save();
				}
				catch (QuotaExceededException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "FolderMruCache.GetUserConfiguration: Failed. Exception: {0}", ex.Message);
				}
				catch (SaveConflictException ex2)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "FolderMruCache.GetUserConfiguration: Failed. Exception: {0}", ex2.Message);
				}
			}
			return userConfiguration;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00041DF8 File Offset: 0x0003FFF8
		public void RenderXml(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(writer))
			{
				xmlTextWriter.WriteStartElement("FolderMruCache");
				for (int i = 0; i < this.cacheLength; i++)
				{
					this.cacheEntries[i].RenderEntryXml(xmlTextWriter, "entry");
				}
				xmlTextWriter.WriteFullEndElement();
			}
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00041E6C File Offset: 0x0004006C
		private void Parse(XmlTextReader reader)
		{
			try
			{
				reader.WhitespaceHandling = WhitespaceHandling.All;
				this.state = FolderMruCache.XmlParseState.Start;
				while (this.state != FolderMruCache.XmlParseState.Finished && reader.Read())
				{
					switch (this.state)
					{
					case FolderMruCache.XmlParseState.Start:
						this.ParseStart(reader);
						break;
					case FolderMruCache.XmlParseState.Root:
						this.ParseRoot(reader);
						break;
					case FolderMruCache.XmlParseState.Child:
						this.ParseChild(reader);
						break;
					}
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Parser threw an XML exception: {0}'", ex.Message);
				this.ClearCache();
				this.Commit();
			}
			catch (OwaFolderMruParserException ex2)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "Mru parser threw an exception: {0}'", ex2.Message);
				this.ClearCache();
				this.Commit();
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00041F38 File Offset: 0x00040138
		private void ParseStart(XmlTextReader reader)
		{
			if (XmlNodeType.Element != reader.NodeType || string.CompareOrdinal("FolderMruCache", reader.Name) != 0)
			{
				this.ThrowParserException(reader);
				return;
			}
			if (reader.IsEmptyElement)
			{
				this.state = FolderMruCache.XmlParseState.Finished;
				return;
			}
			this.state = FolderMruCache.XmlParseState.Root;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00041F74 File Offset: 0x00040174
		private void ParseRoot(XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.Element)
			{
				if (reader.IsEmptyElement)
				{
					this.ThrowParserException(reader);
					return;
				}
				if (string.CompareOrdinal("entry", reader.Name) != 0)
				{
					this.ThrowParserException(reader);
					return;
				}
				if (this.firstEmptyIndex != -1)
				{
					FolderMruCacheEntry folderMruCacheEntry = new FolderMruCacheEntry();
					folderMruCacheEntry.ParseEntry(reader, this.userContext);
					this.cacheEntries[this.firstEmptyIndex] = folderMruCacheEntry;
					this.state = FolderMruCache.XmlParseState.Child;
					if (this.firstEmptyIndex < 19 && this.firstEmptyIndex != -1)
					{
						this.firstEmptyIndex++;
						this.cacheLength = this.firstEmptyIndex;
						return;
					}
					this.firstEmptyIndex = -1;
					this.cacheLength = 20;
					return;
				}
			}
			else
			{
				if (reader.NodeType == XmlNodeType.EndElement && string.CompareOrdinal("FolderMruCache", reader.Name) == 0)
				{
					this.state = FolderMruCache.XmlParseState.Finished;
					return;
				}
				this.ThrowParserException(reader);
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00042054 File Offset: 0x00040254
		private void ParseChild(XmlTextReader reader)
		{
			if (reader.NodeType == XmlNodeType.EndElement && string.CompareOrdinal(reader.Name, "entry") == 0)
			{
				this.state = FolderMruCache.XmlParseState.Root;
				return;
			}
			this.ThrowParserException(reader);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00042084 File Offset: 0x00040284
		private void ThrowParserException(XmlTextReader reader)
		{
			string text = null;
			string message = string.Format(CultureInfo.InvariantCulture, "Mru Parser. Invalid request. Line number: {0} Position: {1}.{2}", new object[]
			{
				reader.LineNumber.ToString(CultureInfo.InvariantCulture),
				reader.LinePosition.ToString(CultureInfo.InvariantCulture),
				(text != null) ? (" " + text) : string.Empty
			});
			throw new OwaFolderMruParserException(message, null, this);
		}

		// Token: 0x0400069D RID: 1693
		public const short CacheSize = 20;

		// Token: 0x0400069E RID: 1694
		private const string ConfigurationName = "OWA.FolderMruCache";

		// Token: 0x0400069F RID: 1695
		private const string MruParamName = "FolderMruCache";

		// Token: 0x040006A0 RID: 1696
		private const string MruEntryName = "entry";

		// Token: 0x040006A1 RID: 1697
		private UserContext userContext;

		// Token: 0x040006A2 RID: 1698
		private FolderMruCacheEntry[] cacheEntries;

		// Token: 0x040006A3 RID: 1699
		private int firstEmptyIndex;

		// Token: 0x040006A4 RID: 1700
		private int cacheLength;

		// Token: 0x040006A5 RID: 1701
		private FolderMruCache.XmlParseState state;

		// Token: 0x02000114 RID: 276
		private enum XmlParseState
		{
			// Token: 0x040006A7 RID: 1703
			Start,
			// Token: 0x040006A8 RID: 1704
			Root,
			// Token: 0x040006A9 RID: 1705
			Child,
			// Token: 0x040006AA RID: 1706
			Finished
		}
	}
}
