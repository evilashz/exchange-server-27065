using System;
using System.Globalization;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	internal class MapiSubmissionInfo : SubmissionInfo
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00004066 File Offset: 0x00002266
		public MapiSubmissionInfo(string serverDN, Guid mailboxGuid, byte[] entryId, byte[] parentEntryId, long eventCounter, string serverFqdn, IPAddress networkAddress, Guid mdbGuid, bool isShadowSupported, DateTime originalCreateTime, string mailboxHopLatency) : base(serverDN, serverFqdn, networkAddress, mdbGuid, isShadowSupported, originalCreateTime, mailboxHopLatency)
		{
			this.mailboxGuid = mailboxGuid;
			this.entryId = entryId;
			this.parentEntryId = parentEntryId;
			this.eventCounter = eventCounter;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00004099 File Offset: 0x00002299
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000040A1 File Offset: 0x000022A1
		public long EventCounter
		{
			get
			{
				return this.eventCounter;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000040A9 File Offset: 0x000022A9
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000040B1 File Offset: 0x000022B1
		public byte[] ParentEntryId
		{
			get
			{
				return this.parentEntryId;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000040B9 File Offset: 0x000022B9
		public override bool IsShadowSubmission
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000040BC File Offset: 0x000022BC
		public TransportMiniRecipient SenderAdEntry
		{
			get
			{
				return this.senderAdEntry;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000040C4 File Offset: 0x000022C4
		public override SubmissionItem CreateSubmissionItem(MailItemSubmitter context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000040CC File Offset: 0x000022CC
		public override OrganizationId GetOrganizationId()
		{
			if (this.MailboxGuid.Equals(Guid.Empty) || this.senderAdEntry == null)
			{
				SubmissionInfo.Diag.TraceDebug(0L, "Using ForestWideOrgId scope for PF replication mail");
				return OrganizationId.ForestWideOrgId;
			}
			return this.senderAdEntry.OrganizationId;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004118 File Offset: 0x00002318
		public override SenderGuidTraceFilter GetTraceFilter()
		{
			return new SenderGuidTraceFilter(base.MdbGuid, this.mailboxGuid);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000412C File Offset: 0x0000232C
		public override string GetPoisonId()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				this.mailboxGuid.Equals(Guid.Empty) ? base.MdbGuid : this.mailboxGuid,
				this.eventCounter
			});
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000418C File Offset: 0x0000238C
		public void LoadAdRawEntry(TenantPartitionHint tenantPartitionHint)
		{
			if (Guid.Empty.Equals(this.mailboxGuid))
			{
				SubmissionInfo.Diag.TraceError(0L, "Mailbox GUID was empty, unable to load AD entry.");
				return;
			}
			try
			{
				IRecipientSession adrecipientSession = this.GetADRecipientSession(tenantPartitionHint);
				this.senderAdEntry = adrecipientSession.FindByExchangeGuidIncludingAlternate<TransportMiniRecipient>(this.mailboxGuid);
				if (this.senderAdEntry != null)
				{
					ADObjectId adobjectId = (ADObjectId)this.senderAdEntry[IADMailStorageSchema.Database];
					if (adobjectId != null)
					{
						ADObjectId adobjectId2 = ADObjectIdResolutionHelper.ResolveDN(adobjectId);
						base.DatabaseName = adobjectId2.Name;
					}
				}
			}
			catch (NonUniqueRecipientException)
			{
				SubmissionInfo.Diag.TraceError<Guid>(0L, "Multiple objects with Mailbox Guid {0} were found.", this.mailboxGuid);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000423C File Offset: 0x0000243C
		public string GetSenderEmailAddress()
		{
			if (this.senderAdEntry == null)
			{
				return string.Empty;
			}
			SmtpAddress smtpAddress = (SmtpAddress)this.senderAdEntry[ADRecipientSchema.PrimarySmtpAddress];
			if (smtpAddress.IsValidAddress)
			{
				return smtpAddress.ToString();
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)this.senderAdEntry[ADRecipientSchema.EmailAddresses];
			if (proxyAddressCollection == null || 0 >= proxyAddressCollection.Count)
			{
				return string.Empty;
			}
			ProxyAddress proxyAddress = proxyAddressCollection.Find(new Predicate<ProxyAddress>(this.IsSmtpAddress));
			if (null != proxyAddress)
			{
				return proxyAddress.ToString();
			}
			return proxyAddressCollection[0].ToString();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000042DB File Offset: 0x000024DB
		public MultiValuedProperty<CultureInfo> GetSenderLocales()
		{
			if (this.senderAdEntry != null)
			{
				return (MultiValuedProperty<CultureInfo>)this.senderAdEntry[ADUserSchema.Languages];
			}
			return new MultiValuedProperty<CultureInfo>();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004300 File Offset: 0x00002500
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Event {0}, mailbox {1}, mdb {2}", new object[]
			{
				this.eventCounter,
				this.mailboxGuid,
				base.MdbGuid
			});
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004350 File Offset: 0x00002550
		private IRecipientSession GetADRecipientSession(TenantPartitionHint tenantPartitionHint)
		{
			if (this.recipientSession == null)
			{
				if (tenantPartitionHint == null)
				{
					this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 439, "GetADRecipientSession", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\StoreDriver\\SubmissionInfo.cs");
				}
				else
				{
					this.recipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantPartitionHint(tenantPartitionHint), 445, "GetADRecipientSession", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\StoreDriver\\SubmissionInfo.cs");
				}
			}
			return this.recipientSession;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000043BB File Offset: 0x000025BB
		private bool IsSmtpAddress(ProxyAddress address)
		{
			return address.Prefix.Equals(ProxyAddressPrefix.Smtp);
		}

		// Token: 0x04000063 RID: 99
		protected TransportMiniRecipient senderAdEntry;

		// Token: 0x04000064 RID: 100
		private readonly Guid mailboxGuid;

		// Token: 0x04000065 RID: 101
		private readonly byte[] entryId;

		// Token: 0x04000066 RID: 102
		private readonly byte[] parentEntryId;

		// Token: 0x04000067 RID: 103
		private readonly long eventCounter;

		// Token: 0x04000068 RID: 104
		private IRecipientSession recipientSession;
	}
}
