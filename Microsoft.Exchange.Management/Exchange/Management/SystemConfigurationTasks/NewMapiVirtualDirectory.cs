using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C24 RID: 3108
	[Cmdlet("New", "MapiVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewMapiVirtualDirectory : NewExchangeVirtualDirectory<ADMapiVirtualDirectory>
	{
		// Token: 0x0600752D RID: 29997 RVA: 0x001DEBC9 File Offset: 0x001DCDC9
		public NewMapiVirtualDirectory()
		{
			this.Name = "mapi";
			this.WebSiteName = null;
		}

		// Token: 0x170023FC RID: 9212
		// (get) Token: 0x0600752E RID: 29998 RVA: 0x001DEBE3 File Offset: 0x001DCDE3
		// (set) Token: 0x0600752F RID: 29999 RVA: 0x001DEBEB File Offset: 0x001DCDEB
		private new string WebSiteName
		{
			get
			{
				return base.WebSiteName;
			}
			set
			{
				base.WebSiteName = value;
			}
		}

		// Token: 0x170023FD RID: 9213
		// (get) Token: 0x06007530 RID: 30000 RVA: 0x001DEBF4 File Offset: 0x001DCDF4
		// (set) Token: 0x06007531 RID: 30001 RVA: 0x001DEBFC File Offset: 0x001DCDFC
		private new string Path
		{
			get
			{
				return base.Path;
			}
			set
			{
				base.Path = value;
			}
		}

		// Token: 0x170023FE RID: 9214
		// (get) Token: 0x06007532 RID: 30002 RVA: 0x001DEC05 File Offset: 0x001DCE05
		// (set) Token: 0x06007533 RID: 30003 RVA: 0x001DEC0D File Offset: 0x001DCE0D
		private new string AppPoolId
		{
			get
			{
				return base.AppPoolId;
			}
			set
			{
				base.AppPoolId = value;
			}
		}

		// Token: 0x170023FF RID: 9215
		// (get) Token: 0x06007534 RID: 30004 RVA: 0x001DEC16 File Offset: 0x001DCE16
		// (set) Token: 0x06007535 RID: 30005 RVA: 0x001DEC1E File Offset: 0x001DCE1E
		private new string ApplicationRoot
		{
			get
			{
				return base.ApplicationRoot;
			}
			set
			{
				base.ApplicationRoot = value;
			}
		}

		// Token: 0x17002400 RID: 9216
		// (get) Token: 0x06007536 RID: 30006 RVA: 0x001DEC27 File Offset: 0x001DCE27
		// (set) Token: 0x06007537 RID: 30007 RVA: 0x001DEC2F File Offset: 0x001DCE2F
		private new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17002401 RID: 9217
		// (get) Token: 0x06007538 RID: 30008 RVA: 0x001DEC38 File Offset: 0x001DCE38
		// (set) Token: 0x06007539 RID: 30009 RVA: 0x001DEC40 File Offset: 0x001DCE40
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

		// Token: 0x17002402 RID: 9218
		// (get) Token: 0x0600753A RID: 30010 RVA: 0x001DEC49 File Offset: 0x001DCE49
		// (set) Token: 0x0600753B RID: 30011 RVA: 0x001DEC51 File Offset: 0x001DCE51
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

		// Token: 0x17002403 RID: 9219
		// (get) Token: 0x0600753C RID: 30012 RVA: 0x001DEC5A File Offset: 0x001DCE5A
		// (set) Token: 0x0600753D RID: 30013 RVA: 0x001DEC62 File Offset: 0x001DCE62
		[Parameter]
		public MultiValuedProperty<AuthenticationMethod> IISAuthenticationMethods
		{
			get
			{
				return this.iisAuthenticationMethods;
			}
			set
			{
				this.iisAuthenticationMethods = value;
			}
		}

		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x0600753E RID: 30014 RVA: 0x001DEC6B File Offset: 0x001DCE6B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewMapiVirtualDirectory;
			}
		}

		// Token: 0x0600753F RID: 30015 RVA: 0x001DEC72 File Offset: 0x001DCE72
		protected override bool FailOnVirtualDirectoryAlreadyExists()
		{
			return false;
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x001DEC75 File Offset: 0x001DCE75
		protected override bool InternalShouldCreateMetabaseObject()
		{
			return false;
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x001DEC78 File Offset: 0x001DCE78
		private void ValidateParameterValues()
		{
			bool flag = base.ExternalUrl != null && !string.IsNullOrEmpty(base.ExternalUrl.ToString());
			bool flag2 = base.InternalUrl != null && !string.IsNullOrEmpty(base.InternalUrl.ToString());
			if (!flag && !flag2)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMapiVirtualDirectoryMustSpecifyEitherInternalOrExternalUrl), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			if (flag)
			{
				base.ExternalUrl = new Uri(string.Format("https://{0}/mapi", base.ExternalUrl.Host));
			}
			if (flag2)
			{
				base.InternalUrl = new Uri(string.Format("https://{0}/mapi", base.InternalUrl.Host));
			}
			if (this.IISAuthenticationMethods == null)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMapiVirtualDirectoryMustSpecifyIISAuthenticationMethods), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			this.DataObject.IISAuthenticationMethods = this.iisAuthenticationMethods;
		}

		// Token: 0x06007542 RID: 30018 RVA: 0x001DED78 File Offset: 0x001DCF78
		private bool IsInstalled()
		{
			ADMapiVirtualDirectory[] array = (base.DataSession as IConfigurationSession).Find<ADMapiVirtualDirectory>((ADObjectId)base.OwningServer.Identity, QueryScope.SubTree, null, null, 1);
			return array.Length > 0;
		}

		// Token: 0x06007543 RID: 30019 RVA: 0x001DEDB0 File Offset: 0x001DCFB0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.ValidateParameterValues();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			try
			{
				if (this.IsInstalled())
				{
					base.WriteError(new ArgumentException(Strings.ErrorMapiHttpAlreadyEnabled(base.OwningServer.Fqdn), string.Empty), ErrorCategory.InvalidArgument, this.DataObject.Identity);
					return;
				}
			}
			catch (ADTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, this.DataObject.Identity);
			}
			catch (DataValidationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidData, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06007544 RID: 30020 RVA: 0x001DEE64 File Offset: 0x001DD064
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			IISConfigurationUtilities.TryRunIISConfigurationOperation(new IISConfigurationUtilities.IISConfigurationOperation(this.RunIisConfigurationOperation), 5, 2, this);
			TaskLogger.LogExit();
		}

		// Token: 0x06007545 RID: 30021 RVA: 0x001DEE8A File Offset: 0x001DD08A
		private void RunIisConfigurationOperation()
		{
			IISConfigurationUtilities.CreateAndConfigureLocalMapiHttpFrontEnd(this.IISAuthenticationMethods);
		}

		// Token: 0x04003B78 RID: 15224
		private const string VirtualDirectoryName = "mapi";

		// Token: 0x04003B79 RID: 15225
		private MultiValuedProperty<AuthenticationMethod> iisAuthenticationMethods;
	}
}
