using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200054C RID: 1356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PhoneNumber : IEquatable<PhoneNumber>
	{
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000EB3DE File Offset: 0x000E95DE
		// (set) Token: 0x06003940 RID: 14656 RVA: 0x000EB3E6 File Offset: 0x000E95E6
		public string Number
		{
			get
			{
				return this.number;
			}
			set
			{
				this.number = value;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000EB3EF File Offset: 0x000E95EF
		// (set) Token: 0x06003942 RID: 14658 RVA: 0x000EB3F7 File Offset: 0x000E95F7
		public PersonPhoneNumberType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<PersonPhoneNumberType>(value, "value");
				this.type = value;
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000EB40C File Offset: 0x000E960C
		public bool Equals(PhoneNumber other)
		{
			if (other == null)
			{
				return false;
			}
			string numericPartFromText = this.GetNumericPartFromText(this.Number);
			string numericPartFromText2 = this.GetNumericPartFromText(other.Number);
			return string.Equals(numericPartFromText, numericPartFromText2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000EB440 File Offset: 0x000E9640
		public override bool Equals(object other)
		{
			return this.Equals(other as PhoneNumber);
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000EB44E File Offset: 0x000E964E
		public override int GetHashCode()
		{
			if (string.IsNullOrEmpty(this.number))
			{
				return 0;
			}
			return this.number.GetHashCode();
		}

		// Token: 0x06003946 RID: 14662 RVA: 0x000EB46A File Offset: 0x000E966A
		private string GetNumericPartFromText(string text)
		{
			if (text == null)
			{
				return string.Empty;
			}
			return string.Join(null, Regex.Split(text, "[^\\d]"));
		}

		// Token: 0x04001E96 RID: 7830
		private const string NumericRegEx = "[^\\d]";

		// Token: 0x04001E97 RID: 7831
		private string number;

		// Token: 0x04001E98 RID: 7832
		private PersonPhoneNumberType type;
	}
}
