using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000012 RID: 18
	[DataContract]
	public class JsonErrorResponse
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002D42 File Offset: 0x00000F42
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002D4A File Offset: 0x00000F4A
		[DataMember]
		public string ErrorCode { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002D53 File Offset: 0x00000F53
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002D5B File Offset: 0x00000F5B
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x0600005A RID: 90 RVA: 0x00002D64 File Offset: 0x00000F64
		public string SerializeToJson(object value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(value);
		}
	}
}
