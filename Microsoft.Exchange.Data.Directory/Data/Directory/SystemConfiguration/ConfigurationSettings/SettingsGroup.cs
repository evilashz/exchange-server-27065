using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings
{
	// Token: 0x0200067A RID: 1658
	[Serializable]
	public sealed class SettingsGroup : XMLSerializableDictionary<string, string>
	{
		// Token: 0x06004D59 RID: 19801 RVA: 0x0011D714 File Offset: 0x0011B914
		public SettingsGroup()
		{
			this.Scopes = new List<SettingsScope>();
		}

		// Token: 0x06004D5A RID: 19802 RVA: 0x0011D728 File Offset: 0x0011B928
		public SettingsGroup(string name, SettingsScope scope)
		{
			this.Name = name;
			this.Scopes = new List<SettingsScope>(1)
			{
				scope
			};
			this.LastModified = DateTime.UtcNow;
			this.Priority = scope.DefaultPriority;
		}

		// Token: 0x06004D5B RID: 19803 RVA: 0x0011D76E File Offset: 0x0011B96E
		public SettingsGroup(string name, string scopeFilter, int priority)
		{
			this.Name = name;
			this.LastModified = DateTime.UtcNow;
			this.Priority = priority;
			this.ScopeFilter = scopeFilter;
			this.HasExplicitScopeFilter = true;
			this.Scopes = SettingsGroup.AlwaysFalseScope;
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x0011D7A8 File Offset: 0x0011B9A8
		// (set) Token: 0x06004D5D RID: 19805 RVA: 0x0011D7B0 File Offset: 0x0011B9B0
		[XmlAttribute(AttributeName = "HasSF")]
		public bool HasExplicitScopeFilter { get; set; }

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06004D5E RID: 19806 RVA: 0x0011D7BC File Offset: 0x0011B9BC
		// (set) Token: 0x06004D5F RID: 19807 RVA: 0x0011D7EA File Offset: 0x0011B9EA
		[XmlElement(ElementName = "SF")]
		public string ScopeFilter
		{
			get
			{
				if (!this.HasExplicitScopeFilter)
				{
					Exception ex;
					QueryFilter queryFilter = this.TryParseFilter(null, null, out ex);
					return queryFilter.GenerateInfixString(FilterLanguage.Monad);
				}
				return this.scopeFilter;
			}
			set
			{
				this.scopeFilter = value;
				this.parsedFilter = null;
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06004D60 RID: 19808 RVA: 0x0011D7FA File Offset: 0x0011B9FA
		// (set) Token: 0x06004D61 RID: 19809 RVA: 0x0011D802 File Offset: 0x0011BA02
		[XmlAttribute(AttributeName = "ED")]
		public DateTime ExpirationDate { get; set; }

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06004D62 RID: 19810 RVA: 0x0011D80B File Offset: 0x0011BA0B
		// (set) Token: 0x06004D63 RID: 19811 RVA: 0x0011D813 File Offset: 0x0011BA13
		[XmlAttribute(AttributeName = "Nm")]
		public string Name { get; set; }

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06004D64 RID: 19812 RVA: 0x0011D81C File Offset: 0x0011BA1C
		// (set) Token: 0x06004D65 RID: 19813 RVA: 0x0011D824 File Offset: 0x0011BA24
		[XmlAttribute(AttributeName = "Pr")]
		public int Priority { get; set; }

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06004D66 RID: 19814 RVA: 0x0011D82D File Offset: 0x0011BA2D
		// (set) Token: 0x06004D67 RID: 19815 RVA: 0x0011D835 File Offset: 0x0011BA35
		[XmlAttribute(AttributeName = "LM")]
		public DateTime LastModified { get; set; }

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06004D68 RID: 19816 RVA: 0x0011D83E File Offset: 0x0011BA3E
		// (set) Token: 0x06004D69 RID: 19817 RVA: 0x0011D846 File Offset: 0x0011BA46
		[XmlAttribute(AttributeName = "En")]
		public bool Enabled { get; set; }

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x0011D84F File Offset: 0x0011BA4F
		// (set) Token: 0x06004D6B RID: 19819 RVA: 0x0011D857 File Offset: 0x0011BA57
		[XmlAttribute(AttributeName = "UB")]
		public string UpdatedBy { get; set; }

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x0011D860 File Offset: 0x0011BA60
		// (set) Token: 0x06004D6D RID: 19821 RVA: 0x0011D868 File Offset: 0x0011BA68
		[XmlAttribute(AttributeName = "Rs")]
		public string Reason { get; set; }

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x0011D871 File Offset: 0x0011BA71
		// (set) Token: 0x06004D6F RID: 19823 RVA: 0x0011D879 File Offset: 0x0011BA79
		[XmlArrayItem("OSc", typeof(SettingsOrganizationScope))]
		[XmlArrayItem("ASc", typeof(SettingsDagScope))]
		[XmlArrayItem("DSc", typeof(SettingsDatabaseScope))]
		[XmlArrayItem("FSc", typeof(SettingsForestScope))]
		[XmlArrayItem("GSc", typeof(SettingsGenericScope))]
		[XmlArrayItem("PSc", typeof(SettingsProcessScope))]
		[XmlArrayItem("SSc", typeof(SettingsServerScope))]
		[XmlArrayItem("USc", typeof(SettingsUserScope))]
		public List<SettingsScope> Scopes { get; set; }

		// Token: 0x06004D70 RID: 19824 RVA: 0x0011D884 File Offset: 0x0011BA84
		public static implicit operator SettingsGroup(string xml)
		{
			return XMLSerializableBase.Deserialize<SettingsGroup>(xml, InternalExchangeSettingsSchema.ConfigurationXMLRaw);
		}

		// Token: 0x06004D71 RID: 19825 RVA: 0x0011D8A0 File Offset: 0x0011BAA0
		public SettingsGroup Clone()
		{
			SettingsGroup result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsGroup));
				xmlSerializer.Serialize(memoryStream, this);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = (SettingsGroup)xmlSerializer.Deserialize(memoryStream);
			}
			return result;
		}

		// Token: 0x06004D72 RID: 19826 RVA: 0x0011D900 File Offset: 0x0011BB00
		public override string ToString()
		{
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("[{0}]", this.Name));
			if (!this.Enabled)
			{
				stringBuilder.AppendLine("; ============================================");
				stringBuilder.AppendLine("; WARNING -- this SettingsGroup is disabled!!!");
				stringBuilder.AppendLine("; ============================================");
				flag = false;
			}
			else if (this.ExpirationDate != DateTime.MinValue && DateTime.UtcNow > this.ExpirationDate)
			{
				stringBuilder.AppendLine("; ============================================");
				stringBuilder.AppendLine("; WARNING -- this SettingsGroup has expired!!!");
				stringBuilder.AppendLine("; ============================================");
				flag = false;
			}
			stringBuilder.AppendLine(string.Format("; Priority: {0}; Expiration: {1}; LastModified: {2}; ModifiedBy: {3}", new object[]
			{
				this.Priority,
				(this.ExpirationDate == DateTime.MinValue) ? "(never)" : this.ExpirationDate.ToString(),
				this.LastModified,
				this.UpdatedBy
			}));
			stringBuilder.AppendLine(string.Format("; Scope: {0}", this.ScopeFilter));
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				stringBuilder.AppendLine(string.Format("{0}{1}={2}", flag ? "" : "; ", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x0011DAB8 File Offset: 0x0011BCB8
		internal bool Matches(IConfigSchema schema, ISettingsContext context)
		{
			if (!this.Enabled || (this.ExpirationDate != DateTime.MinValue && DateTime.UtcNow > this.ExpirationDate))
			{
				return false;
			}
			Exception ex;
			QueryFilter filter = this.TryParseFilter(schema, null, out ex);
			return OpathFilterEvaluator.FilterMatches(filter, null, (PropertyDefinition pdef) => ((SettingsScopeFilterSchema.ScopeFilterPropertyDefinition)pdef).RetrieveValue(context));
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x0011DB2C File Offset: 0x0011BD2C
		internal void Validate(IConfigSchema schema, QueryParser.EvaluateVariableDelegate evalDelegate)
		{
			if (this.Priority <= -1)
			{
				throw new ConfigurationSettingsException(DirectoryStrings.ConfigurationSettingsInvalidPriority(this.Priority));
			}
			if (!this.HasExplicitScopeFilter)
			{
				if (this.Scopes.Count <= 0)
				{
					throw new ConfigurationSettingsRestrictionExpectedException("");
				}
				HashSet<Type> hashSet = new HashSet<Type>();
				foreach (SettingsScope settingsScope in this.Scopes)
				{
					if (hashSet.Contains(settingsScope.GetType()))
					{
						throw new ConfigurationSettingsDuplicateRestrictionException(settingsScope.GetType().Name.ToString(), this.Name);
					}
					hashSet.Add(settingsScope.GetType());
					settingsScope.Validate(schema);
				}
				if (hashSet.Contains(typeof(SettingsForestScope)) && hashSet.Count > 1)
				{
					this.Scopes.RemoveAll((SettingsScope x) => x is SettingsForestScope);
					return;
				}
			}
			else
			{
				Exception ex;
				this.TryParseFilter(schema, evalDelegate, out ex);
				if (ex != null)
				{
					throw ex;
				}
			}
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0011DC68 File Offset: 0x0011BE68
		private QueryFilter TryParseFilter(IConfigSchema schema, QueryParser.EvaluateVariableDelegate evalDelegate, out Exception ex)
		{
			ex = null;
			if (this.parsedFilter == null)
			{
				if (!this.HasExplicitScopeFilter)
				{
					if (this.Scopes == null || this.Scopes.Count == 0)
					{
						this.parsedFilter = QueryFilter.True;
					}
					else
					{
						List<QueryFilter> list = new List<QueryFilter>(1);
						foreach (SettingsScope settingsScope in this.Scopes)
						{
							list.Add(settingsScope.ConstructScopeFilter(schema));
						}
						this.parsedFilter = (QueryFilter.AndTogether(list.ToArray()) ?? QueryFilter.False);
					}
				}
				else if (string.IsNullOrWhiteSpace(this.ScopeFilter))
				{
					this.parsedFilter = QueryFilter.True;
					this.scopeFilter = null;
				}
				else
				{
					try
					{
						SettingsScopeFilterSchema scopeFilterSchema = SettingsScopeFilterSchema.GetSchemaInstance(schema);
						QueryParser queryParser = new QueryParser(this.ScopeFilter, QueryParser.Capabilities.All, new QueryParser.LookupPropertyDelegate(scopeFilterSchema.LookupSchemaProperty), () => scopeFilterSchema.AllFilterableProperties, evalDelegate, new QueryParser.ConvertValueFromStringDelegate(MailboxProvisioningConstraint.ConvertValueFromString));
						this.parsedFilter = queryParser.ParseTree;
					}
					catch (InvalidCastException ex2)
					{
						ex = ex2;
					}
					catch (ParsingException ex3)
					{
						ex = ex3;
					}
					catch (ArgumentOutOfRangeException ex4)
					{
						ex = ex4;
					}
					if (ex != null)
					{
						ex = new ConfigurationSettingsInvalidScopeFilter(ex.Message, ex);
						this.parsedFilter = QueryFilter.False;
					}
					else
					{
						this.scopeFilter = this.parsedFilter.GenerateInfixString(FilterLanguage.Monad);
					}
				}
			}
			return this.parsedFilter;
		}

		// Token: 0x040034A2 RID: 13474
		internal const int InvalidPriority = -1;

		// Token: 0x040034A3 RID: 13475
		private static readonly List<SettingsScope> AlwaysFalseScope = new List<SettingsScope>(1)
		{
			new SettingsProcessScope("This settings group has a ScopeFilter, it cannot be processed by this downlevel server.")
		};

		// Token: 0x040034A4 RID: 13476
		private string scopeFilter;

		// Token: 0x040034A5 RID: 13477
		private QueryFilter parsedFilter;
	}
}
