﻿using System;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006A6 RID: 1702
	internal class ADIdentityInformationCache
	{
		// Token: 0x06003479 RID: 13433 RVA: 0x000BCF64 File Offset: 0x000BB164
		public static void Initialize(int maxCacheSize)
		{
			ADIdentityInformationCache.Initialize(maxCacheSize, ADIdentityInformationCache.DefaultAbsoluteTimeout, ADIdentityInformationCache.DefaultSlidingTimeout);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x000BCF76 File Offset: 0x000BB176
		public static void Initialize(int maxCacheSize, TimeSpan absoluteTimeout, TimeSpan slidingTimeout)
		{
			if (ADIdentityInformationCache.singleton != null)
			{
				throw new InvalidOperationException("ADIdentityInformationCache can only be initialized once.");
			}
			ADIdentityInformationCache.singleton = new ADIdentityInformationCache(maxCacheSize, absoluteTimeout, slidingTimeout);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x000BCF98 File Offset: 0x000BB198
		public static SecurityIdentifier CreateSid(string stringSid)
		{
			if (string.IsNullOrEmpty(stringSid))
			{
				throw new InvalidSidException(stringSid);
			}
			SecurityIdentifier result;
			try
			{
				result = new SecurityIdentifier(stringSid);
			}
			catch (SecurityException innerException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "[ADIdentityInformationCache.CreateSid(string)] Security Exception encountered:  StringSid: {0}", stringSid);
				throw new InvalidSidException(stringSid, innerException);
			}
			catch (ArgumentException innerException2)
			{
				ExTraceGlobals.ServerToServerAuthZTracer.TraceDebug<string>(0L, "[ADIdentityInformationCache.CreateSid(string)] Argument exception encountered.  StringSid: {0}", stringSid);
				throw new InvalidSidException(stringSid, innerException2);
			}
			return result;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x000BD04C File Offset: 0x000BB24C
		internal ADIdentityInformationCache(int maxCacheSize, TimeSpan absoluteTimeout, TimeSpan slidingTimeout)
		{
			this.cache = new ExactTimeoutCache<string, ADIdentityInformation>(null, null, null, maxCacheSize, false, CacheFullBehavior.ExpireExisting);
			this.absoluteTimeout = absoluteTimeout;
			this.slidingTimeout = slidingTimeout;
			this.useCache = (this.absoluteTimeout > TimeSpan.Zero && this.slidingTimeout > TimeSpan.Zero);
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000BD0D1 File Offset: 0x000BB2D1
		public static ADIdentityInformationCache Singleton
		{
			get
			{
				return ADIdentityInformationCache.singleton;
			}
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x000BD0D8 File Offset: 0x000BB2D8
		public bool TryGet(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out ADIdentityInformation adIdentityInformation)
		{
			adIdentityInformation = null;
			if (!this.TryGetFromCache(sid, adRecipientSessionContext.OrganizationPrefix, out adIdentityInformation))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "ADIdentityCache", "Miss");
				if (!this.TryLookupAndAddRecipient(sid, adRecipientSessionContext, out adIdentityInformation) && !this.TryLookupAndAddComputer(sid, out adIdentityInformation))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "[ADIdentityInformationCache::Get] No user found in the directory with sid: {0}", sid);
				}
			}
			return adIdentityInformation != null;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000BD140 File Offset: 0x000BB340
		public bool TryGetRecipientIdentity(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out RecipientIdentity recipientIdentity)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "TryGetADRecipient called for sid: {0}", sid);
			recipientIdentity = null;
			ADIdentityInformation adidentityInformation = null;
			if (this.TryGet(sid, adRecipientSessionContext, out adidentityInformation) && adidentityInformation is RecipientIdentity)
			{
				recipientIdentity = (RecipientIdentity)adidentityInformation;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "ADIdentityInformationCache.TryGetRecipientIdentity. ADRecipient not found for sid: {0}.", sid);
			}
			return recipientIdentity != null;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000BD1A4 File Offset: 0x000BB3A4
		public bool TryGetRecipientIdentity(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext, out RecipientIdentity recipientIdentity)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "TryGetRecipientIdentityBySmtpAddress called for smtpAddress: {0}", smtpAddress);
			recipientIdentity = null;
			if (!ADIdentityInformationCache.TryGetFromRecipientIdentityCache(smtpAddress, adRecipientSessionContext.OrganizationPrefix, out recipientIdentity) && !this.TryLookupAndAddRecipient(smtpAddress, adRecipientSessionContext, out recipientIdentity))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "Recipient for smtpAddress {0} was not found or was not ADRecipient", smtpAddress);
			}
			return recipientIdentity != null;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000BD204 File Offset: 0x000BB404
		public bool TryGetUserIdentity(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out UserIdentity userIdentity)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "TryGetUserIdentity called for sid: {0}", sid);
			userIdentity = null;
			RecipientIdentity recipientIdentity = null;
			if (this.TryGetRecipientIdentity(sid, adRecipientSessionContext, out recipientIdentity) && recipientIdentity is UserIdentity)
			{
				userIdentity = (UserIdentity)recipientIdentity;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "ADIdentityInformationCache.TryGetUserIdentity. ADUser not found for sid: {0}.", sid);
			}
			return userIdentity != null;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000BD268 File Offset: 0x000BB468
		public bool TryGetUserIdentity(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext, out UserIdentity userIdentity)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "TryGetRecipientIdentityBySmtpAddress called for smtpAddress: {0}", smtpAddress);
			userIdentity = null;
			RecipientIdentity recipientIdentity = null;
			if (this.TryGetRecipientIdentity(smtpAddress, adRecipientSessionContext, out recipientIdentity) && recipientIdentity is UserIdentity)
			{
				userIdentity = (UserIdentity)recipientIdentity;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ADIdentityInformationCache.TryGetUserIdentity. ADUser not found for smtp address: {0}.", smtpAddress);
			}
			return userIdentity != null;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000BD2CC File Offset: 0x000BB4CC
		public bool TryGetUserIdentity(Guid mailboxGuid, ADRecipientSessionContext adRecipientSessionContext, out UserIdentity userIdentity)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Guid>((long)this.GetHashCode(), "TryGetUserIdentity called for mailboxGuid: {0}", mailboxGuid);
			userIdentity = null;
			if (mailboxGuid == Guid.Empty)
			{
				return false;
			}
			RecipientIdentity recipientIdentity = null;
			if (ADIdentityInformationCache.TryGetFromRecipientIdentityCache(mailboxGuid, adRecipientSessionContext.OrganizationPrefix, out recipientIdentity))
			{
				userIdentity = (recipientIdentity as UserIdentity);
				if (userIdentity == null)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ADIdentity is not a UserIdentity.  mailboxGuid: {0}", mailboxGuid);
				}
			}
			else
			{
				IRecipientSession adrecipientSession = adRecipientSessionContext.GetADRecipientSession();
				ADRecipient adRecipient;
				if (this.TryFindRecipient(mailboxGuid, adrecipientSession, out adRecipient) && UserIdentity.TryCreate(adRecipient, out userIdentity))
				{
					this.AddToRecipientCaches(userIdentity, null, null, userIdentity.ADUser.ExchangeGuid, userIdentity.ADUser.ArchiveGuid);
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Recipient for mailboxGuid {0} was not found or was not ADUser", mailboxGuid);
				}
			}
			return userIdentity != null;
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000BD39C File Offset: 0x000BB59C
		public UserIdentity GetUserIdentity(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "GetUserIdentity called for sid: {0}", sid);
			UserIdentity result = null;
			if (this.TryGetUserIdentity(sid, adRecipientSessionContext, out result))
			{
				return result;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Unable to get UserIdentity for sid: {0}", sid);
			throw new InvalidUserSidException();
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000BD3EC File Offset: 0x000BB5EC
		public UserIdentity GetUserIdentity(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "GetUserIdentityBySmtpAddress called for smtpAddress: {0}", smtpAddress);
			UserIdentity result = null;
			if (!this.TryGetUserIdentity(smtpAddress, adRecipientSessionContext, out result))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "UserIdentity for smtpAddress {0} was not found or was not ADUser", smtpAddress);
				throw new NonExistentMailboxException((CoreResources.IDs)4088802584U, smtpAddress);
			}
			return result;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000BD448 File Offset: 0x000BB648
		public bool TryGetADUser(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out ADUser adUser)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "TryGetADUser called for sid: {0}", sid);
			adUser = null;
			UserIdentity userIdentity = null;
			if (this.TryGetUserIdentity(sid, adRecipientSessionContext, out userIdentity))
			{
				adUser = userIdentity.ADUser;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>(0L, "ADIdentityInformationCache.GetADUser. ADUser not found for sid: {0}.", sid);
			}
			return adUser != null;
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000BD4A4 File Offset: 0x000BB6A4
		public bool TryGetADUser(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext, out ADUser adUser)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "TryGetADUser called for smtpAddress: {0}", smtpAddress);
			adUser = null;
			RecipientIdentity recipientIdentity = null;
			if (this.TryGetRecipientIdentity(smtpAddress, adRecipientSessionContext, out recipientIdentity) && recipientIdentity is UserIdentity)
			{
				adUser = ((UserIdentity)recipientIdentity).ADUser;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "ADIdentityInformationCache.TryGetADUser. ADUser not found for smtpAddress: {0}.", smtpAddress);
			}
			return adUser != null;
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000BD50C File Offset: 0x000BB70C
		public bool TryGetADUser(Guid mailboxGuid, ADRecipientSessionContext adRecipientSessionContext, out ADUser adUser)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Guid>((long)this.GetHashCode(), "TryGetADUser called for exchangeGuid: {0}", mailboxGuid);
			adUser = null;
			UserIdentity userIdentity = null;
			if (this.TryGetUserIdentity(mailboxGuid, adRecipientSessionContext, out userIdentity))
			{
				adUser = userIdentity.ADUser;
			}
			else
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Guid>(0L, "ADIdentityInformationCache.GetADUser. ADUser not found for mailboxGuid: {0}.", mailboxGuid);
			}
			return adUser != null;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x000BD565 File Offset: 0x000BB765
		private static string BuildSidKey(SecurityIdentifier sid, string orgPrefix)
		{
			return orgPrefix.ToString() + "SD" + sid.ToString();
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x000BD57D File Offset: 0x000BB77D
		private static string BuildSmtpAddressKey(string smtpAddress, string orgPrefix)
		{
			return orgPrefix.ToString() + "SA" + smtpAddress;
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000BD590 File Offset: 0x000BB790
		private static string BuildExchangeGuidKey(Guid exchangeGuid, string orgPrefix)
		{
			return orgPrefix.ToString() + "EG" + exchangeGuid.ToString();
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000BD5B0 File Offset: 0x000BB7B0
		private static bool TryGetFromRecipientIdentityCache(Guid exchangeGuid, string orgPrefix, out RecipientIdentity recipientIdentity)
		{
			string key = ADIdentityInformationCache.BuildExchangeGuidKey(exchangeGuid, orgPrefix);
			ADIdentityInformation adidentityInformation = null;
			ADIdentityInformationCache.singleton.cache.TryGetValue(key, out adidentityInformation);
			recipientIdentity = (adidentityInformation as RecipientIdentity);
			return recipientIdentity != null;
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000BD5EC File Offset: 0x000BB7EC
		private static bool TryGetFromRecipientIdentityCache(string smtpAddress, string orgPrefix, out RecipientIdentity recipientIdentity)
		{
			string key = ADIdentityInformationCache.BuildSmtpAddressKey(smtpAddress, orgPrefix);
			ADIdentityInformation adidentityInformation = null;
			ADIdentityInformationCache.singleton.cache.TryGetValue(key, out adidentityInformation);
			recipientIdentity = (adidentityInformation as RecipientIdentity);
			return recipientIdentity != null;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000BD650 File Offset: 0x000BB850
		private bool TryLookupAndAddRecipient(SecurityIdentifier sid, ADRecipientSessionContext adRecipientSessionContext, out ADIdentityInformation recipientInfo)
		{
			recipientInfo = null;
			IRecipientSession adRecipientSession = adRecipientSessionContext.GetADRecipientSession();
			ADRecipient adRecipient = null;
			bool foundRecipient = false;
			RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.RecipientLookupLatency, delegate()
			{
				foundRecipient = Directory.TryFindRecipient(sid, adRecipientSession, out adRecipient);
			});
			if (!foundRecipient)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddRecipient] No user found in the directory with sid: {0}", sid);
				return false;
			}
			RecipientIdentity recipientIdentity = null;
			if (!RecipientIdentity.TryCreate(adRecipient, out recipientIdentity))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient, SecurityIdentifier>((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddRecipient] ADRecipient {0} with sid {1} is not ADUser or ADContact", adRecipient, sid);
				return false;
			}
			recipientInfo = recipientIdentity;
			this.AddToRecipientCaches(recipientIdentity, null, null, Guid.Empty, Guid.Empty);
			return true;
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000BD740 File Offset: 0x000BB940
		private bool TryLookupAndAddRecipient(string smtpAddress, ADRecipientSessionContext adRecipientSessionContext, out RecipientIdentity recipientInfo)
		{
			recipientInfo = null;
			ADRecipient adRecipient = null;
			IRecipientSession adRecipientSession = adRecipientSessionContext.GetADRecipientSession();
			bool foundRecipient = false;
			RequestDetailsLogger.Current.TrackLatency(ServiceLatencyMetadata.RecipientLookupLatency, delegate()
			{
				foundRecipient = Directory.TryFindRecipient(smtpAddress, adRecipientSession, out adRecipient);
			});
			if (!foundRecipient)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddRecipient] No user found in the directory with smtp address: {0}", smtpAddress);
				return false;
			}
			RecipientIdentity recipientIdentity = null;
			if (!RecipientIdentity.TryCreate(adRecipient, out recipientIdentity))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ADRecipient, string>((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddRecipient] ADRecipient {0} with smtp address {1} is not ADUser or ADContact", adRecipient, smtpAddress);
				return false;
			}
			recipientInfo = recipientIdentity;
			this.AddToRecipientCaches(recipientIdentity, null, smtpAddress, Guid.Empty, Guid.Empty);
			return true;
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000BD80C File Offset: 0x000BBA0C
		private void AddToCache(string key, ADIdentityInformation identity)
		{
			try
			{
				if (this.useCache)
				{
					this.cache.TryAddLimitedSliding(key, identity, this.absoluteTimeout, this.slidingTimeout);
				}
			}
			catch (DuplicateKeyException)
			{
			}
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000BD850 File Offset: 0x000BBA50
		private RecipientIdentity AddToRecipientCaches(RecipientIdentity recipientIdentity, SecurityIdentifier sid, string smtpAddress, Guid exchangeGuid, Guid archiveGuid)
		{
			Guid guid = Guid.Empty;
			Guid guid2 = Guid.Empty;
			SecurityIdentifier securityIdentifier = (sid != null) ? sid : recipientIdentity.Sid;
			if (securityIdentifier != null)
			{
				string key = ADIdentityInformationCache.BuildSidKey(securityIdentifier, recipientIdentity.OrganizationPrefix);
				this.AddToCache(key, recipientIdentity);
			}
			string text = (smtpAddress != null) ? smtpAddress : recipientIdentity.SmtpAddress;
			if (!string.IsNullOrEmpty(text))
			{
				string key2 = ADIdentityInformationCache.BuildSmtpAddressKey(text, recipientIdentity.OrganizationPrefix);
				this.AddToCache(key2, recipientIdentity);
			}
			if (exchangeGuid != Guid.Empty)
			{
				guid = exchangeGuid;
			}
			else
			{
				UserIdentity userIdentity = recipientIdentity as UserIdentity;
				if (userIdentity != null)
				{
					ADUser aduser = userIdentity.ADUser;
					guid = aduser.ExchangeGuid;
				}
			}
			if (guid != Guid.Empty)
			{
				string key3 = ADIdentityInformationCache.BuildExchangeGuidKey(guid, recipientIdentity.OrganizationPrefix);
				this.AddToCache(key3, recipientIdentity);
			}
			if (archiveGuid != Guid.Empty)
			{
				guid2 = archiveGuid;
			}
			else
			{
				UserIdentity userIdentity2 = recipientIdentity as UserIdentity;
				if (userIdentity2 != null)
				{
					ADUser aduser2 = userIdentity2.ADUser;
					guid2 = aduser2.ArchiveGuid;
				}
			}
			if (guid2 != Guid.Empty)
			{
				string key4 = ADIdentityInformationCache.BuildExchangeGuidKey(guid2, recipientIdentity.OrganizationPrefix);
				this.AddToCache(key4, recipientIdentity);
			}
			return recipientIdentity;
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000BD974 File Offset: 0x000BBB74
		public void RemoveUserIdentity(SecurityIdentifier sid, string orgPrefix)
		{
			if (this.useCache)
			{
				string text = ADIdentityInformationCache.BuildSidKey(sid, orgPrefix);
				ADIdentityInformation adidentityInformation = this.cache.Remove(text);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "RemoveUserIdentity with sid key {0}: {1}", text, adidentityInformation != null);
				UserIdentity userIdentity = adidentityInformation as UserIdentity;
				if (userIdentity != null)
				{
					string smtpAddress = userIdentity.SmtpAddress;
					if (!string.IsNullOrEmpty(smtpAddress))
					{
						string text2 = ADIdentityInformationCache.BuildSmtpAddressKey(smtpAddress, userIdentity.OrganizationPrefix);
						adidentityInformation = this.cache.Remove(text2);
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "RemoveUserIdentity with smtp key {0}: {1}", text2, adidentityInformation != null);
					}
					ADUser aduser = userIdentity.ADUser;
					Guid exchangeGuid = aduser.ExchangeGuid;
					Guid archiveGuid = aduser.ArchiveGuid;
					if (exchangeGuid != Guid.Empty)
					{
						string text3 = ADIdentityInformationCache.BuildExchangeGuidKey(exchangeGuid, userIdentity.OrganizationPrefix);
						adidentityInformation = this.cache.Remove(text3);
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "RemoveUserIdentity with exchangeGuid key {0}: {1}", text3, adidentityInformation != null);
					}
					if (archiveGuid != Guid.Empty)
					{
						string text4 = ADIdentityInformationCache.BuildExchangeGuidKey(archiveGuid, userIdentity.OrganizationPrefix);
						adidentityInformation = this.cache.Remove(text4);
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "RemoveUserIdentity with archiveGuid key {0}: {1}", text4, adidentityInformation != null);
					}
				}
			}
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000BDAC8 File Offset: 0x000BBCC8
		public void RemoveExtraUserIdentity(string smtpAddress, string orgPrefix)
		{
			if (this.useCache && !string.IsNullOrEmpty(smtpAddress))
			{
				string text = ADIdentityInformationCache.BuildSmtpAddressKey(smtpAddress, orgPrefix);
				ADIdentityInformation adidentityInformation = this.cache.Remove(text);
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "RemoveExtraUserIdentity with smtp key {0}: {1}", text, adidentityInformation != null);
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000BDB1C File Offset: 0x000BBD1C
		private bool TryGetFromCache(SecurityIdentifier sid, string orgPrefix, out ADIdentityInformation adIdentityInformation)
		{
			string key = ADIdentityInformationCache.BuildSidKey(sid, orgPrefix);
			return this.cache.TryGetValue(key, out adIdentityInformation);
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000BDB3E File Offset: 0x000BBD3E
		private bool TryFindRecipient(Guid mailboxGuid, IRecipientSession recipientSession, out ADRecipient adRecipient)
		{
			adRecipient = recipientSession.FindByExchangeGuidIncludingAlternate(mailboxGuid);
			if (adRecipient == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<Guid>((long)this.GetHashCode(), "ADRecipient for mailboxGuid {0} was not found", mailboxGuid);
			}
			return adRecipient != null;
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000BDB6C File Offset: 0x000BBD6C
		private bool TryLookupAndAddComputer(SecurityIdentifier computerSid, out ADIdentityInformation caller)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddComputer] Called with sid: {0}", computerSid);
			ADComputer adcomputer;
			if (computerSid.IsWellKnown(WellKnownSidType.LocalSystemSid) || computerSid.IsWellKnown(WellKnownSidType.NetworkServiceSid))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddComputer] LocalSystem or NetworkService sid passed.");
				adcomputer = this.rootConfigSession.Member.FindLocalComputer();
			}
			else
			{
				adcomputer = this.rootConfigSession.Member.FindComputerBySid(computerSid);
			}
			if (adcomputer == null)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "[ADIdentityInformationCache::TryLookupAndAddComputer] No computer found in the directory with sid: {0}", computerSid);
				caller = null;
				return false;
			}
			caller = new ComputerIdentity(adcomputer);
			string key = ADIdentityInformationCache.BuildSidKey(computerSid, string.Empty);
			this.AddToCache(key, caller);
			return true;
		}

		// Token: 0x04001D9C RID: 7580
		private const string ExchangeGuidKeyId = "EG";

		// Token: 0x04001D9D RID: 7581
		private const string SmtpAddressKeyId = "SA";

		// Token: 0x04001D9E RID: 7582
		private const string SidKeyId = "SD";

		// Token: 0x04001D9F RID: 7583
		private static readonly TimeSpan DefaultAbsoluteTimeout = TimeSpan.FromMinutes(15.0);

		// Token: 0x04001DA0 RID: 7584
		private static readonly TimeSpan DefaultSlidingTimeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x04001DA1 RID: 7585
		private static ADIdentityInformationCache singleton;

		// Token: 0x04001DA2 RID: 7586
		private TimeSpan absoluteTimeout;

		// Token: 0x04001DA3 RID: 7587
		private TimeSpan slidingTimeout;

		// Token: 0x04001DA4 RID: 7588
		private bool useCache;

		// Token: 0x04001DA5 RID: 7589
		private ExactTimeoutCache<string, ADIdentityInformation> cache;

		// Token: 0x04001DA6 RID: 7590
		private LazyMember<ITopologyConfigurationSession> rootConfigSession = new LazyMember<ITopologyConfigurationSession>(delegate()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 26, "rootConfigSession", "f:\\15.00.1497\\sources\\dev\\services\\src\\Services\\Server\\Types\\ADIdentityInformationCache.cs");
			topologyConfigurationSession.UseConfigNC = false;
			topologyConfigurationSession.UseGlobalCatalog = true;
			return topologyConfigurationSession;
		});
	}
}
