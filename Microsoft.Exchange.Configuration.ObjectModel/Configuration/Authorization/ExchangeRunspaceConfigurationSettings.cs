using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Management.Automation;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x0200022F RID: 559
	public sealed class ExchangeRunspaceConfigurationSettings
	{
		// Token: 0x060013CA RID: 5066 RVA: 0x000460D8 File Offset: 0x000442D8
		internal ExchangeRunspaceConfigurationSettings() : this(null)
		{
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000460E1 File Offset: 0x000442E1
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri) : this(connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown, null, ExchangeRunspaceConfigurationSettings.SerializationLevel.Partial)
		{
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000460ED File Offset: 0x000442ED
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel) : this(connectionUri, clientApplication, tenantOrganization, serializationLevel, PSLanguageMode.NoLanguage)
		{
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000460FB File Offset: 0x000442FB
		internal ExchangeRunspaceConfigurationSettings(ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel) : this(null, clientApplication, tenantOrganization, serializationLevel, PSLanguageMode.NoLanguage)
		{
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x00046108 File Offset: 0x00044308
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, PSLanguageMode languageMode) : this(connectionUri, clientApplication, tenantOrganization, serializationLevel, languageMode, ExchangeRunspaceConfigurationSettings.ProxyMethod.RPS)
		{
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x00046118 File Offset: 0x00044318
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, PSLanguageMode languageMode, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod) : this(connectionUri, clientApplication, tenantOrganization, serializationLevel, languageMode, proxyMethod, false, false)
		{
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00046138 File Offset: 0x00044338
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, PSLanguageMode languageMode, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod, bool proxyFullSerialization, bool encodeDecodeKey) : this(connectionUri, clientApplication, tenantOrganization, serializationLevel, languageMode, proxyMethod, proxyFullSerialization, encodeDecodeKey, false)
		{
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0004615C File Offset: 0x0004435C
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, PSLanguageMode languageMode, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod, bool proxyFullSerialization, bool encodeDecodeKey, bool isProxy) : this(connectionUri, clientApplication, tenantOrganization, serializationLevel, languageMode, proxyMethod, proxyFullSerialization, encodeDecodeKey, isProxy, ExchangeRunspaceConfigurationSettings.ExchangeUserType.Unknown, null)
		{
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00046180 File Offset: 0x00044380
		internal ExchangeRunspaceConfigurationSettings(Uri connectionUri, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication, string tenantOrganization, ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel, PSLanguageMode languageMode, ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod, bool proxyFullSerialization, bool encodeDecodeKey, bool isProxy, ExchangeRunspaceConfigurationSettings.ExchangeUserType user, IEnumerable<KeyValuePair<string, string>> additionalConstraints)
		{
			this.clientApplication = clientApplication;
			this.serializationLevel = serializationLevel;
			this.tenantOrganization = tenantOrganization;
			this.languageMode = languageMode;
			this.originalConnectionUri = connectionUri;
			this.proxyMethod = proxyMethod;
			this.proxyFullSerialization = proxyFullSerialization;
			this.EncodeDecodeKey = encodeDecodeKey;
			this.IsProxy = isProxy;
			this.UserType = user;
			this.additionalConstraints = additionalConstraints;
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x000461E8 File Offset: 0x000443E8
		internal static ExchangeRunspaceConfigurationSettings GetDefaultInstance()
		{
			return ExchangeRunspaceConfigurationSettings.defaultInstance;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000461EF File Offset: 0x000443EF
		internal ExchangeRunspaceConfigurationSettings.ExchangeApplication ClientApplication
		{
			get
			{
				return this.clientApplication;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000461F7 File Offset: 0x000443F7
		internal ExchangeRunspaceConfigurationSettings.SerializationLevel CurrentSerializationLevel
		{
			get
			{
				return this.serializationLevel;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000461FF File Offset: 0x000443FF
		internal IEnumerable<KeyValuePair<string, string>> AdditionalConstraints
		{
			get
			{
				return this.additionalConstraints;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00046207 File Offset: 0x00044407
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0004620F File Offset: 0x0004440F
		internal string TenantOrganization
		{
			get
			{
				return this.tenantOrganization;
			}
			set
			{
				this.tenantOrganization = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00046218 File Offset: 0x00044418
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00046220 File Offset: 0x00044420
		internal PSLanguageMode LanguageMode
		{
			get
			{
				return this.languageMode;
			}
			set
			{
				this.languageMode = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00046229 File Offset: 0x00044429
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00046231 File Offset: 0x00044431
		internal ExchangeRunspaceConfigurationSettings.ProxyMethod CurrentProxyMethod
		{
			get
			{
				return this.proxyMethod;
			}
			set
			{
				this.proxyMethod = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004623A File Offset: 0x0004443A
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00046242 File Offset: 0x00044442
		internal bool ProxyFullSerialization
		{
			get
			{
				return this.proxyFullSerialization;
			}
			set
			{
				this.proxyFullSerialization = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004624B File Offset: 0x0004444B
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00046253 File Offset: 0x00044453
		internal bool EncodeDecodeKey { get; set; }

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0004625C File Offset: 0x0004445C
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00046264 File Offset: 0x00044464
		internal bool IsProxy { get; private set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0004626D File Offset: 0x0004446D
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x00046275 File Offset: 0x00044475
		internal ExchangeRunspaceConfigurationSettings.ExchangeUserType UserType { get; private set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0004627E File Offset: 0x0004447E
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x00046286 File Offset: 0x00044486
		internal UserToken UserToken { get; set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0004628F File Offset: 0x0004448F
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00046297 File Offset: 0x00044497
		internal string SiteRedirectionTemplate
		{
			get
			{
				return this.siteRedirectionTemplate;
			}
			set
			{
				this.siteRedirectionTemplate = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x000462A0 File Offset: 0x000444A0
		// (set) Token: 0x060013EA RID: 5098 RVA: 0x000462A8 File Offset: 0x000444A8
		internal string PodRedirectionTemplate { get; set; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x000462B1 File Offset: 0x000444B1
		internal Uri OriginalConnectionUri
		{
			get
			{
				return this.originalConnectionUri;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060013EC RID: 5100 RVA: 0x000462B9 File Offset: 0x000444B9
		// (set) Token: 0x060013ED RID: 5101 RVA: 0x000462C1 File Offset: 0x000444C1
		internal VariantConfigurationSnapshot VariantConfigurationSnapshot { get; set; }

		// Token: 0x060013EE RID: 5102 RVA: 0x000462CC File Offset: 0x000444CC
		internal static string GetVDirPathFromUriLocalPath(Uri uri)
		{
			string localPath = uri.LocalPath;
			if (string.IsNullOrEmpty(localPath) || localPath[0] != '/')
			{
				return localPath;
			}
			int num = localPath.IndexOf('/', 1);
			if (num == -1)
			{
				return localPath;
			}
			return localPath.Substring(0, num);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0004630D File Offset: 0x0004450D
		internal static ExchangeRunspaceConfigurationSettings FromUriConnectionString(string connectionString, out string vdirPath)
		{
			return ExchangeRunspaceConfigurationSettings.FromUriConnectionString(connectionString, ExchangeRunspaceConfigurationSettings.ExchangeApplication.Unknown, out vdirPath);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00046318 File Offset: 0x00044518
		internal static ExchangeRunspaceConfigurationSettings FromUriConnectionString(string connectionString, ExchangeRunspaceConfigurationSettings.ExchangeApplication defaultApplication, out string vdirPath)
		{
			Uri uri = new Uri(connectionString, UriKind.Absolute);
			vdirPath = ExchangeRunspaceConfigurationSettings.GetVDirPathFromUriLocalPath(uri);
			if (string.IsNullOrEmpty(uri.Query))
			{
				return ExchangeRunspaceConfigurationSettings.GetDefaultInstance();
			}
			NameValueCollection nameValueCollectionFromUri = LiveIdBasicAuthModule.GetNameValueCollectionFromUri(uri);
			return ExchangeRunspaceConfigurationSettings.CreateConfigurationSettingsFromNameValueCollection(uri, nameValueCollectionFromUri, defaultApplication);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00046358 File Offset: 0x00044558
		internal static ExchangeRunspaceConfigurationSettings FromRequestHeaders(string connectionString, ExchangeRunspaceConfigurationSettings.ExchangeApplication defaultApplication)
		{
			Uri uri = new Uri(connectionString, UriKind.Absolute);
			return ExchangeRunspaceConfigurationSettings.CreateConfigurationSettingsFromNameValueCollection(uri, HttpContext.Current.Request.Headers, defaultApplication);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x00046384 File Offset: 0x00044584
		internal static ExchangeRunspaceConfigurationSettings CreateConfigurationSettingsFromNameValueCollection(Uri uri, NameValueCollection collection, ExchangeRunspaceConfigurationSettings.ExchangeApplication defaultApplication)
		{
			string text = collection.Get("organization");
			ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel = ExchangeRunspaceConfigurationSettings.SerializationLevel.Partial;
			if (collection.Get("serializationLevel") != null)
			{
				Enum.TryParse<ExchangeRunspaceConfigurationSettings.SerializationLevel>(collection.Get("serializationLevel"), true, out serializationLevel);
			}
			string text2 = collection.Get("clientApplication");
			ExchangeRunspaceConfigurationSettings.ExchangeApplication exchangeApplication = defaultApplication;
			if (text2 != null)
			{
				Enum.TryParse<ExchangeRunspaceConfigurationSettings.ExchangeApplication>(text2, true, out exchangeApplication);
			}
			PSLanguageMode pslanguageMode = PSLanguageMode.NoLanguage;
			if (exchangeApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.EMC)
			{
				pslanguageMode = PSLanguageMode.NoLanguage;
			}
			ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod = ExchangeRunspaceConfigurationSettings.ProxyMethod.None;
			if (collection.Get("proxyMethod") != null)
			{
				Enum.TryParse<ExchangeRunspaceConfigurationSettings.ProxyMethod>(collection.Get("proxyMethod"), true, out proxyMethod);
			}
			bool flag = false;
			if (collection.Get("proxyFullSerialization") != null)
			{
				bool.TryParse(collection.Get("proxyFullSerialization"), out flag);
			}
			bool encodeDecodeKey = true;
			if (collection.Get("X-EncodeDecode-Key") != null)
			{
				bool.TryParse(collection.Get("X-EncodeDecode-Key"), out encodeDecodeKey);
			}
			bool isProxy = ExchangeRunspaceConfigurationSettings.IsCalledFromProxy(collection);
			return new ExchangeRunspaceConfigurationSettings(uri, exchangeApplication, text, serializationLevel, pslanguageMode, proxyMethod, flag, encodeDecodeKey, isProxy);
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004646C File Offset: 0x0004466C
		internal static string AddClientApplicationToUrl(string url, ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApp)
		{
			if (ExchangeRunspaceConfigurationSettings.regExForClientApp.IsMatch(url))
			{
				return ExchangeRunspaceConfigurationSettings.regExForClientApp.Replace(url, clientApp.ToString());
			}
			if (string.IsNullOrEmpty(new Uri(url).Query))
			{
				return string.Format("{0}?clientApplication={1}", url, clientApp.ToString());
			}
			return url += string.Format("{0}clientApplication={1}", url.EndsWith(";") ? string.Empty : ";", clientApp.ToString());
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00046500 File Offset: 0x00044700
		internal static bool IsCalledFromProxy(NameValueCollection headers)
		{
			string a = headers.Get(WellKnownHeader.CmdletProxyIsOn);
			return a == "99C6ECEE-5A4F-47B9-AE69-49EAFB58F368";
		}

		// Token: 0x04000544 RID: 1348
		internal const string URIPropertyOrganization = "organization";

		// Token: 0x04000545 RID: 1349
		internal const string URIPropertySerializationLevel = "serializationLevel";

		// Token: 0x04000546 RID: 1350
		internal const string URIPropertyClientApplication = "clientApplication";

		// Token: 0x04000547 RID: 1351
		internal const string URIPropertyProxyMethod = "proxyMethod";

		// Token: 0x04000548 RID: 1352
		internal const string URIPropertyProxyFullSerialization = "proxyFullSerialization";

		// Token: 0x04000549 RID: 1353
		internal const string CmdletProxyIsOnValue = "99C6ECEE-5A4F-47B9-AE69-49EAFB58F368";

		// Token: 0x0400054A RID: 1354
		internal const PSLanguageMode DefaultLanguageMode = PSLanguageMode.NoLanguage;

		// Token: 0x0400054B RID: 1355
		private static ExchangeRunspaceConfigurationSettings defaultInstance = new ExchangeRunspaceConfigurationSettings();

		// Token: 0x0400054C RID: 1356
		private ExchangeRunspaceConfigurationSettings.ExchangeApplication clientApplication;

		// Token: 0x0400054D RID: 1357
		private string tenantOrganization;

		// Token: 0x0400054E RID: 1358
		private ExchangeRunspaceConfigurationSettings.SerializationLevel serializationLevel;

		// Token: 0x0400054F RID: 1359
		private PSLanguageMode languageMode;

		// Token: 0x04000550 RID: 1360
		private ExchangeRunspaceConfigurationSettings.ProxyMethod proxyMethod;

		// Token: 0x04000551 RID: 1361
		private bool proxyFullSerialization;

		// Token: 0x04000552 RID: 1362
		private string siteRedirectionTemplate;

		// Token: 0x04000553 RID: 1363
		private Uri originalConnectionUri;

		// Token: 0x04000554 RID: 1364
		private readonly IEnumerable<KeyValuePair<string, string>> additionalConstraints;

		// Token: 0x04000555 RID: 1365
		private static readonly Regex regExForClientApp = new Regex("(?<=clientApplication=)\\w+", RegexOptions.IgnoreCase);

		// Token: 0x02000230 RID: 560
		public enum ExchangeApplication
		{
			// Token: 0x0400055D RID: 1373
			Unknown,
			// Token: 0x0400055E RID: 1374
			PowerShell,
			// Token: 0x0400055F RID: 1375
			EMC,
			// Token: 0x04000560 RID: 1376
			ECP,
			// Token: 0x04000561 RID: 1377
			EWS,
			// Token: 0x04000562 RID: 1378
			ManagementShell,
			// Token: 0x04000563 RID: 1379
			DebugUser,
			// Token: 0x04000564 RID: 1380
			CSVParser,
			// Token: 0x04000565 RID: 1381
			GalSync,
			// Token: 0x04000566 RID: 1382
			MigrationService,
			// Token: 0x04000567 RID: 1383
			SimpleDataMigration,
			// Token: 0x04000568 RID: 1384
			ForwardSync,
			// Token: 0x04000569 RID: 1385
			BackSync,
			// Token: 0x0400056A RID: 1386
			TipTestCase,
			// Token: 0x0400056B RID: 1387
			NonInteractivePowershell,
			// Token: 0x0400056C RID: 1388
			LowPriorityScripts,
			// Token: 0x0400056D RID: 1389
			DiscretionaryScripts,
			// Token: 0x0400056E RID: 1390
			ReportingWebService,
			// Token: 0x0400056F RID: 1391
			PswsClient,
			// Token: 0x04000570 RID: 1392
			Office365Partner,
			// Token: 0x04000571 RID: 1393
			Intune,
			// Token: 0x04000572 RID: 1394
			CRM,
			// Token: 0x04000573 RID: 1395
			ActiveMonitor,
			// Token: 0x04000574 RID: 1396
			TenantUser,
			// Token: 0x04000575 RID: 1397
			SyndicationCentral,
			// Token: 0x04000576 RID: 1398
			LiveEDU,
			// Token: 0x04000577 RID: 1399
			OSP
		}

		// Token: 0x02000231 RID: 561
		public enum SerializationLevel
		{
			// Token: 0x04000579 RID: 1401
			Partial,
			// Token: 0x0400057A RID: 1402
			None,
			// Token: 0x0400057B RID: 1403
			Full
		}

		// Token: 0x02000232 RID: 562
		public enum ProxyMethod
		{
			// Token: 0x0400057D RID: 1405
			None,
			// Token: 0x0400057E RID: 1406
			RPS,
			// Token: 0x0400057F RID: 1407
			PSWS
		}

		// Token: 0x02000233 RID: 563
		public enum ExchangeUserType
		{
			// Token: 0x04000581 RID: 1409
			Unknown,
			// Token: 0x04000582 RID: 1410
			Monitoring
		}
	}
}
