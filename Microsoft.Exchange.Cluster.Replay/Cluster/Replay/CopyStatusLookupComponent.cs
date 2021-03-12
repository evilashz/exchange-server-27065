using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001E8 RID: 488
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CopyStatusLookupComponent : IServiceComponent
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0004EB54 File Offset: 0x0004CD54
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004EB5C File Offset: 0x0004CD5C
		internal CopyStatusLookupComponent()
		{
			CopyStatusClientLookupTable statusTable = null;
			IReplayAdObjectLookup replayAdObjectLookup = Dependencies.ReplayAdObjectLookup;
			this.ActiveManagerInstance = ActiveManager.CreateCustomActiveManager(true, replayAdObjectLookup.DagLookup, replayAdObjectLookup.ServerLookup, replayAdObjectLookup.MiniServerLookup, null, null, replayAdObjectLookup.DatabaseLookup, replayAdObjectLookup.AdSession, true);
			if (CopyStatusLookupComponent.CopyStatusClientCachingEnabled)
			{
				statusTable = new CopyStatusClientLookupTable();
				this.CopyStatusPoller = new CopyStatusPoller(Dependencies.MonitoringADConfigProvider, statusTable, this.ActiveManagerInstance);
			}
			this.CopyStatusLookup = new CopyStatusClientLookup(statusTable, this.CopyStatusPoller, this.ActiveManagerInstance);
			Dependencies.Container.RegisterInstance<ICopyStatusClientLookup>(this.CopyStatusLookup);
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004EBF1 File Offset: 0x0004CDF1
		// (set) Token: 0x06001361 RID: 4961 RVA: 0x0004EBF9 File Offset: 0x0004CDF9
		public ActiveManager ActiveManagerInstance { get; private set; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0004EC02 File Offset: 0x0004CE02
		// (set) Token: 0x06001363 RID: 4963 RVA: 0x0004EC0A File Offset: 0x0004CE0A
		public CopyStatusPoller CopyStatusPoller { get; private set; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004EC13 File Offset: 0x0004CE13
		// (set) Token: 0x06001365 RID: 4965 RVA: 0x0004EC1B File Offset: 0x0004CE1B
		public CopyStatusClientLookup CopyStatusLookup { get; private set; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0004EC24 File Offset: 0x0004CE24
		public string Name
		{
			get
			{
				return "Copy Status Lookup Component";
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0004EC2B File Offset: 0x0004CE2B
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.CopyStatusLookup;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0004EC2F File Offset: 0x0004CE2F
		public bool IsCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004EC32 File Offset: 0x0004CE32
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0004EC35 File Offset: 0x0004CE35
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004EC38 File Offset: 0x0004CE38
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0004EC40 File Offset: 0x0004CE40
		public bool Start()
		{
			if (this.CopyStatusPoller != null)
			{
				this.CopyStatusPoller.Start();
			}
			return true;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004EC56 File Offset: 0x0004CE56
		public void Stop()
		{
			if (this.CopyStatusPoller != null)
			{
				this.CopyStatusPoller.Stop();
			}
			if (this.ActiveManagerInstance != null)
			{
				this.ActiveManagerInstance.Dispose();
				this.ActiveManagerInstance = null;
			}
		}

		// Token: 0x04000776 RID: 1910
		internal static bool CopyStatusClientCachingEnabled = !RegistryParameters.CopyStatusClientCachingDisabled;
	}
}
