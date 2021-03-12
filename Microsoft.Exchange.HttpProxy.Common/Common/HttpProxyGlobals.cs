using System;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000014 RID: 20
	internal static class HttpProxyGlobals
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003B91 File Offset: 0x00001D91
		public static bool IsPartnerHostedOnly
		{
			get
			{
				return HttpProxyGlobals.isPartnerHostedOnly.Member;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003B9D File Offset: 0x00001D9D
		public static ProtocolType ProtocolType
		{
			get
			{
				return HttpProxyGlobals.LazyProtocolType.Member;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003BA9 File Offset: 0x00001DA9
		public static bool OnlyProxySecureConnections
		{
			get
			{
				return HttpProxySettings.OnlyProxySecureConnectionsAppSetting.Value;
			}
		}

		// Token: 0x04000079 RID: 121
		public static readonly LazyMember<string> LocalMachineFqdn = new LazyMember<string>(() => LocalServer.GetServer().Fqdn.ToUpper());

		// Token: 0x0400007A RID: 122
		public static readonly LazyMember<string> LocalMachineForest = new LazyMember<string>(() => LocalServer.GetServer().Domain.ToUpper());

		// Token: 0x0400007B RID: 123
		private static readonly LazyMember<ProtocolType> LazyProtocolType = new LazyMember<ProtocolType>(delegate()
		{
			if (HttpProxySettings.ProtocolTypeAppSetting.Value == ProtocolType.Owa && HttpRuntime.AppDomainAppVirtualPath.ToLower().Contains("calendar"))
			{
				return ProtocolType.OwaCalendar;
			}
			return HttpProxySettings.ProtocolTypeAppSetting.Value;
		});

		// Token: 0x0400007C RID: 124
		private static LazyMember<bool> isPartnerHostedOnly = new LazyMember<bool>(delegate()
		{
			try
			{
				if (Datacenter.IsPartnerHostedOnly(true))
				{
					return true;
				}
			}
			catch (CannotDetermineExchangeModeException)
			{
			}
			return false;
		});
	}
}
