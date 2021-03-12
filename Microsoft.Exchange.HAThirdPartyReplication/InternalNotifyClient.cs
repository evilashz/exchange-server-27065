using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x02000009 RID: 9
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	public class InternalNotifyClient : ClientBase<IInternalNotify>, IInternalNotify
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000026D0 File Offset: 0x000008D0
		public InternalNotifyClient()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026D8 File Offset: 0x000008D8
		public InternalNotifyClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026E1 File Offset: 0x000008E1
		public InternalNotifyClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026EB File Offset: 0x000008EB
		public InternalNotifyClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026F5 File Offset: 0x000008F5
		public InternalNotifyClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026FF File Offset: 0x000008FF
		public void BecomePame()
		{
			base.Channel.BecomePame();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000270C File Offset: 0x0000090C
		public void RevokePame()
		{
			base.Channel.RevokePame();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002719 File Offset: 0x00000919
		public NotificationResponse DatabaseMoveNeeded(Guid databaseId, string currentActiveFqdn, bool mountDesired)
		{
			return base.Channel.DatabaseMoveNeeded(databaseId, currentActiveFqdn, mountDesired);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002729 File Offset: 0x00000929
		public int GetTimeouts(out TimeSpan retryDelay, out TimeSpan openTimeout, out TimeSpan sendTimeout, out TimeSpan receiveTimeout)
		{
			return base.Channel.GetTimeouts(out retryDelay, out openTimeout, out sendTimeout, out receiveTimeout);
		}
	}
}
