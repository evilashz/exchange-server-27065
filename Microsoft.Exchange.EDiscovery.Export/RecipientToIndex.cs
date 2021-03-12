using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200001C RID: 28
	internal class RecipientToIndex
	{
		// Token: 0x060000DD RID: 221 RVA: 0x0000423C File Offset: 0x0000243C
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000429B File Offset: 0x0000249B
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000042A3 File Offset: 0x000024A3
		public RecipientItemType RecipientType { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000042AC File Offset: 0x000024AC
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000042B4 File Offset: 0x000024B4
		public string DisplayName { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000042C5 File Offset: 0x000024C5
		public string EmailAddress { get; private set; }

		// Token: 0x060000E4 RID: 228 RVA: 0x000042D0 File Offset: 0x000024D0
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

		// Token: 0x060000E5 RID: 229 RVA: 0x00004340 File Offset: 0x00002540
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

		// Token: 0x060000E6 RID: 230 RVA: 0x0000438C File Offset: 0x0000258C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RecipientToIndex recipientToIndex = obj as RecipientToIndex;
			return recipientToIndex != null && (recipientToIndex.RecipientType == this.RecipientType && string.Compare(recipientToIndex.DisplayName, this.DisplayName, StringComparison.OrdinalIgnoreCase) == 0) && string.Compare(recipientToIndex.EmailAddress, this.EmailAddress, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000043E3 File Offset: 0x000025E3
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000043F0 File Offset: 0x000025F0
		private static string SanitizeDisplayName(string displayName)
		{
			return displayName.Replace(";", string.Empty).Replace("|", string.Empty);
		}

		// Token: 0x0400006C RID: 108
		internal const string SeparatorToken = ";";
	}
}
