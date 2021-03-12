using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000021 RID: 33
	public class ConversationMembers
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x00031028 File Offset: 0x0002F228
		public ConversationMembers(Mailbox mailbox, IEnumerable<FidMid> conversationMessages, IEnumerable<FidMid> originalConversationMessages, TopMessage modifiedMessage, HashSet<StorePropTag> aggregatePropertiesToCompute)
		{
			this.mailbox = mailbox;
			this.conversationMessages = new List<FidMid>(conversationMessages);
			this.originalConversationMessages = new List<FidMid>(originalConversationMessages);
			this.modifiedMessage = modifiedMessage;
			this.aggregatePropertiesToCompute = aggregatePropertiesToCompute;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00031060 File Offset: 0x0002F260
		public IEnumerable<ExchangeId> FolderIds
		{
			get
			{
				ISet<ExchangeId> set = new HashSet<ExchangeId>();
				foreach (FidMid fidMid in this.conversationMessages)
				{
					set.Add(fidMid.FolderId);
				}
				return set;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000310C4 File Offset: 0x0002F2C4
		public IEnumerable<ExchangeId> OriginalFolderIds
		{
			get
			{
				ISet<ExchangeId> set = new HashSet<ExchangeId>();
				foreach (FidMid fidMid in this.originalConversationMessages)
				{
					set.Add(fidMid.FolderId);
				}
				return set;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00031128 File Offset: 0x0002F328
		public IEnumerable<FidMid> ConversationMessages
		{
			get
			{
				return this.conversationMessages;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00031130 File Offset: 0x0002F330
		public IEnumerable<FidMid> OriginalConversationMessages
		{
			get
			{
				return this.originalConversationMessages;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00031138 File Offset: 0x0002F338
		private static void AddPropertyIfNotPresent(Mailbox mailbox, StorePropTag propTag, List<StorePropTag> propertiesToPromote)
		{
			if (!propertiesToPromote.Contains(propTag))
			{
				propertiesToPromote.Add(propTag);
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0003114A File Offset: 0x0002F34A
		private static bool ShouldSkipNullAndEmptyValue(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType aggregationType)
		{
			return aggregationType != ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder && aggregationType != ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MinInRelevanceScore && aggregationType != ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.BooleanAnd && aggregationType != ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.UnionNotUnique_NullIfAllNotSet;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00031162 File Offset: 0x0002F362
		public object GetAggregateProperty(Context context, StorePropTag proptag, ExchangeId folderId, bool original)
		{
			return this.GetAggregateProperty(context, proptag, null, folderId, original);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00031170 File Offset: 0x0002F370
		public object GetAggregateProperty(Context context, StorePropTag proptag, ICollection<FidMid> filterList, bool original)
		{
			return this.GetAggregateProperty(context, proptag, filterList, ExchangeId.Null, original);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00031184 File Offset: 0x0002F384
		public static bool IsAggregateProperty(StorePropTag propTag)
		{
			ConversationMembers.ConversationPropertyAggregationInfo conversationPropertyAggregationInfo;
			return ConversationMembers.conversationPropertyMapping.TryGetValue(propTag, out conversationPropertyAggregationInfo);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000311A0 File Offset: 0x0002F3A0
		public static StorePropTag? GetAggregatedOnPropTag(Context context, Mailbox mailbox, StorePropTag proptag)
		{
			ConversationMembers.ConversationPropertyAggregationInfo? aggregationInfo = ConversationMembers.GetAggregationInfo(context, mailbox, proptag);
			if (aggregationInfo != null)
			{
				return new StorePropTag?(aggregationInfo.Value.AggregatedProperty);
			}
			return null;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000311E0 File Offset: 0x0002F3E0
		public object GetAggregateProperty(Context context, StorePropTag proptag, ICollection<FidMid> filterList, ExchangeId folderId, bool original)
		{
			ConversationMembers.ConversationPropertyAggregationInfo? aggregationInfo = ConversationMembers.GetAggregationInfo(context, this.mailbox, proptag);
			if (aggregationInfo == null)
			{
				return null;
			}
			this.CacheMessageProperties(context, original);
			if (ConversationMembers.PropTagIsConversationFlagCompleteTime(proptag) && !this.ConversationIsFlagged(context, PropTag.Message.ConversationFlagCompleteTime == proptag, filterList, folderId, original))
			{
				return null;
			}
			ConversationMembers.ConversationPropertyAggregationInfo value = aggregationInfo.Value;
			InitialMessageFinder initialMessageFinder = null;
			object obj = null;
			string origin = string.Empty;
			int maxValue = int.MaxValue;
			string text = null;
			DateTime creationTime = DateTime.MinValue;
			bool? flag = null;
			List<FidMid> list = original ? this.originalConversationMessages : this.conversationMessages;
			foreach (FidMid fidMid in list)
			{
				if (!value.Filtered || ((filterList != null) ? filterList.Contains(fidMid) : (fidMid.FolderId == folderId)))
				{
					if (value.UnreadOnly)
					{
						object messagePropertyValue = this.GetMessagePropertyValue(fidMid, PropTag.Message.Read, original);
						if (messagePropertyValue != null && (bool)messagePropertyValue)
						{
							continue;
						}
					}
					object obj2 = this.GetMessagePropertyValue(fidMid, value.AggregatedProperty, original);
					string text2 = obj2 as string;
					if (!ConversationMembers.ShouldSkipNullAndEmptyValue(value.Type) || (obj2 != null && (text2 == null || !(text2 == string.Empty))))
					{
						if (text2 != null && text2.Length > 255 && (value.Type != ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union || value.AggregatedProperty != PropTag.Message.DisplayTo))
						{
							obj2 = text2.Substring(0, 255);
						}
						if (!(PropTag.Message.IconIndex == value.AggregatedProperty) || ConversationMembers.validIconIndexes.Contains((int)obj2))
						{
							if (PropTag.Message.HasAttach == value.AggregatedProperty && (bool)obj2)
							{
								object messagePropertyValue2 = this.GetMessagePropertyValue(fidMid, ConversationMembers.GetAllAttachmentsAreHiddenPropTag(context, this.mailbox), original);
								if (messagePropertyValue2 != null && (bool)messagePropertyValue2)
								{
									obj2 = false;
								}
							}
							bool flag2 = false;
							switch (value.Type)
							{
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage:
								obj = obj2;
								flag2 = true;
								break;
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MaxInOriginPreferenceOrder:
							{
								string text3 = (string)this.GetMessagePropertyValue(fidMid, PropTag.Message.PartnerNetworkId, original);
								DateTime valueOrDefault = ((DateTime?)this.GetMessagePropertyValue(fidMid, PropTag.Message.CreationTime, original)).GetValueOrDefault(DateTime.MinValue);
								if (OriginPreferenceComparer.Instance.Compare(context, this.mailbox, proptag, obj, origin, creationTime, obj2, text3, valueOrDefault) < 0)
								{
									obj = obj2;
									origin = text3;
									creationTime = valueOrDefault;
								}
								break;
							}
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max:
								if (ValueHelper.ValuesCompare(obj, obj2, (context.Culture == null) ? null : context.Culture.CompareInfo, CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth) < 0)
								{
									obj = obj2;
								}
								break;
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum:
								obj = (int)((obj == null) ? 0 : obj) + (int)obj2;
								break;
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union:
							{
								if (obj == null)
								{
									obj = new Dictionary<object, DateTime>(new ValueEqualityComparer((context.Culture == null) ? null : context.Culture.CompareInfo));
								}
								IDictionary<object, DateTime> multivalues = (IDictionary<object, DateTime>)obj;
								DateTime valueOrDefault2 = ((DateTime?)this.GetMessagePropertyValue(fidMid, PropTag.Message.MessageDeliveryTime, original)).GetValueOrDefault(DateTime.MinValue);
								if (PropTag.Message.DisplayTo == value.AggregatedProperty)
								{
									foreach (string text4 in ((string)obj2).Split(ConversationMembers.DisplayToSeparators, StringSplitOptions.RemoveEmptyEntries))
									{
										string value2 = text4;
										if (text4.Length > 255)
										{
											value2 = text4.Substring(0, 255);
										}
										ConversationMembers.AddToMultiValueCollection(multivalues, value2, valueOrDefault2);
									}
								}
								else if (value.AggregatedProperty.IsMultiValued)
								{
									Array array2 = (Array)obj2;
									for (int j = 0; j < array2.Length; j++)
									{
										ConversationMembers.AddToMultiValueCollection(multivalues, array2.GetValue(j), valueOrDefault2);
									}
								}
								else
								{
									ConversationMembers.AddToMultiValueCollection(multivalues, obj2, valueOrDefault2);
								}
								break;
							}
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder:
								this.UpdateAggregatedValueBasedOnPriorityAndOriginPreference(context, proptag, ref obj, ref origin, ref maxValue, ref text, ref creationTime, fidMid, obj2, original);
								break;
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MinInRelevanceScore:
								if (obj2 == null || !(obj2 is int))
								{
									obj2 = int.MaxValue;
								}
								if (obj == null || (int)obj > (int)obj2)
								{
									obj = obj2;
								}
								break;
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.BooleanAnd:
								flag = new bool?((flag == null || flag.Value) && (bool)((obj2 == null) ? false : obj2));
								if (!flag.Value)
								{
									flag2 = true;
								}
								break;
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromInitialMessage:
							{
								byte[] conversationIndex = this.GetMessagePropertyValue(fidMid, PropTag.Message.ConversationIndex, original) as byte[];
								DateTime valueOrDefault3 = ((DateTime?)this.GetMessagePropertyValue(fidMid, PropTag.Message.MessageDeliveryTime, original)).GetValueOrDefault(DateTime.MinValue);
								if (initialMessageFinder == null)
								{
									initialMessageFinder = new InitialMessageFinder(this.conversationMessages.Count<FidMid>());
								}
								initialMessageFinder.AddMessage(obj2, valueOrDefault3, conversationIndex);
								break;
							}
							case ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.UnionNotUnique_NullIfAllNotSet:
							{
								if (value.AggregatedProperty.IsMultiValued)
								{
									continue;
								}
								if (obj == null)
								{
									obj = new List<KeyValuePair<DateTime, object>>();
								}
								if (obj2 == null)
								{
									obj2 = ConversationMembers.GetAggregatedPropertyDefaultValue(value.AggregatedProperty);
								}
								List<KeyValuePair<DateTime, object>> list2 = (List<KeyValuePair<DateTime, object>>)obj;
								DateTime valueOrDefault4 = ((DateTime?)this.GetMessagePropertyValue(fidMid, PropTag.Message.MessageDeliveryTime, original)).GetValueOrDefault(DateTime.MinValue);
								list2.Add(new KeyValuePair<DateTime, object>(valueOrDefault4, obj2));
								break;
							}
							}
							if (flag2)
							{
								break;
							}
						}
					}
				}
			}
			if (ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union == value.Type && obj != null)
			{
				obj = ConversationMembers.ConvertMultivalueCollectionToMvArray(proptag, (IDictionary<object, DateTime>)obj);
			}
			if (ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.BooleanAnd == value.Type && flag != null)
			{
				obj = (flag.Value ? SerializedValue.BoxedTrue : SerializedValue.BoxedFalse);
			}
			if (ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromInitialMessage == value.Type && initialMessageFinder != null)
			{
				obj = initialMessageFinder.GetInitialMessagePropertyValue();
			}
			if (ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.UnionNotUnique_NullIfAllNotSet == value.Type && obj != null)
			{
				object aggregatedPropertyDefaultValue = ConversationMembers.GetAggregatedPropertyDefaultValue(value.AggregatedProperty);
				obj = ConversationMembers.ConvertUnionNotUniqueMvCollectionToMvArray(proptag, (List<KeyValuePair<DateTime, object>>)obj, aggregatedPropertyDefaultValue);
			}
			return obj;
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00031810 File Offset: 0x0002FA10
		private static ConversationMembers.ConversationPropertyAggregationInfo? GetAggregationInfo(Context context, Mailbox mailbox, StorePropTag proptag)
		{
			ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo;
			if (!ConversationMembers.conversationPropertyMapping.TryGetValue(proptag, out aggregationInfo))
			{
				return null;
			}
			return new ConversationMembers.ConversationPropertyAggregationInfo?(ConversationMembers.TranslateAggregationInfoIfNecessary(context, mailbox, proptag, aggregationInfo));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00031844 File Offset: 0x0002FA44
		private static ConversationMembers.ConversationPropertyAggregationInfo TranslateAggregationInfoIfNecessary(Context context, Mailbox mailbox, StorePropTag proptag, ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo)
		{
			if (PropTag.Message.ConversationCategories == proptag || PropTag.Message.ConversationCategoriesMailboxWide == proptag)
			{
				aggregationInfo = ConversationMembers.GetKeywordsAggregationInfo(context, mailbox, aggregationInfo);
			}
			else if (PropTag.Message.PersonFileAsMailboxWide == proptag)
			{
				aggregationInfo = ConversationMembers.GetFileAsAggregationInfo(context, mailbox, aggregationInfo);
			}
			else if (PropTag.Message.PersonWorkCityMailboxWide == proptag)
			{
				aggregationInfo = ConversationMembers.GetWorkCityAggregationInfo(context, mailbox, aggregationInfo);
			}
			else if (PropTag.Message.PersonDisplayNameFirstLastMailboxWide == proptag)
			{
				aggregationInfo = ConversationMembers.GetDisplayNameFirstLastAggregationInfo(context, mailbox, aggregationInfo);
			}
			else if (PropTag.Message.PersonDisplayNameLastFirstMailboxWide == proptag)
			{
				aggregationInfo = ConversationMembers.GetDisplayNameLastFirstAggregationInfo(context, mailbox, aggregationInfo);
			}
			return aggregationInfo;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000318DA File Offset: 0x0002FADA
		private static StorePropTag GetKeywordsPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.PublicStrings.Keywords.PropName, PropertyType.MVUnicode, ObjectType.Message);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000318F3 File Offset: 0x0002FAF3
		private static StorePropTag GetFileUnderPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Address.FileUnder.PropName, PropertyType.Unicode, ObjectType.Message);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00031909 File Offset: 0x0002FB09
		private static StorePropTag GetWorkAddressCityPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Address.WorkAddressCity.PropName, PropertyType.Unicode, ObjectType.Message);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0003191F File Offset: 0x0002FB1F
		private static StorePropTag GetDisplayNameFirstLastPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Address.DisplayNameFirstLast.PropName, PropertyType.Unicode, ObjectType.Message);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00031935 File Offset: 0x0002FB35
		private static StorePropTag GetDisplayNameLastFirstPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Address.DisplayNameLastFirst.PropName, PropertyType.Unicode, ObjectType.Message);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0003194B File Offset: 0x0002FB4B
		private static StorePropTag GetDisplayNamePriorityPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Address.DisplayNamePriority.PropName, PropertyType.Int32, ObjectType.Message);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00031960 File Offset: 0x0002FB60
		private static StorePropTag GetAllAttachmentsAreHiddenPropTag(Context context, Mailbox mailbox)
		{
			return mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Common.SmartNoAttach.PropName, PropertyType.Boolean, ObjectType.Message);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00031976 File Offset: 0x0002FB76
		private static void AddToMultiValueCollection(IDictionary<object, DateTime> multivalues, object value, DateTime messageTime)
		{
			if (!multivalues.ContainsKey(value) || multivalues[value] < messageTime)
			{
				multivalues[value] = messageTime;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00031998 File Offset: 0x0002FB98
		private static bool PropTagIsConversationFlagCompleteTime(StorePropTag proptag)
		{
			return PropTag.Message.ConversationFlagCompleteTime == proptag || PropTag.Message.ConversationFlagCompleteTimeMailboxWide == proptag;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x000319C8 File Offset: 0x0002FBC8
		private static object ConvertMultivalueCollectionToMvArray(StorePropTag proptag, IEnumerable<KeyValuePair<object, DateTime>> objectList)
		{
			IEnumerable<object> values = (from pair in objectList
			orderby pair.Value descending
			select pair.Key).Take(100);
			return ConversationMembers.CastCollectionToMvArray(proptag, values);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00031A60 File Offset: 0x0002FC60
		private static object ConvertUnionNotUniqueMvCollectionToMvArray(StorePropTag proptag, List<KeyValuePair<DateTime, object>> objectList, object aggregatedPropertyDefaultValue)
		{
			if (objectList.All((KeyValuePair<DateTime, object> pair) => ConversationMembers.CompareMvArrayItemValues(pair.Value, aggregatedPropertyDefaultValue, proptag)))
			{
				return null;
			}
			IEnumerable<object> values = (from pair in objectList
			orderby pair.Key descending
			select pair.Value).Take(100);
			return ConversationMembers.CastCollectionToMvArray(proptag, values);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00031AF0 File Offset: 0x0002FCF0
		private static object CastCollectionToMvArray(StorePropTag proptag, IEnumerable<object> values)
		{
			PropertyType propType = proptag.PropType;
			if (propType <= PropertyType.MVUnicode)
			{
				switch (propType)
				{
				case PropertyType.MVInt16:
					return ConversationMembers.CastToArrayOf<short>(values);
				case PropertyType.MVInt32:
					return ConversationMembers.CastToArrayOf<int>(values);
				default:
					if (propType == PropertyType.MVUnicode)
					{
						return ConversationMembers.CastToArrayOf<string>(values);
					}
					break;
				}
			}
			else
			{
				if (propType == PropertyType.MVSysTime)
				{
					return ConversationMembers.CastToArrayOf<DateTime>(values);
				}
				if (propType == PropertyType.MVBinary)
				{
					return ConversationMembers.CastToArrayOf<byte[]>(values);
				}
			}
			return null;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00031B60 File Offset: 0x0002FD60
		private static object GetAggregatedPropertyDefaultValue(StorePropTag proptag)
		{
			if (proptag == PropTag.Message.RichContent)
			{
				short num = 0;
				return num;
			}
			return null;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00031B84 File Offset: 0x0002FD84
		private static bool CompareMvArrayItemValues(object val1, object val2, StorePropTag proptag)
		{
			PropertyType propType = proptag.PropType;
			if (propType <= PropertyType.MVUnicode)
			{
				switch (propType)
				{
				case PropertyType.MVInt16:
					return (short)val1 == (short)val2;
				case PropertyType.MVInt32:
					return (int)val1 == (int)val2;
				default:
					if (propType == PropertyType.MVUnicode)
					{
						return ((string)val1).Equals((string)val2);
					}
					break;
				}
			}
			else
			{
				if (propType == PropertyType.MVSysTime)
				{
					return ((DateTime)val1).Equals((DateTime)val2);
				}
				if (propType == PropertyType.MVBinary)
				{
					return (byte)val1 == (byte)val2;
				}
			}
			return false;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00031C24 File Offset: 0x0002FE24
		private static T[] CastToArrayOf<T>(IEnumerable<object> objectList)
		{
			return objectList.Cast<T>().ToArray<T>();
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00031C34 File Offset: 0x0002FE34
		internal static byte[] BuildFidMid(ExchangeId fid, ExchangeId mid)
		{
			byte[] array = new byte[16];
			ExchangeIdHelpers.To8ByteArray(fid.Replid, fid.Counter, array, 0);
			ExchangeIdHelpers.To8ByteArray(mid.Replid, mid.Counter, array, 8);
			return array;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00031C78 File Offset: 0x0002FE78
		private static ConversationMembers.ConversationPropertyAggregationInfo GetKeywordsAggregationInfo(Context context, Mailbox mailbox, ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo)
		{
			StorePropTag keywordsPropTag = ConversationMembers.GetKeywordsPropTag(context, mailbox);
			return new ConversationMembers.ConversationPropertyAggregationInfo(aggregationInfo.Type, aggregationInfo.Scope, keywordsPropTag);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		private static ConversationMembers.ConversationPropertyAggregationInfo GetFileAsAggregationInfo(Context context, Mailbox mailbox, ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo)
		{
			StorePropTag fileUnderPropTag = ConversationMembers.GetFileUnderPropTag(context, mailbox);
			return new ConversationMembers.ConversationPropertyAggregationInfo(aggregationInfo.Type, aggregationInfo.Scope, fileUnderPropTag);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00031CD0 File Offset: 0x0002FED0
		private static ConversationMembers.ConversationPropertyAggregationInfo GetWorkCityAggregationInfo(Context context, Mailbox mailbox, ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo)
		{
			StorePropTag workAddressCityPropTag = ConversationMembers.GetWorkAddressCityPropTag(context, mailbox);
			return new ConversationMembers.ConversationPropertyAggregationInfo(aggregationInfo.Type, aggregationInfo.Scope, workAddressCityPropTag);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00031CFC File Offset: 0x0002FEFC
		private static ConversationMembers.ConversationPropertyAggregationInfo GetDisplayNameFirstLastAggregationInfo(Context context, Mailbox mailbox, ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo)
		{
			StorePropTag displayNameFirstLastPropTag = ConversationMembers.GetDisplayNameFirstLastPropTag(context, mailbox);
			return new ConversationMembers.ConversationPropertyAggregationInfo(aggregationInfo.Type, aggregationInfo.Scope, displayNameFirstLastPropTag);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00031D28 File Offset: 0x0002FF28
		private static ConversationMembers.ConversationPropertyAggregationInfo GetDisplayNameLastFirstAggregationInfo(Context context, Mailbox mailbox, ConversationMembers.ConversationPropertyAggregationInfo aggregationInfo)
		{
			StorePropTag displayNameLastFirstPropTag = ConversationMembers.GetDisplayNameLastFirstPropTag(context, mailbox);
			return new ConversationMembers.ConversationPropertyAggregationInfo(aggregationInfo.Type, aggregationInfo.Scope, displayNameLastFirstPropTag);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00031D54 File Offset: 0x0002FF54
		private IList<StorePropTag> GetPropertiesToCache(Context context)
		{
			HashSet<StorePropTag> hashSet = new HashSet<StorePropTag>();
			hashSet.Add(PropTag.Message.CreationTime);
			hashSet.Add(PropTag.Message.DocumentId);
			foreach (KeyValuePair<StorePropTag, ConversationMembers.ConversationPropertyAggregationInfo> keyValuePair in ConversationMembers.conversationPropertyMapping)
			{
				if (this.aggregatePropertiesToCompute == null || this.aggregatePropertiesToCompute.Contains(keyValuePair.Key))
				{
					ConversationMembers.ConversationPropertyAggregationInfo conversationPropertyAggregationInfo = ConversationMembers.TranslateAggregationInfoIfNecessary(context, this.mailbox, keyValuePair.Key, keyValuePair.Value);
					if (conversationPropertyAggregationInfo.AggregatedProperty != PropTag.Message.FidMid && conversationPropertyAggregationInfo.AggregatedProperty != PropTag.Message.ContentCount)
					{
						hashSet.Add(conversationPropertyAggregationInfo.AggregatedProperty);
					}
					if (conversationPropertyAggregationInfo.UnreadOnly)
					{
						hashSet.Add(PropTag.Message.Read);
					}
					if (conversationPropertyAggregationInfo.AggregatedProperty == PropTag.Message.HasAttach)
					{
						hashSet.Add(ConversationMembers.GetAllAttachmentsAreHiddenPropTag(context, this.mailbox));
					}
					if (conversationPropertyAggregationInfo.Type == ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromInitialMessage)
					{
						hashSet.Add(PropTag.Message.ConversationIndex);
						hashSet.Add(PropTag.Message.MessageDeliveryTime);
					}
					else if (conversationPropertyAggregationInfo.Type == ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder)
					{
						hashSet.Add(PropTag.Message.PartnerNetworkId);
						hashSet.Add(ConversationMembers.GetDisplayNamePriorityPropTag(context, this.mailbox));
						hashSet.Add(ConversationMembers.GetDisplayNameFirstLastPropTag(context, this.mailbox));
					}
					else if (conversationPropertyAggregationInfo.Type == ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MaxInOriginPreferenceOrder)
					{
						hashSet.Add(PropTag.Message.PartnerNetworkId);
					}
					else if (conversationPropertyAggregationInfo.Type == ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union)
					{
						hashSet.Add(PropTag.Message.MessageDeliveryTime);
					}
				}
			}
			return hashSet.ToList<StorePropTag>();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00031FCC File Offset: 0x000301CC
		private void CacheMessageProperties(Context context, bool original)
		{
			if ((original ? this.cachedOriginalMessageProperties : this.cachedMessageProperties) != null)
			{
				return;
			}
			IList<FidMid> list = original ? this.originalConversationMessages : this.conversationMessages;
			Dictionary<FidMid, Dictionary<StorePropTag, object>> dictionary = new Dictionary<FidMid, Dictionary<StorePropTag, object>>(list.Count);
			IList<StorePropTag> propertiesToCache = this.GetPropertiesToCache(context);
			ExchangeId exchangeId = ExchangeId.Null;
			ExchangeId exchangeId2 = ExchangeId.Null;
			bool flag = false;
			if (this.modifiedMessage != null)
			{
				if (!original)
				{
					exchangeId = this.modifiedMessage.GetFolderId(context);
					exchangeId2 = this.modifiedMessage.GetId(context);
				}
				else
				{
					exchangeId = this.modifiedMessage.GetOriginalFolderId(context);
					exchangeId2 = this.modifiedMessage.OriginalMessageID;
				}
				FidMid item = new FidMid(exchangeId, exchangeId2);
				if (list.Contains(item))
				{
					list = new List<FidMid>(list);
					list.Remove(item);
					flag = true;
				}
			}
			if (list.Count > 0)
			{
				Dictionary<FidMid, Dictionary<StorePropTag, object>> dictionary2 = original ? this.cachedMessageProperties : this.cachedOriginalMessageProperties;
				if (dictionary2 != null)
				{
					using (IEnumerator<FidMid> enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							FidMid key = enumerator.Current;
							Dictionary<StorePropTag, object> value = dictionary2[key];
							dictionary[key] = value;
						}
						goto IL_43D;
					}
				}
				MessageTable messageTable = DatabaseSchema.MessageTable(this.mailbox.Database);
				ConversationMembersBlobTableFunction conversationMembersBlobTableFunction = DatabaseSchema.ConversationMembersBlobTableFunction(this.mailbox.Database);
				List<Column> list2 = new List<Column>(propertiesToCache.Count + 2);
				for (int i = 0; i < propertiesToCache.Count; i++)
				{
					list2.Add(PropertySchema.MapToColumn(context.Database, ObjectType.Message, propertiesToCache[i]));
				}
				list2.Add(messageTable.FolderId);
				list2.Add(messageTable.MessageId);
				List<ConversationMembersBlob> list3 = new List<ConversationMembersBlob>(list.Count);
				for (int j = 0; j < list.Count; j++)
				{
					list3.Add(new ConversationMembersBlob(list[j].FolderId.To26ByteArray(), list[j].MessageId.To26ByteArray(), j));
				}
				byte[] array = ConversationMembersBlob.Serialize(list3);
				using (TableFunctionOperator tableFunctionOperator = Factory.CreateTableFunctionOperator(context.Culture, context, conversationMembersBlobTableFunction.TableFunction, new object[]
				{
					array
				}, messageTable.MessageUnique.Columns, null, new Dictionary<Column, Column>(4)
				{
					{
						messageTable.MailboxPartitionNumber,
						Factory.CreateConstantColumn(this.mailbox.MailboxPartitionNumber, messageTable.MailboxPartitionNumber)
					},
					{
						messageTable.FolderId,
						conversationMembersBlobTableFunction.FolderId
					},
					{
						messageTable.MessageId,
						conversationMembersBlobTableFunction.MessageId
					},
					{
						messageTable.IsHidden,
						Factory.CreateConstantColumn(false, messageTable.IsHidden)
					}
				}, 0, 0, KeyRange.AllRows, false, true))
				{
					using (JoinOperator joinOperator = Factory.CreateJoinOperator(context.Culture, context, messageTable.Table, list2, null, new Dictionary<Column, Column>(4)
					{
						{
							messageTable.VirtualIsRead,
							messageTable.IsRead
						}
					}, 0, 0, messageTable.MessageUnique.Columns, tableFunctionOperator, true))
					{
						using (Reader reader = joinOperator.ExecuteReader(true))
						{
							while (reader.Read())
							{
								Dictionary<StorePropTag, object> dictionary3 = new Dictionary<StorePropTag, object>(propertiesToCache.Count<StorePropTag>() + 2);
								for (int k = 0; k < propertiesToCache.Count; k++)
								{
									StorePropTag key2 = propertiesToCache[k];
									dictionary3[key2] = reader.GetValue(list2[k]);
								}
								ExchangeId exchangeId3 = ExchangeId.CreateFrom26ByteArray(context, this.mailbox.ReplidGuidMap, reader.GetBinary(messageTable.FolderId));
								ExchangeId exchangeId4 = ExchangeId.CreateFrom26ByteArray(context, this.mailbox.ReplidGuidMap, reader.GetBinary(messageTable.MessageId));
								dictionary3[PropTag.Message.ContentCount] = 1;
								dictionary3[PropTag.Message.FidMid] = ConversationMembers.BuildFidMid(exchangeId3, exchangeId4);
								dictionary[new FidMid(exchangeId3, exchangeId4)] = dictionary3;
							}
						}
					}
				}
			}
			IL_43D:
			if (flag)
			{
				Dictionary<StorePropTag, object> dictionary4 = new Dictionary<StorePropTag, object>(propertiesToCache.Count<StorePropTag>() + 2);
				foreach (StorePropTag storePropTag in propertiesToCache)
				{
					dictionary4[storePropTag] = (original ? this.modifiedMessage.GetOriginalPropertyValue(context, storePropTag) : this.modifiedMessage.GetPropertyValue(context, storePropTag));
				}
				dictionary4[PropTag.Message.ContentCount] = 1;
				dictionary4[PropTag.Message.FidMid] = ConversationMembers.BuildFidMid(exchangeId, exchangeId2);
				dictionary[new FidMid(exchangeId, exchangeId2)] = dictionary4;
			}
			List<FidMid> list4;
			if (original)
			{
				this.cachedOriginalMessageProperties = dictionary;
				list4 = this.originalConversationMessages;
			}
			else
			{
				this.cachedMessageProperties = dictionary;
				list4 = this.conversationMessages;
			}
			if (list4.Count > 1)
			{
				list4.Sort(delegate(FidMid lhs, FidMid rhs)
				{
					DateTime valueOrDefault = ((DateTime?)this.GetMessagePropertyValue(lhs, PropTag.Message.CreationTime, original)).GetValueOrDefault(DateTime.MinValue);
					int num = ((DateTime?)this.GetMessagePropertyValue(rhs, PropTag.Message.CreationTime, original)).GetValueOrDefault(DateTime.MinValue).CompareTo(valueOrDefault);
					if (num == 0)
					{
						int value2 = (int)this.GetMessagePropertyValue(lhs, PropTag.Message.DocumentId, original);
						num = ((int)this.GetMessagePropertyValue(rhs, PropTag.Message.DocumentId, original)).CompareTo(value2);
					}
					return num;
				});
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00032588 File Offset: 0x00030788
		private void UpdateAggregatedValueBasedOnPriorityAndOriginPreference(Context context, StorePropTag propTag, ref object aggregatePropertyValue, ref string aggregatePropertyOrigin, ref int aggregatePropertyPriority, ref string aggregatePropertyDisplayName, ref DateTime aggregatePropertyCreationTime, FidMid fidMid, object propertyValue, bool original)
		{
			StorePropTag displayNamePriorityPropTag = ConversationMembers.GetDisplayNamePriorityPropTag(context, this.mailbox);
			StorePropTag displayNameFirstLastPropTag = ConversationMembers.GetDisplayNameFirstLastPropTag(context, this.mailbox);
			int valueOrDefault = ((int?)this.GetMessagePropertyValue(fidMid, displayNamePriorityPropTag, original)).GetValueOrDefault(int.MaxValue);
			string text = (string)this.GetMessagePropertyValue(fidMid, displayNameFirstLastPropTag, original);
			string text2 = (string)this.GetMessagePropertyValue(fidMid, PropTag.Message.PartnerNetworkId, original);
			DateTime valueOrDefault2 = ((DateTime?)this.GetMessagePropertyValue(fidMid, PropTag.Message.CreationTime, original)).GetValueOrDefault(DateTime.MinValue);
			if (aggregatePropertyPriority > valueOrDefault)
			{
				aggregatePropertyValue = propertyValue;
				aggregatePropertyDisplayName = text;
				aggregatePropertyPriority = valueOrDefault;
				aggregatePropertyOrigin = text2;
				aggregatePropertyCreationTime = valueOrDefault2;
				return;
			}
			if (aggregatePropertyPriority == valueOrDefault && OriginPreferenceComparer.Instance.Compare(context, this.mailbox, propTag, aggregatePropertyDisplayName, aggregatePropertyOrigin, aggregatePropertyCreationTime, text, text2, valueOrDefault2) <= 0)
			{
				aggregatePropertyValue = propertyValue;
				aggregatePropertyDisplayName = text;
				aggregatePropertyPriority = valueOrDefault;
				aggregatePropertyOrigin = text2;
				aggregatePropertyCreationTime = valueOrDefault2;
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00032681 File Offset: 0x00030881
		private object GetMessagePropertyValue(FidMid fidMid, StorePropTag propTag, bool original)
		{
			return (original ? this.cachedOriginalMessageProperties : this.cachedMessageProperties)[fidMid][propTag];
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000326A0 File Offset: 0x000308A0
		private bool ConversationIsFlagged(Context context, bool folderScope, ICollection<FidMid> filterList, ExchangeId folderId, bool original)
		{
			object aggregateProperty = this.GetAggregateProperty(context, folderScope ? PropTag.Message.ConversationFlagStatus : PropTag.Message.ConversationFlagStatusMailboxWide, filterList, folderId, original);
			return aggregateProperty != null && 1 == (int)aggregateProperty;
		}

		// Token: 0x0400023C RID: 572
		internal const int MaxMultivalueInstances = 100;

		// Token: 0x0400023D RID: 573
		internal const int MaxStringValueLength = 255;

		// Token: 0x0400023E RID: 574
		private const int RelevanceScoreForIrrelevantContactEntries = 2147483647;

		// Token: 0x0400023F RID: 575
		internal static readonly string[] DisplayToSeparators = new string[]
		{
			"; "
		};

		// Token: 0x04000240 RID: 576
		private static readonly int[] validIconIndexes = new int[]
		{
			261,
			262,
			275,
			276,
			277,
			278
		};

		// Token: 0x04000241 RID: 577
		private static readonly Dictionary<StorePropTag, ConversationMembers.ConversationPropertyAggregationInfo> conversationPropertyMapping = new Dictionary<StorePropTag, ConversationMembers.ConversationPropertyAggregationInfo>
		{
			{
				PropTag.Message.ConversationTopic,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.NormalizedSubject)
			},
			{
				PropTag.Message.ConversationMvFrom,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.SentRepresentingName)
			},
			{
				PropTag.Message.ConversationMvFromMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.SentRepresentingName)
			},
			{
				PropTag.Message.ConversationMvTo,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.DisplayTo)
			},
			{
				PropTag.Message.ConversationMvToMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.DisplayTo)
			},
			{
				PropTag.Message.ConversationMessageDeliveryTime,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.MessageDeliveryTime)
			},
			{
				PropTag.Message.ConversationMessageDeliveryTimeMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.MessageDeliveryTime)
			},
			{
				PropTag.Message.ConversationCategories,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, StorePropTag.Invalid)
			},
			{
				PropTag.Message.ConversationCategoriesMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, StorePropTag.Invalid)
			},
			{
				PropTag.Message.ConversationFlagStatus,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.FlagStatus)
			},
			{
				PropTag.Message.ConversationFlagStatusMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.FlagStatus)
			},
			{
				PropTag.Message.ConversationFlagCompleteTime,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.FlagCompleteTime)
			},
			{
				PropTag.Message.ConversationFlagCompleteTimeMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.FlagCompleteTime)
			},
			{
				PropTag.Message.ConversationHasAttach,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.HasAttach)
			},
			{
				PropTag.Message.ConversationHasAttachMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.HasAttach)
			},
			{
				PropTag.Message.ConversationContentCount,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.ContentCount)
			},
			{
				PropTag.Message.ConversationContentCountMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.ContentCount)
			},
			{
				PropTag.Message.ConversationContentUnread,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered | ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.UnreadOnly, PropTag.Message.ContentCount)
			},
			{
				PropTag.Message.ConversationContentUnreadMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All | ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.UnreadOnly, PropTag.Message.ContentCount)
			},
			{
				PropTag.Message.ConversationMessageSize,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.MessageSize32)
			},
			{
				PropTag.Message.ConversationMessageSizeMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Sum, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.MessageSize32)
			},
			{
				PropTag.Message.ConversationMessageClasses,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.MessageClass)
			},
			{
				PropTag.Message.ConversationMessageClassesMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.MessageClass)
			},
			{
				PropTag.Message.ConversationReplyForwardState,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.IconIndex)
			},
			{
				PropTag.Message.ConversationReplyForwardStateMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.IconIndex)
			},
			{
				PropTag.Message.ConversationImportance,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.Importance)
			},
			{
				PropTag.Message.ConversationImportanceMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.Importance)
			},
			{
				PropTag.Message.ConversationMvFromUnread,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered | ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.UnreadOnly, PropTag.Message.SentRepresentingName)
			},
			{
				PropTag.Message.ConversationMvFromUnreadMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All | ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.UnreadOnly, PropTag.Message.SentRepresentingName)
			},
			{
				PropTag.Message.ConversationMvItemIds,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.FidMid)
			},
			{
				PropTag.Message.ConversationMvItemIdsMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.FidMid)
			},
			{
				PropTag.Message.ConversationHasIrm,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.IsIRMMessage)
			},
			{
				PropTag.Message.ConversationHasIrmMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.IsIRMMessage)
			},
			{
				PropTag.Message.PersonCompanyNameMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MaxInOriginPreferenceOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.CompanyName)
			},
			{
				PropTag.Message.PersonDisplayNameMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.DisplayName)
			},
			{
				PropTag.Message.PersonGivenNameMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.GivenName)
			},
			{
				PropTag.Message.PersonSurnameMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.SurName)
			},
			{
				PropTag.Message.PersonRelevanceScoreMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MinInRelevanceScore, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.RelevanceScore)
			},
			{
				PropTag.Message.PersonHomeCityMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MaxInOriginPreferenceOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.HomeAddressCity)
			},
			{
				PropTag.Message.PersonCreationTimeMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.CreationTime)
			},
			{
				PropTag.Message.PersonFileAsMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, StorePropTag.Invalid)
			},
			{
				PropTag.Message.PersonWorkCityMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.MaxInOriginPreferenceOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, StorePropTag.Invalid)
			},
			{
				PropTag.Message.PersonDisplayNameFirstLastMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, StorePropTag.Invalid)
			},
			{
				PropTag.Message.PersonDisplayNameLastFirstMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.DisplayNamePriorityOrder, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, StorePropTag.Invalid)
			},
			{
				PropTag.Message.ConversationLastMemberDocumentId,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.DocumentId)
			},
			{
				PropTag.Message.ConversationLastMemberDocumentIdMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.DocumentId)
			},
			{
				PropTag.Message.ConversationHasClutter,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.BooleanAnd, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.IsClutter)
			},
			{
				PropTag.Message.ConversationHasClutterMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.BooleanAnd, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.IsClutter)
			},
			{
				PropTag.Message.ConversationInitialMemberDocumentId,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromInitialMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.DocumentId)
			},
			{
				PropTag.Message.ConversationMemberDocumentIds,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Union, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.DocumentId)
			},
			{
				PropTag.Message.ConversationMessageDeliveryOrRenewTimeMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.DeliveryOrRenewTime)
			},
			{
				PropTag.Message.FamilyId,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.FromNewestMessage, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.ConversationFamilyId)
			},
			{
				PropTag.Message.ConversationMessageRichContentMailboxWide,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.UnionNotUnique_NullIfAllNotSet, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.All, PropTag.Message.RichContent)
			},
			{
				PropTag.Message.ConversationMessageDeliveryOrRenewTime,
				new ConversationMembers.ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType.Max, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered, PropTag.Message.DeliveryOrRenewTime)
			}
		};

		// Token: 0x04000242 RID: 578
		private readonly Mailbox mailbox;

		// Token: 0x04000243 RID: 579
		private readonly List<FidMid> conversationMessages;

		// Token: 0x04000244 RID: 580
		private readonly List<FidMid> originalConversationMessages;

		// Token: 0x04000245 RID: 581
		private readonly TopMessage modifiedMessage;

		// Token: 0x04000246 RID: 582
		private Dictionary<FidMid, Dictionary<StorePropTag, object>> cachedMessageProperties;

		// Token: 0x04000247 RID: 583
		private Dictionary<FidMid, Dictionary<StorePropTag, object>> cachedOriginalMessageProperties;

		// Token: 0x04000248 RID: 584
		private HashSet<StorePropTag> aggregatePropertiesToCompute;

		// Token: 0x02000022 RID: 34
		private struct ConversationPropertyAggregationInfo
		{
			// Token: 0x06000581 RID: 1409 RVA: 0x00032C0F File Offset: 0x00030E0F
			public ConversationPropertyAggregationInfo(ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType type, ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope scope, StorePropTag proptag)
			{
				this.type = type;
				this.scope = scope;
				this.proptag = proptag;
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000582 RID: 1410 RVA: 0x00032C26 File Offset: 0x00030E26
			public ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000583 RID: 1411 RVA: 0x00032C2E File Offset: 0x00030E2E
			public ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope Scope
			{
				get
				{
					return this.scope;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000584 RID: 1412 RVA: 0x00032C36 File Offset: 0x00030E36
			public bool UnreadOnly
			{
				get
				{
					return (ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope)0 != (this.Scope & ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.UnreadOnly);
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000585 RID: 1413 RVA: 0x00032C46 File Offset: 0x00030E46
			public bool Filtered
			{
				get
				{
					return (ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope)0 != (this.Scope & ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope.Filtered);
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000586 RID: 1414 RVA: 0x00032C56 File Offset: 0x00030E56
			public StorePropTag AggregatedProperty
			{
				get
				{
					return this.proptag;
				}
			}

			// Token: 0x0400024D RID: 589
			private readonly ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationType type;

			// Token: 0x0400024E RID: 590
			private readonly ConversationMembers.ConversationPropertyAggregationInfo.ConversationPropertyAggregationScope scope;

			// Token: 0x0400024F RID: 591
			private readonly StorePropTag proptag;

			// Token: 0x02000023 RID: 35
			public enum ConversationPropertyAggregationType
			{
				// Token: 0x04000251 RID: 593
				FromNewestMessage,
				// Token: 0x04000252 RID: 594
				MaxInOriginPreferenceOrder,
				// Token: 0x04000253 RID: 595
				Max,
				// Token: 0x04000254 RID: 596
				Sum,
				// Token: 0x04000255 RID: 597
				Union,
				// Token: 0x04000256 RID: 598
				DisplayNamePriorityOrder,
				// Token: 0x04000257 RID: 599
				MinInRelevanceScore,
				// Token: 0x04000258 RID: 600
				BooleanAnd,
				// Token: 0x04000259 RID: 601
				FromInitialMessage,
				// Token: 0x0400025A RID: 602
				UnionNotUnique_NullIfAllNotSet
			}

			// Token: 0x02000024 RID: 36
			[Flags]
			public enum ConversationPropertyAggregationScope
			{
				// Token: 0x0400025C RID: 604
				Filtered = 1,
				// Token: 0x0400025D RID: 605
				All = 2,
				// Token: 0x0400025E RID: 606
				UnreadOnly = 4
			}
		}
	}
}
