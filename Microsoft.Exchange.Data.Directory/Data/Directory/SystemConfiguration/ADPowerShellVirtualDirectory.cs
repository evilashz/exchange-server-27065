using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200037C RID: 892
	[Serializable]
	public sealed class ADPowerShellVirtualDirectory : ADPowerShellCommonVirtualDirectory
	{
		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002937 RID: 10551 RVA: 0x000AD7FC File Offset: 0x000AB9FC
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADPowerShellVirtualDirectory.schema;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x000AD803 File Offset: 0x000ABA03
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x000AD80B File Offset: 0x000ABA0B
		public bool? RequireSSL { get; internal set; }

		// Token: 0x0400192C RID: 6444
		private static readonly ADPowerShellVirtualDirectorySchema schema = ObjectSchema.GetInstance<ADPowerShellVirtualDirectorySchema>();
	}
}
