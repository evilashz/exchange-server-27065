using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x020000F1 RID: 241
	internal sealed class WmiWrapper
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x0001D5B3 File Offset: 0x0001B7B3
		private WmiWrapper()
		{
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001D5BC File Offset: 0x0001B7BC
		public static bool IsPathOnFixedOrNetworkDrive(string computerName, string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			LocalLongFullPath localLongFullPath = null;
			try
			{
				localLongFullPath = LocalLongFullPath.Parse(pathName);
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			pathName = localLongFullPath.PathName;
			string driveName = localLongFullPath.DriveName;
			bool result;
			using (ManagementObject driveObject = WmiWrapper.GetDriveObject(computerName, driveName))
			{
				result = (driveObject != null && ((uint)driveObject["DriveType"] == 3U || (uint)driveObject["DriveType"] == 4U));
			}
			return result;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001D694 File Offset: 0x0001B894
		public static bool IsPathOnFixedDrive(string computerName, string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			LocalLongFullPath localLongFullPath = null;
			try
			{
				localLongFullPath = LocalLongFullPath.Parse(pathName);
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			pathName = localLongFullPath.PathName;
			string driveName = localLongFullPath.DriveName;
			bool result;
			using (ManagementObject driveObject = WmiWrapper.GetDriveObject(computerName, driveName))
			{
				result = (driveObject != null && (uint)driveObject["DriveType"] == 3U);
			}
			return result;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001D754 File Offset: 0x0001B954
		public static bool IsFileExistingInPath(string computerName, string pathName, WmiWrapper.FileFilter predicate)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			try
			{
				pathName = LocalLongFullPath.Parse(pathName).PathName;
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			bool result = false;
			using (ManagementObjectCollection allFileObjectsInDirectory = WmiWrapper.GetAllFileObjectsInDirectory(computerName, pathName))
			{
				if (allFileObjectsInDirectory != null)
				{
					foreach (ManagementBaseObject managementBaseObject in allFileObjectsInDirectory)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						using (managementObject)
						{
							if (predicate(managementObject["FileName"].ToString(), managementObject["Extension"].ToString()))
							{
								result = true;
								break;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001D878 File Offset: 0x0001BA78
		public static bool IsFileExisting(string computerName, string fileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fileName))
			{
				result = (fileObject != null);
			}
			return result;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		public static string GetUniqueFileName(string computerName, string prefix, string postfix, int maxIteration)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			string text = prefix + postfix;
			string result = string.Empty;
			if (WmiWrapper.IsFileExisting(computerName, text))
			{
				for (int i = 1; i <= maxIteration; i++)
				{
					text = prefix + i.ToString() + postfix;
					if (!WmiWrapper.IsFileExisting(computerName, text))
					{
						result = text;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001D964 File Offset: 0x0001BB64
		public static bool IsDirectoryExisting(string computerName, string dirName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject directoryObject = WmiWrapper.GetDirectoryObject(computerName, dirName))
			{
				result = (directoryObject != null);
			}
			return result;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
		public static bool IsServiceRunning(string computerName, string serviceName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result = false;
			using (ManagementObject serviceObject = WmiWrapper.GetServiceObject(computerName, serviceName))
			{
				if (serviceObject != null && serviceObject["State"].Equals("Running"))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001DA50 File Offset: 0x0001BC50
		public static bool CopyFile(string computerName, string fromFileName, string toFileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fromFileName))
			{
				bool flag = false;
				try
				{
					if (fileObject != null && (uint)fileObject.InvokeMethod("Copy", new object[]
					{
						toFileName
					}) == 0U)
					{
						flag = true;
					}
				}
				catch (COMException e)
				{
					throw new WmiException(e, computerName);
				}
				catch (UnauthorizedAccessException e2)
				{
					throw new WmiException(e2, computerName);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		public static bool RenameFile(string computerName, string fromFileName, string toFileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fromFileName))
			{
				bool flag = false;
				try
				{
					if (fileObject != null && (uint)fileObject.InvokeMethod("Rename", new object[]
					{
						toFileName
					}) == 0U)
					{
						flag = true;
					}
				}
				catch (COMException e)
				{
					throw new WmiException(e, computerName);
				}
				catch (UnauthorizedAccessException e2)
				{
					throw new WmiException(e2, computerName);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001DBC8 File Offset: 0x0001BDC8
		public static bool DeleteFile(string computerName, string fileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fileName))
			{
				bool flag = false;
				try
				{
					if (fileObject == null || (uint)fileObject.InvokeMethod("Delete", null) == 0U)
					{
						flag = true;
					}
				}
				catch (COMException e)
				{
					throw new WmiException(e, computerName);
				}
				catch (UnauthorizedAccessException e2)
				{
					throw new WmiException(e2, computerName);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001DC74 File Offset: 0x0001BE74
		public static bool CopyFileIfExists(string computerName, string fromFileName, string toFileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fromFileName))
			{
				try
				{
					result = (fileObject == null || 0U == (uint)fileObject.InvokeMethod("Copy", new object[]
					{
						toFileName
					}));
				}
				catch (COMException e)
				{
					throw new WmiException(e, computerName);
				}
				catch (UnauthorizedAccessException e2)
				{
					throw new WmiException(e2, computerName);
				}
			}
			return result;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public static bool DeleteFileIfExists(string computerName, string fileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fileName))
			{
				try
				{
					result = (fileObject == null || 0U == (uint)fileObject.InvokeMethod("Delete", null));
				}
				catch (COMException e)
				{
					throw new WmiException(e, computerName);
				}
				catch (UnauthorizedAccessException e2)
				{
					throw new WmiException(e2, computerName);
				}
			}
			return result;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001DDD4 File Offset: 0x0001BFD4
		public static bool MoveFile(string computerName, string fromFilePath, string toFilePath)
		{
			if (string.IsNullOrEmpty(fromFilePath))
			{
				throw new ArgumentNullException("fromFilePath");
			}
			if (string.IsNullOrEmpty(toFilePath))
			{
				throw new ArgumentNullException("toFilePath");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			LocalLongFullPath localLongFullPath = null;
			LocalLongFullPath localLongFullPath2 = null;
			try
			{
				localLongFullPath = LocalLongFullPath.Parse(fromFilePath);
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			try
			{
				localLongFullPath2 = LocalLongFullPath.Parse(toFilePath);
			}
			catch (ArgumentException e2)
			{
				throw new WmiException(e2, computerName);
			}
			fromFilePath = localLongFullPath.PathName;
			toFilePath = localLongFullPath2.PathName;
			string driveName = localLongFullPath.DriveName;
			string driveName2 = localLongFullPath2.DriveName;
			bool result;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fromFilePath))
			{
				bool flag = true;
				if (fileObject != null)
				{
					try
					{
						if (string.Compare(driveName, driveName2, true) == 0)
						{
							flag = ((uint)fileObject.InvokeMethod("Rename", new object[]
							{
								toFilePath
							}) == 0U);
						}
						else if ((uint)fileObject.InvokeMethod("Copy", new object[]
						{
							toFilePath
						}) != 0U)
						{
							flag = false;
						}
						else
						{
							fileObject.InvokeMethod("Delete", null);
						}
					}
					catch (COMException e3)
					{
						throw new WmiException(e3, computerName);
					}
					catch (UnauthorizedAccessException e4)
					{
						throw new WmiException(e4, computerName);
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001DF68 File Offset: 0x0001C168
		public static bool DeleteFilesInDirectory(string computerName, string folderName, WmiWrapper.FileFilter filterDelegate)
		{
			return WmiWrapper.DeleteFilesInDirectory(computerName, folderName, filterDelegate, null);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001DF74 File Offset: 0x0001C174
		public static bool DeleteFilesInDirectory(string computerName, string folderName, WmiWrapper.FileFilter filterDelegate, WmiWrapper.ReportFileOperationProgress reportProgress)
		{
			if (folderName == null)
			{
				throw new ArgumentNullException("folderName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			bool result = true;
			using (ManagementObjectCollection allFileObjectsInDirectory = WmiWrapper.GetAllFileObjectsInDirectory(computerName, folderName))
			{
				if (allFileObjectsInDirectory != null)
				{
					List<ManagementObject> list = new List<ManagementObject>();
					ulong num = 0UL;
					foreach (ManagementBaseObject managementBaseObject in allFileObjectsInDirectory)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (filterDelegate(managementObject["FileName"].ToString(), managementObject["Extension"].ToString()))
						{
							ulong num2 = (ulong)managementObject["FileSize"];
							num += ((num2 == 0UL) ? 1UL : num2);
							list.Add(managementObject);
						}
						else
						{
							managementObject.Dispose();
						}
					}
					ulong num3 = 0UL;
					foreach (ManagementObject managementObject2 in list)
					{
						using (managementObject2)
						{
							string fileName = null;
							try
							{
								fileName = LocalLongFullPath.ParseFromPathNameAndFileName(folderName, managementObject2["FileName"] + "." + managementObject2["Extension"]).PathName;
							}
							catch (ArgumentException e)
							{
								throw new WmiException(e, computerName);
							}
							if (!WmiWrapper.DeleteFile(computerName, fileName))
							{
								result = false;
							}
							if (reportProgress != null)
							{
								ulong num4 = (ulong)managementObject2["FileSize"];
								num3 += ((num4 == 0UL) ? 1UL : num4);
								reportProgress(num, num3);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		public static bool CopyFilesInDirectory(string computerName, string fromPath, string toPath, WmiWrapper.FileFilter filterDelegate)
		{
			return WmiWrapper.CopyFilesInDirectory(computerName, fromPath, toPath, filterDelegate, null);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001E1BC File Offset: 0x0001C3BC
		public static bool CopyFilesInDirectory(string computerName, string fromPath, string toPath, WmiWrapper.FileFilter filterDelegate, WmiWrapper.ReportFileOperationProgress reportProgress)
		{
			if (fromPath == null)
			{
				throw new ArgumentNullException("fromPath");
			}
			if (toPath == null)
			{
				throw new ArgumentNullException("toPath");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			LocalLongFullPath localLongFullPath = null;
			LocalLongFullPath localLongFullPath2 = null;
			try
			{
				localLongFullPath = LocalLongFullPath.Parse(fromPath);
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			try
			{
				localLongFullPath2 = LocalLongFullPath.Parse(toPath);
			}
			catch (ArgumentException e2)
			{
				throw new WmiException(e2, computerName);
			}
			fromPath = localLongFullPath.PathName;
			toPath = localLongFullPath2.PathName;
			string driveName = localLongFullPath.DriveName;
			string driveName2 = localLongFullPath2.DriveName;
			bool flag = false;
			bool result = true;
			using (ManagementObjectCollection allFileObjectsInDirectory = WmiWrapper.GetAllFileObjectsInDirectory(computerName, fromPath))
			{
				if (allFileObjectsInDirectory != null)
				{
					List<ManagementObject> list = new List<ManagementObject>();
					ulong num = 0UL;
					foreach (ManagementBaseObject managementBaseObject in allFileObjectsInDirectory)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (filterDelegate(managementObject["FileName"].ToString(), managementObject["Extension"].ToString()))
						{
							ulong num2 = (ulong)managementObject["FileSize"];
							num += ((num2 == 0UL) ? 1UL : num2);
							list.Add(managementObject);
						}
						else
						{
							managementObject.Dispose();
						}
					}
					ulong num3 = 0UL;
					ulong num4 = (ulong)WmiWrapper.GetDriveObject(computerName, driveName2)["FreeSpace"];
					if (num4 >= num)
					{
						List<string> list2 = new List<string>();
						foreach (ManagementObject managementObject2 in list)
						{
							using (managementObject2)
							{
								string text = null;
								try
								{
									text = LocalLongFullPath.ParseFromPathNameAndFileName(toPath, managementObject2["FileName"] + "." + managementObject2["Extension"]).PathName;
								}
								catch (ArgumentException e3)
								{
									throw new WmiException(e3, computerName);
								}
								try
								{
									uint num5 = (uint)managementObject2.InvokeMethod("Copy", new object[]
									{
										text
									});
									if (num5 != 0U)
									{
										flag = true;
										break;
									}
								}
								catch (COMException e4)
								{
									throw new WmiException(e4, computerName);
								}
								catch (UnauthorizedAccessException e5)
								{
									throw new WmiException(e5, computerName);
								}
								list2.Add(text);
								if (reportProgress != null)
								{
									ulong num6 = (ulong)managementObject2["FileSize"];
									num3 += ((num6 == 0UL) ? 1UL : num6);
									reportProgress(num, num3);
								}
							}
						}
						if (flag)
						{
							foreach (string fileName in list2)
							{
								WmiWrapper.DeleteFile(computerName, fileName);
							}
							result = false;
						}
					}
					else
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001E584 File Offset: 0x0001C784
		public static uint CreateDirectory(string computerName, string pathName)
		{
			uint num = uint.MaxValue;
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			try
			{
				pathName = LocalLongFullPath.Parse(pathName).PathName;
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			try
			{
				ManagementClass managementClass = WmiWrapper.GetManagementClass(computerName, "Win32_Process");
				ManagementBaseObject methodParameters = managementClass.GetMethodParameters("Create");
				methodParameters["CommandLine"] = "cmd.exe /C md \"" + pathName + "\"";
				using (ManagementBaseObject managementBaseObject = managementClass.InvokeMethod("Create", methodParameters, null))
				{
					num = (uint)managementBaseObject["returnValue"];
				}
				if (num == 0U)
				{
					int num2 = 0;
					while (!WmiWrapper.IsDirectoryExisting(computerName, pathName) && ++num2 <= 15)
					{
						Thread.Sleep(1000);
					}
				}
			}
			catch (COMException e2)
			{
				throw new WmiException(e2, computerName);
			}
			return num;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001E6C0 File Offset: 0x0001C8C0
		public static bool RemoveDirectory(string computerName, string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			try
			{
				pathName = LocalLongFullPath.Parse(pathName).PathName;
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			bool result;
			using (ManagementObject directoryObject = WmiWrapper.GetDirectoryObject(computerName, pathName))
			{
				bool flag = false;
				try
				{
					if (directoryObject == null || (uint)directoryObject.InvokeMethod("Delete", null) == 0U)
					{
						flag = true;
					}
				}
				catch (COMException e2)
				{
					throw new WmiException(e2, computerName);
				}
				catch (UnauthorizedAccessException e3)
				{
					throw new WmiException(e3, computerName);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001E7A8 File Offset: 0x0001C9A8
		public static uint ChangeSecurityPermissions(string computerName, string pathName, FileSystemAccessRule[] directoryACEs)
		{
			uint result = uint.MaxValue;
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			try
			{
				pathName = LocalLongFullPath.Parse(pathName).PathName;
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			try
			{
				if (WmiWrapper.IsDirectoryExisting(computerName, pathName))
				{
					ManagementClass value = WmiWrapper.CreateSecurityDescriptor(computerName, directoryACEs);
					ManagementObject directoryObject = WmiWrapper.GetDirectoryObject(computerName, pathName);
					if (directoryObject != null)
					{
						ManagementBaseObject methodParameters = directoryObject.GetMethodParameters("ChangeSecurityPermissions");
						methodParameters["Option"] = "4";
						methodParameters["SecurityDescriptor"] = value;
						using (ManagementBaseObject managementBaseObject = directoryObject.InvokeMethod("ChangeSecurityPermissions", methodParameters, null))
						{
							result = (uint)managementBaseObject["returnValue"];
						}
					}
				}
			}
			catch (COMException e2)
			{
				throw new WmiException(e2, computerName);
			}
			return result;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
		public static ulong GetTotalSizeOfFilesInDirectory(string computerName, string pathName, WmiWrapper.FileFilter filterDelegate)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ulong num = 0UL;
			using (ManagementObjectCollection allFileObjectsInDirectory = WmiWrapper.GetAllFileObjectsInDirectory(computerName, pathName))
			{
				if (allFileObjectsInDirectory != null)
				{
					foreach (ManagementBaseObject managementBaseObject in allFileObjectsInDirectory)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (filterDelegate(managementObject["FileName"].ToString(), managementObject["Extension"].ToString()))
						{
							num += (ulong)managementObject["FileSize"];
						}
						managementObject.Dispose();
					}
				}
			}
			return num;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001E9B4 File Offset: 0x0001CBB4
		public static ulong GetFileSize(string computerName, string fileName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ulong result = 0UL;
			using (ManagementObject fileObject = WmiWrapper.GetFileObject(computerName, fileName))
			{
				if (fileObject != null)
				{
					result = (ulong)fileObject["filesize"];
				}
			}
			return result;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001EA30 File Offset: 0x0001CC30
		public static ulong GetFreeSpaceOnDrive(string computerName, string driveName)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ulong result = 0UL;
			using (ManagementObject driveObject = WmiWrapper.GetDriveObject(computerName, driveName))
			{
				if (driveObject != null)
				{
					result = (ulong)driveObject["freespace"];
				}
			}
			return result;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001EAAC File Offset: 0x0001CCAC
		internal static ManagementObject GetFileObject(string computerName, string fileName)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("fileName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			fileName = fileName.Replace(Path.DirectorySeparatorChar.ToString(), "\\\\");
			if (Path.AltDirectorySeparatorChar != Path.DirectorySeparatorChar)
			{
				fileName = fileName.Replace(Path.AltDirectorySeparatorChar.ToString(), "\\\\");
			}
			return WmiWrapper.GetManagementObject(computerName, "CIM_DataFile.Name=\"" + fileName + "\"");
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001EB68 File Offset: 0x0001CD68
		internal static ManagementObject GetDirectoryObject(string computerName, string dirName)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(dirName))
			{
				throw new ArgumentNullException("dirName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			if (dirName[dirName.Length - 1] == Path.DirectorySeparatorChar || dirName[dirName.Length - 1] == Path.AltDirectorySeparatorChar)
			{
				dirName = dirName.Substring(0, dirName.Length - 1);
				if (dirName.EndsWith(Path.VolumeSeparatorChar.ToString()))
				{
					dirName += Path.DirectorySeparatorChar;
				}
			}
			dirName = dirName.Replace(Path.DirectorySeparatorChar.ToString(), "\\\\");
			if (Path.AltDirectorySeparatorChar != Path.DirectorySeparatorChar)
			{
				dirName = dirName.Replace(Path.AltDirectorySeparatorChar.ToString(), "\\\\");
			}
			return WmiWrapper.GetManagementObject(computerName, "Win32_Directory.Name=\"" + dirName + "\"");
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001EC84 File Offset: 0x0001CE84
		internal static ManagementObject GetServiceObject(string computerName, string serviceName)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(serviceName))
			{
				throw new ArgumentNullException("serviceName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			return WmiWrapper.GetManagementObject(computerName, "Win32_Service.Name=\"" + serviceName + "\"");
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001ED00 File Offset: 0x0001CF00
		internal static ManagementObject GetDriveObject(string computerName, string driveName)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(driveName))
			{
				throw new ArgumentNullException("driveName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			return WmiWrapper.GetManagementObject(computerName, "Win32_logicaldisk.deviceid=\"" + driveName + "\"");
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001ED7C File Offset: 0x0001CF7C
		internal static ManagementObject GetProcessObject(string computerName, string processName)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(processName))
			{
				throw new ArgumentNullException("processName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ManagementClass managementClass = WmiWrapper.GetManagementClass(computerName, "Win32_Process");
			foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
			{
				string value = ((string)managementBaseObject["Name"]) ?? string.Empty;
				if (processName.Equals(value, StringComparison.InvariantCultureIgnoreCase))
				{
					return managementBaseObject as ManagementObject;
				}
			}
			return null;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001EE5C File Offset: 0x0001D05C
		internal static ManagementObject GetProcessObject(string computerName, uint processId)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ManagementClass managementClass = WmiWrapper.GetManagementClass(computerName, "Win32_Process");
			foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
			{
				uint num = (uint)managementBaseObject["ProcessId"];
				if (processId == num)
				{
					return managementBaseObject as ManagementObject;
				}
			}
			return null;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001EF18 File Offset: 0x0001D118
		private static ManagementObject GetManagementObject(string computerName, string managementPath)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(managementPath))
			{
				throw new ArgumentNullException("managementPath");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ManagementObject managementObject = null;
			TaskLogger.Trace("Parameter computerName: {0}, managementPath: {1}", new object[]
			{
				computerName,
				managementPath
			});
			try
			{
				managementObject = new ManagementObject(WmiWrapper.GetManagementScope(computerName), new ManagementPath(managementPath), null);
				managementObject.Get();
			}
			catch (ManagementException ex)
			{
				if (ex.ErrorCode != ManagementStatus.NotFound)
				{
					throw new WmiException(ex, computerName);
				}
				managementObject = null;
			}
			catch (COMException e)
			{
				throw new WmiException(e, computerName);
			}
			catch (UnauthorizedAccessException e2)
			{
				throw new WmiException(e2, computerName);
			}
			return managementObject;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001F010 File Offset: 0x0001D210
		private static bool IsLocalHost(string computerName)
		{
			return string.Equals(computerName, "localhost", StringComparison.OrdinalIgnoreCase) || string.Equals(computerName, NativeHelpers.GetLocalComputerFqdn(true), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001F034 File Offset: 0x0001D234
		private static ManagementScope GetManagementScope(string computerName)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (!string.Equals(computerName, "localhost", StringComparison.OrdinalIgnoreCase) && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ConnectionOptions connectionOptions = new ConnectionOptions();
			if (!WmiWrapper.IsLocalHost(computerName))
			{
				connectionOptions.Authority = string.Format("Kerberos:host/{0}", computerName);
			}
			return new ManagementScope("\\\\" + computerName + "\\root\\cimv2", connectionOptions);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		private static ManagementClass GetManagementClass(string computerName, string managementClass)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (string.IsNullOrEmpty(managementClass))
			{
				throw new ArgumentNullException("managementClass");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ManagementClass result = null;
			try
			{
				result = new ManagementClass(WmiWrapper.GetManagementScope(computerName), new ManagementPath(managementClass), null);
			}
			catch (ManagementException ex)
			{
				if (ex.ErrorCode != ManagementStatus.NotFound)
				{
					throw new WmiException(ex, computerName);
				}
				result = null;
			}
			catch (COMException e)
			{
				throw new WmiException(e, computerName);
			}
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001F178 File Offset: 0x0001D378
		private static ManagementObjectCollection GetAllFileObjectsInDirectory(string computerName, string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				throw new ArgumentNullException("pathName");
			}
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			try
			{
				pathName = LocalLongFullPath.Parse(pathName).PathName;
			}
			catch (ArgumentException e)
			{
				throw new WmiException(e, computerName);
			}
			string query = "ASSOCIATORS OF {Win32_Directory.Name=\"" + pathName.Replace("\\", "\\\\") + "\"} Where ResultClass = CIM_DataFile";
			WqlObjectQuery query2 = new WqlObjectQuery(query);
			ManagementObjectCollection managementObjectCollection;
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(WmiWrapper.GetManagementScope(computerName), query2))
			{
				try
				{
					managementObjectCollection = managementObjectSearcher.Get();
					if (managementObjectCollection.Count == 0)
					{
						managementObjectCollection.Dispose();
						managementObjectCollection = null;
					}
				}
				catch (ManagementException ex)
				{
					if (ex.ErrorCode != ManagementStatus.NotFound)
					{
						throw new WmiException(ex, computerName);
					}
					managementObjectCollection = null;
				}
				catch (COMException e2)
				{
					throw new WmiException(e2, computerName);
				}
				catch (UnauthorizedAccessException e3)
				{
					throw new WmiException(e3, computerName);
				}
			}
			return managementObjectCollection;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		private static ManagementClass CreateSecurityDescriptor(string computerName, FileSystemAccessRule[] securityPermissionAccessRules)
		{
			if (string.Compare(computerName, "localhost", true) != 0 && !computerName.Contains("."))
			{
				throw new ArgumentException(Strings.ErrorNameNotFQDN, "computerName");
			}
			ManagementClass managementClass = WmiWrapper.GetManagementClass(computerName, "Win32_SecurityDescriptor");
			object[] array = new object[securityPermissionAccessRules.Length];
			for (int i = 0; i < securityPermissionAccessRules.Length; i++)
			{
				SecurityIdentifier securityIdentifier = (SecurityIdentifier)securityPermissionAccessRules[i].IdentityReference;
				byte[] array2 = new byte[securityIdentifier.BinaryLength];
				securityIdentifier.GetBinaryForm(array2, 0);
				ManagementClass managementClass2 = WmiWrapper.GetManagementClass(computerName, "Win32_Trustee");
				managementClass2["SID"] = array2;
				ManagementClass managementClass3 = WmiWrapper.GetManagementClass(computerName, "Win32_ACE");
				managementClass3["Trustee"] = managementClass2;
				managementClass3["AccessMask"] = WmiWrapper.CreateAccessMask(securityPermissionAccessRules[i].FileSystemRights);
				managementClass3["AceFlags"] = WmiWrapper.CreateInheritance(securityPermissionAccessRules[i].InheritanceFlags);
				managementClass3["AceType"] = WmiWrapper.CreateAceType(securityPermissionAccessRules[i].AccessControlType);
				array[i] = managementClass3;
			}
			managementClass["ControlFlags"] = 4100;
			managementClass["DACL"] = array;
			return managementClass;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001F3F8 File Offset: 0x0001D5F8
		private static uint CreateAccessMask(FileSystemRights permissions)
		{
			uint num = 0U;
			if ((permissions & FileSystemRights.AppendData) > (FileSystemRights)0)
			{
				num |= 4U;
			}
			if ((permissions & FileSystemRights.ChangePermissions) > (FileSystemRights)0)
			{
				num |= 262144U;
			}
			if ((permissions & FileSystemRights.WriteData) > (FileSystemRights)0)
			{
				num |= 2U;
			}
			if ((permissions & FileSystemRights.Delete) > (FileSystemRights)0)
			{
				num |= 65536U;
			}
			if ((permissions & FileSystemRights.DeleteSubdirectoriesAndFiles) > (FileSystemRights)0)
			{
				num |= 64U;
			}
			if ((permissions & FileSystemRights.ExecuteFile) > (FileSystemRights)0)
			{
				num |= 32U;
			}
			if ((permissions & FileSystemRights.ReadData) > (FileSystemRights)0)
			{
				num |= 1U;
			}
			if ((permissions & FileSystemRights.ReadAttributes) > (FileSystemRights)0)
			{
				num |= 128U;
			}
			if ((permissions & FileSystemRights.ReadExtendedAttributes) > (FileSystemRights)0)
			{
				num |= 8U;
			}
			if ((permissions & FileSystemRights.ReadPermissions) > (FileSystemRights)0)
			{
				num |= 131072U;
			}
			if ((permissions & FileSystemRights.Synchronize) > (FileSystemRights)0)
			{
				num |= 1048576U;
			}
			if ((permissions & FileSystemRights.TakeOwnership) > (FileSystemRights)0)
			{
				num |= 524288U;
			}
			if ((permissions & FileSystemRights.WriteAttributes) > (FileSystemRights)0)
			{
				num |= 256U;
			}
			if ((permissions & FileSystemRights.WriteExtendedAttributes) > (FileSystemRights)0)
			{
				num |= 16U;
			}
			if ((num & 6U) > 0U)
			{
				num |= 1048576U;
			}
			return num;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001F4E0 File Offset: 0x0001D6E0
		private static uint CreateInheritance(InheritanceFlags inheritance)
		{
			uint num = 0U;
			if ((inheritance & InheritanceFlags.ObjectInherit) > InheritanceFlags.None)
			{
				num |= 1U;
			}
			if ((inheritance & InheritanceFlags.ContainerInherit) > InheritanceFlags.None)
			{
				num |= 2U;
			}
			return num;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001F504 File Offset: 0x0001D704
		private static uint CreateAceType(AccessControlType aceType)
		{
			uint result = 0U;
			if (aceType == AccessControlType.Allow)
			{
				result = 0U;
			}
			else if (aceType == AccessControlType.Deny)
			{
				result = 1U;
			}
			return result;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001F524 File Offset: 0x0001D724
		public static string TryGetSystemDrive(string computerFqdn)
		{
			string result;
			try
			{
				string text = null;
				ManagementScope managementScope = WmiWrapper.GetManagementScope(computerFqdn);
				managementScope.Connect();
				SelectQuery query = new SelectQuery("select SystemDrive from Win32_OperatingSystem");
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(managementScope, query))
				{
					using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
					{
						using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								ManagementObject managementObject = (ManagementObject)enumerator.Current;
								text = managementObject.GetPropertyValue("SystemDrive").ToString();
							}
						}
					}
				}
				result = text;
			}
			catch (COMException ex)
			{
				TaskLogger.Trace("Unable to get system drive on host {0}: host not found or rpc server is down. Specific error: {1}", new object[]
				{
					computerFqdn,
					ex
				});
				result = null;
			}
			catch (UnauthorizedAccessException ex2)
			{
				TaskLogger.Trace("Unable to get system drive on host {0}: wrong credentials, no permission to login. Specific error: {1}", new object[]
				{
					computerFqdn,
					ex2
				});
				result = null;
			}
			return result;
		}

		// Token: 0x0400035A RID: 858
		private const int fixedDriveType = 3;

		// Token: 0x0400035B RID: 859
		private const int networkDriveType = 4;

		// Token: 0x0400035C RID: 860
		private static readonly Guid componentGuid = new Guid("12a0a39f-bef6-4401-ba08-1df79c4de030");

		// Token: 0x020000F2 RID: 242
		[Flags]
		internal enum WMI_AccessMask
		{
			// Token: 0x0400035E RID: 862
			FILE_LIST_DIRECTORY = 1,
			// Token: 0x0400035F RID: 863
			FILE_ADD_FILE = 2,
			// Token: 0x04000360 RID: 864
			FILE_ADD_SUBDIRECTORY = 4,
			// Token: 0x04000361 RID: 865
			FILE_READ_EA = 8,
			// Token: 0x04000362 RID: 866
			FILE_WRITE_EA = 16,
			// Token: 0x04000363 RID: 867
			FILE_TRAVERSE = 32,
			// Token: 0x04000364 RID: 868
			FILE_DELETE_CHILD = 64,
			// Token: 0x04000365 RID: 869
			FILE_READ_ATTRIBUTES = 128,
			// Token: 0x04000366 RID: 870
			FILE_WRITE_ATTRIBUTES = 256,
			// Token: 0x04000367 RID: 871
			DELETE = 65536,
			// Token: 0x04000368 RID: 872
			READ_CONTROL = 131072,
			// Token: 0x04000369 RID: 873
			WRITE_DAC = 262144,
			// Token: 0x0400036A RID: 874
			WRITE_OWNER = 524288,
			// Token: 0x0400036B RID: 875
			SYNCHRONIZE = 1048576
		}

		// Token: 0x020000F3 RID: 243
		[Flags]
		internal enum WMI_Inheritance
		{
			// Token: 0x0400036D RID: 877
			OBJECT_INHERIT_ACE = 1,
			// Token: 0x0400036E RID: 878
			CONTAINER_INHERIT_ACE = 2,
			// Token: 0x0400036F RID: 879
			NO_PROPAGATE_INHERIT_ACE = 4,
			// Token: 0x04000370 RID: 880
			INHERIT_ONLY_ACE = 8,
			// Token: 0x04000371 RID: 881
			INHERITED_ACE = 16
		}

		// Token: 0x020000F4 RID: 244
		[Flags]
		internal enum WMI_AceType
		{
			// Token: 0x04000373 RID: 883
			ACCESS_ALLOWED = 0,
			// Token: 0x04000374 RID: 884
			ACCESS_DENIED = 1,
			// Token: 0x04000375 RID: 885
			AUDIT = 2
		}

		// Token: 0x020000F5 RID: 245
		// (Invoke) Token: 0x0600074A RID: 1866
		public delegate bool FileFilter(string fileName, string fileExt);

		// Token: 0x020000F6 RID: 246
		// (Invoke) Token: 0x0600074E RID: 1870
		public delegate void ReportFileOperationProgress(ulong totalSize, ulong completedSize);
	}
}
