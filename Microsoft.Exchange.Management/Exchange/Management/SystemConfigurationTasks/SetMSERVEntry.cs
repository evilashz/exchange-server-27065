using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD8 RID: 2776
	[Cmdlet("Set", "MSERVEntry", DefaultParameterSetName = "ExternalDirectoryOrganizationIdParameterSet", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class SetMSERVEntry : ManageMSERVEntryBase
	{
		// Token: 0x17001DEC RID: 7660
		// (get) Token: 0x060062A2 RID: 25250 RVA: 0x0019B959 File Offset: 0x00199B59
		// (set) Token: 0x060062A3 RID: 25251 RVA: 0x0019B970 File Offset: 0x00199B70
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "AddressParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x17001DED RID: 7661
		// (get) Token: 0x060062A4 RID: 25252 RVA: 0x0019B988 File Offset: 0x00199B88
		// (set) Token: 0x060062A5 RID: 25253 RVA: 0x0019B99F File Offset: 0x00199B9F
		[Parameter(Mandatory = false, ParameterSetName = "AddressParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[Parameter(Mandatory = false, ParameterSetName = "DomainNameParameterSet")]
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

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x060062A6 RID: 25254 RVA: 0x0019B9B8 File Offset: 0x00199BB8
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
				return Strings.ConfirmationMessageSetMservEntry(id);
			}
		}

		// Token: 0x060062A7 RID: 25255 RVA: 0x0019BA48 File Offset: 0x00199C48
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

		// Token: 0x060062A8 RID: 25256 RVA: 0x0019BB84 File Offset: 0x00199D84
		protected override void InternalProcessRecord()
		{
			new MSERVEntry();
			if (base.Fields.IsModified("ExternalDirectoryOrganizationId"))
			{
				base.ProcessExternalOrgIdParameter(this.partnerId, this.minorPartnerId, (string address, int id) => base.UpdateMservEntry(address, id));
				return;
			}
			if (base.Fields.IsModified("DomainName"))
			{
				base.ProcessDomainNameParameter(this.partnerId, this.minorPartnerId, (string address, int id) => base.UpdateMservEntry(address, id));
				return;
			}
			base.ProcessAddressParameter((this.partnerId != -1) ? this.partnerId : this.minorPartnerId, (string address, int id) => base.UpdateMservEntry(address, id));
		}

		// Token: 0x040035E0 RID: 13792
		private int partnerId = -1;

		// Token: 0x040035E1 RID: 13793
		private int minorPartnerId = -1;
	}
}
