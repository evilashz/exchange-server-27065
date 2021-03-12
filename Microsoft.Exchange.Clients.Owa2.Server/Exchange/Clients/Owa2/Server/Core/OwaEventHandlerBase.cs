using System;
using System.Collections;
using System.IO;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001D8 RID: 472
	internal abstract class OwaEventHandlerBase : DisposeTrackableBase
	{
		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0003FF28 File Offset: 0x0003E128
		public static bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0003FF2B File Offset: 0x0003E12B
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0003FF33 File Offset: 0x0003E133
		public OwaEventAttribute EventInfo
		{
			get
			{
				return this.eventInfo;
			}
			internal set
			{
				this.eventInfo = value;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0003FF3C File Offset: 0x0003E13C
		public virtual HttpContext HttpContext
		{
			get
			{
				return HttpContext.Current;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x0003FF43 File Offset: 0x0003E143
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x0003FF4B File Offset: 0x0003E14B
		public OwaEventVerb Verb
		{
			get
			{
				return this.verb;
			}
			internal set
			{
				this.verb = value;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060010C4 RID: 4292 RVA: 0x0003FF54 File Offset: 0x0003E154
		// (set) Token: 0x060010C5 RID: 4293 RVA: 0x0003FF5C File Offset: 0x0003E15C
		public OwaEventContentType ResponseContentType
		{
			get
			{
				return this.responseContentType;
			}
			set
			{
				this.responseContentType = value;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060010C6 RID: 4294 RVA: 0x0003FF65 File Offset: 0x0003E165
		public virtual TextWriter Writer
		{
			get
			{
				return HttpContext.Current.Response.Output;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0003FF76 File Offset: 0x0003E176
		// (set) Token: 0x060010C8 RID: 4296 RVA: 0x0003FF7E File Offset: 0x0003E17E
		public bool DontWriteHeaders
		{
			get
			{
				return this.dontWriteHeaders;
			}
			set
			{
				this.dontWriteHeaders = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0003FF87 File Offset: 0x0003E187
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0003FF8F File Offset: 0x0003E18F
		public bool ShowErrorInPage
		{
			get
			{
				return this.showErrorInPage;
			}
			set
			{
				this.showErrorInPage = value;
			}
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003FF98 File Offset: 0x0003E198
		internal static bool ShouldIgnoreRequest(RequestContext requestContext, IMailboxContext userContext)
		{
			return PendingRequestEventHandler.IsObsoleteRequest(requestContext, userContext);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003FFA1 File Offset: 0x0003E1A1
		internal void SetParameterTable(Hashtable parameterTable)
		{
			this.parameterTable = parameterTable;
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003FFAA File Offset: 0x0003E1AA
		internal object GetParameter(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.parameterTable[name];
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003FFC6 File Offset: 0x0003E1C6
		protected bool IsParameterSet(string name)
		{
			return null != this.GetParameter(name);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003FFD5 File Offset: 0x0003E1D5
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003FFD7 File Offset: 0x0003E1D7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaEventHandlerBase>(this);
		}

		// Token: 0x040009E6 RID: 2534
		private const int InitialTableCapacity = 4;

		// Token: 0x040009E7 RID: 2535
		private const string JavascriptContentType = "application/x-javascript";

		// Token: 0x040009E8 RID: 2536
		private const string HtmlContentType = "text/html";

		// Token: 0x040009E9 RID: 2537
		private OwaEventAttribute eventInfo;

		// Token: 0x040009EA RID: 2538
		private Hashtable parameterTable;

		// Token: 0x040009EB RID: 2539
		private OwaEventContentType responseContentType = OwaEventContentType.Html;

		// Token: 0x040009EC RID: 2540
		private OwaEventVerb verb;

		// Token: 0x040009ED RID: 2541
		private bool dontWriteHeaders;

		// Token: 0x040009EE RID: 2542
		private bool showErrorInPage;
	}
}
