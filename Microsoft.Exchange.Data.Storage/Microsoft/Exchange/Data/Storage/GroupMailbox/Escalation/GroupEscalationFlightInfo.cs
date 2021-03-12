using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation
{
	// Token: 0x02000808 RID: 2056
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupEscalationFlightInfo : IGroupEscalationFlightInfo
	{
		// Token: 0x06004C9A RID: 19610 RVA: 0x0013D8EB File Offset: 0x0013BAEB
		public GroupEscalationFlightInfo(IConstraintProvider constraintProvider)
		{
			this.constraintProvider = constraintProvider;
		}

		// Token: 0x06004C9B RID: 19611 RVA: 0x0013D8FA File Offset: 0x0013BAFA
		public bool IsGroupEscalationFooterEnabled()
		{
			return true;
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x0013D8FD File Offset: 0x0013BAFD
		private VariantConfigurationSnapshot VariantConfig
		{
			get
			{
				if (this.variantConfiguration == null)
				{
					this.variantConfiguration = VariantConfiguration.GetSnapshot(this.constraintProvider, null, null);
				}
				return this.variantConfiguration;
			}
		}

		// Token: 0x040029CE RID: 10702
		private readonly IConstraintProvider constraintProvider;

		// Token: 0x040029CF RID: 10703
		private VariantConfigurationSnapshot variantConfiguration;
	}
}
