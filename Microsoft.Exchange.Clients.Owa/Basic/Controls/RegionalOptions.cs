using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000084 RID: 132
	public class RegionalOptions : OptionsBase
	{
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00020DE8 File Offset: 0x0001EFE8
		private DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				return this.cultureInfo.DateTimeFormat;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00020DF5 File Offset: 0x0001EFF5
		public RegionalOptions(OwaContext owaContext, TextWriter writer) : base(owaContext, writer)
		{
			this.CommitAndLoad();
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00020E10 File Offset: 0x0001F010
		private void Load()
		{
			this.cultureInfo = Thread.CurrentThread.CurrentUICulture;
			this.currentCultureLCID = this.cultureInfo.LCID.ToString();
			if (Utilities.IsPostRequest(this.request) && base.Command != null && base.Command.Equals("chglng", StringComparison.Ordinal))
			{
				string formParameter = Utilities.GetFormParameter(this.request, "selLng", false);
				if (!string.IsNullOrEmpty(formParameter))
				{
					if (Culture.IsSupportedCulture(int.Parse(formParameter)))
					{
						this.cultureInfo = Culture.GetCultureInfoInstance(int.Parse(formParameter));
						return;
					}
					ExTraceGlobals.UserOptionsDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Attempted to update date/time styles with unsupported culture (LCID: {0})", formParameter);
				}
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		private void CommitAndLoad()
		{
			this.Load();
			bool flag = false;
			if (Utilities.IsPostRequest(this.request) && base.Command != null && base.Command.Equals("save", StringComparison.Ordinal))
			{
				flag = true;
				string formParameter = Utilities.GetFormParameter(this.request, "selLng", false);
				if (!string.IsNullOrEmpty(formParameter))
				{
					if (!Culture.IsSupportedCulture(int.Parse(formParameter)))
					{
						ExTraceGlobals.CoreTracer.TraceDebug<string>((long)this.GetHashCode(), "Attempted to set user culture to unsupported culture (LCID: {0})", formParameter);
						throw new OwaEventHandlerException("Regional options could not be saved due to an invalid language parameter.", LocalizedStrings.GetNonEncoded(1073923836));
					}
					this.cultureInfo = Culture.GetCultureInfoInstance(int.Parse(formParameter));
					this.currentCultureLCID = this.cultureInfo.LCID.ToString();
					Culture.UpdateUserCulture(this.userContext, this.cultureInfo);
				}
				string formParameter2 = Utilities.GetFormParameter(this.request, "selDtStl", false);
				if (!string.IsNullOrEmpty(formParameter2))
				{
					this.userContext.UserOptions.DateFormat = formParameter2;
					this.userContext.MailboxSession.PreferedCulture.DateTimeFormat.ShortDatePattern = formParameter2;
				}
				string formParameter3 = Utilities.GetFormParameter(this.request, "selTmStl", false);
				if (!string.IsNullOrEmpty(formParameter3))
				{
					this.userContext.UserOptions.TimeFormat = formParameter3;
					this.userContext.MailboxSession.PreferedCulture.DateTimeFormat.ShortTimePattern = formParameter3;
				}
				string formParameter4 = Utilities.GetFormParameter(this.request, "selTmZn", false);
				if (!string.IsNullOrEmpty(formParameter4))
				{
					this.userContext.UserOptions.TimeZone = formParameter4;
				}
				DateTimeUtilities.SetSessionTimeZone(this.userContext);
			}
			if (flag)
			{
				try
				{
					this.userContext.UserOptions.CommitChanges();
					base.SetSavedSuccessfully(true);
					this.Load();
				}
				catch (StoragePermanentException)
				{
					base.SetSavedSuccessfully(false);
				}
				catch (StorageTransientException)
				{
					base.SetSavedSuccessfully(false);
				}
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000210B0 File Offset: 0x0001F2B0
		public override void Render()
		{
			this.RenderLanguageOptions();
			this.RenderDateTimeFormats();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000210C0 File Offset: 0x0001F2C0
		public override void RenderScript()
		{
			base.RenderJSVariable("a_iLng", this.currentCultureLCID);
			base.RenderJSVariableWithQuotes("a_sDtStl", this.userContext.UserOptions.DateFormat);
			base.RenderJSVariableWithQuotes("a_sTmStl", this.userContext.UserOptions.TimeFormat);
			base.RenderJSVariableWithQuotes("a_iTmZn", this.userContext.UserOptions.TimeZone);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00021130 File Offset: 0x0001F330
		private void RenderLanguageOptions()
		{
			base.RenderHeaderRow(ThemeFileId.Globe, 468777496);
			this.writer.Write("<tr><td class=\"bd lrgLn\">");
			this.writer.Write("<div>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1882021095));
			this.writer.Write("</div>");
			this.writer.Write("<div>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-833638460));
			this.RenderLanguageSelection();
			this.writer.Write("</div>");
			this.writer.Write("<div>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(647522285));
			this.writer.Write("</div>");
			this.writer.Write("</td></tr>");
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00021210 File Offset: 0x0001F410
		protected void RenderLanguageSelection()
		{
			this.writer.Write("<select name=\"{0}\"{1}{2}>", "selLng", " class=\"rs padLR multiLanguangeChar\"", " onchange=\"return onChgLng(this);\"");
			CultureInfo[] supportedCultures = Culture.GetSupportedCultures(true);
			for (int i = 0; i < supportedCultures.Length; i++)
			{
				this.writer.Write("<option value=\"");
				this.writer.Write(supportedCultures[i].LCID);
				this.writer.Write('"');
				if (supportedCultures[i].LCID == this.cultureInfo.LCID)
				{
					this.writer.Write(" selected");
				}
				this.writer.Write('>');
				Utilities.HtmlEncode(supportedCultures[i].NativeName, this.writer);
				this.writer.Write("</option>");
			}
			this.writer.Write("</select>");
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000212EC File Offset: 0x0001F4EC
		private void RenderDateTimeFormats()
		{
			base.RenderHeaderRow(ThemeFileId.DateBook, -1860736294);
			this.writer.Write("<tr><td class=\"bd\">");
			this.writer.Write("<table class=\"fmt\">");
			this.writer.Write("<tr>");
			this.writer.Write("<td nowrap>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1176481198));
			this.writer.Write("</td>");
			this.writer.Write("<td>");
			this.RenderDateStyleSelection();
			this.writer.Write("</td>");
			this.writer.Write("<td class=\"w100\">");
			this.RenderCalendarOptions();
			this.writer.Write("</td>");
			this.writer.Write("</tr>");
			this.writer.Write("<tr>");
			this.writer.Write("<td nowrap>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(1486945283));
			this.writer.Write("</td>");
			this.writer.Write("<td colspan=2 class=\"w100\">");
			this.RenderTimeStyleSelection();
			this.writer.Write("</td>");
			this.writer.Write("</tr>");
			this.writer.Write("<tr>");
			this.writer.Write("<td nowrap>");
			this.writer.Write(LocalizedStrings.GetHtmlEncoded(-1147623709));
			this.writer.Write("</td>");
			this.writer.Write("<td colspan=2 class=\"w100\">");
			this.RenderTimeZoneSelection();
			this.writer.Write("</td>");
			this.writer.Write("</tr>");
			this.writer.Write("</table>");
			this.writer.Write("</td></tr>");
			if (this.userContext.WorkingHours.IsTimeZoneDifferent && this.userContext.IsFeatureEnabled(Feature.Calendar))
			{
				this.writer.Write("<tr><td id=\"tdTZNt\">");
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(566587615));
				this.writer.Write("</td></tr>");
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00021533 File Offset: 0x0001F733
		protected void RenderCalendarOptions()
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00021538 File Offset: 0x0001F738
		protected void RenderDateStyleSelection()
		{
			string[] allDateTimePatterns = this.DateTimeFormat.GetAllDateTimePatterns('d');
			this.writer.Write("<select name=\"{0}\"{1}{2}>", "selDtStl", " class=\"rs padLR\"", " onchange=\"return onChgSel(this);\"");
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.writer.Write("<option value=\"");
				Utilities.HtmlEncode(allDateTimePatterns[i], this.writer);
				this.writer.Write('"');
				if (this.userContext.UserOptions.DateFormat == allDateTimePatterns[i])
				{
					this.writer.Write(" selected");
				}
				this.writer.Write('>');
				Utilities.HtmlEncode(DateTimeUtilities.ExampleDate.ToString(allDateTimePatterns[i], this.DateTimeFormat), this.writer);
				this.writer.Write("</option>");
			}
			this.writer.Write("</select>");
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0002162C File Offset: 0x0001F82C
		protected void RenderTimeStyleSelection()
		{
			string[] allDateTimePatterns = this.DateTimeFormat.GetAllDateTimePatterns('t');
			ExDateTime exDateTime = new ExDateTime(this.userContext.TimeZone, 2008, 1, 21, 1, 1, 0);
			ExDateTime exDateTime2 = new ExDateTime(this.userContext.TimeZone, 2008, 1, 21, 23, 59, 0);
			this.writer.Write("<select name=\"{0}\"{1}{2}>", "selTmStl", " class=\"rs padLR\"", " onchange=\"return onChgSel(this);\"");
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.writer.Write("<option value=\"");
				Utilities.HtmlEncode(allDateTimePatterns[i], this.writer);
				this.writer.Write('"');
				if (allDateTimePatterns[i] == this.userContext.UserOptions.TimeFormat)
				{
					this.writer.Write(" selected");
				}
				this.writer.Write('>');
				this.writer.Write(LocalizedStrings.GetHtmlEncoded(466973109), Utilities.HtmlEncode(exDateTime.ToString(allDateTimePatterns[i], this.DateTimeFormat)), Utilities.HtmlEncode(exDateTime2.ToString(allDateTimePatterns[i], this.DateTimeFormat)));
				this.writer.Write("</option>");
			}
			this.writer.Write("</select>");
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00021778 File Offset: 0x0001F978
		protected void RenderTimeZoneSelection()
		{
			this.writer.Write("<select name=\"{0}\"{1}{2}>", "selTmZn", " class=\"padLR\"", " onchange=\"return onChgSel(this);\"");
			foreach (ExTimeZone exTimeZone in ExTimeZoneEnumerator.Instance)
			{
				this.writer.Write("<option value=\"");
				Utilities.HtmlEncode(exTimeZone.Id, this.writer);
				this.writer.Write('"');
				if (string.Equals(exTimeZone.Id, this.userContext.UserOptions.TimeZone, StringComparison.OrdinalIgnoreCase))
				{
					this.writer.Write(" selected");
				}
				this.writer.Write('>');
				Utilities.HtmlEncode(exTimeZone.LocalizableDisplayName.ToString(Thread.CurrentThread.CurrentUICulture), this.writer);
				this.writer.Write("</option>");
			}
			this.writer.Write("</select>");
		}

		// Token: 0x040002DC RID: 732
		private const string SelectText = "<select name=\"{0}\"{1}{2}>";

		// Token: 0x040002DD RID: 733
		private const string FormLanguageSelection = "selLng";

		// Token: 0x040002DE RID: 734
		private const string FormDateSelection = "selDtStl";

		// Token: 0x040002DF RID: 735
		private const string FormTimeSelection = "selTmStl";

		// Token: 0x040002E0 RID: 736
		private const string FormTimeZoneSelection = "selTmZn";

		// Token: 0x040002E1 RID: 737
		private const string FormJavaScriptLanguageSelection = "a_iLng";

		// Token: 0x040002E2 RID: 738
		private const string FormJavaScriptDateSelection = "a_sDtStl";

		// Token: 0x040002E3 RID: 739
		private const string FormJavaScriptTimeSelection = "a_sTmStl";

		// Token: 0x040002E4 RID: 740
		private const string FormJavaScriptTimeZoneSelection = "a_iTmZn";

		// Token: 0x040002E5 RID: 741
		private const string ChangeLanguageCommand = "chglng";

		// Token: 0x040002E6 RID: 742
		private const string SaveCommand = "save";

		// Token: 0x040002E7 RID: 743
		private CultureInfo cultureInfo;

		// Token: 0x040002E8 RID: 744
		private string currentCultureLCID = string.Empty;
	}
}
