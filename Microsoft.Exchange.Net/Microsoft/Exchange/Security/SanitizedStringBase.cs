using System;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000A44 RID: 2628
	public class SanitizedStringBase<SanitizingPolicy> : ISanitizedString<SanitizingPolicy>, IComparable, IComparable<ISanitizedString<SanitizingPolicy>>, IComparable<string>, IEquatable<ISanitizedString<SanitizingPolicy>>, IEquatable<string> where SanitizingPolicy : ISanitizingPolicy, new()
	{
		// Token: 0x06003959 RID: 14681 RVA: 0x000918F0 File Offset: 0x0008FAF0
		public SanitizedStringBase() : this(string.Empty)
		{
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000918FD File Offset: 0x0008FAFD
		public SanitizedStringBase(string value)
		{
			if (value == null)
			{
				this.containedString = string.Empty;
			}
			else
			{
				this.containedString = value;
			}
			this.isContainedStringTrusted = (this.containedString.Length == 0);
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x00091930 File Offset: 0x0008FB30
		// (set) Token: 0x0600395C RID: 14684 RVA: 0x00091942 File Offset: 0x0008FB42
		public string UntrustedValue
		{
			get
			{
				if (this.isContainedStringTrusted)
				{
					return null;
				}
				return this.containedString;
			}
			set
			{
				if (value == null)
				{
					this.containedString = string.Empty;
				}
				else
				{
					this.containedString = value;
				}
				this.isContainedStringTrusted = (value.Length == 0);
			}
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x0009196A File Offset: 0x0008FB6A
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x0009197C File Offset: 0x0008FB7C
		protected string TrustedValue
		{
			get
			{
				if (this.isContainedStringTrusted)
				{
					return this.containedString;
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					this.containedString = string.Empty;
				}
				else
				{
					this.containedString = value;
				}
				this.isContainedStringTrusted = true;
			}
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x0009199C File Offset: 0x0008FB9C
		public static bool IsNullOrEmpty(SanitizedStringBase<SanitizingPolicy> str)
		{
			return str == null || str.containedString.Length == 0;
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x000919B1 File Offset: 0x0008FBB1
		public sealed override string ToString()
		{
			if (!this.isContainedStringTrusted)
			{
				if (!StringSanitizer<SanitizingPolicy>.IsTrustedString(this.containedString))
				{
					this.containedString = this.Sanitize(this.containedString);
				}
				this.isContainedStringTrusted = true;
			}
			return this.containedString;
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x000919E7 File Offset: 0x0008FBE7
		public void DecreeToBeTrusted()
		{
			this.isContainedStringTrusted = true;
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x000919F0 File Offset: 0x0008FBF0
		public void DecreeToBeUntrusted()
		{
			this.isContainedStringTrusted = false;
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000919FC File Offset: 0x0008FBFC
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return int.MaxValue;
			}
			if (obj is ISanitizedString<SanitizingPolicy>)
			{
				ISanitizedString<SanitizingPolicy> sanitizedString = (ISanitizedString<SanitizingPolicy>)obj;
				return this.ToString().CompareTo(sanitizedString.ToString());
			}
			if (obj is string)
			{
				return this.ToString().CompareTo((string)obj);
			}
			throw new ArgumentException("Object is neither a string nor an ISanitizedString<" + typeof(SanitizingPolicy) + ">");
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x00091A6B File Offset: 0x0008FC6B
		public int CompareTo(ISanitizedString<SanitizingPolicy> other)
		{
			if (other == null)
			{
				return int.MaxValue;
			}
			return this.ToString().CompareTo(other.ToString());
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x00091A87 File Offset: 0x0008FC87
		public int CompareTo(string other)
		{
			if (other == null)
			{
				return int.MaxValue;
			}
			return this.ToString().CompareTo(other);
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x00091A9E File Offset: 0x0008FC9E
		public bool Equals(ISanitizedString<SanitizingPolicy> other)
		{
			return other != null && this.ToString().Equals(other.ToString());
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x00091AB6 File Offset: 0x0008FCB6
		public bool Equals(string other)
		{
			return other != null && this.ToString().Equals(other);
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x00091AC9 File Offset: 0x0008FCC9
		public bool Equals(ISanitizedString<SanitizingPolicy> value, StringComparison comparisonType)
		{
			return value != null && this.ToString().Equals(value.ToString(), comparisonType);
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00091AE2 File Offset: 0x0008FCE2
		public bool Equals(string value, StringComparison comparisonType)
		{
			return value != null && this.ToString().Equals(value, comparisonType);
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00091AF6 File Offset: 0x0008FCF6
		public sealed override bool Equals(object obj)
		{
			return obj != null && this.ToString().Equals(obj.ToString());
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x00091B0E File Offset: 0x0008FD0E
		public sealed override int GetHashCode()
		{
			return this.containedString.GetHashCode();
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00091B1B File Offset: 0x0008FD1B
		protected virtual string Sanitize(string rawValue)
		{
			return StringSanitizer<SanitizingPolicy>.Sanitize(rawValue);
		}

		// Token: 0x040030E3 RID: 12515
		private string containedString;

		// Token: 0x040030E4 RID: 12516
		private bool isContainedStringTrusted;
	}
}
