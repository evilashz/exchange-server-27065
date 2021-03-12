using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200029B RID: 667
	internal class AnonymousSessionContext : ISessionContext
	{
		// Token: 0x060019A4 RID: 6564 RVA: 0x00095B10 File Offset: 0x00093D10
		public static string GetEscapedPathFromUri(Uri uri)
		{
			string pathAndQuery = uri.PathAndQuery;
			int length = pathAndQuery.LastIndexOf('/');
			return pathAndQuery.Substring(0, length);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x00095B38 File Offset: 0x00093D38
		public AnonymousSessionContext(OwaContext owaContext)
		{
			this.owaContext = owaContext;
			this.PublishingUrl = (PublishingUrl)owaContext.HttpContext.Items["AnonymousUserContextPublishedUrl"];
			HttpCookie httpCookie = owaContext.HttpContext.Request.Cookies["timezone"];
			ExTimeZone exTimeZone;
			if (httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value) && ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName(httpCookie.Value, out exTimeZone))
			{
				this.timeZone = exTimeZone;
				this.IsTimeZoneFromCookie = true;
				return;
			}
			this.timeZone = ExTimeZone.CurrentTimeZone;
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00095BCB File Offset: 0x00093DCB
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x00095BD3 File Offset: 0x00093DD3
		public PublishingUrl PublishingUrl { get; private set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x00095BDC File Offset: 0x00093DDC
		public bool IsRtl
		{
			get
			{
				return Culture.IsRtl;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x00095BE3 File Offset: 0x00093DE3
		public Theme Theme
		{
			get
			{
				return ThemeManager.GetDefaultTheme(OwaConfigurationManager.Configuration.DefaultTheme);
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x00095BF4 File Offset: 0x00093DF4
		public bool IsBasicExperience
		{
			get
			{
				return string.Compare("Basic", this.experiences[0].Name, StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x00095C11 File Offset: 0x00093E11
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x00095C19 File Offset: 0x00093E19
		public Experience[] Experiences
		{
			get
			{
				return this.experiences;
			}
			set
			{
				if (this.experiences == null)
				{
					this.experiences = value;
					return;
				}
				throw new InvalidOperationException("Experiences can only be initialized once");
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00095C35 File Offset: 0x00093E35
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x00095C42 File Offset: 0x00093E42
		public CultureInfo UserCulture
		{
			get
			{
				return this.owaContext.Culture;
			}
			set
			{
				this.owaContext.Culture = value;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x00095C50 File Offset: 0x00093E50
		public ulong SegmentationFlags
		{
			get
			{
				return OwaConfigurationManager.Configuration.SegmentationFlags;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x00095C5C File Offset: 0x00093E5C
		public BrowserType BrowserType
		{
			get
			{
				BrowserType browserType = Utilities.GetBrowserType(this.owaContext.HttpContext.Request.UserAgent);
				if (browserType == BrowserType.Other)
				{
					browserType = BrowserType.Safari;
				}
				return browserType;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00095C8B File Offset: 0x00093E8B
		public bool IsExplicitLogon
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x00095C8E File Offset: 0x00093E8E
		public bool IsWebPartRequest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00095C91 File Offset: 0x00093E91
		public bool IsProxy
		{
			get
			{
				return this.owaContext.RequestExecution != RequestExecution.Local;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00095CA4 File Offset: 0x00093EA4
		public string Canary
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x00095CAB File Offset: 0x00093EAB
		public int LogonAndErrorLanguage
		{
			get
			{
				return OwaConfigurationManager.Configuration.LogonAndErrorLanguage;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00095CB7 File Offset: 0x00093EB7
		public DayOfWeek WeekStartDay
		{
			get
			{
				return DayOfWeek.Sunday;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x00095CBA File Offset: 0x00093EBA
		public WorkingHours WorkingHours
		{
			get
			{
				return WorkingHours.CreateFromAvailabilityWorkingHours(this, null);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00095CC3 File Offset: 0x00093EC3
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00095CCB File Offset: 0x00093ECB
		public ExTimeZone TimeZone
		{
			get
			{
				return this.timeZone;
			}
			set
			{
				this.timeZone = value;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00095CD4 File Offset: 0x00093ED4
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x00095CDC File Offset: 0x00093EDC
		public bool IsTimeZoneFromCookie { get; private set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00095CE5 File Offset: 0x00093EE5
		public string DateFormat
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00095CF4 File Offset: 0x00093EF4
		public string GetWeekdayDateFormat(bool useFullWeekdayFormat)
		{
			string str = useFullWeekdayFormat ? "dddd" : "ddd";
			return str + " " + this.DateFormat;
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x00095D22 File Offset: 0x00093F22
		public string TimeFormat
		{
			get
			{
				return DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060019BF RID: 6591 RVA: 0x00095D2E File Offset: 0x00093F2E
		public bool HideMailTipsByDefault
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x00095D31 File Offset: 0x00093F31
		public bool ShowWeekNumbers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x00095D34 File Offset: 0x00093F34
		public CalendarWeekRule FirstWeekOfYear
		{
			get
			{
				return CalendarWeekRule.FirstDay;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x00095D37 File Offset: 0x00093F37
		public bool CanActAsOwner
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x00095D3A File Offset: 0x00093F3A
		public int HourIncrement
		{
			get
			{
				return 30;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x00095D3E File Offset: 0x00093F3E
		public bool IsSmsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x00095D41 File Offset: 0x00093F41
		public bool IsReplyByPhoneEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x00095D44 File Offset: 0x00093F44
		public string CalendarFolderOwaIdString
		{
			get
			{
				return "PublishedCalendar";
			}
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00095D4B File Offset: 0x00093F4B
		public bool IsInstantMessageEnabled()
		{
			return false;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00095D4E File Offset: 0x00093F4E
		public bool IsPublicRequest(HttpRequest request)
		{
			return true;
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00095D51 File Offset: 0x00093F51
		public void RenderCssLink(TextWriter writer, HttpRequest request)
		{
			SessionContextUtilities.RenderCssLink(writer, request, this);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x00095D5B File Offset: 0x00093F5B
		public void RenderThemeFileUrl(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderThemeFileUrl(writer, themeFileId, this);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x00095D65 File Offset: 0x00093F65
		public void RenderThemeFileUrl(TextWriter writer, int themeFileIndex)
		{
			SessionContextUtilities.RenderThemeFileUrl(writer, themeFileIndex, this);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00095D6F File Offset: 0x00093F6F
		public void RenderThemeImage(StringBuilder builder, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImage(builder, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x00095D7C File Offset: 0x00093F7C
		public void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderThemeImage(writer, themeFileId, this);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00095D86 File Offset: 0x00093F86
		public void RenderThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImage(writer, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00095D93 File Offset: 0x00093F93
		public void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderBaseThemeImage(writer, themeFileId, this);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00095D9D File Offset: 0x00093F9D
		public void RenderBaseThemeImage(TextWriter writer, ThemeFileId themeFileId, string styleClass, params object[] extraAttributes)
		{
			SessionContextUtilities.RenderBaseThemeImage(writer, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00095DAA File Offset: 0x00093FAA
		public void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, params string[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageWithToolTip(writer, themeFileId, styleClass, this, extraAttributes);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00095DB7 File Offset: 0x00093FB7
		public void RenderThemeImageWithToolTip(TextWriter writer, ThemeFileId themeFileId, string styleClass, Strings.IDs tooltipStringId, params string[] extraAttributes)
		{
			SessionContextUtilities.RenderThemeImageWithToolTip(writer, themeFileId, styleClass, tooltipStringId, this, extraAttributes);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00095DC6 File Offset: 0x00093FC6
		public void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, this);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x00095DD1 File Offset: 0x00093FD1
		public void RenderThemeImageStart(TextWriter writer, ThemeFileId themeFileId, string styleClass, bool renderBaseTheme)
		{
			SessionContextUtilities.RenderThemeImageStart(writer, themeFileId, styleClass, renderBaseTheme, this);
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00095DDE File Offset: 0x00093FDE
		public void RenderThemeImageEnd(TextWriter writer, ThemeFileId themeFileId)
		{
			SessionContextUtilities.RenderThemeImageEnd(writer, themeFileId);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x00095DE7 File Offset: 0x00093FE7
		public string GetThemeFileUrl(ThemeFileId themeFileId)
		{
			return SessionContextUtilities.GetThemeFileUrl(themeFileId, this);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00095DF0 File Offset: 0x00093FF0
		public void RenderCssFontThemeFileUrl(TextWriter writer)
		{
			SessionContextUtilities.RenderCssFontThemeFileUrl(writer, this);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00095DF9 File Offset: 0x00093FF9
		public bool IsFeatureEnabled(Feature feature)
		{
			return this.AreFeaturesEnabled((ulong)((uint)feature));
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00095E04 File Offset: 0x00094004
		public bool AreFeaturesEnabled(ulong features)
		{
			return (features & this.SegmentationFlags) == features;
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x00095E11 File Offset: 0x00094011
		public string EscapedPath
		{
			get
			{
				if (this.escapedPath == null)
				{
					this.escapedPath = AnonymousSessionContext.GetEscapedPathFromUri(this.PublishingUrl.Uri);
				}
				return this.escapedPath;
			}
		}

		// Token: 0x040012C2 RID: 4802
		public const string PublishedCalendarIdentity = "PublishedCalendar";

		// Token: 0x040012C3 RID: 4803
		public const string TimeZoneCookieName = "timezone";

		// Token: 0x040012C4 RID: 4804
		private OwaContext owaContext;

		// Token: 0x040012C5 RID: 4805
		private Experience[] experiences;

		// Token: 0x040012C6 RID: 4806
		private ExTimeZone timeZone;

		// Token: 0x040012C7 RID: 4807
		private string escapedPath;
	}
}
