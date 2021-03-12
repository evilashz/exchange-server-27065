using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004CE RID: 1230
	[DataContract]
	public class UMExtension
	{
		// Token: 0x06003C55 RID: 15445 RVA: 0x000B53A4 File Offset: 0x000B35A4
		public UMExtension()
		{
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x000B53AC File Offset: 0x000B35AC
		public UMExtension(string extension, string phoneContext, string dpName)
		{
			this.Extension = extension;
			this.PhoneContext = phoneContext;
			this.DialPlanName = dpName;
		}

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x06003C57 RID: 15447 RVA: 0x000B53C9 File Offset: 0x000B35C9
		// (set) Token: 0x06003C58 RID: 15448 RVA: 0x000B53D1 File Offset: 0x000B35D1
		[DataMember]
		public string PhoneContext { get; set; }

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x000B53DA File Offset: 0x000B35DA
		// (set) Token: 0x06003C5A RID: 15450 RVA: 0x000B53E2 File Offset: 0x000B35E2
		[DataMember]
		public string DialPlanName { get; set; }

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000B53EB File Offset: 0x000B35EB
		// (set) Token: 0x06003C5C RID: 15452 RVA: 0x000B53F3 File Offset: 0x000B35F3
		[DataMember]
		public string Extension { get; set; }
	}
}
