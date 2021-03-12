using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public class DialGroupEntry
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003035A File Offset: 0x0002E55A
		public DialGroupEntry(string name, string mask, string number, string comment)
		{
			this.name = DialGroupEntryName.Parse(name);
			this.numberMask = DialGroupEntryNumberMask.Parse(mask);
			this.dialedNumber = DialGroupEntryDialedNumber.Parse(number);
			this.comment = DialGroupEntryComment.Parse(comment);
			this.Validate();
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003039C File Offset: 0x0002E59C
		public DialGroupEntry(PSObject importedObject)
		{
			try
			{
				this.name = DialGroupEntryName.Parse((string)importedObject.Properties["Name"].Value);
				this.numberMask = DialGroupEntryNumberMask.Parse((string)importedObject.Properties["NumberMask"].Value);
				this.dialedNumber = DialGroupEntryDialedNumber.Parse((string)importedObject.Properties["DialedNumber"].Value);
				this.comment = DialGroupEntryComment.Parse((string)importedObject.Properties["Comment"].Value);
			}
			catch (NullReferenceException)
			{
				throw new FormatException(DataStrings.InvalidDialGroupEntryCsvFormat);
			}
			this.Validate();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003046C File Offset: 0x0002E66C
		private void Validate()
		{
			if (this.NumberMask.IndexOf('*') != -1 && this.DialedNumber.IndexOf('x') != -1)
			{
				throw new FormatException(DataStrings.InvalidDialledNumberFormatA);
			}
			if (string.CompareOrdinal(this.NumberMask, "*") == 0 && !DialGroupEntry.digitRegex.IsMatch(this.DialedNumber) && string.CompareOrdinal(this.DialedNumber, "*") != 0)
			{
				throw new FormatException(DataStrings.InvalidDialledNumberFormatB);
			}
			if (DialGroupEntry.digitRegex.IsMatch(this.NumberMask) && string.CompareOrdinal(this.DialedNumber, "*") == -1 && !DialGroupEntry.digitRegex.IsMatch(this.DialedNumber))
			{
				throw new FormatException(DataStrings.InvalidDialledNumberFormatC);
			}
			if (this.NumberMask.IndexOf('x') == -1 && this.DialedNumber.IndexOf('x') != -1)
			{
				throw new FormatException(DataStrings.InvalidDialledNumberFormatD);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00030566 File Offset: 0x0002E766
		public string Name
		{
			get
			{
				return this.name.ToString();
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00030573 File Offset: 0x0002E773
		public string NumberMask
		{
			get
			{
				return this.numberMask.ToString();
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00030580 File Offset: 0x0002E780
		public string DialedNumber
		{
			get
			{
				return this.dialedNumber.ToString();
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0003058D File Offset: 0x0002E78D
		public string Comment
		{
			get
			{
				return this.comment.ToString();
			}
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0003059C File Offset: 0x0002E79C
		public static DialGroupEntry Parse(string line)
		{
			if (string.IsNullOrEmpty(line))
			{
				return null;
			}
			line = line.Trim();
			string[] array = line.Split(new char[]
			{
				','
			});
			if (array.Length < 3 || array.Length > 4)
			{
				throw new FormatException(DataStrings.InvalidDialGroupEntryFormat);
			}
			return new DialGroupEntry(array[0], array[1], array[2], (array.Length > 3) ? array[3] : null);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00030608 File Offset: 0x0002E808
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(this.Comment))
			{
				stringBuilder.AppendFormat("{0},{1},{2},{3}", new object[]
				{
					this.Name,
					this.NumberMask,
					this.DialedNumber,
					this.Comment
				});
			}
			else
			{
				stringBuilder.AppendFormat("{0},{1},{2}", this.Name, this.NumberMask, this.DialedNumber);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00030688 File Offset: 0x0002E888
		public static bool ValidateGroup(ICollection<DialGroupEntry> configuredGroups, ICollection<string> allowedGroups, bool inCountryGroup, out LocalizedString LocalizedError)
		{
			LocalizedError = LocalizedString.Empty;
			string group = inCountryGroup ? "ConfiguredInCountryOrRegionGroups" : "ConfiguredInternationalGroups";
			if (allowedGroups == null || allowedGroups.Count == 0)
			{
				return true;
			}
			if (allowedGroups.Count > 0 && (configuredGroups == null || configuredGroups.Count == 0))
			{
				LocalizedError = DataStrings.DialGroupNotSpecifiedOnDialPlan(group);
				return false;
			}
			foreach (string strA in allowedGroups)
			{
				bool flag = false;
				foreach (DialGroupEntry dialGroupEntry in configuredGroups)
				{
					if (string.Compare(strA, dialGroupEntry.Name, StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					LocalizedError = DataStrings.DialGroupNotSpecifiedOnDialPlanB(strA, group);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0003077C File Offset: 0x0002E97C
		public override bool Equals(object obj)
		{
			DialGroupEntry dialGroupEntry = obj as DialGroupEntry;
			return dialGroupEntry != null && this.ToString().Equals(dialGroupEntry.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000307AD File Offset: 0x0002E9AD
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x04000971 RID: 2417
		private const char Wildcard = '*';

		// Token: 0x04000972 RID: 2418
		private const char DigitPlaceholder = 'x';

		// Token: 0x04000973 RID: 2419
		private DialGroupEntryName name;

		// Token: 0x04000974 RID: 2420
		private DialGroupEntryNumberMask numberMask;

		// Token: 0x04000975 RID: 2421
		private DialGroupEntryDialedNumber dialedNumber;

		// Token: 0x04000976 RID: 2422
		private DialGroupEntryComment comment;

		// Token: 0x04000977 RID: 2423
		private static Regex digitRegex = new Regex("^\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
	}
}
