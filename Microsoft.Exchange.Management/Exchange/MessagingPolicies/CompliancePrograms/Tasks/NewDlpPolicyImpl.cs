using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks
{
	// Token: 0x02000968 RID: 2408
	internal class NewDlpPolicyImpl : CmdletImplementation
	{
		// Token: 0x0600560D RID: 22029 RVA: 0x00161998 File Offset: 0x0015FB98
		internal NewDlpPolicyImpl(NewDlpPolicy taskObject)
		{
			this.taskObject = taskObject;
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x001619F8 File Offset: 0x0015FBF8
		public override void Validate()
		{
			Exception exception = null;
			this.ValidateParameterSets();
			DlpPolicyTemplateMetaData dlpPolicyTemplateMetaData = null;
			if (this.taskObject.Fields.IsModified("TemplateData"))
			{
				dlpPolicyTemplateMetaData = this.LoadDlpPolicyFromCustomTemplateData();
			}
			if (this.taskObject.Fields.IsModified("Template"))
			{
				dlpPolicyTemplateMetaData = this.LoadDlpPolicyFromInstalledTemplate();
			}
			if (this.taskObject.Fields.IsModified("Name"))
			{
				this.dlpPolicy.Name = this.taskObject.Name;
			}
			if (dlpPolicyTemplateMetaData != null)
			{
				this.dlpPolicy = new DlpPolicyMetaData(dlpPolicyTemplateMetaData, this.taskObject.CommandRuntime.Host.CurrentCulture);
				if (!string.IsNullOrEmpty(this.taskObject.Name))
				{
					this.dlpPolicy.Name = this.taskObject.Name;
				}
				this.dlpPolicy.PolicyCommands = NewDlpPolicyImpl.ParameterizeCmdlets(this.dlpPolicy.Name, dlpPolicyTemplateMetaData.PolicyCommands, dlpPolicyTemplateMetaData.RuleParameters, this.taskObject.Parameters, new NewDlpPolicy.WarningWriterDelegate(this.taskObject.WriteWarning), out exception);
				this.WriteParameterErrorIfExceptionOccurred(exception, "Parameters");
				this.dlpPolicy.PolicyCommands = DlpPolicyTemplateMetaData.LocalizeCmdlets(this.dlpPolicy.PolicyCommands, dlpPolicyTemplateMetaData.LocalizedPolicyCommandResources, this.taskObject.CommandRuntime.Host.CurrentCulture).ToList<string>();
				this.dlpPolicy.PolicyCommands.ForEach(delegate(string command)
				{
					DlpPolicyTemplateMetaData.ValidateCmdletParameters(command);
				});
			}
			if (this.taskObject.Fields.IsModified("State"))
			{
				this.dlpPolicy.State = this.taskObject.State;
			}
			if (this.taskObject.Fields.IsModified("Mode"))
			{
				this.dlpPolicy.Mode = this.taskObject.Mode;
			}
			if (this.taskObject.Fields.IsModified("Description"))
			{
				this.dlpPolicy.Description = this.taskObject.Description;
			}
			this.ValidateDlpPolicyName();
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x00161C0D File Offset: 0x0015FE0D
		private void WriteParameterErrorIfExceptionOccurred(Exception exception, string parameterName)
		{
			if (exception != null)
			{
				this.taskObject.WriteError(exception, ErrorCategory.InvalidArgument, parameterName);
			}
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x00161C20 File Offset: 0x0015FE20
		internal DlpPolicyTemplateMetaData LoadDlpPolicyFromCustomTemplateData()
		{
			try
			{
				return DlpUtils.LoadDlpPolicyTemplates(this.taskObject.TemplateData).FirstOrDefault<DlpPolicyTemplateMetaData>();
			}
			catch (Exception exception)
			{
				this.WriteParameterErrorIfExceptionOccurred(exception, "TemplateData");
			}
			return null;
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x00161C68 File Offset: 0x0015FE68
		private DlpPolicyTemplateMetaData LoadDlpPolicyFromInstalledTemplate()
		{
			DlpPolicyTemplateMetaData dlpPolicyTemplateMetaData = DlpUtils.LoadOutOfBoxDlpTemplate(this.taskObject.DomainController, this.taskObject.Template);
			if (dlpPolicyTemplateMetaData == null)
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorDlpPolicyTemplateNotFound(this.taskObject.Template)), ErrorCategory.InvalidArgument, "Template");
			}
			return dlpPolicyTemplateMetaData;
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x00161D28 File Offset: 0x0015FF28
		internal static List<string> ParameterizeCmdlets(string policyName, IEnumerable<string> cmdlets, IEnumerable<DlpTemplateRuleParameter> ruleParameters, Hashtable userSuppliedParameters, NewDlpPolicy.WarningWriterDelegate warningWriter, out Exception ex)
		{
			ex = null;
			Dictionary<string, string> parameterValues = new Dictionary<string, string>
			{
				{
					"%%DlpPolicyName%%",
					Utils.EscapeCmdletParameter(policyName)
				}
			};
			foreach (DlpTemplateRuleParameter dlpTemplateRuleParameter in ruleParameters)
			{
				bool flag = false;
				string text = dlpTemplateRuleParameter.Token.Replace("%%", string.Empty);
				if (parameterValues.ContainsKey(dlpTemplateRuleParameter.Token))
				{
					ex = new ArgumentException(Strings.ErrorDlpTemplateDuplicateParameter(text));
					return Enumerable.Empty<string>().ToList<string>();
				}
				if (userSuppliedParameters != null)
				{
					foreach (object obj in userSuppliedParameters.Keys)
					{
						string text2 = (string)obj;
						if (string.Compare(text, text2, true) == 0)
						{
							parameterValues.Add(dlpTemplateRuleParameter.Token, userSuppliedParameters[text2].ToString());
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					if (dlpTemplateRuleParameter.Required)
					{
						ex = new ArgumentException(Strings.ErrorDlpTemplateRequiresParameter(text, string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, from ruleParameter in ruleParameters
						select ruleParameter.Token.Replace("%%", ""))));
						return Enumerable.Empty<string>().ToList<string>();
					}
					parameterValues.Add(dlpTemplateRuleParameter.Token, string.Empty);
					warningWriter(Strings.DlpPolicyOptionalParameterNotSupplied(text));
				}
			}
			return (from cmdlet in cmdlets
			select parameterValues.Aggregate(cmdlet, (string current, KeyValuePair<string, string> parameter) => current.Replace(parameter.Key, parameter.Value).Trim())).ToList<string>();
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x00161F2C File Offset: 0x0016012C
		internal void ValidateParameterSets()
		{
			if (!this.taskObject.Fields.IsModified("Template") && !this.taskObject.Fields.IsModified("TemplateData") && !this.taskObject.Fields.IsModified("Name"))
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorDlpPolicyNameOrTemplateParameterMustBeSpecified), ErrorCategory.InvalidArgument, "Name");
			}
			if (this.taskObject.Fields.IsModified("Template") && this.taskObject.Fields.IsModified("TemplateData"))
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorTemplateAndTemplateDataCannotBothBeDefined), ErrorCategory.InvalidArgument, "TemplateData");
			}
			if (!this.taskObject.Fields.IsModified("Template") && !this.taskObject.Fields.IsModified("TemplateData") && !this.taskObject.Fields.IsModified("Name"))
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorParametersThatMustBeDefinedIfNeitherTemplateNorTemplateDataAreDefined(string.Join(CultureInfo.CurrentCulture.TextInfo.ListSeparator, new string[]
				{
					"Name"
				}))), ErrorCategory.InvalidArgument, "Template");
			}
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x00162078 File Offset: 0x00160278
		internal void ValidateDlpPolicyName()
		{
			if (DlpUtils.GetInstalledTenantDlpPolicies(base.DataSession, this.dlpPolicy.Name).Any<ADComplianceProgram>())
			{
				this.taskObject.WriteError(new ArgumentException(Strings.ErrorDlpPolicyAlreadyInstalled(this.dlpPolicy.Name)), ErrorCategory.InvalidArgument, "Name");
			}
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001620D0 File Offset: 0x001602D0
		public override void ProcessRecord()
		{
			try
			{
				IEnumerable<PSObject> enumerable;
				DlpUtils.AddTenantDlpPolicy(base.DataSession, this.dlpPolicy, Utils.GetOrganizationParameterValue(this.taskObject.Fields), out enumerable);
			}
			catch (DlpPolicyScriptExecutionException exception)
			{
				this.taskObject.WriteError(exception, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x040031CF RID: 12751
		private readonly NewDlpPolicy taskObject;

		// Token: 0x040031D0 RID: 12752
		private DlpPolicyMetaData dlpPolicy = new DlpPolicyMetaData
		{
			State = RuleState.Enabled,
			Mode = RuleMode.Audit,
			PublisherName = " ",
			Version = NewDlpPolicy.DefaultVersion,
			Description = " "
		};
	}
}
