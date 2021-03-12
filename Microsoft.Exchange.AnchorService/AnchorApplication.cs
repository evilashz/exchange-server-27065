using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.SyncMigrationServicelet;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000007 RID: 7
	internal class AnchorApplication : IDiagnosable
	{
		// Token: 0x0600004B RID: 75 RVA: 0x000029AC File Offset: 0x00000BAC
		internal AnchorApplication(AnchorContext context, WaitHandle stopEvent)
		{
			this.Context = context;
			this.JobCache = new JobCache(this.Context);
			this.WaitHandles = new WaitHandle[]
			{
				stopEvent,
				this.JobCache.CacheUpdated
			};
			this.Components = this.Context.CreateCacheComponents(stopEvent);
			this.IssueCache = new AnchorIssueCache(this.Context, this.JobCache);
			this.IssueCache.EnableScanning();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A2B File Offset: 0x00000C2B
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002A33 File Offset: 0x00000C33
		private protected AnchorContext Context { protected get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00002A44 File Offset: 0x00000C44
		private protected JobCache JobCache { protected get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002A4D File Offset: 0x00000C4D
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002A55 File Offset: 0x00000C55
		private protected CacheProcessorBase[] Components { protected get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002A5E File Offset: 0x00000C5E
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002A66 File Offset: 0x00000C66
		private protected AnchorIssueCache IssueCache { protected get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002A6F File Offset: 0x00000C6F
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002A77 File Offset: 0x00000C77
		private WaitHandle[] WaitHandles { get; set; }

		// Token: 0x06000056 RID: 86 RVA: 0x00002A80 File Offset: 0x00000C80
		public void Process()
		{
			bool flag = false;
			try
			{
				this.RegisterDiagnosticComponents();
				flag = true;
				this.RegisterFaultInjectionHandler();
				TimeSpan timeSpan;
				do
				{
					timeSpan = this.Context.Config.GetConfig<TimeSpan>("IdleRunDelay");
					if (!this.Context.Config.GetConfig<bool>("IsEnabled"))
					{
						this.Context.Logger.Log(MigrationEventType.Information, "Application is no longer enabled.  sleeping... will check again in {0}", new object[]
						{
							timeSpan
						});
					}
					else
					{
						foreach (CacheProcessorBase cacheProcessorBase in this.Components)
						{
							if (!cacheProcessorBase.ShouldProcess() || this.WaitHandles[0].WaitOne(0, false))
							{
								this.Context.Logger.Log(MigrationEventType.Verbose, "Skipping a pass of {0}", new object[]
								{
									cacheProcessorBase.Name
								});
							}
							else
							{
								cacheProcessorBase.IncrementSequenceNumber();
								cacheProcessorBase.StartWorkTime = new ExDateTime?(ExDateTime.UtcNow);
								Stopwatch stopwatch = Stopwatch.StartNew();
								try
								{
									this.Context.Logger.Log(MigrationEventType.Verbose, "Running a pass of {0}", new object[]
									{
										cacheProcessorBase.Name
									});
									if (cacheProcessorBase.Process(this.JobCache))
									{
										TimeSpan activeRunDelay = cacheProcessorBase.ActiveRunDelay;
										if (timeSpan > activeRunDelay)
										{
											this.Context.Logger.Log(MigrationEventType.Verbose, "overriding current run delay {0} with {1} from component {2}", new object[]
											{
												timeSpan,
												activeRunDelay,
												cacheProcessorBase.Name
											});
											timeSpan = activeRunDelay;
										}
										cacheProcessorBase.LastWorkTime = new ExDateTime?(ExDateTime.UtcNow);
									}
									cacheProcessorBase.Duration = stopwatch.ElapsedMilliseconds;
									cacheProcessorBase.LastRunTime = new ExDateTime?(ExDateTime.UtcNow);
									this.Context.Logger.Log(MigrationEventType.Verbose, "Component {0} pass completed, sec = {1}", new object[]
									{
										cacheProcessorBase.Name,
										stopwatch.Elapsed.TotalSeconds
									});
								}
								catch (TransientException ex)
								{
									this.Context.Logger.Log(MigrationEventType.Error, "component {0} encountered a transient error {1}", new object[]
									{
										cacheProcessorBase.Name,
										ex
									});
									timeSpan = this.Context.Config.GetConfig<TimeSpan>("TransientErrorRunDelay");
									break;
								}
								catch (StoragePermanentException ex2)
								{
									this.Context.Logger.Log(MigrationEventType.Error, "component {0} encountered a permanent error {1}", new object[]
									{
										cacheProcessorBase.Name,
										ex2
									});
									break;
								}
								catch (MigrationPermanentException ex3)
								{
									this.Context.Logger.Log(MigrationEventType.Error, "component {0} encountered a permanent error {1}", new object[]
									{
										cacheProcessorBase.Name,
										ex3
									});
									break;
								}
							}
						}
						this.Context.Logger.Log(MigrationEventType.Verbose, "AnchorService.Process: Sleeping between runs for {0} sec", new object[]
						{
							timeSpan.TotalSeconds
						});
					}
				}
				while (WaitHandle.WaitAny(this.WaitHandles, timeSpan, false) != 0);
			}
			catch (RpcException ex4)
			{
				this.Context.Logger.Log(MigrationEventType.Error, "Encountered error registering diagnostic components, exiting.: {0}", new object[]
				{
					ex4
				});
			}
			finally
			{
				if (flag)
				{
					this.UnregisterDiagnosticComponents();
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E2C File Offset: 0x0000102C
		public string GetDiagnosticComponentName()
		{
			return this.Context.ApplicationName;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E3C File Offset: 0x0000103C
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("AnchorApplication", new XAttribute("name", this.GetDiagnosticComponentName()));
			XElement xelement2 = new XElement("AnchorServiceComponents", new XElement("count", this.Components.Length));
			xelement.Add(xelement2);
			foreach (CacheProcessorBase cacheProcessorBase in this.Components)
			{
				xelement2.Add(cacheProcessorBase.GetDiagnosticInfo(parameters));
			}
			xelement.Add(this.JobCache.GetDiagnosticInfo(parameters));
			xelement.Add(this.Context.Config.GetDiagnosticInfo(parameters.Argument));
			xelement.Add(((IDiagnosable)this.IssueCache).GetDiagnosticInfo(parameters));
			return xelement;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002F0E File Offset: 0x0000110E
		protected virtual void RegisterDiagnosticComponents()
		{
			ProcessAccessManager.RegisterComponent(this);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F16 File Offset: 0x00001116
		protected virtual void UnregisterDiagnosticComponents()
		{
			ProcessAccessManager.UnregisterComponent(this);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F20 File Offset: 0x00001120
		private void RegisterFaultInjectionHandler()
		{
			string config = this.Context.Config.GetConfig<string>("FaultInjectionHandler");
			if (string.IsNullOrEmpty(config))
			{
				return;
			}
			string[] array = config.Split(new char[]
			{
				';'
			});
			if (array.Length != 2)
			{
				return;
			}
			string assemblyFile = array[0];
			string name = array[1];
			Assembly assembly = Assembly.LoadFrom(assemblyFile);
			Type type = assembly.GetType(name);
			IExceptionInjectionHandler exceptionInjectionHandler = (IExceptionInjectionHandler)Activator.CreateInstance(type);
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(exceptionInjectionHandler.Callback);
		}

		// Token: 0x0400000D RID: 13
		private const int StopEventIndex = 0;
	}
}
