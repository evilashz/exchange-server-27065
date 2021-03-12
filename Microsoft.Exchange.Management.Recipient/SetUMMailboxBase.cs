using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Common;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F0 RID: 240
	public abstract class SetUMMailboxBase<TIdentity, TPublicObject> : SetRecipientObjectTask<TIdentity, TPublicObject, ADUser> where TIdentity : IIdentityParameter, new() where TPublicObject : IConfigurable, new()
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x000420E0 File Offset: 0x000402E0
		protected override IConfigurable ResolveDataObject()
		{
			ADRecipient adrecipient = (ADRecipient)base.ResolveDataObject();
			if (MailboxTaskHelper.ExcludeArbitrationMailbox(adrecipient, false))
			{
				TIdentity identity = this.Identity;
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(identity.ToString(), typeof(ADUser).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, this.Identity);
			}
			return adrecipient;
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0004215D File Offset: 0x0004035D
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00042174 File Offset: 0x00040374
		[Parameter(Mandatory = false)]
		public MailboxPolicyIdParameter UMMailboxPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["UMMailboxPolicy"];
			}
			set
			{
				base.Fields["UMMailboxPolicy"] = value;
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00042187 File Offset: 0x00040387
		public SetUMMailboxBase()
		{
		}

		// Token: 0x0400037A RID: 890
		private const string UMMailboxPolicyName = "UMMailboxPolicy";
	}
}
