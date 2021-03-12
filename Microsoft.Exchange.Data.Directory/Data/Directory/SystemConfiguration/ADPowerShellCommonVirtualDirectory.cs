using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200037B RID: 891
	[Serializable]
	public class ADPowerShellCommonVirtualDirectory : ADExchangeServiceVirtualDirectory
	{
		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002930 RID: 10544 RVA: 0x000AD7AB File Offset: 0x000AB9AB
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADPowerShellCommonVirtualDirectory.MostDerivedClass;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000AD7B2 File Offset: 0x000AB9B2
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x000AD7BA File Offset: 0x000AB9BA
		public bool? CertificateAuthentication { get; internal set; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000AD7C3 File Offset: 0x000AB9C3
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x000AD7D5 File Offset: 0x000AB9D5
		public PowerShellVirtualDirectoryType VirtualDirectoryType
		{
			get
			{
				return (PowerShellVirtualDirectoryType)this[ADPowerShellCommonVirtualDirectorySchema.VirtualDirectoryType];
			}
			internal set
			{
				this[ADPowerShellCommonVirtualDirectorySchema.VirtualDirectoryType] = value;
			}
		}

		// Token: 0x0400192A RID: 6442
		public static readonly string MostDerivedClass = "msExchPowerShellVirtualDirectory";
	}
}
