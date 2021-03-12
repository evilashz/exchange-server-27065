using System;
using System.DirectoryServices;
using System.IO;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.HttpProxy;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200004D RID: 77
	public abstract class VdirConfiguration
	{
		// Token: 0x06000247 RID: 583 RVA: 0x0000D40E File Offset: 0x0000B60E
		internal VdirConfiguration(ExchangeWebAppVirtualDirectory virtualDirectory)
		{
			this.internalAuthenticationMethod = VdirConfiguration.ConvertAuthenticationMethods(virtualDirectory.InternalAuthenticationMethods);
			this.externalAuthenticationMethod = VdirConfiguration.ConvertAuthenticationMethods(virtualDirectory.ExternalAuthenticationMethods);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000D438 File Offset: 0x0000B638
		public static VdirConfiguration Instance
		{
			get
			{
				if (VdirConfiguration.instance == null)
				{
					lock (VdirConfiguration.syncRoot)
					{
						if (VdirConfiguration.instance == null)
						{
							VdirConfiguration.instance = VdirConfiguration.BaseCreateInstance();
						}
					}
				}
				return VdirConfiguration.instance;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000D498 File Offset: 0x0000B698
		internal AuthenticationMethod InternalAuthenticationMethod
		{
			get
			{
				return this.internalAuthenticationMethod;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		internal AuthenticationMethod ExternalAuthenticationMethod
		{
			get
			{
				return this.externalAuthenticationMethod;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		private static AuthenticationMethod ConvertAuthenticationMethods(MultiValuedProperty<AuthenticationMethod> configMethods)
		{
			AuthenticationMethod authenticationMethod = AuthenticationMethod.None;
			using (MultiValuedProperty<AuthenticationMethod>.Enumerator enumerator = configMethods.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
					{
					case AuthenticationMethod.Basic:
						authenticationMethod |= AuthenticationMethod.Basic;
						break;
					case AuthenticationMethod.Digest:
						authenticationMethod |= AuthenticationMethod.Digest;
						break;
					case AuthenticationMethod.Ntlm:
						authenticationMethod |= AuthenticationMethod.Ntlm;
						break;
					case AuthenticationMethod.Fba:
						authenticationMethod |= AuthenticationMethod.Fba;
						break;
					case AuthenticationMethod.WindowsIntegrated:
						authenticationMethod |= AuthenticationMethod.WindowsIntegrated;
						break;
					case AuthenticationMethod.LiveIdFba:
						authenticationMethod |= AuthenticationMethod.LiveIdFba;
						break;
					case AuthenticationMethod.LiveIdBasic:
						authenticationMethod |= AuthenticationMethod.LiveIdBasic;
						break;
					case AuthenticationMethod.WSSecurity:
						authenticationMethod |= AuthenticationMethod.WSSecurity;
						break;
					case AuthenticationMethod.Certificate:
						authenticationMethod |= AuthenticationMethod.Certificate;
						break;
					case AuthenticationMethod.NegoEx:
						authenticationMethod |= AuthenticationMethod.NegoEx;
						break;
					case AuthenticationMethod.OAuth:
						authenticationMethod |= AuthenticationMethod.OAuth;
						break;
					case AuthenticationMethod.Adfs:
						authenticationMethod |= AuthenticationMethod.Adfs;
						break;
					case AuthenticationMethod.Kerberos:
						authenticationMethod |= AuthenticationMethod.Kerberos;
						break;
					case AuthenticationMethod.Negotiate:
						authenticationMethod |= AuthenticationMethod.Negotiate;
						break;
					case AuthenticationMethod.LiveIdNegotiate:
						authenticationMethod |= AuthenticationMethod.LiveIdNegotiate;
						break;
					}
				}
			}
			return authenticationMethod;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D5BC File Offset: 0x0000B7BC
		private static VdirConfiguration BaseCreateInstance()
		{
			ITopologyConfigurationSession session = VdirConfiguration.CreateADSystemConfigurationSessionScopedToFirstOrg();
			ExchangeVirtualDirectory member = HttpProxyGlobals.VdirObject.Member;
			if (member is ADEcpVirtualDirectory)
			{
				return EcpVdirConfiguration.CreateInstance(session, member.Id);
			}
			if (member is ADOwaVirtualDirectory)
			{
				return OwaVdirConfiguration.CreateInstance(session, member.Id);
			}
			throw new ADNoSuchObjectException(new LocalizedString(string.Format("NoVdirConfiguration. AppDomainAppId:{0},VDirDN:{1}", HttpRuntime.AppDomainAppId, (member == null) ? "NULL" : member.DistinguishedName)));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D630 File Offset: 0x0000B830
		private static ITopologyConfigurationSession CreateADSystemConfigurationSessionScopedToFirstOrg()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 207, "CreateADSystemConfigurationSessionScopedToFirstOrg", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\fba\\VdirConfiguration.cs");
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000D660 File Offset: 0x0000B860
		private static string GetWebSiteName(string webSiteRootPath)
		{
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry(webSiteRootPath))
				{
					using (DirectoryEntry parent = directoryEntry.Parent)
					{
						if (parent != null)
						{
							return ((string)parent.Properties["ServerComment"].Value) ?? string.Empty;
						}
					}
				}
			}
			catch (DirectoryServicesCOMException)
			{
			}
			catch (DirectoryNotFoundException)
			{
			}
			return string.Empty;
		}

		// Token: 0x04000135 RID: 309
		private static volatile VdirConfiguration instance;

		// Token: 0x04000136 RID: 310
		private static object syncRoot = new object();

		// Token: 0x04000137 RID: 311
		private readonly AuthenticationMethod internalAuthenticationMethod;

		// Token: 0x04000138 RID: 312
		private readonly AuthenticationMethod externalAuthenticationMethod;
	}
}
