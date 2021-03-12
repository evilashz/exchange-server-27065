using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000019 RID: 25
	internal class LegacyConfig
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000A109 File Offset: 0x00008309
		internal static LegacyConfig Instance
		{
			get
			{
				if (LegacyConfig.instance == null)
				{
					LegacyConfig.instance = new LegacyConfig();
				}
				return LegacyConfig.instance;
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000A121 File Offset: 0x00008321
		private LegacyConfig()
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000A140 File Offset: 0x00008340
		internal bool ShouldJournal(RoutingAddress recipient, IDictionary<string, object> recipientProperties, out string journalRecipientEmail, out Guid journalRecipientGuid)
		{
			journalRecipientEmail = null;
			journalRecipientGuid = Guid.Empty;
			ADObjectId homeMdb = this.GetHomeMdb(recipientProperties);
			if (homeMdb == null)
			{
				return false;
			}
			ADRecipient adrecipient = this.LookupMdbJournalRecipCached(homeMdb);
			if (adrecipient == null)
			{
				return false;
			}
			foreach (ProxyAddress proxyAddress in adrecipient.EmailAddresses)
			{
				if (proxyAddress.Prefix == ProxyAddressPrefix.Smtp)
				{
					journalRecipientEmail = proxyAddress.ToString();
					break;
				}
			}
			if (string.IsNullOrEmpty(journalRecipientEmail))
			{
				ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "No SMTP proxy address found for journal recipient AD object. The object is corrupt");
				return false;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<string, Guid>((long)this.GetHashCode(), "Journal-recipient address:{0}, Guid:{0}", journalRecipientEmail, journalRecipientGuid);
			journalRecipientEmail = journalRecipientEmail.Substring("SMTP:".Length);
			journalRecipientGuid = adrecipient.Guid;
			return true;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000A234 File Offset: 0x00008434
		private ADObjectId GetHomeMdb(IDictionary<string, object> recipientProperties)
		{
			object obj = null;
			if (recipientProperties.TryGetValue("Microsoft.Exchange.Transport.DirectoryData.Database", out obj))
			{
				return (ADObjectId)obj;
			}
			return null;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000A2B4 File Offset: 0x000084B4
		private ADRecipient LookupMdbJournalRecipCached(ADObjectId mdbAdObjectId)
		{
			ADRecipient adrecipient = null;
			ExTraceGlobals.JournalingTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Searching for journal recipient for MDB, Guid:{0}", mdbAdObjectId);
			mdbAdObjectId = ADObjectIdResolutionHelper.ResolveDN(mdbAdObjectId);
			if (this.mdbConfigCache.TryGetValue(mdbAdObjectId.DistinguishedName, out adrecipient))
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "MDB lookup completed from cache, journaling is {0} for MDB", (adrecipient == null) ? "disabled" : "enabled");
				return adrecipient;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "Cache miss for MDB, doing AD lookup");
			LegacyMailboxDatabase[] mdbObjects = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(mdbAdObjectId.GetPartitionId()), 195, "LookupMdbJournalRecipCached", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\Journaling\\Agent\\LegacyConfig.cs");
				mdbObjects = tenantOrTopologyConfigurationSession.Find<LegacyMailboxDatabase>(mdbAdObjectId, QueryScope.Base, null, null, 0);
			});
			if (mdbObjects == null || mdbObjects.Length != 1)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "No MDB objects located");
				return null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Found MDB object. Mailbox server: {0}, Storage group: {1}", mdbObjects[0].ServerName, mdbObjects[0].StorageGroupName);
			if (mdbObjects[0].JournalRecipient == null)
			{
				ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "MDB not enabled for journaling");
				this.mdbConfigCache.AddIgnoringDups(mdbAdObjectId.DistinguishedName, null);
				return null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "MDB is journaling enabled, locating journal recipient");
			adrecipient = this.FindRecipientByGuid(mdbObjects[0].JournalRecipient.ObjectGuid);
			if (adrecipient == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError<Guid>((long)this.GetHashCode(), "Recipient referenced by GUID: {0} does not exist. Treating as if MDB was not enabled for journaling", mdbObjects[0].JournalRecipient.ObjectGuid);
				this.mdbConfigCache.AddIgnoringDups(mdbAdObjectId.DistinguishedName, null);
				return null;
			}
			this.mdbConfigCache.AddIgnoringDups(mdbObjects[0].DistinguishedName, adrecipient);
			this.journalRecipCache.AddIgnoringDups(adrecipient.Guid, adrecipient);
			return adrecipient;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000A4A4 File Offset: 0x000086A4
		internal ADRecipient LookupJournalRecipientByGuidCached(Guid recipientGuid)
		{
			ADRecipient adrecipient = null;
			ExTraceGlobals.JournalingTracer.TraceDebug<Guid>((long)this.GetHashCode(), "Searching for recipient, Guid:{0}", recipientGuid);
			if (this.journalRecipCache.TryGetValue(recipientGuid, out adrecipient))
			{
				ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "Recipient found in cache, DN:{0}", adrecipient.DistinguishedName);
				return adrecipient;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug((long)this.GetHashCode(), "Recipient not found in cache. Doing AD lookup");
			adrecipient = this.FindRecipientByGuid(recipientGuid);
			if (adrecipient == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "Recipient not found in AD, maybe stale XEXCH50");
				return null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "Recipient found in AD, DN:{0}", adrecipient.DistinguishedName);
			this.journalRecipCache.AddIgnoringDups(recipientGuid, adrecipient);
			return adrecipient;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000A5A4 File Offset: 0x000087A4
		private ADRecipient FindRecipientByGuid(Guid recipientId)
		{
			ADRecipient recipient = null;
			ADNotificationAdapter.RunADOperation(delegate()
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 290, "FindRecipientByGuid", "f:\\15.00.1497\\sources\\dev\\MessagingPolicies\\src\\Journaling\\Agent\\LegacyConfig.cs");
				recipient = tenantOrRootOrgRecipientSession.FindByObjectGuid(recipientId);
			});
			if (recipient == null)
			{
				ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "Recipient not found");
				return null;
			}
			ExTraceGlobals.JournalingTracer.TraceDebug<string>((long)this.GetHashCode(), "Found recipient, DN:{0}", recipient.DistinguishedName);
			if (recipient.EmailAddresses == null || recipient.EmailAddresses.Count == 0)
			{
				ExTraceGlobals.JournalingTracer.TraceError((long)this.GetHashCode(), "No email addresses for recipient");
				return null;
			}
			return recipient;
		}

		// Token: 0x040000A8 RID: 168
		private static LegacyConfig instance;

		// Token: 0x040000A9 RID: 169
		private SimpleCache<string, ADRecipient> mdbConfigCache = new SimpleCache<string, ADRecipient>();

		// Token: 0x040000AA RID: 170
		private SimpleCache<Guid, ADRecipient> journalRecipCache = new SimpleCache<Guid, ADRecipient>();
	}
}
