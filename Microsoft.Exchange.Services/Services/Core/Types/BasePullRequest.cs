using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003FD RID: 1021
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public abstract class BasePullRequest : BaseRequest
	{
		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x0009EC02 File Offset: 0x0009CE02
		// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0009EC0A File Offset: 0x0009CE0A
		[DataMember(IsRequired = true)]
		[XmlElement]
		public string SubscriptionId { get; set; }

		// Token: 0x06001CF4 RID: 7412 RVA: 0x0009EC14 File Offset: 0x0009CE14
		internal static SubscriptionId ParseSubscriptionId(string subscriptionId)
		{
			SubscriptionId result;
			try
			{
				result = Microsoft.Exchange.Services.Core.Types.SubscriptionId.Parse(subscriptionId);
			}
			catch (InvalidSubscriptionException)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string>(0L, "[BasePullRequest::ParseSubscriptionId] Failed to parse subscription id string: '{0}'", subscriptionId ?? "<NULL>");
				result = null;
			}
			return result;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x0009EC5C File Offset: 0x0009CE5C
		internal static WebServicesInfo[] PerformServiceDiscoveryForSubscriptionId(string subscriptionId, CallContext callContext, BaseRequest request)
		{
			WebServicesInfo proxyToSelfCASIfNeeded = request.GetProxyToSelfCASIfNeeded();
			if (proxyToSelfCASIfNeeded != null)
			{
				return new WebServicesInfo[]
				{
					proxyToSelfCASIfNeeded
				};
			}
			SubscriptionId subscriptionId2 = BasePullRequest.ParseSubscriptionId(subscriptionId);
			if (subscriptionId2 == null)
			{
				return null;
			}
			if (subscriptionId2.MailboxGuid != Guid.Empty)
			{
				MailboxIdServerInfo mailboxIdServerInfo = MailboxIdServerInfo.Create(new MailboxId(subscriptionId2.MailboxGuid));
				if (mailboxIdServerInfo != null && !string.IsNullOrEmpty(mailboxIdServerInfo.ServerFQDN) && !string.Equals(mailboxIdServerInfo.ServerFQDN, subscriptionId2.ServerFQDN, StringComparison.OrdinalIgnoreCase))
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(callContext.ProtocolLog, "SubscribedMailboxFailedOver", subscriptionId2.MailboxGuid);
					throw new SubscriptionNotFoundException();
				}
			}
			if (string.Compare(LocalServer.GetServer().Fqdn, subscriptionId2.ServerFQDN, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return null;
			}
			WebServicesInfo casserviceForServer = SingleProxyDeterministicCASBoxScoring.GetCASServiceForServer(subscriptionId2.ServerFQDN);
			if (casserviceForServer == null)
			{
				ExTraceGlobals.ProxyEvaluatorTracer.TraceError<string>((long)request.GetHashCode(), "[NotificationCommandBase::EvaluateSubscriptionProxy] Tried to get WebServicesInfo instance for FQDN {0}, but failed.", subscriptionId2.ServerFQDN);
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(callContext.ProtocolLog, "ServerInSubscriptionId", subscriptionId2.ServerFQDN);
				ProxyEventLogHelper.LogNoApplicableDestinationCAS(subscriptionId2.ServerFQDN);
				throw new SubscriptionNotFoundException();
			}
			return new WebServicesInfo[]
			{
				casserviceForServer
			};
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x0009ED72 File Offset: 0x0009CF72
		internal override WebServicesInfo[] PerformServiceDiscovery(CallContext callContext)
		{
			return BasePullRequest.PerformServiceDiscoveryForSubscriptionId(this.SubscriptionId, callContext, this);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x0009ED81 File Offset: 0x0009CF81
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0009ED84 File Offset: 0x0009CF84
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return null;
		}
	}
}
