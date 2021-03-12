using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200018B RID: 395
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public sealed class EventSourceInstaller
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x000296AF File Offset: 0x000278AF
		private EventSourceInstaller()
		{
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000296B8 File Offset: 0x000278B8
		public static void InstallSecurityEventSource(string sourceName, string eventMessageFile, string eventSourceXmlSchemaFile, string eventAccessStringsFile, string executableImagePath, bool allowMultipleInstances)
		{
			if (string.IsNullOrEmpty(sourceName))
			{
				throw new ArgumentNullException(DiagnosticsResources.InvalidSourceName);
			}
			if (!AuditProvider.IsSourceNameValid(sourceName))
			{
				throw new ArgumentException(DiagnosticsResources.InvalidSourceName, sourceName);
			}
			try
			{
				NativeMethods.AUTHZ_SOURCE_SCHEMA_REGISTRATION authz_SOURCE_SCHEMA_REGISTRATION;
				authz_SOURCE_SCHEMA_REGISTRATION.dwFlags = (allowMultipleInstances ? 1U : 0U);
				authz_SOURCE_SCHEMA_REGISTRATION.eventSourceName = sourceName;
				authz_SOURCE_SCHEMA_REGISTRATION.eventMessageFile = eventMessageFile;
				authz_SOURCE_SCHEMA_REGISTRATION.eventSourceXmlSchemaFile = eventSourceXmlSchemaFile;
				authz_SOURCE_SCHEMA_REGISTRATION.eventAccessStringsFile = eventAccessStringsFile;
				authz_SOURCE_SCHEMA_REGISTRATION.executableImagePath = executableImagePath;
				authz_SOURCE_SCHEMA_REGISTRATION.pReserved = IntPtr.Zero;
				authz_SOURCE_SCHEMA_REGISTRATION.dwObjectTypeNameCount = 0U;
				authz_SOURCE_SCHEMA_REGISTRATION.objectTypeNames.dwOffset = 0U;
				authz_SOURCE_SCHEMA_REGISTRATION.objectTypeNames.szObjectTypeName = null;
				if (!NativeMethods.AuthzInstallSecurityEventSource(0U, ref authz_SOURCE_SCHEMA_REGISTRATION))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 8)
					{
						throw new OutOfMemoryException();
					}
					if (lastWin32Error == 5)
					{
						throw new UnauthorizedAccessException();
					}
					if (lastWin32Error == 5010)
					{
						throw new InvalidOperationException(DiagnosticsResources.SourceAlreadyExists);
					}
					throw new Win32Exception(lastWin32Error);
				}
			}
			catch (EntryPointNotFoundException)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000297B8 File Offset: 0x000279B8
		public static void InstallApplicationEventSource(string sourceName, string eventMessageFile, string parameterMessageFile)
		{
			if (string.IsNullOrEmpty(sourceName))
			{
				throw new ArgumentNullException(DiagnosticsResources.InvalidSourceName);
			}
			if (!AuditProvider.IsSourceNameValid(sourceName))
			{
				throw new ArgumentException(DiagnosticsResources.InvalidSourceName, sourceName);
			}
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey(EventSourceInstaller.ApplicationKey, true);
				registryKey2 = registryKey.OpenSubKey(sourceName, true);
				if (registryKey2 != null)
				{
					throw new InvalidOperationException(DiagnosticsResources.SourceAlreadyExists);
				}
				registryKey2 = registryKey.CreateSubKey(sourceName);
				if (!string.IsNullOrEmpty(eventMessageFile))
				{
					registryKey2.SetValue("EventMessageFile", eventMessageFile);
				}
				if (!string.IsNullOrEmpty(parameterMessageFile))
				{
					registryKey2.SetValue("ParameterMessageFile", parameterMessageFile);
				}
				registryKey2.SetValue("TypesSupported", 24);
			}
			finally
			{
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00029890 File Offset: 0x00027A90
		public static void UninstallSecurityEventSource(string sourceName)
		{
			if (string.IsNullOrEmpty(sourceName))
			{
				throw new ArgumentNullException(DiagnosticsResources.NullSourceName);
			}
			if (!AuditProvider.IsSourceNameValid(sourceName))
			{
				throw new ArgumentException(DiagnosticsResources.InvalidSourceName, sourceName);
			}
			try
			{
				if (!NativeMethods.AuthzUninstallSecurityEventSource(0U, sourceName))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 8)
					{
						throw new OutOfMemoryException();
					}
					if (lastWin32Error == 5)
					{
						throw new UnauthorizedAccessException();
					}
					if (lastWin32Error != 2)
					{
						throw new Win32Exception(lastWin32Error);
					}
				}
			}
			catch (EntryPointNotFoundException)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002991C File Offset: 0x00027B1C
		public static void UninstallApplicationEventSource(string sourceName)
		{
			if (string.IsNullOrEmpty(sourceName))
			{
				throw new ArgumentNullException(DiagnosticsResources.NullSourceName);
			}
			if (!AuditProvider.IsSourceNameValid(sourceName))
			{
				throw new ArgumentException(DiagnosticsResources.InvalidSourceName, sourceName);
			}
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.LocalMachine.OpenSubKey(EventSourceInstaller.ApplicationKey, true);
				registryKey.DeleteSubKey(sourceName, false);
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
		}

		// Token: 0x040007E1 RID: 2017
		private static readonly string ApplicationKey = "SYSTEM\\CurrentControlSet\\Services\\EventLog\\Application";
	}
}
