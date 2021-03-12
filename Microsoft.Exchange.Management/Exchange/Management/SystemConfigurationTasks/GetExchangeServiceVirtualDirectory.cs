using System;
using System.DirectoryServices;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C0A RID: 3082
	public abstract class GetExchangeServiceVirtualDirectory<T> : GetExchangeVirtualDirectory<T> where T : ADExchangeServiceVirtualDirectory, new()
	{
		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x06007484 RID: 29828 RVA: 0x001DBB36 File Offset: 0x001D9D36
		protected virtual LocalizedString MetabaseGetPropertiesFailureMessage
		{
			get
			{
				return Strings.MetabaseGetPropertiesFailure;
			}
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x001DBB40 File Offset: 0x001D9D40
		protected override void ProcessMetabaseProperties(ExchangeVirtualDirectory dataObject)
		{
			TaskLogger.LogEnter();
			base.ProcessMetabaseProperties(dataObject);
			ADExchangeServiceVirtualDirectory adexchangeServiceVirtualDirectory = (ADExchangeServiceVirtualDirectory)dataObject;
			try
			{
				DirectoryEntry directoryEntry2;
				DirectoryEntry directoryEntry = directoryEntry2 = IisUtility.CreateIISDirectoryEntry(adexchangeServiceVirtualDirectory.MetabasePath, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), dataObject.Identity, false);
				try
				{
					if (directoryEntry != null)
					{
						adexchangeServiceVirtualDirectory.BasicAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic));
						adexchangeServiceVirtualDirectory.DigestAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Digest));
						adexchangeServiceVirtualDirectory.WindowsAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Ntlm));
						adexchangeServiceVirtualDirectory.LiveIdNegotiateAuthentication = new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.LiveIdNegotiate));
						adexchangeServiceVirtualDirectory.LiveIdBasicAuthentication = new bool?(adexchangeServiceVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.LiveIdBasic));
						adexchangeServiceVirtualDirectory.OAuthAuthentication = new bool?(adexchangeServiceVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.OAuth));
						adexchangeServiceVirtualDirectory.AdfsAuthentication = new bool?(adexchangeServiceVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.Adfs));
						adexchangeServiceVirtualDirectory.WSSecurityAuthentication = new bool?(adexchangeServiceVirtualDirectory.InternalAuthenticationMethods.Contains(AuthenticationMethod.WSSecurity) && IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.WSSecurity));
					}
				}
				finally
				{
					if (directoryEntry2 != null)
					{
						((IDisposable)directoryEntry2).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				TaskLogger.Trace("Exception occurred: {0}", new object[]
				{
					ex.Message
				});
				base.WriteError(new LocalizedException(this.MetabaseGetPropertiesFailureMessage, ex), ErrorCategory.InvalidOperation, dataObject.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x001DBCE0 File Offset: 0x001D9EE0
		protected bool? GetCertificateAuthentication(ExchangeVirtualDirectory dataObject)
		{
			return this.GetCertificateAuthentication(dataObject, null);
		}

		// Token: 0x06007487 RID: 29831 RVA: 0x001DBCEA File Offset: 0x001D9EEA
		protected bool? GetCertificateAuthentication(ExchangeVirtualDirectory dataObject, string subVDirName)
		{
			return this.GetAuthentication(dataObject, subVDirName, AuthenticationMethodFlags.Certificate);
		}

		// Token: 0x06007488 RID: 29832 RVA: 0x001DBCF9 File Offset: 0x001D9EF9
		protected bool? GetLiveIdNegotiateAuthentication(ExchangeVirtualDirectory dataObject, string subVDirName)
		{
			return this.GetAuthentication(dataObject, subVDirName, AuthenticationMethodFlags.LiveIdNegotiate);
		}

		// Token: 0x06007489 RID: 29833 RVA: 0x001DBD08 File Offset: 0x001D9F08
		private bool? GetAuthentication(ExchangeVirtualDirectory dataObject, string subVDirName, AuthenticationMethodFlags authFlags)
		{
			TaskLogger.LogEnter();
			try
			{
				string text = dataObject.MetabasePath;
				if (!string.IsNullOrEmpty(subVDirName))
				{
					text = string.Format("{0}/{1}", text, subVDirName);
				}
				if (IisUtility.Exists(text))
				{
					using (DirectoryEntry directoryEntry = IisUtility.CreateIISDirectoryEntry(text, new Task.TaskErrorLoggingReThrowDelegate(this.WriteError), dataObject.Identity))
					{
						bool ignoreAnonymousOnCert = dataObject is ADPowerShellCommonVirtualDirectory;
						return new bool?(IisUtility.CheckForAuthenticationMethod(directoryEntry, authFlags, ignoreAnonymousOnCert));
					}
				}
			}
			catch (Exception ex)
			{
				TaskLogger.Trace("Exception occurred: {0}", new object[]
				{
					ex.Message
				});
				base.WriteError(new LocalizedException(this.MetabaseGetPropertiesFailureMessage, ex), (ErrorCategory)1001, dataObject.Identity);
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return null;
		}

		// Token: 0x0600748A RID: 29834 RVA: 0x001DBDFC File Offset: 0x001D9FFC
		protected override bool CanIgnoreMissingMetabaseEntry()
		{
			return true;
		}
	}
}
