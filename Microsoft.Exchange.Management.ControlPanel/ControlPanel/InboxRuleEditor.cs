using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003FA RID: 1018
	[ClientScriptResource("InboxRuleEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[RequiredScript(typeof(ExtenderControlBase))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class InboxRuleEditor : RuleEditor
	{
		// Token: 0x0600341A RID: 13338 RVA: 0x000A279C File Offset: 0x000A099C
		protected override void OnPreRender(EventArgs e)
		{
			NameValueCollection queryString = this.Context.Request.QueryString;
			if (!base.UseSetObject)
			{
				string text = null;
				if (!string.IsNullOrEmpty(queryString["id"]))
				{
					text = queryString["id"];
				}
				else if (!string.IsNullOrEmpty(queryString["ewsid"]))
				{
					string id = queryString["ewsid"].Replace(' ', '+');
					text = StoreId.EwsIdToStoreObjectId(id).ToBase64String();
				}
				if (!string.IsNullOrEmpty(text))
				{
					this.ruleObj = this.GetMessageInfo(text);
				}
				if (!base.UseSetObject && !string.IsNullOrEmpty(queryString["tplNames"]))
				{
					this.templatePhraseNames = this.GetTemplatePhraseNameArray(queryString["tplNames"]);
				}
			}
			if (this.ruleObj != null && this.templatePhraseNames != null)
			{
				throw new BadRequestException(new Exception("Both Contextual and Template mode are not supported together."));
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000A2888 File Offset: 0x000A0A88
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			OptionsHelpId optionsHelpId = base.UseSetObject ? OptionsHelpId.EditInboxRule : OptionsHelpId.NewInboxRule;
			descriptor.AddProperty("HrefForStopProcessingRules", HelpUtil.BuildEhcHref(optionsHelpId.ToString()), true);
			if (this.ruleObj != null)
			{
				descriptor.AddScriptProperty("MailMessageObject", this.ruleObj.ToJsonString(null));
			}
			if (this.templatePhraseNames != null)
			{
				descriptor.AddScriptProperty("TemplatePhraseNames", this.templatePhraseNames.ToJsonString(null));
			}
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x000A2914 File Offset: 0x000A0B14
		private string[] GetTemplatePhraseNameArray(string tplNames)
		{
			string[] array = tplNames.Split(new char[]
			{
				','
			});
			int num = 0;
			int num2 = 0;
			foreach (string value in array)
			{
				if (InboxRuleEditor.allConditionNames.Contains(value))
				{
					num++;
				}
				else
				{
					if (!InboxRuleEditor.allActionNames.Contains(value))
					{
						throw new BadQueryParameterException("tplNames", new ArgumentException("One of the supplied phrase name is not supported."));
					}
					num2++;
				}
			}
			if ((num != 1 && num2 != 1) || num > 1 || num2 > 1)
			{
				throw new BadQueryParameterException("tplNames", new Exception("None/Multiple actions or conditions are not supported."));
			}
			IEnumerable<string> enumerable = from phraseName in array
			where !this.NonWritablePhraseNameList.Contains(phraseName)
			select phraseName;
			if (enumerable != null)
			{
				return enumerable.ToArray<string>();
			}
			return null;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x000A29E4 File Offset: 0x000A0BE4
		private InboxRule GetMessageInfo(string messageId)
		{
			InboxRules inboxRules = base.RuleService as InboxRules;
			PowerShellResults<InboxRule> mailMessage = inboxRules.GetMailMessage(new NewInboxRule
			{
				FromMessageId = new Identity(messageId, messageId),
				ValidateOnly = new bool?(true),
				AllowExceuteThruHttpGetRequest = true
			});
			if (mailMessage.Failed)
			{
				string cause = (mailMessage.ErrorRecords[0].Exception is ManagementObjectNotFoundException) ? "messagenotfound" : "unexpected";
				ErrorHandlingUtil.TransferToErrorPage(cause);
			}
			return mailMessage.Value;
		}

		// Token: 0x04002520 RID: 9504
		private static string[] allConditionNames = Array.ConvertAll<RulePhrase, string>(new InboxRules().SupportedConditions, (RulePhrase x) => x.Name);

		// Token: 0x04002521 RID: 9505
		private static string[] allActionNames = Array.ConvertAll<RulePhrase, string>(new InboxRules().SupportedActions, (RulePhrase x) => x.Name);

		// Token: 0x04002522 RID: 9506
		private InboxRule ruleObj;

		// Token: 0x04002523 RID: 9507
		private string[] templatePhraseNames;
	}
}
