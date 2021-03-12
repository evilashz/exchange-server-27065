using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	internal struct NextHopType
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x000469D4 File Offset: 0x00044BD4
		public NextHopType(DeliveryType deliveryType)
		{
			this.deliveryType = deliveryType;
			if (TransportDeliveryTypes.internalDeliveryTypes.Contains(this.deliveryType))
			{
				this.nextHopCategory = NextHopCategory.Internal;
				return;
			}
			if (TransportDeliveryTypes.externalDeliveryTypes.Contains(this.deliveryType))
			{
				this.nextHopCategory = NextHopCategory.External;
				return;
			}
			throw new InvalidOperationException(string.Format("DeliveryType '{0}' not categorized as internal or external, or missing a value for NextHopCategory", this.deliveryType));
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00046A36 File Offset: 0x00044C36
		public NextHopType(int nextHopType)
		{
			this = new NextHopType((DeliveryType)nextHopType);
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00046A40 File Offset: 0x00044C40
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x00046A48 File Offset: 0x00044C48
		public DeliveryType DeliveryType
		{
			get
			{
				return this.deliveryType;
			}
			set
			{
				this.deliveryType = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00046A51 File Offset: 0x00044C51
		public NextHopCategory NextHopCategory
		{
			get
			{
				return this.nextHopCategory;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x00046A59 File Offset: 0x00044C59
		public bool IsConnectorDeliveryType
		{
			get
			{
				return this.IsSmtpConnectorDeliveryType || this.deliveryType == DeliveryType.NonSmtpGatewayDelivery || this.deliveryType == DeliveryType.DeliveryAgent;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00046A78 File Offset: 0x00044C78
		public bool IsSmtpConnectorDeliveryType
		{
			get
			{
				return this.deliveryType == DeliveryType.DnsConnectorDelivery || this.deliveryType == DeliveryType.SmartHostConnectorDelivery;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00046A8E File Offset: 0x00044C8E
		public bool IsSmtpSmtpRelayToEdge
		{
			get
			{
				return this.deliveryType == DeliveryType.SmtpRelayWithinAdSiteToEdge;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00046A99 File Offset: 0x00044C99
		public bool IsHubRelayDeliveryType
		{
			get
			{
				return this.deliveryType == DeliveryType.SmtpRelayToDag || this.deliveryType == DeliveryType.SmtpRelayToMailboxDeliveryGroup || this.deliveryType == DeliveryType.SmtpRelayToConnectorSourceServers || this.deliveryType == DeliveryType.SmtpRelayToServers || this.deliveryType == DeliveryType.SmtpRelayToRemoteAdSite || this.deliveryType == DeliveryType.SmtpRelayWithinAdSite;
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00046AD7 File Offset: 0x00044CD7
		public static bool IsMailboxDeliveryType(DeliveryType deliveryType)
		{
			return deliveryType == DeliveryType.MapiDelivery || deliveryType == DeliveryType.SmtpDeliveryToMailbox;
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00046AE4 File Offset: 0x00044CE4
		public static bool operator ==(NextHopType op1, NextHopType op2)
		{
			return op1.Equals(op2);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00046AEE File Offset: 0x00044CEE
		public static bool operator !=(NextHopType op1, NextHopType op2)
		{
			return !(op1 == op2);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00046AFA File Offset: 0x00044CFA
		public bool Equals(NextHopType obj)
		{
			return this.deliveryType == obj.deliveryType;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00046B0B File Offset: 0x00044D0B
		public int ToInt32()
		{
			return (int)this.deliveryType;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00046B13 File Offset: 0x00044D13
		public override bool Equals(object obj)
		{
			return obj is NextHopType && this.Equals((NextHopType)obj);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00046B2B File Offset: 0x00044D2B
		public override string ToString()
		{
			return this.deliveryType.ToString();
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00046B3D File Offset: 0x00044D3D
		public override int GetHashCode()
		{
			return (int)this.deliveryType;
		}

		// Token: 0x04000937 RID: 2359
		public static readonly NextHopType Empty = new NextHopType(DeliveryType.Undefined);

		// Token: 0x04000938 RID: 2360
		public static readonly NextHopType Unreachable = new NextHopType(DeliveryType.Unreachable);

		// Token: 0x04000939 RID: 2361
		public static readonly NextHopType ShadowRedundancy = new NextHopType(DeliveryType.ShadowRedundancy);

		// Token: 0x0400093A RID: 2362
		public static readonly NextHopType Heartbeat = new NextHopType(DeliveryType.Heartbeat);

		// Token: 0x0400093B RID: 2363
		private DeliveryType deliveryType;

		// Token: 0x0400093C RID: 2364
		private NextHopCategory nextHopCategory;
	}
}
