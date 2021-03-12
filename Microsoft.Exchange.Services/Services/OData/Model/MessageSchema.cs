using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E59 RID: 3673
	internal class MessageSchema : ItemSchema
	{
		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06005EDB RID: 24283 RVA: 0x00127977 File Offset: 0x00125B77
		public new static MessageSchema SchemaInstance
		{
			get
			{
				return MessageSchema.MessageSchemaInstance.Member;
			}
		}

		// Token: 0x170015A5 RID: 5541
		// (get) Token: 0x06005EDC RID: 24284 RVA: 0x00127983 File Offset: 0x00125B83
		public override EdmEntityType EdmEntityType
		{
			get
			{
				return Message.EdmEntityType;
			}
		}

		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06005EDD RID: 24285 RVA: 0x0012798A File Offset: 0x00125B8A
		public override ReadOnlyCollection<PropertyDefinition> DeclaredProperties
		{
			get
			{
				return MessageSchema.DeclaredMessageProperties;
			}
		}

		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06005EDE RID: 24286 RVA: 0x00127991 File Offset: 0x00125B91
		public override ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return MessageSchema.AllMessageProperties;
			}
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06005EDF RID: 24287 RVA: 0x00127998 File Offset: 0x00125B98
		public override ReadOnlyCollection<PropertyDefinition> DefaultProperties
		{
			get
			{
				return MessageSchema.DefaultMessageProperties;
			}
		}

		// Token: 0x06005EE0 RID: 24288 RVA: 0x001279A0 File Offset: 0x00125BA0
		public override void RegisterEdmModel(EdmModel model)
		{
			base.RegisterEdmModel(model);
			CustomActions.RegisterAction(model, Message.EdmEntityType, Message.EdmEntityType, "Copy", new Dictionary<string, IEdmTypeReference>
			{
				{
					"DestinationId",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Message.EdmEntityType, Message.EdmEntityType, "Move", new Dictionary<string, IEdmTypeReference>
			{
				{
					"DestinationId",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Message.EdmEntityType, Message.EdmEntityType, "CreateReply", null);
			CustomActions.RegisterAction(model, Message.EdmEntityType, Message.EdmEntityType, "CreateReplyAll", null);
			CustomActions.RegisterAction(model, Message.EdmEntityType, Message.EdmEntityType, "CreateForward", null);
			CustomActions.RegisterAction(model, Message.EdmEntityType, null, "Reply", new Dictionary<string, IEdmTypeReference>
			{
				{
					"Comment",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Message.EdmEntityType, null, "ReplyAll", new Dictionary<string, IEdmTypeReference>
			{
				{
					"Comment",
					EdmCoreModel.Instance.GetString(true)
				}
			});
			CustomActions.RegisterAction(model, Message.EdmEntityType, null, "Forward", new Dictionary<string, IEdmTypeReference>
			{
				{
					"Comment",
					EdmCoreModel.Instance.GetString(true)
				},
				{
					"ToRecipients",
					new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true)))
				}
			});
			CustomActions.RegisterAction(model, Message.EdmEntityType, null, "Send", null);
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x00127C18 File Offset: 0x00125E18
		// Note: this type is marked as 'beforefieldinit'.
		static MessageSchema()
		{
			PropertyDefinition propertyDefinition = new PropertyDefinition("ParentFolderId", typeof(string));
			propertyDefinition.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition2 = propertyDefinition;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider = new SimpleEwsPropertyProvider(ItemSchema.ParentFolderId);
			simpleEwsPropertyProvider.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = EwsIdConverter.EwsIdToODataId((s[sp] as FolderId).Id);
			};
			propertyDefinition2.EwsPropertyProvider = simpleEwsPropertyProvider;
			MessageSchema.ParentFolderId = propertyDefinition;
			PropertyDefinition propertyDefinition3 = new PropertyDefinition("Sender", typeof(Recipient));
			propertyDefinition3.EdmType = new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true);
			propertyDefinition3.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate);
			PropertyDefinition propertyDefinition4 = propertyDefinition3;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider2 = new SimpleEwsPropertyProvider(MessageSchema.Sender);
			simpleEwsPropertyProvider2.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = s.GetValueOrDefault<SingleRecipientType>(sp).ToRecipient();
			};
			simpleEwsPropertyProvider2.Setter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				s[sp] = (e[ep] as Recipient).ToSingleRecipientType();
			};
			propertyDefinition4.EwsPropertyProvider = simpleEwsPropertyProvider2;
			propertyDefinition3.ODataPropertyValueConverter = new RecipientODataConverter();
			MessageSchema.Sender = propertyDefinition3;
			PropertyDefinition propertyDefinition5 = new PropertyDefinition("From", typeof(Recipient));
			propertyDefinition5.EdmType = new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true);
			propertyDefinition5.Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate);
			PropertyDefinition propertyDefinition6 = propertyDefinition5;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider3 = new SimpleEwsPropertyProvider(MessageSchema.From);
			simpleEwsPropertyProvider3.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				e[ep] = s.GetValueOrDefault<SingleRecipientType>(sp).ToRecipient();
			};
			simpleEwsPropertyProvider3.Setter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				s[sp] = (e[ep] as Recipient).ToSingleRecipientType();
			};
			propertyDefinition6.EwsPropertyProvider = simpleEwsPropertyProvider3;
			propertyDefinition5.ODataPropertyValueConverter = new RecipientODataConverter();
			MessageSchema.From = propertyDefinition5;
			MessageSchema.ToRecipients = new PropertyDefinition("ToRecipients", typeof(Recipient[]))
			{
				EdmType = new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true))),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new RecipientsPropertyProvider(MessageSchema.ToRecipients),
				ODataPropertyValueConverter = new RecipientsODataConverter()
			};
			MessageSchema.CcRecipients = new PropertyDefinition("CcRecipients", typeof(Recipient[]))
			{
				EdmType = new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true))),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new RecipientsPropertyProvider(MessageSchema.CcRecipients),
				ODataPropertyValueConverter = new RecipientsODataConverter()
			};
			MessageSchema.BccRecipients = new PropertyDefinition("BccRecipients", typeof(Recipient[]))
			{
				EdmType = new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true))),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new RecipientsPropertyProvider(MessageSchema.BccRecipients),
				ODataPropertyValueConverter = new RecipientsODataConverter()
			};
			MessageSchema.ReplyTo = new PropertyDefinition("ReplyTo", typeof(Recipient[]))
			{
				EdmType = new EdmCollectionTypeReference(new EdmCollectionType(new EdmComplexTypeReference(Recipient.EdmComplexType.Member, true))),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new RecipientsPropertyProvider(MessageSchema.ReplyTo),
				ODataPropertyValueConverter = new RecipientsODataConverter()
			};
			PropertyDefinition propertyDefinition7 = new PropertyDefinition("ConversationId", typeof(string));
			propertyDefinition7.EdmType = EdmCoreModel.Instance.GetString(true);
			propertyDefinition7.Flags = PropertyDefinitionFlags.CanFilter;
			PropertyDefinition propertyDefinition8 = propertyDefinition7;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider4 = new SimpleEwsPropertyProvider(ItemSchema.ConversationId);
			simpleEwsPropertyProvider4.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				string value = null;
				ItemId valueOrDefault = s.GetValueOrDefault<ItemId>(ItemSchema.ConversationId);
				if (valueOrDefault != null)
				{
					value = EwsIdConverter.EwsIdToODataId(valueOrDefault.Id);
				}
				e[ep] = value;
			};
			simpleEwsPropertyProvider4.QueryConstantBuilder = ((object o) => EwsIdConverter.ODataIdToEwsId(o as string));
			propertyDefinition8.EwsPropertyProvider = simpleEwsPropertyProvider4;
			MessageSchema.ConversationId = propertyDefinition7;
			MessageSchema.UniqueBody = new PropertyDefinition("UniqueBody", typeof(ItemBody))
			{
				EdmType = new EdmComplexTypeReference(ItemBody.EdmComplexType.Member, true),
				EwsPropertyProvider = new BodyPropertyProvider(ItemSchema.UniqueBody),
				ODataPropertyValueConverter = new ItemBodyODataConverter()
			};
			MessageSchema.IsDeliveryReceiptRequested = new PropertyDefinition("IsDeliveryReceiptRequested", typeof(bool))
			{
				EdmType = EdmCoreModel.Instance.GetBoolean(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(MessageSchema.IsDeliveryReceiptRequested)
			};
			MessageSchema.IsReadReceiptRequested = new PropertyDefinition("IsReadReceiptRequested", typeof(bool))
			{
				EdmType = EdmCoreModel.Instance.GetBoolean(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(MessageSchema.IsReadReceiptRequested)
			};
			MessageSchema.IsRead = new PropertyDefinition("IsRead", typeof(bool))
			{
				EdmType = EdmCoreModel.Instance.GetBoolean(true),
				Flags = (PropertyDefinitionFlags.CanFilter | PropertyDefinitionFlags.CanCreate | PropertyDefinitionFlags.CanUpdate),
				EwsPropertyProvider = new SimpleEwsPropertyProvider(MessageSchema.IsRead)
			};
			MessageSchema.IsDraft = new PropertyDefinition("IsDraft", typeof(bool))
			{
				EdmType = EdmCoreModel.Instance.GetBoolean(true),
				Flags = PropertyDefinitionFlags.CanFilter,
				EwsPropertyProvider = new SimpleEwsPropertyProvider(ItemSchema.IsDraft)
			};
			MessageSchema.DateTimeReceived = new PropertyDefinition("DateTimeReceived", typeof(DateTimeOffset))
			{
				EdmType = EdmCoreModel.Instance.GetDateTimeOffset(true),
				Flags = PropertyDefinitionFlags.CanFilter,
				EwsPropertyProvider = new DateTimePropertyProvider(ItemSchema.DateTimeReceived)
			};
			MessageSchema.DateTimeSent = new PropertyDefinition("DateTimeSent", typeof(DateTimeOffset))
			{
				EdmType = EdmCoreModel.Instance.GetDateTimeOffset(true),
				Flags = PropertyDefinitionFlags.CanFilter,
				EwsPropertyProvider = new DateTimePropertyProvider(ItemSchema.DateTimeSent)
			};
			PropertyDefinition propertyDefinition9 = new PropertyDefinition("EventId", typeof(string));
			propertyDefinition9.EdmType = EdmCoreModel.Instance.GetString(true);
			PropertyDefinition propertyDefinition10 = propertyDefinition9;
			SimpleEwsPropertyProvider simpleEwsPropertyProvider5 = new SimpleEwsPropertyProvider(MeetingMessageSchema.AssociatedCalendarItemId);
			simpleEwsPropertyProvider5.Getter = delegate(Entity e, PropertyDefinition ep, ServiceObject s, PropertyInformation sp)
			{
				string value = null;
				ItemId valueOrDefault = s.GetValueOrDefault<ItemId>(MeetingMessageSchema.AssociatedCalendarItemId);
				if (valueOrDefault != null)
				{
					value = EwsIdConverter.EwsIdToODataId(valueOrDefault.Id);
				}
				e[ep] = value;
			};
			propertyDefinition10.EwsPropertyProvider = simpleEwsPropertyProvider5;
			MessageSchema.EventId = propertyDefinition9;
			MessageSchema.MeetingMessageType = new PropertyDefinition("MeetingMessageType", typeof(MeetingMessageType))
			{
				EdmType = new EdmEnumTypeReference(EnumTypes.GetEdmEnumType(typeof(MeetingMessageType)), true),
				Flags = PropertyDefinitionFlags.CanFilter,
				EwsPropertyProvider = new MeetingMessageTypePropertyProvider(ItemSchema.ItemClass)
			};
			MessageSchema.DeclaredMessageProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>
			{
				MessageSchema.ParentFolderId,
				MessageSchema.From,
				MessageSchema.Sender,
				MessageSchema.ToRecipients,
				MessageSchema.CcRecipients,
				MessageSchema.BccRecipients,
				MessageSchema.ReplyTo,
				MessageSchema.ConversationId,
				MessageSchema.UniqueBody,
				MessageSchema.DateTimeReceived,
				MessageSchema.DateTimeSent,
				MessageSchema.IsDeliveryReceiptRequested,
				MessageSchema.IsReadReceiptRequested,
				MessageSchema.IsDraft,
				MessageSchema.IsRead,
				MessageSchema.EventId,
				MessageSchema.MeetingMessageType,
				ItemSchema.DateTimeCreated,
				ItemSchema.LastModifiedTime
			});
			MessageSchema.AllMessageProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(ItemSchema.AllItemProperties.Union(MessageSchema.DeclaredMessageProperties)));
			MessageSchema.DefaultMessageProperties = new ReadOnlyCollection<PropertyDefinition>(new List<PropertyDefinition>(ItemSchema.DefaultItemProperties)
			{
				MessageSchema.ParentFolderId,
				MessageSchema.From,
				MessageSchema.Sender,
				MessageSchema.ToRecipients,
				MessageSchema.CcRecipients,
				MessageSchema.BccRecipients,
				MessageSchema.ReplyTo,
				MessageSchema.ConversationId,
				MessageSchema.DateTimeReceived,
				MessageSchema.DateTimeSent,
				MessageSchema.IsDeliveryReceiptRequested,
				MessageSchema.IsReadReceiptRequested,
				MessageSchema.IsDraft,
				MessageSchema.IsRead,
				MessageSchema.EventId,
				MessageSchema.MeetingMessageType,
				ItemSchema.DateTimeCreated,
				ItemSchema.LastModifiedTime
			});
			MessageSchema.MessageSchemaInstance = new LazyMember<MessageSchema>(() => new MessageSchema());
		}

		// Token: 0x04003352 RID: 13138
		public static readonly PropertyDefinition ParentFolderId;

		// Token: 0x04003353 RID: 13139
		public static readonly PropertyDefinition Sender;

		// Token: 0x04003354 RID: 13140
		public static readonly PropertyDefinition From;

		// Token: 0x04003355 RID: 13141
		public static readonly PropertyDefinition ToRecipients;

		// Token: 0x04003356 RID: 13142
		public static readonly PropertyDefinition CcRecipients;

		// Token: 0x04003357 RID: 13143
		public static readonly PropertyDefinition BccRecipients;

		// Token: 0x04003358 RID: 13144
		public static readonly PropertyDefinition ReplyTo;

		// Token: 0x04003359 RID: 13145
		public static readonly PropertyDefinition ConversationId;

		// Token: 0x0400335A RID: 13146
		public static readonly PropertyDefinition UniqueBody;

		// Token: 0x0400335B RID: 13147
		public static readonly PropertyDefinition IsDeliveryReceiptRequested;

		// Token: 0x0400335C RID: 13148
		public static readonly PropertyDefinition IsReadReceiptRequested;

		// Token: 0x0400335D RID: 13149
		public static readonly PropertyDefinition IsRead;

		// Token: 0x0400335E RID: 13150
		public static readonly PropertyDefinition IsDraft;

		// Token: 0x0400335F RID: 13151
		public static readonly PropertyDefinition DateTimeReceived;

		// Token: 0x04003360 RID: 13152
		public static readonly PropertyDefinition DateTimeSent;

		// Token: 0x04003361 RID: 13153
		public static readonly PropertyDefinition EventId;

		// Token: 0x04003362 RID: 13154
		public static readonly PropertyDefinition MeetingMessageType;

		// Token: 0x04003363 RID: 13155
		public static readonly ReadOnlyCollection<PropertyDefinition> DeclaredMessageProperties;

		// Token: 0x04003364 RID: 13156
		public static readonly ReadOnlyCollection<PropertyDefinition> AllMessageProperties;

		// Token: 0x04003365 RID: 13157
		public static readonly ReadOnlyCollection<PropertyDefinition> DefaultMessageProperties;

		// Token: 0x04003366 RID: 13158
		private static readonly LazyMember<MessageSchema> MessageSchemaInstance;
	}
}
