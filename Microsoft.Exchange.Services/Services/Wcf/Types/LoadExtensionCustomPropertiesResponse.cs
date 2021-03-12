using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A4B RID: 2635
	[DataContract]
	public class LoadExtensionCustomPropertiesResponse
	{
		// Token: 0x06004AA1 RID: 19105 RVA: 0x00104925 File Offset: 0x00102B25
		public LoadExtensionCustomPropertiesResponse(string customProperties, string customPropertyNames, string errorMessage)
		{
			if (errorMessage != null)
			{
				this.WasSuccessful = false;
				this.ErrorMessage = errorMessage;
				return;
			}
			this.WasSuccessful = true;
			this.CustomProperties = customProperties;
			this.CustomPropertyNames = customPropertyNames;
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x00104954 File Offset: 0x00102B54
		// (set) Token: 0x06004AA3 RID: 19107 RVA: 0x0010495C File Offset: 0x00102B5C
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x00104965 File Offset: 0x00102B65
		// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x0010496D File Offset: 0x00102B6D
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x00104976 File Offset: 0x00102B76
		// (set) Token: 0x06004AA7 RID: 19111 RVA: 0x0010497E File Offset: 0x00102B7E
		[DataMember]
		public string CustomProperties { get; set; }

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00104987 File Offset: 0x00102B87
		// (set) Token: 0x06004AA9 RID: 19113 RVA: 0x0010498F File Offset: 0x00102B8F
		[DataMember]
		public string CustomPropertyNames { get; set; }
	}
}
