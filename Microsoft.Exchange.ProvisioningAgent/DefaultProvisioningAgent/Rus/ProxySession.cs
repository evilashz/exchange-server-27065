using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.ProvisioningAgent;
using Microsoft.Win32;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000055 RID: 85
	internal class ProxySession : IDisposable
	{
		// Token: 0x0600021E RID: 542 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		private ProxySession()
		{
			this.allSingleProxySessions = new List<SingleProxySession>();
			this.allProxyDlls = new List<ProxyDLL>();
			this.serverName = Environment.MachineName;
			this.locker = new ReaderWriterLock();
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000D51C File Offset: 0x0000B71C
		~ProxySession()
		{
			this.Dispose(false);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000D54C File Offset: 0x0000B74C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000D55C File Offset: 0x0000B75C
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (SingleProxySession singleProxySession in this.allSingleProxySessions)
				{
					singleProxySession.Dispose();
				}
				this.allSingleProxySessions.Clear();
				foreach (ProxyDLL proxyDLL in this.allProxyDlls)
				{
					proxyDLL.Dispose();
				}
				this.allProxyDlls.Clear();
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000D60C File Offset: 0x0000B80C
		private void AddSingleBaseAddress(IConfigurationSession configSession, ProxyAddressTemplate baseAddress)
		{
			SingleProxySession singleProxySessionFromBaseAddress = this.GetSingleProxySessionFromBaseAddress(baseAddress);
			if (singleProxySessionFromBaseAddress != null)
			{
				return;
			}
			this.locker.AcquireWriterLock(-1);
			try
			{
				bool flag = false;
				foreach (SingleProxySession singleProxySession in this.allSingleProxySessions)
				{
					if (this.IsSameAddr(singleProxySession.BaseAddress, baseAddress))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					if (this.GetProxyDll(baseAddress.PrefixString) == null)
					{
						ProxyAddressTypeInfo singleProxyAddrTypeInfo = this.GetSingleProxyAddrTypeInfo(baseAddress);
						this.LoadProxyDll(singleProxyAddrTypeInfo);
					}
					ProxyDLL proxyDll = this.GetProxyDll(baseAddress.PrefixString);
					SingleProxySession singleProxySession2 = new SingleProxySession(proxyDll, baseAddress, this.serverName);
					singleProxySession2.Initialize();
					this.allSingleProxySessions.Add(singleProxySession2);
				}
			}
			finally
			{
				this.locker.ReleaseWriterLock();
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		public bool ValidateBaseAddress(IConfigurationSession configSession, ProxyAddressTemplate baseAddress)
		{
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			if (baseAddress == null)
			{
				throw new ArgumentNullException("baseAddress");
			}
			SingleProxySession singleProxySessionFromAddressType = this.GetSingleProxySessionFromAddressType(baseAddress);
			if (singleProxySessionFromAddressType == null)
			{
				this.AddSingleBaseAddress(configSession, baseAddress);
				singleProxySessionFromAddressType = this.GetSingleProxySessionFromAddressType(baseAddress);
			}
			return singleProxySessionFromAddressType.ValidateBaseAddress(baseAddress.ToString());
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000D74C File Offset: 0x0000B94C
		public ProxyAddress[] CreateProxies(IConfigurationSession configSession, IRecipientSession recipientSession, IRecipientSession globalCatalogSession, IEnumerable<ProxyAddressTemplate> baseAddresses, ADRecipient recipient, LogMessageDelegate logger)
		{
			if (configSession == null)
			{
				throw new ArgumentNullException("configSession");
			}
			if (recipientSession == null)
			{
				throw new ArgumentNullException("recipientSession");
			}
			if (globalCatalogSession == null)
			{
				throw new ArgumentNullException("globalCatalogSession");
			}
			if (baseAddresses == null)
			{
				throw new ArgumentNullException("baseAddresses");
			}
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			List<ProxyAddressTemplate> list = new List<ProxyAddressTemplate>();
			list.AddRange(baseAddresses);
			ProxyAddress externalEmailAddress = recipient.ExternalEmailAddress;
			if (externalEmailAddress != null && (recipient.EmailAddresses.Count == 0 || !recipient.EmailAddresses.Contains(externalEmailAddress)) && recipient.GetType() != typeof(ADPublicFolder))
			{
				recipient.EmailAddresses.Add(externalEmailAddress);
				recipient.EmailAddresses.MakePrimary(externalEmailAddress);
			}
			if (list.Count == 0)
			{
				return recipient.EmailAddresses.ToArray();
			}
			List<ProxyAddress> list2 = new List<ProxyAddress>(recipient.EmailAddresses);
			List<ProxyAddress> list3 = this.GenerateProxies(configSession, recipientSession, globalCatalogSession, list, list2, new RecipientInfo
			{
				LastName = (string)recipient[ADUserSchema.LastName],
				FirstName = (string)recipient[ADUserSchema.FirstName],
				CommonName = (string.IsNullOrEmpty(recipient.Alias) ? recipient.Name : recipient.Alias),
				DisplayName = recipient.DisplayName,
				Initials = (string)recipient[ADUserSchema.Initials]
			}, recipient, logger);
			int count = list3.Count;
			bool flag = this.IsExternalProxyAddressPrimary(recipient);
			for (int i = 0; i < list2.Count; i++)
			{
				bool flag2 = true;
				int j;
				for (j = 0; j < count; j++)
				{
					if (list2[i].IsPrimaryAddress && this.IsSameAddrType(list3[j], list2[i]))
					{
						if (flag && externalEmailAddress.Prefix == list3[j].Prefix)
						{
							list3[j] = (ProxyAddress)list3[j].ToSecondary();
						}
						else
						{
							list2[i] = (ProxyAddress)list2[i].ToSecondary();
						}
					}
				}
				j = 0;
				while (j < count)
				{
					if (list3[j] == list2[i])
					{
						flag2 = false;
						if (list2[i].IsPrimaryAddress)
						{
							list3[j] = (ProxyAddress)list3[j].ToPrimary();
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
				if (flag2)
				{
					list3.Add(list2[i]);
				}
			}
			return list3.ToArray();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000DA00 File Offset: 0x0000BC00
		private bool IsExternalProxyAddressPrimary(ADRecipient recipient)
		{
			if (recipient is ADPublicFolder)
			{
				return false;
			}
			if (recipient is ADUser && RemoteMailbox.IsRemoteMailbox(((ADUser)recipient).RecipientTypeDetails))
			{
				return false;
			}
			ProxyAddress externalEmailAddress = recipient.ExternalEmailAddress;
			return externalEmailAddress != null && externalEmailAddress.IsPrimaryAddress;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		private List<ProxyAddress> GenerateProxies(IConfigurationSession configSession, IRecipientSession recipientSession, IRecipientSession globalCatalogSession, IEnumerable<ProxyAddressTemplate> baseAddresses, IEnumerable<ProxyAddress> oldProxies, RecipientInfo recipientInfo, ADRecipient recipient, LogMessageDelegate logger)
		{
			List<ProxyAddress> list = new List<ProxyAddress>();
			List<ProxyAddressTemplate> list2 = new List<ProxyAddressTemplate>(baseAddresses);
			for (int i = list2.Count - 1; i >= 0; i--)
			{
				try
				{
					this.AddSingleBaseAddress(configSession, list2[i]);
				}
				catch (RusException e)
				{
					if (!this.IsContinue(list2[i], e, logger))
					{
						throw;
					}
					list2.RemoveAt(i);
				}
			}
			if (list2.Count == 0)
			{
				return list;
			}
			for (int j = list2.Count - 1; j > -1; j--)
			{
				bool flag = false;
				foreach (ProxyAddress proxyAddress in oldProxies)
				{
					if (this.IsSameAddrType(list2[j], proxyAddress))
					{
						try
						{
							flag = this.CheckSingleProxy(list2[j], recipientInfo, proxyAddress);
						}
						catch (RusException e2)
						{
							if (!this.IsContinue(list2[j], e2, logger))
							{
								throw;
							}
							continue;
						}
						if (flag)
						{
							break;
						}
					}
				}
				if (flag)
				{
					list2.RemoveAt(j);
				}
			}
			if (list2.Count == 0)
			{
				return list;
			}
			List<ProxyAddress> list3 = new List<ProxyAddress>();
			List<ProxyAddress> list4 = new List<ProxyAddress>();
			int num = 0;
			for (;;)
			{
				list4.Clear();
				for (int k = list2.Count - 1; k > -1; k--)
				{
					try
					{
						ProxyAddress item = this.GenerateSingleProxy(list2[k], recipientInfo, num);
						list4.Insert(0, item);
					}
					catch (RusException e3)
					{
						if (!this.IsContinue(list2[k], e3, logger))
						{
							throw;
						}
						list2.RemoveAt(k);
					}
				}
				if (list4.Count == 0)
				{
					break;
				}
				List<ADRecipient> list5 = new List<ADRecipient>();
				ADObjectId id = recipient.Id;
				this.UniqueProxies(recipientSession, globalCatalogSession, list4, id, list5);
				if (list5.Count != 0)
				{
					for (int l = list4.Count - 1; l > -1; l--)
					{
						bool flag2 = false;
						foreach (ADRecipient adrecipient in list5)
						{
							if (adrecipient.EmailAddresses.Contains(list4[l]))
							{
								list4.RemoveAt(l);
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							list2.RemoveAt(l);
						}
					}
				}
				else
				{
					list2.Clear();
				}
				list3.AddRange(list4);
				num++;
			}
			for (int m = 0; m < list3.Count; m++)
			{
				if (list3[m].IsPrimaryAddress)
				{
					list.Add(list3[m]);
				}
			}
			for (int n = 0; n < list3.Count; n++)
			{
				if (!list3[n].IsPrimaryAddress)
				{
					bool flag3 = false;
					foreach (ProxyAddress a in list)
					{
						if (a == list3[n])
						{
							flag3 = true;
							break;
						}
					}
					if (!flag3)
					{
						list.Add(list3[n]);
					}
				}
			}
			return list;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000DD98 File Offset: 0x0000BF98
		private ProxyDLL GetProxyDll(string addressType)
		{
			foreach (ProxyDLL proxyDLL in this.allProxyDlls)
			{
				if (StringComparer.InvariantCultureIgnoreCase.Equals(proxyDLL.AddrType, addressType))
				{
					return proxyDLL;
				}
			}
			return null;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000DE00 File Offset: 0x0000C000
		private void LoadProxyDll(ProxyAddressTypeInfo addressTypeInfo)
		{
			foreach (ProxyDLL proxyDLL in this.allProxyDlls)
			{
				if (StringComparer.InvariantCultureIgnoreCase.Equals(proxyDLL.AddrType, addressTypeInfo.AddrType))
				{
					return;
				}
			}
			ITopologyConfigurationSession cfgSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 575, "LoadProxyDll", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\ProxySession.cs");
			ProxyDLL proxyDLL2 = new ProxyDLL(cfgSession, addressTypeInfo.AddrType, addressTypeInfo.ProxyDLLPath, addressTypeInfo.ProxyDLLVersion, this.serverName);
			proxyDLL2.ScInitialize();
			this.allProxyDlls.Add(proxyDLL2);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		private ProxyAddressTypeInfo GetSingleProxyAddrTypeInfo(ProxyAddressTemplate baseAddress)
		{
			if (baseAddress == null)
			{
				throw new ArgumentNullException("baseAddress");
			}
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 595, "GetSingleProxyAddrTypeInfo", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\Rus\\ProxySession.cs");
			string cpuType = ProxySession.GetCpuType();
			string text = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(ProxySession.InsatllPathKey))
				{
					if (registryKey != null)
					{
						text = (string)registryKey.GetValue(ProxySession.InstallPathValueName);
					}
					else
					{
						text = ConfigurationContext.Setup.InstallPath;
					}
				}
			}
			catch (SecurityException ex)
			{
				throw new RusException(Strings.ErrorFailedToReadRegistryKey(ProxySession.InsatllPathKey, ex.Message), ex);
			}
			catch (IOException ex2)
			{
				throw new RusException(Strings.ErrorFailedToReadRegistryKey(ProxySession.InsatllPathKey, ex2.Message), ex2);
			}
			catch (UnauthorizedAccessException ex3)
			{
				throw new RusException(Strings.ErrorFailedToReadRegistryKey(ProxySession.InsatllPathKey, ex3.Message), ex3);
			}
			text = text.Replace("\"", null);
			string prefixString = baseAddress.PrefixString;
			string text2 = string.Format("{0}:{1}", prefixString, cpuType);
			ADObjectId childId = tenantOrTopologyConfigurationSession.GetOrgContainerId().GetDescendantId(ADAddressType.ContainerId).GetChildId(text2);
			ADAddressType adaddressType = tenantOrTopologyConfigurationSession.Read<ADAddressType>(childId);
			if (adaddressType != null)
			{
				string text3 = Path.Combine(text, "Mailbox");
				text3 = Path.Combine(text3, "address");
				text3 = Path.Combine(text3, prefixString);
				text3 = Path.Combine(text3, cpuType);
				text3 = Path.Combine(text3, adaddressType.ProxyGeneratorDll);
				return new ProxyAddressTypeInfo
				{
					AddrType = prefixString,
					BaseAddress = baseAddress,
					ProxyDLLPath = text3,
					ProxyDLLVersion = adaddressType.FileVersion
				};
			}
			throw new RusException(Strings.ErrorFailedToFindAddressTypeObject(text2));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000E08C File Offset: 0x0000C28C
		private SingleProxySession GetSingleProxySessionFromBaseAddress(ProxyAddressTemplate baseAddress)
		{
			SingleProxySession result = null;
			this.locker.AcquireReaderLock(-1);
			try
			{
				foreach (SingleProxySession singleProxySession in this.allSingleProxySessions)
				{
					if (this.IsSameAddr(singleProxySession.BaseAddress, baseAddress))
					{
						result = singleProxySession;
						break;
					}
				}
			}
			finally
			{
				this.locker.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000E114 File Offset: 0x0000C314
		private SingleProxySession GetSingleProxySessionFromAddressType(ProxyAddressTemplate baseAddress)
		{
			SingleProxySession result = null;
			this.locker.AcquireReaderLock(-1);
			try
			{
				foreach (SingleProxySession singleProxySession in this.allSingleProxySessions)
				{
					if (this.IsSameAddrTypeI(singleProxySession.BaseAddress, baseAddress))
					{
						result = singleProxySession;
						break;
					}
				}
			}
			finally
			{
				this.locker.ReleaseReaderLock();
			}
			return result;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000E19C File Offset: 0x0000C39C
		private bool CheckSingleProxy(ProxyAddressTemplate baseAddress, RecipientInfo recipientInfo, ProxyAddress oldProxyAddress)
		{
			SingleProxySession singleProxySessionFromBaseAddress = this.GetSingleProxySessionFromBaseAddress(baseAddress);
			return singleProxySessionFromBaseAddress.CheckProxy(recipientInfo, oldProxyAddress.ToString());
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		private ProxyAddress GenerateSingleProxy(ProxyAddressTemplate baseAddress, RecipientInfo recipientInfo, int nRetries)
		{
			if (nRetries > 20)
			{
				throw new RusException(Strings.ErrorFailedToGenerateUniqueProxy(baseAddress.ToString(), recipientInfo.CommonName));
			}
			SingleProxySession singleProxySessionFromBaseAddress = this.GetSingleProxySessionFromBaseAddress(baseAddress);
			string proxyAddressString = singleProxySessionFromBaseAddress.GenerateProxy(recipientInfo, nRetries);
			return ProxyAddress.Parse(proxyAddressString);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000E208 File Offset: 0x0000C408
		private void UniqueProxies(IRecipientSession recipientSession, IRecipientSession globalCatalogSession, IEnumerable<ProxyAddress> proxies, ADObjectId self, List<ADRecipient> conflictRecipients)
		{
			conflictRecipients.Clear();
			List<QueryFilter> list = new List<QueryFilter>();
			foreach (ProxyAddress proxyAddress in proxies)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.EmailAddresses, proxyAddress.ToString()));
			}
			int num = LdapFilterBuilder.MaxCustomFilterTreeSize - 5;
			while (0 < list.Count)
			{
				QueryFilter[] array;
				if (num >= list.Count)
				{
					array = list.ToArray();
					list.Clear();
				}
				else
				{
					array = new QueryFilter[num];
					list.CopyTo(0, array, 0, num);
					list.RemoveRange(0, num);
				}
				QueryFilter queryFilter = new OrFilter(array);
				QueryFilter queryFilter2 = new NotFilter(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, self));
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					queryFilter2,
					queryFilter
				});
				ADPagedReader<ADRecipient> adpagedReader = globalCatalogSession.FindPaged(null, QueryScope.SubTree, filter, null, 0);
				if (adpagedReader != null)
				{
					foreach (ADRecipient item in adpagedReader)
					{
						conflictRecipients.Add(item);
					}
				}
				if (string.IsNullOrEmpty(recipientSession.DomainController) || !StringComparer.InvariantCultureIgnoreCase.Equals(recipientSession.DomainController, globalCatalogSession.DomainController))
				{
					adpagedReader = recipientSession.FindPaged(null, QueryScope.SubTree, filter, null, 0);
					if (adpagedReader != null)
					{
						foreach (ADRecipient item2 in adpagedReader)
						{
							conflictRecipients.Add(item2);
						}
					}
				}
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000E3C0 File Offset: 0x0000C5C0
		private bool IsSameAddrType(ProxyAddressBase template1, ProxyAddressBase template2)
		{
			return StringComparer.InvariantCulture.Equals(template1.PrefixString, template2.PrefixString);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		private bool IsSameAddrTypeI(ProxyAddressBase template1, ProxyAddressBase template2)
		{
			return StringComparer.InvariantCultureIgnoreCase.Equals(template1.PrefixString, template2.PrefixString);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		private bool IsSameAddr(ProxyAddressBase template1, ProxyAddressBase template2)
		{
			return StringComparer.InvariantCulture.Equals(template1.PrefixString, template2.PrefixString) && StringComparer.InvariantCultureIgnoreCase.Equals(template1.ValueString, template2.ValueString);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000E424 File Offset: 0x0000C624
		private bool IsContinue(ProxyAddressTemplate baseAddress, RusException e, LogMessageDelegate logger)
		{
			ExTraceGlobals.RusTracer.TraceWarning<string, string>((long)this.GetHashCode(), "ProxySession encounter an exception while handle ProxyAddressTemplate={0}, the message:{1}", baseAddress.ToString(), e.Message);
			ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_GenerateProxyAddressFailed, new string[]
			{
				baseAddress.ToString(),
				e.Message
			});
			if (!StringComparer.InvariantCulture.Equals(baseAddress.PrefixString, "SMTP"))
			{
				if (logger != null)
				{
					logger(e.Message);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		internal static string GetCpuType()
		{
			if (IntPtr.Size != 8)
			{
				return "i386";
			}
			return "AMD64";
		}

		// Token: 0x0400011D RID: 285
		private const int MaxRetries = 20;

		// Token: 0x0400011E RID: 286
		private static readonly string InsatllPathKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x0400011F RID: 287
		private static readonly string InstallPathValueName = "MsiInstallPath";

		// Token: 0x04000120 RID: 288
		private string serverName;

		// Token: 0x04000121 RID: 289
		private ReaderWriterLock locker;

		// Token: 0x04000122 RID: 290
		private List<SingleProxySession> allSingleProxySessions;

		// Token: 0x04000123 RID: 291
		private List<ProxyDLL> allProxyDlls;

		// Token: 0x04000124 RID: 292
		public static ProxySession Instance = new ProxySession();
	}
}
