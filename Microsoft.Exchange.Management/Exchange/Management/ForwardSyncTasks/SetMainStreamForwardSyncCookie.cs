using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200037A RID: 890
	[Cmdlet("Set", "MainStreamForwardSyncCookie")]
	public sealed class SetMainStreamForwardSyncCookie : Task
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x00086942 File Offset: 0x00084B42
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x0008694A File Offset: 0x00084B4A
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public ServiceInstanceId ServiceInstanceId { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001F2C RID: 7980 RVA: 0x00086953 File Offset: 0x00084B53
		// (set) Token: 0x06001F2D RID: 7981 RVA: 0x0008695B File Offset: 0x00084B5B
		[Parameter(Mandatory = true)]
		public int RollbackTimeIntervalMinutes { get; set; }

		// Token: 0x06001F2E RID: 7982 RVA: 0x00086964 File Offset: 0x00084B64
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || TaskHelper.IsTaskKnownException(exception) || exception is CannotGetDomainInfoException || exception is ServiceInstanceContainerNotFoundException || exception is DataValidationException;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00086994 File Offset: 0x00084B94
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.rollbackTimeSpan = new TimeSpan(0, this.RollbackTimeIntervalMinutes, 0);
			this.configSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 83, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\SetMainStreamForwardSyncCookie.cs");
			this.cookieSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 89, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\SetMainStreamForwardSyncCookie.cs");
			this.cookieSession.UseConfigNC = false;
			this.container = this.cookieSession.GetMsoMainStreamCookieContainer(this.ServiceInstanceId.InstanceId);
			if (this.container.IsMultiObjectCookieEnabled)
			{
				this.cookieObjects[ForwardSyncCookieType.RecipientIncremental] = MsoMultiObjectCookieManager.LoadCookieHeaders(this.cookieSession, this.ServiceInstanceId.InstanceId, ForwardSyncCookieType.RecipientIncremental);
				this.cookieObjects[ForwardSyncCookieType.CompanyIncremental] = MsoMultiObjectCookieManager.LoadCookieHeaders(this.cookieSession, this.ServiceInstanceId.InstanceId, ForwardSyncCookieType.CompanyIncremental);
				this.ValidateAllCookiebjectsAreNotRemoved(this.cookieObjects[ForwardSyncCookieType.RecipientIncremental]);
				this.ValidateAllCookiebjectsAreNotRemoved(this.cookieObjects[ForwardSyncCookieType.CompanyIncremental]);
			}
			else
			{
				this.ValidateAllCookiesAreNotRemoved(this.container.MsoForwardSyncRecipientCookie);
				this.ValidateAllCookiesAreNotRemoved(this.container.MsoForwardSyncNonRecipientCookie);
			}
			this.forwardSyncDataAccessHelper = new ForwardSyncDataAccessHelper(this.ServiceInstanceId.InstanceId);
			TaskLogger.LogExit();
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00086AE8 File Offset: 0x00084CE8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (this.container.IsMultiObjectCookieEnabled)
			{
				this.RollbackObjectCookies(ForwardSyncCookieType.RecipientIncremental);
				this.RollbackObjectCookies(ForwardSyncCookieType.CompanyIncremental);
			}
			else
			{
				this.RollbackCookies(this.container.MsoForwardSyncRecipientCookie, false);
				this.RollbackCookies(this.container.MsoForwardSyncNonRecipientCookie, true);
			}
			if (this.cookiesUpdated)
			{
				this.cookieSession.Save(this.container);
				int minorPartnerId = LocalSiteCache.LocalSite.MinorPartnerId;
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.NotEqual, ADSiteSchema.MinorPartnerId, minorPartnerId);
				ADSite[] array = this.configSession.Find<ADSite>(null, QueryScope.SubTree, filter, null, 0);
				if (array.Length > 0)
				{
					ADObjectId[] array2 = new ADObjectId[array.Length];
					int num = 0;
					foreach (ADSite adsite in array)
					{
						array2[num] = new ADObjectId(adsite.DistinguishedName);
						num++;
					}
					this.cookieSession.ReplicateSingleObject(this.container, array2);
				}
			}
			else
			{
				this.WriteWarning(Strings.WarningNoCookiesRemovedForRollback);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00086BF8 File Offset: 0x00084DF8
		private void RollbackObjectCookies(ForwardSyncCookieType type)
		{
			ForwardSyncCookieHeader[] array = this.cookieObjects[type];
			DateTime t = this.dateTimeNow.Subtract(this.rollbackTimeSpan);
			int num = array.Length - 1;
			while (num > 0 && array[num].Timestamp > t)
			{
				this.cookieSession.Delete(array[num--]);
			}
			this.cookiesUpdated = (num < array.Length - 1);
			if (num >= 0)
			{
				this.RollBackDivergences(type == ForwardSyncCookieType.CompanyIncremental, array[num].Timestamp);
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00086C7C File Offset: 0x00084E7C
		private void RollbackCookies(MultiValuedProperty<byte[]> cookies, bool isCompanyStream)
		{
			DateTime? dateTime = null;
			for (int i = cookies.Count - 1; i >= 0; i--)
			{
				MsoMainStreamCookie msoMainStreamCookie = null;
				Exception ex = null;
				if (MsoMainStreamCookie.TryFromStorageCookie(cookies[i], out msoMainStreamCookie, out ex) && string.Equals(this.ServiceInstanceId.ToString(), msoMainStreamCookie.ServiceInstanceName, StringComparison.OrdinalIgnoreCase))
				{
					if (this.dateTimeNow < new DateTime(msoMainStreamCookie.TimeStamp.Ticks + this.rollbackTimeSpan.Ticks, DateTimeKind.Utc))
					{
						cookies.RemoveAt(i);
						this.cookiesUpdated = true;
					}
					else if (dateTime == null || msoMainStreamCookie.TimeStamp > dateTime.Value)
					{
						dateTime = new DateTime?(msoMainStreamCookie.TimeStamp);
					}
				}
			}
			if (dateTime != null)
			{
				this.RollBackDivergences(isCompanyStream, dateTime.Value);
			}
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00086D5C File Offset: 0x00084F5C
		private void RollBackDivergences(bool isCompanyStream, DateTime latestCookieTimestamp)
		{
			ComparisonFilter comparisonFilter = new ComparisonFilter(isCompanyStream ? ComparisonOperator.Equal : ComparisonOperator.NotEqual, FailedMSOSyncObjectSchema.ExternalDirectoryObjectClass, DirectoryObjectClass.Company);
			ComparisonFilter comparisonFilter2 = new ComparisonFilter(ComparisonOperator.GreaterThan, FailedMSOSyncObjectSchema.DivergenceTimestamp, latestCookieTimestamp);
			ComparisonFilter comparisonFilter3 = new ComparisonFilter(ComparisonOperator.Equal, FailedMSOSyncObjectSchema.IsIncrementalOnly, true);
			IEnumerable<FailedMSOSyncObject> enumerable = this.forwardSyncDataAccessHelper.FindDivergence(new AndFilter(new QueryFilter[]
			{
				comparisonFilter,
				comparisonFilter2,
				comparisonFilter3
			}));
			foreach (FailedMSOSyncObject divergence in enumerable)
			{
				this.forwardSyncDataAccessHelper.DeleteDivergence(divergence);
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00086E18 File Offset: 0x00085018
		private void ValidateAllCookiebjectsAreNotRemoved(IEnumerable<ForwardSyncCookieHeader> cookies)
		{
			ForwardSyncCookieHeader forwardSyncCookieHeader = cookies.FirstOrDefault<ForwardSyncCookieHeader>();
			if (forwardSyncCookieHeader != null && this.dateTimeNow < new DateTime(forwardSyncCookieHeader.Timestamp.Ticks + this.rollbackTimeSpan.Ticks, DateTimeKind.Utc))
			{
				base.WriteError(new InvalidUserInputException(Strings.ErrorCannotRemoveAllCookies(this.rollbackTimeSpan.ToString())), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00086E84 File Offset: 0x00085084
		private void ValidateAllCookiesAreNotRemoved(MultiValuedProperty<byte[]> cookies)
		{
			MsoMainStreamCookie msoMainStreamCookie = null;
			foreach (byte[] storageCookie in cookies)
			{
				MsoMainStreamCookie msoMainStreamCookie2 = null;
				Exception ex = null;
				if (MsoMainStreamCookie.TryFromStorageCookie(storageCookie, out msoMainStreamCookie2, out ex) && string.Equals(this.ServiceInstanceId.ToString(), msoMainStreamCookie2.ServiceInstanceName, StringComparison.OrdinalIgnoreCase) && (msoMainStreamCookie == null || msoMainStreamCookie.TimeStamp > msoMainStreamCookie2.TimeStamp))
				{
					msoMainStreamCookie = msoMainStreamCookie2;
				}
			}
			if (msoMainStreamCookie != null && this.dateTimeNow < new DateTime(msoMainStreamCookie.TimeStamp.Ticks + this.rollbackTimeSpan.Ticks, DateTimeKind.Utc))
			{
				base.WriteError(new InvalidUserInputException(Strings.ErrorCannotRemoveAllCookies(this.rollbackTimeSpan.ToString())), (ErrorCategory)1000, null);
			}
		}

		// Token: 0x04001956 RID: 6486
		private readonly Dictionary<ForwardSyncCookieType, ForwardSyncCookieHeader[]> cookieObjects = new Dictionary<ForwardSyncCookieType, ForwardSyncCookieHeader[]>();

		// Token: 0x04001957 RID: 6487
		private IConfigurationSession configSession;

		// Token: 0x04001958 RID: 6488
		private ITopologyConfigurationSession cookieSession;

		// Token: 0x04001959 RID: 6489
		private MsoMainStreamCookieContainer container;

		// Token: 0x0400195A RID: 6490
		private bool cookiesUpdated;

		// Token: 0x0400195B RID: 6491
		private TimeSpan rollbackTimeSpan;

		// Token: 0x0400195C RID: 6492
		private readonly DateTime dateTimeNow = DateTime.UtcNow;

		// Token: 0x0400195D RID: 6493
		private ForwardSyncDataAccessHelper forwardSyncDataAccessHelper;
	}
}
