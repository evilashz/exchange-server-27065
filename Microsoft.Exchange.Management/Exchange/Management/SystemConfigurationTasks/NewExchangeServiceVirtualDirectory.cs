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
	// Token: 0x02000C1E RID: 3102
	public abstract class NewExchangeServiceVirtualDirectory<T> : NewExchangeVirtualDirectory<T> where T : ADExchangeServiceVirtualDirectory, new()
	{
		// Token: 0x060074D1 RID: 29905 RVA: 0x001DD5E0 File Offset: 0x001DB7E0
		private string GetAppRootValue()
		{
			return base.RetrieveVDirAppRootValue(base.GetAbsolutePath(IisUtility.AbsolutePathType.WebSiteRoot), this.Name);
		}

		// Token: 0x170023D8 RID: 9176
		// (get) Token: 0x060074D2 RID: 29906
		protected abstract string VirtualDirectoryName { get; }

		// Token: 0x170023D9 RID: 9177
		// (get) Token: 0x060074D3 RID: 29907
		protected abstract string VirtualDirectoryPath { get; }

		// Token: 0x170023DA RID: 9178
		// (get) Token: 0x060074D4 RID: 29908
		protected abstract string DefaultApplicationPoolId { get; }

		// Token: 0x170023DB RID: 9179
		// (get) Token: 0x060074D5 RID: 29909 RVA: 0x001DD5F5 File Offset: 0x001DB7F5
		protected virtual Uri DefaultInternalUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170023DC RID: 9180
		// (get) Token: 0x060074D6 RID: 29910 RVA: 0x001DD5F8 File Offset: 0x001DB7F8
		protected virtual LocalizedString MetabaseSetPropertiesFailureMessage
		{
			get
			{
				return Strings.MetabaseSetPropertiesFailure;
			}
		}

		// Token: 0x060074D7 RID: 29911 RVA: 0x001DD5FF File Offset: 0x001DB7FF
		protected virtual void SetDefaultAuthenticationMethods(ADExchangeServiceVirtualDirectory virtualDirectory)
		{
			virtualDirectory.WindowsAuthentication = new bool?(true);
		}

		// Token: 0x060074D8 RID: 29912 RVA: 0x001DD610 File Offset: 0x001DB810
		protected virtual void AddCustomVDirProperties(ArrayList customProperties)
		{
			customProperties.Add(new MetabaseProperty("AppFriendlyName", this.Name));
			customProperties.Add(new MetabaseProperty("AppRoot", this.GetAppRootValue()));
			customProperties.Add(new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled));
			customProperties.Add(new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Ntlm));
			customProperties.Add(new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Script));
		}

		// Token: 0x060074D9 RID: 29913 RVA: 0x001DD694 File Offset: 0x001DB894
		public NewExchangeServiceVirtualDirectory()
		{
		}

		// Token: 0x170023DD RID: 9181
		// (get) Token: 0x060074DA RID: 29914 RVA: 0x001DD69C File Offset: 0x001DB89C
		internal override MetabasePropertyTypes.AppPoolIdentityType AppPoolIdentityType
		{
			get
			{
				return MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			}
		}

		// Token: 0x170023DE RID: 9182
		// (get) Token: 0x060074DB RID: 29915 RVA: 0x001DD6A0 File Offset: 0x001DB8A0
		protected override ArrayList CustomizedVDirProperties
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				this.AddCustomVDirProperties(arrayList);
				return arrayList;
			}
		}

		// Token: 0x060074DC RID: 29916 RVA: 0x001DD6BC File Offset: 0x001DB8BC
		protected void InternalValidateBasicLiveIdBasic()
		{
			T dataObject = this.DataObject;
			string metabasePath = dataObject.MetabasePath;
			Task.TaskErrorLoggingReThrowDelegate writeError = new Task.TaskErrorLoggingReThrowDelegate(this.WriteError);
			T dataObject2 = this.DataObject;
			using (IisUtility.CreateIISDirectoryEntry(metabasePath, writeError, dataObject2.Identity))
			{
				T dataObject3 = this.DataObject;
				bool flag = dataObject3.BasicAuthentication ?? false;
				T dataObject4 = this.DataObject;
				bool flag2 = dataObject4.LiveIdBasicAuthentication ?? false;
				if (flag && flag2)
				{
					TaskLogger.Trace("Enabling both Basic and LiveIdBasic Authentication is not allowed.", new object[0]);
					Exception exception = new LocalizedException(Strings.ErrorBasicAndLiveIdBasicNotAllowed);
					ErrorCategory category = ErrorCategory.InvalidOperation;
					T dataObject5 = this.DataObject;
					base.WriteError(exception, category, dataObject5.Identity);
				}
			}
		}

		// Token: 0x060074DD RID: 29917 RVA: 0x001DD7B4 File Offset: 0x001DB9B4
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (!base.Fields.IsModified("Path"))
			{
				base.Path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, this.VirtualDirectoryPath);
			}
			if (!base.Fields.IsModified("Name"))
			{
				this.Name = this.VirtualDirectoryName;
			}
			if (!base.Fields.IsModified("AppPoolId"))
			{
				base.AppPoolId = this.DefaultApplicationPoolId;
			}
			if (base.Role == VirtualDirectoryRole.ClientAccess && base.InternalUrl == null && this.DefaultInternalUrl != null)
			{
				base.InternalUrl = this.DefaultInternalUrl;
			}
		}

		// Token: 0x060074DE RID: 29918 RVA: 0x001DD85C File Offset: 0x001DBA5C
		protected override void InternalProcessComplete()
		{
			ADExchangeServiceVirtualDirectory adexchangeServiceVirtualDirectory = this.DataObject;
			base.InternalProcessComplete();
			ExchangeServiceVDirHelper.SetIisVirtualDirectoryAuthenticationMethods(adexchangeServiceVirtualDirectory, new Task.TaskErrorLoggingDelegate(base.WriteError), this.MetabaseSetPropertiesFailureMessage);
			T dataObject = this.DataObject;
			if (dataObject.LiveIdBasicAuthentication != null)
			{
				T dataObject2 = this.DataObject;
				ExchangeServiceVDirHelper.SetLiveIdBasicAuthModule(dataObject2.LiveIdBasicAuthentication.Value, false, adexchangeServiceVirtualDirectory);
			}
			T dataObject3 = this.DataObject;
			if (dataObject3.OAuthAuthentication != null)
			{
				T dataObject4 = this.DataObject;
				ExchangeServiceVDirHelper.SetOAuthAuthenticationModule(dataObject4.OAuthAuthentication.Value, false, adexchangeServiceVirtualDirectory);
			}
			ExchangeServiceVDirHelper.CheckAndUpdateWindowsAuthProvidersIfNecessary(adexchangeServiceVirtualDirectory, new bool?(true));
		}

		// Token: 0x060074DF RID: 29919 RVA: 0x001DD928 File Offset: 0x001DBB28
		protected override IConfigurable PrepareDataObject()
		{
			ADExchangeServiceVirtualDirectory adexchangeServiceVirtualDirectory = (ADExchangeServiceVirtualDirectory)base.PrepareDataObject();
			if (base.Fields["BasicAuthentication"] == null && base.Fields["DigestAuthentication"] == null && base.Fields["WindowsAuthentication"] == null)
			{
				this.SetDefaultAuthenticationMethods(adexchangeServiceVirtualDirectory);
			}
			else
			{
				adexchangeServiceVirtualDirectory.BasicAuthentication = new bool?(this.BasicAuthentication);
				adexchangeServiceVirtualDirectory.DigestAuthentication = new bool?(this.DigestAuthentication);
				adexchangeServiceVirtualDirectory.WindowsAuthentication = new bool?(this.WindowsAuthentication);
			}
			return adexchangeServiceVirtualDirectory;
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x001DD9B4 File Offset: 0x001DBBB4
		private bool GetAuthMethodFieldValue(string fieldName)
		{
			return base.Fields[fieldName] != null && (bool)base.Fields[fieldName];
		}

		// Token: 0x170023DF RID: 9183
		// (get) Token: 0x060074E1 RID: 29921 RVA: 0x001DD9D7 File Offset: 0x001DBBD7
		// (set) Token: 0x060074E2 RID: 29922 RVA: 0x001DD9E4 File Offset: 0x001DBBE4
		[Parameter(Mandatory = false)]
		public bool BasicAuthentication
		{
			get
			{
				return this.GetAuthMethodFieldValue("BasicAuthentication");
			}
			set
			{
				base.Fields["BasicAuthentication"] = value;
			}
		}

		// Token: 0x170023E0 RID: 9184
		// (get) Token: 0x060074E3 RID: 29923 RVA: 0x001DD9FC File Offset: 0x001DBBFC
		// (set) Token: 0x060074E4 RID: 29924 RVA: 0x001DDA09 File Offset: 0x001DBC09
		[Parameter(Mandatory = false)]
		public bool DigestAuthentication
		{
			get
			{
				return this.GetAuthMethodFieldValue("DigestAuthentication");
			}
			set
			{
				base.Fields["DigestAuthentication"] = value;
			}
		}

		// Token: 0x170023E1 RID: 9185
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x001DDA21 File Offset: 0x001DBC21
		// (set) Token: 0x060074E6 RID: 29926 RVA: 0x001DDA2E File Offset: 0x001DBC2E
		[Parameter(Mandatory = false)]
		public bool WindowsAuthentication
		{
			get
			{
				return this.GetAuthMethodFieldValue("WindowsAuthentication");
			}
			set
			{
				base.Fields["WindowsAuthentication"] = value;
			}
		}

		// Token: 0x170023E2 RID: 9186
		// (get) Token: 0x060074E7 RID: 29927 RVA: 0x001DDA46 File Offset: 0x001DBC46
		// (set) Token: 0x060074E8 RID: 29928 RVA: 0x001DDA4E File Offset: 0x001DBC4E
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

		// Token: 0x170023E3 RID: 9187
		// (get) Token: 0x060074E9 RID: 29929 RVA: 0x001DDA57 File Offset: 0x001DBC57
		// (set) Token: 0x060074EA RID: 29930 RVA: 0x001DDA5F File Offset: 0x001DBC5F
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

		// Token: 0x170023E4 RID: 9188
		// (get) Token: 0x060074EB RID: 29931 RVA: 0x001DDA68 File Offset: 0x001DBC68
		// (set) Token: 0x060074EC RID: 29932 RVA: 0x001DDA70 File Offset: 0x001DBC70
		internal new string Name
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
	}
}
