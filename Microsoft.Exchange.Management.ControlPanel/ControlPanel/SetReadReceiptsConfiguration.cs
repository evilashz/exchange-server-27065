using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000A3 RID: 163
	[DataContract]
	public class SetReadReceiptsConfiguration : SetMessagingConfigurationBase
	{
		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x00057D42 File Offset: 0x00055F42
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x00057D54 File Offset: 0x00055F54
		[DataMember]
		public string ReadReceiptResponse
		{
			get
			{
				return (string)base["ReadReceiptResponse"];
			}
			set
			{
				base["ReadReceiptResponse"] = value;
			}
		}
	}
}
