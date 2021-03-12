using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.Setup;
using Microsoft.Exchange.Setup.AcquireLanguagePack;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.CommonBase
{
	// Token: 0x02000006 RID: 6
	internal static class SetupHelper
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000347B File Offset: 0x0000167B
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00003482 File Offset: 0x00001682
		public static SortedList<string, string> FilesToCopy { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000348A File Offset: 0x0000168A
		public static string WindowsDir
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.Windows);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003494 File Offset: 0x00001694
		public static bool IsSourceRemote
		{
			get
			{
				int driveType = SetupHelper.GetDriveType(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				return driveType != 3 || driveType != 5;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000034C1 File Offset: 0x000016C1
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000034C8 File Offset: 0x000016C8
		internal static bool IsDatacenter { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000034D0 File Offset: 0x000016D0
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000034D7 File Offset: 0x000016D7
		internal static bool TreatPreReqErrorsAsWarnings { get; set; }

		// Token: 0x06000047 RID: 71 RVA: 0x000034DF File Offset: 0x000016DF
		public static void CopyFiles(string srcDir, string dstDir, bool overwrite)
		{
			SetupHelper.CopyFiles(srcDir, dstDir, overwrite, null);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000034EA File Offset: 0x000016EA
		public static void CopyFiles(string srcDir, string dstDir, bool overwrite, IList<string> exceptionList)
		{
			SetupHelper.CopyFiles(srcDir, dstDir, overwrite, true, null, exceptionList);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000034F7 File Offset: 0x000016F7
		public static void CopyFiles(string srcDir, string dstDir, bool overwrite, bool recursive, string fileNamePattern)
		{
			SetupHelper.CopyFiles(srcDir, dstDir, overwrite, recursive, fileNamePattern, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003508 File Offset: 0x00001708
		public static void CopyFiles(string srcDir, string dstDir, bool overwrite, bool recursive, string fileNamePattern, IList<string> exceptionList)
		{
			try
			{
				SetupHelper.CheckDiskSpace(srcDir, dstDir, overwrite, recursive, fileNamePattern, exceptionList);
				if (!Directory.Exists(dstDir))
				{
					Directory.CreateDirectory(dstDir);
				}
				foreach (KeyValuePair<string, string> keyValuePair in SetupHelper.FilesToCopy)
				{
					FileInfo fileInfo = new FileInfo(keyValuePair.Value);
					if (!Directory.Exists(fileInfo.DirectoryName))
					{
						Directory.CreateDirectory(fileInfo.DirectoryName);
					}
					try
					{
						File.Copy(keyValuePair.Key, keyValuePair.Value, overwrite);
					}
					catch (Exception ex)
					{
						if (ex is IOException || ex is ArgumentException || ex is NotSupportedException)
						{
							throw new FileCopyException(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				throw new FileCopyException(srcDir, dstDir);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035F8 File Offset: 0x000017F8
		public static void CopyFiles(IEnumerable<string> fileList, string srcDir, string dstDir, bool ignoreIfNotExist, bool overwrite)
		{
			if (fileList == null || fileList.Count<string>() == 0)
			{
				return;
			}
			try
			{
				SetupHelper.CheckDiskSpace(srcDir, ignoreIfNotExist);
				Directory.CreateDirectory(dstDir);
				foreach (string path in fileList)
				{
					string text = Path.Combine(srcDir, path);
					if (!File.Exists(text))
					{
						if (!ignoreIfNotExist)
						{
							throw new FileNotExistsException(text);
						}
					}
					else
					{
						string text2 = Path.Combine(dstDir, path);
						string directoryName = Path.GetDirectoryName(text2);
						if (!Directory.Exists(directoryName))
						{
							Directory.CreateDirectory(directoryName);
						}
						if (!overwrite)
						{
							if (File.Exists(text2))
							{
								continue;
							}
						}
						try
						{
							File.Copy(text, text2, overwrite);
						}
						catch (Exception ex)
						{
							if (ex is IOException || ex is ArgumentException || ex is NotSupportedException)
							{
								throw new FileCopyException(text, text2);
							}
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				throw new FileCopyException(srcDir, dstDir);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000036F4 File Offset: 0x000018F4
		public static void DeleteDirectory(string path)
		{
			string[] files = Directory.GetFiles(path);
			string[] directories = Directory.GetDirectories(path);
			foreach (string path2 in files)
			{
				if (File.Exists(path2))
				{
					File.SetAttributes(path2, FileAttributes.Normal);
					File.Delete(path2);
				}
			}
			foreach (string path3 in directories)
			{
				SetupHelper.DeleteDirectory(path3);
			}
			if (Directory.Exists(path))
			{
				Directory.Delete(path, false);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003778 File Offset: 0x00001978
		public static bool ResumeUpgrade()
		{
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Path.Combine(SetupChecksRegistryConstant.RegistryExchangePath, "Setup-save")))
			{
				result = (registryKey != null);
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000037C4 File Offset: 0x000019C4
		public static bool MoveFiles(string srcDir, string dstDir)
		{
			if (!Directory.Exists(dstDir))
			{
				Directory.CreateDirectory(dstDir);
			}
			string[] files = SetupHelper.GetFiles(srcDir);
			foreach (string text in files)
			{
				if (Directory.Exists(text) && !SetupChecksFileConstant.GetExcludedPaths().Contains(text))
				{
					SetupHelper.MoveFiles(text, Path.Combine(dstDir, Path.GetFileName(text)));
				}
				else
				{
					string text2 = Path.Combine(dstDir, Path.GetFileName(text));
					if (File.Exists(text2))
					{
						File.Delete(text2);
					}
					File.Move(text, text2);
				}
			}
			return true;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000387C File Offset: 0x00001A7C
		public static IList<string> GetSetupRequiredFilesFromAssembly(string dirToSetupAssembly)
		{
			if (string.IsNullOrEmpty(dirToSetupAssembly))
			{
				throw new ArgumentNullException("dirToSetupAssembly");
			}
			if (!Directory.Exists(dirToSetupAssembly))
			{
				return null;
			}
			string path = Path.Combine(dirToSetupAssembly, "Microsoft.Exchange.Setup.Bootstrapper.Common.dll");
			if (!File.Exists(path))
			{
				return null;
			}
			byte[] rawAssembly = File.ReadAllBytes(path);
			Assembly assembly = Assembly.Load(rawAssembly);
			string typeFullName = typeof(SetupChecksFileConstant).FullName;
			Type type = assembly.GetTypes().FirstOrDefault((Type x) => x.FullName.Equals(typeFullName));
			if (type == null)
			{
				throw new ArgumentNullException(typeFullName);
			}
			MethodInfo methodInfo = type.GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((MethodInfo x) => x.Name.Equals("GetSetupRequiredFiles"));
			if (methodInfo == null)
			{
				throw new MissingMethodException("GetSetupRequiredFiles");
			}
			return (IList<string>)methodInfo.Invoke(type, null);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003968 File Offset: 0x00001B68
		public static string GetVersionOfApplication(string fullFileName)
		{
			string result = string.Empty;
			if (File.Exists(fullFileName))
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fullFileName);
				if (!string.IsNullOrEmpty(versionInfo.FileVersion))
				{
					result = versionInfo.FileVersion;
				}
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000039C0 File Offset: 0x00001BC0
		public static void TryFindUpdates(string dirToCheck, out string mspFileName, out string lpFileName)
		{
			mspFileName = null;
			lpFileName = null;
			if (!string.IsNullOrEmpty(dirToCheck) && Directory.Exists(dirToCheck))
			{
				IEnumerable<string> source = Directory.EnumerateFiles(dirToCheck, "*", SearchOption.TopDirectoryOnly);
				string text = source.FirstOrDefault((string x) => !string.IsNullOrEmpty(x) && MsiHelper.IsMspFileExtension(x));
				if (!string.IsNullOrEmpty(text))
				{
					mspFileName = text;
				}
				string text2 = source.FirstOrDefault((string x) => x.EndsWith("LanguagePackBundle.exe", StringComparison.InvariantCultureIgnoreCase));
				if (!string.IsNullOrEmpty(text2))
				{
					lpFileName = text2;
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003A50 File Offset: 0x00001C50
		public static void ValidOSVersion()
		{
			OperatingSystem osversion = Environment.OSVersion;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<OperatingSystem>(2160471357U, ref osversion);
			int num = 0;
			if (!string.IsNullOrEmpty(osversion.ServicePack))
			{
				MatchCollection matchCollection = Regex.Matches(osversion.ServicePack, "(\\d+\\.?\\d*|\\.\\d+)");
				if (matchCollection.Count == 1)
				{
					int.TryParse(matchCollection[0].Value, out num);
				}
			}
			if ((osversion.Version.Major != 6 || osversion.Version.Minor != 1 || num < 1) && (osversion.Version.Major != 6 || osversion.Version.Minor <= 1) && osversion.Version.Major <= 6)
			{
				throw new InvalidOSVersionException();
			}
			if (!Environment.Is64BitProcess || !Environment.Is64BitOperatingSystem)
			{
				throw new Bit64OnlyException();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003B18 File Offset: 0x00001D18
		public static bool ValidDotNetFrameworkInstalled()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full"))
			{
				try
				{
					if (registryKey == null)
					{
						return false;
					}
					string text = (string)registryKey.GetValue("version");
					if (string.IsNullOrEmpty(text))
					{
						return false;
					}
					Version v = new Version(text);
					ExTraceGlobals.FaultInjectionTracer.TraceTest<Version>(4144377149U, ref v);
					if (v < SetupChecksRegistryConstant.DotNetVersion)
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					if (ex is TypeInitializationException || ex is FileLoadException)
					{
						return false;
					}
					throw;
				}
			}
			return true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003BCC File Offset: 0x00001DCC
		public static void ValidPowershellInstalled()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\PowerShell\\3\\PowerShellEngine"))
			{
				if (registryKey == null)
				{
					throw new InvalidPSVersionException();
				}
				string text = (string)registryKey.GetValue("PowerShellVersion");
				if (string.IsNullOrEmpty(text))
				{
					throw new InvalidPSVersionException();
				}
				Version v = new Version(text);
				ExTraceGlobals.FaultInjectionTracer.TraceTest<Version>(2533764413U, ref v);
				if (v < SetupChecksRegistryConstant.PowershellVersion)
				{
					throw new InvalidPSVersionException();
				}
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003C58 File Offset: 0x00001E58
		public static bool IsClientVersionOS()
		{
			SetupHelper.OSVERSIONINFOEX osversioninfoex = default(SetupHelper.OSVERSIONINFOEX);
			osversioninfoex.VersionInfoSize = Marshal.SizeOf(typeof(SetupHelper.OSVERSIONINFOEX));
			return SetupHelper.GetVersionEx(ref osversioninfoex) && osversioninfoex.ProductType == 1;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003C98 File Offset: 0x00001E98
		internal static void GetDatacenterSettings()
		{
			SetupHelper.IsDatacenter = false;
			SetupHelper.TreatPreReqErrorsAsWarnings = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
			{
				if (registryKey != null)
				{
					int num = (int)registryKey.GetValue("DatacenterMode", 0);
					if (num != 0)
					{
						SetupHelper.IsDatacenter = true;
					}
					num = (int)registryKey.GetValue("TreatPreReqErrorsAsWarnings", 0);
					if (num != 0)
					{
						SetupHelper.TreatPreReqErrorsAsWarnings = true;
					}
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003D20 File Offset: 0x00001F20
		internal static long FindTotalSize(string srcDir, bool ignoreIfNotExist)
		{
			long num = 0L;
			foreach (string path in SetupChecksFileConstant.GetSetupRequiredFiles())
			{
				string text = Path.Combine(srcDir, path);
				if (!File.Exists(text))
				{
					if (!ignoreIfNotExist)
					{
						return 0L;
					}
				}
				else
				{
					FileInfo fileInfo = new FileInfo(text);
					num += fileInfo.Length;
				}
			}
			return num;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003DD4 File Offset: 0x00001FD4
		internal static long FindTotalSize(string srcDir, string dstDir, bool overwrite, bool recursive, string fileNamePattern, IList<string> exceptionList, long totalDirSize)
		{
			string[] fileSystemEntries = Directory.GetFileSystemEntries(srcDir);
			string[] array = fileSystemEntries;
			for (int i = 0; i < array.Length; i++)
			{
				string file = array[i];
				if (Directory.Exists(file))
				{
					if (recursive)
					{
						string fileCompare = file.ToLower() + "\\";
						if (string.IsNullOrEmpty(SetupChecksFileConstant.GetExcludedPaths().FirstOrDefault((string paths) => fileCompare.IndexOf(paths) > 0)))
						{
							totalDirSize = SetupHelper.FindTotalSize(file, Path.Combine(dstDir, Path.GetFileName(file)), overwrite, recursive, fileNamePattern, exceptionList, totalDirSize);
						}
					}
				}
				else
				{
					if (exceptionList != null)
					{
						if (exceptionList.Any((string x) => file.IndexOf(x, StringComparison.InvariantCultureIgnoreCase) >= 0))
						{
							goto IL_164;
						}
					}
					if (string.IsNullOrEmpty(fileNamePattern) || Regex.IsMatch(Path.GetFileName(file), fileNamePattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
					{
						FileInfo fileInfo = new FileInfo(file);
						string text = Path.Combine(dstDir, Path.GetFileName(file));
						if (File.Exists(text))
						{
							if (!overwrite)
							{
								goto IL_164;
							}
							totalDirSize += fileInfo.Length;
							FileInfo fileInfo2 = new FileInfo(text);
							totalDirSize -= fileInfo2.Length;
						}
						else
						{
							totalDirSize += fileInfo.Length;
						}
						SetupHelper.FilesToCopy.Add(file, text);
					}
				}
				IL_164:;
			}
			return totalDirSize;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F58 File Offset: 0x00002158
		private static string[] GetFiles(string path)
		{
			if (!Directory.Exists(path))
			{
				return null;
			}
			return Directory.GetFileSystemEntries(path);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003F6C File Offset: 0x0000216C
		private static void CheckDiskSpace(string srcDir, string dstDir, bool overwrite, bool recursive, string fileNamePattern, IList<string> exceptionList)
		{
			if (!Directory.Exists(srcDir))
			{
				throw new DirectoryNotExistsException(srcDir);
			}
			SetupHelper.FilesToCopy = new SortedList<string, string>();
			long num = SetupHelper.FindTotalSize(srcDir, dstDir, overwrite, recursive, fileNamePattern, exceptionList, 0L);
			DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(SetupHelper.WindowsDir));
			if (num > 0L && driveInfo.AvailableFreeSpace < num)
			{
				throw new InsufficientDiskSpaceException();
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3607506237U);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003FD8 File Offset: 0x000021D8
		private static void CheckDiskSpace(string srcDir, bool ignoreIfNotExist)
		{
			if (!Directory.Exists(srcDir))
			{
				throw new DirectoryNotExistsException(srcDir);
			}
			long num = SetupHelper.FindTotalSize(srcDir, ignoreIfNotExist);
			DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(SetupHelper.WindowsDir));
			if (num > 0L && driveInfo.AvailableFreeSpace < num)
			{
				throw new InsufficientDiskSpaceException();
			}
		}

		// Token: 0x0600005C RID: 92
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern int GetDriveType(string lpRootPathName);

		// Token: 0x0600005D RID: 93
		[DllImport("kernel32.Dll", SetLastError = true)]
		private static extern bool GetVersionEx(ref SetupHelper.OSVERSIONINFOEX osVersionInfo);

		// Token: 0x04000024 RID: 36
		private const byte VERNTWORKSTATION = 1;

		// Token: 0x02000007 RID: 7
		private struct OSVERSIONINFOEX
		{
			// Token: 0x0400002B RID: 43
			public int VersionInfoSize;

			// Token: 0x0400002C RID: 44
			public int MajorVersion;

			// Token: 0x0400002D RID: 45
			public int MinorVersion;

			// Token: 0x0400002E RID: 46
			public int BuildNumber;

			// Token: 0x0400002F RID: 47
			public int PlatformId;

			// Token: 0x04000030 RID: 48
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string ServicePackVersion;

			// Token: 0x04000031 RID: 49
			public short ServicePackMajor;

			// Token: 0x04000032 RID: 50
			public short ServicePackMinor;

			// Token: 0x04000033 RID: 51
			public short SuiteMask;

			// Token: 0x04000034 RID: 52
			public byte ProductType;

			// Token: 0x04000035 RID: 53
			public byte Reserved;
		}
	}
}
