using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020001A8 RID: 424
	internal abstract class TextMessagingStateBase : IEquatable<TextMessagingStateBase>
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00057499 File Offset: 0x00055699
		public TextMessagingStateBase(int rawValue)
		{
			this.RawValue = rawValue;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000574A8 File Offset: 0x000556A8
		public static TextMessagingStateBase ParseFromADString(string valueString)
		{
			if (string.IsNullOrEmpty(valueString))
			{
				throw new ArgumentNullException("valueString");
			}
			return TextMessagingStateBase.FromRawInteger32(int.Parse(valueString));
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000574C8 File Offset: 0x000556C8
		public static TextMessagingStateBase FromRawInteger32(int rawValue)
		{
			if ((-2147483648 & rawValue) == 0)
			{
				return new TextMessagingDeliveryPointState(rawValue);
			}
			return new ReservedTextMessagingState(rawValue);
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000574E0 File Offset: 0x000556E0
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x000574E8 File Offset: 0x000556E8
		internal int RawValue { get; set; }

		// Token: 0x060011E2 RID: 4578 RVA: 0x000574F4 File Offset: 0x000556F4
		public string ToADString()
		{
			return this.RawValue.ToString();
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00057510 File Offset: 0x00055710
		public bool Equals(TextMessagingStateBase other)
		{
			return !object.ReferenceEquals(null, other) && this.RawValue.Equals(other.RawValue);
		}

		// Token: 0x04000A5D RID: 2653
		internal const int StartBitReserved = 31;

		// Token: 0x04000A5E RID: 2654
		internal const int MaskReserved = -2147483648;
	}
}
