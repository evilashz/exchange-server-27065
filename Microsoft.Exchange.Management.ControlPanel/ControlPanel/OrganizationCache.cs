using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000257 RID: 599
	public static class OrganizationCache
	{
		// Token: 0x060028B0 RID: 10416 RVA: 0x00080168 File Offset: 0x0007E368
		static OrganizationCache()
		{
			int num = 0;
			if (!int.TryParse(ConfigurationManager.AppSettings["OrgCacheLifetimeInMinute"], out num))
			{
				num = 30;
			}
			else if (num < 15)
			{
				num = 15;
			}
			OrganizationCache.orgCacheLifeTime = new TimeSpan(0, num, 0);
			OrganizationCache.RegisterImpl("EntHasTargetDeliveryDomain", "Get-RemoteDomain", new OrganizationCache.LoadHandler(OrganizationCache.LoadTargetDeliveryDomain), !Util.IsDataCenter, OrganizationCacheExpirationType.Default, null);
			OrganizationCache.RegisterImpl("EntTargetDeliveryDomain", "Get-RemoteDomain", new OrganizationCache.LoadHandler(OrganizationCache.LoadTargetDeliveryDomain), !Util.IsDataCenter, OrganizationCacheExpirationType.Default, null);
			OrganizationCache.RegisterImpl("CrossPremiseUrlFormat", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseUrl), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("CrossPremiseServer", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseUrl), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("CrossPremiseUrlFormatWorldWide", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseUrl), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("CrossPremiseServerWorldWide", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseUrl), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("CrossPremiseUrlFormatGallatin", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseUrl), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("CrossPremiseServerGallatin", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseUrl), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("ServiceInstance", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadCrossPremiseServiceInstance), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("RestrictIOCToSP1OrGreaterWorldWide", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadRestrictIOCToSP1OrGreater), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("RestrictIOCToSP1OrGreaterGallatin", "ControlPanelAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadRestrictIOCToSP1OrGreater), !Util.IsDataCenter, OrganizationCacheExpirationType.Never, null);
			OrganizationCache.RegisterImpl("DCIsDirSyncRunning", "HybridAdmin", new OrganizationCache.LoadHandler(OrganizationCache.LoadDCIsDirSyncRunning), Util.IsMicrosoftHostedOnly, OrganizationCacheExpirationType.Default, null);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000803EC File Offset: 0x0007E5EC
		public static void Register(string key, string role, OrganizationCache.LoadHandler loader, bool skuMatch = true, OrganizationCacheExpirationType expiration = OrganizationCacheExpirationType.Default, TimeSpan? customExpireTime = null)
		{
			lock (OrganizationCache.definitionStore)
			{
				OrganizationCache.RegisterImpl(key, role, loader, skuMatch, expiration, customExpireTime);
			}
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x00080434 File Offset: 0x0007E634
		private static void RegisterImpl(string key, string role, OrganizationCache.LoadHandler loader, bool skuMatch = true, OrganizationCacheExpirationType expiration = OrganizationCacheExpirationType.Default, TimeSpan? customExpireTime = null)
		{
			if (expiration == OrganizationCacheExpirationType.Custom != (customExpireTime != null))
			{
				throw new ArgumentException("CustomExpireTime must be specified together with expiration as Custom.");
			}
			TimeSpan item = (expiration == OrganizationCacheExpirationType.Default) ? OrganizationCache.orgCacheLifeTime : ((expiration == OrganizationCacheExpirationType.Never) ? TimeSpan.MaxValue : customExpireTime.Value);
			Tuple<bool, string, OrganizationCache.LoadHandler, TimeSpan> value = new Tuple<bool, string, OrganizationCache.LoadHandler, TimeSpan>(skuMatch, role, loader, item);
			OrganizationCache.definitionStore.Add(key, value);
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0008048F File Offset: 0x0007E68F
		public static bool KeyRegistered(string key)
		{
			return OrganizationCache.definitionStore.ContainsKey(key);
		}

		// Token: 0x17001C69 RID: 7273
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x0008049C File Offset: 0x0007E69C
		private static Dictionary<string, Tuple<object, DateTime>> ValueStore
		{
			get
			{
				Dictionary<string, Tuple<object, DateTime>> result = null;
				if (OrganizationCache.orgIdForTest == null && !Util.IsDataCenter)
				{
					if (OrganizationCache.entValueStore == null)
					{
						lock (OrganizationCache.syncObject)
						{
							if (OrganizationCache.entValueStore == null)
							{
								OrganizationCache.entValueStore = new Dictionary<string, Tuple<object, DateTime>>(16, StringComparer.OrdinalIgnoreCase);
							}
						}
					}
					result = OrganizationCache.entValueStore;
				}
				else
				{
					if (OrganizationCache.dcValueStores == null)
					{
						lock (OrganizationCache.syncObject)
						{
							if (OrganizationCache.dcValueStores == null)
							{
								OrganizationCache.dcValueStores = new MruDictionaryCache<string, Dictionary<string, Tuple<object, DateTime>>>(32, 720);
							}
						}
					}
					string text = OrganizationCache.orgIdForTest ?? RbacPrincipal.Current.RbacConfiguration.OrganizationId.OrganizationalUnit.ToString();
					if (!OrganizationCache.dcValueStores.TryGetValue(text, out result))
					{
						lock (OrganizationCache.dcValueStores)
						{
							if (!OrganizationCache.dcValueStores.TryGetValue(text, out result))
							{
								OrganizationCache.dcValueStores.Add(text, new Dictionary<string, Tuple<object, DateTime>>(16, StringComparer.OrdinalIgnoreCase));
							}
						}
						result = OrganizationCache.dcValueStores[text];
					}
				}
				return result;
			}
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000805EC File Offset: 0x0007E7EC
		internal static void SetTestTenantId(string testTenantId)
		{
			OrganizationCache.orgIdForTest = testTenantId;
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000805F4 File Offset: 0x0007E7F4
		internal static void ExpireEntry(string key)
		{
			if (OrganizationCache.ValueStore.ContainsKey(key))
			{
				OrganizationCache.ValueStore.Remove(key);
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x00080610 File Offset: 0x0007E810
		public static T GetValue<T>(string key)
		{
			T result;
			OrganizationCache.TryGetValue<T>(key, out result);
			return result;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00080628 File Offset: 0x0007E828
		public static bool TryGetValue<T>(string key, out T value)
		{
			bool flag = false;
			value = default(T);
			Tuple<bool, string, OrganizationCache.LoadHandler, TimeSpan> tuple = null;
			if (OrganizationCache.definitionStore.TryGetValue(key, out tuple) && tuple.Item1)
			{
				Dictionary<string, Tuple<object, DateTime>> valueStore = OrganizationCache.ValueStore;
				flag = (string.IsNullOrEmpty(tuple.Item2) || RbacPrincipal.Current.IsInRole(tuple.Item2));
				if (flag)
				{
					Tuple<object, DateTime> tuple2;
					if (!valueStore.TryGetValue(key, out tuple2) || tuple2.Item2 < DateTime.UtcNow)
					{
						tuple.Item3(new OrganizationCache.AddValueHandler(OrganizationCache.AddValue), new OrganizationCache.LogErrorHandler(OrganizationCache.LogError));
						valueStore.TryGetValue(key, out tuple2);
					}
					if (tuple2 != null)
					{
						value = (T)((object)tuple2.Item1);
					}
					else
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000806EC File Offset: 0x0007E8EC
		private static void AddValue(string key, object value)
		{
			TimeSpan item = OrganizationCache.definitionStore[key].Item4;
			DateTime item2 = (item == TimeSpan.MaxValue) ? DateTime.MaxValue : (DateTime.UtcNow + item);
			Dictionary<string, Tuple<object, DateTime>> valueStore = OrganizationCache.ValueStore;
			Tuple<object, DateTime> value2 = new Tuple<object, DateTime>(value, item2);
			lock (valueStore)
			{
				valueStore[key] = value2;
			}
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0008076C File Offset: 0x0007E96C
		private static void LogError(string key, string errorMessage)
		{
			EcpEventLogConstants.Tuple_UnableToDetectRbacRoleViaCmdlet.LogEvent(new object[]
			{
				EcpEventLogExtensions.GetUserNameToLog(),
				key,
				errorMessage
			});
		}

		// Token: 0x17001C6A RID: 7274
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x0008079B File Offset: 0x0007E99B
		public static bool EntHasTargetDeliveryDomain
		{
			get
			{
				return OrganizationCache.GetValue<bool>("EntHasTargetDeliveryDomain");
			}
		}

		// Token: 0x17001C6B RID: 7275
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x000807A7 File Offset: 0x0007E9A7
		public static string EntTargetDeliveryDomain
		{
			get
			{
				return OrganizationCache.GetValue<string>("EntTargetDeliveryDomain");
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000807B4 File Offset: 0x0007E9B4
		private static void LoadTargetDeliveryDomain(OrganizationCache.AddValueHandler addValue, OrganizationCache.LogErrorHandler logError)
		{
			WebServiceReference webServiceReference = new WebServiceReference("~/DDI/DDIService.svc?schema=RemoteDomains");
			PowerShellResults<JsonDictionary<object>> list = webServiceReference.GetList(null, null);
			bool flag = false;
			string value = null;
			if (list.Output != null)
			{
				for (int i = 0; i < list.Output.Length; i++)
				{
					Dictionary<string, object> dictionary = list.Output[i];
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						if (keyValuePair.Key == "DomainName")
						{
							value = (string)keyValuePair.Value;
						}
						else if (keyValuePair.Key == "TargetDeliveryDomain")
						{
							flag = (bool)keyValuePair.Value;
						}
					}
					if (flag)
					{
						break;
					}
					value = null;
				}
			}
			if (!list.ErrorRecords.IsNullOrEmpty())
			{
				string errorMessage = list.ErrorRecords[0].ToString();
				logError("EntHasTargetDeliveryDomain", errorMessage);
			}
			addValue("EntTargetDeliveryDomain", value);
			addValue("EntHasTargetDeliveryDomain", flag);
		}

		// Token: 0x17001C6C RID: 7276
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000808DC File Offset: 0x0007EADC
		public static string ServiceInstance
		{
			get
			{
				return OrganizationCache.GetValue<string>("ServiceInstance");
			}
		}

		// Token: 0x17001C6D RID: 7277
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x000808E8 File Offset: 0x0007EAE8
		public static bool EntHasServiceInstance
		{
			get
			{
				return !string.IsNullOrEmpty(OrganizationCache.ServiceInstance);
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000808F8 File Offset: 0x0007EAF8
		private static void LoadCrossPremiseServiceInstance(OrganizationCache.AddValueHandler addValue, OrganizationCache.LogErrorHandler logError)
		{
			string value = string.Empty;
			try
			{
				WebServiceReference webServiceReference = new WebServiceReference("~/DDI/DDIService.svc?schema=HybridConfigurationWizardService&workflow=GetServiceInstance");
				PowerShellResults<JsonDictionary<object>> powerShellResults = (PowerShellResults<JsonDictionary<object>>)webServiceReference.GetObject(null);
				if (powerShellResults.Output.Length > 0)
				{
					value = (string)powerShellResults.Output[0].RawDictionary["ServiceInstance"];
				}
			}
			catch (Exception ex)
			{
				logError("ServiceInstance", ex.ToString());
			}
			addValue("ServiceInstance", value);
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x00080980 File Offset: 0x0007EB80
		public static string CrossPremiseServer
		{
			get
			{
				return OrganizationCache.GetValue<string>("CrossPremiseServer");
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x0008098C File Offset: 0x0007EB8C
		public static string CrossPremiseServerWorldWide
		{
			get
			{
				return OrganizationCache.GetValue<string>("CrossPremiseServerWorldWide");
			}
		}

		// Token: 0x17001C70 RID: 7280
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x00080998 File Offset: 0x0007EB98
		public static string CrossPremiseServerGallatin
		{
			get
			{
				return OrganizationCache.GetValue<string>("CrossPremiseServerGallatin");
			}
		}

		// Token: 0x17001C71 RID: 7281
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000809A4 File Offset: 0x0007EBA4
		public static string CrossPremiseUrlFormat
		{
			get
			{
				return OrganizationCache.GetValue<string>("CrossPremiseUrlFormat");
			}
		}

		// Token: 0x17001C72 RID: 7282
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000809B0 File Offset: 0x0007EBB0
		public static string CrossPremiseUrlFormatWorldWide
		{
			get
			{
				return OrganizationCache.GetValue<string>("CrossPremiseUrlFormatWorldWide");
			}
		}

		// Token: 0x17001C73 RID: 7283
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000809BC File Offset: 0x0007EBBC
		public static string CrossPremiseUrlFormatGallatin
		{
			get
			{
				return OrganizationCache.GetValue<string>("CrossPremiseUrlFormatGallatin");
			}
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x000809C8 File Offset: 0x0007EBC8
		private static void LoadCrossPremiseUrl(OrganizationCache.AddValueHandler addValue, OrganizationCache.LogErrorHandler logError)
		{
			string value = string.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			string value2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			try
			{
				string text5 = ConfigurationManager.AppSettings["HybridServerUrl0"] ?? "https://outlook.office365.com/ecp/";
				string text6 = ConfigurationManager.AppSettings["HybridServerUrl1"] ?? "https://partner.outlook.cn/ecp/";
				text = new Uri(text5).Host;
				text3 = string.Format("{0}hybrid.aspx?xprs={{0}}&xprf={{1}}&xprv={1}&realm={{2}}&exsvurl=1", text5, Util.ApplicationVersion);
				text2 = new Uri(text6).Host;
				text4 = string.Format("{0}hybrid.aspx?xprs={{0}}&xprf={{1}}&xprv={1}&realm={{2}}&exsvurl=1", text6, Util.ApplicationVersion);
			}
			catch (UriFormatException ex)
			{
				logError("CrossPremiseServer", ex.ToString());
			}
			string serviceInstance;
			if ((serviceInstance = OrganizationCache.ServiceInstance) != null)
			{
				if (!(serviceInstance == "0"))
				{
					if (serviceInstance == "1")
					{
						value = text2;
						value2 = text4;
					}
				}
				else
				{
					value = text;
					value2 = text3;
				}
			}
			addValue("CrossPremiseServer", value);
			addValue("CrossPremiseUrlFormat", value2);
			addValue("CrossPremiseServerWorldWide", text);
			addValue("CrossPremiseUrlFormatWorldWide", text3);
			addValue("CrossPremiseServerGallatin", text2);
			addValue("CrossPremiseUrlFormatGallatin", text4);
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x00080B18 File Offset: 0x0007ED18
		public static bool DCIsDirSyncRunning
		{
			get
			{
				return OrganizationCache.GetValue<bool>("DCIsDirSyncRunning");
			}
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x00080B24 File Offset: 0x0007ED24
		private static void LoadDCIsDirSyncRunning(OrganizationCache.AddValueHandler addValue, OrganizationCache.LogErrorHandler logError)
		{
			OrganizationId organizationId = RbacPrincipal.Current.RbacConfiguration.OrganizationId;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 668, "LoadDCIsDirSyncRunning", "f:\\15.00.1497\\sources\\dev\\admin\\src\\ecp\\Organization\\OrganizationCache.cs");
			ADRawEntry adrawEntry = tenantOrTopologyConfigurationSession.ReadADRawEntry(organizationId.ConfigurationUnit, new PropertyDefinition[]
			{
				OrganizationSchema.IsDirSyncRunning
			});
			bool flag = adrawEntry != null && (bool)adrawEntry[OrganizationSchema.IsDirSyncRunning];
			addValue("DCIsDirSyncRunning", flag);
		}

		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x00080BAA File Offset: 0x0007EDAA
		public static bool RestrictIOCToSP1OrGreaterWorldWide
		{
			get
			{
				return OrganizationCache.GetValue<bool>("RestrictIOCToSP1OrGreaterWorldWide");
			}
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x00080BB6 File Offset: 0x0007EDB6
		public static bool RestrictIOCToSP1OrGreaterGallatin
		{
			get
			{
				return OrganizationCache.GetValue<bool>("RestrictIOCToSP1OrGreaterGallatin");
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x00080BC4 File Offset: 0x0007EDC4
		private static void LoadRestrictIOCToSP1OrGreater(OrganizationCache.AddValueHandler addValue, OrganizationCache.LogErrorHandler logError)
		{
			string text = "true";
			string text2 = "false";
			try
			{
				text = ConfigurationManager.AppSettings["RestrictIOCToSP1OrGreater0"];
				if (string.IsNullOrEmpty(text))
				{
					text = "true";
				}
				text2 = ConfigurationManager.AppSettings["RestrictIOCToSP1OrGreater1"];
				if (string.IsNullOrEmpty(text2))
				{
					text2 = "false";
				}
			}
			catch (UriFormatException ex)
			{
				logError("RestrictIOCToSP1OrGreaterWorldWide", ex.ToString());
			}
			addValue("RestrictIOCToSP1OrGreaterWorldWide", !string.Equals(text, "false"));
			addValue("RestrictIOCToSP1OrGreaterGallatin", !string.Equals(text2, "false"));
		}

		// Token: 0x04002083 RID: 8323
		public const string EntTargetDeliveryDomainKey = "EntTargetDeliveryDomain";

		// Token: 0x04002084 RID: 8324
		public const string EntHasTargetDeliveryDomainKey = "EntHasTargetDeliveryDomain";

		// Token: 0x04002085 RID: 8325
		public const string CrossPremiseUrlFormatKey = "CrossPremiseUrlFormat";

		// Token: 0x04002086 RID: 8326
		public const string CrossPremiseServerKey = "CrossPremiseServer";

		// Token: 0x04002087 RID: 8327
		public const string CrossPremiseUrlFormatWorldWideKey = "CrossPremiseUrlFormatWorldWide";

		// Token: 0x04002088 RID: 8328
		public const string CrossPremiseServerWorldWideKey = "CrossPremiseServerWorldWide";

		// Token: 0x04002089 RID: 8329
		public const string CrossPremiseUrlFormatGallatinKey = "CrossPremiseUrlFormatGallatin";

		// Token: 0x0400208A RID: 8330
		public const string CrossPremiseServerGallatinKey = "CrossPremiseServerGallatin";

		// Token: 0x0400208B RID: 8331
		public const string DCIsDirSyncRunningKey = "DCIsDirSyncRunning";

		// Token: 0x0400208C RID: 8332
		public const string ServiceInstanceKey = "ServiceInstance";

		// Token: 0x0400208D RID: 8333
		public const string RestrictIOCToSP1OrGreaterWorldWideKey = "RestrictIOCToSP1OrGreaterWorldWide";

		// Token: 0x0400208E RID: 8334
		public const string RestrictIOCToSP1OrGreaterGallatinKey = "RestrictIOCToSP1OrGreaterGallatin";

		// Token: 0x0400208F RID: 8335
		private const int OneOrgCacheSize = 16;

		// Token: 0x04002090 RID: 8336
		private const int CacheSizeTenantNumber = 32;

		// Token: 0x04002091 RID: 8337
		private const int TenantCacheRetireInMinutes = 720;

		// Token: 0x04002092 RID: 8338
		private const int DefaultOrgCacheLifeTimeInMinute = 30;

		// Token: 0x04002093 RID: 8339
		private const int MinOrgCacheLifeTimeInMinute = 15;

		// Token: 0x04002094 RID: 8340
		private static readonly TimeSpan orgCacheLifeTime;

		// Token: 0x04002095 RID: 8341
		private static readonly Dictionary<string, Tuple<bool, string, OrganizationCache.LoadHandler, TimeSpan>> definitionStore = new Dictionary<string, Tuple<bool, string, OrganizationCache.LoadHandler, TimeSpan>>(16, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002096 RID: 8342
		private static Dictionary<string, Tuple<object, DateTime>> entValueStore;

		// Token: 0x04002097 RID: 8343
		private static MruDictionaryCache<string, Dictionary<string, Tuple<object, DateTime>>> dcValueStores;

		// Token: 0x04002098 RID: 8344
		private static readonly object syncObject = new object();

		// Token: 0x04002099 RID: 8345
		private static string orgIdForTest = null;

		// Token: 0x02000258 RID: 600
		// (Invoke) Token: 0x060028CE RID: 10446
		public delegate void LoadHandler(OrganizationCache.AddValueHandler addValue, OrganizationCache.LogErrorHandler logError);

		// Token: 0x02000259 RID: 601
		// (Invoke) Token: 0x060028D2 RID: 10450
		public delegate void AddValueHandler(string key, object value);

		// Token: 0x0200025A RID: 602
		// (Invoke) Token: 0x060028D6 RID: 10454
		public delegate void LogErrorHandler(string key, string errorMessage);
	}
}
