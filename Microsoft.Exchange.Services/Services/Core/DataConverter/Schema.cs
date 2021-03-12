using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000191 RID: 401
	internal abstract class Schema
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x000357C0 File Offset: 0x000339C0
		static Schema()
		{
			Schema.itemInformationList = new List<ObjectInformation>
			{
				Schema.CalendarItem,
				Schema.Contact,
				Schema.DistributionList,
				Schema.MeetingCancellation,
				Schema.MeetingMessage,
				Schema.MeetingRequest,
				Schema.MeetingResponse,
				Schema.Message,
				Schema.Item,
				Schema.Task,
				Schema.PostItem
			};
			Schema.folderInformationList = new List<ObjectInformation>
			{
				Schema.CalendarFolder,
				Schema.ContactsFolder,
				Schema.Folder,
				Schema.SearchFolder,
				Schema.OutlookSearchFolder,
				Schema.TasksFolder
			};
			Schema.itemInformationForFolderDictionary = new Dictionary<ObjectInformation, List<ObjectInformation>>
			{
				{
					Schema.CalendarFolder,
					new List<ObjectInformation>
					{
						Schema.CalendarItem,
						Schema.Item
					}
				},
				{
					Schema.ContactsFolder,
					new List<ObjectInformation>
					{
						Schema.Contact,
						Schema.DistributionList,
						Schema.Item
					}
				},
				{
					Schema.Folder,
					new List<ObjectInformation>
					{
						Schema.MeetingCancellation,
						Schema.MeetingMessage,
						Schema.MeetingRequest,
						Schema.MeetingResponse,
						Schema.Message,
						Schema.Item,
						Schema.PostItem
					}
				},
				{
					Schema.SearchFolder,
					Schema.itemInformationList
				},
				{
					Schema.OutlookSearchFolder,
					Schema.itemInformationList
				},
				{
					Schema.TasksFolder,
					new List<ObjectInformation>
					{
						Schema.Item,
						Schema.Task
					}
				}
			};
			Schema.dictionaryObjects = new List<ObjectInformation>();
			Schema.dictionaryObjects.AddRange(Schema.itemInformationList);
			Schema.dictionaryObjects.AddRange(Schema.folderInformationList);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00035FDC File Offset: 0x000341DC
		public Schema(XmlElementInformation[] xmlElements, PropertyInformation itemIdPropertyInformation)
		{
			this.propertyInformationInXmlSchemaSequence = new List<PropertyInformation>();
			this.xmlElementInformationByPath = new Dictionary<string, XmlElementInformation>();
			this.propertyInformationByPath = new Dictionary<PropertyPath, PropertyInformation>();
			this.propertyInformationListByShapeEnum = new Dictionary<ShapeEnum, IList<PropertyInformation>>();
			IList<PropertyInformation> list = new List<PropertyInformation>();
			IList<PropertyInformation> list2 = new List<PropertyInformation>();
			foreach (XmlElementInformation xmlElementInformation in xmlElements)
			{
				PropertyInformation propertyInformation = xmlElementInformation as PropertyInformation;
				if (propertyInformation != null)
				{
					this.propertyInformationInXmlSchemaSequence.Add(propertyInformation);
					if (propertyInformation.PropertyPath != null)
					{
						this.propertyInformationByPath.Add(propertyInformation.PropertyPath, propertyInformation);
					}
					list.Add(propertyInformation);
				}
				if (xmlElementInformation.Path != null)
				{
					this.xmlElementInformationByPath.Add(xmlElementInformation.Path, xmlElementInformation);
				}
			}
			if (itemIdPropertyInformation != null)
			{
				list2.Add(itemIdPropertyInformation);
			}
			this.propertyInformationListByShapeEnum.Add(ShapeEnum.IdOnly, list2);
			this.propertyInformationListByShapeEnum.Add(ShapeEnum.AllProperties, list);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000360B8 File Offset: 0x000342B8
		public Schema(XmlElementInformation[] xmlElements) : this(xmlElements, null)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x000360C2 File Offset: 0x000342C2
		public IList<PropertyInformation> PropertyInformationInXmlSchemaSequence
		{
			get
			{
				return this.propertyInformationInXmlSchemaSequence;
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000360CA File Offset: 0x000342CA
		public bool TryGetXmlElementInformationByPath(string path, out XmlElementInformation xmlElementInformation)
		{
			return this.xmlElementInformationByPath.TryGetValue(path, out xmlElementInformation);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000360D9 File Offset: 0x000342D9
		public bool TryGetPropertyInformationByPath(PropertyPath propertyPath, out PropertyInformation propertyInformation)
		{
			return this.propertyInformationByPath.TryGetValue(propertyPath, out propertyInformation);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000360E8 File Offset: 0x000342E8
		public IList<PropertyInformation> GetPropertyInformationListByShapeEnum(ShapeEnum shapeEnum)
		{
			return this.propertyInformationListByShapeEnum[shapeEnum];
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000360F6 File Offset: 0x000342F6
		internal static ObjectInformation[] GetFolderInformation()
		{
			return Schema.folderInformationList.ToArray();
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00036102 File Offset: 0x00034302
		internal static ObjectInformation[] GetItemInformation()
		{
			return Schema.itemInformationList.ToArray();
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00036110 File Offset: 0x00034310
		internal static ObjectInformation[] GetItemInformationForFolder(ObjectInformation folderInfo)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ObjectInformation.GetItemInformationForFolder: folderInfo.LocalName = {0}", folderInfo.LocalName);
			List<ObjectInformation> list;
			if (!Schema.itemInformationForFolderDictionary.TryGetValue(folderInfo, out list))
			{
				list = Schema.itemInformationList;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<int>(0L, "ObjectInformation.GetItemInformationForFolder: results.Count = {0}", list.Count);
			return list.ToArray();
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00036168 File Offset: 0x00034368
		internal static ObjectInformation GetObjectInformation(StoreObject storeObject)
		{
			ObjectInformation objectInformation = null;
			Type typeFromHandle = typeof(object);
			Type type = storeObject.GetType();
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Type>(0L, "ObjectInformation.GetObjectInformation: storeObject.GetType() = {0}", type);
			while (type != typeFromHandle && !Schema.objectInformationByType.Member.TryGetValue(type, out objectInformation))
			{
				type = type.GetTypeInfo().BaseType;
			}
			if (objectInformation == Schema.Folder)
			{
				if (storeObject.Id != null && IdConverter.GetAsStoreObjectId(storeObject.Id).ObjectType == StoreObjectType.TasksFolder)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ObjectInformation.GetObjectInformation: Folder is TaskFolder. Folder replaced with TasksFolder");
					objectInformation = Schema.TasksFolder;
				}
			}
			else if (objectInformation == Schema.Message)
			{
				MessageItem messageItem = storeObject as MessageItem;
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1) && Shape.IsGenericMessageOnly(messageItem))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ObjectInformation.GetObjectInformation: MessageItem is generic. Message replaced with Item");
					objectInformation = Schema.Item;
				}
			}
			return objectInformation;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00036244 File Offset: 0x00034444
		internal static ObjectInformation GetObjectInformation(StoreObjectType storeObjectType)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<StoreObjectType>(0L, "ObjectInformation.GetObjectInformation: storeObjectType = {0}", storeObjectType);
			ObjectInformation result = null;
			if (!Schema.objectInformationByStoreObjectType.Member.TryGetValue(storeObjectType, out result))
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ObjectInformation.GetObjectInformation: ObjectInformation not found for storeObjectType.  Message used.");
					result = Schema.Message;
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "ObjectInformation.GetObjectInformation: ObjectInformation not found for storeObjectType.  Item used.");
					result = Schema.Item;
				}
			}
			return result;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000362BC File Offset: 0x000344BC
		public static string BuildXmlElementPath(XmlElement xmlElement, string path)
		{
			if (path == Schema.RootXmlElementPath)
			{
				return ServiceXml.GetFullyQualifiedName(xmlElement.LocalName, xmlElement.NamespaceURI);
			}
			StringBuilder stringBuilder = new StringBuilder(path);
			stringBuilder.Append("/");
			stringBuilder.Append(xmlElement.LocalName);
			if (xmlElement.HasAttribute("Key"))
			{
				stringBuilder.Append("[@Key");
				string attribute = xmlElement.GetAttribute("Key");
				if (!string.IsNullOrEmpty(attribute))
				{
					stringBuilder.Append("='");
					stringBuilder.Append(attribute);
					stringBuilder.Append("'");
				}
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040007E1 RID: 2017
		protected const PropertyInformationAttributes AppendUpdateCommand = PropertyInformationAttributes.ImplementsAppendUpdateCommand;

		// Token: 0x040007E2 RID: 2018
		protected const PropertyInformationAttributes DeleteUpdateCommand = PropertyInformationAttributes.ImplementsDeleteUpdateCommand;

		// Token: 0x040007E3 RID: 2019
		protected const PropertyInformationAttributes SetCommand = PropertyInformationAttributes.ImplementsSetCommand;

		// Token: 0x040007E4 RID: 2020
		protected const PropertyInformationAttributes SetUpdateCommand = PropertyInformationAttributes.ImplementsSetUpdateCommand;

		// Token: 0x040007E5 RID: 2021
		protected const PropertyInformationAttributes ToXmlCommand = PropertyInformationAttributes.ImplementsToXmlCommand;

		// Token: 0x040007E6 RID: 2022
		protected const PropertyInformationAttributes ToXmlForPropertyBagCommand = PropertyInformationAttributes.ImplementsToXmlForPropertyBagCommand;

		// Token: 0x040007E7 RID: 2023
		protected const PropertyInformationAttributes ToServiceObjectCommand = PropertyInformationAttributes.ImplementsToServiceObjectCommand;

		// Token: 0x040007E8 RID: 2024
		protected const PropertyInformationAttributes ToServiceObjectForPropertyBagCommand = PropertyInformationAttributes.ImplementsToServiceObjectForPropertyBagCommand;

		// Token: 0x040007E9 RID: 2025
		public static string RootXmlElementPath = string.Empty;

		// Token: 0x040007EA RID: 2026
		private static List<ObjectInformation> itemInformationList;

		// Token: 0x040007EB RID: 2027
		private static List<ObjectInformation> folderInformationList;

		// Token: 0x040007EC RID: 2028
		private static Dictionary<ObjectInformation, List<ObjectInformation>> itemInformationForFolderDictionary;

		// Token: 0x040007ED RID: 2029
		private static List<ObjectInformation> dictionaryObjects;

		// Token: 0x040007EE RID: 2030
		private static LazyMember<Dictionary<Type, ObjectInformation>> objectInformationByType = new LazyMember<Dictionary<Type, ObjectInformation>>(delegate()
		{
			Dictionary<Type, ObjectInformation> dictionary = new Dictionary<Type, ObjectInformation>();
			foreach (ObjectInformation objectInformation in Schema.dictionaryObjects)
			{
				if (objectInformation.AssociatedType != null)
				{
					dictionary.Add(objectInformation.AssociatedType, objectInformation);
				}
			}
			return dictionary;
		});

		// Token: 0x040007EF RID: 2031
		private static LazyMember<Dictionary<StoreObjectType, ObjectInformation>> objectInformationByStoreObjectType = new LazyMember<Dictionary<StoreObjectType, ObjectInformation>>(delegate()
		{
			Dictionary<StoreObjectType, ObjectInformation> dictionary = new Dictionary<StoreObjectType, ObjectInformation>();
			foreach (ObjectInformation objectInformation in Schema.dictionaryObjects)
			{
				if (objectInformation.AssociatedStoreObjectTypes != null)
				{
					foreach (StoreObjectType key in objectInformation.AssociatedStoreObjectTypes)
					{
						dictionary.Add(key, objectInformation);
					}
				}
			}
			return dictionary;
		});

		// Token: 0x040007F0 RID: 2032
		private IList<PropertyInformation> propertyInformationInXmlSchemaSequence;

		// Token: 0x040007F1 RID: 2033
		private IDictionary<string, XmlElementInformation> xmlElementInformationByPath;

		// Token: 0x040007F2 RID: 2034
		private IDictionary<PropertyPath, PropertyInformation> propertyInformationByPath;

		// Token: 0x040007F3 RID: 2035
		private IDictionary<ShapeEnum, IList<PropertyInformation>> propertyInformationListByShapeEnum;

		// Token: 0x040007F4 RID: 2036
		internal static readonly ObjectInformation Item = new ObjectInformation("Item", ExchangeVersion.Exchange2007, typeof(Item), null, new Shape.CreateShapeCallback(ItemShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007F5 RID: 2037
		internal static readonly ObjectInformation Conversation = new ObjectInformation("Conversation", ExchangeVersion.Exchange2010SP1, null, null, new Shape.CreateShapeCallback(ConversationShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007F6 RID: 2038
		internal static readonly ObjectInformation Contact = new ObjectInformation("Contact", ExchangeVersion.Exchange2007, typeof(Contact), new StoreObjectType[]
		{
			StoreObjectType.Contact
		}, new Shape.CreateShapeCallback(ContactShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007F7 RID: 2039
		internal static readonly ObjectInformation DistributionList = new ObjectInformation("DistributionList", ExchangeVersion.Exchange2007, typeof(DistributionList), new StoreObjectType[]
		{
			StoreObjectType.DistributionList
		}, new Shape.CreateShapeCallback(DistributionListShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007F8 RID: 2040
		internal static readonly ObjectInformation CalendarItem = new ObjectInformation("CalendarItem", ExchangeVersion.Exchange2007, typeof(CalendarItemBase), new StoreObjectType[]
		{
			StoreObjectType.CalendarItem,
			StoreObjectType.CalendarItemOccurrence
		}, new Shape.CreateShapeCallback(CalendarItemShape.CreateShapeForAttendee), null, new Shape.CreateShapeForStoreObjectCallback(CalendarItemShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007F9 RID: 2041
		internal static readonly ObjectInformation CalendarItemOccurrenceException = new ObjectInformation("CalendarItemOccurrenceException", ExchangeVersion.Exchange2007, null, null, new Shape.CreateShapeCallback(CalendarItemShape.CreateShapeForAttendee), null, new Shape.CreateShapeForStoreObjectCallback(CalendarItemShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007FA RID: 2042
		internal static readonly ObjectInformation MeetingCancellation = new ObjectInformation("MeetingCancellation", ExchangeVersion.Exchange2007, typeof(MeetingCancellation), new StoreObjectType[]
		{
			StoreObjectType.MeetingCancellation
		}, new Shape.CreateShapeCallback(MeetingCancellationShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007FB RID: 2043
		internal static readonly ObjectInformation MeetingMessage = new ObjectInformation("MeetingMessage", ExchangeVersion.Exchange2007, typeof(MeetingMessage), new StoreObjectType[]
		{
			StoreObjectType.MeetingMessage
		}, new Shape.CreateShapeCallback(MeetingMessageShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007FC RID: 2044
		internal static readonly ObjectInformation MeetingRequest = new ObjectInformation("MeetingRequest", ExchangeVersion.Exchange2007, typeof(MeetingRequest), new StoreObjectType[]
		{
			StoreObjectType.MeetingRequest
		}, new Shape.CreateShapeCallback(MeetingRequestShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007FD RID: 2045
		internal static readonly ObjectInformation MeetingResponse = new ObjectInformation("MeetingResponse", ExchangeVersion.Exchange2007, typeof(MeetingResponse), new StoreObjectType[]
		{
			StoreObjectType.MeetingResponse
		}, new Shape.CreateShapeCallback(MeetingResponseShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007FE RID: 2046
		internal static readonly ObjectInformation Message = new ObjectInformation("Message", ExchangeVersion.Exchange2007, typeof(MessageItem), new StoreObjectType[]
		{
			StoreObjectType.Message,
			StoreObjectType.Report,
			StoreObjectType.MeetingForwardNotification
		}, new Shape.CreateShapeCallback(MessageShape.CreateShape), new Shape.CreateShapeForPropertyBagCallback(MessageShape.CreateShapeForPropertyBag), null, ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x040007FF RID: 2047
		internal static readonly ObjectInformation Task = new ObjectInformation("Task", ExchangeVersion.Exchange2007, typeof(Task), new StoreObjectType[]
		{
			StoreObjectType.Task
		}, new Shape.CreateShapeCallback(TaskShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000800 RID: 2048
		internal static readonly ObjectInformation PostItem = new ObjectInformation("PostItem", ExchangeVersion.Exchange2007SP1, typeof(PostItem), new StoreObjectType[]
		{
			StoreObjectType.Post
		}, new Shape.CreateShapeCallback(PostItemShape.CreateShape), Schema.Item);

		// Token: 0x04000801 RID: 2049
		internal static readonly ObjectInformation DeliveryReport = new ObjectInformation("Message", ExchangeVersion.Exchange2007, null, null, new Shape.CreateShapeCallback(MessageShape.CreateShape), new Shape.CreateShapeForPropertyBagCallback(MessageShape.CreateShapeForPropertyBag), null, ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000802 RID: 2050
		internal static readonly ObjectInformation NonDeliveryReport = new ObjectInformation("Message", ExchangeVersion.Exchange2007, null, null, new Shape.CreateShapeCallback(MessageShape.CreateShape), new Shape.CreateShapeForPropertyBagCallback(MessageShape.CreateShapeForPropertyBag), null, ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000803 RID: 2051
		internal static readonly ObjectInformation Persona = new ObjectInformation("Persona", ExchangeVersion.Exchange2012, null, null, new Shape.CreateShapeCallback(PersonaShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000804 RID: 2052
		internal static readonly ObjectInformation ReadReceipt = new ObjectInformation("Message", ExchangeVersion.Exchange2007, null, null, new Shape.CreateShapeCallback(MessageShape.CreateShape), new Shape.CreateShapeForPropertyBagCallback(MessageShape.CreateShapeForPropertyBag), null, ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000805 RID: 2053
		internal static readonly ObjectInformation NonReadReceipt = new ObjectInformation("Message", ExchangeVersion.Exchange2007, null, null, new Shape.CreateShapeCallback(MessageShape.CreateShape), new Shape.CreateShapeForPropertyBagCallback(MessageShape.CreateShapeForPropertyBag), null, ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000806 RID: 2054
		internal static readonly ObjectInformation Folder = new ObjectInformation("Folder", ExchangeVersion.Exchange2007, typeof(Folder), new StoreObjectType[]
		{
			StoreObjectType.Folder,
			StoreObjectType.JournalFolder,
			StoreObjectType.NotesFolder
		}, new Shape.CreateShapeCallback(FolderShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000807 RID: 2055
		internal static readonly ObjectInformation ContactsFolder = new ObjectInformation("ContactsFolder", ExchangeVersion.Exchange2007, typeof(ContactsFolder), new StoreObjectType[]
		{
			StoreObjectType.ContactsFolder
		}, new Shape.CreateShapeCallback(ContactsFolderShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000808 RID: 2056
		internal static readonly ObjectInformation CalendarFolder = new ObjectInformation("CalendarFolder", ExchangeVersion.Exchange2007, typeof(CalendarFolder), new StoreObjectType[]
		{
			StoreObjectType.CalendarFolder
		}, new Shape.CreateShapeCallback(CalendarFolderShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x04000809 RID: 2057
		internal static readonly ObjectInformation SearchFolder = new ObjectInformation("SearchFolder", ExchangeVersion.Exchange2007, typeof(SearchFolder), new StoreObjectType[]
		{
			StoreObjectType.SearchFolder
		}, new Shape.CreateShapeCallback(SearchFolderShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);

		// Token: 0x0400080A RID: 2058
		internal static readonly ObjectInformation OutlookSearchFolder = new ObjectInformation("OutlookSearchFolder", ExchangeVersion.Exchange2010, typeof(OutlookSearchFolder), new StoreObjectType[]
		{
			StoreObjectType.OutlookSearchFolder
		}, new Shape.CreateShapeCallback(SearchFolderShape.CreateShape), Schema.SearchFolder);

		// Token: 0x0400080B RID: 2059
		internal static readonly ObjectInformation TasksFolder = new ObjectInformation("TasksFolder", ExchangeVersion.Exchange2007, null, new StoreObjectType[]
		{
			StoreObjectType.TasksFolder
		}, new Shape.CreateShapeCallback(TasksFolderShape.CreateShape), ObjectInformation.NoPriorVersionObjectInformation);
	}
}
