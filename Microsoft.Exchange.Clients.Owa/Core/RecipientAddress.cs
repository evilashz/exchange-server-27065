using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200022D RID: 557
	internal class RecipientAddress : IComparable
	{
		// Token: 0x060012BC RID: 4796 RVA: 0x000718DC File Offset: 0x0006FADC
		public static AddressOrigin ToAddressOrigin(Participant participant)
		{
			if (participant.Origin is DirectoryParticipantOrigin)
			{
				return AddressOrigin.Directory;
			}
			if (participant.Origin is OneOffParticipantOrigin)
			{
				if (!(participant.RoutingType == "EX"))
				{
					return AddressOrigin.OneOff;
				}
				return AddressOrigin.Directory;
			}
			else
			{
				if (participant.Origin is StoreParticipantOrigin)
				{
					return AddressOrigin.Store;
				}
				return AddressOrigin.Unknown;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0007192B File Offset: 0x0006FB2B
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x00071933 File Offset: 0x0006FB33
		public string Alias
		{
			get
			{
				return this.alias;
			}
			set
			{
				this.alias = value;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0007193C File Offset: 0x0006FB3C
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x00071944 File Offset: 0x0006FB44
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0007194D File Offset: 0x0006FB4D
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x00071955 File Offset: 0x0006FB55
		public bool IsDistributionList
		{
			get
			{
				return this.isDistributionList;
			}
			set
			{
				this.isDistributionList = value;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0007195E File Offset: 0x0006FB5E
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x00071966 File Offset: 0x0006FB66
		public bool IsRoom
		{
			get
			{
				return this.isRoom;
			}
			set
			{
				this.isRoom = value;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0007196F File Offset: 0x0006FB6F
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x00071977 File Offset: 0x0006FB77
		public StoreObjectId StoreObjectId
		{
			get
			{
				return this.storeObjectId;
			}
			set
			{
				this.storeObjectId = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00071980 File Offset: 0x0006FB80
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x00071988 File Offset: 0x0006FB88
		public ADObjectId ADObjectId
		{
			get
			{
				return this.adObjectId;
			}
			set
			{
				this.adObjectId = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00071991 File Offset: 0x0006FB91
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x00071999 File Offset: 0x0006FB99
		public AddressOrigin AddressOrigin
		{
			get
			{
				return this.addressOrigin;
			}
			set
			{
				this.addressOrigin = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x000719A4 File Offset: 0x0006FBA4
		public int RecipientFlags
		{
			get
			{
				int num = 0;
				if (this.isRoom)
				{
					num |= 2;
				}
				if (this.isDistributionList)
				{
					num |= 1;
				}
				return num;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x000719CC File Offset: 0x0006FBCC
		// (set) Token: 0x060012CD RID: 4813 RVA: 0x000719D4 File Offset: 0x0006FBD4
		public string RoutingAddress
		{
			get
			{
				return this.routingAddress;
			}
			set
			{
				this.routingAddress = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x000719DD File Offset: 0x0006FBDD
		// (set) Token: 0x060012CF RID: 4815 RVA: 0x000719E5 File Offset: 0x0006FBE5
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
			set
			{
				this.routingType = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000719EE File Offset: 0x0006FBEE
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x000719F6 File Offset: 0x0006FBF6
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
			set
			{
				this.smtpAddress = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x000719FF File Offset: 0x0006FBFF
		// (set) Token: 0x060012D3 RID: 4819 RVA: 0x00071A07 File Offset: 0x0006FC07
		public EmailAddressIndex EmailAddressIndex
		{
			get
			{
				return this.emailAddressIndex;
			}
			set
			{
				this.emailAddressIndex = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00071A10 File Offset: 0x0006FC10
		// (set) Token: 0x060012D5 RID: 4821 RVA: 0x00071A18 File Offset: 0x0006FC18
		public RecipientType RecipientType
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

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00071A21 File Offset: 0x0006FC21
		// (set) Token: 0x060012D7 RID: 4823 RVA: 0x00071A29 File Offset: 0x0006FC29
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
			set
			{
				this.sipUri = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00071A32 File Offset: 0x0006FC32
		// (set) Token: 0x060012D9 RID: 4825 RVA: 0x00071A3A File Offset: 0x0006FC3A
		public string MobilePhoneNumber
		{
			get
			{
				return this.mobilePhoneNumber;
			}
			set
			{
				this.mobilePhoneNumber = value;
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00071A44 File Offset: 0x0006FC44
		public int CompareTo(object value)
		{
			RecipientAddress recipientAddress = value as RecipientAddress;
			if (recipientAddress == null)
			{
				throw new ArgumentException("object is not an RecipientAddress");
			}
			if (this.displayName != null && recipientAddress.DisplayName != null)
			{
				return this.displayName.CompareTo(recipientAddress.DisplayName);
			}
			if (this.displayName == null && recipientAddress.DisplayName != null)
			{
				return -1;
			}
			if (this.displayName != null && recipientAddress.DisplayName == null)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04000CE3 RID: 3299
		private ADObjectId adObjectId;

		// Token: 0x04000CE4 RID: 3300
		private StoreObjectId storeObjectId;

		// Token: 0x04000CE5 RID: 3301
		private AddressOrigin addressOrigin;

		// Token: 0x04000CE6 RID: 3302
		private string alias;

		// Token: 0x04000CE7 RID: 3303
		private string displayName;

		// Token: 0x04000CE8 RID: 3304
		private string routingAddress;

		// Token: 0x04000CE9 RID: 3305
		private string routingType;

		// Token: 0x04000CEA RID: 3306
		private string smtpAddress;

		// Token: 0x04000CEB RID: 3307
		private bool isDistributionList;

		// Token: 0x04000CEC RID: 3308
		private bool isRoom;

		// Token: 0x04000CED RID: 3309
		private EmailAddressIndex emailAddressIndex;

		// Token: 0x04000CEE RID: 3310
		private RecipientType recipientType;

		// Token: 0x04000CEF RID: 3311
		private string sipUri;

		// Token: 0x04000CF0 RID: 3312
		private string mobilePhoneNumber;

		// Token: 0x0200022E RID: 558
		[Flags]
		public enum RecipientAddressFlags
		{
			// Token: 0x04000CF2 RID: 3314
			None = 0,
			// Token: 0x04000CF3 RID: 3315
			DistributionList = 1,
			// Token: 0x04000CF4 RID: 3316
			Room = 2
		}
	}
}
