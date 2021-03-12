using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000288 RID: 648
	internal static class ConversationLoaderHelper
	{
		// Token: 0x060010DC RID: 4316 RVA: 0x00051C50 File Offset: 0x0004FE50
		public static PropertyDefinition[] CalculateInferenceEnabledPropertiesToLoad(PropertyDefinition[] baseSetOfPropertiesToLoad)
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>(baseSetOfPropertiesToLoad);
			list.AddRange(ConversationLoaderHelper.inferencePropertiesToLoad);
			return list.ToArray();
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00051C78 File Offset: 0x0004FE78
		[Conditional("DEBUG")]
		public static void CheckRequestedExtendedProperties(PropertyPath[] additionalProperties)
		{
			if (additionalProperties != null)
			{
				foreach (ExtendedPropertyUri extendedPropertyUri in additionalProperties.OfType<ExtendedPropertyUri>())
				{
					bool flag = false;
					foreach (ExtendedPropertyUri second in ConversationLoaderHelper.specialExtendedPropUris)
					{
						if (ExtendedPropertyUri.AreEqual(extendedPropertyUri, second))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new InvalidExtendedPropertyException(extendedPropertyUri);
					}
				}
			}
		}

		// Token: 0x04000C5B RID: 3163
		private const string MessagingNamespaceGuidString = "41F28F13-83F4-4114-A584-EEDB5A6B0BFF";

		// Token: 0x04000C5C RID: 3164
		private static readonly ExtendedPropertyUri nativeBodyInfoPropertyDefinition = new ExtendedPropertyUri
		{
			PropertyTag = "0x1016",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C5D RID: 3165
		private static readonly ExtendedPropertyUri normalizedSubjectPropertyDefinition = new ExtendedPropertyUri
		{
			PropertyTag = "0xe1d",
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x04000C5E RID: 3166
		public static PropertyDefinition[] MandatoryConversationPropertiesToLoad = new PropertyDefinition[]
		{
			StoreObjectSchema.ItemClass,
			ItemSchema.Id,
			StoreObjectSchema.ChangeKey,
			StoreObjectSchema.ParentItemId,
			StoreObjectSchema.LastModifiedTime,
			ItemSchema.ReceivedTime,
			ItemSchema.InternetMessageId,
			MessageItemSchema.IsRead,
			MessageItemSchema.HasBeenSubmitted,
			ItemSchema.Categories,
			ItemSchema.ItemColor,
			ItemSchema.IsToDoItem,
			ItemSchema.FlagStatus,
			ItemSchema.FlagRequest,
			TaskSchema.StartDate,
			TaskSchema.DueDate,
			ItemSchema.CompleteDate,
			MessageItemSchema.LastVerbExecuted,
			MessageItemSchema.LastVerbExecutionTime,
			ItemSchema.Size,
			ItemSchema.IconIndex,
			ItemSchema.DocumentId,
			MessageItemSchema.IsDraft
		};

		// Token: 0x04000C5F RID: 3167
		public static PropertyDefinition[] InReplyToPropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.Preview,
			ItemSchema.From,
			ItemSchema.InternetMessageId
		};

		// Token: 0x04000C60 RID: 3168
		public static PropertyDefinition[] ModernConversationMandatoryPropertiesToLoad = new PropertyDefinition[]
		{
			MessageItemSchema.ReplyToBlobExists,
			MessageItemSchema.ReplyToNamesExists,
			InternalSchema.EffectiveRights,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04000C61 RID: 3169
		public static PropertyInformation[] ModernConversationOptionalPropertiesToLoad = new PropertyInformation[]
		{
			MessageSchema.LikeCount,
			MessageSchema.Likers,
			ItemSchema.SupportsSideConversation
		};

		// Token: 0x04000C62 RID: 3170
		public static PropertyDefinition[] NonMandatoryPropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.Subject,
			MessageItemSchema.MessageInConflict,
			ItemSchema.From,
			ItemSchema.Sender,
			ItemSchema.HasAttachment,
			MessageItemSchema.IsDraft,
			ItemSchema.Importance,
			ItemSchema.Sensitivity,
			ItemSchema.IsClassified,
			MessageItemSchema.IsReadReceiptPending,
			ItemSchema.BlockStatus,
			ItemSchema.EdgePcl,
			ItemSchema.LinkEnabled,
			ItemSchema.IsResponseRequested,
			MessageItemSchema.VoiceMessageAttachmentOrder,
			MessageItemSchema.RequireProtectedPlayOnPhone,
			StoreObjectSchema.IsRestricted,
			StoreObjectSchema.PolicyTag,
			ItemSchema.RetentionDate,
			ConversationLoaderHelper.nativeBodyInfoPropertyDefinition.ToPropertyDefinition(),
			ConversationLoaderHelper.normalizedSubjectPropertyDefinition.ToPropertyDefinition(),
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.LastModifiedTime,
			MessageItemSchema.IsReadReceiptRequested,
			MessageItemSchema.IsDeliveryReceiptRequested,
			ItemSchema.InstanceKey,
			ItemSchema.Fid,
			MessageItemSchema.MID,
			StoreObjectSchema.CreationTime,
			ItemSchema.SentTime,
			ItemSchema.ReceivedOrRenewTime,
			ItemSchema.RetentionDate,
			MessageItemSchema.TextMessageDeliveryStatus,
			MessageItemSchema.SharingInstanceGuid,
			ItemSchema.DisplayTo,
			ItemSchema.DisplayCc,
			MessageItemSchema.ReplyToBlobExists,
			MessageItemSchema.ReplyToNamesExists,
			MessageItemSchema.ReplyToNames,
			MessageItemSchema.MessageBccMe,
			MessageItemSchema.VotingBlob,
			MessageItemSchema.VotingResponse,
			ItemSchema.RichContent,
			MessageItemSchema.IsGroupEscalationMessage
		};

		// Token: 0x04000C63 RID: 3171
		private static PropertyDefinition[] inferencePropertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.IsClutter
		};

		// Token: 0x04000C64 RID: 3172
		public static PropertyDefinition[] ComplianceProperties = new PropertyDefinition[]
		{
			ItemSchema.IsClassified,
			ItemSchema.ClassificationGuid,
			ItemSchema.ClassificationDescription,
			ItemSchema.Classification,
			ItemSchema.ClassificationKeep
		};

		// Token: 0x04000C65 RID: 3173
		public static PropertyDefinition[] VoiceMailProperties = new PropertyDefinition[]
		{
			MessageItemSchema.MessageAudioNotes,
			MessageItemSchema.VoiceMessageDuration,
			MessageItemSchema.VoiceMessageAttachmentOrder,
			MessageItemSchema.PstnCallbackTelephoneNumber
		};

		// Token: 0x04000C66 RID: 3174
		public static PropertyDefinition[] ApprovalRequestProperties = new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalDecision,
			MessageItemSchema.ApprovalDecisionMaker,
			MessageItemSchema.ApprovalDecisionTime
		};

		// Token: 0x04000C67 RID: 3175
		public static PropertyDefinition[] ReminderMessageProperties = new PropertyDefinition[]
		{
			ReminderMessageSchema.ReminderText,
			CalendarItemBaseSchema.Location,
			ReminderMessageSchema.ReminderStartTime,
			ReminderMessageSchema.ReminderEndTime
		};

		// Token: 0x04000C68 RID: 3176
		public static PropertyDefinition[] SingleRecipientProperties = new PropertyDefinition[]
		{
			ItemSchema.From,
			ItemSchema.Sender,
			MessageItemSchema.ReceivedRepresenting,
			MessageItemSchema.ReceivedBy
		};

		// Token: 0x04000C69 RID: 3177
		public static PropertyDefinition[] ChangeHighlightingProperties = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.ChangeHighlight,
			CalendarItemBaseSchema.OldLocation,
			MeetingRequestSchema.OldStartWhole,
			MeetingRequestSchema.OldEndWhole
		};

		// Token: 0x04000C6A RID: 3178
		public static PropertyDefinition[] EntityExtractionPropeties = new PropertyDefinition[]
		{
			ItemSchema.ExtractedAddresses,
			ItemSchema.ExtractedContacts,
			ItemSchema.ExtractedEmails,
			ItemSchema.ExtractedMeetings,
			ItemSchema.ExtractedPhones,
			ItemSchema.ExtractedTasks,
			ItemSchema.ExtractedUrls
		};

		// Token: 0x04000C6B RID: 3179
		public static PropertyDefinition[] MeetingMessageProperties = new PropertyDefinition[]
		{
			CalendarItemBaseSchema.IsOrganizer,
			MeetingMessageSchema.IsOutOfDate
		};

		// Token: 0x04000C6C RID: 3180
		public static PropertyDefinition[] InferenceReasonsProperties = new PropertyDefinition[]
		{
			ItemSchema.InferencePredictedReplyForwardReasons,
			ItemSchema.InferencePredictedDeleteReasons,
			ItemSchema.InferencePredictedIgnoreReasons
		};

		// Token: 0x04000C6D RID: 3181
		private static ExtendedPropertyUri voiceMessageAttachmentOrder = new ExtendedPropertyUri
		{
			PropertyTag = "0x6805",
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x04000C6E RID: 3182
		private static ExtendedPropertyUri pstnCallbackTelephoneNumber = new ExtendedPropertyUri
		{
			PropertyName = "PstnCallbackTelephoneNumber",
			DistinguishedPropertySetId = DistinguishedPropertySet.UnifiedMessaging,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x04000C6F RID: 3183
		private static ExtendedPropertyUri voiceMessageDuration = new ExtendedPropertyUri
		{
			PropertyTag = "0x6801",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C70 RID: 3184
		private static ExtendedPropertyUri isClassified = new ExtendedPropertyUri
		{
			PropertyId = 34229,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x04000C71 RID: 3185
		private static ExtendedPropertyUri classificationGuid = new ExtendedPropertyUri
		{
			PropertyId = 34232,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x04000C72 RID: 3186
		private static ExtendedPropertyUri classification = new ExtendedPropertyUri
		{
			PropertyId = 34230,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x04000C73 RID: 3187
		private static ExtendedPropertyUri classificationDescription = new ExtendedPropertyUri
		{
			PropertyId = 34231,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.String
		};

		// Token: 0x04000C74 RID: 3188
		private static ExtendedPropertyUri classificationKeep = new ExtendedPropertyUri
		{
			PropertyId = 34234,
			DistinguishedPropertySetId = DistinguishedPropertySet.Common,
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x04000C75 RID: 3189
		private static ExtendedPropertyUri sharingInstanceGuid = new ExtendedPropertyUri
		{
			PropertyId = 35356,
			DistinguishedPropertySetId = DistinguishedPropertySet.Sharing,
			PropertyType = MapiPropertyType.CLSID
		};

		// Token: 0x04000C76 RID: 3190
		private static ExtendedPropertyUri messageBccMe = new ExtendedPropertyUri
		{
			PropertyName = "MessageBccMe",
			PropertySetId = "41F28F13-83F4-4114-A584-EEDB5A6B0BFF",
			PropertyType = MapiPropertyType.Boolean
		};

		// Token: 0x04000C77 RID: 3191
		private static ExtendedPropertyUri retentionFlags = new ExtendedPropertyUri
		{
			PropertyTag = "0x301d",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C78 RID: 3192
		private static ExtendedPropertyUri retentionPeriod = new ExtendedPropertyUri
		{
			PropertyTag = "0x301a",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C79 RID: 3193
		private static ExtendedPropertyUri archivePeriod = new ExtendedPropertyUri
		{
			PropertyTag = "0x301e",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C7A RID: 3194
		private static ExtendedPropertyUri lastVerbExecuted = new ExtendedPropertyUri
		{
			PropertyTag = "0x1081",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C7B RID: 3195
		private static ExtendedPropertyUri lastVerbExecutionTime = new ExtendedPropertyUri
		{
			PropertyTag = "0x1082",
			PropertyType = MapiPropertyType.SystemTime
		};

		// Token: 0x04000C7C RID: 3196
		private static ExtendedPropertyUri documentId = new ExtendedPropertyUri
		{
			PropertyTag = "0x6815",
			PropertyType = MapiPropertyType.Integer
		};

		// Token: 0x04000C7D RID: 3197
		private static List<ExtendedPropertyUri> specialExtendedPropUris = new List<ExtendedPropertyUri>
		{
			ConversationLoaderHelper.normalizedSubjectPropertyDefinition,
			ConversationLoaderHelper.nativeBodyInfoPropertyDefinition,
			ConversationLoaderHelper.voiceMessageAttachmentOrder,
			ConversationLoaderHelper.pstnCallbackTelephoneNumber,
			ConversationLoaderHelper.voiceMessageDuration,
			ConversationLoaderHelper.isClassified,
			ConversationLoaderHelper.classificationGuid,
			ConversationLoaderHelper.classification,
			ConversationLoaderHelper.classificationDescription,
			ConversationLoaderHelper.classificationKeep,
			ConversationLoaderHelper.sharingInstanceGuid,
			ConversationLoaderHelper.messageBccMe,
			ConversationLoaderHelper.retentionFlags,
			ConversationLoaderHelper.retentionPeriod,
			ConversationLoaderHelper.archivePeriod,
			ConversationLoaderHelper.lastVerbExecuted,
			ConversationLoaderHelper.lastVerbExecutionTime,
			ConversationLoaderHelper.documentId
		};
	}
}
