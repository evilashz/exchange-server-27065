using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD6 RID: 2774
	[Cmdlet("New", "MSERVEntry", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true)]
	public sealed class NewMSERVEntry : ManageMSERVEntryBase
	{
		// Token: 0x17001DE8 RID: 7656
		// (get) Token: 0x06006291 RID: 25233 RVA: 0x0019B520 File Offset: 0x00199720
		// (set) Token: 0x06006292 RID: 25234 RVA: 0x0019B537 File Offset: 0x00199737
		[Parameter(Mandatory = false, ParameterSetName = "AddressParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		public int PartnerId
		{
			get
			{
				return (int)base.Fields["PartnerId"];
			}
			set
			{
				base.Fields["PartnerId"] = value;
			}
		}

		// Token: 0x17001DE9 RID: 7657
		// (get) Token: 0x06006293 RID: 25235 RVA: 0x0019B54F File Offset: 0x0019974F
		// (set) Token: 0x06006294 RID: 25236 RVA: 0x0019B566 File Offset: 0x00199766
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "AddressParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		public int MinorPartnerId
		{
			get
			{
				return (int)base.Fields["MinorPartnerId"];
			}
			set
			{
				base.Fields["MinorPartnerId"] = value;
			}
		}

		// Token: 0x17001DEA RID: 7658
		// (get) Token: 0x06006295 RID: 25237 RVA: 0x0019B580 File Offset: 0x00199780
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				string id;
				if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
				{
					id = ((Guid)base.Fields["ExternalDirectoryOrganizationId"]).ToString();
				}
				else if (base.Fields.IsModified("DomainName"))
				{
					id = ((SmtpDomain)base.Fields["DomainName"]).Domain;
				}
				else
				{
					id = (string)base.Fields["Address"];
				}
				return Strings.ConfirmationMessageNewMservEntry(id);
			}
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x0019B610 File Offset: 0x00199810
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!base.Fields.IsModified("PartnerId") && !base.Fields.IsModified("MinorPartnerId"))
			{
				base.WriteError(new ParameterBindingException("Either PartnerId or MinorPartnerId should be specified"), ErrorCategory.InvalidArgument, null);
			}
			if (base.Fields.IsModified("PartnerId"))
			{
				this.partnerId = (int)base.Fields["PartnerId"];
				base.ValidateMservIdValue(this.partnerId);
			}
			if (base.Fields.IsModified("MinorPartnerId"))
			{
				this.minorPartnerId = (int)base.Fields["MinorPartnerId"];
				base.ValidateMservIdValue(this.minorPartnerId);
			}
			if (base.Fields.IsModified("Address"))
			{
				if (base.Fields.IsModified("PartnerId") && base.Fields.IsModified("MinorPartnerId"))
				{
					base.WriteError(new ParameterBindingException("Both PartnerId and MinorPartnerId cannot be specified when address parameter is used"), ErrorCategory.InvalidArgument, null);
				}
				base.ValidateAddressParameter((string)base.Fields["Address"]);
			}
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x0019B74C File Offset: 0x0019994C
		protected override void InternalProcessRecord()
		{
			MSERVEntry sendToPipeline = new MSERVEntry();
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				sendToPipeline = base.ProcessExternalOrgIdParameter(this.partnerId, this.minorPartnerId, (string address, int id) => base.AddMservEntry(address, id));
			}
			else if (base.Fields.IsModified("DomainName"))
			{
				sendToPipeline = base.ProcessDomainNameParameter(this.partnerId, this.minorPartnerId, (string address, int id) => base.AddMservEntry(address, id));
			}
			else
			{
				sendToPipeline = base.ProcessAddressParameter((this.partnerId != -1) ? this.partnerId : this.minorPartnerId, (string address, int id) => base.AddMservEntry(address, id));
			}
			base.WriteObject(sendToPipeline);
		}

		// Token: 0x040035DE RID: 13790
		private int partnerId = -1;

		// Token: 0x040035DF RID: 13791
		private int minorPartnerId = -1;
	}
}
