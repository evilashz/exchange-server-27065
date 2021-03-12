using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200001E RID: 30
	internal static class RuleActionTranslator
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000FDA4 File Offset: 0x0000DFA4
		internal static RuleAction Translate(StoreSession session, RuleAction dataRuleAction)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(dataRuleAction, "dataRuleAction");
			switch (dataRuleAction.ActionType)
			{
			case RuleActionType.Move:
			{
				RuleAction.MoveAction moveAction = (RuleAction.MoveAction)dataRuleAction;
				byte[] storeEntryId = RuleActionTranslator.CheckAndWrapStoreEntryId(moveAction.DestinationStoreEntryId);
				return new RuleAction.MoveAction(0U, moveAction.UserFlags, moveAction.DestinationFolderId != null, storeEntryId, RuleActionTranslator.GenerateDestinationFolderEntryId(session, moveAction));
			}
			case RuleActionType.Copy:
			{
				RuleAction.CopyAction copyAction = (RuleAction.CopyAction)dataRuleAction;
				byte[] storeEntryId2 = RuleActionTranslator.CheckAndWrapStoreEntryId(copyAction.DestinationStoreEntryId);
				return new RuleAction.CopyAction(0U, copyAction.UserFlags, copyAction.DestinationFolderId != null, storeEntryId2, RuleActionTranslator.GenerateDestinationFolderEntryId(session, copyAction));
			}
			case RuleActionType.Reply:
			{
				RuleAction.ReplyAction replyAction = (RuleAction.ReplyAction)dataRuleAction;
				StoreId replyTemplateFolderId;
				StoreId replyTemplateMessageId;
				RuleActionTranslator.GetReplyTemplateIds(session, replyAction.ReplyTemplateMessageId, out replyTemplateFolderId, out replyTemplateMessageId);
				return new RuleAction.ReplyAction((uint)RuleActionTranslator.TranslateReplyFlags(replyAction.Flags), replyAction.UserFlags, replyTemplateFolderId, replyTemplateMessageId, replyAction.ReplyTemplateGuid);
			}
			case RuleActionType.OutOfOfficeReply:
			{
				RuleAction.OutOfOfficeReplyAction outOfOfficeReplyAction = (RuleAction.OutOfOfficeReplyAction)dataRuleAction;
				StoreId replyTemplateFolderId2;
				StoreId replyTemplateMessageId2;
				RuleActionTranslator.GetReplyTemplateIds(session, outOfOfficeReplyAction.ReplyTemplateMessageId, out replyTemplateFolderId2, out replyTemplateMessageId2);
				return new RuleAction.OutOfOfficeReplyAction(0U, outOfOfficeReplyAction.UserFlags, replyTemplateFolderId2, replyTemplateMessageId2, outOfOfficeReplyAction.ReplyTemplateGuid);
			}
			case RuleActionType.DeferAction:
			{
				RuleAction.DeferAction deferAction = (RuleAction.DeferAction)dataRuleAction;
				return new RuleAction.DeferAction(0U, deferAction.UserFlags, deferAction.Data);
			}
			case RuleActionType.Bounce:
			{
				RuleAction.BounceAction bounceAction = (RuleAction.BounceAction)dataRuleAction;
				return new RuleAction.BounceAction(0U, bounceAction.UserFlags, bounceAction.BounceCode);
			}
			case RuleActionType.Forward:
			{
				RuleAction.ForwardAction forwardAction = (RuleAction.ForwardAction)dataRuleAction;
				return new RuleAction.ForwardAction((uint)RuleActionTranslator.TranslateForwardFlags(forwardAction.Flags), forwardAction.UserFlags, RuleActionTranslator.TranslateRecipients(session, forwardAction.Recipients));
			}
			case RuleActionType.Delegate:
			{
				RuleAction.DelegateAction delegateAction = (RuleAction.DelegateAction)dataRuleAction;
				return new RuleAction.DelegateAction(0U, delegateAction.UserFlags, RuleActionTranslator.TranslateRecipients(session, delegateAction.Recipients));
			}
			case RuleActionType.Tag:
			{
				RuleAction.TagAction tagAction = (RuleAction.TagAction)dataRuleAction;
				bool flag = true;
				PropertyTag propertyTag = MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<NativeStorePropertyDefinition>(session, new NativeStorePropertyDefinition[]
				{
					tagAction.PropertyDefinition
				}, flag).First<PropertyTag>();
				PropertyValue propertyValue = MEDSPropertyTranslator.TranslatePropertyValue(session, propertyTag, tagAction.PropertyValue, flag);
				return new RuleAction.TagAction(0U, tagAction.UserFlags, propertyValue);
			}
			case RuleActionType.Delete:
			{
				RuleAction.DeleteAction deleteAction = (RuleAction.DeleteAction)dataRuleAction;
				return new RuleAction.DeleteAction(0U, deleteAction.UserFlags);
			}
			case RuleActionType.MarkAsRead:
			{
				RuleAction.MarkAsReadAction markAsReadAction = (RuleAction.MarkAsReadAction)dataRuleAction;
				return new RuleAction.MarkAsReadAction(0U, markAsReadAction.UserFlags);
			}
			default:
				throw new ArgumentException(string.Format("Invalid action type {0}.", dataRuleAction.ActionType));
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00010004 File Offset: 0x0000E204
		internal static RuleAction Translate(StoreSession session, RuleAction ruleAction)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(ruleAction, "ruleAction");
			switch (ruleAction.ActionType)
			{
			case RuleActionType.Move:
			{
				RuleAction.MoveAction moveAction = (RuleAction.MoveAction)ruleAction;
				if (moveAction.FolderIsInThisStore)
				{
					return new RuleAction.MoveAction(moveAction.UserFlags, RuleActionTranslator.GenerateDestinationStoreObjectId(session, moveAction));
				}
				return new RuleAction.MoveAction(moveAction.UserFlags, moveAction.DestinationStoreEntryId, moveAction.DestinationFolderId);
			}
			case RuleActionType.Copy:
			{
				RuleAction.CopyAction copyAction = (RuleAction.CopyAction)ruleAction;
				if (copyAction.FolderIsInThisStore)
				{
					return new RuleAction.CopyAction(copyAction.UserFlags, RuleActionTranslator.GenerateDestinationStoreObjectId(session, copyAction));
				}
				return new RuleAction.CopyAction(copyAction.UserFlags, copyAction.DestinationStoreEntryId, copyAction.DestinationFolderId);
			}
			case RuleActionType.Reply:
			{
				RuleAction.ReplyAction replyAction = (RuleAction.ReplyAction)ruleAction;
				StoreObjectId storeObjectId = RuleActionTranslator.CreateReplyTemplateMessageId(session, replyAction.ReplyTemplateFolderId, replyAction.ReplyTemplateMessageId);
				Guid replyTemplateGuid = replyAction.ReplyTemplateGuid;
				if (session.IsPublicFolderSession && storeObjectId != null && replyAction.ReplyTemplateGuid == Guid.Empty)
				{
					using (Item item = Item.Bind(session, storeObjectId, RuleActionTranslator.ReplyTemplateGuidPropertyArray))
					{
						replyTemplateGuid = Guid.NewGuid();
						item.OpenAsReadWrite();
						item.SafeSetProperty(ItemSchema.ReplyTemplateId, replyTemplateGuid.ToByteArray());
						item.Save(SaveMode.NoConflictResolutionForceSave);
					}
				}
				return new RuleAction.ReplyAction(replyAction.UserFlags, RuleActionTranslator.TranslateReplyFlags((RuleAction.ReplyAction.ReplyFlags)replyAction.Flavor), storeObjectId, replyTemplateGuid);
			}
			case RuleActionType.OutOfOfficeReply:
			{
				RuleAction.OutOfOfficeReplyAction outOfOfficeReplyAction = (RuleAction.OutOfOfficeReplyAction)ruleAction;
				return new RuleAction.OutOfOfficeReplyAction(outOfOfficeReplyAction.UserFlags, RuleActionTranslator.CreateReplyTemplateMessageId(session, outOfOfficeReplyAction.ReplyTemplateFolderId, outOfOfficeReplyAction.ReplyTemplateMessageId), outOfOfficeReplyAction.ReplyTemplateGuid);
			}
			case RuleActionType.DeferAction:
			{
				RuleAction.DeferAction deferAction = (RuleAction.DeferAction)ruleAction;
				return new RuleAction.DeferAction(deferAction.UserFlags, deferAction.Data);
			}
			case RuleActionType.Bounce:
			{
				RuleAction.BounceAction bounceAction = (RuleAction.BounceAction)ruleAction;
				return new RuleAction.BounceAction(bounceAction.UserFlags, bounceAction.BounceCode);
			}
			case RuleActionType.Forward:
			{
				RuleAction.ForwardAction forwardAction = (RuleAction.ForwardAction)ruleAction;
				return new RuleAction.ForwardAction(forwardAction.UserFlags, RuleActionTranslator.TranslateRecipients(session, forwardAction.Recipients), RuleActionTranslator.TranslateForwardFlags((RuleAction.ForwardAction.ForwardFlags)forwardAction.Flavor));
			}
			case RuleActionType.Delegate:
			{
				RuleAction.DelegateAction delegateAction = (RuleAction.DelegateAction)ruleAction;
				return new RuleAction.DelegateAction(delegateAction.UserFlags, RuleActionTranslator.TranslateRecipients(session, delegateAction.Recipients));
			}
			case RuleActionType.Tag:
			{
				RuleAction.TagAction tagAction = (RuleAction.TagAction)ruleAction;
				NativeStorePropertyDefinition[] array;
				if (!MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(session, session.Mailbox.CoreObject.PropertyBag, new PropertyTag[]
				{
					tagAction.PropertyValue.PropertyTag
				}, out array))
				{
					throw new RopExecutionException(string.Format("Property {0} cannot be resolved", tagAction.PropertyValue.PropertyTag), ErrorCode.UnsupportedProperty);
				}
				NativeStorePropertyDefinition propertyDefinition = array[0];
				object propertyValue = MEDSPropertyTranslator.TranslatePropertyValue(session, tagAction.PropertyValue);
				return new RuleAction.TagAction(tagAction.UserFlags, propertyDefinition, propertyValue);
			}
			case RuleActionType.Delete:
			{
				RuleAction.DeleteAction deleteAction = (RuleAction.DeleteAction)ruleAction;
				return new RuleAction.DeleteAction(deleteAction.UserFlags);
			}
			case RuleActionType.MarkAsRead:
			{
				RuleAction.MarkAsReadAction markAsReadAction = (RuleAction.MarkAsReadAction)ruleAction;
				return new RuleAction.MarkAsReadAction(markAsReadAction.UserFlags);
			}
			default:
				throw new ArgumentException(string.Format("Invalid action type {0}.", ruleAction.ActionType));
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00010330 File Offset: 0x0000E530
		private static StoreObjectId CreateReplyTemplateMessageId(StoreSession session, StoreId folderId, StoreId messageId)
		{
			if (messageId != StoreId.Empty)
			{
				return session.IdConverter.CreateMessageId(folderId, messageId);
			}
			return null;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00010360 File Offset: 0x0000E560
		private static void GetReplyTemplateIds(StoreSession session, StoreObjectId storeObjectId, out StoreId folderId, out StoreId messageId)
		{
			folderId = StoreId.Empty;
			messageId = StoreId.Empty;
			if (storeObjectId != null)
			{
				folderId = new StoreId(session.IdConverter.GetFidFromId(storeObjectId));
				messageId = new StoreId(session.IdConverter.GetMidFromMessageId(storeObjectId));
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000103B4 File Offset: 0x0000E5B4
		private static RuleAction.ForwardActionBase.ActionRecipient[] TranslateRecipients(StoreSession session, IList<RuleAction.ForwardActionBase.ActionRecipient> dataRecipients)
		{
			Util.ThrowOnNullArgument(dataRecipients, "dataRecipients");
			RuleAction.ForwardActionBase.ActionRecipient[] array = new RuleAction.ForwardActionBase.ActionRecipient[dataRecipients.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RuleActionTranslator.TranslateRecipient(session, dataRecipients[i]);
			}
			return array;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00010400 File Offset: 0x0000E600
		private static RuleAction.ForwardActionBase.ActionRecipient TranslateRecipient(StoreSession session, RuleAction.ForwardActionBase.ActionRecipient dataRecipient)
		{
			Util.ThrowOnNullArgument(dataRecipient, "dataRecipient");
			bool useUnicodeType = true;
			IEnumerable<PropertyTag> enumerable = MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<NativeStorePropertyDefinition>(session, dataRecipient.PropertyDefinitions, useUnicodeType);
			PropertyValue[] array = new PropertyValue[dataRecipient.PropertyDefinitions.Count];
			int num = 0;
			foreach (PropertyTag propertyTag in enumerable)
			{
				array[num] = MEDSPropertyTranslator.TranslatePropertyValue(session, propertyTag, dataRecipient.PropertyValues[num], true);
				num++;
			}
			return new RuleAction.ForwardActionBase.ActionRecipient(0, array);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000104AC File Offset: 0x0000E6AC
		private static RuleAction.ForwardActionBase.ActionRecipient[] TranslateRecipients(StoreSession session, IList<RuleAction.ForwardActionBase.ActionRecipient> recipients)
		{
			Util.ThrowOnNullArgument(recipients, "recipients");
			RuleAction.ForwardActionBase.ActionRecipient[] array = new RuleAction.ForwardActionBase.ActionRecipient[recipients.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = RuleActionTranslator.TranslateRecipient(session, recipients[i]);
			}
			return array;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000104F8 File Offset: 0x0000E6F8
		private static RuleAction.ForwardActionBase.ActionRecipient TranslateRecipient(StoreSession session, RuleAction.ForwardActionBase.ActionRecipient recipient)
		{
			Util.ThrowOnNullArgument(recipient, "recipient");
			PropertyTag[] array = new PropertyTag[recipient.Properties.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = recipient.Properties[i].PropertyTag;
			}
			NativeStorePropertyDefinition[] propertyDefinitions;
			if (!MEDSPropertyTranslator.TryGetPropertyDefinitionsFromPropertyTags(session, session.Mailbox.CoreObject.PropertyBag, array, out propertyDefinitions))
			{
				throw new RopExecutionException("Failed to resolve all properties in a recipient within a rule.", (ErrorCode)2147942487U);
			}
			object[] array2 = new object[recipient.Properties.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = MEDSPropertyTranslator.TranslatePropertyValue(session, recipient.Properties[j]);
			}
			return new RuleAction.ForwardActionBase.ActionRecipient(propertyDefinitions, array2);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000105C8 File Offset: 0x0000E7C8
		private static byte[] GenerateDestinationFolderEntryId(StoreSession session, RuleAction.MoveCopyActionBase action)
		{
			Util.ThrowOnNullArgument(action, "action");
			if (action.ExternalDestinationFolderId != null)
			{
				return action.ExternalDestinationFolderId;
			}
			StoreId storeId = new StoreId(session.IdConverter.GetFidFromId(action.DestinationFolderId));
			return ServerIdConverter.MakeOurServerId(storeId, 0L, 0);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00010618 File Offset: 0x0000E818
		private static StoreObjectId GenerateDestinationStoreObjectId(StoreSession session, RuleAction.MoveCopyActionBase action)
		{
			Util.ThrowOnNullArgument(action, "action");
			byte[] array = ServerIdConverter.MakeEntryIdFromServerId(session, action.DestinationFolderId);
			StoreObjectId result;
			try
			{
				result = StoreObjectId.FromProviderSpecificId(array);
			}
			catch (CorruptDataException)
			{
				throw new RopExecutionException(string.Format("Invalid DestinationFolderId: {0}.", array), (ErrorCode)2147942487U);
			}
			return result;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00010670 File Offset: 0x0000E870
		private static byte[] CheckAndWrapStoreEntryId(byte[] entryId)
		{
			if (RuleActionTranslator.IsMDBEntryId(entryId))
			{
				entryId = RuleActionTranslator.WrapStoreEntryId(entryId);
			}
			return entryId;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00010683 File Offset: 0x0000E883
		private static bool IsMDBEntryId(byte[] entryId)
		{
			return entryId != null && (RuleActionTranslator.IsMuidInEntryId(RuleActionTranslator.muidMDBPrivate, entryId) || RuleActionTranslator.IsMuidInEntryId(RuleActionTranslator.muidMDBPublic, entryId));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000106A8 File Offset: 0x0000E8A8
		private static bool IsMuidInEntryId(byte[] muid, byte[] entryId)
		{
			if (entryId.Length > muid.Length + 4)
			{
				for (int i = 0; i < muid.Length; i++)
				{
					if (entryId[i + 4] != muid[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000106DC File Offset: 0x0000E8DC
		private static byte[] WrapStoreEntryId(byte[] unwrappedEntryId)
		{
			byte[] array = new byte[unwrappedEntryId.Length + RuleActionTranslator.wrappedEntryIdPrefix.Length];
			Buffer.BlockCopy(RuleActionTranslator.wrappedEntryIdPrefix, 0, array, 0, RuleActionTranslator.wrappedEntryIdPrefix.Length);
			Buffer.BlockCopy(unwrappedEntryId, 0, array, RuleActionTranslator.wrappedEntryIdPrefix.Length, unwrappedEntryId.Length);
			return array;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00010724 File Offset: 0x0000E924
		private static RuleAction.ForwardAction.ForwardFlags TranslateForwardFlags(RuleAction.ForwardAction.ForwardFlags dataForwardFlags)
		{
			RuleAction.ForwardAction.ForwardFlags forwardFlags = RuleAction.ForwardAction.ForwardFlags.None;
			if ((dataForwardFlags & RuleAction.ForwardAction.ForwardFlags.DoNotChangeMessage) == RuleAction.ForwardAction.ForwardFlags.DoNotChangeMessage)
			{
				forwardFlags |= RuleAction.ForwardAction.ForwardFlags.DoNotChangeMessage;
			}
			if ((dataForwardFlags & RuleAction.ForwardAction.ForwardFlags.ForwardAsAttachment) == RuleAction.ForwardAction.ForwardFlags.ForwardAsAttachment)
			{
				forwardFlags |= RuleAction.ForwardAction.ForwardFlags.ForwardAsAttachment;
			}
			if ((dataForwardFlags & RuleAction.ForwardAction.ForwardFlags.PreserveSender) == RuleAction.ForwardAction.ForwardFlags.PreserveSender)
			{
				forwardFlags |= RuleAction.ForwardAction.ForwardFlags.PreserveSender;
			}
			if ((dataForwardFlags & RuleAction.ForwardAction.ForwardFlags.SendSmsAlert) == RuleAction.ForwardAction.ForwardFlags.SendSmsAlert)
			{
				forwardFlags |= RuleAction.ForwardAction.ForwardFlags.SendSmsAlert;
			}
			return forwardFlags;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001075C File Offset: 0x0000E95C
		private static RuleAction.ForwardAction.ForwardFlags TranslateForwardFlags(RuleAction.ForwardAction.ForwardFlags forwardFlags)
		{
			RuleAction.ForwardAction.ForwardFlags forwardFlags2 = RuleAction.ForwardAction.ForwardFlags.None;
			if ((forwardFlags & RuleAction.ForwardAction.ForwardFlags.DoNotChangeMessage) == RuleAction.ForwardAction.ForwardFlags.DoNotChangeMessage)
			{
				forwardFlags2 |= RuleAction.ForwardAction.ForwardFlags.DoNotChangeMessage;
			}
			if ((forwardFlags & RuleAction.ForwardAction.ForwardFlags.ForwardAsAttachment) == RuleAction.ForwardAction.ForwardFlags.ForwardAsAttachment)
			{
				forwardFlags2 |= RuleAction.ForwardAction.ForwardFlags.ForwardAsAttachment;
			}
			if ((forwardFlags & RuleAction.ForwardAction.ForwardFlags.PreserveSender) == RuleAction.ForwardAction.ForwardFlags.PreserveSender)
			{
				forwardFlags2 |= RuleAction.ForwardAction.ForwardFlags.PreserveSender;
			}
			if ((forwardFlags & RuleAction.ForwardAction.ForwardFlags.SendSmsAlert) == RuleAction.ForwardAction.ForwardFlags.SendSmsAlert)
			{
				forwardFlags2 |= RuleAction.ForwardAction.ForwardFlags.SendSmsAlert;
			}
			return forwardFlags2;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00010794 File Offset: 0x0000E994
		private static RuleAction.ReplyAction.ReplyFlags TranslateReplyFlags(RuleAction.ReplyAction.ReplyFlags replyFlags)
		{
			RuleAction.ReplyAction.ReplyFlags replyFlags2 = RuleAction.ReplyAction.ReplyFlags.None;
			if ((replyFlags & RuleAction.ReplyAction.ReplyFlags.DoNotSendToOriginator) == RuleAction.ReplyAction.ReplyFlags.DoNotSendToOriginator)
			{
				replyFlags2 |= RuleAction.ReplyAction.ReplyFlags.DoNotSendToOriginator;
			}
			if ((replyFlags & RuleAction.ReplyAction.ReplyFlags.UseStockReplyTemplate) == RuleAction.ReplyAction.ReplyFlags.UseStockReplyTemplate)
			{
				replyFlags2 |= RuleAction.ReplyAction.ReplyFlags.UseStockReplyTemplate;
			}
			return replyFlags2;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000107B8 File Offset: 0x0000E9B8
		private static RuleAction.ReplyAction.ReplyFlags TranslateReplyFlags(RuleAction.ReplyAction.ReplyFlags dataReplyFlags)
		{
			RuleAction.ReplyAction.ReplyFlags replyFlags = RuleAction.ReplyAction.ReplyFlags.None;
			if ((dataReplyFlags & RuleAction.ReplyAction.ReplyFlags.DoNotSendToOriginator) == RuleAction.ReplyAction.ReplyFlags.DoNotSendToOriginator)
			{
				replyFlags |= RuleAction.ReplyAction.ReplyFlags.DoNotSendToOriginator;
			}
			if ((dataReplyFlags & RuleAction.ReplyAction.ReplyFlags.UseStockReplyTemplate) == RuleAction.ReplyAction.ReplyFlags.UseStockReplyTemplate)
			{
				replyFlags |= RuleAction.ReplyAction.ReplyFlags.UseStockReplyTemplate;
			}
			return replyFlags;
		}

		// Token: 0x0400007D RID: 125
		internal static PropertyDefinition[] ReplyTemplateGuidPropertyArray = new PropertyDefinition[]
		{
			ItemSchema.ReplyTemplateId
		};

		// Token: 0x0400007E RID: 126
		private static readonly byte[] wrappedEntryIdPrefix = new byte[]
		{
			0,
			0,
			0,
			0,
			56,
			161,
			187,
			16,
			5,
			229,
			16,
			26,
			161,
			187,
			8,
			0,
			43,
			42,
			86,
			194,
			0,
			0,
			69,
			77,
			83,
			77,
			68,
			66,
			46,
			68,
			76,
			76,
			0,
			0,
			0,
			0
		};

		// Token: 0x0400007F RID: 127
		private static readonly byte[] muidMDBPublic = new byte[]
		{
			28,
			131,
			2,
			16,
			170,
			102,
			17,
			205,
			155,
			200,
			0,
			170,
			0,
			47,
			196,
			90
		};

		// Token: 0x04000080 RID: 128
		private static readonly byte[] muidMDBPrivate = new byte[]
		{
			27,
			85,
			250,
			32,
			170,
			102,
			17,
			205,
			155,
			200,
			0,
			170,
			0,
			47,
			196,
			90
		};
	}
}
