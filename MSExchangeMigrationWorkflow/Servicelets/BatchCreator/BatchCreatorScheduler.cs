using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.ServiceModel;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalanceClient;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Servicelets.BatchCreator
{
	// Token: 0x0200000D RID: 13
	internal class BatchCreatorScheduler : CacheScheduler
	{
		// Token: 0x06000033 RID: 51 RVA: 0x000029FB File Offset: 0x00000BFB
		internal BatchCreatorScheduler(AnchorContext context, WaitHandle stopEvent) : this(context, stopEvent, LoadBalanceServiceAdapter.Create(context.Logger))
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A10 File Offset: 0x00000C10
		protected BatchCreatorScheduler(AnchorContext context, WaitHandle stopEvent, ILoadBalanceServicePort loadBalanceClient) : base(context, stopEvent)
		{
			this.MailboxCache = new BatchCreatorScheduler.MigrationMailboxCache(context);
			this.OccupantCreators = new Dictionary<MigrationOccupantType, BatchCreatorScheduler.OccupantCreator>();
			this.LoadBalanceClient = loadBalanceClient;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002A38 File Offset: 0x00000C38
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002A40 File Offset: 0x00000C40
		private BatchCreatorScheduler.MigrationMailboxCache MailboxCache { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002A49 File Offset: 0x00000C49
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00002A51 File Offset: 0x00000C51
		private ILoadBalanceServicePort LoadBalanceClient { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002A5A File Offset: 0x00000C5A
		// (set) Token: 0x0600003A RID: 58 RVA: 0x00002A62 File Offset: 0x00000C62
		private Dictionary<MigrationOccupantType, BatchCreatorScheduler.OccupantCreator> OccupantCreators { get; set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002A84 File Offset: 0x00000C84
		protected override AnchorJobProcessorResult ProcessEntry(CacheEntryBase cacheEntry)
		{
			MigrationOccupantType migrationOccupantType = MigrationOccupantType.Regular;
			try
			{
				migrationOccupantType = base.Context.Config.GetConfig<MigrationOccupantType>("OccupantTypes");
			}
			catch (ConfigurationSettingsException ex)
			{
				this.LogFailure(ex);
				return AnchorJobProcessorResult.Waiting;
			}
			base.Context.Logger.Log(MigrationEventType.Verbose, "Entered ProcessEntry, working on types '{0}'", new object[]
			{
				migrationOccupantType
			});
			bool flag = false;
			foreach (MigrationOccupantType migrationOccupantType2 in from t in migrationOccupantType.ToString().Split(new char[]
			{
				','
			})
			select (MigrationOccupantType)Enum.Parse(typeof(MigrationOccupantType), t))
			{
				base.Context.Logger.Log(MigrationEventType.Verbose, "working on type {0}", new object[]
				{
					migrationOccupantType2
				});
				BatchCreatorScheduler.OccupantCreator occupantCreator;
				if (!this.OccupantCreators.TryGetValue(migrationOccupantType2, out occupantCreator))
				{
					base.Context.Logger.Log(MigrationEventType.Information, "Creating a creator to work on {0}", new object[]
					{
						migrationOccupantType2
					});
					occupantCreator = new BatchCreatorScheduler.OccupantCreator(migrationOccupantType2);
					this.OccupantCreators.Add(migrationOccupantType2, occupantCreator);
				}
				using (occupantCreator.ActivateContext(base.Context))
				{
					try
					{
						if (this.ShouldCreate(occupantCreator.LastRun))
						{
							IList<MailboxId> migrationMailboxes = this.GetMigrationMailboxes(cacheEntry.ADProvider);
							base.Context.Logger.Log(MigrationEventType.Verbose, "Found {0} active migration mailboxes to run batches", new object[]
							{
								migrationMailboxes.Count
							});
							if (migrationMailboxes.Count <= 0)
							{
								base.Context.Logger.Log(MigrationEventType.Warning, "No active migration mailboxes to run batches", new object[0]);
							}
							else
							{
								BatchCapacityDatum batchCapacityForForest = this.LoadBalanceClient.GetBatchCapacityForForest(base.Context.Config.GetConfig<int>("MailboxRunLimit"));
								base.Context.Logger.Log(MigrationEventType.Verbose, "Load balancer allowed us to create {0} mailboxes", new object[]
								{
									batchCapacityForForest.MaximumNumberOfMailboxes
								});
								long maximumAccounts = (long)batchCapacityForForest.MaximumNumberOfMailboxes;
								int? config = base.Context.Config.GetConfig<int?>("MaximumTotalMailboxSize");
								MigrationAccount[] array = this.SelectAccountsToMigrate(maximumAccounts, (config != null) ? new long?((long)config.GetValueOrDefault()) : null, base.Context.Config.GetConfig<int?>("ConstraintId"));
								flag = (array.Length > 0);
								int index = 0;
								foreach (List<MigrationAccount> list in this.CreateSubBatches<MigrationAccount>(array, migrationMailboxes.Count))
								{
									base.Context.Logger.Log(MigrationEventType.Verbose, "Creating migration batch of size {0} for migration mailbox id {1}", new object[]
									{
										list.Count,
										migrationMailboxes[index]
									});
									this.CreateMigrationBatch(migrationMailboxes[index++], this.CreateCsv(list));
								}
							}
						}
					}
					catch (ConfigurationSettingsException ex2)
					{
						this.LogFailure(ex2);
					}
					catch (MigrationPermanentException ex3)
					{
						this.LogFailure(ex3);
					}
					catch (BatchCreatorException ex4)
					{
						this.LogFailure(ex4);
					}
				}
			}
			if (!flag)
			{
				return AnchorJobProcessorResult.Waiting;
			}
			return AnchorJobProcessorResult.Working;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E94 File Offset: 0x00001094
		protected virtual MigrationAccount[] SelectAccountsToMigrate(long maximumAccounts, long? maximumTotalSize, int? constraintId)
		{
			for (int i = 3; i > 0; i--)
			{
				try
				{
					IMailboxReplicationProxyService mailboxReplicationProxyService = MailboxReplicationProxyClient.CreateForOlcConnection(OlcTopology.Instance.FindServerByLocalDatacenter(), ProxyControlFlags.DoNotApplyProxyThrottling);
					return mailboxReplicationProxyService.SelectAccountsToMigrate(maximumAccounts, maximumTotalSize, constraintId);
				}
				catch (TimeoutException ex)
				{
					base.Context.Logger.Log(MigrationEventType.Warning, "Encountered timeout error when trying to make wcf call, iteration {0}", new object[]
					{
						i,
						ex
					});
					if (i <= 1)
					{
						throw;
					}
				}
				catch (CommunicationException ex2)
				{
					base.Context.Logger.Log(MigrationEventType.Warning, "Encountered error when trying to make wcf call, iteration {0}", new object[]
					{
						i,
						ex2
					});
					if (i <= 1)
					{
						throw;
					}
				}
			}
			return null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F64 File Offset: 0x00001164
		protected virtual void CreateMigrationBatch(MailboxId migrationMailbox, byte[] csvFile)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					using (AnchorRunspaceProxy anchorRunspaceProxy = AnchorRunspaceProxy.CreateRunspaceForDatacenterAdmin(base.Context, base.Context.ApplicationName))
					{
						PSCommand pscommand = new PSCommand();
						pscommand.AddCommand("New-MigrationBatch");
						pscommand.AddParameter("XO1", true);
						pscommand.AddParameter("CSVData", csvFile);
						pscommand.AddParameter("Partition", migrationMailbox);
						anchorRunspaceProxy.RunPSCommand<PSObject>(pscommand);
					}
					base.Context.Logger.Log(MigrationEventType.Information, "Successfully created new migration batch against {0}", new object[]
					{
						migrationMailbox
					});
					break;
				}
				catch (InvalidRunspaceStateException ex)
				{
					base.Context.Logger.Log(MigrationEventType.Error, "PS Runspace was closed so command could not be run : {0}", new object[]
					{
						ex
					});
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000030D4 File Offset: 0x000012D4
		private byte[] CreateCsv(IList<MigrationAccount> accounts)
		{
			XO1CsvSchema xo1CsvSchema = new XO1CsvSchema();
			Dictionary<string, Func<MigrationAccount, string>> dictionary = new Dictionary<string, Func<MigrationAccount, string>>(7);
			dictionary["EmailAddress"] = ((MigrationAccount account) => account.Login);
			dictionary["Puid"] = ((MigrationAccount account) => account.Puid.ToString());
			dictionary["FirstName"] = ((MigrationAccount account) => account.FirstName);
			dictionary["LastName"] = ((MigrationAccount account) => account.LastName);
			dictionary["AccountSize"] = ((MigrationAccount account) => account.AccountSize.ToString());
			dictionary["TimeZone"] = ((MigrationAccount account) => account.TimeZone);
			dictionary["Lcid"] = ((MigrationAccount account) => account.Lcid);
			dictionary["Aliases"] = ((MigrationAccount account) => string.Join(string.Format("{0}", '\u0001'), account.Aliases));
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(accounts.Count * 128))
			{
				using (StreamWriter streamWriter = new StreamWriter(memoryStream))
				{
					xo1CsvSchema.WriteHeaders(streamWriter);
					foreach (MigrationAccount arg in accounts)
					{
						List<string> list = new List<string>(7);
						foreach (string key in xo1CsvSchema.GetOrderedHeaders())
						{
							list.Add(dictionary[key](arg));
						}
						streamWriter.WriteCsvLine(list);
					}
					streamWriter.Flush();
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003328 File Offset: 0x00001528
		private IList<MailboxId> GetMigrationMailboxes(IAnchorADProvider adProvider)
		{
			List<MailboxId> list = new List<MailboxId>();
			IEnumerable<BatchCreatorScheduler.MigrationMailbox> mailboxes = this.MailboxCache.GetMailboxes(adProvider);
			foreach (BatchCreatorScheduler.MigrationMailbox migrationMailbox in mailboxes)
			{
				string mailboxServerFqdn = adProvider.GetMailboxServerFqdn(migrationMailbox.User, false);
				using (new ServerSettingsContext(mailboxServerFqdn, null, null).Activate())
				{
					if (!base.Context.Config.GetConfig<bool>("IsEnabled"))
					{
						base.Context.Logger.Log(MigrationEventType.Verbose, "Migration mailbox located {0} is disabled via config", new object[]
						{
							mailboxServerFqdn
						});
						migrationMailbox.SetFailure(new BatchCreatorScheduler.ServerNotEnabledException(mailboxServerFqdn));
						continue;
					}
				}
				list.Add(new MailboxId(migrationMailbox.User.ExchangeGuid));
			}
			return list;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003420 File Offset: 0x00001620
		private bool ShouldCreate(DateTime lastRunTime)
		{
			if (!base.Context.Config.GetConfig<bool>("IsEnabled"))
			{
				base.Context.Logger.Log(MigrationEventType.Verbose, "Skipping run because service is not enabled", new object[0]);
				return false;
			}
			TimeSpan config = base.Context.Config.GetConfig<TimeSpan>("RunInterval");
			if (lastRunTime + config > DateTime.UtcNow)
			{
				base.Context.Logger.Log(MigrationEventType.Verbose, "Skipping run because last ran at {0} and next run should be {1}", new object[]
				{
					base.LastRunTime,
					base.LastRunTime + config
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034F4 File Offset: 0x000016F4
		private void LogFailure(Exception ex)
		{
			base.Context.Logger.Log(MigrationEventType.Error, "Encountered error", new object[]
			{
				ex
			});
			CommonFailureLog.LogCommonFailureEvent(base.Context.ApplicationName, ex, Guid.NewGuid(), 0, AnchorLogContext.Current.ToString());
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000036A0 File Offset: 0x000018A0
		private IEnumerable<List<T>> CreateSubBatches<T>(IEnumerable<T> batch, int numOfBatches)
		{
			return from p in batch.Select((T p, int i) => new
			{
				p,
				i
			})
			group p by p.i % numOfBatches into p
			select (from v in p
			select v.p).ToList<T>();
		}

		// Token: 0x0200000E RID: 14
		private class OccupantContext : DisposeTrackableBase
		{
			// Token: 0x0600004F RID: 79 RVA: 0x000036F0 File Offset: 0x000018F0
			public OccupantContext(AnchorContext context, MigrationOccupantType occupantType)
			{
				this.LogContext = new BatchCreatorScheduler.OccupantContext.OccupantLogContext(occupantType);
				this.ConfigContext = new GenericSettingsContext("MigrationOccupantType", occupantType.ToString(), null).Activate();
				AnchorLogContext.Current.SetSummarizable(this.LogContext);
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000050 RID: 80 RVA: 0x00003740 File Offset: 0x00001940
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00003748 File Offset: 0x00001948
			private IDisposable ConfigContext { get; set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00003751 File Offset: 0x00001951
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00003759 File Offset: 0x00001959
			private BatchCreatorScheduler.OccupantContext.OccupantLogContext LogContext { get; set; }

			// Token: 0x06000054 RID: 84 RVA: 0x00003762 File Offset: 0x00001962
			protected override void InternalDispose(bool disposing)
			{
				if (this.ConfigContext != null)
				{
					this.ConfigContext.Dispose();
					this.ConfigContext = null;
				}
				if (this.LogContext != null)
				{
					AnchorLogContext.Current.ClearSummarizable(this.LogContext);
					this.LogContext = null;
				}
			}

			// Token: 0x06000055 RID: 85 RVA: 0x0000379D File Offset: 0x0000199D
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<BatchCreatorScheduler.OccupantContext>(this);
			}

			// Token: 0x0200000F RID: 15
			private class OccupantLogContext : ISummarizable
			{
				// Token: 0x06000056 RID: 86 RVA: 0x000037A5 File Offset: 0x000019A5
				public OccupantLogContext(MigrationOccupantType occupantType)
				{
					this.SummaryName = occupantType.ToString();
				}

				// Token: 0x17000012 RID: 18
				// (get) Token: 0x06000057 RID: 87 RVA: 0x0000388C File Offset: 0x00001A8C
				public IEnumerable<string> SummaryTokens
				{
					get
					{
						yield return this.SummaryName;
						yield break;
					}
				}

				// Token: 0x17000013 RID: 19
				// (get) Token: 0x06000058 RID: 88 RVA: 0x000038A9 File Offset: 0x00001AA9
				// (set) Token: 0x06000059 RID: 89 RVA: 0x000038B1 File Offset: 0x00001AB1
				public string SummaryName { get; set; }
			}
		}

		// Token: 0x02000010 RID: 16
		private class OccupantCreator
		{
			// Token: 0x0600005A RID: 90 RVA: 0x000038BA File Offset: 0x00001ABA
			public OccupantCreator(MigrationOccupantType occupantType)
			{
				this.OccupantType = occupantType;
				this.LastRun = DateTime.MinValue;
			}

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600005B RID: 91 RVA: 0x000038D4 File Offset: 0x00001AD4
			// (set) Token: 0x0600005C RID: 92 RVA: 0x000038DC File Offset: 0x00001ADC
			public DateTime LastRun { get; private set; }

			// Token: 0x0600005D RID: 93 RVA: 0x000038E5 File Offset: 0x00001AE5
			public IDisposable ActivateContext(AnchorContext context)
			{
				return new BatchCreatorScheduler.OccupantContext(context, this.OccupantType);
			}

			// Token: 0x04000027 RID: 39
			public readonly MigrationOccupantType OccupantType;
		}

		// Token: 0x02000011 RID: 17
		private class MigrationMailbox
		{
			// Token: 0x0600005E RID: 94 RVA: 0x000038F3 File Offset: 0x00001AF3
			public MigrationMailbox(ADUser user) : this(user, DateTime.MinValue)
			{
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00003901 File Offset: 0x00001B01
			public MigrationMailbox(ADUser user, DateTime lastRefreshed)
			{
				AnchorUtil.ThrowOnNullArgument(user, "user");
				this.User = user;
				this.LastRefreshed = lastRefreshed;
				this.Exception = null;
			}

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x06000060 RID: 96 RVA: 0x00003929 File Offset: 0x00001B29
			// (set) Token: 0x06000061 RID: 97 RVA: 0x00003931 File Offset: 0x00001B31
			public ADUser User { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x06000062 RID: 98 RVA: 0x0000393A File Offset: 0x00001B3A
			// (set) Token: 0x06000063 RID: 99 RVA: 0x00003942 File Offset: 0x00001B42
			public Exception Exception { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000064 RID: 100 RVA: 0x0000394B File Offset: 0x00001B4B
			// (set) Token: 0x06000065 RID: 101 RVA: 0x00003953 File Offset: 0x00001B53
			public DateTime LastRefreshed { get; set; }

			// Token: 0x06000066 RID: 102 RVA: 0x0000395C File Offset: 0x00001B5C
			public void SetSuccess()
			{
				this.LastRefreshed = DateTime.UtcNow;
				this.Exception = null;
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00003970 File Offset: 0x00001B70
			public void SetFailure(Exception exception)
			{
				this.Exception = exception;
			}
		}

		// Token: 0x02000012 RID: 18
		private class MigrationMailboxCache
		{
			// Token: 0x06000068 RID: 104 RVA: 0x00003979 File Offset: 0x00001B79
			public MigrationMailboxCache(AnchorContext context)
			{
				AnchorUtil.ThrowOnNullArgument(context, "context");
				this.Context = context;
				this.LastRefreshed = DateTime.MinValue;
				this.MigrationMailboxes = new Dictionary<Guid, BatchCreatorScheduler.MigrationMailbox>();
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000069 RID: 105 RVA: 0x000039A9 File Offset: 0x00001BA9
			// (set) Token: 0x0600006A RID: 106 RVA: 0x000039B1 File Offset: 0x00001BB1
			private DateTime LastRefreshed { get; set; }

			// Token: 0x17000019 RID: 25
			// (get) Token: 0x0600006B RID: 107 RVA: 0x000039BA File Offset: 0x00001BBA
			// (set) Token: 0x0600006C RID: 108 RVA: 0x000039C2 File Offset: 0x00001BC2
			private Dictionary<Guid, BatchCreatorScheduler.MigrationMailbox> MigrationMailboxes { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x0600006D RID: 109 RVA: 0x000039CB File Offset: 0x00001BCB
			// (set) Token: 0x0600006E RID: 110 RVA: 0x000039D3 File Offset: 0x00001BD3
			private AnchorContext Context { get; set; }

			// Token: 0x0600006F RID: 111 RVA: 0x000039F4 File Offset: 0x00001BF4
			public IEnumerable<BatchCreatorScheduler.MigrationMailbox> GetMailboxes(IAnchorADProvider adProvider)
			{
				if (this.LastRefreshed + this.Context.Config.GetConfig<TimeSpan>("MigrationMailboxRefreshInterval") < DateTime.UtcNow)
				{
					this.Context.Logger.Log(MigrationEventType.Information, "refreshing migration mailboxes", new object[0]);
					ADUser[] array = AnchorADProvider.GetRootOrgProvider(this.Context).GetOrganizationMailboxesByCapability(OrganizationCapability.Migration).ToArray<ADUser>();
					Dictionary<Guid, BatchCreatorScheduler.MigrationMailbox> dictionary = new Dictionary<Guid, BatchCreatorScheduler.MigrationMailbox>(array.Length);
					foreach (ADUser aduser in array)
					{
						DateTime lastRefreshed = DateTime.MinValue;
						BatchCreatorScheduler.MigrationMailbox migrationMailbox;
						if (this.MigrationMailboxes.TryGetValue(aduser.ExchangeGuid, out migrationMailbox))
						{
							lastRefreshed = migrationMailbox.LastRefreshed;
						}
						dictionary[aduser.ExchangeGuid] = new BatchCreatorScheduler.MigrationMailbox(aduser, lastRefreshed);
					}
					this.MigrationMailboxes = dictionary;
					this.LastRefreshed = DateTime.UtcNow;
				}
				return from x in this.MigrationMailboxes.Values
				orderby x.LastRefreshed, x.User.ExchangeGuid
				select x;
			}
		}

		// Token: 0x02000013 RID: 19
		private class ServerNotEnabledException : BatchCreatorException
		{
			// Token: 0x06000072 RID: 114 RVA: 0x00003B22 File Offset: 0x00001D22
			public ServerNotEnabledException(string msg) : base(msg)
			{
			}
		}
	}
}
