using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200000C RID: 12
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "3.0.0.0")]
	public class InternalRequestClient : ClientBase<IInternalRequest>, IInternalRequest
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000273B File Offset: 0x0000093B
		public InternalRequestClient()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002743 File Offset: 0x00000943
		public InternalRequestClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000274C File Offset: 0x0000094C
		public InternalRequestClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002756 File Offset: 0x00000956
		public InternalRequestClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002760 File Offset: 0x00000960
		public InternalRequestClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000276A File Offset: 0x0000096A
		public string GetPrimaryActiveManager(out byte[] ex)
		{
			return base.Channel.GetPrimaryActiveManager(out ex);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002778 File Offset: 0x00000978
		public byte[] ChangeActiveServer(Guid databaseId, string newActiveServerName)
		{
			return base.Channel.ChangeActiveServer(databaseId, newActiveServerName);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002787 File Offset: 0x00000987
		public byte[] ImmediateDismountMailboxDatabase(Guid databaseId)
		{
			return base.Channel.ImmediateDismountMailboxDatabase(databaseId);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002795 File Offset: 0x00000995
		public void AmeIsStarting(TimeSpan retryDelay, TimeSpan openTimeout, TimeSpan sendTimeout, TimeSpan receiveTimeout)
		{
			base.Channel.AmeIsStarting(retryDelay, openTimeout, sendTimeout, receiveTimeout);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027A7 File Offset: 0x000009A7
		public void AmeIsStopping()
		{
			base.Channel.AmeIsStopping();
		}
	}
}
