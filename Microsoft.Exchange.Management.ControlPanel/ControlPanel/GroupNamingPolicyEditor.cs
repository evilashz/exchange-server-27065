using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000504 RID: 1284
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("GroupNamingPolicyEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class GroupNamingPolicyEditor : ScriptControlBase, INamingContainer
	{
		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x000B9A18 File Offset: 0x000B7C18
		public RulePhrase[] SupportedPrefixes
		{
			get
			{
				return GroupNamingPolicyEditor.supportedRules;
			}
		}

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x06003DBA RID: 15802 RVA: 0x000B9A1F File Offset: 0x000B7C1F
		public RulePhrase[] SupportedSuffixes
		{
			get
			{
				return GroupNamingPolicyEditor.supportedRules;
			}
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000B9A26 File Offset: 0x000B7C26
		public GroupNamingPolicyEditor() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "GroupNamingPolicyEditor";
			this.parametersPanel.ID = "parametersPanel";
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x06003DBC RID: 15804 RVA: 0x000B9A56 File Offset: 0x000B7C56
		// (set) Token: 0x06003DBD RID: 15805 RVA: 0x000B9A5E File Offset: 0x000B7C5E
		public string PrefixLabel { get; set; }

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x06003DBE RID: 15806 RVA: 0x000B9A67 File Offset: 0x000B7C67
		// (set) Token: 0x06003DBF RID: 15807 RVA: 0x000B9A6F File Offset: 0x000B7C6F
		public string SuffixLabel { get; set; }

		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x06003DC0 RID: 15808 RVA: 0x000B9A78 File Offset: 0x000B7C78
		public string ParametersPanel
		{
			get
			{
				return this.parametersPanel.ClientID;
			}
		}

		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x06003DC1 RID: 15809 RVA: 0x000B9A85 File Offset: 0x000B7C85
		// (set) Token: 0x06003DC2 RID: 15810 RVA: 0x000B9A8D File Offset: 0x000B7C8D
		[DefaultValue(null)]
		public string WriteScope { get; set; }

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x06003DC3 RID: 15811 RVA: 0x000B9A96 File Offset: 0x000B7C96
		public bool UseSetObject
		{
			get
			{
				return this.PropertyControl.UseSetObject;
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x000B9AA4 File Offset: 0x000B7CA4
		private Properties PropertyControl
		{
			get
			{
				if (this.propertiesControl == null)
				{
					for (Control parent = this.Parent; parent != null; parent = parent.Parent)
					{
						if (parent.GetType() == typeof(Properties))
						{
							this.propertiesControl = (Properties)parent;
							break;
						}
					}
					if (this.propertiesControl == null)
					{
						throw new InvalidOperationException("GroupNamingPolicyEditor control must be put inside a Properties control.");
					}
				}
				return this.propertiesControl;
			}
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000B9B20 File Offset: 0x000B7D20
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("GroupNameLocString", DistributionGroupNamingPolicy.GroupNameLocString);
			descriptor.AddProperty("PrefixLabel", this.PrefixLabel, true);
			descriptor.AddProperty("SuffixLabel", this.SuffixLabel, true);
			descriptor.AddProperty("MaxLength", 1024.ToString(), true);
			descriptor.AddElementProperty("ParametersPanel", this.ParametersPanel, this);
			if (!this.UseSetObject)
			{
				descriptor.AddProperty("UseSetObject", false);
			}
			EnumParameter enumParameter = (EnumParameter)this.SupportedPrefixes[0].Parameters[0];
			Array.Sort<EnumValue>(enumParameter.Values, (EnumValue val1, EnumValue val2) => val1.DisplayText.CompareTo(val2.DisplayText));
			descriptor.AddScriptProperty("AllPrefixes", this.SupportedPrefixes.ToJsonString(null));
			descriptor.AddScriptProperty("AllSuffixes", this.SupportedSuffixes.ToJsonString(null));
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x000B9C18 File Offset: 0x000B7E18
		protected override void CreateChildControls()
		{
			this.Controls.Add(this.parametersPanel);
			RulePhrase[] supportedPrefixes = this.SupportedPrefixes;
			RulePhrase[] supportedSuffixes = this.SupportedSuffixes;
			List<Type> list = new List<Type>();
			this.GetRequiredFormlets(supportedPrefixes, list);
			this.GetRequiredFormlets(supportedSuffixes, list);
			Panel panel = new Panel();
			panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			foreach (Type type in list)
			{
				panel.Controls.Add((Control)Activator.CreateInstance(type));
			}
			this.Controls.Add(panel);
			base.CreateChildControls();
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000B9CD8 File Offset: 0x000B7ED8
		private void GetRequiredFormlets(RulePhrase[] phrases, List<Type> requiredFormlets)
		{
			if (!phrases.IsNullOrEmpty())
			{
				foreach (RulePhrase rulePhrase in phrases)
				{
					if (!rulePhrase.Parameters.IsNullOrEmpty())
					{
						foreach (FormletParameter formletParameter in rulePhrase.Parameters)
						{
							if (formletParameter.FormletType != null && !requiredFormlets.Contains(formletParameter.FormletType))
							{
								requiredFormlets.Add(formletParameter.FormletType);
							}
						}
					}
				}
			}
		}

		// Token: 0x04002826 RID: 10278
		private static RulePhrase[] supportedRules = new RulePhrase[]
		{
			new RulePhrase("Attribute", Strings.GroupNamingPolicyAttribute, new FormletParameter[]
			{
				new EnumParameter("Attribute", Strings.GroupNamingPolicyAttributeDialigTitle, Strings.GroupNamingPolicyAttributeDialigLabel, typeof(GroupNamingPolicyAttributeEnum), null)
			}, null, false),
			new RulePhrase("Text", Strings.GroupNamingPolicyText, new FormletParameter[]
			{
				new StringParameter("Text", Strings.GroupNamingPolicyTextDialigTitle, Strings.GroupNamingPolicyTextDialigLabel, typeof(GroupNamingPolicyText), false)
			}, null, false)
		};

		// Token: 0x04002827 RID: 10279
		private Panel parametersPanel = new Panel();

		// Token: 0x04002828 RID: 10280
		private Properties propertiesControl;
	}
}
