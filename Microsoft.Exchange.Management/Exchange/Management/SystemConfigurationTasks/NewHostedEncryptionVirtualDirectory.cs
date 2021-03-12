using System;
using System.Collections;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C23 RID: 3107
	[Cmdlet("New", "HostedEncryptionVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewHostedEncryptionVirtualDirectory : NewWebAppVirtualDirectory<ADE4eVirtualDirectory>
	{
		// Token: 0x06007520 RID: 29984 RVA: 0x001DE8DC File Offset: 0x001DCADC
		public NewHostedEncryptionVirtualDirectory()
		{
			this.Name = "Encryption";
			base.AppPoolId = "MSExchangeEncryptionAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
		}

		// Token: 0x170023F7 RID: 9207
		// (get) Token: 0x06007521 RID: 29985 RVA: 0x001DE901 File Offset: 0x001DCB01
		// (set) Token: 0x06007522 RID: 29986 RVA: 0x001DE909 File Offset: 0x001DCB09
		public new string Name
		{
			get
			{
				return base.Name;
			}
			private set
			{
				base.Name = value;
			}
		}

		// Token: 0x170023F8 RID: 9208
		// (get) Token: 0x06007523 RID: 29987 RVA: 0x001DE912 File Offset: 0x001DCB12
		// (set) Token: 0x06007524 RID: 29988 RVA: 0x001DE91A File Offset: 0x001DCB1A
		public new string ApplicationRoot
		{
			get
			{
				return base.ApplicationRoot;
			}
			private set
			{
				base.ApplicationRoot = value;
			}
		}

		// Token: 0x170023F9 RID: 9209
		// (get) Token: 0x06007525 RID: 29989 RVA: 0x001DE923 File Offset: 0x001DCB23
		// (set) Token: 0x06007526 RID: 29990 RVA: 0x001DE92B File Offset: 0x001DCB2B
		internal new MultiValuedProperty<AuthenticationMethod> ExternalAuthenticationMethods
		{
			get
			{
				return base.ExternalAuthenticationMethods;
			}
			private set
			{
				base.ExternalAuthenticationMethods = value;
			}
		}

		// Token: 0x170023FA RID: 9210
		// (get) Token: 0x06007527 RID: 29991 RVA: 0x001DE934 File Offset: 0x001DCB34
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewHostedEncryptionVirtualDirectory(base.WebSiteName, base.Server.ToString());
			}
		}

		// Token: 0x170023FB RID: 9211
		// (get) Token: 0x06007528 RID: 29992 RVA: 0x001DE94C File Offset: 0x001DCB4C
		protected override ArrayList CustomizedVDirProperties
		{
			get
			{
				return new ArrayList
				{
					new MetabaseProperty("DefaultDoc", "default.aspx"),
					new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script),
					new MetabaseProperty("CacheControlCustom", "public"),
					new MetabaseProperty("HttpExpires", "D, 0x278d00"),
					new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Basic),
					new MetabaseProperty("AppFriendlyName", this.Name),
					new MetabaseProperty("AppRoot", base.AppRootValue),
					new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled),
					new MetabaseProperty("AccessSSLFlags", MetabasePropertyTypes.AccessSSLFlags.AccessSSL | MetabasePropertyTypes.AccessSSLFlags.AccessSSL128)
				};
			}
		}

		// Token: 0x06007529 RID: 29993 RVA: 0x001DEA34 File Offset: 0x001DCC34
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			if (!base.Fields.IsModified("Path"))
			{
				base.Path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, (base.Role == VirtualDirectoryRole.ClientAccess) ? "FrontEnd\\HttpProxy\\e4e" : "ClientAccess\\e4e");
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!new VirtualDirectoryPathExistsCondition(base.OwningServer.Fqdn, base.Path).Verify())
			{
				base.WriteError(new ArgumentException(Strings.ErrorPathNotExistsOnServer(base.Path, base.OwningServer.Name), "Path"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600752A RID: 29994 RVA: 0x001DEAFA File Offset: 0x001DCCFA
		protected override bool InternalProcessStartWork()
		{
			this.SetDefaults();
			return true;
		}

		// Token: 0x0600752B RID: 29995 RVA: 0x001DEB04 File Offset: 0x001DCD04
		protected override void InternalProcessMetabase()
		{
			ADOwaVirtualDirectory adowaVirtualDirectory = WebAppVirtualDirectoryHelper.FindWebAppVirtualDirectoryInSameWebSite<ADOwaVirtualDirectory>(this.DataObject, base.DataSession);
			if (adowaVirtualDirectory != null && !string.IsNullOrEmpty(adowaVirtualDirectory.DefaultDomain))
			{
				this.DataObject.DefaultDomain = adowaVirtualDirectory.DefaultDomain;
			}
			WebAppVirtualDirectoryHelper.UpdateMetabase(this.DataObject, this.DataObject.MetabasePath, true);
		}

		// Token: 0x0600752C RID: 29996 RVA: 0x001DEB5C File Offset: 0x001DCD5C
		private void SetDefaults()
		{
			this.DataObject.GzipLevel = GzipLevel.High;
			this.DataObject.FormsAuthentication = (base.Role == VirtualDirectoryRole.ClientAccess);
			this.DataObject.BasicAuthentication = (base.Role == VirtualDirectoryRole.ClientAccess);
			this.DataObject.WindowsAuthentication = (base.Role == VirtualDirectoryRole.Mailbox);
			this.DataObject.DigestAuthentication = false;
			this.DataObject.LiveIdAuthentication = false;
		}

		// Token: 0x04003B75 RID: 15221
		private const string LocalPath = "ClientAccess\\e4e";

		// Token: 0x04003B76 RID: 15222
		private const string CafePath = "FrontEnd\\HttpProxy\\e4e";

		// Token: 0x04003B77 RID: 15223
		private const string DefaultApplicationPool = "MSExchangeEncryptionAppPool";
	}
}
