using System;
using Microsoft.Exchange.Data.ApplicationLogic.UserPhotos;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient;

namespace Microsoft.Exchange.InfoWorker.Common.UserPhotos
{
	// Token: 0x02000143 RID: 323
	internal sealed class UserPhotoApplication : Application
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x00025FF8 File Offset: 0x000241F8
		public UserPhotoApplication(PhotoRequest photoRequest, PhotosConfiguration configuration, bool traceRequest, ITracer upstreamTracer) : base(false)
		{
			this.photoRequest = photoRequest;
			this.photosConfiguration = configuration;
			this.traceRequest = traceRequest;
			this.upstreamTracer = upstreamTracer;
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00026030 File Offset: 0x00024230
		public override IService CreateService(WebServiceUri webServiceUri, TargetServerVersion targetVersion, RequestType requestType)
		{
			switch (requestType)
			{
			case RequestType.Local:
			case RequestType.IntraSite:
			case RequestType.CrossSite:
				return this.CreateRestService(webServiceUri);
			}
			return this.CreateSoapService(webServiceUri);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00026070 File Offset: 0x00024270
		public override IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxArray, AsyncCallback callback, object asyncState)
		{
			if (mailboxArray == null)
			{
				throw new ArgumentNullException("mailboxArray");
			}
			if (mailboxArray.Length < 1)
			{
				throw new ArgumentOutOfRangeException("mailboxArray.Length");
			}
			this.tracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "Proxying GetUserPhoto request.  Target: {0}. Service endpoint: {1}. Timeout: {2}.", this.photoRequest.TargetSmtpAddress, service.Url, service.Timeout);
			this.proxyLatencyTracker = new StopwatchPerformanceTracker("ProxyRequest", this.photoRequest.PerformanceLogger);
			return service.BeginGetUserPhoto(this.photoRequest, this.photosConfiguration, callback, asyncState);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000260FC File Offset: 0x000242FC
		public override void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult)
		{
			try
			{
				GetUserPhotoResponseMessageType getUserPhotoResponseMessageType = service.EndGetUserPhoto(asyncResult);
				queryList.SetResultInAllQueries(new UserPhotoQueryResult(getUserPhotoResponseMessageType.PictureData, getUserPhotoResponseMessageType.CacheId, getUserPhotoResponseMessageType.StatusCode, getUserPhotoResponseMessageType.Expires, getUserPhotoResponseMessageType.ContentType, this.upstreamTracer));
			}
			finally
			{
				this.proxyLatencyTracker.Stop();
			}
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00026160 File Offset: 0x00024360
		public override string GetParameterDataString()
		{
			return string.Empty;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00026167 File Offset: 0x00024367
		public override LocalQuery CreateLocalQuery(ClientContext clientContext, DateTime requestCompletionDeadline)
		{
			return new UserPhotoLocalQuery(clientContext, requestCompletionDeadline, this.photoRequest, this.photosConfiguration, this.upstreamTracer);
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00026182 File Offset: 0x00024382
		public override BaseQueryResult CreateQueryResult(LocalizedException exception)
		{
			return new UserPhotoQueryResult(exception, this.upstreamTracer);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00026190 File Offset: 0x00024390
		public override BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return UserPhotoQuery.CreateFromUnknown(recipientData, exception, this.upstreamTracer);
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002619F File Offset: 0x0002439F
		public override BaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return UserPhotoQuery.CreateFromIndividual(recipientData, this.upstreamTracer);
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000261AD File Offset: 0x000243AD
		public override BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return UserPhotoQuery.CreateFromIndividual(recipientData, exception, this.upstreamTracer);
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000261BC File Offset: 0x000243BC
		public override AvailabilityException CreateExceptionForUnsupportedVersion(RecipientData recipient, int serverVersion)
		{
			ProxyServerWithMinimumRequiredVersionNotFound proxyServerWithMinimumRequiredVersionNotFound = new ProxyServerWithMinimumRequiredVersionNotFound(recipient.EmailAddress, serverVersion, Globals.E15Version);
			proxyServerWithMinimumRequiredVersionNotFound.Data["ThumbnailPhotoKey"] = recipient.ThumbnailPhoto;
			return proxyServerWithMinimumRequiredVersionNotFound;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x000261F2 File Offset: 0x000243F2
		public override BaseQuery CreateFromGroup(RecipientData recipientData, BaseQuery[] groupMembers, bool groupCapped)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000261F9 File Offset: 0x000243F9
		public override bool EnabledInRelationship(OrganizationRelationship organizationRelationship)
		{
			return organizationRelationship.PhotosEnabled;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00026201 File Offset: 0x00024401
		public override Offer OfferForExternalSharing
		{
			get
			{
				return Offer.SharingCalendarFreeBusy;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00026208 File Offset: 0x00024408
		public override ThreadCounter Worker
		{
			get
			{
				return UserPhotoApplication.UserPhotoWorker;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0002620F File Offset: 0x0002440F
		public override ThreadCounter IOCompletion
		{
			get
			{
				return UserPhotoApplication.UserPhotoIOCompletion;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00026216 File Offset: 0x00024416
		public override int MinimumRequiredVersion
		{
			get
			{
				return Globals.E15Version;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0002621D File Offset: 0x0002441D
		public override int MinimumRequiredVersionForExternalUser
		{
			get
			{
				return Globals.E15Version;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00026224 File Offset: 0x00024424
		public override bool SupportsPersonalSharingRelationship
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00026227 File Offset: 0x00024427
		public override LocalizedString Name
		{
			get
			{
				return Strings.PhotosApplicationName;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002622E File Offset: 0x0002442E
		private IService CreateRestService(WebServiceUri webServiceUri)
		{
			this.photoRequest.PerformanceLogger.Log("ProxyOverRestService", string.Empty, 1U);
			return new RestService(HttpAuthenticator.NetworkService, webServiceUri, this.traceRequest, this.upstreamTracer);
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00026264 File Offset: 0x00024464
		private IService CreateSoapService(WebServiceUri webServiceUri)
		{
			return new Service(webServiceUri, this.traceRequest, this.upstreamTracer)
			{
				RequestServerVersionValue = new RequestServerVersion(),
				RequestServerVersionValue = 
				{
					Version = ExchangeVersionType.Exchange2012
				}
			};
		}

		// Token: 0x040006D8 RID: 1752
		internal const string ThumbnailPhotoKey = "ThumbnailPhotoKey";

		// Token: 0x040006D9 RID: 1753
		private readonly ITracer tracer;

		// Token: 0x040006DA RID: 1754
		private readonly ITracer upstreamTracer;

		// Token: 0x040006DB RID: 1755
		private readonly bool traceRequest;

		// Token: 0x040006DC RID: 1756
		private readonly PhotoRequest photoRequest;

		// Token: 0x040006DD RID: 1757
		private readonly PhotosConfiguration photosConfiguration;

		// Token: 0x040006DE RID: 1758
		private StopwatchPerformanceTracker proxyLatencyTracker;

		// Token: 0x040006DF RID: 1759
		public static readonly ThreadCounter UserPhotoWorker = new ThreadCounter();

		// Token: 0x040006E0 RID: 1760
		public static readonly ThreadCounter UserPhotoIOCompletion = new ThreadCounter();
	}
}
