using System;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Services.Common;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000F9 RID: 249
	internal sealed class FreeBusyApplication : Application
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0001D2AE File Offset: 0x0001B4AE
		public FreeBusyApplication(ClientContext clientContext, FreeBusyViewOptions freeBusyView, bool defaultFreeBusyOnly, QueryType queryType) : base(true)
		{
			this.freeBusyView = freeBusyView;
			this.clientContext = clientContext;
			this.defaultFreeBusyOnly = defaultFreeBusyOnly;
			this.queryType = queryType;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001D2D4 File Offset: 0x0001B4D4
		public static FreeBusyQuery[] ConvertBaseToFreeBusyQuery(BaseQuery[] baseQueries)
		{
			FreeBusyQuery[] array = new FreeBusyQuery[baseQueries.Length];
			for (int i = 0; i < baseQueries.Length; i++)
			{
				array[i] = (FreeBusyQuery)baseQueries[i];
			}
			return array;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001D304 File Offset: 0x0001B504
		public override IService CreateService(WebServiceUri webServiceUri, TargetServerVersion targetVersion, RequestType requestType)
		{
			Service service = new Service(webServiceUri);
			if (targetVersion >= TargetServerVersion.E14R3OrLater || targetVersion == TargetServerVersion.Unknown)
			{
				this.SetTimeZoneDefinitionHeader(service);
			}
			return service;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001D328 File Offset: 0x0001B528
		public override IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxArray, AsyncCallback callback, object asyncState)
		{
			return service.BeginGetUserAvailability(new GetUserAvailabilityRequest
			{
				MailboxDataArray = mailboxArray,
				FreeBusyViewOptions = this.freeBusyView,
				TimeZone = new SerializableTimeZone(this.clientContext.TimeZone)
			}, callback, null);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001D370 File Offset: 0x0001B570
		public override void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult)
		{
			GetUserAvailabilityResponse getUserAvailabilityResponse = service.EndGetUserAvailability(asyncResult);
			FreeBusyResponse[] array = null;
			if (getUserAvailabilityResponse != null)
			{
				array = getUserAvailabilityResponse.FreeBusyResponseArray;
			}
			if (array == null)
			{
				Application.ProxyWebRequestTracer.TraceError((long)proxyWebRequest.GetHashCode(), "{0}: Proxy web request returned NULL FreeBusyResponseArray.", new object[]
				{
					TraceContext.Get()
				});
			}
			for (int i = 0; i < queryList.Count; i++)
			{
				FreeBusyQuery freeBusyQuery = (FreeBusyQuery)queryList[i];
				FreeBusyResponse freeBusyResponse = null;
				if (array != null && i < array.Length)
				{
					freeBusyResponse = array[i];
					if (freeBusyResponse == null)
					{
						Application.ProxyWebRequestTracer.TraceDebug<object, EmailAddress>((long)proxyWebRequest.GetHashCode(), "{0}: Proxy web request returned NULL FreeBusyResponse for mailbox {1}.", TraceContext.Get(), freeBusyQuery.Email);
					}
				}
				FreeBusyQueryResult resultOnFirstCall;
				if (freeBusyResponse == null)
				{
					resultOnFirstCall = new FreeBusyQueryResult(new ProxyNoResultException(Strings.descProxyNoResultError(freeBusyQuery.Email.Address, service.Url), 60732U));
				}
				else
				{
					resultOnFirstCall = FreeBusyApplication.CopyViewAndResponseToResult(service.Url, freeBusyResponse.FreeBusyView, freeBusyResponse.ResponseMessage, freeBusyQuery.Email);
				}
				freeBusyQuery.SetResultOnFirstCall(resultOnFirstCall);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001D470 File Offset: 0x0001B670
		public override string GetParameterDataString()
		{
			return string.Format("Parameters: windowStart = {0}, windowEnd = {1}, MergedFBInterval = {2}, RequestedView = {3}", new object[]
			{
				this.freeBusyView.TimeWindow.StartTime,
				this.freeBusyView.TimeWindow.EndTime,
				this.freeBusyView.MergedFreeBusyIntervalInMinutes,
				this.freeBusyView.RequestedView
			});
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001D4E5 File Offset: 0x0001B6E5
		public override LocalQuery CreateLocalQuery(ClientContext clientContext, DateTime requestCompletionDeadline)
		{
			return new CalendarQuery(clientContext, this.freeBusyView, this.defaultFreeBusyOnly, requestCompletionDeadline);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001D4FA File Offset: 0x0001B6FA
		public override BaseQueryResult CreateQueryResult(LocalizedException exception)
		{
			return new FreeBusyQueryResult(exception);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001D502 File Offset: 0x0001B702
		public override BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return FreeBusyQuery.CreateFromUnknown(recipientData, exception);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001D50B File Offset: 0x0001B70B
		public override BaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return FreeBusyQuery.CreateFromIndividual(recipientData);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001D513 File Offset: 0x0001B713
		public override BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return FreeBusyQuery.CreateFromIndividual(recipientData, exception);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001D51C File Offset: 0x0001B71C
		public override AvailabilityException CreateExceptionForUnsupportedVersion(RecipientData recipient, int serverVersion)
		{
			return new E14orHigherProxyServerNotFound((recipient != null) ? recipient.EmailAddress : null, Globals.E14Version);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001D534 File Offset: 0x0001B734
		public override BaseQuery CreateFromGroup(RecipientData recipientData, BaseQuery[] groupMembers, bool groupCapped)
		{
			bool flag = (this.queryType & QueryType.FreeBusy) != (QueryType)0;
			bool flag2 = (this.queryType & QueryType.MeetingSuggestions) != (QueryType)0 && !groupCapped;
			FreeBusyQuery[] groupMembersForFreeBusy = null;
			FreeBusyQuery[] groupMembersForSuggestions = null;
			if (flag)
			{
				if (groupCapped)
				{
					Application.ProxyWebRequestTracer.TraceDebug<object, RecipientData>((long)this.GetHashCode(), "{0}:Not generating requests for members of group {1} because the group is larger than the allowed group size.", TraceContext.Get(), recipientData);
					return FreeBusyQuery.CreateFromGroup(recipientData, new FreeBusyDLLimitReachedException(Configuration.MaximumGroupMemberCount));
				}
				groupMembersForFreeBusy = FreeBusyApplication.ConvertBaseToFreeBusyQuery(groupMembers);
			}
			if (flag2)
			{
				groupMembersForSuggestions = FreeBusyApplication.ConvertBaseToFreeBusyQuery(groupMembers);
			}
			return FreeBusyQuery.CreateFromGroup(recipientData, groupMembersForFreeBusy, groupMembersForSuggestions);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001D5B2 File Offset: 0x0001B7B2
		public override AsyncRequestWithQueryList CreateCrossForestAsyncRequestWithAutoDiscover(ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, TargetForestConfiguration targetForestConfiguration)
		{
			return new ProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, targetForestConfiguration, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestXmlByUser.Create));
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001D5CC File Offset: 0x0001B7CC
		public override AsyncRequestWithQueryList CreateExternalAsyncRequestWithAutoDiscover(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, ExternalAuthenticationRequest autoDiscoverExternalAuthenticationRequest, ExternalAuthenticationRequest webProxyExternalAuthenticationRequest, Uri autoDiscoverUrl, SmtpAddress sharingKey)
		{
			return new ExternalProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverExternalAuthenticationRequest, webProxyExternalAuthenticationRequest, autoDiscoverUrl, sharingKey, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByUser.Create));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001D5F6 File Offset: 0x0001B7F6
		public override AsyncRequestWithQueryList CreateExternalByOAuthAsyncRequestWithAutoDiscover(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, Uri autoDiscoverUrl)
		{
			return new ExternalByOAuthProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverUrl, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByUser.Create));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001D60F File Offset: 0x0001B80F
		public override bool EnabledInRelationship(OrganizationRelationship organizationRelationship)
		{
			return organizationRelationship.FreeBusyAccessEnabled;
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0001D617 File Offset: 0x0001B817
		public override Offer OfferForExternalSharing
		{
			get
			{
				return Offer.SharingCalendarFreeBusy;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0001D61E File Offset: 0x0001B81E
		public override ThreadCounter Worker
		{
			get
			{
				return FreeBusyApplication.FreeBusyWorker;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001D625 File Offset: 0x0001B825
		public override ThreadCounter IOCompletion
		{
			get
			{
				return FreeBusyApplication.FreeBusyIOCompletion;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x0001D62C File Offset: 0x0001B82C
		public override int MinimumRequiredVersion
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001D62F File Offset: 0x0001B82F
		public override LocalizedString Name
		{
			get
			{
				return Strings.FreeBusyApplicationName;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001D638 File Offset: 0x0001B838
		private static FreeBusyQueryResult CopyViewAndResponseToResult(string source, FreeBusyView view, ResponseMessage responseMessage, EmailAddress emailAddress)
		{
			if (responseMessage == null || view == null)
			{
				Application.ProxyWebRequestTracer.TraceError<object, string, EmailAddress>(0L, "{0}: Proxy web request failed to return a {1} for mailbox {2}.", TraceContext.Get(), (responseMessage == null) ? "response message" : "view", emailAddress);
				return new FreeBusyQueryResult(new ProxyNoResultException(Strings.descProxyNoResultError(emailAddress.Address, source), 36156U));
			}
			if (responseMessage.ResponseClass != ResponseClassType.Success)
			{
				int errorCode = 0;
				string str = string.Empty;
				string serverName = string.Empty;
				string str2 = string.Empty;
				if (responseMessage.MessageXml != null && responseMessage.MessageXml.HasChildNodes)
				{
					foreach (object obj in responseMessage.MessageXml.ChildNodes)
					{
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.Name == "ExceptionCode")
						{
							errorCode = int.Parse(xmlNode.InnerText);
						}
						else if (xmlNode.Name == "ExceptionMessage")
						{
							str = xmlNode.InnerText;
						}
						else if (xmlNode.Name == "ExceptionServerName")
						{
							serverName = xmlNode.InnerText;
						}
						else if (xmlNode.Name == "ExceptionType")
						{
							str2 = xmlNode.InnerText;
						}
					}
				}
				return new FreeBusyQueryResult(new ProxyQueryFailureException(serverName, new LocalizedString(str2 + ":" + str), (ErrorConstants)errorCode, responseMessage, source));
			}
			return new FreeBusyQueryResult(view.FreeBusyViewType, view.CalendarEventArray, view.MergedFreeBusy, view.WorkingHours);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001D7D4 File Offset: 0x0001B9D4
		private void SetTimeZoneDefinitionHeader(Service service)
		{
			XmlDocument xmlDocument = new SafeXmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("TimeZoneDefinitionType", "http://schemas.microsoft.com/exchange/services/2006/types");
			TimeZoneDefinition timeZoneDefinition = new TimeZoneDefinition(this.clientContext.TimeZone);
			timeZoneDefinition.Render(xmlElement, "ext", "http://schemas.microsoft.com/exchange/services/2006/types", "TimeZoneDefinition", true, this.clientContext.ClientCulture ?? CultureInfo.InvariantCulture);
			service.TimeZoneDefinitionContextValue = new TimeZoneContext();
			service.TimeZoneDefinitionContextValue.TimeZoneDefinitionValue = (xmlElement.FirstChild as XmlElement);
		}

		// Token: 0x040003F1 RID: 1009
		private const string ErrorsNamespacePrefix = "Errors";

		// Token: 0x040003F2 RID: 1010
		private const string TimeZoneDefinitionType = "TimeZoneDefinitionType";

		// Token: 0x040003F3 RID: 1011
		private const string ProxyTypePrefix = "ext";

		// Token: 0x040003F4 RID: 1012
		private const string TimeZoneDefinitionElement = "TimeZoneDefinition";

		// Token: 0x040003F5 RID: 1013
		public static readonly ThreadCounter FreeBusyWorker = new ThreadCounter();

		// Token: 0x040003F6 RID: 1014
		public static readonly ThreadCounter FreeBusyIOCompletion = new ThreadCounter();

		// Token: 0x040003F7 RID: 1015
		private ClientContext clientContext;

		// Token: 0x040003F8 RID: 1016
		private FreeBusyViewOptions freeBusyView;

		// Token: 0x040003F9 RID: 1017
		private bool defaultFreeBusyOnly;

		// Token: 0x040003FA RID: 1018
		private QueryType queryType;
	}
}
