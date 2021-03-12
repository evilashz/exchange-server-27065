using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring.Client
{
	// Token: 0x020001D5 RID: 469
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	internal class InternalMonitoringServiceClient : ClientBase<InternalMonitoringService>, InternalMonitoringService
	{
		// Token: 0x060012B6 RID: 4790 RVA: 0x0004BD00 File Offset: 0x00049F00
		public InternalMonitoringServiceClient()
		{
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0004BD08 File Offset: 0x00049F08
		public InternalMonitoringServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004BD11 File Offset: 0x00049F11
		public InternalMonitoringServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004BD1B File Offset: 0x00049F1B
		public InternalMonitoringServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004BD25 File Offset: 0x00049F25
		public InternalMonitoringServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0004BD2F File Offset: 0x00049F2F
		public ServiceVersion GetVersion()
		{
			return base.Channel.GetVersion();
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004BD3C File Offset: 0x00049F3C
		public Task<ServiceVersion> GetVersionAsync()
		{
			return base.Channel.GetVersionAsync();
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0004BD49 File Offset: 0x00049F49
		public void PublishDagHealthInfo(HealthInfoPersisted healthInfo)
		{
			base.Channel.PublishDagHealthInfo(healthInfo);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0004BD57 File Offset: 0x00049F57
		public Task PublishDagHealthInfoAsync(HealthInfoPersisted healthInfo)
		{
			return base.Channel.PublishDagHealthInfoAsync(healthInfo);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0004BD65 File Offset: 0x00049F65
		public DateTime GetDagHealthInfoUpdateTimeUtc()
		{
			return base.Channel.GetDagHealthInfoUpdateTimeUtc();
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004BD72 File Offset: 0x00049F72
		public Task<DateTime> GetDagHealthInfoUpdateTimeUtcAsync()
		{
			return base.Channel.GetDagHealthInfoUpdateTimeUtcAsync();
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0004BD7F File Offset: 0x00049F7F
		public HealthInfoPersisted GetDagHealthInfo()
		{
			return base.Channel.GetDagHealthInfo();
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0004BD8C File Offset: 0x00049F8C
		public Task<HealthInfoPersisted> GetDagHealthInfoAsync()
		{
			return base.Channel.GetDagHealthInfoAsync();
		}
	}
}
