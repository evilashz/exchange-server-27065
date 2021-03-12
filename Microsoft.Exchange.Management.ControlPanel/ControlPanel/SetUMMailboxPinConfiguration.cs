using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C3 RID: 1219
	[DataContract]
	public class SetUMMailboxPinConfiguration : UMBasePinSetConfiguration
	{
		// Token: 0x06003BE4 RID: 15332 RVA: 0x000B4A8C File Offset: 0x000B2C8C
		public SetUMMailboxPinConfiguration(UMMailbox umMailbox) : base(umMailbox)
		{
			this.policy = umMailbox.GetPolicy();
		}

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x000B4AA1 File Offset: 0x000B2CA1
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x000B4AB8 File Offset: 0x000B2CB8
		[DataMember]
		public int MinPinLength
		{
			get
			{
				if (this.policy == null)
				{
					return 0;
				}
				return this.policy.MinPINLength;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002787 RID: 10119
		private UMMailboxPolicy policy;
	}
}
