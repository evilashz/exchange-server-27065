using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200017B RID: 379
	[Serializable]
	public class OrganizationSummaryEntry : IEquatable<OrganizationSummaryEntry>, IComparable<OrganizationSummaryEntry>, ICloneable
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002679A File Offset: 0x0002499A
		internal static bool IsValidKeyForCurrentRelease(string key)
		{
			return key.Equals(OrganizationSummaryEntry.SummaryInfoUpdateDate) || Array.LastIndexOf<string>(OrganizationSummaryEntry.SummaryInfoKeys, key) >= 0;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000267BC File Offset: 0x000249BC
		internal static bool IsValidKeyForCurrentAndPreviousRelease(string key)
		{
			return key.Equals(OrganizationSummaryEntry.SummaryInfoUpdateDate) || Array.LastIndexOf<string>(OrganizationSummaryEntry.SummaryInfoKeysInCurrentAndPreviousRelease, key) >= 0;
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x000267DE File Offset: 0x000249DE
		public int NumberOfFields
		{
			get
			{
				return this.numberOfFields;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x000267E6 File Offset: 0x000249E6
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x000267EE File Offset: 0x000249EE
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x000267F6 File Offset: 0x000249F6
		public bool HasError
		{
			get
			{
				return this.hasError;
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00026800 File Offset: 0x00024A00
		public OrganizationSummaryEntry(string key, string value, bool hasError)
		{
			if (!OrganizationSummaryEntry.ValidateKey(key))
			{
				throw new ArgumentException(DataStrings.InvalidOrganizationSummaryEntryKey(key));
			}
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(DataStrings.InvalidOrganizationSummaryEntryValue(value));
			}
			this.numberOfFields = 3;
			this.key = key;
			this.value = value;
			this.hasError = hasError;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00026864 File Offset: 0x00024A64
		public OrganizationSummaryEntry(string s)
		{
			OrganizationSummaryEntry organizationSummaryEntry;
			if (!OrganizationSummaryEntry.TryParse(s, out organizationSummaryEntry))
			{
				throw new FormatException(DataStrings.InvalidOrganizationSummaryEntryFormat(s));
			}
			this.numberOfFields = s.Split(new char[]
			{
				OrganizationSummaryEntry.fieldSeparator
			}).Length;
			this.key = organizationSummaryEntry.Key;
			this.value = organizationSummaryEntry.Value;
			this.hasError = organizationSummaryEntry.HasError;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000268D4 File Offset: 0x00024AD4
		private static bool ValidateKey(string key)
		{
			return !string.IsNullOrEmpty(key);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000268DF File Offset: 0x00024ADF
		public static OrganizationSummaryEntry Parse(string s)
		{
			return new OrganizationSummaryEntry(s);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x000268E7 File Offset: 0x00024AE7
		private OrganizationSummaryEntry(OrganizationSummaryEntry from)
		{
			this.key = from.Key;
			this.value = from.Value;
			this.hasError = from.HasError;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00026913 File Offset: 0x00024B13
		public static bool operator ==(OrganizationSummaryEntry value1, OrganizationSummaryEntry value2)
		{
			if (value1 != null)
			{
				return value1.Equals(value2);
			}
			return value2 == null;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00026924 File Offset: 0x00024B24
		public static bool operator !=(OrganizationSummaryEntry value1, OrganizationSummaryEntry value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00026930 File Offset: 0x00024B30
		public bool Equals(OrganizationSummaryEntry entry)
		{
			return entry != null && this.Key == entry.Key && this.Value == entry.Value && this.HasError == entry.HasError;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002696B File Offset: 0x00024B6B
		public override bool Equals(object entry)
		{
			return this.Equals(entry as OrganizationSummaryEntry);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002697C File Offset: 0x00024B7C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Key,
				OrganizationSummaryEntry.fieldSeparator,
				this.Value,
				OrganizationSummaryEntry.fieldSeparator,
				this.HasError
			});
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000269D0 File Offset: 0x00024BD0
		public override int GetHashCode()
		{
			return this.Key.GetHashCode() + this.Value.GetHashCode() + this.HasError.GetHashCode();
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00026A04 File Offset: 0x00024C04
		public static bool TryParse(string s, out OrganizationSummaryEntry entry)
		{
			entry = null;
			if (!string.IsNullOrEmpty(s))
			{
				string[] array = s.Split(new char[]
				{
					OrganizationSummaryEntry.fieldSeparator
				});
				if (array != null && array.Length >= 2)
				{
					string text = array[0];
					string text2 = array[1];
					bool flag = false;
					if (OrganizationSummaryEntry.ValidateKey(text) && !string.IsNullOrEmpty(text2) && (array.Length == 2 || bool.TryParse(array[2], out flag)))
					{
						entry = new OrganizationSummaryEntry(text, text2, flag);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00026A78 File Offset: 0x00024C78
		public int CompareTo(OrganizationSummaryEntry entry)
		{
			if (entry == null)
			{
				return 1;
			}
			return this.Key.CompareTo(entry.Key);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00026A90 File Offset: 0x00024C90
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00026A98 File Offset: 0x00024C98
		public OrganizationSummaryEntry Clone()
		{
			return new OrganizationSummaryEntry(this);
		}

		// Token: 0x04000776 RID: 1910
		internal const int NumberOfFieldsConst = 3;

		// Token: 0x04000777 RID: 1911
		internal static char fieldSeparator = ',';

		// Token: 0x04000778 RID: 1912
		internal static readonly string[] SummaryInfoKeys = new string[]
		{
			"TotalDatabases",
			"TotalDatabasesCopy",
			"TotalDatabasesCopyUnhealthy",
			"TotalCALMailboxes",
			"StandardCALs",
			"EnterpriseCALs",
			"TotalExchangeServers",
			"Total2009ExchangeServers",
			"Total2007ExchangeServers",
			"Total2003ExchangeServers",
			"TotalMailboxes",
			"TotalDistributionGroups",
			"TotalDynamicDistributionGroups",
			"TotalMailContacts",
			"TotalMailUsers",
			"TotalLegacyMailbox",
			"TotalMessagingRecordManagementUser",
			"TotalJounalingUser",
			"TotalOWAUser",
			"TotalActiveSyncUser",
			"TotalUnifiedMessagingUser",
			"TotalMAPIUser",
			"TotalPOP3User",
			"TotalIMAP4User",
			"TotalMailboxServers",
			"TotalUMServers",
			"TotalTransportServers",
			"TotalClientAccessServers",
			"TotalUnlicensedExchangeServers",
			"TotalRecipients"
		};

		// Token: 0x04000779 RID: 1913
		internal static readonly string[] SummaryInfoKeysInCurrentAndPreviousRelease = new string[]
		{
			"TotalDatabases",
			"TotalDatabasesCopy",
			"TotalDatabasesCopyUnhealthy",
			"TotalCALMailboxes",
			"StandardCALs",
			"EnterpriseCALs",
			"TotalExchangeServers",
			"Total2009ExchangeServers",
			"Total2007ExchangeServers",
			"Total2003ExchangeServers",
			"TotalMailboxes",
			"TotalDistributionGroups",
			"TotalDynamicDistributionGroups",
			"TotalMailContacts",
			"TotalMailUsers",
			"TotalLegacyMailbox",
			"TotalMessagingRecordManagementUser",
			"TotalJounalingUser",
			"TotalOWAUser",
			"TotalActiveSyncUser",
			"TotalUnifiedMessagingUser",
			"TotalMAPIUser",
			"TotalPOP3User",
			"TotalIMAP4User",
			"TotalMailboxServers",
			"TotalUMServers",
			"TotalTransportServers",
			"TotalClientAccessServers",
			"TotalUnlicensedExchangeServers",
			"TotalRecipients"
		};

		// Token: 0x0400077A RID: 1914
		internal static readonly string SummaryInfoUpdateDate = "UpdateDate";

		// Token: 0x0400077B RID: 1915
		private int numberOfFields;

		// Token: 0x0400077C RID: 1916
		private string key;

		// Token: 0x0400077D RID: 1917
		private string value;

		// Token: 0x0400077E RID: 1918
		private bool hasError;
	}
}
