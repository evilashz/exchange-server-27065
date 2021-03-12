using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000768 RID: 1896
	internal static class ExchangePrincipalCache
	{
		// Token: 0x06003888 RID: 14472 RVA: 0x000C7907 File Offset: 0x000C5B07
		private static string BuildSmtpKey(string smtpAddress, bool isArchive)
		{
			return "_EPSMTP_" + (isArchive ? "_A_" : string.Empty) + smtpAddress.ToLowerInvariant();
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000C7928 File Offset: 0x000C5B28
		private static string BuildMailboxGuidKey(Guid mailboxGuid)
		{
			return "_EPMBXGUID_" + mailboxGuid.ToString().ToLowerInvariant();
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000C7946 File Offset: 0x000C5B46
		private static string BuildSidKey(SecurityIdentifier sid, bool isArchive)
		{
			return "_EPSID_" + (isArchive ? "_A_" : string.Empty) + sid.ToString();
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000C79A4 File Offset: 0x000C5BA4
		private static void RequireUniquePrimarySmtpAddress(string primarySmtpAddress, SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, string badPrincipalCacheKey)
		{
			if (!string.IsNullOrEmpty(primarySmtpAddress))
			{
				IRecipientSession adRecipientSession = adRecipientSessionContext.GetADRecipientSession();
				try
				{
					ADRecipient adRecipient = null;
					RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.RecipientLookupLatency, delegate()
					{
						Directory.TryFindRecipient(primarySmtpAddress, adRecipientSession, out adRecipient);
					});
				}
				catch (ADConfigurationException)
				{
					ExchangePrincipalCache.HandleBadPrincipal(badPrincipalCacheKey, sid, "ExchangePrincipalCache.RequireUniquePrimarySmtpAddress  Found an account with a duplicate primary smtp address:  User Sid: {0}", new CoreResources.IDs[0]);
					throw;
				}
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000C7A3C File Offset: 0x000C5C3C
		private static void AddToCache(ExchangePrincipal exchangePrincipal)
		{
			string primarySmtpAddress = exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			ExchangePrincipalCache.AddToCache(exchangePrincipal, primarySmtpAddress);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000C7A6C File Offset: 0x000C5C6C
		private static void AddToCache(ExchangePrincipal exchangePrincipal, string primarySmtpAddress)
		{
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			bool isArchive = exchangePrincipal.MailboxInfo != null && exchangePrincipal.MailboxInfo.IsArchive;
			string text = string.IsNullOrEmpty(primarySmtpAddress) ? null : ExchangePrincipalCache.BuildSmtpKey(primarySmtpAddress, isArchive);
			string text2 = (exchangePrincipal.MailboxInfo == null || exchangePrincipal.MailboxInfo.MailboxGuid == Guid.Empty) ? null : ExchangePrincipalCache.BuildMailboxGuidKey(exchangePrincipal.MailboxInfo.MailboxGuid);
			string key = ExchangePrincipalCache.BuildSidKey(exchangePrincipal.Sid, isArchive);
			bool flag = exchangePrincipal.MailboxInfo != null && exchangePrincipal.MailboxInfo.IsAggregated;
			bool flag2 = exchangePrincipal.MailboxInfo != null && exchangePrincipal.MailboxInfo.MailboxType == MailboxLocationType.AuxArchive;
			if (text != null && !flag && !flag2)
			{
				ExchangePrincipalCache.AddOrInsertToCache(text, exchangePrincipal);
			}
			if (text2 != null)
			{
				ExchangePrincipalCache.AddOrInsertToCache(text2, exchangePrincipal);
			}
			if (!flag && !flag2)
			{
				ExchangePrincipalCache.AddOrInsertToCache(key, exchangePrincipal);
			}
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000C7B50 File Offset: 0x000C5D50
		private static void AddOrInsertToCache(string key, ExchangePrincipal exchangePrincipal)
		{
			Cache cache = HttpRuntime.Cache;
			int num = (Global.ExchangePrincipalCacheTimeoutInMinutes <= 0) ? 5 : Global.ExchangePrincipalCacheTimeoutInMinutes;
			ExchangePrincipalWrapper value = new ExchangePrincipalWrapper(exchangePrincipal);
			if (cache[key] == null)
			{
				cache.Add(key, value, null, ExDateTime.Now.AddMinutes((double)num).UniversalTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
				return;
			}
			cache.Insert(key, value, null, ExDateTime.Now.AddMinutes((double)num).UniversalTime, Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000C7BD7 File Offset: 0x000C5DD7
		internal static ExchangePrincipal GetFromCache(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext)
		{
			return ExchangePrincipalCache.GetFromCache(smtpAddress, adRecipientSessionContext, false);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000C7BE4 File Offset: 0x000C5DE4
		internal static ExchangePrincipal GetFromCache(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext, bool isArchive)
		{
			if (string.IsNullOrEmpty(smtpAddress))
			{
				throw new ArgumentNullException("smtpAddress");
			}
			string key = ExchangePrincipalCache.BuildSmtpKey(smtpAddress, isArchive);
			string text = BadExchangePrincipalCache.BuildKey(key, adRecipientSessionContext.OrganizationPrefix);
			if (BadExchangePrincipalCache.Contains(text))
			{
				ExchangePrincipalCache.HandleBadPrincipal(text, smtpAddress, "ExchangePrincipalCache.GetADUser.  Smtp address was found in bad principal cache.  SMTP Address: {0}", new CoreResources.IDs[]
				{
					(CoreResources.IDs)4088802584U
				});
			}
			ADUser aduser = ExchangePrincipalCache.GetADUser(smtpAddress, adRecipientSessionContext, text);
			ExchangePrincipal exchangePrincipal = null;
			if (!ExchangePrincipalCache.TryGetFromCache(key, out exchangePrincipal))
			{
				if (ExchangePrincipalCache.TryCreateExchangePrincipal(aduser, Guid.Empty, out exchangePrincipal, false))
				{
					ExchangePrincipalCache.AddToCache(exchangePrincipal);
				}
				else
				{
					ExchangePrincipalCache.HandleBadPrincipal(text, smtpAddress, "ExchangePrincipalCache.GetFromCache.  Cannot create ExchangePrincipal for SMTP Address: {0}", new CoreResources.IDs[]
					{
						(CoreResources.IDs)4088802584U
					});
				}
			}
			return exchangePrincipal;
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000C7C90 File Offset: 0x000C5E90
		private static ADUser GetADUser(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext, string badPrincipalCacheKey)
		{
			ADUser result = null;
			try
			{
				if (!ADIdentityInformationCache.Singleton.TryGetADUser(smtpAddress, adRecipientSessionContext, out result))
				{
					ExchangePrincipalCache.HandleBadPrincipal(badPrincipalCacheKey, smtpAddress, "ExchangePrincipalCache.GetADUser.  ExchangePrincipal was not found in AD.  SMTP Address: {0}", new CoreResources.IDs[]
					{
						(CoreResources.IDs)4088802584U
					});
				}
			}
			catch (InvalidSmtpAddressException arg)
			{
				ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<string, InvalidSmtpAddressException>(0L, "ExchangePrincipalCache.GetADUser.  Invalid SMTP Address: {0}  Exception: {1}", smtpAddress, arg);
				ExchangePrincipalCache.HandleBadPrincipal(badPrincipalCacheKey, smtpAddress, "ExchangePrincipalCache.GetADUser.  Invalid SMTP Address: {0}", new CoreResources.IDs[]
				{
					(CoreResources.IDs)4088802584U
				});
			}
			catch (ADConfigurationException arg2)
			{
				ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<ADConfigurationException>(0L, "ExchangePrincipalCache.GetADUser.  ExchangePrincipal may not have a non-unique address.  Exception {0}", arg2);
				ExchangePrincipalCache.HandleBadPrincipal(badPrincipalCacheKey, smtpAddress, "ExchangePrincipalCache.GetADUser.  Can't get ADUser for smtp address. {0}", new CoreResources.IDs[0]);
				throw;
			}
			return result;
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000C7D48 File Offset: 0x000C5F48
		public static ExchangePrincipal GetFromCacheByGuid(Guid mailboxGuid, ADRecipientSessionContext adRecipientSessionContext)
		{
			string key = ExchangePrincipalCache.BuildMailboxGuidKey(mailboxGuid);
			string text = BadExchangePrincipalCache.BuildKey(key, adRecipientSessionContext.OrganizationPrefix);
			ExchangePrincipal exchangePrincipal = null;
			if (BadExchangePrincipalCache.Contains(text) || mailboxGuid == Guid.Empty)
			{
				ExchangePrincipalCache.HandleBadPrincipal(text, mailboxGuid, "ExchangePrincipalCache.GetADUser.  MailboxGuid was found in the bad principal cache.  MailboxGuid: {0}", new CoreResources.IDs[]
				{
					(CoreResources.IDs)3279543955U
				});
			}
			ADUser aduser = ExchangePrincipalCache.GetADUser(mailboxGuid, adRecipientSessionContext, text);
			if (!ExchangePrincipalCache.TryGetFromCache(key, out exchangePrincipal))
			{
				if (ExchangePrincipalCache.TryCreateExchangePrincipal(aduser, mailboxGuid, out exchangePrincipal, false))
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "ExchangePrincipalCache", "Miss");
					ExchangePrincipalCache.AddToCache(exchangePrincipal);
				}
				else
				{
					ExchangePrincipalCache.HandleBadPrincipal(text, mailboxGuid, "ExchangePrincipalCache.GetFromCacheByGuid. Cannot create ExchangePrincipal for mailboxGuid: {0}.", new CoreResources.IDs[]
					{
						(CoreResources.IDs)3279543955U
					});
				}
			}
			return exchangePrincipal;
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000C7E04 File Offset: 0x000C6004
		private static ADUser GetADUser(Guid mailboxGuid, ADRecipientSessionContext adRecipientSessionContext, string badPrincipalCacheKey)
		{
			ADUser result = null;
			if (!ADIdentityInformationCache.Singleton.TryGetADUser(mailboxGuid, adRecipientSessionContext, out result))
			{
				ADRecipientSessionContext adrecipientSessionContext = ADRecipientSessionContext.CreateForOrganization(adRecipientSessionContext.OrganizationId);
				adrecipientSessionContext.GetADRecipientSession().SessionSettings.IncludeInactiveMailbox = true;
				if (!ADIdentityInformationCache.Singleton.TryGetADUser(mailboxGuid, adrecipientSessionContext, out result))
				{
					ExchangePrincipalCache.HandleBadPrincipal(badPrincipalCacheKey, mailboxGuid, "ExchangePrincipalCache.GetADUser. ADUser not found for mailboxGuid: {0}.", new CoreResources.IDs[]
					{
						(CoreResources.IDs)3279543955U
					});
				}
			}
			return result;
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000C7E74 File Offset: 0x000C6074
		private static void HandleBadPrincipal(string key, object invalidObject, string traceMessage, params CoreResources.IDs[] exceptionMessage)
		{
			ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug(0L, traceMessage, new object[]
			{
				invalidObject
			});
			BadExchangePrincipalCache.Add(key);
			if (exceptionMessage.Length > 0)
			{
				throw new NonExistentMailboxException(exceptionMessage[0], invalidObject.ToString());
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000C7EBC File Offset: 0x000C60BC
		private static bool TryGetFromCache(string key, out ExchangePrincipal exchangePrincipal)
		{
			exchangePrincipal = null;
			Cache cache = HttpRuntime.Cache;
			ExchangePrincipalWrapper exchangePrincipalWrapper = (ExchangePrincipalWrapper)cache[key];
			exchangePrincipal = ((exchangePrincipalWrapper != null) ? exchangePrincipalWrapper.ExchangePrincipal : null);
			return exchangePrincipalWrapper != null;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000C7EF4 File Offset: 0x000C60F4
		public static ExchangePrincipal GetFromCache(MailboxId mailboxId, ADRecipientSessionContext adRecipientSessionContext)
		{
			if (mailboxId == null)
			{
				throw new ArgumentNullException("mailboxId");
			}
			if (string.IsNullOrEmpty(mailboxId.SmtpAddress) && string.IsNullOrEmpty(mailboxId.MailboxGuid))
			{
				throw new ArgumentException("MailboxId.smtpAddress and mailbox guid are both null or empty.", "mailboxId");
			}
			ExchangePrincipal result;
			if (!string.IsNullOrEmpty(mailboxId.MailboxGuid))
			{
				result = ExchangePrincipalCache.GetFromCacheByGuid(new Guid(mailboxId.MailboxGuid), adRecipientSessionContext);
			}
			else
			{
				result = ExchangePrincipalCache.GetFromCache(mailboxId.SmtpAddress, adRecipientSessionContext);
			}
			return result;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000C7F6C File Offset: 0x000C616C
		public static bool TryGetFromCache(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out ExchangePrincipal exchangePrincipal)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			exchangePrincipal = null;
			string key = ExchangePrincipalCache.BuildSidKey(sid, false);
			string text = BadExchangePrincipalCache.BuildKey(key, adRecipientSessionContext.OrganizationPrefix);
			if (BadExchangePrincipalCache.Contains(text))
			{
				ExchangePrincipalCache.HandleBadPrincipal(text, sid, "ExchangePrincipalCache.GetADUser.  Sid was found in the bad principal cache.  UserSid: {0}", new CoreResources.IDs[0]);
				return false;
			}
			ADUser adUser = null;
			if (ExchangePrincipalCache.TryGetADUser(sid, adRecipientSessionContext, text, out adUser) && !ExchangePrincipalCache.TryGetFromCache(key, out exchangePrincipal))
			{
				if (ExchangePrincipalCache.TryCreateExchangePrincipal(adUser, Guid.Empty, out exchangePrincipal, false))
				{
					string primarySmtpAddress;
					if (exchangePrincipal is RemoteUserMailboxPrincipal)
					{
						primarySmtpAddress = ((RemoteUserMailboxPrincipal)exchangePrincipal).PrimarySmtpAddress.ToString();
					}
					else
					{
						primarySmtpAddress = exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
					}
					ExchangePrincipalCache.RequireUniquePrimarySmtpAddress(primarySmtpAddress, sid, adRecipientSessionContext, text);
					ExchangePrincipalCache.AddToCache(exchangePrincipal, primarySmtpAddress);
				}
				else
				{
					ExchangePrincipalCache.HandleBadPrincipal(text, sid, "ExchangePrincipalCache.GetFromCache.  Cannot create ExchangePrincipal for sid: {0}", new CoreResources.IDs[0]);
				}
			}
			return exchangePrincipal != null;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000C805A File Offset: 0x000C625A
		private static bool TryGetADUser(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, string badPrincipalCacheKey, out ADUser adUser)
		{
			adUser = null;
			if (!ADIdentityInformationCache.Singleton.TryGetADUser(sid, adRecipientSessionContext, out adUser))
			{
				ExchangePrincipalCache.HandleBadPrincipal(badPrincipalCacheKey, sid, "ExchangePrincipalCache.GetADUser.  ExchangePrincipal was not found in AD.  UserSid: {0}", new CoreResources.IDs[0]);
			}
			return adUser != null;
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000C81AC File Offset: 0x000C63AC
		private static bool TryCreateExchangePrincipal(ADUser adUser, Guid mailboxGuid, out ExchangePrincipal exchangePrincipal, bool includeInactiveMailboxes = false)
		{
			ExchangePrincipal localExchangePrincipal = null;
			RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.ExchangePrincipalLatency, delegate()
			{
				try
				{
					bool flag = mailboxGuid != Guid.Empty && adUser.ArchiveGuid == mailboxGuid;
					ADSessionSettings adsessionSettings = adUser.OrganizationId.ToADSessionSettings();
					if (includeInactiveMailboxes)
					{
						adsessionSettings.IncludeInactiveMailbox = true;
					}
					if (mailboxGuid == Guid.Empty || flag)
					{
						localExchangePrincipal = ExchangePrincipal.FromADUser(adsessionSettings, adUser, RemotingOptions.AllowCrossSite);
					}
					else
					{
						localExchangePrincipal = ExchangePrincipal.FromMailboxGuid(adsessionSettings, mailboxGuid, RemotingOptions.AllowCrossSite, null);
					}
					if (localExchangePrincipal.MailboxInfo != null && !localExchangePrincipal.MailboxInfo.IsArchive && flag)
					{
						localExchangePrincipal = localExchangePrincipal.GetArchiveExchangePrincipal(RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
					}
				}
				catch (ObjectNotFoundException arg)
				{
					ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<ObjectNotFoundException, ADUser>(0L, "ExchangePrincipalCache.TryCreateExchangePrincipal.  ExchangePrincipal creation failed with exception: {0} user: {1}", arg, adUser);
					if (!includeInactiveMailboxes)
					{
						ExchangePrincipalCache.TryCreateExchangePrincipal(adUser, mailboxGuid, out localExchangePrincipal, true);
					}
				}
			});
			exchangePrincipal = localExchangePrincipal;
			return exchangePrincipal != null;
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000C82CC File Offset: 0x000C64CC
		internal static bool TryGetExchangePrincipalForHybridPublicFolderAccess(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out ExchangePrincipal exchangePrincipal, bool includeInactiveMailboxes = false)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			exchangePrincipal = null;
			string key = ExchangePrincipalCache.BuildSidKey(sid, false);
			string text = BadExchangePrincipalCache.BuildKey(key, adRecipientSessionContext.OrganizationPrefix);
			if (BadExchangePrincipalCache.Contains(text))
			{
				ExchangePrincipalCache.HandleBadPrincipal(text, sid, "ExchangePrincipalCache.GetADUser.  Sid was found in the bad principal cache.  UserSid: {0}", new CoreResources.IDs[0]);
				return false;
			}
			ADUser adUser = null;
			bool flag = ExchangePrincipalCache.TryGetADUser(sid, adRecipientSessionContext, text, out adUser);
			if (flag && adUser.RecipientType == RecipientType.MailUser && adUser.RecipientTypeDetails == (RecipientTypeDetails)((ulong)-2147483648))
			{
				ExchangePrincipal localExchangePrincipal = null;
				RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.ExchangePrincipalLatency, delegate()
				{
					try
					{
						ADSessionSettings adsessionSettings = adUser.OrganizationId.ToADSessionSettings();
						if (includeInactiveMailboxes)
						{
							adsessionSettings.IncludeInactiveMailbox = true;
						}
						localExchangePrincipal = ExchangePrincipal.FromADUser(adsessionSettings, adUser, RemotingOptions.AllowHybridAccess);
					}
					catch (ObjectNotFoundException arg)
					{
						ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<ObjectNotFoundException, ADUser>(0L, "ExchangePrincipalCache.TryCreateExchangePrincipal.  ExchangePrincipal creation failed with exception: {0} user: {1}", arg, adUser);
						if (!includeInactiveMailboxes)
						{
							ExchangePrincipalCache.TryGetExchangePrincipalForHybridPublicFolderAccess(sid, adRecipientSessionContext, out localExchangePrincipal, true);
						}
					}
				});
				exchangePrincipal = localExchangePrincipal;
			}
			return exchangePrincipal != null;
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000C840C File Offset: 0x000C660C
		public static ADSessionSettings GetAdSessionSettingsForOrg(OrganizationId orgId)
		{
			ADSessionSettings localSettings = null;
			RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.EPCacheGetAdSessionSettingsForOrg, delegate()
			{
				localSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ExchangePrincipalCache.servicesRootOrgId.Value, orgId, null, false);
			});
			return localSettings;
		}

		// Token: 0x0600389C RID: 14492 RVA: 0x000C8450 File Offset: 0x000C6650
		public static void Remove(IExchangePrincipal exchangePrincipal)
		{
			ExchangePrincipalCache.TryRemoveInternal(exchangePrincipal, true);
		}

		// Token: 0x0600389D RID: 14493 RVA: 0x000C845A File Offset: 0x000C665A
		public static bool TryRemoveStale(IExchangePrincipal exchangePrincipal)
		{
			return ExchangePrincipalCache.TryRemoveInternal(exchangePrincipal, false);
		}

		// Token: 0x0600389E RID: 14494 RVA: 0x000C8464 File Offset: 0x000C6664
		private static bool TryRemoveInternal(IExchangePrincipal exchangePrincipal, bool forceRemove)
		{
			if (exchangePrincipal == null)
			{
				throw new ArgumentNullException("exchangePrincipal");
			}
			string text = exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			string text2 = string.IsNullOrEmpty(text) ? null : ExchangePrincipalCache.BuildSmtpKey(text, exchangePrincipal.MailboxInfo.IsArchive);
			string text3 = (exchangePrincipal.MailboxInfo.MailboxGuid == Guid.Empty) ? null : ExchangePrincipalCache.BuildMailboxGuidKey(exchangePrincipal.MailboxInfo.MailboxGuid);
			string text4 = ExchangePrincipalCache.BuildSidKey(exchangePrincipal.Sid, exchangePrincipal.MailboxInfo.IsArchive);
			bool result = false;
			if (text2 != null)
			{
				ExchangePrincipalWrapper exchangePrincipalWrapper = (ExchangePrincipalWrapper)HttpRuntime.Cache[text2];
				if (exchangePrincipalWrapper != null && (forceRemove || DateTime.UtcNow.Subtract(exchangePrincipalWrapper.CreatedOn).TotalSeconds > (double)Global.ExchangePrincipalCacheTimeoutInSecondsOnError))
				{
					object obj = HttpRuntime.Cache.Remove(text2);
					ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<string, bool>(0L, "ExchangePrincipalCache.Remove.  exchangePrincipal removed with smtp key {0}: {1}", text2, obj != null);
					result = true;
				}
			}
			if (text3 != null)
			{
				ExchangePrincipalWrapper exchangePrincipalWrapper = (ExchangePrincipalWrapper)HttpRuntime.Cache[text3];
				if (exchangePrincipalWrapper != null && (forceRemove || DateTime.UtcNow.Subtract(exchangePrincipalWrapper.CreatedOn).TotalSeconds > (double)Global.ExchangePrincipalCacheTimeoutInSecondsOnError))
				{
					object obj = HttpRuntime.Cache.Remove(text3);
					ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<string, bool>(0L, "ExchangePrincipalCache.Remove.  exchangePrincipal removed with mailboxGuid key {0}: {1}", text3, obj != null);
					result = true;
				}
			}
			if (text4 != null)
			{
				ExchangePrincipalWrapper exchangePrincipalWrapper = (ExchangePrincipalWrapper)HttpRuntime.Cache[text4];
				if (exchangePrincipalWrapper != null && (forceRemove || DateTime.UtcNow.Subtract(exchangePrincipalWrapper.CreatedOn).TotalSeconds > (double)Global.ExchangePrincipalCacheTimeoutInSecondsOnError))
				{
					object obj = HttpRuntime.Cache.Remove(text4);
					ExTraceGlobals.ExchangePrincipalCacheTracer.TraceDebug<string, bool>(0L, "ExchangePrincipalCache.Remove.  exchangePrincipal removed with sid key {0}: {1}", text4, obj != null);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04001F62 RID: 8034
		private const string SidKeyPrefix = "_EPSID_";

		// Token: 0x04001F63 RID: 8035
		private const string SmtpKeyPrefix = "_EPSMTP_";

		// Token: 0x04001F64 RID: 8036
		private const string MailboxGuidKeyPrefix = "_EPMBXGUID_";

		// Token: 0x04001F65 RID: 8037
		private const string ArchiveKeyPrefix = "_A_";

		// Token: 0x04001F66 RID: 8038
		private static Lazy<ADObjectId> servicesRootOrgId = new Lazy<ADObjectId>(() => ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), LazyThreadSafetyMode.PublicationOnly);
	}
}
