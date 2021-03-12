using System;
using System.Collections;
using System.DirectoryServices;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C30 RID: 3120
	internal class WebAppVirtualDirectoryHelper
	{
		// Token: 0x0600763C RID: 30268 RVA: 0x001E2386 File Offset: 0x001E0586
		protected WebAppVirtualDirectoryHelper()
		{
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x001E2390 File Offset: 0x001E0590
		internal static void CopyMetabaseProperties(ExchangeWebAppVirtualDirectory target, ExchangeWebAppVirtualDirectory source)
		{
			target.DefaultDomain = source.DefaultDomain;
			target.FormsAuthentication = source.FormsAuthentication;
			target.BasicAuthentication = source.BasicAuthentication;
			target.DigestAuthentication = source.DigestAuthentication;
			target.WindowsAuthentication = source.WindowsAuthentication;
			target.LiveIdAuthentication = source.LiveIdAuthentication;
			target.AdfsAuthentication = source.AdfsAuthentication;
			target.GzipLevel = source.GzipLevel;
			target.WebSite = source.WebSite;
		}

		// Token: 0x0600763E RID: 30270 RVA: 0x001E240C File Offset: 0x001E060C
		internal static T FindWebAppVirtualDirectoryInSameWebSite<T>(ExchangeWebAppVirtualDirectory source, IConfigDataProvider dataProvider) where T : ExchangeWebAppVirtualDirectory, new()
		{
			T result = default(T);
			IConfigurable[] array = dataProvider.Find<T>(null, source.Server, true, null);
			if (array != null)
			{
				foreach (ExchangeWebAppVirtualDirectory exchangeWebAppVirtualDirectory in array)
				{
					if (IisUtility.Exists(exchangeWebAppVirtualDirectory.MetabasePath))
					{
						WebAppVirtualDirectoryHelper.UpdateFromMetabase(exchangeWebAppVirtualDirectory);
						if (string.Equals(source.WebSite, exchangeWebAppVirtualDirectory.WebSite, StringComparison.OrdinalIgnoreCase))
						{
							result = (T)((object)exchangeWebAppVirtualDirectory);
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600763F RID: 30271 RVA: 0x001E2484 File Offset: 0x001E0684
		internal static void CheckTwoWebAppVirtualDirectories(ExchangeWebAppVirtualDirectory first, ExchangeWebAppVirtualDirectory second, Action<string> WriteWarning, LocalizedString authMethedNotMatch, LocalizedString urlNotMatch)
		{
			if ((first.IsModified(ExchangeWebAppVirtualDirectorySchema.BasicAuthentication) || first.IsModified(ExchangeWebAppVirtualDirectorySchema.DigestAuthentication) || first.IsModified(ExchangeWebAppVirtualDirectorySchema.FormsAuthentication) || first.IsModified(ExchangeWebAppVirtualDirectorySchema.LiveIdAuthentication) || first.IsModified(ExchangeWebAppVirtualDirectorySchema.WindowsAuthentication)) && (first.BasicAuthentication != second.BasicAuthentication || first.DigestAuthentication != second.DigestAuthentication || first.FormsAuthentication != second.FormsAuthentication || first.LiveIdAuthentication != second.LiveIdAuthentication || first.WindowsAuthentication != second.WindowsAuthentication))
			{
				WriteWarning(authMethedNotMatch);
			}
			if ((first.IsModified(ADVirtualDirectorySchema.InternalUrl) && !WebAppVirtualDirectoryHelper.IsUrlConsistent(first.InternalUrl, second.InternalUrl)) || (first.IsModified(ADVirtualDirectorySchema.ExternalUrl) && !WebAppVirtualDirectoryHelper.IsUrlConsistent(first.ExternalUrl, second.ExternalUrl)))
			{
				WriteWarning(urlNotMatch);
			}
		}

		// Token: 0x06007640 RID: 30272 RVA: 0x001E2574 File Offset: 0x001E0774
		private static bool IsUrlConsistent(Uri url1, Uri url2)
		{
			if (url1 != null && url2 != null)
			{
				return url1.Scheme == url2.Scheme && url1.Host == url2.Host && url1.Port == url2.Port;
			}
			return url1 == url2;
		}

		// Token: 0x06007641 RID: 30273 RVA: 0x001E25D4 File Offset: 0x001E07D4
		internal static void UpdateFromMetabase(ExchangeWebAppVirtualDirectory webAppVirtualDirectory)
		{
			try
			{
				DirectoryEntry directoryEntry2;
				DirectoryEntry directoryEntry = directoryEntry2 = IisUtility.CreateIISDirectoryEntry(webAppVirtualDirectory.MetabasePath);
				try
				{
					MetabaseProperty[] properties = IisUtility.GetProperties(directoryEntry);
					webAppVirtualDirectory.DefaultDomain = (string)IisUtility.GetIisPropertyValue("DefaultLogonDomain", properties);
					webAppVirtualDirectory[ExchangeWebAppVirtualDirectorySchema.FormsAuthentication] = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Fba);
					webAppVirtualDirectory[ExchangeWebAppVirtualDirectorySchema.BasicAuthentication] = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic);
					webAppVirtualDirectory[ExchangeWebAppVirtualDirectorySchema.DigestAuthentication] = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Digest);
					webAppVirtualDirectory[ExchangeWebAppVirtualDirectorySchema.WindowsAuthentication] = IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Ntlm);
					if (!IisUtility.CheckForAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.None))
					{
						webAppVirtualDirectory[ExchangeWebAppVirtualDirectorySchema.LiveIdAuthentication] = false;
					}
					webAppVirtualDirectory.DisplayName = directoryEntry.Name;
					webAppVirtualDirectory.WebSite = IisUtility.GetWebSiteName(directoryEntry.Parent.Path);
				}
				finally
				{
					if (directoryEntry2 != null)
					{
						((IDisposable)directoryEntry2).Dispose();
					}
				}
				webAppVirtualDirectory.GzipLevel = Gzip.GetGzipLevel(webAppVirtualDirectory.MetabasePath);
			}
			catch (IISGeneralCOMException ex)
			{
				if (ex.Code == -2147023174)
				{
					throw new IISNotReachableException(IisUtility.GetHostName(webAppVirtualDirectory.MetabasePath), ex.Message);
				}
				throw;
			}
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x001E2708 File Offset: 0x001E0908
		internal static void UpdateMetabase(ExchangeWebAppVirtualDirectory webAppVirtualDirectory, string metabasePath, bool enableAnonymous)
		{
			try
			{
				DirectoryEntry directoryEntry2;
				DirectoryEntry directoryEntry = directoryEntry2 = IisUtility.CreateIISDirectoryEntry(webAppVirtualDirectory.MetabasePath);
				try
				{
					ArrayList arrayList = new ArrayList();
					if (webAppVirtualDirectory.DefaultDomain.Length > 0)
					{
						arrayList.Add(new MetabaseProperty("DefaultLogonDomain", webAppVirtualDirectory.DefaultDomain, true));
					}
					else if (webAppVirtualDirectory.DefaultDomain == "")
					{
						directoryEntry.Properties["DefaultLogonDomain"].Clear();
					}
					IisUtility.SetProperties(directoryEntry, arrayList);
					directoryEntry.CommitChanges();
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.None, true);
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Basic, webAppVirtualDirectory.BasicAuthentication);
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.Digest, webAppVirtualDirectory.DigestAuthentication);
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.WindowsIntegrated, webAppVirtualDirectory.WindowsAuthentication);
					IisUtility.SetAuthenticationMethod(directoryEntry, AuthenticationMethodFlags.LiveIdFba, webAppVirtualDirectory.LiveIdAuthentication);
					if (webAppVirtualDirectory.FormsAuthentication)
					{
						OwaIsapiFilter.EnableFba(directoryEntry);
					}
					else
					{
						OwaIsapiFilter.DisableFba(directoryEntry);
					}
					IisUtility.SetAuthenticationMethod(directoryEntry, MetabasePropertyTypes.AuthFlags.Anonymous, enableAnonymous);
					directoryEntry.CommitChanges();
				}
				finally
				{
					if (directoryEntry2 != null)
					{
						((IDisposable)directoryEntry2).Dispose();
					}
				}
				GzipLevel gzipLevel = webAppVirtualDirectory.GzipLevel;
				string site = IisUtility.WebSiteFromMetabasePath(webAppVirtualDirectory.MetabasePath);
				Gzip.SetIisGzipLevel(site, GzipLevel.High);
				Gzip.SetVirtualDirectoryGzipLevel(webAppVirtualDirectory.MetabasePath, gzipLevel);
			}
			catch (IISGeneralCOMException ex)
			{
				if (ex.Code == -2147023174)
				{
					throw new IISNotReachableException(IisUtility.GetHostName(webAppVirtualDirectory.MetabasePath), ex.Message);
				}
				throw;
			}
		}
	}
}
