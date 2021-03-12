using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B1 RID: 433
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SmsRecipientInfoCache : IDisposable
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x00043E18 File Offset: 0x00042018
		// (set) Token: 0x0600108E RID: 4238 RVA: 0x00043E20 File Offset: 0x00042020
		private RecipientInfoCache RecipientInfoCache { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x00043E29 File Offset: 0x00042029
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x00043E31 File Offset: 0x00042031
		private List<RecipientInfoCacheEntry> CacheEntries { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00043E3A File Offset: 0x0004203A
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x00043E42 File Offset: 0x00042042
		private MailboxSession MailboxSession { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00043E4B File Offset: 0x0004204B
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00043E53 File Offset: 0x00042053
		private Trace Tracer { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x00043E5C File Offset: 0x0004205C
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00043E64 File Offset: 0x00042064
		private bool IsDirty { get; set; }

		// Token: 0x06001097 RID: 4247 RVA: 0x00043E6D File Offset: 0x0004206D
		public static SmsRecipientInfoCache Create(MailboxSession mailboxSession, Trace tracer)
		{
			return new SmsRecipientInfoCache(mailboxSession, tracer);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00043E78 File Offset: 0x00042078
		public SmsRecipientInfoCache(MailboxSession mailboxSession, Trace tracer)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (tracer == null)
			{
				throw new ArgumentNullException("tracer");
			}
			this.MailboxSession = mailboxSession;
			this.Tracer = tracer;
			this.RecipientInfoCache = RecipientInfoCache.Create(this.MailboxSession, "SMS.RecipientInfoCache");
			try
			{
				this.CacheEntries = this.RecipientInfoCache.Load("AutoCompleteCache");
			}
			catch (CorruptDataException)
			{
				this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "The SMS recipient cache is corrupt in {0}'s mailbox", mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00043F20 File Offset: 0x00042120
		public RecipientInfoCacheEntry LookUp(string number)
		{
			if (this.CacheEntries == null || this.CacheEntries.Count == 0)
			{
				return null;
			}
			E164Number number2;
			if (!E164Number.TryParse(number, out number2))
			{
				return null;
			}
			foreach (RecipientInfoCacheEntry recipientInfoCacheEntry in this.CacheEntries)
			{
				E164Number number3;
				if (!E164Number.TryParse(recipientInfoCacheEntry.RoutingAddress, out number3))
				{
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "There's an invalid phone number in the SMS recipient cache of {0}'s mailbox", this.MailboxSession.MailboxOwner.MailboxInfo.DisplayName);
				}
				else if (SmsRecipientInfoCache.NumbersMatch(number2, number3))
				{
					return recipientInfoCacheEntry;
				}
			}
			return null;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00043FE0 File Offset: 0x000421E0
		public void AddRecipient(Participant recipient)
		{
			if (this.CacheEntries == null)
			{
				this.CacheEntries = new List<RecipientInfoCacheEntry>(150);
			}
			this.IsDirty |= this.AddParticipant(recipient);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00044010 File Offset: 0x00042210
		private bool AddParticipant(Participant participant)
		{
			E164Number e164Number;
			if (!E164Number.TryParse(participant.EmailAddress, out e164Number))
			{
				return false;
			}
			for (int i = 0; i < this.CacheEntries.Count; i++)
			{
				RecipientInfoCacheEntry recipientInfoCacheEntry = this.CacheEntries[i];
				E164Number number;
				if (!E164Number.TryParse(recipientInfoCacheEntry.RoutingAddress, out number))
				{
					this.Tracer.TraceDebug<string>((long)this.GetHashCode(), "There's an invalid phone number in the SMS recipient cache of {0}'s mailbox", this.MailboxSession.MailboxOwner.MailboxInfo.DisplayName);
				}
				else if (SmsRecipientInfoCache.NumbersMatch(e164Number, number))
				{
					this.CacheEntries[i] = SmsRecipientInfoCache.CreateCacheEntry(participant, e164Number.Number);
					return true;
				}
			}
			if (this.CacheEntries.Count < 150)
			{
				this.CacheEntries.Add(SmsRecipientInfoCache.CreateCacheEntry(participant, e164Number.Number));
				return true;
			}
			int index = 0;
			for (int j = 1; j < this.CacheEntries.Count; j++)
			{
				if (this.CacheEntries[j].DateTimeTicks < this.CacheEntries[index].DateTimeTicks)
				{
					index = j;
				}
			}
			this.CacheEntries[index] = SmsRecipientInfoCache.CreateCacheEntry(participant, e164Number.Number);
			return true;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00044140 File Offset: 0x00042340
		private static RecipientInfoCacheEntry CreateCacheEntry(Participant participant, string number)
		{
			ParticipantOrigin origin = participant.Origin;
			return new RecipientInfoCacheEntry(participant.DisplayName, null, number, null, participant.RoutingType, AddressOrigin.OneOff, 0, null, EmailAddressIndex.None, null, number);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00044170 File Offset: 0x00042370
		private static bool NumbersMatch(E164Number number1, E164Number number2)
		{
			return number1 == number2 || ((string.IsNullOrEmpty(number1.CountryCode) || string.IsNullOrEmpty(number2.CountryCode)) && string.Equals(number1.SignificantNumber, number2.SignificantNumber, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000441AB File Offset: 0x000423AB
		private void Dispose(bool disposing)
		{
			if (this.RecipientInfoCache != null)
			{
				this.RecipientInfoCache.Dispose();
				this.RecipientInfoCache = null;
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000441C7 File Offset: 0x000423C7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000441D6 File Offset: 0x000423D6
		public void Commit()
		{
			if (this.IsDirty)
			{
				this.RecipientInfoCache.Save(this.CacheEntries, "AutoCompleteCache", 150);
			}
		}

		// Token: 0x040008CD RID: 2253
		private const string ConfigurationName = "SMS.RecipientInfoCache";

		// Token: 0x040008CE RID: 2254
		private const short CacheSize = 150;
	}
}
