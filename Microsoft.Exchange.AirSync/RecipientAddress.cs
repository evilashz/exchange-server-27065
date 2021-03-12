using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000114 RID: 276
	internal class RecipientAddress : IComparable
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00053A1A File Offset: 0x00051C1A
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00053A22 File Offset: 0x00051C22
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

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x00053A2B File Offset: 0x00051C2B
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00053A33 File Offset: 0x00051C33
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

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00053A3C File Offset: 0x00051C3C
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00053A44 File Offset: 0x00051C44
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00053A4D File Offset: 0x00051C4D
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00053A55 File Offset: 0x00051C55
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

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00053A5E File Offset: 0x00051C5E
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00053A66 File Offset: 0x00051C66
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00053A6F File Offset: 0x00051C6F
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00053A77 File Offset: 0x00051C77
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

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00053A80 File Offset: 0x00051C80
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00053A88 File Offset: 0x00051C88
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

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00053A94 File Offset: 0x00051C94
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

		// Token: 0x04000A16 RID: 2582
		private AddressOrigin addressOrigin;

		// Token: 0x04000A17 RID: 2583
		private string displayName;

		// Token: 0x04000A18 RID: 2584
		private int index;

		// Token: 0x04000A19 RID: 2585
		private string routingAddress;

		// Token: 0x04000A1A RID: 2586
		private string routingType;

		// Token: 0x04000A1B RID: 2587
		private string smtpAddress;

		// Token: 0x04000A1C RID: 2588
		private StoreObjectId storeObjectId;
	}
}
