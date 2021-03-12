using System;
using System.Collections.Generic;
using System.Net.Security;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.Diagnostics.Components.MailboxReplicationService;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000770 RID: 1904
	internal sealed class MailboxReplicationServiceClientSlim : WcfClientBase<IMailboxReplicationServiceSlim>
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x0004F418 File Offset: 0x0004D618
		private MailboxReplicationServiceClientSlim(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0004F424 File Offset: 0x0004D624
		public static void NotifyToSync(SyncNowNotificationFlags flags, Guid mailboxGuid, Guid mdbGuid)
		{
			try
			{
				using (MailboxReplicationServiceClientSlim mailboxReplicationServiceClientSlim = MailboxReplicationServiceClientSlim.Create())
				{
					SyncNowNotification item = new SyncNowNotification
					{
						MailboxGuid = mailboxGuid,
						MdbGuid = mdbGuid,
						Flags = (int)flags
					};
					mailboxReplicationServiceClientSlim.SyncNow(new List<SyncNowNotification>
					{
						item
					});
				}
			}
			catch (EndpointNotFoundTransientException arg)
			{
				ExTraceGlobals.MailboxReplicationCommonTracer.TraceWarning<EndpointNotFoundTransientException>(0L, "MRSClientSlim: MRS service is down: {0}", arg);
			}
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x0004F4AC File Offset: 0x0004D6AC
		internal static MailboxReplicationServiceClientSlim Create()
		{
			ExTraceGlobals.MailboxReplicationCommonTracer.TraceDebug<string>(0L, "MRSClientSlim: attempting to connect to local server '{0}'", ComputerInformation.DnsFullyQualifiedDomainName);
			string text = "net.pipe://localhost/Microsoft.Exchange.MailboxReplicationService";
			NetNamedPipeBinding netNamedPipeBinding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
			netNamedPipeBinding.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;
			EndpointAddress remoteAddress = new EndpointAddress(text);
			MailboxReplicationServiceClientSlim result = new MailboxReplicationServiceClientSlim(netNamedPipeBinding, remoteAddress);
			ExTraceGlobals.MailboxReplicationCommonTracer.TraceDebug<string>(0L, "MRSClientSlim: connected to '{0}'", text);
			return result;
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x0004F530 File Offset: 0x0004D730
		internal void SyncNow(List<SyncNowNotification> notifications)
		{
			this.CallService(delegate()
			{
				this.Channel.SyncNow(notifications);
			});
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x0004F564 File Offset: 0x0004D764
		private void CallService(Action serviceCall)
		{
			ExTraceGlobals.MailboxReplicationCommonTracer.TraceDebug<string>(0L, "MRSClientSlim: attempting to call: {0}", serviceCall.Method.Name);
			this.CallService(serviceCall, base.Endpoint.Address.ToString());
			ExTraceGlobals.MailboxReplicationCommonTracer.TraceDebug<string>(0L, "MRSClientSlim: completed the call: {0}", serviceCall.Method.Name);
		}
	}
}
