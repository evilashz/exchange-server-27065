using System;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Microsoft.Exchange.AutoDiscoverV2
{
	// Token: 0x02000011 RID: 17
	[DataContract]
	internal class JsonSuccessResponse
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002CFE File Offset: 0x00000EFE
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002D06 File Offset: 0x00000F06
		[DataMember]
		public string Protocol { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002D0F File Offset: 0x00000F0F
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002D17 File Offset: 0x00000F17
		[DataMember]
		public string Url { get; set; }

		// Token: 0x06000054 RID: 84 RVA: 0x00002D20 File Offset: 0x00000F20
		public string SerializeToJson(object value)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(value);
		}
	}
}
