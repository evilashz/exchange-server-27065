using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C48 RID: 3144
	[Cmdlet("Set", "MapiVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMapiVirtualDirectory : SetExchangeVirtualDirectory<ADMapiVirtualDirectory>
	{
		// Token: 0x170024BD RID: 9405
		// (get) Token: 0x06007718 RID: 30488 RVA: 0x001E58C0 File Offset: 0x001E3AC0
		// (set) Token: 0x06007719 RID: 30489 RVA: 0x001E58C8 File Offset: 0x001E3AC8
		private new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return base.InternalAuthenticationMethods;
			}
			set
			{
				base.InternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170024BE RID: 9406
		// (get) Token: 0x0600771A RID: 30490 RVA: 0x001E58D1 File Offset: 0x001E3AD1
		// (set) Token: 0x0600771B RID: 30491 RVA: 0x001E58D9 File Offset: 0x001E3AD9
		private new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return base.ExternalAuthenticationMethods;
			}
			set
			{
				base.ExternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170024BF RID: 9407
		// (get) Token: 0x0600771C RID: 30492 RVA: 0x001E58E2 File Offset: 0x001E3AE2
		// (set) Token: 0x0600771D RID: 30493 RVA: 0x001E58F9 File Offset: 0x001E3AF9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return (MultiValuedProperty<AuthenticationMethod>)base.Fields["IISAuthenticationMethods"];
			}
			set
			{
				base.Fields["IISAuthenticationMethods"] = value;
			}
		}

		// Token: 0x170024C0 RID: 9408
		// (get) Token: 0x0600771E RID: 30494 RVA: 0x001E590C File Offset: 0x001E3B0C
		// (set) Token: 0x0600771F RID: 30495 RVA: 0x001E5914 File Offset: 0x001E3B14
		[Parameter(Mandatory = false)]
		public bool ApplyDefaults
		{
			get
			{
				return this.applyDefaults;
			}
			set
			{
				this.applyDefaults = value;
			}
		}

		// Token: 0x170024C1 RID: 9409
		// (get) Token: 0x06007720 RID: 30496 RVA: 0x001E591D File Offset: 0x001E3B1D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetMapiVirtualDirectory;
			}
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x001E5924 File Offset: 0x001E3B24
		protected override IConfigurable PrepareDataObject()
		{
			ADMapiVirtualDirectory admapiVirtualDirectory = (ADMapiVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			bool flag = admapiVirtualDirectory.ExternalUrl != null && !string.IsNullOrEmpty(admapiVirtualDirectory.ExternalUrl.ToString());
			bool flag2 = admapiVirtualDirectory.InternalUrl != null && !string.IsNullOrEmpty(admapiVirtualDirectory.InternalUrl.ToString());
			if (!flag && !flag2)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMapiVirtualDirectoryMustSpecifyEitherInternalOrExternalUrl), ErrorCategory.InvalidArgument, admapiVirtualDirectory.Identity);
				return null;
			}
			if (flag)
			{
				admapiVirtualDirectory.ExternalUrl = new Uri(string.Format("https://{0}/mapi", admapiVirtualDirectory.ExternalUrl.Host));
			}
			if (flag2)
			{
				admapiVirtualDirectory.InternalUrl = new Uri(string.Format("https://{0}/mapi", admapiVirtualDirectory.InternalUrl.Host));
			}
			if (this.IISAuthenticationMethods == null)
			{
				this.IISAuthenticationMethods = admapiVirtualDirectory.IISAuthenticationMethods;
			}
			else
			{
				admapiVirtualDirectory.IISAuthenticationMethods = this.IISAuthenticationMethods;
			}
			if (admapiVirtualDirectory.IISAuthenticationMethods == null)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMapiVirtualDirectoryMustSpecifyIISAuthenticationMethods), ErrorCategory.InvalidArgument, admapiVirtualDirectory.Identity);
				return null;
			}
			if (this.applyDefaults && !SetMapiVirtualDirectory.IsLocalServer(admapiVirtualDirectory))
			{
				base.WriteError(new ArgumentException(Strings.ErrorMapiVirtualDirectoryMustBeLocalServerToReset), ErrorCategory.InvalidArgument, admapiVirtualDirectory.Identity);
				return null;
			}
			this.metabasePath = admapiVirtualDirectory.MetabasePath;
			return admapiVirtualDirectory;
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x001E5A7E File Offset: 0x001E3C7E
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			IISConfigurationUtilities.TryRunIISConfigurationOperation(new IISConfigurationUtilities.IISConfigurationOperation(this.RunIisConfigurationOperation), 5, 2, this);
			TaskLogger.LogExit();
		}

		// Token: 0x06007723 RID: 30499 RVA: 0x001E5AA4 File Offset: 0x001E3CA4
		private static string ServerShortName(string serverFqdn)
		{
			int num = serverFqdn.IndexOf('.');
			if (num != -1)
			{
				return serverFqdn.Substring(0, num);
			}
			return serverFqdn;
		}

		// Token: 0x06007724 RID: 30500 RVA: 0x001E5AC8 File Offset: 0x001E3CC8
		private static bool IsLocalServer(ADMapiVirtualDirectory dataObject)
		{
			string hostName = IisUtility.GetHostName(dataObject.MetabasePath);
			if (string.IsNullOrEmpty(hostName))
			{
				return false;
			}
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			if (hostName.IndexOf('.') != -1)
			{
				return hostName.Equals(localComputerFqdn, StringComparison.InvariantCultureIgnoreCase);
			}
			string value = SetMapiVirtualDirectory.ServerShortName(localComputerFqdn);
			string text = SetMapiVirtualDirectory.ServerShortName(hostName);
			return text.Equals(value, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x001E5B1C File Offset: 0x001E3D1C
		private void RunIisConfigurationOperation()
		{
			if (this.applyDefaults)
			{
				IISConfigurationUtilities.CreateAndConfigureLocalMapiHttpFrontEnd(this.IISAuthenticationMethods);
				return;
			}
			IISConfigurationUtilities.UpdateRemoteMapiHttpFrontEnd(IisUtility.GetHostName(this.metabasePath), this.IISAuthenticationMethods);
		}

		// Token: 0x04003BC9 RID: 15305
		private const string IISAuthenticationMethodsKey = "IISAuthenticationMethods";

		// Token: 0x04003BCA RID: 15306
		private string metabasePath;

		// Token: 0x04003BCB RID: 15307
		private bool applyDefaults;
	}
}
