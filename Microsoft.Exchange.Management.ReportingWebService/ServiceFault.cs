using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x0200003C RID: 60
	[DataContract]
	public class ServiceFault
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00007778 File Offset: 0x00005978
		public ServiceFault(string errorCode, Exception ex)
		{
			this.ErrorCode = errorCode;
			this.Message = ex.Message;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007793 File Offset: 0x00005993
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000779B File Offset: 0x0000599B
		[DataMember]
		public string ErrorCode { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000077A4 File Offset: 0x000059A4
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000077AC File Offset: 0x000059AC
		[DataMember]
		public string Message { get; set; }
	}
}
