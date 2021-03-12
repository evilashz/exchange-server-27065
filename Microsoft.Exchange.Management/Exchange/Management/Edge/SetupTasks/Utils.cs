using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002FB RID: 763
	internal sealed class Utils
	{
		// Token: 0x06001A1E RID: 6686 RVA: 0x000740FA File Offset: 0x000722FA
		public static bool IsPortValid(int port)
		{
			return port >= 0 && port <= 65535;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x00074110 File Offset: 0x00072310
		public static bool IsPortAvailable(int port)
		{
			Socket socket = null;
			try
			{
				socket = Utils.GetSocketBoundToPort(port);
			}
			catch (SocketException ex)
			{
				if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
				{
					Utils.RunAndLogProcessOutput("netstat", "-abn -p TCP");
					return false;
				}
				throw;
			}
			finally
			{
				if (socket != null)
				{
					socket.Close();
				}
			}
			return true;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x00074178 File Offset: 0x00072378
		public static int GetAvailablePort(object takenPortObj)
		{
			Socket socket = null;
			Socket socket2 = null;
			int port2;
			try
			{
				if (takenPortObj != null)
				{
					int port = (int)takenPortObj;
					socket = Utils.GetSocketBoundToPort(port);
				}
				socket2 = Utils.GetSocketBoundToPort(0);
				port2 = ((IPEndPoint)socket2.LocalEndPoint).Port;
			}
			finally
			{
				if (socket != null)
				{
					socket.Close();
				}
				if (socket2 != null)
				{
					socket2.Close();
				}
			}
			return port2;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000741D8 File Offset: 0x000723D8
		public static Socket GetSocketBoundToPort(int port)
		{
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (Socket.OSSupportsIPv4)
				{
					IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
					socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					socket.ExclusiveAddressUse = true;
					socket.Bind(localEP);
				}
				if (Socket.OSSupportsIPv6)
				{
					int port2 = port;
					if (socket != null)
					{
						port2 = ((IPEndPoint)socket.LocalEndPoint).Port;
					}
					IPEndPoint localEP2 = new IPEndPoint(IPAddress.IPv6Any, port2);
					socket2 = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
					socket2.ExclusiveAddressUse = true;
					socket2.Bind(localEP2);
				}
			}
			catch (Exception)
			{
				if (socket != null)
				{
					socket.Close();
					socket = null;
				}
				if (socket2 != null)
				{
					socket2.Close();
					socket2 = null;
				}
				throw;
			}
			if (socket != null && socket2 != null)
			{
				socket2.Close();
				socket2 = null;
			}
			if (socket == null)
			{
				return socket2;
			}
			return socket;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00074298 File Offset: 0x00072498
		public static void ValidateDirectory(string path, string propertyName)
		{
			string fullPath = Path.GetFullPath(path);
			if (Directory.Exists(fullPath))
			{
				return;
			}
			int num = Utils.FindNextSeparator(fullPath, 0);
			if (-1 == num)
			{
				throw new InvalidDriveInPathException(fullPath);
			}
			string text = fullPath.Substring(0, num + 1);
			if (!Directory.Exists(text))
			{
				throw new InvalidDriveInPathException(fullPath);
			}
			if (text.Length == fullPath.Length)
			{
				return;
			}
			string path2;
			do
			{
				num = Utils.FindNextSeparator(fullPath, num + 1);
				path2 = ((num == -1) ? fullPath : fullPath.Substring(0, num));
			}
			while (-1 != num && Directory.Exists(path2));
			Utils.CreateDirectory(fullPath, propertyName);
			Directory.Delete(path2, true);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x00074328 File Offset: 0x00072528
		public static void CreateDirectory(string path, string propertyName)
		{
			DirectorySecurity directorySecurity = new DirectorySecurity();
			for (int i = 0; i < Utils.DirectoryAccessRules.Length; i++)
			{
				directorySecurity.AddAccessRule(Utils.DirectoryAccessRules[i]);
			}
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				directorySecurity.SetOwner(current.User);
			}
			directorySecurity.SetAccessRuleProtection(true, false);
			try
			{
				if (!Directory.Exists(path))
				{
					Directory.CreateDirectory(path, directorySecurity);
				}
			}
			catch (UnauthorizedAccessException)
			{
				throw new NoPermissionsForPathException(path);
			}
			catch (ArgumentException)
			{
				throw new InvalidCharsInPathException(path);
			}
			catch (NotSupportedException)
			{
				throw new InvalidCharsInPathException(path);
			}
			catch (PathTooLongException)
			{
				throw new PathIsTooLongException(path);
			}
			catch (DirectoryNotFoundException)
			{
				throw new InvalidDriveInPathException(path);
			}
			catch (IOException)
			{
				throw new ReadOnlyPathException(path);
			}
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x0007441C File Offset: 0x0007261C
		public static int FindNextSeparator(string s, int pos)
		{
			return s.IndexOfAny(Utils.PathSeparators, pos);
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x0007442C File Offset: 0x0007262C
		public static void DeleteRegSubKeyTreeIfExist(RegistryKey key, string regKeyPath)
		{
			try
			{
				key.DeleteSubKeyTree(regKeyPath);
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x00074458 File Offset: 0x00072658
		public static string MakeIniFileSetting(string key, string value)
		{
			StringBuilder stringBuilder = new StringBuilder(key);
			stringBuilder.Append('=');
			stringBuilder.Append(value);
			return stringBuilder.ToString();
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x00074484 File Offset: 0x00072684
		public static int LogRunProcess(string fileName, string arguments, string path)
		{
			string processName = path + " " + fileName;
			TaskLogger.Log(Strings.LogRunningCommand(processName, arguments));
			int num = Utils.RunProcess(fileName, arguments, path);
			TaskLogger.Log(Strings.LogProcessExitCode(fileName, num));
			return num;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000744C0 File Offset: 0x000726C0
		public static int RunProcess(string fileName, string arguments, string path)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = fileName;
			processStartInfo.Arguments = arguments;
			processStartInfo.CreateNoWindow = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.UseShellExecute = true;
			if (path != null)
			{
				processStartInfo.WorkingDirectory = path;
			}
			int exitCode;
			using (Process process = Process.Start(processStartInfo))
			{
				process.WaitForExit();
				exitCode = process.ExitCode;
			}
			return exitCode;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x00074534 File Offset: 0x00072734
		public static void RunAndLogProcessOutput(string fileName, string arguments)
		{
			using (Process process = Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				Arguments = arguments,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false,
				RedirectStandardOutput = true
			}))
			{
				StreamReader standardOutput = process.StandardOutput;
				string processOutput;
				while ((processOutput = standardOutput.ReadLine()) != null)
				{
					TaskLogger.Log(Strings.OccupiedPortsInformation(processOutput));
				}
				standardOutput.Close();
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000745B8 File Offset: 0x000727B8
		public static string GetTempDir()
		{
			return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000745E2 File Offset: 0x000727E2
		public static string GetSetupLogDir()
		{
			return ConfigurationContext.Setup.SetupLoggingPath;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000745E9 File Offset: 0x000727E9
		public static void DeleteFileIfExist(string filePath)
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x000745F9 File Offset: 0x000727F9
		public static string GetWindowsDir()
		{
			return Path.GetDirectoryName(Environment.SystemDirectory);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00074608 File Offset: 0x00072808
		public static bool GetServiceExists(string serviceName)
		{
			bool result = false;
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				try
				{
					ServiceControllerStatus status = serviceController.Status;
					result = true;
				}
				catch (InvalidOperationException ex)
				{
					Win32Exception ex2 = ex.InnerException as Win32Exception;
					if (ex2 == null || 1060 != ex2.NativeErrorCode)
					{
						throw;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x04000B67 RID: 2919
		internal const string ServicesSubkeyPath = "System\\CurrentControlSet\\Services";

		// Token: 0x04000B68 RID: 2920
		internal const string ServiceStartModeValueName = "Start";

		// Token: 0x04000B69 RID: 2921
		private static readonly char[] PathSeparators = new char[]
		{
			Path.DirectorySeparatorChar,
			Path.AltDirectorySeparatorChar
		};

		// Token: 0x04000B6A RID: 2922
		private static readonly FileSystemAccessRule[] DirectoryAccessRules = new FileSystemAccessRule[]
		{
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow),
			new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null), FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow)
		};
	}
}
