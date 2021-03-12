using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000291 RID: 657
	[DataContract]
	public class RecoverAccount : BaseRow
	{
		// Token: 0x06002AC8 RID: 10952 RVA: 0x00085E5C File Offset: 0x0008405C
		public RecoverAccount(RemovedMailbox removedMailbox) : base(removedMailbox)
		{
			if (Util.IsMicrosoftHostedOnly)
			{
				this.UserName = removedMailbox.WindowsLiveID.Local;
				this.Domain = removedMailbox.WindowsLiveID.Domain;
				this.OriginalLiveID = removedMailbox.WindowsLiveID.ToString();
				this.IsPasswordRequired = removedMailbox.IsPasswordResetRequired;
			}
			else
			{
				this.UserName = removedMailbox.Name;
			}
			this.RemovedMailbox = removedMailbox.Guid.ToString();
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00085EF0 File Offset: 0x000840F0
		public RecoverAccount(Mailbox mailbox) : base(mailbox)
		{
			if (Util.IsMicrosoftHostedOnly)
			{
				this.UserName = mailbox.WindowsLiveID.Local;
				this.Domain = mailbox.WindowsLiveID.Domain;
				this.OriginalLiveID = mailbox.WindowsLiveID.ToString();
				this.IsPasswordRequired = false;
				this.NeedCredential = true;
				using (MultiValuedProperty<ProxyAddress>.Enumerator enumerator = mailbox.EmailAddresses.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ProxyAddress proxyAddress = enumerator.Current;
						if (proxyAddress.Prefix.ToString().Equals("DLTDNETID", StringComparison.OrdinalIgnoreCase))
						{
							this.NeedCredential = false;
							break;
						}
					}
					goto IL_BE;
				}
			}
			this.UserName = mailbox.Name;
			IL_BE:
			this.SoftDeletedMailbox = mailbox.Guid.ToString();
		}

		// Token: 0x17001D2C RID: 7468
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x00085FE8 File Offset: 0x000841E8
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x00085FF0 File Offset: 0x000841F0
		[DataMember]
		public string UserName { get; private set; }

		// Token: 0x17001D2D RID: 7469
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x00085FF9 File Offset: 0x000841F9
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x00086001 File Offset: 0x00084201
		[DataMember]
		public string Domain { get; private set; }

		// Token: 0x17001D2E RID: 7470
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x0008600A File Offset: 0x0008420A
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x00086012 File Offset: 0x00084212
		[DataMember]
		public string OriginalLiveID { get; private set; }

		// Token: 0x17001D2F RID: 7471
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x0008601B File Offset: 0x0008421B
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x00086023 File Offset: 0x00084223
		[DataMember]
		public string RemovedMailbox { get; private set; }

		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x0008602C File Offset: 0x0008422C
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x00086034 File Offset: 0x00084234
		[DataMember]
		public bool IsPasswordRequired { get; private set; }

		// Token: 0x17001D31 RID: 7473
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0008603D File Offset: 0x0008423D
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x00086045 File Offset: 0x00084245
		[DataMember]
		public string SoftDeletedMailbox { get; private set; }

		// Token: 0x17001D32 RID: 7474
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x0008604E File Offset: 0x0008424E
		// (set) Token: 0x06002AD7 RID: 10967 RVA: 0x00086056 File Offset: 0x00084256
		[DataMember]
		public bool NeedCredential { get; private set; }

		// Token: 0x04002163 RID: 8547
		private const string DELETED_NETID_PREFIX = "DLTDNETID";
	}
}
