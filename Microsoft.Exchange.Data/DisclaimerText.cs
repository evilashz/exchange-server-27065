using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000FB RID: 251
	[Serializable]
	public struct DisclaimerText : IComparable, ISerializable
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x0001C418 File Offset: 0x0001A618
		public DisclaimerText(string input)
		{
			this.value = null;
			if (this.IsValid(input))
			{
				this.value = input;
				return;
			}
			throw new ArgumentOutOfRangeException(DataStrings.DisclaimerText, DataStrings.InvalidInputErrorMsg);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001C44B File Offset: 0x0001A64B
		public static DisclaimerText Parse(string s)
		{
			return new DisclaimerText(s);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001C454 File Offset: 0x0001A654
		private DisclaimerText(SerializationInfo info, StreamingContext context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			if (!this.IsValid(this.value))
			{
				throw new ArgumentOutOfRangeException(DataStrings.DisclaimerText, this.value.ToString());
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001C4AA File Offset: 0x0001A6AA
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("value", this.value);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001C4BD File Offset: 0x0001A6BD
		private bool IsValid(string input)
		{
			return input == null || (input.Length <= 5120 && DisclaimerText.ValidatingExpression.IsMatch(input));
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0001C4E0 File Offset: 0x0001A6E0
		public static DisclaimerText Empty
		{
			get
			{
				return default(DisclaimerText);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001C4F6 File Offset: 0x0001A6F6
		public string Value
		{
			get
			{
				if (this.IsValid(this.value))
				{
					return this.value;
				}
				throw new ArgumentOutOfRangeException("Value", this.value.ToString());
			}
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C522 File Offset: 0x0001A722
		public override string ToString()
		{
			if (this.value == null)
			{
				return string.Empty;
			}
			return this.value.ToString();
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C53D File Offset: 0x0001A73D
		public override int GetHashCode()
		{
			if (this.value == null)
			{
				return string.Empty.GetHashCode();
			}
			return this.value.GetHashCode();
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C55D File Offset: 0x0001A75D
		public override bool Equals(object obj)
		{
			return obj is DisclaimerText && this.Equals((DisclaimerText)obj);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x0001C575 File Offset: 0x0001A775
		public bool Equals(DisclaimerText obj)
		{
			return this.value == obj.Value;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x0001C589 File Offset: 0x0001A789
		public static bool operator ==(DisclaimerText a, DisclaimerText b)
		{
			return a.Value == b.Value;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0001C59E File Offset: 0x0001A79E
		public static bool operator !=(DisclaimerText a, DisclaimerText b)
		{
			return a.Value != b.Value;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		public int CompareTo(object obj)
		{
			if (!(obj is DisclaimerText))
			{
				throw new ArgumentException("Parameter is not of type DisclaimerText.");
			}
			return string.Compare(this.value, ((DisclaimerText)obj).Value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040005B8 RID: 1464
		public const int MaxLength = 5120;

		// Token: 0x040005B9 RID: 1465
		public const string AllowedCharacters = "(.|[^.])";

		// Token: 0x040005BA RID: 1466
		public static readonly Regex ValidatingExpression = new Regex("^(.|[^.])+$", RegexOptions.Compiled);

		// Token: 0x040005BB RID: 1467
		private string value;
	}
}
