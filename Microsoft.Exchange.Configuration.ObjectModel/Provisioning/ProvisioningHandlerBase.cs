using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Provisioning
{
	// Token: 0x020001FC RID: 508
	public abstract class ProvisioningHandlerBase : ProvisioningHandler
	{
		// Token: 0x060011E0 RID: 4576 RVA: 0x000375C0 File Offset: 0x000357C0
		public override IConfigurable ProvisionDefaultProperties(IConfigurable readOnlyIConfigurable)
		{
			return null;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000375C3 File Offset: 0x000357C3
		public override bool UpdateAffectedIConfigurable(IConfigurable writeableIConfigurable)
		{
			return false;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000375C6 File Offset: 0x000357C6
		public override bool PreInternalProcessRecord(IConfigurable writeableIConfigurable)
		{
			return false;
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000375C9 File Offset: 0x000357C9
		public override ProvisioningValidationError[] Validate(IConfigurable readOnlyIConfigurable)
		{
			return null;
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000375CC File Offset: 0x000357CC
		public override ProvisioningValidationError[] ValidateUserScope()
		{
			return null;
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x000375CF File Offset: 0x000357CF
		public override void OnComplete(bool succeeded, Exception e)
		{
		}
	}
}
