using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000116 RID: 278
	internal class RequestParsingException : LocalizedException
	{
		// Token: 0x06000EC7 RID: 3783 RVA: 0x0005441E File Offset: 0x0005261E
		public RequestParsingException(string errorMessage) : this(errorMessage, errorMessage)
		{
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00054428 File Offset: 0x00052628
		public RequestParsingException(string errorMessage, string logMessage) : base(new LocalizedString(errorMessage))
		{
			this.LogMessage = logMessage;
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0005443D File Offset: 0x0005263D
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x00054445 File Offset: 0x00052645
		public string LogMessage { get; private set; }
	}
}
