using System;
using System.Globalization;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001EE RID: 494
	public class OwaPerformanceData
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00063988 File Offset: 0x00061B88
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x00063990 File Offset: 0x00061B90
		public string RequestType
		{
			get
			{
				return this.requestType;
			}
			set
			{
				this.requestType = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00063999 File Offset: 0x00061B99
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x000639A1 File Offset: 0x00061BA1
		public string RowId
		{
			get
			{
				return this.clientRowId;
			}
			set
			{
				this.clientRowId = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x000639AA File Offset: 0x00061BAA
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x000639B2 File Offset: 0x00061BB2
		public long TotalLatency
		{
			get
			{
				return this.totalLatency;
			}
			set
			{
				this.totalLatency = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x000639BB File Offset: 0x00061BBB
		// (set) Token: 0x0600100E RID: 4110 RVA: 0x000639C3 File Offset: 0x00061BC3
		public int RpcLatency
		{
			get
			{
				return this.rpcLatency;
			}
			set
			{
				this.rpcLatency = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x000639CC File Offset: 0x00061BCC
		public uint RpcCount
		{
			set
			{
				this.rpcCount = (long)((ulong)value);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (set) Token: 0x06001010 RID: 4112 RVA: 0x000639D6 File Offset: 0x00061BD6
		public uint LdapCount
		{
			set
			{
				this.ldapCount = (long)((ulong)value);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (set) Token: 0x06001011 RID: 4113 RVA: 0x000639E0 File Offset: 0x00061BE0
		public int LdapLatency
		{
			set
			{
				this.ldapLatency = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000639E9 File Offset: 0x00061BE9
		public long KilobytesAllocated
		{
			set
			{
				this.kilobytesAllocated = value;
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000639F4 File Offset: 0x00061BF4
		public void TraceOther(string trace)
		{
			if (this.other == null)
			{
				this.other = new StringBuilder();
			}
			if (this.other.Length != 0)
			{
				this.other.Append(",");
			}
			this.other.Append(trace ?? "(null)");
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00063A48 File Offset: 0x00061C48
		public OwaPerformanceData(HttpRequest request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.requestUrl = request.Url;
			this.clientRowId = request.Headers["X-OWA-PerfConsoleRowId"];
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00063ADD File Offset: 0x00061CDD
		public void RetrieveFinishJS(int rowId, out string js)
		{
			js = "fnshRq('srv" + rowId + "');";
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00063AF8 File Offset: 0x00061CF8
		public void SetFormRequestType(string formName)
		{
			if (formName != null)
			{
				string[] array = formName.Split(new char[]
				{
					'/'
				});
				if (array != null)
				{
					formName = array[array.Length - 1];
					int num = formName.LastIndexOf('.');
					if (num >= 0)
					{
						formName = formName.Substring(0, num);
					}
				}
				this.RequestType = formName;
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00063B48 File Offset: 0x00061D48
		public void SetOehRequestType(string handlerName, string methodName)
		{
			int num = handlerName.IndexOf("EventHandler", 0, StringComparison.OrdinalIgnoreCase);
			if (num > 0)
			{
				handlerName = handlerName.Substring(0, num);
			}
			this.RequestType = handlerName + " " + methodName;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00063B84 File Offset: 0x00061D84
		public void RetrieveHtmlForPerfData(int rowId, out string html, bool finished, int index)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("<tr id='srv");
			stringBuilder.Append(rowId);
			if (finished)
			{
				stringBuilder.Append("' pndFn='1'");
			}
			stringBuilder.Append("'>");
			stringBuilder.Append("<td class='prfCell noLBrd'>");
			stringBuilder.Append(index);
			stringBuilder.Append("</td>");
			this.AppendDefaultTdToHtmlRender(string.Concat(new object[]
			{
				"<a href='#' _url='",
				this.RequestUrl,
				"'>",
				this.requestType,
				"</a>"
			}), stringBuilder);
			this.AppendDefaultTdToHtmlRender("(Unknown)", stringBuilder);
			this.AppendDefaultTdToHtmlRender("(Unknown)", stringBuilder);
			this.AppendDefaultTdToHtmlRender(this.totalLatency, stringBuilder, "&nbsp;ms");
			this.AppendDefaultTdToHtmlRender(this.rpcCount, stringBuilder);
			this.AppendDefaultTdToHtmlRender(this.rpcLatency, stringBuilder, "&nbsp;ms");
			this.AppendDefaultTdToHtmlRender(this.ldapCount, stringBuilder);
			this.AppendDefaultTdToHtmlRender(this.ldapLatency, stringBuilder, "&nbsp;ms");
			this.AppendDefaultTdToHtmlRender(this.kilobytesAllocated, stringBuilder, "&nbsp;kB");
			this.AppendDefaultTdToHtmlRender((this.other != null) ? Utilities.HtmlEncode(this.other.ToString()) : null, stringBuilder);
			stringBuilder.Append("</tr>");
			html = stringBuilder.ToString();
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00063CD7 File Offset: 0x00061ED7
		private void AppendDefaultTdToHtmlRender(long content, StringBuilder builder)
		{
			this.AppendDefaultTdToHtmlRender((content == long.MinValue || content < 0L) ? null : content.ToString(), builder);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00063CFB File Offset: 0x00061EFB
		private void AppendDefaultTdToHtmlRender(long content, StringBuilder builder, string add)
		{
			this.AppendDefaultTdToHtmlRender((content == long.MinValue || content < 0L) ? null : (content.ToString() + add), builder);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00063D25 File Offset: 0x00061F25
		private void AppendDefaultTdToHtmlRender(int content, StringBuilder builder)
		{
			this.AppendDefaultTdToHtmlRender((content == int.MinValue || content < 0) ? null : content.ToString(), builder);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00063D44 File Offset: 0x00061F44
		private void AppendDefaultTdToHtmlRender(int content, StringBuilder builder, string add)
		{
			this.AppendDefaultTdToHtmlRender((content == int.MinValue || content < 0) ? null : (content.ToString() + add), builder);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00063D69 File Offset: 0x00061F69
		private void AppendDefaultTdToHtmlRender(string content, StringBuilder builder)
		{
			builder.Append("<td class='prfCell'>");
			if (content != null)
			{
				builder.Append(content);
			}
			builder.Append("</td>");
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00063D8E File Offset: 0x00061F8E
		private void AppendAttributeTdToHtmlRender(string attribute, StringBuilder builder)
		{
			builder.Append("<td class='prfCell' ");
			if (attribute != null)
			{
				builder.Append(attribute);
			}
			builder.Append("></td>");
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00063DB4 File Offset: 0x00061FB4
		public void RetrieveJSforPerfData(int rowId, out string js)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append("[null");
			if (this.requestType != null)
			{
				stringBuilder.Append(",'" + this.requestType + "'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			stringBuilder.Append(",'(Unknown)','(Unknown)'");
			if (this.totalLatency >= 0L)
			{
				stringBuilder.Append(",'" + this.totalLatency + "&nbsp;ms'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			if (this.rpcCount >= 0L)
			{
				stringBuilder.Append(",'" + this.rpcCount + "'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			if (this.rpcLatency >= 0)
			{
				stringBuilder.Append(",'" + this.rpcLatency + "&nbsp;ms'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			if (this.ldapCount >= 0L)
			{
				stringBuilder.Append(",'" + this.ldapCount + "'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			if (this.ldapLatency >= 0)
			{
				stringBuilder.Append(",'" + this.ldapLatency + "&nbsp;ms'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			if (this.kilobytesAllocated >= 0L)
			{
				stringBuilder.Append(",'" + this.kilobytesAllocated + "&nbsp;kB'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			if (this.other != null && this.other.Length > 0)
			{
				stringBuilder.Append(",'" + Utilities.JavascriptEncode(this.other.ToString()) + "'");
			}
			else
			{
				stringBuilder.Append(",null");
			}
			stringBuilder.Append("]");
			js = string.Format(CultureInfo.InvariantCulture, "srvUpdtRw('srv{0}',{1});", new object[]
			{
				rowId,
				stringBuilder
			});
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00063FE3 File Offset: 0x000621E3
		public Uri RequestUrl
		{
			get
			{
				return this.requestUrl;
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00063FEC File Offset: 0x000621EC
		public void GenerateInitialPayload(StringBuilder builder, int rowId)
		{
			builder.Append("excPrfCnsl(\"");
			builder.Append("dtFrmSrv('");
			builder.Append(Utilities.JavascriptEncode(this.RequestUrl.ToString()));
			builder.Append("','srv");
			builder.Append(rowId);
			builder.Append("',null,'");
			builder.Append(this.RowId);
			if (this.RequestType != null)
			{
				builder.Append("','");
				builder.Append(Utilities.JavascriptEncode(this.RequestType));
			}
			builder.Append("');");
			builder.Append("\");");
		}

		// Token: 0x04000AB5 RID: 2741
		private const string UPDATEALLROWFORMAT = "srvUpdtRw('srv{0}',{1});";

		// Token: 0x04000AB6 RID: 2742
		private const string UPDATEDATEROWFORMAT = "chngDtTb({0},{1},'{2}',{3});";

		// Token: 0x04000AB7 RID: 2743
		public const int ConsoleColumCount = 11;

		// Token: 0x04000AB8 RID: 2744
		private long ldapCount = long.MinValue;

		// Token: 0x04000AB9 RID: 2745
		private int ldapLatency = int.MinValue;

		// Token: 0x04000ABA RID: 2746
		private long rpcCount = long.MinValue;

		// Token: 0x04000ABB RID: 2747
		private int rpcLatency = int.MinValue;

		// Token: 0x04000ABC RID: 2748
		private long kilobytesAllocated = long.MinValue;

		// Token: 0x04000ABD RID: 2749
		private StringBuilder other;

		// Token: 0x04000ABE RID: 2750
		private Uri requestUrl;

		// Token: 0x04000ABF RID: 2751
		private long totalLatency = long.MinValue;

		// Token: 0x04000AC0 RID: 2752
		private string clientRowId;

		// Token: 0x04000AC1 RID: 2753
		private string requestType;
	}
}
