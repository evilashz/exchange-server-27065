using System;
using System.Globalization;
using System.Reflection;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000036 RID: 54
	internal static class HttpProxyGlobals
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000099B0 File Offset: 0x00007BB0
		public static bool IsMultitenant
		{
			get
			{
				return VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000099D4 File Offset: 0x00007BD4
		public static ProtocolType ProtocolType
		{
			get
			{
				return HttpProxyGlobals.ProtocolType;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x000099DB File Offset: 0x00007BDB
		public static bool OnlyProxySecureConnections
		{
			get
			{
				return HttpProxyGlobals.OnlyProxySecureConnections;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000099E2 File Offset: 0x00007BE2
		public static string ApplicationVersion
		{
			get
			{
				return HttpProxyGlobals.ApplicationVersionInternal;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000099EC File Offset: 0x00007BEC
		private static string GetSlimApplicationVersion()
		{
			string result = string.Empty;
			object[] customAttributes = typeof(HttpProxyGlobals).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
			if (customAttributes != null && customAttributes.Length > 0)
			{
				AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)customAttributes[0];
				if (assemblyFileVersionAttribute != null)
				{
					result = Constants.NoLeadingZeroRegex.Replace(Constants.NoRevisionNumberRegex.Replace(assemblyFileVersionAttribute.Version, "$1"), "$1");
				}
			}
			return result;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009A5C File Offset: 0x00007C5C
		private static ExchangeVirtualDirectory LoadVirtualDirectoryFromAD()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 176, "LoadVirtualDirectoryFromAD", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\Configuration\\HttpProxyGlobals.cs");
			Server server = topologyConfigurationSession.FindLocalServer();
			if (server == null || server.Id == null)
			{
				ExTraceGlobals.BriefTracer.TraceError<ProtocolType>(0L, "Could not find Server object in AD", HttpProxyGlobals.ProtocolType);
				return null;
			}
			string text = HttpRuntime.AppDomainAppId.Substring(3);
			if (text.EndsWith("owa/integrated", StringComparison.OrdinalIgnoreCase))
			{
				text = text.Remove(text.LastIndexOf('/'));
			}
			text = string.Format(CultureInfo.InvariantCulture, "IIS://{0}{1}", new object[]
			{
				server.Fqdn,
				text
			});
			ADObjectId descendantId = server.Id.GetDescendantId("Protocols", "HTTP", new string[0]);
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ExchangeVirtualDirectorySchema.MetabasePath, text);
			ExchangeVirtualDirectory[] array;
			switch (HttpProxyGlobals.ProtocolType)
			{
			case ProtocolType.Eas:
				array = topologyConfigurationSession.Find<ADMobileVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Ecp:
				array = topologyConfigurationSession.Find<ADEcpVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Ews:
				array = topologyConfigurationSession.Find<ADWebServicesVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Oab:
				array = topologyConfigurationSession.Find<ADOabVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Owa:
			case ProtocolType.OwaCalendar:
				array = topologyConfigurationSession.Find<ADOwaVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.PowerShell:
			case ProtocolType.PowerShellLiveId:
			case ProtocolType.PowerShellGateway:
				array = topologyConfigurationSession.Find<ADPowerShellVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.RpcHttp:
				array = topologyConfigurationSession.Find<ADRpcHttpVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Autodiscover:
				array = topologyConfigurationSession.Find<ADAutodiscoverVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Psws:
				array = topologyConfigurationSession.Find<ADPswsVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.PushNotifications:
				array = topologyConfigurationSession.Find<ADPushNotificationsVirtualDirectory>(descendantId, QueryScope.OneLevel, filter, null, 2);
				goto IL_20C;
			case ProtocolType.Mapi:
				array = topologyConfigurationSession.Find<ADMapiVirtualDirectory>(descendantId, QueryScope.SubTree, filter, null, 2);
				goto IL_20C;
			case ProtocolType.SnackyService:
				array = topologyConfigurationSession.Find<ADSnackyServiceVirtualDirectory>(descendantId, QueryScope.OneLevel, filter, null, 2);
				goto IL_20C;
			case ProtocolType.O365SuiteService:
				array = topologyConfigurationSession.Find<ADO365SuiteServiceVirtualDirectory>(descendantId, QueryScope.OneLevel, filter, null, 2);
				goto IL_20C;
			}
			array = null;
			IL_20C:
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.BriefTracer.TraceError<ProtocolType>(0L, "Could not find AD virtual directory entry for protocol [0]", HttpProxyGlobals.ProtocolType);
				return null;
			}
			ExchangeVirtualDirectory result;
			if (array.Length == 1)
			{
				result = array[0];
			}
			else
			{
				ExTraceGlobals.BriefTracer.TraceError<ProtocolType>(0L, "Found more than one AD virtual directory entry for protocol [0]", HttpProxyGlobals.ProtocolType);
				result = array[0];
			}
			return result;
		}

		// Token: 0x040000CB RID: 203
		public static readonly LazyMember<ExchangeVirtualDirectory> VdirObject = new LazyMember<ExchangeVirtualDirectory>(() => HttpProxyGlobals.LoadVirtualDirectoryFromAD());

		// Token: 0x040000CC RID: 204
		public static readonly LazyMember<string> LocalMachineFqdn = new LazyMember<string>(() => HttpProxyGlobals.LocalMachineFqdn.Member);

		// Token: 0x040000CD RID: 205
		public static readonly LazyMember<string> LocalMachineForest = new LazyMember<string>(() => HttpProxyGlobals.LocalMachineForest.Member);

		// Token: 0x040000CE RID: 206
		public static readonly LazyMember<string> LocalMachineRegion = new LazyMember<string>(() => HttpProxyGlobals.LocalMachineForest.Member.Substring(0, 3).ToUpper());

		// Token: 0x040000CF RID: 207
		public static readonly LazyMember<Site> LocalSite = new LazyMember<Site>(() => new Site(new TopologySite(LocalSiteCache.LocalSite)));

		// Token: 0x040000D0 RID: 208
		public static readonly LazyMember<string> VirtualDirectoryName = new LazyMember<string>(delegate()
		{
			string text = HttpRuntime.AppDomainAppVirtualPath;
			if (text[0] == '/')
			{
				text = text.Substring(1);
			}
			return text;
		});

		// Token: 0x040000D1 RID: 209
		private static readonly string ApplicationVersionInternal = HttpProxyGlobals.GetSlimApplicationVersion();
	}
}
