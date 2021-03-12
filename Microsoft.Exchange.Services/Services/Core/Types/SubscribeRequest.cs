using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000484 RID: 1156
	[KnownType(typeof(PushSubscriptionRequest))]
	[KnownType(typeof(PullSubscriptionRequest))]
	[KnownType(typeof(StreamingSubscriptionRequest))]
	[XmlType("SubscribeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeRequest : BaseRequest
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000A2E7E File Offset: 0x000A107E
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x000A2E86 File Offset: 0x000A1086
		[XmlElement("PullSubscriptionRequest", Type = typeof(PullSubscriptionRequest), Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "SubscriptionRequest", IsRequired = true)]
		[XmlElement("PushSubscriptionRequest", Type = typeof(PushSubscriptionRequest), Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlElement("StreamingSubscriptionRequest", Type = typeof(StreamingSubscriptionRequest), Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public SubscriptionRequestBase SubscriptionRequest { get; set; }

		// Token: 0x0600224E RID: 8782 RVA: 0x000A2E90 File Offset: 0x000A1090
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			if (this.SubscriptionRequest is PullSubscriptionRequest)
			{
				RequestDetailsLogger.Current.AppendGenericInfo("SubscriptionType", "Pull");
				return new SubscribeForPull(callContext, this);
			}
			if (this.SubscriptionRequest is PushSubscriptionRequest)
			{
				RequestDetailsLogger.Current.AppendGenericInfo("SubscriptionType", "Push");
				return new SubscribeForPush(callContext, this);
			}
			if (this.SubscriptionRequest is StreamingSubscriptionRequest)
			{
				RequestDetailsLogger.Current.AppendGenericInfo("SubscriptionType", "Stream");
				return new SubscribeForStreaming(callContext, this);
			}
			throw new InvalidRequestException();
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000A2F20 File Offset: 0x000A1120
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			if (this.SubscriptionRequest == null)
			{
				return null;
			}
			BaseServerIdInfo baseServerIdInfo = null;
			if (this.SubscriptionRequest.SubscribeToAllFolders)
			{
				baseServerIdInfo = callContext.GetServerInfoForEffectiveCaller();
			}
			else if (this.SubscriptionRequest.FolderIds != null && this.SubscriptionRequest.FolderIds.Length > 0)
			{
				baseServerIdInfo = BaseRequest.GetServerInfoForFolderIdList(callContext, this.SubscriptionRequest.FolderIds);
			}
			if (this.SubscriptionRequest is StreamingSubscriptionRequest && baseServerIdInfo != null)
			{
				string siteIdForServer = SingleProxyDeterministicCASBoxScoring.GetSiteIdForServer(baseServerIdInfo.ServerFQDN);
				if (siteIdForServer == SubscribeRequest.LocalSiteId)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[SubscribeRequest::GetProxyInfo] Enforce no proxy when creating streaming subscription for the same-site mailboxthe intended server is '{0}', site is '{1}'", baseServerIdInfo.ServerFQDN, siteIdForServer);
					if (!baseServerIdInfo.IsLocalServer)
					{
						if (!this.proxyInfoLogged)
						{
							RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(callContext.ProtocolLog, "CrossServerXso", baseServerIdInfo.ServerFQDN);
							this.proxyInfoLogged = true;
						}
						baseServerIdInfo = null;
					}
				}
				else if (!this.proxyInfoLogged)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(callContext.ProtocolLog, "IntendedCrossSite", baseServerIdInfo.ServerFQDN);
					this.proxyInfoLogged = true;
				}
			}
			return baseServerIdInfo;
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000A3021 File Offset: 0x000A1221
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x000A302C File Offset: 0x000A122C
		internal override ProxyServiceTask<T> CreateProxyServiceTask<T>(ServiceAsyncResult<T> serviceAsyncResult, CallContext callContext, WebServicesInfo[] services)
		{
			if (this.SubscriptionRequest is StreamingSubscriptionRequest)
			{
				bool flag = callContext.IsPartnerUser || SubscribeRequest.HasPreferServerAffinityHeader(callContext);
				if (flag)
				{
					ExTraceGlobals.ProxyEvaluatorTracer.TraceDebug((long)this.GetHashCode(), "[SubscribeRequest::CreateProxyServiceTask] Do not proxy streaming subscription requests to the remote site for calls with PreferServerAffinity header.");
					throw FaultExceptionUtilities.CreateFault(new ProxyRequestNotAllowedException(), FaultParty.Sender);
				}
			}
			return base.CreateProxyServiceTask<T>(serviceAsyncResult, callContext, services);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x000A3086 File Offset: 0x000A1286
		internal static bool HasPreferServerAffinityHeader(CallContext callContext)
		{
			return callContext.HttpContext != null && callContext.HttpContext.Request != null && !string.IsNullOrEmpty(callContext.HttpContext.Request.Headers["X-PreferServerAffinity"]);
		}

		// Token: 0x040014E5 RID: 5349
		private const string PreferServerAffinityHeader = "X-PreferServerAffinity";

		// Token: 0x040014E6 RID: 5350
		public static readonly string BEServerRoutingErrorHeaderName = "X-BEServerRoutingError";

		// Token: 0x040014E7 RID: 5351
		private static readonly string LocalSiteId = SingleProxyDeterministicCASBoxScoring.GetSiteIdForServer(BaseServerIdInfo.LocalServerFQDN);

		// Token: 0x040014E8 RID: 5352
		private bool proxyInfoLogged;
	}
}
