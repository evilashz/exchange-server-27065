using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C4D RID: 3149
	public abstract class SetPowerShellCommonVirtualDirectory<T> : SetExchangeServiceVirtualDirectory<T> where T : ADPowerShellCommonVirtualDirectory, new()
	{
		// Token: 0x170024E0 RID: 9440
		// (get) Token: 0x06007774 RID: 30580 RVA: 0x001E7148 File Offset: 0x001E5348
		// (set) Token: 0x06007775 RID: 30581 RVA: 0x001E7173 File Offset: 0x001E5373
		[Parameter(Mandatory = false)]
		public bool CertificateAuthentication
		{
			get
			{
				return base.Fields["CertificateAuthentication"] != null && (bool)base.Fields["CertificateAuthentication"];
			}
			set
			{
				base.Fields["CertificateAuthentication"] = value;
			}
		}

		// Token: 0x170024E1 RID: 9441
		// (get) Token: 0x06007776 RID: 30582 RVA: 0x001E718B File Offset: 0x001E538B
		// (set) Token: 0x06007777 RID: 30583 RVA: 0x001E71B6 File Offset: 0x001E53B6
		[Parameter(Mandatory = false)]
		public bool EnableCertificateHeaderAuthModule
		{
			get
			{
				return base.Fields["EnableCertificateHeaderAuthModule"] != null && (bool)base.Fields["EnableCertificateHeaderAuthModule"];
			}
			set
			{
				base.Fields["EnableCertificateHeaderAuthModule"] = value;
			}
		}

		// Token: 0x170024E2 RID: 9442
		// (get) Token: 0x06007778 RID: 30584 RVA: 0x001E71CE File Offset: 0x001E53CE
		// (set) Token: 0x06007779 RID: 30585 RVA: 0x001E71D1 File Offset: 0x001E53D1
		protected new ExtendedProtectionTokenCheckingMode ExtendedProtectionTokenChecking
		{
			get
			{
				return ExtendedProtectionTokenCheckingMode.None;
			}
			set
			{
			}
		}

		// Token: 0x170024E3 RID: 9443
		// (get) Token: 0x0600777A RID: 30586 RVA: 0x001E71D3 File Offset: 0x001E53D3
		// (set) Token: 0x0600777B RID: 30587 RVA: 0x001E71D6 File Offset: 0x001E53D6
		protected new MultiValuedProperty<ExtendedProtectionFlag> ExtendedProtectionFlags
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170024E4 RID: 9444
		// (get) Token: 0x0600777C RID: 30588 RVA: 0x001E71D8 File Offset: 0x001E53D8
		// (set) Token: 0x0600777D RID: 30589 RVA: 0x001E71DB File Offset: 0x001E53DB
		protected new MultiValuedProperty<string> ExtendedProtectionSPNList
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170024E5 RID: 9445
		// (get) Token: 0x0600777E RID: 30590
		protected abstract PowerShellVirtualDirectoryType AllowedVirtualDirectoryType { get; }

		// Token: 0x0600777F RID: 30591 RVA: 0x001E71E0 File Offset: 0x001E53E0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			T dataObject = this.DataObject;
			if (dataObject.VirtualDirectoryType != this.AllowedVirtualDirectoryType)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(T).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
			}
		}

		// Token: 0x06007780 RID: 30592 RVA: 0x001E7264 File Offset: 0x001E5464
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			base.InternalEnableLiveIdNegotiateAuxiliaryModule();
			if (base.Fields["CertificateAuthentication"] != null)
			{
				T dataObject = this.DataObject;
				dataObject.CertificateAuthentication = new bool?((bool)base.Fields["CertificateAuthentication"]);
				ADExchangeServiceVirtualDirectory virtualDirectory = this.DataObject;
				Task.TaskErrorLoggingDelegate errorHandler = new Task.TaskErrorLoggingDelegate(base.WriteError);
				T dataObject2 = this.DataObject;
				ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(virtualDirectory, errorHandler, Strings.ErrorUpdatingVDir(dataObject2.MetabasePath, string.Empty));
				T dataObject3 = this.DataObject;
				ExchangeServiceVDirHelper.ConfigureAnonymousAuthentication(dataObject3.MetabasePath, false);
			}
			if (base.Fields["EnableCertificateHeaderAuthModule"] != null)
			{
				base.SetCertificateHeaderAuthenticationModule(this.EnableCertificateHeaderAuthModule, false);
			}
		}
	}
}
