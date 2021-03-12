using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.MailTips;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.Net.WSTrust;

namespace Microsoft.Exchange.InfoWorker.Common.MailTips
{
	// Token: 0x02000117 RID: 279
	internal sealed class MailTipsApplication : Application
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00022D6D File Offset: 0x00020F6D
		public MailTipsApplication(int traceId, ProxyAddress sendingAs, MailTipTypes mailTipTypes, IBudget callerBudget) : base(false)
		{
			this.traceId = traceId;
			this.sendingAs = sendingAs;
			this.mailTipTypes = mailTipTypes;
			this.callerBudget = callerBudget;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00022D94 File Offset: 0x00020F94
		public override IService CreateService(WebServiceUri webServiceUri, TargetServerVersion targetVersion, RequestType requestType)
		{
			Service service = new Service(webServiceUri);
			service.RequestServerVersionValue = new RequestServerVersion();
			if (targetVersion >= TargetServerVersion.E15)
			{
				service.RequestServerVersionValue.Version = ExchangeVersionType.Exchange2012;
			}
			else
			{
				this.mailTipTypes &= ~MailTipTypes.Scope;
				service.RequestServerVersionValue.Version = ExchangeVersionType.Exchange2010;
			}
			return service;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00022DE8 File Offset: 0x00020FE8
		public override IAsyncResult BeginProxyWebRequest(IService service, MailboxData[] mailboxArray, AsyncCallback callback, object asyncState)
		{
			MailTipsApplication.GetMailTipsTracer.TraceFunction((long)this.traceId, "Entering MailTipsApplication.BeginProxyWebRequest");
			if (Testability.WebServiceCredentials != null)
			{
				service.Credentials = Testability.WebServiceCredentials;
				ServicePointManager.ServerCertificateValidationCallback = ((object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true);
			}
			GetMailTipsType getMailTipsType = new GetMailTipsType();
			getMailTipsType.SendingAs = new EmailAddressType();
			getMailTipsType.SendingAs.EmailAddress = this.sendingAs.AddressString;
			getMailTipsType.SendingAs.RoutingType = this.sendingAs.Prefix.PrimaryPrefix;
			getMailTipsType.Recipients = new EmailAddressType[mailboxArray.Length];
			for (int i = 0; i < mailboxArray.Length; i++)
			{
				MailboxData mailboxData = mailboxArray[i];
				getMailTipsType.Recipients[i] = new EmailAddressType();
				getMailTipsType.Recipients[i].EmailAddress = mailboxData.Email.Address;
				getMailTipsType.Recipients[i].RoutingType = mailboxData.Email.RoutingType;
			}
			getMailTipsType.MailTipsRequested = (MailTipTypes)this.mailTipTypes;
			return service.BeginGetMailTips(getMailTipsType, callback, asyncState);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00022EF8 File Offset: 0x000210F8
		public override void EndProxyWebRequest(ProxyWebRequest proxyWebRequest, QueryList queryList, IService service, IAsyncResult asyncResult)
		{
			MailTipsApplication.GetMailTipsTracer.TraceFunction((long)this.traceId, "Entering MailTipsApplication.EndProxyWebRequest");
			GetMailTipsResponseMessageType getMailTipsResponseMessageType = service.EndGetMailTips(asyncResult);
			int hashCode = proxyWebRequest.GetHashCode();
			if (getMailTipsResponseMessageType == null)
			{
				Application.ProxyWebRequestTracer.TraceError((long)this.traceId, "{0}: Proxy web request returned NULL GetMailTipsResponseMessageType", new object[]
				{
					TraceContext.Get()
				});
				queryList.SetResultInAllQueries(new MailTipsQueryResult(new NoEwsResponseException()));
				base.HandleNullResponse(proxyWebRequest);
				return;
			}
			ResponseCodeType responseCode = getMailTipsResponseMessageType.ResponseCode;
			if (responseCode != ResponseCodeType.NoError)
			{
				Application.ProxyWebRequestTracer.TraceError<object, string>((long)hashCode, "{0}: Proxy web request returned error code {1}", TraceContext.Get(), responseCode.ToString());
				queryList.SetResultInAllQueries(new MailTipsQueryResult(new ErrorEwsResponseException(responseCode)));
				return;
			}
			this.ProcessResponseMessages(hashCode, queryList, getMailTipsResponseMessageType);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00022FB2 File Offset: 0x000211B2
		public override string GetParameterDataString()
		{
			return this.traceId.ToString() + " " + this.sendingAs.ToString();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00022FD4 File Offset: 0x000211D4
		public override LocalQuery CreateLocalQuery(ClientContext clientContext, DateTime requestCompletionDeadline)
		{
			return new MailTipsLocalQuery(clientContext, requestCompletionDeadline, this.callerBudget);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00022FF0 File Offset: 0x000211F0
		public override BaseQueryResult CreateQueryResult(LocalizedException exception)
		{
			return new MailTipsQueryResult(exception);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00022FF8 File Offset: 0x000211F8
		public override BaseQuery CreateFromUnknown(RecipientData recipientData, LocalizedException exception)
		{
			return MailTipsQuery.CreateFromUnknown(recipientData, exception);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00023001 File Offset: 0x00021201
		public override BaseQuery CreateFromIndividual(RecipientData recipientData)
		{
			return MailTipsQuery.CreateFromIndividual(recipientData);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00023009 File Offset: 0x00021209
		public override BaseQuery CreateFromIndividual(RecipientData recipientData, LocalizedException exception)
		{
			return MailTipsQuery.CreateFromIndividual(recipientData, exception);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00023012 File Offset: 0x00021212
		public override AvailabilityException CreateExceptionForUnsupportedVersion(RecipientData recipient, int serverVersion)
		{
			return new E14orHigherProxyServerNotFound((recipient != null) ? recipient.EmailAddress : null, Globals.E14Version);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0002302A File Offset: 0x0002122A
		public override BaseQuery CreateFromGroup(RecipientData recipientData, BaseQuery[] groupMembers, bool groupCapped)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00023031 File Offset: 0x00021231
		public override bool EnabledInRelationship(OrganizationRelationship organizationRelationship)
		{
			return organizationRelationship.MailTipsAccessEnabled;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00023039 File Offset: 0x00021239
		public override Offer OfferForExternalSharing
		{
			get
			{
				return Offer.MailTips;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00023040 File Offset: 0x00021240
		public override ThreadCounter Worker
		{
			get
			{
				return MailTipsApplication.MailTipsWorker;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x00023047 File Offset: 0x00021247
		public override ThreadCounter IOCompletion
		{
			get
			{
				return MailTipsApplication.MailTipsIOCompletion;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002304E File Offset: 0x0002124E
		public override int MinimumRequiredVersion
		{
			get
			{
				return Globals.E14Version;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00023055 File Offset: 0x00021255
		public override int MinimumRequiredVersionForExternalUser
		{
			get
			{
				return Globals.E14SP1Version;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0002305C File Offset: 0x0002125C
		public override LocalizedString Name
		{
			get
			{
				return Strings.MailtipsApplicationName;
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00023064 File Offset: 0x00021264
		private void ProcessResponseMessages(int traceId, QueryList queryList, GetMailTipsResponseMessageType response)
		{
			if (response.ResponseMessages == null)
			{
				Application.ProxyWebRequestTracer.TraceError((long)traceId, "{0}: Proxy web request returned NULL GetMailTipsResponseMessageType.ResponseMessages", new object[]
				{
					TraceContext.Get()
				});
				queryList.SetResultInAllQueries(new MailTipsQueryResult(new NoEwsResponseException()));
				return;
			}
			for (int i = 0; i < response.ResponseMessages.Length; i++)
			{
				MailTipsResponseMessageType mailTipsResponseMessageType = response.ResponseMessages[i];
				BaseQuery[] array = queryList.FindByEmailAddress(queryList[i].Email.Address);
				foreach (MailTipsQuery mailTipsQuery in array)
				{
					if (mailTipsResponseMessageType == null)
					{
						Application.ProxyWebRequestTracer.TraceError<object, Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress>((long)traceId, "{0}: Proxy web request returned NULL MailTipsResponseMessageType for mailbox {1}.", TraceContext.Get(), queryList[i].Email);
						mailTipsQuery.SetResultOnFirstCall(new MailTipsQueryResult(new NoEwsResponseException()));
					}
					else if (mailTipsResponseMessageType.ResponseCode != ResponseCodeType.NoError)
					{
						Application.ProxyWebRequestTracer.TraceError<object, Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress, ResponseCodeType>((long)traceId, "{0}: Proxy web request returned error MailTipsResponseMessageType for mailbox {1}. Error coee is {2}.", TraceContext.Get(), queryList[i].Email, mailTipsResponseMessageType.ResponseCode);
						mailTipsQuery.SetResultOnFirstCall(new MailTipsQueryResult(new ErrorEwsResponseException(mailTipsResponseMessageType.ResponseCode)));
					}
					else
					{
						MailTips mailTips = mailTipsResponseMessageType.MailTips;
						if (mailTips == null)
						{
							Application.ProxyWebRequestTracer.TraceDebug<object, Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress>((long)traceId, "{0}: Proxy web request returned NULL MailTips for mailbox {1}.", TraceContext.Get(), queryList[i].Email);
							mailTipsQuery.SetResultOnFirstCall(new MailTipsQueryResult(new NoMailTipsInEwsResponseMessageException()));
						}
						else
						{
							MailTips mailTips2 = MailTipsApplication.ParseWebServiceMailTips(mailTips);
							MailTipsQueryResult resultOnFirstCall = new MailTipsQueryResult(mailTips2);
							mailTipsQuery.SetResultOnFirstCall(resultOnFirstCall);
						}
					}
				}
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000231F0 File Offset: 0x000213F0
		private static MailTips ParseWebServiceMailTips(MailTips webServiceMailTips)
		{
			Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress emailAddress = new Microsoft.Exchange.InfoWorker.Common.Availability.EmailAddress(string.Empty, webServiceMailTips.RecipientAddress.EmailAddress, webServiceMailTips.RecipientAddress.RoutingType);
			MailTips mailTips = new MailTips(emailAddress);
			if (webServiceMailTips.CustomMailTip != null)
			{
				mailTips.CustomMailTip = webServiceMailTips.CustomMailTip;
			}
			if (webServiceMailTips.DeliveryRestrictedSpecified)
			{
				mailTips.DeliveryRestricted = webServiceMailTips.DeliveryRestricted;
			}
			if (webServiceMailTips.ExternalMemberCountSpecified)
			{
				mailTips.ExternalMemberCount = webServiceMailTips.ExternalMemberCount;
			}
			if (webServiceMailTips.InvalidRecipientSpecified)
			{
				mailTips.InvalidRecipient = webServiceMailTips.InvalidRecipient;
			}
			if (webServiceMailTips.ScopeSpecified)
			{
				mailTips.Scope = (ScopeTypes)webServiceMailTips.Scope;
			}
			if (webServiceMailTips.IsModeratedSpecified)
			{
				mailTips.IsModerated = webServiceMailTips.IsModerated;
			}
			if (webServiceMailTips.MailboxFullSpecified)
			{
				mailTips.MailboxFull = webServiceMailTips.MailboxFull;
			}
			if (webServiceMailTips.MaxMessageSizeSpecified)
			{
				mailTips.MaxMessageSize = webServiceMailTips.MaxMessageSize;
			}
			if (webServiceMailTips.OutOfOffice != null && webServiceMailTips.OutOfOffice.ReplyBody != null)
			{
				mailTips.OutOfOfficeMessage = webServiceMailTips.OutOfOffice.ReplyBody.Message;
				mailTips.OutOfOfficeMessageLanguage = webServiceMailTips.OutOfOffice.ReplyBody.Lang;
				mailTips.OutOfOfficeDuration = webServiceMailTips.OutOfOffice.Duration;
			}
			if (webServiceMailTips.MailboxFullSpecified)
			{
				mailTips.MailboxFull = webServiceMailTips.MailboxFull;
			}
			if (webServiceMailTips.TotalMemberCountSpecified)
			{
				mailTips.TotalMemberCount = webServiceMailTips.TotalMemberCount;
			}
			return mailTips;
		}

		// Token: 0x040004AD RID: 1197
		private static readonly Trace GetMailTipsTracer = ExTraceGlobals.GetMailTipsTracer;

		// Token: 0x040004AE RID: 1198
		private int traceId;

		// Token: 0x040004AF RID: 1199
		private ProxyAddress sendingAs;

		// Token: 0x040004B0 RID: 1200
		private MailTipTypes mailTipTypes;

		// Token: 0x040004B1 RID: 1201
		private IBudget callerBudget;

		// Token: 0x040004B2 RID: 1202
		public static readonly ThreadCounter MailTipsWorker = new ThreadCounter();

		// Token: 0x040004B3 RID: 1203
		public static readonly ThreadCounter MailTipsIOCompletion = new ThreadCounter();
	}
}
