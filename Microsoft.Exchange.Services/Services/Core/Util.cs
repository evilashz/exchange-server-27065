using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Security.Application;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000B5 RID: 181
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Util
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0001B1FC File Offset: 0x000193FC
		internal static bool IsValidSmtpAddress(string smtpAddress)
		{
			return !string.IsNullOrEmpty(smtpAddress) && SmtpAddress.IsValidSmtpAddress(smtpAddress);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001B20E File Offset: 0x0001940E
		internal static void ValidateSmtpAddress(string smtpAddress)
		{
			if (!Util.IsValidSmtpAddress(smtpAddress))
			{
				throw new InvalidSmtpAddressException();
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001B21E File Offset: 0x0001941E
		internal static bool GetItemBlockStatus(Item item, bool blockExternalImages, bool blockExternalImagesIfSenderUntrusted)
		{
			return Util.GetItemBlockStatus(StaticParticipantResolver.DefaultInstance, item, blockExternalImages, blockExternalImagesIfSenderUntrusted);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001B230 File Offset: 0x00019430
		internal static bool GetItemBlockStatus(IParticipantResolver participantResolver, Item item, bool blockExternalImages, bool blockExternalImagesIfSenderUntrusted)
		{
			bool result = false;
			if (blockExternalImages)
			{
				result = true;
			}
			else if (blockExternalImagesIfSenderUntrusted)
			{
				BlockStatus blockStatus = BlockStatus.DontKnow;
				if (item != null)
				{
					blockStatus = item.GetValueOrDefault<BlockStatus>(ItemSchema.BlockStatus, BlockStatus.DontKnow);
				}
				if (blockStatus != BlockStatus.NoNeverAgain)
				{
					result = !Util.IsSafeSender(participantResolver, item);
				}
			}
			return result;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001B26C File Offset: 0x0001946C
		internal static bool GetItemBlockStatus(IParticipantResolver participantResolver, ItemPart itemPart, bool blockExternalImages, bool blockExternalImagesIfSenderUntrusted)
		{
			bool result = false;
			if (blockExternalImages)
			{
				result = true;
			}
			else if (blockExternalImagesIfSenderUntrusted)
			{
				BlockStatus blockStatus = BlockStatus.DontKnow;
				if (itemPart != null)
				{
					blockStatus = itemPart.StorePropertyBag.GetValueOrDefault<BlockStatus>(ItemSchema.BlockStatus, BlockStatus.DontKnow);
				}
				if (blockStatus != BlockStatus.NoNeverAgain)
				{
					Participant valueOrDefault = itemPart.StorePropertyBag.GetValueOrDefault<Participant>(ItemSchema.Sender, null);
					bool valueOrDefault2 = itemPart.StorePropertyBag.GetValueOrDefault<bool>(MessageItemSchema.IsDraft, false);
					result = !Util.IsSafeSender(participantResolver, valueOrDefault, valueOrDefault2);
				}
			}
			return result;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001B2D4 File Offset: 0x000194D4
		internal static bool IsSafeSender(IParticipantResolver participantResolver, Item item)
		{
			bool result = false;
			MessageItem messageItem = item as MessageItem;
			if (messageItem != null)
			{
				result = Util.IsSafeSender(participantResolver, messageItem.Sender, messageItem.IsDraft);
			}
			CalendarItem calendarItem = item as CalendarItem;
			if (calendarItem != null)
			{
				result = Util.IsSafeSender(participantResolver, calendarItem.Organizer, false);
			}
			return result;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001B31C File Offset: 0x0001951C
		internal static bool IsTrustedSender(string senderEmailAddress)
		{
			JunkEmailRule junkEmailRuleForAccessingPrincipal = Util.GetJunkEmailRuleForAccessingPrincipal();
			return junkEmailRuleForAccessingPrincipal != null && junkEmailRuleForAccessingPrincipal.TrustedSenderEmailCollection.Contains(senderEmailAddress);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001B340 File Offset: 0x00019540
		internal static JunkEmailRule GetJunkEmailRuleForAccessingPrincipal()
		{
			if (EWSSettings.JunkEmailRule == null)
			{
				MailboxSession mailboxIdentityMailboxSession = CallContext.Current.SessionCache.GetMailboxIdentityMailboxSession();
				if (mailboxIdentityMailboxSession != null)
				{
					EWSSettings.JunkEmailRule = mailboxIdentityMailboxSession.JunkEmailRule;
				}
			}
			return EWSSettings.JunkEmailRule;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001B378 File Offset: 0x00019578
		internal static T GetItemPropertyOrDefault<T>(object[] row, PropertyDefinition propertyDefinition, T defaultValue, Dictionary<PropertyDefinition, int> propertyMap)
		{
			int num = propertyMap[propertyDefinition];
			object obj = row[num];
			if (!(obj is T))
			{
				return defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001B3A4 File Offset: 0x000195A4
		internal static Dictionary<PropertyDefinition, int> LoadPropertyMap(PropertyDefinition[] properties)
		{
			Dictionary<PropertyDefinition, int> dictionary = new Dictionary<PropertyDefinition, int>();
			for (int i = 0; i < properties.Length; i++)
			{
				dictionary[properties[i]] = i;
			}
			return dictionary;
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001B3D0 File Offset: 0x000195D0
		internal static ExDateTime GetLocalTime()
		{
			ExTimeZone timeZone;
			if (EWSSettings.RequestTimeZone != null)
			{
				timeZone = EWSSettings.RequestTimeZone;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[Util::GetLocalTime] EWSSettings.RequestTimeZone is null");
				timeZone = ExTimeZone.UtcTimeZone;
			}
			return ExDateTime.GetNow(timeZone);
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001B40C File Offset: 0x0001960C
		internal static bool IsArchiveMailbox(StoreSession session)
		{
			MailboxSession mailboxSession = session as MailboxSession;
			return mailboxSession != null && mailboxSession.MailboxOwner.MailboxInfo.IsArchive;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001B435 File Offset: 0x00019635
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001B441 File Offset: 0x00019641
		internal static AttachmentCollection GetEffectiveAttachmentCollection(Item item, bool getAttachmentCollectionWhenClientSmimeInstalled = false)
		{
			if (Util.IsSMimeButNotSecureSign(item))
			{
				item = Util.GetSignedSmimeItemIfNeeded(item, getAttachmentCollectionWhenClientSmimeInstalled);
			}
			return IrmUtils.GetAttachmentCollection(item);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0001B45A File Offset: 0x0001965A
		internal static Body GetEffectiveBody(Item item)
		{
			if (Util.IsSMimeButNotSecureSign(item))
			{
				item = Util.GetSignedSmimeItemIfNeeded(item, false);
			}
			return IrmUtils.GetBody(item);
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0001B474 File Offset: 0x00019674
		public static ConversationType LoadConversationUsingConversationId(ConversationId conversationId, string conversationShapeName, TargetFolderId conversationFolderId, IdConverter idConverter, int hashCode, RequestDetailsLogger protocolLogFromCallContext)
		{
			ConversationType result = null;
			if (!string.IsNullOrEmpty(conversationShapeName))
			{
				ConversationResponseShape responseShape = Global.ResponseShapeResolver.GetResponseShape<ConversationResponseShape>(conversationShapeName, null, null);
				if (responseShape != null)
				{
					IdAndSession idAndSession = idConverter.ConvertFolderIdToIdAndSession(conversationFolderId.BaseFolderId, IdConverter.ConvertOption.IgnoreChangeKey);
					PropertyListForViewRowDeterminer propertyListForViewRowDeterminer = PropertyListForViewRowDeterminer.BuildForConversation(responseShape);
					PropertyDefinition[] propertiesToFetch = propertyListForViewRowDeterminer.GetPropertiesToFetch();
					QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ConversationId, conversationId);
					using (Folder folder = Folder.Bind(idAndSession.Session, idAndSession.Id, null))
					{
						using (QueryResult queryResult = folder.ConversationItemQuery(queryFilter, null, propertiesToFetch))
						{
							BasePageResult basePageResult = BasePagingType.ApplyPostQueryPaging(queryResult, null);
							ConversationType[] array = basePageResult.View.ConvertToConversationObjects(propertiesToFetch, propertyListForViewRowDeterminer, idAndSession, protocolLogFromCallContext);
							if (array.Length > 0)
							{
								result = array[0];
								if (array.Length > 1)
								{
									ExTraceGlobals.CreateItemCallTracer.TraceError<int, string>((long)hashCode, "CreateUpdateItemCommandBase.PopulateServiceObjectWithConversationIfNecessary: {0} conversations found with store conversation id {1} where only 1 conversation was expected.", array.Length, conversationId.ToString());
								}
							}
							else
							{
								ExTraceGlobals.CreateItemCallTracer.TraceError<string>((long)hashCode, "CreateUpdateItemCommandBase.PopulateServiceObjectWithConversationIfNecessary: No conversations found with store conversation id {0}, skipping returning conversation.", conversationId.ToString());
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001B590 File Offset: 0x00019790
		public static ClutterState GetMailboxClutterState(MailboxSession session)
		{
			VariantConfigurationSnapshot configuration = session.MailboxOwner.GetConfiguration();
			return new ClutterState
			{
				IsClassificationEnabled = ClutterUtilities.IsClassificationEnabled(session, configuration),
				IsClutterEligible = ClutterUtilities.IsClutterEligible(session, configuration),
				IsClutterEnabled = ClutterUtilities.IsClutterEnabled(session, configuration)
			};
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001B5DC File Offset: 0x000197DC
		private static Item GetSignedSmimeItemIfNeeded(Item item, bool getAttachmentCollectionWhenClientSmimeInstalled = false)
		{
			int hashCode = item.GetHashCode();
			CallContext callContext = CallContext.Current;
			if (callContext.IsOwa && (!callContext.IsSmimeInstalled || (getAttachmentCollectionWhenClientSmimeInstalled && ObjectClass.IsSmimeClearSigned(item.ClassName))))
			{
				Item result;
				if (Util.TryOpenSMimeContent(item, out result))
				{
					ExTraceGlobals.ItemAlgorithmTracer.TraceInformation(hashCode, 0L, "GetSignedSmimeItemIfAvailable: Caller requested to open signed smime message and the message is clear or opaque signed, signed message opened and returned");
					return result;
				}
				ExTraceGlobals.ItemAlgorithmTracer.TraceWarning(hashCode, 0L, "GetSmimeAttachmentCollection: We were not able to open the signed smime message, using the outer attachment collection instead");
			}
			else
			{
				ExTraceGlobals.ItemAlgorithmTracer.TraceInformation(hashCode, 0L, "GetSmimeAttachmentCollection: Caller did not request to open signed smime message, returning null");
			}
			return item;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001B65B File Offset: 0x0001985B
		private static bool IsSMimeButNotSecureSign(Item message)
		{
			return !ObjectClass.IsOfClass(message.ClassName, "IPM.Note.Secure.Sign") && ObjectClass.IsSmime(message.ClassName);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001B67C File Offset: 0x0001987C
		private static bool IsOpaqueSigned(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			string className = item.ClassName;
			return (ObjectClass.IsOfClass(className, "IPM.Note.Secure") || (ObjectClass.IsSmime(className) && !ObjectClass.IsSmimeClearSigned(className))) && ConvertUtils.IsMessageOpaqueSigned(item);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001B6C4 File Offset: 0x000198C4
		private static bool TryOpenSMimeContent(Item smimeMessage, out Item signedSmimeItem)
		{
			bool flag = ItemConversion.TryOpenSMimeContent(smimeMessage as MessageItem, CallContext.Current.DefaultDomain.DomainName.ToString(), out signedSmimeItem);
			if (flag)
			{
				CallContext.Current.ObjectsToDisposeList.Add(signedSmimeItem);
			}
			return flag;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001B708 File Offset: 0x00019908
		internal static bool IsSafeSender(IParticipantResolver resolver, Participant sender, bool isDraft)
		{
			if (PropertyCommand.InMemoryProcessOnly)
			{
				return true;
			}
			bool result = false;
			MailboxSession mailboxIdentityMailboxSession = CallContext.Current.SessionCache.GetMailboxIdentityMailboxSession();
			if (mailboxIdentityMailboxSession != null)
			{
				if (isDraft)
				{
					result = true;
				}
				else if (sender != null)
				{
					SmtpAddress smtpAddress = resolver.ResolveToSmtpAddress(sender);
					result = (CalendarSharingPermissionsUtils.CheckIfRecipientDomainIsInternal(mailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.OrganizationId, smtpAddress.Domain) || Util.IsTrustedSender(smtpAddress.ToString()));
				}
			}
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001B780 File Offset: 0x00019980
		internal static string EncodeForAntiXSS(string s)
		{
			return Encoder.HtmlEncode(s, true);
		}
	}
}
