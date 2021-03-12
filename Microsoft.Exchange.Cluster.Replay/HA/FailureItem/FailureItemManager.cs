using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000186 RID: 390
	internal class FailureItemManager : TimerComponent, IServiceComponent
	{
		// Token: 0x06000F92 RID: 3986 RVA: 0x000434B4 File Offset: 0x000416B4
		internal FailureItemManager(IADConfig adConfig) : base(TimeSpan.Zero, TimeSpan.FromSeconds((double)RegistryParameters.FailureItemManagerDatabaseListUpdaterIntervalInSec), "FailureItemManager")
		{
			this.adConfig = adConfig;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x000434E3 File Offset: 0x000416E3
		public string Name
		{
			get
			{
				return "Failure Item Manager";
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000434EA File Offset: 0x000416EA
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.FailureItemManager;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x000434ED File Offset: 0x000416ED
		public bool IsCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x000434F0 File Offset: 0x000416F0
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x000434F3 File Offset: 0x000416F3
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000434F6 File Offset: 0x000416F6
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x000434FE File Offset: 0x000416FE
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00043506 File Offset: 0x00041706
		private IADConfig adConfig { get; set; }

		// Token: 0x06000F9B RID: 3995 RVA: 0x0004350F File Offset: 0x0004170F
		public new bool Start()
		{
			this.Trace("Starting", new object[0]);
			base.Start();
			this.Trace("Started", new object[0]);
			return true;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0004353C File Offset: 0x0004173C
		protected override void StopInternal()
		{
			this.Trace("Stopping", new object[0]);
			base.StopInternal();
			foreach (FailureItemWatcher failureItemWatcher in this.m_watcherMap.Values)
			{
				failureItemWatcher.Stop();
				failureItemWatcher.Dispose();
			}
			this.Trace("Stopped", new object[0]);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000435C4 File Offset: 0x000417C4
		protected override void TimerCallbackInternal()
		{
			ExTraceGlobals.FailureItemTracer.TraceFunction(0L, "[FIM] Entering TimerCallbackInternal()");
			IEnumerable<IADDatabase> databasesOnLocalServer = this.adConfig.GetDatabasesOnLocalServer();
			if (databasesOnLocalServer != null)
			{
				Dictionary<Guid, bool> dictionary = new Dictionary<Guid, bool>();
				foreach (Guid key in this.m_watcherMap.Keys)
				{
					dictionary[key] = false;
				}
				foreach (IADDatabase iaddatabase in databasesOnLocalServer)
				{
					Guid guid = iaddatabase.Guid;
					FailureItemWatcher value;
					if (this.m_watcherMap.TryGetValue(guid, out value))
					{
						dictionary[guid] = true;
					}
					else
					{
						value = new FailureItemWatcher(iaddatabase);
						this.m_watcherMap[guid] = value;
						dictionary[guid] = true;
					}
				}
				foreach (Guid guid2 in dictionary.Keys)
				{
					if (!dictionary[guid2])
					{
						this.Trace("Removing database entry for {0} since it no more has copies on this server", new object[]
						{
							guid2
						});
						FailureItemWatcher failureItemWatcher = this.m_watcherMap[guid2];
						failureItemWatcher.Stop(true);
						failureItemWatcher.Dispose();
						this.m_watcherMap.Remove(guid2);
					}
				}
				foreach (Guid key2 in this.m_watcherMap.Keys)
				{
					if (base.PrepareToStopCalled)
					{
						this.Trace("Skipping to start the failure item watchers since failure item manager is stopping", new object[0]);
						break;
					}
					this.m_watcherMap[key2].Start();
				}
			}
			ExTraceGlobals.FailureItemTracer.TraceFunction(0L, "[FIM] Exiting TimerCallbackInternal()");
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000437D8 File Offset: 0x000419D8
		private void Trace(string formatString, params object[] args)
		{
			if (ExTraceGlobals.FailureItemTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				string formatString2 = "[FIM] " + formatString;
				ExTraceGlobals.FailureItemTracer.TraceDebug(0L, formatString2, args);
			}
		}

		// Token: 0x0400066C RID: 1644
		private Dictionary<Guid, FailureItemWatcher> m_watcherMap = new Dictionary<Guid, FailureItemWatcher>();
	}
}
