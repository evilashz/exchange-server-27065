using System;
using System.Collections;
using System.Collections.Specialized;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C28 RID: 3112
	[Cmdlet("New", "OwaVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewOwaVirtualDirectory : NewWebAppVirtualDirectory<ADOwaVirtualDirectory>
	{
		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x06007591 RID: 30097 RVA: 0x001E034B File Offset: 0x001DE54B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOwaVirtualDirectory(base.WebSiteName.ToString(), base.Server.ToString());
			}
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x001E0368 File Offset: 0x001DE568
		public NewOwaVirtualDirectory()
		{
			base.Fields["Name"] = string.Empty;
			base.AppPoolId = "MSExchangeOWAAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
		}

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x06007593 RID: 30099 RVA: 0x001E0397 File Offset: 0x001DE597
		internal new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x06007594 RID: 30100 RVA: 0x001E039F File Offset: 0x001DE59F
		private string DomainName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x06007595 RID: 30101 RVA: 0x001E03A2 File Offset: 0x001DE5A2
		private MultiValuedProperty<string> WebReadyFileTypes
		{
			get
			{
				if (this.webReadyFileTypes == null)
				{
					this.webReadyFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyFileTypes);
				}
				return this.webReadyFileTypes;
			}
		}

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06007596 RID: 30102 RVA: 0x001E03C2 File Offset: 0x001DE5C2
		private MultiValuedProperty<string> WebReadyMimeTypes
		{
			get
			{
				if (this.webReadyMimeTypes == null)
				{
					this.webReadyMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultWebReadyMimeTypes);
				}
				return this.webReadyMimeTypes;
			}
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x06007597 RID: 30103 RVA: 0x001E03E2 File Offset: 0x001DE5E2
		private MultiValuedProperty<string> AllowedFileTypes
		{
			get
			{
				if (this.allowedFileTypes == null)
				{
					this.allowedFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultAllowedFileTypes);
				}
				return this.allowedFileTypes;
			}
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x06007598 RID: 30104 RVA: 0x001E0402 File Offset: 0x001DE602
		private MultiValuedProperty<string> AllowedMimeTypes
		{
			get
			{
				if (this.allowedMimeTypes == null)
				{
					this.allowedMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultAllowedMimeTypes);
				}
				return this.allowedMimeTypes;
			}
		}

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x06007599 RID: 30105 RVA: 0x001E0422 File Offset: 0x001DE622
		private MultiValuedProperty<string> ForceSaveFileTypes
		{
			get
			{
				if (this.forceSaveFileTypes == null)
				{
					this.forceSaveFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultForceSaveFileTypes);
				}
				return this.forceSaveFileTypes;
			}
		}

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x0600759A RID: 30106 RVA: 0x001E0442 File Offset: 0x001DE642
		private MultiValuedProperty<string> ForceSaveMimeTypes
		{
			get
			{
				if (this.forceSaveMimeTypes == null)
				{
					this.forceSaveMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultForceSaveMimeTypes);
				}
				return this.forceSaveMimeTypes;
			}
		}

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x0600759B RID: 30107 RVA: 0x001E0462 File Offset: 0x001DE662
		private MultiValuedProperty<string> BlockedFileTypes
		{
			get
			{
				if (this.blockedFileTypes == null)
				{
					this.blockedFileTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultBlockedFileTypes);
				}
				return this.blockedFileTypes;
			}
		}

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x0600759C RID: 30108 RVA: 0x001E0482 File Offset: 0x001DE682
		private MultiValuedProperty<string> BlockedMimeTypes
		{
			get
			{
				if (this.blockedMimeTypes == null)
				{
					this.blockedMimeTypes = new MultiValuedProperty<string>(OwaMailboxPolicySchema.DefaultBlockedMimeTypes);
				}
				return this.blockedMimeTypes;
			}
		}

		// Token: 0x0600759D RID: 30109 RVA: 0x001E04A2 File Offset: 0x001DE6A2
		protected override bool FailOnVirtualDirectoryAlreadyExists()
		{
			return true;
		}

		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x0600759E RID: 30110 RVA: 0x001E04A8 File Offset: 0x001DE6A8
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
					new MetabaseProperty("AppIsolated", MetabasePropertyTypes.AppIsolated.Pooled)
				};
			}
		}

		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x0600759F RID: 30111 RVA: 0x001E0578 File Offset: 0x001DE778
		protected override ListDictionary ChildVirtualDirectories
		{
			get
			{
				ListDictionary listDictionary = new ListDictionary();
				this.AddAuthVDir(listDictionary);
				if (base.Role == VirtualDirectoryRole.Mailbox)
				{
					base.AddBinVDir(listDictionary);
					this.AddSMimeVDir(listDictionary);
					OwaVirtualDirectoryHelper.AddVersionVDir(listDictionary);
				}
				return listDictionary;
			}
		}

		// Token: 0x060075A0 RID: 30112 RVA: 0x001E05B0 File Offset: 0x001DE7B0
		protected override void DeleteFromMetabase()
		{
			base.DeleteFromMetabase();
			string webSiteRoot = IisUtility.GetWebSiteRoot(this.DataObject.MetabasePath);
			IList createdLegacyVDirs = OwaVirtualDirectoryHelper.CreatedLegacyVDirs;
			if (createdLegacyVDirs != null)
			{
				foreach (object obj in createdLegacyVDirs)
				{
					string name = (string)obj;
					if (IisUtility.WebDirObjectExists(webSiteRoot, name))
					{
						IisUtility.DeleteWebDirObject(webSiteRoot, name);
					}
				}
				OwaVirtualDirectoryHelper.CreatedLegacyVDirs.Clear();
			}
		}

		// Token: 0x060075A1 RID: 30113 RVA: 0x001E063C File Offset: 0x001DE83C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			if (!base.Fields.IsModified("Path"))
			{
				base.Path = System.IO.Path.Combine(ConfigurationContext.Setup.InstallPath, (base.Role == VirtualDirectoryRole.ClientAccess) ? "FrontEnd\\HttpProxy\\owa" : "ClientAccess\\owa");
			}
			if (this.Name != string.Empty && !this.Name.Equals("owa"))
			{
				this.WriteWarning(Strings.OwaNameParameterIgnored);
			}
			base.Fields["Name"] = "owa";
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (!new VirtualDirectoryPathExistsCondition(base.OwningServer.Fqdn, base.Path).Verify())
			{
				base.WriteError(new ArgumentException(Strings.ErrorPathNotExistsOnServer(base.Path, base.OwningServer.Name), "Path"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060075A2 RID: 30114 RVA: 0x001E0747 File Offset: 0x001DE947
		protected override bool InternalProcessStartWork()
		{
			this.SetDefaults();
			return true;
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x001E0750 File Offset: 0x001DE950
		protected override void InternalProcessMetabase()
		{
			OwaVirtualDirectoryHelper.CreateOwaCalendarVDir(this.DataObject.MetabasePath, base.Role);
			if (base.Role == VirtualDirectoryRole.Mailbox)
			{
				OwaVirtualDirectoryHelper.CreateLegacyVDirs(this.DataObject.MetabasePath, false);
				try
				{
					OwaVirtualDirectoryHelper.EnableIsapiFilter(this.DataObject, false);
					goto IL_EB;
				}
				catch (Exception ex)
				{
					TaskLogger.Trace("Exception occurred in EnableIsapiFilter(): {0}", new object[]
					{
						ex.Message
					});
					this.WriteWarning(Strings.OwaMetabaseIsapiInstallFailure);
					throw;
				}
			}
			if (!Datacenter.IsMultiTenancyEnabled())
			{
				OwaVirtualDirectoryHelper.CreateOwaIntegratedVDir(this.DataObject.MetabasePath, base.Role);
				this.DataObject.IntegratedFeaturesEnabled = new bool?(true);
			}
			OwaVirtualDirectoryHelper.CreateOmaVDir(this.DataObject.MetabasePath, base.Role);
			try
			{
				OwaVirtualDirectoryHelper.EnableIsapiFilter(this.DataObject, true);
			}
			catch (Exception ex2)
			{
				TaskLogger.Trace("Exception occurred in EnableIsapiFilterForCafe(): {0}", new object[]
				{
					ex2.Message
				});
				this.WriteWarning(Strings.OwaMetabaseIsapiInstallFailure);
				throw;
			}
			try
			{
				IL_EB:
				WebAppVirtualDirectoryHelper.UpdateMetabase(this.DataObject, this.DataObject.MetabasePath, base.Role == VirtualDirectoryRole.Mailbox);
			}
			catch (Exception ex3)
			{
				TaskLogger.Trace("Exception occurred in UpdateMetabase(): {0}", new object[]
				{
					ex3.Message
				});
				this.WriteWarning(Strings.OwaMetabaseGetPropertiesFailure);
				throw;
			}
			if (base.Role == VirtualDirectoryRole.Mailbox && Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6)
			{
				try
				{
					Gzip.SetIisGzipMimeTypes();
				}
				catch (Exception ex4)
				{
					TaskLogger.Trace("Exception occurred in SetIisGzipMimeTypes(): {0}", new object[]
					{
						ex4.Message
					});
					this.WriteWarning(Strings.SetIISGzipMimeTypesFailure);
					throw;
				}
			}
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x001E0924 File Offset: 0x001DEB24
		protected override void InternalProcessComplete()
		{
			this.SetAttachmentPolicy();
			if (base.Role == VirtualDirectoryRole.Mailbox)
			{
				ExchangeServiceVDirHelper.EwsAutodiscMWA.OnNewManageWCFEndpoints(this, ExchangeServiceVDirHelper.EwsAutodiscMWA.EndpointProtocol.OwaEws, new bool?(this.DataObject.BasicAuthentication), new bool?(this.DataObject.WindowsAuthentication), false, false, this.DataObject, base.Role);
			}
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x001E0975 File Offset: 0x001DEB75
		protected override void WriteResultMetabaseFixup(ExchangeVirtualDirectory targetDataObject)
		{
			base.WriteResultMetabaseFixup(targetDataObject);
			if (WebAppVirtualDirectoryHelper.FindWebAppVirtualDirectoryInSameWebSite<ADEcpVirtualDirectory>((ExchangeWebAppVirtualDirectory)targetDataObject, base.DataSession) == null)
			{
				this.WriteWarning(Strings.CreateEcpForOwaWarning);
			}
		}

		// Token: 0x060075A6 RID: 30118 RVA: 0x001E099C File Offset: 0x001DEB9C
		private void SetAttachmentPolicy()
		{
			this.DataObject.WebReadyFileTypes = this.WebReadyFileTypes;
			this.DataObject.WebReadyMimeTypes = this.WebReadyMimeTypes;
			this.DataObject.AllowedFileTypes = this.AllowedFileTypes;
			this.DataObject.AllowedMimeTypes = this.AllowedMimeTypes;
			this.DataObject.ForceSaveFileTypes = this.ForceSaveFileTypes;
			this.DataObject.ForceSaveMimeTypes = this.ForceSaveMimeTypes;
			this.DataObject.BlockedFileTypes = this.BlockedFileTypes;
			this.DataObject.BlockedMimeTypes = this.BlockedMimeTypes;
			base.DataSession.Save(this.DataObject);
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x001E0A44 File Offset: 0x001DEC44
		private void SetDefaults()
		{
			this.DataObject.DefaultClientLanguage = new int?((int)OwaMailboxPolicySchema.DefaultClientLanguage.DefaultValue);
			this.DataObject.OutboundCharset = new OutboundCharsetOptions?((OutboundCharsetOptions)OwaMailboxPolicySchema.OutboundCharset.DefaultValue);
			this.DataObject.OwaVersion = (OwaVersions)ADOwaVirtualDirectorySchema.OwaVersion.DefaultValue;
			this.DataObject.LogonFormat = LogonFormats.FullDomain;
			this.DataObject.ClientAuthCleanupLevel = ClientAuthCleanupLevels.High;
			this.DataObject.LogonAndErrorLanguage = (int)OwaMailboxPolicySchema.LogonAndErrorLanguage.DefaultValue;
			this.DataObject.RemoteDocumentsActionForUnknownServers = new RemoteDocumentsActions?(RemoteDocumentsActions.Block);
			this.DataObject.ActionForUnknownFileAndMIMETypes = new AttachmentBlockingActions?(AttachmentBlockingActions.Allow);
			this.DataObject.FilterWebBeaconsAndHtmlForms = new WebBeaconFilterLevels?(WebBeaconFilterLevels.UserFilterChoice);
			this.DataObject.NotificationInterval = new int?(120);
			this.DataObject.UserContextTimeout = new int?(60);
			this.DataObject.RedirectToOptimalOWAServer = new bool?(true);
			this.DataObject.UseGB18030 = new bool?(Convert.ToBoolean((int)OwaMailboxPolicySchema.UseGB18030.DefaultValue));
			this.DataObject.UseISO885915 = new bool?(Convert.ToBoolean((int)OwaMailboxPolicySchema.UseISO885915.DefaultValue));
			this.DataObject.InstantMessagingType = new InstantMessagingTypeOptions?((InstantMessagingTypeOptions)OwaMailboxPolicySchema.InstantMessagingType.DefaultValue);
			this.DataObject[ADOwaVirtualDirectorySchema.ADMailboxFolderSet] = (int)OwaMailboxPolicySchema.ADMailboxFolderSet.DefaultValue;
			this.DataObject[ADOwaVirtualDirectorySchema.ADMailboxFolderSet2] = (int)OwaMailboxPolicySchema.ADMailboxFolderSet2.DefaultValue;
			this.DataObject[ADOwaVirtualDirectorySchema.FileAccessControlOnPublicComputers] = (int)OwaMailboxPolicySchema.FileAccessControlOnPublicComputers.DefaultValue;
			this.DataObject[ADOwaVirtualDirectorySchema.FileAccessControlOnPrivateComputers] = (int)OwaMailboxPolicySchema.FileAccessControlOnPrivateComputers.DefaultValue;
			this.DataObject.DefaultDomain = "";
			this.DataObject.GzipLevel = GzipLevel.High;
			this.DataObject.FormsAuthentication = (base.Role == VirtualDirectoryRole.ClientAccess);
			this.DataObject.BasicAuthentication = (base.Role == VirtualDirectoryRole.ClientAccess);
			this.DataObject.DigestAuthentication = false;
			this.DataObject.WindowsAuthentication = (base.Role == VirtualDirectoryRole.Mailbox);
			this.DataObject.LiveIdAuthentication = false;
			this.DataObject.ExternalAuthenticationMethods = new MultiValuedProperty<AuthenticationMethod>(AuthenticationMethod.Fba);
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x001E0CC0 File Offset: 0x001DEEC0
		private void AddAuthVDir(ListDictionary childVDirs)
		{
			TaskLogger.LogEnter();
			childVDirs.Add("auth", new ArrayList
			{
				new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read | MetabasePropertyTypes.AccessFlags.Execute | MetabasePropertyTypes.AccessFlags.Script),
				new MetabaseProperty("AuthFlags", MetabasePropertyTypes.AuthFlags.Anonymous),
				new MetabaseProperty("LogonMethod", MetabasePropertyTypes.LogonMethod.ClearTextLogon),
				new MetabaseProperty("HttpExpires", "D, 0x278d00")
			});
			TaskLogger.LogExit();
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x001E0D48 File Offset: 0x001DEF48
		private void AddSMimeVDir(ListDictionary childVDirs)
		{
			base.AddChildVDir(childVDirs, "smime", new MetabaseProperty[]
			{
				new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.Read)
			});
		}

		// Token: 0x060075AA RID: 30122 RVA: 0x001E0D7C File Offset: 0x001DEF7C
		private string LDAPPrefix()
		{
			return NewOwaVirtualDirectory.LDAPPrefix(base.DomainController);
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x001E0D8E File Offset: 0x001DEF8E
		private static string LDAPPrefix(string DomainController)
		{
			if (!string.IsNullOrEmpty(DomainController))
			{
				return "LDAP://" + DomainController + "/";
			}
			return "LDAP://";
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x001E0DAE File Offset: 0x001DEFAE
		private string GetFullyQualifiedDomainName()
		{
			return NewOwaVirtualDirectory.GetFullyQualifiedDomainName(base.DomainController);
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x001E0DC0 File Offset: 0x001DEFC0
		internal static string GetFullyQualifiedDomainName(string DomainController)
		{
			TaskLogger.LogEnter();
			DirectoryEntry directoryEntry = null;
			DirectoryEntry directoryEntry2 = null;
			DirectorySearcher directorySearcher = null;
			SearchResultCollection searchResultCollection = null;
			int num;
			try
			{
				string path = NewOwaVirtualDirectory.LDAPPrefix(DomainController) + "RootDSE";
				directoryEntry = new DirectoryEntry(path);
				directoryEntry2 = new DirectoryEntry(NewOwaVirtualDirectory.LDAPPrefix(DomainController) + directoryEntry.Properties["configurationNamingContext"].Value);
				directorySearcher = new DirectorySearcher(directoryEntry2);
				directorySearcher.Filter = "(&(objectClass=msExchRecipientPolicy)(msExchPolicyOrder=2147483647))";
				directorySearcher.PropertiesToLoad.Add("gatewayProxy");
				directorySearcher.SearchScope = SearchScope.Subtree;
				searchResultCollection = directorySearcher.FindAll();
				foreach (object obj in searchResultCollection)
				{
					SearchResult searchResult = (SearchResult)obj;
					ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["gatewayProxy"];
					foreach (object obj2 in resultPropertyValueCollection)
					{
						string text = obj2.ToString();
						if (text.StartsWith("SMTP:"))
						{
							num = text.IndexOf('@');
							if (num >= 0)
							{
								TaskLogger.LogExit();
								return text.Substring(num + 1);
							}
						}
					}
				}
			}
			catch (COMException ex)
			{
				throw new IISGeneralCOMException(ex.Message, ex.ErrorCode, ex);
			}
			finally
			{
				if (searchResultCollection != null)
				{
					searchResultCollection.Dispose();
				}
				if (directorySearcher != null)
				{
					directorySearcher.Dispose();
				}
				if (directoryEntry2 != null)
				{
					directoryEntry2.Dispose();
				}
				if (directoryEntry != null)
				{
					directoryEntry.Dispose();
				}
			}
			TaskLogger.LogExit();
			string hostName = Dns.GetHostName();
			IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
			string hostName2 = hostEntry.HostName;
			num = hostName2.IndexOf('.');
			return (num >= 0 && num < hostName2.Length - 1) ? hostName2.Substring(num + 1) : hostName2;
		}

		// Token: 0x04003B8A RID: 15242
		private const string defaultE12Name = "owa";

		// Token: 0x04003B8B RID: 15243
		private const string backendStorageUncPath = "\\\\.\\BackOfficeStorage\\";

		// Token: 0x04003B8C RID: 15244
		private const string LdapProtocol = "LDAP://";

		// Token: 0x04003B8D RID: 15245
		private MultiValuedProperty<string> webReadyFileTypes;

		// Token: 0x04003B8E RID: 15246
		private MultiValuedProperty<string> webReadyMimeTypes;

		// Token: 0x04003B8F RID: 15247
		private MultiValuedProperty<string> allowedFileTypes;

		// Token: 0x04003B90 RID: 15248
		private MultiValuedProperty<string> allowedMimeTypes;

		// Token: 0x04003B91 RID: 15249
		private MultiValuedProperty<string> forceSaveFileTypes;

		// Token: 0x04003B92 RID: 15250
		private MultiValuedProperty<string> forceSaveMimeTypes;

		// Token: 0x04003B93 RID: 15251
		private MultiValuedProperty<string> blockedFileTypes;

		// Token: 0x04003B94 RID: 15252
		private MultiValuedProperty<string> blockedMimeTypes;
	}
}
