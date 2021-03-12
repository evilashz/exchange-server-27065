using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.DefaultProvisioningAgent.PolicyEngine;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000021 RID: 33
	internal class ADPolicyProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000670E File Offset: 0x0000490E
		internal ProvisioningSession Session
		{
			get
			{
				if (this.provisioningSession == null)
				{
					this.provisioningSession = new ProvisioningSession(this);
				}
				return this.provisioningSession;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000672A File Offset: 0x0000492A
		public override IConfigurable ProvisionDefaultProperties(IConfigurable readOnlyIConfigurable)
		{
			return this.Session.ProvisionDefaultProperties();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006737 File Offset: 0x00004937
		public override ProvisioningValidationError[] Validate(IConfigurable readOnlyADObject)
		{
			if (readOnlyADObject == null)
			{
				return null;
			}
			return this.Session.Validate(readOnlyADObject);
		}

		// Token: 0x04000095 RID: 149
		private ProvisioningSession provisioningSession;
	}
}
