using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200000A RID: 10
	public abstract class ListView
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00004052 File Offset: 0x00002252
		protected ListView(UserContext userContext, ColumnId sortedColumn, SortOrder sortOrder, ListView.ViewType viewType)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
			this.sortedColumn = sortedColumn;
			this.sortOrder = sortOrder;
			this.viewType = viewType;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00004085 File Offset: 0x00002285
		protected UserContext UserContext
		{
			get
			{
				return this.userContext;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000408D File Offset: 0x0000228D
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00004095 File Offset: 0x00002295
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000409E File Offset: 0x0000229E
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000040A6 File Offset: 0x000022A6
		protected ListViewContents Contents
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000040AF File Offset: 0x000022AF
		protected SortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000040B7 File Offset: 0x000022B7
		protected ColumnId SortedColumn
		{
			get
			{
				return this.sortedColumn;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000040BF File Offset: 0x000022BF
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000040C7 File Offset: 0x000022C7
		internal ListViewDataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000040D0 File Offset: 0x000022D0
		public int StartRange
		{
			get
			{
				if (this.dataSource == null)
				{
					return int.MinValue;
				}
				return this.dataSource.StartRange;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000040EB File Offset: 0x000022EB
		public int EndRange
		{
			get
			{
				if (this.dataSource == null)
				{
					return int.MinValue;
				}
				return this.dataSource.EndRange;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004106 File Offset: 0x00002306
		public int RangeCount
		{
			get
			{
				if (this.dataSource != null)
				{
					return this.dataSource.RangeCount;
				}
				return 0;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000411D File Offset: 0x0000231D
		public int TotalCount
		{
			get
			{
				if (this.dataSource != null)
				{
					return this.dataSource.TotalCount;
				}
				return 0;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004134 File Offset: 0x00002334
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000413C File Offset: 0x0000233C
		protected bool FilteredView
		{
			get
			{
				return this.filteredView;
			}
			set
			{
				this.filteredView = value;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004145 File Offset: 0x00002345
		protected ListView(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
		}

		// Token: 0x06000061 RID: 97
		protected abstract void InitializeListViewContents();

		// Token: 0x06000062 RID: 98
		protected abstract void InitializeDataSource();

		// Token: 0x06000063 RID: 99 RVA: 0x00004162 File Offset: 0x00002362
		public virtual void Initialize(int startRange, int endRange)
		{
			if (startRange < 1)
			{
				throw new ArgumentOutOfRangeException("startRange", "startRange must be greater than or equal to 1");
			}
			if (endRange < startRange)
			{
				throw new ArgumentOutOfRangeException("endRange", "endRange must be greater than or equal to startRange");
			}
			this.PerformInitialization();
			this.dataSource.LoadData(startRange, endRange);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000419F File Offset: 0x0000239F
		internal int Initialize(StoreObjectId storeObjectId, int itemsPerPage)
		{
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			if (itemsPerPage <= 0)
			{
				throw new ArgumentOutOfRangeException("itemsPerPage", "itemsPerPage has to be greater than zero");
			}
			this.PerformInitialization();
			return this.dataSource.LoadData(storeObjectId, itemsPerPage);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000041D8 File Offset: 0x000023D8
		public void Render(TextWriter writer, string errorMessage)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (errorMessage == null)
			{
				throw new ArgumentNullException("errorMessage");
			}
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListView.Render");
			writer.Write("<table class=\"lvw\" cellpadding=0 cellspacing=0><caption>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(573876176));
			writer.Write("</caption>");
			if (errorMessage.Length != 0)
			{
				writer.Write("<tr><td colspan=8 class=\"errMsg\"><img src=\"");
				this.userContext.RenderThemeFileUrl(writer, ThemeFileId.Error);
				writer.Write("\">");
				writer.Write(errorMessage);
				writer.Write("</td></tr>");
			}
			ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Rendering ListView headers");
			ListViewHeaders listViewHeaders = new ListViewHeaders(this.viewDescriptor, this.sortedColumn, this.sortOrder, this.userContext, this.viewType);
			listViewHeaders.Render(writer);
			ExTraceGlobals.MailTracer.TraceDebug((long)this.GetHashCode(), "Rendering ListView contents");
			if (this.dataSource.RangeCount > 0)
			{
				this.contents.DataSource = this.dataSource;
				this.contents.Render(writer);
			}
			else
			{
				writer.Write("<tr><td class=\"ni\" colspan=");
				writer.Write(this.viewDescriptor.ColumnCount);
				writer.Write(" align=\"center\" valign=\"middle\"><br>");
				writer.Write(LocalizedStrings.GetHtmlEncoded(this.filteredView ? 417836457 : -474826895));
				writer.Write("<br><br></td></tr>");
			}
			writer.Write("</table>");
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000435C File Offset: 0x0000255C
		public static void RenderPageNumbers(TextWriter writer, int pageNumber, int totalNumberOfPages)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (pageNumber < 1)
			{
				throw new ArgumentOutOfRangeException("pageNumber", "pageNumber must be greater than or equal to 1");
			}
			if (totalNumberOfPages < 0)
			{
				throw new ArgumentOutOfRangeException("totalNumberOfPages", "totalNumberOfPages must be greater than or equal to 0");
			}
			int num;
			if (pageNumber % 5 == 0)
			{
				num = pageNumber / 5;
			}
			else
			{
				num = pageNumber / 5 + 1;
			}
			int num2 = (num - 1) * 5 + 1;
			int num3 = num2 + 5 - 1;
			if (num3 > totalNumberOfPages)
			{
				num3 = totalNumberOfPages;
			}
			if (num3 >= num2)
			{
				writer.Write("<td class=\"pl\" nowrap>");
				writer.Write(LocalizedStrings.GetHtmlEncoded(-2042236200));
				writer.Write("</td>");
			}
			if (num > 1)
			{
				ListView.RenderPageLink(writer, (num - 1) * 5, true);
			}
			for (int i = num2; i <= num3; i++)
			{
				if (i != pageNumber)
				{
					ListView.RenderPageLink(writer, i, false);
				}
				else
				{
					writer.Write("<td class=\"pTxt\">");
					writer.Write(i);
					writer.Write("</td>");
				}
			}
			if (totalNumberOfPages % 5 == 0)
			{
				if (num < totalNumberOfPages / 5)
				{
					ListView.RenderPageLink(writer, num * 5 + 1, true);
					return;
				}
			}
			else if (num < totalNumberOfPages / 5 + 1)
			{
				ListView.RenderPageLink(writer, num * 5 + 1, true);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004468 File Offset: 0x00002668
		internal void RenderHeaderPagingControls(TextWriter writer, int pageNumber, int totalNumberOfPages)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (pageNumber < 1)
			{
				throw new ArgumentOutOfRangeException("pageNumber", "pageNumber must be greater than or equal to 1");
			}
			if (totalNumberOfPages < 0)
			{
				throw new ArgumentOutOfRangeException("totalNumberOfPages", "totalNumberOfPages must be greater than or equal to 0");
			}
			if (pageNumber > 1)
			{
				this.RenderPageControlImage(writer, ThemeFileId.FirstPage, 1, -946066775, "lnkFrstPgHdr");
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPage, pageNumber - 1, -1907861992, "lnkPrvPgHdr");
			}
			else
			{
				this.RenderPageControlImage(writer, ThemeFileId.FirstPageGray, 0, -946066775, "lnkFrstPgHdr");
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPageGray, 0, -1907861992, "lnkPrvPgHdr");
			}
			if (pageNumber < totalNumberOfPages)
			{
				this.RenderPageControlImage(writer, ThemeFileId.NextPage, pageNumber + 1, 1548165396, "lnkNxtPgHdr");
				this.RenderPageControlImage(writer, ThemeFileId.LastPage, totalNumberOfPages, -991618511, "lnkLstPgHdr");
				return;
			}
			this.RenderPageControlImage(writer, ThemeFileId.NextPageGray, 0, 1548165396, "lnkNxtPgHdr");
			this.RenderPageControlImage(writer, ThemeFileId.LastPageGray, 0, -991618511, "lnkLstPgHdr");
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000456C File Offset: 0x0000276C
		public void RenderPagingControls(TextWriter writer, int pageNumber, int totalNumberOfPages)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (pageNumber < 1)
			{
				throw new ArgumentOutOfRangeException("pageNumber", "pageNumber must be greater than or equal to 1");
			}
			if (totalNumberOfPages < 0)
			{
				throw new ArgumentOutOfRangeException("totalNumberOfPages", "totalNumberOfPages must be greater than or equal to 0");
			}
			if (pageNumber > 1)
			{
				this.RenderPageControlImage(writer, ThemeFileId.FirstPage, 1, -946066775, "lnkFrstPg");
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPage, pageNumber - 1, -1907861992, "lnkPrvPg");
			}
			else
			{
				this.RenderPageControlImage(writer, ThemeFileId.FirstPageGray, 0, -946066775, "lnkFrstPg");
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPageGray, 0, -1907861992, "lnkPrvPg");
			}
			if (pageNumber < totalNumberOfPages)
			{
				this.RenderPageControlImage(writer, ThemeFileId.NextPage, pageNumber + 1, 1548165396, "lnkNxtPg");
				this.RenderPageControlImage(writer, ThemeFileId.LastPage, totalNumberOfPages, -991618511, "lnkLstPg");
				return;
			}
			this.RenderPageControlImage(writer, ThemeFileId.NextPageGray, 0, 1548165396, "lnkNxtPg");
			this.RenderPageControlImage(writer, ThemeFileId.LastPageGray, 0, -991618511, "lnkLstPg");
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004670 File Offset: 0x00002870
		private static void RenderPageLink(TextWriter writer, int pageNumber, bool isPageSetLink)
		{
			writer.Write("<td class=\"pTxt\"><a href=\"#\" id=\"lnkPgNm");
			writer.Write(pageNumber);
			writer.Write("\" onClick=\"return onClkPg('");
			writer.Write(pageNumber);
			writer.Write("');\">");
			if (isPageSetLink)
			{
				writer.Write("...");
			}
			else
			{
				writer.Write(pageNumber);
			}
			writer.Write("</a></td>");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000046D0 File Offset: 0x000028D0
		private void RenderPageControlImage(TextWriter writer, ThemeFileId image, int pageNumber, Strings.IDs title, string controlId)
		{
			writer.Write("<td class=\"pImg\">");
			if (pageNumber > 0)
			{
				writer.Write("<a href=\"#\" id=\"");
				writer.Write(controlId);
				writer.Write("\" onClick=\"return onClkPg('{0}');\">", pageNumber);
			}
			writer.Write("<img border=\"0\" src=\"");
			this.userContext.RenderThemeFileUrl(writer, image);
			writer.Write("\" title=\"");
			writer.Write(LocalizedStrings.GetHtmlEncoded(title));
			writer.Write("\" ");
			Utilities.RenderImageAltAttribute(writer, this.UserContext, image);
			writer.Write(">");
			if (pageNumber > 0)
			{
				writer.Write("</a>");
			}
			writer.Write("</td>");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000477D File Offset: 0x0000297D
		private void PerformInitialization()
		{
			this.InitializeListViewContents();
			this.InitializeDataSource();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000478C File Offset: 0x0000298C
		internal void RenderADContentsHeaderPaging(TextWriter writer, int pageNumber)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (pageNumber > 1)
			{
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPage, pageNumber - 1, -1907861992, "lnkPrvPgHdr");
			}
			else
			{
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPageGray, 0, -1907861992, "lnkPrvPgHdr");
			}
			if (string.IsNullOrEmpty(this.dataSource.Cookie))
			{
				this.RenderPageControlImage(writer, ThemeFileId.NextPageGray, 0, 1548165396, "lnkNxtPgHdr");
				return;
			}
			this.RenderPageControlImage(writer, ThemeFileId.NextPage, pageNumber + 1, 1548165396, "lnkNxtPgHdr");
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004820 File Offset: 0x00002A20
		internal void RenderADContentsPaging(TextWriter writer, int pageNumber)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (pageNumber > 1)
			{
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPage, pageNumber - 1, -1907861992, "lnkPrvPg");
			}
			else
			{
				this.RenderPageControlImage(writer, ThemeFileId.PreviousPageGray, 0, -1907861992, "lnkPrvPg");
			}
			if (string.IsNullOrEmpty(this.dataSource.Cookie))
			{
				this.RenderPageControlImage(writer, ThemeFileId.NextPageGray, 0, 1548165396, "lnkNxtPg");
				return;
			}
			this.RenderPageControlImage(writer, ThemeFileId.NextPage, pageNumber + 1, 1548165396, "lnkNxtPg");
		}

		// Token: 0x0400002B RID: 43
		private const string PreviousPageHeaderId = "lnkPrvPgHdr";

		// Token: 0x0400002C RID: 44
		private const string NextPageHeaderId = "lnkNxtPgHdr";

		// Token: 0x0400002D RID: 45
		private const string FirstPageHeaderId = "lnkFrstPgHdr";

		// Token: 0x0400002E RID: 46
		private const string LastPageHeaderId = "lnkLstPgHdr";

		// Token: 0x0400002F RID: 47
		private const string PreviousPageId = "lnkPrvPg";

		// Token: 0x04000030 RID: 48
		private const string NextPageId = "lnkNxtPg";

		// Token: 0x04000031 RID: 49
		private const string FirstPageId = "lnkFrstPg";

		// Token: 0x04000032 RID: 50
		private const string LastPageId = "lnkLstPg";

		// Token: 0x04000033 RID: 51
		private ListView.ViewType viewType;

		// Token: 0x04000034 RID: 52
		private ListViewContents contents;

		// Token: 0x04000035 RID: 53
		private ViewDescriptor viewDescriptor;

		// Token: 0x04000036 RID: 54
		private SortOrder sortOrder;

		// Token: 0x04000037 RID: 55
		private ColumnId sortedColumn;

		// Token: 0x04000038 RID: 56
		private ListViewDataSource dataSource;

		// Token: 0x04000039 RID: 57
		private UserContext userContext;

		// Token: 0x0400003A RID: 58
		private bool filteredView;

		// Token: 0x0200000B RID: 11
		public enum ViewType
		{
			// Token: 0x0400003C RID: 60
			None,
			// Token: 0x0400003D RID: 61
			MessageListView,
			// Token: 0x0400003E RID: 62
			ADContentsListView,
			// Token: 0x0400003F RID: 63
			ContactsListView
		}
	}
}
