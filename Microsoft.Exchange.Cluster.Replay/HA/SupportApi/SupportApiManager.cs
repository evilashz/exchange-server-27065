using System;
using System.Runtime.CompilerServices;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.HA.SupportApi
{
	// Token: 0x0200037F RID: 895
	internal class SupportApiManager : IServiceComponent
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060023EF RID: 9199 RVA: 0x000A804E File Offset: 0x000A624E
		public static SupportApiManager Instance
		{
			get
			{
				return SupportApiManager.s_manager;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060023F0 RID: 9200 RVA: 0x000A8055 File Offset: 0x000A6255
		public string Name
		{
			get
			{
				return "SupportApi";
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060023F1 RID: 9201 RVA: 0x000A805C File Offset: 0x000A625C
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.SupportApi;
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060023F2 RID: 9202 RVA: 0x000A8060 File Offset: 0x000A6260
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060023F3 RID: 9203 RVA: 0x000A8063 File Offset: 0x000A6263
		public bool IsEnabled
		{
			get
			{
				return RegistryParameters.EnableSupportApi;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060023F4 RID: 9204 RVA: 0x000A806A File Offset: 0x000A626A
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060023F5 RID: 9205 RVA: 0x000A806D File Offset: 0x000A626D
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x060023F6 RID: 9206 RVA: 0x000A8078 File Offset: 0x000A6278
		public bool Start()
		{
			if (this.m_service == null)
			{
				Exception ex;
				this.m_service = SupportApiService.StartListening(out ex);
			}
			return this.m_service != null;
		}

		// Token: 0x060023F7 RID: 9207 RVA: 0x000A80A8 File Offset: 0x000A62A8
		public void Stop()
		{
			lock (this)
			{
				if (this.m_service != null)
				{
					this.m_service.StopListening();
					this.m_service = null;
				}
			}
		}

		// Token: 0x04000F52 RID: 3922
		private static SupportApiManager s_manager = new SupportApiManager();

		// Token: 0x04000F53 RID: 3923
		private SupportApiService m_service;
	}
}
