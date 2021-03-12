using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D1B RID: 3355
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RecipientToIndex
	{
		// Token: 0x060073EF RID: 29679 RVA: 0x00202730 File Offset: 0x00200930
		internal RecipientToIndex(RecipientItemType recipientType, string displayName, string emailAddress)
		{
			if (recipientType != RecipientItemType.To && recipientType != RecipientItemType.Cc && recipientType != RecipientItemType.Bcc)
			{
				throw new ArgumentException("Only TO, CC, and BCC recipient type is supported.");
			}
			if (displayName == null)
			{
				throw new ArgumentNullException("displayName");
			}
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			this.RecipientType = recipientType;
			this.DisplayName = RecipientToIndex.SanitizeDisplayName(displayName);
			this.EmailAddress = emailAddress;
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x060073F0 RID: 29680 RVA: 0x00202790 File Offset: 0x00200990
		// (set) Token: 0x060073F1 RID: 29681 RVA: 0x00202798 File Offset: 0x00200998
		internal RecipientItemType RecipientType { get; private set; }

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x060073F2 RID: 29682 RVA: 0x002027A1 File Offset: 0x002009A1
		// (set) Token: 0x060073F3 RID: 29683 RVA: 0x002027A9 File Offset: 0x002009A9
		public string DisplayName { get; private set; }

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x060073F4 RID: 29684 RVA: 0x002027B2 File Offset: 0x002009B2
		// (set) Token: 0x060073F5 RID: 29685 RVA: 0x002027BA File Offset: 0x002009BA
		public string EmailAddress { get; private set; }

		// Token: 0x060073F6 RID: 29686 RVA: 0x002027C4 File Offset: 0x002009C4
		public static RecipientToIndex Parse(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}
			string[] array = data.Split(new string[]
			{
				";"
			}, StringSplitOptions.None);
			if (array.Length < 3)
			{
				throw new FormatException(string.Format("Invalid input data: {0}", data));
			}
			RecipientItemType recipientType = RecipientItemType.To;
			Enum.TryParse<RecipientItemType>(array[0], out recipientType);
			string displayName = array[1];
			string emailAddress = array[2];
			return new RecipientToIndex(recipientType, displayName, emailAddress);
		}

		// Token: 0x060073F7 RID: 29687 RVA: 0x00202834 File Offset: 0x00200A34
		public override string ToString()
		{
			return string.Format("{1}{0}{2}{0}{3}", new object[]
			{
				";",
				this.RecipientType.ToString(),
				this.DisplayName,
				this.EmailAddress
			});
		}

		// Token: 0x060073F8 RID: 29688 RVA: 0x00202880 File Offset: 0x00200A80
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RecipientToIndex recipientToIndex = obj as RecipientToIndex;
			return recipientToIndex != null && (recipientToIndex.RecipientType == this.RecipientType && string.Compare(recipientToIndex.DisplayName, this.DisplayName, StringComparison.OrdinalIgnoreCase) == 0) && string.Compare(recipientToIndex.EmailAddress, this.EmailAddress, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060073F9 RID: 29689 RVA: 0x002028D7 File Offset: 0x00200AD7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060073FA RID: 29690 RVA: 0x002028DF File Offset: 0x00200ADF
		private static string SanitizeDisplayName(string displayName)
		{
			return displayName.Replace(";", string.Empty).Replace("|", string.Empty);
		}

		// Token: 0x040050FB RID: 20731
		internal const string SeparatorToken = ";";
	}
}
