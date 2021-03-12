using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000052 RID: 82
	internal abstract class AsyncRequest : AsyncTask
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00009920 File Offset: 0x00007B20
		protected AsyncRequest(Application application, ClientContext clientContext, RequestLogger requestLogger)
		{
			this.Application = application;
			this.ClientContext = clientContext;
			this.RequestLogger = requestLogger;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000993D File Offset: 0x00007B3D
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009945 File Offset: 0x00007B45
		public ClientContext ClientContext { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000994E File Offset: 0x00007B4E
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00009956 File Offset: 0x00007B56
		public Application Application { get; private set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000995F File Offset: 0x00007B5F
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00009967 File Offset: 0x00007B67
		public RequestLogger RequestLogger { get; private set; }
	}
}
