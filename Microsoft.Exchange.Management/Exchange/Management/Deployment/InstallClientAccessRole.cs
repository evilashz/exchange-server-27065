using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E6 RID: 486
	[Cmdlet("Install", "ClientAccessRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallClientAccessRole : ManageRole
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x00049785 File Offset: 0x00047985
		public InstallClientAccessRole()
		{
			base.Fields["NoSelfSignedCertificates"] = false;
			base.Fields["CustomerFeedbackEnabled"] = null;
			base.Fields["ExternalCASServerDomain"] = null;
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x000497C5 File Offset: 0x000479C5
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallClientAccessRoleDescription;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x000497CC File Offset: 0x000479CC
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x000497E8 File Offset: 0x000479E8
		[Parameter(Mandatory = false)]
		public SwitchParameter NoSelfSignedCertificates
		{
			get
			{
				return new SwitchParameter((bool)base.Fields["NoSelfSignedCertificates"]);
			}
			set
			{
				base.Fields["NoSelfSignedCertificates"] = value.ToBool();
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00049806 File Offset: 0x00047A06
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x0004981D File Offset: 0x00047A1D
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)base.Fields["CustomerFeedbackEnabled"];
			}
			set
			{
				base.Fields["CustomerFeedbackEnabled"] = value;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00049835 File Offset: 0x00047A35
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x0004984C File Offset: 0x00047A4C
		[Parameter(Mandatory = false)]
		public Fqdn ExternalCASServerDomain
		{
			get
			{
				return (Fqdn)base.Fields["ExternalCASServerDomain"];
			}
			set
			{
				base.Fields["ExternalCASServerDomain"] = value;
			}
		}
	}
}
