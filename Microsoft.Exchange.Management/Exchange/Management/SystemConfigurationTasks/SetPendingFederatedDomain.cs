using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F9 RID: 2553
	[Cmdlet("Set", "PendingFederatedDomain", SupportsShouldProcess = true)]
	public sealed class SetPendingFederatedDomain : SetFederatedOrganizationIdBase
	{
		// Token: 0x17001B6A RID: 7018
		// (get) Token: 0x06005B7F RID: 23423 RVA: 0x0017EB79 File Offset: 0x0017CD79
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPendingFederationDomain;
			}
		}

		// Token: 0x17001B6B RID: 7019
		// (get) Token: 0x06005B80 RID: 23424 RVA: 0x0017EB80 File Offset: 0x0017CD80
		// (set) Token: 0x06005B81 RID: 23425 RVA: 0x0017EB97 File Offset: 0x0017CD97
		[Parameter]
		public SmtpDomain PendingAccountNamespace
		{
			get
			{
				return base.Fields["PendingAccountNamespace"] as SmtpDomain;
			}
			set
			{
				base.Fields["PendingAccountNamespace"] = value;
			}
		}

		// Token: 0x17001B6C RID: 7020
		// (get) Token: 0x06005B82 RID: 23426 RVA: 0x0017EBAA File Offset: 0x0017CDAA
		// (set) Token: 0x06005B83 RID: 23427 RVA: 0x0017EBC1 File Offset: 0x0017CDC1
		[Parameter]
		public SmtpDomain[] PendingDomains
		{
			get
			{
				return base.Fields["PendingFederatedDomain"] as SmtpDomain[];
			}
			set
			{
				base.Fields["PendingFederatedDomain"] = value;
			}
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0017EBF4 File Offset: 0x0017CDF4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IEnumerable<AcceptedDomain> enumerable = base.DataSession.FindPaged<AcceptedDomain>(null, base.CurrentOrgContainerId, true, null, 0);
			using (IEnumerator<AcceptedDomain> enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AcceptedDomain acceptedDomain = enumerator.Current;
					bool pendingFederatedAccountNamespace = acceptedDomain.PendingFederatedAccountNamespace;
					bool pendingFederatedDomain = acceptedDomain.PendingFederatedDomain;
					acceptedDomain.PendingFederatedAccountNamespace = false;
					acceptedDomain.PendingFederatedDomain = false;
					if (acceptedDomain.DomainName.SmtpDomain.Equals(this.PendingAccountNamespace))
					{
						acceptedDomain.PendingFederatedAccountNamespace = true;
					}
					if (this.PendingDomains != null)
					{
						if (Array.Exists<SmtpDomain>(this.PendingDomains, (SmtpDomain pendingDomain) => pendingDomain.Equals(acceptedDomain.DomainName.SmtpDomain)))
						{
							acceptedDomain.PendingFederatedDomain = true;
						}
					}
					if (acceptedDomain.PendingFederatedAccountNamespace != pendingFederatedAccountNamespace || acceptedDomain.PendingFederatedDomain != pendingFederatedDomain)
					{
						base.DataSession.Save(acceptedDomain);
					}
				}
			}
			TaskLogger.LogExit();
		}
	}
}
