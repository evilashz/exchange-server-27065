using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200064C RID: 1612
	internal class PublicFolderRecipient
	{
		// Token: 0x06004BC0 RID: 19392 RVA: 0x00117D6E File Offset: 0x00115F6E
		public PublicFolderRecipient(string mailboxName, Guid mailboxGuid, ADObjectId database, SmtpAddress primarySmtpAddress, ADObjectId objectId, bool isLocalRecipient)
		{
			this.MailboxName = mailboxName;
			this.MailboxGuid = mailboxGuid;
			this.PrimarySmtpAddress = primarySmtpAddress;
			this.Database = database;
			this.ObjectId = objectId;
			this.IsLocal = isLocalRecipient;
		}

		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06004BC1 RID: 19393 RVA: 0x00117DA3 File Offset: 0x00115FA3
		// (set) Token: 0x06004BC2 RID: 19394 RVA: 0x00117DAB File Offset: 0x00115FAB
		public string MailboxName { get; private set; }

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06004BC3 RID: 19395 RVA: 0x00117DB4 File Offset: 0x00115FB4
		// (set) Token: 0x06004BC4 RID: 19396 RVA: 0x00117DBC File Offset: 0x00115FBC
		public Guid MailboxGuid { get; private set; }

		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06004BC5 RID: 19397 RVA: 0x00117DC5 File Offset: 0x00115FC5
		// (set) Token: 0x06004BC6 RID: 19398 RVA: 0x00117DCD File Offset: 0x00115FCD
		public ADObjectId Database { get; private set; }

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x06004BC7 RID: 19399 RVA: 0x00117DD6 File Offset: 0x00115FD6
		// (set) Token: 0x06004BC8 RID: 19400 RVA: 0x00117DDE File Offset: 0x00115FDE
		public SmtpAddress PrimarySmtpAddress { get; private set; }

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x06004BC9 RID: 19401 RVA: 0x00117DE7 File Offset: 0x00115FE7
		// (set) Token: 0x06004BCA RID: 19402 RVA: 0x00117DEF File Offset: 0x00115FEF
		public ADObjectId ObjectId { get; private set; }

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x06004BCB RID: 19403 RVA: 0x00117DF8 File Offset: 0x00115FF8
		// (set) Token: 0x06004BCC RID: 19404 RVA: 0x00117E00 File Offset: 0x00116000
		public bool IsLocal { get; private set; }

		// Token: 0x06004BCD RID: 19405 RVA: 0x00117E0C File Offset: 0x0011600C
		public override int GetHashCode()
		{
			return this.ObjectId.ObjectGuid.GetHashCode();
		}

		// Token: 0x1700190D RID: 6413
		// (get) Token: 0x06004BCE RID: 19406 RVA: 0x00117E34 File Offset: 0x00116034
		public long ItemSize
		{
			get
			{
				long num = 0L;
				if (this.Database != null)
				{
					num += (long)this.Database.GetBytes().Length;
				}
				num += (long)(string.IsNullOrEmpty(this.MailboxName) ? 0 : this.MailboxName.Length);
				num += 16L;
				SmtpAddress primarySmtpAddress = this.PrimarySmtpAddress;
				num += (long)this.PrimarySmtpAddress.Length;
				if (this.ObjectId != null)
				{
					num += (long)this.ObjectId.GetBytes().Length;
				}
				return num + 1L;
			}
		}
	}
}
