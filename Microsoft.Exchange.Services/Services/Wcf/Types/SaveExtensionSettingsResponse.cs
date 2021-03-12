using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A4D RID: 2637
	[DataContract]
	public class SaveExtensionSettingsResponse
	{
		// Token: 0x06004AB0 RID: 19120 RVA: 0x001049DF File Offset: 0x00102BDF
		public SaveExtensionSettingsResponse(string errorMessage)
		{
			this.WasSuccessful = false;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x001049F5 File Offset: 0x00102BF5
		public SaveExtensionSettingsResponse()
		{
			this.WasSuccessful = true;
		}

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x00104A04 File Offset: 0x00102C04
		// (set) Token: 0x06004AB3 RID: 19123 RVA: 0x00104A0C File Offset: 0x00102C0C
		[DataMember]
		public bool WasSuccessful { get; set; }

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x06004AB4 RID: 19124 RVA: 0x00104A15 File Offset: 0x00102C15
		// (set) Token: 0x06004AB5 RID: 19125 RVA: 0x00104A1D File Offset: 0x00102C1D
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
