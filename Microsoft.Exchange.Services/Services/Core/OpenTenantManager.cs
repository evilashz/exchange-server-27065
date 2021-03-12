using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200001B RID: 27
	internal sealed class OpenTenantManager : IDisposable
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00007890 File Offset: 0x00005A90
		internal OpenTenantManager()
		{
			try
			{
				this.activeOrganization = this.GetNextActiveOrganization();
			}
			catch (ADTransientException ex)
			{
				OpenTenantManager.trace.TraceWarning<string>((long)Thread.CurrentThread.ManagedThreadId, "Encountered a transient exception when trying to query AD for an open tenant on initialize.  Mesage: {0}", ex.Message);
			}
			catch (OpenTenantQueryException ex2)
			{
				OpenTenantManager.trace.TraceError<string>((long)Thread.CurrentThread.ManagedThreadId, "Encountered an unexpected error when trying to query AD for an open tenant on initialize.  Message: {0}", ex2.Message);
			}
			this.workerThread = new Thread(new ThreadStart(this.EvaluateActiveOrganizationWork))
			{
				IsBackground = true
			};
			this.workerThread.Start();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000191 RID: 401 RVA: 0x00007984 File Offset: 0x00005B84
		// (remove) Token: 0x06000192 RID: 402 RVA: 0x000079BC File Offset: 0x00005BBC
		internal event EventHandler EvaluationWaitingOnPulse = delegate(object sender, EventArgs args)
		{
		};

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000193 RID: 403 RVA: 0x000079F1 File Offset: 0x00005BF1
		public static OpenTenantManager Instance
		{
			get
			{
				return OpenTenantManager.instance.Value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007A00 File Offset: 0x00005C00
		public OrganizationId ActiveOrganizationId
		{
			get
			{
				if (this.ActiveOrganization == null)
				{
					lock (this.disposing)
					{
						if (this.activeOrganization == null)
						{
							this.activeOrganization = this.GetNextActiveOrganization();
						}
					}
				}
				return this.activeOrganization.OrganizationId;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007A68 File Offset: 0x00005C68
		internal ExchangeConfigurationUnit ActiveOrganization
		{
			get
			{
				lock (this.evaluationPulseObject)
				{
					Monitor.Pulse(this.evaluationPulseObject);
				}
				return this.activeOrganization;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007AB8 File Offset: 0x00005CB8
		internal static IntAppSettingsEntry MaximumUserPerTenantThreshold
		{
			get
			{
				return OpenTenantManager.maximumUserPerTenantThreshold;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00007AC0 File Offset: 0x00005CC0
		void IDisposable.Dispose()
		{
			lock (this.disposing)
			{
				this.disposing.Cancel();
			}
			lock (this.evaluationPulseObject)
			{
				Monitor.Pulse(this.evaluationPulseObject);
			}
			if (!this.workerThread.Join(TimeSpan.FromSeconds(10.0)))
			{
				this.workerThread.Abort();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007B60 File Offset: 0x00005D60
		private static void SaveFullOrganizationImplementation(ExchangeConfigurationUnit organization)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(organization.OrganizationId.PartitionId);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(false, ConsistencyMode.IgnoreInvalid, sessionSettings, 287, "SaveFullOrganizationImplementation", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Common\\OpenTenantManager.cs");
			organization = tenantConfigurationSession.Read<ExchangeConfigurationUnit>(organization.Id);
			if (!organization.OpenTenantFull)
			{
				organization.OpenTenantFull = true;
				tenantConfigurationSession.Save(organization);
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007BC0 File Offset: 0x00005DC0
		private static ExchangeConfigurationUnit ReloadOrganizationImplementation(ExchangeConfigurationUnit organization)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(organization.OrganizationId.PartitionId);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.NonCacheSessionFactory.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 304, "ReloadOrganizationImplementation", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Common\\OpenTenantManager.cs");
			return tenantConfigurationSession.Read<ExchangeConfigurationUnit>(organization.Id);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007C08 File Offset: 0x00005E08
		private bool ValidateOrganizationHasVacancy()
		{
			if (this.activeOrganization.OpenTenantFull)
			{
				return false;
			}
			try
			{
				ExchangeConfigurationUnit exchangeConfigurationUnit = OpenTenantManager.ReloadOrganization.Value(this.activeOrganization);
				if (exchangeConfigurationUnit != null)
				{
					this.activeOrganization = exchangeConfigurationUnit;
				}
			}
			catch (ADTransientException ex)
			{
				OpenTenantManager.trace.TraceWarning<string, string>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: Encountered a transient exception when reloading the organization, ignoring.  Message: {1}", this.activeOrganization.ToString(), ex.Message);
			}
			return this.ValidateOrganizationHasVacancy(this.activeOrganization);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007C9C File Offset: 0x00005E9C
		private bool ValidateOrganizationHasVacancy(ExchangeConfigurationUnit organization)
		{
			if (organization.OpenTenantFull)
			{
				return false;
			}
			bool flag = true;
			try
			{
				int num = OpenTenantManager.GetMailboxCount.Value(organization.OrganizationId);
				OpenTenantManager.trace.TraceDebug<string, int>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: Found {1} mailboxes.", organization.ToString(), num);
				flag = (num < OpenTenantManager.maximumUserPerTenantThreshold.Value);
			}
			catch (ADTransientException ex)
			{
				OpenTenantManager.trace.TraceWarning<string, string>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: Encountered a transient exception when querying AD for tenant vacancy, ignoring.  Message: {1}", organization.ToString(), ex.Message);
			}
			if (!flag)
			{
				OpenTenantManager.trace.TraceDebug<string, int>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: Exceeded the user threshold of {1}.", organization.ToString(), OpenTenantManager.maximumUserPerTenantThreshold.Value);
				try
				{
					OpenTenantManager.SaveFullOrganization.Value(organization);
				}
				catch (ADTransientException ex2)
				{
					OpenTenantManager.trace.TraceWarning<string>((long)Thread.CurrentThread.ManagedThreadId, "Encountered a transient exception trying to update the vacancy flag on a tenant.  Message: {0}", ex2.Message);
				}
			}
			return flag;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007DA4 File Offset: 0x00005FA4
		private void EvaluateActiveOrganizationWork()
		{
			for (;;)
			{
				lock (this.evaluationPulseObject)
				{
					EventHandler evaluationWaitingOnPulse = this.EvaluationWaitingOnPulse;
					if (evaluationWaitingOnPulse != null)
					{
						evaluationWaitingOnPulse(this, EventArgs.Empty);
					}
					Monitor.Wait(this.evaluationPulseObject, OpenTenantManager.activeTenantVacancyEvaluationInterval.Value);
				}
				try
				{
					lock (this.disposing)
					{
						if (this.disposing.IsCancellationRequested)
						{
							break;
						}
						this.EvaluateActiveOrganization();
					}
					continue;
				}
				catch (ADTransientException ex)
				{
					OpenTenantManager.trace.TraceWarning<string>((long)Thread.CurrentThread.ManagedThreadId, "ADTransientException: {0}", ex.Message);
					OpenTenantManager.trace.TraceWarning<TimeSpan>((long)Thread.CurrentThread.ManagedThreadId, "Retry no sooner than {0}.", OpenTenantManager.minimumDelayBetweenFailedEvaluations.Value);
					Thread.Sleep(OpenTenantManager.minimumDelayBetweenFailedEvaluations.Value);
					continue;
				}
				catch (OpenTenantQueryException)
				{
					OpenTenantManager.trace.TraceWarning<TimeSpan>((long)Thread.CurrentThread.ManagedThreadId, "Retry no sooner than {0}.", OpenTenantManager.minimumDelayBetweenFailedEvaluations.Value);
					Thread.Sleep(OpenTenantManager.minimumDelayBetweenFailedEvaluations.Value);
					continue;
				}
				break;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00007F00 File Offset: 0x00006100
		private void EvaluateActiveOrganization()
		{
			if (this.activeOrganization == null || !this.ValidateOrganizationHasVacancy() || this.activeLifetime.Elapsed > OpenTenantManager.activeTenantLifespan.Value)
			{
				this.activeOrganization = this.GetNextActiveOrganization();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00007F58 File Offset: 0x00006158
		private ExchangeConfigurationUnit GetNextActiveOrganization()
		{
			OpenTenantManager.trace.TraceDebug((long)Thread.CurrentThread.ManagedThreadId, "Querying for partitions.");
			PartitionId[] array = OpenTenantManager.GetPartitions.Value();
			if (array == null || array.Length == 0)
			{
				OpenTenantManager.trace.TraceError((long)Thread.CurrentThread.ManagedThreadId, "Query for partitions returned no results.");
				throw new OpenTenantQueryException("Query for partitions returned no results.");
			}
			OpenTenantManager.trace.TraceDebug<int>((long)Thread.CurrentThread.ManagedThreadId, "Found {0} partitions.", array.Length);
			ADTransientException ex = null;
			foreach (PartitionId partitionId in from p in array
			orderby OpenTenantManager.random.Next()
			select p)
			{
				OpenTenantManager.trace.TraceDebug<string>((long)Thread.CurrentThread.ManagedThreadId, "Partition [{0}]: Querying for open tenants.", partitionId.ToString());
				ExchangeConfigurationUnit[] array2;
				try
				{
					array2 = OpenTenantManager.GetOpenOrganizations.Value(partitionId);
				}
				catch (ADTransientException ex2)
				{
					OpenTenantManager.trace.TraceWarning<string, string>((long)Thread.CurrentThread.ManagedThreadId, "Partition [{0}]: Encountered a transient exception when querying AD for open tenants.  Message: {1}", partitionId.ToString(), ex2.Message);
					ex = ex2;
					continue;
				}
				if (array2 == null || array2.Length == 0)
				{
					OpenTenantManager.trace.TraceWarning<string>((long)Thread.CurrentThread.ManagedThreadId, "Partition [{0}]: Query for organizations returned no results.", partitionId.ToString());
				}
				else
				{
					OpenTenantManager.trace.TraceDebug<string, int>((long)Thread.CurrentThread.ManagedThreadId, "Partition [{0}]: Found {1} open tenants.", partitionId.ToString(), array2.Length);
					foreach (ExchangeConfigurationUnit exchangeConfigurationUnit in array2.OrderBy((ExchangeConfigurationUnit p) => OpenTenantManager.random.Next()))
					{
						OpenTenantManager.trace.TraceDebug<string>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: Querying for vacancy.", exchangeConfigurationUnit.ToString());
						if (this.ValidateOrganizationHasVacancy(exchangeConfigurationUnit))
						{
							OpenTenantManager.trace.TraceDebug<string>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: Has vacancy and will be used.", exchangeConfigurationUnit.ToString());
							this.activeLifetime.Restart();
							return exchangeConfigurationUnit;
						}
						OpenTenantManager.trace.TraceWarning<string>((long)Thread.CurrentThread.ManagedThreadId, "Organization [{0}]: No vacancy.", exchangeConfigurationUnit.ToString());
					}
					OpenTenantManager.trace.TraceWarning<string>((long)Thread.CurrentThread.ManagedThreadId, "Partition [{0}]: No organizations have vacancy.", partitionId.ToString());
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			OpenTenantManager.trace.TraceError((long)Thread.CurrentThread.ManagedThreadId, "Unable to find any organizations with vacancy.");
			throw new OpenTenantQueryException("Unable to find any organizations with vacancy.");
		}

		// Token: 0x04000142 RID: 322
		internal static readonly Hookable<Func<PartitionId[]>> GetPartitions = Hookable<Func<PartitionId[]>>.Create(true, new Func<PartitionId[]>(ADAccountPartitionLocator.GetAllAccountPartitionIds));

		// Token: 0x04000143 RID: 323
		internal static readonly Hookable<Func<PartitionId, ExchangeConfigurationUnit[]>> GetOpenOrganizations = Hookable<Func<PartitionId, ExchangeConfigurationUnit[]>>.Create(true, delegate(PartitionId partitionId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsPartitionId(partitionId);
			ITenantConfigurationSession tenantConfigurationSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.IgnoreInvalid, sessionSettings, 44, "GetOpenOrganizations", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Common\\OpenTenantManager.cs");
			return tenantConfigurationSession.FindAllOpenConfigurationUnits(true);
		});

		// Token: 0x04000144 RID: 324
		internal static readonly Hookable<Func<OrganizationId, int>> GetMailboxCount = Hookable<Func<OrganizationId, int>>.Create(true, delegate(OrganizationId organizationId)
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, null, false);
			IConfigurationSession configSession = DirectorySessionFactory.Default.CreateTenantConfigurationSession(ConsistencyMode.PartiallyConsistent, sessionSettings, 61, "GetMailboxCount", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Common\\OpenTenantManager.cs");
			return SystemAddressListMemberCount.GetCount(configSession, organizationId, "All Mail Users(VLV)", false);
		});

		// Token: 0x04000145 RID: 325
		internal static readonly Hookable<Action<ExchangeConfigurationUnit>> SaveFullOrganization = Hookable<Action<ExchangeConfigurationUnit>>.Create(true, new Action<ExchangeConfigurationUnit>(OpenTenantManager.SaveFullOrganizationImplementation));

		// Token: 0x04000146 RID: 326
		internal static readonly Hookable<Func<ExchangeConfigurationUnit, ExchangeConfigurationUnit>> ReloadOrganization = Hookable<Func<ExchangeConfigurationUnit, ExchangeConfigurationUnit>>.Create(true, new Func<ExchangeConfigurationUnit, ExchangeConfigurationUnit>(OpenTenantManager.ReloadOrganizationImplementation));

		// Token: 0x04000147 RID: 327
		private static readonly Microsoft.Exchange.Diagnostics.Trace trace = ExTraceGlobals.OpenTenantManagerTracer;

		// Token: 0x04000148 RID: 328
		private static readonly TimeSpanAppSettingsEntry activeTenantLifespan = new TimeSpanAppSettingsEntry("OpenTenantManager.ActiveTenantLifespan_Hours", TimeSpanUnit.Hours, TimeSpan.FromHours(24.0), OpenTenantManager.trace);

		// Token: 0x04000149 RID: 329
		private static readonly TimeSpanAppSettingsEntry activeTenantVacancyEvaluationInterval = new TimeSpanAppSettingsEntry("OpenTenantManager.ActiveTenantVacancyEvaluationInterval_Minutes", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(5.0), OpenTenantManager.trace);

		// Token: 0x0400014A RID: 330
		private static readonly IntAppSettingsEntry maximumUserPerTenantThreshold = new IntAppSettingsEntry("OpenTenantManager.MaximumUsersPerTenantThreshold", 100000, OpenTenantManager.trace);

		// Token: 0x0400014B RID: 331
		private static readonly TimeSpanAppSettingsEntry minimumDelayBetweenFailedEvaluations = new TimeSpanAppSettingsEntry("OpenTenantManager.MinimumDelayBetweenFailedEvaluations_Seconds", TimeSpanUnit.Seconds, TimeSpan.FromSeconds(10.0), OpenTenantManager.trace);

		// Token: 0x0400014C RID: 332
		private static readonly Lazy<OpenTenantManager> instance = new Lazy<OpenTenantManager>(() => new OpenTenantManager());

		// Token: 0x0400014D RID: 333
		private static readonly Random random = new Random();

		// Token: 0x0400014E RID: 334
		private readonly Stopwatch activeLifetime = new Stopwatch();

		// Token: 0x0400014F RID: 335
		private readonly object evaluationPulseObject = new object();

		// Token: 0x04000150 RID: 336
		private readonly CancellationTokenSource disposing = new CancellationTokenSource();

		// Token: 0x04000151 RID: 337
		private readonly Thread workerThread;

		// Token: 0x04000152 RID: 338
		private volatile ExchangeConfigurationUnit activeOrganization;
	}
}
