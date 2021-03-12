using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200001F RID: 31
	internal class EasSelector : IMobileServiceSelector
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004400 File Offset: 0x00002600
		public EasSelector(ExchangePrincipal principal, DeliveryPoint dp)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (DeliveryPointType.ExchangeActiveSync != dp.Type)
			{
				throw new ArgumentOutOfRangeException("dp");
			}
			if (!dp.Ready)
			{
				throw new ArgumentOutOfRangeException("dp");
			}
			this.Principal = principal;
			this.DeliveryPoint = dp;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004464 File Offset: 0x00002664
		internal EasSelector(ExchangePrincipal principal, E164Number number)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (null == number)
			{
				throw new ArgumentNullException("number");
			}
			this.Principal = principal;
			this.number = number;
			this.p2pPriority = new int?(0);
			this.m2pPriority = new int?(0);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000044BF File Offset: 0x000026BF
		public MobileServiceType Type
		{
			get
			{
				return MobileServiceType.Eas;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000044C2 File Offset: 0x000026C2
		public int PersonToPersonMessagingPriority
		{
			get
			{
				if (this.p2pPriority == null)
				{
					this.p2pPriority = new int?(this.DeliveryPoint.P2pMessagingPriority);
				}
				return this.p2pPriority.Value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000044F2 File Offset: 0x000026F2
		public int MachineToPersonMessagingPriority
		{
			get
			{
				if (this.m2pPriority == null)
				{
					this.m2pPriority = new int?(this.DeliveryPoint.M2pMessagingPriority);
				}
				return this.m2pPriority.Value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00004522 File Offset: 0x00002722
		// (set) Token: 0x060000AD RID: 173 RVA: 0x0000452A File Offset: 0x0000272A
		public ExchangePrincipal Principal { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004533 File Offset: 0x00002733
		public E164Number Number
		{
			get
			{
				if (null == this.number)
				{
					this.number = this.DeliveryPoint.PhoneNumber;
				}
				return this.number;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000455A File Offset: 0x0000275A
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004562 File Offset: 0x00002762
		private DeliveryPoint DeliveryPoint { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000456B File Offset: 0x0000276B
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00004573 File Offset: 0x00002773
		private string Literal { get; set; }

		// Token: 0x060000B3 RID: 179 RVA: 0x0000457C File Offset: 0x0000277C
		public override string ToString()
		{
			string result;
			if ((result = this.Literal) == null)
			{
				result = (this.Literal = string.Format("{0}:{1}@{2}", this.Type, this.Number, (this.Principal == null) ? null : this.Principal.ObjectId));
			}
			return result;
		}

		// Token: 0x0400003E RID: 62
		private int? p2pPriority;

		// Token: 0x0400003F RID: 63
		private int? m2pPriority;

		// Token: 0x04000040 RID: 64
		private E164Number number;
	}
}
