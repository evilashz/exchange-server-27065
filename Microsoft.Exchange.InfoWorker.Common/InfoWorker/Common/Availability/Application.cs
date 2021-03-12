using System;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.RequestDispatch;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.SoapWebClient.AutoDiscover;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200004F RID: 79
	internal abstract class Application
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000947F File Offset: 0x0000767F
		protected Application(bool shouldProcessGroup)
		{
			this.shouldProcessGroup = shouldProcessGroup;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000948E File Offset: 0x0000768E
		private Application()
		{
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00009496 File Offset: 0x00007696
		public bool ShouldProcessGroup
		{
			get
			{
				return this.shouldProcessGroup;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000949E File Offset: 0x0000769E
		public virtual bool SupportsPersonalSharingRelationship
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AA RID: 426
		public abstract int MinimumRequiredVersion { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000094A1 File Offset: 0x000076A1
		public virtual int MinimumRequiredVersionForExternalUser
		{
			get
			{
				return this.MinimumRequiredVersion;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001AC RID: 428
		public abstract LocalizedString Name { get; }

		// Token: 0x060001AD RID: 429
		public abstract IService CreateService(WebServiceUri webServiceUri, TargetServerVersion targetVersion, RequestType requestType);

		// Token: 0x060001AE RID: 430
		public abstract IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxDataArray, AsyncCallback callback, object asyncState);

		// Token: 0x060001AF RID: 431
		public abstract void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult);

		// Token: 0x060001B0 RID: 432
		public abstract string GetParameterDataString();

		// Token: 0x060001B1 RID: 433
		public abstract LocalQuery CreateLocalQuery(ClientContext clientContext, DateTime requestCompletionDeadline);

		// Token: 0x060001B2 RID: 434
		public abstract BaseQueryResult CreateQueryResult(LocalizedException exception);

		// Token: 0x060001B3 RID: 435
		public abstract BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception);

		// Token: 0x060001B4 RID: 436
		public abstract BaseQuery CreateFromIndividual(RecipientData recipientData);

		// Token: 0x060001B5 RID: 437
		public abstract BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception);

		// Token: 0x060001B6 RID: 438
		public abstract AvailabilityException CreateExceptionForUnsupportedVersion(RecipientData recipient, int serverVersion);

		// Token: 0x060001B7 RID: 439
		public abstract BaseQuery CreateFromGroup(RecipientData recipientData, BaseQuery[] groupMembers, bool groupCapped);

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B8 RID: 440
		public abstract Offer OfferForExternalSharing { get; }

		// Token: 0x060001B9 RID: 441
		public abstract bool EnabledInRelationship(OrganizationRelationship organizationRelationship);

		// Token: 0x060001BA RID: 442 RVA: 0x000094AC File Offset: 0x000076AC
		public ExchangeVersion? GetRequestedVersionForAutoDiscover(AutodiscoverType autodiscoverType)
		{
			int minimumVersionByAutodiscoverType = this.GetMinimumVersionByAutodiscoverType(autodiscoverType);
			if (Globals.E14SP1Version == minimumVersionByAutodiscoverType)
			{
				return new ExchangeVersion?(ExchangeVersion.Exchange2010_SP1);
			}
			if (Globals.E14Version == minimumVersionByAutodiscoverType)
			{
				return new ExchangeVersion?(ExchangeVersion.Exchange2010);
			}
			if (Globals.E15Version == minimumVersionByAutodiscoverType)
			{
				return new ExchangeVersion?(ExchangeVersion.Exchange2013);
			}
			if (minimumVersionByAutodiscoverType == 0)
			{
				return null;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported minimum required version {0} for {1}, please add this to Application.GetRequestedVersionForAutoDiscover code.", new object[]
			{
				minimumVersionByAutodiscoverType,
				base.GetType()
			}));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000952C File Offset: 0x0000772C
		public int GetAutodiscoverVersionBucket(AutodiscoverType autodiscoverType)
		{
			int minimumVersionByAutodiscoverType = this.GetMinimumVersionByAutodiscoverType(autodiscoverType);
			if (Globals.E15Version == minimumVersionByAutodiscoverType)
			{
				return 3;
			}
			if (Globals.E14SP1Version == minimumVersionByAutodiscoverType)
			{
				return 2;
			}
			if (Globals.E14Version == minimumVersionByAutodiscoverType)
			{
				return 1;
			}
			if (minimumVersionByAutodiscoverType == 0)
			{
				return 0;
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported minimum required version {0} for {1}, please add this to Application.GetAutodiscoverVersionBucket code.", new object[]
			{
				minimumVersionByAutodiscoverType,
				base.GetType()
			}));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009592 File Offset: 0x00007792
		public virtual AsyncRequestWithQueryList CreateCrossForestAsyncRequestWithAutoDiscover(ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, TargetForestConfiguration targetForestConfiguration)
		{
			return new ProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, targetForestConfiguration, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByDomain.CreateForCrossForest));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000095AB File Offset: 0x000077AB
		public virtual AsyncRequestWithQueryList CreateCrossForestAsyncRequestWithAutoDiscoverForRemoteMailbox(ClientContext clientContext, RequestLogger requestLogger, QueryList queryList, TargetForestConfiguration targetForestConfiguration)
		{
			return new ProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, targetForestConfiguration, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestXmlByUser.Create));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000095C4 File Offset: 0x000077C4
		public virtual AsyncRequestWithQueryList CreateExternalAsyncRequestWithAutoDiscover(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, ExternalAuthenticationRequest autoDiscoverExternalAuthenticationRequest, ExternalAuthenticationRequest webProxyExternalAuthenticationRequest, Uri autoDiscoverUrl, SmtpAddress sharingKey)
		{
			return new ExternalProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverExternalAuthenticationRequest, webProxyExternalAuthenticationRequest, autoDiscoverUrl, sharingKey, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByDomain.CreateForExternal));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000095F0 File Offset: 0x000077F0
		public virtual AsyncRequestWithQueryList CreateExternalAsyncRequestWithAutoDiscoverForRemoteMailbox(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, ExternalAuthenticationRequest autoDiscoverExternalAuthenticationRequest, ExternalAuthenticationRequest webProxyExternalAuthenticationRequest, Uri autoDiscoverUrl, SmtpAddress sharingKey)
		{
			return new ExternalProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverExternalAuthenticationRequest, webProxyExternalAuthenticationRequest, autoDiscoverUrl, sharingKey, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByUser.Create));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000961A File Offset: 0x0000781A
		public virtual AsyncRequestWithQueryList CreateExternalByOAuthAsyncRequestWithAutoDiscover(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, Uri autoDiscoverUrl)
		{
			return new ExternalByOAuthProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverUrl, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByDomain.CreateForExternal));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009633 File Offset: 0x00007833
		public virtual AsyncRequestWithQueryList CreateExternalByOAuthAsyncRequestWithAutoDiscoverForRemoteMailbox(InternalClientContext clientContext, RequestLogger requestLogger, QueryList queryList, Uri autoDiscoverUrl)
		{
			return new ExternalByOAuthProxyWebRequestWithAutoDiscover(this, clientContext, requestLogger, queryList, autoDiscoverUrl, new CreateAutoDiscoverRequestDelegate(AutoDiscoverRequestByUser.Create));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000964C File Offset: 0x0000784C
		protected void HandleNullResponse(ProxyWebRequest proxyWebRequest)
		{
			Application.ProxyWebRequestTracer.TraceError((long)proxyWebRequest.GetHashCode(), "{0}: Proxy web request returned NULL response.", new object[]
			{
				TraceContext.Get()
			});
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001C3 RID: 451
		public abstract ThreadCounter Worker { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C4 RID: 452
		public abstract ThreadCounter IOCompletion { get; }

		// Token: 0x060001C5 RID: 453 RVA: 0x0000967F File Offset: 0x0000787F
		public bool IsVersionSupported(int serverVersion)
		{
			return serverVersion >= this.MinimumRequiredVersion;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000968D File Offset: 0x0000788D
		public bool IsVersionSupportedForExternalUser(int serverVersion)
		{
			return serverVersion >= this.MinimumRequiredVersionForExternalUser;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000969C File Offset: 0x0000789C
		public void LogThreadsUsage(RequestLogger requestLogger)
		{
			int value;
			int value2;
			ThreadPool.GetAvailableThreads(out value, out value2);
			requestLogger.AppendToLog<int>("Threads.Worker.Available", value);
			requestLogger.AppendToLog<int>("Threads.Worker.InUse", this.Worker.Count);
			requestLogger.AppendToLog<int>("Threads.IO.Available", value2);
			requestLogger.AppendToLog<int>("Threads.IO.InUse", this.IOCompletion.Count);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000096F8 File Offset: 0x000078F8
		private int GetMinimumVersionByAutodiscoverType(AutodiscoverType autodiscoverType)
		{
			switch (autodiscoverType)
			{
			case AutodiscoverType.Internal:
				return this.MinimumRequiredVersion;
			case AutodiscoverType.External:
				return this.MinimumRequiredVersionForExternalUser;
			default:
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported AutodiscoverType {0} encountered, please add this to Application.GetMinimumVersionFromAutodiscoverType code.", new object[]
				{
					autodiscoverType
				}));
			}
		}

		// Token: 0x04000128 RID: 296
		protected static readonly Trace ProxyWebRequestTracer = ExTraceGlobals.ProxyWebRequestTracer;

		// Token: 0x04000129 RID: 297
		private bool shouldProcessGroup;
	}
}
