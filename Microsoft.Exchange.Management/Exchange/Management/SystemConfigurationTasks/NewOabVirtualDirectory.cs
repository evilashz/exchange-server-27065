using System;
using System.Collections;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Runtime.InteropServices;
using IISOle;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C27 RID: 3111
	[Cmdlet("New", "OabVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class NewOabVirtualDirectory : NewExchangeVirtualDirectory<ADOabVirtualDirectory>
	{
		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x06007576 RID: 30070 RVA: 0x001DF9B6 File Offset: 0x001DDBB6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOabVirtualDirectory(base.WebSiteName.ToString(), base.Server.ToString());
			}
		}

		// Token: 0x06007577 RID: 30071 RVA: 0x001DF9D3 File Offset: 0x001DDBD3
		public NewOabVirtualDirectory()
		{
			this.Name = "OAB";
			base.AppPoolId = "MSExchangeOABAppPool";
			base.AppPoolManagedPipelineMode = MetabasePropertyTypes.ManagedPipelineMode.Integrated;
			this.RequireSSL = true;
		}

		// Token: 0x06007578 RID: 30072 RVA: 0x001DF9FF File Offset: 0x001DDBFF
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(IISNotInstalledException).IsInstanceOfType(exception);
		}

		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x06007579 RID: 30073 RVA: 0x001DFA21 File Offset: 0x001DDC21
		// (set) Token: 0x0600757A RID: 30074 RVA: 0x001DFA29 File Offset: 0x001DDC29
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

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x0600757B RID: 30075 RVA: 0x001DFA32 File Offset: 0x001DDC32
		// (set) Token: 0x0600757C RID: 30076 RVA: 0x001DFA3A File Offset: 0x001DDC3A
		internal new string ApplicationRoot
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

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x0600757D RID: 30077 RVA: 0x001DFA43 File Offset: 0x001DDC43
		// (set) Token: 0x0600757E RID: 30078 RVA: 0x001DFA4B File Offset: 0x001DDC4B
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

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x0600757F RID: 30079 RVA: 0x001DFA54 File Offset: 0x001DDC54
		// (set) Token: 0x06007580 RID: 30080 RVA: 0x001DFA5C File Offset: 0x001DDC5C
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

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x06007581 RID: 30081 RVA: 0x001DFA65 File Offset: 0x001DDC65
		// (set) Token: 0x06007582 RID: 30082 RVA: 0x001DFA72 File Offset: 0x001DDC72
		[Parameter]
		public int PollInterval
		{
			get
			{
				return this.DataObject.PollInterval;
			}
			set
			{
				this.DataObject.PollInterval = value;
			}
		}

		// Token: 0x17002421 RID: 9249
		// (get) Token: 0x06007583 RID: 30083 RVA: 0x001DFA80 File Offset: 0x001DDC80
		// (set) Token: 0x06007584 RID: 30084 RVA: 0x001DFA97 File Offset: 0x001DDC97
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

		// Token: 0x17002422 RID: 9250
		// (get) Token: 0x06007585 RID: 30085 RVA: 0x001DFAAF File Offset: 0x001DDCAF
		// (set) Token: 0x06007586 RID: 30086 RVA: 0x001DFAD5 File Offset: 0x001DDCD5
		[Parameter]
		public SwitchParameter Recovery
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recovery"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Recovery"] = value;
			}
		}

		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x06007587 RID: 30087 RVA: 0x001DFAED File Offset: 0x001DDCED
		internal override MetabasePropertyTypes.AppPoolIdentityType AppPoolIdentityType
		{
			get
			{
				return MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			}
		}

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x06007588 RID: 30088 RVA: 0x001DFAF0 File Offset: 0x001DDCF0
		protected override ArrayList CustomizedVDirProperties
		{
			get
			{
				ArrayList customizedVDirProperties = base.CustomizedVDirProperties;
				customizedVDirProperties.Add(new MetabaseProperty("MimeMap", new MimeMapClass
				{
					Extension = ".lzx",
					MimeType = "application/octet-stream"
				}));
				return customizedVDirProperties;
			}
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x001DFB34 File Offset: 0x001DDD34
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADOabVirtualDirectory adoabVirtualDirectory = (ADOabVirtualDirectory)base.PrepareDataObject();
			adoabVirtualDirectory.RequireSSL = this.RequireSSL;
			this.serverFQDN = base.OwningServer.Fqdn;
			string text;
			if (string.IsNullOrEmpty(base.WebSiteName))
			{
				if (base.Role == VirtualDirectoryRole.ClientAccess)
				{
					text = IisUtility.GetWebSiteName(IisUtility.CreateAbsolutePath(IisUtility.AbsolutePathType.WebSiteRoot, this.serverFQDN, "/W3SVC/1/ROOT", null));
				}
				else
				{
					text = "Exchange Back End";
				}
			}
			else
			{
				text = base.WebSiteName;
			}
			ADOabVirtualDirectory[] array = ((IConfigurationSession)base.DataSession).Find<ADOabVirtualDirectory>(base.OwningServer.Id, QueryScope.SubTree, null, null, 0);
			if (array != null && array.Length != 0)
			{
				ADOabVirtualDirectory[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					ADOabVirtualDirectory adoabVirtualDirectory2 = array2[i];
					string webSiteRootPath = null;
					string text2 = null;
					try
					{
						IisUtility.ParseApplicationRootPath(adoabVirtualDirectory2.MetabasePath, ref webSiteRootPath, ref text2);
					}
					catch (IisUtilityInvalidApplicationRootPathException ex)
					{
						base.WriteWarning(ex.Message);
						goto IL_24E;
					}
					goto IL_D6;
					IL_24E:
					i++;
					continue;
					IL_D6:
					string webSiteName = IisUtility.GetWebSiteName(webSiteRootPath);
					if (string.Compare(webSiteName, text, false) == 0)
					{
						try
						{
							if (DirectoryEntry.Exists(adoabVirtualDirectory2.MetabasePath))
							{
								if (!this.Recovery)
								{
									base.WriteError(new InvalidOperationException(Strings.ErrorOabVirtualDirectoryAlreadyExists(adoabVirtualDirectory2.Identity.ToString(), text, this.serverFQDN)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
								}
								else
								{
									base.WriteError(new InvalidOperationException(Strings.ErrorOabVirtualDirectoryAlreadyExistsWithRecovery(adoabVirtualDirectory2.Identity.ToString(), text, this.serverFQDN)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
								}
							}
							else if (!this.Recovery)
							{
								base.WriteError(new InvalidOperationException(Strings.ErrorOabVirtualDirectoryADObjectAlreadyExists(adoabVirtualDirectory2.Identity.ToString(), text, this.serverFQDN)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
							}
							else
							{
								array[0].CopyChangesFrom(adoabVirtualDirectory);
								adoabVirtualDirectory = adoabVirtualDirectory2;
								adoabVirtualDirectory.SetId(new ADObjectId(adoabVirtualDirectory.Server.DistinguishedName).GetDescendantId("Protocols", "HTTP", new string[]
								{
									string.Format("{0} ({1})", this.Name, base.WebSiteName)
								}));
								adoabVirtualDirectory.MetabasePath = string.Format("IIS://{0}{1}/{2}", this.serverFQDN, IisUtility.FindWebSiteRootPath(base.WebSiteName, this.serverFQDN), this.Name);
							}
						}
						catch (COMException exception)
						{
							base.WriteError(exception, ErrorCategory.ReadError, null);
						}
						goto IL_24E;
					}
					goto IL_24E;
				}
			}
			if (new VirtualDirectoryExistsCondition(this.serverFQDN, text, this.Name).Verify())
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorOabVirtualDirectoryIISObjectAlreadyExists(string.Format("{0}\\{1}", text, this.Name), text, this.serverFQDN)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			this.isExistingFolder = true;
			this.isLocalServer = this.IsLocalServer();
			if (string.IsNullOrEmpty(base.Path))
			{
				base.Path = System.IO.Path.Combine(base.OwningServer.InstallPath.PathName, (base.Role == VirtualDirectoryRole.ClientAccess) ? "FrontEnd\\HttpProxy\\OAB" : "ClientAccess\\OAB");
			}
			else
			{
				LocalLongFullPath localLongFullPath = null;
				try
				{
					localLongFullPath = LocalLongFullPath.Parse(base.Path);
				}
				catch (ArgumentException ex2)
				{
					base.WriteError(new ArgumentException(new LocalizedString(ex2.Message.ToString()), "Path"), ErrorCategory.InvalidArgument, base.OwningServer.Identity);
					return null;
				}
				base.Path = localLongFullPath.PathName;
			}
			if (base.Role == VirtualDirectoryRole.ClientAccess && adoabVirtualDirectory.InternalUrl == null)
			{
				if (this.isLocalServer)
				{
					adoabVirtualDirectory.InternalUrl = new Uri(string.Format("http://{0}/{1}", ComputerInformation.DnsFullyQualifiedDomainName, "OAB"));
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorMissingInternalUrlInRemoteScenario, "InternalUrl"), ErrorCategory.InvalidArgument, base.OwningServer.Identity);
				}
			}
			TaskLogger.LogExit();
			return adoabVirtualDirectory;
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x001DFF58 File Offset: 0x001DE158
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			try
			{
				if (!new PathOnFixedDriveCondition(this.serverFQDN, base.Path).Verify())
				{
					base.WriteError(new ArgumentException(Strings.ErrorOabVirtualDirctoryPathNotOnFixedDrive(base.Path), "Path"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
				if (!new VirtualDirectoryPathExistsCondition(this.serverFQDN, base.Path).Verify())
				{
					if (this.isLocalServer)
					{
						try
						{
							Directory.CreateDirectory(base.Path);
							goto IL_FA;
						}
						catch (UnauthorizedAccessException ex)
						{
							this.isExistingFolder = false;
							TaskLogger.Trace("The creation of directoy '{0}' failed, returned error: {1}.", new object[]
							{
								base.Path,
								ex.Message.ToString()
							});
							goto IL_FA;
						}
						catch (IOException ex2)
						{
							this.isExistingFolder = false;
							TaskLogger.Trace("The creation of directoy '{0}' failed, returned error: {1}.", new object[]
							{
								base.Path,
								ex2.Message.ToString()
							});
							goto IL_FA;
						}
					}
					this.isExistingFolder = false;
				}
				IL_FA:;
			}
			catch (WmiException ex3)
			{
				base.WriteError(new InvalidOperationException(new LocalizedString(ex3.Message)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600758B RID: 30091 RVA: 0x001E00B8 File Offset: 0x001DE2B8
		private bool IsLocalServer()
		{
			string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			int num = localComputerFqdn.IndexOf('.');
			string value = (num == -1) ? localComputerFqdn : localComputerFqdn.Substring(0, num);
			return base.OwningServer.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600758C RID: 30092 RVA: 0x001E00F8 File Offset: 0x001DE2F8
		internal static void UpdateMetabase(ADOabVirtualDirectory virtualDirectory, bool updateAuthenticationMethod, Task.TaskErrorLoggingDelegate handler)
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
					if (updateAuthenticationMethod)
					{
						uint num2 = (uint)((int)(IisUtility.GetIisPropertyValue("AuthFlags", IisUtility.GetProperties(directoryEntry)) ?? 0));
						num2 |= 4U;
						num2 &= 4294967294U;
						arrayList.Add(new MetabaseProperty("AuthFlags", num2, true));
						MultiValuedProperty<AuthenticationMethod> multiValuedProperty = new MultiValuedProperty<AuthenticationMethod>();
						multiValuedProperty.Add(AuthenticationMethod.WindowsIntegrated);
						if (IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic))
						{
							multiValuedProperty.Add(AuthenticationMethod.Basic);
						}
						if (IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Digest))
						{
							multiValuedProperty.Add(AuthenticationMethod.Digest);
						}
						if (IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Fba))
						{
							multiValuedProperty.Add(AuthenticationMethod.Fba);
						}
						virtualDirectory.ExternalAuthenticationMethods = (virtualDirectory.InternalAuthenticationMethods = multiValuedProperty);
					}
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

		// Token: 0x0600758D RID: 30093 RVA: 0x001E0290 File Offset: 0x001DE490
		protected override void InternalProcessMetabase()
		{
			TaskLogger.LogEnter();
			base.InternalProcessMetabase();
			NewOabVirtualDirectory.UpdateMetabase(this.DataObject, true, new Task.TaskErrorLoggingDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x0600758E RID: 30094 RVA: 0x001E02BC File Offset: 0x001DE4BC
		protected override void InternalProcessComplete()
		{
			TaskLogger.LogEnter();
			base.InternalProcessComplete();
			if (!this.isExistingFolder)
			{
				if (this.isLocalServer)
				{
					this.WriteWarning(Strings.FaildToCreateFolder(base.Path));
				}
				else
				{
					this.WriteWarning(Strings.FolderNotExistsOnRemoteServer(base.Path, this.serverFQDN));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600758F RID: 30095 RVA: 0x001E0313 File Offset: 0x001DE513
		protected override bool FailOnVirtualDirectoryADObjectAlreadyExists()
		{
			return this.Recovery == false;
		}

		// Token: 0x06007590 RID: 30096 RVA: 0x001E0321 File Offset: 0x001DE521
		protected override void WriteResultMetabaseFixup(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.WriteResultMetabaseFixup(dataObject);
			((ADOabVirtualDirectory)dataObject).RequireSSL = this.RequireSSL;
			dataObject.ResetChangeTracking();
			TaskLogger.LogExit();
		}

		// Token: 0x04003B83 RID: 15235
		private const string oabVirtualDirectoryName = "OAB";

		// Token: 0x04003B84 RID: 15236
		private const string oabVirtualDirectoryPath = "FrontEnd\\HttpProxy\\OAB";

		// Token: 0x04003B85 RID: 15237
		private const string oabBackEndVirtualDirectoryPath = "ClientAccess\\OAB";

		// Token: 0x04003B86 RID: 15238
		private const string defaultApplicationPool = "MSExchangeOABAppPool";

		// Token: 0x04003B87 RID: 15239
		private bool isExistingFolder;

		// Token: 0x04003B88 RID: 15240
		private bool isLocalServer;

		// Token: 0x04003B89 RID: 15241
		private string serverFQDN;
	}
}
