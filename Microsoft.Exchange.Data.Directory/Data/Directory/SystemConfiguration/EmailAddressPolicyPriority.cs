using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200043F RID: 1087
	[Serializable]
	public struct EmailAddressPolicyPriority : IComparable, IComparable<EmailAddressPolicyPriority>, IEquatable<EmailAddressPolicyPriority>
	{
		// Token: 0x06003132 RID: 12594 RVA: 0x000C5F0D File Offset: 0x000C410D
		public EmailAddressPolicyPriority(int priority)
		{
			this.priority = priority;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000C5F18 File Offset: 0x000C4118
		public EmailAddressPolicyPriority(string priority)
		{
			if (!string.IsNullOrEmpty(priority))
			{
				priority.Trim();
			}
			if (string.Equals(DirectoryStrings.EmailAddressPolicyPriorityLowest, priority, StringComparison.OrdinalIgnoreCase) || string.Equals("Lowest", priority, StringComparison.OrdinalIgnoreCase))
			{
				this.priority = EmailAddressPolicyPriority.Lowest.priority;
				return;
			}
			if (!int.TryParse(priority, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out this.priority))
			{
				throw new FormatException(DirectoryStrings.EmailAddressPolicyPriorityLowestFormatError(priority));
			}
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000C5F87 File Offset: 0x000C4187
		public static EmailAddressPolicyPriority Parse(string priority)
		{
			return new EmailAddressPolicyPriority(priority);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000C5F8F File Offset: 0x000C418F
		public static implicit operator int(EmailAddressPolicyPriority value)
		{
			return value.priority;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000C5F98 File Offset: 0x000C4198
		public static explicit operator EmailAddressPolicyPriority(int value)
		{
			return new EmailAddressPolicyPriority(value);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000C5FA0 File Offset: 0x000C41A0
		public static bool operator ==(EmailAddressPolicyPriority a, EmailAddressPolicyPriority b)
		{
			return a.priority == b.priority;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000C5FB2 File Offset: 0x000C41B2
		public static bool operator !=(EmailAddressPolicyPriority a, EmailAddressPolicyPriority b)
		{
			return a.priority != b.priority;
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000C5FC7 File Offset: 0x000C41C7
		public override string ToString()
		{
			if (this.Equals(EmailAddressPolicyPriority.Lowest))
			{
				return DirectoryStrings.EmailAddressPolicyPriorityLowest;
			}
			return this.priority.ToString();
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000C5FEC File Offset: 0x000C41EC
		public override int GetHashCode()
		{
			return this.priority.GetHashCode();
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000C5FF9 File Offset: 0x000C41F9
		public int CompareTo(EmailAddressPolicyPriority value)
		{
			return this.priority.CompareTo(value.priority);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000C6010 File Offset: 0x000C4210
		public int CompareTo(object value)
		{
			if (!(value is EmailAddressPolicyPriority))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "object must be of type {0}", new object[]
				{
					base.GetType().Name
				}));
			}
			return this.priority.CompareTo(((EmailAddressPolicyPriority)value).priority);
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000C6070 File Offset: 0x000C4270
		public bool Equals(EmailAddressPolicyPriority value)
		{
			return this.priority.Equals(value.priority);
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000C6084 File Offset: 0x000C4284
		public override bool Equals(object value)
		{
			if (!(value is EmailAddressPolicyPriority))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "object must be of type {0}", new object[]
				{
					base.GetType().Name
				}));
			}
			return this.priority.Equals(((EmailAddressPolicyPriority)value).priority);
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000C60E4 File Offset: 0x000C42E4
		public int ToInt32()
		{
			return this.priority;
		}

		// Token: 0x0400210A RID: 8458
		public const int MaxLength = 10;

		// Token: 0x0400210B RID: 8459
		public const string AllowedCharacters = "[0-9]";

		// Token: 0x0400210C RID: 8460
		private int priority;

		// Token: 0x0400210D RID: 8461
		public static readonly int HighestPriorityValue = 1;

		// Token: 0x0400210E RID: 8462
		public static readonly int LenientHighestPriorityValue = 0;

		// Token: 0x0400210F RID: 8463
		public static readonly int LowestPriorityValue = int.MaxValue;

		// Token: 0x04002110 RID: 8464
		public static readonly EmailAddressPolicyPriority Highest = new EmailAddressPolicyPriority(EmailAddressPolicyPriority.HighestPriorityValue);

		// Token: 0x04002111 RID: 8465
		public static readonly EmailAddressPolicyPriority Lowest = new EmailAddressPolicyPriority(EmailAddressPolicyPriority.LowestPriorityValue);
	}
}
