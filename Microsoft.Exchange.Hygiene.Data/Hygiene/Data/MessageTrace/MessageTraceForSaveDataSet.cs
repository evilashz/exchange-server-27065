using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200016E RID: 366
	internal sealed class MessageTraceForSaveDataSet : ConfigurablePropertyBag, IValidateable
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002D138 File Offset: 0x0002B338
		static MessageTraceForSaveDataSet()
		{
			MessageTraceForSaveDataSet.mapTableToTvpColumnInfo = new Dictionary<HygienePropertyDefinition, HygienePropertyDefinition[]>();
			foreach (TvpInfo tvpInfo in MessageTraceForSaveDataSet.tvpPrototypeList)
			{
				MessageTraceForSaveDataSet.mapTableToTvpColumnInfo.Add(tvpInfo.TableName, tvpInfo.Columns);
			}
			MessageTraceForSaveDataSet.mappingInfoList = new List<MessageTraceForSaveDataSet.MappingInfo>();
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageRecipientsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageRecipientPropertiesTableProperty,
				ParentKeyProperty = MessageRecipientSchema.RecipientIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EmailHashKeyProperty, CommonMessageTraceSchema.EmailHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageRecipientsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageRecipientStatusTableProperty,
				ParentKeyProperty = MessageRecipientSchema.RecipientIdProperty,
				ChildKeyProperty = MessageRecipientStatusSchema.RecipientIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EmailHashKeyProperty, CommonMessageTraceSchema.EmailHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventPropertiesTableProperty,
				ParentKeyProperty = MessageEventSchema.EventIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventRulesTableProperty,
				ParentKeyProperty = MessageEventSchema.EventIdProperty,
				ChildKeyProperty = MessageEventRuleSchema.EventIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty,
				ParentKeyProperty = MessageEventSchema.EventIdProperty,
				ChildKeyProperty = MessageEventSourceItemSchema.EventIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageRecipientStatusTableProperty,
				ParentKeyProperty = MessageEventSchema.EventIdProperty,
				ChildKeyProperty = MessageRecipientStatusSchema.EventIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventRulesTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageActionTableProperty,
				ParentKeyProperty = MessageEventRuleSchema.EventRuleIdProperty,
				ChildKeyProperty = MessageActionSchema.EventRuleIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageEventRuleSchema.RuleIdProperty, CommonMessageTraceSchema.RuleIdProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventRulesTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventRulePropertiesTableProperty,
				ParentKeyProperty = MessageEventRuleSchema.EventRuleIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageEventRuleSchema.RuleIdProperty, PropertyBase.ParentObjectIdProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventRulesTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty,
				ParentKeyProperty = MessageEventRuleSchema.EventRuleIdProperty,
				ChildKeyProperty = MessageEventRuleClassificationSchema.EventRuleIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.RuleIdProperty, CommonMessageTraceSchema.RuleIdProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventRuleClassificationPropertiesTableProperty,
				ParentKeyProperty = MessageEventRuleClassificationSchema.EventRuleClassificationIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.RuleIdProperty, PropertyBase.ParentObjectIdProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageEventRuleClassificationSchema.DataClassificationIdProperty, PropertyBase.RefObjectIdProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageEventSourceItemPropertiesTableProperty,
				ParentKeyProperty = MessageEventSourceItemSchema.SourceItemIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageEventSourceItemSchema.NameProperty, PropertyBase.RefNameProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageClassificationsTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageClassificationPropertiesTableProperty,
				ParentKeyProperty = MessageClassificationSchema.ClassificationIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageClassificationSchema.DataClassificationIdProperty, PropertyBase.ParentObjectIdProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageClientInformationTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageClientInformationPropertiesTableProperty,
				ParentKeyProperty = MessageClientInformationSchema.ClientInformationIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageClientInformationSchema.DataClassificationIdProperty, PropertyBase.ParentObjectIdProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageRecipientStatusTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageRecipientStatusPropertiesTableProperty,
				ParentKeyProperty = MessageRecipientStatusSchema.RecipientStatusIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EmailHashKeyProperty, CommonMessageTraceSchema.EmailHashKeyProperty)
				}
			});
			MessageTraceForSaveDataSet.mappingInfoList.Add(new MessageTraceForSaveDataSet.MappingInfo
			{
				ParentTableProperty = MessageTraceDataSetSchema.MessageActionTableProperty,
				ChildTableProperty = MessageTraceDataSetSchema.MessageActionPropertiesTableProperty,
				ParentKeyProperty = MessageActionSchema.RuleActionIdProperty,
				ChildKeyProperty = PropertyBase.ParentIdProperty,
				PropertyMappings = new List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>>
				{
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.RuleIdProperty, PropertyBase.ParentObjectIdProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(CommonMessageTraceSchema.EventHashKeyProperty, CommonMessageTraceSchema.EventHashKeyProperty),
					new Tuple<HygienePropertyDefinition, HygienePropertyDefinition>(MessageActionSchema.NameProperty, PropertyBase.RefNameProperty)
				}
			});
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0002DB35 File Offset: 0x0002BD35
		public override ObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x0002DB3D File Offset: 0x0002BD3D
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x0002DB4A File Offset: 0x0002BD4A
		public object PhysicalPartionId
		{
			get
			{
				return this[CommonMessageTraceSchema.PhysicalInstanceKeyProp];
			}
			set
			{
				this[CommonMessageTraceSchema.PhysicalInstanceKeyProp] = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0002DB58 File Offset: 0x0002BD58
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0002DB65 File Offset: 0x0002BD65
		public object FssCopyId
		{
			get
			{
				return this[CommonMessageTraceSchema.FssCopyIdProp];
			}
			set
			{
				this[CommonMessageTraceSchema.FssCopyIdProp] = value;
			}
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002DB74 File Offset: 0x0002BD74
		public static MessageTraceForSaveDataSet CreateDataSet(object partitionId, Guid? organizationId, IEnumerable<MessageTrace> messageList, int? fssCopyId = null)
		{
			if (partitionId == null && (organizationId == null || organizationId == null))
			{
				throw new ArgumentException("CreateDataSet call is invalid. Both partitionId and organizationId cannot be null");
			}
			if (partitionId != null && organizationId != null && organizationId != null)
			{
				throw new ArgumentException("CreateDataSet call is invalid. Shouldn't set both partitionId and organizationId.");
			}
			MessageTraceForSaveDataSet.FillConfigurablePropertyBagFromGraph fillConfigurablePropertyBagFromGraph = new MessageTraceForSaveDataSet.FillConfigurablePropertyBagFromGraph();
			foreach (MessageTrace messageTrace in messageList)
			{
				MessageTraceForSaveDataSet.SetMessageProperties(messageTrace);
				messageTrace.Accept(fillConfigurablePropertyBagFromGraph);
			}
			if (partitionId != null)
			{
				fillConfigurablePropertyBagFromGraph.PropertyBag.PhysicalPartionId = (int)partitionId;
			}
			if (organizationId != null && organizationId != null)
			{
				fillConfigurablePropertyBagFromGraph.PropertyBag[CommonMessageTraceSchema.OrganizationalUnitRootProperty] = organizationId.Value;
			}
			if (fssCopyId != null)
			{
				fillConfigurablePropertyBagFromGraph.PropertyBag.FssCopyId = fssCopyId;
			}
			fillConfigurablePropertyBagFromGraph.PropertyBag.identity = new ConfigObjectId(Guid.NewGuid().ToString());
			fillConfigurablePropertyBagFromGraph.PropertyBag.ValidateObject();
			return fillConfigurablePropertyBagFromGraph.PropertyBag;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		public override Type GetSchemaType()
		{
			return typeof(MessageTraceDataSetSchema);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0002DCAC File Offset: 0x0002BEAC
		public void ValidateObject()
		{
			this.RemoveDuplicates();
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002DCB4 File Offset: 0x0002BEB4
		public int GetDatasize()
		{
			int num = 0;
			foreach (HygienePropertyDefinition propertyDefinition in MessageTraceForSaveDataSet.tvpDataTables)
			{
				DataTable dataTable = this[propertyDefinition] as DataTable;
				if (dataTable != null)
				{
					num += dataTable.Rows.Count;
				}
			}
			return num;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002DE54 File Offset: 0x0002C054
		private static void SetProperties(DataTable parentTable, DataTable childTable, HygienePropertyDefinition parentKey, HygienePropertyDefinition childKey, List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>> propertyMappings)
		{
			if (!parentTable.Columns.Contains(parentKey.Name) || !childTable.Columns.Contains(childKey.Name) || propertyMappings == null || propertyMappings.Count == 0)
			{
				return;
			}
			var enumerable = from childRow in childTable.AsEnumerable()
			join parentRow in parentTable.AsEnumerable() on childRow[childKey.Name] equals parentRow[parentKey.Name]
			select new
			{
				ChildRow = childRow,
				ParentRow = parentRow
			};
			foreach (var <>f__AnonymousType in enumerable)
			{
				foreach (Tuple<HygienePropertyDefinition, HygienePropertyDefinition> tuple in propertyMappings)
				{
					if (childTable.Columns.Contains(tuple.Item2.Name))
					{
						<>f__AnonymousType.ChildRow[tuple.Item2.Name] = <>f__AnonymousType.ParentRow[tuple.Item1.Name];
					}
				}
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		private static void SetMessageProperties(DataTable messageTable)
		{
			if (messageTable == null)
			{
				return;
			}
			foreach (object obj in messageTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (dataRow[MessageTraceSchema.IPAddressProperty.Name] is string)
				{
					dataRow[CommonMessageTraceSchema.IPHashKeyProperty.Name] = DalHelper.GetSHA1Hash((dataRow[MessageTraceSchema.IPAddressProperty.Name] as string).ToLower());
				}
				dataRow[MessageTraceSchema.FromEmailPrefixProperty.Name] = MessageTraceEntityBase.StandardizeEmailPrefix(dataRow[MessageTraceSchema.FromEmailPrefixProperty.Name] as string);
				dataRow[MessageTraceSchema.FromEmailDomainProperty.Name] = MessageTraceEntityBase.StandardizeEmailDomain(dataRow[MessageTraceSchema.FromEmailDomainProperty.Name] as string);
				dataRow[CommonMessageTraceSchema.EmailDomainHashKeyProperty.Name] = (MessageTraceEntityBase.GetEmailDomainHashKey(dataRow[MessageTraceSchema.FromEmailDomainProperty.Name] as string) ?? MessageTraceEntityBase.EmptyEmailDomainHashKey);
				dataRow[CommonMessageTraceSchema.EmailHashKeyProperty.Name] = (MessageTraceEntityBase.GetEmailHashKey(dataRow[MessageTraceSchema.FromEmailPrefixProperty.Name] as string, dataRow[MessageTraceSchema.FromEmailDomainProperty.Name] as string) ?? MessageTraceEntityBase.EmptyEmailHashKey);
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002E134 File Offset: 0x0002C334
		private static void SetMessageProperties(MessageTrace messageTrace)
		{
			Guid exMessageId = messageTrace.ExMessageId;
			if (messageTrace.ExMessageId == Guid.Empty)
			{
				throw new ArgumentException("MessageTrace object has an invalid ExMessageId (null or empty)");
			}
			if (messageTrace.IPAddress == null)
			{
				messageTrace[CommonMessageTraceSchema.IPHashKeyProperty] = null;
			}
			else
			{
				messageTrace[CommonMessageTraceSchema.IPHashKeyProperty] = DalHelper.GetSHA1Hash(messageTrace.IPAddress.ToString().ToLower());
			}
			messageTrace.FromEmailPrefix = MessageTraceEntityBase.StandardizeEmailPrefix(messageTrace.FromEmailPrefix);
			messageTrace.FromEmailDomain = MessageTraceEntityBase.StandardizeEmailDomain(messageTrace.FromEmailDomain);
			messageTrace[CommonMessageTraceSchema.EmailDomainHashKeyProperty] = (MessageTraceEntityBase.GetEmailDomainHashKey(messageTrace.FromEmailDomain) ?? MessageTraceEntityBase.EmptyEmailDomainHashKey);
			messageTrace[CommonMessageTraceSchema.EmailHashKeyProperty] = (MessageTraceEntityBase.GetEmailHashKey(messageTrace.FromEmailPrefix, messageTrace.FromEmailDomain) ?? MessageTraceEntityBase.EmptyEmailHashKey);
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002E204 File Offset: 0x0002C404
		private static TvpInfo CreateTvpInfoPrototype(HygienePropertyDefinition tableName, HygienePropertyDefinition[] columnDefinitions)
		{
			HygienePropertyDefinition[] array = new HygienePropertyDefinition[columnDefinitions.Length];
			DataTable dataTable = new DataTable();
			DataColumnCollection columns = dataTable.Columns;
			dataTable.TableName = tableName.Name;
			foreach (HygienePropertyDefinition hygienePropertyDefinition in columnDefinitions)
			{
				if (!hygienePropertyDefinition.IsCalculated)
				{
					DataColumn dataColumn = columns.Add(hygienePropertyDefinition.Name, (hygienePropertyDefinition.Type == typeof(byte[])) ? hygienePropertyDefinition.Type : DalHelper.ConvertToStoreType(hygienePropertyDefinition));
					array[dataColumn.Ordinal] = hygienePropertyDefinition;
				}
			}
			dataTable.BeginLoadData();
			return new TvpInfo(tableName, dataTable, array);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		private static MessageTraceForSaveDataSet CreateMessageTraceForSaveDataSet()
		{
			MessageTraceForSaveDataSet messageTraceForSaveDataSet = new MessageTraceForSaveDataSet();
			foreach (TvpInfo tvpInfo in MessageTraceForSaveDataSet.tvpPrototypeList)
			{
				messageTraceForSaveDataSet[tvpInfo.TableName] = tvpInfo.Tvp.Clone();
			}
			return messageTraceForSaveDataSet;
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0002E328 File Offset: 0x0002C528
		private void SyncProperties()
		{
			foreach (HygienePropertyDefinition hygienePropertyDefinition in MessageTraceForSaveDataSet.mapTableToTvpColumnInfo.Keys)
			{
				DataTable table = this[hygienePropertyDefinition] as DataTable;
				if (table != null)
				{
					foreach (HygienePropertyDefinition hygienePropertyDefinition2 in MessageTraceForSaveDataSet.mapTableToTvpColumnInfo[hygienePropertyDefinition])
					{
						if (!table.Columns.Contains(hygienePropertyDefinition2.Name))
						{
							table.Columns.Add(hygienePropertyDefinition2.Name, (hygienePropertyDefinition2.Type == typeof(byte[])) ? hygienePropertyDefinition2.Type : DalHelper.ConvertToStoreType(hygienePropertyDefinition2));
						}
					}
					int i;
					for (i = 0; i < table.Columns.Count; i++)
					{
						if (MessageTraceForSaveDataSet.mapTableToTvpColumnInfo[hygienePropertyDefinition].FirstOrDefault((HygienePropertyDefinition propertyDefinition) => string.Compare(propertyDefinition.Name, table.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase) == 0) == null)
						{
							table.Columns.RemoveAt(i--);
						}
					}
				}
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0002E4C0 File Offset: 0x0002C6C0
		private void RemoveDuplicates()
		{
			DataTable table = this[MessageTraceDataSetSchema.MessageEventsTableProperty] as DataTable;
			if (this.RemoveDuplicates(table, MessageTraceForSaveDataSet.eventComparer))
			{
				DataTable table2 = this[MessageTraceDataSetSchema.MessageEventPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table2, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table3 = this[MessageTraceDataSetSchema.MessageRecipientStatusTableProperty] as DataTable;
			if (this.RemoveDuplicates(table3, MessageTraceForSaveDataSet.recipientStatusComparer))
			{
				DataTable table4 = this[MessageTraceDataSetSchema.MessageRecipientStatusPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table4, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table5 = this[MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty] as DataTable;
			if (this.RemoveDuplicates(table5, MessageTraceForSaveDataSet.eventSourceItemComparer))
			{
				DataTable table6 = this[MessageTraceDataSetSchema.MessageEventSourceItemPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table6, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table7 = this[MessageTraceDataSetSchema.MessageEventRulesTableProperty] as DataTable;
			if (this.RemoveDuplicates(table7, MessageTraceForSaveDataSet.eventRuleComparer))
			{
				DataTable table8 = this[MessageTraceDataSetSchema.MessageEventRulePropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table8, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table9 = this[MessageTraceDataSetSchema.MessageActionTableProperty] as DataTable;
			if (this.RemoveDuplicates(table9, MessageTraceForSaveDataSet.actionComparer))
			{
				DataTable table10 = this[MessageTraceDataSetSchema.MessageActionPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table10, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table11 = this[MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty] as DataTable;
			if (this.RemoveDuplicates(table11, MessageTraceForSaveDataSet.eventRuleClassificationComparer))
			{
				DataTable table12 = this[MessageTraceDataSetSchema.MessageEventRuleClassificationPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table12, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table13 = this[MessageTraceDataSetSchema.MessageRecipientsTableProperty] as DataTable;
			if (this.RemoveDuplicates(table13, MessageTraceForSaveDataSet.recipientsComparer))
			{
				DataTable table14 = this[MessageTraceDataSetSchema.MessageRecipientPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table14, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table15 = this[MessageTraceDataSetSchema.MessageClassificationsTableProperty] as DataTable;
			if (this.RemoveDuplicates(table15, MessageTraceForSaveDataSet.classificationsComparer))
			{
				DataTable table16 = this[MessageTraceDataSetSchema.MessageClassificationPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table16, MessageTraceForSaveDataSet.propertyComparer);
			}
			DataTable table17 = this[MessageTraceDataSetSchema.MessageClientInformationTableProperty] as DataTable;
			if (this.RemoveDuplicates(table17, MessageTraceForSaveDataSet.clientInformationsComparer))
			{
				DataTable table18 = this[MessageTraceDataSetSchema.MessageClientInformationPropertiesTableProperty] as DataTable;
				this.RemoveDuplicates(table18, MessageTraceForSaveDataSet.propertyComparer);
			}
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0002E710 File Offset: 0x0002C910
		private bool RemoveDuplicates(DataTable table, MessageTraceForSaveDataSet.MessageRowComparer comparer)
		{
			if (table == null || table.Rows.Count == 0)
			{
				return false;
			}
			bool result = false;
			HashSet<DataRow> hashSet = new HashSet<DataRow>(comparer);
			int i = 0;
			while (i < table.Rows.Count)
			{
				if (!hashSet.Contains(table.Rows[i]))
				{
					hashSet.Add(table.Rows[i++]);
				}
				else
				{
					table.Rows.RemoveAt(i);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x040006F0 RID: 1776
		private static TvpInfo[] tvpPrototypeList = new TvpInfo[]
		{
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessagesTableProperty, MessageTrace.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessagePropertiesTableProperty, MessageProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageActionTableProperty, MessageAction.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageActionPropertiesTableProperty, MessageActionProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventsTableProperty, MessageEvent.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventPropertiesTableProperty, MessageEventProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventRulesTableProperty, MessageEventRule.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventRulePropertiesTableProperty, MessageEventRuleProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty, MessageEventSourceItem.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventSourceItemPropertiesTableProperty, MessageEventSourceItemProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageRecipientsTableProperty, MessageRecipient.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageRecipientPropertiesTableProperty, MessageRecipientProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageRecipientStatusTableProperty, MessageRecipientStatus.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageRecipientStatusPropertiesTableProperty, MessageRecipientStatusProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageClassificationsTableProperty, MessageClassification.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageClassificationPropertiesTableProperty, MessageClassificationProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty, MessageEventRuleClassification.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageEventRuleClassificationPropertiesTableProperty, MessageEventRuleClassificationProperty.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageClientInformationTableProperty, MessageClientInformation.Properties),
			MessageTraceForSaveDataSet.CreateTvpInfoPrototype(MessageTraceDataSetSchema.MessageClientInformationPropertiesTableProperty, MessageClientInformationProperty.Properties)
		};

		// Token: 0x040006F1 RID: 1777
		private static HygienePropertyDefinition[] tvpDataTables = new HygienePropertyDefinition[]
		{
			MessageTraceDataSetSchema.MessagesTableProperty,
			MessageTraceDataSetSchema.MessagePropertiesTableProperty,
			MessageTraceDataSetSchema.MessageActionTableProperty,
			MessageTraceDataSetSchema.MessageActionPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageEventsTableProperty,
			MessageTraceDataSetSchema.MessageEventPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageEventRulesTableProperty,
			MessageTraceDataSetSchema.MessageEventRulePropertiesTableProperty,
			MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty,
			MessageTraceDataSetSchema.MessageEventSourceItemPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageRecipientsTableProperty,
			MessageTraceDataSetSchema.MessageRecipientPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageRecipientStatusTableProperty,
			MessageTraceDataSetSchema.MessageRecipientStatusPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageClassificationsTableProperty,
			MessageTraceDataSetSchema.MessageClassificationPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty,
			MessageTraceDataSetSchema.MessageEventRuleClassificationPropertiesTableProperty,
			MessageTraceDataSetSchema.MessageClientInformationTableProperty,
			MessageTraceDataSetSchema.MessageClientInformationPropertiesTableProperty
		};

		// Token: 0x040006F2 RID: 1778
		private static Dictionary<HygienePropertyDefinition, HygienePropertyDefinition[]> mapTableToTvpColumnInfo;

		// Token: 0x040006F3 RID: 1779
		private static List<MessageTraceForSaveDataSet.MappingInfo> mappingInfoList;

		// Token: 0x040006F4 RID: 1780
		private static MessageTraceForSaveDataSet.MessageEventRowComparer eventComparer = new MessageTraceForSaveDataSet.MessageEventRowComparer();

		// Token: 0x040006F5 RID: 1781
		private static MessageTraceForSaveDataSet.MessageRecipientStatusRowComparer recipientStatusComparer = new MessageTraceForSaveDataSet.MessageRecipientStatusRowComparer();

		// Token: 0x040006F6 RID: 1782
		private static MessageTraceForSaveDataSet.MessageEventSourceItemRowComparer eventSourceItemComparer = new MessageTraceForSaveDataSet.MessageEventSourceItemRowComparer();

		// Token: 0x040006F7 RID: 1783
		private static MessageTraceForSaveDataSet.MessageEventRuleRowComparer eventRuleComparer = new MessageTraceForSaveDataSet.MessageEventRuleRowComparer();

		// Token: 0x040006F8 RID: 1784
		private static MessageTraceForSaveDataSet.MessageActionRowComparer actionComparer = new MessageTraceForSaveDataSet.MessageActionRowComparer();

		// Token: 0x040006F9 RID: 1785
		private static MessageTraceForSaveDataSet.MessageEventRuleClassificationRowComparer eventRuleClassificationComparer = new MessageTraceForSaveDataSet.MessageEventRuleClassificationRowComparer();

		// Token: 0x040006FA RID: 1786
		private static MessageTraceForSaveDataSet.MessageRecipientRowComparer recipientsComparer = new MessageTraceForSaveDataSet.MessageRecipientRowComparer();

		// Token: 0x040006FB RID: 1787
		private static MessageTraceForSaveDataSet.MessageClassificationRowComparer classificationsComparer = new MessageTraceForSaveDataSet.MessageClassificationRowComparer();

		// Token: 0x040006FC RID: 1788
		private static MessageTraceForSaveDataSet.MessageClientInformationRowComparer clientInformationsComparer = new MessageTraceForSaveDataSet.MessageClientInformationRowComparer();

		// Token: 0x040006FD RID: 1789
		private static MessageTraceForSaveDataSet.MessagePropertyRowComparer propertyComparer = new MessageTraceForSaveDataSet.MessagePropertyRowComparer();

		// Token: 0x040006FE RID: 1790
		private ObjectId identity;

		// Token: 0x02000170 RID: 368
		private class FillConfigurablePropertyBagFromGraph : MessageTraceVisitorBase
		{
			// Token: 0x06000F0F RID: 3855 RVA: 0x0002E7C0 File Offset: 0x0002C9C0
			public FillConfigurablePropertyBagFromGraph()
			{
				this.bag = MessageTraceForSaveDataSet.CreateMessageTraceForSaveDataSet();
			}

			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0002E841 File Offset: 0x0002CA41
			public MessageTraceForSaveDataSet PropertyBag
			{
				get
				{
					return this.bag;
				}
			}

			// Token: 0x06000F11 RID: 3857 RVA: 0x0002E849 File Offset: 0x0002CA49
			public override void Visit(MessageTrace messageTraceInfo)
			{
				this.lastVisitedTrace = messageTraceInfo;
				this.SerializeObjectToDataTable<MessageTrace>(messageTraceInfo, MessageTraceDataSetSchema.MessagesTableProperty);
			}

			// Token: 0x06000F12 RID: 3858 RVA: 0x0002E85E File Offset: 0x0002CA5E
			public override void Visit(MessageProperty messageProperty)
			{
				this.ApplyMessageProperties(messageProperty);
				this.SerializeObjectToDataTable<MessageProperty>(messageProperty, MessageTraceDataSetSchema.MessagePropertiesTableProperty);
			}

			// Token: 0x06000F13 RID: 3859 RVA: 0x0002E874 File Offset: 0x0002CA74
			public override void Visit(MessageEvent messageEvent)
			{
				this.ApplyMessageProperties(messageEvent);
				string inputString = string.Format("{0}{1:yyyy-MM-dd hh:mm:ss.fffffff}{2}", (int)messageEvent.EventType, messageEvent.TimeStamp, (int)messageEvent.EventSource);
				messageEvent[CommonMessageTraceSchema.EventHashKeyProperty] = DalHelper.GetMDHash(inputString);
				this.messageEventIdMap[messageEvent.EventId] = messageEvent;
				this.SerializeObjectToDataTable<MessageEvent>(messageEvent, MessageTraceDataSetSchema.MessageEventsTableProperty);
			}

			// Token: 0x06000F14 RID: 3860 RVA: 0x0002E8E3 File Offset: 0x0002CAE3
			public override void Visit(MessageEventProperty messageEventProperty)
			{
				this.ApplyMessageProperties(messageEventProperty);
				this.ApplyMessageEventProperties(this.messageEventIdMap[messageEventProperty.EventId], messageEventProperty);
				this.SerializeObjectToDataTable<MessageEventProperty>(messageEventProperty, MessageTraceDataSetSchema.MessageEventPropertiesTableProperty);
			}

			// Token: 0x06000F15 RID: 3861 RVA: 0x0002E910 File Offset: 0x0002CB10
			public override void Visit(MessageRecipient messageRecipient)
			{
				this.ApplyMessageProperties(messageRecipient);
				messageRecipient.ToEmailPrefix = MessageTraceEntityBase.StandardizeEmailPrefix(messageRecipient.ToEmailPrefix);
				messageRecipient.ToEmailDomain = MessageTraceEntityBase.StandardizeEmailDomain(messageRecipient.ToEmailDomain);
				messageRecipient[CommonMessageTraceSchema.EmailDomainHashKeyProperty] = MessageTraceEntityBase.GetEmailDomainHashKey(messageRecipient.ToEmailDomain);
				messageRecipient[CommonMessageTraceSchema.EmailHashKeyProperty] = MessageTraceEntityBase.GetEmailHashKey(messageRecipient.ToEmailPrefix, messageRecipient.ToEmailDomain);
				this.recipientIdMap[messageRecipient.RecipientId] = messageRecipient;
				this.SerializeObjectToDataTable<MessageRecipient>(messageRecipient, MessageTraceDataSetSchema.MessageRecipientsTableProperty);
			}

			// Token: 0x06000F16 RID: 3862 RVA: 0x0002E996 File Offset: 0x0002CB96
			public override void Visit(MessageRecipientProperty messageRecipientProperty)
			{
				this.ApplyMessageProperties(messageRecipientProperty);
				this.ApplyRecipientProperties(this.recipientIdMap[messageRecipientProperty.RecipientId], messageRecipientProperty);
				this.SerializeObjectToDataTable<MessageRecipientProperty>(messageRecipientProperty, MessageTraceDataSetSchema.MessageRecipientPropertiesTableProperty);
			}

			// Token: 0x06000F17 RID: 3863 RVA: 0x0002E9C4 File Offset: 0x0002CBC4
			public override void Visit(MessageEventRule messageEventRule)
			{
				this.ApplyMessageProperties(messageEventRule);
				this.messageEventRuleIdMap[messageEventRule.EventRuleId] = messageEventRule;
				this.ApplyMessageEventProperties(this.messageEventIdMap[messageEventRule.EventId], messageEventRule);
				this.messageEventRuleIdMap[messageEventRule.EventRuleId] = messageEventRule;
				this.SerializeObjectToDataTable<MessageEventRule>(messageEventRule, MessageTraceDataSetSchema.MessageEventRulesTableProperty);
			}

			// Token: 0x06000F18 RID: 3864 RVA: 0x0002EA20 File Offset: 0x0002CC20
			public override void Visit(MessageEventRuleProperty messageEventRuleProperty)
			{
				this.ApplyMessageProperties(messageEventRuleProperty);
				this.ApplyMessageEventRuleExProperties(this.messageEventRuleIdMap[messageEventRuleProperty.EventRuleId], messageEventRuleProperty);
				this.SerializeObjectToDataTable<MessageEventRuleProperty>(messageEventRuleProperty, MessageTraceDataSetSchema.MessageEventRulePropertiesTableProperty);
			}

			// Token: 0x06000F19 RID: 3865 RVA: 0x0002EA4D File Offset: 0x0002CC4D
			public override void Visit(MessageEventRuleClassification messageEventRuleClassification)
			{
				this.ApplyMessageProperties(messageEventRuleClassification);
				this.ApplyMessageEventRuleProperties(this.messageEventRuleIdMap[messageEventRuleClassification.EventRuleId], messageEventRuleClassification);
				this.messageEventRuleClassificationIdMap[messageEventRuleClassification.EventRuleClassificationId] = messageEventRuleClassification;
				this.SerializeObjectToDataTable<MessageEventRuleClassification>(messageEventRuleClassification, MessageTraceDataSetSchema.MessageEventRuleClassificationsTableProperty);
			}

			// Token: 0x06000F1A RID: 3866 RVA: 0x0002EA8C File Offset: 0x0002CC8C
			public override void Visit(MessageEventRuleClassificationProperty messageEventRuleClassificationProperty)
			{
				this.ApplyMessageProperties(messageEventRuleClassificationProperty);
				this.ApplyMessageEventRuleClassificationExProperties(this.messageEventRuleClassificationIdMap[messageEventRuleClassificationProperty.EventRuleClassificationId], messageEventRuleClassificationProperty);
				this.SerializeObjectToDataTable<MessageEventRuleClassificationProperty>(messageEventRuleClassificationProperty, MessageTraceDataSetSchema.MessageEventRuleClassificationPropertiesTableProperty);
			}

			// Token: 0x06000F1B RID: 3867 RVA: 0x0002EAB9 File Offset: 0x0002CCB9
			public override void Visit(MessageEventSourceItem messageEventSourceItem)
			{
				this.ApplyMessageProperties(messageEventSourceItem);
				this.ApplyMessageEventProperties(this.messageEventIdMap[messageEventSourceItem.EventId], messageEventSourceItem);
				this.messageEventSourceItemIdMap[messageEventSourceItem.SourceItemId] = messageEventSourceItem;
				this.SerializeObjectToDataTable<MessageEventSourceItem>(messageEventSourceItem, MessageTraceDataSetSchema.MessageEventSourceItemsTableProperty);
			}

			// Token: 0x06000F1C RID: 3868 RVA: 0x0002EAF8 File Offset: 0x0002CCF8
			public override void Visit(MessageEventSourceItemProperty messageEventSourceItemProperty)
			{
				this.ApplyMessageProperties(messageEventSourceItemProperty);
				this.ApplyMessageEventSourceItemExProperties(this.messageEventSourceItemIdMap[messageEventSourceItemProperty.SourceItemId], messageEventSourceItemProperty);
				this.SerializeObjectToDataTable<MessageEventSourceItemProperty>(messageEventSourceItemProperty, MessageTraceDataSetSchema.MessageEventSourceItemPropertiesTableProperty);
			}

			// Token: 0x06000F1D RID: 3869 RVA: 0x0002EB25 File Offset: 0x0002CD25
			public override void Visit(MessageClassification messageClassification)
			{
				this.ApplyMessageProperties(messageClassification);
				this.messageClassificationIdMap[messageClassification.ClassificationId] = messageClassification;
				this.SerializeObjectToDataTable<MessageClassification>(messageClassification, MessageTraceDataSetSchema.MessageClassificationsTableProperty);
			}

			// Token: 0x06000F1E RID: 3870 RVA: 0x0002EB4C File Offset: 0x0002CD4C
			public override void Visit(MessageClassificationProperty messageClassificationProperty)
			{
				this.ApplyMessageProperties(messageClassificationProperty);
				this.ApplyMessageClassificationExProperty(this.messageClassificationIdMap[messageClassificationProperty.ClassificationId], messageClassificationProperty);
				this.SerializeObjectToDataTable<MessageClassificationProperty>(messageClassificationProperty, MessageTraceDataSetSchema.MessageClassificationPropertiesTableProperty);
			}

			// Token: 0x06000F1F RID: 3871 RVA: 0x0002EB79 File Offset: 0x0002CD79
			public override void Visit(MessageClientInformation messageClientInformation)
			{
				this.ApplyMessageProperties(messageClientInformation);
				this.messageClientInformationIdMap[messageClientInformation.ClientInformationId] = messageClientInformation;
				this.SerializeObjectToDataTable<MessageClientInformation>(messageClientInformation, MessageTraceDataSetSchema.MessageClientInformationTableProperty);
			}

			// Token: 0x06000F20 RID: 3872 RVA: 0x0002EBA0 File Offset: 0x0002CDA0
			public override void Visit(MessageClientInformationProperty messageClientInformationProperty)
			{
				this.ApplyMessageProperties(messageClientInformationProperty);
				this.ApplyMessageClientInformationExProperties(this.messageClientInformationIdMap[messageClientInformationProperty.ClientInformationId], messageClientInformationProperty);
				this.SerializeObjectToDataTable<MessageClientInformationProperty>(messageClientInformationProperty, MessageTraceDataSetSchema.MessageClientInformationPropertiesTableProperty);
			}

			// Token: 0x06000F21 RID: 3873 RVA: 0x0002EBD0 File Offset: 0x0002CDD0
			public override void Visit(MessageRecipientStatus recipientStatus)
			{
				this.ApplyMessageProperties(recipientStatus);
				this.ApplyRecipientProperties(this.recipientIdMap[recipientStatus.RecipientId], recipientStatus);
				this.ApplyMessageEventProperties(this.messageEventIdMap[recipientStatus.EventId], recipientStatus);
				this.messageRecipientStatusIdMap[recipientStatus.RecipientStatusId] = recipientStatus;
				this.SerializeObjectToDataTable<MessageRecipientStatus>(recipientStatus, MessageTraceDataSetSchema.MessageRecipientStatusTableProperty);
			}

			// Token: 0x06000F22 RID: 3874 RVA: 0x0002EC32 File Offset: 0x0002CE32
			public override void Visit(MessageRecipientStatusProperty recipientStatusProperty)
			{
				this.ApplyMessageProperties(recipientStatusProperty);
				this.ApplyMessageRecipientStatusExProperties(this.messageRecipientStatusIdMap[recipientStatusProperty.RecipientStatusId], recipientStatusProperty);
				this.SerializeObjectToDataTable<MessageRecipientStatusProperty>(recipientStatusProperty, MessageTraceDataSetSchema.MessageRecipientStatusPropertiesTableProperty);
			}

			// Token: 0x06000F23 RID: 3875 RVA: 0x0002EC60 File Offset: 0x0002CE60
			public override void Visit(MessageAction messageAction)
			{
				this.ApplyMessageProperties(messageAction);
				if (messageAction.EventRuleId != Guid.Empty)
				{
					this.ApplyMessageEventRuleProperties(this.messageEventRuleIdMap[messageAction.EventRuleId], messageAction);
				}
				this.messageActionIdMap[messageAction.RuleActionId] = messageAction;
				this.SerializeObjectToDataTable<MessageAction>(messageAction, MessageTraceDataSetSchema.MessageActionTableProperty);
			}

			// Token: 0x06000F24 RID: 3876 RVA: 0x0002ECBC File Offset: 0x0002CEBC
			public override void Visit(MessageActionProperty messageActionProperty)
			{
				this.ApplyMessageProperties(messageActionProperty);
				this.ApplyMessageActionExProperties(this.messageActionIdMap[messageActionProperty.RuleActionId], messageActionProperty);
				this.SerializeObjectToDataTable<MessageActionProperty>(messageActionProperty, MessageTraceDataSetSchema.MessageActionPropertiesTableProperty);
			}

			// Token: 0x06000F25 RID: 3877 RVA: 0x0002ECE9 File Offset: 0x0002CEE9
			private void ApplyMessageProperties(MessageTraceEntityBase entity)
			{
				entity[CommonMessageTraceSchema.ExMessageIdProperty] = this.lastVisitedTrace.ExMessageId;
				entity[CommonMessageTraceSchema.HashBucketProperty] = this.lastVisitedTrace[CommonMessageTraceSchema.HashBucketProperty];
			}

			// Token: 0x06000F26 RID: 3878 RVA: 0x0002ED21 File Offset: 0x0002CF21
			private void ApplyMessageEventProperties(MessageEvent parentEvent, MessageTraceEntityBase entity)
			{
				entity[CommonMessageTraceSchema.EventIdProperty] = parentEvent.EventId;
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentEvent[CommonMessageTraceSchema.EventHashKeyProperty];
			}

			// Token: 0x06000F27 RID: 3879 RVA: 0x0002ED50 File Offset: 0x0002CF50
			private void ApplyMessageEventRuleProperties(MessageEventRule parentEventRule, MessageTraceEntityBase entity)
			{
				entity[MessageEventRuleSchema.EventRuleIdProperty] = parentEventRule.EventRuleId;
				entity[CommonMessageTraceSchema.RuleIdProperty] = parentEventRule.RuleId;
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentEventRule[CommonMessageTraceSchema.EventHashKeyProperty];
			}

			// Token: 0x06000F28 RID: 3880 RVA: 0x0002ED9F File Offset: 0x0002CF9F
			private void ApplyMessageEventRuleExProperties(MessageEventRule parentEventRule, MessageEventRuleProperty entity)
			{
				entity[PropertyBase.ParentObjectIdProperty] = parentEventRule.RuleId;
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentEventRule[CommonMessageTraceSchema.EventHashKeyProperty];
			}

			// Token: 0x06000F29 RID: 3881 RVA: 0x0002EDCD File Offset: 0x0002CFCD
			private void ApplyMessageActionExProperties(MessageAction parentAction, MessageActionProperty entity)
			{
				entity[PropertyBase.ParentObjectIdProperty] = parentAction[CommonMessageTraceSchema.RuleIdProperty];
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentAction[CommonMessageTraceSchema.EventHashKeyProperty];
				entity[PropertyBase.RefNameProperty] = parentAction.Name;
			}

			// Token: 0x06000F2A RID: 3882 RVA: 0x0002EE0C File Offset: 0x0002D00C
			private void ApplyMessageEventRuleClassificationExProperties(MessageEventRuleClassification parentRuleClassification, MessageEventRuleClassificationProperty entity)
			{
				entity[PropertyBase.ParentObjectIdProperty] = parentRuleClassification[CommonMessageTraceSchema.RuleIdProperty];
				entity[PropertyBase.RefObjectIdProperty] = parentRuleClassification.DataClassificationId;
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentRuleClassification[CommonMessageTraceSchema.EventHashKeyProperty];
			}

			// Token: 0x06000F2B RID: 3883 RVA: 0x0002EE5B File Offset: 0x0002D05B
			private void ApplyMessageEventSourceItemExProperties(MessageEventSourceItem parentSourceItem, MessageEventSourceItemProperty entity)
			{
				entity[PropertyBase.RefNameProperty] = parentSourceItem.Name;
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentSourceItem[CommonMessageTraceSchema.EventHashKeyProperty];
			}

			// Token: 0x06000F2C RID: 3884 RVA: 0x0002EE84 File Offset: 0x0002D084
			private void ApplyRecipientProperties(MessageRecipient parentRecipient, MessageTraceEntityBase entity)
			{
				entity[MessageRecipientSchema.RecipientIdProperty] = parentRecipient.RecipientId;
				entity[CommonMessageTraceSchema.EmailHashKeyProperty] = parentRecipient[CommonMessageTraceSchema.EmailHashKeyProperty];
			}

			// Token: 0x06000F2D RID: 3885 RVA: 0x0002EEB2 File Offset: 0x0002D0B2
			private void ApplyMessageRecipientStatusExProperties(MessageRecipientStatus parentRecipientStatus, MessageRecipientStatusProperty entity)
			{
				entity[CommonMessageTraceSchema.EventHashKeyProperty] = parentRecipientStatus[CommonMessageTraceSchema.EventHashKeyProperty];
				entity[CommonMessageTraceSchema.EmailHashKeyProperty] = parentRecipientStatus[CommonMessageTraceSchema.EmailHashKeyProperty];
			}

			// Token: 0x06000F2E RID: 3886 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
			private void ApplyMessageClassificationExProperty(MessageClassification parentClassification, MessageClassificationProperty entity)
			{
				entity[PropertyBase.ParentObjectIdProperty] = parentClassification.DataClassificationId;
			}

			// Token: 0x06000F2F RID: 3887 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
			private void ApplyMessageClientInformationExProperties(MessageClientInformation parentClientInformation, MessageClientInformationProperty entity)
			{
				entity[PropertyBase.ParentObjectIdProperty] = parentClientInformation.DataClassificationId;
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x0002EF10 File Offset: 0x0002D110
			private void SerializeObjectToDataTable<T>(T source, HygienePropertyDefinition tableDefinition) where T : MessageTraceEntityBase
			{
				DataTable dataTable = this.bag[tableDefinition] as DataTable;
				HygienePropertyDefinition[] columns = MessageTraceForSaveDataSet.mapTableToTvpColumnInfo[tableDefinition];
				DataRow row = dataTable.NewRow();
				this.PopulateRow(row, columns, source);
				dataTable.Rows.Add(row);
			}

			// Token: 0x06000F31 RID: 3889 RVA: 0x0002EF5C File Offset: 0x0002D15C
			private void PopulateRow(DataRow row, HygienePropertyDefinition[] columns, MessageTraceEntityBase dataSource)
			{
				for (int i = 0; i < columns.Length; i++)
				{
					HygienePropertyDefinition hygienePropertyDefinition = columns[i];
					if (hygienePropertyDefinition != null && !hygienePropertyDefinition.IsCalculated)
					{
						object obj = dataSource[hygienePropertyDefinition];
						if (obj != hygienePropertyDefinition.DefaultValue)
						{
							row[i] = obj;
						}
					}
				}
			}

			// Token: 0x04000700 RID: 1792
			private MessageTraceForSaveDataSet bag;

			// Token: 0x04000701 RID: 1793
			private MessageTrace lastVisitedTrace;

			// Token: 0x04000702 RID: 1794
			private Dictionary<Guid, MessageRecipient> recipientIdMap = new Dictionary<Guid, MessageRecipient>();

			// Token: 0x04000703 RID: 1795
			private Dictionary<Guid, MessageEvent> messageEventIdMap = new Dictionary<Guid, MessageEvent>();

			// Token: 0x04000704 RID: 1796
			private Dictionary<Guid, MessageEventRule> messageEventRuleIdMap = new Dictionary<Guid, MessageEventRule>();

			// Token: 0x04000705 RID: 1797
			private Dictionary<Guid, MessageEventRuleClassification> messageEventRuleClassificationIdMap = new Dictionary<Guid, MessageEventRuleClassification>();

			// Token: 0x04000706 RID: 1798
			private Dictionary<Guid, MessageAction> messageActionIdMap = new Dictionary<Guid, MessageAction>();

			// Token: 0x04000707 RID: 1799
			private Dictionary<Guid, MessageRecipientStatus> messageRecipientStatusIdMap = new Dictionary<Guid, MessageRecipientStatus>();

			// Token: 0x04000708 RID: 1800
			private Dictionary<Guid, MessageEventSourceItem> messageEventSourceItemIdMap = new Dictionary<Guid, MessageEventSourceItem>();

			// Token: 0x04000709 RID: 1801
			private Dictionary<Guid, MessageClientInformation> messageClientInformationIdMap = new Dictionary<Guid, MessageClientInformation>();

			// Token: 0x0400070A RID: 1802
			private Dictionary<Guid, MessageClassification> messageClassificationIdMap = new Dictionary<Guid, MessageClassification>();
		}

		// Token: 0x02000171 RID: 369
		private sealed class MappingInfo
		{
			// Token: 0x17000464 RID: 1124
			// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0002EF9F File Offset: 0x0002D19F
			// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0002EFA7 File Offset: 0x0002D1A7
			public HygienePropertyDefinition ParentTableProperty { get; internal set; }

			// Token: 0x17000465 RID: 1125
			// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0002EFB0 File Offset: 0x0002D1B0
			// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0002EFB8 File Offset: 0x0002D1B8
			public HygienePropertyDefinition ChildTableProperty { get; internal set; }

			// Token: 0x17000466 RID: 1126
			// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0002EFC1 File Offset: 0x0002D1C1
			// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0002EFC9 File Offset: 0x0002D1C9
			public HygienePropertyDefinition ParentKeyProperty { get; internal set; }

			// Token: 0x17000467 RID: 1127
			// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0002EFD2 File Offset: 0x0002D1D2
			// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0002EFDA File Offset: 0x0002D1DA
			public HygienePropertyDefinition ChildKeyProperty { get; internal set; }

			// Token: 0x17000468 RID: 1128
			// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0002EFE3 File Offset: 0x0002D1E3
			// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0002EFEB File Offset: 0x0002D1EB
			public List<Tuple<HygienePropertyDefinition, HygienePropertyDefinition>> PropertyMappings { get; internal set; }
		}

		// Token: 0x02000172 RID: 370
		private abstract class MessageRowComparer : IEqualityComparer<DataRow>
		{
			// Token: 0x06000F3D RID: 3901 RVA: 0x0002EFFC File Offset: 0x0002D1FC
			public virtual bool Equals(DataRow x, DataRow y)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000F3E RID: 3902 RVA: 0x0002F004 File Offset: 0x0002D204
			public virtual int GetHashCode(DataRow row)
			{
				return row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name).GetHashCode();
			}

			// Token: 0x06000F3F RID: 3903 RVA: 0x0002F02F File Offset: 0x0002D22F
			public virtual bool CanCompare(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
			}
		}

		// Token: 0x02000173 RID: 371
		private sealed class MessageEventRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F41 RID: 3905 RVA: 0x0002F060 File Offset: 0x0002D260
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name));
			}

			// Token: 0x06000F42 RID: 3906 RVA: 0x0002F0BC File Offset: 0x0002D2BC
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value);
			}
		}

		// Token: 0x02000174 RID: 372
		private sealed class MessageEventSourceItemRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F44 RID: 3908 RVA: 0x0002F108 File Offset: 0x0002D308
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name)) && string.Compare(x.Field(MessageEventSourceItemSchema.NameProperty.Name), y.Field(MessageEventSourceItemSchema.NameProperty.Name), StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x06000F45 RID: 3909 RVA: 0x0002F190 File Offset: 0x0002D390
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name);
				string text = row.Field(MessageEventSourceItemSchema.NameProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value) ^ (int)DalHelper.FastHash(text.ToLowerInvariant());
			}
		}

		// Token: 0x02000175 RID: 373
		private sealed class MessageRecipientStatusRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F47 RID: 3911 RVA: 0x0002F1FC File Offset: 0x0002D3FC
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EmailHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EmailHashKeyProperty.Name)) && x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name));
			}

			// Token: 0x06000F48 RID: 3912 RVA: 0x0002F280 File Offset: 0x0002D480
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name);
				byte[] value2 = row.Field(CommonMessageTraceSchema.EmailHashKeyProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value) ^ (int)DalHelper.FastHash(value2);
			}
		}

		// Token: 0x02000176 RID: 374
		private sealed class MessageEventRuleRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F4A RID: 3914 RVA: 0x0002F2E8 File Offset: 0x0002D4E8
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name)) && x.Field(CommonMessageTraceSchema.RuleIdProperty.Name) == y.Field(CommonMessageTraceSchema.RuleIdProperty.Name) && string.Equals(x.Field(MessageEventRuleSchema.RuleTypeProperty.Name), y.Field(MessageEventRuleSchema.RuleTypeProperty.Name), StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x06000F4B RID: 3915 RVA: 0x0002F394 File Offset: 0x0002D594
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name);
				Guid guid2 = row.Field(CommonMessageTraceSchema.RuleIdProperty.Name);
				string value2 = row.Field(MessageEventRuleSchema.RuleTypeProperty.Name) ?? string.Empty;
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value) ^ guid2.GetHashCode() ^ (int)DalHelper.FastHash(value2);
			}
		}

		// Token: 0x02000177 RID: 375
		private sealed class MessageActionRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F4D RID: 3917 RVA: 0x0002F424 File Offset: 0x0002D624
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name)) && x.Field(CommonMessageTraceSchema.RuleIdProperty.Name) == y.Field(CommonMessageTraceSchema.RuleIdProperty.Name) && string.Compare(x.Field(MessageActionSchema.NameProperty.Name), y.Field(MessageActionSchema.NameProperty.Name), StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x06000F4E RID: 3918 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name);
				Guid guid2 = row.Field(CommonMessageTraceSchema.RuleIdProperty.Name);
				string text = row.Field(MessageActionSchema.NameProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value) ^ guid2.GetHashCode() ^ (int)DalHelper.FastHash(text.ToLowerInvariant());
			}
		}

		// Token: 0x02000178 RID: 376
		private sealed class MessageEventRuleClassificationRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F50 RID: 3920 RVA: 0x0002F560 File Offset: 0x0002D760
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name)) && x.Field(CommonMessageTraceSchema.RuleIdProperty.Name) == y.Field(CommonMessageTraceSchema.RuleIdProperty.Name) && x.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name) == y.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name);
			}

			// Token: 0x06000F51 RID: 3921 RVA: 0x0002F60C File Offset: 0x0002D80C
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name);
				Guid guid2 = row.Field(CommonMessageTraceSchema.RuleIdProperty.Name);
				Guid guid3 = row.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value) ^ guid2.GetHashCode() ^ guid3.GetHashCode();
			}
		}

		// Token: 0x02000179 RID: 377
		private sealed class MessageClassificationRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F53 RID: 3923 RVA: 0x0002F698 File Offset: 0x0002D898
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name) == y.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name);
			}

			// Token: 0x06000F54 RID: 3924 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				Guid guid2 = row.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name);
				return guid.GetHashCode() ^ guid2.GetHashCode();
			}
		}

		// Token: 0x0200017A RID: 378
		private sealed class MessageClientInformationRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F56 RID: 3926 RVA: 0x0002F748 File Offset: 0x0002D948
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name) == y.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name);
			}

			// Token: 0x06000F57 RID: 3927 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				Guid guid2 = row.Field(CommonMessageTraceSchema.DataClassificationIdProperty.Name);
				return guid.GetHashCode() ^ guid2.GetHashCode();
			}
		}

		// Token: 0x0200017B RID: 379
		private sealed class MessageRecipientRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F59 RID: 3929 RVA: 0x0002F7F8 File Offset: 0x0002D9F8
			public override bool Equals(DataRow x, DataRow y)
			{
				return x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) == y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) && x.Field(CommonMessageTraceSchema.EmailHashKeyProperty.Name).SequenceEqual(y.Field(CommonMessageTraceSchema.EmailHashKeyProperty.Name));
			}

			// Token: 0x06000F5A RID: 3930 RVA: 0x0002F854 File Offset: 0x0002DA54
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EmailHashKeyProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value);
			}
		}

		// Token: 0x0200017C RID: 380
		private sealed class MessagePropertyRowComparer : MessageTraceForSaveDataSet.MessageRowComparer
		{
			// Token: 0x06000F5C RID: 3932 RVA: 0x0002F8A0 File Offset: 0x0002DAA0
			public override bool Equals(DataRow x, DataRow y)
			{
				return !(x.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name) != y.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name)) && MessageTraceForSaveDataSet.MessagePropertyRowComparer.Equals(x.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name), y.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name)) && !(x.Field(PropertyBase.ParentObjectIdProperty.Name) != y.Field(PropertyBase.ParentObjectIdProperty.Name)) && !(x.Field(PropertyBase.RefObjectIdProperty.Name) != y.Field(PropertyBase.RefObjectIdProperty.Name)) && string.Compare(x.Field(PropertyBase.PropertyNameProperty.Name), y.Field(PropertyBase.PropertyNameProperty.Name), StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(x.Field(PropertyBase.RefNameProperty.Name), y.Field(PropertyBase.RefNameProperty.Name), StringComparison.OrdinalIgnoreCase) == 0 && x.Field(PropertyBase.PropertyIndexProperty.Name) == y.Field(PropertyBase.PropertyIndexProperty.Name);
			}

			// Token: 0x06000F5D RID: 3933 RVA: 0x0002FA28 File Offset: 0x0002DC28
			public override int GetHashCode(DataRow row)
			{
				Guid guid = row.Field(CommonMessageTraceSchema.ExMessageIdProperty.Name);
				byte[] value = row.Field(CommonMessageTraceSchema.EventHashKeyProperty.Name) ?? MessageTraceForSaveDataSet.MessagePropertyRowComparer.emptyByteArray;
				Guid guid2 = row.Field(PropertyBase.ParentObjectIdProperty.Name) ?? Guid.Empty;
				Guid guid3 = row.Field(PropertyBase.RefObjectIdProperty.Name) ?? Guid.Empty;
				string text = row.Field(PropertyBase.RefNameProperty.Name) ?? string.Empty;
				string text2 = row.Field(PropertyBase.PropertyNameProperty.Name) ?? string.Empty;
				int num = row.Field(PropertyBase.PropertyIndexProperty.Name);
				return guid.GetHashCode() ^ (int)DalHelper.FastHash(value) ^ guid2.GetHashCode() ^ guid3.GetHashCode() ^ (int)DalHelper.FastHash(text.ToLowerInvariant()) ^ (int)DalHelper.FastHash(text2.ToLowerInvariant()) ^ num;
			}

			// Token: 0x06000F5E RID: 3934 RVA: 0x0002FB4C File Offset: 0x0002DD4C
			private static bool Equals(byte[] leftArray, byte[] rightArray)
			{
				return (leftArray == null && rightArray == null) || (leftArray != null && rightArray != null && leftArray.SequenceEqual(rightArray));
			}

			// Token: 0x04000710 RID: 1808
			private static readonly byte[] emptyByteArray = new byte[0];
		}
	}
}
