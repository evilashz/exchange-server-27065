using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200077A RID: 1914
	internal class MissingKeywordException : HttpWebResponseWrapperException
	{
		// Token: 0x060025F2 RID: 9714 RVA: 0x0005003F File Offset: 0x0004E23F
		public MissingKeywordException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, string expectedKeyword) : base(message, request, response)
		{
			this.expectedKeyword = expectedKeyword;
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x00050052 File Offset: 0x0004E252
		public override string ExceptionHint
		{
			get
			{
				return "MissingKeyword: " + this.expectedKeyword;
			}
		}

		// Token: 0x040022FD RID: 8957
		private readonly string expectedKeyword;
	}
}
