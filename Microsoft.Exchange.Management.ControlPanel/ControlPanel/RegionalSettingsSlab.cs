using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A7 RID: 167
	public class RegionalSettingsSlab : SlabControl
	{
		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00057DDB File Offset: 0x00055FDB
		public static Dictionary<string, Dictionary<string, string>> LanguageDateSets
		{
			get
			{
				return RegionalSettingsSlab.languageDateSets;
			}
		}

		// Token: 0x170018D0 RID: 6352
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x00057DE2 File Offset: 0x00055FE2
		public static Dictionary<string, Dictionary<string, string>> LanguageTimeSets
		{
			get
			{
				return RegionalSettingsSlab.languageTimeSets;
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00057DEC File Offset: 0x00055FEC
		static RegionalSettingsSlab()
		{
			foreach (CultureInfo cultureInfo in RegionalSettingsSlab.supportedCultureInfos)
			{
				RegionalSettingsSlab.supportedLanguages.Add(cultureInfo.NativeName, cultureInfo.LCID);
				cultureInfo.DateTimeFormat.Calendar = new GregorianCalendar();
				RegionalSettingsSlab.languageDateSets.Add(cultureInfo.LCID.ToString(), RegionalSettingsSlab.GetDateFormatsInCulture(cultureInfo));
				RegionalSettingsSlab.languageTimeSets.Add(cultureInfo.LCID.ToString(), RegionalSettingsSlab.GetTimeFormatsInCulture(cultureInfo));
			}
			if (RegionalSettingsSlab.supportedLanguages.Contains(RegionalSettingsSlab.cultureInfoForSrCyrlCS.NativeName))
			{
				RegionalSettingsSlab.supportedLanguages.Remove(RegionalSettingsSlab.cultureInfoForSrCyrlCS.NativeName);
				RegionalSettingsSlab.supportedLanguages.Add("српски (ћирилица, Србија и Црна Гора (бивша))", RegionalSettingsSlab.cultureInfoForSrCyrlCS.LCID);
			}
			if (RegionalSettingsSlab.supportedLanguages.Contains(RegionalSettingsSlab.cultureInfoForSrLatnCS.NativeName))
			{
				RegionalSettingsSlab.supportedLanguages.Remove(RegionalSettingsSlab.cultureInfoForSrLatnCS.NativeName);
				RegionalSettingsSlab.supportedLanguages.Add("srpski (latinica, Srbija i Crna Gora (bivša))", RegionalSettingsSlab.cultureInfoForSrLatnCS.LCID);
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00057FBC File Offset: 0x000561BC
		private static Dictionary<string, string> GetDateFormatsInCulture(CultureInfo culture)
		{
			string arg = culture.TextInfo.IsRightToLeft ? RtlUtil.DecodedRtlDirectionMark : RtlUtil.DecodedLtrDirectionMark;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in culture.DateTimeFormat.GetAllDateTimePatterns('d'))
			{
				if (!dictionary.ContainsKey(text))
				{
					dictionary.Add(text, string.Format("{0}{1}{0}", arg, RegionalSettingsSlab.sampleDate.ToString(text, culture)));
				}
			}
			return dictionary;
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00058038 File Offset: 0x00056238
		private static Dictionary<string, string> GetTimeFormatsInCulture(CultureInfo culture)
		{
			string arg = culture.TextInfo.IsRightToLeft ? RtlUtil.DecodedRtlDirectionMark : RtlUtil.DecodedLtrDirectionMark;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (string text in culture.DateTimeFormat.GetAllDateTimePatterns('t'))
			{
				if (!dictionary.ContainsKey(text))
				{
					dictionary.Add(text, string.Format("{0}{1}{0} - {0}{2}{0}", arg, RegionalSettingsSlab.sampleStartTime.ToString(text, culture), RegionalSettingsSlab.sampleEndTime.ToString(text, culture)));
				}
			}
			return dictionary;
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x000580C0 File Offset: 0x000562C0
		protected void Page_Load(object sender, EventArgs e)
		{
			bool isRtl = RtlUtil.IsRtl;
			for (int i = 0; i < RegionalSettingsSlab.supportedLanguages.Count; i++)
			{
				this.ddlLanguage.Items.Add(new ListItem(RtlUtil.ConvertToDecodedBidiString(RegionalSettingsSlab.supportedLanguages.GetKey(i).ToString(), isRtl), RegionalSettingsSlab.supportedLanguages.GetByIndex(i).ToString()));
			}
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				string text = RtlUtil.ConvertToDecodedBidiString(exTimeZone.LocalizableDisplayName.ToString(CultureInfo.CurrentCulture), isRtl);
				this.ddlTimeZone.Items.Add(new ListItem(text, exTimeZone.Id));
			}
		}

		// Token: 0x04001B78 RID: 7032
		private const string SrLatnCSNativeNameSuffixedWithFormer = "srpski (latinica, Srbija i Crna Gora (bivša))";

		// Token: 0x04001B79 RID: 7033
		private const string SrCyrlCSNativeNameSuffixedWithFormer = "српски (ћирилица, Србија и Црна Гора (бивша))";

		// Token: 0x04001B7A RID: 7034
		protected DropDownList ddlLanguage;

		// Token: 0x04001B7B RID: 7035
		protected DropDownList ddlTimeZone;

		// Token: 0x04001B7C RID: 7036
		private static readonly List<CultureInfo> supportedCultureInfos = new List<CultureInfo>(LanguagePackInfo.GetInstalledLanguagePackSpecificCultures(LanguagePackType.Client));

		// Token: 0x04001B7D RID: 7037
		private static readonly Dictionary<string, Dictionary<string, string>> languageDateSets = new Dictionary<string, Dictionary<string, string>>();

		// Token: 0x04001B7E RID: 7038
		private static readonly Dictionary<string, Dictionary<string, string>> languageTimeSets = new Dictionary<string, Dictionary<string, string>>();

		// Token: 0x04001B7F RID: 7039
		private static SortedList supportedLanguages = new SortedList();

		// Token: 0x04001B80 RID: 7040
		private static DateTime sampleDate = new DateTime(2013, 9, 1);

		// Token: 0x04001B81 RID: 7041
		private static DateTime sampleStartTime = new DateTime(2013, 9, 1, 1, 1, 0, 0);

		// Token: 0x04001B82 RID: 7042
		private static DateTime sampleEndTime = new DateTime(2013, 9, 1, 23, 59, 0, 0);

		// Token: 0x04001B83 RID: 7043
		private static CultureInfo cultureInfoForSrCyrlCS = new CultureInfo("sr-Cyrl-CS");

		// Token: 0x04001B84 RID: 7044
		private static CultureInfo cultureInfoForSrLatnCS = new CultureInfo("sr-Latn-CS");
	}
}
