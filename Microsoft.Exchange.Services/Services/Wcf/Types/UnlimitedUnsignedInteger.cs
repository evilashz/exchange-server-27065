using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B0E RID: 2830
	[TypeConverter(typeof(SimpleGenericsTypeConverter))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnlimitedUnsignedInteger : IComparable, IEquatable<UnlimitedUnsignedInteger>, IEquatable<Unlimited<ulong>>, IEquatable<Unlimited<ByteQuantifiedSize>>, IComparable<UnlimitedUnsignedInteger>, IComparable<Unlimited<ulong>>, IComparable<Unlimited<ByteQuantifiedSize>>
	{
		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06005054 RID: 20564 RVA: 0x00109761 File Offset: 0x00107961
		public static string UnlimitedString
		{
			get
			{
				return Unlimited<ulong>.UnlimitedString;
			}
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x00109768 File Offset: 0x00107968
		public UnlimitedUnsignedInteger()
		{
			this.limitedValue = ulong.MaxValue;
			this.IsUnlimited = true;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x0010977F File Offset: 0x0010797F
		public UnlimitedUnsignedInteger(ulong limitedValue)
		{
			this.IsUnlimited = false;
			this.limitedValue = limitedValue;
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x00109795 File Offset: 0x00107995
		public UnlimitedUnsignedInteger(Unlimited<ulong> value)
		{
			this.IsUnlimited = value.IsUnlimited;
			if (!this.IsUnlimited)
			{
				this.limitedValue = value.Value;
			}
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x001097C0 File Offset: 0x001079C0
		public UnlimitedUnsignedInteger(Unlimited<ByteQuantifiedSize> value)
		{
			this.IsUnlimited = value.IsUnlimited;
			if (!this.IsUnlimited)
			{
				this.limitedValue = value.Value.ToBytes();
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06005059 RID: 20569 RVA: 0x001097FD File Offset: 0x001079FD
		// (set) Token: 0x0600505A RID: 20570 RVA: 0x00109805 File Offset: 0x00107A05
		[DataMember]
		public bool IsUnlimited { get; set; }

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x0010980E File Offset: 0x00107A0E
		// (set) Token: 0x0600505C RID: 20572 RVA: 0x00109821 File Offset: 0x00107A21
		[DataMember]
		public ulong Value
		{
			get
			{
				if (this.IsUnlimited)
				{
					return ulong.MaxValue;
				}
				return this.limitedValue;
			}
			set
			{
				this.limitedValue = value;
			}
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x0010982C File Offset: 0x00107A2C
		public override bool Equals(object other)
		{
			UnlimitedUnsignedInteger unlimitedUnsignedInteger = other as UnlimitedUnsignedInteger;
			if (unlimitedUnsignedInteger != null)
			{
				return this.Equals(unlimitedUnsignedInteger);
			}
			if (other is Unlimited<ByteQuantifiedSize>)
			{
				Unlimited<ByteQuantifiedSize> other2 = (Unlimited<ByteQuantifiedSize>)other;
				return this.Equals(other2);
			}
			if (other is Unlimited<ulong>)
			{
				Unlimited<ulong> other3 = (Unlimited<ulong>)other;
				return this.Equals(other3);
			}
			return false;
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0010987C File Offset: 0x00107A7C
		public bool Equals(Unlimited<ByteQuantifiedSize> other)
		{
			return (this.IsUnlimited && other.IsUnlimited) || this.IsUnlimited || this.limitedValue.Equals(other.Value.ToBytes());
		}

		// Token: 0x0600505F RID: 20575 RVA: 0x001098C0 File Offset: 0x00107AC0
		public bool Equals(Unlimited<ulong> other)
		{
			return (this.IsUnlimited && other.IsUnlimited) || this.IsUnlimited || this.limitedValue.Equals(other.Value);
		}

		// Token: 0x06005060 RID: 20576 RVA: 0x001098F1 File Offset: 0x00107AF1
		public bool Equals(UnlimitedUnsignedInteger other)
		{
			return this.IsUnlimited == other.IsUnlimited && (this.IsUnlimited || this.limitedValue.Equals(other.Value));
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x00109920 File Offset: 0x00107B20
		public override int GetHashCode()
		{
			if (!this.IsUnlimited)
			{
				return this.Value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x00109945 File Offset: 0x00107B45
		public Unlimited<ByteQuantifiedSize> ToUnlimitedByteQuantifiedSize()
		{
			if (this.IsUnlimited)
			{
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
			return new Unlimited<ByteQuantifiedSize>(new ByteQuantifiedSize(this.limitedValue));
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x00109965 File Offset: 0x00107B65
		public override string ToString()
		{
			if (!this.IsUnlimited)
			{
				return this.limitedValue.ToString(CultureInfo.InvariantCulture);
			}
			return Unlimited<ulong>.UnlimitedString;
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x00109985 File Offset: 0x00107B85
		public int CompareTo(Unlimited<ByteQuantifiedSize> other)
		{
			if (this.IsUnlimited)
			{
				if (!other.IsUnlimited)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (!other.IsUnlimited)
				{
					return Comparer<ByteQuantifiedSize>.Default.Compare(new ByteQuantifiedSize(this.limitedValue), other.Value);
				}
				return -1;
			}
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x001099C3 File Offset: 0x00107BC3
		public int CompareTo(Unlimited<ulong> other)
		{
			if (this.IsUnlimited)
			{
				if (!other.IsUnlimited)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (!other.IsUnlimited)
				{
					return Comparer<ulong>.Default.Compare(this.limitedValue, other.Value);
				}
				return -1;
			}
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x001099FC File Offset: 0x00107BFC
		public int CompareTo(UnlimitedUnsignedInteger other)
		{
			if (this.IsUnlimited)
			{
				if (!other.IsUnlimited)
				{
					return 1;
				}
				return 0;
			}
			else
			{
				if (!other.IsUnlimited)
				{
					return Comparer<ulong>.Default.Compare(this.limitedValue, other.Value);
				}
				return -1;
			}
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x00109A34 File Offset: 0x00107C34
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			UnlimitedUnsignedInteger unlimitedUnsignedInteger = other as UnlimitedUnsignedInteger;
			if (unlimitedUnsignedInteger != null)
			{
				return this.CompareTo(unlimitedUnsignedInteger);
			}
			if (other is Unlimited<ByteQuantifiedSize>)
			{
				return this.CompareTo((Unlimited<ByteQuantifiedSize>)other);
			}
			if (other is Unlimited<ulong>)
			{
				return this.CompareTo((Unlimited<ulong>)other);
			}
			throw new ArgumentException(DataStrings.ExceptionObjectInvalid);
		}

		// Token: 0x04002CE5 RID: 11493
		private ulong limitedValue;
	}
}
