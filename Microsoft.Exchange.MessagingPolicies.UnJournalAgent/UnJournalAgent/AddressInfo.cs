using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000003 RID: 3
	internal class AddressInfo
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002668 File Offset: 0x00000868
		public AddressInfo(string friendlyName, RoutingAddress address)
		{
			this.friendlyName = friendlyName;
			this.address = address;
			this.primarySmtpAddress = string.Empty;
			this.recipientType = UnjournalRecipientType.Unknown;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002690 File Offset: 0x00000890
		public AddressInfo(RoutingAddress address)
		{
			this.friendlyName = address.ToString();
			this.address = address;
			this.primarySmtpAddress = string.Empty;
			this.recipientType = UnjournalRecipientType.Unknown;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000026C4 File Offset: 0x000008C4
		// (set) Token: 0x06000006 RID: 6 RVA: 0x000026CC File Offset: 0x000008CC
		public string FriendlyName
		{
			get
			{
				return this.friendlyName;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.friendlyName = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000026E3 File Offset: 0x000008E3
		public RoutingAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000026EB File Offset: 0x000008EB
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000026F3 File Offset: 0x000008F3
		public UnjournalRecipientType RecipientType
		{
			get
			{
				return this.recipientType;
			}
			set
			{
				this.recipientType = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000026FC File Offset: 0x000008FC
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002704 File Offset: 0x00000904
		public string PrimarySmtpAddress
		{
			get
			{
				return this.primarySmtpAddress;
			}
			set
			{
				this.primarySmtpAddress = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000270D File Offset: 0x0000090D
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002715 File Offset: 0x00000915
		public bool IncludedInTo
		{
			get
			{
				return this.includedInTo;
			}
			set
			{
				this.includedInTo = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000271E File Offset: 0x0000091E
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002726 File Offset: 0x00000926
		public bool IncludedInCc
		{
			get
			{
				return this.includedInCc;
			}
			set
			{
				this.includedInCc = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000272F File Offset: 0x0000092F
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002737 File Offset: 0x00000937
		public bool IncludedInBcc
		{
			get
			{
				return this.includedInBcc;
			}
			set
			{
				this.includedInBcc = value;
			}
		}

		// Token: 0x04000022 RID: 34
		private string friendlyName;

		// Token: 0x04000023 RID: 35
		private RoutingAddress address;

		// Token: 0x04000024 RID: 36
		private bool includedInTo;

		// Token: 0x04000025 RID: 37
		private bool includedInCc;

		// Token: 0x04000026 RID: 38
		private bool includedInBcc;

		// Token: 0x04000027 RID: 39
		private string primarySmtpAddress;

		// Token: 0x04000028 RID: 40
		private UnjournalRecipientType recipientType;
	}
}
