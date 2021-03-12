using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.Ceres.InteractionEngine.Services.Exchange;
using Microsoft.Ceres.SearchCore.Admin.Model;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Fast
{
	// Token: 0x02000019 RID: 25
	internal class InstantSearchSchema
	{
		// Token: 0x06000172 RID: 370 RVA: 0x00008D80 File Offset: 0x00006F80
		public InstantSearchSchema(ICollection<PropertyDefinition> properties)
		{
			Dictionary<PropertyDefinition, int> dictionary = new Dictionary<PropertyDefinition, int>();
			HashSet<string> hashSet = new HashSet<string>();
			foreach (PropertyDefinition key in properties)
			{
				InstantSearchSchema.InstantSearchPropertyDefinition instantSearchPropertyDefinition;
				if (InstantSearchSchema.KnownProperties.TryGetValue(key, out instantSearchPropertyDefinition))
				{
					if (!dictionary.ContainsKey(key))
					{
						dictionary.Add(key, dictionary.Count);
						foreach (FastIndexSystemField fastIndexSystemField in instantSearchPropertyDefinition.FastProperties)
						{
							hashSet.Add(fastIndexSystemField.Name);
						}
					}
				}
				else
				{
					this.HasUnsupportedXsoProperties = true;
				}
			}
			this.xsoPropertyOffsets = dictionary;
			this.FastProperties = new List<string>(hashSet);
			this.XsoProperties = InstantSearchSchema.ToReadOnlyCollection<PropertyDefinition>(properties);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00008E58 File Offset: 0x00007058
		public static InstantSearchSchema DefaultSchema
		{
			get
			{
				if (InstantSearchSchema.defaultSchema == null)
				{
					lock (InstantSearchSchema.schemaLock)
					{
						if (InstantSearchSchema.defaultSchema == null)
						{
							InstantSearchSchema.defaultSchema = new InstantSearchSchema(InstantSearchSchema.DefaultRequestedProperties);
						}
					}
				}
				return InstantSearchSchema.defaultSchema;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00008EB4 File Offset: 0x000070B4
		public static InstantSearchSchema DefaultConversationsSchema
		{
			get
			{
				if (InstantSearchSchema.defaultConversationsSchema == null)
				{
					lock (InstantSearchSchema.schemaLock)
					{
						if (InstantSearchSchema.defaultConversationsSchema == null)
						{
							InstantSearchSchema.defaultConversationsSchema = new InstantSearchSchema(InstantSearchSchema.DefaultRequestedPropertiesConversations);
						}
					}
				}
				return InstantSearchSchema.defaultConversationsSchema;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00008F10 File Offset: 0x00007110
		public static IReadOnlyCollection<PropertyDefinition> DefaultRefinersFAST
		{
			get
			{
				if (InstantSearchSchema.defaultRefinersFAST == null)
				{
					lock (InstantSearchSchema.schemaLock)
					{
						if (InstantSearchSchema.defaultRefinersFAST == null)
						{
							PropertyDefinition[] array = new PropertyDefinition[InstantSearchSchema.PropertyToRefinersMap.Count];
							InstantSearchSchema.PropertyToRefinersMap.Keys.CopyTo(array, 0);
							InstantSearchSchema.defaultRefinersFAST = array;
						}
					}
				}
				return InstantSearchSchema.defaultRefinersFAST;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008F84 File Offset: 0x00007184
		public static IReadOnlyCollection<PropertyDefinition> DefaultRefiners
		{
			get
			{
				if (InstantSearchSchema.defaultRefiners == null)
				{
					lock (InstantSearchSchema.schemaLock)
					{
						if (InstantSearchSchema.defaultRefiners == null)
						{
							PropertyDefinition[] array = new PropertyDefinition[InstantSearchSchema.RefinablePropertiesMap.Count];
							InstantSearchSchema.RefinablePropertiesMap.Keys.CopyTo(array, 0);
							InstantSearchSchema.defaultRefiners = array;
						}
					}
				}
				return InstantSearchSchema.defaultRefiners;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00008FF8 File Offset: 0x000071F8
		public static IReadOnlyCollection<PropertyDefinition> DefaultRefinersConversations
		{
			get
			{
				if (InstantSearchSchema.defaultRefinersConversations == null)
				{
					lock (InstantSearchSchema.schemaLock)
					{
						if (InstantSearchSchema.defaultRefinersConversations == null)
						{
							PropertyDefinition[] array = new PropertyDefinition[InstantSearchSchema.RefinablePropertiesMap.Count];
							InstantSearchSchema.RefinablePropertiesMap.Values.CopyTo(array, 0);
							InstantSearchSchema.defaultRefinersConversations = array;
						}
					}
				}
				return InstantSearchSchema.defaultRefinersConversations;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000906C File Offset: 0x0000726C
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00009074 File Offset: 0x00007274
		public ICollection<PropertyDefinition> XsoProperties { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000907D File Offset: 0x0000727D
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00009085 File Offset: 0x00007285
		public ICollection<string> FastProperties { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000908E File Offset: 0x0000728E
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00009096 File Offset: 0x00007296
		public bool HasUnsupportedXsoProperties { get; private set; }

		// Token: 0x0600017E RID: 382 RVA: 0x000090A0 File Offset: 0x000072A0
		public IReadOnlyCollection<IReadOnlyPropertyBag> ConvertSearchResultItemsToPropertyBags(MailboxSession mailboxSession, bool useConversations, ISearchResultItem[] fastResults, ISearchServiceConfig searchConfig)
		{
			int num;
			if (useConversations && !this.xsoPropertyOffsets.TryGetValue(ItemSchema.ConversationId, out num))
			{
				throw new InvalidOperationException("useConversations without including ItemSchema.ConversationId in the schema");
			}
			List<IReadOnlyPropertyBag> list = new List<IReadOnlyPropertyBag>(fastResults.Length);
			Dictionary<Guid, object[]> dictionary = useConversations ? new Dictionary<Guid, object[]>() : null;
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>(this.FastProperties.Count);
			int i = 0;
			while (i < fastResults.Length)
			{
				ISearchResultItem result = fastResults[i];
				dictionary2.Clear();
				this.ParseSearchResultItem(result, dictionary2);
				if (!searchConfig.ReadFlagEnabled)
				{
					dictionary2[FastIndexSystemSchema.IsRead.Name] = true;
				}
				bool updatingConversation = false;
				if (!useConversations)
				{
					object[] array = new object[this.XsoProperties.Count];
					list.Add(new InstantSearchSchema.FastResultsPropertyBag(this, array));
					goto IL_126;
				}
				object obj;
				if (dictionary2.TryGetValue(FastIndexSystemSchema.ConversationGuid.Name, out obj))
				{
					byte[] array2 = obj as byte[];
					if (array2 != null && array2.Length == 16)
					{
						Guid key = new Guid(array2);
						object[] array;
						if (!dictionary.TryGetValue(key, out array))
						{
							array = new object[this.XsoProperties.Count];
							dictionary.Add(key, array);
							list.Add(new InstantSearchSchema.FastResultsPropertyBag(this, array));
							goto IL_126;
						}
						updatingConversation = true;
						goto IL_126;
					}
				}
				IL_163:
				i++;
				continue;
				IL_126:
				foreach (PropertyDefinition property in this.XsoProperties)
				{
					object[] array;
					this.CalculateXsoProperty(array, property, mailboxSession, dictionary2, updatingConversation);
				}
				goto IL_163;
			}
			return list;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00009234 File Offset: 0x00007434
		public string GetFastSortOrder(ICollection<SortBy> sortByCollection)
		{
			if (sortByCollection == null || sortByCollection.Count == 0)
			{
				return "-received";
			}
			if (sortByCollection.Count == 1)
			{
				using (IEnumerator<SortBy> enumerator = sortByCollection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						SortBy sortBy = enumerator.Current;
						string str;
						if (!InstantSearchSchema.SortOrderMap.TryGetValue(sortBy.ColumnDefinition, out str))
						{
							throw new InvalidOperationException("Unsupported SortBy column");
						}
						return ((sortBy.SortOrder == SortOrder.Ascending) ? "+" : "-") + str;
					}
				}
			}
			throw new InvalidOperationException("Too many SortBy clauses");
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000092D8 File Offset: 0x000074D8
		public bool SortOrderSupportedByFast(ICollection<SortBy> sortByCollection)
		{
			if (sortByCollection == null || sortByCollection.Count == 0)
			{
				return true;
			}
			if (sortByCollection.Count > 1)
			{
				return false;
			}
			using (IEnumerator<SortBy> enumerator = sortByCollection.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					SortBy sortBy = enumerator.Current;
					return InstantSearchSchema.SortOrderMap.ContainsKey(sortBy.ColumnDefinition);
				}
			}
			throw new InvalidOperationException("Could not find SortBy field");
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009350 File Offset: 0x00007550
		private static ReadOnlyCollection<T> ToReadOnlyCollection<T>(ICollection<T> collection)
		{
			IList<T> list = collection as IList<T>;
			if (list != null)
			{
				return new ReadOnlyCollection<T>(list);
			}
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return new ReadOnlyCollection<T>(array);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009388 File Offset: 0x00007588
		private static string ParticipantListToString(ICollection<string> participants)
		{
			if (participants == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string participantString in participants)
			{
				SearchParticipant searchParticipant = SearchParticipant.FromParticipantString(participantString);
				if (searchParticipant != null)
				{
					string value;
					if (!string.IsNullOrEmpty(searchParticipant.DisplayName))
					{
						value = searchParticipant.DisplayName;
					}
					else
					{
						if (string.IsNullOrEmpty(searchParticipant.SmtpAddress))
						{
							continue;
						}
						value = searchParticipant.SmtpAddress;
					}
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append("; ");
					}
					stringBuilder.Append(value);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009434 File Offset: 0x00007634
		private void CalculateXsoProperty(object[] row, PropertyDefinition property, MailboxSession mailboxSession, Dictionary<string, object> parsedFastResult, bool updatingConversation)
		{
			int num;
			if (!this.xsoPropertyOffsets.TryGetValue(property, out num))
			{
				return;
			}
			InstantSearchSchema.InstantSearchPropertyDefinition instantSearchPropertyDefinition;
			if (!InstantSearchSchema.KnownProperties.TryGetValue(property, out instantSearchPropertyDefinition))
			{
				return;
			}
			if (updatingConversation && instantSearchPropertyDefinition.ConversionMethod != InstantSearchSchema.ConversionMethod.UnreadCount && instantSearchPropertyDefinition.ConversionMethod != InstantSearchSchema.ConversionMethod.MessageCount)
			{
				return;
			}
			object obj = null;
			if (instantSearchPropertyDefinition.FastProperties.Length == 1)
			{
				parsedFastResult.TryGetValue(instantSearchPropertyDefinition.FastProperties[0].Name, out obj);
			}
			switch (instantSearchPropertyDefinition.ConversionMethod)
			{
			case InstantSearchSchema.ConversionMethod.String:
				row[num] = obj;
				return;
			case InstantSearchSchema.ConversionMethod.Boolean:
				row[num] = this.GetBoolean(obj);
				return;
			case InstantSearchSchema.ConversionMethod.DateTime:
				row[num] = ((obj is DateTime) ? ((ExDateTime)((DateTime)obj)) : null);
				return;
			case InstantSearchSchema.ConversionMethod.ConversationId:
			{
				byte[] array = obj as byte[];
				row[num] = ((array != null) ? ConversationId.Create(array) : null);
				return;
			}
			case InstantSearchSchema.ConversionMethod.From:
				row[num] = this.GetFromAsParticipant(obj);
				return;
			case InstantSearchSchema.ConversionMethod.DisplayNames:
				row[num] = InstantSearchSchema.ParticipantListToString(obj as ICollection<string>);
				return;
			case InstantSearchSchema.ConversionMethod.SenderDisplayName:
				row[num] = this.GetFromString(obj);
				return;
			case InstantSearchSchema.ConversionMethod.EntryId:
				row[num] = this.GetEntryId(mailboxSession, parsedFastResult);
				return;
			case InstantSearchSchema.ConversionMethod.IconIndex:
				row[num] = ((obj != null) ? ((IconIndex)this.GetUnboxedLong(obj)) : null);
				return;
			case InstantSearchSchema.ConversionMethod.FlagStatus:
				row[num] = ((obj != null) ? ((FlagStatus)this.GetUnboxedLong(obj)) : null);
				return;
			case InstantSearchSchema.ConversionMethod.Importance:
				row[num] = ((obj != null) ? ((Importance)this.GetUnboxedLong(obj)) : null);
				return;
			case InstantSearchSchema.ConversionMethod.UnreadCount:
			{
				int num2 = (row[num] == null) ? 0 : ((int)row[num]);
				if (!this.GetBoolean(obj))
				{
					num2++;
				}
				row[num] = num2;
				return;
			}
			case InstantSearchSchema.ConversionMethod.MessageCount:
			{
				int num3 = (row[num] == null) ? 0 : ((int)row[num]);
				row[num] = num3 + 1;
				return;
			}
			case InstantSearchSchema.ConversionMethod.MultiValueString:
				row[num] = ((obj != null) ? new string[]
				{
					(string)obj
				} : null);
				return;
			case InstantSearchSchema.ConversionMethod.MultiValueEntryId:
			{
				StoreObjectId entryId = this.GetEntryId(mailboxSession, parsedFastResult);
				row[num] = ((entryId == null) ? null : new StoreObjectId[]
				{
					entryId
				});
				return;
			}
			case InstantSearchSchema.ConversionMethod.MultiValueFrom:
			{
				string fromString = this.GetFromString(obj);
				row[num] = ((fromString == null) ? null : new string[]
				{
					fromString
				});
				return;
			}
			case InstantSearchSchema.ConversionMethod.MultiValueDisplayNames:
				row[num] = ((obj != null) ? new string[]
				{
					InstantSearchSchema.ParticipantListToString(obj as ICollection<string>)
				} : null);
				return;
			case InstantSearchSchema.ConversionMethod.WorkingSetSource:
				row[num] = ((obj != null) ? ((WorkingSetSource)this.GetUnboxedLong(obj)) : null);
				return;
			default:
				throw new InvalidOperationException("Unknown ConversionMethod: " + instantSearchPropertyDefinition.ConversionMethod);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000096CB File Offset: 0x000078CB
		private long GetUnboxedLong(object value)
		{
			if (value is long)
			{
				return (long)value;
			}
			return (long)((int)value);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000096E4 File Offset: 0x000078E4
		private string GetFromString(object value)
		{
			IList<string> list = value as IList<string>;
			if (list == null || list.Count == 0)
			{
				return null;
			}
			SearchParticipant searchParticipant = SearchParticipant.FromParticipantString(list[0]);
			if (searchParticipant == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(searchParticipant.DisplayName))
			{
				return searchParticipant.SmtpAddress;
			}
			return searchParticipant.DisplayName;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00009734 File Offset: 0x00007934
		private bool GetBoolean(object value)
		{
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is int)
			{
				return (int)value != 0;
			}
			if (value is long)
			{
				return (long)value != 0L;
			}
			string text = value as string;
			return text != null && text != "0";
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00009794 File Offset: 0x00007994
		private Participant GetFromAsParticipant(object value)
		{
			IList<string> list = value as IList<string>;
			if (list == null || list.Count == 0)
			{
				return null;
			}
			return this.GetParticipant(list[0]);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000097C4 File Offset: 0x000079C4
		private StoreObjectId GetEntryId(MailboxSession mailboxSession, Dictionary<string, object> parsedFastResult)
		{
			object obj;
			object obj2;
			if (mailboxSession == null || !parsedFastResult.TryGetValue(FastIndexSystemSchema.FolderId.Name, out obj) || !parsedFastResult.TryGetValue(FastIndexSystemSchema.Mid.Name, out obj2))
			{
				return null;
			}
			string value = (string)obj;
			long messageMid = (long)obj2;
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			byte[] array = HexConverter.HexStringToByteArray(value);
			if (array.Length != 24 && array.Length != 22)
			{
				return null;
			}
			Array.Resize<byte>(ref array, 22);
			long idFromLongTermId = mailboxSession.IdConverter.GetIdFromLongTermId(array);
			StoreObjectId storeObjectId = mailboxSession.IdConverter.CreateMessageId(idFromLongTermId, messageMid);
			object obj3;
			parsedFastResult.TryGetValue(FastIndexSystemSchema.ItemClass.Name, out obj3);
			storeObjectId.UpdateItemType(ObjectClass.GetObjectType(obj3 as string));
			return storeObjectId;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009884 File Offset: 0x00007A84
		private Participant GetParticipant(string participant)
		{
			SearchParticipant searchParticipant = SearchParticipant.FromParticipantString(participant);
			if (searchParticipant == null)
			{
				return null;
			}
			return new Participant(searchParticipant.DisplayName, searchParticipant.SmtpAddress, searchParticipant.RoutingType);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000098B4 File Offset: 0x00007AB4
		private void ParseSearchResultItem(ISearchResultItem result, Dictionary<string, object> parsedResults)
		{
			foreach (IFieldHolder fieldHolder in result.Fields)
			{
				if (fieldHolder.Value != null)
				{
					if (fieldHolder.Name != "Other")
					{
						parsedResults[fieldHolder.Name] = fieldHolder.Value;
					}
					else
					{
						this.ParseSearchResultItem((ISearchResultItem)fieldHolder.Value, parsedResults);
					}
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000993C File Offset: 0x00007B3C
		private object GetProperty(PropertyDefinition property, object[] row)
		{
			int num;
			if (this.xsoPropertyOffsets.TryGetValue(property, out num))
			{
				return row[num];
			}
			return null;
		}

		// Token: 0x04000095 RID: 149
		private const string DefaultFastSortSpec = "-received";

		// Token: 0x04000096 RID: 150
		internal static readonly ICollection<PropertyDefinition> DefaultRequestedProperties = new ReadOnlyCollection<PropertyDefinition>(new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.ConversationId,
			ItemSchema.Subject,
			StoreObjectSchema.ItemClass,
			ItemSchema.From,
			MessageItemSchema.SenderDisplayName,
			ItemSchema.DisplayTo,
			ItemSchema.ReceivedTime,
			ItemSchema.IconIndex,
			ItemSchema.Importance,
			MessageItemSchema.IsRead,
			ItemSchema.HasAttachment,
			ItemSchema.FlagStatus,
			ItemSchema.Preview
		});

		// Token: 0x04000097 RID: 151
		internal static readonly ICollection<PropertyDefinition> DefaultRequestedPropertiesConversations = new ReadOnlyCollection<PropertyDefinition>(new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationId,
			ConversationItemSchema.ConversationItemIds,
			ConversationItemSchema.ConversationGlobalItemIds,
			ConversationItemSchema.ConversationTopic,
			ConversationItemSchema.ConversationMVFrom,
			ConversationItemSchema.ConversationMVTo,
			ConversationItemSchema.ConversationLastDeliveryTime,
			ConversationItemSchema.ConversationMessageClasses,
			ConversationItemSchema.ConversationFlagStatus,
			ConversationItemSchema.ConversationHasIrm,
			ConversationItemSchema.ConversationImportance,
			ConversationItemSchema.ConversationHasAttach,
			ConversationItemSchema.ConversationUnreadMessageCount,
			ConversationItemSchema.ConversationGlobalUnreadMessageCount,
			ConversationItemSchema.ConversationMessageCount,
			ConversationItemSchema.ConversationGlobalMessageCount,
			ConversationItemSchema.ConversationPreview
		});

		// Token: 0x04000098 RID: 152
		internal static readonly IDictionary<PropertyDefinition, string> PropertyToRefinersMap = new ReadOnlyDictionary<PropertyDefinition, string>(new Dictionary<PropertyDefinition, string>
		{
			{
				ItemSchema.SearchRecipients,
				FastIndexSystemSchema.Recipients.Name
			},
			{
				ItemSchema.From,
				FastIndexSystemSchema.From.Name
			},
			{
				ItemSchema.HasAttachment,
				FastIndexSystemSchema.HasAttachment.Name
			},
			{
				StoreObjectSchema.ParentEntryId,
				FastIndexSystemSchema.FolderId.Name
			}
		});

		// Token: 0x04000099 RID: 153
		internal static readonly IDictionary<PropertyDefinition, PropertyDefinition> RefinablePropertiesMap = new ReadOnlyDictionary<PropertyDefinition, PropertyDefinition>(new Dictionary<PropertyDefinition, PropertyDefinition>
		{
			{
				ItemSchema.From,
				ConversationItemSchema.ConversationMVFrom
			},
			{
				ItemSchema.HasAttachment,
				ConversationItemSchema.ConversationHasAttach
			},
			{
				ItemSchema.SearchRecipients,
				ConversationItemSchema.ConversationMVTo
			}
		});

		// Token: 0x0400009A RID: 154
		internal static readonly FastIndexSystemField AllSearchableProperties = new FastIndexSystemField(VersionInfo.Legacy, new IndexSystemField
		{
			Name = "AllSearchableProps",
			Searchable = true,
			Queryable = false
		});

		// Token: 0x0400009B RID: 155
		internal static readonly IDictionary<PropertyDefinition, FastIndexSystemField> FastIndexSystemFieldsMap = new ReadOnlyDictionary<PropertyDefinition, FastIndexSystemField>(new Dictionary<PropertyDefinition, FastIndexSystemField>
		{
			{
				ItemSchema.SearchAllIndexedProps,
				InstantSearchSchema.AllSearchableProperties
			},
			{
				ItemSchema.MailboxGuid,
				FastIndexSystemSchema.MailboxGuid
			},
			{
				ItemSchema.From,
				FastIndexSystemSchema.From
			},
			{
				ItemSchema.SearchSender,
				FastIndexSystemSchema.From
			},
			{
				ItemSchema.SearchRecipientsTo,
				FastIndexSystemSchema.To
			},
			{
				ItemSchema.SearchRecipientsCc,
				FastIndexSystemSchema.Cc
			},
			{
				ItemSchema.SearchRecipientsBcc,
				FastIndexSystemSchema.Bcc
			},
			{
				ItemSchema.SearchRecipients,
				FastIndexSystemSchema.Recipients
			},
			{
				ItemSchema.Categories,
				FastIndexSystemSchema.Categories
			},
			{
				ItemSchema.Importance,
				FastIndexSystemSchema.Importance
			},
			{
				ItemSchema.Size,
				FastIndexSystemSchema.Size
			},
			{
				ItemSchema.HasAttachment,
				FastIndexSystemSchema.HasAttachment
			},
			{
				StoreObjectSchema.ParentEntryId,
				FastIndexSystemSchema.FolderId
			},
			{
				StoreObjectSchema.ParentItemId,
				FastIndexSystemSchema.FolderId
			},
			{
				ItemSchema.Subject,
				FastIndexSystemSchema.Subject
			},
			{
				StoreObjectSchema.ItemClass,
				FastIndexSystemSchema.ItemClass
			},
			{
				ItemSchema.ReceivedTime,
				FastIndexSystemSchema.Received
			},
			{
				ItemSchema.SentTime,
				FastIndexSystemSchema.Received
			},
			{
				MessageItemSchema.IsRead,
				FastIndexSystemSchema.IsRead
			},
			{
				ItemSchema.AttachmentContent,
				FastIndexSystemSchema.AttachmentFileNames
			},
			{
				AttachmentSchema.AttachLongFileName,
				FastIndexSystemSchema.AttachmentFileNames
			},
			{
				ItemSchema.TextBody,
				FastIndexSystemSchema.Body.Definition.Queryable ? FastIndexSystemSchema.Body : InstantSearchSchema.AllSearchableProperties
			},
			{
				ItemSchema.IndexingErrorCode,
				FastIndexSystemSchema.ErrorCode
			},
			{
				ItemSchema.LastIndexingAttemptTime,
				FastIndexSystemSchema.LastAttemptTime
			},
			{
				ItemSchema.IsPartiallyIndexed,
				FastIndexSystemSchema.IsPartiallyProcessed
			},
			{
				ItemSchema.WorkingSetFlags,
				FastIndexSystemSchema.WorkingSetFlags
			},
			{
				ItemSchema.WorkingSetSourcePartition,
				FastIndexSystemSchema.WorkingSetSourcePartition
			}
		});

		// Token: 0x0400009C RID: 156
		internal static readonly IReadOnlyCollection<SortBy> DefaultSortBySpec = new SortBy[]
		{
			new SortBy(ItemSchema.ReceivedTime, SortOrder.Descending)
		};

		// Token: 0x0400009D RID: 157
		internal static readonly IReadOnlyCollection<SortBy> DefaultSortBySpecConversations = new SortBy[]
		{
			new SortBy(ConversationItemSchema.ConversationLastDeliveryTime, SortOrder.Descending)
		};

		// Token: 0x0400009E RID: 158
		private static readonly Dictionary<PropertyDefinition, string> SortOrderMap = new Dictionary<PropertyDefinition, string>
		{
			{
				ItemSchema.ReceivedTime,
				FastIndexSystemSchema.Received.Name
			},
			{
				ConversationItemSchema.ConversationLastDeliveryTime,
				FastIndexSystemSchema.Received.Name
			}
		};

		// Token: 0x0400009F RID: 159
		private static readonly Dictionary<PropertyDefinition, InstantSearchSchema.InstantSearchPropertyDefinition> KnownProperties = new Dictionary<PropertyDefinition, InstantSearchSchema.InstantSearchPropertyDefinition>
		{
			{
				ItemSchema.Id,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.EntryId, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.FolderId,
					FastIndexSystemSchema.Mid,
					FastIndexSystemSchema.ItemClass
				})
			},
			{
				ItemSchema.From,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.From, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.From
				})
			},
			{
				ItemSchema.DisplayTo,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.DisplayNames, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.To
				})
			},
			{
				ItemSchema.Subject,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.Subject
				})
			},
			{
				ItemSchema.ConversationId,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.ConversationId, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.ConversationGuid
				})
			},
			{
				StoreObjectSchema.ItemClass,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.ItemClass
				})
			},
			{
				MessageItemSchema.SenderDisplayName,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.SenderDisplayName, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.From
				})
			},
			{
				ItemSchema.IconIndex,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.IconIndex, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.IconIndex
				})
			},
			{
				ItemSchema.Importance,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.Importance, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.Importance
				})
			},
			{
				MessageItemSchema.IsRead,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.Boolean, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.IsRead
				})
			},
			{
				ItemSchema.HasAttachment,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.Boolean, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.HasAttachment
				})
			},
			{
				ItemSchema.FlagStatus,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.FlagStatus, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.FlagStatus
				})
			},
			{
				ItemSchema.Preview,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.BodyPreview
				})
			},
			{
				ItemSchema.ReceivedTime,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.DateTime, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.RefinableReceived
				})
			},
			{
				ItemSchema.WorkingSetId,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.WorkingSetId
				})
			},
			{
				ItemSchema.WorkingSetSource,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.WorkingSetSource, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.WorkingSetSource
				})
			},
			{
				ItemSchema.WorkingSetSourcePartition,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.WorkingSetSourcePartition
				})
			},
			{
				ConversationItemSchema.ConversationItemIds,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MultiValueEntryId, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.FolderId,
					FastIndexSystemSchema.Mid,
					FastIndexSystemSchema.ItemClass
				})
			},
			{
				ConversationItemSchema.ConversationGlobalItemIds,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MultiValueEntryId, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.FolderId,
					FastIndexSystemSchema.Mid,
					FastIndexSystemSchema.ItemClass
				})
			},
			{
				ConversationItemSchema.ConversationTopic,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.Subject
				})
			},
			{
				ConversationItemSchema.ConversationMVFrom,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MultiValueFrom, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.From
				})
			},
			{
				ConversationItemSchema.ConversationMVTo,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MultiValueDisplayNames, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.To
				})
			},
			{
				ConversationItemSchema.ConversationLastDeliveryTime,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.DateTime, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.RefinableReceived
				})
			},
			{
				ConversationItemSchema.ConversationMessageClasses,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MultiValueString, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.ItemClass
				})
			},
			{
				ConversationItemSchema.ConversationFlagStatus,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.FlagStatus, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.FlagStatus
				})
			},
			{
				ConversationItemSchema.ConversationHasIrm,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.Boolean, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.HasIrm
				})
			},
			{
				ConversationItemSchema.ConversationImportance,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.Importance, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.Importance
				})
			},
			{
				ConversationItemSchema.ConversationHasAttach,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.Boolean, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.HasAttachment
				})
			},
			{
				ConversationItemSchema.ConversationUnreadMessageCount,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.UnreadCount, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.IsRead
				})
			},
			{
				ConversationItemSchema.ConversationGlobalUnreadMessageCount,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.UnreadCount, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.IsRead
				})
			},
			{
				ConversationItemSchema.ConversationMessageCount,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MessageCount, new FastIndexSystemField[0])
			},
			{
				ConversationItemSchema.ConversationGlobalMessageCount,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.MessageCount, new FastIndexSystemField[0])
			},
			{
				ConversationItemSchema.ConversationPreview,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.BodyPreview
				})
			},
			{
				ConversationItemSchema.ConversationWorkingSetSourcePartition,
				new InstantSearchSchema.InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod.String, new FastIndexSystemField[]
				{
					FastIndexSystemSchema.WorkingSetSourcePartition
				})
			}
		};

		// Token: 0x040000A0 RID: 160
		private static object schemaLock = new object();

		// Token: 0x040000A1 RID: 161
		private static InstantSearchSchema defaultSchema;

		// Token: 0x040000A2 RID: 162
		private static InstantSearchSchema defaultConversationsSchema;

		// Token: 0x040000A3 RID: 163
		private static IReadOnlyCollection<PropertyDefinition> defaultRefinersFAST;

		// Token: 0x040000A4 RID: 164
		private static IReadOnlyCollection<PropertyDefinition> defaultRefiners;

		// Token: 0x040000A5 RID: 165
		private static IReadOnlyCollection<PropertyDefinition> defaultRefinersConversations;

		// Token: 0x040000A6 RID: 166
		private readonly IReadOnlyDictionary<PropertyDefinition, int> xsoPropertyOffsets;

		// Token: 0x0200001A RID: 26
		private enum ConversionMethod
		{
			// Token: 0x040000AB RID: 171
			String,
			// Token: 0x040000AC RID: 172
			Boolean,
			// Token: 0x040000AD RID: 173
			DateTime,
			// Token: 0x040000AE RID: 174
			ConversationId,
			// Token: 0x040000AF RID: 175
			From,
			// Token: 0x040000B0 RID: 176
			DisplayNames,
			// Token: 0x040000B1 RID: 177
			SenderDisplayName,
			// Token: 0x040000B2 RID: 178
			EntryId,
			// Token: 0x040000B3 RID: 179
			IconIndex,
			// Token: 0x040000B4 RID: 180
			FlagStatus,
			// Token: 0x040000B5 RID: 181
			Importance,
			// Token: 0x040000B6 RID: 182
			UnreadCount,
			// Token: 0x040000B7 RID: 183
			MessageCount,
			// Token: 0x040000B8 RID: 184
			MultiValueString,
			// Token: 0x040000B9 RID: 185
			MultiValueEntryId,
			// Token: 0x040000BA RID: 186
			MultiValueFrom,
			// Token: 0x040000BB RID: 187
			MultiValueDisplayNames,
			// Token: 0x040000BC RID: 188
			WorkingSetSource
		}

		// Token: 0x0200001B RID: 27
		private class InstantSearchPropertyDefinition
		{
			// Token: 0x0600018D RID: 397 RVA: 0x0000A30B File Offset: 0x0000850B
			public InstantSearchPropertyDefinition(InstantSearchSchema.ConversionMethod conversionMethod, params FastIndexSystemField[] fastProperties)
			{
				this.ConversionMethod = conversionMethod;
				this.FastProperties = fastProperties;
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x0600018E RID: 398 RVA: 0x0000A321 File Offset: 0x00008521
			// (set) Token: 0x0600018F RID: 399 RVA: 0x0000A329 File Offset: 0x00008529
			public InstantSearchSchema.ConversionMethod ConversionMethod { get; private set; }

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000190 RID: 400 RVA: 0x0000A332 File Offset: 0x00008532
			// (set) Token: 0x06000191 RID: 401 RVA: 0x0000A33A File Offset: 0x0000853A
			public FastIndexSystemField[] FastProperties { get; private set; }
		}

		// Token: 0x0200001C RID: 28
		private class FastResultsPropertyBag : IReadOnlyPropertyBag
		{
			// Token: 0x06000192 RID: 402 RVA: 0x0000A343 File Offset: 0x00008543
			internal FastResultsPropertyBag(InstantSearchSchema schema, object[] row)
			{
				this.schema = schema;
				this.row = row;
			}

			// Token: 0x17000068 RID: 104
			public object this[PropertyDefinition propertyDefinition]
			{
				get
				{
					return this.schema.GetProperty(propertyDefinition, this.row);
				}
			}

			// Token: 0x06000194 RID: 404 RVA: 0x0000A36D File Offset: 0x0000856D
			public object[] GetProperties(ICollection<PropertyDefinition> propertyDefinitionArray)
			{
				throw new NotImplementedException();
			}

			// Token: 0x040000BF RID: 191
			private readonly object[] row;

			// Token: 0x040000C0 RID: 192
			private readonly InstantSearchSchema schema;
		}
	}
}
