using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Exchange.Clients.Owa.Core.Transcoding
{
	// Token: 0x020002F9 RID: 761
	internal class TranscodingParameters
	{
		// Token: 0x06001C9C RID: 7324 RVA: 0x000A4C3C File Offset: 0x000A2E3C
		public TranscodingParameters(string sessionId, string documentId, Stream sourceStream, string sourceDocType, int currentPageNumber)
		{
			this.sessionId = sessionId;
			this.documentId = documentId;
			this.sourceStream = sourceStream;
			this.sourceDocType = sourceDocType;
			this.currentPageNumber = currentPageNumber;
			this.errorCode = null;
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x000A4C8B File Offset: 0x000A2E8B
		public string SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x000A4C93 File Offset: 0x000A2E93
		public string DocumentId
		{
			get
			{
				return this.documentId;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x000A4C9B File Offset: 0x000A2E9B
		public Stream SourceStream
		{
			get
			{
				return this.sourceStream;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x000A4CA3 File Offset: 0x000A2EA3
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x000A4CAB File Offset: 0x000A2EAB
		public string RewrittenHtmlFileName
		{
			get
			{
				return this.rewrittenHtmlFileName;
			}
			set
			{
				this.rewrittenHtmlFileName = value;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x000A4CB4 File Offset: 0x000A2EB4
		public string SourceDocType
		{
			get
			{
				return this.sourceDocType;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x000A4CBC File Offset: 0x000A2EBC
		public int CurrentPageNumber
		{
			get
			{
				return this.currentPageNumber;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x000A4CC4 File Offset: 0x000A2EC4
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x000A4CCC File Offset: 0x000A2ECC
		public int TotalPageNumber
		{
			get
			{
				return this.totalPageNumber;
			}
			set
			{
				this.totalPageNumber = value;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x000A4CD5 File Offset: 0x000A2ED5
		public Stopwatch Stopwatch
		{
			get
			{
				return this.stopwatch;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x000A4CDD File Offset: 0x000A2EDD
		// (set) Token: 0x06001CA8 RID: 7336 RVA: 0x000A4CE5 File Offset: 0x000A2EE5
		public TranscodeErrorCode? ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x000A4CEE File Offset: 0x000A2EEE
		// (set) Token: 0x06001CAA RID: 7338 RVA: 0x000A4CF6 File Offset: 0x000A2EF6
		public bool IsLeftQueueHandled
		{
			get
			{
				return this.isLeftQueueHandled;
			}
			set
			{
				this.isLeftQueueHandled = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x000A4CFF File Offset: 0x000A2EFF
		// (set) Token: 0x06001CAC RID: 7340 RVA: 0x000A4D07 File Offset: 0x000A2F07
		public int SourceDocSize
		{
			get
			{
				return this.sourceDocSize;
			}
			set
			{
				this.sourceDocSize = value;
			}
		}

		// Token: 0x0400152C RID: 5420
		private string sessionId;

		// Token: 0x0400152D RID: 5421
		private string documentId;

		// Token: 0x0400152E RID: 5422
		private Stream sourceStream;

		// Token: 0x0400152F RID: 5423
		private string rewrittenHtmlFileName;

		// Token: 0x04001530 RID: 5424
		private string sourceDocType;

		// Token: 0x04001531 RID: 5425
		private int currentPageNumber;

		// Token: 0x04001532 RID: 5426
		private int totalPageNumber;

		// Token: 0x04001533 RID: 5427
		private bool isLeftQueueHandled;

		// Token: 0x04001534 RID: 5428
		private TranscodeErrorCode? errorCode;

		// Token: 0x04001535 RID: 5429
		private Stopwatch stopwatch;

		// Token: 0x04001536 RID: 5430
		private int sourceDocSize;
	}
}
