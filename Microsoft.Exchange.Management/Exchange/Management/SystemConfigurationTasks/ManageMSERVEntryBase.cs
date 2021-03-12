using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD4 RID: 2772
	public class ManageMSERVEntryBase : Task
	{
		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06006278 RID: 25208 RVA: 0x0019B084 File Offset: 0x00199284
		// (set) Token: 0x06006279 RID: 25209 RVA: 0x0019B09B File Offset: 0x0019929B
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "ExternalDirectoryOrganizationIdParameterSet")]
		[ValidateNotNull]
		public Guid ExternalDirectoryOrganizationId
		{
			get
			{
				return (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			}
			set
			{
				base.Fields["ExternalDirectoryOrganizationId"] = value;
			}
		}

		// Token: 0x17001DE6 RID: 7654
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x0019B0B3 File Offset: 0x001992B3
		// (set) Token: 0x0600627B RID: 25211 RVA: 0x0019B0CA File Offset: 0x001992CA
		[ValidateNotNull]
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "DomainNameParameterSet")]
		public SmtpDomain DomainName
		{
			get
			{
				return (SmtpDomain)base.Fields["DomainName"];
			}
			set
			{
				base.Fields["DomainName"] = value;
			}
		}

		// Token: 0x17001DE7 RID: 7655
		// (get) Token: 0x0600627C RID: 25212 RVA: 0x0019B0DD File Offset: 0x001992DD
		// (set) Token: 0x0600627D RID: 25213 RVA: 0x0019B0F4 File Offset: 0x001992F4
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "AddressParameterSet")]
		public string Address
		{
			get
			{
				return (string)base.Fields["Address"];
			}
			set
			{
				base.Fields["Address"] = value;
			}
		}

		// Token: 0x0600627E RID: 25214 RVA: 0x0019B107 File Offset: 0x00199307
		protected MSERVEntry ProcessExternalOrgIdParameter(Func<string, int, int> invoke)
		{
			return this.ProcessExternalOrgIdParameter(0, 0, invoke);
		}

		// Token: 0x0600627F RID: 25215 RVA: 0x0019B114 File Offset: 0x00199314
		protected MSERVEntry ProcessExternalOrgIdParameter(int partnerId, int minorPartnerId, Func<string, int, int> invoke)
		{
			MSERVEntry mserventry = new MSERVEntry();
			Guid guid = (Guid)base.Fields["ExternalDirectoryOrganizationId"];
			mserventry.ExternalDirectoryOrganizationId = guid;
			if (partnerId > -1)
			{
				string text = string.Format("43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved", guid);
				mserventry.AddressForPartnerId = text;
				mserventry.PartnerId = invoke(text, partnerId);
				MServDirectorySession mservDirectorySession = new MServDirectorySession(null);
				string forest;
				if (mservDirectorySession.TryGetForestFqdnFromPartnerId(mserventry.PartnerId, out forest))
				{
					mserventry.Forest = forest;
				}
			}
			if (minorPartnerId > -1)
			{
				string text = string.Format("3da19c7b44a74bd3896daaf008594b6c@{0}.exchangereserved", guid);
				mserventry.AddressForMinorPartnerId = text;
				mserventry.MinorPartnerId = invoke(text, minorPartnerId);
			}
			return mserventry;
		}

		// Token: 0x06006280 RID: 25216 RVA: 0x0019B1B9 File Offset: 0x001993B9
		protected MSERVEntry ProcessDomainNameParameter(Func<string, int, int> invoke)
		{
			return this.ProcessDomainNameParameter(0, 0, invoke);
		}

		// Token: 0x06006281 RID: 25217 RVA: 0x0019B1C4 File Offset: 0x001993C4
		protected MSERVEntry ProcessDomainNameParameter(int partnerId, int minorPartnerId, Func<string, int, int> invoke)
		{
			MSERVEntry mserventry = new MSERVEntry();
			string domain = ((SmtpDomain)base.Fields["DomainName"]).Domain;
			mserventry.DomainName = domain;
			if (partnerId > -1)
			{
				string text = string.Format("E5CB63F56E8B4b69A1F70C192276D6AD@{0}", domain);
				mserventry.AddressForPartnerId = text;
				mserventry.PartnerId = invoke(text, partnerId);
				MServDirectorySession mservDirectorySession = new MServDirectorySession(null);
				string forest;
				if (mservDirectorySession.TryGetForestFqdnFromPartnerId(mserventry.PartnerId, out forest))
				{
					mserventry.Forest = forest;
				}
			}
			if (minorPartnerId > -1)
			{
				string text = string.Format("7f66cd009b304aeda37ffdeea1733ff6@{0}", domain);
				mserventry.AddressForMinorPartnerId = text;
				mserventry.MinorPartnerId = invoke(text, minorPartnerId);
			}
			return mserventry;
		}

		// Token: 0x06006282 RID: 25218 RVA: 0x0019B264 File Offset: 0x00199464
		protected MSERVEntry ProcessAddressParameter(Func<string, int, int> invoke)
		{
			return this.ProcessAddressParameter(0, invoke);
		}

		// Token: 0x06006283 RID: 25219 RVA: 0x0019B270 File Offset: 0x00199470
		protected MSERVEntry ProcessAddressParameter(int id, Func<string, int, int> invoke)
		{
			MSERVEntry mserventry = new MSERVEntry();
			string text = (string)base.Fields["Address"];
			if (this.ShouldProcessPartnerId(text))
			{
				mserventry.AddressForPartnerId = text;
				mserventry.PartnerId = invoke(text, id);
				MServDirectorySession mservDirectorySession = new MServDirectorySession(null);
				string forest;
				if (mservDirectorySession.TryGetForestFqdnFromPartnerId(mserventry.PartnerId, out forest))
				{
					mserventry.Forest = forest;
				}
			}
			else
			{
				mserventry.AddressForMinorPartnerId = text;
				mserventry.MinorPartnerId = invoke(text, id);
			}
			return mserventry;
		}

		// Token: 0x06006284 RID: 25220 RVA: 0x0019B2F0 File Offset: 0x001994F0
		protected bool ShouldProcessPartnerId(string address)
		{
			string text = address.Split(new char[]
			{
				'@'
			})[0];
			return "E5CB63F56E8B4b69A1F70C192276D6AD@{0}".ToLower().Contains(text.ToLower()) || "43BA6209CC0F4542958F65F8BF1CDED6@{0}.exchangereserved".ToLower().Contains(text.ToLower());
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x0019B344 File Offset: 0x00199544
		protected void ValidateAddressParameter(string address)
		{
			if (this.ShouldProcessPartnerId(address))
			{
				if (!base.Fields.IsModified("PartnerId"))
				{
					base.WriteError(new ParameterBindingException("PartnerId is not specified"), ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			else if (!base.Fields.IsModified("MinorPartnerId"))
			{
				base.WriteError(new ParameterBindingException("MinorPartnerId is not specified"), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x06006286 RID: 25222 RVA: 0x0019B3A3 File Offset: 0x001995A3
		protected void ValidateMservIdValue(int id)
		{
			if (id == -1 || id < 50000 || id > 59999)
			{
				base.WriteError(new InvalidPartnerIdException(Strings.ErrorInvalidPartnerId(id)), ErrorCategory.InvalidData, null);
			}
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x0019B3D1 File Offset: 0x001995D1
		protected int ReadMservEntry(string address)
		{
			return MServDirectorySession.ReadMservEntry(address);
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x0019B3D9 File Offset: 0x001995D9
		protected int AddMservEntry(string address, int newPartnerId)
		{
			MServDirectorySession.AddMserveEntry(address, newPartnerId);
			return newPartnerId;
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x0019B3E4 File Offset: 0x001995E4
		protected int RemoveMservEntry(string address)
		{
			int num = MServDirectorySession.ReadMservEntry(address);
			if (num != -1)
			{
				MServDirectorySession.RemoveMserveEntry(address, num);
			}
			else
			{
				base.WriteError(new TenantNotFoundException(DirectoryStrings.TenantNotFoundInMservError(address)), ExchangeErrorCategory.Client, null);
			}
			return num;
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x0019B420 File Offset: 0x00199620
		protected int UpdateMservEntry(string address, int newPartnerId)
		{
			int num = MServDirectorySession.ReadMservEntry(address);
			if (num != -1)
			{
				MServDirectorySession.RemoveMserveEntry(address, num);
				MServDirectorySession.AddMserveEntry(address, newPartnerId);
			}
			else
			{
				base.WriteError(new TenantNotFoundException(DirectoryStrings.TenantNotFoundInMservError(address)), ExchangeErrorCategory.Client, null);
			}
			return newPartnerId;
		}

		// Token: 0x040035D6 RID: 13782
		internal const string ExternalDirectoryOrganizationIdParameterName = "ExternalDirectoryOrganizationId";

		// Token: 0x040035D7 RID: 13783
		internal const string ExternalDirectoryOrganizationIdParameterSetName = "ExternalDirectoryOrganizationIdParameterSet";

		// Token: 0x040035D8 RID: 13784
		internal const string DomainNameParameterName = "DomainName";

		// Token: 0x040035D9 RID: 13785
		internal const string DomainNameParameterSetName = "DomainNameParameterSet";

		// Token: 0x040035DA RID: 13786
		internal const string AddressParameterName = "Address";

		// Token: 0x040035DB RID: 13787
		internal const string AddressParameterSetName = "AddressParameterSet";

		// Token: 0x040035DC RID: 13788
		internal const string PartnerIdParameterName = "PartnerId";

		// Token: 0x040035DD RID: 13789
		internal const string MinorPartnerIdParameterName = "MinorPartnerId";
	}
}
