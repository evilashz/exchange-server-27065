using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001BF RID: 447
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("RuleEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	public class RuleEditor : ScriptControlBase, INamingContainer
	{
		// Token: 0x0600240D RID: 9229 RVA: 0x0006E6C4 File Offset: 0x0006C8C4
		public RuleEditor() : base(HtmlTextWriterTag.Div)
		{
			this.CssClass = "RuleEditor";
			base.Attributes.Add("mode", "simple");
			this.parametersPanel.ID = "parametersPanel";
			this.AdvModeDialogMinSize = RuleEditor.DefaultAdvModeMinSize;
			this.evenMoreOptionsPanel.ID = "evenMoreOptionsPanel";
			this.staticOptionsPanel.ID = "staticOptionsPanel";
			this.defaultValues = new Dictionary<string, string>();
			this.AllowTypingInPicker = true;
			this.PreferOwaPicker = true;
			this.IsCopyMode = false;
		}

		// Token: 0x17001AFE RID: 6910
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x0006E77C File Offset: 0x0006C97C
		// (set) Token: 0x0600240F RID: 9231 RVA: 0x0006E784 File Offset: 0x0006C984
		public string Caption { get; set; }

		// Token: 0x17001AFF RID: 6911
		// (get) Token: 0x06002410 RID: 9232 RVA: 0x0006E78D File Offset: 0x0006C98D
		// (set) Token: 0x06002411 RID: 9233 RVA: 0x0006E795 File Offset: 0x0006C995
		public string ActionLabel { get; set; }

		// Token: 0x17001B00 RID: 6912
		// (get) Token: 0x06002412 RID: 9234 RVA: 0x0006E79E File Offset: 0x0006C99E
		// (set) Token: 0x06002413 RID: 9235 RVA: 0x0006E7A6 File Offset: 0x0006C9A6
		public string ConditionLabel { get; set; }

		// Token: 0x17001B01 RID: 6913
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x0006E7AF File Offset: 0x0006C9AF
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x0006E7B7 File Offset: 0x0006C9B7
		public string ExceptionLabel { get; set; }

		// Token: 0x17001B02 RID: 6914
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0006E7C0 File Offset: 0x0006C9C0
		public string ParametersPanel
		{
			get
			{
				return this.parametersPanel.ClientID;
			}
		}

		// Token: 0x17001B03 RID: 6915
		// (get) Token: 0x06002417 RID: 9239 RVA: 0x0006E7CD File Offset: 0x0006C9CD
		public string MoreOptionLink
		{
			get
			{
				return this.lnkMoreOption.ClientID;
			}
		}

		// Token: 0x17001B04 RID: 6916
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x0006E7DA File Offset: 0x0006C9DA
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x0006E7E2 File Offset: 0x0006C9E2
		[DefaultValue(null)]
		public string WriteScope { get; set; }

		// Token: 0x17001B05 RID: 6917
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x0006E7EB File Offset: 0x0006C9EB
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x0006E7F3 File Offset: 0x0006C9F3
		[DefaultValue(true)]
		public bool PreferOwaPicker { get; set; }

		// Token: 0x17001B06 RID: 6918
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x0006E7FC File Offset: 0x0006C9FC
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x0006E804 File Offset: 0x0006CA04
		[DefaultValue(true)]
		public bool AllowTypingInPicker { get; set; }

		// Token: 0x17001B07 RID: 6919
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x0006E80D File Offset: 0x0006CA0D
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x0006E815 File Offset: 0x0006CA15
		[TypeConverter(typeof(SizeConverter))]
		public Size AdvModeDialogMinSize { get; set; }

		// Token: 0x17001B08 RID: 6920
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x0006E81E File Offset: 0x0006CA1E
		public bool UseSetObject
		{
			get
			{
				return this.PropertyControl.UseSetObject;
			}
		}

		// Token: 0x17001B09 RID: 6921
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x0006E82B File Offset: 0x0006CA2B
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x0006E833 File Offset: 0x0006CA33
		public bool SupportAdvancedMode
		{
			get
			{
				return this.supportAdvancedMode;
			}
			set
			{
				this.supportAdvancedMode = value;
			}
		}

		// Token: 0x17001B0A RID: 6922
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x0006E83C File Offset: 0x0006CA3C
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x0006E844 File Offset: 0x0006CA44
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] DefaultActions
		{
			get
			{
				return this.defaultActions;
			}
			set
			{
				this.defaultActions = value;
			}
		}

		// Token: 0x17001B0B RID: 6923
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x0006E84D File Offset: 0x0006CA4D
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x0006E855 File Offset: 0x0006CA55
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] DefaultConditions
		{
			get
			{
				return this.defaultConditions;
			}
			set
			{
				this.defaultConditions = value;
			}
		}

		// Token: 0x17001B0C RID: 6924
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x0006E85E File Offset: 0x0006CA5E
		// (set) Token: 0x06002428 RID: 9256 RVA: 0x0006E866 File Offset: 0x0006CA66
		public bool IsCopyMode { get; set; }

		// Token: 0x17001B0D RID: 6925
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x0006E86F File Offset: 0x0006CA6F
		public Dictionary<string, string> DefaultValues
		{
			get
			{
				return this.defaultValues;
			}
		}

		// Token: 0x17001B0E RID: 6926
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x0006E8B4 File Offset: 0x0006CAB4
		protected virtual List<string> NonWritablePhraseNameList
		{
			get
			{
				if (this.nonWritablePhraseNameList == null)
				{
					this.nonWritablePhraseNameList = new List<string>();
					this.nonWritablePhraseNameList.AddRange(from condition in this.RuleService.SupportedConditions
					where !this.HasPermissionsOnPhrase(condition)
					select condition.Name);
					this.nonWritablePhraseNameList.AddRange(from action in this.RuleService.SupportedActions
					where !this.HasPermissionsOnPhrase(action)
					select action.Name);
					this.nonWritablePhraseNameList.AddRange(from exception in this.RuleService.SupportedExceptions
					where !this.HasPermissionsOnPhrase(exception)
					select exception.Name);
					StringBuilder stringBuilder = new StringBuilder(50);
					stringBuilder.Append(this.UseSetObject ? "Set-" : "New-");
					stringBuilder.Append(this.RuleService.TaskNoun);
					stringBuilder.Append("?");
					stringBuilder.Append("Name");
					stringBuilder.Append(this.WriteScope);
					if (!RbacPrincipal.Current.IsInRole(stringBuilder.ToString()))
					{
						this.nonWritablePhraseNameList.Add("Name");
					}
				}
				return this.nonWritablePhraseNameList;
			}
		}

		// Token: 0x17001B0F RID: 6927
		// (get) Token: 0x0600242B RID: 9259 RVA: 0x0006EA49 File Offset: 0x0006CC49
		protected RuleDataService RuleService
		{
			get
			{
				return (RuleDataService)this.PropertyControl.ServiceUrl.ServiceInstance;
			}
		}

		// Token: 0x17001B10 RID: 6928
		// (get) Token: 0x0600242C RID: 9260 RVA: 0x0006EA60 File Offset: 0x0006CC60
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
						throw new InvalidOperationException("RuleEditor control must be put inside a Properties control.");
					}
				}
				return this.propertiesControl;
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0006EAC8 File Offset: 0x0006CCC8
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("Caption", this.Caption, true);
			descriptor.AddProperty("ActionLabel", this.ActionLabel, true);
			descriptor.AddProperty("ConditionLabel", this.ConditionLabel, true);
			descriptor.AddProperty("ExceptionLabel", this.ExceptionLabel, true);
			descriptor.AddProperty("AdvModeDialogMinHeight", this.AdvModeDialogMinSize.Height.ToString(), true);
			descriptor.AddProperty("AdvModeDialogMinWidth", this.AdvModeDialogMinSize.Width.ToString(), true);
			descriptor.AddProperty("RuleNameMaxLength", this.RuleService.RuleNameMaxLength.ToString(), true);
			descriptor.AddElementProperty("MoreOptionButton", this.MoreOptionLink, this);
			descriptor.AddElementProperty("StaticOptionsPanel", this.staticOptionsPanel.ClientID, this);
			descriptor.AddElementProperty("MoreOptionsPanel", this.evenMoreOptionsPanel.ClientID, this);
			descriptor.AddElementProperty("ParametersPanel", this.ParametersPanel, this);
			if (!this.UseSetObject)
			{
				descriptor.AddProperty("UseSetObject", false);
			}
			descriptor.AddProperty("SupportAdvancedMode", this.SupportAdvancedMode);
			descriptor.AddScriptProperty("NonWritablePhraseNames", this.NonWritablePhraseNameList.ToJsonString(null));
			descriptor.AddScriptProperty("AllConditions", this.RuleService.SupportedConditions.ToJsonString(null));
			descriptor.AddScriptProperty("AllActions", this.RuleService.SupportedActions.ToJsonString(null));
			descriptor.AddScriptProperty("AllExceptions", this.RuleService.SupportedExceptions.ToJsonString(null));
			if (this.defaultConditions != null)
			{
				descriptor.AddScriptProperty("DefaultConditions", this.defaultConditions.ToJsonString(null));
			}
			if (this.defaultActions != null)
			{
				descriptor.AddScriptProperty("DefaultActions", this.defaultActions.ToJsonString(null));
			}
			if (this.defaultValues.Count != 0)
			{
				descriptor.AddScriptProperty("DefaultValues", new JsonDictionary<string>(this.defaultValues).ToJsonString(null));
			}
			descriptor.AddProperty("IsCopyMode", this.IsCopyMode);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0006ECF4 File Offset: 0x0006CEF4
		protected override void CreateChildControls()
		{
			this.Controls.Add(this.parametersPanel);
			RulePhrase[] phrases = this.FilterConditions(this.RuleService.SupportedConditions);
			RulePhrase[] phrases2 = this.FilterActions(this.RuleService.SupportedActions);
			RulePhrase[] phrases3 = this.FilterExceptions(this.RuleService.SupportedExceptions);
			List<Type> list = new List<Type>();
			RuleEditor.GetRequiredFormlets(phrases, list);
			RuleEditor.GetRequiredFormlets(phrases2, list);
			RuleEditor.GetRequiredFormlets(phrases3, list);
			Panel panel = new Panel();
			panel.Style.Add(HtmlTextWriterStyle.Display, "none");
			foreach (Type type in list)
			{
				if (string.Equals(type.Name, "PeoplePicker", StringComparison.Ordinal))
				{
					PeoplePicker peoplePicker = (PeoplePicker)Activator.CreateInstance(type);
					peoplePicker.PreferOwaPicker = this.PreferOwaPicker;
					peoplePicker.AllowTyping = this.AllowTypingInPicker;
					panel.Controls.Add(peoplePicker);
				}
				else
				{
					panel.Controls.Add((Control)Activator.CreateInstance(type));
				}
			}
			this.Controls.Add(panel);
			this.Controls.Add(this.staticOptionsPanel);
			Panel panel2 = new Panel();
			panel2.CssClass = "MoreOptionDiv";
			this.lnkMoreOption = new HtmlAnchor();
			this.lnkMoreOption.HRef = "javascript:void(0);";
			this.lnkMoreOption.ID = "btnMoreOption";
			this.lnkMoreOption.Attributes.Add("class", "MoreOptionLnk");
			this.lnkMoreOption.Controls.Add(new LiteralControl(Strings.RuleMoreOptions));
			panel2.Controls.Add(this.lnkMoreOption);
			this.Controls.Add(panel2);
			this.evenMoreOptionsPanel.CssClass = "hidden";
			this.Controls.Add(this.evenMoreOptionsPanel);
			base.CreateChildControls();
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0006EEF8 File Offset: 0x0006D0F8
		private bool HasPermissionsOnPhrase(RulePhrase phrase)
		{
			string str = (this.UseSetObject ? "Set-" : "New-") + this.RuleService.TaskNoun + "?";
			foreach (FormletParameter formletParameter in phrase.Parameters)
			{
				if (formletParameter.TaskParameterNames != null)
				{
					string str2 = string.IsNullOrEmpty(phrase.AdditionalRoles) ? string.Empty : (phrase.AdditionalRoles + "+");
					string str3 = string.Join("&", formletParameter.TaskParameterNames);
					if (!RbacPrincipal.Current.IsInRole(str2 + str + str3 + this.WriteScope))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0006EFB4 File Offset: 0x0006D1B4
		internal static void GetRequiredFormlets(RulePhrase[] phrases, List<Type> requiredFormlets)
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

		// Token: 0x06002431 RID: 9265 RVA: 0x0006F033 File Offset: 0x0006D233
		protected virtual RulePhrase[] FilterConditions(RulePhrase[] conditions)
		{
			return conditions;
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0006F036 File Offset: 0x0006D236
		protected virtual RulePhrase[] FilterActions(RulePhrase[] actions)
		{
			return actions;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0006F039 File Offset: 0x0006D239
		protected virtual RulePhrase[] FilterExceptions(RulePhrase[] exceptions)
		{
			return exceptions;
		}

		// Token: 0x04001E3E RID: 7742
		private const string NameParameterString = "Name";

		// Token: 0x04001E3F RID: 7743
		private const int DefaultAdvModeMinWidth = 700;

		// Token: 0x04001E40 RID: 7744
		private const int DefaultAdvModeMinHeight = 450;

		// Token: 0x04001E41 RID: 7745
		private static readonly Size DefaultAdvModeMinSize = new Size(700, 450);

		// Token: 0x04001E42 RID: 7746
		private Panel parametersPanel = new Panel();

		// Token: 0x04001E43 RID: 7747
		private HtmlAnchor lnkMoreOption;

		// Token: 0x04001E44 RID: 7748
		private List<string> nonWritablePhraseNameList;

		// Token: 0x04001E45 RID: 7749
		private Properties propertiesControl;

		// Token: 0x04001E46 RID: 7750
		private bool supportAdvancedMode = true;

		// Token: 0x04001E47 RID: 7751
		private string[] defaultActions;

		// Token: 0x04001E48 RID: 7752
		private string[] defaultConditions;

		// Token: 0x04001E49 RID: 7753
		private Dictionary<string, string> defaultValues;

		// Token: 0x04001E4A RID: 7754
		protected Panel evenMoreOptionsPanel = new Panel();

		// Token: 0x04001E4B RID: 7755
		protected Panel staticOptionsPanel = new Panel();
	}
}
