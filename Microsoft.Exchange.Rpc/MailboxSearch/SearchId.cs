using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x0200027D RID: 637
	internal sealed class SearchId
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0002A188 File Offset: 0x00029588
		public SearchId(string mailboxDsName, Guid mailboxGuid, string searchName)
		{
			this.m_mailboxDsName = mailboxDsName;
			this.m_mailboxGuid = mailboxGuid;
			this.m_searchName = searchName;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0002A1B0 File Offset: 0x000295B0
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0002A1C4 File Offset: 0x000295C4
		public string MailboxDsName
		{
			get
			{
				return this.m_mailboxDsName;
			}
			set
			{
				this.m_mailboxDsName = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0002A1D8 File Offset: 0x000295D8
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0002A1F0 File Offset: 0x000295F0
		public Guid MailboxGuid
		{
			get
			{
				return this.m_mailboxGuid;
			}
			set
			{
				this.m_mailboxGuid = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x0002A204 File Offset: 0x00029604
		// (set) Token: 0x06000BE8 RID: 3048 RVA: 0x0002A218 File Offset: 0x00029618
		public string SearchName
		{
			get
			{
				return this.m_searchName;
			}
			set
			{
				this.m_searchName = value;
			}
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002A22C File Offset: 0x0002962C
		public sealed override int GetHashCode()
		{
			int hashCode = this.m_mailboxGuid.GetHashCode();
			if (this.m_mailboxGuid.Equals(Guid.Empty) && !string.IsNullOrEmpty(this.m_mailboxDsName))
			{
				hashCode = this.m_mailboxDsName.ToLowerInvariant().GetHashCode();
			}
			return this.m_searchName.ToLowerInvariant().GetHashCode() ^ hashCode;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002A288 File Offset: 0x00029688
		[return: MarshalAs(UnmanagedType.U1)]
		public sealed override bool Equals(object obj)
		{
			if (obj == null || base.GetType() != obj.GetType())
			{
				return false;
			}
			SearchId searchId = (SearchId)obj;
			string searchName = searchId.m_searchName;
			if (!this.m_searchName.Equals(searchName, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			Guid mailboxGuid = this.m_mailboxGuid;
			if (!mailboxGuid.Equals(Guid.Empty))
			{
				Guid mailboxGuid2 = searchId.m_mailboxGuid;
				if (!mailboxGuid2.Equals(Guid.Empty))
				{
					Guid mailboxGuid3 = searchId.m_mailboxGuid;
					Guid mailboxGuid4 = this.m_mailboxGuid;
					return mailboxGuid4.Equals(mailboxGuid3);
				}
			}
			if (this.m_mailboxDsName == null)
			{
				return searchId.m_mailboxDsName == null;
			}
			string mailboxDsName = searchId.m_mailboxDsName;
			return this.m_mailboxDsName.Equals(mailboxDsName, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x04000D0B RID: 3339
		private string m_mailboxDsName;

		// Token: 0x04000D0C RID: 3340
		private Guid m_mailboxGuid;

		// Token: 0x04000D0D RID: 3341
		private string m_searchName;
	}
}
