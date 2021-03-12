using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200031A RID: 794
	[Serializable]
	public abstract class MailboxPolicy : ADLegacyVersionableObject
	{
		// Token: 0x0600246C RID: 9324
		internal abstract bool CheckForAssociatedUsers();

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x0009D08D File Offset: 0x0009B28D
		// (set) Token: 0x0600246E RID: 9326 RVA: 0x0009D09F File Offset: 0x0009B29F
		internal int MailboxPolicyFlags
		{
			get
			{
				return (int)this[MailboxPolicySchema.MailboxPolicyFlags];
			}
			set
			{
				this[MailboxPolicySchema.MailboxPolicyFlags] = value;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x0009D0B2 File Offset: 0x0009B2B2
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x0009D0B5 File Offset: 0x0009B2B5
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
			set
			{
			}
		}
	}
}
