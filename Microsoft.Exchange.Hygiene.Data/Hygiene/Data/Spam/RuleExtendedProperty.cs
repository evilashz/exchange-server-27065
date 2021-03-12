using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001F4 RID: 500
	internal class RuleExtendedProperty : ConfigurablePropertyTable
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00041F9C File Offset: 0x0004019C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.ID.ToString());
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00041FC2 File Offset: 0x000401C2
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00041FD4 File Offset: 0x000401D4
		public Guid ID
		{
			get
			{
				return (Guid)this[RuleExtendedProperty.IDProperty];
			}
			set
			{
				this[RuleExtendedProperty.IDProperty] = value;
			}
		}

		// Token: 0x04000A88 RID: 2696
		public static readonly HygienePropertyDefinition IDProperty = new HygienePropertyDefinition("id_RuleId", typeof(Guid));
	}
}
