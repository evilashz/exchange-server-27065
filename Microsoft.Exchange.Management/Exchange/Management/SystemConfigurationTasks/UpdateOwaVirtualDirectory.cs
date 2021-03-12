﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C55 RID: 3157
	[Cmdlet("Update", "OwaVirtualDirectory", SupportsShouldProcess = true)]
	public sealed class UpdateOwaVirtualDirectory : DataAccessTask<ADOwaVirtualDirectory>
	{
		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x060077DA RID: 30682 RVA: 0x001E851C File Offset: 0x001E671C
		// (set) Token: 0x060077DB RID: 30683 RVA: 0x001E8524 File Offset: 0x001E6724
		[Parameter]
		public new Fqdn DomainController
		{
			get
			{
				return base.DomainController;
			}
			set
			{
				base.DomainController = value;
			}
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x001E8530 File Offset: 0x001E6730
		private static void XCopyEntireFoldersWithOverwrite(string fromFolder, string toFolder, Task.TaskVerboseLoggingDelegate logger)
		{
			if (string.IsNullOrEmpty(fromFolder))
			{
				throw new ArgumentNullException("fromFolder");
			}
			if (string.IsNullOrEmpty(toFolder))
			{
				throw new ArgumentNullException("toFolder");
			}
			if (logger == null)
			{
				throw new ArgumentNullException("logger");
			}
			logger(Strings.VerboseCopyDirectory("localhost", fromFolder, toFolder));
			if (!Directory.Exists(fromFolder))
			{
				return;
			}
			if (!Directory.Exists(toFolder))
			{
				logger(Strings.VerboseCreateDirectory("localhost", toFolder));
				Directory.CreateDirectory(toFolder);
			}
			foreach (string text in Directory.GetFiles(fromFolder))
			{
				string text2 = Path.Combine(toFolder, Path.GetFileName(text));
				logger(Strings.VerboseCopyFile("localhost", text, text2));
				File.Copy(text, text2, true);
			}
			foreach (string text3 in Directory.GetDirectories(fromFolder))
			{
				string toFolder2 = Path.Combine(toFolder, Path.GetFileName(text3));
				UpdateOwaVirtualDirectory.XCopyEntireFoldersWithOverwrite(text3, toFolder2, logger);
			}
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x001E862C File Offset: 0x001E682C
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.owaVersion = OwaVirtualDirectoryHelper.GetOwaAssemblyVersion();
			this.owaCurrentFolder = Path.Combine(OwaVirtualDirectoryHelper.OwaPath, "Current");
			this.owaVersionFolder = Path.Combine(OwaVirtualDirectoryHelper.OwaPath, this.owaVersion);
			try
			{
				UpdateOwaVirtualDirectory.XCopyEntireFoldersWithOverwrite(this.owaCurrentFolder, this.owaVersionFolder, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			catch (UnauthorizedAccessException ex)
			{
				throw new CopyDirectoriesException(this.owaCurrentFolder, this.owaVersionFolder, ex.Message, ex);
			}
			catch (IOException ex2)
			{
				throw new CopyDirectoriesException(this.owaCurrentFolder, this.owaVersionFolder, ex2.Message, ex2);
			}
			catch (NotSupportedException ex3)
			{
				throw new CopyDirectoriesException(this.owaCurrentFolder, this.owaVersionFolder, ex3.Message, ex3);
			}
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 131, "InternalBeginProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\VirtualDirectoryTasks\\UpdateOWAVirtualDirectory.cs");
			TaskLogger.LogExit();
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x001E8748 File Offset: 0x001E6948
		protected override IConfigDataProvider CreateSession()
		{
			return this.configurationSession;
		}

		// Token: 0x060077DF RID: 30687 RVA: 0x001E8750 File Offset: 0x001E6950
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			ServerIdParameter serverIdParameter = new ServerIdParameter();
			Server server = (Server)base.GetDataObject<Server>(serverIdParameter, base.DataSession, null, new LocalizedString?(Strings.ErrorServerNotFound(serverIdParameter.ToString())), new LocalizedString?(Strings.ErrorServerNotUnique(serverIdParameter.ToString())));
			if (!server.IsClientAccessServer && !server.IsCafeServer)
			{
				base.ThrowTerminatingError(server.GetServerRoleError(ServerRole.ClientAccess), ErrorCategory.InvalidOperation, server);
			}
			using (ServerManager serverManager = new ServerManager())
			{
				ApplicationPool applicationPool = serverManager.ApplicationPools["MSExchangeOWAAppPool"];
				if (applicationPool == null)
				{
					base.ThrowTerminatingError(new ADNoSuchObjectException(Strings.ErrorOWAVdirAppPoolNotExist), ErrorCategory.ObjectNotFound, serverManager.ApplicationPools);
				}
				applicationPool.ManagedPipelineMode = 0;
				serverManager.CommitChanges();
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.NotEqual, ADOwaVirtualDirectorySchema.OwaVersion, OwaVersions.Exchange2003or2000);
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ADOwaVirtualDirectory), filter, server.Identity, true));
			IConfigDataProvider dataSession = base.DataSession;
			IEnumerable<ADOwaVirtualDirectory> enumerable = dataSession.FindPaged<ADOwaVirtualDirectory>(filter, server.Identity, true, null, 0);
			foreach (ADOwaVirtualDirectory adowaVirtualDirectory in enumerable)
			{
				if (adowaVirtualDirectory.WebSite.Equals("Exchange Back End", StringComparison.OrdinalIgnoreCase))
				{
					string metabasePath = adowaVirtualDirectory.MetabasePath;
					try
					{
						base.WriteVerbose(Strings.VerboseConnectingIISVDir(metabasePath));
						using (IisUtility.CreateIISDirectoryEntry(metabasePath))
						{
							if (!DirectoryEntry.Exists(metabasePath))
							{
								this.WriteWarning(Strings.OwaAdOrphanFound(adowaVirtualDirectory.Identity.ToString()));
								continue;
							}
							if (!IisUtility.WebDirObjectExists(metabasePath, this.owaVersion))
							{
								base.WriteVerbose(Strings.VerboseCreatingChildVDir(this.owaVersion, metabasePath));
								CreateVirtualDirectory createVirtualDirectory = new CreateVirtualDirectory();
								createVirtualDirectory.Name = this.owaVersion;
								createVirtualDirectory.Parent = metabasePath;
								createVirtualDirectory.CustomizedVDirProperties = OwaVirtualDirectoryHelper.GetVersionVDirProperties();
								createVirtualDirectory.LocalPath = (string)IisUtility.GetIisPropertyValue("Path", createVirtualDirectory.CustomizedVDirProperties);
								createVirtualDirectory.Initialize();
								createVirtualDirectory.Execute();
							}
						}
						OwaVirtualDirectoryHelper.CreateLegacyVDirs(metabasePath, true);
						OwaVirtualDirectoryHelper.CreateOwaCalendarVDir(metabasePath, VirtualDirectoryRole.Mailbox);
						if (ExchangeServiceVDirHelper.IsBackEndVirtualDirectory(adowaVirtualDirectory))
						{
							WebAppVirtualDirectoryHelper.UpdateMetabase(adowaVirtualDirectory, metabasePath, true);
						}
					}
					catch (COMException ex)
					{
						base.WriteError(new IISGeneralCOMException(ex.Message, ex.ErrorCode, ex), ErrorCategory.InvalidOperation, null);
					}
					if (adowaVirtualDirectory.ExchangeVersion.IsOlderThan(adowaVirtualDirectory.MaximumSupportedExchangeObjectVersion))
					{
						try
						{
							adowaVirtualDirectory.SetExchangeVersion(adowaVirtualDirectory.MaximumSupportedExchangeObjectVersion);
							base.DataSession.Save(adowaVirtualDirectory);
						}
						catch (DataSourceTransientException exception)
						{
							base.WriteError(exception, ErrorCategory.WriteError, null);
						}
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04003BF0 RID: 15344
		private ITopologyConfigurationSession configurationSession;

		// Token: 0x04003BF1 RID: 15345
		private string owaVersion;

		// Token: 0x04003BF2 RID: 15346
		private string owaCurrentFolder;

		// Token: 0x04003BF3 RID: 15347
		private string owaVersionFolder;
	}
}
