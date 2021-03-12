using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200014F RID: 335
	internal abstract class WcfClientWithFaultHandling<TChannel, TFault> : WcfClientBase<TChannel> where TChannel : class where TFault : FaultException
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00019E53 File Offset: 0x00018053
		public WcfClientWithFaultHandling(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00019E5C File Offset: 0x0001805C
		public WcfClientWithFaultHandling(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x00019E66 File Offset: 0x00018066
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x00019E6E File Offset: 0x0001806E
		public VersionInformation ServerVersion { get; protected set; }

		// Token: 0x06000BAD RID: 2989
		protected abstract void HandleFaultException(TFault fault, string context);

		// Token: 0x06000BAE RID: 2990 RVA: 0x00019EC4 File Offset: 0x000180C4
		protected override void CallService(Action serviceCall, string context)
		{
			string serverNameVersion = context;
			if (this.ServerVersion != null)
			{
				serverNameVersion = string.Format("{0} {1} ({2})", context, this.ServerVersion.ComputerName, this.ServerVersion.ToString());
			}
			base.CallService(delegate
			{
				try
				{
					serviceCall();
				}
				catch (TFault tfault)
				{
					TFault fault = (TFault)((object)tfault);
					this.HandleFaultException(fault, serverNameVersion);
				}
			}, serverNameVersion);
		}
	}
}
