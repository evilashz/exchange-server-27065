using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000064 RID: 100
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmRpcServerWrapper : IServiceComponent
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x00016E1C File Offset: 0x0001501C
		public AmRpcServerWrapper(ActiveManagerCore amInstance)
		{
			this.m_amInstance = amInstance;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00016E2B File Offset: 0x0001502B
		public string Name
		{
			get
			{
				return "Active Manager RPC Server";
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00016E32 File Offset: 0x00015032
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.ActiveManagerRpcServer;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00016E36 File Offset: 0x00015036
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00016E39 File Offset: 0x00015039
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00016E3C File Offset: 0x0001503C
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00016E3F File Offset: 0x0001503F
		public bool Start()
		{
			return AmRpcServer.TryStart(this.m_amInstance);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00016E4C File Offset: 0x0001504C
		public void Stop()
		{
			AmRpcServer.Stop();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00016E53 File Offset: 0x00015053
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x040001E3 RID: 483
		private ActiveManagerCore m_amInstance;
	}
}
