using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x0200008E RID: 142
	internal class MsgTrackRedirectInfo
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public MsgTrackRedirectInfo(string sourceContext, RoutingAddress originalAddress, RoutingDomain redirectedConnectorDomain, string redirectedDeliveryDestination, long? relatedMailItemId) : this(sourceContext, originalAddress, RoutingAddress.Empty, relatedMailItemId, null)
		{
			this.redirectedConnectorDomain = redirectedConnectorDomain;
			this.redirectedDeliveryDestination = redirectedDeliveryDestination;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00014BFC File Offset: 0x00012DFC
		public MsgTrackRedirectInfo(RoutingAddress originalAddress, RoutingAddress redirectedAddress, long? relatedMailItemId) : this(null, originalAddress, redirectedAddress, relatedMailItemId, null)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00014C1C File Offset: 0x00012E1C
		public MsgTrackRedirectInfo(RoutingAddress originalAddress, RoutingAddress redirectedAddress, long? relatedMailItemId, SmtpResponse? response) : this(null, originalAddress, redirectedAddress, relatedMailItemId, response)
		{
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00014C2C File Offset: 0x00012E2C
		public MsgTrackRedirectInfo(string sourceContext, RoutingAddress originalAddress, RoutingAddress redirectedAddress, long? relatedMailItemId) : this(sourceContext, originalAddress, redirectedAddress, relatedMailItemId, null)
		{
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00014C4D File Offset: 0x00012E4D
		public MsgTrackRedirectInfo(string sourceContext, RoutingAddress originalAddress, RoutingAddress redirectedAddress, long? relatedMailItemId, SmtpResponse? response)
		{
			this.sourceContext = sourceContext;
			this.originalAddress = originalAddress;
			this.redirectedAddress = redirectedAddress;
			this.relatedMailItemId = relatedMailItemId;
			this.response = response;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00014C7A File Offset: 0x00012E7A
		internal string SourceContext
		{
			get
			{
				return this.sourceContext;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x00014C82 File Offset: 0x00012E82
		internal RoutingAddress OriginalAddress
		{
			get
			{
				return this.originalAddress;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00014C8A File Offset: 0x00012E8A
		internal RoutingAddress RedirectedAddress
		{
			get
			{
				return this.redirectedAddress;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00014C92 File Offset: 0x00012E92
		internal RoutingDomain RedirectedConnectorDomain
		{
			get
			{
				return this.redirectedConnectorDomain;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00014C9A File Offset: 0x00012E9A
		internal string RedirectedDeliveryDestination
		{
			get
			{
				return this.redirectedDeliveryDestination;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00014CA2 File Offset: 0x00012EA2
		internal long? RelatedMailItemId
		{
			get
			{
				return this.relatedMailItemId;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00014CAA File Offset: 0x00012EAA
		internal SmtpResponse? Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x0400027B RID: 635
		private readonly string sourceContext;

		// Token: 0x0400027C RID: 636
		private readonly RoutingAddress originalAddress;

		// Token: 0x0400027D RID: 637
		private readonly RoutingAddress redirectedAddress;

		// Token: 0x0400027E RID: 638
		private readonly RoutingDomain redirectedConnectorDomain;

		// Token: 0x0400027F RID: 639
		private readonly string redirectedDeliveryDestination;

		// Token: 0x04000280 RID: 640
		private readonly long? relatedMailItemId;

		// Token: 0x04000281 RID: 641
		private readonly SmtpResponse? response;
	}
}
