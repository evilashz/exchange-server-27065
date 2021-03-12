using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal sealed class JunkEmailSettings : ItemPropertiesBase
	{
		// Token: 0x06000093 RID: 147 RVA: 0x00002910 File Offset: 0x00000B10
		public override void Apply(MrsPSHandler psHandler, MailboxSession mailboxSession)
		{
			JunkEmailRule junkEmailRule = mailboxSession.JunkEmailRule;
			if (this.TrustedSenderDomain != null)
			{
				this.AddEntriesToCollection(junkEmailRule.TrustedSenderDomainCollection, "TrustedSenderDomainCollection", mailboxSession.MailboxGuid, this.TrustedSenderDomain);
			}
			if (this.TrustedSenderEmail != null)
			{
				this.AddEntriesToCollection(junkEmailRule.TrustedSenderEmailCollection, "TrustedSenderEmailCollection", mailboxSession.MailboxGuid, this.TrustedSenderEmail);
			}
			if (this.BlockedSenderDomain != null)
			{
				this.AddEntriesToCollection(junkEmailRule.BlockedSenderDomainCollection, "BlockedSenderDomainCollection", mailboxSession.MailboxGuid, this.BlockedSenderDomain);
			}
			if (this.BlockedSenderEmail != null)
			{
				this.AddEntriesToCollection(junkEmailRule.BlockedSenderEmailCollection, "BlockedSenderEmailCollection", mailboxSession.MailboxGuid, this.BlockedSenderEmail);
			}
			if (this.TrustedRecipientDomain != null)
			{
				this.AddEntriesToCollection(junkEmailRule.TrustedRecipientDomainCollection, "TrustedRecipientDomainCollection", mailboxSession.MailboxGuid, this.TrustedRecipientDomain);
			}
			if (this.TrustedRecipientEmail != null)
			{
				this.AddEntriesToCollection(junkEmailRule.TrustedRecipientEmailCollection, "TrustedRecipientEmailCollection", mailboxSession.MailboxGuid, this.TrustedRecipientEmail);
			}
			if (this.TrustedContactsEmail != null)
			{
				IRecipientSession adrecipientSession = mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
				if (adrecipientSession == null)
				{
					string itemList = this.TrustedContactsEmail.Aggregate((string result, string email) => result + ", " + email);
					throw new MailboxSettingsJunkMailErrorPermanentException("TrustedContactsEmail", itemList, "error getting RecipientSession");
				}
				junkEmailRule.SynchronizeContactsCache();
				foreach (string email2 in this.TrustedContactsEmail)
				{
					junkEmailRule.AddTrustedContact(email2, adrecipientSession);
				}
			}
			if (this.SendAsEmail != null)
			{
				this.AddEntriesToCollection(junkEmailRule.TrustedSenderEmailCollection, "TrustedSenderEmailCollection", mailboxSession.MailboxGuid, this.SendAsEmail);
			}
			junkEmailRule.Save();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002AAC File Offset: 0x00000CAC
		private void AddEntriesToCollection(JunkEmailCollection collection, string collectionName, Guid mailboxGuid, string[] entriesToAdd)
		{
			foreach (string text in entriesToAdd)
			{
				JunkEmailCollection.ValidationProblem validationProblem = collection.TryAdd(text);
				if (validationProblem != JunkEmailCollection.ValidationProblem.NoError && validationProblem != JunkEmailCollection.ValidationProblem.Duplicate)
				{
					throw new MailboxSettingsJunkMailErrorPermanentException(collectionName, text, validationProblem.ToString());
				}
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002AF0 File Offset: 0x00000CF0
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002AF8 File Offset: 0x00000CF8
		[DataMember]
		public string[] TrustedSenderEmail { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002B01 File Offset: 0x00000D01
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00002B09 File Offset: 0x00000D09
		[DataMember]
		public string[] TrustedSenderDomain { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002B12 File Offset: 0x00000D12
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002B1A File Offset: 0x00000D1A
		[DataMember]
		public string[] TrustedRecipientEmail { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00002B23 File Offset: 0x00000D23
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00002B2B File Offset: 0x00000D2B
		[DataMember]
		public string[] TrustedRecipientDomain { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002B3C File Offset: 0x00000D3C
		[DataMember]
		public string[] TrustedContactsEmail { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002B45 File Offset: 0x00000D45
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002B4D File Offset: 0x00000D4D
		[DataMember]
		public string[] BlockedSenderEmail { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002B56 File Offset: 0x00000D56
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002B5E File Offset: 0x00000D5E
		[DataMember]
		public string[] BlockedSenderDomain { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002B67 File Offset: 0x00000D67
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002B6F File Offset: 0x00000D6F
		[DataMember]
		public string[] SendAsEmail { get; set; }
	}
}
