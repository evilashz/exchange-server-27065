using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003C9 RID: 969
	internal class InternalDeliverMailItemEventSource : DeliverMailItemEventSource
	{
		// Token: 0x06002C4A RID: 11338 RVA: 0x000B0C29 File Offset: 0x000AEE29
		public InternalDeliverMailItemEventSource(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, RoutedMailItemWrapper deliverableMailItem, ulong sessionId, NextHopConnection nextHopConnection, string remoteHost, DeliveryAgentConnection.Stats stats)
		{
			this.source = new InternalDeliveryAgentEventSource(mexSession, deliverableMailItem, sessionId, nextHopConnection, remoteHost, stats);
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000B0C45 File Offset: 0x000AEE45
		public InternalDeliveryAgentEventSource InternalEventSource
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000B0C4D File Offset: 0x000AEE4D
		public override void FailQueue(SmtpResponse smtpResponse)
		{
			this.source.FailQueue(smtpResponse);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000B0C5B File Offset: 0x000AEE5B
		public override void DeferQueue(SmtpResponse smtpResponse)
		{
			this.source.DeferQueue(smtpResponse);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000B0C69 File Offset: 0x000AEE69
		public override void DeferQueue(SmtpResponse smtpResponse, TimeSpan interval)
		{
			this.source.DeferQueue(smtpResponse, interval);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000B0C78 File Offset: 0x000AEE78
		public override void AckMailItemSuccess(SmtpResponse smtpResponse)
		{
			this.source.AckMailItemSuccess(smtpResponse);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000B0C86 File Offset: 0x000AEE86
		public override void AckMailItemDefer(SmtpResponse smtpResponse)
		{
			this.source.AckMailItemDefer(smtpResponse);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000B0C94 File Offset: 0x000AEE94
		public override void AckMailItemFail(SmtpResponse smtpResponse)
		{
			this.source.AckMailItemFail(smtpResponse);
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000B0CA2 File Offset: 0x000AEEA2
		public override void AckRecipientSuccess(EnvelopeRecipient recipient, SmtpResponse smtpResponse)
		{
			this.source.AckRecipientSuccess(recipient, smtpResponse);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000B0CB1 File Offset: 0x000AEEB1
		public override void AckRecipientDefer(EnvelopeRecipient recipient, SmtpResponse smtpResponse)
		{
			this.source.AckRecipientDefer(recipient, smtpResponse);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000B0CC0 File Offset: 0x000AEEC0
		public override void AckRecipientFail(EnvelopeRecipient recipient, SmtpResponse smtpResponse)
		{
			this.source.AckRecipientFail(recipient, smtpResponse);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000B0CCF File Offset: 0x000AEECF
		internal override void AddDsnParameters(string key, object value)
		{
			this.source.AddDsnParameters(key, value);
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000B0CDE File Offset: 0x000AEEDE
		internal override bool TryGetDsnParameters(string key, out object value)
		{
			return this.source.TryGetDsnParameters(key, out value);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000B0CED File Offset: 0x000AEEED
		internal override void AddDsnParameters(EnvelopeRecipient recipient, string key, object value)
		{
			this.source.AddDsnParameters(recipient, key, value);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000B0CFD File Offset: 0x000AEEFD
		internal override bool TryGetDsnParameters(EnvelopeRecipient recipient, string key, out object value)
		{
			return this.source.TryGetDsnParameters(recipient, key, out value);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000B0D0D File Offset: 0x000AEF0D
		public override void UnregisterConnection(SmtpResponse smtpResponse)
		{
			this.source.UnregisterConnection(smtpResponse);
		}

		// Token: 0x04001635 RID: 5685
		private InternalDeliveryAgentEventSource source;
	}
}
