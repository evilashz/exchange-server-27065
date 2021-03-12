using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Timers;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.AuditLogSearchServicelet;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.AuditLogSearch.Messages;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Servicelets.AuditLogSearch
{
	// Token: 0x0200000B RID: 11
	public class AuditLogSearchServicelet : Servicelet
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003074 File Offset: 0x00001274
		private static ADUser[] GlobalFindAllArbitrationMailboxes()
		{
			Server localhost = AuditLogSearchContext.Localhost;
			if (!MapiTaskHelper.IsDatacenter)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 99, "GlobalFindAllArbitrationMailboxes", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\AuditLogSearch\\Program\\AuditLogSearchServicelet.cs");
				return tenantOrRootOrgRecipientSession.FindPaged<ADUser>(RecipientFilterHelper.DiscoveryMailboxFilterForAuditLog(localhost.ExchangeLegacyDN), null, true, null, 0).ToArray<ADUser>();
			}
			return PartitionDataAggregator.FindAllArbitrationMailboxes(localhost.ExchangeLegacyDN);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030DC File Offset: 0x000012DC
		public override void Work()
		{
			AuditLogSearchHealthHandler instance = AuditLogSearchHealthHandler.GetInstance();
			AuditLogSearchHealth auditLogSearchHealth = instance.AuditLogSearchHealth;
			WaitHandle[] waitHandles = new WaitHandle[]
			{
				base.StopEvent,
				instance.RunSearchNowEvent
			};
			AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchServiceletStarted, string.Empty, new object[]
			{
				this.pid
			});
			ExTraceGlobals.ServiceletTracer.TraceInformation(42415, (long)this.GetHashCode(), "AuditLogSearch servicelet starting");
			this.PublishNotification("AsyncSearchServiceletStarting", this.pid.ToString(), ResultSeverityLevel.Informational);
			AuditLogSearchServicelet.hbTimer.Elapsed += this.Callback;
			AuditLogSearchServicelet.hbTimer.Enabled = true;
			for (;;)
			{
				int defaultDelay = this.ReadPollInterval();
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						this.WorkInternal();
					});
				}
				catch (GrayException ex)
				{
					ExTraceGlobals.ServiceletTracer.TraceError(21863L, ex.ToString());
					AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_ServiceletException, string.Empty, new object[]
					{
						ex.ToString()
					});
					this.PublishNotification("AuditLogSearchCompletedWithErrors", ex.ToString(), ResultSeverityLevel.Error);
					auditLogSearchHealth.AddException(ex);
				}
				int millisecondsTimeout = this.retryPolicy.ProceedToNextIteration(defaultDelay);
				int num = WaitHandle.WaitAny(waitHandles, millisecondsTimeout, false);
				bool flag = num == 0;
				if (flag)
				{
					break;
				}
				bool flag2 = num == 1;
				if (flag2)
				{
					this.retryPolicy.Reset();
				}
			}
			AuditLogSearchServicelet.hbTimer.Enabled = false;
			AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchServiceletEnded, string.Empty, new object[]
			{
				this.pid
			});
			ExTraceGlobals.ServiceletTracer.TraceInformation(28310, (long)this.GetHashCode(), "AuditLogSearch servicelet stopped");
			AuditLogSearchServicelet.hbTimer.Enabled = false;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000032D4 File Offset: 0x000014D4
		public void Callback(object state, ElapsedEventArgs e)
		{
			this.PublishNotification("AsyncSearchServiceletRunning", this.pid.ToString(), ResultSeverityLevel.Informational);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032FC File Offset: 0x000014FC
		private int ReadPollInterval()
		{
			string text = "AuditLogSearchPollIntervalInMilliseconds";
			int pollIntervalDefaultValue = AuditLogSearchServicelet.PollIntervalDefaultValue;
			string text2 = null;
			try
			{
				text2 = ConfigurationManager.AppSettings[text];
			}
			catch (ConfigurationException ex)
			{
				ExTraceGlobals.ServiceletTracer.TraceWarning<string, ConfigurationException>(17790, (long)this.GetHashCode(), "Exception caught while reading {0} value from app.config, {1}", text, ex);
				AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchReadConfigError, string.Empty, new object[]
				{
					ex.ToString()
				});
			}
			int num;
			if (!int.TryParse(text2, out num))
			{
				num = pollIntervalDefaultValue;
				ExTraceGlobals.ServiceletTracer.TraceWarning<string, string, int>(17790, (long)this.GetHashCode(), "Unable to parse integer setting '{0}' value '{1}' from app.config, using default value {2}", text, text2, pollIntervalDefaultValue);
			}
			if (num != -1 && num < AuditLogSearchServicelet.PollIntervalMinValue)
			{
				num = pollIntervalDefaultValue;
				ExTraceGlobals.ServiceletTracer.TraceWarning<string, string, int>(17790, (long)this.GetHashCode(), "'{0}' value '{1}' from app.config is too small, using default value {2}", text, text2, pollIntervalDefaultValue);
			}
			ExTraceGlobals.ServiceletTracer.TraceInformation<int>(13340, (long)this.GetHashCode(), "Poll interval to be used: {0} milliseconds", num);
			return num;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033F8 File Offset: 0x000015F8
		private void WorkInternal()
		{
			AuditLogSearchHealth auditLogSearchHealth = AuditLogSearchHealthHandler.GetInstance().AuditLogSearchHealth;
			auditLogSearchHealth.ProcessStartTime = DateTime.UtcNow;
			auditLogSearchHealth.ProcessEndTime = null;
			int num = this.random.Next(100000, 999999);
			AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchStarted, string.Empty, new object[]
			{
				this.pid,
				num
			});
			Exception ex = null;
			try
			{
				ConcurrentQueue<ADUser> concurrentQueue;
				if (this.retryPolicy.IsRetrying)
				{
					concurrentQueue = this.retryPolicy.RetryTenants;
					ExTraceGlobals.ServiceletTracer.TraceInformation<int>(25566, (long)this.GetHashCode(), "Retrying search for {0} tenant arbitration mailbox(es)", concurrentQueue.Count);
				}
				else
				{
					auditLogSearchHealth.Clear();
					ADUser[] array = AuditLogSearchServicelet.GlobalFindAllArbitrationMailboxes();
					if (array == null || array.Length == 0)
					{
						ExTraceGlobals.ServiceletTracer.TraceInformation(36433, (long)this.GetHashCode(), "No tenant arbitration mailboxes found in this server");
						return;
					}
					concurrentQueue = new ConcurrentQueue<ADUser>(array);
					ExTraceGlobals.ServiceletTracer.TraceInformation<int>(25566, (long)this.GetHashCode(), "Found {0} tenant arbitration mailbox(es)", concurrentQueue.Count);
					foreach (ADUser tenant in concurrentQueue)
					{
						auditLogSearchHealth.AddTenant(tenant);
					}
				}
				this.retryPolicy.ClearRetryTenants();
				int num2 = Math.Min(concurrentQueue.Count, Environment.ProcessorCount);
				ExTraceGlobals.ServiceletTracer.TraceInformation<int>(19568, (long)this.GetHashCode(), "Starting {0} worker threads", num2);
				Thread[] array2 = new Thread[num2];
				for (int i = 0; i < num2; i++)
				{
					Thread thread = array2[i] = new Thread(new ParameterizedThreadStart(this.ThreadStart));
					thread.Start(concurrentQueue);
				}
				ExTraceGlobals.ServiceletTracer.TraceInformation(28049, (long)this.GetHashCode(), "Waiting for the threads to join");
				foreach (Thread thread2 in array2)
				{
					thread2.Join();
				}
				AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_AuditLogSearchEnded, string.Empty, new object[]
				{
					this.pid,
					num
				});
			}
			catch (ADTransientException ex2)
			{
				ex = ex2;
			}
			catch (LocalServerNotFoundException ex3)
			{
				ex = ex3;
			}
			finally
			{
				auditLogSearchHealth.ProcessEndTime = new DateTime?(DateTime.UtcNow);
			}
			if (ex != null)
			{
				string text = ex.ToString();
				ExTraceGlobals.ServiceletTracer.TraceInformation<string>(11881, (long)this.GetHashCode(), "Error: {0}", text);
				this.PublishNotification("AuditLogSearchCompletedWithErrors", text, ResultSeverityLevel.Error);
				AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_TransientException, string.Empty, new object[]
				{
					this.pid,
					num,
					text
				});
				auditLogSearchHealth.AddException(ex);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003740 File Offset: 0x00001940
		private void PublishNotification(string notification, string message, ResultSeverityLevel level)
		{
			new EventNotificationItem(ExchangeComponent.Compliance.Name, ExchangeComponent.Compliance.Name, notification, level)
			{
				Message = message
			}.Publish(false);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000037A8 File Offset: 0x000019A8
		private void ThreadStart(object queue)
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					ConcurrentQueue<ADUser> tenants = (ConcurrentQueue<ADUser>)queue;
					this.ProcessTenants(tenants);
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.WorkerTracer.TraceError(11881L, ex.ToString());
				AuditLogSearchContext.EventLogger.LogEvent(MSExchangeAuditLogSearchEventLogConstants.Tuple_WorkerException, string.Empty, new object[]
				{
					ex.ToString()
				});
				AuditLogSearchHealthHandler.GetInstance().AuditLogSearchHealth.AddException(ex);
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003844 File Offset: 0x00001A44
		private void ProcessTenants(ConcurrentQueue<ADUser> tenants)
		{
			TenantWorker tenantWorker = new TenantWorker(base.StopEvent, this.retryPolicy.RetryIteration);
			ADUser aduser;
			while (tenants.TryDequeue(out aduser))
			{
				if (base.StopEvent.WaitOne(0, false))
				{
					ExTraceGlobals.WorkerTracer.TraceInformation(29459, (long)this.GetHashCode(), "Service is shutting down");
					return;
				}
				if (!tenantWorker.RunSearches(aduser))
				{
					this.retryPolicy.RetryTenants.Enqueue(aduser);
				}
			}
		}

		// Token: 0x04000031 RID: 49
		private const string AuditLogSearchPollInterval = "AuditLogSearchPollIntervalInMilliseconds";

		// Token: 0x04000032 RID: 50
		private const string SearchErrorEventName = "AuditLogSearchCompletedWithErrors";

		// Token: 0x04000033 RID: 51
		private const string ServiceletStartEventName = "AsyncSearchServiceletStarting";

		// Token: 0x04000034 RID: 52
		private const string ServiceletHeartbeatEventName = "AsyncSearchServiceletRunning";

		// Token: 0x04000035 RID: 53
		private static readonly int PollIntervalDefaultValue = (int)TimeSpan.FromMinutes(30.0).TotalMilliseconds;

		// Token: 0x04000036 RID: 54
		private static readonly int PollIntervalMinValue = (int)TimeSpan.FromMinutes(1.0).TotalMilliseconds;

		// Token: 0x04000037 RID: 55
		private static System.Timers.Timer hbTimer = new System.Timers.Timer(300000.0);

		// Token: 0x04000038 RID: 56
		private readonly AuditLogSearchRetryPolicy retryPolicy = new AuditLogSearchRetryPolicy();

		// Token: 0x04000039 RID: 57
		private readonly int pid = ApplicationName.Current.ProcessId;

		// Token: 0x0400003A RID: 58
		private Random random = new Random();
	}
}
