using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004E2 RID: 1250
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EmailAddress : IEquatable<EmailAddress>
	{
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060036A6 RID: 13990 RVA: 0x000DCEC3 File Offset: 0x000DB0C3
		// (set) Token: 0x060036A7 RID: 13991 RVA: 0x000DCECB File Offset: 0x000DB0CB
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000DCED4 File Offset: 0x000DB0D4
		// (set) Token: 0x060036A9 RID: 13993 RVA: 0x000DCEDC File Offset: 0x000DB0DC
		public string OriginalDisplayName
		{
			get
			{
				return this.originalDisplayName;
			}
			set
			{
				this.originalDisplayName = value;
			}
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000DCEE5 File Offset: 0x000DB0E5
		// (set) Token: 0x060036AB RID: 13995 RVA: 0x000DCEED File Offset: 0x000DB0ED
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = value;
			}
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060036AC RID: 13996 RVA: 0x000DCEF6 File Offset: 0x000DB0F6
		// (set) Token: 0x060036AD RID: 13997 RVA: 0x000DCEFE File Offset: 0x000DB0FE
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

		// Token: 0x060036AE RID: 13998 RVA: 0x000DCF07 File Offset: 0x000DB107
		public bool Equals(EmailAddress other)
		{
			return other != null && string.Equals(this.address, other.address, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000DCF20 File Offset: 0x000DB120
		public override bool Equals(object other)
		{
			return this.Equals(other as EmailAddress);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000DCF2E File Offset: 0x000DB12E
		public override int GetHashCode()
		{
			if (string.IsNullOrEmpty(this.address))
			{
				return 0;
			}
			return this.address.GetHashCode();
		}

		// Token: 0x060036B1 RID: 14001 RVA: 0x000DCF4C File Offset: 0x000DB14C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.routingType,
				":",
				this.address,
				":",
				this.name
			});
		}

		// Token: 0x04001D32 RID: 7474
		private string name;

		// Token: 0x04001D33 RID: 7475
		private string originalDisplayName;

		// Token: 0x04001D34 RID: 7476
		private string address;

		// Token: 0x04001D35 RID: 7477
		private string routingType;
	}
}
