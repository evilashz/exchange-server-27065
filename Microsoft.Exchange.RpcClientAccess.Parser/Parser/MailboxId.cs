using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F8 RID: 760
	internal struct MailboxId : IEquatable<MailboxId>
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x00030630 File Offset: 0x0002E830
		public MailboxId(string mailboxLegacyDn)
		{
			this.isLegacyDn = true;
			this.mailboxLegacyDn = mailboxLegacyDn;
			this.mailboxGuid = Guid.Empty;
			this.databaseGuid = Guid.Empty;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00030656 File Offset: 0x0002E856
		public MailboxId(Guid mailboxGuid, Guid databaseGuid)
		{
			this.isLegacyDn = false;
			this.mailboxGuid = mailboxGuid;
			this.databaseGuid = databaseGuid;
			this.mailboxLegacyDn = null;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001194 RID: 4500 RVA: 0x00030674 File Offset: 0x0002E874
		public bool IsLegacyDn
		{
			get
			{
				return this.isLegacyDn;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0003067C File Offset: 0x0002E87C
		public string LegacyDn
		{
			get
			{
				return this.mailboxLegacyDn;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x00030684 File Offset: 0x0002E884
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0003068C File Offset: 0x0002E88C
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00030694 File Offset: 0x0002E894
		public uint SerializedLength()
		{
			if (this.isLegacyDn)
			{
				return (uint)(this.mailboxLegacyDn.Length + 1);
			}
			return 32U;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000306B0 File Offset: 0x0002E8B0
		public bool Equals(MailboxId other)
		{
			if (this.isLegacyDn != other.isLegacyDn)
			{
				return false;
			}
			if (this.isLegacyDn)
			{
				return this.mailboxLegacyDn == other.mailboxLegacyDn;
			}
			return this.databaseGuid == other.databaseGuid && this.mailboxGuid == other.mailboxGuid;
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00030711 File Offset: 0x0002E911
		public override bool Equals(object obj)
		{
			return obj is MailboxId && this.Equals((MailboxId)obj);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003072C File Offset: 0x0002E92C
		public override int GetHashCode()
		{
			if (this.isLegacyDn)
			{
				return this.mailboxLegacyDn.GetHashCode();
			}
			return this.mailboxGuid.GetHashCode() ^ this.databaseGuid.GetHashCode();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00030776 File Offset: 0x0002E976
		public override string ToString()
		{
			return "MailboxId: " + this.ToBareString();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00030788 File Offset: 0x0002E988
		public string ToBareString()
		{
			if (this.isLegacyDn)
			{
				return string.Format("Dn[{0}]", this.mailboxLegacyDn);
			}
			return string.Format("Mailbox[{0}] Database[{1}]", this.mailboxGuid, this.databaseGuid);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000307C3 File Offset: 0x0002E9C3
		internal void Serialize(Writer writer)
		{
			if (this.isLegacyDn)
			{
				writer.WriteAsciiString(this.mailboxLegacyDn, StringFlags.IncludeNull);
				return;
			}
			writer.WriteGuid(this.mailboxGuid);
			writer.WriteGuid(this.databaseGuid);
		}

		// Token: 0x0400098F RID: 2447
		private readonly bool isLegacyDn;

		// Token: 0x04000990 RID: 2448
		private readonly Guid mailboxGuid;

		// Token: 0x04000991 RID: 2449
		private readonly Guid databaseGuid;

		// Token: 0x04000992 RID: 2450
		private readonly string mailboxLegacyDn;
	}
}
