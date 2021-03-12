using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter;
using Microsoft.Exchange.AirSync.SchemaConverter.AirSync;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000D7 RID: 215
	internal class MailboxSearchCriteriaBuilder
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00041700 File Offset: 0x0003F900
		public MailboxSearchCriteriaBuilder(CultureInfo cultureInfo)
		{
			this.ExcludedFolders = new HashSet<StoreObjectId>();
			this.cultureInfo = cultureInfo;
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0004171A File Offset: 0x0003F91A
		public List<string> FolderScope
		{
			get
			{
				return this.folderScope;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00041722 File Offset: 0x0003F922
		public Dictionary<string, MailboxSearchCriteriaBuilder.SchemaCacheItem> SchemaCache
		{
			get
			{
				return this.schemaCache;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0004172A File Offset: 0x0003F92A
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x00041732 File Offset: 0x0003F932
		public HashSet<StoreObjectId> ExcludedFolders { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0004173B File Offset: 0x0003F93B
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00041743 File Offset: 0x0003F943
		public Conversation Conversation { get; private set; }

		// Token: 0x06000C6D RID: 3181 RVA: 0x0004174C File Offset: 0x0003F94C
		public QueryFilter ParseTopLevelClassAndFolders(XmlNode queryNode, bool contentIndexingEnabled, IAirSyncVersionFactory versionFactory, IAirSyncContext context)
		{
			this.Clear();
			if (queryNode.ChildNodes.Count != 1)
			{
				throw new SearchFilterTooComplexException
				{
					ErrorStringForProtocolLogger = "SearchTooComplexError1"
				};
			}
			XmlNode xmlNode = queryNode.ChildNodes[0];
			if ("Search:" != xmlNode.NamespaceURI || "And" != xmlNode.Name)
			{
				throw new SearchFilterTooComplexException
				{
					ErrorStringForProtocolLogger = "SearchTooComplexError1"
				};
			}
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			List<XmlNode> list3 = new List<XmlNode>();
			this.contentIndexingEnabled = contentIndexingEnabled;
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				if ("AirSync:" == xmlNode2.NamespaceURI && "CollectionId" == xmlNode2.Name)
				{
					if (xmlNode2.ChildNodes.Count != 1 || xmlNode2.FirstChild.NodeType != XmlNodeType.Text)
					{
						throw new SearchProtocolErrorException
						{
							ErrorStringForProtocolLogger = "SearchProtocolError1"
						};
					}
					list2.Add(xmlNode2.InnerText);
				}
				else if ("AirSync:" == xmlNode2.NamespaceURI && "Class" == xmlNode2.Name)
				{
					if (xmlNode2.ChildNodes.Count != 1 || xmlNode2.FirstChild.NodeType != XmlNodeType.Text)
					{
						throw new SearchProtocolErrorException
						{
							ErrorStringForProtocolLogger = "SearchProtocolError2"
						};
					}
					list.Add(xmlNode2.InnerText);
				}
				else
				{
					list3.Add(xmlNode2);
				}
			}
			if (list3.Count < 1)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError3"
				};
			}
			this.folderScope = list2;
			this.airSyncClasses = list;
			this.schemaCache = new Dictionary<string, MailboxSearchCriteriaBuilder.SchemaCacheItem>();
			List<QueryFilter> list4 = new List<QueryFilter>();
			new List<QueryFilter>();
			if (list.Count == 0)
			{
				list.Add("Email");
				list.Add("Calendar");
				list.Add("Contacts");
				list.Add("Tasks");
				if (context.Request.Version >= 140)
				{
					list.Add("Notes");
					list.Add("SMS");
				}
			}
			foreach (string text in list)
			{
				string key;
				switch (key = text)
				{
				case "Email":
					this.schemaCache["Email"] = new MailboxSearchCriteriaBuilder.SchemaCacheItem(versionFactory.CreateEmailSchema(null), versionFactory.CreateMissingPropertyStrategy(null));
					continue;
				case "Calendar":
					this.schemaCache["Calendar"] = new MailboxSearchCriteriaBuilder.SchemaCacheItem(versionFactory.CreateCalendarSchema(), versionFactory.CreateMissingPropertyStrategy(null));
					continue;
				case "Contacts":
					this.schemaCache["Contacts"] = new MailboxSearchCriteriaBuilder.SchemaCacheItem(versionFactory.CreateContactsSchema(), versionFactory.CreateMissingPropertyStrategy(null));
					continue;
				case "Tasks":
					this.schemaCache["Tasks"] = new MailboxSearchCriteriaBuilder.SchemaCacheItem(versionFactory.CreateTasksSchema(), versionFactory.CreateMissingPropertyStrategy(null));
					continue;
				case "Notes":
					if (context.Request.Version < 140)
					{
						throw new SearchProtocolErrorException
						{
							ErrorStringForProtocolLogger = "SearchProtocolError4"
						};
					}
					this.schemaCache["Notes"] = new MailboxSearchCriteriaBuilder.SchemaCacheItem(versionFactory.CreateNotesSchema(), versionFactory.CreateMissingPropertyStrategy(null));
					continue;
				case "SMS":
					if (context.Request.Version < 140)
					{
						throw new SearchProtocolErrorException
						{
							ErrorStringForProtocolLogger = "SearchProtocolError5"
						};
					}
					this.schemaCache["SMS"] = new MailboxSearchCriteriaBuilder.SchemaCacheItem(versionFactory.CreateSmsSchema(), versionFactory.CreateMissingPropertyStrategy(null));
					continue;
				}
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError6"
				};
			}
			foreach (XmlNode node in list3)
			{
				QueryFilter queryFilter = this.ParseSearchNamespace(node);
				if (queryFilter == null)
				{
					return null;
				}
				list4.Add(queryFilter);
			}
			return MailboxSearchCriteriaBuilder.ConstructAndOrOperator(list4, "And");
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00041C64 File Offset: 0x0003FE64
		public bool DoesMatchCriteria(StoreObjectId parentFolderId, StoreObjectId storeObjectId)
		{
			IConversationTreeNode conversationTreeNode;
			return !this.ExcludedFolders.Contains(parentFolderId) && (this.Conversation == null || this.Conversation.ConversationTree.TryGetConversationTreeNode(storeObjectId, out conversationTreeNode));
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00041CA0 File Offset: 0x0003FEA0
		private static QueryFilter ConstructAndOrOperator(List<QueryFilter> childFilters, string op)
		{
			if (op != null)
			{
				QueryFilter result;
				if (!(op == "And"))
				{
					if (!(op == "Or"))
					{
						goto IL_3F;
					}
					result = new OrFilter(childFilters.ToArray());
				}
				else
				{
					result = new AndFilter(childFilters.ToArray());
				}
				return result;
			}
			IL_3F:
			throw new InvalidOperationException("Unexpected bad operator: " + op);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00041D00 File Offset: 0x0003FF00
		private static QueryFilter ParseLtGtOperator(XmlNode parentNode)
		{
			if (2 != parentNode.ChildNodes.Count)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError7"
				};
			}
			XmlNode xmlNode = parentNode.ChildNodes[0];
			if ("Email:" != xmlNode.NamespaceURI || "DateReceived" != xmlNode.Name)
			{
				throw new SearchFilterTooComplexException
				{
					ErrorStringForProtocolLogger = "SearchTooComplexError3"
				};
			}
			xmlNode = parentNode.ChildNodes[1];
			if ("Search:" != xmlNode.NamespaceURI || "Value" != xmlNode.Name)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError8"
				};
			}
			if (xmlNode.ChildNodes.Count > 1)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError9"
				};
			}
			AirSyncUtcDateTimeProperty airSyncUtcDateTimeProperty = new AirSyncUtcDateTimeProperty("Search:", "Value", AirSyncDateFormat.Punctuate, false);
			airSyncUtcDateTimeProperty.Bind(xmlNode);
			ExDateTime dateTime;
			try
			{
				dateTime = airSyncUtcDateTimeProperty.DateTime;
			}
			catch (AirSyncPermanentException ex)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError10:" + ex.ErrorStringForProtocolLogger
				};
			}
			string name;
			if ((name = parentNode.Name) != null)
			{
				QueryFilter result;
				if (!(name == "LessThan"))
				{
					if (!(name == "GreaterThan"))
					{
						goto IL_16F;
					}
					result = new ComparisonFilter(ComparisonOperator.GreaterThan, ItemSchema.ReceivedTime, dateTime);
				}
				else
				{
					result = new ComparisonFilter(ComparisonOperator.LessThan, ItemSchema.ReceivedTime, dateTime);
				}
				return result;
			}
			IL_16F:
			throw new InvalidOperationException("Unexpected operator in Search request: " + parentNode.Name);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00041EA4 File Offset: 0x000400A4
		private void Clear()
		{
			this.schemaCache = null;
			this.airSyncClasses = null;
			this.folderScope = null;
			this.contentIndexingEnabled = false;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00041EC4 File Offset: 0x000400C4
		private QueryFilter ParseFreeText(XmlNode node)
		{
			SearchFilterGenerator searchFilterGenerator = new SearchFilterGenerator();
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (string text in this.airSyncClasses)
			{
				string a;
				if ((a = text) != null)
				{
					if (!(a == "Tasks"))
					{
						if (!(a == "Email") && !(a == "SMS"))
						{
							if (!(a == "Calendar"))
							{
								if (!(a == "Contacts"))
								{
									if (a == "Notes")
									{
										dictionary["IPF.StickyNote"] = false;
									}
								}
								else
								{
									dictionary["IPF.Contact"] = false;
								}
							}
							else
							{
								dictionary["IPF.Appointment"] = false;
							}
						}
						else
						{
							dictionary["IPF.Note"] = false;
						}
					}
					else
					{
						dictionary["IPF.Task"] = false;
					}
				}
			}
			return searchFilterGenerator.Execute(node.InnerText, this.contentIndexingEnabled, dictionary, SearchScope.AllFoldersAndItems, this.cultureInfo);
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00041FE4 File Offset: 0x000401E4
		private QueryFilter ParseConversationId(XmlNode node)
		{
			if (this.Conversation != null)
			{
				throw new SearchFilterTooComplexException
				{
					ErrorStringForProtocolLogger = "SearchTooComplexError4"
				};
			}
			AirSyncByteArrayProperty airSyncByteArrayProperty = new AirSyncByteArrayProperty("Search:", "ConversationId", false);
			airSyncByteArrayProperty.Bind(node);
			byte[] byteArrayData = airSyncByteArrayProperty.ByteArrayData;
			if (byteArrayData == null)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ProtocolVersionMismatch, false)
				{
					ErrorStringForProtocolLogger = "BadConversationIdInSearch"
				};
			}
			ConversationId conversationId;
			try
			{
				conversationId = ConversationId.Create(byteArrayData);
			}
			catch (CorruptDataException innerException)
			{
				throw new AirSyncPermanentException(StatusCode.Sync_ClientServerConversion, innerException, false)
				{
					ErrorStringForProtocolLogger = "BadConversationIdInSearch2"
				};
			}
			Conversation conversation;
			Command.CurrentCommand.GetOrCreateConversation(conversationId, false, out conversation);
			this.Conversation = conversation;
			return new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.ConversationTopic, this.Conversation.Topic);
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000420AC File Offset: 0x000402AC
		private QueryFilter ParseContains(XmlNode parentNode)
		{
			if (parentNode.ChildNodes.Count != 2)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError7"
				};
			}
			XmlNode xmlNode = parentNode.ChildNodes[0];
			XmlNode xmlNode2 = parentNode.ChildNodes[1];
			if ("Search:" != xmlNode2.NamespaceURI || "Value" != xmlNode2.Name)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError8"
				};
			}
			string innerText = xmlNode2.InnerText;
			string name;
			if ((name = xmlNode.Name) != null)
			{
				QueryFilter result;
				if (!(name == "From"))
				{
					if (!(name == "CategoryId"))
					{
						goto IL_FE;
					}
					int num;
					if (!int.TryParse(innerText, NumberStyles.None, CultureInfo.InvariantCulture, out num) || num % 2 != 1)
					{
						throw new SearchProtocolErrorException
						{
							ErrorStringForProtocolLogger = "SearchProtocolError11"
						};
					}
					string text = AirSyncUtility.ConvertSytemCategoryIdToKeywordsFormat(num);
					result = new TextFilter(ItemSchema.Categories, text, MatchOptions.SubString, MatchFlags.Default);
				}
				else
				{
					result = new TextFilter(ItemSchema.SentRepresentingEmailAddress, innerText, MatchOptions.SubString, MatchFlags.Loose);
				}
				return result;
			}
			IL_FE:
			throw new SearchProtocolErrorException
			{
				ErrorStringForProtocolLogger = "BadNode(" + xmlNode.Name + ")InSearch"
			};
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000421DF File Offset: 0x000403DF
		private QueryFilter ParseDoesNotContain(XmlNode parentNode)
		{
			return new NotFilter(this.ParseContains(parentNode));
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000421F0 File Offset: 0x000403F0
		private QueryFilter ParseOrOp(XmlNode parentNode)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (object obj in parentNode.ChildNodes)
			{
				XmlNode node = (XmlNode)obj;
				QueryFilter item = this.ParseSearchNamespace(node);
				list.Add(item);
			}
			return MailboxSearchCriteriaBuilder.ConstructAndOrOperator(list, "Or");
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00042268 File Offset: 0x00040468
		private QueryFilter ParseSearchNamespace(XmlNode node)
		{
			if ("Search:" != node.NamespaceURI)
			{
				throw new SearchProtocolErrorException
				{
					ErrorStringForProtocolLogger = "SearchProtocolError11"
				};
			}
			string name;
			switch (name = node.Name)
			{
			case "FreeText":
				return this.ParseFreeText(node);
			case "LessThan":
			case "GreaterThan":
				return MailboxSearchCriteriaBuilder.ParseLtGtOperator(node);
			case "ConversationId":
				return this.ParseConversationId(node);
			case "Contains":
				return this.ParseContains(node);
			case "DoesNotContain":
				return this.ParseDoesNotContain(node);
			case "Or":
				return this.ParseOrOp(node);
			}
			throw new SearchProtocolErrorException
			{
				ErrorStringForProtocolLogger = "BadNode(" + node.Name + ")InSearch"
			};
		}

		// Token: 0x04000795 RID: 1941
		private const string BadConversationIdInSearch = "BadConversationIdInSearch";

		// Token: 0x04000796 RID: 1942
		private const string BadConversationIdInSearch2 = "BadConversationIdInSearch2";

		// Token: 0x04000797 RID: 1943
		private const string SearchProtocolError1 = "SearchProtocolError1";

		// Token: 0x04000798 RID: 1944
		private const string SearchProtocolError2 = "SearchProtocolError2";

		// Token: 0x04000799 RID: 1945
		private const string SearchProtocolError3 = "SearchProtocolError3";

		// Token: 0x0400079A RID: 1946
		private const string SearchProtocolError4 = "SearchProtocolError4";

		// Token: 0x0400079B RID: 1947
		private const string SearchProtocolError5 = "SearchProtocolError5";

		// Token: 0x0400079C RID: 1948
		private const string SearchProtocolError6 = "SearchProtocolError6";

		// Token: 0x0400079D RID: 1949
		private const string SearchProtocolError7 = "SearchProtocolError7";

		// Token: 0x0400079E RID: 1950
		private const string SearchProtocolError8 = "SearchProtocolError8";

		// Token: 0x0400079F RID: 1951
		private const string SearchProtocolError9 = "SearchProtocolError9";

		// Token: 0x040007A0 RID: 1952
		private const string SearchProtocolError10 = "SearchProtocolError10";

		// Token: 0x040007A1 RID: 1953
		private const string SearchProtocolError11 = "SearchProtocolError11";

		// Token: 0x040007A2 RID: 1954
		private const string SearchProtocolError12 = "SearchProtocolError12";

		// Token: 0x040007A3 RID: 1955
		private const string SearchTooComplexError1 = "SearchTooComplexError1";

		// Token: 0x040007A4 RID: 1956
		private const string SearchTooComplexError3 = "SearchTooComplexError3";

		// Token: 0x040007A5 RID: 1957
		private const string SearchTooComplexError4 = "SearchTooComplexError4";

		// Token: 0x040007A6 RID: 1958
		private List<string> airSyncClasses;

		// Token: 0x040007A7 RID: 1959
		private bool contentIndexingEnabled;

		// Token: 0x040007A8 RID: 1960
		private List<string> folderScope;

		// Token: 0x040007A9 RID: 1961
		private Dictionary<string, MailboxSearchCriteriaBuilder.SchemaCacheItem> schemaCache;

		// Token: 0x040007AA RID: 1962
		private CultureInfo cultureInfo;

		// Token: 0x020000D8 RID: 216
		internal class SchemaCacheItem
		{
			// Token: 0x06000C78 RID: 3192 RVA: 0x000423A0 File Offset: 0x000405A0
			public SchemaCacheItem(AirSyncSchemaState schemaState, IAirSyncMissingPropertyStrategy missingPropertyStrategy)
			{
				this.schemaState = schemaState;
				AirSyncEntitySchemaState airSyncEntitySchemaState = this.schemaState as AirSyncEntitySchemaState;
				AirSyncXsoSchemaState airSyncXsoSchemaState = this.schemaState as AirSyncXsoSchemaState;
				if (airSyncEntitySchemaState != null)
				{
					this.mailboxDataObject = airSyncEntitySchemaState.GetEntityDataObject();
				}
				else
				{
					if (airSyncXsoSchemaState == null)
					{
						throw new ApplicationException(string.Format("SchemaState of type {0} is not supported", schemaState.GetType().FullName));
					}
					this.mailboxDataObject = airSyncXsoSchemaState.GetXsoDataObject();
				}
				this.airSyncDataObject = this.schemaState.GetAirSyncDataObject(new Hashtable(), missingPropertyStrategy);
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00042426 File Offset: 0x00040626
			public AirSyncDataObject AirSyncDataObject
			{
				get
				{
					return this.airSyncDataObject;
				}
			}

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0004242E File Offset: 0x0004062E
			public IServerDataObject MailboxDataObject
			{
				get
				{
					return this.mailboxDataObject;
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00042436 File Offset: 0x00040636
			public AirSyncSchemaState SchemaState
			{
				get
				{
					return this.schemaState;
				}
			}

			// Token: 0x040007AD RID: 1965
			private AirSyncDataObject airSyncDataObject;

			// Token: 0x040007AE RID: 1966
			private IServerDataObject mailboxDataObject;

			// Token: 0x040007AF RID: 1967
			private AirSyncSchemaState schemaState;
		}
	}
}
