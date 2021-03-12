using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B1 RID: 2481
	[Cmdlet("Get", "ExchangeServerAccessLicenseUser", DefaultParameterSetName = "LicenseName")]
	public sealed class GetExchangeServerAccessLicenseUser : Task
	{
		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x0600588C RID: 22668 RVA: 0x0017193A File Offset: 0x0016FB3A
		// (set) Token: 0x0600588D RID: 22669 RVA: 0x00171951 File Offset: 0x0016FB51
		[Parameter(Mandatory = true, ParameterSetName = "LicenseName")]
		[ValidateNotNullOrEmpty]
		public string LicenseName
		{
			get
			{
				return (string)base.Fields["LicenseName"];
			}
			set
			{
				base.Fields["LicenseName"] = value;
			}
		}

		// Token: 0x0600588E RID: 22670 RVA: 0x00171964 File Offset: 0x0016FB64
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!ExchangeServerAccessLicense.TryParse(this.LicenseName, out this.license))
			{
				base.WriteError(new ArgumentException(Strings.InvalidLicenseName(this.LicenseName ?? "null")), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600588F RID: 22671 RVA: 0x001719BA File Offset: 0x0016FBBA
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.license.UnitLabel == ExchangeServerAccessLicense.UnitLabelType.Server)
			{
				this.InternalProcessServer();
			}
			else
			{
				if (this.license.UnitLabel != ExchangeServerAccessLicense.UnitLabelType.CAL)
				{
					throw new NotImplementedException();
				}
				this.InternalProcessCAL();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06005890 RID: 22672 RVA: 0x001719F8 File Offset: 0x0016FBF8
		private void InternalProcessServer()
		{
			this.InternalWriteResult(base.InvokeCommand.InvokeScript(string.Format("Get-ExchangeServer | Where {{$_.AdminDisplayVersion.Major -eq {0}}} | Where {{$_.Edition -eq '{1}'}} | Select {2}", (int)this.license.VersionMajor, this.license.AccessLicense, ExchangeServerSchema.Fqdn.Name)), ExchangeServerSchema.Fqdn);
		}

		// Token: 0x06005891 RID: 22673 RVA: 0x00171A50 File Offset: 0x0016FC50
		private void InternalProcessCAL()
		{
			this.InternalWriteResult(base.InvokeCommand.InvokeScript(string.Format("CalCalculation.ps1 {0} {1}", (int)this.license.VersionMajor, this.license.AccessLicense)), MailEnabledRecipientSchema.PrimarySmtpAddress);
		}

		// Token: 0x06005892 RID: 22674 RVA: 0x00171AA0 File Offset: 0x0016FCA0
		private void InternalWriteResult(Collection<PSObject> psObjects, ADPropertyDefinition propertyDef)
		{
			if (psObjects == null)
			{
				throw new ArgumentNullException(Strings.ErrorNullParameter("psObjects"));
			}
			if (propertyDef == null)
			{
				throw new ArgumentNullException(Strings.ErrorNullParameter("propertyDef"));
			}
			foreach (PSObject psobject in psObjects)
			{
				foreach (PSPropertyInfo pspropertyInfo in psobject.Properties)
				{
					if (pspropertyInfo == null)
					{
						throw new ArgumentNullException(Strings.ErrorNullParameter("property"));
					}
					if (pspropertyInfo.Value == null)
					{
						throw new ArgumentNullException(Strings.ErrorNullParameter("property value"));
					}
					base.WriteObject(new ExchangeServerAccessLicenseUser(this.license.LicenseName, pspropertyInfo.Value.ToString()));
				}
			}
		}

		// Token: 0x040032D5 RID: 13013
		private ExchangeServerAccessLicense license;
	}
}
