using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000355 RID: 853
	public abstract class ListView : IListView
	{
		// Token: 0x0600202C RID: 8236 RVA: 0x000BA918 File Offset: 0x000B8B18
		protected ListView(UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, bool isFilteredView)
		{
			this.userContext = userContext;
			this.sortedColumn = sortedColumn;
			this.sortOrder = sortOrder;
			this.isFilteredView = isFilteredView;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000BA950 File Offset: 0x000B8B50
		protected ListView(UserContext userContext) : this(userContext, ColumnId.Count, SortOrder.Ascending, false)
		{
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600202E RID: 8238
		protected abstract ViewType ViewTypeId { get; }

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600202F RID: 8239
		protected abstract string EventNamespace { get; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x000BA95D File Offset: 0x000B8B5D
		public virtual string Cookie
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x000BA964 File Offset: 0x000B8B64
		public virtual int CookieLcid
		{
			get
			{
				return Culture.GetUserCulture().LCID;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x000BA970 File Offset: 0x000B8B70
		public virtual string PreferredDC
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x000BA977 File Offset: 0x000B8B77
		protected virtual bool IsSortable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x000BA97A File Offset: 0x000B8B7A
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000BA982 File Offset: 0x000B8B82
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x000BA98A File Offset: 0x000B8B8A
		protected ViewDescriptor ViewDescriptor
		{
			get
			{
				return this.viewDescriptor;
			}
			set
			{
				this.viewDescriptor = value;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x000BA993 File Offset: 0x000B8B93
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x000BA99B File Offset: 0x000B8B9B
		protected LegacyListViewContents Contents
		{
			get
			{
				return this.contents;
			}
			set
			{
				this.contents = value;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x000BA9A4 File Offset: 0x000B8BA4
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600203A RID: 8250 RVA: 0x000BA9AC File Offset: 0x000B8BAC
		protected ColumnId SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000BA9B4 File Offset: 0x000B8BB4
		protected virtual bool IsMultipleRequestAllowed
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x000BA9B7 File Offset: 0x000B8BB7
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x000BA9C4 File Offset: 0x000B8BC4
		public IListViewDataSource DataSource
		{
			get
			{
				return this.contents.DataSource;
			}
			set
			{
				if (!this.IsValidDataSource(value))
				{
					throw new ArgumentException("DataSource " + value + " cannot be used to render this view");
				}
				this.contents.DataSource = value;
			}
		}

		// Token: 0x0600203E RID: 8254
		protected abstract bool IsValidDataSource(IListViewDataSource dataSource);

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x000BA9F1 File Offset: 0x000B8BF1
		public Hashtable Properties
		{
			get
			{
				return this.contents.Properties;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x000BA9FE File Offset: 0x000B8BFE
		public bool IsFilteredView
		{
			get
			{
				return this.isFilteredView;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x000BAA06 File Offset: 0x000B8C06
		public int StartRange
		{
			get
			{
				if (this.DataSource == null)
				{
					return int.MinValue;
				}
				return this.DataSource.StartRange;
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x000BAA21 File Offset: 0x000B8C21
		public int EndRange
		{
			get
			{
				if (this.DataSource == null)
				{
					return int.MinValue;
				}
				return this.DataSource.EndRange;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x000BAA3C File Offset: 0x000B8C3C
		public int RangeCount
		{
			get
			{
				if (this.DataSource == null)
				{
					return 0;
				}
				return this.DataSource.RangeCount;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x000BAA53 File Offset: 0x000B8C53
		public int TotalCount
		{
			get
			{
				if (this.DataSource == null)
				{
					return int.MinValue;
				}
				return this.DataSource.TotalCount;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x000BAA6E File Offset: 0x000B8C6E
		public int UnreadCount
		{
			get
			{
				if (this.DataSource == null)
				{
					return int.MinValue;
				}
				return this.DataSource.UnreadCount;
			}
		}

		// Token: 0x06002046 RID: 8262
		protected abstract void InitializeListViewContents();

		// Token: 0x06002047 RID: 8263 RVA: 0x000BAA89 File Offset: 0x000B8C89
		protected void Initialize()
		{
			this.InitializeListViewContents();
		}

		// Token: 0x06002048 RID: 8264
		public abstract SortBy[] GetSortByProperties();

		// Token: 0x06002049 RID: 8265 RVA: 0x000BAA91 File Offset: 0x000B8C91
		public void Render(TextWriter writer)
		{
			this.Render(writer, ListView.RenderFlags.All);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000BAA9C File Offset: 0x000B8C9C
		public void RenderForCompactWebPart(TextWriter writer)
		{
			this.Render(writer, ListView.RenderFlags.All);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000BAAA8 File Offset: 0x000B8CA8
		public void Render(TextWriter writer, ListView.RenderFlags renderFlags)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListView.Render");
			if ((renderFlags & ListView.RenderFlags.Behavior) > (ListView.RenderFlags)0)
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Rendering ListView behavior");
				writer.Write("<div id=\"divLstV\" tabIndex=\"0\" hidefocus=\"true\"");
				if (this.userContext.IsRtl)
				{
					ListView.RenderBehaviorAttribute(writer, "rtl", 1);
					ListView.RenderBehaviorAttribute(writer, "class", "rtl");
				}
				ListView.RenderBehaviorAttribute(writer, "L_Ldng", LocalizedStrings.GetNonEncoded(-695375226));
				ListView.RenderBehaviorAttribute(writer, "L_Srchng", LocalizedStrings.GetNonEncoded(-1057914178));
				ListView.RenderBehaviorAttribute(writer, "sSid", this.DataSource.ContainerId);
				ListView.RenderBehaviorAttribute(writer, "iT", (int)this.ViewTypeId);
				if ((renderFlags & ListView.RenderFlags.CompactWebPart) > (ListView.RenderFlags)0)
				{
					ListView.RenderBehaviorAttribute(writer, "iWP", 1);
				}
				ListView.RenderBehaviorAttribute(writer, "sEvtNS", this.EventNamespace);
				ListView.RenderBehaviorAttribute(writer, "iTC", this.DataSource.TotalCount);
				ListView.RenderBehaviorAttribute(writer, "iUC", this.DataSource.UnreadCount);
				ListView.RenderBehaviorAttribute(writer, "iRC", this.userContext.UserOptions.ViewRowCount);
				ListView.RenderBehaviorAttribute(writer, "iML", 0);
				ListView.RenderBehaviorAttribute(writer, "iSC", (int)this.sortedColumn);
				ListView.RenderBehaviorAttribute(writer, "iSO", (this.sortOrder == SortOrder.Ascending) ? ListView.sortAscending : ListView.sortDescending);
				ListView.RenderBehaviorAttribute(writer, "fQR", (!this.IsMultipleRequestAllowed) ? 1 : 0);
				Column column = ListViewColumns.GetColumn(this.sortedColumn);
				ListView.RenderBehaviorAttribute(writer, "iTD", column.IsTypeDownCapable ? 1 : 0);
				ListView.RenderBehaviorAttribute(writer, "fSrt", this.IsSortable ? 1 : 0);
				ListView.RenderBehaviorAttribute(writer, "iNsDir", (int)this.userContext.UserOptions.NextSelection);
				ListView.RenderBehaviorAttribute(writer, "sCki", this.Cookie);
				ListView.RenderBehaviorAttribute(writer, "iLcid", this.CookieLcid);
				ListView.RenderBehaviorAttribute(writer, "sPfdDC", this.PreferredDC);
				ListView.RenderBehaviorAttribute(writer, "fROLv", (this.ViewTypeId == ViewType.Documents) ? 1 : 0);
				foreach (string text in this.extraAttributes.Keys)
				{
					ListView.RenderBehaviorAttribute(writer, text, this.extraAttributes[text]);
				}
				writer.Write(">");
				writer.Write("<table class=\"lyt\" cellpadding=\"0\"><tr><td id=\"tdHdr\"><table id=\"tblHdrLyt\" cellpadding=\"0\"><tr><td id=\"tdHdrCtnt\"><div id=\"divHS\">");
			}
			if ((renderFlags & ListView.RenderFlags.Headers) > (ListView.RenderFlags)0)
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Rendering ListView headers");
				LegacyListViewHeaders legacyListViewHeaders = new ColumnHeaders(this.viewDescriptor, this.sortedColumn, this.sortOrder, this.userContext);
				legacyListViewHeaders.Render(writer);
			}
			if ((renderFlags & ListView.RenderFlags.Behavior) > (ListView.RenderFlags)0)
			{
				writer.Write("</div></td>");
				writer.Write("<td id=\"tdFill\">&nbsp;</td>");
				writer.Write("</tr>");
				writer.Write("</table>");
				writer.Write("</td></tr>");
				if (this.HasInlineControl)
				{
					writer.Write("<tr><td id=tdILC>");
					this.RenderInlineControl(writer);
					writer.Write("</td></tr>");
				}
				writer.Write("<tr><td id=tdIL>");
				writer.Write("<div id=divPrgrs style=\"display:none\">");
				this.userContext.RenderThemeImage(writer, ThemeFileId.ProgressSmall);
				writer.Write(" <span id=spnTxt></span>");
				writer.Write("</div>");
			}
			if ((renderFlags & ListView.RenderFlags.Contents) > (ListView.RenderFlags)0)
			{
				ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Rendering ListView contents");
				writer.Write("<div id=divIL>");
				if (this.isFilteredView)
				{
					writer.Write("<div class=fltrBg></div>");
				}
				if (!this.DataSource.UserHasRightToLoad || this.DataSource.RangeCount == 0)
				{
					writer.Write("<div id=divNI>");
					if (!this.DataSource.UserHasRightToLoad)
					{
						writer.Write(LocalizedStrings.GetHtmlEncoded(-593658721));
					}
					else if (this.isFilteredView)
					{
						writer.Write(LocalizedStrings.GetHtmlEncoded(417836457));
					}
					else
					{
						writer.Write(LocalizedStrings.GetHtmlEncoded(-474826895));
					}
					writer.Write("</div>");
				}
				else
				{
					this.contents.Render(writer);
				}
				writer.Write("</div>");
			}
			if ((renderFlags & ListView.RenderFlags.Behavior) > (ListView.RenderFlags)0)
			{
				writer.Write("</td></tr>");
				if ((renderFlags & ListView.RenderFlags.Paging) > (ListView.RenderFlags)0)
				{
					writer.Write("<tr><td>");
				}
				else
				{
					writer.Write("<tr style=\"display:none\"><td>");
				}
				this.RenderPagingUI(writer);
				writer.Write("</td></tr>");
				writer.Write("</table>");
				writer.Write("</div>");
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000BAF44 File Offset: 0x000B9144
		private static void RenderBehaviorAttribute(TextWriter writer, string name, string value)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name cannot be null or empty.");
			}
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			writer.Write(" ");
			writer.Write(name);
			writer.Write("=\"");
			Utilities.HtmlEncode(value, writer);
			writer.Write("\"");
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000BAFAC File Offset: 0x000B91AC
		private static void RenderBehaviorAttribute(TextWriter writer, string name, int value)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name cannot be null or empty.");
			}
			writer.Write(" ");
			writer.Write(name);
			writer.Write("=");
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000BB009 File Offset: 0x000B9209
		private static void RenderPagingItems(TextWriter writer)
		{
			writer.Write(LocalizedStrings.GetHtmlEncoded(-577737662));
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000BB01C File Offset: 0x000B921C
		private void RenderPagingUI(TextWriter writer)
		{
			writer.Write("<table class=\"pgLyt\">");
			writer.Write("<tr>");
			writer.Write("<td id=\"tdCT\" class=\"");
			writer.Write(this.userContext.IsRtl ? "r" : "l");
			writer.Write("\" nowrap>");
			ListView.RenderPagingItems(writer);
			this.RenderPagingRangeStart(writer);
			this.RenderPagingEndRange(writer);
			writer.Write("</td>");
			writer.Write("<td id=\"tdPF\"></td>");
			writer.Write("<td id=\"tdPI\"");
			if (this.DataSource.RangeCount == 0 || this.DataSource.RangeCount == this.DataSource.TotalCount)
			{
				writer.Write(" style=\"filter:alpha(opacity=35);\"");
			}
			writer.Write("><div>");
			this.userContext.RenderThemeImage(writer, this.userContext.IsRtl ? ThemeFileId.LastPage : ThemeFileId.FirstPage, null, new object[]
			{
				"id=fP",
				"title=\"" + LocalizedStrings.GetHtmlEncoded(-946066775) + "\""
			});
			this.userContext.RenderThemeImage(writer, this.userContext.IsRtl ? ThemeFileId.NextPage : ThemeFileId.PreviousPage, null, new object[]
			{
				"id=pP",
				"title=\"" + LocalizedStrings.GetHtmlEncoded(-1907861992) + "\""
			});
			this.userContext.RenderThemeImage(writer, this.userContext.IsRtl ? ThemeFileId.PreviousPage : ThemeFileId.NextPage, null, new object[]
			{
				"id=nP",
				"title=\"" + LocalizedStrings.GetHtmlEncoded(1548165396) + "\""
			});
			this.userContext.RenderThemeImage(writer, this.userContext.IsRtl ? ThemeFileId.FirstPage : ThemeFileId.LastPage, null, new object[]
			{
				"id=lP",
				"title=\"" + LocalizedStrings.GetHtmlEncoded(-991618511) + "\""
			});
			writer.Write("</div></td>");
			writer.Write("</tr>");
			writer.Write("</table>");
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000BB248 File Offset: 0x000B9448
		private void RenderPagingRangeStart(TextWriter writer)
		{
			int value = (this.DataSource.RangeCount == 0) ? 0 : (this.DataSource.StartRange + 1);
			writer.Write("&nbsp;");
			writer.Write("<input id=\"txtSR\" type=\"text\" value=\"");
			writer.Write(value);
			writer.Write("\" sR=\"");
			writer.Write(value);
			writer.Write("\"");
			if (this.DataSource.RangeCount == 0 || this.DataSource.RangeCount == this.DataSource.TotalCount)
			{
				writer.Write(" disabled=\"true\"");
			}
			writer.Write("> ");
			writer.Write("&nbsp;");
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000BB2F4 File Offset: 0x000B94F4
		private void RenderPagingEndRange(TextWriter writer)
		{
			int num;
			if (this.DataSource.RangeCount == 0)
			{
				num = 0;
			}
			else
			{
				num = 1 + this.DataSource.EndRange;
			}
			if (this.DataSource is ADListViewDataSource)
			{
				writer.Write(string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetHtmlEncoded(-727972566), new object[]
				{
					"<span id=spnER>" + num + "</span>"
				}));
				return;
			}
			writer.Write(string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetHtmlEncoded(-948999625), new object[]
			{
				"<span id=spnER>" + num + "</span>",
				"<span id=spnTC>" + this.DataSource.TotalCount + "</span>"
			}));
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000BB3C4 File Offset: 0x000B95C4
		protected virtual void RenderInlineControl(TextWriter writer)
		{
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002053 RID: 8275 RVA: 0x000BB3C6 File Offset: 0x000B95C6
		protected virtual bool HasInlineControl
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000BB3C9 File Offset: 0x000B95C9
		public void AddAttribute(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name is null or empty.");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.extraAttributes.Add(name, value);
		}

		// Token: 0x0400174C RID: 5964
		private static readonly string sortAscending = 0.ToString(CultureInfo.InvariantCulture);

		// Token: 0x0400174D RID: 5965
		private static readonly string sortDescending = 1.ToString(CultureInfo.InvariantCulture);

		// Token: 0x0400174E RID: 5966
		private LegacyListViewContents contents;

		// Token: 0x0400174F RID: 5967
		private ViewDescriptor viewDescriptor;

		// Token: 0x04001750 RID: 5968
		private SortOrder sortOrder;

		// Token: 0x04001751 RID: 5969
		private ColumnId sortedColumn = ColumnId.Count;

		// Token: 0x04001752 RID: 5970
		private UserContext userContext;

		// Token: 0x04001753 RID: 5971
		private bool isFilteredView;

		// Token: 0x04001754 RID: 5972
		private Dictionary<string, string> extraAttributes = new Dictionary<string, string>();

		// Token: 0x02000356 RID: 854
		[Flags]
		public enum RenderFlags
		{
			// Token: 0x04001756 RID: 5974
			Behavior = 1,
			// Token: 0x04001757 RID: 5975
			Contents = 2,
			// Token: 0x04001758 RID: 5976
			Headers = 4,
			// Token: 0x04001759 RID: 5977
			Paging = 8,
			// Token: 0x0400175A RID: 5978
			CompactWebPart = 13,
			// Token: 0x0400175B RID: 5979
			All = 15
		}
	}
}
