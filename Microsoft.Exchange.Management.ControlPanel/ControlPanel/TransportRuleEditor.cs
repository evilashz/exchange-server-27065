using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.MessagingPolicies.Rules;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001C0 RID: 448
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("TransportRuleEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class TransportRuleEditor : RuleEditor
	{
		// Token: 0x0600243B RID: 9275 RVA: 0x0006F054 File Offset: 0x0006D254
		public TransportRuleEditor()
		{
			this.dtpExpiryDate.ID = "expiryDate";
			this.dtpExpiryDate.HasTimePicker = true;
			this.dtpActivationDate.ID = "enableDate";
			this.dtpActivationDate.HasTimePicker = true;
			this.radRuleMode.ID = "ruleMode";
			this.txtComment.ID = "rulecomments";
			this.txtComment.Rows = 3;
			this.txtComment.TextMode = TextBoxMode.MultiLine;
			base.IsCopyMode = false;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0006F1E4 File Offset: 0x0006D3E4
		protected override void CreateChildControls()
		{
			this.chkExpiryDate.Text = Strings.TransportRuleExpiryDate;
			this.chkActivationDate.Text = Strings.TransportRuleActivationDate;
			this.chkExpiryDate.ID = string.Format("{0}_label", this.dtpExpiryDate.ID);
			this.chkActivationDate.ID = string.Format("{0}_label", this.dtpActivationDate.ID);
			this.radRuleMode.CellPadding = 0;
			this.radRuleMode.Items.Add(new ListItem(Strings.EnforceRule, RuleMode.Enforce.ToString()));
			if (RbacPrincipal.Current.IsInRole("FFO") && !RbacPrincipal.Current.IsInRole("EOPPremium"))
			{
				this.radRuleMode.Items.Add(new ListItem(Strings.TestRuleDisabledFFO, RuleMode.Audit.ToString()));
			}
			else
			{
				this.radRuleMode.Items.Add(new ListItem(Strings.TestRuleEnabled, RuleMode.AuditAndNotify.ToString()));
				this.radRuleMode.Items.Add(new ListItem(Strings.TestRuleDisabled, RuleMode.Audit.ToString()));
			}
			this.radRuleMode.SelectedValue = RuleMode.Enforce.ToString();
			this.ruleModePanel.Controls.Add(new Label
			{
				Text = Strings.RuleModeDescription,
				ID = string.Format("{0}_label", this.radRuleMode.ID)
			});
			this.ruleModePanel.Controls.Add(this.radRuleMode);
			NumericInputExtender numericInputExtender = new NumericInputExtender();
			numericInputExtender.TargetControlID = this.numberControl.UniqueID;
			this.rulePriorityPanel.Controls.Add(new Label
			{
				Text = Strings.TransportRulePriority,
				ID = string.Format("{0}_label", this.numberControl.ID)
			});
			this.rulePriorityPanel.Controls.Add(new LiteralControl("<br />"));
			this.rulePriorityPanel.Controls.Add(this.numberControl);
			this.rulePriorityPanel.Controls.Add(numericInputExtender);
			this.staticOptionsPanel.Controls.Add(this.rulePriorityPanel);
			this.staticOptionsPanel.Controls.Add(this.ruleModePanel);
			this.activationExpiryPanel.Controls.Add(this.chkActivationDate);
			this.activationExpiryPanel.Controls.Add(this.dtpActivationDate);
			this.activationExpiryPanel.Controls.Add(this.chkExpiryDate);
			this.activationExpiryPanel.Controls.Add(this.dtpExpiryDate);
			this.ruleCommentsPanel.Controls.Add(new Label
			{
				Text = Strings.RuleComments,
				ID = string.Format("{0}_label", this.txtComment.ID)
			});
			this.ruleCommentsPanel.Controls.Add(new LiteralControl("<br />"));
			this.ruleCommentsPanel.Controls.Add(this.txtComment);
			this.evenMoreOptionsPanel.Controls.Add(this.activationExpiryPanel);
			this.evenMoreOptionsPanel.Controls.Add(this.ruleCommentsPanel);
			base.CreateChildControls();
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0006F564 File Offset: 0x0006D764
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("AuditLevelPropertyName", "SetAuditSeverity", true);
			descriptor.AddProperty("SenderAddressLocationPropertyName", "SenderAddressLocation", true);
			descriptor.AddProperty("ActivationDatePropertyName", "ActivationDate", true);
			descriptor.AddProperty("ExpiryDatePropertyName", "ExpiryDate", true);
			descriptor.AddProperty("UseLegecyRegExParameterName", "UseLegacyRegex", true);
			descriptor.AddProperty("DLPPolicyParameterName", "DlpPolicy", true);
			descriptor.AddProperty("ModePropertyName", "Mode", true);
			descriptor.AddProperty("StopProcessingRuleParameterName", "StopRuleProcessing", true);
			descriptor.AddProperty("CommentsPropertyName", "Comments", true);
			descriptor.AddProperty("TransportRuleAgentErrorActionName", "RuleErrorAction", true);
			descriptor.AddProperty("PriorityParameterName", "Priority", true);
			descriptor.AddElementProperty("ActivationExpiryContainer", this.activationExpiryPanel.ClientID, false);
			descriptor.AddComponentProperty("ActivationDatePicker", this.dtpActivationDate.ClientID, false);
			descriptor.AddComponentProperty("ExpiryDatePicker", this.dtpExpiryDate.ClientID, false);
			descriptor.AddElementProperty("ActivationDateCheckbox", this.chkActivationDate.ClientID);
			descriptor.AddElementProperty("ExpiryDateCheckbox", this.chkExpiryDate.ClientID);
			descriptor.AddElementProperty("RuleModeContainer", this.ruleModePanel.ClientID, false);
			descriptor.AddComponentProperty("RuleModeList", this.radRuleMode.ClientID, this);
			descriptor.AddElementProperty("RuleCommentsContainer", this.ruleCommentsPanel.ClientID, false);
			descriptor.AddElementProperty("CommentsField", this.txtComment.ClientID, this);
			descriptor.AddProperty("NotifySenderActionName", "NotifySender", true);
			descriptor.AddProperty("RejectMessageActionName", "RejectMessage", true);
			descriptor.AddProperty("RejectMessageStatusCodeActionName", "RejectMessageEnhancedStatusCode", true);
			descriptor.AddProperty("AuditLevelValues", new EnumValue[]
			{
				new EnumValue(Strings.ReportSeverityLevelLow, "Low"),
				new EnumValue(Strings.ReportSeverityLevelMedium, "Medium"),
				new EnumValue(Strings.ReportSeverityLevelHigh, "High"),
				new EnumValue(Strings.ReportSeverityLevelAuditNone, string.Empty)
			});
			descriptor.AddProperty("PolicyGroupMembershipValues", this.QueryDLPPolicies());
			List<EnumValue> list = new List<EnumValue>();
			foreach (object obj in Enum.GetValues(typeof(SenderAddressLocation)))
			{
				list.Add(new EnumValue(LocalizedDescriptionAttribute.FromEnum(typeof(SenderAddressLocation), obj), obj.ToString()));
			}
			descriptor.AddProperty("SenderAddressLocationValues", list.ToArray());
			descriptor.AddElementProperty("RulePriorityContainer", this.rulePriorityPanel.ClientID, false);
			descriptor.AddElementProperty("PriorityTextbox", this.numberControl.ClientID, this);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0006F868 File Offset: 0x0006DA68
		private EnumValue[] QueryDLPPolicies()
		{
			if (!RbacPrincipal.Current.IsInRole("Get-DLPPolicy"))
			{
				return new EnumValue[0];
			}
			WebServiceReference webServiceReference = new WebServiceReference(EcpUrl.EcpVDirForStaticResource + "DDI/DDIService.svc?schema=DLPPolicy&Workflow=GetPolicyList");
			PowerShellResults<JsonDictionary<object>> list = webServiceReference.GetList(null, null);
			if (list.Output != null)
			{
				List<EnumValue> list2 = new List<EnumValue>();
				JsonDictionary<object>[] output = list.Output;
				for (int i = 0; i < output.Length; i++)
				{
					Dictionary<string, object> dictionary = output[i];
					string text = (string)dictionary["Name"];
					list2.Add(new EnumValue(AntiXssEncoder.HtmlEncode(text, false), text));
				}
				return list2.ToArray();
			}
			return new EnumValue[0];
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0006F930 File Offset: 0x0006DB30
		protected override RulePhrase[] FilterActions(RulePhrase[] actions)
		{
			if (this.SimpleModeActions != null)
			{
				Array.ForEach<RulePhrase>(actions, delegate(RulePhrase a)
				{
					a.DisplayedInSimpleMode = this.SimpleModeActions.Contains(a.Name);
				});
			}
			return actions;
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0006F978 File Offset: 0x0006DB78
		protected override RulePhrase[] FilterConditions(RulePhrase[] conditions)
		{
			if (this.SimpleModeConditions != null)
			{
				Array.ForEach<RulePhrase>(conditions, delegate(RulePhrase a)
				{
					a.DisplayedInSimpleMode = this.SimpleModeConditions.Contains(a.Name);
				});
			}
			return conditions;
		}

		// Token: 0x17001B11 RID: 6929
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x0006FA44 File Offset: 0x0006DC44
		protected override List<string> NonWritablePhraseNameList
		{
			get
			{
				List<string> items = base.NonWritablePhraseNameList;
				string[] array = new string[]
				{
					"ActivationDate",
					"ExpiryDate",
					"StopRuleProcessing",
					"Mode",
					"SetAuditSeverity",
					"SenderAddressLocation",
					"RuleErrorAction",
					"UseLegacyRegex",
					"Comments",
					"DlpPolicy",
					"Priority"
				};
				Array.ForEach<string>(array, delegate(string f)
				{
					StringBuilder stringBuilder = new StringBuilder(150);
					stringBuilder.Append(this.UseSetObject ? "Set-" : "New-");
					stringBuilder.Append(this.RuleService.TaskNoun);
					stringBuilder.Append("?");
					stringBuilder.Append(f);
					stringBuilder.Append(this.WriteScope);
					if (!RbacPrincipal.Current.IsInRole(stringBuilder.ToString()))
					{
						items.Add(f);
					}
				});
				return items;
			}
		}

		// Token: 0x17001B12 RID: 6930
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x0006FAE6 File Offset: 0x0006DCE6
		// (set) Token: 0x06002443 RID: 9283 RVA: 0x0006FAEE File Offset: 0x0006DCEE
		public EACHelpId StopProcessingRulesHelpId { get; set; }

		// Token: 0x17001B13 RID: 6931
		// (get) Token: 0x06002444 RID: 9284 RVA: 0x0006FAF7 File Offset: 0x0006DCF7
		// (set) Token: 0x06002445 RID: 9285 RVA: 0x0006FAFF File Offset: 0x0006DCFF
		public EACHelpId UseLegacyRegexHelpId { get; set; }

		// Token: 0x17001B14 RID: 6932
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x0006FB08 File Offset: 0x0006DD08
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x0006FB10 File Offset: 0x0006DD10
		public EACHelpId DisclaimerLearnMoreHelpId { get; set; }

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x0006FB19 File Offset: 0x0006DD19
		// (set) Token: 0x06002449 RID: 9289 RVA: 0x0006FB21 File Offset: 0x0006DD21
		public string DisclaimerMessage { get; set; }

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x0006FB2A File Offset: 0x0006DD2A
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x0006FB32 File Offset: 0x0006DD32
		[DefaultValue(false)]
		public bool ForceDisclaimerOnNew { get; set; }

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x0006FB3B File Offset: 0x0006DD3B
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x0006FB43 File Offset: 0x0006DD43
		public string[] SimpleModeActions { get; set; }

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x0006FB4C File Offset: 0x0006DD4C
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x0006FB54 File Offset: 0x0006DD54
		public string[] SimpleModeConditions { get; set; }

		// Token: 0x04001E58 RID: 7768
		private DateTimePicker dtpExpiryDate = new DateTimePicker();

		// Token: 0x04001E59 RID: 7769
		private DateTimePicker dtpActivationDate = new DateTimePicker();

		// Token: 0x04001E5A RID: 7770
		private CheckBox chkExpiryDate = new CheckBox
		{
			CssClass = "RuleStopProcessingCheckBox"
		};

		// Token: 0x04001E5B RID: 7771
		private CheckBox chkActivationDate = new CheckBox
		{
			CssClass = "RuleStopProcessingCheckBox"
		};

		// Token: 0x04001E5C RID: 7772
		private EcpRadioButtonList radRuleMode = new EcpRadioButtonList();

		// Token: 0x04001E5D RID: 7773
		private TextBox txtComment = new TextBox
		{
			Columns = 60,
			MaxLength = 1024
		};

		// Token: 0x04001E5E RID: 7774
		private Panel activationExpiryPanel = new Panel
		{
			ID = "activationExpiryPanel"
		};

		// Token: 0x04001E5F RID: 7775
		private Panel ruleCommentsPanel = new Panel
		{
			ID = "ruleCommentsPanel"
		};

		// Token: 0x04001E60 RID: 7776
		private Panel ruleModePanel = new Panel
		{
			ID = "ruleModePanel"
		};

		// Token: 0x04001E61 RID: 7777
		private Panel rulePriorityPanel = new Panel
		{
			ID = "rulePriorityPanel"
		};

		// Token: 0x04001E62 RID: 7778
		private TextBox numberControl = new TextBox
		{
			ID = "priority",
			MaxLength = 3,
			Columns = 4
		};
	}
}
