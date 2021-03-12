using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Management.Automation;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Permission;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientPermission
{
	// Token: 0x0200068C RID: 1676
	public abstract class SetRecipientPermissionTaskBase : SetRecipientObjectTask<RecipientIdParameter, RecipientPermission, ADRecipient>
	{
		// Token: 0x170011AF RID: 4527
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000FCA38 File Offset: 0x000FAC38
		protected new SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.IgnoreDefaultScope;
			}
		}

		// Token: 0x170011B0 RID: 4528
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x000FCA40 File Offset: 0x000FAC40
		// (set) Token: 0x06003B5A RID: 15194 RVA: 0x000FCA57 File Offset: 0x000FAC57
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public SecurityPrincipalIdParameter Trustee
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["Trustee"];
			}
			set
			{
				base.Fields["Trustee"] = value;
			}
		}

		// Token: 0x170011B1 RID: 4529
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000FCA6A File Offset: 0x000FAC6A
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x000FCA81 File Offset: 0x000FAC81
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public MultiValuedProperty<RecipientAccessRight> AccessRights
		{
			get
			{
				return (MultiValuedProperty<RecipientAccessRight>)base.Fields["AccessRights"];
			}
			set
			{
				base.Fields["AccessRights"] = value;
			}
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000FCA94 File Offset: 0x000FAC94
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.trustee = (ADRecipient)SecurityPrincipalIdParameter.GetSecurityPrincipal((IRecipientSession)base.DataSession, this.Trustee, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			List<ActiveDirectoryAccessRule> list = new List<ActiveDirectoryAccessRule>();
			foreach (RecipientAccessRight right in this.AccessRights)
			{
				list.Add(new ActiveDirectoryAccessRule(((IADSecurityPrincipal)this.trustee).Sid, ActiveDirectoryRights.ExtendedRight, AccessControlType.Allow, RecipientPermissionHelper.GetRecipientAccessRightGuid(right), this.GetInheritanceType(), Guid.Empty));
			}
			this.ApplyModification(list.ToArray());
			TaskLogger.LogExit();
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000FCB68 File Offset: 0x000FAD68
		protected void WriteResults(ActiveDirectoryAccessRule[] modifiedAces)
		{
			foreach (ActiveDirectoryAccessRule activeDirectoryAccessRule in modifiedAces)
			{
				string friendlyNameOfSecurityIdentifier = RecipientPermissionTaskHelper.GetFriendlyNameOfSecurityIdentifier((SecurityIdentifier)activeDirectoryAccessRule.IdentityReference, base.TenantGlobalCatalogSession, new Task.TaskErrorLoggingDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				RecipientPermission sendToPipeline = new RecipientPermission(activeDirectoryAccessRule, this.DataObject.Id, friendlyNameOfSecurityIdentifier);
				base.WriteObject(sendToPipeline);
			}
		}

		// Token: 0x06003B5F RID: 15199
		protected abstract void ApplyModification(ActiveDirectoryAccessRule[] modifiedAces);

		// Token: 0x06003B60 RID: 15200
		protected abstract ActiveDirectorySecurityInheritance GetInheritanceType();

		// Token: 0x040026C0 RID: 9920
		protected ADRecipient trustee;
	}
}
