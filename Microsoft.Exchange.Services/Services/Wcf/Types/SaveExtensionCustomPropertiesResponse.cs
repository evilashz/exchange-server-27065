using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A4C RID: 2636
	[DataContract]
	public class SaveExtensionCustomPropertiesResponse
	{
		// Token: 0x06004AAA RID: 19114 RVA: 0x00104998 File Offset: 0x00102B98
		public SaveExtensionCustomPropertiesResponse(string errorMessage)
		{
			this.WasSuccessful = false;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x001049AE File Offset: 0x00102BAE
		public SaveExtensionCustomPropertiesResponse()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06004AAC RID: 19116 RVA: 0x001049BD File Offset: 0x00102BBD
		// (set) Token: 0x06004AAD RID: 19117 RVA: 0x001049C5 File Offset: 0x00102BC5
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06004AAE RID: 19118 RVA: 0x001049CE File Offset: 0x00102BCE
		// (set) Token: 0x06004AAF RID: 19119 RVA: 0x001049D6 File Offset: 0x00102BD6
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
