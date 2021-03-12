using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F1 RID: 2545
	[Serializable]
	public sealed class PendingFederatedDomain : IConfigurable
	{
		// Token: 0x17001B3B RID: 6971
		// (get) Token: 0x06005B07 RID: 23303 RVA: 0x0017CD74 File Offset: 0x0017AF74
		public ObjectId Identity
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001B3C RID: 6972
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x0017CD77 File Offset: 0x0017AF77
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001B3D RID: 6973
		// (get) Token: 0x06005B09 RID: 23305 RVA: 0x0017CD7A File Offset: 0x0017AF7A
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.New;
			}
		}

		// Token: 0x17001B3E RID: 6974
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x0017CD7D File Offset: 0x0017AF7D
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x0017CD85 File Offset: 0x0017AF85
		public SmtpDomain PendingAccountNamespace { get; internal set; }

		// Token: 0x17001B3F RID: 6975
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x0017CD8E File Offset: 0x0017AF8E
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0017CD96 File Offset: 0x0017AF96
		public SmtpDomain[] PendingDomains { get; internal set; }

		// Token: 0x06005B0E RID: 23310 RVA: 0x0017CD9F File Offset: 0x0017AF9F
		public PendingFederatedDomain()
		{
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x0017CDA7 File Offset: 0x0017AFA7
		public PendingFederatedDomain(SmtpDomain pendingAccountNamespace, List<SmtpDomain> pendingDomains)
		{
			this.PendingAccountNamespace = pendingAccountNamespace;
			this.PendingDomains = pendingDomains.ToArray();
		}

		// Token: 0x06005B10 RID: 23312 RVA: 0x0017CDC2 File Offset: 0x0017AFC2
		public void CopyChangesFrom(IConfigurable source)
		{
		}

		// Token: 0x06005B11 RID: 23313 RVA: 0x0017CDC4 File Offset: 0x0017AFC4
		public void ResetChangeTracking()
		{
		}

		// Token: 0x06005B12 RID: 23314 RVA: 0x0017CDC6 File Offset: 0x0017AFC6
		public ValidationError[] Validate()
		{
			return new ValidationError[0];
		}
	}
}
