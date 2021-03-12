using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200004A RID: 74
	internal class PropertyConverter
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x000182F8 File Offset: 0x000164F8
		internal PropertyConverter(IEnumerable<PropertyConverter.PropertyDefinitionMapping> propertyDefinitionMapping, params PropertyConversion[] conversions)
		{
			this.propertyConversionToClientDictionary = PropertyConverter.CreatePropertyConversionToClientDictionary(conversions);
			this.propertyConversions = conversions;
			if (propertyDefinitionMapping != null)
			{
				foreach (PropertyConverter.PropertyDefinitionMapping propertyDefinitionMapping2 in propertyDefinitionMapping)
				{
					if (this.propertyTagToPropertyDefinitionMapping.ContainsKey(propertyDefinitionMapping2.PropertyTag))
					{
						throw new ArgumentException(string.Format("Mapping for property tag [{0}] already exists", propertyDefinitionMapping2.PropertyTag), "propertyDefinitionMapping");
					}
					if (this.propertyDefinitionToPropertyTagMapping.ContainsKey(propertyDefinitionMapping2.PropertyDefinition))
					{
						throw new ArgumentException(string.Format("Mapping for property definition [{0}] already exists", propertyDefinitionMapping2.PropertyDefinition), "propertyDefinitionMapping");
					}
					this.propertyTagToPropertyDefinitionMapping.Add(propertyDefinitionMapping2.PropertyTag, propertyDefinitionMapping2.PropertyDefinition);
					this.propertyDefinitionToPropertyTagMapping.Add(propertyDefinitionMapping2.PropertyDefinition, propertyDefinitionMapping2.PropertyTag);
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00018404 File Offset: 0x00016604
		internal static PropertyConverter Message
		{
			get
			{
				return PropertyConverter.genericConverter;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001840B File Offset: 0x0001660B
		internal static PropertyConverter Folder
		{
			get
			{
				return PropertyConverter.folderConverter;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00018412 File Offset: 0x00016612
		internal static PropertyConverter HierarchyView
		{
			get
			{
				return PropertyConverter.hierarchyViewConverter;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00018419 File Offset: 0x00016619
		internal static PropertyConverter Attachment
		{
			get
			{
				return PropertyConverter.genericConverter;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00018420 File Offset: 0x00016620
		internal static PropertyConverter Recipient
		{
			get
			{
				return PropertyConverter.genericConverter;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00018427 File Offset: 0x00016627
		internal static PropertyConverter Rule
		{
			get
			{
				return PropertyConverter.genericConverter;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0001842E File Offset: 0x0001662E
		internal static PropertyConverter Logon
		{
			get
			{
				return PropertyConverter.logonConverter;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00018435 File Offset: 0x00016635
		internal static PropertyConverter Permission
		{
			get
			{
				return PropertyConverter.genericConverter;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00018444 File Offset: 0x00016644
		internal IEnumerable<PropertyTag> ClientPropertyTagsThatRequireConversion
		{
			get
			{
				return from conversion in this.propertyConversions
				select conversion.ClientPropertyTag;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00018476 File Offset: 0x00016676
		internal IEnumerable<PropertyTag> ServerPropertyTags
		{
			get
			{
				return from conversion in this.propertyConversions
				select conversion.ServerPropertyTag;
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000184A0 File Offset: 0x000166A0
		internal static void ConvertFromExportToOurServerId(StoreSession session, ref PropertyValue propertyValue)
		{
			if (propertyValue.PropertyTag.PropertyType == PropertyType.ServerId)
			{
				byte[] valueAssert = propertyValue.GetValueAssert<byte[]>();
				ServerIdType serverIdType = ServerIdConverter.GetServerIdType(new ArraySegment<byte>(valueAssert));
				if (serverIdType == ServerIdType.Export)
				{
					propertyValue = new PropertyValue(propertyValue.PropertyTag, ServerIdConverter.MakeOurServerIdFromExportServerId(session, valueAssert));
				}
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000184F4 File Offset: 0x000166F4
		internal static void ConvertFromOurToExportServerId(StoreSession session, ref PropertyValue propertyValue)
		{
			if (propertyValue.PropertyTag.PropertyType == PropertyType.ServerId)
			{
				byte[] valueAssert = propertyValue.GetValueAssert<byte[]>();
				ServerIdType serverIdType = ServerIdConverter.GetServerIdType(new ArraySegment<byte>(valueAssert));
				if (serverIdType == ServerIdType.Ours)
				{
					propertyValue = new PropertyValue(propertyValue.PropertyTag, ServerIdConverter.MakeExportServerIdFromOurServerId(session, valueAssert));
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00018548 File Offset: 0x00016748
		internal PropertyTag ConvertPropertyTagFromClient(PropertyTag propertyTag)
		{
			for (int i = 0; i < this.propertyConversions.Length; i++)
			{
				PropertyConversion propertyConversion = this.propertyConversions[i];
				PropertyTag result;
				if (propertyConversion.TryConvertPropertyTagFromClient(propertyTag, out result))
				{
					return result;
				}
			}
			return propertyTag;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00018580 File Offset: 0x00016780
		internal PropertyTag[] ConvertPropertyTagsFromClient(PropertyTag[] propertyTags)
		{
			if (propertyTags == null)
			{
				return null;
			}
			PropertyTag[] array = null;
			for (int i = 0; i < propertyTags.Length; i++)
			{
				PropertyTag propertyTag = this.ConvertPropertyTagFromClient(propertyTags[i]);
				if (propertyTag != propertyTags[i])
				{
					if (array == null)
					{
						array = new PropertyTag[propertyTags.Length];
						Array.Copy(propertyTags, array, propertyTags.Length);
					}
					array[i] = propertyTag;
				}
			}
			return array ?? propertyTags;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000185F4 File Offset: 0x000167F4
		internal void ConvertPropertyValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, ref PropertyValue propertyValue)
		{
			for (int i = 0; i < this.propertyConversions.Length; i++)
			{
				PropertyConversion propertyConversion = this.propertyConversions[i];
				if (propertyConversion.TryConvertPropertyValueFromClient(session, storageObjectProperties, ref propertyValue))
				{
					return;
				}
			}
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001862C File Offset: 0x0001682C
		internal void ConvertPropertyValuesFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, PropertyValue[] propertyValues)
		{
			if (propertyValues == null)
			{
				return;
			}
			for (int i = 0; i < propertyValues.Length; i++)
			{
				this.ConvertPropertyValueFromClient(session, storageObjectProperties, ref propertyValues[i]);
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001865C File Offset: 0x0001685C
		internal PropertyTag ConvertPropertyTagToClient(PropertyTag propertyTag, PropertyTag? originalTag)
		{
			PropertyConversion propertyConversion;
			PropertyTag result;
			if (this.propertyConversionToClientDictionary.TryGetValue(propertyTag.PropertyId, out propertyConversion) && propertyConversion.TryConvertPropertyTagToClient(propertyTag, originalTag, out result))
			{
				return result;
			}
			return propertyTag;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00018690 File Offset: 0x00016890
		internal PropertyTag ConvertPropertyTagToClient(PropertyTag propertyTag)
		{
			return this.ConvertPropertyTagToClient(propertyTag, null);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000186B0 File Offset: 0x000168B0
		internal void ConvertPropertyValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, ref PropertyValue propertyValue, PropertyTag? originalTag)
		{
			PropertyConversion propertyConversion;
			if (this.propertyConversionToClientDictionary.TryGetValue(propertyValue.PropertyTag.PropertyId, out propertyConversion))
			{
				propertyConversion.TryConvertPropertyValueToClient(session, storageObjectProperties, originalTag, ref propertyValue);
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000186E8 File Offset: 0x000168E8
		internal void ConvertPropertyValuesToClientAndSuppressClientSide(StoreSession session, IStorageObjectProperties storageObjectProperties, PropertyValue[] propertyValues, PropertyTag[] originalTags, ClientSideProperties clientSideProperties)
		{
			if (propertyValues == null)
			{
				return;
			}
			if (originalTags != null && propertyValues.Length != originalTags.Length)
			{
				throw new ArgumentException("PropertyValue[] length isn't the same as the PropertyTag[] length.");
			}
			for (int i = 0; i < propertyValues.Length; i++)
			{
				this.ConvertPropertyValueToClient(session, storageObjectProperties, ref propertyValues[i], (originalTags != null) ? new PropertyTag?(originalTags[i]) : null);
				if (!clientSideProperties.ShouldBeReturnedIfRequested(propertyValues[i].PropertyTag.PropertyId))
				{
					propertyValues[i] = new PropertyValue(new PropertyTag(propertyValues[i].PropertyTag.PropertyId, PropertyType.Error), (ErrorCode)2147746063U);
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000187A8 File Offset: 0x000169A8
		internal PropertyTag[] GetValidClientSideProperties(PropertyServerObject propertyServerObject, ICollection<PropertyDefinition> propertyDefinitions, bool useUnicodeType, PropertyTag[] additionalTags)
		{
			ICollection<PropertyDefinition> smartProperties = propertyServerObject.Schema.SmartProperties;
			List<PropertyTag> list = new List<PropertyTag>(propertyDefinitions.Count + ((additionalTags != null) ? additionalTags.Length : 0));
			list.AddRange(this.GetMappedPropertyTags(smartProperties));
			ICollection<PropertyTag> collection = MEDSPropertyTranslator.PropertyTagsFromPropertyDefinitions<PropertyDefinition>(propertyServerObject.Session, propertyDefinitions, useUnicodeType);
			foreach (PropertyTag propertyTag in collection)
			{
				if (!propertyServerObject.ClientSideProperties.ExcludeFromGetPropertyList(propertyTag.PropertyId))
				{
					list.Add(this.ConvertPropertyTagToClient(propertyTag));
				}
			}
			if (additionalTags != null)
			{
				foreach (PropertyTag propertyTag2 in additionalTags)
				{
					if (!propertyServerObject.ClientSideProperties.ExcludeFromGetPropertyList(propertyTag2.PropertyId))
					{
						PropertyTag propertyTag3 = this.ConvertPropertyTagToClient(propertyTag2);
						if (!list.Contains(propertyTag3, PropertyTag.PropertyIdComparer))
						{
							list.Add(propertyTag3);
						}
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000188B4 File Offset: 0x00016AB4
		internal bool TryGetMappedPropertyTag(PropertyDefinition propertyDefinition, out PropertyTag propertyTag)
		{
			return this.propertyDefinitionToPropertyTagMapping.TryGetValue(propertyDefinition, out propertyTag);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000188C3 File Offset: 0x00016AC3
		internal bool TryGetMappedPropertyDefinition(PropertyTag propertyTag, out PropertyDefinition propertyDefinition)
		{
			return this.propertyTagToPropertyDefinitionMapping.TryGetValue(propertyTag, out propertyDefinition);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000188EE File Offset: 0x00016AEE
		internal IEnumerable<PropertyTag> GetMappedPropertyTags(IEnumerable<PropertyDefinition> propertyDefinitions)
		{
			return from propertyDefinition in propertyDefinitions
			where this.propertyDefinitionToPropertyTagMapping.ContainsKey(propertyDefinition)
			select this.propertyDefinitionToPropertyTagMapping[propertyDefinition];
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001892F File Offset: 0x00016B2F
		internal IEnumerable<PropertyDefinition> GetMappedPropertyDefinitions(IEnumerable<PropertyTag> propertyTags)
		{
			return from propTag in propertyTags
			where this.propertyTagToPropertyDefinitionMapping.ContainsKey(propTag)
			select this.propertyTagToPropertyDefinitionMapping[propTag];
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00018954 File Offset: 0x00016B54
		private static Dictionary<PropertyId, PropertyConversion> CreatePropertyConversionToClientDictionary(PropertyConversion[] conversions)
		{
			Dictionary<PropertyId, PropertyConversion> dictionary = new Dictionary<PropertyId, PropertyConversion>(conversions.Length, PropertyIdComparer.Instance);
			foreach (PropertyConversion propertyConversion in conversions)
			{
				dictionary.Add(propertyConversion.ServerPropertyTag.PropertyId, propertyConversion);
			}
			return dictionary;
		}

		// Token: 0x04000106 RID: 262
		private static PropertyConverter genericConverter = new PropertyConverter(Array<PropertyConverter.PropertyDefinitionMapping>.Empty, new PropertyConversion[]
		{
			new SentMailConversion(),
			new DamOrgMsgConversion(),
			new ParentIdConversion(),
			new ConflictMsgKeyConversion(),
			new RuleFolderIdConversion(),
			new MessageSubmissionIdConversion(),
			new ConversationMvItemIdsConversion(),
			new ConversationMvItemIdsMailboxWideConversion(),
			new LocalDirectoryEntryIdConversion()
		});

		// Token: 0x04000107 RID: 263
		private static PropertyConverter folderConverter = new PropertyConverter(Array<PropertyConverter.PropertyDefinitionMapping>.Empty, new PropertyConversion[]
		{
			new SentMailConversion(),
			new DamOrgMsgConversion(),
			new ParentIdConversion(),
			new ConflictMsgKeyConversion(),
			new RuleFolderIdConversion(),
			new MessageSubmissionIdConversion(),
			new ConversationMvItemIdsConversion(),
			new ConversationMvItemIdsMailboxWideConversion(),
			new LocalDirectoryEntryIdConversion(),
			new FolderSecurityDescriptorConversion()
		});

		// Token: 0x04000108 RID: 264
		private static PropertyConverter hierarchyViewConverter = new PropertyConverter(Array<PropertyConverter.PropertyDefinitionMapping>.Empty, new PropertyConversion[]
		{
			new SentMailConversion(),
			new DamOrgMsgConversion(),
			new ParentIdConversion(),
			new ConflictMsgKeyConversion(),
			new RuleFolderIdConversion(),
			new MessageSubmissionIdConversion(),
			new ConversationMvItemIdsConversion(),
			new ConversationMvItemIdsMailboxWideConversion(),
			new LocalDirectoryEntryIdConversion()
		});

		// Token: 0x04000109 RID: 265
		private static PropertyConverter logonConverter = new PropertyConverter(new PropertyConverter.PropertyDefinitionMapping[]
		{
			PropertyConverter.PropertyDefinitionMapping.InferenceOLKUserActivityLoggingEnabled
		}, new PropertyConversion[]
		{
			new SentMailConversion(),
			new DamOrgMsgConversion(),
			new ParentIdConversion(),
			new ConflictMsgKeyConversion(),
			new RuleFolderIdConversion(),
			new MessageSubmissionIdConversion(),
			new ConversationMvItemIdsConversion(),
			new ConversationMvItemIdsMailboxWideConversion(),
			new LocalDirectoryEntryIdConversion()
		});

		// Token: 0x0400010A RID: 266
		private readonly Dictionary<PropertyId, PropertyConversion> propertyConversionToClientDictionary;

		// Token: 0x0400010B RID: 267
		private readonly PropertyConversion[] propertyConversions;

		// Token: 0x0400010C RID: 268
		private readonly Dictionary<PropertyDefinition, PropertyTag> propertyDefinitionToPropertyTagMapping = new Dictionary<PropertyDefinition, PropertyTag>();

		// Token: 0x0400010D RID: 269
		private readonly Dictionary<PropertyTag, PropertyDefinition> propertyTagToPropertyDefinitionMapping = new Dictionary<PropertyTag, PropertyDefinition>();

		// Token: 0x0200004B RID: 75
		internal class PropertyDefinitionMapping
		{
			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060002EB RID: 747 RVA: 0x00018B48 File Offset: 0x00016D48
			// (set) Token: 0x060002EC RID: 748 RVA: 0x00018B50 File Offset: 0x00016D50
			internal PropertyTag PropertyTag { get; private set; }

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060002ED RID: 749 RVA: 0x00018B59 File Offset: 0x00016D59
			// (set) Token: 0x060002EE RID: 750 RVA: 0x00018B61 File Offset: 0x00016D61
			internal PropertyDefinition PropertyDefinition { get; private set; }

			// Token: 0x060002EF RID: 751 RVA: 0x00018B6C File Offset: 0x00016D6C
			public PropertyDefinitionMapping(PropertyTag propertyTag, PropertyDefinition propertyDefinition)
			{
				Type type = NativeStorePropertyDefinition.ClrTypeFromPropertyTag(propertyTag);
				if (type != propertyDefinition.Type)
				{
					throw new ArgumentException(string.Format("Property type [propertyTag ({0})] must match with [propertyDefinition ({1})]", type, propertyDefinition.Type));
				}
				this.PropertyTag = propertyTag;
				this.PropertyDefinition = propertyDefinition;
			}

			// Token: 0x04000110 RID: 272
			internal static readonly PropertyConverter.PropertyDefinitionMapping InferenceOLKUserActivityLoggingEnabled = new PropertyConverter.PropertyDefinitionMapping(new PropertyTag(1748369419U), MailboxSchema.InferenceOLKUserActivityLoggingEnabled);
		}
	}
}
