using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.DxStore.Common;
using Microsoft.Exchange.DxStore.HA.Events;
using Microsoft.Exchange.DxStore.Server;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x0200016E RID: 366
	internal class DistributedStoreManagerComponent : IServiceComponent
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0003F2E3 File Offset: 0x0003D4E3
		public string Name
		{
			get
			{
				return "Distributed Store Manager";
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0003F2EA File Offset: 0x0003D4EA
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.DistributedStoreManager;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x0003F2EE File Offset: 0x0003D4EE
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0003F2F1 File Offset: 0x0003D4F1
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0003F2F4 File Offset: 0x0003D4F4
		public bool IsRetriableOnError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0003F2F7 File Offset: 0x0003D4F7
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		public DxStoreManager Manager { get; set; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0003F308 File Offset: 0x0003D508
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0003F310 File Offset: 0x0003D510
		public DistributedStoreTopologyProvider ConfigProvider { get; set; }

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0003F319 File Offset: 0x0003D519
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0003F321 File Offset: 0x0003D521
		public DistributedStoreEventLogger EventLogger { get; set; }

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003F36C File Offset: 0x0003D56C
		public bool Start()
		{
			Task.Factory.StartNew(delegate()
			{
				Exception ex = Utils.RunBestEffort(delegate
				{
					this.StartInternal();
				});
				if (ex != null)
				{
					DxStoreHACrimsonEvents.FailedToStartDxStore.Log<string, string>(ex.Message, ex.ToString());
				}
			});
			return true;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003F388 File Offset: 0x0003D588
		public void Stop()
		{
			lock (this.locker)
			{
				if (this.Manager != null)
				{
					this.Manager.Stop(null);
				}
				DistributedStore.Instance.StopProcessRestartTimer();
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003F3E8 File Offset: 0x0003D5E8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003F3F0 File Offset: 0x0003D5F0
		private void StartInternal()
		{
			lock (this.locker)
			{
				DistributedStore.Instance.StartProcessRestartTimer();
				IActiveManagerSettings settings = DxStoreSetting.Instance.GetSettings();
				if (settings.DxStoreRunMode != DxStoreMode.Disabled)
				{
					this.EventLogger = new DistributedStoreEventLogger(false);
					this.ConfigProvider = new DistributedStoreTopologyProvider(this.EventLogger, null, false);
					this.Manager = new DxStoreManager(this.ConfigProvider, this.EventLogger);
					this.Manager.Start();
				}
			}
		}

		// Token: 0x04000613 RID: 1555
		private object locker = new object();
	}
}
