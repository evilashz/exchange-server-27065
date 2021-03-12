using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.RedirectionModule;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C43 RID: 3139
	public abstract class SetExchangeServiceVirtualDirectory<T> : SetExchangeVirtualDirectory<T> where T : ADExchangeServiceVirtualDirectory, new()
	{
		// Token: 0x0600769D RID: 30365 RVA: 0x001E40FC File Offset: 0x001E22FC
		internal static bool CheckAuthModule(ADExchangeServiceVirtualDirectory advdir, bool isChildVDirApplication, string moduleName)
		{
			DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(advdir.MetabasePath);
			bool result;
			using (ServerManager serverManager = ServerManager.OpenRemote(IisUtility.GetHostName(advdir.MetabasePath)))
			{
				Configuration webConfiguration;
				if (isChildVDirApplication)
				{
					webConfiguration = serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Parent.Path)].Applications[string.Format("/{0}/{1}", directoryEntry.Parent.Name, directoryEntry.Name)].GetWebConfiguration();
				}
				else
				{
					webConfiguration = serverManager.Sites[IisUtility.GetWebSiteName(directoryEntry.Parent.Path)].Applications["/" + directoryEntry.Name].GetWebConfiguration();
				}
				ConfigurationElementCollection collection = webConfiguration.GetSection("system.webServer/modules").GetCollection();
				foreach (ConfigurationElement configurationElement in collection)
				{
					if (string.Equals(configurationElement.Attributes["name"].Value.ToString(), moduleName, StringComparison.Ordinal))
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0600769E RID: 30366 RVA: 0x001E4244 File Offset: 0x001E2444
		protected bool CheckLiveIdBasicAuthModule(bool isChildVDirApplication)
		{
			return SetExchangeServiceVirtualDirectory<T>.CheckAuthModule(this.DataObject, isChildVDirApplication, "LiveIdBasicAuthenticationModule");
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x001E425C File Offset: 0x001E245C
		protected void SetLiveIdNegotiateAuxiliaryModule(bool EnableModule, bool isChildVDirApplication)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "LiveIdNegotiateAuxiliaryModule", typeof(LiveIdNegotiateAuxiliaryModule).FullName, this.DataObject);
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x001E4284 File Offset: 0x001E2484
		protected void SetDelegatedAuthenticationModule(bool EnableModule, bool isChildVDirApplication)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "DelegatedAuthModule", "Microsoft.Exchange.Configuration.DelegatedAuthentication.DelegatedAuthenticationModule", this.DataObject);
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x001E42A2 File Offset: 0x001E24A2
		protected void SetPowerShellRequestFilterModule(bool EnableModule, bool isChildVDirApplication)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "PowerShellRequestFilterModule", "Microsoft.Exchange.Configuration.DelegatedAuthentication.PowerShellRequestFilterModule", this.DataObject);
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x001E42C0 File Offset: 0x001E24C0
		protected void SetCertificateHeaderAuthenticationModule(bool EnableModule, bool isChildVDirApplication)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "CertificateHeaderAuthModule", "Microsoft.Exchange.Configuration.CertificateAuthentication.CertificateHeaderAuthModule", this.DataObject);
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x001E42DE File Offset: 0x001E24DE
		protected void SetSessionKeyRedirectionModule(bool EnableModule, bool isChildVDirApplication)
		{
			ExchangeServiceVDirHelper.SetAuthModule(EnableModule, isChildVDirApplication, "SessionKeyRedirectionModule", typeof(SessionKeyRedirectionModule).FullName, this.DataObject);
		}

		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x060076A4 RID: 30372 RVA: 0x001E4306 File Offset: 0x001E2506
		protected virtual LocalizedString MetabaseSetPropertiesFailureMessage
		{
			get
			{
				return Strings.MetabaseSetPropertiesFailure;
			}
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x001E4310 File Offset: 0x001E2510
		protected void InternalValidateBasicLiveIdBasic()
		{
			T dataObject = this.DataObject;
			string metabasePath = dataObject.MetabasePath;
			Task.TaskErrorLoggingReThrowDelegate writeError = new Task.TaskErrorLoggingReThrowDelegate(this.WriteError);
			T dataObject2 = this.DataObject;
			using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(metabasePath, writeError, dataObject2.Identity))
			{
				T dataObject3 = this.DataObject;
				bool? basicAuthentication = dataObject3.BasicAuthentication;
				T dataObject4 = this.DataObject;
				bool? liveIdBasicAuthentication = dataObject4.LiveIdBasicAuthentication;
				bool flag = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic);
				bool flag2 = this.CheckLiveIdBasicAuthModule(false);
				bool flag3 = basicAuthentication ?? flag;
				bool flag4 = liveIdBasicAuthentication ?? flag2;
				if (flag3 && flag4)
				{
					string format = "Enabling both Basic and LiveIdBasic Authentication is not allowed. Virtual directory '{0}' has Basic={1}, LiveIdBasic={2}";
					object[] array = new object[3];
					object[] array2 = array;
					int num = 0;
					T dataObject5 = this.DataObject;
					array2[num] = dataObject5.MetabasePath;
					array[1] = flag.ToString();
					array[2] = flag2.ToString();
					TaskLogger.Trace(format, array);
					T dataObject6 = this.DataObject;
					Exception exception = new LocalizedException(Strings.ErrorBasicAndLiveIdBasicNotAllowedVDir(dataObject6.MetabasePath, flag.ToString(), flag2.ToString()));
					ErrorCategory category = ErrorCategory.InvalidOperation;
					T dataObject7 = this.DataObject;
					base.WriteError(exception, category, dataObject7.Identity);
				}
			}
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x001E448C File Offset: 0x001E268C
		protected void InternalEnableLiveIdNegotiateAuxiliaryModule()
		{
			T dataObject = this.DataObject;
			if (dataObject.LiveIdNegotiateAuthentication != null)
			{
				bool liveIdNegotiateAuthentication = this.LiveIdNegotiateAuthentication;
				this.SetLiveIdNegotiateAuxiliaryModule(liveIdNegotiateAuthentication, false);
			}
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x001E44C8 File Offset: 0x001E26C8
		protected override IConfigurable PrepareDataObject()
		{
			ADExchangeServiceVirtualDirectory adexchangeServiceVirtualDirectory = (ADExchangeServiceVirtualDirectory)base.PrepareDataObject();
			adexchangeServiceVirtualDirectory.BasicAuthentication = (bool?)base.Fields["BasicAuthentication"];
			adexchangeServiceVirtualDirectory.DigestAuthentication = (bool?)base.Fields["DigestAuthentication"];
			adexchangeServiceVirtualDirectory.WindowsAuthentication = (bool?)base.Fields["WindowsAuthentication"];
			adexchangeServiceVirtualDirectory.LiveIdBasicAuthentication = (bool?)base.Fields["LiveIdBasicAuthentication"];
			adexchangeServiceVirtualDirectory.LiveIdNegotiateAuthentication = (bool?)base.Fields["LiveIdNegotiateAuthentication"];
			adexchangeServiceVirtualDirectory.OAuthAuthentication = (bool?)base.Fields["OAuthAuthentication"];
			return adexchangeServiceVirtualDirectory;
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x001E4584 File Offset: 0x001E2784
		protected override void InternalProcessRecord()
		{
			T dataObject = this.DataObject;
			byte major = dataObject.ExchangeVersion.ExchangeBuild.Major;
			T dataObject2 = this.DataObject;
			if (major != dataObject2.MaximumSupportedExchangeObjectVersion.ExchangeBuild.Major)
			{
				T dataObject3 = this.DataObject;
				base.WriteError(new CannotModifyCrossVersionObjectException(dataObject3.Id.DistinguishedName), ErrorCategory.InvalidOperation, null);
				return;
			}
			base.InternalProcessRecord();
			T dataObject4 = this.DataObject;
			base.WriteVerbose(Strings.VerboseApplyingAuthenticationSettingForVDir(dataObject4.MetabasePath));
			ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), this.MetabaseSetPropertiesFailureMessage);
			T dataObject5 = this.DataObject;
			if (dataObject5.LiveIdBasicAuthentication != null)
			{
				ExchangeServiceVDirHelper.SetLiveIdBasicAuthModule(this.LiveIdBasicAuthentication, false, this.DataObject);
			}
			T dataObject6 = this.DataObject;
			if (dataObject6.OAuthAuthentication != null)
			{
				T dataObject7 = this.DataObject;
				ExchangeServiceVDirHelper.SetOAuthAuthenticationModule(dataObject7.OAuthAuthentication.Value, false, this.DataObject);
			}
			ExchangeServiceVDirHelper.CheckAndUpdateWindowsAuthProvidersIfNecessary(this.DataObject, (bool?)base.Fields["WindowsAuthentication"]);
		}

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x060076A9 RID: 30377 RVA: 0x001E46E8 File Offset: 0x001E28E8
		// (set) Token: 0x060076AA RID: 30378 RVA: 0x001E4713 File Offset: 0x001E2913
		[Parameter(Mandatory = false)]
		public bool LiveIdBasicAuthentication
		{
			get
			{
				return base.Fields["LiveIdBasicAuthentication"] != null && (bool)base.Fields["LiveIdBasicAuthentication"];
			}
			set
			{
				base.Fields["LiveIdBasicAuthentication"] = value;
			}
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x060076AB RID: 30379 RVA: 0x001E472B File Offset: 0x001E292B
		// (set) Token: 0x060076AC RID: 30380 RVA: 0x001E4756 File Offset: 0x001E2956
		[Parameter(Mandatory = false)]
		public bool LiveIdNegotiateAuthentication
		{
			get
			{
				return base.Fields["LiveIdNegotiateAuthentication"] != null && (bool)base.Fields["LiveIdNegotiateAuthentication"];
			}
			set
			{
				base.Fields["LiveIdNegotiateAuthentication"] = value;
			}
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x060076AD RID: 30381 RVA: 0x001E476E File Offset: 0x001E296E
		// (set) Token: 0x060076AE RID: 30382 RVA: 0x001E4799 File Offset: 0x001E2999
		[Parameter(Mandatory = false)]
		public bool BasicAuthentication
		{
			get
			{
				return base.Fields["BasicAuthentication"] != null && (bool)base.Fields["BasicAuthentication"];
			}
			set
			{
				base.Fields["BasicAuthentication"] = value;
			}
		}

		// Token: 0x17002490 RID: 9360
		// (get) Token: 0x060076AF RID: 30383 RVA: 0x001E47B1 File Offset: 0x001E29B1
		// (set) Token: 0x060076B0 RID: 30384 RVA: 0x001E47DC File Offset: 0x001E29DC
		[Parameter(Mandatory = false)]
		public bool DigestAuthentication
		{
			get
			{
				return base.Fields["DigestAuthentication"] != null && (bool)base.Fields["DigestAuthentication"];
			}
			set
			{
				base.Fields["DigestAuthentication"] = value;
			}
		}

		// Token: 0x17002491 RID: 9361
		// (get) Token: 0x060076B1 RID: 30385 RVA: 0x001E47F4 File Offset: 0x001E29F4
		// (set) Token: 0x060076B2 RID: 30386 RVA: 0x001E481F File Offset: 0x001E2A1F
		[Parameter(Mandatory = false)]
		public bool WindowsAuthentication
		{
			get
			{
				return base.Fields["WindowsAuthentication"] != null && (bool)base.Fields["WindowsAuthentication"];
			}
			set
			{
				base.Fields["WindowsAuthentication"] = value;
			}
		}

		// Token: 0x17002492 RID: 9362
		// (get) Token: 0x060076B3 RID: 30387 RVA: 0x001E4837 File Offset: 0x001E2A37
		// (set) Token: 0x060076B4 RID: 30388 RVA: 0x001E483F File Offset: 0x001E2A3F
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

		// Token: 0x17002493 RID: 9363
		// (get) Token: 0x060076B5 RID: 30389 RVA: 0x001E4848 File Offset: 0x001E2A48
		// (set) Token: 0x060076B6 RID: 30390 RVA: 0x001E4850 File Offset: 0x001E2A50
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
	}
}
