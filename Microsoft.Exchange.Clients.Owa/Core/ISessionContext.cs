using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200014D RID: 333
	public interface ISessionContext
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B51 RID: 2897
		bool IsRtl { get; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B52 RID: 2898
		Theme Theme { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B53 RID: 2899
		bool IsBasicExperience { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000B54 RID: 2900
		// (set) Token: 0x06000B55 RID: 2901
		CultureInfo UserCulture { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000B56 RID: 2902
		ulong SegmentationFlags { get; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000B57 RID: 2903
		BrowserType BrowserType { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000B58 RID: 2904
		bool IsExplicitLogon { get; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B59 RID: 2905
		bool IsWebPartRequest { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000B5A RID: 2906
		bool IsProxy { get; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000B5B RID: 2907
		string Canary { get; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000B5C RID: 2908
		int LogonAndErrorLanguage { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000B5D RID: 2909
		DayOfWeek WeekStartDay { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000B5E RID: 2910
		WorkingHours WorkingHours { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000B5F RID: 2911
		ExTimeZone TimeZone { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000B60 RID: 2912
		string DateFormat { get; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000B61 RID: 2913
		bool HideMailTipsByDefault { get; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000B62 RID: 2914
		bool ShowWeekNumbers { get; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000B63 RID: 2915
		CalendarWeekRule FirstWeekOfYear { get; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000B64 RID: 2916
		Experience[] Experiences { get; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000B65 RID: 2917
		string TimeFormat { get; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000B66 RID: 2918
		bool CanActAsOwner { get; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000B67 RID: 2919
		int HourIncrement { get; }

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000B68 RID: 2920
		bool IsSmsEnabled { get; }

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000B69 RID: 2921
		bool IsReplyByPhoneEnabled { get; }

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000B6A RID: 2922
		string CalendarFolderOwaIdString { get; }

		// Token: 0x06000B6B RID: 2923
		string GetWeekdayDateFormat(bool useFullWeekdayFormat);

		// Token: 0x06000B6C RID: 2924
		bool IsInstantMessageEnabled();

		// Token: 0x06000B6D RID: 2925
		bool IsPublicRequest(HttpRequest request);

		// Token: 0x06000B6E RID: 2926
		void RenderCssLink(TextWriter writer, HttpRequest request);

		// Token: 0x06000B6F RID: 2927
		void RenderThemeFileUrl(TextWriter writer, ThemeFileId themeFileId);

		// Token: 0x06000B70 RID: 2928
		void RenderThemeFileUrl(TextWriter writer, int themeFileIndex);

		// Token: 0x06000B71 RID: 2929
		void RenderThemeImage(StringBuilder builder, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes);

		// Token: 0x06000B72 RID: 2930
		void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId);

		// Token: 0x06000B73 RID: 2931
		void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes);

		// Token: 0x06000B74 RID: 2932
		void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId);

		// Token: 0x06000B75 RID: 2933
		void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes);

		// Token: 0x06000B76 RID: 2934
		void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, params string[] extraAttributes);

		// Token: 0x06000B77 RID: 2935
		void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, Strings.IDs tooltipStringId, params string[] extraAttributes);

		// Token: 0x06000B78 RID: 2936
		void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass);

		// Token: 0x06000B79 RID: 2937
		void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass, bool renderBaseTheme);

		// Token: 0x06000B7A RID: 2938
		void RenderThemeImageEnd(TextWriter writer, ThemeFileId themeFileId);

		// Token: 0x06000B7B RID: 2939
		string GetThemeFileUrl(ThemeFileId themeFileId);

		// Token: 0x06000B7C RID: 2940
		void RenderCssFontThemeFileUrl(TextWriter writer);

		// Token: 0x06000B7D RID: 2941
		bool IsFeatureEnabled(Feature feature);

		// Token: 0x06000B7E RID: 2942
		bool AreFeaturesEnabled(ulong features);
	}
}
