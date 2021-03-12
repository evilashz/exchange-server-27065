using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200063F RID: 1599
	public class RecipientConditionEditor : RecipientConditionEditorBase
	{
		// Token: 0x17002706 RID: 9990
		// (get) Token: 0x06004619 RID: 17945 RVA: 0x000D3C58 File Offset: 0x000D1E58
		protected override RulePhrase[] SupportedConditions
		{
			get
			{
				return RecipientConditionEditor.featureSets.GetOrAdd(this.FeatureSet.ToString(), delegate(string key)
				{
					RulePhrase[] result = RecipientConditionEditor.allSupportedRules;
					if (key != null)
					{
						if (!(key == "ExcludeRecipientContainer"))
						{
							if (!(key == "All"))
							{
								goto IL_56;
							}
						}
						else
						{
							result = (from rule in RecipientConditionEditor.allSupportedRules
							where !string.Equals(rule.Name, "RecipientContainer")
							select rule).ToArray<RulePhrase>();
						}
						return result;
					}
					IL_56:
					throw new InvalidOperationException("Unknown Feature Set: " + this.FeatureSet.ToString());
				});
			}
		}

		// Token: 0x17002707 RID: 9991
		// (get) Token: 0x0600461A RID: 17946 RVA: 0x000D3C80 File Offset: 0x000D1E80
		// (set) Token: 0x0600461B RID: 17947 RVA: 0x000D3C88 File Offset: 0x000D1E88
		[DefaultValue(RecipientConditionEditorFeatureSet.All)]
		public RecipientConditionEditorFeatureSet FeatureSet { get; set; }

		// Token: 0x04002F64 RID: 12132
		private static ConcurrentDictionary<string, RulePhrase[]> featureSets = new ConcurrentDictionary<string, RulePhrase[]>();

		// Token: 0x04002F65 RID: 12133
		private static RulePhrase[] allSupportedRules = new RulePhrase[]
		{
			new RulePhrase("RecipientContainer", Strings.RecipientContainerText, new FormletParameter[]
			{
				new OUPickerParameter("RecipientContainer", Strings.SelectOrganizationalUnitDialogLabel, Strings.SelectOrganizationalUnitDialogLabel, "~/DDI/DDIService.svc?schema=OrganizationalUnitPicker")
			}, null, false),
			new RulePhrase("ConditionalStateOrProvince", Strings.ConditionalStateOrProvinceText, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalStateOrProvince", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCompany", Strings.ConditionalCompanyText, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCompany", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalDepartment", Strings.ConditionalDepartmentText, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalDepartment", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute1", Strings.ConditionalCustomAttribute1Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute1", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute2", Strings.ConditionalCustomAttribute2Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute2", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute3", Strings.ConditionalCustomAttribute3Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute3", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute4", Strings.ConditionalCustomAttribute4Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute4", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute5", Strings.ConditionalCustomAttribute5Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute5", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute6", Strings.ConditionalCustomAttribute6Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute6", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute7", Strings.ConditionalCustomAttribute7Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute7", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute8", Strings.ConditionalCustomAttribute8Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute8", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute9", Strings.ConditionalCustomAttribute9Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute9", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute10", Strings.ConditionalCustomAttribute10Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute10", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute11", Strings.ConditionalCustomAttribute11Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute11", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute12", Strings.ConditionalCustomAttribute12Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute12", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute13", Strings.ConditionalCustomAttribute13Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute13", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute14", Strings.ConditionalCustomAttribute14Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute14", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false),
			new RulePhrase("ConditionalCustomAttribute15", Strings.ConditionalCustomAttribute15Text, new FormletParameter[]
			{
				new StringArrayParameter("ConditionalCustomAttribute15", Strings.StringArrayDialogTitle, Strings.StringArrayDialogLabel)
			}, null, false)
		};
	}
}
