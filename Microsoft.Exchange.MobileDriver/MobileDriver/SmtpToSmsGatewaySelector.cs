using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.VersionedXml;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000042 RID: 66
	internal class SmtpToSmsGatewaySelector : IMobileServiceSelector
	{
		// Token: 0x06000162 RID: 354 RVA: 0x00007F64 File Offset: 0x00006164
		public SmtpToSmsGatewaySelector(ExchangePrincipal principal, DeliveryPoint dp)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			if (dp == null)
			{
				throw new ArgumentNullException("dp");
			}
			if (DeliveryPointType.SmtpToSmsGateway != dp.Type)
			{
				throw new ArgumentOutOfRangeException("dp");
			}
			this.Principal = principal;
			this.DeliveryPoint = dp;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007FB5 File Offset: 0x000061B5
		internal SmtpToSmsGatewaySelector(ExchangePrincipal principal)
		{
			this.Principal = principal;
			this.p2pPriority = new int?(0);
			this.m2pPriority = new int?(0);
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007FDC File Offset: 0x000061DC
		public MobileServiceType Type
		{
			get
			{
				return MobileServiceType.SmtpToSmsGateway;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007FDF File Offset: 0x000061DF
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

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000800F File Offset: 0x0000620F
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000803F File Offset: 0x0000623F
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00008047 File Offset: 0x00006247
		public ExchangePrincipal Principal { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00008050 File Offset: 0x00006250
		// (set) Token: 0x0600016A RID: 362 RVA: 0x00008058 File Offset: 0x00006258
		private DeliveryPoint DeliveryPoint { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00008061 File Offset: 0x00006261
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00008069 File Offset: 0x00006269
		private string Literal { get; set; }

		// Token: 0x0600016D RID: 365 RVA: 0x00008074 File Offset: 0x00006274
		public override string ToString()
		{
			string result;
			if ((result = this.Literal) == null)
			{
				result = (this.Literal = string.Format("{0}:{1}", this.Type, (this.Principal == null) ? null : this.Principal.ObjectId));
			}
			return result;
		}

		// Token: 0x040000DC RID: 220
		private int? p2pPriority;

		// Token: 0x040000DD RID: 221
		private int? m2pPriority;
	}
}
