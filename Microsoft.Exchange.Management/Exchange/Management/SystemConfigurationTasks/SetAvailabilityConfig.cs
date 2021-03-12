using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F7 RID: 2039
	[Cmdlet("Set", "AvailabilityConfig", SupportsShouldProcess = true)]
	public sealed class SetAvailabilityConfig : SetMultitenancySingletonSystemConfigurationObjectTask<AvailabilityConfig>
	{
		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x0600471C RID: 18204 RVA: 0x00123F00 File Offset: 0x00122100
		// (set) Token: 0x0600471D RID: 18205 RVA: 0x00123F17 File Offset: 0x00122117
		[Parameter(Mandatory = false)]
		public SecurityPrincipalIdParameter OrgWideAccount
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["OrgWideAccount"];
			}
			set
			{
				base.Fields["OrgWideAccount"] = value;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x00123F2A File Offset: 0x0012212A
		// (set) Token: 0x0600471F RID: 18207 RVA: 0x00123F41 File Offset: 0x00122141
		[Parameter(Mandatory = false)]
		public SecurityPrincipalIdParameter PerUserAccount
		{
			get
			{
				return (SecurityPrincipalIdParameter)base.Fields["PerUserAccount"];
			}
			set
			{
				base.Fields["PerUserAccount"] = value;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x00123F54 File Offset: 0x00122154
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAvailabilityConfig;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x00123F5C File Offset: 0x0012215C
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				return configurationSession.GetOrgContainerId();
			}
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x00123F7C File Offset: 0x0012217C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			this.DataObject.OrgWideAccount = this.ValidateUser((SecurityPrincipalIdParameter)base.Fields["OrgWideAccount"]);
			this.DataObject.PerUserAccount = this.ValidateUser((SecurityPrincipalIdParameter)base.Fields["PerUserAccount"]);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x00123FE0 File Offset: 0x001221E0
		private ADObjectId ValidateUser(SecurityPrincipalIdParameter principalId)
		{
			if (principalId == null)
			{
				return null;
			}
			IEnumerable<ADRecipient> objects = principalId.GetObjects<ADRecipient>(null, base.TenantGlobalCatalogSession);
			ADObjectId result;
			using (IEnumerator<ADRecipient> enumerator = objects.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorUserNotFound(principalId.ToString())), ErrorCategory.ObjectNotFound, null);
				}
				ADObjectId adobjectId = (ADObjectId)enumerator.Current.Identity;
				if (enumerator.MoveNext())
				{
					base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorUserNotUnique(principalId.ToString())), ErrorCategory.InvalidData, null);
				}
				this.WriteWarning(Strings.AccountPrivilegeWarning);
				result = adobjectId;
			}
			return result;
		}

		// Token: 0x04002B0F RID: 11023
		private const string propOrgWideAccount = "OrgWideAccount";

		// Token: 0x04002B10 RID: 11024
		private const string propPerUserAccount = "PerUserAccount";
	}
}
