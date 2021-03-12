﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200016C RID: 364
	internal sealed class MessageTraceDataSet : ConfigurablePropertyBag
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x0002C166 File Offset: 0x0002A366
		public override ObjectId Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002C16C File Offset: 0x0002A36C
		public MessageTrace ConvertToMessageTraceObject()
		{
			Dictionary<Guid, MessageAction> dictionary = new Dictionary<Guid, MessageAction>();
			Dictionary<Guid, MessageClassification> dict = new Dictionary<Guid, MessageClassification>();
			Dictionary<Guid, MessageClientInformation> dict2 = new Dictionary<Guid, MessageClientInformation>();
			Dictionary<Guid, MessageEvent> dictionary2 = new Dictionary<Guid, MessageEvent>();
			Dictionary<Guid, MessageEventRule> dictionary3 = new Dictionary<Guid, MessageEventRule>();
			Dictionary<Guid, MessageEventRuleClassification> dictionary4 = new Dictionary<Guid, MessageEventRuleClassification>();
			Dictionary<Guid, MessageEventSourceItem> dictionary5 = new Dictionary<Guid, MessageEventSourceItem>();
			Dictionary<Guid, MessageRecipient> dict3 = new Dictionary<Guid, MessageRecipient>();
			Dictionary<Guid, MessageRecipientStatus> dictionary6 = new Dictionary<Guid, MessageRecipientStatus>();
			MessageTraceEntityBase[] array = this.LoadConfigurables<MessageTrace>(MessageTraceDataSetSchema.MessagesTableProperty);
			MessageTrace messageTrace = null;
			if (array != null && array.Length > 0)
			{
				messageTrace = (array.FirstOrDefault<MessageTraceEntityBase>() as MessageTrace);
			}
			if (messageTrace == null)
			{
				return null;
			}
			MessageTraceEntityBase[] messageProperties = this.LoadConfigurables<MessageProperty>(MessageTraceDataSetSchema.MessagePropertiesTableProperty);
			MessageTraceEntityBase[] msgActions = this.LoadConfigurables<MessageAction>(MessageTraceDataSetSchema.MessageActionTableProperty);
			MessageTraceEntityBase[] msgActionProperties = this.LoadConfigurables<MessageActionProperty>(MessageTraceDataSetSchema.MessageActionPropertiesTableProperty);
			MessageTraceEntityBase[] msgClassifications = this.LoadConfigurables<MessageClassification>(MessageTraceDataSetSchema.MessageClassificationsTableProperty);
			MessageTraceEntityBase[] msgClassificationProperties = this.LoadConfigurables<MessageClassificationProperty>(MessageTraceDataSetSchema.MessageClassificationPropertiesTableProperty);
			MessageTraceEntityBase[] msgClientInformation = this.LoadConfigurables<MessageClientInformation>(MessageTraceDataSetSchema.MessageClientInformationTableProperty);
			MessageTraceEntityBase[] msgClientInformationProperties = this.LoadConfigurables<MessageClientInformationProperty>(MessageTraceDataSetSchema.MessageClientInformationPropertiesTableProperty);
			MessageTraceEntityBase[] msgEvents = this.LoadConfigurables<MessageEvent>(MessageTraceDataSetSchema.MessageEventsTableProperty);
			MessageTraceEntityBase[] eventProperties = this.LoadConfigurables<MessageEventProperty>(MessageTraceDataSetSchema.MessageEventPropertiesTableProperty);
			MessageTraceEntityBase[] eventRules = this.LoadConfigurables<MessageEventRule>(MessageTraceDataSetSchema.MessageEventRulesTableProperty);
			MessageTraceEntityBase[] msgEventRuleProperties = this.LoadConfigurables<MessageEventRuleProperty>(MessageTraceDataSetSchema.MessageEventRulePropertiesTableProperty);
			MessageTraceEntityBase[] msgEventRuleClassifications = this.LoadConfigurables<MessageEventRuleClassification>(MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty);
			MessageTraceEntityBase[] msgEventRuleClassificationProperties = this.LoadConfigurables<MessageEventRuleClassificationProperty>(MessageTraceDataSetSchema.MessageEventRuleClassificationPropertiesTableProperty);
			MessageTraceEntityBase[] eventSourceItems = this.LoadConfigurables<MessageEventSourceItem>(MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty);
			MessageTraceEntityBase[] msgEventSourceItemProperties = this.LoadConfigurables<MessageEventSourceItemProperty>(MessageTraceDataSetSchema.MessageEventSourceItemPropertiesTableProperty);
			MessageTraceEntityBase[] msgRecipients = this.LoadConfigurables<MessageRecipient>(MessageTraceDataSetSchema.MessageRecipientsTableProperty);
			MessageTraceEntityBase[] recipientProperties = this.LoadConfigurables<MessageRecipientProperty>(MessageTraceDataSetSchema.MessageRecipientPropertiesTableProperty);
			MessageTraceEntityBase[] msgRecipientStatuses = this.LoadConfigurables<MessageRecipientStatus>(MessageTraceDataSetSchema.MessageRecipientStatusTableProperty);
			MessageTraceEntityBase[] msgRecipientStatusProperties = this.LoadConfigurables<MessageRecipientStatusProperty>(MessageTraceDataSetSchema.MessageRecipientStatusPropertiesTableProperty);
			MessageTraceDataSet.AddMessageProperties(messageProperties, messageTrace);
			MessageTraceDataSet.AddClassifications(msgClassifications, messageTrace, dict);
			MessageTraceDataSet.AddClassificationProperties(msgClassificationProperties, dict);
			MessageTraceDataSet.AddClientInformation(msgClientInformation, messageTrace, dict2);
			MessageTraceDataSet.AddClientInformationProperties(msgClientInformationProperties, dict2);
			MessageTraceDataSet.AddEvents(msgEvents, messageTrace, dictionary2);
			MessageTraceDataSet.AddEventPropertiesToEvents(eventProperties, dictionary2);
			MessageTraceDataSet.AddEventRules(eventRules, dictionary2, dictionary3);
			MessageTraceDataSet.AddEventRuleProperties(msgEventRuleProperties, dictionary3);
			MessageTraceDataSet.AddEventRuleClassifications(msgEventRuleClassifications, dictionary3, dictionary4);
			MessageTraceDataSet.AddEventRuleClassificationProperties(msgEventRuleClassificationProperties, dictionary4);
			MessageTraceDataSet.AddActions(msgActions, dictionary3, dictionary);
			MessageTraceDataSet.AddActionProperties(msgActionProperties, dictionary);
			MessageTraceDataSet.AddEventSourceItems(eventSourceItems, dictionary2, dictionary5);
			MessageTraceDataSet.AddEventSourceItemProperties(msgEventSourceItemProperties, dictionary5);
			MessageTraceDataSet.AddRecipients(msgRecipients, messageTrace, dict3);
			MessageTraceDataSet.AddRecipientPropertiesToRecipients(recipientProperties, dict3);
			MessageTraceDataSet.AddRecipientStatuses(msgRecipientStatuses, dictionary6, dictionary2);
			MessageTraceDataSet.AddRecipientStatusProperties(msgRecipientStatusProperties, dictionary6);
			return messageTrace;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0002C38E File Offset: 0x0002A58E
		public override Type GetSchemaType()
		{
			return typeof(MessageTraceDataSetSchema);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002C4DC File Offset: 0x0002A6DC
		private static MessageTraceEntityBase[] MapResultsTable<T>(DataTable table) where T : MessageTraceEntityBase, new()
		{
			List<MessageTraceEntityBase> list = new List<MessageTraceEntityBase>();
			if (list != null && table != null && table.Rows.Count > 0)
			{
				MessageTraceDataSet.<>c__DisplayClass3<T> CS$<>8__locals1 = new MessageTraceDataSet.<>c__DisplayClass3<T>();
				CS$<>8__locals1.columnSchema = table.Columns;
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					MessageTraceEntityBase entity = Activator.CreateInstance<T>();
					IEnumerable<PropertyDefinition> propertyDefinitions = (IEnumerable<PropertyDefinition>)entity.GetAllProperties();
					using (IEnumerator enumerator2 = CS$<>8__locals1.columnSchema.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							DataColumn column = (DataColumn)enumerator2.Current;
							try
							{
								PropertyDefinition propertyDefinition2 = propertyDefinitions.First((PropertyDefinition propertyDefinition) => string.Compare(propertyDefinition.Name, column.ColumnName, StringComparison.OrdinalIgnoreCase) == 0);
								if (propertyDefinition2 != null)
								{
									entity[propertyDefinition2] = DalHelper.ConvertFromStoreObject(dataRow[column], propertyDefinition2.Type);
								}
							}
							catch (InvalidOperationException)
							{
							}
						}
					}
					PropertyBase propertyBase = entity as PropertyBase;
					if (propertyBase != null && propertyBase.PropertyName == MessageTraceCollapsedProperty.PropertyDefinition.Name)
					{
						byte[] data = Convert.FromBase64String(propertyBase.PropertyValueBlob.Value);
						list.AddRange(MessageTraceCollapsedProperty.Expand<PropertyBase>(data, propertyBase.Namespace, delegate
						{
							PropertyBase propertyBase2 = Activator.CreateInstance<T>() as PropertyBase;
							using (IEnumerator enumerator3 = CS$<>8__locals1.columnSchema.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									MessageTraceDataSet.<>c__DisplayClass3<T> CS$<>8__locals4 = CS$<>8__locals1;
									DataColumn column = (DataColumn)enumerator3.Current;
									try
									{
										PropertyDefinition propertyDefinition3 = propertyDefinitions.First((PropertyDefinition propertyDefinition) => string.Compare(propertyDefinition.Name, column.ColumnName, StringComparison.OrdinalIgnoreCase) == 0);
										if (propertyDefinition3 != null && propertyDefinition3 != PropertyBase.PropertyValueBlobProperty)
										{
											propertyBase2[propertyDefinition3] = entity[propertyDefinition3];
										}
									}
									catch (InvalidOperationException)
									{
									}
								}
							}
							propertyBase2.PropertyId = Guid.NewGuid();
							return propertyBase2;
						}));
					}
					else
					{
						list.Add(entity);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002C718 File Offset: 0x0002A918
		private static void AddMessageProperties(MessageTraceEntityBase[] messageProperties, MessageTrace msgTrace)
		{
			if (messageProperties == null)
			{
				return;
			}
			foreach (MessageProperty extendedProperty in messageProperties.Cast<MessageProperty>())
			{
				msgTrace.AddExtendedProperty(extendedProperty);
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002C76C File Offset: 0x0002A96C
		private static void AddEvents(MessageTraceEntityBase[] msgEvents, MessageTrace msgTrace, Dictionary<Guid, MessageEvent> dict)
		{
			if (msgEvents == null)
			{
				return;
			}
			foreach (MessageEvent messageEvent in msgEvents.Cast<MessageEvent>())
			{
				msgTrace.Add(messageEvent);
				dict.Add(messageEvent.EventId, messageEvent);
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002C7CC File Offset: 0x0002A9CC
		private static void AddEventPropertiesToEvents(MessageTraceEntityBase[] eventProperties, Dictionary<Guid, MessageEvent> dict)
		{
			if (eventProperties == null)
			{
				return;
			}
			foreach (MessageEventProperty messageEventProperty in eventProperties.Cast<MessageEventProperty>())
			{
				MessageEvent messageEvent = null;
				if (dict.TryGetValue(messageEventProperty.EventId, out messageEvent) && messageEvent != null)
				{
					messageEvent.AddExtendedProperty(messageEventProperty);
				}
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002C834 File Offset: 0x0002AA34
		private static void AddEventRules(MessageTraceEntityBase[] eventRules, Dictionary<Guid, MessageEvent> eventDict, Dictionary<Guid, MessageEventRule> ruleDict)
		{
			if (eventRules == null)
			{
				return;
			}
			foreach (MessageEventRule messageEventRule in eventRules.Cast<MessageEventRule>())
			{
				MessageEvent messageEvent = null;
				if (eventDict.TryGetValue(messageEventRule.EventId, out messageEvent) && messageEvent != null)
				{
					messageEvent.Add(messageEventRule);
					ruleDict.Add(messageEventRule.EventRuleId, messageEventRule);
				}
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		private static void AddEventRuleProperties(MessageTraceEntityBase[] msgEventRuleProperties, Dictionary<Guid, MessageEventRule> dict)
		{
			if (msgEventRuleProperties == null)
			{
				return;
			}
			foreach (MessageEventRuleProperty messageEventRuleProperty in msgEventRuleProperties.Cast<MessageEventRuleProperty>())
			{
				MessageEventRule messageEventRule = null;
				if (dict.TryGetValue(messageEventRuleProperty.EventRuleId, out messageEventRule) && messageEventRule != null)
				{
					messageEventRule.AddExtendedProperty(messageEventRuleProperty);
				}
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002C910 File Offset: 0x0002AB10
		private static void AddEventRuleClassifications(MessageTraceEntityBase[] msgEventRuleClassifications, Dictionary<Guid, MessageEventRule> eventRuleDict, Dictionary<Guid, MessageEventRuleClassification> ruleClassificationDict)
		{
			if (msgEventRuleClassifications == null)
			{
				return;
			}
			foreach (MessageEventRuleClassification messageEventRuleClassification in msgEventRuleClassifications.Cast<MessageEventRuleClassification>())
			{
				MessageEventRule messageEventRule = null;
				if (eventRuleDict.TryGetValue(messageEventRuleClassification.EventRuleId, out messageEventRule) && messageEventRule != null)
				{
					messageEventRule.Add(messageEventRuleClassification);
					ruleClassificationDict.Add(messageEventRuleClassification.EventRuleClassificationId, messageEventRuleClassification);
				}
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0002C984 File Offset: 0x0002AB84
		private static void AddEventRuleClassificationProperties(MessageTraceEntityBase[] msgEventRuleClassificationProperties, Dictionary<Guid, MessageEventRuleClassification> dict)
		{
			if (msgEventRuleClassificationProperties == null)
			{
				return;
			}
			foreach (MessageEventRuleClassificationProperty messageEventRuleClassificationProperty in msgEventRuleClassificationProperties.Cast<MessageEventRuleClassificationProperty>())
			{
				MessageEventRuleClassification messageEventRuleClassification = null;
				if (dict.TryGetValue(messageEventRuleClassificationProperty.EventRuleClassificationId, out messageEventRuleClassification) && messageEventRuleClassification != null)
				{
					messageEventRuleClassification.AddExtendedProperty(messageEventRuleClassificationProperty);
				}
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0002C9EC File Offset: 0x0002ABEC
		private static void AddEventSourceItems(MessageTraceEntityBase[] eventSourceItems, Dictionary<Guid, MessageEvent> eventDict, Dictionary<Guid, MessageEventSourceItem> itemDict)
		{
			if (eventSourceItems == null)
			{
				return;
			}
			foreach (MessageEventSourceItem messageEventSourceItem in eventSourceItems.Cast<MessageEventSourceItem>())
			{
				MessageEvent messageEvent = null;
				if (eventDict.TryGetValue(messageEventSourceItem.EventId, out messageEvent) && messageEvent != null)
				{
					messageEvent.Add(messageEventSourceItem);
				}
				itemDict.Add(messageEventSourceItem.SourceItemId, messageEventSourceItem);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0002CA60 File Offset: 0x0002AC60
		private static void AddEventSourceItemProperties(MessageTraceEntityBase[] msgEventSourceItemProperties, Dictionary<Guid, MessageEventSourceItem> dict)
		{
			if (msgEventSourceItemProperties == null)
			{
				return;
			}
			foreach (MessageEventSourceItemProperty messageEventSourceItemProperty in msgEventSourceItemProperties.Cast<MessageEventSourceItemProperty>())
			{
				MessageEventSourceItem messageEventSourceItem = null;
				if (dict.TryGetValue(messageEventSourceItemProperty.SourceItemId, out messageEventSourceItem) && messageEventSourceItem != null)
				{
					messageEventSourceItem.AddExtendedProperty(messageEventSourceItemProperty);
				}
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0002CAC8 File Offset: 0x0002ACC8
		private static void AddRecipients(MessageTraceEntityBase[] msgRecipients, MessageTrace msgTrace, Dictionary<Guid, MessageRecipient> dict)
		{
			if (msgRecipients == null)
			{
				return;
			}
			foreach (MessageRecipient messageRecipient in msgRecipients.Cast<MessageRecipient>())
			{
				msgTrace.Add(messageRecipient);
				dict.Add(messageRecipient.RecipientId, messageRecipient);
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0002CB28 File Offset: 0x0002AD28
		private static void AddRecipientStatuses(MessageTraceEntityBase[] msgRecipientStatuses, Dictionary<Guid, MessageRecipientStatus> dictMessageRecipientStatus, Dictionary<Guid, MessageEvent> dictMessageEvent)
		{
			if (msgRecipientStatuses == null)
			{
				return;
			}
			foreach (MessageRecipientStatus messageRecipientStatus in msgRecipientStatuses.Cast<MessageRecipientStatus>())
			{
				MessageEvent messageEvent = null;
				if (dictMessageEvent.TryGetValue(messageRecipientStatus.EventId, out messageEvent) && messageEvent != null)
				{
					messageEvent.Add(messageRecipientStatus);
					dictMessageRecipientStatus.Add(messageRecipientStatus.RecipientStatusId, messageRecipientStatus);
				}
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0002CB9C File Offset: 0x0002AD9C
		private static void AddRecipientStatusProperties(MessageTraceEntityBase[] msgRecipientStatusProperties, Dictionary<Guid, MessageRecipientStatus> dict)
		{
			if (msgRecipientStatusProperties == null)
			{
				return;
			}
			foreach (MessageRecipientStatusProperty messageRecipientStatusProperty in msgRecipientStatusProperties.Cast<MessageRecipientStatusProperty>())
			{
				MessageRecipientStatus messageRecipientStatus = null;
				if (dict.TryGetValue(messageRecipientStatusProperty.RecipientStatusId, out messageRecipientStatus) && messageRecipientStatus != null)
				{
					messageRecipientStatus.AddExtendedProperty(messageRecipientStatusProperty);
				}
			}
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0002CC04 File Offset: 0x0002AE04
		private static void AddClassifications(MessageTraceEntityBase[] msgClassifications, MessageTrace msgTrace, Dictionary<Guid, MessageClassification> dict)
		{
			if (msgClassifications == null)
			{
				return;
			}
			foreach (MessageClassification messageClassification in msgClassifications.Cast<MessageClassification>())
			{
				msgTrace.Add(messageClassification);
				dict.Add(messageClassification.ClassificationId, messageClassification);
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002CC64 File Offset: 0x0002AE64
		private static void AddClassificationProperties(MessageTraceEntityBase[] msgClassificationProperties, Dictionary<Guid, MessageClassification> dict)
		{
			if (msgClassificationProperties == null)
			{
				return;
			}
			foreach (MessageClassificationProperty messageClassificationProperty in msgClassificationProperties.Cast<MessageClassificationProperty>())
			{
				MessageClassification messageClassification = null;
				if (dict.TryGetValue(messageClassificationProperty.ClassificationId, out messageClassification) && messageClassification != null)
				{
					messageClassification.AddExtendedProperty(messageClassificationProperty);
				}
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002CCCC File Offset: 0x0002AECC
		private static void AddClientInformation(MessageTraceEntityBase[] msgClientInformation, MessageTrace msgTrace, Dictionary<Guid, MessageClientInformation> dict)
		{
			if (msgClientInformation == null)
			{
				return;
			}
			foreach (MessageClientInformation messageClientInformation in msgClientInformation.Cast<MessageClientInformation>())
			{
				msgTrace.Add(messageClientInformation);
				dict.Add(messageClientInformation.ClientInformationId, messageClientInformation);
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002CD2C File Offset: 0x0002AF2C
		private static void AddClientInformationProperties(MessageTraceEntityBase[] msgClientInformationProperties, Dictionary<Guid, MessageClientInformation> dict)
		{
			if (msgClientInformationProperties == null)
			{
				return;
			}
			foreach (MessageClientInformationProperty messageClientInformationProperty in msgClientInformationProperties.Cast<MessageClientInformationProperty>())
			{
				MessageClientInformation messageClientInformation = null;
				if (dict.TryGetValue(messageClientInformationProperty.ClientInformationId, out messageClientInformation) && messageClientInformation != null)
				{
					messageClientInformation.AddExtendedProperty(messageClientInformationProperty);
				}
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002CD94 File Offset: 0x0002AF94
		private static void AddActions(MessageTraceEntityBase[] msgActions, Dictionary<Guid, MessageEventRule> ruleDict, Dictionary<Guid, MessageAction> actionDict)
		{
			if (msgActions == null)
			{
				return;
			}
			foreach (MessageAction messageAction in msgActions.Cast<MessageAction>())
			{
				MessageEventRule messageEventRule = null;
				if (messageAction.EventRuleId != Guid.Empty && ruleDict.TryGetValue(messageAction.EventRuleId, out messageEventRule) && messageEventRule != null)
				{
					messageEventRule.Add(messageAction);
					actionDict[messageAction.RuleActionId] = messageAction;
				}
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002CE1C File Offset: 0x0002B01C
		private static void AddActionProperties(MessageTraceEntityBase[] msgActionProperties, Dictionary<Guid, MessageAction> dict)
		{
			if (msgActionProperties == null)
			{
				return;
			}
			foreach (MessageActionProperty messageActionProperty in msgActionProperties.Cast<MessageActionProperty>())
			{
				MessageAction messageAction = null;
				if (dict.TryGetValue(messageActionProperty.RuleActionId, out messageAction) && messageAction != null)
				{
					messageAction.AddExtendedProperty(messageActionProperty);
				}
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002CE84 File Offset: 0x0002B084
		private static void AddRecipientPropertiesToRecipients(MessageTraceEntityBase[] recipientProperties, Dictionary<Guid, MessageRecipient> dict)
		{
			if (recipientProperties == null)
			{
				return;
			}
			foreach (MessageRecipientProperty messageRecipientProperty in recipientProperties.Cast<MessageRecipientProperty>())
			{
				MessageRecipient messageRecipient = null;
				if (dict.TryGetValue(messageRecipientProperty.RecipientId, out messageRecipient) && messageRecipient != null)
				{
					messageRecipient.AddExtendedProperty(messageRecipientProperty);
				}
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		private MessageTraceEntityBase[] LoadConfigurables<T>(HygienePropertyDefinition tableProperty) where T : MessageTraceEntityBase, new()
		{
			object obj;
			base.TryGetValue(tableProperty, out obj);
			DataTable table = obj as DataTable;
			return MessageTraceDataSet.MapResultsTable<T>(table);
		}
	}
}
