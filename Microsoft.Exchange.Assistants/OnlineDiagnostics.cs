using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x020000A3 RID: 163
	internal class OnlineDiagnostics : ExchangeDiagnosableWrapper<QueryResponse>
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x000196B1 File Offset: 0x000178B1
		public static OnlineDiagnostics Instance
		{
			get
			{
				return OnlineDiagnostics.instance;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000196B8 File Offset: 0x000178B8
		protected override string ComponentName
		{
			get
			{
				return "EBA";
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000196C0 File Offset: 0x000178C0
		private OnlineDiagnostics()
		{
			this.dispatcher = new Dictionary<string, QueryTemplate>(StringComparer.OrdinalIgnoreCase);
			this.dispatcher.Add("DatabaseManager", new QueryTemplate(new Func<object[], QueryFilter, List<QueryableObject>>(this.QueryDatabaseManager), new SimpleProviderPropertyDefinition[0]));
			this.dispatcher.Add("AssistantType", new QueryTemplate(new Func<object[], QueryFilter, List<QueryableObject>>(this.QueryAssistantType), new SimpleProviderPropertyDefinition[0]));
			this.dispatcher.Add("OnlineDatabase", new QueryTemplate(new Func<object[], QueryFilter, List<QueryableObject>>(this.QueryOnlineDatabase), new SimpleProviderPropertyDefinition[]
			{
				QueryableObjectSchema.DatabaseGuid
			}));
			this.dispatcher.Add("EventController", new QueryTemplate(new Func<object[], QueryFilter, List<QueryableObject>>(this.QueryEventController), new SimpleProviderPropertyDefinition[]
			{
				QueryableObjectSchema.DatabaseGuid
			}));
			this.dispatcher.Add("MailboxDispatcher", new QueryTemplate(new Func<object[], QueryFilter, List<QueryableObject>>(this.QueryMailboxDispatcher), new SimpleProviderPropertyDefinition[]
			{
				QueryableObjectSchema.DatabaseGuid,
				QueryableObjectSchema.MailboxGuid
			}));
			this.dispatcher.Add("EventDispatcher", new QueryTemplate(new Func<object[], QueryFilter, List<QueryableObject>>(this.QueryEventDispatcher), new SimpleProviderPropertyDefinition[]
			{
				QueryableObjectSchema.DatabaseGuid,
				QueryableObjectSchema.MailboxGuid,
				QueryableObjectSchema.AssistantGuid
			}));
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001980D File Offset: 0x00017A0D
		public void RegisterDatabaseManager(DatabaseManager databaseManager)
		{
			if (!this.registered && databaseManager != null)
			{
				this.databaseManager = databaseManager;
				ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents(this);
				this.registered = true;
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001984C File Offset: 0x00017A4C
		internal override QueryResponse GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			QueryResponse result;
			try
			{
				if (this.databaseManager == null)
				{
					result = QueryResponse.CreateError("DatabaseManager is not start yet, or not registered for online diagnostics");
				}
				else if (string.IsNullOrEmpty(arguments.Argument))
				{
					StringBuilder sb = new StringBuilder();
					this.dispatcher.Keys.ToList<string>().ForEach(delegate(string x)
					{
						sb.AppendFormat(" {0}", x);
					});
					result = QueryResponse.CreateError("Please specify argument paramter, supported objectClass:" + sb.ToString());
				}
				else
				{
					QueryParser queryParser = new QueryParser(arguments.Argument, ObjectSchema.GetInstance<QueryableObjectSchema>(), QueryParser.Capabilities.All, null, new QueryParser.ConvertValueFromStringDelegate(QueryParserUtils.ConvertValueFromString));
					QueryFilter filter = queryParser.ParseTree;
					filter = QueryFilter.SimplifyFilter(filter);
					result = this.ExecuteQuery(filter);
				}
			}
			catch (ArgumentException ex)
			{
				result = QueryResponse.CreateError(ex.ToString());
			}
			return result;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0001994C File Offset: 0x00017B4C
		protected override string UsageSample
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				this.dispatcher.Keys.ToList<string>().ForEach(delegate(string x)
				{
					sb.AppendFormat("\r\n{0}", x);
				});
				return " Example 1: Get DatabaseManager\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeMailboxAssistants -Component EBA -Argument \"ObjectClass -eq 'DatabaseManager\";\r\n\r\n                        Example 2: Get a particular onlineDatabase\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeMailboxAssistants -Component EBA -Argument \"ObjectClass -eq 'OnlineDatabase' -and DatabaseGuid -eq 'ad55da35-71e1-477c-80f3-6353cc1dfb4e'\";\r\n\r\n                        Example 3: Get a particular EventDispatcher\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeMailboxAssistants -Component EBA -Argument \"ObjectClass -eq 'EventDispatcher' -and DatabaseGuid -eq 'ad55da35-71e1-477c-80f3-6353cc1dfb4e' -and MailboxGuid -eq '55548e2a-7867-4291-a539-cee04febfeff'-and AssistantGuid -eq '15166323-526d-4bbf-b9b0-4ef6226acc03'\";\r\n\r\n                        Supported ObjectClass:\r\n                        " + sb.ToString();
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000199C4 File Offset: 0x00017BC4
		private QueryResponse ExecuteQuery(QueryFilter filter)
		{
			string text = null;
			if (!this.ExtractIdentityPropertyFromFilter<string>(QueryableObjectSchema.ObjectClass, ref filter, out text))
			{
				return QueryResponse.CreateError("Unable to extract object class from filter");
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "DatabaseManager";
			}
			QueryTemplate queryTemplate = null;
			if (!this.dispatcher.TryGetValue(text, out queryTemplate) || queryTemplate == null)
			{
				StringBuilder sb = new StringBuilder();
				this.dispatcher.Keys.ToList<string>().ForEach(delegate(string x)
				{
					sb.AppendFormat(" {0}", x);
				});
				return QueryResponse.CreateError("Unsupported objectClass, please use one of the supported objectClass:" + sb.ToString());
			}
			List<object> list = null;
			if (queryTemplate.Parameters != null)
			{
				list = new List<object>(queryTemplate.Parameters.Length);
				for (int i = 0; i < queryTemplate.Parameters.Length; i++)
				{
					SimpleProviderPropertyDefinition simpleProviderPropertyDefinition = queryTemplate.Parameters[i];
					object item = null;
					if (!this.ExtractIdentityPropertyFromFilter<object>(simpleProviderPropertyDefinition, ref filter, out item))
					{
						if (i < queryTemplate.Parameters.Length - 1)
						{
							return QueryResponse.CreateError("Missing required property \"" + simpleProviderPropertyDefinition.Name + "\"");
						}
						item = null;
					}
					list.Add(item);
				}
			}
			List<QueryableObject> source = queryTemplate.Executor((list != null) ? list.ToArray() : null, filter);
			List<Dictionary<string, object>> obj = (from x in source
			select x.ToDictionary()).ToList<Dictionary<string, object>>();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return new QueryResponse(text, javaScriptSerializer.Serialize(obj));
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00019B3C File Offset: 0x00017D3C
		private List<QueryableObject> QueryDatabaseManager(object[] parameters, QueryFilter filter)
		{
			QueryableDatabaseManager queryableDatabaseManager = new QueryableDatabaseManager();
			OnlineDiagnostics.Instance.databaseManager.ExportToQueryableObject(queryableDatabaseManager);
			List<QueryableObject> list = new List<QueryableObject>(1);
			if (filter == null || OpathFilterEvaluator.FilterMatches(filter, queryableDatabaseManager))
			{
				list.Add(queryableDatabaseManager);
			}
			return list;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00019B7C File Offset: 0x00017D7C
		private List<QueryableObject> QueryAssistantType(object[] parameters, QueryFilter filter)
		{
			List<QueryableObject> list = new List<QueryableObject>(OnlineDiagnostics.Instance.databaseManager.AssistantTypes.Length);
			foreach (AssistantType assistantType in OnlineDiagnostics.Instance.databaseManager.AssistantTypes)
			{
				QueryableEventBasedAssistantType queryableEventBasedAssistantType = new QueryableEventBasedAssistantType();
				assistantType.ExportToQueryableObject(queryableEventBasedAssistantType);
				if (filter == null || OpathFilterEvaluator.FilterMatches(filter, queryableEventBasedAssistantType))
				{
					list.Add(queryableEventBasedAssistantType);
				}
			}
			return list;
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00019BE8 File Offset: 0x00017DE8
		private List<QueryableObject> QueryOnlineDatabase(object[] parameters, QueryFilter filter)
		{
			IList<OnlineDatabase> onlineDatabases = OnlineDiagnostics.Instance.databaseManager.GetOnlineDatabases((Guid?)parameters[0]);
			List<QueryableObject> list = new List<QueryableObject>(onlineDatabases.Count);
			foreach (OnlineDatabase onlineDatabase in onlineDatabases)
			{
				QueryableOnlineDatabase queryableOnlineDatabase = new QueryableOnlineDatabase();
				onlineDatabase.ExportToQueryableObject(queryableOnlineDatabase);
				if (filter == null || OpathFilterEvaluator.FilterMatches(filter, queryableOnlineDatabase))
				{
					list.Add(queryableOnlineDatabase);
				}
			}
			return list;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x00019C74 File Offset: 0x00017E74
		private List<QueryableObject> QueryMailboxDispatcher(object[] parameters, QueryFilter filter)
		{
			List<QueryableObject> list = new List<QueryableObject>();
			IList<OnlineDatabase> onlineDatabases = OnlineDiagnostics.Instance.databaseManager.GetOnlineDatabases((Guid?)parameters[0]);
			if (onlineDatabases == null || onlineDatabases.Count != 1)
			{
				throw new ArgumentException("Could not find the database specified by DatabaseGuid", "DatabaseGuid");
			}
			IList<MailboxDispatcher> mailboxDispatcher = ((EventControllerPrivate)onlineDatabases[0].EventController).GetMailboxDispatcher((Guid?)parameters[1]);
			foreach (MailboxDispatcher mailboxDispatcher2 in mailboxDispatcher)
			{
				QueryableMailboxDispatcher queryableMailboxDispatcher = new QueryableMailboxDispatcher();
				mailboxDispatcher2.ExportToQueryableObject(queryableMailboxDispatcher);
				if (filter == null || OpathFilterEvaluator.FilterMatches(filter, queryableMailboxDispatcher))
				{
					list.Add(queryableMailboxDispatcher);
				}
			}
			return list;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00019D38 File Offset: 0x00017F38
		private List<QueryableObject> QueryEventController(object[] parameters, QueryFilter filter)
		{
			IList<OnlineDatabase> onlineDatabases = OnlineDiagnostics.Instance.databaseManager.GetOnlineDatabases((Guid?)parameters[0]);
			List<QueryableObject> list = new List<QueryableObject>(onlineDatabases.Count);
			foreach (OnlineDatabase onlineDatabase in onlineDatabases)
			{
				if (onlineDatabase != null && onlineDatabase.EventController != null)
				{
					QueryableEventController queryableEventController = new QueryableEventController();
					onlineDatabase.EventController.ExportToQueryableObject(queryableEventController);
					if (filter == null || OpathFilterEvaluator.FilterMatches(filter, queryableEventController))
					{
						list.Add(queryableEventController);
					}
				}
			}
			return list;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00019DD4 File Offset: 0x00017FD4
		private List<QueryableObject> QueryEventDispatcher(object[] parameters, QueryFilter filter)
		{
			List<QueryableObject> list = new List<QueryableObject>(50);
			IList<OnlineDatabase> onlineDatabases = OnlineDiagnostics.Instance.databaseManager.GetOnlineDatabases((Guid?)parameters[0]);
			if (onlineDatabases == null || onlineDatabases.Count != 1)
			{
				throw new ArgumentException("Could not find the database specified by DatabaseGuid", "DatabaseGuid");
			}
			EventControllerPrivate eventControllerPrivate = (EventControllerPrivate)onlineDatabases[0].EventController;
			IList<MailboxDispatcher> mailboxDispatcher = eventControllerPrivate.GetMailboxDispatcher((Guid?)parameters[1]);
			if (mailboxDispatcher == null)
			{
				return list;
			}
			MailboxDispatcher mailboxDispatcher2 = mailboxDispatcher[0];
			foreach (EventDispatcherPrivate eventDispatcherPrivate in mailboxDispatcher2.GetEventDispatcher((Guid?)parameters[2]))
			{
				QueryableEventDispatcher queryableEventDispatcher = new QueryableEventDispatcher();
				eventDispatcherPrivate.ExportToQueryableObject(queryableEventDispatcher);
				if (filter == null || OpathFilterEvaluator.FilterMatches(filter, queryableEventDispatcher))
				{
					list.Add(queryableEventDispatcher);
				}
			}
			return list;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019EF4 File Offset: 0x000180F4
		private bool ExtractIdentityPropertyFromFilter<T>(SimpleProviderPropertyDefinition property, ref QueryFilter filter, out T propertyValue)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			AndFilter andFilter = filter as AndFilter;
			Func<QueryFilter, bool> func = delegate(QueryFilter f)
			{
				ComparisonFilter comparisonFilter2 = f as ComparisonFilter;
				return comparisonFilter2 != null && comparisonFilter2.Property == property && comparisonFilter2.ComparisonOperator == ComparisonOperator.Equal;
			};
			if (func(comparisonFilter))
			{
				propertyValue = (T)((object)comparisonFilter.PropertyValue);
				filter = null;
				return true;
			}
			if (andFilter != null)
			{
				comparisonFilter = (ComparisonFilter)andFilter.Filters.FirstOrDefault(func);
				if (comparisonFilter != null)
				{
					propertyValue = (T)((object)comparisonFilter.PropertyValue);
					filter = QueryFilter.AndTogether(andFilter.Filters.SkipWhile(func).ToArray<QueryFilter>());
					filter = QueryFilter.SimplifyFilter(filter);
					return true;
				}
			}
			propertyValue = default(T);
			return false;
		}

		// Token: 0x040002EE RID: 750
		private const string DiagnosticsComponentName = "EBA";

		// Token: 0x040002EF RID: 751
		private static OnlineDiagnostics instance = new OnlineDiagnostics();

		// Token: 0x040002F0 RID: 752
		private Dictionary<string, QueryTemplate> dispatcher;

		// Token: 0x040002F1 RID: 753
		private DatabaseManager databaseManager;

		// Token: 0x040002F2 RID: 754
		private bool registered;
	}
}
