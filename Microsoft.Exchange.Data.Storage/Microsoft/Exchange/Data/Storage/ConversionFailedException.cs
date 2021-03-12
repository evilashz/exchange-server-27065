using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200071F RID: 1823
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConversionFailedException : StoragePermanentException
	{
		// Token: 0x060047A8 RID: 18344 RVA: 0x00130354 File Offset: 0x0012E554
		public ConversionFailedException(ConversionFailureReason reason) : this(reason, null)
		{
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0013035E File Offset: 0x0012E55E
		public ConversionFailedException(ConversionFailureReason reason, Exception innerException) : base(ConversionFailedException.GetReasonDescription(reason), innerException)
		{
			EnumValidator.ThrowIfInvalid<ConversionFailureReason>(reason, "reason");
			this.reason = reason;
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0013037F File Offset: 0x0012E57F
		public ConversionFailedException(ConversionFailureReason reason, LocalizedString message, Exception innerException) : base(message, innerException)
		{
			EnumValidator.ThrowIfInvalid<ConversionFailureReason>(reason, "reason");
			this.reason = reason;
		}

		// Token: 0x170014D3 RID: 5331
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x0013039B File Offset: 0x0012E59B
		public ConversionFailureReason ConversionFailureReason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x001303A4 File Offset: 0x0012E5A4
		private static LocalizedString GetReasonDescription(ConversionFailureReason reason)
		{
			switch (reason)
			{
			case ConversionFailureReason.ExceedsLimit:
				return ServerStrings.ConversionLimitsExceeded;
			case ConversionFailureReason.MaliciousContent:
				return ServerStrings.ConversionMaliciousContent;
			case ConversionFailureReason.CorruptContent:
				return ServerStrings.ConversionCorruptContent;
			case ConversionFailureReason.ConverterInternalFailure:
				return ServerStrings.ConversionInternalFailure;
			case ConversionFailureReason.ConverterUnsupportedContent:
				return ServerStrings.ConversionUnsupportedContent;
			default:
				return LocalizedString.Empty;
			}
		}

		// Token: 0x0400272C RID: 10028
		private ConversionFailureReason reason;
	}
}
