using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000524 RID: 1316
	internal sealed class PickupDirectory
	{
		// Token: 0x06003D61 RID: 15713 RVA: 0x00100115 File Offset: 0x000FE315
		public PickupDirectory(PickupType pickupType, IPickupSubmitHandler submitHandler)
		{
			this.pickupType = pickupType;
			this.submitHandler = submitHandler;
			this.eventLogger = new ExEventLog(ExTraceGlobals.PickupTracer.Category, TransportEventLog.GetEventSource());
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003D62 RID: 15714 RVA: 0x00100150 File Offset: 0x000FE350
		public PickupPerfCountersInstance PickupPerformanceCounter
		{
			get
			{
				return this.pickupPerfCounterInstance;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x00100158 File Offset: 0x000FE358
		public IPickupSubmitHandler SubmitHandler
		{
			get
			{
				return this.submitHandler;
			}
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x00100160 File Offset: 0x000FE360
		public static string GetRenamedFileName(string fileName, string newExtension)
		{
			if (fileName.Length < 4)
			{
				throw new InvalidOperationException("fileName length should be > 3, but it is " + fileName.Length);
			}
			string str = newExtension;
			string str2 = fileName.Substring(0, fileName.Length - 3);
			for (int i = 0; i < 3; i++)
			{
				string text = str2 + str;
				if (!File.Exists(text))
				{
					return text;
				}
				str = DateTime.UtcNow.Ticks.ToString() + "." + newExtension;
			}
			return str2 + Guid.NewGuid().ToString() + "." + newExtension;
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x00100204 File Offset: 0x000FE404
		public static void CreateDirectory(string path, FileSystemAccessRule[] securityRules)
		{
			DirectorySecurity directorySecurity = new DirectorySecurity();
			for (int i = 0; i < securityRules.Length; i++)
			{
				directorySecurity.AddAccessRule(securityRules[i]);
			}
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				directorySecurity.SetOwner(current.User);
			}
			directorySecurity.SetAccessRuleProtection(true, false);
			Directory.CreateDirectory(path, directorySecurity);
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x0010026C File Offset: 0x000FE46C
		public void Start()
		{
			lock (this.syncObj)
			{
				this.InternalStart();
			}
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x001002AC File Offset: 0x000FE4AC
		public void Stop()
		{
			lock (this.syncObj)
			{
				this.InternalStop();
			}
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x001002EC File Offset: 0x000FE4EC
		public void Reconfigure()
		{
			lock (this.syncObj)
			{
				this.InternalStop();
				this.InternalStart();
			}
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x00100334 File Offset: 0x000FE534
		private void InternalStart()
		{
			if (this.pickupPerfCounterInstance == null)
			{
				this.pickupPerfCounterInstance = PickupPerfCounters.GetInstance(this.pickupType.ToString());
			}
			LocalLongFullPath localLongFullPath = (this.pickupType == PickupType.Pickup) ? Components.Configuration.LocalServer.TransportServer.PickupDirectoryPath : Components.Configuration.LocalServer.TransportServer.ReplayDirectoryPath;
			if (null == localLongFullPath)
			{
				this.currentPath = null;
				return;
			}
			string pathName = localLongFullPath.PathName;
			bool flag = !pathName.Equals(this.currentPath, StringComparison.OrdinalIgnoreCase);
			this.currentPath = pathName;
			this.directoryPermissionOk = true;
			this.failedToCreateDirectory = false;
			if (this.CheckDirectory(pathName) && flag)
			{
				this.RenameAllTmpToEml();
			}
			if (this.pickupType == PickupType.Pickup)
			{
				if (flag)
				{
					this.WritePickupPathRegkey();
				}
				this.pickupFileMailer = new PickupFileMailer(this, pathName, this.eventLogger);
			}
			else
			{
				this.pickupFileMailer = new ReplayFileMailer(this, pathName, this.eventLogger);
			}
			this.directoryScanner = new DirectoryScanner(pathName, Components.Configuration.LocalServer.TransportServer.PickupDirectoryMaxMessagesPerMinute, "*.eml", new DirectoryScanner.FileFoundCallBack(this.pickupFileMailer.ProcessFile), new DirectoryScanner.CheckDirectoryCallBack(this.CheckDirectory));
			this.directoryScanner.Start();
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0010046D File Offset: 0x000FE66D
		private void InternalStop()
		{
			if (this.directoryScanner != null)
			{
				this.directoryScanner.Stop();
			}
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x00100484 File Offset: 0x000FE684
		private void WritePickupPathRegkey()
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Pickup"))
				{
					if (registryKey.GetValue("Path") != null && registryKey.GetValueKind("Path") != RegistryValueKind.String)
					{
						registryKey.DeleteValue("Path", false);
					}
					registryKey.SetValue("Path", this.currentPath, RegistryValueKind.String);
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.PickupTracer.TraceError<SecurityException, string>((long)this.GetHashCode(), "SecurityException {0} trying to add Pickup path {1} to registry.", ex, this.currentPath);
				this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_AccessErrorModifyingPickupRegkey, null, new object[]
				{
					"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Pickup",
					ex
				});
			}
			catch (UnauthorizedAccessException ex2)
			{
				ExTraceGlobals.PickupTracer.TraceError<UnauthorizedAccessException, string>((long)this.GetHashCode(), "SecurityException {0} trying to add Pickup path {1} to registry.", ex2, this.currentPath);
				this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_AccessErrorModifyingPickupRegkey, null, new object[]
				{
					"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Pickup",
					ex2
				});
			}
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x001005A4 File Offset: 0x000FE7A4
		private void RenameAllTmpToEml()
		{
			using (FileList fileList = new FileList(this.currentPath, "*.tmp"))
			{
				string text;
				ulong num;
				while (fileList.GetNextFile(out text, out num))
				{
					try
					{
						string renamedFileName = PickupDirectory.GetRenamedFileName(text, "eml");
						File.Move(text, renamedFileName);
					}
					catch (FileNotFoundException arg)
					{
						ExTraceGlobals.PickupTracer.TraceDebug<string, FileNotFoundException>((long)this.GetHashCode(), "Cannot find {0}, exception {1}, leaving file.", text, arg);
					}
					catch (IOException arg2)
					{
						ExTraceGlobals.PickupTracer.TraceDebug<string, IOException>((long)this.GetHashCode(), "Cannot rename {0}, exception {1}, leaving file.", text, arg2);
					}
					catch (UnauthorizedAccessException arg3)
					{
						ExTraceGlobals.PickupTracer.TraceDebug<string, UnauthorizedAccessException>((long)this.GetHashCode(), "Unauthorized to rename {0}, exception {1}", text, arg3);
						this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_NoPermissionToRenamePickupFile, this.currentPath, new object[]
						{
							this.currentPath
						});
					}
				}
			}
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x001006B0 File Offset: 0x000FE8B0
		private bool CheckDirectory(string fullDirectoryPath)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(fullDirectoryPath);
				if (!directoryInfo.Exists)
				{
					ExTraceGlobals.PickupTracer.TraceDebug<string>((long)this.GetHashCode(), "Directory {0} does not exist.", fullDirectoryPath);
					if (!this.failedToCreateDirectory)
					{
						this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_DirectoryDoesNotExist, null, new object[]
						{
							fullDirectoryPath
						});
						try
						{
							PickupDirectory.CreateDirectory(fullDirectoryPath, PickupDirectory.DirectoryAccessRules);
							this.directoryPermissionOk = true;
							return true;
						}
						catch (DirectoryNotFoundException ex)
						{
							this.failedToCreateDirectory = true;
							this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_FailedToCreatePickupDirectory, null, new object[]
							{
								fullDirectoryPath,
								ex
							});
							string notificationReason = string.Format("The Microsoft Exchange Transport service failed to create the Pickup directory: {0}. Pickup will not function until the directory is created. The detailed error is {1}.", fullDirectoryPath, ex);
							EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "FailedToCreatePickupDirectory", null, notificationReason, ResultSeverityLevel.Warning, false);
						}
						catch (UnauthorizedAccessException ex2)
						{
							this.failedToCreateDirectory = true;
							this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_FailedToCreatePickupDirectory, null, new object[]
							{
								fullDirectoryPath,
								ex2
							});
							string notificationReason2 = string.Format("The Microsoft Exchange Transport service failed to create the Pickup directory: {0}. Pickup will not function until the directory is created. The detailed error is {1}.", fullDirectoryPath, ex2);
							EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "FailedToCreatePickupDirectory", null, notificationReason2, ResultSeverityLevel.Warning, false);
						}
						catch (IOException ex3)
						{
							this.failedToCreateDirectory = true;
							this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_FailedToCreatePickupDirectory, null, new object[]
							{
								fullDirectoryPath,
								ex3
							});
							string notificationReason3 = string.Format("The Microsoft Exchange Transport service failed to create the Pickup directory: {0}. Pickup will not function until the directory is created. The detailed error is {1}.", fullDirectoryPath, ex3);
							EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "FailedToCreatePickupDirectory", null, notificationReason3, ResultSeverityLevel.Warning, false);
						}
					}
					return false;
				}
				this.failedToCreateDirectory = false;
				SecurityIdentifier securityIdentifier = null;
				using (WindowsIdentity current = WindowsIdentity.GetCurrent(false))
				{
					securityIdentifier = current.User;
				}
				if (securityIdentifier.IsWellKnown(WellKnownSidType.NetworkServiceSid))
				{
					DirectorySecurity accessControl = directoryInfo.GetAccessControl();
					FileSystemRights fileSystemRights = (FileSystemRights)0;
					FileSystemRights fileSystemRights2 = (FileSystemRights)0;
					foreach (object obj in accessControl.GetAccessRules(true, true, typeof(SecurityIdentifier)))
					{
						FileSystemAccessRule fileSystemAccessRule = (FileSystemAccessRule)obj;
						SecurityIdentifier left = fileSystemAccessRule.IdentityReference as SecurityIdentifier;
						if (left != null && left == securityIdentifier)
						{
							if (fileSystemAccessRule.AccessControlType == AccessControlType.Allow)
							{
								fileSystemRights2 |= fileSystemAccessRule.FileSystemRights;
							}
							else
							{
								fileSystemRights |= fileSystemAccessRule.FileSystemRights;
							}
						}
						ExTraceGlobals.PickupTracer.TraceDebug<string, FileSystemRights, IdentityReference>((long)this.GetHashCode(), "Rule {0} {1} access to {2}", (fileSystemAccessRule.AccessControlType == AccessControlType.Allow) ? "grants" : "denies", fileSystemAccessRule.FileSystemRights, fileSystemAccessRule.IdentityReference);
					}
					if ((fileSystemRights & (FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.ReadPermissions)) != (FileSystemRights)0 || (FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.ReadPermissions) != (fileSystemRights2 & (FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.ReadPermissions)))
					{
						if (this.directoryPermissionOk)
						{
							this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_NoDirectoryPermission, null, new object[]
							{
								fullDirectoryPath
							});
						}
						this.directoryPermissionOk = false;
						return false;
					}
				}
			}
			catch (UnauthorizedAccessException arg)
			{
				ExTraceGlobals.PickupTracer.TraceDebug<UnauthorizedAccessException>((long)this.GetHashCode(), "No permission to check permissions {0}", arg);
				if (this.directoryPermissionOk)
				{
					this.eventLogger.LogEvent(TransportEventLogConstants.Tuple_NoDirectoryPermission, null, new object[]
					{
						fullDirectoryPath
					});
				}
				this.directoryPermissionOk = false;
				return false;
			}
			this.directoryPermissionOk = true;
			return true;
		}

		// Token: 0x04001F3D RID: 7997
		public const string BadMailExtension = "bad";

		// Token: 0x04001F3E RID: 7998
		public const string PickupExtension = "eml";

		// Token: 0x04001F3F RID: 7999
		public const string PickupTempExtension = "tmp";

		// Token: 0x04001F40 RID: 8000
		public const string PickupPoisonExtension = "psn";

		// Token: 0x04001F41 RID: 8001
		private const string PickupRegKeyLocation = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Pickup";

		// Token: 0x04001F42 RID: 8002
		private const string PickupRegKeyLocationForEventLog = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Pickup";

		// Token: 0x04001F43 RID: 8003
		private const string PickupPathRegistryName = "Path";

		// Token: 0x04001F44 RID: 8004
		private static readonly FileSystemAccessRule[] DirectoryAccessRules = new FileSystemAccessRule[]
		{
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null), FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.DeleteSubdirectoriesAndFiles | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.ReadPermissions, InheritanceFlags.ObjectInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow)
		};

		// Token: 0x04001F45 RID: 8005
		private ExEventLog eventLogger;

		// Token: 0x04001F46 RID: 8006
		private PickupFileMailer pickupFileMailer;

		// Token: 0x04001F47 RID: 8007
		private bool failedToCreateDirectory;

		// Token: 0x04001F48 RID: 8008
		private bool directoryPermissionOk;

		// Token: 0x04001F49 RID: 8009
		private PickupPerfCountersInstance pickupPerfCounterInstance;

		// Token: 0x04001F4A RID: 8010
		private PickupType pickupType;

		// Token: 0x04001F4B RID: 8011
		private DirectoryScanner directoryScanner;

		// Token: 0x04001F4C RID: 8012
		private string currentPath;

		// Token: 0x04001F4D RID: 8013
		private object syncObj = new object();

		// Token: 0x04001F4E RID: 8014
		private IPickupSubmitHandler submitHandler;
	}
}
