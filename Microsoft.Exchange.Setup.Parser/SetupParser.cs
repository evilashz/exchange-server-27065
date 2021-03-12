using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x02000002 RID: 2
	internal abstract class SetupParser
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		// (set) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public string SeparatorCharacters { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		public Dictionary<string, string> TokenMapping
		{
			get
			{
				return this.tokenMapping;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		public Dictionary<string, ParameterSchemaEntry> ParserSchema
		{
			get
			{
				return this.parserSchema;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F4 File Offset: 0x000002F4
		public SetupParameter Parse(string parseString)
		{
			string text = null;
			int num = parseString.IndexOfAny(this.SeparatorCharacters.ToCharArray());
			string text2;
			bool flag;
			if (num < 0)
			{
				text2 = parseString.ToLowerInvariant();
				flag = false;
			}
			else
			{
				text2 = parseString.Substring(0, num).ToLowerInvariant();
				text2 = text2.TrimEnd(new char[0]);
				text = parseString.Substring(num + 1, parseString.Length - (num + 1));
				text = text.TrimStart(new char[0]);
				flag = true;
			}
			if (!this.TokenMapping.ContainsKey(text2))
			{
				throw new ParseException(Strings.UnknownParameter(text2));
			}
			text2 = this.TokenMapping[text2];
			ParameterSchemaEntry parameterSchemaEntry = this.ParserSchema[text2];
			if (!flag)
			{
				if (parameterSchemaEntry.ParameterType == ParameterType.MustHaveValue)
				{
					throw new ParseException(Strings.ParameterMustHaveValue(text2));
				}
			}
			else
			{
				if (parameterSchemaEntry.ParameterType == ParameterType.CannotHaveValue)
				{
					throw new ParseException(Strings.ParameterCannotHaveValue(text2));
				}
				if (string.IsNullOrEmpty(text))
				{
					throw new ParseException(Strings.EmptyValueSpecified(text2));
				}
			}
			return new SetupParameter(text2, parameterSchemaEntry.ParseMethod(text));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021EC File Offset: 0x000003EC
		public Dictionary<string, SetupParameter> ParseAll(List<string> args)
		{
			Dictionary<string, SetupParameter> dictionary = new Dictionary<string, SetupParameter>();
			foreach (string parseString in args)
			{
				SetupParameter setupParameter = this.Parse(parseString);
				if (dictionary.ContainsKey(setupParameter.Name))
				{
					throw new ParseException(Strings.ParameterSpecifiedMultipleTimes(setupParameter.Name));
				}
				dictionary.Add(setupParameter.Name, setupParameter);
			}
			return dictionary;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002270 File Offset: 0x00000470
		public virtual void ValidateParameters(Dictionary<string, SetupParameter> parameters, SetupOperations currentOperation)
		{
			if (currentOperation == SetupOperations.None)
			{
				throw new ParseException(Strings.CurrentOperationNotSet);
			}
			foreach (SetupParameter setupParameter in parameters.Values)
			{
				ParameterSchemaEntry parameterSchemaEntry = this.ParserSchema[setupParameter.Name];
				if ((currentOperation & parameterSchemaEntry.ValidOperations) == SetupOperations.None)
				{
					throw new ParseException(Strings.ParameterNotValidForCurrentOperation(setupParameter.Name, currentOperation.ToString()));
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly Dictionary<string, string> tokenMapping = new Dictionary<string, string>();

		// Token: 0x04000002 RID: 2
		private readonly Dictionary<string, ParameterSchemaEntry> parserSchema = new Dictionary<string, ParameterSchemaEntry>();

		// Token: 0x02000003 RID: 3
		public static class Tokens
		{
			// Token: 0x04000004 RID: 4
			public const string Mode = "mode";

			// Token: 0x04000005 RID: 5
			public const string Roles = "roles";

			// Token: 0x04000006 RID: 6
			public const string PrepareAD = "preparead";

			// Token: 0x04000007 RID: 7
			public const string PrepareSCT = "preparesct";

			// Token: 0x04000008 RID: 8
			public const string PrepareSchema = "prepareschema";

			// Token: 0x04000009 RID: 9
			public const string PrepareDomain = "preparedomain";

			// Token: 0x0400000A RID: 10
			public const string PrepareAllDomains = "preparealldomains";

			// Token: 0x0400000B RID: 11
			public const string SourceDir = "sourcedir";

			// Token: 0x0400000C RID: 12
			public const string TargetDir = "targetdir";

			// Token: 0x0400000D RID: 13
			public const string AnswerFile = "answerfile";

			// Token: 0x0400000E RID: 14
			public const string DomainController = "domaincontroller";

			// Token: 0x0400000F RID: 15
			public const string AdamLdapPort = "adamldapport";

			// Token: 0x04000010 RID: 16
			public const string AdamSslPort = "adamsslport";

			// Token: 0x04000011 RID: 17
			public const string NewProvisionedServer = "newprovisionedserver";

			// Token: 0x04000012 RID: 18
			public const string RemoveProvisionedServer = "removeprovisionedserver";

			// Token: 0x04000013 RID: 19
			public const string EnableErrorReporting = "enableerrorreporting";

			// Token: 0x04000014 RID: 20
			public const string NoSelfSignedCertificates = "noselfsignedcertificates";

			// Token: 0x04000015 RID: 21
			public const string OrganizationName = "organizationname";

			// Token: 0x04000016 RID: 22
			public const string DoNotStartTransport = "donotstarttransport";

			// Token: 0x04000017 RID: 23
			public const string AddUmLanguagePack = "addumlanguagepack";

			// Token: 0x04000018 RID: 24
			public const string RemoveUmLanguagePack = "removeumlanguagepack";

			// Token: 0x04000019 RID: 25
			public const string UpdatesDir = "updatesdir";

			// Token: 0x0400001A RID: 26
			public const string LanguagePack = "languagepack";

			// Token: 0x0400001B RID: 27
			public const string CustomerFeedbackEnabled = "customerfeedbackenabled";

			// Token: 0x0400001C RID: 28
			public const string Industry = "industry";

			// Token: 0x0400001D RID: 29
			public const string ExternalCASServerDomain = "externalcasserverdomain";

			// Token: 0x0400001E RID: 30
			public const string Mdbname = "mdbname";

			// Token: 0x0400001F RID: 31
			public const string DbFilePath = "dbfilepath";

			// Token: 0x04000020 RID: 32
			public const string LogFolderPath = "logfolderpath";

			// Token: 0x04000021 RID: 33
			public const string ActiveDirectorySplitPermissions = "ActiveDirectorySplitPermissions";

			// Token: 0x04000022 RID: 34
			public const string InstallWindowsComponents = "installwindowscomponents";

			// Token: 0x04000023 RID: 35
			public const string DisableAMFiltering = "disableamfiltering";

			// Token: 0x04000024 RID: 36
			public const string Restart = "restart";

			// Token: 0x04000025 RID: 37
			public const string IAcceptExchangeServerLicenseTerms = "iacceptexchangeserverlicenseterms";

			// Token: 0x04000026 RID: 38
			public const string TenantOrganizationConfig = "tenantorganizationconfig";
		}
	}
}
