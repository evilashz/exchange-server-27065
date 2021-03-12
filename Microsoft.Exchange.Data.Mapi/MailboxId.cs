using System;
using Microsoft.Exchange.Data.Mapi.Common;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public sealed class MailboxId : MessageStoreId
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002640 File Offset: 0x00000840
		public string MailboxExchangeLegacyDn
		{
			get
			{
				return this.mailboxExchangeLegacyDn;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002648 File Offset: 0x00000848
		public Guid MailboxGuid
		{
			get
			{
				return this.mailboxGuid;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002650 File Offset: 0x00000850
		public DatabaseId MailboxDatabaseId
		{
			get
			{
				return this.mailboxDatabaseId;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002658 File Offset: 0x00000858
		public static MailboxId Parse(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new FormatException(Strings.ExceptionFormatNotSupported);
			}
			Guid guid;
			Guid guid2;
			if (73 == input.Length && '\\' == input[36] && GuidHelper.TryParseGuid(input.Substring(0, 36), out guid) && GuidHelper.TryParseGuid(input.Substring(37, 36), out guid2))
			{
				return new MailboxId(new DatabaseId(guid), guid2);
			}
			if (GuidHelper.TryParseGuid(input, out guid2))
			{
				return new MailboxId(null, guid2);
			}
			MailboxId result;
			try
			{
				result = new MailboxId((byte[])new MapiEntryId(input));
			}
			catch (FormatException)
			{
				result = new MailboxId(input);
			}
			return result;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002708 File Offset: 0x00000908
		public override bool Equals(object obj)
		{
			bool result;
			if (this.mailboxGuid != Guid.Empty)
			{
				result = this.Equals(obj as MailboxId);
			}
			else
			{
				result = base.Equals(obj);
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002744 File Offset: 0x00000944
		public override bool Equals(MapiObjectId other)
		{
			bool result = false;
			if (this.mailboxGuid != Guid.Empty)
			{
				MailboxId mailboxId = other as MailboxId;
				if (mailboxId != null)
				{
					result = this.MailboxGuid.Equals(mailboxId.MailboxGuid);
				}
			}
			else
			{
				result = base.Equals(other);
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002798 File Offset: 0x00000998
		public override int GetHashCode()
		{
			Guid guid = this.MailboxGuid;
			int hashCode;
			if (this.MailboxGuid != Guid.Empty)
			{
				hashCode = this.MailboxGuid.GetHashCode();
			}
			else
			{
				hashCode = base.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027E0 File Offset: 0x000009E0
		public override string ToString()
		{
			if (Guid.Empty != this.MailboxGuid)
			{
				return this.MailboxGuid.ToString();
			}
			if (this.MailboxExchangeLegacyDn != null)
			{
				return this.MailboxExchangeLegacyDn;
			}
			if (null != base.MapiEntryId)
			{
				return base.MapiEntryId.ToString();
			}
			return null;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000283E File Offset: 0x00000A3E
		public MailboxId()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002846 File Offset: 0x00000A46
		public MailboxId(byte[] bytes) : base(bytes)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000284F File Offset: 0x00000A4F
		public MailboxId(MapiEntryId entryId) : base(entryId)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002858 File Offset: 0x00000A58
		public MailboxId(string mailboxExchangeLegacyDn)
		{
			this.mailboxExchangeLegacyDn = mailboxExchangeLegacyDn;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002867 File Offset: 0x00000A67
		public MailboxId(DatabaseId mailboxDatabaseId, Guid mailboxGuid)
		{
			this.mailboxDatabaseId = mailboxDatabaseId;
			this.mailboxGuid = mailboxGuid;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000287D File Offset: 0x00000A7D
		internal MailboxId(MapiEntryId entryId, DatabaseId mailboxDatabaseId, Guid mailboxGuid, string mailboxExchangeLegacyDn) : base(entryId)
		{
			this.mailboxDatabaseId = mailboxDatabaseId;
			this.mailboxGuid = mailboxGuid;
			this.mailboxExchangeLegacyDn = mailboxExchangeLegacyDn;
		}

		// Token: 0x04000008 RID: 8
		private const int LiteralGuidLength = 36;

		// Token: 0x04000009 RID: 9
		private readonly string mailboxExchangeLegacyDn;

		// Token: 0x0400000A RID: 10
		private readonly DatabaseId mailboxDatabaseId;

		// Token: 0x0400000B RID: 11
		private readonly Guid mailboxGuid;
	}
}
