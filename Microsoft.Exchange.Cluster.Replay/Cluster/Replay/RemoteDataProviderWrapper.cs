using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000378 RID: 888
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RemoteDataProviderWrapper : IServiceComponent
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x000A7698 File Offset: 0x000A5898
		public string Name
		{
			get
			{
				return "Tcp Listener";
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x000A769F File Offset: 0x000A589F
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.TcpListener;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x000A76A2 File Offset: 0x000A58A2
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x000A76A5 File Offset: 0x000A58A5
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x000A76A8 File Offset: 0x000A58A8
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x000A76AB File Offset: 0x000A58AB
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x000A76B3 File Offset: 0x000A58B3
		public bool Start()
		{
			ExTraceGlobals.MonitoredDatabaseTracer.TraceError(0L, "Start RemoteDatabaseProviderWrapper");
			NetworkManager.Start();
			return RemoteDataProvider.StartListening(true);
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000A76D1 File Offset: 0x000A58D1
		public void Stop()
		{
			RemoteDataProvider.StopMonitoring();
			NetworkManager.Shutdown();
		}
	}
}
