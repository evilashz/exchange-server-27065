using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200005A RID: 90
	public class DxStoreManagerClient : IDxStoreClient<IDxStoreManager>
	{
		// Token: 0x06000381 RID: 897 RVA: 0x00008CFF File Offset: 0x00006EFF
		public DxStoreManagerClient(CachedChannelFactory<IDxStoreManager> channelFactory, TimeSpan? operationTimeout = null)
		{
			this.Initialize(channelFactory, operationTimeout);
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00008D0F File Offset: 0x00006F0F
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00008D16 File Offset: 0x00006F16
		public static DxStoreManagerExceptionTranslator Runner { get; set; } = new DxStoreManagerExceptionTranslator();

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00008D1E File Offset: 0x00006F1E
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00008D26 File Offset: 0x00006F26
		public TimeSpan? OperationTimeout { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00008D2F File Offset: 0x00006F2F
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00008D37 File Offset: 0x00006F37
		public CachedChannelFactory<IDxStoreManager> ChannelFactory { get; set; }

		// Token: 0x06000388 RID: 904 RVA: 0x00008D40 File Offset: 0x00006F40
		public void Initialize(CachedChannelFactory<IDxStoreManager> channelFactory, TimeSpan? operationTimeout = null)
		{
			this.ChannelFactory = channelFactory;
			this.OperationTimeout = operationTimeout;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008D6C File Offset: 0x00006F6C
		public void StartInstance(string groupName, bool isForce = false, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreManager> runner = DxStoreManagerClient.Runner;
			CachedChannelFactory<IDxStoreManager> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreManager service)
			{
				service.StartInstance(groupName, isForce);
			});
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00008DE4 File Offset: 0x00006FE4
		public void RestartInstance(string groupName, bool isForce = false, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreManager> runner = DxStoreManagerClient.Runner;
			CachedChannelFactory<IDxStoreManager> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreManager service)
			{
				service.RestartInstance(groupName, isForce);
			});
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00008E58 File Offset: 0x00007058
		public void RemoveInstance(string groupName, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreManager> runner = DxStoreManagerClient.Runner;
			CachedChannelFactory<IDxStoreManager> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreManager service)
			{
				service.RemoveInstance(groupName);
			});
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00008EC4 File Offset: 0x000070C4
		public void StopInstance(string groupName, bool isDisable = true, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreManager> runner = DxStoreManagerClient.Runner;
			CachedChannelFactory<IDxStoreManager> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreManager service)
			{
				service.StopInstance(groupName, true);
			});
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00008F30 File Offset: 0x00007130
		public InstanceGroupConfig GetInstanceConfig(string groupName, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreManager> runner = DxStoreManagerClient.Runner;
			CachedChannelFactory<IDxStoreManager> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<InstanceGroupConfig>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreManager service) => service.GetInstanceConfig(groupName, false));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00008FA4 File Offset: 0x000071A4
		public void TriggerRefresh(string reason, bool isForceRefreshCache, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreManager> runner = DxStoreManagerClient.Runner;
			CachedChannelFactory<IDxStoreManager> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreManager service)
			{
				service.TriggerRefresh(reason, isForceRefreshCache);
			});
		}
	}
}
