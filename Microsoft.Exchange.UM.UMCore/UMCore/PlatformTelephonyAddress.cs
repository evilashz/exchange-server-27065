using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000300 RID: 768
	internal class PlatformTelephonyAddress
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0006385F File Offset: 0x00061A5F
		public PlatformTelephonyAddress(string name, PlatformSipUri uri)
		{
			this.Name = name;
			this.Uri = uri;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x00063875 File Offset: 0x00061A75
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0006387D File Offset: 0x00061A7D
		public string Name { get; private set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00063886 File Offset: 0x00061A86
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x0006388E File Offset: 0x00061A8E
		public PlatformSipUri Uri { get; private set; }
	}
}
