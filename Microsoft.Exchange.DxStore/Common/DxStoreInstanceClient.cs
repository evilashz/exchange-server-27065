using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000056 RID: 86
	public class DxStoreInstanceClient : IDxStoreClient<IDxStoreInstance>
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000791C File Offset: 0x00005B1C
		public DxStoreInstanceClient(CachedChannelFactory<IDxStoreInstance> channelFactory, TimeSpan? operationTimeout = null)
		{
			this.Initialize(channelFactory, operationTimeout);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000792C File Offset: 0x00005B2C
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00007933 File Offset: 0x00005B33
		public static DxStoreInstanceExceptionTranslator Runner { get; set; } = new DxStoreInstanceExceptionTranslator();

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000793B File Offset: 0x00005B3B
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00007943 File Offset: 0x00005B43
		public TimeSpan? OperationTimeout { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000794C File Offset: 0x00005B4C
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00007954 File Offset: 0x00005B54
		public CachedChannelFactory<IDxStoreInstance> ChannelFactory { get; set; }

		// Token: 0x06000342 RID: 834 RVA: 0x0000795D File Offset: 0x00005B5D
		public void Initialize(CachedChannelFactory<IDxStoreInstance> channelFactory, TimeSpan? operationTimeout = null)
		{
			this.ChannelFactory = channelFactory;
			this.OperationTimeout = operationTimeout;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00007984 File Offset: 0x00005B84
		public void Stop(bool isFlush, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreInstance service)
			{
				service.Stop(isFlush);
			});
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000079E4 File Offset: 0x00005BE4
		public void Flush(TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreInstance service)
			{
				service.Flush();
			});
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00007A54 File Offset: 0x00005C54
		public void Reconfigure(InstanceGroupMemberConfig[] members, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreInstance service)
			{
				service.Reconfigure(members);
			});
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public InstanceStatusInfo GetStatus(TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<InstanceStatusInfo>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreInstance service) => service.GetStatus());
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00007B2C File Offset: 0x00005D2C
		public InstanceSnapshotInfo AcquireSnapshot(string fullKeyName = null, bool isCompress = true, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			return runner.Execute<InstanceSnapshotInfo>(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, (IDxStoreInstance service) => service.AcquireSnapshot(fullKeyName, isCompress));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public void ApplySnapshot(InstanceSnapshotInfo snapshot, bool isForce = false, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreInstance service)
			{
				service.ApplySnapshot(snapshot, isForce);
			});
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00007C08 File Offset: 0x00005E08
		public void TryBecomeLeader(TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreInstance service)
			{
				service.TryBecomeLeader();
			});
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00007C90 File Offset: 0x00005E90
		public void NotifyInitiator(Guid commandId, string sender, int instanceNumber, bool isSucceeded, string errorMessage, TimeSpan? timeout = null)
		{
			WcfExceptionTranslator<IDxStoreInstance> runner = DxStoreInstanceClient.Runner;
			CachedChannelFactory<IDxStoreInstance> channelFactory = this.ChannelFactory;
			TimeSpan? timeSpan = timeout;
			runner.Execute(channelFactory, (timeSpan != null) ? new TimeSpan?(timeSpan.GetValueOrDefault()) : this.OperationTimeout, delegate(IDxStoreInstance service)
			{
				service.NotifyInitiator(commandId, sender, instanceNumber, isSucceeded, errorMessage);
			});
		}
	}
}
