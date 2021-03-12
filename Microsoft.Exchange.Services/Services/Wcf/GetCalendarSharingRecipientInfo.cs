using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200095B RID: 2395
	internal sealed class GetCalendarSharingRecipientInfo : CalendarActionBase<GetCalendarSharingRecipientInfoResponse>
	{
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x060044FD RID: 17661 RVA: 0x000EFF90 File Offset: 0x000EE190
		// (set) Token: 0x060044FE RID: 17662 RVA: 0x000EFF98 File Offset: 0x000EE198
		private GetCalendarSharingRecipientInfoRequest Request { get; set; }

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x060044FF RID: 17663 RVA: 0x000EFFA1 File Offset: 0x000EE1A1
		// (set) Token: 0x06004500 RID: 17664 RVA: 0x000EFFA9 File Offset: 0x000EE1A9
		private ExchangePrincipal AccessingPrincipal { get; set; }

		// Token: 0x06004501 RID: 17665 RVA: 0x000EFFCC File Offset: 0x000EE1CC
		public GetCalendarSharingRecipientInfo(MailboxSession session, GetCalendarSharingRecipientInfoRequest request, ExchangePrincipal accessingPrincipal) : base(session)
		{
			this.Request = request;
			this.sharingProviderLocator = new SharingProviderLocator(accessingPrincipal, () => session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid));
			this.AccessingPrincipal = accessingPrincipal;
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x000F0020 File Offset: 0x000EE220
		public override GetCalendarSharingRecipientInfoResponse Execute()
		{
			this.TraceDebug("Internal Execute", new object[0]);
			List<CalendarSharingRecipientInfo> list = new List<CalendarSharingRecipientInfo>(this.Request.Recipients.Count);
			foreach (KeyValuePair<SmtpAddress, ADRecipient> keyValuePair in this.Request.Recipients)
			{
				this.TraceDebug("Get Response object for {0}", new object[]
				{
					keyValuePair.Key
				});
				list.Add(this.CreateResponseRecipientInfo(keyValuePair.Key, keyValuePair.Value));
			}
			return new GetCalendarSharingRecipientInfoResponse
			{
				Recipients = list.ToArray()
			};
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000F00EC File Offset: 0x000EE2EC
		private CalendarSharingRecipientInfo CreateResponseRecipientInfo(SmtpAddress address, ADRecipient adRecipient)
		{
			EmailAddressWrapper emailAddressWrapper;
			if (adRecipient != null)
			{
				emailAddressWrapper = ResolveNames.EmailAddressWrapperFromRecipient(adRecipient);
			}
			else
			{
				emailAddressWrapper = new EmailAddressWrapper();
				emailAddressWrapper.EmailAddress = address.ToString();
				emailAddressWrapper.Name = emailAddressWrapper.EmailAddress;
				emailAddressWrapper.RoutingType = "SMTP";
				emailAddressWrapper.MailboxType = MailboxHelper.MailboxTypeType.Unknown.ToString();
			}
			CalendarSharingRecipientInfo calendarSharingRecipientInfo = new CalendarSharingRecipientInfo
			{
				EmailAddress = emailAddressWrapper
			};
			MailboxSession mailboxSession = base.MailboxSession;
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
			bool isSharingDefaultCalendar = defaultFolderId.Equals(this.Request.CalendarStoreId);
			SharingProvider sharingProvider;
			DetailLevelEnumType detailLevelEnumType;
			if (this.sharingProviderLocator.TryGetProvider(address, adRecipient, new FrontEndLocator(), out sharingProvider, out detailLevelEnumType))
			{
				if (sharingProvider == SharingProvider.SharingProviderInternal)
				{
					calendarSharingRecipientInfo.IsInsideOrganization = true;
					calendarSharingRecipientInfo.HandlerType = SharingHandlerType.Internal.ToString();
					calendarSharingRecipientInfo.AllowedDetailLevels = CalendarSharingPermissionsUtils.CalculateAllowedDetailLevels(detailLevelEnumType, isSharingDefaultCalendar, PermissionSecurityPrincipal.SecurityPrincipalType.ADRecipientPrincipal);
				}
				else if (sharingProvider == SharingProvider.SharingProviderExternal)
				{
					calendarSharingRecipientInfo.IsInsideOrganization = CalendarSharingPermissionsUtils.CheckIfRecipientDomainIsInternal(this.AccessingPrincipal.MailboxInfo.OrganizationId, address.Domain);
					calendarSharingRecipientInfo.HandlerType = SharingHandlerType.Federated.ToString();
					calendarSharingRecipientInfo.AllowedDetailLevels = CalendarSharingPermissionsUtils.CalculateAllowedDetailLevels(detailLevelEnumType, isSharingDefaultCalendar, PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal);
				}
				else if (sharingProvider == SharingProvider.SharingProviderPublishReach)
				{
					calendarSharingRecipientInfo.IsInsideOrganization = CalendarSharingPermissionsUtils.CheckIfRecipientDomainIsInternal(this.AccessingPrincipal.MailboxInfo.OrganizationId, address.Domain);
					calendarSharingRecipientInfo.HandlerType = SharingHandlerType.Publishing.ToString();
					calendarSharingRecipientInfo.AllowedDetailLevels = CalendarSharingPermissionsUtils.CalculateAllowedDetailLevels(detailLevelEnumType, isSharingDefaultCalendar, PermissionSecurityPrincipal.SecurityPrincipalType.ExternalUserPrincipal);
				}
				else
				{
					if (sharingProvider != SharingProvider.SharingProviderConsumer)
					{
						throw new NotSupportedException(sharingProvider.ToString());
					}
					calendarSharingRecipientInfo.IsInsideOrganization = false;
					calendarSharingRecipientInfo.HandlerType = SharingHandlerType.Consumer.ToString();
					calendarSharingRecipientInfo.AllowedDetailLevels = new string[]
					{
						CalendarSharingDetailLevel.AvailabilityOnly.ToString(),
						CalendarSharingDetailLevel.LimitedDetails.ToString(),
						CalendarSharingDetailLevel.FullDetails.ToString(),
						CalendarSharingDetailLevel.Editor.ToString(),
						CalendarSharingDetailLevel.Delegate.ToString()
					};
				}
				CalendarSharingDetailLevel calendarSharingDetailLevel = CalendarSharingPermissionsUtils.ConvertToCalendarSharingDetailLevelEnum(detailLevelEnumType, isSharingDefaultCalendar);
				if (calendarSharingDetailLevel > CalendarSharingDetailLevel.FullDetails)
				{
					calendarSharingDetailLevel = CalendarSharingDetailLevel.FullDetails;
				}
				calendarSharingRecipientInfo.CurrentDetailLevel = calendarSharingDetailLevel.ToString();
			}
			return calendarSharingRecipientInfo;
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000F0319 File Offset: 0x000EE519
		private void TraceDebug(string messageFormat, params object[] args)
		{
			ExTraceGlobals.GetCalendarSharingRecipientInfoCallTracer.TraceDebug((long)this.GetHashCode(), messageFormat, args);
		}

		// Token: 0x04002825 RID: 10277
		private readonly SharingProviderLocator sharingProviderLocator;
	}
}
