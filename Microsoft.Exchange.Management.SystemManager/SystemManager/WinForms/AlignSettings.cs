using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A9 RID: 425
	public static class AlignSettings
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x00042258 File Offset: 0x00040458
		private static IList<IAlignRule> GetRulesList()
		{
			List<IAlignRule> list = new List<IAlignRule>();
			list.Add(new RelativeControlAlignRule(3)
			{
				Conditions = 
				{
					new RelativeControlAlignRule.Condition(-1, 0, new Type[]
					{
						typeof(RadioButton),
						typeof(CheckBox),
						typeof(CheckedTextBox)
					}),
					new RelativeControlAlignRule.Condition(0, 0, new Type[]
					{
						typeof(RadioButton),
						typeof(CheckBox),
						typeof(CheckedTextBox)
					})
				}
			});
			list.Add(new RelativeControlAlignRule(3)
			{
				Conditions = 
				{
					new RelativeControlAlignRule.Condition(-1, 0, new Type[]
					{
						typeof(Label),
						typeof(ToolStrip)
					}, new Type[]
					{
						typeof(AutoHeightLabel)
					}),
					new RelativeControlAlignRule.Condition(0, 0, new Type[0], new Type[]
					{
						typeof(Label),
						typeof(ExchangeSummaryControl)
					})
				}
			});
			RelativeControlAlignRule relativeControlAlignRule = new RelativeControlAlignRule(3);
			relativeControlAlignRule.Conditions.Add(new RelativeControlAlignRule.Condition(-2, 0, new Type[]
			{
				typeof(Label)
			}));
			ICollection<RelativeControlAlignRule.Condition> conditions = relativeControlAlignRule.Conditions;
			int row = -1;
			int column = 0;
			IList<Type> includedTypes = new Type[0];
			Type[] array = new Type[2];
			array[0] = typeof(Label);
			conditions.Add(new RelativeControlAlignRule.Condition(row, column, includedTypes, array));
			relativeControlAlignRule.Conditions.Add(new RelativeControlAlignRule.Condition(0, 0, new Type[]
			{
				typeof(Label)
			}));
			ICollection<RelativeControlAlignRule.Condition> conditions2 = relativeControlAlignRule.Conditions;
			int row2 = 1;
			int column2 = 0;
			Type[] array2 = new Type[2];
			array2[0] = typeof(Label);
			conditions2.Add(new RelativeControlAlignRule.Condition(row2, column2, array2));
			list.Add(relativeControlAlignRule);
			IAlignRule item = new IndentionRule(new Type[]
			{
				typeof(RadioButton),
				typeof(CheckBox),
				typeof(EnumToRadioButtonAdapter)
			});
			list.Add(item);
			list.Add(new DefaultAlignRule());
			IAlignRule item2 = new ConstLeftPaddingRule(new Type[]
			{
				typeof(Label),
				typeof(UserControl)
			});
			list.Add(item2);
			IAlignRule item3 = new ControlsPropertySettingsRule();
			list.Add(item3);
			return list;
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000424F1 File Offset: 0x000406F1
		static AlignSettings()
		{
			AlignSettings.InitializeMappingEntryList();
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0004250C File Offset: 0x0004070C
		public static AlignMappingEntry GetMappingEntry(Type type)
		{
			if (AlignSettings.typeMappingEntriesList.ContainsKey(type))
			{
				return AlignSettings.typeMappingEntriesList[type];
			}
			if (AlignSettings.nameMappingEntriesList.ContainsKey(type.FullName))
			{
				return AlignSettings.nameMappingEntriesList[type.FullName];
			}
			return AlignMappingEntry.Empty;
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0004255C File Offset: 0x0004075C
		private static void InitializeMappingEntryList()
		{
			AlignSettings.typeMappingEntriesList = new Dictionary<Type, AlignMappingEntry>();
			AlignSettings.typeMappingEntriesList.Add(typeof(Label), new AlignMappingEntry(13, new Padding(0, 0, 0, 0)));
			AlignSettings.typeMappingEntriesList.Add(typeof(AutoHeightLabel), new AlignMappingEntry(16, new Padding(0, 0, 0, 4), new Padding(0, 0, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(CheckBox), new AlignMappingEntry(17, new Padding(0, 1, 0, 3)));
			AlignSettings.typeMappingEntriesList.Add(typeof(AutoHeightCheckBox), new AlignMappingEntry(17, new Padding(0, 1, 0, 3)));
			AlignSettings.typeMappingEntriesList.Add(typeof(RadioButton), new AlignMappingEntry(17, new Padding(0, 1, 0, 3)));
			AlignSettings.typeMappingEntriesList.Add(typeof(AutoHeightRadioButton), new AlignMappingEntry(17, new Padding(0, 1, 0, 3)));
			AlignSettings.typeMappingEntriesList.Add(typeof(TextBox), new AlignMappingEntry(20, new Padding(0, 3, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(LabeledTextBox), new AlignMappingEntry(20, new Padding(0, 3, 0, 4), new Padding(0, 3, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(CheckedTextBox), new AlignMappingEntry(20, new Padding(0, 3, 0, 4), new Padding(0, 2, 0, 1)));
			AlignSettings.typeMappingEntriesList.Add(typeof(ExchangeTextBox), new AlignMappingEntry(20, new Padding(0, 3, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(NumericUpDown), new AlignMappingEntry(20, new Padding(0, 3, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(ExchangeNumericUpDown), new AlignMappingEntry(20, new Padding(0, 3, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(DomainUpDown), new AlignMappingEntry(20, new Padding(0, 3, 0, 4)));
			AlignSettings.typeMappingEntriesList.Add(typeof(ComboBox), new AlignMappingEntry(21, new Padding(0, 3, 0, 5)));
			AlignSettings.typeMappingEntriesList.Add(typeof(ExchangeComboBox), new AlignMappingEntry(21, new Padding(0, 3, 0, 5)));
			AlignSettings.typeMappingEntriesList.Add(typeof(ComboBoxPicker), new AlignMappingEntry(21, new Padding(0, 3, 0, 5)));
			AlignSettings.typeMappingEntriesList.Add(typeof(UserLogonNameControl), new AlignMappingEntry(21, new Padding(0, 3, 0, 5), new Padding(0, 0, 0, 1)));
			AlignSettings.typeMappingEntriesList.Add(typeof(DateTimePicker), new AlignMappingEntry(21, new Padding(0, 3, 0, 5)));
			AlignSettings.typeMappingEntriesList.Add(typeof(Button), new AlignMappingEntry(23, new Padding(0, 5, 0, 5)));
			AlignSettings.typeMappingEntriesList.Add(typeof(ExchangeButton), new AlignMappingEntry(23, new Padding(0, 5, 0, 5)));
			AlignSettings.typeMappingEntriesList.Add(typeof(PickerLauncherTextBox), new AlignMappingEntry(23, new Padding(0, 5, 0, 5), new Padding(0, 2, 0, 1)));
			AlignSettings.typeMappingEntriesList.Add(typeof(TabbableToolStrip), new AlignMappingEntry(25, new Padding(0, 6, 0, 6)));
			AlignSettings.nameMappingEntriesList = new Dictionary<string, AlignMappingEntry>();
			AlignSettings.nameMappingEntriesList.Add("Microsoft.Exchange.Management.SnapIn.Esm.OrganizationConfiguration.UnifiedMessaging.GreetingsSettingLaunchTextBox", AlignSettings.typeMappingEntriesList[typeof(PickerLauncherTextBox)]);
		}

		// Token: 0x04000680 RID: 1664
		public static IList<IAlignRule> RulesList = AlignSettings.GetRulesList();

		// Token: 0x04000681 RID: 1665
		public static int DefaultVertical = 12;

		// Token: 0x04000682 RID: 1666
		private static IDictionary<Type, AlignMappingEntry> typeMappingEntriesList;

		// Token: 0x04000683 RID: 1667
		private static IDictionary<string, AlignMappingEntry> nameMappingEntriesList;
	}
}
