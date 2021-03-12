using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001FA RID: 506
	internal class ADRecipientCache<TEntry> : IADRecipientCache, IEnumerable<KeyValuePair<ProxyAddress, Result<ADRawEntry>>>, IEnumerable, IADRecipientCache<TEntry> where TEntry : ADRawEntry, new()
	{
		// Token: 0x06001A61 RID: 6753 RVA: 0x0006DC15 File Offset: 0x0006BE15
		public ADRecipientCache(ADPropertyDefinition[] properties) : this(properties, 0)
		{
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0006DC20 File Offset: 0x0006BE20
		public ADRecipientCache(ADPropertyDefinition[] properties, int capacity)
		{
			this.adSessionLock = new object();
			this.orgId = OrganizationId.ForestWideOrgId;
			this.dictionaryLock = new object();
			base..ctor();
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i] == null)
				{
					throw new ArgumentException("Null property definition is not allowed!", "properties");
				}
			}
			if (typeof(TEntry) != typeof(ADRawEntry) && typeof(TEntry) != typeof(TransportMiniRecipient))
			{
				throw new ArgumentException("This class can only be used with ADRawEntry or TransportMiniRecipient generic parameter values.");
			}
			TEntry tentry = Activator.CreateInstance<TEntry>();
			this.isFullADRecipientObject = (tentry is ADRecipient);
			this.dictionary = new Dictionary<ProxyAddress, Result<TEntry>>(capacity);
			this.properties = properties;
			this.propertyCollection = new ReadOnlyCollection<ADPropertyDefinition>(this.properties);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0006DD08 File Offset: 0x0006BF08
		public ADRecipientCache(IRecipientSession adSession, ADPropertyDefinition[] properties, int capacity) : this(properties, capacity)
		{
			this.adSession = adSession;
			if (adSession != null)
			{
				this.orgId = null;
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0006DD23 File Offset: 0x0006BF23
		public ADRecipientCache(IRecipientSession adSession, ADPropertyDefinition[] properties, int capacity, OrganizationId orgId) : this(properties, capacity)
		{
			if (adSession == null)
			{
				throw new ArgumentNullException("adSession");
			}
			this.adSession = adSession;
			this.orgId = orgId;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0006DD4A File Offset: 0x0006BF4A
		public ADRecipientCache(ADPropertyDefinition[] properties, int capacity, OrganizationId orgId) : this(properties, capacity)
		{
			if (!OrganizationId.ForestWideOrgId.Equals(orgId))
			{
				this.orgId = orgId;
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0006DD68 File Offset: 0x0006BF68
		protected ADRecipientCache()
		{
			this.adSessionLock = new object();
			this.orgId = OrganizationId.ForestWideOrgId;
			this.dictionaryLock = new object();
			base..ctor();
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0006DD91 File Offset: 0x0006BF91
		public virtual OrganizationId OrganizationId
		{
			get
			{
				return this.orgId;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0006DD9C File Offset: 0x0006BF9C
		public virtual IRecipientSession ADSession
		{
			get
			{
				if (this.adSession == null && this.orgId != null)
				{
					lock (this.adSessionLock)
					{
						if (this.adSession == null)
						{
							this.adSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.orgId), 532, "ADSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipientCache.cs");
						}
					}
				}
				if (this.adSession != null)
				{
					return this.adSession;
				}
				if (ADRecipientCache<ADRawEntry>.defaultADSession == null)
				{
					ADRecipientCache<TEntry>.defaultADSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(OrganizationId.ForestWideOrgId), 548, "ADSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Recipient\\ADRecipientCache.cs");
				}
				return ADRecipientCache<TEntry>.defaultADSession;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0006DE64 File Offset: 0x0006C064
		public virtual ReadOnlyCollection<ADPropertyDefinition> CachedADProperties
		{
			get
			{
				return this.propertyCollection;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0006DE6C File Offset: 0x0006C06C
		public virtual int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0006DE79 File Offset: 0x0006C079
		public virtual ICollection<ProxyAddress> Keys
		{
			get
			{
				return this.dictionary.Keys;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0006DE88 File Offset: 0x0006C088
		public virtual ICollection<ProxyAddress> ClonedKeys
		{
			get
			{
				ICollection<ProxyAddress> result;
				lock (this.dictionaryLock)
				{
					result = this.dictionary.Keys.ToArray<ProxyAddress>();
				}
				return result;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0006DED4 File Offset: 0x0006C0D4
		public virtual ICollection<Result<TEntry>> Values
		{
			get
			{
				return this.dictionary.Values;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0006E098 File Offset: 0x0006C298
		IEnumerable<Result<ADRawEntry>> IADRecipientCache.Values
		{
			get
			{
				foreach (Result<TEntry> value in this.dictionary.Values)
				{
					Result<TEntry> result = value;
					ADRawEntry data = result.Data;
					Result<TEntry> result2 = value;
					yield return new Result<ADRawEntry>(data, result2.Error);
				}
				yield break;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0006E0B5 File Offset: 0x0006C2B5
		private static MSExchangeADRecipientCacheInstance PerfCounters
		{
			get
			{
				return ADRecipientCache<TEntry>.perfCounters;
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0006E0BC File Offset: 0x0006C2BC
		public static void InitializePerfCounters(string instanceName)
		{
			ADRecipientCache<TEntry>.perfCounters = MSExchangeADRecipientCache.GetInstance(instanceName);
			ADRecipientCache<TEntry>.numberOfQueriesPercentileCounter = new AggregatingPercentileCounter(1L, 100L);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0006E0D8 File Offset: 0x0006C2D8
		public void Clear()
		{
			lock (this.dictionaryLock)
			{
				this.dictionary = new Dictionary<ProxyAddress, Result<TEntry>>();
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0006E120 File Offset: 0x0006C320
		public virtual bool ContainsKey(ProxyAddress proxyAddress)
		{
			bool result;
			lock (this.dictionaryLock)
			{
				result = this.dictionary.ContainsKey(proxyAddress);
			}
			return result;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0006E168 File Offset: 0x0006C368
		public virtual bool CopyEntryFrom(IADRecipientCache<TEntry> cacheToCopyFrom, string smtpAddress)
		{
			return SmtpAddress.IsValidSmtpAddress(smtpAddress) && this.CopyEntryFrom(cacheToCopyFrom, new SmtpProxyAddress(smtpAddress, true));
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0006E184 File Offset: 0x0006C384
		public virtual bool CopyEntryFrom(IADRecipientCache<TEntry> cacheToCopyFrom, ProxyAddress proxyAddress)
		{
			if (cacheToCopyFrom == null)
			{
				throw new ArgumentNullException("cacheToCopyFrom");
			}
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			if (!this.CachedADProperties.SequenceEqual(cacheToCopyFrom.CachedADProperties))
			{
				return false;
			}
			Result<TEntry> result;
			if (cacheToCopyFrom.TryGetValue(proxyAddress, out result))
			{
				this.AddCacheEntry(proxyAddress, new Result<TEntry>(result.Data, result.Error), false);
				return true;
			}
			return false;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0006E1F4 File Offset: 0x0006C3F4
		public virtual Result<TEntry> FindAndCacheRecipient(ProxyAddress proxyAddress)
		{
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			Result<TEntry> result;
			bool flag2;
			lock (this.dictionaryLock)
			{
				flag2 = this.dictionary.TryGetValue(proxyAddress, out result);
			}
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.AggregateHits_Base.Increment();
			}
			if (!flag2)
			{
				if (ADRecipientCache<TEntry>.PerfCounters != null)
				{
					ADRecipientCache<TEntry>.PerfCounters.AggregateMisses.Increment();
				}
				result = this.LookUpRecipientInAD(proxyAddress, this.properties);
				lock (this.dictionaryLock)
				{
					Result<TEntry> result2;
					if (this.dictionary.TryGetValue(proxyAddress, out result2))
					{
						if (ADRecipientCache<TEntry>.PerfCounters != null)
						{
							ADRecipientCache<TEntry>.PerfCounters.RepeatedQueryForTheSameRecipient.Increment();
						}
						return result2;
					}
					this.AddCacheEntry(proxyAddress, result, false, true);
				}
				return result;
			}
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.AggregateHits.Increment();
			}
			return result;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0006E40C File Offset: 0x0006C60C
		public virtual Result<TEntry> FindAndCacheRecipient(ADObjectId objectId)
		{
			if (objectId == null)
			{
				throw new ArgumentNullException("objectId");
			}
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.IndividualAddressLookupsTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.RequestsPendingTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.AggregateHits_Base.Increment();
				ADRecipientCache<TEntry>.PerfCounters.AggregateMisses.Increment();
				ADRecipientCache<TEntry>.PerfCounters.AggregateLookupsTotal.Increment();
				this.IncrementQueriesPerCacheCounter();
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			Result<TEntry> result;
			try
			{
				TEntry entry = default(TEntry);
				ADNotificationAdapter.RunADOperation(delegate()
				{
					if (typeof(TEntry) == typeof(TransportMiniRecipient))
					{
						entry = (this.ADSession.ReadMiniRecipient<TransportMiniRecipient>(objectId, this.properties) as TEntry);
						return;
					}
					if (this.isFullADRecipientObject)
					{
						entry = (TEntry)((object)this.ADSession.Read(objectId));
						return;
					}
					entry = (TEntry)((object)this.ADSession.ReadADRawEntry(objectId, this.properties));
				});
				if (entry == null)
				{
					result = new Result<TEntry>(default(TEntry), ProviderError.NotFound);
				}
				else
				{
					result = new Result<TEntry>(entry, null);
				}
			}
			catch (DataValidationException ex)
			{
				ComponentTrace<ADRecipientCacheTags>.TraceError<DataValidationException>(0, -1L, "DataValidationException: {0}", ex);
				result = new Result<TEntry>(default(TEntry), ex.Error);
			}
			finally
			{
				stopwatch.Stop();
				if (ADRecipientCache<TEntry>.PerfCounters != null)
				{
					ADRecipientCache<TEntry>.PerfCounters.AverageLookupQueryLatency.IncrementBy(stopwatch.ElapsedMilliseconds);
				}
				ADRecipientCache<TEntry>.DecrementPendingRequestsCounter();
			}
			if (result.Data != null)
			{
				ProxyAddress primarySmtpAddress = ADRecipientCache<TEntry>.GetPrimarySmtpAddress(result.Data);
				if (primarySmtpAddress != null)
				{
					this.AddCacheEntry(primarySmtpAddress, result);
				}
			}
			return result;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x0006E6D4 File Offset: 0x0006C8D4
		public virtual IList<Result<TEntry>> FindAndCacheRecipients(IList<ProxyAddress> proxyAddressList)
		{
			if (proxyAddressList == null)
			{
				throw new ArgumentNullException("proxyAddressList");
			}
			List<Result<TEntry>> list = new List<Result<TEntry>>(proxyAddressList.Count);
			List<ProxyAddress> proxies = new List<ProxyAddress>(proxyAddressList.Count);
			List<int> list2 = new List<int>(proxyAddressList.Count);
			lock (this.dictionaryLock)
			{
				int num = 0;
				foreach (ProxyAddress proxyAddress in proxyAddressList)
				{
					Result<TEntry> item = new Result<TEntry>(default(TEntry), null);
					if (null == proxyAddress || this.dictionary.TryGetValue(proxyAddress, out item))
					{
						list.Add(item);
					}
					else
					{
						list.Add(item);
						proxies.Add(proxyAddress);
						list2.Add(num);
					}
					num++;
				}
			}
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.AggregateHits_Base.Increment();
				if (list2.Count == 0)
				{
					ADRecipientCache<TEntry>.PerfCounters.AggregateHits.Increment();
				}
				else
				{
					int num2 = list2.Count / ADRecipientCache<TEntry>.BatchSize;
					if (list2.Count % ADRecipientCache<TEntry>.BatchSize != 0)
					{
						num2++;
					}
					ADRecipientCache<TEntry>.PerfCounters.AggregateMisses.IncrementBy((long)num2);
				}
			}
			if (proxies.Count == 0)
			{
				return list;
			}
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.BatchedAddressLookupsTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.RequestsPendingTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.AggregateLookupsTotal.Increment();
				this.IncrementQueriesPerCacheCounter();
			}
			Result<TEntry>[] rawResults = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				int i;
				ADNotificationAdapter.RunADOperation(delegate()
				{
					if (typeof(TEntry) == typeof(ADRawEntry))
					{
						Result<ADRawEntry>[] source = this.ADSession.FindByProxyAddresses(proxies.ToArray(), this.properties);
						rawResults = (from i in source
						select new Result<TEntry>((TEntry)((object)i.Data), i.Error)).ToArray<Result<TEntry>>();
						return;
					}
					if (typeof(TEntry) == typeof(TransportMiniRecipient))
					{
						Result<TransportMiniRecipient>[] source2 = this.ADSession.FindByProxyAddresses<TransportMiniRecipient>(proxies.ToArray());
						rawResults = (from i in source2
						select new Result<TEntry>(i.Data as TEntry, i.Error)).ToArray<Result<TEntry>>();
						return;
					}
					throw new NotSupportedException();
				});
			}
			finally
			{
				stopwatch.Stop();
				if (ADRecipientCache<TEntry>.PerfCounters != null)
				{
					ADRecipientCache<TEntry>.PerfCounters.AverageLookupQueryLatency.IncrementBy(stopwatch.ElapsedMilliseconds);
				}
				ADRecipientCache<TEntry>.DecrementPendingRequestsCounter();
			}
			for (int i = 0; i < rawResults.Length; i++)
			{
				if (rawResults[i].Data != null)
				{
					this.PopulateCalculatedProperties(rawResults[i].Data);
				}
				list[list2[i]] = rawResults[i];
			}
			bool flag2 = false;
			lock (this.dictionaryLock)
			{
				for (int j = 0; j < proxies.Count; j++)
				{
					Result<TEntry> value;
					if (proxies[j] != null && this.dictionary.TryGetValue(proxies[j], out value))
					{
						list[list2[j]] = value;
						flag2 = true;
					}
					else
					{
						this.AddCacheEntry(proxies[j], list[list2[j]], false, false);
					}
				}
			}
			if (flag2 && ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.RepeatedQueryForTheSameRecipient.Increment();
			}
			return list;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0006EA4C File Offset: 0x0006CC4C
		public Dictionary<ProxyAddress, Result<TEntry>>.Enumerator GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0006EA5C File Offset: 0x0006CC5C
		public virtual bool Remove(ProxyAddress proxyAddress)
		{
			bool result2;
			lock (this.dictionaryLock)
			{
				Result<TEntry> result;
				if (this.dictionary.TryGetValue(proxyAddress, out result))
				{
					this.dictionary.Remove(proxyAddress);
					if (result.Data == null)
					{
						result2 = true;
					}
					else
					{
						if (ADRecipientCache<TEntry>.IsExAddress(proxyAddress))
						{
							this.dictionary.Remove(ADRecipientCache<TEntry>.GetPrimarySmtpAddress(result.Data));
						}
						else if (ADRecipientCache<TEntry>.IsSmtpAddress(proxyAddress))
						{
							this.dictionary.Remove(ProxyAddress.Parse(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, ADRecipientCache<TEntry>.GetLegacyExchangeDN(result.Data)));
						}
						result2 = true;
					}
				}
				else
				{
					result2 = false;
				}
			}
			return result2;
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x0006EB20 File Offset: 0x0006CD20
		public virtual bool TryGetValue(ProxyAddress proxyAddress, out Result<TEntry> result)
		{
			bool result2;
			lock (this.dictionaryLock)
			{
				result2 = this.dictionary.TryGetValue(proxyAddress, out result);
			}
			return result2;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x0006EB6C File Offset: 0x0006CD6C
		public virtual void AddCacheEntry(ProxyAddress proxyAddress, Result<TEntry> result)
		{
			this.AddCacheEntry(proxyAddress, result, true, true);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0006EDA4 File Offset: 0x0006CFA4
		public virtual IEnumerable<TRecipient> ExpandGroup<TRecipient>(IADDistributionList group) where TRecipient : MiniRecipient, new()
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.ExpandGroupRequestsTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.RequestsPendingTotal.Increment();
			}
			try
			{
				foreach (TRecipient entry in group.Expand<TRecipient>(1000, this.properties))
				{
					yield return entry;
				}
			}
			finally
			{
				ADRecipientCache<TEntry>.DecrementPendingRequestsCounter();
			}
			yield break;
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006EDC8 File Offset: 0x0006CFC8
		public virtual Result<TEntry> ReadSecurityDescriptor(ProxyAddress proxyAddress)
		{
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			Result<TEntry> result = this.FindAndCacheRecipient(proxyAddress);
			if (result.Data != null)
			{
				IDirectorySession adsession = this.ADSession;
				TEntry data = result.Data;
				RawSecurityDescriptor rawSecurityDescriptor = adsession.ReadSecurityDescriptor(data.Id);
				lock (result.Data)
				{
					result.Data.propertyBag.SetField(ADObjectSchema.NTSecurityDescriptor, SecurityDescriptor.FromRawSecurityDescriptor(rawSecurityDescriptor));
				}
			}
			return result;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006EE78 File Offset: 0x0006D078
		public virtual void DropSecurityDescriptor(ProxyAddress proxyAddress)
		{
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			Result<TEntry> result;
			if (this.TryGetValue(proxyAddress, out result))
			{
				lock (result.Data)
				{
					result.Data.propertyBag.SetField(ADObjectSchema.NTSecurityDescriptor, null);
				}
			}
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0006EEF4 File Offset: 0x0006D0F4
		public virtual Result<TEntry> ReloadRecipient(ProxyAddress proxyAddress, IEnumerable<ADPropertyDefinition> extraProperties)
		{
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			if (extraProperties == null)
			{
				throw new ArgumentNullException("extraProperties");
			}
			Result<TEntry> result = this.FindAndCacheRecipient(proxyAddress);
			if (result.Data != null)
			{
				IDirectorySession adsession = this.ADSession;
				TEntry data = result.Data;
				ADRawEntry adrawEntry = adsession.ReadADRawEntry(data.Id, extraProperties);
				lock (result.Data)
				{
					foreach (ADPropertyDefinition adpropertyDefinition in extraProperties)
					{
						result.Data.propertyBag.SetField(adpropertyDefinition, adrawEntry[adpropertyDefinition]);
					}
				}
			}
			return result;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0006EFEC File Offset: 0x0006D1EC
		Result<ADRawEntry> IADRecipientCache.FindAndCacheRecipient(ADObjectId objectId)
		{
			Result<TEntry> result = this.FindAndCacheRecipient(objectId);
			return new Result<ADRawEntry>(result.Data, result.Error);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0006F01C File Offset: 0x0006D21C
		Result<ADRawEntry> IADRecipientCache.FindAndCacheRecipient(ProxyAddress proxyAddress)
		{
			Result<TEntry> result = this.FindAndCacheRecipient(proxyAddress);
			return new Result<ADRawEntry>(result.Data, result.Error);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0006F064 File Offset: 0x0006D264
		IList<Result<ADRawEntry>> IADRecipientCache.FindAndCacheRecipients(IList<ProxyAddress> proxyAddressList)
		{
			IList<Result<TEntry>> source = this.FindAndCacheRecipients(proxyAddressList);
			return (from i in source
			select new Result<ADRawEntry>(i.Data, i.Error)).ToList<Result<ADRawEntry>>();
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0006F0A1 File Offset: 0x0006D2A1
		void IADRecipientCache.AddCacheEntry(ProxyAddress proxyAddress, Result<ADRawEntry> result)
		{
			this.AddCacheEntry(proxyAddress, new Result<TEntry>((TEntry)((object)result.Data), result.Error));
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0006F0C2 File Offset: 0x0006D2C2
		bool IADRecipientCache.CopyEntryFrom(IADRecipientCache cacheToCopyFrom, string smtpAddress)
		{
			return SmtpAddress.IsValidSmtpAddress(smtpAddress) && ((IADRecipientCache)this).CopyEntryFrom(cacheToCopyFrom, new SmtpProxyAddress(smtpAddress, true));
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0006F0DC File Offset: 0x0006D2DC
		bool IADRecipientCache.CopyEntryFrom(IADRecipientCache cacheToCopyFrom, ProxyAddress proxyAddress)
		{
			if (cacheToCopyFrom == null)
			{
				throw new ArgumentNullException("cacheToCopyFrom");
			}
			if (proxyAddress == null)
			{
				throw new ArgumentNullException("proxyAddress");
			}
			if (!this.CachedADProperties.SequenceEqual(cacheToCopyFrom.CachedADProperties))
			{
				return false;
			}
			Result<ADRawEntry> result;
			if (cacheToCopyFrom.TryGetValue(proxyAddress, out result))
			{
				this.AddCacheEntry(proxyAddress, new Result<TEntry>((TEntry)((object)result.Data), result.Error), false);
				return true;
			}
			return false;
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0006F150 File Offset: 0x0006D350
		Result<ADRawEntry> IADRecipientCache.ReloadRecipient(ProxyAddress proxyAddress, IEnumerable<ADPropertyDefinition> extraProperties)
		{
			Result<TEntry> result = this.ReloadRecipient(proxyAddress, extraProperties);
			return new Result<ADRawEntry>(result.Data, result.Error);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0006F180 File Offset: 0x0006D380
		Result<ADRawEntry> IADRecipientCache.ReadSecurityDescriptor(ProxyAddress proxyAddress)
		{
			Result<TEntry> result = this.ReadSecurityDescriptor(proxyAddress);
			return new Result<ADRawEntry>(result.Data, result.Error);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x0006F1B0 File Offset: 0x0006D3B0
		bool IADRecipientCache.TryGetValue(ProxyAddress proxyAddress, out Result<ADRawEntry> result)
		{
			Result<TEntry> result2;
			if (this.TryGetValue(proxyAddress, out result2))
			{
				result = new Result<ADRawEntry>(result2.Data, result2.Error);
				return true;
			}
			result = new Result<ADRawEntry>(null, null);
			return false;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0006F1F6 File Offset: 0x0006D3F6
		private static bool IsSmtpAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.Smtp);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0006F208 File Offset: 0x0006D408
		private static bool IsExAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.LegacyDN);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x0006F21C File Offset: 0x0006D41C
		private static ProxyAddress GetPrimarySmtpAddress(TEntry entry)
		{
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)entry[ADRecipientSchema.EmailAddresses];
			return proxyAddressCollection.FindPrimary(ProxyAddressPrefix.Smtp);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x0006F24E File Offset: 0x0006D44E
		private static string GetLegacyExchangeDN(TEntry entry)
		{
			return (string)entry[ADRecipientSchema.LegacyExchangeDN];
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x0006F267 File Offset: 0x0006D467
		private static void DecrementPendingRequestsCounter()
		{
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.RequestsPendingTotal.Decrement();
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0006F320 File Offset: 0x0006D520
		private Result<TEntry> LookUpRecipientInAD(ProxyAddress proxyAddress, ADPropertyDefinition[] properties)
		{
			if (ADRecipientCache<TEntry>.PerfCounters != null)
			{
				ADRecipientCache<TEntry>.PerfCounters.IndividualAddressLookupsTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.RequestsPendingTotal.Increment();
				ADRecipientCache<TEntry>.PerfCounters.AggregateLookupsTotal.Increment();
				this.IncrementQueriesPerCacheCounter();
			}
			ComponentTrace<ADRecipientCacheTags>.TraceDebug<ProxyAddress>(0, -1L, "Lookup recipient {0}", proxyAddress);
			TEntry entry = default(TEntry);
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				ADNotificationAdapter.RunADOperation(delegate()
				{
					if (typeof(TEntry) == typeof(ADRawEntry))
					{
						entry = (TEntry)((object)this.ADSession.FindByProxyAddress(proxyAddress, properties));
						return;
					}
					if (typeof(TEntry) == typeof(TransportMiniRecipient))
					{
						entry = (this.ADSession.FindByProxyAddress<TransportMiniRecipient>(proxyAddress) as TEntry);
						return;
					}
					throw new NotSupportedException();
				});
				if (entry == null)
				{
					return new Result<TEntry>(default(TEntry), ProviderError.NotFound);
				}
			}
			catch (DataValidationException ex)
			{
				ComponentTrace<ADRecipientCacheTags>.TraceError<DataValidationException>(0, -1L, "DataValidationException: {0}", ex);
				return new Result<TEntry>(default(TEntry), ex.Error);
			}
			finally
			{
				stopwatch.Stop();
				if (ADRecipientCache<TEntry>.PerfCounters != null)
				{
					ADRecipientCache<TEntry>.PerfCounters.AverageLookupQueryLatency.IncrementBy(stopwatch.ElapsedMilliseconds);
				}
				ADRecipientCache<TEntry>.DecrementPendingRequestsCounter();
			}
			return new Result<TEntry>(entry, null);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0006F468 File Offset: 0x0006D668
		private void PopulateCalculatedProperties(ADRawEntry entry)
		{
			MiniRecipient miniRecipient = entry as MiniRecipient;
			bool flag = false;
			foreach (ADPropertyDefinition adpropertyDefinition in this.CachedADProperties)
			{
				if (adpropertyDefinition.IsCalculated)
				{
					if (miniRecipient == null || miniRecipient.HasSupportingProperties(adpropertyDefinition))
					{
						object obj = entry[adpropertyDefinition];
					}
					else
					{
						flag = true;
						ComponentTrace<ADRecipientCacheTags>.TraceInformation<string>(0, (long)this.GetHashCode(), "After lookup, supporting properties are missing for the calculated property: {0}.", adpropertyDefinition.Name);
					}
				}
			}
			if (flag)
			{
				ComponentTrace<ADRecipientCacheTags>.TraceWarning<string>(0, (long)this.GetHashCode(), "Supporting properties were missing for the type: {0}.", entry.GetType().Name);
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0006F518 File Offset: 0x0006D718
		private void AddCacheEntry(ProxyAddress proxyAddress, Result<TEntry> result, bool populateCalculatedProperties)
		{
			this.AddCacheEntry(proxyAddress, result, true, populateCalculatedProperties);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0006F524 File Offset: 0x0006D724
		private void AddCacheEntry(ProxyAddress proxyAddress, Result<TEntry> result, bool isLockRequired, bool populateCalculatedProperties)
		{
			TEntry data = result.Data;
			if (populateCalculatedProperties && data != null)
			{
				this.PopulateCalculatedProperties(data);
			}
			this.SetEntry(proxyAddress, result, isLockRequired);
			if (data == null)
			{
				return;
			}
			ProxyAddress primarySmtpAddress = ADRecipientCache<TEntry>.GetPrimarySmtpAddress(data);
			if (ADRecipientCache<TEntry>.IsSmtpAddress(proxyAddress))
			{
				ProxyAddress proxyAddress2 = ProxyAddress.Parse(ProxyAddressPrefix.LegacyDN.PrimaryPrefix, ADRecipientCache<TEntry>.GetLegacyExchangeDN(data));
				this.SetEntry(proxyAddress2, result, isLockRequired);
				if (null != primarySmtpAddress && primarySmtpAddress != proxyAddress)
				{
					this.SetEntry(primarySmtpAddress, result, isLockRequired);
					return;
				}
			}
			else if (primarySmtpAddress != null)
			{
				this.SetEntry(primarySmtpAddress, result, isLockRequired);
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x0006F5C0 File Offset: 0x0006D7C0
		private void SetEntry(ProxyAddress proxyAddress, Result<TEntry> result, bool isLockRequired)
		{
			if (isLockRequired)
			{
				lock (this.dictionaryLock)
				{
					this.AddRecipientCacheEntry(result, proxyAddress);
					return;
				}
			}
			this.AddRecipientCacheEntry(result, proxyAddress);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x0006F610 File Offset: 0x0006D810
		private void AddRecipientCacheEntry(Result<TEntry> result, ProxyAddress proxyAddress)
		{
			if (this.dictionary.ContainsKey(proxyAddress))
			{
				return;
			}
			this.dictionary[proxyAddress] = result;
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0006F630 File Offset: 0x0006D830
		private void IncrementQueriesPerCacheCounter()
		{
			ADRecipientCache<TEntry>.numberOfQueriesPercentileCounter.IncrementValue(ref this.numberOfLookups, 1L);
			ADRecipientCache<TEntry>.PerfCounters.NumberOfQueriesPerRecipientCache50Percentile.RawValue = ADRecipientCache<TEntry>.numberOfQueriesPercentileCounter.PercentileQuery(50.0);
			ADRecipientCache<TEntry>.PerfCounters.NumberOfQueriesPerRecipientCache80Percentile.RawValue = ADRecipientCache<TEntry>.numberOfQueriesPercentileCounter.PercentileQuery(80.0);
			ADRecipientCache<TEntry>.PerfCounters.NumberOfQueriesPerRecipientCache95Percentile.RawValue = ADRecipientCache<TEntry>.numberOfQueriesPercentileCounter.PercentileQuery(95.0);
			ADRecipientCache<TEntry>.PerfCounters.NumberOfQueriesPerRecipientCache99Percentile.RawValue = ADRecipientCache<TEntry>.numberOfQueriesPercentileCounter.PercentileQuery(99.0);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0006F858 File Offset: 0x0006DA58
		IEnumerator<KeyValuePair<ProxyAddress, Result<ADRawEntry>>> IEnumerable<KeyValuePair<ProxyAddress, Result<ADRawEntry>>>.GetEnumerator()
		{
			foreach (KeyValuePair<ProxyAddress, Result<TEntry>> pair in this.dictionary)
			{
				KeyValuePair<ProxyAddress, Result<TEntry>> keyValuePair = pair;
				ProxyAddress key = keyValuePair.Key;
				KeyValuePair<ProxyAddress, Result<TEntry>> keyValuePair2 = pair;
				ADRawEntry data = keyValuePair2.Value.Data;
				KeyValuePair<ProxyAddress, Result<TEntry>> keyValuePair3 = pair;
				yield return new KeyValuePair<ProxyAddress, Result<ADRawEntry>>(key, new Result<ADRawEntry>(data, keyValuePair3.Value.Error));
			}
			yield break;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x0006F874 File Offset: 0x0006DA74
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000B71 RID: 2929
		private const int PageSize = 1000;

		// Token: 0x04000B72 RID: 2930
		public static readonly int BatchSize = ADRecipientObjectSession.ReadMultipleMaxBatchSize;

		// Token: 0x04000B73 RID: 2931
		private readonly ADPropertyDefinition[] properties;

		// Token: 0x04000B74 RID: 2932
		private readonly ReadOnlyCollection<ADPropertyDefinition> propertyCollection;

		// Token: 0x04000B75 RID: 2933
		private IRecipientSession adSession;

		// Token: 0x04000B76 RID: 2934
		private readonly object adSessionLock;

		// Token: 0x04000B77 RID: 2935
		private readonly OrganizationId orgId;

		// Token: 0x04000B78 RID: 2936
		private static MSExchangeADRecipientCacheInstance perfCounters = null;

		// Token: 0x04000B79 RID: 2937
		private static AggregatingPercentileCounter numberOfQueriesPercentileCounter = null;

		// Token: 0x04000B7A RID: 2938
		private static IRecipientSession defaultADSession;

		// Token: 0x04000B7B RID: 2939
		private Dictionary<ProxyAddress, Result<TEntry>> dictionary;

		// Token: 0x04000B7C RID: 2940
		private readonly object dictionaryLock;

		// Token: 0x04000B7D RID: 2941
		private long numberOfLookups;

		// Token: 0x04000B7E RID: 2942
		private readonly bool isFullADRecipientObject;
	}
}
