using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002A2 RID: 674
	internal abstract class MessageTrackingApplication : Application
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x000567A1 File Offset: 0x000549A1
		public MessageTrackingApplication(bool shouldProcessGroup, bool supportsPublicFolder, ExchangeVersion ewsRequestedVersion) : base(false)
		{
			this.ewsRequestedVersion = ewsRequestedVersion;
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000567B4 File Offset: 0x000549B4
		public override IService CreateService(WebServiceUri webServiceUri, TargetServerVersion targetVersion, RequestType requestType)
		{
			Service service = new Service(webServiceUri);
			service.RequestServerVersionValue = new RequestServerVersion();
			service.RequestServerVersionValue.Version = VersionConverter.GetRdExchangeVersionType(service.ServiceVersion);
			return service;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000567EC File Offset: 0x000549EC
		public override LocalQuery CreateLocalQuery(ClientContext clientContext, DateTime requestCompletionDeadline)
		{
			TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Attempted to create local query in x-forest case.", new object[0]);
			TrackingError trackingError = new TrackingError(ErrorCode.UnexpectedErrorPermanent, string.Empty, "Local autodiscover query for Cross-Forest disallowed", string.Empty);
			TrackingFatalException ex = new TrackingFatalException(trackingError, null, false);
			DiagnosticWatson.SendWatsonWithoutCrash(ex, "CreateLocalQuery", TimeSpan.FromDays(1.0));
			throw ex;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00056850 File Offset: 0x00054A50
		public override BaseQuery CreateFromGroup(RecipientData recipientData, BaseQuery[] groupMembers, bool groupCapped)
		{
			TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Attempted to create group query in x-forest case.", new object[0]);
			TrackingError trackingError = new TrackingError(ErrorCode.UnexpectedErrorPermanent, string.Empty, "Group autodiscover query for Cross-Forest disallowed", string.Empty);
			TrackingFatalException ex = new TrackingFatalException(trackingError, null, false);
			DiagnosticWatson.SendWatsonWithoutCrash(ex, "CreateFromGroup", TimeSpan.FromDays(1.0));
			throw ex;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000568B2 File Offset: 0x00054AB2
		public override AsyncRequestWithQueryList CreateCrossForestAsyncRequestWithAutoDiscoverForRemoteMailbox(ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, TargetForestConfiguration targetForestConfiguration)
		{
			return new ProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, targetForestConfiguration, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestXmlByUser.Create));
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000568CC File Offset: 0x00054ACC
		public override AsyncRequestWithQueryList CreateExternalAsyncRequestWithAutoDiscoverForRemoteMailbox(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, ExternalAuthenticationRequest autoDiscoverExternalAuthenticationRequest, ExternalAuthenticationRequest webProxyExternalAuthenticationRequest, Uri autoDiscoverUrl, SmtpAddress sharingKey)
		{
			return new ExternalProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverExternalAuthenticationRequest, webProxyExternalAuthenticationRequest, autoDiscoverUrl, sharingKey, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByUser.Create));
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x000568F6 File Offset: 0x00054AF6
		public override AsyncRequestWithQueryList CreateExternalByOAuthAsyncRequestWithAutoDiscoverForRemoteMailbox(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, Uri autoDiscoverUrl)
		{
			return new ExternalByOAuthProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverUrl, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByUser.Create));
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0005690F File Offset: 0x00054B0F
		public override bool EnabledInRelationship(OrganizationRelationship organizationRelationship)
		{
			return organizationRelationship.DeliveryReportEnabled;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00056917 File Offset: 0x00054B17
		public override AvailabilityException CreateExceptionForUnsupportedVersion(RecipientData recipient, int serverVersion)
		{
			return new ProxyServerWithMinimumRequiredVersionNotFound((recipient != null) ? recipient.EmailAddress : null, serverVersion, this.MinimumRequiredVersion);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00056934 File Offset: 0x00054B34
		internal static IList<RecipientData> CreateRecipientQueryResult(DirectoryContext directoryContext, DateTime queryPrepareDeadline, string proxyRecipient)
		{
			RecipientQuery recipientQuery = new RecipientQuery(directoryContext.ClientContext, directoryContext.TenantGalSession, queryPrepareDeadline, FindMessageTrackingQuery.RecipientProperties);
			Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress emailAddress = new Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress(string.Empty, proxyRecipient);
			Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress[] emailAddressArray = new Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress[]
			{
				emailAddress
			};
			IList<RecipientData> list = recipientQuery.Query(emailAddressArray);
			if (list[0].IsEmpty)
			{
				list = MessageTrackingApplication.CreateFakeRecipientQueryResult(ServerCache.Instance.GetOrgMailboxForDomain(emailAddress.Domain).ToString());
			}
			return list;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000569B4 File Offset: 0x00054BB4
		internal static IList<RecipientData> CreateFakeRecipientQueryResult(string address)
		{
			Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress emailAddress = new Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress(string.Empty, address);
			Dictionary<PropertyDefinition, object> propertyMap = new Dictionary<PropertyDefinition, object>
			{
				{
					ADRecipientSchema.RecipientType,
					RecipientType.MailContact
				},
				{
					ADRecipientSchema.PrimarySmtpAddress,
					new SmtpAddress(address)
				},
				{
					ADRecipientSchema.ExternalEmailAddress,
					new SmtpProxyAddress(address, true)
				}
			};
			RecipientData item = RecipientData.Create(emailAddress, propertyMap);
			return new List<RecipientData>
			{
				item
			};
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00056A27 File Offset: 0x00054C27
		public override Offer OfferForExternalSharing
		{
			get
			{
				return Offer.SharingCalendarFreeBusy;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00056A30 File Offset: 0x00054C30
		public override int MinimumRequiredVersion
		{
			get
			{
				switch (this.ewsRequestedVersion)
				{
				case ExchangeVersion.Exchange2010:
					return Globals.E14Version;
				case ExchangeVersion.Exchange2010_SP1:
					return Globals.E14SP1Version;
				case ExchangeVersion.Exchange2013:
					return Globals.E15Version;
				}
				throw new InvalidOperationException("Message tracking application has unexpected ewsRequestedVersion");
			}
		}

		// Token: 0x04000CB3 RID: 3251
		private ExchangeVersion ewsRequestedVersion;
	}
}
