﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000122 RID: 290
	internal static class ManageInboxRule
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x0002F2EC File Offset: 0x0002D4EC
		public static void ProcessRecord(Action action, ManageInboxRule.ThrowTerminatingErrorDelegate handleError, object identity)
		{
			try
			{
				action();
			}
			catch (InboxRuleOperationException ex)
			{
				handleError(new InvalidOperationException(ex.Message), ErrorCategory.InvalidOperation, identity);
			}
			catch (RulesTooBigException)
			{
				handleError(new InvalidOperationException(Strings.ErrorInboxRuleTooBig), ErrorCategory.InvalidOperation, identity);
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002F350 File Offset: 0x0002D550
		public static void VerifyRange<T>(T min, T max, bool allowEqual, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler, LocalizedString errorMessage)
		{
			if (min == null || max == null)
			{
				return;
			}
			int num = ((IComparable)((object)min)).CompareTo(max);
			if (!allowEqual && num == 0)
			{
				errorHandler(new LocalizedException(errorMessage), ErrorCategory.InvalidArgument, null);
			}
			if (num > 0)
			{
				errorHandler(new LocalizedException(errorMessage), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002F3B0 File Offset: 0x0002D5B0
		public static MailboxFolder ResolveMailboxFolder(MailboxFolderIdParameter folderId, DataAccessHelper.GetDataObjectDelegate getUserHandler, DataAccessHelper.GetDataObjectDelegate getFolderHandler, IRecipientSession resolveUserSession, ADSessionSettings sessionSettings, ADUser adUser, ISecurityAccessToken userToken, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler)
		{
			if (!ManageInboxRule.TryValidateFolderId(folderId, getUserHandler, getFolderHandler, resolveUserSession, adUser, errorHandler))
			{
				return null;
			}
			MailboxFolder result;
			using (MailboxFolderDataProvider mailboxFolderDataProvider = new MailboxFolderDataProvider(sessionSettings, adUser, userToken, "ResolveMailboxFolder"))
			{
				result = (MailboxFolder)getFolderHandler(folderId, mailboxFolderDataProvider, null, null, new LocalizedString?(Strings.ErrorMailboxFolderNotFound(folderId.ToString())), new LocalizedString?(Strings.ErrorMailboxFolderNotUnique(folderId.ToString())));
			}
			return result;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002F42C File Offset: 0x0002D62C
		public static MailboxFolder ResolveMailboxFolder(MailboxFolderIdParameter folderId, DataAccessHelper.GetDataObjectDelegate getUserHandler, DataAccessHelper.GetDataObjectDelegate getFolderHandler, IRecipientSession resolveUserSession, ADSessionSettings sessionSettings, ADUser adUser, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler)
		{
			if (!ManageInboxRule.TryValidateFolderId(folderId, getUserHandler, getFolderHandler, resolveUserSession, adUser, errorHandler))
			{
				return null;
			}
			MailboxFolder result;
			using (MailboxFolderDataProvider mailboxFolderDataProvider = new MailboxFolderDataProvider(sessionSettings, adUser, "ResolveMailboxFolder"))
			{
				result = (MailboxFolder)getFolderHandler(folderId, mailboxFolderDataProvider, null, null, new LocalizedString?(Strings.ErrorMailboxFolderNotFound(folderId.ToString())), new LocalizedString?(Strings.ErrorMailboxFolderNotUnique(folderId.ToString())));
			}
			return result;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
		public static ADRecipientOrAddress[] ResolveRecipients(IList<RecipientIdParameter> recipientIDs, DataAccessHelper.GetDataObjectDelegate getRecipientObject, IRecipientSession recipientSession, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler)
		{
			if (recipientIDs == null || recipientIDs.Count == 0)
			{
				return null;
			}
			ADRecipientOrAddress[] array = new ADRecipientOrAddress[recipientIDs.Count];
			int num = 0;
			foreach (RecipientIdParameter recipientIdParameter in recipientIDs)
			{
				try
				{
					recipientIdParameter.SearchWithDisplayName = false;
					ADRecipient adEntry = (ADRecipient)getRecipientObject(recipientIdParameter, recipientSession, null, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())));
					array[num++] = new ADRecipientOrAddress(new Participant(adEntry));
				}
				catch (ManagementObjectNotFoundException)
				{
					MimeRecipient mimeRecipient = null;
					try
					{
						mimeRecipient = MimeRecipient.Parse(recipientIdParameter.RawIdentity, AddressParserFlags.IgnoreComments | AddressParserFlags.AllowSquareBrackets);
					}
					catch (MimeException)
					{
					}
					if (mimeRecipient == null || string.IsNullOrEmpty(mimeRecipient.Email) || !SmtpAddress.IsValidSmtpAddress(mimeRecipient.Email))
					{
						errorHandler(new LocalizedException(Strings.ErrorInboxRuleUserInvalid(recipientIdParameter.ToString())), ErrorCategory.InvalidArgument, null);
					}
					string text = string.Empty;
					try
					{
						text = mimeRecipient.DisplayName;
					}
					catch (MimeException)
					{
					}
					if (string.IsNullOrEmpty(text))
					{
						text = mimeRecipient.Email;
					}
					array[num++] = new ADRecipientOrAddress(new Participant(text, mimeRecipient.Email, "smtp"));
				}
			}
			return array;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002F64C File Offset: 0x0002D84C
		public static MessageClassification[] ResolveMessageClassifications(ICollection<MessageClassificationIdParameter> ids, IConfigurationSession configurationSession, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler)
		{
			if (ids == null || ids.Count == 0)
			{
				return new MessageClassification[0];
			}
			ADObjectId rootId = MessageClassificationIdParameter.DefaultRoot(configurationSession);
			List<MessageClassification> list = new List<MessageClassification>(ids.Count);
			foreach (MessageClassificationIdParameter messageClassificationIdParameter in ids)
			{
				IEnumerable<MessageClassification> objects = messageClassificationIdParameter.GetObjects<MessageClassification>(rootId, configurationSession);
				using (IEnumerator<MessageClassification> enumerator2 = objects.GetEnumerator())
				{
					if (!enumerator2.MoveNext())
					{
						errorHandler(new LocalizedException(Strings.InvalidMessageClassification(messageClassificationIdParameter.ToString())), ErrorCategory.InvalidArgument, null);
					}
					do
					{
						list.Add(enumerator2.Current);
					}
					while (enumerator2.MoveNext());
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002F720 File Offset: 0x0002D920
		public static AggregationSubscriptionIdentity[] ResolveSubscriptions(ICollection<AggregationSubscriptionIdentity> ids, ADUser mailboxOwner, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler)
		{
			if (ids == null || ids.Count == 0)
			{
				return new AggregationSubscriptionIdentity[0];
			}
			List<AggregationSubscriptionIdentity> list = new List<AggregationSubscriptionIdentity>(ids.Count);
			PimSubscriptionProxy[] allSubscriptions = InboxRuleDataProvider.GetAllSubscriptions(mailboxOwner);
			List<AggregationSubscriptionIdentity> list2 = new List<AggregationSubscriptionIdentity>(allSubscriptions.Length);
			foreach (PimSubscriptionProxy pimSubscriptionProxy in allSubscriptions)
			{
				list2.Add(pimSubscriptionProxy.Subscription.SubscriptionIdentity);
			}
			foreach (AggregationSubscriptionIdentity aggregationSubscriptionIdentity in ids)
			{
				if (!list2.Contains(aggregationSubscriptionIdentity))
				{
					errorHandler(new LocalizedException(Strings.InvalidSubscription(aggregationSubscriptionIdentity.ToString())), ErrorCategory.InvalidArgument, null);
				}
				else if (!list.Contains(aggregationSubscriptionIdentity))
				{
					list.Add(aggregationSubscriptionIdentity);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002F800 File Offset: 0x0002DA00
		internal static void CleanupInboxRuleDataProvider(IConfigDataProvider provider)
		{
			XsoMailboxDataProviderBase xsoMailboxDataProviderBase = provider as XsoMailboxDataProviderBase;
			if (xsoMailboxDataProviderBase != null)
			{
				xsoMailboxDataProviderBase.Dispose();
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0002F820 File Offset: 0x0002DA20
		private static bool TryValidateFolderId(MailboxFolderIdParameter folderId, DataAccessHelper.GetDataObjectDelegate getUserHandler, DataAccessHelper.GetDataObjectDelegate getFolderHandler, IRecipientSession resolveUserSession, ADUser adUser, ManageInboxRule.ThrowTerminatingErrorDelegate errorHandler)
		{
			if (folderId != null)
			{
				if (folderId.RawOwner != null)
				{
					ADUser aduser = (ADUser)getUserHandler(folderId.RawOwner, resolveUserSession, null, null, new LocalizedString?(Strings.ErrorMailboxNotFound(folderId.RawOwner.ToString())), new LocalizedString?(Strings.ErrorMailboxNotUnique(folderId.RawOwner.ToString())));
					if (!aduser.Identity.Equals(adUser.Identity))
					{
						errorHandler(new LocalizedException(Strings.ErrorConflictingMailboxFolder(adUser.Identity.ToString(), folderId.ToString())), ErrorCategory.InvalidOperation, null);
					}
				}
				if (folderId.InternalMailboxFolderId == null)
				{
					folderId.InternalMailboxFolderId = new Microsoft.Exchange.Data.Storage.Management.MailboxFolderId(adUser.Id, folderId.RawFolderStoreId, folderId.RawFolderPath);
				}
				return true;
			}
			return false;
		}

		// Token: 0x02000123 RID: 291
		// (Invoke) Token: 0x06000A39 RID: 2617
		public delegate void ThrowTerminatingErrorDelegate(Exception exception, ErrorCategory category, object target);
	}
}
