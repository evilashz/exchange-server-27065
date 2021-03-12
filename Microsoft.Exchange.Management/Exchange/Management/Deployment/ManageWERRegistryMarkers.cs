using System;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000206 RID: 518
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public abstract class ManageWERRegistryMarkers : Task
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x0004E478 File Offset: 0x0004C678
		protected void WriteRegistryMarkers()
		{
			try
			{
				using (RegistryKey werconsentRegistryKey = this.GetWERConsentRegistryKey())
				{
					if (werconsentRegistryKey != null)
					{
						foreach (string name in this.exchangeEventTypes)
						{
							werconsentRegistryKey.SetValue(name, 4, RegistryValueKind.DWord);
						}
					}
				}
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.SecurityError, null);
			}
			catch (UnauthorizedAccessException exception2)
			{
				base.WriteError(exception2, ErrorCategory.PermissionDenied, null);
			}
			catch (IOException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ResourceUnavailable, null);
			}
			catch (Win32Exception exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0004E544 File Offset: 0x0004C744
		protected void DeleteRegistryMarkers()
		{
			try
			{
				using (RegistryKey werconsentRegistryKey = this.GetWERConsentRegistryKey())
				{
					if (werconsentRegistryKey != null)
					{
						foreach (string name in this.exchangeEventTypes)
						{
							werconsentRegistryKey.DeleteValue(name, false);
						}
					}
				}
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.SecurityError, null);
			}
			catch (UnauthorizedAccessException exception2)
			{
				base.WriteError(exception2, ErrorCategory.PermissionDenied, null);
			}
			catch (IOException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ResourceUnavailable, null);
			}
			catch (Win32Exception exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0004E60C File Offset: 0x0004C80C
		private RegistryKey GetWERConsentRegistryKey()
		{
			RegistryKey registryKey = null;
			if (ConfigurationContext.Setup.IsLonghornServer)
			{
				registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\Consent", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (registryKey == null)
				{
					throw new IOException(Strings.ExceptionRegistryKeyNotFound("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\Consent"));
				}
			}
			return registryKey;
		}

		// Token: 0x04000799 RID: 1945
		private const string WERConsentKey = "SOFTWARE\\Microsoft\\Windows\\Windows Error Reporting\\Consent";

		// Token: 0x0400079A RID: 1946
		private string[] exchangeEventTypes = new string[]
		{
			"E12",
			"E12N",
			"E12IE",
			"E12IIS"
		};
	}
}
