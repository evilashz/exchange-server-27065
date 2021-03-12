using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007DD RID: 2013
	public sealed class MsoCookieConverter
	{
		// Token: 0x060063C6 RID: 25542 RVA: 0x0015A33C File Offset: 0x0015853C
		public void MigrateE14CookieToE15Cookie(string serviceInstanceName)
		{
			SyncServiceInstance syncServiceInstance = ServiceInstanceId.GetSyncServiceInstance(serviceInstanceName);
			if (syncServiceInstance == null)
			{
				throw new ArgumentException("serviceInstanceName could not be found");
			}
			int maxCookieHistoryCount = 100;
			TimeSpan cookieHistoryInterval = new TimeSpan(0, 30, 0);
			if (syncServiceInstance.IsMultiObjectCookieEnabled)
			{
				MsoCookieConverter.PerformConversion(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval);
				return;
			}
			MsoCookieConverter.ConvertE14MVtoE15MV(syncServiceInstance, maxCookieHistoryCount, cookieHistoryInterval);
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0015A388 File Offset: 0x00158588
		public void MigrateE15CookieToE14Cookie(string serviceInstanceName)
		{
			SyncServiceInstance syncServiceInstance = ServiceInstanceId.GetSyncServiceInstance(serviceInstanceName);
			if (syncServiceInstance != null)
			{
				int maxCookieHistoryCount = 100;
				TimeSpan cookieHistoryInterval = new TimeSpan(0, 30, 0);
				MsoCookieConverter.ConvertE15toE14MV(syncServiceInstance, maxCookieHistoryCount, cookieHistoryInterval);
				return;
			}
			throw new ArgumentException("serviceInstanceName could not be found");
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0015A3C0 File Offset: 0x001585C0
		public static void ConvertE15toE14MV(SyncServiceInstance serviceInstance, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval)
		{
			ITopologyConfigurationSession ridmasterSession = MsoCookieConverter.GetRIDMasterSession();
			MsoMainStreamCookie cookieToAdd = null;
			MsoMainStreamCookie cookieToAdd2 = null;
			if (serviceInstance.IsMultiObjectCookieEnabled)
			{
				MsoMultiObjectCookieManager msoMultiObjectCookieManager = new MsoMultiObjectCookieManager(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval, ForwardSyncCookieType.CompanyIncremental);
				MsoMultiObjectCookieManager msoMultiObjectCookieManager2 = new MsoMultiObjectCookieManager(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval, ForwardSyncCookieType.RecipientIncremental);
				ForwardSyncCookie forwardSyncCookie = msoMultiObjectCookieManager.ReadMostRecentCookie();
				ForwardSyncCookie forwardSyncCookie2 = msoMultiObjectCookieManager2.ReadMostRecentCookie();
				if (forwardSyncCookie != null)
				{
					cookieToAdd = new MsoMainStreamCookie(serviceInstance.Name, forwardSyncCookie.Data, forwardSyncCookie.Timestamp, new ServerVersion(forwardSyncCookie.SyncPropertySetVersion), forwardSyncCookie.IsUpgradingSyncPropertySet);
				}
				if (forwardSyncCookie2 != null)
				{
					cookieToAdd2 = new MsoMainStreamCookie(serviceInstance.Name, forwardSyncCookie2.Data, forwardSyncCookie2.Timestamp, new ServerVersion(forwardSyncCookie2.SyncPropertySetVersion), forwardSyncCookie2.IsUpgradingSyncPropertySet);
				}
			}
			else
			{
				MsoCompanyMainStreamCookieManager msoCompanyMainStreamCookieManager = new MsoCompanyMainStreamCookieManager(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval);
				MsoRecipientMainStreamCookieManager msoRecipientMainStreamCookieManager = new MsoRecipientMainStreamCookieManager(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval);
				cookieToAdd = msoCompanyMainStreamCookieManager.ReadMostRecentCookie();
				cookieToAdd2 = msoRecipientMainStreamCookieManager.ReadMostRecentCookie();
			}
			ridmasterSession.UseConfigNC = true;
			MsoCookieConverter.E14MsoMainStreamCookieContainer[] array = ridmasterSession.Find<MsoCookieConverter.E14MsoMainStreamCookieContainer>(ridmasterSession.GetOrgContainerId().Parent, QueryScope.SubTree, null, null, 1);
			if (array == null || array.Length == 0)
			{
				throw new ExchangeConfigurationContainerNotFoundException();
			}
			List<MsoMainStreamCookie> list = MsoCookieConverter.GetOrderedE14CookieList(array[0].MsoForwardSyncNonRecipientCookie);
			List<MsoMainStreamCookie> list2 = MsoCookieConverter.GetOrderedE14CookieList(array[0].MsoForwardSyncRecipientCookie);
			list = MsoCookieConverter.AddCookieToE14MVList(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval, cookieToAdd, list);
			list2 = MsoCookieConverter.AddCookieToE14MVList(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval, cookieToAdd2, list2);
			array[0].MsoForwardSyncNonRecipientCookie.Clear();
			foreach (MsoMainStreamCookie msoMainStreamCookie in list)
			{
				array[0].MsoForwardSyncNonRecipientCookie.Add(msoMainStreamCookie.ToStorageCookie());
			}
			array[0].MsoForwardSyncRecipientCookie.Clear();
			foreach (MsoMainStreamCookie msoMainStreamCookie2 in list2)
			{
				array[0].MsoForwardSyncRecipientCookie.Add(msoMainStreamCookie2.ToStorageCookie());
			}
			ridmasterSession.Save(array[0]);
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0015A5DC File Offset: 0x001587DC
		public static void ConvertE14MVtoE15MV(SyncServiceInstance serviceInstance, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval)
		{
			ITopologyConfigurationSession ridmasterSession = MsoCookieConverter.GetRIDMasterSession();
			ridmasterSession.UseConfigNC = true;
			ADObjectId orgContainerId = ridmasterSession.GetOrgContainerId();
			ADRawEntry adrawEntry = ridmasterSession.ReadADRawEntry(orgContainerId.Parent, MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.CookieProperties);
			ridmasterSession.UseConfigNC = false;
			List<MsoMainStreamCookie> ordredCookieList = MsoCookieConverter.GetOrdredCookieList(serviceInstance.Name, adrawEntry.propertyBag[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie] as IEnumerable<byte[]>);
			List<MsoMainStreamCookie> ordredCookieList2 = MsoCookieConverter.GetOrdredCookieList(serviceInstance.Name, adrawEntry.propertyBag[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie] as IEnumerable<byte[]>);
			MsoCompanyMainStreamCookieManager msoCompanyMainStreamCookieManager = new MsoCompanyMainStreamCookieManager(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval);
			MsoRecipientMainStreamCookieManager msoRecipientMainStreamCookieManager = new MsoRecipientMainStreamCookieManager(serviceInstance.Name, maxCookieHistoryCount, cookieHistoryInterval);
			if (ordredCookieList.Count > 0)
			{
				MsoMainStreamCookie msoMainStreamCookie = ordredCookieList.Last<MsoMainStreamCookie>();
				msoCompanyMainStreamCookieManager.WriteCookie(msoMainStreamCookie.RawCookie, null, msoMainStreamCookie.TimeStamp, msoMainStreamCookie.IsSyncPropertySetUpgrading, msoMainStreamCookie.SyncPropertySetVersion, true);
			}
			if (ordredCookieList2.Count > 0)
			{
				MsoMainStreamCookie msoMainStreamCookie2 = ordredCookieList2.Last<MsoMainStreamCookie>();
				msoRecipientMainStreamCookieManager.WriteCookie(msoMainStreamCookie2.RawCookie, null, msoMainStreamCookie2.TimeStamp, msoMainStreamCookie2.IsSyncPropertySetUpgrading, msoMainStreamCookie2.SyncPropertySetVersion, true);
			}
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x0015A6E8 File Offset: 0x001588E8
		public static void PerformConversion(string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval)
		{
			SyncServiceInstance syncServiceInstance = ServiceInstanceId.GetSyncServiceInstance(serviceInstanceName);
			if (syncServiceInstance != null && syncServiceInstance.IsMultiObjectCookieEnabled)
			{
				MsoMultiObjectCookieManager msoMultiObjectCookieManager = new MsoMultiObjectCookieManager(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval, ForwardSyncCookieType.RecipientIncremental);
				MsoMultiObjectCookieManager msoMultiObjectCookieManager2 = new MsoMultiObjectCookieManager(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval, ForwardSyncCookieType.CompanyIncremental);
				ITopologyConfigurationSession ridmasterSession = MsoCookieConverter.GetRIDMasterSession();
				MsoMainStreamCookieContainer msoMainStreamCookieContainer = ridmasterSession.GetMsoMainStreamCookieContainer(serviceInstanceName);
				MsoCookieConverter.FromMultiValuedCookieToMultiObjectCookie(serviceInstanceName, msoMainStreamCookieContainer.MsoForwardSyncRecipientCookie, msoMultiObjectCookieManager);
				MsoCookieConverter.FromMultiValuedCookieToMultiObjectCookie(serviceInstanceName, msoMainStreamCookieContainer.MsoForwardSyncNonRecipientCookie, msoMultiObjectCookieManager2);
				ridmasterSession.UseConfigNC = true;
				ADObjectId orgContainerId = ridmasterSession.GetOrgContainerId();
				ADRawEntry adrawEntry = ridmasterSession.ReadADRawEntry(orgContainerId.Parent, MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.CookieProperties);
				MsoCookieConverter.FromMultiValuedCookieToMultiObjectCookie(serviceInstanceName, adrawEntry.propertyBag[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie] as IEnumerable<byte[]>, msoMultiObjectCookieManager);
				MsoCookieConverter.FromMultiValuedCookieToMultiObjectCookie(serviceInstanceName, adrawEntry.propertyBag[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie] as IEnumerable<byte[]>, msoMultiObjectCookieManager2);
			}
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x0015A7AC File Offset: 0x001589AC
		private static List<MsoMainStreamCookie> AddCookieToE14MVList(string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval, MsoMainStreamCookie cookieToAdd, List<MsoMainStreamCookie> cookies)
		{
			List<MsoMainStreamCookieWithIndex> list = new List<MsoMainStreamCookieWithIndex>();
			for (int i = cookies.Count - 1; i >= 0; i--)
			{
				list.Add(new MsoMainStreamCookieWithIndex(cookies[i], i));
			}
			List<MsoMainStreamCookieWithIndex> list2 = MsoCookieConverter.SortMainStreamCookieList(list);
			if (list2.Count >= 2)
			{
				MsoMainStreamCookieWithIndex msoMainStreamCookieWithIndex = list2[list2.Count - 1];
				MsoMainStreamCookieWithIndex msoMainStreamCookieWithIndex2 = list2[list2.Count - 2];
				if (msoMainStreamCookieWithIndex.Cookie.TimeStamp < new DateTime(msoMainStreamCookieWithIndex2.Cookie.TimeStamp.Ticks + cookieHistoryInterval.Ticks, DateTimeKind.Utc))
				{
					cookies.RemoveAt(msoMainStreamCookieWithIndex.Index);
				}
				else if (list2.Count >= maxCookieHistoryCount)
				{
					cookies.RemoveAt(list2[0].Index);
				}
			}
			if (cookieToAdd != null)
			{
				cookies.Add(cookieToAdd);
			}
			return cookies;
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x0015A884 File Offset: 0x00158A84
		private static void FromMultiValuedCookieToMultiObjectCookie(string serviceInstanceName, IEnumerable<byte[]> multiValueCookieAttribute, MsoMultiObjectCookieManager msoMultiObjectCookieManager)
		{
			List<MsoMainStreamCookie> ordredCookieList = MsoCookieConverter.GetOrdredCookieList(serviceInstanceName, multiValueCookieAttribute);
			if (ordredCookieList.Count > 0)
			{
				ForwardSyncCookie forwardSyncCookie = msoMultiObjectCookieManager.ReadMostRecentCookie();
				MsoMainStreamCookie msoMainStreamCookie = ordredCookieList.Last<MsoMainStreamCookie>();
				if (forwardSyncCookie == null || msoMainStreamCookie.TimeStamp > forwardSyncCookie.Timestamp)
				{
					msoMultiObjectCookieManager.WriteCookie(msoMainStreamCookie.RawCookie, null, msoMainStreamCookie.TimeStamp, msoMainStreamCookie.IsSyncPropertySetUpgrading, msoMainStreamCookie.SyncPropertySetVersion, true);
				}
			}
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0015A8E8 File Offset: 0x00158AE8
		private static ITopologyConfigurationSession GetRIDMasterSession()
		{
			string name;
			using (Domain computerDomain = Domain.GetComputerDomain())
			{
				name = computerDomain.RidRoleOwner.Name;
			}
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(name, false, ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 327, "GetRIDMasterSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\CookieManager\\MsoCookieConverter.cs");
			topologyConfigurationSession.UseConfigNC = false;
			return topologyConfigurationSession;
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x0015A964 File Offset: 0x00158B64
		private static List<MsoMainStreamCookie> GetOrderedE14CookieList(IEnumerable<byte[]> multiValueCookieAttribute)
		{
			List<MsoMainStreamCookie> list = new List<MsoMainStreamCookie>();
			if (multiValueCookieAttribute != null)
			{
				list.AddRange(multiValueCookieAttribute.Select(new Func<byte[], MsoMainStreamCookie>(MsoMainStreamCookie.FromStorageCookie)));
				list.Sort((MsoMainStreamCookie x, MsoMainStreamCookie y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
			}
			return list;
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x0015A9E8 File Offset: 0x00158BE8
		private static List<MsoMainStreamCookie> GetOrdredCookieList(string serviceInstanceName, IEnumerable<byte[]> multiValueCookieAttribute)
		{
			List<MsoMainStreamCookie> list = new List<MsoMainStreamCookie>();
			if (multiValueCookieAttribute != null)
			{
				list.AddRange(from cookie in multiValueCookieAttribute.Select(new Func<byte[], MsoMainStreamCookie>(MsoMainStreamCookie.FromStorageCookie))
				where serviceInstanceName.Equals(cookie.ServiceInstanceName, StringComparison.OrdinalIgnoreCase)
				select cookie);
				list.Sort((MsoMainStreamCookie x, MsoMainStreamCookie y) => DateTime.Compare(x.TimeStamp, y.TimeStamp));
			}
			return list;
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x0015AA6C File Offset: 0x00158C6C
		private static List<MsoMainStreamCookieWithIndex> SortMainStreamCookieList(List<MsoMainStreamCookieWithIndex> cookieList)
		{
			List<MsoMainStreamCookieWithIndex> list = new List<MsoMainStreamCookieWithIndex>();
			IOrderedEnumerable<MsoMainStreamCookieWithIndex> orderedEnumerable = from cookieWithIndex in cookieList
			orderby cookieWithIndex.Cookie.TimeStamp
			select cookieWithIndex;
			foreach (MsoMainStreamCookieWithIndex item in orderedEnumerable)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x020007DE RID: 2014
		internal sealed class E14MsoMainStreamCookieContainerSchema : ADContainerSchema
		{
			// Token: 0x04004271 RID: 17009
			internal static readonly ADPropertyDefinition MsoForwardSyncRecipientCookie = new ADPropertyDefinition("MsoForwardSyncRecipientCookie", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchMsoForwardSyncRecipientCookie", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new ByteArrayLengthConstraint(1, 49152)
			}, null, null);

			// Token: 0x04004272 RID: 17010
			internal static readonly ADPropertyDefinition MsoForwardSyncNonRecipientCookie = new ADPropertyDefinition("MsoForwardSyncNonRecipientCookie", ExchangeObjectVersion.Exchange2003, typeof(byte[]), "msExchMsoForwardSyncNonRecipientCookie", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
			{
				new ByteArrayLengthConstraint(1, 49152)
			}, null, null);

			// Token: 0x04004273 RID: 17011
			public static readonly ADPropertyDefinition[] CookieProperties = new ADPropertyDefinition[]
			{
				MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie,
				MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie
			};
		}

		// Token: 0x020007DF RID: 2015
		[ObjectScope(ConfigScopes.Global)]
		[Serializable]
		internal sealed class E14MsoMainStreamCookieContainer : ADContainer
		{
			// Token: 0x17002364 RID: 9060
			// (get) Token: 0x060063D6 RID: 25558 RVA: 0x0015AB9E File Offset: 0x00158D9E
			// (set) Token: 0x060063D7 RID: 25559 RVA: 0x0015ABB0 File Offset: 0x00158DB0
			internal MultiValuedProperty<byte[]> MsoForwardSyncRecipientCookie
			{
				get
				{
					return (MultiValuedProperty<byte[]>)this[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie];
				}
				set
				{
					this[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncRecipientCookie] = value;
				}
			}

			// Token: 0x17002365 RID: 9061
			// (get) Token: 0x060063D8 RID: 25560 RVA: 0x0015ABBE File Offset: 0x00158DBE
			// (set) Token: 0x060063D9 RID: 25561 RVA: 0x0015ABD0 File Offset: 0x00158DD0
			internal MultiValuedProperty<byte[]> MsoForwardSyncNonRecipientCookie
			{
				get
				{
					return (MultiValuedProperty<byte[]>)this[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie];
				}
				set
				{
					this[MsoCookieConverter.E14MsoMainStreamCookieContainerSchema.MsoForwardSyncNonRecipientCookie] = value;
				}
			}

			// Token: 0x17002366 RID: 9062
			// (get) Token: 0x060063DA RID: 25562 RVA: 0x0015ABDE File Offset: 0x00158DDE
			internal override string MostDerivedObjectClass
			{
				get
				{
					return "msExchConfigurationContainer";
				}
			}

			// Token: 0x17002367 RID: 9063
			// (get) Token: 0x060063DB RID: 25563 RVA: 0x0015ABE5 File Offset: 0x00158DE5
			internal override ADObjectSchema Schema
			{
				get
				{
					return MsoCookieConverter.E14MsoMainStreamCookieContainer.schema;
				}
			}

			// Token: 0x04004274 RID: 17012
			private const string MostDerivedObjectClassInternal = "msExchConfigurationContainer";

			// Token: 0x04004275 RID: 17013
			private static MsoCookieConverter.E14MsoMainStreamCookieContainerSchema schema = ObjectSchema.GetInstance<MsoCookieConverter.E14MsoMainStreamCookieContainerSchema>();
		}
	}
}
