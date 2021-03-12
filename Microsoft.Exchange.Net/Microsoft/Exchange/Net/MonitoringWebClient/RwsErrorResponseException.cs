using System;
using Microsoft.Exchange.Net.MonitoringWebClient.Rws.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x020007EF RID: 2031
	internal class RwsErrorResponseException : HttpWebResponseWrapperException
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x0005CBF5 File Offset: 0x0005ADF5
		// (set) Token: 0x06002A8E RID: 10894 RVA: 0x0005CBFD File Offset: 0x0005ADFD
		public RwsErrorResponse RwsErrorResponse { get; private set; }

		// Token: 0x06002A8F RID: 10895 RVA: 0x0005CC06 File Offset: 0x0005AE06
		public RwsErrorResponseException(string message, HttpWebRequestWrapper request, HttpWebResponseWrapper response, RwsErrorResponse rwsErrorResponse) : base(message, request, response)
		{
			this.RwsErrorResponse = rwsErrorResponse;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0005CC1C File Offset: 0x0005AE1C
		public override string ToString()
		{
			string text = base.ToString();
			text = text + Environment.NewLine + Environment.NewLine;
			if (this.RwsErrorResponse != null)
			{
				text = text + this.RwsErrorResponse.ToString() + Environment.NewLine;
			}
			return text;
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002A91 RID: 10897 RVA: 0x0005CC61 File Offset: 0x0005AE61
		public override string ExceptionHint
		{
			get
			{
				return "RwsErrorResponse: " + this.RwsErrorResponse.ExceptionType;
			}
		}
	}
}
