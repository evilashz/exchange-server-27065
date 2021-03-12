using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001EF RID: 495
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "FfoWebServiceRole", SupportsShouldProcess = true)]
	public sealed class InstallFfoWebServiceRole : ManageFfoWebServiceRole
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x0004A72D File Offset: 0x0004892D
		public InstallFfoWebServiceRole()
		{
			base.Fields["NoSelfSignedCertificates"] = false;
			base.Fields["CustomerFeedbackEnabled"] = null;
			base.Fields["ExternalCASServerDomain"] = null;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0004A76D File Offset: 0x0004896D
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallFfoWebServiceRoleDescription;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0004A774 File Offset: 0x00048974
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x0004A790 File Offset: 0x00048990
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

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0004A7AE File Offset: 0x000489AE
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x0004A7C5 File Offset: 0x000489C5
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

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0004A7DD File Offset: 0x000489DD
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0004A7F4 File Offset: 0x000489F4
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

		// Token: 0x04000789 RID: 1929
		private const string NoSelfSignedCertificatesProperty = "NoSelfSignedCertificates";

		// Token: 0x0400078A RID: 1930
		private const string CustomerFeedbackEnabledProperty = "CustomerFeedbackEnabled";

		// Token: 0x0400078B RID: 1931
		private const string ExternalCASServerDomainProperty = "ExternalCASServerDomain";
	}
}
