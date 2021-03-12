using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200007D RID: 125
	internal abstract class DirectorySessionFactory
	{
		// Token: 0x1700013A RID: 314
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00020CE3 File Offset: 0x0001EEE3
		public static DirectorySessionFactory MockDefault
		{
			set
			{
				DirectorySessionFactory.mockDefault = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00020CEB File Offset: 0x0001EEEB
		public static DirectorySessionFactory Default
		{
			get
			{
				if (DirectorySessionFactory.mockDefault != null)
				{
					return DirectorySessionFactory.mockDefault;
				}
				if (!Configuration.IsCacheEnableForCurrentProcess())
				{
					return DirectorySessionFactory.NonCacheSessionFactory;
				}
				return DirectorySessionFactory.CacheFallbackSessionFactory;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00020D0C File Offset: 0x0001EF0C
		internal static DirectorySessionFactory NonCacheSessionFactory
		{
			get
			{
				if (DirectorySessionFactory.nonCacheSessionFactory == null)
				{
					DirectorySessionFactory.nonCacheSessionFactory = (DirectorySessionFactory)DirectorySessionFactory.InstantiateFfoOrExoClass("Microsoft.Exchange.Hygiene.Data.Directory.FfoDirectorySesssionFactory", typeof(ADSessionFactory));
				}
				return DirectorySessionFactory.nonCacheSessionFactory;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00020D38 File Offset: 0x0001EF38
		internal static DirectorySessionFactory CacheSessionFactory
		{
			get
			{
				if (DirectorySessionFactory.cacheSessionFactory == null)
				{
					DirectorySessionFactory.cacheSessionFactory = (DirectorySessionFactory)DirectorySessionFactory.InstantiateFfoOrExoClass("Microsoft.Exchange.Hygiene.Data.Directory.FfoDirectorySesssionFactory", typeof(CacheDirectorySessionFactory));
				}
				return DirectorySessionFactory.cacheSessionFactory;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00020D64 File Offset: 0x0001EF64
		protected static DirectorySessionFactory CacheFallbackSessionFactory
		{
			get
			{
				if (DirectorySessionFactory.cacheFallbackSessionFactory == null)
				{
					DirectorySessionFactory.cacheFallbackSessionFactory = (DirectorySessionFactory)DirectorySessionFactory.InstantiateFfoOrExoClass("Microsoft.Exchange.Hygiene.Data.Directory.FfoDirectorySesssionFactory", typeof(CompositeDirectorySessionFactory));
				}
				return DirectorySessionFactory.cacheFallbackSessionFactory;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00020D90 File Offset: 0x0001EF90
		internal static DirectorySessionFactory GetInstance(DirectorySessionFactoryType directorySessionFactoryType)
		{
			if (directorySessionFactoryType == DirectorySessionFactoryType.Cached)
			{
				return DirectorySessionFactory.CacheFallbackSessionFactory;
			}
			return DirectorySessionFactory.Default;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00020DA1 File Offset: 0x0001EFA1
		public static IGlobalDirectorySession GetGlobalSession(string redirectFormatForMServ = null)
		{
			return new GlsMServDirectorySession(redirectFormatForMServ);
		}

		// Token: 0x060005F6 RID: 1526
		public abstract ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005F7 RID: 1527
		public abstract ITenantConfigurationSession CreateTenantConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005F8 RID: 1528 RVA: 0x00020DAC File Offset: 0x0001EFAC
		public ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x060005F9 RID: 1529
		public abstract ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005FA RID: 1530
		public abstract ITenantConfigurationSession CreateTenantConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005FB RID: 1531
		public abstract ITenantConfigurationSession CreateTenantConfigurationSession(ConsistencyMode consistencyMode, Guid externalDirectoryOrganizationId, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005FC RID: 1532
		public abstract ITopologyConfigurationSession CreateTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005FD RID: 1533
		public abstract ITopologyConfigurationSession CreateTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x060005FE RID: 1534 RVA: 0x00020DCC File Offset: 0x0001EFCC
		public ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.CreateTopologyConfigurationSession(domainController, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x060005FF RID: 1535
		public abstract ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000600 RID: 1536
		public abstract ITopologyConfigurationSession CreateTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000601 RID: 1537
		public abstract ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000602 RID: 1538
		public abstract ITenantRecipientSession CreateTenantRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScopes, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000603 RID: 1539 RVA: 0x00020DEC File Offset: 0x0001EFEC
		public ITenantRecipientSession CreateTenantRecipientSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.CreateTenantRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000604 RID: 1540
		public abstract IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScopes, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000605 RID: 1541
		public abstract IRootOrganizationRecipientSession CreateRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000606 RID: 1542 RVA: 0x00020E15 File Offset: 0x0001F015
		public IRootOrganizationRecipientSession CreateRootOrgRecipientSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.CreateRootOrgRecipientSession(true, consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00020E28 File Offset: 0x0001F028
		public IRootOrganizationRecipientSession CreateRootOrgRecipientSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.CreateRootOrgRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00020E51 File Offset: 0x0001F051
		public IConfigurationSession GetTenantOrTopologyConfigurationSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrTopologyConfigurationSession(true, consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00020E64 File Offset: 0x0001F064
		public IConfigurationSession GetTenantOrTopologyConfigurationSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrTopologyConfigurationSession(null, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00020E84 File Offset: 0x0001F084
		public IConfigurationSession GetTenantOrTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrTopologyConfigurationSession(domainController, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		public IConfigurationSession GetTenantOrTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			if (sessionSettings.CurrentOrganizationId == OrganizationId.ForestWideOrgId && sessionSettings.ConfigScopes != ConfigScopes.AllTenants)
			{
				return this.CreateTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
			}
			return this.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00020EF8 File Offset: 0x0001F0F8
		public IConfigurationSession GetTenantOrTopologyConfigurationSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			if (sessionSettings.CurrentOrganizationId == OrganizationId.ForestWideOrgId && sessionSettings.ConfigScopes != ConfigScopes.AllTenants)
			{
				return this.CreateTopologyConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath);
			}
			return this.CreateTenantConfigurationSession(domainController, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00020F4D File Offset: 0x0001F14D
		public IRecipientSession GetTenantOrRootOrgRecipientSession(ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrRootOrgRecipientSession(true, consistencyMode, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00020F60 File Offset: 0x0001F160
		public IRecipientSession GetTenantOrRootOrgRecipientSession(bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrRootOrgRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00020F8C File Offset: 0x0001F18C
		public IRecipientSession GetTenantOrRootOrgRecipientSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrRootOrgRecipientSession(domainController, null, CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, null, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00020FB8 File Offset: 0x0001F1B8
		public IRecipientSession GetTenantOrRootOrgRecipientSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrRootOrgRecipientSession(domainController, null, CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		public IRecipientSession GetTenantOrRootOrgRecipientSession(string domainController, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			return this.GetTenantOrRootOrgRecipientSession(domainController, null, CultureInfo.CurrentCulture.LCID, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00021014 File Offset: 0x0001F214
		public IRecipientSession GetTenantOrRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			if (sessionSettings.CurrentOrganizationId == OrganizationId.ForestWideOrgId && sessionSettings.ConfigScopes != ConfigScopes.AllTenants)
			{
				return this.CreateRootOrgRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
			}
			return this.CreateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00021070 File Offset: 0x0001F270
		public IRecipientSession GetTenantOrRootOrgRecipientSession(string domainController, ADObjectId searchRoot, int lcid, bool readOnly, ConsistencyMode consistencyMode, NetworkCredential networkCredential, ADSessionSettings sessionSettings, ConfigScopes configScope, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			if (sessionSettings.CurrentOrganizationId == OrganizationId.ForestWideOrgId && sessionSettings.ConfigScopes != ConfigScopes.AllTenants)
			{
				return this.CreateRootOrgRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath);
			}
			return this.CreateTenantRecipientSession(domainController, searchRoot, lcid, readOnly, consistencyMode, networkCredential, sessionSettings, configScope, callerFileLine, memberName, callerFilePath);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000210D0 File Offset: 0x0001F2D0
		public IRecipientSession GetTenantOrRootRecipientReadOnlySession(IRecipientSession recipientSession, string domainController = null, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = this.GetTenantOrRootOrgRecipientSession(null, recipientSession.SearchRoot, recipientSession.Lcid, true, recipientSession.ConsistencyMode, recipientSession.NetworkCredential, recipientSession.SessionSettings, callerFileLine, memberName, callerFilePath);
			tenantOrRootOrgRecipientSession.DomainController = domainController;
			return tenantOrRootOrgRecipientSession;
		}

		// Token: 0x06000615 RID: 1557
		public abstract IRecipientSession GetReducedRecipientSession(IRecipientSession baseSession, [CallerLineNumber] int callerFileLine = 0, [CallerMemberName] string memberName = null, [CallerFilePath] string callerFilePath = null);

		// Token: 0x06000616 RID: 1558 RVA: 0x00021114 File Offset: 0x0001F314
		internal static object InstantiateFfoOrExoClass(string ffoTypeName, Type exoType)
		{
			if (DatacenterRegistry.IsForefrontForOffice() && !DatacenterRegistry.IsForefrontForOfficeDeployment())
			{
				Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.Data");
				Type type = assembly.GetType(ffoTypeName);
				return Activator.CreateInstance(type);
			}
			return Activator.CreateInstance(exoType);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0002114F File Offset: 0x0001F34F
		[Conditional("DEBUG")]
		private static void Dbg_CheckCallStackForNonDefaultSession()
		{
		}

		// Token: 0x0400026D RID: 621
		private static DirectorySessionFactory nonCacheSessionFactory;

		// Token: 0x0400026E RID: 622
		private static DirectorySessionFactory cacheSessionFactory;

		// Token: 0x0400026F RID: 623
		private static DirectorySessionFactory cacheFallbackSessionFactory;

		// Token: 0x04000270 RID: 624
		private static DirectorySessionFactory mockDefault;
	}
}
