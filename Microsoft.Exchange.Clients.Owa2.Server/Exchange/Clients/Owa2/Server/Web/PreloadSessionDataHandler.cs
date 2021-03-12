using System;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Core;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x02000486 RID: 1158
	internal sealed class PreloadSessionDataHandler : SessionDataHandler
	{
		// Token: 0x06002753 RID: 10067 RVA: 0x0009165E File Offset: 0x0008F85E
		static PreloadSessionDataHandler()
		{
			OwsLogRegistry.Register("SessionDataPreload_Overall", typeof(SessionDataMetadata), new Type[0]);
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x0009167A File Offset: 0x0008F87A
		protected override Stream OutputStream
		{
			get
			{
				return this.sessionDataCache.OutputStream;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x00091687 File Offset: 0x0008F887
		protected override Encoding Encoding
		{
			get
			{
				return this.sessionDataCache.Encoding;
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x00091694 File Offset: 0x0008F894
		// (set) Token: 0x06002757 RID: 10071 RVA: 0x0009169C File Offset: 0x0008F89C
		protected override bool BufferOutput { get; set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x000916A5 File Offset: 0x0008F8A5
		// (set) Token: 0x06002759 RID: 10073 RVA: 0x000916AD File Offset: 0x0008F8AD
		protected override string ContentType { get; set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x000916B6 File Offset: 0x0008F8B6
		protected override bool IsSessionDataPreloadRequest
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000916B9 File Offset: 0x0008F8B9
		protected override string LogEventId
		{
			get
			{
				return "SessionDataPreload_Overall";
			}
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000916C0 File Offset: 0x0008F8C0
		protected override void Flush()
		{
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x000916C4 File Offset: 0x0008F8C4
		protected override void OnBeforeBeginProcessing(HttpContext context, out bool shouldContinue)
		{
			shouldContinue = true;
			ExAssert.RetailAssert(context != null, "HttpContext is null");
			if (!base.IsSessionDataPreloadEnabled)
			{
				ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), "[SessionDataPreload] SessionDataCache is not supported.");
				shouldContinue = false;
				return;
			}
			this.sessionDataCache = base.RequestContext.UserContext.SessionDataCache;
			ExAssert.RetailAssert(this.sessionDataCache != null, "SessionDataCache is null");
			shouldContinue = this.sessionDataCache.StartBuilding();
			ExTraceGlobals.SessionDataHandlerTracer.TraceDebug<bool>((long)this.GetHashCode(), "[SessionDataPreload] Starting to build SessionDataCache {0}.", shouldContinue);
		}

		// Token: 0x0600275E RID: 10078 RVA: 0x00091758 File Offset: 0x0008F958
		protected override void OnBeforeEndProcessing(UserContext userContext)
		{
			if (!base.IsSessionDataPreloadEnabled)
			{
				ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), "[SessionDataPreload] SessionDataCache is not supported.");
				return;
			}
			ExTraceGlobals.SessionDataHandlerTracer.TraceDebug((long)this.GetHashCode(), "[SessionDataPreload] Completing to build SessionDataCache.");
			this.sessionDataCache.CompleteBuilding();
		}

		// Token: 0x040016F4 RID: 5876
		public const string SessionDataPreLoadOverallEventId = "SessionDataPreload_Overall";

		// Token: 0x040016F5 RID: 5877
		private SessionDataCache sessionDataCache;
	}
}
