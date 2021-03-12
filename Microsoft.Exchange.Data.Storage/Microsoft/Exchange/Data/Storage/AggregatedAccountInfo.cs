using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001B6 RID: 438
	[DataContract]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedAccountInfo
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x00074E91 File Offset: 0x00073091
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x00074E99 File Offset: 0x00073099
		[DataMember(Name = "Id")]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x00074EA2 File Offset: 0x000730A2
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x00074EAA File Offset: 0x000730AA
		[DataMember]
		public SmtpAddress SmtpAddress { get; set; }

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00074EB3 File Offset: 0x000730B3
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x00074EBB File Offset: 0x000730BB
		[DataMember]
		[Obsolete("AccountRootId is no longer required for AggregatedAccountInfo.", true)]
		internal StoreId AccountRootId { get; set; }

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x00074EC4 File Offset: 0x000730C4
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x00074ECC File Offset: 0x000730CC
		[DataMember]
		internal Guid RequestGuid { get; set; }

		// Token: 0x060017EA RID: 6122 RVA: 0x00074ED5 File Offset: 0x000730D5
		public AggregatedAccountInfo(Guid mailboxGuid, SmtpAddress smtpAddress, Guid requestGuid)
		{
			Util.ThrowOnNullArgument(mailboxGuid, "mailboxGuid");
			Util.ThrowOnNullArgument(smtpAddress, "smtpAddress");
			this.MailboxGuid = mailboxGuid;
			this.SmtpAddress = smtpAddress;
			this.RequestGuid = requestGuid;
		}
	}
}
