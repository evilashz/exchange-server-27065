using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DD9 RID: 3545
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SharingProviderHandlerPublish : SharingProviderHandler
	{
		// Token: 0x06007A03 RID: 31235 RVA: 0x0021BC06 File Offset: 0x00219E06
		internal SharingProviderHandlerPublish()
		{
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x0021BC0E File Offset: 0x00219E0E
		internal override void FillSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			sharingMessageProvider.BrowseUrl = context.BrowseUrl.ToString();
			if (context.ICalUrl != null)
			{
				sharingMessageProvider.ICalUrl = context.ICalUrl.ToString();
			}
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x0021BC3C File Offset: 0x00219E3C
		internal override void ParseSharingMessageProvider(SharingContext context, SharingMessageProvider sharingMessageProvider)
		{
			Uri uri = null;
			if (!PublishingUrl.IsAbsoluteUriString(sharingMessageProvider.BrowseUrl, out uri))
			{
				ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: BrowseUrl is invalid: {1}", context.UserLegacyDN, sharingMessageProvider.BrowseUrl);
				throw new InvalidSharingDataException("BrowseUrl", sharingMessageProvider.BrowseUrl);
			}
			context.BrowseUrl = sharingMessageProvider.BrowseUrl;
			if (context.DataType == SharingDataType.Calendar)
			{
				if (string.IsNullOrEmpty(sharingMessageProvider.ICalUrl))
				{
					ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: ICalUrl is missing: {1}", context.UserLegacyDN, sharingMessageProvider.ICalUrl);
					throw new InvalidSharingDataException("ICalUrl", string.Empty);
				}
				if (!PublishingUrl.IsAbsoluteUriString(sharingMessageProvider.ICalUrl, out uri))
				{
					ExTraceGlobals.SharingTracer.TraceError<string, string>((long)this.GetHashCode(), "{0}: ICalUrl is invalid: {1}", context.UserLegacyDN, sharingMessageProvider.ICalUrl);
					throw new InvalidSharingDataException("ICalUrl", sharingMessageProvider.ICalUrl);
				}
				context.ICalUrl = sharingMessageProvider.ICalUrl;
			}
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x0021BD38 File Offset: 0x00219F38
		protected override bool InternalValidateCompatibility(Folder folderToShare)
		{
			CalendarFolder calendarFolder = folderToShare as CalendarFolder;
			return calendarFolder != null && calendarFolder.IsExchangePublishedCalendar;
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x0021BD58 File Offset: 0x00219F58
		protected override ValidRecipient InternalCheckOneRecipient(ADRecipient mailboxOwner, string recipient, IRecipientSession recipientSession)
		{
			ADRecipient adrecipient = recipientSession.FindByProxyAddress(new SmtpProxyAddress(recipient, false));
			if (adrecipient != null)
			{
				ExTraceGlobals.SharingTracer.TraceDebug<ADRecipient, string>((long)this.GetHashCode(), "{0}: {1} is found from AD in {0}'s organization.", mailboxOwner, recipient);
				return null;
			}
			return new ValidRecipient(recipient, adrecipient);
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x0021BD98 File Offset: 0x00219F98
		protected override PerformInvitationResults InternalPerformInvitation(MailboxSession mailboxSession, SharingContext context, ValidRecipient[] recipients, IFrontEndLocator frontEndLocator)
		{
			using (Folder folder = Folder.Bind(mailboxSession, context.FolderId))
			{
				context.PopulateUrls(folder);
			}
			return new PerformInvitationResults(recipients);
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x0021BDDC File Offset: 0x00219FDC
		protected override void InternalPerformRevocation(MailboxSession mailboxSession, SharingContext context)
		{
			throw new NotSupportedException("cannot revoke published folder");
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x0021BDE8 File Offset: 0x00219FE8
		protected override SubscribeResults InternalPerformSubscribe(MailboxSession mailboxSession, SharingContext context)
		{
			if (context.ICalUrl == null)
			{
				throw new NotSupportedException("cannot subscribe to non ical url");
			}
			PublishingSubscriptionData newSubscription = this.CreateSubscriptionData(context);
			return WebCalendar.InternalSubscribe(mailboxSession, newSubscription, context.InitiatorSmtpAddress, context.InitiatorName);
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x0021BE23 File Offset: 0x0021A023
		private PublishingSubscriptionData CreateSubscriptionData(SharingContext context)
		{
			if (context.ICalUrl == null)
			{
				throw new InvalidSharingDataException("ICalUrl", string.Empty);
			}
			return new PublishingSubscriptionData(context.DataType.PublishName, new Uri(context.ICalUrl, UriKind.Absolute), context.FolderName, null);
		}
	}
}
