using System;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A96 RID: 2710
	internal abstract class ChainPolicyParameters
	{
		// Token: 0x06003A72 RID: 14962 RVA: 0x0009562C File Offset: 0x0009382C
		protected ChainPolicyParameters(ChainPolicyOptions options)
		{
			this.flags = options;
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x0009563B File Offset: 0x0009383B
		// (set) Token: 0x06003A74 RID: 14964 RVA: 0x00095643 File Offset: 0x00093843
		public ChainPolicyOptions Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
			}
		}

		// Token: 0x040032C0 RID: 12992
		private ChainPolicyOptions flags;
	}
}
