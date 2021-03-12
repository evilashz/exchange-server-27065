using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x020001AE RID: 430
	internal sealed class ItemSchema : Schema
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003A3B4 File Offset: 0x000385B4
		static ItemSchema()
		{
			XmlElementInformation[] xmlElements = new XmlElementInformation[]
			{
				ItemSchema.MimeContent,
				ItemSchema.ItemId,
				ItemSchema.ParentFolderId,
				ItemSchema.ItemClass,
				ItemSchema.Subject,
				ItemSchema.Sensitivity,
				ItemSchema.Body,
				ItemSchema.Attachments,
				ItemSchema.DateTimeReceived,
				ItemSchema.Size,
				ItemSchema.Categories,
				ItemSchema.Importance,
				ItemSchema.InReplyTo,
				ItemSchema.IsSubmitted,
				ItemSchema.IsDraft,
				ItemSchema.IsFromMe,
				ItemSchema.IsResend,
				ItemSchema.IsUnmodified,
				ItemSchema.InternetMessageHeaders,
				ItemSchema.InternetMessageHeader,
				ItemSchema.DateTimeSent,
				ItemSchema.DateTimeCreated,
				ItemSchema.ResponseObjects,
				ItemSchema.ReminderDueBy,
				ItemSchema.ReminderIsSet,
				ItemSchema.ReminderNextTime,
				ItemSchema.ReminderMinutesBeforeStart,
				ItemSchema.DisplayCc,
				ItemSchema.DisplayTo,
				ItemSchema.HasAttachments,
				ItemSchema.ExtendedProperty,
				ItemSchema.Culture,
				ItemSchema.EffectiveRights,
				ItemSchema.LastModifiedName,
				ItemSchema.LastModifiedTime,
				ItemSchema.IsAssociated,
				ItemSchema.WebClientReadFormQueryString,
				ItemSchema.WebClientEditFormQueryString,
				ItemSchema.ConversationId,
				ItemSchema.UniqueBody,
				ItemSchema.PredictedActionReasons,
				ItemSchema.IsClutter,
				ItemSchema.Flag,
				ItemSchema.StoreEntryId,
				ItemSchema.InstanceKey,
				ItemSchema.NormalizedBody,
				ItemSchema.Preview,
				ItemSchema.EntityExtractionResult,
				ItemSchema.PolicyTag,
				ItemSchema.ArchiveTag,
				ItemSchema.RetentionDate,
				ItemSchema.RightsManagementLicenseData,
				ItemSchema.BlockStatus,
				ItemSchema.HasBlockedImages,
				ItemSchema.TextBody,
				ItemSchema.IconIndex,
				ItemSchema.RichContent,
				ItemSchema.MailboxGuid,
				ItemSchema.ReceivedOrRenewTime,
				ItemSchema.WorkingSetSourcePartition,
				ItemSchema.RenewTime,
				ItemSchema.SupportsSideConversation,
				ItemSchema.MimeContentUTF8
			};
			ItemSchema.schema = new ItemSchema(xmlElements, ItemSchema.ItemId);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0003B2EC File Offset: 0x000394EC
		private ItemSchema(XmlElementInformation[] xmlElements, PropertyInformation itemIdPropertyInformation) : base(xmlElements, itemIdPropertyInformation)
		{
			IList<PropertyInformation> propertyInformationListByShapeEnum = base.GetPropertyInformationListByShapeEnum(ShapeEnum.AllProperties);
			propertyInformationListByShapeEnum.Remove(ItemSchema.MimeContent);
			propertyInformationListByShapeEnum.Remove(ItemSchema.InternetMessageHeader);
			propertyInformationListByShapeEnum.Remove(ItemSchema.ExtendedProperty);
			propertyInformationListByShapeEnum.Remove(ItemSchema.UniqueBody);
			propertyInformationListByShapeEnum.Remove(ItemSchema.PredictedActionReasons);
			propertyInformationListByShapeEnum.Remove(ItemSchema.IsClutter);
			propertyInformationListByShapeEnum.Remove(ItemSchema.StoreEntryId);
			propertyInformationListByShapeEnum.Remove(ItemSchema.NormalizedBody);
			propertyInformationListByShapeEnum.Remove(ItemSchema.Preview);
			propertyInformationListByShapeEnum.Remove(ItemSchema.RightsManagementLicenseData);
			propertyInformationListByShapeEnum.Remove(ItemSchema.BlockStatus);
			propertyInformationListByShapeEnum.Remove(ItemSchema.HasBlockedImages);
			propertyInformationListByShapeEnum.Remove(ItemSchema.TextBody);
			propertyInformationListByShapeEnum.Remove(ItemSchema.IconIndex);
			propertyInformationListByShapeEnum.Remove(ItemSchema.ReceivedOrRenewTime);
			propertyInformationListByShapeEnum.Remove(ItemSchema.RenewTime);
			propertyInformationListByShapeEnum.Remove(ItemSchema.MimeContentUTF8);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0003B3D5 File Offset: 0x000395D5
		public static Schema GetSchema()
		{
			return ItemSchema.schema;
		}

		// Token: 0x040008FF RID: 2303
		private static Schema schema;

		// Token: 0x04000900 RID: 2304
		public static readonly PropertyInformation Attachments = new AttachmentsPropertyInformation();

		// Token: 0x04000901 RID: 2305
		public static readonly PropertyInformation Body = new PropertyInformation("Body", ExchangeVersion.Exchange2007, null, new PropertyUri(PropertyUriEnum.Body), new PropertyCommand.CreatePropertyCommand(BodyProperty.CreateCommand));

		// Token: 0x04000902 RID: 2306
		public static readonly PropertyInformation Categories = new ArrayPropertyInformation("Categories", ExchangeVersion.Exchange2007, "String", ItemSchema.Categories, new PropertyUri(PropertyUriEnum.Categories), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x04000903 RID: 2307
		public static readonly PropertyInformation Importance = new PropertyInformation("Importance", ExchangeVersion.Exchange2007, ItemSchema.Importance, new PropertyUri(PropertyUriEnum.Importance), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000904 RID: 2308
		public static readonly PropertyInformation Culture = new PropertyInformation("Culture", ServiceXml.GetFullyQualifiedName("Culture"), "http://schemas.microsoft.com/exchange/services/2006/types", ExchangeVersion.Exchange2007, new PropertyDefinition[]
		{
			CultureProperty.InternetCPID,
			CultureProperty.MessageLocaleID,
			ItemSchema.Codepage
		}, new PropertyUri(PropertyUriEnum.Culture), new PropertyCommand.CreatePropertyCommand(CultureProperty.CreateCommand));

		// Token: 0x04000905 RID: 2309
		public static readonly PropertyInformation DateTimeCreated = new PropertyInformation("DateTimeCreated", ExchangeVersion.Exchange2007, StoreObjectSchema.CreationTime, new PropertyUri(PropertyUriEnum.DateTimeCreated), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000906 RID: 2310
		public static readonly PropertyInformation DisplayCc = new PropertyInformation("DisplayCc", ExchangeVersion.Exchange2007, ItemSchema.DisplayCc, new PropertyUri(PropertyUriEnum.DisplayCc), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000907 RID: 2311
		public static readonly PropertyInformation DisplayTo = new PropertyInformation("DisplayTo", ExchangeVersion.Exchange2007, ItemSchema.DisplayTo, new PropertyUri(PropertyUriEnum.DisplayTo), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000908 RID: 2312
		public static readonly PropertyInformation ExtendedProperty = new ExtendedPropertyInformation();

		// Token: 0x04000909 RID: 2313
		public static readonly PropertyInformation HasAttachments = new PropertyInformation("HasAttachments", ExchangeVersion.Exchange2007, ItemSchema.HasAttachment, new PropertyUri(PropertyUriEnum.HasAttachments), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400090A RID: 2314
		public static readonly PropertyInformation InReplyTo = new PropertyInformation("InReplyTo", ExchangeVersion.Exchange2007, ItemSchema.InReplyTo, new PropertyUri(PropertyUriEnum.InReplyTo), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand));

		// Token: 0x0400090B RID: 2315
		public static readonly PropertyInformation InternetMessageHeader = new InternetMessageHeaderPropertyInformation();

		// Token: 0x0400090C RID: 2316
		public static readonly PropertyInformation InternetMessageHeaders = new PropertyInformation("InternetMessageHeaders", ExchangeVersion.Exchange2007, MessageItemSchema.TransportMessageHeaders, new PropertyUri(PropertyUriEnum.InternetMessageHeaders), new PropertyCommand.CreatePropertyCommand(InternetMessageHeadersProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400090D RID: 2317
		public static readonly PropertyInformation IsAssociated = PropertyInformation.CreateMessageFlagsPropertyInformation("IsAssociated", ExchangeVersion.Exchange2010, PropertyUriEnum.IsAssociated, new PropertyCommand.CreatePropertyCommand(MessageFlagsProperty.CreateIsAssociatedCommand));

		// Token: 0x0400090E RID: 2318
		public static readonly PropertyInformation IsDraft = PropertyInformation.CreateMessageFlagsPropertyInformation("IsDraft", ExchangeVersion.Exchange2007, PropertyUriEnum.IsDraft, new PropertyCommand.CreatePropertyCommand(MessageFlagsProperty.CreateIsDraftCommand));

		// Token: 0x0400090F RID: 2319
		public static readonly PropertyInformation IsFromMe = PropertyInformation.CreateMessageFlagsPropertyInformation("IsFromMe", ExchangeVersion.Exchange2007, PropertyUriEnum.IsFromMe, new PropertyCommand.CreatePropertyCommand(MessageFlagsProperty.CreateIsFromMeCommand));

		// Token: 0x04000910 RID: 2320
		public static readonly PropertyInformation IsResend = PropertyInformation.CreateMessageFlagsPropertyInformation("IsResend", ExchangeVersion.Exchange2007, PropertyUriEnum.IsResend, new PropertyCommand.CreatePropertyCommand(MessageFlagsProperty.CreateIsResendCommand));

		// Token: 0x04000911 RID: 2321
		public static readonly PropertyInformation IsSubmitted = PropertyInformation.CreateMessageFlagsPropertyInformation("IsSubmitted", ExchangeVersion.Exchange2007, PropertyUriEnum.IsSubmitted, new PropertyCommand.CreatePropertyCommand(MessageFlagsProperty.CreateIsSubmittedCommand));

		// Token: 0x04000912 RID: 2322
		public static readonly PropertyInformation IsUnmodified = PropertyInformation.CreateMessageFlagsPropertyInformation("IsUnmodified", ExchangeVersion.Exchange2007, PropertyUriEnum.IsUnmodified, new PropertyCommand.CreatePropertyCommand(MessageFlagsProperty.CreateIsUnmodifiedCommand));

		// Token: 0x04000913 RID: 2323
		public static readonly PropertyInformation ItemClass = new PropertyInformation("ItemClass", ExchangeVersion.Exchange2007, StoreObjectSchema.ItemClass, new PropertyUri(PropertyUriEnum.ItemClass), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000914 RID: 2324
		public static readonly PropertyInformation ItemId = new PropertyInformation("ItemId", ExchangeVersion.Exchange2007, ItemSchema.Id, new PropertyUri(PropertyUriEnum.ItemId), new PropertyCommand.CreatePropertyCommand(ItemIdProperty.CreateCommand));

		// Token: 0x04000915 RID: 2325
		public static readonly PropertyInformation LastModifiedName = new PropertyInformation("LastModifiedName", ExchangeVersion.Exchange2007SP1, ItemSchema.LastModifiedBy, new PropertyUri(PropertyUriEnum.LastModifiedName), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000916 RID: 2326
		public static readonly PropertyInformation LastModifiedTime = new PropertyInformation("LastModifiedTime", ExchangeVersion.Exchange2007SP1, StoreObjectSchema.LastModifiedTime, new PropertyUri(PropertyUriEnum.ItemLastModifiedTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000917 RID: 2327
		public static readonly PropertyInformation MimeContent = new PropertyInformation("MimeContent", ExchangeVersion.Exchange2007, null, new PropertyUri(PropertyUriEnum.MimeContent), new PropertyCommand.CreatePropertyCommand(MimeContentProperty.CreateCommand));

		// Token: 0x04000918 RID: 2328
		public static readonly PropertyInformation MimeContentUTF8 = new PropertyInformation("MimeContentUTF8", ExchangeVersion.Exchange2013_SP1, null, new PropertyUri(PropertyUriEnum.MimeContentUTF8), new PropertyCommand.CreatePropertyCommand(MimeContentProperty.CreateCommand));

		// Token: 0x04000919 RID: 2329
		public static readonly PropertyInformation ParentFolderId = new PropertyInformation("ParentFolderId", ExchangeVersion.Exchange2007, StoreObjectSchema.ParentItemId, new PropertyUri(PropertyUriEnum.ItemParentId), new PropertyCommand.CreatePropertyCommand(ParentFolderIdProperty.CreateCommand));

		// Token: 0x0400091A RID: 2330
		public static readonly PropertyInformation ResponseObjects = new PropertyInformation("ResponseObjects", ExchangeVersion.Exchange2007, MessageItemSchema.IsReadReceiptPending, new PropertyUri(PropertyUriEnum.ResponseObjects), new PropertyCommand.CreatePropertyCommand(ResponseObjectsProperty.CreateCommand));

		// Token: 0x0400091B RID: 2331
		public static readonly PropertyInformation Sensitivity = new PropertyInformation("Sensitivity", ExchangeVersion.Exchange2007, ItemSchema.Sensitivity, new PropertyUri(PropertyUriEnum.Sensitivity), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400091C RID: 2332
		public static readonly PropertyInformation Size = new PropertyInformation("Size", ExchangeVersion.Exchange2007, ItemSchema.Size, new PropertyUri(PropertyUriEnum.Size), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400091D RID: 2333
		public static readonly PropertyInformation Subject = new PropertyInformation("Subject", ExchangeVersion.Exchange2007, ItemSchema.Subject, new PropertyUri(PropertyUriEnum.Subject), new PropertyCommand.CreatePropertyCommand(SubjectProperty.CreateCommand));

		// Token: 0x0400091E RID: 2334
		public static readonly PropertyInformation ReminderDueBy = new PropertyInformation(PropertyUriEnum.ReminderDueBy, ExchangeVersion.Exchange2007, ItemSchema.ReminderDueBy, new PropertyCommand.CreatePropertyCommand(ReminderDueByProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400091F RID: 2335
		public static readonly PropertyInformation ReminderIsSet = new PropertyInformation(PropertyUriEnum.ReminderIsSet, ExchangeVersion.Exchange2007, ItemSchema.ReminderIsSet, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000920 RID: 2336
		public static readonly PropertyInformation ReminderNextTime = new PropertyInformation(PropertyUriEnum.ReminderNextTime, ExchangeVersion.Exchange2012, ItemSchema.ReminderNextTime, new PropertyCommand.CreatePropertyCommand(ReminderNextTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000921 RID: 2337
		public static readonly PropertyInformation ReminderMinutesBeforeStart = new PropertyInformation(PropertyUriEnum.ReminderMinutesBeforeStart, ExchangeVersion.Exchange2007, ItemSchema.ReminderMinutesBeforeStart, new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000922 RID: 2338
		public static readonly PropertyInformation EffectiveRights = new PropertyInformation("EffectiveRights", ExchangeVersion.Exchange2007SP1, StoreObjectSchema.EffectiveRights, new PropertyUri(PropertyUriEnum.ItemEffectiveRights), new PropertyCommand.CreatePropertyCommand(EffectiveRightsProperty.CreateCommand));

		// Token: 0x04000923 RID: 2339
		public static readonly PropertyInformation WebClientReadFormQueryString = new PropertyInformation("WebClientReadFormQueryString", ServiceXml.GetFullyQualifiedName("WebClientReadFormQueryString"), "http://schemas.microsoft.com/exchange/services/2006/types", ExchangeVersion.Exchange2010, new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			MessageItemSchema.IsAssociated
		}, new PropertyUri(PropertyUriEnum.WebClientReadFormQueryString), new PropertyCommand.CreatePropertyCommand(WebClientReadFormQueryStringProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000924 RID: 2340
		public static readonly PropertyInformation WebClientEditFormQueryString = new PropertyInformation("WebClientEditFormQueryString", ServiceXml.GetFullyQualifiedName("WebClientEditFormQueryString"), "http://schemas.microsoft.com/exchange/services/2006/types", ExchangeVersion.Exchange2010, new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			MessageItemSchema.IsAssociated,
			MessageItemSchema.IsDraft
		}, new PropertyUri(PropertyUriEnum.WebClientEditFormQueryString), new PropertyCommand.CreatePropertyCommand(WebClientEditFormQueryStringProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000925 RID: 2341
		public static readonly PropertyInformation ConversationId = new PropertyInformation("ConversationId", ExchangeVersion.Exchange2010, ItemSchema.ConversationId, new PropertyUri(PropertyUriEnum.ConversationId), new PropertyCommand.CreatePropertyCommand(ConversationIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000926 RID: 2342
		public static readonly PropertyInformation UniqueBody = new PropertyInformation("UniqueBody", ExchangeVersion.Exchange2010, null, new PropertyUri(PropertyUriEnum.UniqueBody), new PropertyCommand.CreatePropertyCommand(UniqueBodyProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000927 RID: 2343
		public static readonly PropertyInformation Flag = new PropertyInformation("Flag", ServiceXml.GetFullyQualifiedName("Flag"), "http://schemas.microsoft.com/exchange/services/2006/types", ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			ItemSchema.FlagStatus,
			TaskSchema.StartDate,
			TaskSchema.DueDate,
			ItemSchema.CompleteDate
		}, new PropertyUri(PropertyUriEnum.Flag), new PropertyCommand.CreatePropertyCommand(FlagProperty.CreateCommand));

		// Token: 0x04000928 RID: 2344
		public static readonly PropertyInformation StoreEntryId = new PropertyInformation("StoreEntryId", ExchangeVersion.Exchange2010SP2, StoreObjectSchema.MapiStoreEntryId, new PropertyUri(PropertyUriEnum.StoreEntryId), new PropertyCommand.CreatePropertyCommand(StoreEntryIdProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000929 RID: 2345
		public static readonly PropertyInformation InstanceKey = new PropertyInformation("InstanceKey", ServiceXml.GetFullyQualifiedName("InstanceKey"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			ItemSchema.InstanceKey,
			ItemSchema.Fid,
			MessageItemSchema.MID
		}, new PropertyUri(PropertyUriEnum.InstanceKey), new PropertyCommand.CreatePropertyCommand(InstanceKeyProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400092A RID: 2346
		public static readonly PropertyInformation NormalizedBody = new PropertyInformation("NormalizedBody", ExchangeVersion.Exchange2012, null, new PropertyUri(PropertyUriEnum.NormalizedBody), new PropertyCommand.CreatePropertyCommand(NormalizedBodyProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400092B RID: 2347
		public static readonly PropertyInformation EntityExtractionResult = new PropertyInformation("EntityExtractionResult", ServiceXml.GetFullyQualifiedName("EntityExtractionResult"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			ItemSchema.ExtractedAddresses,
			ItemSchema.ExtractedMeetings,
			ItemSchema.ExtractedTasks,
			ItemSchema.ExtractedEmails,
			ItemSchema.ExtractedPhones,
			ItemSchema.ExtractedUrls,
			ItemSchema.ExtractedContacts
		}, new PropertyUri(PropertyUriEnum.EntityExtractionResult), new PropertyCommand.CreatePropertyCommand(EntityExtractionResultProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x0400092C RID: 2348
		public static readonly PropertyInformation PolicyTag = new PropertyInformation("PolicyTag", ServiceXml.GetFullyQualifiedName("PolicyTag"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			ItemSchema.RetentionDate
		}, new PropertyUri(PropertyUriEnum.ItemPolicyTag), new PropertyCommand.CreatePropertyCommand(ItemPolicyTagProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400092D RID: 2349
		public static readonly PropertyInformation ArchiveTag = new PropertyInformation("ArchiveTag", ServiceXml.GetFullyQualifiedName("ArchiveTag"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2012, new PropertyDefinition[]
		{
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			ItemSchema.ArchiveDate,
			StoreObjectSchema.RetentionFlags
		}, new PropertyUri(PropertyUriEnum.ItemArchiveTag), new PropertyCommand.CreatePropertyCommand(ItemArchiveTagProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsDeleteUpdateCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x0400092E RID: 2350
		public static readonly PropertyInformation RetentionDate = new PropertyInformation("RetentionDate", ExchangeVersion.Exchange2012, ItemSchema.RetentionDate, new PropertyUri(PropertyUriEnum.ItemRetentionDate), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400092F RID: 2351
		public static readonly PropertyInformation Preview = new PropertyInformation("Preview", ExchangeVersion.Exchange2012, ItemSchema.Preview, new PropertyUri(PropertyUriEnum.Preview), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000930 RID: 2352
		public static readonly PropertyInformation RightsManagementLicenseData = new PropertyInformation("RightsManagementLicenseData", ExchangeVersion.Exchange2012, null, new PropertyUri(PropertyUriEnum.RightsManagementLicenseData), new PropertyCommand.CreatePropertyCommand(RightsManagementLicenseDataProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000931 RID: 2353
		public static readonly PropertyInformation PredictedActionReasons = new PropertyInformation("PredictedActionReasons", ServiceXml.GetFullyQualifiedName("PredictedActionReasons"), ServiceXml.DefaultNamespaceUri, ExchangeVersion.Exchange2013, new PropertyDefinition[]
		{
			ItemSchema.IsClutter,
			ItemSchema.InferencePredictedReplyForwardReasons,
			ItemSchema.InferencePredictedDeleteReasons,
			ItemSchema.InferencePredictedIgnoreReasons
		}, new PropertyUri(PropertyUriEnum.PredictedActionReasons), new PropertyCommand.CreatePropertyCommand(PredictedActionReasonsProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000932 RID: 2354
		public static readonly PropertyInformation IsClutter = new PropertyInformation("IsClutter", ExchangeVersion.Exchange2013, ItemSchema.IsClutter, new PropertyUri(PropertyUriEnum.IsClutter), new PropertyCommand.CreatePropertyCommand(IsClutterProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetCommand | PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand | PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand);

		// Token: 0x04000933 RID: 2355
		public static readonly PropertyInformation BlockStatus = new PropertyInformation("BlockStatus", ExchangeVersion.Exchange2012, null, new PropertyUri(PropertyUriEnum.BlockStatus), new PropertyCommand.CreatePropertyCommand(BlockStatusProperty.CreateCommand), PropertyInformationAttributes.ImplementsSetUpdateCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000934 RID: 2356
		public static readonly PropertyInformation HasBlockedImages = new PropertyInformation("HasBlockedImages", ExchangeVersion.Exchange2012, null, new PropertyUri(PropertyUriEnum.HasBlockedImages), new PropertyCommand.CreatePropertyCommand(HasBlockedImagesProperty.CreateCommand), PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000935 RID: 2357
		public static readonly PropertyInformation TextBody = new PropertyInformation("TextBody", ExchangeVersion.Exchange2012, null, new PropertyUri(PropertyUriEnum.TextBody), new PropertyCommand.CreatePropertyCommand(TextBodyProperty.CreateCommand), PropertyInformationAttributes.ImplementsToXmlCommand | PropertyInformationAttributes.ImplementsToServiceObjectCommand);

		// Token: 0x04000936 RID: 2358
		public static readonly PropertyInformation IconIndex = new PropertyInformation("IconIndex", ExchangeVersion.Exchange2012, ItemSchema.IconIndex, new PropertyUri(PropertyUriEnum.IconIndex), new PropertyCommand.CreatePropertyCommand(IconIndexProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000937 RID: 2359
		public static readonly PropertyInformation RichContent = new PropertyInformation("RichContent", ExchangeVersion.Exchange2012, ItemSchema.RichContent, new PropertyUri(PropertyUriEnum.RichContent), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000938 RID: 2360
		public static readonly PropertyInformation MailboxGuid = new PropertyInformation("MailboxGuid", ExchangeVersion.Exchange2012, ItemSchema.MailboxGuid, new PropertyUri(PropertyUriEnum.ConversationMailboxGuid), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x04000939 RID: 2361
		public static readonly PropertyInformation ReceivedOrRenewTime = new PropertyInformation("ReceivedOrRenewTime", ExchangeVersion.Exchange2012, ItemSchema.ReceivedOrRenewTime, new PropertyUri(PropertyUriEnum.ReceivedOrRenewTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400093A RID: 2362
		public static readonly PropertyInformation WorkingSetSourcePartition = new PropertyInformation("WorkingSetSourcePartition", ExchangeVersion.Exchange2012, ItemSchema.WorkingSetSourcePartition, new PropertyUri(PropertyUriEnum.WorkingSetSourcePartition), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400093B RID: 2363
		public static readonly PropertyInformation RenewTime = new PropertyInformation("RenewTime", ExchangeVersion.Exchange2012, ItemSchema.RenewTime, new PropertyUri(PropertyUriEnum.RenewTime), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand));

		// Token: 0x0400093C RID: 2364
		public static readonly PropertyInformation SupportsSideConversation = new PropertyInformation("SupportsSideConversation", ExchangeVersion.Exchange2013, ItemSchema.SupportsSideConversation, new PropertyUri(PropertyUriEnum.SupportsSideConversation), new PropertyCommand.CreatePropertyCommand(SimpleProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400093D RID: 2365
		public static readonly PropertyInformation DateTimeSent = new PropertyInformation("DateTimeSent", ExchangeVersion.Exchange2007, ItemSchema.SentTime, new PropertyUri(PropertyUriEnum.DateTimeSent), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);

		// Token: 0x0400093E RID: 2366
		public static readonly PropertyInformation DateTimeReceived = new PropertyInformation("DateTimeReceived", ExchangeVersion.Exchange2007, ItemSchema.ReceivedTime, new PropertyUri(PropertyUriEnum.DateTimeReceived), new PropertyCommand.CreatePropertyCommand(DateTimeProperty.CreateCommand), PropertyInformationAttributes.ImplementsReadOnlyCommands);
	}
}
