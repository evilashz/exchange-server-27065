using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B5A RID: 2906
	[Cmdlet("Import", "TransportRuleCollection", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class ImportTransportRuleCollection : GetMultitenancySystemConfigurationObjectTask<RuleIdParameter, TransportRule>
	{
		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x06006963 RID: 26979 RVA: 0x001B2EDC File Offset: 0x001B10DC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageImportTransportRuleCollection;
			}
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x001B2EE3 File Offset: 0x001B10E3
		public ImportTransportRuleCollection()
		{
			this.ruleCollectionName = Utils.RuleCollectionNameFromRole();
		}

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x06006965 RID: 26981 RVA: 0x001B2EF6 File Offset: 0x001B10F6
		// (set) Token: 0x06006966 RID: 26982 RVA: 0x001B2F0D File Offset: 0x001B110D
		[Parameter(Mandatory = true, Position = 0)]
		public byte[] FileData
		{
			get
			{
				return (byte[])base.Fields["FileData"];
			}
			set
			{
				base.Fields["FileData"] = value;
			}
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x06006967 RID: 26983 RVA: 0x001B2F20 File Offset: 0x001B1120
		// (set) Token: 0x06006968 RID: 26984 RVA: 0x001B2F46 File Offset: 0x001B1146
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x06006969 RID: 26985 RVA: 0x001B2F5E File Offset: 0x001B115E
		// (set) Token: 0x0600696A RID: 26986 RVA: 0x001B2F75 File Offset: 0x001B1175
		[Parameter(Mandatory = false)]
		public MigrationSourceType MigrationSource
		{
			get
			{
				return (MigrationSourceType)base.Fields["MigrationSource"];
			}
			set
			{
				base.Fields["MigrationSource"] = value;
			}
		}

		// Token: 0x0600696B RID: 26987 RVA: 0x001B2F8D File Offset: 0x001B118D
		protected override void InternalValidate()
		{
			if (this.FileData == null)
			{
				base.WriteError(new ArgumentException(Strings.ImportFileDataIsNull), ErrorCategory.InvalidArgument, "FileData");
				return;
			}
			base.InternalValidate();
		}

		// Token: 0x0600696C RID: 26988 RVA: 0x001B2FBC File Offset: 0x001B11BC
		protected override void InternalProcessRecord()
		{
			if (!this.Force && !base.ShouldContinue(Strings.PromptToOverwriteRulesOnImport))
			{
				return;
			}
			Exception ex = null;
			using (MemoryStream memoryStream = new MemoryStream(this.FileData))
			{
				bool flag = false;
				if (!this.IsMigratedFopeRuleCollectionBeingImported())
				{
					ex = ImportTransportRuleCollection.TryParseVersion(memoryStream, out flag);
				}
				if (ex == null)
				{
					if (flag)
					{
						if (this.IsDatacenter())
						{
							base.WriteError(new ParseException(Strings.ImportE14TransportRuleCollectionInDCError), ErrorCategory.InvalidData, "File Data");
							return;
						}
						TransportRuleCollection rules;
						ex = ImportTransportRuleCollection.TryParseE14Format(memoryStream, out rules);
						if (ex == null)
						{
							this.ProcessE14Format(rules);
							return;
						}
					}
					else
					{
						IEnumerable<string> cmdlets;
						ex = ImportTransportRuleCollection.TryParseE15Format(memoryStream, out cmdlets);
						if (ex == null)
						{
							this.ProcessE15Format(cmdlets);
							return;
						}
					}
				}
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidData, "File Data");
			}
		}

		// Token: 0x0600696D RID: 26989 RVA: 0x001B31EC File Offset: 0x001B13EC
		internal static Exception TryParseVersion(Stream contentStream, out bool isE14Format)
		{
			isE14Format = false;
			try
			{
				contentStream.Position = 0L;
				XDocument xdocument = XDocument.Load(contentStream);
				IEnumerable<XElement> source = from rule in xdocument.Elements("rules").Elements("rule")
				select rule;
				if (!source.Any<XElement>())
				{
					return null;
				}
				IEnumerable<string> enumerable = from rule in source
				let formatAttribute = rule.Attribute("format")
				where formatAttribute != null && !string.IsNullOrWhiteSpace(formatAttribute.Value)
				select formatAttribute.Value;
				if (!enumerable.Any<string>())
				{
					isE14Format = true;
					return null;
				}
				if (source.Count<XElement>() > enumerable.Count<string>())
				{
					throw new ParseException(RulesStrings.InvalidAttribute("format", "rule", enumerable.First<string>()));
				}
				foreach (string text in enumerable)
				{
					if (string.Compare(text, "cmdlet", true) != 0)
					{
						throw new ParseException(RulesStrings.InvalidAttribute("format", "rule", text));
					}
				}
				isE14Format = false;
			}
			catch (ParseException result)
			{
				return result;
			}
			catch (XmlException result2)
			{
				return result2;
			}
			return null;
		}

		// Token: 0x0600696E RID: 26990 RVA: 0x001B33C0 File Offset: 0x001B15C0
		internal static Exception TryParseE15Format(Stream contentStream, out IEnumerable<string> cmdlets)
		{
			try
			{
				contentStream.Position = 0L;
				cmdlets = PowershellTransportRuleSerializer.ParseStream(contentStream);
			}
			catch (ParseException result)
			{
				cmdlets = null;
				return result;
			}
			catch (ArgumentException result2)
			{
				cmdlets = null;
				return result2;
			}
			catch (XmlException result3)
			{
				cmdlets = null;
				return result3;
			}
			if (cmdlets == null || cmdlets.Any(new Func<string, bool>(string.IsNullOrEmpty)))
			{
				return new ArgumentException("File Data - Empty cmdlet block");
			}
			return null;
		}

		// Token: 0x0600696F RID: 26991 RVA: 0x001B3448 File Offset: 0x001B1648
		internal static Exception TryParseE14Format(Stream contentStream, out TransportRuleCollection rules)
		{
			try
			{
				contentStream.Position = 0L;
				rules = (TransportRuleCollection)TransportRuleParser.Instance.LoadStream(contentStream);
			}
			catch (ParserException result)
			{
				rules = null;
				return result;
			}
			return null;
		}

		// Token: 0x06006970 RID: 26992 RVA: 0x001B348C File Offset: 0x001B168C
		private static bool IsMigratedRule(TransportRuleHandle ruleHandle)
		{
			ArgumentValidator.ThrowIfNull("ruleHandle", ruleHandle);
			return ruleHandle.Rule.Comments != null && ruleHandle.Rule.Comments.Contains("FOPEPolicyMigration");
		}

		// Token: 0x06006971 RID: 26993 RVA: 0x001B34C4 File Offset: 0x001B16C4
		private IEnumerable<string> ProcessCmdlets(IEnumerable<string> cmdlets)
		{
			CmdletValidator cmdletValidator = new CmdletValidator(ImportTransportRuleCollection.AllowedCommands, null, ImportTransportRuleCollection.NotAllowedParams);
			List<string> list = new List<string>();
			foreach (string text in cmdlets)
			{
				if (!cmdletValidator.ValidateCmdlet(text))
				{
					base.WriteError(new ArgumentException("Command " + text + " does not meet the validation constraints"), ErrorCategory.InvalidArgument, null);
				}
				else
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06006972 RID: 26994 RVA: 0x001B354C File Offset: 0x001B174C
		private void ClearExistingRules(ADRuleStorageManager storedRules)
		{
			switch ((base.Fields["MigrationSource"] == null) ? MigrationSourceType.None : ((MigrationSourceType)base.Fields["MigrationSource"]))
			{
			case MigrationSourceType.Fope:
				storedRules.ClearRules(new ADRuleStorageManager.RuleFilter(ImportTransportRuleCollection.IsMigratedRule));
				return;
			case MigrationSourceType.Ehe:
				this.BackupRulesForEheMigration();
				storedRules.ClearRules(null);
				return;
			default:
				storedRules.ClearRules(null);
				return;
			}
		}

		// Token: 0x06006973 RID: 26995 RVA: 0x001B35C0 File Offset: 0x001B17C0
		private bool IsMigratedFopeRuleCollectionBeingImported()
		{
			return base.Fields["MigrationSource"] != null && (MigrationSourceType)base.Fields["MigrationSource"] == MigrationSourceType.Fope;
		}

		// Token: 0x06006974 RID: 26996 RVA: 0x001B35F0 File Offset: 0x001B17F0
		private void ProcessE15Format(IEnumerable<string> cmdlets)
		{
			Exception ex = null;
			try
			{
				IConfigDataProvider session = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
				ADRuleStorageManager storedRules = new ADRuleStorageManager(this.ruleCollectionName, session);
				this.ClearExistingRules(storedRules);
			}
			catch (RuleCollectionNotInAdException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				return;
			}
			string lastUsedDc = (base.DataSession as IConfigurationSession).LastUsedDc;
			cmdlets = this.ProcessCmdlets(cmdlets);
			try
			{
				CmdletRunner.RunCmdlets(Utils.AddOrganizationScopeToCmdlets(cmdlets, Utils.GetOrganizationParameterValue(base.Fields)));
			}
			catch (ArgumentException ex2)
			{
				ex = ex2;
			}
			catch (ParseException ex3)
			{
				ex = ex3;
			}
			catch (RuntimeException ex4)
			{
				ex = ex4;
			}
			catch (CmdletExecutionException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				this.RecoverDeletedRules(lastUsedDc);
				base.WriteError(ex, ErrorCategory.InvalidArgument, "Error executing script from the cmdlet block: " + ex.Message);
			}
		}

		// Token: 0x06006975 RID: 26997 RVA: 0x001B36E4 File Offset: 0x001B18E4
		private void ProcessE14Format(TransportRuleCollection rules)
		{
			ADRuleStorageManager adruleStorageManager;
			try
			{
				IConfigDataProvider session = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
				adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, session);
			}
			catch (RuleCollectionNotInAdException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidOperation, null);
				return;
			}
			Exception ex = null;
			try
			{
				if (!Utils.IsEdgeRoleInstalled())
				{
					Version v = null;
					bool flag = false;
					bool flag2 = false;
					foreach (Rule rule in rules)
					{
						TransportRule transportRule = (TransportRule)rule;
						if (transportRule.IsTooAdvancedToParse)
						{
							base.WriteError(new InvalidOperationException(Strings.CannotCreateRuleDueToVersion(transportRule.Name)), ErrorCategory.InvalidOperation, null);
							return;
						}
						Version minimumVersion = transportRule.MinimumVersion;
						if (v == null || v < minimumVersion)
						{
							v = minimumVersion;
						}
						if (!flag || !flag2)
						{
							foreach (Action action in transportRule.Actions)
							{
								if (string.Equals(action.Name, "ApplyDisclaimer") || string.Equals(action.Name, "ApplyDisclaimerWithSeparator") || string.Equals(action.Name, "ApplyDisclaimerWithSeparatorAndReadingOrder"))
								{
									flag = true;
								}
								if (string.Equals(action.Name, "LogEvent"))
								{
									flag2 = true;
								}
							}
						}
					}
					if (flag && !this.Force && !base.ShouldContinue(Strings.PromptToUpgradeRulesFormat))
					{
						return;
					}
					if (flag2 && !this.Force && !base.ShouldContinue(Strings.PromptToRemoveLogEventAction))
					{
						return;
					}
				}
				try
				{
					adruleStorageManager.ReplaceRules(rules, this.ResolveCurrentOrganization());
				}
				catch (DataValidationException exception2)
				{
					base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				ex = ex2;
			}
			catch (ArgumentException ex3)
			{
				ex = ex3;
			}
			catch (PathTooLongException ex4)
			{
				ex = ex4;
			}
			catch (DirectoryNotFoundException ex5)
			{
				ex = ex5;
			}
			catch (UnauthorizedAccessException ex6)
			{
				ex = ex6;
			}
			catch (FileNotFoundException ex7)
			{
				ex = ex7;
			}
			catch (IOException ex8)
			{
				ex = ex8;
			}
			catch (NotSupportedException ex9)
			{
				ex = ex9;
			}
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06006976 RID: 26998 RVA: 0x001B3A10 File Offset: 0x001B1C10
		private bool IsDatacenter()
		{
			OrganizationId a = this.ResolveCurrentOrganization();
			return a != OrganizationId.ForestWideOrgId;
		}

		// Token: 0x06006977 RID: 26999 RVA: 0x001B3A30 File Offset: 0x001B1C30
		private void BackupRulesForEheMigration()
		{
			ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, base.DataSession);
			adruleStorageManager.LoadRuleCollection();
			this.transportRuleCollectionBackUp = adruleStorageManager.GetRuleCollection();
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x001B3A64 File Offset: 0x001B1C64
		private void RecoverDeletedRules(string domainController)
		{
			if (this.transportRuleCollectionBackUp != null)
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, false, ConsistencyMode.IgnoreInvalid, base.SessionSettings, 643, "RecoverDeletedRules", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\TransportRule\\ImportTransportRuleCollection.cs");
				IConfigDataProvider session = new MessagingPoliciesSyncLogDataSession(tenantOrTopologyConfigurationSession, null, null);
				ADRuleStorageManager adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, session);
				adruleStorageManager.ClearRules(null);
				adruleStorageManager.ReplaceRules(this.transportRuleCollectionBackUp, this.ResolveCurrentOrganization());
			}
		}

		// Token: 0x040036C0 RID: 14016
		internal const string FileDataParameterName = "FileData";

		// Token: 0x040036C1 RID: 14017
		internal const string MigrationSourceParameterName = "MigrationSource";

		// Token: 0x040036C2 RID: 14018
		internal const string E15FormatValue = "cmdlet";

		// Token: 0x040036C3 RID: 14019
		private static readonly HashSet<string> AllowedCommands = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"New-TransportRule"
		};

		// Token: 0x040036C4 RID: 14020
		private static readonly Dictionary<string, HashSet<string>> NotAllowedParams = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"New-TransportRule",
				new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"-Organization"
				}
			}
		};

		// Token: 0x040036C5 RID: 14021
		private readonly string ruleCollectionName;

		// Token: 0x040036C6 RID: 14022
		private TransportRuleCollection transportRuleCollectionBackUp;
	}
}
