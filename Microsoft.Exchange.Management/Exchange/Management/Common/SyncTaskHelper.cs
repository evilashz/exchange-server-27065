using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000137 RID: 311
	internal class SyncTaskHelper
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x00034148 File Offset: 0x00032348
		public static SyncCookie ResolveSyncCookie(byte[] cookieData, IDirectorySession session, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskErrorLoggingDelegate writeError)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			string text = SyncTaskHelper.GetCurrentServerFromSession(session);
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(text, true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 84, "ResolveSyncCookie", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\SyncTaskHelper.cs");
			Guid guid = topologyConfigurationSession.GetInvocationIdByFqdn(text);
			writeVerbose(Strings.VerboseSyncTaskDomainControllerToUse(text, guid));
			if (cookieData == null)
			{
				writeVerbose(SyncTaskHelper.FormatCookieVerboseString(null));
				SyncCookie syncCookie = new SyncCookie(guid, WatermarkMap.Empty, SyncConfiguration.GetReplicationCursors(topologyConfigurationSession), null);
				writeVerbose(Strings.VerboseSyncTaskHighestCommittedUSN(text, syncCookie.HighWatermark));
				return syncCookie;
			}
			SyncCookie syncCookie2 = null;
			Exception exception = null;
			if (!SyncCookie.TryFromBytes(cookieData, out syncCookie2, out exception))
			{
				writeError(exception, ErrorCategory.InvalidData, null);
			}
			writeVerbose(SyncTaskHelper.FormatCookieVerboseString(syncCookie2));
			if (syncCookie2.DomainController != guid)
			{
				writeVerbose(Strings.VerboseSyncTaskDomainControllerMismatch(syncCookie2.DomainController, guid));
				ADServer adserver = topologyConfigurationSession.FindDCByFqdn(text);
				if (syncCookie2.PageCookie != null)
				{
					writeVerbose(Strings.VerboseSyncTaskCookieHasPageData(syncCookie2.DomainController, guid));
					string text2 = SyncTaskHelper.FindAvailableServerByInvocationId(syncCookie2.DomainController, topologyConfigurationSession, adserver.Site);
					if (text2 != null)
					{
						session.DomainController = text2;
						writeVerbose(Strings.VerboseSyncTaskHighestCommittedUSN(text2, syncCookie2.HighWatermark));
						return syncCookie2;
					}
					writeError(new PageCookieInterruptedException(), ErrorCategory.InvalidData, null);
				}
				if (!syncCookie2.LowWatermarks.ContainsKey(guid))
				{
					writeVerbose(Strings.VerboseSyncTaskCookieCurrentWatermarksMissingCurrentDC(syncCookie2.DomainController, guid));
					string text3 = SyncTaskHelper.FindAvailableServerByInvocationId(syncCookie2.DomainController, topologyConfigurationSession, adserver.Site);
					if (text3 != null)
					{
						session.DomainController = text3;
						topologyConfigurationSession.DomainController = text3;
						text = text3;
						guid = syncCookie2.DomainController;
					}
					else
					{
						writeVerbose(Strings.VerboseSyncTaskCookieOriginatingDCNotAvailable(syncCookie2.DomainController));
						Guid guid2;
						string text4 = SyncTaskHelper.FindAvailableServerFromWatermarks(syncCookie2.LowWatermarks, topologyConfigurationSession, adserver.Site, out guid2);
						if (text4 != null)
						{
							session.DomainController = text4;
							topologyConfigurationSession.DomainController = text4;
							text = text4;
							guid = guid2;
						}
						else
						{
							writeError(new CookieExpiredException(syncCookie2.DomainController, guid), ErrorCategory.InvalidData, null);
						}
					}
				}
			}
			WatermarkMap lowWatermarks = SyncTaskHelper.FallbackWatermarks(syncCookie2.LowWatermarks, syncCookie2.DomainController, guid);
			WatermarkMap highWatermarks = SyncTaskHelper.FallbackWatermarks(syncCookie2.HighWatermarks, syncCookie2.DomainController, guid);
			if (syncCookie2.HighWatermark == 0L)
			{
				syncCookie2 = new SyncCookie(guid, lowWatermarks, SyncConfiguration.GetReplicationCursors(topologyConfigurationSession), null);
			}
			else
			{
				syncCookie2 = new SyncCookie(guid, lowWatermarks, highWatermarks, syncCookie2.PageCookie);
			}
			writeVerbose(Strings.VerboseSyncTaskHighestCommittedUSN(text, syncCookie2.HighWatermark));
			return syncCookie2;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0003440C File Offset: 0x0003260C
		public static ProxyAddressCollection FilterDuplicateEmailAddresses(IRecipientSession tenantCatalogSession, ProxyAddressCollection originalEmailAddresses, ADRecipient self, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskWarningLoggingDelegate writeWarning)
		{
			ProxyAddressCollection proxyAddressCollection = originalEmailAddresses;
			Dictionary<string, ADObjectId> dups = RecipientTaskHelper.ValidateEmailAddress(tenantCatalogSession, originalEmailAddresses, self, writeVerbose);
			new ProxyAddressCollection();
			if (dups.Count > 0)
			{
				if (!dups.Keys.Any((string x) => x.StartsWith("SMTP:")))
				{
					writeWarning(Strings.WarningDuplicateProxyAddressIsFiltered(string.Join(Environment.NewLine, (from x in dups
					select string.Format("{0} | {1} | {2}", x.Key, x.Value.ToString(), self.Name)).ToArray<string>())));
					proxyAddressCollection = (from c in originalEmailAddresses
					where !dups.ContainsKey(c.ProxyAddressString)
					select c).ToArray<ProxyAddress>();
				}
			}
			ProxyAddress proxyAddress = null;
			if (!string.IsNullOrEmpty(self.LegacyExchangeDN))
			{
				foreach (ProxyAddress proxyAddress2 in proxyAddressCollection)
				{
					if (proxyAddress2.Prefix.Equals(ProxyAddressPrefix.X500) && proxyAddress2.AddressString.Equals(self.LegacyExchangeDN, StringComparison.OrdinalIgnoreCase))
					{
						proxyAddress = proxyAddress2;
						break;
					}
				}
				if (proxyAddress != null)
				{
					proxyAddressCollection.Remove(proxyAddress);
				}
			}
			return proxyAddressCollection;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00034590 File Offset: 0x00032790
		public static MultiValuedProperty<ADObjectId> RetrieveFullADObjectId(IRecipientSession tenantCatalogSession, MultiValuedProperty<ADObjectId> orignal)
		{
			if (orignal != null && orignal.Count != 0)
			{
				Result<ADRawEntry>[] source = tenantCatalogSession.ReadMultiple(orignal.ToArray(), new PropertyDefinition[]
				{
					ADObjectSchema.Guid
				});
				IEnumerable<ADObjectId> source2 = from c in source
				select c.Data.Id;
				return new MultiValuedProperty<ADObjectId>(source2.ToArray<ADObjectId>());
			}
			return null;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000345F8 File Offset: 0x000327F8
		private static WatermarkMap FallbackWatermarks(WatermarkMap originalWatermarks, Guid originalInvocationId, Guid newInvocationId)
		{
			WatermarkMap watermarkMap = originalWatermarks;
			if (originalInvocationId != newInvocationId)
			{
				watermarkMap = WatermarkMap.Empty;
				long value = 0L;
				if (originalWatermarks.TryGetValue(newInvocationId, out value))
				{
					watermarkMap.Add(newInvocationId, value);
				}
			}
			return watermarkMap;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00034630 File Offset: 0x00032830
		internal static string GetCurrentServerFromSession(IDirectorySession session)
		{
			string text = session.ServerSettings.PreferredGlobalCatalog(session.SessionSettings.GetAccountOrResourceForestFqdn());
			if (string.IsNullOrEmpty(text))
			{
				ADObjectId adobjectId = null;
				PooledLdapConnection pooledLdapConnection = null;
				try
				{
					pooledLdapConnection = session.GetReadConnection(null, ref adobjectId);
					text = pooledLdapConnection.ServerName;
				}
				finally
				{
					if (pooledLdapConnection != null)
					{
						pooledLdapConnection.ReturnToPool();
					}
				}
			}
			return text;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00034694 File Offset: 0x00032894
		private static string FindAvailableServerFromWatermarks(WatermarkMap watermarks, IConfigurationSession configSession, ADObjectId allowedSite, out Guid newInvocationId)
		{
			newInvocationId = Guid.Empty;
			foreach (Guid guid in watermarks.Keys)
			{
				string text = SyncTaskHelper.FindAvailableServerByInvocationId(guid, configSession, allowedSite);
				if (text != null)
				{
					newInvocationId = guid;
					return text;
				}
			}
			return null;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00034708 File Offset: 0x00032908
		private static string FindAvailableServerByInvocationId(Guid invocationId, IConfigurationSession configSession, ADObjectId allowedSite)
		{
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, NtdsDsaSchema.InvocationId, invocationId);
			NtdsDsa[] array = configSession.Find<NtdsDsa>(allowedSite, QueryScope.SubTree, filter, null, 1);
			if (array == null || array.Length <= 0)
			{
				return null;
			}
			ADServer adserver = configSession.Read<ADServer>(array[0].Id.Parent);
			if (adserver == null)
			{
				return null;
			}
			LocalizedString empty = LocalizedString.Empty;
			string text;
			if (!SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserver.DnsHostName, true, null, out text, out empty))
			{
				return null;
			}
			return adserver.DnsHostName;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00034778 File Offset: 0x00032978
		private static LocalizedString FormatCookieVerboseString(SyncCookie cookie)
		{
			if (cookie == null)
			{
				return Strings.VerboseSyncTaskNullCookie;
			}
			string pageCookie = "<null>";
			if (cookie.PageCookie != null && cookie.PageCookie.Length > 0)
			{
				pageCookie = string.Format("{{{0}, ...}}", cookie.PageCookie[0]);
			}
			return Strings.VerboseSyncTaskPopulateCookie(cookie.Version, cookie.DomainController, cookie.LowWatermarks.Count, cookie.LowWatermark, cookie.HighWatermarks.Count, cookie.HighWatermark, pageCookie);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000347F4 File Offset: 0x000329F4
		public static QueryFilter GetDeltaFilter(SyncCookie inputCookie)
		{
			if (inputCookie == null)
			{
				throw new ArgumentNullException("inputCookie");
			}
			return new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.GreaterThan, SyncMailboxSchema.UsnChanged, inputCookie.LowWatermark),
				new ComparisonFilter(ComparisonOperator.LessThanOrEqual, SyncMailboxSchema.UsnChanged, inputCookie.HighWatermark)
			});
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00034850 File Offset: 0x00032A50
		public static bool HandleTaskWritePagedResult<T>(IEnumerable<T> dataObjects, SyncCookie inputCookie, ref SyncCookie outputCookie, SyncTaskHelper.ParameterlessMethod<bool> isStopping, SyncTaskHelper.OneParameterMethod<bool, IConfigurable> shouldSkipObject, SyncTaskHelper.VoidOneParameterMethod<IConfigurable> writeResult, int pages, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskErrorLoggingDelegate writeError) where T : IConfigurable, new()
		{
			bool flag;
			return SyncTaskHelper.HandleTaskWritePagedResult<T>(dataObjects, inputCookie, ref outputCookie, isStopping, shouldSkipObject, writeResult, pages, writeVerbose, writeError, out flag);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00034874 File Offset: 0x00032A74
		public static bool HandleTaskWritePagedResult<T>(IEnumerable<T> dataObjects, SyncCookie inputCookie, ref SyncCookie outputCookie, SyncTaskHelper.ParameterlessMethod<bool> isStopping, SyncTaskHelper.OneParameterMethod<bool, IConfigurable> shouldSkipObject, SyncTaskHelper.VoidOneParameterMethod<IConfigurable> writeResult, int pages, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskErrorLoggingDelegate writeError, out bool hasOutput) where T : IConfigurable, new()
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObjects
			});
			ADPagedReader<T> adpagedReader = dataObjects as ADPagedReader<T>;
			hasOutput = false;
			using (IEnumerator<T> enumerator = dataObjects.GetEnumerator())
			{
				bool flag = true;
				SyncTaskHelper.CookieObjectTriple<T> cookieObjectTriple = null;
				byte[] cookie = adpagedReader.Cookie;
				bool flag2 = enumerator.MoveNext();
				if (adpagedReader.LastRetrievedCount == 0 && adpagedReader.Cookie != null && adpagedReader.Cookie.Length != 0)
				{
					return false;
				}
				while (!isStopping() && flag2)
				{
					T t = enumerator.Current;
					byte[] pageCookie = adpagedReader.Cookie;
					int pagesReturned = adpagedReader.PagesReturned;
					flag2 = enumerator.MoveNext();
					if (!shouldSkipObject(t))
					{
						hasOutput = true;
						if (cookieObjectTriple != null)
						{
							if (cookieObjectTriple.CurrentPage != pagesReturned || flag)
							{
								outputCookie = cookieObjectTriple.Cookie;
							}
							else
							{
								outputCookie = null;
							}
							writeResult(cookieObjectTriple.DataObject);
							if (outputCookie != null)
							{
								pages--;
								if (pages < 0)
								{
									break;
								}
							}
							flag = false;
						}
						else
						{
							pageCookie = cookie;
						}
						cookieObjectTriple = new SyncTaskHelper.CookieObjectTriple<T>(pagesReturned, t, new SyncCookie(inputCookie.DomainController, inputCookie.LowWatermarks, inputCookie.HighWatermarks, pageCookie));
					}
				}
				if (!flag2 && cookieObjectTriple != null && pages >= 0)
				{
					outputCookie = new SyncCookie(inputCookie.DomainController, inputCookie.HighWatermarks, WatermarkMap.Empty, null);
					writeResult(cookieObjectTriple.DataObject);
				}
			}
			TaskLogger.LogExit();
			return true;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00034A00 File Offset: 0x00032C00
		public static bool HandleTaskWritePagedResult<T>(IEnumerable<T> dataObjects, IEnumerable<T> dataObjects2, SyncCookie inputCookie, ref SyncCookie outputCookie, SyncTaskHelper.ParameterlessMethod<bool> isStopping, SyncTaskHelper.OneParameterMethod<bool, IConfigurable> shouldSkipObject, SyncTaskHelper.VoidOneParameterMethod<IConfigurable> writeResult, int pages, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskErrorLoggingDelegate writeError, out bool hasOutput) where T : IConfigurable, new()
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObjects
			});
			int num = 1000;
			int num2 = 0;
			bool flag = true;
			bool flag2 = true;
			SyncTaskHelper.CookieObjectTriple<T> cookieObjectTriple = null;
			int num3 = 1;
			ADPagedReader<T> adpagedReader = dataObjects as ADPagedReader<T>;
			ADPagedReader<T> adpagedReader2 = dataObjects2 as ADPagedReader<T>;
			byte[] pageCookie = null;
			byte[] array = null;
			byte[] cookie = adpagedReader.Cookie;
			byte[] cookie2 = adpagedReader2.Cookie;
			byte[] array2 = cookie2;
			hasOutput = false;
			for (;;)
			{
				using (IEnumerator<T> enumerator = flag ? dataObjects.GetEnumerator() : dataObjects2.GetEnumerator())
				{
					bool flag3 = enumerator.MoveNext();
					bool flag4 = !flag && ((cookieObjectTriple == null && num2 == 0) || (cookieObjectTriple != null && num2 == num - 1));
					if ((adpagedReader.LastRetrievedCount == 0 && adpagedReader.Cookie != null && adpagedReader.Cookie.Length != 0) || (adpagedReader2.LastRetrievedCount == 0 && adpagedReader2.Cookie != null && adpagedReader2.Cookie.Length != 0))
					{
						return false;
					}
					while (!isStopping() && flag3)
					{
						T t = enumerator.Current;
						if (flag)
						{
							pageCookie = adpagedReader.Cookie;
						}
						else if (array != adpagedReader2.Cookie)
						{
							array2 = array;
							array = adpagedReader2.Cookie;
						}
						flag3 = enumerator.MoveNext();
						if (!shouldSkipObject(t))
						{
							hasOutput = true;
							if (cookieObjectTriple != null)
							{
								if (num2 == num - 1 || flag2)
								{
									outputCookie = cookieObjectTriple.Cookie;
								}
								else
								{
									outputCookie = null;
								}
								writeResult(cookieObjectTriple.DataObject);
								flag2 = false;
								if (num2 == num - 1)
								{
									num2 = 0;
									num3++;
								}
								else
								{
									num2++;
								}
								if (outputCookie != null)
								{
									pages--;
									if (pages < 0)
									{
										break;
									}
								}
							}
							else
							{
								pageCookie = cookie;
								array = cookie2;
								array2 = cookie2;
							}
							cookieObjectTriple = new SyncTaskHelper.CookieObjectTriple<T>(num3, t, new SyncCookie(inputCookie.DomainController, inputCookie.LowWatermarks, inputCookie.HighWatermarks, pageCookie, flag4 ? array : array2));
						}
					}
					if (pages >= 0)
					{
						if (!flag && !flag3 && cookieObjectTriple != null && pages >= 0)
						{
							outputCookie = new SyncCookie(inputCookie.DomainController, inputCookie.HighWatermarks, WatermarkMap.Empty, null, null);
							writeResult(cookieObjectTriple.DataObject);
						}
						else if (flag)
						{
							flag = false;
							continue;
						}
					}
				}
				break;
			}
			TaskLogger.LogExit();
			return true;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00034D28 File Offset: 0x00032F28
		internal static void ResolveModifiedMultiReferenceParameter<TIdentityParameter>(string parameterName, object propertyBagKey, IEnumerable<TIdentityParameter> parameters, GetRecipientDelegate<TIdentityParameter> getRecipient, IReferenceErrorReporter referenceErrorReporter, Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary, Dictionary<object, MultiValuedProperty<ADRecipient>> recipientsDictionary, Dictionary<ADRecipient, IIdentityParameter> parameterDictionary) where TIdentityParameter : IIdentityParameter
		{
			MultiValuedProperty<ADRecipient> recipients = null;
			MultiValuedProperty<ADObjectId> recipientIds = null;
			if (parameters != null)
			{
				recipients = new MultiValuedProperty<ADRecipient>();
				recipientIds = new MultiValuedProperty<ADObjectId>();
				using (IEnumerator<TIdentityParameter> enumerator = parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TIdentityParameter parameter = enumerator.Current;
						string parameterName2 = parameterName;
						TIdentityParameter parameter2 = parameter;
						referenceErrorReporter.ValidateReference(parameterName2, parameter2.RawIdentity, delegate(Task.ErrorLoggerDelegate writeError)
						{
							ADRecipient adrecipient = getRecipient(parameter, writeError);
							if (recipientIds.Contains((ADObjectId)adrecipient.Identity))
							{
								writeError(new RecipientTaskException(Strings.ErrorRecipientIdParamElementsNotUnique(parameterName, adrecipient.Id.ToString())), ExchangeErrorCategory.Client, parameter);
							}
							recipientIds.Add(adrecipient.Identity);
							recipients.Add(adrecipient);
							parameterDictionary.Add(adrecipient, parameter);
						});
					}
				}
			}
			recipientsDictionary[propertyBagKey] = recipients;
			recipientIdsDictionary[propertyBagKey] = recipientIds;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00034E48 File Offset: 0x00033048
		internal static void ValidateModifiedMultiReferenceParameter<TDataObject>(string parameterName, object propertyKey, TDataObject targetObject, ValidateRecipientWithBaseObjectDelegate<TDataObject> validateRecipient, IReferenceErrorReporter referenceErrorReporter, Dictionary<object, MultiValuedProperty<ADRecipient>> recipientsDictionary, Dictionary<ADRecipient, IIdentityParameter> parameterDictionary) where TDataObject : ADObject
		{
			MultiValuedProperty<ADRecipient> multiValuedProperty;
			if (recipientsDictionary.TryGetValue(propertyKey, out multiValuedProperty))
			{
				using (MultiValuedProperty<ADRecipient>.Enumerator enumerator = multiValuedProperty.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ADRecipient recipient = enumerator.Current;
						referenceErrorReporter.ValidateReference(parameterName, parameterDictionary[recipient].RawIdentity, delegate(Task.ErrorLoggerDelegate writeError)
						{
							validateRecipient(targetObject, recipient, writeError);
						});
					}
				}
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00034EF0 File Offset: 0x000330F0
		internal static void AddModifiedRecipientIds(object sourceBagProperty, PropertyDefinition targetBagProperty, ADObject targetObject, Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary)
		{
			SyncTaskHelper.AddModifiedRecipientIds(sourceBagProperty, targetBagProperty, targetObject, recipientIdsDictionary, null, null);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00034F00 File Offset: 0x00033100
		internal static void AddModifiedRecipientIds(object sourceBagProperty, PropertyDefinition targetBagProperty, ADObject targetObject, Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary, Func<ADGroup, ADObjectId, IConfigDataProvider, bool> memberExistingCheck, IConfigDataProvider session)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = recipientIdsDictionary[sourceBagProperty];
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				if (targetObject[targetBagProperty] == null)
				{
					targetObject[targetBagProperty] = new MultiValuedProperty<ADObjectId>();
				}
				MultiValuedProperty<ADObjectId> multiValuedProperty2 = targetObject[targetBagProperty] as MultiValuedProperty<ADObjectId>;
				foreach (ADObjectId adobjectId in multiValuedProperty)
				{
					if (memberExistingCheck != null)
					{
						if (memberExistingCheck((ADGroup)targetObject, adobjectId, session))
						{
							continue;
						}
					}
					else if (multiValuedProperty2.Contains(adobjectId))
					{
						continue;
					}
					multiValuedProperty2.Add(adobjectId);
				}
			}
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00034FA8 File Offset: 0x000331A8
		internal static void RemoveModifiedRecipientIds(object sourceBagProperty, PropertyDefinition targetBagProperty, ADObject targetObject, Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary)
		{
			SyncTaskHelper.RemoveModifiedRecipientIds(sourceBagProperty, targetBagProperty, targetObject, recipientIdsDictionary, null, null);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00034FB8 File Offset: 0x000331B8
		internal static void RemoveModifiedRecipientIds(object sourceBagProperty, PropertyDefinition targetBagProperty, ADObject targetObject, Dictionary<object, MultiValuedProperty<ADObjectId>> recipientIdsDictionary, Func<ADGroup, ADObjectId, IConfigDataProvider, bool> memberExistingCheck, IConfigDataProvider session)
		{
			MultiValuedProperty<ADObjectId> multiValuedProperty = recipientIdsDictionary[sourceBagProperty];
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				if (targetObject[targetBagProperty] == null)
				{
					targetObject[targetBagProperty] = new MultiValuedProperty<ADObjectId>();
				}
				MultiValuedProperty<ADObjectId> multiValuedProperty2 = targetObject[targetBagProperty] as MultiValuedProperty<ADObjectId>;
				foreach (ADObjectId adobjectId in multiValuedProperty)
				{
					if (memberExistingCheck != null)
					{
						if (!memberExistingCheck((ADGroup)targetObject, adobjectId, session))
						{
							continue;
						}
					}
					else if (!multiValuedProperty2.Contains(adobjectId))
					{
						continue;
					}
					MailboxTaskHelper.RemoveItem(multiValuedProperty2, adobjectId);
				}
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00035084 File Offset: 0x00033284
		public static ValidateRecipientDelegate ValidateBypassADUser(ValidateRecipientDelegate validateRecipient)
		{
			return delegate(ADRecipient recipient, string recipientId, Task.ErrorLoggerDelegate writeError)
			{
				if (recipient.RecipientType == RecipientType.User)
				{
					return;
				}
				validateRecipient(recipient, recipientId, writeError);
			};
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000350CC File Offset: 0x000332CC
		public static ValidateRecipientWithBaseObjectDelegate<TDataObject> ValidateWithBaseObjectBypassADUser<TDataObject>(ValidateRecipientWithBaseObjectDelegate<TDataObject> validateRecipient)
		{
			return delegate(TDataObject baseObject, ADRecipient recipient, Task.ErrorLoggerDelegate writeError)
			{
				if (recipient.RecipientType == RecipientType.User)
				{
					return;
				}
				validateRecipient(baseObject, recipient, writeError);
			};
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000350F4 File Offset: 0x000332F4
		public static ProxyAddressCollection MergeAddresses(ProxyAddressCollection smtpAndX500Addresses, ProxyAddressCollection sipAddresses)
		{
			ProxyAddressCollection proxyAddressCollection = new ProxyAddressCollection();
			if (smtpAndX500Addresses != null)
			{
				foreach (ProxyAddress item in smtpAndX500Addresses)
				{
					if (!proxyAddressCollection.Contains(item))
					{
						proxyAddressCollection.Add(item);
					}
				}
			}
			if (sipAddresses != null)
			{
				foreach (ProxyAddress item2 in sipAddresses)
				{
					if (!proxyAddressCollection.Contains(item2))
					{
						proxyAddressCollection.Add(item2);
					}
				}
			}
			return proxyAddressCollection;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000351A0 File Offset: 0x000333A0
		public static ProxyAddressCollection ReplaceSmtpAndX500Addresses(ProxyAddressCollection smtpAndX500Addresses, ProxyAddressCollection originalAddresses)
		{
			if (smtpAndX500Addresses == null)
			{
				throw new ArgumentNullException("smtpAndX500Addresses");
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)smtpAndX500Addresses.Clone();
			ProxyAddress proxyAddress = null;
			int i = 0;
			while (i < proxyAddressCollection.Count)
			{
				if (proxyAddressCollection[i].Prefix == ProxyAddressPrefix.JRNL)
				{
					if (proxyAddress == null)
					{
						proxyAddress = proxyAddressCollection[i];
					}
					proxyAddressCollection.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			bool flag = false;
			if (originalAddresses != null)
			{
				foreach (ProxyAddress proxyAddress2 in originalAddresses)
				{
					if (proxyAddress2.Prefix == ProxyAddressPrefix.JRNL)
					{
						flag = true;
					}
					if (proxyAddress2.Prefix != ProxyAddressPrefix.Smtp && proxyAddress2.Prefix != ProxyAddressPrefix.X500 && !proxyAddressCollection.Contains(proxyAddress2))
					{
						proxyAddressCollection.Add(proxyAddress2);
					}
				}
			}
			if (proxyAddress != null && !flag)
			{
				proxyAddressCollection.Add(proxyAddress);
			}
			return proxyAddressCollection;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000352B0 File Offset: 0x000334B0
		public static ProxyAddressCollection ReplaceSipAddresses(ProxyAddressCollection sipAddresses, ProxyAddressCollection originalAddresses)
		{
			ProxyAddressCollection proxyAddressCollection = (sipAddresses != null) ? ((ProxyAddressCollection)sipAddresses.Clone()) : new ProxyAddressCollection();
			if (originalAddresses != null)
			{
				foreach (ProxyAddress proxyAddress in originalAddresses)
				{
					if (proxyAddress.Prefix != ProxyAddressPrefix.SIP && !proxyAddressCollection.Contains(proxyAddress))
					{
						proxyAddressCollection.Add(proxyAddress);
					}
				}
			}
			return proxyAddressCollection;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00035334 File Offset: 0x00033534
		public static ProxyAddressCollection UpdateSipNameEumProxyAddress(ProxyAddressCollection emailAddresses)
		{
			if (emailAddresses == null)
			{
				throw new ArgumentNullException("emailAddresses");
			}
			string text = null;
			foreach (ProxyAddress proxyAddress in emailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.SIP)
				{
					if (text != null)
					{
						text = null;
						break;
					}
					text = proxyAddress.ValueString;
				}
			}
			EumProxyAddress eumProxyAddress = null;
			if (!string.IsNullOrEmpty(text))
			{
				foreach (ProxyAddress proxyAddress2 in emailAddresses)
				{
					if (proxyAddress2.Prefix == ProxyAddressPrefix.UM && proxyAddress2.IsPrimaryAddress)
					{
						EumProxyAddress eumProxyAddress2 = proxyAddress2 as EumProxyAddress;
						EumAddress eumAddress = (EumAddress)eumProxyAddress2;
						if (eumAddress.IsSipExtension && !string.Equals(eumAddress.Extension, text, StringComparison.OrdinalIgnoreCase))
						{
							eumProxyAddress = eumProxyAddress2;
							break;
						}
					}
				}
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)emailAddresses.Clone();
			if (eumProxyAddress != null)
			{
				string phoneContext = ((EumAddress)eumProxyAddress).PhoneContext;
				EumAddress eumAddress2 = new EumAddress(text, phoneContext);
				EumProxyAddress item = new EumProxyAddress(eumAddress2.ToString(), true);
				proxyAddressCollection.Remove(eumProxyAddress);
				proxyAddressCollection.Add(item);
			}
			return proxyAddressCollection;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00035494 File Offset: 0x00033694
		public static void SetExchangeAccountDisabled(ADUser user, bool disabled)
		{
			if (disabled)
			{
				user.ExchangeUserAccountControl |= UserAccountControlFlags.AccountDisabled;
				return;
			}
			user.ExchangeUserAccountControl &= ~UserAccountControlFlags.AccountDisabled;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000354B7 File Offset: 0x000336B7
		public static void SetExchangeAccountDisabledWithADLogon(ADUser user, bool disabled)
		{
			if (disabled)
			{
				user.UserAccountControl |= UserAccountControlFlags.AccountDisabled;
				user.ExchangeUserAccountControl |= UserAccountControlFlags.AccountDisabled;
				return;
			}
			user.UserAccountControl &= ~UserAccountControlFlags.AccountDisabled;
			user.ExchangeUserAccountControl &= ~UserAccountControlFlags.AccountDisabled;
		}

		// Token: 0x0400058F RID: 1423
		public const string CookieParameterSet = "CookieSet";

		// Token: 0x04000590 RID: 1424
		public const string SmtpAndX500AddressesParameterName = "SmtpAndX500Addresses";

		// Token: 0x04000591 RID: 1425
		public const string ReleaseTrackParameterName = "ReleaseTrack";

		// Token: 0x04000592 RID: 1426
		public const string SipAddressesParameterName = "SipAddresses";

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x06000B55 RID: 2901
		internal delegate void VoidOneParameterMethod<A>(A arg);

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x06000B59 RID: 2905
		internal delegate R OneParameterMethod<R, A>(A arg);

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x06000B5D RID: 2909
		internal delegate R ParameterlessMethod<R>();

		// Token: 0x0200013B RID: 315
		private class CookieObjectTriple<T>
		{
			// Token: 0x1700031D RID: 797
			// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000354FF File Offset: 0x000336FF
			// (set) Token: 0x06000B61 RID: 2913 RVA: 0x00035507 File Offset: 0x00033707
			public int CurrentPage { get; set; }

			// Token: 0x1700031E RID: 798
			// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00035510 File Offset: 0x00033710
			// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00035518 File Offset: 0x00033718
			public T DataObject { get; set; }

			// Token: 0x1700031F RID: 799
			// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00035521 File Offset: 0x00033721
			// (set) Token: 0x06000B65 RID: 2917 RVA: 0x00035529 File Offset: 0x00033729
			public SyncCookie Cookie { get; set; }

			// Token: 0x06000B66 RID: 2918 RVA: 0x00035532 File Offset: 0x00033732
			public CookieObjectTriple(int currentPage, T dataObject, SyncCookie cookie)
			{
				this.CurrentPage = currentPage;
				this.DataObject = dataObject;
				this.Cookie = cookie;
			}
		}
	}
}
