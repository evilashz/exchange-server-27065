using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C0 RID: 448
	internal class DiscoveryEwsClient : IEwsClient
	{
		// Token: 0x06000C1A RID: 3098 RVA: 0x00034B90 File Offset: 0x00032D90
		public DiscoveryEwsClient(GroupId groupId, MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo caller)
		{
			this.groupId = groupId;
			this.mailboxes = mailboxes;
			this.searchCriteria = searchCriteria;
			this.pagingInfo = pagingInfo;
			this.callerInfo = caller;
			CertificateValidationManager.RegisterCallback(base.GetType().FullName, new RemoteCertificateValidationCallback(CertificateValidation.CertificateErrorHandler));
			this.service = new ExchangeService(4, DiscoveryEwsClient.GetTimeZoneInfoFromExTimeZone(this.pagingInfo.TimeZone));
			this.service.Url = this.groupId.Uri;
			this.service.HttpHeaders[CertificateValidationManager.ComponentIdHeaderName] = base.GetType().FullName;
			string text = string.Format("{0}&FOUT=true", caller.UserAgent);
			if (this.groupId.GroupType != GroupType.CrossPremise)
			{
				this.service.UserAgent = WellKnownUserAgent.GetEwsNegoAuthUserAgent(string.Format("{0}-{1}", DiscoveryEwsClient.CrossServerUserAgent, text));
			}
			else
			{
				this.service.UserAgent = text;
			}
			this.service.ClientRequestId = this.callerInfo.QueryCorrelationId.ToString("N");
			this.Authenticate();
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00034CB0 File Offset: 0x00032EB0
		public IAsyncResult BeginEwsCall(AsyncCallback callback, object state)
		{
			SearchMailboxesParameters searchMailboxesParameters = new SearchMailboxesParameters();
			searchMailboxesParameters.SearchQueries = DiscoveryEwsClient.GetMailboxQueries(this.mailboxes, this.searchCriteria, this.pagingInfo);
			searchMailboxesParameters.ResultType = DiscoveryEwsClient.GetSearchType(this.searchCriteria.SearchType);
			searchMailboxesParameters.SortBy = DiscoveryEwsClient.GetSortbyProperty(this.pagingInfo.SortBy);
			searchMailboxesParameters.SortOrder = DiscoveryEwsClient.GetSortDirection(this.pagingInfo.SortBy);
			searchMailboxesParameters.PerformDeduplication = this.pagingInfo.ExcludeDuplicates;
			searchMailboxesParameters.PageSize = this.pagingInfo.PageSize;
			searchMailboxesParameters.PageDirection = DiscoveryEwsClient.GetPageDirection(this.pagingInfo.Direction);
			searchMailboxesParameters.PageItemReference = ((this.pagingInfo.SortValue == null) ? null : this.pagingInfo.SortValue.ToString());
			searchMailboxesParameters.PreviewItemResponseShape = DiscoveryEwsClient.GetPreviewItemResponseShape(this.pagingInfo.BaseShape, this.pagingInfo.AdditionalProperties);
			if (this.searchCriteria.QueryCulture != null && !string.IsNullOrEmpty(this.searchCriteria.QueryCulture.Name))
			{
				searchMailboxesParameters.Language = this.searchCriteria.QueryCulture.Name;
			}
			return this.service.BeginSearchMailboxes(callback, state, searchMailboxesParameters);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00034DEC File Offset: 0x00032FEC
		public ServiceResponse EndEwsCall(IAsyncResult asyncResult)
		{
			ServiceResponseCollection<SearchMailboxesResponse> serviceResponseCollection = this.service.EndSearchMailboxes(asyncResult);
			return serviceResponseCollection[0];
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00034E10 File Offset: 0x00033010
		internal static MailboxQuery[] GetMailboxQueries(MailboxInfo[] mailboxes, SearchCriteria criteria, PagingInfo info)
		{
			List<MailboxSearchScope> list = new List<MailboxSearchScope>(mailboxes.Length);
			foreach (MailboxInfo mailboxInfo in mailboxes)
			{
				MailboxSearchLocation mailboxSearchLocation = (mailboxInfo.Type == MailboxType.Primary) ? 0 : 1;
				list.Add(new MailboxSearchScope(mailboxInfo.LegacyExchangeDN, mailboxSearchLocation));
			}
			return new MailboxQuery[]
			{
				new MailboxQuery(criteria.QueryString, list.ToArray())
			};
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00034E80 File Offset: 0x00033080
		internal static SearchResultType GetSearchType(SearchType type)
		{
			SearchResultType searchResultType = 0;
			if (type == SearchType.Preview)
			{
				searchResultType |= 1;
			}
			if (type == SearchType.Statistics)
			{
				searchResultType = searchResultType;
			}
			return searchResultType;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00034EA0 File Offset: 0x000330A0
		internal static PreviewItemResponseShape GetPreviewItemResponseShape(PreviewItemBaseShape baseShape, List<ExtendedPropertyInfo> additionalProperties)
		{
			if (baseShape == PreviewItemBaseShape.Default && (additionalProperties == null || additionalProperties.Count == 0))
			{
				return null;
			}
			PreviewItemResponseShape previewItemResponseShape = new PreviewItemResponseShape
			{
				BaseShape = DiscoveryEwsClient.GetBaseShape(baseShape)
			};
			int num = 0;
			if (additionalProperties != null)
			{
				previewItemResponseShape.AdditionalProperties = new ExtendedPropertyDefinition[additionalProperties.Count];
				num = additionalProperties.Count;
			}
			for (int i = 0; i < num; i++)
			{
				ExtendedPropertyDefinition extendedPropertyDefinition = DiscoveryEwsClient.ConvertExtendedPropertyInfoToExtendedPropertyDefinition(additionalProperties[i]);
				previewItemResponseShape.AdditionalProperties[i] = extendedPropertyDefinition;
			}
			return previewItemResponseShape;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00034F14 File Offset: 0x00033114
		internal static PreviewItemBaseShape GetBaseShape(PreviewItemBaseShape baseShape)
		{
			if (baseShape == PreviewItemBaseShape.Compact)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00034F20 File Offset: 0x00033120
		internal static ExtendedPropertyDefinition ConvertExtendedPropertyInfoToExtendedPropertyDefinition(ExtendedPropertyInfo extendedPropertyInfo)
		{
			MapiPropertyType mapiPropertyType = extendedPropertyInfo.GetMapiPropertyType();
			if (extendedPropertyInfo.PropertyTagId != null && extendedPropertyInfo.PropertyTagId != null)
			{
				return new ExtendedPropertyDefinition(extendedPropertyInfo.PropertyTagId.Value, mapiPropertyType);
			}
			if (extendedPropertyInfo.PropertySetId != Guid.Empty)
			{
				if (!string.IsNullOrEmpty(extendedPropertyInfo.PropertyName))
				{
					return new ExtendedPropertyDefinition(extendedPropertyInfo.PropertySetId, extendedPropertyInfo.PropertyName, mapiPropertyType);
				}
				if (extendedPropertyInfo.PropertyId != null && extendedPropertyInfo.PropertyId != null)
				{
					return new ExtendedPropertyDefinition(extendedPropertyInfo.PropertySetId, extendedPropertyInfo.PropertyId.Value, mapiPropertyType);
				}
			}
			throw new ArgumentException(string.Format("Could not convert {0} to ExtendedPropertyDefinition", extendedPropertyInfo.ToString()));
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00034FF0 File Offset: 0x000331F0
		internal static bool VerifyExtendedPropertyInfoAndExtendedPropertyDefinition(ExtendedPropertyInfo epi, ExtendedPropertyDefinition epd)
		{
			if (epi == null && epd == null)
			{
				return true;
			}
			if (epi != null != (epd != null))
			{
				return false;
			}
			if (epi.GetMapiPropertyType() != epd.MapiType)
			{
				return false;
			}
			if (epi.PropertyTagId != null && epi.PropertyTagId != null && epi.PropertyTagId.Equals(epd.Tag))
			{
				return true;
			}
			if (epi.PropertySetId != Guid.Empty)
			{
				if (!string.IsNullOrEmpty(epi.PropertyName) && string.Compare(epi.PropertyName, epd.Name, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
				if (epi.PropertyId != null && epi.PropertyId != null && epd.Id != null && epd.Id != null && epi.PropertyId.Equals(epd.Id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00035110 File Offset: 0x00033310
		internal static string GetSortbyProperty(SortBy sortBy)
		{
			return "item:DateTimeReceived";
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00035117 File Offset: 0x00033317
		internal static SortDirection GetSortDirection(SortBy sortBy)
		{
			if (sortBy.SortOrder == SortOrder.Ascending)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00035124 File Offset: 0x00033324
		internal static SearchPageDirection GetPageDirection(PageDirection direction)
		{
			if (PageDirection.Previous == direction)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0003512D File Offset: 0x0003332D
		internal static TimeZoneInfo GetTimeZoneInfoFromExTimeZone(ExTimeZone timeZone)
		{
			return TimeZoneInfo.Utc;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00035134 File Offset: 0x00033334
		private static SidAndAttributesType[] SidStringAndAttributesConverter(SidStringAndAttributes[] sidStringAndAttributesArray)
		{
			if (sidStringAndAttributesArray == null)
			{
				return null;
			}
			SidAndAttributesType[] array = new SidAndAttributesType[sidStringAndAttributesArray.Length];
			for (int i = 0; i < sidStringAndAttributesArray.Length; i++)
			{
				array[i] = new SidAndAttributesType();
				array[i].SecurityIdentifier = sidStringAndAttributesArray[i].SecurityIdentifier;
				array[i].Attributes = sidStringAndAttributesArray[i].Attributes;
			}
			return array;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00035186 File Offset: 0x00033386
		private void OnSerializeCustomSoapHeaders(XmlWriter writer)
		{
			Util.SerializeIdentityCustomSoapHeaders(DiscoveryEwsClient.securityContextSerializer, writer, this.callerInfo.PrimarySmtpAddress);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000351A0 File Offset: 0x000333A0
		private void Authenticate()
		{
			switch (this.groupId.GroupType)
			{
			case GroupType.CrossServer:
				this.AuthenticateForCrossServer();
				return;
			case GroupType.CrossPremise:
				this.service.ManagementRoles = new ManagementRoles(null, DiscoveryEwsClient.MailboxSearchApplicationRole);
				this.service.Credentials = new OAuthCredentials(OAuthCredentials.GetOAuthCredentialsForAppToken(this.callerInfo.OrganizationId, this.mailboxes[0].GetDomain()));
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00035214 File Offset: 0x00033414
		private void AuthenticateForCrossServer()
		{
			this.service.Credentials = new WebCredentials();
			if (this.callerInfo.IsOpenAsAdmin)
			{
				this.service.OnSerializeCustomSoapHeaders += new CustomXmlSerializationDelegate(this.OnSerializeCustomSoapHeaders);
				return;
			}
			if (this.callerInfo.CommonAccessToken != null)
			{
				this.service.HttpHeaders["X-CommonAccessToken"] = this.callerInfo.CommonAccessToken.Serialize();
				if (this.callerInfo.UserRoles != null || this.callerInfo.ApplicationRoles != null)
				{
					this.service.ManagementRoles = new ManagementRoles(this.callerInfo.UserRoles, this.callerInfo.ApplicationRoles);
				}
			}
		}

		// Token: 0x040008F3 RID: 2291
		internal static readonly string CrossServerUserAgent = "DiscoveryEwsClient.XServer";

		// Token: 0x040008F4 RID: 2292
		internal static readonly string MailboxSearchApplicationRole = "MailboxSearchApplication";

		// Token: 0x040008F5 RID: 2293
		private static XmlSerializer securityContextSerializer = new XmlSerializer(typeof(OpenAsAdminOrSystemServiceType));

		// Token: 0x040008F6 RID: 2294
		private readonly ExchangeService service;

		// Token: 0x040008F7 RID: 2295
		private readonly GroupId groupId;

		// Token: 0x040008F8 RID: 2296
		private readonly MailboxInfo[] mailboxes;

		// Token: 0x040008F9 RID: 2297
		private readonly SearchCriteria searchCriteria;

		// Token: 0x040008FA RID: 2298
		private readonly PagingInfo pagingInfo;

		// Token: 0x040008FB RID: 2299
		private readonly CallerInfo callerInfo;
	}
}
