using System;
using System.Threading;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020009CE RID: 2510
	internal class InstantSearchRequestContext
	{
		// Token: 0x060046F6 RID: 18166 RVA: 0x000FC784 File Offset: 0x000FA984
		public InstantSearchRequestContext(PerformInstantSearchRequest request, IInstantSearchNotificationHandler notificationHandler, SearchPerfMarkerContainer perfMarkerContainer)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (notificationHandler == null)
			{
				throw new ArgumentNullException("notificationHandler");
			}
			if (perfMarkerContainer == null)
			{
				throw new ArgumentNullException("perfMarkerContainer");
			}
			this.request = request;
			this.notificationHandler = notificationHandler;
			this.PerfMarkers = perfMarkerContainer;
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x000FC7D6 File Offset: 0x000FA9D6
		public IInstantSearchNotificationHandler NotificationHandler
		{
			get
			{
				return this.notificationHandler;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x060046F8 RID: 18168 RVA: 0x000FC7DE File Offset: 0x000FA9DE
		// (set) Token: 0x060046F9 RID: 18169 RVA: 0x000FC7E6 File Offset: 0x000FA9E6
		public string[] SearchTerms { get; set; }

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x060046FA RID: 18170 RVA: 0x000FC7EF File Offset: 0x000FA9EF
		public PerformInstantSearchRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x000FC7F7 File Offset: 0x000FA9F7
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x000FC7FF File Offset: 0x000FA9FF
		public SearchPerfMarkerContainer PerfMarkers { get; private set; }

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x000FC808 File Offset: 0x000FAA08
		// (set) Token: 0x060046FE RID: 18174 RVA: 0x000FC810 File Offset: 0x000FAA10
		internal ManualResetEvent SearchResultsReceivedEvent { get; set; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x000FC819 File Offset: 0x000FAA19
		// (set) Token: 0x06004700 RID: 18176 RVA: 0x000FC821 File Offset: 0x000FAA21
		public PerformInstantSearchResponse Response { get; set; }

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x000FC82A File Offset: 0x000FAA2A
		// (set) Token: 0x06004702 RID: 18178 RVA: 0x000FC832 File Offset: 0x000FAA32
		public string Error { get; set; }

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x000FC83B File Offset: 0x000FAA3B
		// (set) Token: 0x06004704 RID: 18180 RVA: 0x000FC843 File Offset: 0x000FAA43
		public bool ResponseSent { get; set; }

		// Token: 0x040028C0 RID: 10432
		private readonly PerformInstantSearchRequest request;

		// Token: 0x040028C1 RID: 10433
		private readonly IInstantSearchNotificationHandler notificationHandler;
	}
}
