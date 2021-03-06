using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000153 RID: 339
	internal static class JunkEmailUtilities
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x00050DEC File Offset: 0x0004EFEC
		private static bool IsDomain(ref string email)
		{
			switch (email.IndexOf('@'))
			{
			case -1:
				email = "@" + email;
				return true;
			case 0:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00050E2C File Offset: 0x0004F02C
		public static bool IsInternalToOrganization(string email, UserContext userContext)
		{
			RecipientAddress recipientAddress = AnrManager.ResolveAnrString(email, new AnrManager.Options
			{
				ResolveOnlyFromAddressBook = true
			}, userContext);
			return recipientAddress != null && recipientAddress.AddressOrigin == AddressOrigin.Directory && recipientAddress.RecipientType != RecipientType.MailUser && recipientAddress.RecipientType != RecipientType.MailContact;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00050E70 File Offset: 0x0004F070
		private static bool IsUsersEmailOrDomain(string email, bool isDomain, UserContext userContext)
		{
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(Culture.GetUserCulture().LCID, true, ConsistencyMode.IgnoreInvalid, true, userContext);
			ADRecipient adrecipient = null;
			try
			{
				SmtpProxyAddress proxyAddress = new SmtpProxyAddress(userContext.ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), true);
				adrecipient = recipientSession.FindByProxyAddress(proxyAddress);
			}
			catch (NonUniqueRecipientException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "IsUsersEmailOrDomain: NonUniqueRecipientException was thrown by FindByProxyAddress: {0}", ex.Message);
			}
			if (adrecipient == null)
			{
				return false;
			}
			foreach (ProxyAddress proxyAddress2 in adrecipient.EmailAddresses)
			{
				if (proxyAddress2 != null && SmtpAddress.IsValidSmtpAddress(proxyAddress2.AddressString) && proxyAddress2.Prefix == ProxyAddressPrefix.Smtp)
				{
					string smtpAddress = ((SmtpProxyAddress)proxyAddress2).SmtpAddress;
					int num = smtpAddress.IndexOf('@');
					int length = smtpAddress.Length;
					if (string.Equals((!isDomain || num == -1) ? smtpAddress : smtpAddress.Substring(num, length - num), email, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00050FAC File Offset: 0x0004F1AC
		private static bool InternalAdd(JunkEmailRule junkEmailRule, string email, JunkEmailListType junkEmailListType, UserContext userContext, bool isFromOptions, out string message)
		{
			bool result = true;
			message = string.Empty;
			string text = string.Empty;
			bool flag = JunkEmailUtilities.IsDomain(ref email);
			JunkEmailCollection.ValidationProblem validationProblem = JunkEmailCollection.ValidationProblem.NoError;
			try
			{
				switch (junkEmailListType)
				{
				case JunkEmailListType.SafeSenders:
					text = LocalizedStrings.GetNonEncoded(-644781195);
					if (flag)
					{
						validationProblem = junkEmailRule.TrustedSenderDomainCollection.TryAdd(email);
						message = string.Format(LocalizedStrings.GetNonEncoded(-1801060492), email, text);
					}
					else if (JunkEmailUtilities.IsInternalToOrganization(email, userContext))
					{
						message = string.Format(LocalizedStrings.GetNonEncoded(878888369), email, text);
						result = false;
					}
					else
					{
						validationProblem = junkEmailRule.TrustedSenderEmailCollection.TryAdd(email);
						message = string.Format(LocalizedStrings.GetNonEncoded(-1801060492), email, text);
					}
					break;
				case JunkEmailListType.BlockedSenders:
					text = LocalizedStrings.GetNonEncoded(-145011736);
					if (flag)
					{
						validationProblem = junkEmailRule.BlockedSenderDomainCollection.TryAdd(email);
						message = string.Format(LocalizedStrings.GetNonEncoded(-1801060492), email, text);
					}
					else if (JunkEmailUtilities.IsInternalToOrganization(email, userContext))
					{
						message = string.Format(LocalizedStrings.GetNonEncoded(878888369), email, text);
						result = false;
					}
					else
					{
						validationProblem = junkEmailRule.BlockedSenderEmailCollection.TryAdd(email);
						message = string.Format(LocalizedStrings.GetNonEncoded(-1801060492), email, text);
					}
					break;
				case JunkEmailListType.SafeRecipients:
					text = LocalizedStrings.GetNonEncoded(-606405795);
					if (JunkEmailUtilities.IsUsersEmailOrDomain(email, flag, userContext))
					{
						message = string.Format(LocalizedStrings.GetNonEncoded(-1238229754), text);
						result = false;
					}
					else if (flag)
					{
						validationProblem = junkEmailRule.TrustedRecipientDomainCollection.TryAdd(email);
						message = string.Format(LocalizedStrings.GetNonEncoded(-1801060492), email, text);
					}
					else
					{
						validationProblem = junkEmailRule.TrustedRecipientEmailCollection.TryAdd(email);
						message = string.Format(LocalizedStrings.GetNonEncoded(-1801060492), email, text);
					}
					break;
				default:
					throw new OwaInvalidRequestException("Invalid list type");
				}
			}
			catch (JunkEmailValidationException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug(0L, "JunkEmailValidationException was caught by JunkEmailUtilities.InternalAdd.");
				validationProblem = ex.Problem;
			}
			finally
			{
				switch (validationProblem)
				{
				case JunkEmailCollection.ValidationProblem.NoError:
				case JunkEmailCollection.ValidationProblem.Empty:
					goto IL_309;
				case JunkEmailCollection.ValidationProblem.Duplicate:
					message = string.Format(LocalizedStrings.GetNonEncoded(-1222968570), flag ? LocalizedStrings.GetNonEncoded(-520821858) : LocalizedStrings.GetNonEncoded(-1951590110), text);
					goto IL_309;
				case JunkEmailCollection.ValidationProblem.FormatError:
					message = string.Format(LocalizedStrings.GetNonEncoded(488857414), flag ? LocalizedStrings.GetNonEncoded(-520821858) : LocalizedStrings.GetNonEncoded(-1951590110), text, isFromOptions ? LocalizedStrings.GetNonEncoded(-2139153122) : string.Empty);
					result = false;
					goto IL_309;
				case JunkEmailCollection.ValidationProblem.TooBig:
					message = string.Format(LocalizedStrings.GetNonEncoded(1628764363), flag ? LocalizedStrings.GetNonEncoded(-520821858) : LocalizedStrings.GetNonEncoded(-1951590110), text);
					result = false;
					goto IL_309;
				case JunkEmailCollection.ValidationProblem.TooManyEntries:
					message = string.Format(LocalizedStrings.GetNonEncoded(1708451641), flag ? LocalizedStrings.GetNonEncoded(-520821858) : LocalizedStrings.GetNonEncoded(-1951590110), text);
					result = false;
					goto IL_309;
				}
				message = string.Format(LocalizedStrings.GetNonEncoded(1312248603), flag ? LocalizedStrings.GetNonEncoded(-520821858) : LocalizedStrings.GetNonEncoded(-1951590110), text);
				result = false;
				IL_309:;
			}
			return result;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000512F8 File Offset: 0x0004F4F8
		public static bool Add(string email, JunkEmailListType junkEmailListType, UserContext userContext, bool isFromOptions, out string message)
		{
			if (string.IsNullOrEmpty(email))
			{
				throw new ArgumentNullException("email", "email cannot be null or empty");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			JunkEmailRule junkEmailRule = userContext.MailboxSession.JunkEmailRule;
			bool flag = JunkEmailUtilities.InternalAdd(junkEmailRule, email, junkEmailListType, userContext, isFromOptions, out message);
			if (flag)
			{
				junkEmailRule.Save();
			}
			return flag;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00051350 File Offset: 0x0004F550
		private static void InternalRemove(JunkEmailRule junkEmailRule, string[] email, JunkEmailListType junkEmailListType, UserContext userContext)
		{
			switch (junkEmailListType)
			{
			case JunkEmailListType.SafeSenders:
				for (int i = 0; i < email.Length; i++)
				{
					if (JunkEmailUtilities.IsDomain(ref email[i]))
					{
						junkEmailRule.TrustedSenderDomainCollection.Remove(email[i]);
					}
					else
					{
						junkEmailRule.TrustedSenderEmailCollection.Remove(email[i]);
					}
				}
				return;
			case JunkEmailListType.BlockedSenders:
				for (int j = 0; j < email.Length; j++)
				{
					if (JunkEmailUtilities.IsDomain(ref email[j]))
					{
						junkEmailRule.BlockedSenderDomainCollection.Remove(email[j]);
					}
					else
					{
						junkEmailRule.BlockedSenderEmailCollection.Remove(email[j]);
					}
				}
				return;
			case JunkEmailListType.SafeRecipients:
				for (int k = 0; k < email.Length; k++)
				{
					if (JunkEmailUtilities.IsDomain(ref email[k]))
					{
						junkEmailRule.TrustedRecipientDomainCollection.Remove(email[k]);
					}
					else
					{
						junkEmailRule.TrustedRecipientEmailCollection.Remove(email[k]);
					}
				}
				return;
			default:
				throw new OwaInvalidRequestException("Invalid list type");
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0005143C File Offset: 0x0004F63C
		public static void Remove(string[] email, JunkEmailListType junkEmailListType, UserContext userContext)
		{
			if (email == null || email.Length <= 0)
			{
				throw new ArgumentNullException("email", "email cannot be null or empty");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			JunkEmailRule junkEmailRule = userContext.MailboxSession.JunkEmailRule;
			JunkEmailUtilities.InternalRemove(junkEmailRule, email, junkEmailListType, userContext);
			junkEmailRule.Save();
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0005148C File Offset: 0x0004F68C
		public static bool Edit(string oldEmail, string newEmail, JunkEmailListType junkEmailListType, UserContext userContext, bool isFromOptions, out string message)
		{
			if (string.IsNullOrEmpty(oldEmail))
			{
				throw new ArgumentNullException("oldEmail", "oldEmail cannot be null or empty");
			}
			if (string.IsNullOrEmpty(newEmail))
			{
				throw new ArgumentNullException("newEmail", "newEmail cannot be null or empty");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			bool flag = true;
			message = string.Empty;
			string empty = string.Empty;
			JunkEmailRule junkEmailRule = userContext.MailboxSession.JunkEmailRule;
			string[] email = new string[]
			{
				oldEmail
			};
			JunkEmailUtilities.InternalRemove(junkEmailRule, email, junkEmailListType, userContext);
			if (!JunkEmailUtilities.InternalAdd(junkEmailRule, newEmail, junkEmailListType, userContext, isFromOptions, out message))
			{
				flag = false;
			}
			if (flag)
			{
				junkEmailRule.Save();
			}
			return flag;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00051524 File Offset: 0x0004F724
		public static void SaveOptions(bool isEnabled, bool isContactsTrusted, bool safeListsOnly, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			JunkEmailRule junkEmailRule = userContext.MailboxSession.JunkEmailRule;
			junkEmailRule.IsEnabled = isEnabled;
			if (isEnabled)
			{
				if (userContext.IsFeatureEnabled(Feature.Contacts) && junkEmailRule.IsContactsFolderTrusted != isContactsTrusted)
				{
					if (isContactsTrusted)
					{
						Utilities.JunkEmailRuleSynchronizeContactsCache(junkEmailRule);
					}
					else
					{
						junkEmailRule.ClearContactsCache();
					}
				}
				junkEmailRule.TrustedListsOnly = safeListsOnly;
			}
			junkEmailRule.Save();
			userContext.RefreshIsJunkEmailEnabled();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0005158C File Offset: 0x0004F78C
		public static void SetLinkEnabled(Item item, params PropertyDefinition[] prefetchProperties)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			item.OpenAsReadWrite();
			item[ItemSchema.LinkEnabled] = true;
			ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new OwaSaveConflictException(LocalizedStrings.GetNonEncoded(-482397486), conflictResolutionResult);
			}
			item.Load(prefetchProperties);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000515E8 File Offset: 0x0004F7E8
		public static bool IsItemLinkEnabled(IStorePropertyBag storePropertyBag)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			bool result = false;
			object obj = storePropertyBag.TryGetProperty(ItemSchema.LinkEnabled);
			if (!(obj is PropertyError))
			{
				result = (bool)obj;
			}
			return result;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00051624 File Offset: 0x0004F824
		public static bool IsSuspectedPhishingItem(IStorePropertyBag storePropertyBag)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			int itemPhishingLevel = 1;
			object obj = storePropertyBag.TryGetProperty(ItemSchema.EdgePcl);
			if (!(obj is PropertyError))
			{
				itemPhishingLevel = (int)obj;
			}
			return JunkEmailUtilities.IsSuspectedPhishingItem(itemPhishingLevel);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00051662 File Offset: 0x0004F862
		public static bool IsSuspectedPhishingItem(int itemPhishingLevel)
		{
			return itemPhishingLevel < 1 || itemPhishingLevel > 7 || itemPhishingLevel >= (int)Globals.MinimumSuspiciousPhishingLevel;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00051679 File Offset: 0x0004F879
		public static bool IsInJunkEmailFolder(IStorePropertyBag storePropertyBag, bool isEmbeddedItem, UserContext userContext)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return !isEmbeddedItem && Utilities.IsItemInDefaultFolder(storePropertyBag, DefaultFolderType.JunkEmail, userContext.MailboxSession);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000516AC File Offset: 0x0004F8AC
		public static bool IsInJunkEmailFolder(StoreObjectId itemId, UserContext userContext)
		{
			bool result;
			using (Item item = Item.BindAsMessage(userContext.MailboxSession, itemId))
			{
				result = JunkEmailUtilities.IsInJunkEmailFolder(item, false, userContext);
			}
			return result;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000516EC File Offset: 0x0004F8EC
		public static bool IsInJunkEmailFolder(OwaStoreObjectId itemId, UserContext userContext)
		{
			return !itemId.IsPublic && JunkEmailUtilities.IsInJunkEmailFolder(itemId.StoreObjectId, userContext);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00051704 File Offset: 0x0004F904
		public static void SortJunkEmailIds(UserContext userContext, OwaStoreObjectId[] sourceIds, out OwaStoreObjectId[] junkIds, out OwaStoreObjectId[] normalIds)
		{
			List<OwaStoreObjectId> list = new List<OwaStoreObjectId>();
			List<OwaStoreObjectId> list2 = new List<OwaStoreObjectId>();
			for (int i = 0; i < sourceIds.Length; i++)
			{
				try
				{
					if (JunkEmailUtilities.IsInJunkEmailFolder(sourceIds[i].StoreObjectId, userContext))
					{
						list.Add(sourceIds[i]);
					}
					else
					{
						list2.Add(sourceIds[i]);
					}
				}
				catch (ObjectNotFoundException)
				{
				}
			}
			junkIds = list.ToArray();
			normalIds = list2.ToArray();
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00051774 File Offset: 0x0004F974
		public static void SortJunkEmailIds(UserContext userContext, StoreObjectId[] sourceIds, out StoreObjectId[] junkIds, out StoreObjectId[] normalIds)
		{
			List<StoreObjectId> list = new List<StoreObjectId>();
			List<StoreObjectId> list2 = new List<StoreObjectId>();
			for (int i = 0; i < sourceIds.Length; i++)
			{
				try
				{
					if (JunkEmailUtilities.IsInJunkEmailFolder(sourceIds[i], userContext))
					{
						list.Add(sourceIds[i]);
					}
					else
					{
						list2.Add(sourceIds[i]);
					}
				}
				catch (ObjectNotFoundException)
				{
				}
			}
			junkIds = list.ToArray();
			normalIds = list2.ToArray();
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000517E0 File Offset: 0x0004F9E0
		private static bool IsJunkOrPhishing(bool isInJunkEmailFolder, bool isSuspectedPhishingItem, bool itemLinkEnabled)
		{
			return isInJunkEmailFolder || (isSuspectedPhishingItem && !itemLinkEnabled);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000517F0 File Offset: 0x0004F9F0
		public static bool IsJunkOrPhishing(IStorePropertyBag storePropertyBag, bool isEmbeddedItem, UserContext userContext)
		{
			return JunkEmailUtilities.IsJunkOrPhishing(storePropertyBag, isEmbeddedItem, false, userContext);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000517FC File Offset: 0x0004F9FC
		public static bool IsJunkOrPhishing(IStorePropertyBag storePropertyBag, bool isEmbeddedItem, bool forceEnableItemLink, UserContext userContext)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool result = false;
			JunkEmailUtilities.GetJunkEmailPropertiesForItem(storePropertyBag, isEmbeddedItem, forceEnableItemLink, userContext, out flag, out flag2, out flag3, out result);
			return result;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00051840 File Offset: 0x0004FA40
		public static void GetJunkEmailPropertiesForItem(IStorePropertyBag storePropertyBag, bool isEmbedded, bool forceEnableItemLink, UserContext userContext, out bool isInJunkEmailFolder, out bool isSuspectedPhishingItem, out bool itemLinkEnabled, out bool isJunkOrPhishing)
		{
			if (storePropertyBag == null)
			{
				throw new ArgumentNullException("storePropertyBag");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			isInJunkEmailFolder = JunkEmailUtilities.IsInJunkEmailFolder(storePropertyBag, isEmbedded, userContext);
			isSuspectedPhishingItem = JunkEmailUtilities.IsSuspectedPhishingItem(storePropertyBag);
			bool flag = JunkEmailUtilities.IsItemLinkEnabled(storePropertyBag);
			itemLinkEnabled = (forceEnableItemLink || flag);
			isJunkOrPhishing = JunkEmailUtilities.IsJunkOrPhishing(isInJunkEmailFolder, isSuspectedPhishingItem, itemLinkEnabled);
		}
	}
}
