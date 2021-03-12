using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A48 RID: 2632
	[DataContract]
	public class EwsProxyResponse
	{
		// Token: 0x06004A69 RID: 19049 RVA: 0x00104704 File Offset: 0x00102904
		public EwsProxyResponse(string errorMessage)
		{
			this.WasProxySuccessful = false;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x0010471A File Offset: 0x0010291A
		public EwsProxyResponse(int statusCode, string statusDescription, string body)
		{
			this.WasProxySuccessful = true;
			this.StatusCode = statusCode;
			this.StatusDescription = statusDescription;
			this.Body = body;
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x0010473E File Offset: 0x0010293E
		// (set) Token: 0x06004A6C RID: 19052 RVA: 0x00104746 File Offset: 0x00102946
		[DataMember]
		public bool WasProxySuccessful { get; set; }

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x0010474F File Offset: 0x0010294F
		// (set) Token: 0x06004A6E RID: 19054 RVA: 0x00104757 File Offset: 0x00102957
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x00104760 File Offset: 0x00102960
		// (set) Token: 0x06004A70 RID: 19056 RVA: 0x00104768 File Offset: 0x00102968
		[DataMember]
		public int StatusCode { get; set; }

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x00104771 File Offset: 0x00102971
		// (set) Token: 0x06004A72 RID: 19058 RVA: 0x00104779 File Offset: 0x00102979
		[DataMember]
		public string StatusDescription { get; set; }

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x00104782 File Offset: 0x00102982
		// (set) Token: 0x06004A74 RID: 19060 RVA: 0x0010478A File Offset: 0x0010298A
		[DataMember]
		public string Body { get; set; }
	}
}
