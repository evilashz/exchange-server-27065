using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000082 RID: 130
	public class GlobalSettingsResult
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x00026544 File Offset: 0x00024744
		public static GlobalSettingsResult Create(string propertyNameFilter, bool returnOnlySettingsThatAreNotDefault)
		{
			GlobalSettingsResult globalSettingsResult = new GlobalSettingsResult();
			List<GlobalSettingsResultItem> list = new List<GlobalSettingsResultItem>();
			Regex regex = null;
			if (propertyNameFilter != null)
			{
				string pattern = string.Format("^{0}$", Regex.Escape(propertyNameFilter).Replace("\\*", ".*").Replace("\\?", "."));
				regex = new Regex(pattern);
			}
			IList<GlobalSettingsPropertyDefinition> allProperties = GlobalSettingsSchema.AllProperties;
			foreach (GlobalSettingsPropertyDefinition globalSettingsPropertyDefinition in allProperties)
			{
				if (regex == null || regex.Match(globalSettingsPropertyDefinition.Name).Success)
				{
					object setting = GlobalSettings.GetSetting(globalSettingsPropertyDefinition);
					string valueString = GlobalSettingsResult.GetValueString(setting);
					string valueString2 = GlobalSettingsResult.GetValueString(globalSettingsPropertyDefinition.DefaultValue);
					if (!returnOnlySettingsThatAreNotDefault || valueString != valueString2)
					{
						list.Add(new GlobalSettingsResultItem(globalSettingsPropertyDefinition.Name, globalSettingsPropertyDefinition.Type.Name, GlobalSettingsResult.GetValueString(setting), GlobalSettingsResult.GetValueString(globalSettingsPropertyDefinition.DefaultValue)));
					}
				}
			}
			globalSettingsResult.Entries = list.ToArray();
			return globalSettingsResult;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00026660 File Offset: 0x00024860
		private static string GetValueString(object value)
		{
			if (value == null)
			{
				return "$null";
			}
			IList<string> list = value as IList<string>;
			if (list != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				foreach (string value2 in list)
				{
					if (!flag)
					{
						stringBuilder.Append(", ");
					}
					else
					{
						flag = false;
					}
					stringBuilder.Append(value2);
				}
				return stringBuilder.ToString();
			}
			return value.ToString();
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x000266F4 File Offset: 0x000248F4
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x000266FC File Offset: 0x000248FC
		[XmlArrayItem("AppSetting")]
		[XmlArray("AppSettings")]
		public GlobalSettingsResultItem[] Entries { get; set; }
	}
}
