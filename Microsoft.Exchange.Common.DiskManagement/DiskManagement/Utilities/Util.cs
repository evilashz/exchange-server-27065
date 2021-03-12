using System;
using System.ComponentModel;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common.DiskManagement.Utilities
{
	// Token: 0x02000007 RID: 7
	public static class Util
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public static string RemoveEscapeCharacters(string path)
		{
			return path.Replace("\\\\", "\\");
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003D08 File Offset: 0x00001F08
		public static string WindowsErrorMessageLookup(int errorID)
		{
			Win32Exception ex = new Win32Exception(errorID);
			return ex.Message;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003D22 File Offset: 0x00001F22
		public static bool IsOperatingSystemWin8OrHigher()
		{
			return Environment.OSVersion.Version.Major > 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003D5D File Offset: 0x00001F5D
		public static void AssertReturnValueExceptionInconsistency(int returnValue, string methodName, Exception ex)
		{
			if (returnValue == 0)
			{
				ExAssert.RetailAssert(ex == null, Util.GetReturnValueExceptionInconsistencyErrorMessage(methodName, ex));
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003D72 File Offset: 0x00001F72
		public static Exception ReturnWMIErrorExceptionOnExceptionOrError(int returnValue, string methodName, Exception ex)
		{
			if (ex != null)
			{
				return ex;
			}
			if (returnValue == 0)
			{
				return null;
			}
			return new WMIErrorException(returnValue, methodName, Util.WindowsErrorMessageLookup(returnValue));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003D8B File Offset: 0x00001F8B
		public static string GetReturnValueExceptionInconsistencyErrorMessage(string methodName, Exception ex)
		{
			if (ex != null)
			{
				return DiskManagementStrings.ReturnValueExceptionInconsistency(methodName, ex.Message);
			}
			return DiskManagementStrings.ReturnValueExceptionInconsistency(methodName, "Exception is null");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public static Exception HandleExceptions(Action operation)
		{
			try
			{
				operation();
			}
			catch (COMException result)
			{
				return result;
			}
			catch (ManagementException result2)
			{
				return result2;
			}
			catch (ArgumentException result3)
			{
				return result3;
			}
			catch (MethodAccessException result4)
			{
				return result4;
			}
			catch (ObjectDisposedException result5)
			{
				return result5;
			}
			catch (InvalidOperationException result6)
			{
				return result6;
			}
			catch (NotSupportedException result7)
			{
				return result7;
			}
			catch (DirectoryNotFoundException result8)
			{
				return result8;
			}
			catch (BitlockerUtilException result9)
			{
				return result9;
			}
			return null;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003E80 File Offset: 0x00002080
		public static void ThrowIfNotNull(Exception ex)
		{
			if (ex != null)
			{
				throw ex;
			}
		}
	}
}
