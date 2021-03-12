using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009B3 RID: 2483
	[Cmdlet("Get", "MailboxServer", DefaultParameterSetName = "Identity")]
	public sealed class GetMailboxServer : GetSystemConfigurationObjectTask<MailboxServerIdParameter, Server>
	{
		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x06005899 RID: 22681 RVA: 0x00171C2C File Offset: 0x0016FE2C
		// (set) Token: 0x0600589A RID: 22682 RVA: 0x00171C52 File Offset: 0x0016FE52
		[Parameter]
		public SwitchParameter Status
		{
			get
			{
				return (SwitchParameter)(base.Fields["Status"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x0600589B RID: 22683 RVA: 0x00171C6A File Offset: 0x0016FE6A
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new BitMaskAndFilter(ServerSchema.CurrentServerRole, 2UL);
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x0600589C RID: 22684 RVA: 0x00171C78 File Offset: 0x0016FE78
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600589D RID: 22685 RVA: 0x00171C7C File Offset: 0x0016FE7C
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			MailboxServer mailboxServer = new MailboxServer((Server)dataObject);
			string fqdn = ((Server)dataObject).Fqdn;
			if (this.Status && ((Server)dataObject).IsProvisionedServer)
			{
				this.WriteWarning(Strings.StatusSpecifiedForProvisionedServer);
			}
			if (this.Status && !mailboxServer.IsReadOnly && !((Server)dataObject).IsProvisionedServer)
			{
				if (string.IsNullOrEmpty(fqdn))
				{
					this.WriteWarning(Strings.ErrorInvalidObjectMissingCriticalProperty(typeof(Server).Name, mailboxServer.Identity.ToString(), ServerSchema.Fqdn.Name));
				}
				else
				{
					Exception ex = null;
					CultureInfo[] array;
					GetMailboxServer.GetConfigurationFromRegistry(fqdn, out array, out ex);
					if (ex != null)
					{
						this.WriteWarning(Strings.ErrorAccessingRegistryRaisesException(fqdn, ex.Message));
					}
					mailboxServer.Locale = array;
					mailboxServer.ResetChangeTracking();
				}
			}
			base.WriteResult(mailboxServer);
			TaskLogger.LogExit();
		}

		// Token: 0x0600589E RID: 22686 RVA: 0x00171D84 File Offset: 0x0016FF84
		internal static void GetConfigurationFromRegistry(string computerName, out CultureInfo[] locale, out Exception caughtException)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			caughtException = null;
			List<CultureInfo> list = new List<CultureInfo>();
			try
			{
				using (RegistryKey registryKey = RegistryUtil.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language"))
					{
						if (registryKey2 != null)
						{
							string[] valueNames = registryKey2.GetValueNames();
							if (valueNames != null)
							{
								foreach (string text in valueNames)
								{
									int culture = (int)registryKey2.GetValue(text, 0);
									try
									{
										CultureInfo cultureInfo = new CultureInfo(culture);
										if (cultureInfo.ThreeLetterWindowsLanguageName.Equals(text, StringComparison.InvariantCultureIgnoreCase))
										{
											list.Add(cultureInfo);
										}
									}
									catch (ArgumentException)
									{
										TaskLogger.Trace("There is An inlvad data in the rigistry: SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language", new object[0]);
									}
								}
							}
						}
					}
				}
			}
			catch (SecurityException ex)
			{
				caughtException = ex;
			}
			catch (IOException ex2)
			{
				caughtException = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				caughtException = ex3;
			}
			locale = list.ToArray();
		}

		// Token: 0x0600589F RID: 22687 RVA: 0x00171EC4 File Offset: 0x001700C4
		internal static void SetConfigurationFromRegistry(string computerName, IEnumerable<CultureInfo> locale, out Exception caughtException)
		{
			if (string.IsNullOrEmpty(computerName))
			{
				throw new ArgumentNullException("computerName");
			}
			if (locale == null)
			{
				throw new ArgumentNullException("locale");
			}
			caughtException = null;
			try
			{
				using (RegistryKey registryKey = RegistryUtil.OpenRemoteBaseKey(RegistryHive.LocalMachine, computerName))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language"))
					{
						if (registryKey2 != null)
						{
							registryKey.DeleteSubKeyTree("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language");
						}
					}
					IEnumerator<CultureInfo> enumerator = locale.GetEnumerator();
					if (enumerator.MoveNext())
					{
						using (RegistryKey registryKey3 = registryKey.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language"))
						{
							do
							{
								CultureInfo cultureInfo = enumerator.Current;
								registryKey3.SetValue(cultureInfo.ThreeLetterWindowsLanguageName, cultureInfo.LCID, RegistryValueKind.DWord);
							}
							while (enumerator.MoveNext());
						}
					}
				}
			}
			catch (SecurityException ex)
			{
				caughtException = ex;
			}
			catch (IOException ex2)
			{
				caughtException = ex2;
			}
			catch (UnauthorizedAccessException ex3)
			{
				caughtException = ex3;
			}
		}

		// Token: 0x040032D6 RID: 13014
		internal const string LanguageKeyName = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Language";
	}
}
