using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020005BD RID: 1469
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ConversionLimitsTracker
	{
		// Token: 0x06003C3E RID: 15422 RVA: 0x000F7D0A File Offset: 0x000F5F0A
		internal ConversionLimitsTracker(ConversionLimits limits)
		{
			this.limits = limits;
			this.depth = 0;
			this.bodyCounted = false;
			this.partCount = 0;
			this.recipientCount = 0;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x000F7D3C File Offset: 0x000F5F3C
		internal ConversionLimitsTracker.State SaveState()
		{
			ConversionLimitsTracker.State result;
			result.Depth = this.depth;
			result.RecipientCount = this.recipientCount;
			result.PartCount = this.partCount;
			result.BodyCounted = this.bodyCounted;
			return result;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x000F7D7E File Offset: 0x000F5F7E
		internal void RestoreState(ConversionLimitsTracker.State state)
		{
			while (this.depth > state.Depth)
			{
				this.EndEmbeddedMessage();
			}
			this.partCount = state.PartCount;
			this.recipientCount = state.RecipientCount;
			this.bodyCounted = state.BodyCounted;
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x000F7DC0 File Offset: 0x000F5FC0
		internal void StartEmbeddedMessage()
		{
			if (this.enforceLimits && this.depth >= this.limits.MaxEmbeddedMessageDepth)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcGenericTracer, "ConversionLimitsTracker::StartEmbeddedMessage: maximum depth exceeded.");
				throw new ConversionFailedException(ConversionFailureReason.ExceedsLimit, ServerStrings.ConversionMaxEmbeddedDepthExceeded(this.limits.MaxEmbeddedMessageDepth), null);
			}
			this.depth++;
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x000F7E1D File Offset: 0x000F601D
		internal void EndEmbeddedMessage()
		{
			this.depth--;
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x000F7E2D File Offset: 0x000F602D
		internal void CountMessageBody()
		{
			if (this.depth == 0 && !this.bodyCounted)
			{
				this.CountMessagePart();
				this.bodyCounted = true;
			}
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x000F7E4C File Offset: 0x000F604C
		internal void CountMessageAttachment()
		{
			this.CountMessagePart();
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x000F7E54 File Offset: 0x000F6054
		private void CountMessagePart()
		{
			if (this.enforceLimits && this.partCount >= this.limits.MaxBodyPartsTotal)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcGenericTracer, "ConversionLimitsTracker::CountBodyPart: maximum body part count exceeded.");
				throw new ConversionFailedException(ConversionFailureReason.ExceedsLimit, ServerStrings.ConversionMaxBodyPartsExceeded(this.limits.MaxBodyPartsTotal), null);
			}
			this.partCount++;
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06003C46 RID: 15430 RVA: 0x000F7EB1 File Offset: 0x000F60B1
		internal int PartCount
		{
			get
			{
				return this.partCount;
			}
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x000F7EBC File Offset: 0x000F60BC
		internal void CountRecipient()
		{
			if (this.enforceLimits && this.recipientCount >= this.limits.MaxMimeRecipients)
			{
				StorageGlobals.ContextTraceError(ExTraceGlobals.CcGenericTracer, "ConversionLimitsTracker::CountRecipient: maximum recipient count exceeded.");
				throw new ConversionFailedException(ConversionFailureReason.ExceedsLimit, ServerStrings.ConversionMaxRecipientExceeded(this.limits.MaxMimeRecipients), null);
			}
			this.recipientCount++;
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x000F7F19 File Offset: 0x000F6119
		internal void RollbackRecipients(int count)
		{
			this.recipientCount -= count;
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x000F7F29 File Offset: 0x000F6129
		internal void SuppressLimitChecks()
		{
			if (this.limits.ExemptPFReplicationMessages)
			{
				this.enforceLimits = false;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003C4A RID: 15434 RVA: 0x000F7F3F File Offset: 0x000F613F
		internal bool EnforceLimits
		{
			get
			{
				return this.enforceLimits;
			}
		}

		// Token: 0x04002006 RID: 8198
		private int depth;

		// Token: 0x04002007 RID: 8199
		private bool bodyCounted;

		// Token: 0x04002008 RID: 8200
		private int partCount;

		// Token: 0x04002009 RID: 8201
		private int recipientCount;

		// Token: 0x0400200A RID: 8202
		private bool enforceLimits = true;

		// Token: 0x0400200B RID: 8203
		private ConversionLimits limits;

		// Token: 0x020005BE RID: 1470
		internal struct State
		{
			// Token: 0x0400200C RID: 8204
			internal int Depth;

			// Token: 0x0400200D RID: 8205
			internal int PartCount;

			// Token: 0x0400200E RID: 8206
			internal int RecipientCount;

			// Token: 0x0400200F RID: 8207
			internal bool BodyCounted;
		}
	}
}
