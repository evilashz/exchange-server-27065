using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.ApplicationLogic.Diagnostics
{
	// Token: 0x020000CF RID: 207
	internal abstract class BaseConditionalRegistration
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00022C0E File Offset: 0x00020E0E
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00022C15 File Offset: 0x00020E15
		public static bool Initialized { get; private set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00022C1D File Offset: 0x00020E1D
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00022C24 File Offset: 0x00020E24
		public static Dictionary<string, string> PropertyGroups { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00022C2C File Offset: 0x00020E2C
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00022C33 File Offset: 0x00020E33
		public static Func<int> GetHitCountForCookie { get; set; }

		// Token: 0x060008BF RID: 2239 RVA: 0x00022C3C File Offset: 0x00020E3C
		public static void Initialize(string protocolName, ObjectSchema fetchSchema, ObjectSchema querySchema, Dictionary<string, string> customPropertyGroups = null)
		{
			if (BaseConditionalRegistration.Initialized)
			{
				return;
			}
			BaseConditionalRegistration.FetchSchema = fetchSchema;
			BaseConditionalRegistration.QuerySchema = querySchema;
			ConditionalRegistrationLog.ProtocolName = protocolName;
			if (BaseConditionalRegistration.PropertyGroups == null)
			{
				BaseConditionalRegistration.PropertyGroups = new Dictionary<string, string>();
			}
			BaseConditionalRegistration.PropertyGroups.Add("user", BaseConditionalRegistration.GetConfigurationValue("UserPropertyGroup") ?? "SmtpAddress,DisplayName,TenantName,WindowsLiveId,MailboxServer,MailboxDatabase,MailboxServerVersion,IsMonitoringUser");
			BaseConditionalRegistration.PropertyGroups.Add("wlm", BaseConditionalRegistration.GetConfigurationValue("WlmPropertyGroup") ?? "IsOverBudgetAtStart,IsOverBudgetAtEnd,BudgetBalanceStart,BudgetBalanceEnd,BudgetDelay,BudgetUsed,BudgetLockedOut,BudgetLockedUntil");
			BaseConditionalRegistration.PropertyGroups.Add("policy", BaseConditionalRegistration.GetConfigurationValue("PolicyPropertyGroup") ?? "ThrottlingPolicyName,MaxConcurrency,MaxBurst,RechargeRate,CutoffBalance,ThrottlingPolicyScope,ConcurrencyStart,ConcurrencyEnd");
			BaseConditionalRegistration.PropertyGroups.Add("error", BaseConditionalRegistration.GetConfigurationValue("ErrorPropertyGroup") ?? "Exception");
			if (customPropertyGroups != null)
			{
				foreach (string key in customPropertyGroups.Keys)
				{
					if (!BaseConditionalRegistration.PropertyGroups.ContainsKey(key))
					{
						BaseConditionalRegistration.PropertyGroups.Add(key, customPropertyGroups[key]);
					}
				}
			}
			BaseConditionalRegistration.Initialized = true;
			RegisterConditionHandler.GetInstance().HydratePersistentHandlers();
			RegisterConditionHandler.GetInstance().HydrateNonPersistentRegistrations();
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00022D78 File Offset: 0x00020F78
		protected static void ParseArgument(string argument, out string selectClause, out string whereClause)
		{
			string text = argument.Trim();
			string text2 = text.ToLower();
			int num = text2.IndexOf("select ");
			int num2 = text2.IndexOf(" where ");
			int num3 = text2.IndexOf(" options ");
			if (num != 0)
			{
				throw new ArgumentException("Conditional must start with 'select'.");
			}
			if (num2 <= "select ".Length)
			{
				throw new ArgumentException("Conditional must contains a 'where' clause which should come after 'select'.");
			}
			selectClause = text.Substring("select ".Length, num2 - "select ".Length);
			whereClause = text.Substring(num2 + " where ".Length, ((num3 == -1) ? text.Length : num3) - (num2 + " where ".Length));
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00022E2C File Offset: 0x0002102C
		protected static string GetRightHand(string expression)
		{
			int num = expression.IndexOf('=');
			if (num == -1)
			{
				throw new ArgumentException("Expression should have an equal sign.  Expression: " + expression);
			}
			return expression.Substring(num + 1).Trim();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00022E68 File Offset: 0x00021068
		protected BaseConditionalRegistration(string cookie, string user, string propertiesToFetch, string whereClause)
		{
			this.User = user;
			this.OriginalFilter = whereClause;
			this.OriginalPropertiesToFetch = propertiesToFetch;
			this.PropertiesToFetch = BaseConditionalRegistration.ParsePropertiesToFetch(propertiesToFetch);
			this.QueryFilter = BaseConditionalRegistration.ParseWhereClause(whereClause);
			this.Created = (ExDateTime)TimeProvider.UtcNow;
			this.Cookie = cookie;
			if (BaseConditionalRegistration.GetHitCountForCookie == null)
			{
				ConditionalRegistrationLog.ConditionalRegistrationHitMetadata hitsForCookie = ConditionalRegistrationLog.GetHitsForCookie(user, cookie);
				if (hitsForCookie != null)
				{
					this.hits = hitsForCookie.HitFiles.Length;
					return;
				}
			}
			else
			{
				this.hits = BaseConditionalRegistration.GetHitCountForCookie();
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00022EF2 File Offset: 0x000210F2
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00022EF9 File Offset: 0x000210F9
		public static ObjectSchema FetchSchema { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00022F01 File Offset: 0x00021101
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00022F08 File Offset: 0x00021108
		public static ObjectSchema QuerySchema { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00022F10 File Offset: 0x00021110
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00022F18 File Offset: 0x00021118
		public Action<BaseConditionalRegistration, RemoveReason> OnExpired { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00022F21 File Offset: 0x00021121
		public int CurrentHits
		{
			get
			{
				return this.hits;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00022F29 File Offset: 0x00021129
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00022F31 File Offset: 0x00021131
		public DateTime LastHitUtc { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00022F3A File Offset: 0x0002113A
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x00022F42 File Offset: 0x00021142
		public string User { get; private set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00022F4B File Offset: 0x0002114B
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x00022F53 File Offset: 0x00021153
		public string OriginalFilter { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00022F5C File Offset: 0x0002115C
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00022F64 File Offset: 0x00021164
		public string OriginalPropertiesToFetch { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00022F6D File Offset: 0x0002116D
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00022F75 File Offset: 0x00021175
		public ExDateTime Created { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00022F7E File Offset: 0x0002117E
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00022F86 File Offset: 0x00021186
		public string Cookie { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00022F8F File Offset: 0x0002118F
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00022F97 File Offset: 0x00021197
		public PropertyDefinition[] PropertiesToFetch { get; private set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00022FA0 File Offset: 0x000211A0
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00022FA8 File Offset: 0x000211A8
		public QueryFilter QueryFilter { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060008DA RID: 2266
		// (set) Token: 0x060008DB RID: 2267
		public abstract string Description { get; protected set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00022FB1 File Offset: 0x000211B1
		public virtual bool ShouldEvaluate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00022FB4 File Offset: 0x000211B4
		public ConditionalResults Evaluate(IReadOnlyPropertyBag propertyBag)
		{
			if (this.ShouldEvaluate && OpathFilterEvaluator.FilterMatches(this.QueryFilter, propertyBag))
			{
				Interlocked.Increment(ref this.hits);
				this.LastHitUtc = TimeProvider.UtcNow;
				return this.FetchData(propertyBag);
			}
			return null;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00022FEC File Offset: 0x000211EC
		private ConditionalResults FetchData(IReadOnlyPropertyBag propertyBag)
		{
			Dictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
			foreach (PropertyDefinition propertyDefinition in this.PropertiesToFetch)
			{
				object obj = propertyBag[propertyDefinition];
				if (obj != null)
				{
					dictionary[propertyDefinition] = obj;
				}
			}
			return new ConditionalResults(this, RegistrationResult.Success, dictionary);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00023038 File Offset: 0x00021238
		internal static PropertyDefinition[] ParsePropertiesToFetch(string propertiesToFetch)
		{
			if (string.IsNullOrEmpty(propertiesToFetch))
			{
				throw new ArgumentException("propertiesToFetch cannot be null or empty.");
			}
			string[] array = BaseConditionalRegistration.ExpandPropertyGroups(propertiesToFetch);
			if (array.Length > 100)
			{
				throw new ArgumentException(string.Format("A maximum of {0} properties can be requested", 100));
			}
			PropertyDefinition[] array2 = new PropertyDefinition[array.Length];
			int num = 0;
			foreach (string text in array)
			{
				PropertyDefinition propertyDefinition = BaseConditionalRegistration.FetchSchema[text.Trim()];
				if (propertyDefinition == null)
				{
					throw new ArgumentException("Undefined property in properties to fetch: " + text);
				}
				array2[num++] = propertyDefinition;
			}
			return array2;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000230D8 File Offset: 0x000212D8
		internal static QueryFilter ParseWhereClause(string whereClause)
		{
			QueryParser queryParser = new QueryParser(whereClause, BaseConditionalRegistration.QuerySchema, QueryParser.Capabilities.All, null, new QueryParser.ConvertValueFromStringDelegate(QueryParserUtils.ConvertValueFromString));
			return queryParser.ParseTree;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00023109 File Offset: 0x00021309
		internal static string GetConfigurationValue(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00023118 File Offset: 0x00021318
		private static string[] ExpandPropertyGroups(string properties)
		{
			List<string> list = new List<string>();
			foreach (string text in properties.Split(new char[]
			{
				','
			}))
			{
				if (BaseConditionalRegistration.IsPropertyGroup(text))
				{
					list.AddRange(BaseConditionalRegistration.GetPropertiesForPropertyGroup(text.ToLower().Trim(new char[]
					{
						'[',
						']'
					})).Split(new char[]
					{
						','
					}));
				}
				else
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000231AE File Offset: 0x000213AE
		private static bool IsPropertyGroup(string property)
		{
			return property.StartsWith("[") && property.EndsWith("]");
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000231CD File Offset: 0x000213CD
		private static string GetPropertiesForPropertyGroup(string propertyGroup)
		{
			if (BaseConditionalRegistration.PropertyGroups.ContainsKey(propertyGroup))
			{
				return BaseConditionalRegistration.PropertyGroups[propertyGroup];
			}
			return propertyGroup;
		}

		// Token: 0x040003F3 RID: 1011
		private const string SelectKeywordWithSpaces = "select ";

		// Token: 0x040003F4 RID: 1012
		private const string WhereKeywordWithSpaces = " where ";

		// Token: 0x040003F5 RID: 1013
		protected const string OptionsKeywordWithSpaces = " options ";

		// Token: 0x040003F6 RID: 1014
		public const string UserPropertyGroup = "SmtpAddress,DisplayName,TenantName,WindowsLiveId,MailboxServer,MailboxDatabase,MailboxServerVersion,IsMonitoringUser";

		// Token: 0x040003F7 RID: 1015
		public const string WlmPropertyGroup = "IsOverBudgetAtStart,IsOverBudgetAtEnd,BudgetBalanceStart,BudgetBalanceEnd,BudgetDelay,BudgetUsed,BudgetLockedOut,BudgetLockedUntil";

		// Token: 0x040003F8 RID: 1016
		public const string PolicyPropertyGroup = "ThrottlingPolicyName,MaxConcurrency,MaxBurst,RechargeRate,CutoffBalance,ThrottlingPolicyScope,ConcurrencyStart,ConcurrencyEnd";

		// Token: 0x040003F9 RID: 1017
		private const string ErrorPropertyGroup = "Exception";

		// Token: 0x040003FA RID: 1018
		private const int MaxPropertiesToFetch = 100;

		// Token: 0x040003FB RID: 1019
		private int hits;
	}
}
