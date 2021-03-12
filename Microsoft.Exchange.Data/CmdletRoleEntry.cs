using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000192 RID: 402
	[Serializable]
	public sealed class CmdletRoleEntry : RoleEntry, IEquatable<CmdletRoleEntry>
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00028C94 File Offset: 0x00026E94
		internal CmdletRoleEntry(string name, string snapinName, string[] parameters) : base(name, parameters)
		{
			if (string.IsNullOrEmpty(snapinName))
			{
				throw new FormatException(DataStrings.SnapinNameTooShort);
			}
			if (RoleEntry.ContainsInvalidChars(snapinName))
			{
				throw new FormatException(DataStrings.SnapinNameInvalidCharException(snapinName));
			}
			this.snapinName = snapinName;
			this.fullName = this.snapinName + "\\" + base.Name;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00028D00 File Offset: 0x00026F00
		internal CmdletRoleEntry(string entryString)
		{
			int num = base.ExtractAndSetName(entryString);
			if (num <= 0 || num == entryString.Length)
			{
				throw new FormatException(DataStrings.SnapinNameTooShort);
			}
			num = this.ParseVersion(entryString, num);
			int num2 = entryString.IndexOf(',', num) + 1;
			int num3 = ((num2 <= 0) ? entryString.Length : (num2 - 1)) - num;
			if (num3 < 1)
			{
				throw new FormatException(DataStrings.SnapinNameTooShort);
			}
			base.ExtractAndSetParameters(entryString, num2);
			string text = entryString.Substring(num, num3);
			if (RoleEntry.ContainsInvalidChars(text))
			{
				throw new FormatException(DataStrings.SnapinNameInvalidCharException(text));
			}
			this.snapinName = text;
			this.fullName = this.snapinName + "\\" + base.Name;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00028DC0 File Offset: 0x00026FC0
		private int ParseVersion(string entryString, int snapinIndex)
		{
			if (snapinIndex + 8 >= entryString.Length || entryString.IndexOf('v', snapinIndex, 1) != snapinIndex || entryString.IndexOf('v', snapinIndex + 4, 1) != snapinIndex + 4 || !char.IsDigit(entryString, snapinIndex + 1) || !char.IsDigit(entryString, snapinIndex + 2) || !char.IsDigit(entryString, snapinIndex + 5) || !char.IsDigit(entryString, snapinIndex + 6) || entryString.IndexOf(',', snapinIndex + 3, 1) != snapinIndex + 3 || entryString.IndexOf(',', snapinIndex + 7, 1) != snapinIndex + 7)
			{
				return snapinIndex;
			}
			this.versionInfo = entryString.Substring(snapinIndex, 7);
			return snapinIndex + 8;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00028E59 File Offset: 0x00027059
		internal CmdletRoleEntry(CmdletRoleEntry entryToClone)
		{
			this.snapinName = entryToClone.snapinName;
			this.fullName = entryToClone.fullName;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00028E79 File Offset: 0x00027079
		internal CmdletRoleEntry(CmdletRoleEntry entryToClone, string newName) : this(entryToClone)
		{
			if (!string.IsNullOrWhiteSpace(newName))
			{
				this.fullName = entryToClone.snapinName + "\\" + newName;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00028EA1 File Offset: 0x000270A1
		public string PSSnapinName
		{
			get
			{
				return this.snapinName;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00028EA9 File Offset: 0x000270A9
		internal string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00028EB1 File Offset: 0x000270B1
		public override string ToString()
		{
			return base.ToString(this.PSSnapinName);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00028EBF File Offset: 0x000270BF
		public override string ToADString()
		{
			return base.ToADString('c', this.PSSnapinName, this.versionInfo);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00028ED8 File Offset: 0x000270D8
		internal new static void ValidateName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new FormatException(DataStrings.CmdletFullNameFormatException(name ?? string.Empty));
			}
			int num = name.IndexOf(',');
			if (-1 == num)
			{
				if (RoleEntry.ContainsInvalidChars(name))
				{
					throw new FormatException(DataStrings.CmdletFullNameFormatException(name));
				}
			}
			else
			{
				if (num == 0 || name.Length - 1 == num)
				{
					throw new FormatException(DataStrings.CmdletFullNameFormatException(name));
				}
				if (RoleEntry.ContainsInvalidChars(name, 0, num))
				{
					throw new FormatException(DataStrings.CmdletFullNameFormatException(name));
				}
				if (RoleEntry.ContainsInvalidChars(name, 1 + num, name.Length - num - 1))
				{
					throw new FormatException(DataStrings.CmdletFullNameFormatException(name));
				}
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00028F8D File Offset: 0x0002718D
		public bool Equals(CmdletRoleEntry other)
		{
			return other != null && this.PSSnapinName.Equals(other.PSSnapinName, StringComparison.OrdinalIgnoreCase) && base.Equals(other);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00028FB1 File Offset: 0x000271B1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CmdletRoleEntry);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00028FBF File Offset: 0x000271BF
		public override int GetHashCode()
		{
			return base.Name.GetHashCode();
		}

		// Token: 0x040007EA RID: 2026
		internal const char TypeHint = 'c';

		// Token: 0x040007EB RID: 2027
		private string snapinName;

		// Token: 0x040007EC RID: 2028
		private string fullName;

		// Token: 0x040007ED RID: 2029
		private string versionInfo;
	}
}
