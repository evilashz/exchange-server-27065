using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync.CookieManager
{
	// Token: 0x020007DC RID: 2012
	internal class MsoMultiObjectCookieManager : DeltaSyncCookieManager
	{
		// Token: 0x060063BB RID: 25531 RVA: 0x00159E98 File Offset: 0x00158098
		internal MsoMultiObjectCookieManager(string serviceInstanceName, int maxCookieHistoryCount, TimeSpan cookieHistoryInterval, ForwardSyncCookieType cookieType) : base(serviceInstanceName, maxCookieHistoryCount, cookieHistoryInterval)
		{
			this.cookieSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 71, ".ctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\Sync\\CookieManager\\MsoMultiObjectCookieManager.cs");
			this.cookieSession.UseConfigNC = false;
			this.cookieType = cookieType;
			this.cookieRootContainer = MsoMultiObjectCookieManager.CreateOrGetRootContainer(serviceInstanceName, this.cookieSession);
			ForwardSyncCookieHeader[] source = MsoMultiObjectCookieManager.LoadCookieHeaders(this.cookieSession, serviceInstanceName, this.cookieType);
			this.cookieHeaderList = source.ToList<ForwardSyncCookieHeader>();
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x00159F2C File Offset: 0x0015812C
		public static ForwardSyncCookieHeader[] LoadCookieHeaders(ITopologyConfigurationSession cookieSession, string serviceInstanceName, ForwardSyncCookieType cookieType)
		{
			ADObjectId serviceInstanceObjectId = SyncServiceInstance.GetServiceInstanceObjectId(serviceInstanceName);
			Container container = cookieSession.Read<Container>(serviceInstanceObjectId.GetChildId("Cookies"));
			if (container != null)
			{
				ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ForwardSyncCookieHeaderSchema.Type, cookieType);
				ForwardSyncCookieHeader[] array = cookieSession.Find<ForwardSyncCookieHeader>(container.Id, QueryScope.OneLevel, filter, null, 0);
				Array.Sort<ForwardSyncCookieHeader>(array, (ForwardSyncCookieHeader x, ForwardSyncCookieHeader y) => DateTime.Compare(x.Timestamp, y.Timestamp));
				return array;
			}
			return new ForwardSyncCookieHeader[0];
		}

		// Token: 0x060063BD RID: 25533 RVA: 0x00159FA4 File Offset: 0x001581A4
		private static ADObjectId CreateOrGetRootContainer(string serviceInstanceName, ITopologyConfigurationSession cookieSession)
		{
			ADObjectId serviceInstanceObjectId = SyncServiceInstance.GetServiceInstanceObjectId(serviceInstanceName);
			Container container = cookieSession.Read<Container>(serviceInstanceObjectId.GetChildId("Cookies"));
			if (container == null)
			{
				container = new Container();
				container.SetId(serviceInstanceObjectId.GetChildId("Cookies"));
				try
				{
					cookieSession.Save(container);
				}
				catch (ADObjectAlreadyExistsException)
				{
				}
			}
			return container.Id;
		}

		// Token: 0x060063BE RID: 25534 RVA: 0x0015A008 File Offset: 0x00158208
		public ForwardSyncCookie ReadMostRecentCookie()
		{
			if (this.mostRecentCookie == null && this.cookieHeaderList.Count > 0)
			{
				ForwardSyncCookieHeader forwardSyncCookieHeader = this.cookieHeaderList.Last<ForwardSyncCookieHeader>();
				this.mostRecentCookie = this.cookieSession.Read<ForwardSyncCookie>(forwardSyncCookieHeader.Id);
				this.lastNewCookieTimestamp = this.mostRecentCookie.Timestamp;
				base.SyncPropertySetVersion = new ServerVersion(this.mostRecentCookie.SyncPropertySetVersion);
				base.IsSyncPropertySetUpgrading = this.mostRecentCookie.IsUpgradingSyncPropertySet;
			}
			return this.mostRecentCookie;
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0015A08C File Offset: 0x0015828C
		public override byte[] ReadCookie()
		{
			ForwardSyncCookie forwardSyncCookie = this.ReadMostRecentCookie();
			if (forwardSyncCookie == null)
			{
				return null;
			}
			return forwardSyncCookie.Data;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0015A0AC File Offset: 0x001582AC
		public override DateTime? GetMostRecentCookieTimestamp()
		{
			ForwardSyncCookie forwardSyncCookie = this.ReadMostRecentCookie();
			if (forwardSyncCookie != null)
			{
				return new DateTime?(forwardSyncCookie.Timestamp);
			}
			return null;
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0015A0D8 File Offset: 0x001582D8
		public override void WriteCookie(byte[] cookie, IEnumerable<string> filteredContextIds, DateTime timestamp, bool isUpgradingCookie, ServerVersion version, bool more)
		{
			if (cookie == null || cookie.Length == 0)
			{
				throw new ArgumentNullException("cookie");
			}
			base.UpdateSyncPropertySetVersion(isUpgradingCookie, version, more);
			ForwardSyncCookie forwardSyncCookie = this.ReadMostRecentCookie();
			bool flag = forwardSyncCookie == null || timestamp.Subtract(this.lastNewCookieTimestamp) >= this.cookieHistoryInterval;
			bool flag2 = false;
			if (flag)
			{
				if (this.cookieHeaderList.Count > this.maxCookieHistoryCount)
				{
					this.CleanupOldCookies();
				}
				else
				{
					flag2 = (this.cookieHeaderList.Count < this.maxCookieHistoryCount);
				}
				forwardSyncCookie = new ForwardSyncCookie();
				if (flag2)
				{
					string unescapedCommonName = string.Format("{0}-{1}", Enum.GetName(typeof(ForwardSyncCookieType), this.cookieType), timestamp.Ticks);
					forwardSyncCookie.SetId(this.cookieRootContainer.GetChildId(unescapedCommonName));
				}
				else
				{
					forwardSyncCookie.SetId(this.cookieHeaderList.First<ForwardSyncCookieHeader>().Id);
					forwardSyncCookie.Name = forwardSyncCookie.Id.Name;
					forwardSyncCookie.m_Session = this.cookieSession;
					forwardSyncCookie.ResetChangeTracking(true);
				}
				forwardSyncCookie.Type = this.cookieType;
				forwardSyncCookie.Version = 1;
			}
			forwardSyncCookie.Timestamp = timestamp;
			forwardSyncCookie.Data = cookie;
			if (filteredContextIds != null)
			{
				forwardSyncCookie.FilteredContextIds = new MultiValuedProperty<string>(filteredContextIds);
			}
			forwardSyncCookie.IsUpgradingSyncPropertySet = base.IsSyncPropertySetUpgrading;
			forwardSyncCookie.SyncPropertySetVersion = base.SyncPropertySetVersion.ToInt();
			this.cookieSession.Save(forwardSyncCookie);
			this.mostRecentCookie = forwardSyncCookie;
			if (flag)
			{
				ForwardSyncCookieHeader forwardSyncCookieHeader = new ForwardSyncCookieHeader
				{
					Name = forwardSyncCookie.Name,
					Timestamp = forwardSyncCookie.Timestamp
				};
				forwardSyncCookieHeader.SetId(forwardSyncCookie.Id);
				this.cookieHeaderList.Add(forwardSyncCookieHeader);
				this.lastNewCookieTimestamp = timestamp;
				if (!flag2)
				{
					this.cookieHeaderList.RemoveAt(0);
				}
			}
		}

		// Token: 0x17002363 RID: 9059
		// (get) Token: 0x060063C2 RID: 25538 RVA: 0x0015A2A1 File Offset: 0x001584A1
		public override string DomainController
		{
			get
			{
				if (this.cookieSession != null)
				{
					return this.cookieSession.DomainController;
				}
				return string.Empty;
			}
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0015A2BC File Offset: 0x001584BC
		private void CleanupOldCookies()
		{
			while (this.cookieHeaderList.Count > this.maxCookieHistoryCount)
			{
				try
				{
					this.cookieSession.Delete(this.cookieHeaderList.First<ForwardSyncCookieHeader>());
					this.cookieHeaderList.RemoveAt(0);
				}
				catch (Exception ex)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_FailedToCleanupCookies, base.ServiceInstanceName, new object[]
					{
						ex
					});
					break;
				}
			}
		}

		// Token: 0x04004265 RID: 16997
		private const int CookieVersion = 1;

		// Token: 0x04004266 RID: 16998
		public const string CookieContainerName = "Cookies";

		// Token: 0x04004267 RID: 16999
		private readonly ITopologyConfigurationSession cookieSession;

		// Token: 0x04004268 RID: 17000
		private readonly List<ForwardSyncCookieHeader> cookieHeaderList;

		// Token: 0x04004269 RID: 17001
		private readonly ADObjectId cookieRootContainer;

		// Token: 0x0400426A RID: 17002
		private readonly ForwardSyncCookieType cookieType;

		// Token: 0x0400426B RID: 17003
		private ForwardSyncCookie mostRecentCookie;

		// Token: 0x0400426C RID: 17004
		private DateTime lastNewCookieTimestamp;
	}
}
