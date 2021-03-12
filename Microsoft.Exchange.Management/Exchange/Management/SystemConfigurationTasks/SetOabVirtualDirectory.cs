using System;
using System.Collections;
using System.DirectoryServices;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C4B RID: 3147
	[Cmdlet("Set", "OabVirtualDirectory", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOabVirtualDirectory : SetExchangeVirtualDirectory<ADOabVirtualDirectory>
	{
		// Token: 0x170024D3 RID: 9427
		// (get) Token: 0x0600774D RID: 30541 RVA: 0x001E64D4 File Offset: 0x001E46D4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOabVirtualDirectory(this.Identity.ToString());
			}
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x001E64E6 File Offset: 0x001E46E6
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(IISNotInstalledException).IsInstanceOfType(exception);
		}

		// Token: 0x170024D4 RID: 9428
		// (get) Token: 0x0600774F RID: 30543 RVA: 0x001E6508 File Offset: 0x001E4708
		// (set) Token: 0x06007750 RID: 30544 RVA: 0x001E6510 File Offset: 0x001E4710
		internal new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
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

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x06007751 RID: 30545 RVA: 0x001E6519 File Offset: 0x001E4719
		// (set) Token: 0x06007752 RID: 30546 RVA: 0x001E6521 File Offset: 0x001E4721
		internal new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
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

		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x06007753 RID: 30547 RVA: 0x001E652A File Offset: 0x001E472A
		// (set) Token: 0x06007754 RID: 30548 RVA: 0x001E6541 File Offset: 0x001E4741
		[Parameter]
		public bool RequireSSL
		{
			get
			{
				return (bool)base.Fields["RequireSSL"];
			}
			set
			{
				base.Fields["RequireSSL"] = value;
			}
		}

		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x06007755 RID: 30549 RVA: 0x001E6559 File Offset: 0x001E4759
		// (set) Token: 0x06007756 RID: 30550 RVA: 0x001E6570 File Offset: 0x001E4770
		[Parameter]
		public bool BasicAuthentication
		{
			get
			{
				return (bool)base.Fields["BasicAuthentication"];
			}
			set
			{
				base.Fields["BasicAuthentication"] = value;
			}
		}

		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x06007757 RID: 30551 RVA: 0x001E6588 File Offset: 0x001E4788
		// (set) Token: 0x06007758 RID: 30552 RVA: 0x001E659F File Offset: 0x001E479F
		[Parameter]
		public bool WindowsAuthentication
		{
			get
			{
				return (bool)base.Fields["WindowsAuthentication"];
			}
			set
			{
				base.Fields["WindowsAuthentication"] = value;
			}
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x06007759 RID: 30553 RVA: 0x001E65B7 File Offset: 0x001E47B7
		// (set) Token: 0x0600775A RID: 30554 RVA: 0x001E65CE File Offset: 0x001E47CE
		[Parameter]
		public bool OAuthAuthentication
		{
			get
			{
				return (bool)base.Fields["OAuthAuthentication"];
			}
			set
			{
				base.Fields["OAuthAuthentication"] = value;
			}
		}

		// Token: 0x0600775B RID: 30555 RVA: 0x001E65E8 File Offset: 0x001E47E8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.BasicAuthentication && !this.DataObject.RequireSSL)
			{
				this.WriteWarning(Strings.OABVdirBasicAuthWithoutSSL(this.DataObject.Identity.ToString()));
			}
			if (!base.HasErrors && this.DataObject.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new ArgumentException(Strings.ErrorOabVirtualDirectoryNameIsImmutable, "Name"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600775C RID: 30556 RVA: 0x001E667C File Offset: 0x001E487C
		protected override IConfigurable PrepareDataObject()
		{
			ADOabVirtualDirectory adoabVirtualDirectory = (ADOabVirtualDirectory)base.PrepareDataObject();
			if (base.HasErrors)
			{
				return null;
			}
			if (base.Fields.Contains("RequireSSL"))
			{
				adoabVirtualDirectory.RequireSSL = (bool)base.Fields["RequireSSL"];
			}
			if (base.Fields.Contains("BasicAuthentication"))
			{
				adoabVirtualDirectory.BasicAuthentication = (bool)base.Fields["BasicAuthentication"];
			}
			if (base.Fields.Contains("WindowsAuthentication"))
			{
				adoabVirtualDirectory.WindowsAuthentication = (bool)base.Fields["WindowsAuthentication"];
			}
			if (base.Fields.Contains("OAuthAuthentication"))
			{
				adoabVirtualDirectory.OAuthAuthentication = (bool)base.Fields["OAuthAuthentication"];
			}
			return adoabVirtualDirectory;
		}

		// Token: 0x0600775D RID: 30557 RVA: 0x001E6754 File Offset: 0x001E4954
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			TaskLogger.LogEnter();
			ADOabVirtualDirectory adoabVirtualDirectory = (ADOabVirtualDirectory)dataObject;
			adoabVirtualDirectory.OAuthAuthentication = adoabVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.OAuth);
			adoabVirtualDirectory.RequireSSL = IisUtility.SSLEnabled(adoabVirtualDirectory.MetabasePath);
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(adoabVirtualDirectory.MetabasePath))
			{
				adoabVirtualDirectory.BasicAuthentication = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic);
				adoabVirtualDirectory.WindowsAuthentication = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Ntlm);
			}
			dataObject.ResetChangeTracking();
			base.StampChangesOn(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x0600775E RID: 30558 RVA: 0x001E67E8 File Offset: 0x001E49E8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject.IsModified(ADOabVirtualDirectorySchema.OAuthAuthentication))
			{
				ExchangeServiceVDirHelper.SetOAuthAuthenticationModule(this.OAuthAuthentication, false, this.DataObject);
			}
			bool flag = this.DataObject.IsModified(ADOabVirtualDirectorySchema.RequireSSL) | this.DataObject.IsModified(ADOabVirtualDirectorySchema.BasicAuthentication) | this.DataObject.IsModified(ADOabVirtualDirectorySchema.WindowsAuthentication);
			base.InternalProcessRecord();
			if (flag)
			{
				try
				{
					SetOabVirtualDirectory.UpdateMetabase(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				finally
				{
					if (base.HasErrors)
					{
						base.WriteError(new LocalizedException(Strings.ErrorADOperationSucceededButMetabaseOperationFailed), ErrorCategory.WriteError, this.DataObject.Identity);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600775F RID: 30559 RVA: 0x001E68B0 File Offset: 0x001E4AB0
		internal static void UpdateMetabase(ADOabVirtualDirectory virtualDirectory, Task.TaskErrorLoggingDelegate handler)
		{
			try
			{
				DirectoryEntry directoryEntry2;
				DirectoryEntry directoryEntry = directoryEntry2 = IisUtility.CreateIISDirectoryEntry(virtualDirectory.MetabasePath);
				try
				{
					ArrayList arrayList = new ArrayList();
					int num = (int)(IisUtility.GetIisPropertyValue("AccessSSLFlags", IisUtility.GetProperties(directoryEntry)) ?? 0);
					if (virtualDirectory.RequireSSL)
					{
						num |= 8;
					}
					else
					{
						num &= -9;
						num &= -257;
						num &= -65;
					}
					arrayList.Add(new MetabaseProperty("AccessSSLFlags", num, true));
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic, virtualDirectory.BasicAuthentication);
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.WindowsIntegrated, virtualDirectory.WindowsAuthentication);
					IisUtility.SetProperties(directoryEntry, arrayList);
					directoryEntry.CommitChanges();
					IisUtility.CommitMetabaseChanges((virtualDirectory.Server == null) ? null : virtualDirectory.Server.ToString());
				}
				finally
				{
					if (directoryEntry2 != null)
					{
						((IDisposable)directoryEntry2).Dispose();
					}
				}
			}
			catch (COMException exception)
			{
				handler(exception, ErrorCategory.InvalidOperation, virtualDirectory.Identity);
			}
		}
	}
}
