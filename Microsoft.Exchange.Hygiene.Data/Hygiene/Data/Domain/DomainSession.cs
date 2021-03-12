using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HygieneData;
using Microsoft.Exchange.Hygiene.Cache.Data;
using Microsoft.Exchange.Hygiene.Common.Diagnosis;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.Domain
{
	// Token: 0x02000124 RID: 292
	internal class DomainSession
	{
		// Token: 0x06000B0D RID: 2829 RVA: 0x00021FAF File Offset: 0x000201AF
		public DomainSession() : this(CombGuidGenerator.NewGuid(), "Unknown", CacheFailoverMode.DatabaseOnly, null)
		{
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00021FC3 File Offset: 0x000201C3
		public DomainSession(CacheFailoverMode dataAccessType) : this(CombGuidGenerator.NewGuid(), "Unknown", dataAccessType, null)
		{
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00021FD7 File Offset: 0x000201D7
		public DomainSession(CacheFailoverMode dataAccessType, Tracking profiler) : this(CombGuidGenerator.NewGuid(), "Unknown", dataAccessType, profiler)
		{
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00021FEB File Offset: 0x000201EB
		public DomainSession(Guid transactionId) : this(transactionId, "Unknown", CacheFailoverMode.DatabaseOnly, null)
		{
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00021FFC File Offset: 0x000201FC
		public DomainSession(Guid transactionId, string callerId = "Unknown", CacheFailoverMode dataAccessType = CacheFailoverMode.DatabaseOnly, Tracking profiler = null)
		{
			if (callerId == null)
			{
				throw new ArgumentNullException("callerId");
			}
			this.transactionId = transactionId;
			this.callerId = callerId;
			this.DataAcccess = dataAccessType;
			this.Profiler = profiler;
			if (CacheFailoverMode.DatabaseOnly == this.DataAcccess)
			{
				this.dataProvider = this.WebStoreDataProvider;
			}
			else
			{
				this.dataProvider = this.CacheFallbackDataProvider;
			}
			this.TraceInformation("DomainSession.DataProvider type={0}, dataAccessType={1}", new object[]
			{
				this.dataProvider.GetType().Name,
				this.DataAcccess
			});
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00022091 File Offset: 0x00020291
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00022099 File Offset: 0x00020299
		public Tracking Profiler { get; private set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000220A2 File Offset: 0x000202A2
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x000220AA File Offset: 0x000202AA
		public CacheFailoverMode DataAcccess { get; private set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x000220B3 File Offset: 0x000202B3
		public string ContextPrefix
		{
			get
			{
				return string.Format("ThreadId: {0}, TransactionId: {1}, ", Environment.CurrentManagedThreadId, this.transactionId);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000220D4 File Offset: 0x000202D4
		internal IConfigDataProvider DefaultDataProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x000220DC File Offset: 0x000202DC
		private IConfigDataProvider CacheDataProvider
		{
			get
			{
				if (this.cacheDataProvider == null)
				{
					this.cacheDataProvider = ConfigDataProviderFactory.CacheDefault.Create(DatabaseType.Domain);
				}
				return this.cacheDataProvider;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x000220FD File Offset: 0x000202FD
		private IConfigDataProvider CacheFallbackDataProvider
		{
			get
			{
				if (this.cacheFallbackDataProvider == null)
				{
					this.cacheFallbackDataProvider = ConfigDataProviderFactory.CacheFallbackDefault.Create(DatabaseType.Domain);
				}
				return this.cacheFallbackDataProvider;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0002211E File Offset: 0x0002031E
		private IConfigDataProvider WebStoreDataProvider
		{
			get
			{
				if (this.webStoreDataProvider == null)
				{
					this.webStoreDataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Domain);
				}
				return this.webStoreDataProvider;
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002213F File Offset: 0x0002033F
		public static string TrackingDictionaryKey(DomainSession.CacheTrackingCategory category, DomainSession.CacheTrackingTypes type)
		{
			return string.Format("{0}_{1}", category, type);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00022158 File Offset: 0x00020358
		public static SqlPropertyDefinition[] FindPropertyDefinition(string entityName = null, string propertyName = null, int? entityId = null, int? propertyId = null)
		{
			ObjectCache @default = MemoryCache.Default;
			string key = string.Format("DBs={0};Entity={1};Prop={2};EntityID={3};PropID={4}", new object[]
			{
				DatabaseType.Domain,
				entityName,
				propertyName,
				entityId,
				propertyId
			});
			SqlPropertyDefinition[] array = @default[key] as SqlPropertyDefinition[];
			if (array == null)
			{
				array = HygieneSession.FindPropertyDefinition(DatabaseType.Domain, entityName, propertyName, entityId, propertyId);
				if (array != null)
				{
					@default.Set(key, array, DomainSession.propDefinitioncachItemPolicy, null);
				}
				ExTraceGlobals.DomainSessionTracer.TraceInformation<string, string>(0, 0L, "DomainSession.FindPropertyDefinition EntityName={0}, PropName={1}: Bag read from DB", entityName, propertyName);
			}
			else
			{
				ExTraceGlobals.DomainSessionTracer.TraceInformation<string, string>(0, 0L, "DomainSession.FindPropertyDefinition EntityName={0}, PropName={1}: Bag read from Cache", entityName, propertyName);
			}
			return array;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000221F8 File Offset: 0x000203F8
		public static void SavePropertyDefinition(SqlPropertyDefinition propertyDefinition)
		{
			HygieneSession.SavePropertyDefinition(DatabaseType.Domain, propertyDefinition);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00022201 File Offset: 0x00020401
		public static void DeletePropertyDefinition(SqlPropertyDefinition propertyDefinition)
		{
			HygieneSession.DeletePropertyDefinition(DatabaseType.Domain, propertyDefinition);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002220C File Offset: 0x0002040C
		public void Save(IConfigurable obj)
		{
			this.CheckInputType(obj);
			this.Track(this.GetTrackingTag(true, "Sav", new Type[]
			{
				obj.GetType()
			}));
			DomainSession.AddIdentifier(obj as IPropertyBag);
			this.ApplyAuditProperties(obj);
			this.TraceDebug("Calling Save on {0}", new object[]
			{
				obj
			});
			this.DefaultDataProvider.Save(obj);
			this.Track(this.GetTrackingTag(false, "Sav", new Type[]
			{
				obj.GetType()
			}));
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000222E0 File Offset: 0x000204E0
		public void SaveDomainAndTargetService(DomainTargetEnvironment domainTargetEnvironment, TargetService targetService)
		{
			this.TraceDebug("Calling SaveDomainAndTargetService");
			this.Track(this.GetTrackingTag(true, "Sav", new Type[]
			{
				typeof(DomainTargetEnvironment),
				typeof(TargetService)
			}));
			if (domainTargetEnvironment == null)
			{
				throw new ArgumentNullException("domainTargetEnvironment");
			}
			if (targetService == null)
			{
				throw new ArgumentNullException("targetService");
			}
			this.ValidateDomainRecordsMatch(domainTargetEnvironment, targetService);
			this.Run(false, "SaveDomainAndTargetService", delegate
			{
				this.Save(domainTargetEnvironment);
				targetService.DomainKey = domainTargetEnvironment.DomainKey;
				this.Save(targetService);
			});
			this.Track(this.GetTrackingTag(false, "Sav", new Type[]
			{
				typeof(DomainTargetEnvironment),
				typeof(TargetService)
			}));
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000223CC File Offset: 0x000205CC
		public void Delete(IConfigurable obj)
		{
			this.CheckInputType(obj);
			this.Track(this.GetTrackingTag(true, "Del", new Type[]
			{
				obj.GetType()
			}));
			this.ApplyAuditProperties(obj);
			this.TraceDebug("Calling Delete on {0}", new object[]
			{
				obj
			});
			this.DefaultDataProvider.Delete(obj);
			this.Track(this.GetTrackingTag(false, "Del", new Type[]
			{
				obj.GetType()
			}));
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000225E8 File Offset: 0x000207E8
		public void DeleteTenantAndDomains(Guid tenantId, IEnumerable<int> tenantPropEntityIds, IEnumerable<int> domainPropEntityIds)
		{
			this.Track(this.GetTrackingTag(true, "Del", new Type[]
			{
				typeof(TenantTargetEnvironment),
				typeof(DomainTargetEnvironment)
			}));
			if (tenantPropEntityIds == null)
			{
				throw new ArgumentNullException("tenantPropEntityIds");
			}
			if (domainPropEntityIds == null)
			{
				throw new ArgumentNullException("domainPropEntityIds");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling DeleteTenantAndDomains with tenantId={0}, tenantPropEntityIds={1}, domainPropEntityIds={2}  ", new object[]
				{
					tenantId,
					tenantPropEntityIds.ConvertToString<int>(),
					domainPropEntityIds.ConvertToString<int>()
				});
			}
			TenantTargetEnvironment tenant = new TenantTargetEnvironment
			{
				TenantId = tenantId
			};
			tenant.Properties = new Dictionary<int, Dictionary<int, string>>();
			foreach (int key in tenantPropEntityIds)
			{
				if (!tenant.Properties.ContainsKey(key))
				{
					tenant.Properties.Add(key, null);
				}
			}
			Dictionary<int, Dictionary<int, string>> domainProperties = new Dictionary<int, Dictionary<int, string>>();
			foreach (int key2 in domainPropEntityIds)
			{
				if (!domainProperties.ContainsKey(key2))
				{
					domainProperties.Add(key2, null);
				}
			}
			this.Run(false, "DeleteTenantAndDomains", delegate
			{
				object[] allPhysicalPartitions = ((IPartitionedDataProvider)this.WebStoreDataProvider).GetAllPhysicalPartitions();
				Task[] array = new Task[allPhysicalPartitions.Length * 2 + 1];
				int num = 0;
				array[num++] = Task.Factory.StartNew(delegate()
				{
					this.Delete(tenant);
				});
				object[] array2 = allPhysicalPartitions;
				for (int i = 0; i < array2.Length; i++)
				{
					object value = array2[i];
					DomainTargetEnvironment domain = new DomainTargetEnvironment
					{
						TenantId = tenant.TenantId,
						Properties = domainProperties
					};
					domain[DalHelper.PhysicalInstanceKeyProp] = value;
					array[num++] = Task.Factory.StartNew(delegate()
					{
						this.Delete(domain);
					});
					TargetService targetService = new TargetService
					{
						TenantId = tenant.TenantId,
						Properties = domainProperties
					};
					targetService[DalHelper.PhysicalInstanceKeyProp] = value;
					array[num++] = Task.Factory.StartNew(delegate()
					{
						this.Delete(targetService);
					});
				}
				Task.WaitAll(array);
			});
			this.Track(this.GetTrackingTag(false, "Del", new Type[]
			{
				typeof(TenantTargetEnvironment),
				typeof(DomainTargetEnvironment)
			}));
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000227FC File Offset: 0x000209FC
		public void DeleteDomainAndTargetService(DomainTargetEnvironment domainTargetEnvironment, TargetService targetService)
		{
			this.Track(this.GetTrackingTag(true, "Del", new Type[]
			{
				typeof(DomainTargetEnvironment),
				typeof(TargetService)
			}));
			this.TraceDebug("Calling DeleteDomainAndTargetService");
			if (domainTargetEnvironment == null && targetService == null)
			{
				throw new ArgumentNullException("domainTargetEnvironment && targetService");
			}
			if (domainTargetEnvironment == null)
			{
				this.TraceDebug("domainTargetEnvironment is null");
				if (string.IsNullOrWhiteSpace(targetService.DomainName))
				{
					TargetService targetService2 = this.FindTargetService(targetService.DomainKey);
					targetService.DomainName = ((targetService2 != null) ? targetService2.DomainName : null);
					this.TraceDebug("DomainName= {0} fetched from targetservice in DB", new object[]
					{
						targetService.DomainName
					});
				}
				else
				{
					this.TraceDebug("using DomainName= {0} specified in targetservice", new object[]
					{
						targetService.DomainName
					});
				}
				if (!string.IsNullOrWhiteSpace(targetService.DomainName))
				{
					domainTargetEnvironment = new DomainTargetEnvironment
					{
						DomainName = targetService.DomainName,
						TenantId = targetService.TenantId,
						Properties = targetService.Properties
					};
				}
				else
				{
					this.TraceDebug("could not get DomainName from targetservice, skipping DomainTargetEnvironment delete");
				}
			}
			else if (targetService == null)
			{
				this.TraceDebug("targetService is null");
				if (string.IsNullOrWhiteSpace(domainTargetEnvironment.DomainKey))
				{
					DomainTargetEnvironment domainTargetEnvironment2 = this.FindDomainTargetEnvironment(domainTargetEnvironment.DomainName);
					domainTargetEnvironment.DomainKey = ((domainTargetEnvironment2 != null) ? domainTargetEnvironment2.DomainKey : null);
					this.TraceDebug("DomainKey= {0} fetched from domainTargetEnvironment in DB", new object[]
					{
						domainTargetEnvironment.DomainKey
					});
				}
				else
				{
					this.TraceDebug("using DomainKey= {0} specified in domainTargetEnvironment", new object[]
					{
						domainTargetEnvironment.DomainKey
					});
				}
				if (!string.IsNullOrWhiteSpace(domainTargetEnvironment.DomainKey))
				{
					targetService = new TargetService
					{
						DomainKey = domainTargetEnvironment.DomainKey,
						DomainName = domainTargetEnvironment.DomainName,
						TenantId = domainTargetEnvironment.TenantId,
						Properties = domainTargetEnvironment.Properties
					};
				}
				else
				{
					this.TraceDebug("could not get DomainKey from domainTargetEnvironment, skipping targetService delete");
				}
			}
			else
			{
				this.ValidateDomainRecordsMatch(domainTargetEnvironment, targetService);
			}
			this.Run(false, "DeleteDomainAndTargetService", delegate
			{
				if (targetService != null)
				{
					this.Delete(targetService);
				}
				if (domainTargetEnvironment != null)
				{
					this.Delete(domainTargetEnvironment);
				}
			});
			this.Track(this.GetTrackingTag(false, "Del", new Type[]
			{
				typeof(DomainTargetEnvironment),
				typeof(TargetService)
			}));
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00022B20 File Offset: 0x00020D20
		public IEnumerable<DomainTargetEnvironment> FindDomainTargetEnvironments(IEnumerable<string> domainNames)
		{
			if (domainNames == null)
			{
				throw new ArgumentNullException("domainNames");
			}
			IEnumerable<string> enumerable = from domainName in domainNames.Distinct(StringComparer.OrdinalIgnoreCase)
			where !string.IsNullOrWhiteSpace(domainName)
			select domainName;
			if (enumerable.Count<string>() == 0)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyList, "domainNames");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling FindDomainTargetEnvironments, domainNames={0}", new object[]
				{
					enumerable.ConvertToString<string>()
				});
			}
			return this.Find<DomainTargetEnvironment, string>(enumerable, DomainSchema.DomainNames, DomainSchema.DomainName);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00022BC8 File Offset: 0x00020DC8
		public IEnumerable<TargetService> FindTargetServices(IEnumerable<string> domainKeys)
		{
			if (domainKeys == null)
			{
				throw new ArgumentNullException("domainKeys");
			}
			IEnumerable<string> enumerable = from domainKey in domainKeys.Distinct(StringComparer.OrdinalIgnoreCase)
			where !string.IsNullOrWhiteSpace(domainKey)
			select domainKey;
			if (enumerable.Count<string>() == 0)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyList, "domainKeys");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling FindTargetServices, domainKeys={0}", new object[]
				{
					enumerable.ConvertToString<string>()
				});
			}
			return this.Find<TargetService, string>(enumerable, DomainSchema.DomainKeys, DomainSchema.DomainKey);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00022C70 File Offset: 0x00020E70
		public IEnumerable<TenantTargetEnvironment> FindTenantTargetEnvironments(IEnumerable<Guid> tenantIds)
		{
			if (tenantIds == null)
			{
				throw new ArgumentNullException("tenantIds");
			}
			IEnumerable<Guid> enumerable = from tenantId in tenantIds.Distinct<Guid>()
			where tenantId != Guid.Empty
			select tenantId;
			if (enumerable.Count<Guid>() == 0)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyList, "tenantIds");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling FindTenantTargetEnvironments, tenantIds={0}", new object[]
				{
					enumerable.ConvertToString<Guid>()
				});
			}
			return this.Find<TenantTargetEnvironment, Guid>(enumerable, DomainSchema.TenantIds, DomainSchema.TenantId);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00022D14 File Offset: 0x00020F14
		public IEnumerable<DomainTargetEnvironment> FindDomainTargetEnvironmentsByTenantIds(IEnumerable<Guid> tenantIds)
		{
			if (tenantIds == null)
			{
				throw new ArgumentNullException("tenantIds");
			}
			IEnumerable<Guid> enumerable = from tenantId in tenantIds.Distinct<Guid>()
			where tenantId != Guid.Empty
			select tenantId;
			if (!enumerable.Any<Guid>())
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyList, "tenantIds");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling FindDomainTargetEnvironmentsByTenantIds, tenantIds={0}", new object[]
				{
					enumerable.ConvertToString<Guid>()
				});
			}
			List<DomainTargetEnvironment> list = new List<DomainTargetEnvironment>();
			foreach (int num in this.GetAllPhysicalPartitions())
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.TenantIds, new MultiValuedProperty<Guid>(enumerable));
				list.AddRange(this.Find<DomainTargetEnvironment>(filter, this.WebStoreDataProvider, num));
			}
			return list;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00022DFC File Offset: 0x00020FFC
		public IEnumerable<TargetService> FindTargetServicesByTenantIds(IEnumerable<Guid> tenantIds)
		{
			if (tenantIds == null)
			{
				throw new ArgumentNullException("tenantIds");
			}
			IEnumerable<Guid> enumerable = from tenantId in tenantIds.Distinct<Guid>()
			where tenantId != Guid.Empty
			select tenantId;
			if (!enumerable.Any<Guid>())
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyList, "tenantIds");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling FindTargetServicesByTenantIds, tenantIds={0}", new object[]
				{
					enumerable.ConvertToString<Guid>()
				});
			}
			List<TargetService> list = new List<TargetService>();
			foreach (int num in this.GetAllPhysicalPartitions())
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.TenantIds, new MultiValuedProperty<Guid>(enumerable));
				list.AddRange(this.Find<TargetService>(filter, this.WebStoreDataProvider, num));
			}
			return list;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00022ED8 File Offset: 0x000210D8
		public DomainTargetEnvironment FindDomainTargetEnvironment(string domainName)
		{
			if (string.IsNullOrWhiteSpace(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			this.TraceDebug("Calling FindDomainTargetEnvironment, domainName={0}", new object[]
			{
				domainName
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainName, domainName);
			return this.Find<DomainTargetEnvironment>(filter, null, null).FirstOrDefault<DomainTargetEnvironment>();
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00022F2C File Offset: 0x0002112C
		public IEnumerable<DIDomainTargetEnvironment> DIFindDomainTargetEnvironment(string domainName)
		{
			if (string.IsNullOrWhiteSpace(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			this.TraceDebug("Calling DIFindDomainTargetEnvironment, domainName={0}", new object[]
			{
				domainName
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainName, domainName);
			return this.Find<DIDomainTargetEnvironment>(filter, null, null);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00022F7C File Offset: 0x0002117C
		public IEnumerable<DomainTargetEnvironment> FindDomainTargetEnvironmentsByTenantId(Guid tenantId)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException("TenantId cannot be Guid.Empty", "tenantId");
			}
			this.TraceDebug("Calling FindDomainTargetEnvironmentsByTenantId, tenantId={0}", new object[]
			{
				tenantId
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, tenantId);
			return this.FindFromAllPartitions<DomainTargetEnvironment>(filter);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00022FDC File Offset: 0x000211DC
		public IEnumerable<DIDomainTargetEnvironment> DIFindDomainTargetEnvironmentsByTenantId(Guid tenantId)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException("TenantId cannot be Guid.Empty", "tenantId");
			}
			this.TraceDebug("Calling DIFindDomainTargetEnvironmentsByTenantId, tenantId={0}", new object[]
			{
				tenantId
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, tenantId);
			return this.FindFromAllPartitions<DIDomainTargetEnvironment>(filter);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002303C File Offset: 0x0002123C
		public TargetService FindTargetService(string domainKey)
		{
			if (string.IsNullOrWhiteSpace(domainKey))
			{
				throw new ArgumentNullException("domainKey");
			}
			this.TraceDebug("Calling FindTargetService, domainKey={0}", new object[]
			{
				domainKey
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainKey, domainKey);
			return this.Find<TargetService>(filter, null, null).FirstOrDefault<TargetService>();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00023090 File Offset: 0x00021290
		public IEnumerable<DITargetService> DIFindTargetService(string domainKey)
		{
			if (string.IsNullOrWhiteSpace(domainKey))
			{
				throw new ArgumentNullException("domainKey");
			}
			this.TraceDebug("Calling DIFindTargetService, domainKey={0}", new object[]
			{
				domainKey
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainKey, domainKey);
			return this.Find<DITargetService>(filter, null, null);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000230E0 File Offset: 0x000212E0
		public IEnumerable<TargetService> FindTargetServicesByTenantId(Guid tenantId)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException("TenantId cannot be Guid.Empty", "tenantId");
			}
			this.TraceDebug("Calling FindTargetServicesByTenantId, tenantId={0}", new object[]
			{
				tenantId
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, tenantId);
			return this.FindFromAllPartitions<TargetService>(filter);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00023140 File Offset: 0x00021340
		public UserTargetEnvironment FindUserEnvironment(string userKey)
		{
			if (string.IsNullOrWhiteSpace(userKey))
			{
				throw new ArgumentNullException("userKey");
			}
			this.TraceDebug("Calling FindUserTargetEnvironment, userKey={0}", new object[]
			{
				userKey
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.UserKey, userKey);
			return this.Find<UserTargetEnvironment>(filter, null, null).FirstOrDefault<UserTargetEnvironment>();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00023194 File Offset: 0x00021394
		public TenantTargetEnvironment FindTenantTargetEnvironment(Guid tenantId)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyGuid, "tenantId");
			}
			this.TraceDebug("Calling FindTenantTargetEnvironment, tenantId={0}", new object[]
			{
				tenantId
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.TenantId, tenantId);
			return this.Find<TenantTargetEnvironment>(filter, null, null).FirstOrDefault<TenantTargetEnvironment>();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00023200 File Offset: 0x00021400
		public IEnumerable<TenantTargetEnvironment> FindTenantTargetEnvironmentCopies(Guid tenantId, int copiesToRead = 2)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyGuid, "tenantId");
			}
			this.TraceDebug("Calling FindTenantTargetEnvironmentCopies, tenantId={0}, copiesToRead={1}", new object[]
			{
				tenantId,
				copiesToRead
			});
			int physicalPartitionCopyCount = this.GetPhysicalPartitionCopyCount(this.GetPhysicalInstanceId(tenantId));
			if (copiesToRead < 1 || (physicalPartitionCopyCount > 1 && physicalPartitionCopyCount < copiesToRead))
			{
				throw new ArgumentOutOfRangeException("copiesToRead", string.Format("The minimum allowed value is 1 and maximum allowed value is {0}", physicalPartitionCopyCount));
			}
			List<TransientDALException> list = null;
			List<TenantTargetEnvironment> list2 = new List<TenantTargetEnvironment>();
			int num = 0;
			int num2 = 0;
			int num3 = DomainSession.randomGenerator.Next(0, physicalPartitionCopyCount);
			do
			{
				try
				{
					num3 = ++num3 % physicalPartitionCopyCount;
					QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
					{
						new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, tenantId),
						new ComparisonFilter(ComparisonOperator.Equal, DalHelper.FssCopyIdProp, num3)
					});
					list2.AddRange(this.Find<TenantTargetEnvironment>(filter, this.WebStoreDataProvider, null));
					num++;
				}
				catch (TransientDALException item)
				{
					if (list == null)
					{
						list = new List<TransientDALException>();
					}
					list.Add(item);
				}
			}
			while (++num2 < physicalPartitionCopyCount && num < copiesToRead);
			if (list != null && list.Count == physicalPartitionCopyCount)
			{
				throw new AggregateException(list);
			}
			return list2;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00023354 File Offset: 0x00021554
		public IEnumerable<DITenantTargetEnvironment> DIFindTenantTargetEnvironment(Guid tenantId)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyGuid, "tenantId");
			}
			this.TraceDebug("Calling DIFindTenantTargetEnvironment, tenantId={0}", new object[]
			{
				tenantId
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.TenantId, tenantId);
			return this.Find<DITenantTargetEnvironment>(filter, null, null);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000233BC File Offset: 0x000215BC
		public IEnumerable<NsResourceRecord> FindNsResourceRecord(string domainName)
		{
			if (string.IsNullOrWhiteSpace(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			this.TraceDebug("Calling FindNsResourceRecord, domainName={0}", new object[]
			{
				domainName
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainName, domainName);
			return this.Find<NsResourceRecord>(filter, null, null);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002340C File Offset: 0x0002160C
		public SoaResourceRecord FindSoaResourceRecord(string domainName)
		{
			if (string.IsNullOrWhiteSpace(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			this.TraceDebug("Calling FindSoaResourceRecord, domainName={0}", new object[]
			{
				domainName
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainName, domainName);
			return this.Find<SoaResourceRecord>(filter, null, null).FirstOrDefault<SoaResourceRecord>();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00023460 File Offset: 0x00021660
		public Zone FindZone(string domainName)
		{
			if (string.IsNullOrWhiteSpace(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			this.TraceDebug("Calling FindZone, domainName={0}", new object[]
			{
				domainName
			});
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DomainSchema.DomainName, domainName);
			return this.Find<Zone>(filter, null, null).FirstOrDefault<Zone>();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000234B2 File Offset: 0x000216B2
		public IEnumerable<Zone> FindZoneAll()
		{
			this.TraceDebug("Calling FindZoneAll");
			return this.Find<Zone>(null, null, null);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000234C8 File Offset: 0x000216C8
		public bool UpdateTargetServiceByDomainKey(string domainKey, Dictionary<int, Dictionary<int, string>> properties)
		{
			this.Track(this.GetTrackingTag(true, "Upd", new Type[]
			{
				typeof(TargetService)
			}));
			if (string.IsNullOrWhiteSpace(domainKey))
			{
				throw new ArgumentNullException("domainKey");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling UpdateTargetServiceByDomainKey, domainKey={0}, properties(EntityId:PropertyId:PropertyValue)={1}", new object[]
				{
					domainKey,
					properties.ConvertToString()
				});
			}
			TargetService targetService = this.FindTargetService(domainKey);
			if (targetService != null)
			{
				targetService.Properties = properties;
				this.Save(targetService);
				this.Track(this.GetTrackingTag(false, "Upd", new Type[]
				{
					typeof(TargetService)
				}));
				return true;
			}
			this.Track(this.GetTrackingTag(false, "Upd", new Type[]
			{
				typeof(TargetService)
			}));
			this.TraceDebug("TargetService not found");
			return false;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000235FC File Offset: 0x000217FC
		public bool UpdateDomainKey(string domainName, string newDomainKey)
		{
			if (string.IsNullOrWhiteSpace(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			if (string.IsNullOrWhiteSpace(newDomainKey))
			{
				throw new ArgumentNullException("newDomainKey");
			}
			this.Track(this.GetTrackingTag(true, "Upd", new Type[]
			{
				typeof(DomainTargetEnvironment),
				typeof(TargetService)
			}));
			try
			{
				this.TraceDebug("Calling UpdateDomainKey, domainName={0}, newDomainKey={1}", new object[]
				{
					domainName,
					newDomainKey
				});
				DomainTargetEnvironment domainTargetEnvironment = this.FindDomainTargetEnvironment(domainName);
				if (domainTargetEnvironment == null)
				{
					this.TraceDebug("DomainTargetEnvironment not found, domainName:{0}", new object[]
					{
						domainName
					});
					return false;
				}
				string domainKey = domainTargetEnvironment.DomainKey;
				TargetService oldTargetService = this.FindTargetService(domainKey);
				if (oldTargetService == null)
				{
					this.TraceDebug("TargetService not found, domainKey:{0}", new object[]
					{
						domainKey
					});
					return false;
				}
				if (newDomainKey.Equals(domainKey, StringComparison.OrdinalIgnoreCase))
				{
					this.TraceDebug("Domain:{0} already has the new domainKey:{1}", new object[]
					{
						domainName,
						newDomainKey
					});
					return true;
				}
				TargetService newTargetService = new TargetService
				{
					DomainName = oldTargetService.DomainName,
					DomainKey = newDomainKey,
					TenantId = oldTargetService.TenantId,
					Properties = oldTargetService.Properties
				};
				domainTargetEnvironment.DomainKey = newDomainKey;
				domainTargetEnvironment[DomainSchema.UpdateDomainKey] = true;
				this.Run(false, "UpdateDomainKey", delegate
				{
					this.Save(newTargetService);
					this.Save(domainTargetEnvironment);
					this.Delete(oldTargetService);
				});
			}
			finally
			{
				this.Track(this.GetTrackingTag(false, "Upd", new Type[]
				{
					typeof(DomainTargetEnvironment),
					typeof(TargetService)
				}));
			}
			return true;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00023834 File Offset: 0x00021A34
		public IEnumerable<string> UpdateTargetServiceByTenantId(Guid tenantId, Dictionary<int, Dictionary<int, string>> properties)
		{
			this.Track(this.GetTrackingTag(true, "Upd", new Type[]
			{
				typeof(TargetService)
			}));
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyGuid, "tenantId");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Calling UpdateTargetServiceByTenantId, tenantId={0}, properties(EntityId:PropertyId:PropertyValue)={1}", new object[]
				{
					tenantId,
					properties.ConvertToString()
				});
			}
			TargetServiceByTenantId targetService = new TargetServiceByTenantId
			{
				TenantId = tenantId,
				Properties = properties
			};
			this.Run(false, "UpdateTargetServiceByTenantId", delegate
			{
				this.Save(targetService);
			});
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("Updated domainkeys = {0}", new object[]
				{
					targetService.UpdatedDomainKeys.ConvertToString<string>()
				});
			}
			this.Track(this.GetTrackingTag(false, "Upd", new Type[]
			{
				typeof(TargetService)
			}));
			return targetService.UpdatedDomainKeys;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002396C File Offset: 0x00021B6C
		public void UndeleteTenant(Guid tenantId, DateTime deletedDatetime)
		{
			if (tenantId == Guid.Empty)
			{
				throw new ArgumentException("The tenantId must not be empty.");
			}
			this.DefaultDataProvider.Save(new TenantUndeleteRequest
			{
				TenantId = tenantId,
				DeletedDatetime = deletedDatetime
			});
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000239B4 File Offset: 0x00021BB4
		private static void AddIdentifier(IPropertyBag obj)
		{
			HygienePropertyDefinition propertyDefinition = null;
			if (DomainSession.IdentifierMap.TryGetValue(obj.GetType(), out propertyDefinition))
			{
				Guid a = (Guid)obj[propertyDefinition];
				if (a == Guid.Empty)
				{
					obj[propertyDefinition] = DomainSession.GenerateIdentifier();
				}
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00023A02 File Offset: 0x00021C02
		private static Guid GenerateIdentifier()
		{
			return CombGuidGenerator.NewGuid();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00023A0C File Offset: 0x00021C0C
		private void ApplyAuditProperties(IConfigurable configurable)
		{
			IPropertyBag propertyBag = configurable as IPropertyBag;
			AuditHelper.ApplyAuditProperties(propertyBag, this.transactionId, this.callerId);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x00023A34 File Offset: 0x00021C34
		private void CheckInputType(IConfigurable obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("DomainSession input");
			}
			if (!(obj is Zone) && !(obj is NsResourceRecord) && !(obj is SoaResourceRecord) && !(obj is TargetService) && !(obj is TargetServiceByTenantId) && !(obj is TenantTargetEnvironment) && !(obj is UserTargetEnvironment) && !(obj is DomainTargetEnvironment))
			{
				string name = obj.GetType().Name;
				this.TraceWarning("Invalid object type = {0}", new object[]
				{
					name
				});
				throw new InvalidObjectTypeForSessionException(HygieneDataStrings.ErrorInvalidObjectTypeForSession("DomainSession", name));
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00023AC4 File Offset: 0x00021CC4
		private void ValidateDomainRecordsMatch(DomainTargetEnvironment domainTargetEnvironment, TargetService targetService)
		{
			if (!string.IsNullOrWhiteSpace(domainTargetEnvironment.DomainKey) && !string.IsNullOrWhiteSpace(targetService.DomainKey) && string.Compare(domainTargetEnvironment.DomainKey, targetService.DomainKey, StringComparison.OrdinalIgnoreCase) != 0)
			{
				this.TraceWarning("domainKeys don't match domainTargetEnvironment.DomainKey = {0}, targetService.DomainKey = {1}", new object[]
				{
					domainTargetEnvironment.DomainKey,
					targetService.DomainKey
				});
				throw new ArgumentException(HygieneDataStrings.ErrorInvalidArgumentDomainKeyMismatch(domainTargetEnvironment.DomainKey, targetService.DomainKey));
			}
			if (!string.IsNullOrWhiteSpace(domainTargetEnvironment.DomainName) && !string.IsNullOrWhiteSpace(targetService.DomainName) && string.Compare(domainTargetEnvironment.DomainName, targetService.DomainName, StringComparison.OrdinalIgnoreCase) != 0)
			{
				this.TraceWarning("domainNames don't match domainTargetEnvironment.DomainName = {0}, targetService.DomainName = {1}", new object[]
				{
					domainTargetEnvironment.DomainName,
					targetService.DomainName
				});
				throw new ArgumentException(HygieneDataStrings.ErrorInvalidArgumentDomainNameMismatch(domainTargetEnvironment.DomainName, targetService.DomainName));
			}
			if (domainTargetEnvironment.TenantId != targetService.TenantId)
			{
				this.TraceWarning("tenantIds don't match domainTargetEnvironment.TenantId = {0}, targetService.TenantId = {1}", new object[]
				{
					domainTargetEnvironment.TenantId,
					targetService.TenantId
				});
				throw new ArgumentException(HygieneDataStrings.ErrorInvalidArgumentTenantIdMismatch(domainTargetEnvironment.TenantId, targetService.TenantId));
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00023C10 File Offset: 0x00021E10
		private void GetCacheStatus(Dictionary<PropertyDefinition, object> cacheOutputBag, IConfigurable[] results, out int cacheHit, out CachePrimingState primingState, out CacheFailoverMode failoverMode, out int bloomFilterHit, out int inMemoryHit)
		{
			primingState = CachePrimingState.Unknown;
			failoverMode = CacheFailoverMode.Default;
			bloomFilterHit = -1;
			cacheHit = -1;
			inMemoryHit = -1;
			if (cacheOutputBag.ContainsKey(DalHelper.CachePrimingStateProp))
			{
				primingState = (CachePrimingState)cacheOutputBag[DalHelper.CachePrimingStateProp];
			}
			if (cacheOutputBag.ContainsKey(DalHelper.CacheFailoverModeProp))
			{
				failoverMode = (CacheFailoverMode)cacheOutputBag[DalHelper.CacheFailoverModeProp];
			}
			if (cacheOutputBag.ContainsKey(DalHelper.BloomHitProp))
			{
				bloomFilterHit = (((bool)cacheOutputBag[DalHelper.BloomHitProp]) ? 1 : 0);
			}
			if (cacheOutputBag.ContainsKey(DalHelper.InMemoryCacheHitProp))
			{
				inMemoryHit = (((bool)cacheOutputBag[DalHelper.InMemoryCacheHitProp]) ? 1 : 0);
			}
			if (results != null && results.Length > 0 && results[0] != null)
			{
				IPropertyBag propertyBag = results[0] as IPropertyBag;
				if (propertyBag != null)
				{
					cacheHit = (((bool)propertyBag[DalHelper.CacheHitProp]) ? 1 : 0);
				}
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00023CF0 File Offset: 0x00021EF0
		private IEnumerable<T> Find<T>(QueryFilter filter, IConfigDataProvider alternateDataProvider = null, object physicalInstanceId = null) where T : IConfigurable, new()
		{
			Dictionary<PropertyDefinition, object> dictionary = null;
			this.Track(this.GetTrackingTag(true, "Fnd", physicalInstanceId, new Type[]
			{
				typeof(T)
			}));
			if (physicalInstanceId != null)
			{
				filter = QueryFilter.AndTogether(new QueryFilter[]
				{
					filter,
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PhysicalInstanceKeyProp, physicalInstanceId)
				});
			}
			IConfigDataProvider configDataProvider = (alternateDataProvider != null) ? alternateDataProvider : this.DefaultDataProvider;
			if (configDataProvider is ICachePrimingInfo)
			{
				dictionary = new Dictionary<PropertyDefinition, object>();
				filter = QueryFilter.AndTogether(new QueryFilter[]
				{
					filter,
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.CacheFailoverModeProp, this.DataAcccess),
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.CacheOutputBagProp, dictionary)
				});
			}
			IConfigurable[] array = configDataProvider.Find<T>(filter, null, true, null);
			if (configDataProvider is ICachePrimingInfo)
			{
				int num = -1;
				int num2 = -1;
				int num3 = -1;
				CachePrimingState cachePrimingState;
				CacheFailoverMode cacheFailoverMode;
				this.GetCacheStatus(dictionary, array, out num, out cachePrimingState, out cacheFailoverMode, out num2, out num3);
				if (TraceHelper.IsDebugTraceEnabled())
				{
					this.TraceDebug("Find type={0} result={1}, cacheStatus={2}, hitCache={3} mode={4} bloom={5} mem={6}", new object[]
					{
						typeof(T).Name,
						array.ConvertToString<IConfigurable>(),
						cachePrimingState,
						num,
						cacheFailoverMode,
						num2,
						num3
					});
				}
				this.Track(this.GetCacheTrackingType(typeof(T)), this.GetTrackingTag(false, "Fnd", physicalInstanceId, new Type[]
				{
					typeof(T)
				}), num, cachePrimingState, cacheFailoverMode, num2, num3, false);
			}
			else
			{
				if (TraceHelper.IsDebugTraceEnabled())
				{
					this.TraceDebug("Find type={0} result={1}", new object[]
					{
						typeof(T).Name,
						array.ConvertToString<IConfigurable>()
					});
				}
				this.Track(this.GetCacheTrackingType(typeof(T)), this.GetTrackingTag(false, "Fnd", physicalInstanceId, new Type[]
				{
					typeof(T)
				}), -1, CachePrimingState.Unknown, CacheFailoverMode.DatabaseOnly, -1, -1, this.DataAcccess == CacheFailoverMode.DatabaseOnly);
			}
			return array.Cast<T>();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00023F50 File Offset: 0x00022150
		private IEnumerable<TReturn> Find<TReturn, TInput>(IEnumerable<TInput> lookupValues, HygienePropertyDefinition lookupCollectionDefinition, HygienePropertyDefinition lookupValueDefinition) where TReturn : IConfigurable, IPropertyBag, new()
		{
			this.Track(this.GetTrackingTag(true, "FndB", new Type[]
			{
				typeof(TReturn)
			}));
			List<TReturn> list = new List<TReturn>();
			CacheFailoverMode cacheFailoverMode = this.DataAcccess;
			if (this.DataAcccess != CacheFailoverMode.DatabaseOnly && this.DefaultDataProvider is ICachePrimingInfo)
			{
				ICachePrimingInfo cachePrimingInfo = this.DefaultDataProvider as ICachePrimingInfo;
				Guid guid = Guid.NewGuid();
				this.TraceDebug("Find type={0}: Trying to get CurrentFailoverMode from CompositeDataProvider {1}, requestMode={2}", new object[]
				{
					typeof(TReturn),
					guid,
					this.DataAcccess
				});
				CachePrimingState currentPrimingState = cachePrimingInfo.GetCurrentPrimingState(typeof(TReturn));
				cacheFailoverMode = cachePrimingInfo.GetCurrentFailoverMode(typeof(TReturn), this.DataAcccess, currentPrimingState);
				this.TraceDebug("CurrentFailoverMode={0}, PrimingState={1}", new object[]
				{
					cacheFailoverMode,
					currentPrimingState
				});
			}
			IEnumerable<TInput> enumerable = null;
			if (cacheFailoverMode != CacheFailoverMode.DatabaseOnly)
			{
				IEnumerable<TReturn> enumerable2 = this.FindFromCache<TReturn, TInput>(lookupValues, lookupValueDefinition);
				enumerable = from IPropertyBag r in enumerable2
				select (TInput)((object)r[lookupValueDefinition]);
				if (enumerable.Count<TInput>() > 0)
				{
					list.AddRange(enumerable2);
				}
			}
			if (cacheFailoverMode != CacheFailoverMode.CacheOnly)
			{
				IEnumerable<TInput> enumerable3 = (enumerable != null && enumerable.Count<TInput>() > 0) ? lookupValues.Except(enumerable, new DomainSession.InputEqualityComparer<TInput>()) : lookupValues;
				if (enumerable3.Count<TInput>() > 0)
				{
					IEnumerable<TReturn> enumerable4 = this.FindFromDB<TReturn, TInput>(enumerable3, lookupCollectionDefinition);
					IEnumerable<TInput> source = from IPropertyBag r in enumerable4
					select (TInput)((object)r[lookupValueDefinition]);
					if (source.Count<TInput>() > 0)
					{
						list.AddRange(enumerable4);
					}
				}
			}
			this.Track(this.GetTrackingTag(false, "FndB", new Type[]
			{
				typeof(TReturn)
			}));
			return list;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0002415C File Offset: 0x0002235C
		private IEnumerable<TReturn> FindFromDB<TReturn, TInput>(IEnumerable<TInput> lookupValues, HygienePropertyDefinition lookupCollectionDefinition) where TReturn : IConfigurable, new()
		{
			this.Track(this.GetTrackingTag(true, "FndBD", new Type[]
			{
				typeof(TReturn)
			}));
			Dictionary<object, List<TInput>> dictionary = DalHelper.SplitByPhysicalInstance<TInput>((IHashBucket)this.WebStoreDataProvider, lookupValues, (TInput i) => i.ToString());
			List<TReturn> list = new List<TReturn>();
			foreach (object obj in dictionary.Keys)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, lookupCollectionDefinition, new MultiValuedProperty<TInput>(dictionary[obj]));
				list.AddRange(this.Find<TReturn>(filter, this.WebStoreDataProvider, obj));
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("FindFromDB type={0},  result={1}", new object[]
				{
					typeof(TReturn),
					list.ConvertToString<TReturn>()
				});
			}
			this.Track(this.GetTrackingTag(false, "FndBD", new Type[]
			{
				typeof(TReturn)
			}));
			return list;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00024280 File Offset: 0x00022480
		private IEnumerable<TReturn> FindFromAllPartitions<TReturn>(QueryFilter filter) where TReturn : IConfigurable, new()
		{
			this.Track(this.GetTrackingTag(true, "FndAllP", new Type[]
			{
				typeof(TReturn)
			}));
			object[] allPhysicalPartitions = ((IPartitionedDataProvider)this.WebStoreDataProvider).GetAllPhysicalPartitions();
			List<TReturn> list = new List<TReturn>();
			foreach (object physicalInstanceId in allPhysicalPartitions)
			{
				list.AddRange(this.Find<TReturn>(filter, this.WebStoreDataProvider, physicalInstanceId));
			}
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("FindFromDB type={0},  result={1}", new object[]
				{
					typeof(TReturn),
					list.ConvertToString<TReturn>()
				});
			}
			this.Track(this.GetTrackingTag(false, "FndAllP", new Type[]
			{
				typeof(TReturn)
			}));
			return list;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000243B8 File Offset: 0x000225B8
		private IEnumerable<TReturn> FindFromCache<TReturn, TInput>(IEnumerable<TInput> lookupValues, HygienePropertyDefinition lookupValueDefinition) where TReturn : IConfigurable, new()
		{
			this.Track(this.GetTrackingTag(true, "FndBC", new Type[]
			{
				typeof(TReturn)
			}));
			ConcurrentBag<TReturn> results = new ConcurrentBag<TReturn>();
			Parallel.ForEach<TInput>(lookupValues, delegate(TInput lookupValue)
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, lookupValueDefinition, lookupValue);
				TReturn treturn = this.Find<TReturn>(filter, this.CacheDataProvider, null).FirstOrDefault<TReturn>();
				if (treturn != null)
				{
					results.Add(treturn);
				}
			});
			if (TraceHelper.IsDebugTraceEnabled())
			{
				this.TraceDebug("FindFromCache type={0},  result={1}", new object[]
				{
					typeof(TReturn),
					results.ConvertToString<TReturn>()
				});
			}
			this.Track(this.GetTrackingTag(false, "FndBC", new Type[]
			{
				typeof(TReturn)
			}));
			return results;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00024480 File Offset: 0x00022680
		private int GetPhysicalPartitionCopyCount(int physicalInstanceId)
		{
			IPartitionedDataProvider partitionedDataProvider = this.webStoreDataProvider as IPartitionedDataProvider;
			if (partitionedDataProvider == null)
			{
				throw new NotSupportedException(string.Format("Not supported for DataProvider type: {0}", this.webStoreDataProvider.GetType()));
			}
			return partitionedDataProvider.GetNumberOfPersistentCopiesPerPartition(physicalInstanceId);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000244C0 File Offset: 0x000226C0
		private int GetPhysicalInstanceId(Guid objectId)
		{
			IHashBucket hashBucket = this.WebStoreDataProvider as IHashBucket;
			if (hashBucket == null)
			{
				throw new NotSupportedException(string.Format("Not supported for DataProvider type: {0}", this.webStoreDataProvider.GetType()));
			}
			int logicalHash = hashBucket.GetLogicalHash(objectId.ToString());
			return (int)hashBucket.GetPhysicalInstanceIdByHashValue(logicalHash);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00024517 File Offset: 0x00022717
		private int[] GetAllPhysicalPartitions()
		{
			return ((IPartitionedDataProvider)this.WebStoreDataProvider).GetAllPhysicalPartitions().Cast<int>().ToArray<int>();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00024533 File Offset: 0x00022733
		private void Track(string trackInfo)
		{
			if (this.Profiler != null)
			{
				this.Profiler.SnapShot(trackInfo, -1, true, null);
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00024550 File Offset: 0x00022750
		private void Track(DomainSession.CacheTrackingTypes type, string profilerHeader, int cacheHit, CachePrimingState primingState, CacheFailoverMode failoverMode, int bloomFilterHit, int inMemoryHit, bool cacheExplicitlyBypassed)
		{
			if (this.Profiler != null)
			{
				string text = string.Format("{0}:Health={1}/CacheHit={2}/Mode={3}/Bloom={4}/Mem={5}#", new object[]
				{
					profilerHeader,
					(int)primingState,
					cacheHit,
					(int)failoverMode,
					bloomFilterHit,
					inMemoryHit
				});
				if (!cacheExplicitlyBypassed)
				{
					if (primingState == CachePrimingState.Healthy)
					{
						if (!this.Profiler.AddDictionary(DomainSession.TrackingDictionaryKey(DomainSession.CacheTrackingCategory.CacheHealthy, type), "1", 2))
						{
							EventLogger.LogDomainCacheTrackingError(new object[]
							{
								"CacheHealthy",
								text
							});
						}
					}
					else if (!this.Profiler.AddDictionary(DomainSession.TrackingDictionaryKey(DomainSession.CacheTrackingCategory.CacheUnHealthy, type), "1", 2))
					{
						EventLogger.LogDomainCacheTrackingError(new object[]
						{
							"CacheUnHealthy",
							text
						});
					}
					if (cacheHit == 1)
					{
						if (!this.Profiler.AddDictionary(DomainSession.TrackingDictionaryKey(DomainSession.CacheTrackingCategory.CacheHit, type), "1", 2))
						{
							EventLogger.LogDomainCacheTrackingError(new object[]
							{
								"CacheHit",
								text
							});
						}
					}
					else if (!this.Profiler.AddDictionary(DomainSession.TrackingDictionaryKey(DomainSession.CacheTrackingCategory.CacheMiss, type), "1", 2))
					{
						EventLogger.LogDomainCacheTrackingError(new object[]
						{
							"CacheMiss",
							text
						});
					}
				}
				this.Profiler.SnapShot(text, -1, true, null);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000246AC File Offset: 0x000228AC
		private DomainSession.CacheTrackingTypes GetCacheTrackingType(Type objectType)
		{
			DomainSession.CacheTrackingTypes result;
			if (!DomainSession.ObjectTypeToCacheTrackingTypeMap.TryGetValue(objectType, out result))
			{
				result = DomainSession.CacheTrackingTypes.TypeMinIndex;
			}
			return result;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000246F4 File Offset: 0x000228F4
		private string GetTrackingTag(bool isStart, string operationTag, object physicalInstanceId, params Type[] objectTypes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DAL");
			stringBuilder.Append("_");
			stringBuilder.Append(operationTag);
			if (physicalInstanceId != null)
			{
				stringBuilder.Append("P" + physicalInstanceId.ToString());
			}
			if (objectTypes != null && objectTypes.Length > 0)
			{
				stringBuilder.Append("_");
				stringBuilder.Append(string.Join("_", objectTypes.Select(delegate(Type objectType)
				{
					string result = null;
					if (!DomainSession.ObjectTypeToTrackingTagMap.TryGetValue(objectType, out result))
					{
						result = "Unkown";
					}
					return result;
				})));
			}
			stringBuilder.Append("_");
			stringBuilder.Append(isStart ? "S" : "E");
			return stringBuilder.ToString();
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000247B5 File Offset: 0x000229B5
		private string GetTrackingTag(bool isStart, string operationTag, params Type[] objectTypes)
		{
			return this.GetTrackingTag(isStart, operationTag, null, objectTypes);
		}

		// Token: 0x040005C1 RID: 1473
		public const int ConstUseExistingDomainKeyFromDB = 1;

		// Token: 0x040005C2 RID: 1474
		private const string TrackingTagSeparator = "_";

		// Token: 0x040005C3 RID: 1475
		private const string FindTrackingTag = "Fnd";

		// Token: 0x040005C4 RID: 1476
		private const string FindBulkTrackingTag = "FndB";

		// Token: 0x040005C5 RID: 1477
		private const string FindBulkDBTrackingTag = "FndBD";

		// Token: 0x040005C6 RID: 1478
		private const string FindAllPartitionsDBTrackingTag = "FndAllP";

		// Token: 0x040005C7 RID: 1479
		private const string FindBulkCacheTrackingTag = "FndBC";

		// Token: 0x040005C8 RID: 1480
		private const string SaveTrackingTag = "Sav";

		// Token: 0x040005C9 RID: 1481
		private const string DeleteTrackingTag = "Del";

		// Token: 0x040005CA RID: 1482
		private const string UpdateTrackingTag = "Upd";

		// Token: 0x040005CB RID: 1483
		private const string DalTrackingTag = "DAL";

		// Token: 0x040005CC RID: 1484
		private const string PhysicalInstanceTag = "P";

		// Token: 0x040005CD RID: 1485
		private const string UnknownTag = "Unkown";

		// Token: 0x040005CE RID: 1486
		private const string StartTag = "S";

		// Token: 0x040005CF RID: 1487
		private const string EndTag = "E";

		// Token: 0x040005D0 RID: 1488
		private const string UnknownCallerId = "Unknown";

		// Token: 0x040005D1 RID: 1489
		public static readonly Dictionary<Type, HygienePropertyDefinition> IdentifierMap = new Dictionary<Type, HygienePropertyDefinition>
		{
			{
				typeof(TargetService),
				DomainSchema.TargetServiceId
			},
			{
				typeof(DomainTargetEnvironment),
				DomainSchema.DomainTargetEnvironmentId
			},
			{
				typeof(TenantTargetEnvironment),
				DomainSchema.TenantTargetEnvironmentId
			},
			{
				typeof(UserTargetEnvironment),
				DomainSchema.UserTargetEnvironmentId
			},
			{
				typeof(Zone),
				DomainSchema.ZoneId
			},
			{
				typeof(NsResourceRecord),
				DomainSchema.ResourceRecordId
			},
			{
				typeof(SoaResourceRecord),
				DomainSchema.ResourceRecordId
			}
		};

		// Token: 0x040005D2 RID: 1490
		private static readonly Dictionary<Type, string> ObjectTypeToTrackingTagMap = new Dictionary<Type, string>
		{
			{
				typeof(TargetService),
				"TS"
			},
			{
				typeof(DomainTargetEnvironment),
				"D"
			},
			{
				typeof(TenantTargetEnvironment),
				"T"
			},
			{
				typeof(UserTargetEnvironment),
				"U"
			},
			{
				typeof(Zone),
				"ZN"
			},
			{
				typeof(NsResourceRecord),
				"NS"
			},
			{
				typeof(SoaResourceRecord),
				"SOA"
			}
		};

		// Token: 0x040005D3 RID: 1491
		private static readonly Dictionary<Type, DomainSession.CacheTrackingTypes> ObjectTypeToCacheTrackingTypeMap = new Dictionary<Type, DomainSession.CacheTrackingTypes>
		{
			{
				typeof(TargetService),
				DomainSession.CacheTrackingTypes.TargetService
			},
			{
				typeof(DomainTargetEnvironment),
				DomainSession.CacheTrackingTypes.Domain
			},
			{
				typeof(TenantTargetEnvironment),
				DomainSession.CacheTrackingTypes.Tenant
			}
		};

		// Token: 0x040005D4 RID: 1492
		private IConfigDataProvider dataProvider;

		// Token: 0x040005D5 RID: 1493
		private IConfigDataProvider webStoreDataProvider;

		// Token: 0x040005D6 RID: 1494
		private static readonly Random randomGenerator = new Random();

		// Token: 0x040005D7 RID: 1495
		private static readonly CacheItemPolicy propDefinitioncachItemPolicy = new CacheItemPolicy();

		// Token: 0x040005D8 RID: 1496
		private readonly Guid transactionId;

		// Token: 0x040005D9 RID: 1497
		private readonly string callerId;

		// Token: 0x040005DA RID: 1498
		private IConfigDataProvider cacheDataProvider;

		// Token: 0x040005DB RID: 1499
		private IConfigDataProvider cacheFallbackDataProvider;

		// Token: 0x02000125 RID: 293
		public enum CacheTrackingTypes
		{
			// Token: 0x040005E5 RID: 1509
			TypeMinIndex,
			// Token: 0x040005E6 RID: 1510
			Tenant,
			// Token: 0x040005E7 RID: 1511
			Domain,
			// Token: 0x040005E8 RID: 1512
			TargetService,
			// Token: 0x040005E9 RID: 1513
			TypeMaxIndex
		}

		// Token: 0x02000126 RID: 294
		public enum CacheTrackingCategory
		{
			// Token: 0x040005EB RID: 1515
			CategoryMinIndex,
			// Token: 0x040005EC RID: 1516
			CacheHealthy,
			// Token: 0x040005ED RID: 1517
			CacheUnHealthy,
			// Token: 0x040005EE RID: 1518
			CacheHit,
			// Token: 0x040005EF RID: 1519
			CacheMiss,
			// Token: 0x040005F0 RID: 1520
			CategoryMaxIndex
		}

		// Token: 0x02000127 RID: 295
		private class InputEqualityComparer<TInput> : IEqualityComparer<TInput>
		{
			// Token: 0x06000B57 RID: 2903 RVA: 0x00024964 File Offset: 0x00022B64
			public bool Equals(TInput x, TInput y)
			{
				if (typeof(TInput) == typeof(string))
				{
					return string.Equals(Convert.ToString(x), Convert.ToString(y), StringComparison.OrdinalIgnoreCase);
				}
				return x.Equals(y);
			}

			// Token: 0x06000B58 RID: 2904 RVA: 0x000249BC File Offset: 0x00022BBC
			public int GetHashCode(TInput obj)
			{
				if (typeof(TInput) == typeof(string))
				{
					return Convert.ToString(obj).ToLower().GetHashCode();
				}
				return obj.GetHashCode();
			}
		}
	}
}
