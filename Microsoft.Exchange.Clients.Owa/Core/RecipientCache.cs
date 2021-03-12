using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000CD RID: 205
	internal abstract class RecipientCache
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0003841C File Offset: 0x0003661C
		protected RecipientCache(UserContext userContext, short cacheSize, UserConfiguration configuration)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
			this.cacheSize = cacheSize;
			this.cacheEntries = new List<RecipientInfoCacheEntry>((int)cacheSize);
			if (userContext.CanActAsOwner)
			{
				this.Load(configuration);
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0003846E File Offset: 0x0003666E
		public static bool RunGetCacheOperationUnderDefaultExceptionHandler(RecipientCache.GetCacheOperation operation, int hashCode)
		{
			return RecipientCache.RunGetCacheOperationUnderExceptionHandler(operation, new RecipientCache.ExceptionHandler(RecipientCache.HandleGetCacheException), hashCode);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00038484 File Offset: 0x00036684
		public static bool RunGetCacheOperationUnderExceptionHandler(RecipientCache.GetCacheOperation operation, RecipientCache.ExceptionHandler exceptionHandler, int hashCode)
		{
			try
			{
				operation();
				return true;
			}
			catch (DataSourceTransientException e)
			{
				exceptionHandler(e, hashCode);
			}
			catch (DataSourceOperationException e2)
			{
				exceptionHandler(e2, hashCode);
			}
			catch (StorageTransientException e3)
			{
				exceptionHandler(e3, hashCode);
			}
			catch (StoragePermanentException e4)
			{
				exceptionHandler(e4, hashCode);
			}
			catch (MapiRetryableException e5)
			{
				exceptionHandler(e5, hashCode);
			}
			catch (MapiPermanentException e6)
			{
				exceptionHandler(e6, hashCode);
			}
			return false;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00038534 File Offset: 0x00036734
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x0003853C File Offset: 0x0003673C
		public int CacheLength
		{
			get
			{
				return this.cacheEntries.Count;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00038549 File Offset: 0x00036749
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00038551 File Offset: 0x00036751
		public List<RecipientInfoCacheEntry> CacheEntries
		{
			get
			{
				return this.cacheEntries;
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00038559 File Offset: 0x00036759
		public void Sort()
		{
			this.cacheEntries.Sort(new RecipientCache.UsageSort());
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0003856B File Offset: 0x0003676B
		public void SortByDisplayName()
		{
			this.cacheEntries.Sort(new RecipientCache.DisplayNameSort());
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00038580 File Offset: 0x00036780
		public void AddEntry(string displayName, string smtpAddress, string routingAddress, string alias, string routingType, AddressOrigin addressOrigin, int recipientFlags, string itemId, EmailAddressIndex emailAddressIndex)
		{
			this.AddEntry(displayName, smtpAddress, routingAddress, alias, routingType, addressOrigin, recipientFlags, itemId, emailAddressIndex, null, null);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000385A4 File Offset: 0x000367A4
		public void AddEntry(string displayName, string smtpAddress, string routingAddress, string alias, string routingType, AddressOrigin addressOrigin, int recipientFlags, string itemId, EmailAddressIndex emailAddressIndex, string sipUri, string mobilePhoneNumber)
		{
			if (!Utilities.IsMapiPDL(routingType) && string.IsNullOrEmpty(routingAddress))
			{
				return;
			}
			this.AddEntry(new RecipientInfoCacheEntry(displayName, smtpAddress, routingAddress, alias, routingType, addressOrigin, recipientFlags, itemId, emailAddressIndex, sipUri, mobilePhoneNumber));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000385E4 File Offset: 0x000367E4
		public virtual void AddEntry(RecipientInfoCacheEntry newEntry)
		{
			if (newEntry == null)
			{
				throw new ArgumentNullException("newEntry");
			}
			int num;
			if (Utilities.IsMapiPDL(newEntry.RoutingType))
			{
				num = this.GetEntryIndexByItemId(newEntry.ItemId);
			}
			else
			{
				num = this.GetEntryIndexByEmail(newEntry.RoutingAddress);
			}
			int num2;
			if (num == -1)
			{
				if (this.cacheEntries.Count >= (int)this.cacheSize)
				{
					num2 = this.GetLeastPriorityEntryIndex();
					this.cacheEntries[num2] = newEntry;
				}
				else
				{
					num2 = this.cacheEntries.Count;
					this.cacheEntries.Add(newEntry);
				}
				newEntry.SetSessionFlag();
			}
			else
			{
				num2 = num;
				this.UpdateEntry(newEntry, num2);
			}
			this.cacheEntries[num2].UpdateTimeStamp();
			this.isDirty = true;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0003869B File Offset: 0x0003689B
		private static void HandleGetCacheException(Exception e, int hashCode)
		{
			ExTraceGlobals.CoreCallTracer.TraceError<string>((long)hashCode, "Failed to get cache from server. Exception {0}", e.Message);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000386B4 File Offset: 0x000368B4
		private void UpdateEntry(RecipientInfoCacheEntry newEntry, int oldEntryIndex)
		{
			RecipientInfoCacheEntry recipientInfoCacheEntry = this.cacheEntries[oldEntryIndex];
			recipientInfoCacheEntry.IncrementUsage();
			recipientInfoCacheEntry.SetSessionFlag();
			if (!string.Equals(newEntry.DisplayName, newEntry.RoutingAddress))
			{
				recipientInfoCacheEntry.DisplayName = newEntry.DisplayName;
			}
			if (newEntry.Alias.Length > 0)
			{
				recipientInfoCacheEntry.Alias = newEntry.Alias;
			}
			recipientInfoCacheEntry.RoutingType = newEntry.RoutingType;
			recipientInfoCacheEntry.ItemId = newEntry.ItemId;
			recipientInfoCacheEntry.EmailAddressIndex = newEntry.EmailAddressIndex;
			recipientInfoCacheEntry.AddressOrigin = newEntry.AddressOrigin;
			recipientInfoCacheEntry.SipUri = newEntry.SipUri;
			recipientInfoCacheEntry.MobilePhoneNumber = newEntry.MobilePhoneNumber;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0003875C File Offset: 0x0003695C
		private int GetEntryIndexByEmail(string routingAddress)
		{
			if (string.IsNullOrEmpty(routingAddress))
			{
				return -1;
			}
			for (int i = 0; i < this.cacheEntries.Count; i++)
			{
				if (string.Equals(this.cacheEntries[i].RoutingAddress, routingAddress, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000387A8 File Offset: 0x000369A8
		private int GetEntryIndexByItemId(string itemId)
		{
			if (string.IsNullOrEmpty(itemId))
			{
				return -1;
			}
			for (int i = 0; i < this.cacheEntries.Count; i++)
			{
				if (string.CompareOrdinal(this.cacheEntries[i].ItemId, itemId) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000387F4 File Offset: 0x000369F4
		private int GetLeastPriorityEntryIndex()
		{
			IComparer<RecipientInfoCacheEntry> comparer = new RecipientCache.UsageSort();
			int num = 0;
			for (int i = 1; i < this.cacheEntries.Count; i++)
			{
				if (comparer.Compare(this.cacheEntries[i], this.cacheEntries[num]) > 0)
				{
					num = i;
				}
			}
			return num;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00038843 File Offset: 0x00036A43
		private void ShiftBackEntries(int index)
		{
			this.cacheEntries.RemoveAt(index);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00038854 File Offset: 0x00036A54
		internal void RemoveEntry(string email, string id)
		{
			if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(id))
			{
				throw new OwaInvalidRequestException("email and id can not be both empty");
			}
			int num;
			if (!string.IsNullOrEmpty(email))
			{
				num = this.GetEntryIndexByEmail(email);
			}
			else
			{
				num = this.GetEntryIndexByItemId(id);
			}
			if (num == -1)
			{
				return;
			}
			this.ShiftBackEntries(num);
			this.isDirty = true;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000388AC File Offset: 0x00036AAC
		protected void FinishSession(RecipientCache backEndCache, UserConfiguration configuration)
		{
			this.Merge(backEndCache);
			foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in this.cacheEntries)
			{
				recipientInfoCacheEntry.Decay();
			}
			this.Commit(configuration);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0003890C File Offset: 0x00036B0C
		internal void StartCacheSession()
		{
			foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in this.cacheEntries)
			{
				recipientInfoCacheEntry.ClearSessionFlag();
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00038960 File Offset: 0x00036B60
		public void ClearCache()
		{
			this.cacheEntries.Clear();
			this.isDirty = true;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00038974 File Offset: 0x00036B74
		protected void Merge(RecipientCache externalRecipientCache)
		{
			if (externalRecipientCache == null)
			{
				return;
			}
			if (!(externalRecipientCache.syncTime > this.syncTime))
			{
				return;
			}
			int cacheLength = externalRecipientCache.CacheLength;
			int count = this.cacheEntries.Count;
			if (cacheLength <= 0)
			{
				return;
			}
			int num = cacheLength + count;
			List<RecipientInfoCacheEntry> list = new List<RecipientInfoCacheEntry>((num > (int)this.cacheSize) ? num : ((int)this.cacheSize));
			IComparer<RecipientInfoCacheEntry> comparer = new RecipientCache.PrimaryKeySort();
			this.cacheEntries.Sort(comparer);
			externalRecipientCache.cacheEntries.Sort(comparer);
			int num2 = 0;
			int num3 = 0;
			while (num2 < count || num3 < cacheLength)
			{
				RecipientInfoCacheEntry recipientInfoCacheEntry;
				if (num2 < count)
				{
					recipientInfoCacheEntry = this.CacheEntries[num2];
				}
				else
				{
					recipientInfoCacheEntry = null;
				}
				RecipientInfoCacheEntry recipientInfoCacheEntry2;
				if (num3 < cacheLength)
				{
					recipientInfoCacheEntry2 = externalRecipientCache.CacheEntries[num3];
				}
				else
				{
					recipientInfoCacheEntry2 = null;
				}
				int num4 = comparer.Compare(recipientInfoCacheEntry, recipientInfoCacheEntry2);
				int num5 = 0;
				if (recipientInfoCacheEntry != null && recipientInfoCacheEntry2 != null)
				{
					num5 = (int)(Convert.ToByte(Utilities.IsMapiPDL(recipientInfoCacheEntry.RoutingType)) | Convert.ToByte(Utilities.IsMapiPDL(recipientInfoCacheEntry2.RoutingType)));
				}
				if (num4 < 0 || num5 == 1)
				{
					if (recipientInfoCacheEntry.IsSessionFlagSet())
					{
						list.Add(recipientInfoCacheEntry);
					}
					num2++;
				}
				else if (num4 == 0)
				{
					short sessionCount = Math.Max(recipientInfoCacheEntry.SessionCount, recipientInfoCacheEntry2.SessionCount);
					if (recipientInfoCacheEntry2.DateTimeTicks > recipientInfoCacheEntry.DateTimeTicks)
					{
						this.UpdateEntry(recipientInfoCacheEntry2, num2);
						list.Add(this.cacheEntries[num2]);
					}
					else
					{
						list.Add(recipientInfoCacheEntry);
					}
					list[list.Count - 1].SessionCount = sessionCount;
					num2++;
					num3++;
				}
				else
				{
					list.Add(recipientInfoCacheEntry2);
					num3++;
				}
			}
			list.Sort(new RecipientCache.UsageSort());
			if (num > (int)this.cacheSize && list.Count > (int)this.cacheSize)
			{
				this.cacheEntries = list.GetRange(0, (int)this.cacheSize);
				return;
			}
			this.cacheEntries = list;
		}

		// Token: 0x06000758 RID: 1880
		public abstract void Commit(bool mergeBeforeCommit);

		// Token: 0x06000759 RID: 1881 RVA: 0x00038B6F File Offset: 0x00036D6F
		internal virtual void Commit(RecipientCache backEndRecipientCache, UserConfiguration configuration)
		{
			this.Merge(backEndRecipientCache);
			this.Commit(configuration);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00038B7F File Offset: 0x00036D7F
		protected virtual void Commit(UserConfiguration configuration)
		{
			this.Commit(configuration, false);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00038B8C File Offset: 0x00036D8C
		protected virtual void Commit(UserConfiguration configuration, bool forceSave)
		{
			if (this.userContext.CanActAsOwner)
			{
				if (this.cacheEntries.Count == 0 && !this.isDirty && !forceSave)
				{
					return;
				}
				using (RecipientInfoCache recipientInfoCache = RecipientInfoCache.Create(configuration))
				{
					try
					{
						recipientInfoCache.Save(this.cacheEntries, "AutoCompleteCache", (int)this.cacheSize);
					}
					finally
					{
						recipientInfoCache.DetachUserConfiguration();
					}
				}
			}
			this.isDirty = false;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00038C18 File Offset: 0x00036E18
		private void Load(UserConfiguration configuration)
		{
			try
			{
				using (RecipientInfoCache recipientInfoCache = RecipientInfoCache.Create(configuration))
				{
					try
					{
						this.cacheEntries = recipientInfoCache.Load("AutoCompleteCache");
						this.syncTime = recipientInfoCache.LastModifiedTime;
					}
					finally
					{
						recipientInfoCache.DetachUserConfiguration();
					}
				}
			}
			catch (XmlException ex)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "Parser threw an XML exception: {0}'", ex.Message);
				this.ClearCache();
				this.Commit(configuration, true);
			}
			catch (CorruptDataException ex2)
			{
				ExTraceGlobals.CoreTracer.TraceError<string>(0L, "Parser threw a CorruptDataException exception: {0}'", ex2.Message);
				this.ClearCache();
				this.Commit(configuration, true);
			}
		}

		// Token: 0x04000509 RID: 1289
		private const string AutoCompleteParamName = "AutoCompleteCache";

		// Token: 0x0400050A RID: 1290
		private const string AutoCompleteEntryName = "entry";

		// Token: 0x0400050B RID: 1291
		private const string AutoCompleteCacheVersionName = "version";

		// Token: 0x0400050C RID: 1292
		private short cacheSize = 100;

		// Token: 0x0400050D RID: 1293
		private bool isDirty;

		// Token: 0x0400050E RID: 1294
		private UserContext userContext;

		// Token: 0x0400050F RID: 1295
		private List<RecipientInfoCacheEntry> cacheEntries;

		// Token: 0x04000510 RID: 1296
		private ExDateTime syncTime;

		// Token: 0x020000CE RID: 206
		// (Invoke) Token: 0x0600075E RID: 1886
		public delegate void GetCacheOperation();

		// Token: 0x020000CF RID: 207
		// (Invoke) Token: 0x06000762 RID: 1890
		public delegate void ExceptionHandler(Exception e, int hashCode);

		// Token: 0x020000D0 RID: 208
		private class DisplayNameSort : IComparer<RecipientInfoCacheEntry>
		{
			// Token: 0x06000765 RID: 1893 RVA: 0x00038CE8 File Offset: 0x00036EE8
			public int Compare(RecipientInfoCacheEntry x, RecipientInfoCacheEntry y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (x.DisplayName != null && y.DisplayName != null)
				{
					return x.DisplayName.CompareTo(y.DisplayName);
				}
				if (x.DisplayName != null && y.DisplayName == null)
				{
					return 1;
				}
				if (x.DisplayName == null && y.DisplayName != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x020000D1 RID: 209
		private class PrimaryKeySort : IComparer<RecipientInfoCacheEntry>
		{
			// Token: 0x06000767 RID: 1895 RVA: 0x00038D5C File Offset: 0x00036F5C
			public int Compare(RecipientInfoCacheEntry x, RecipientInfoCacheEntry y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				string text = x.RoutingAddress;
				string text2 = y.RoutingAddress;
				if (Utilities.IsMapiPDL(x.RoutingType))
				{
					text = x.ItemId;
				}
				if (Utilities.IsMapiPDL(y.RoutingType))
				{
					text2 = y.ItemId;
				}
				if (text != null && text2 != null)
				{
					return string.CompareOrdinal(text, text2);
				}
				if (text != null && text2 == null)
				{
					return -1;
				}
				if (text == null && text2 != null)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x020000D2 RID: 210
		private class UsageSort : IComparer<RecipientInfoCacheEntry>
		{
			// Token: 0x06000769 RID: 1897 RVA: 0x00038DDC File Offset: 0x00036FDC
			public int Compare(RecipientInfoCacheEntry x, RecipientInfoCacheEntry y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return 1;
				}
				if (y == null)
				{
					return -1;
				}
				if (y.Usage != x.Usage)
				{
					return y.Usage.CompareTo(x.Usage);
				}
				if (y.DateTimeTicks != x.DateTimeTicks)
				{
					return y.DateTimeTicks.CompareTo(x.DateTimeTicks);
				}
				return x.DisplayName.CompareTo(y.DisplayName);
			}
		}
	}
}
