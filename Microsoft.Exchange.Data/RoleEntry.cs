using System;
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Text;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics.Components.Authorization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000190 RID: 400
	[Serializable]
	public abstract class RoleEntry
	{
		// Token: 0x06000CDF RID: 3295 RVA: 0x00027F8F File Offset: 0x0002618F
		protected RoleEntry(string name, string[] parameters)
		{
			RoleEntry.ValidateName(name);
			this.SetParameters(parameters, true);
			this.name = name;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00027FAC File Offset: 0x000261AC
		protected RoleEntry()
		{
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x00027FB4 File Offset: 0x000261B4
		public static RoleEntry Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length < 3)
			{
				throw new FormatException(DataStrings.RoleEntryNameTooShort);
			}
			if (input[1] != ',')
			{
				throw new FormatException(DataStrings.RoleEntryStringMustBeCommaSeparated);
			}
			RoleEntry roleEntry = null;
			if (RoleEntry.roleEntryCache.TryGetValue(input, out roleEntry))
			{
				return roleEntry;
			}
			char c = input[0];
			switch (c)
			{
			case 'a':
				roleEntry = new ApplicationPermissionRoleEntry(input);
				goto IL_A9;
			case 'b':
				break;
			case 'c':
				roleEntry = new CmdletRoleEntry(input);
				goto IL_A9;
			default:
				if (c == 's')
				{
					roleEntry = new ScriptRoleEntry(input);
					goto IL_A9;
				}
				if (c == 'w')
				{
					roleEntry = new WebServiceRoleEntry(input);
					goto IL_A9;
				}
				break;
			}
			roleEntry = new UnknownRoleEntry(input);
			IL_A9:
			RoleEntry.roleEntryCache.Add(input, roleEntry);
			return roleEntry;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00028077 File Offset: 0x00026277
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0002807F File Offset: 0x0002627F
		internal SessionStateCommandEntry CachedIssEntry
		{
			get
			{
				return this.sessionStateCommandEntry;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CachedIssEntry");
				}
				this.sessionStateCommandEntry = value;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00028096 File Offset: 0x00026296
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0002809E File Offset: 0x0002629E
		internal int CachedIssEntryParameterCount
		{
			get
			{
				return this.cachedIssEntryParameterCount;
			}
			set
			{
				this.cachedIssEntryParameterCount = value;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x000280A8 File Offset: 0x000262A8
		protected int ExtractAndSetName(string entryString)
		{
			int num = 2;
			int num2 = entryString.IndexOf(',', num);
			int num3 = ((num2 < 0) ? entryString.Length : num2) - num;
			if (num3 < 1)
			{
				throw new FormatException(DataStrings.RoleEntryNameTooShort);
			}
			string text = entryString.Substring(num, num3);
			RoleEntry.ValidateName(text);
			this.name = text;
			return num2 + 1;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00028100 File Offset: 0x00026300
		protected void ExtractAndSetParameters(string entryString, int paramIndex)
		{
			if (paramIndex <= 0)
			{
				this.parameterCollection = RoleEntry.noParameters;
				this.parameters = RoleEntry.emptyArray;
				return;
			}
			string text = entryString.Substring(paramIndex);
			string[] value = text.Split(new char[]
			{
				','
			});
			this.SetParameters(value, true);
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002814C File Offset: 0x0002634C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x00028154 File Offset: 0x00026354
		public ICollection<string> Parameters
		{
			get
			{
				return this.parameterCollection;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002815C File Offset: 0x0002635C
		internal static void FormatParameters(string[] parameters)
		{
			if (parameters == null)
			{
				return;
			}
			bool flag = false;
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].Length < 1)
				{
					throw new FormatException(DataStrings.ParameterNameEmptyException);
				}
				if (RoleEntry.ContainsInvalidChars(parameters[i]))
				{
					throw new FormatException(DataStrings.ParameterNameInvalidCharException(parameters[i]));
				}
				if (!flag && i < parameters.Length - 1 && string.Compare(parameters[i], parameters[i + 1], StringComparison.OrdinalIgnoreCase) >= 0)
				{
					flag = true;
				}
			}
			if (flag)
			{
				Array.Sort<string>(parameters, StringComparer.OrdinalIgnoreCase);
				for (int j = 0; j < parameters.Length - 1; j++)
				{
					if (string.Equals(parameters[j], parameters[j + 1], StringComparison.OrdinalIgnoreCase))
					{
						throw new FormatException(DataStrings.DuplicateParameterException(parameters[j]));
					}
				}
			}
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00028214 File Offset: 0x00026414
		protected void SetParameters(ICollection<string> value, bool performValidation)
		{
			if (value == null)
			{
				this.parameterCollection = RoleEntry.noParameters;
				this.parameters = RoleEntry.emptyArray;
				return;
			}
			string[] array = new string[value.Count];
			value.CopyTo(array, 0);
			if (performValidation)
			{
				RoleEntry.FormatParameters(array);
			}
			this.parameters = array;
			this.parameterCollection = new ReadOnlyCollection<string>(array);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002826C File Offset: 0x0002646C
		internal static RoleEntry MergeParameters(IList<RoleEntry> entries)
		{
			if (entries == null)
			{
				throw new ArgumentNullException("entries");
			}
			if (entries.Count == 0)
			{
				throw new ArgumentException("entries");
			}
			if (entries.Count == 1)
			{
				return entries[0];
			}
			int num = 0;
			int count = entries[0].Parameters.Count;
			int index = 0;
			string b = entries[0].Name;
			CmdletRoleEntry cmdletRoleEntry = entries[0] as CmdletRoleEntry;
			string text = (cmdletRoleEntry == null) ? null : cmdletRoleEntry.PSSnapinName;
			int[] array = new int[entries.Count];
			for (int i = 0; i < entries.Count; i++)
			{
				RoleEntry roleEntry = entries[i];
				if (i > 0 && object.ReferenceEquals(roleEntry, entries[i - 1]))
				{
					array[i] = -1;
				}
				else
				{
					if (!string.Equals(roleEntry.Name, b, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException("entries");
					}
					if (text != null)
					{
						CmdletRoleEntry cmdletRoleEntry2 = roleEntry as CmdletRoleEntry;
						if (roleEntry == null)
						{
							throw new ArgumentException("entries");
						}
						if (!string.Equals(cmdletRoleEntry2.PSSnapinName, text, StringComparison.OrdinalIgnoreCase))
						{
							throw new ArgumentException("entries");
						}
					}
					else if (roleEntry == null)
					{
						throw new ArgumentException("entries");
					}
					if (count < roleEntry.Parameters.Count)
					{
						count = roleEntry.Parameters.Count;
						index = i;
					}
					num += roleEntry.Parameters.Count;
					if (roleEntry.Parameters.Count == 0)
					{
						array[i] = -1;
					}
				}
			}
			if (num == entries[0].Parameters.Count)
			{
				return entries[0];
			}
			List<string> list = new List<string>(num);
			string b2 = string.Empty;
			string text2 = null;
			for (;;)
			{
				for (int j = 0; j < entries.Count; j++)
				{
					if (array[j] != -1)
					{
						string text3 = entries[j].parameters[array[j]];
						if (string.Equals(text3, b2, StringComparison.OrdinalIgnoreCase))
						{
							int num2 = ++array[j];
							if (num2 >= entries[j].parameters.Length)
							{
								array[j] = -1;
								goto IL_242;
							}
							text3 = entries[j].parameters[array[j]];
						}
						if (text2 == null || string.Compare(text3, text2, StringComparison.OrdinalIgnoreCase) < 0)
						{
							text2 = text3;
						}
					}
					IL_242:;
				}
				if (text2 == null)
				{
					break;
				}
				list.Add(text2);
				b2 = text2;
				text2 = null;
			}
			if (list.Count == count)
			{
				return entries[index];
			}
			RoleEntry roleEntry2 = entries[0].Clone(list, false, null);
			string token = roleEntry2.ToADString();
			RoleEntry result = null;
			if (RoleEntry.roleEntryCache.TryGetValue(token, out result))
			{
				return result;
			}
			RoleEntry.roleEntryCache.Add(token, roleEntry2);
			return roleEntry2;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002853C File Offset: 0x0002673C
		internal RoleEntry IntersectParameters(RoleEntry entry)
		{
			List<string> list = null;
			bool flag = false;
			int i = 0;
			for (int j = 0; j < this.Parameters.Count; j++)
			{
				string text = this.parameters[j];
				int num = 1;
				while (i < entry.Parameters.Count)
				{
					num = string.Compare(text, entry.parameters[i], StringComparison.OrdinalIgnoreCase);
					if (num >= 0)
					{
						i++;
					}
					if (num <= 0)
					{
						break;
					}
				}
				if (num == 0)
				{
					if (flag)
					{
						list.Add(text);
					}
				}
				else if (!flag)
				{
					flag = true;
					list = new List<string>(this.Parameters.Count);
					for (int k = 0; k < j; k++)
					{
						list.Add(this.parameters[k]);
					}
				}
			}
			if (!flag)
			{
				return this;
			}
			CmdletRoleEntry cmdletRoleEntry = this as CmdletRoleEntry;
			if (cmdletRoleEntry != null)
			{
				return new CmdletRoleEntry(cmdletRoleEntry.Name, cmdletRoleEntry.PSSnapinName, list.ToArray());
			}
			throw new NotSupportedException("Parameter intersection for RoleEntry other than CmdletRoleEntry is not supported.");
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002862C File Offset: 0x0002682C
		internal bool ContainsAllParameters(IList<string> parametersToCheck)
		{
			if (parametersToCheck == null)
			{
				throw new ArgumentNullException("parametersToCheck");
			}
			for (int i = 0; i < parametersToCheck.Count; i++)
			{
				if (!this.ContainsParameter(parametersToCheck[i]))
				{
					ExTraceGlobals.AccessDeniedTracer.TraceError<string, string>(0L, "Role entry {0} does not contain parameter {1}", this.Name, parametersToCheck[i]);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00028688 File Offset: 0x00026888
		internal bool ContainsAllParametersFromRoleEntry(RoleEntry roleEntryToCheck, out ICollection<string> missingParameters)
		{
			missingParameters = null;
			if (roleEntryToCheck == null)
			{
				throw new ArgumentNullException("roleEntryToCheck");
			}
			for (int i = 0; i < roleEntryToCheck.parameters.Length; i++)
			{
				if (!this.ContainsParameter(roleEntryToCheck.parameters[i]))
				{
					missingParameters = (missingParameters ?? new List<string>());
					missingParameters.Add(roleEntryToCheck.parameters[i]);
				}
			}
			return missingParameters == null;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x000286F0 File Offset: 0x000268F0
		internal bool ContainsAnyParameter(IList<string> parametersToCheck)
		{
			if (parametersToCheck == null)
			{
				throw new ArgumentNullException("parametersToCheck");
			}
			for (int i = 0; i < parametersToCheck.Count; i++)
			{
				if (this.ContainsParameter(parametersToCheck[i]))
				{
					return true;
				}
			}
			ExTraceGlobals.AccessDeniedTracer.TraceError<string, int>(0L, "Role entry {0} does not contain any parameters from the specified list of {1}", this.Name, parametersToCheck.Count);
			return false;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0002874B File Offset: 0x0002694B
		internal bool ContainsParameter(string parameterToCheck)
		{
			return Array.BinarySearch<string>(this.parameters, parameterToCheck, StringComparer.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00028764 File Offset: 0x00026964
		internal bool TryMatchRoleEntryToArrayAndRemoveKnownParameters(RoleEntry[] availableEntries, out RoleEntry filteredEntry)
		{
			int num = Array.BinarySearch<RoleEntry>(availableEntries, this, RoleEntry.NameComparer);
			if (num < 0)
			{
				filteredEntry = this;
				return false;
			}
			new List<string>(this.Parameters);
			RoleEntry roleEntry = availableEntries[num];
			ICollection<string> collection;
			if (roleEntry.ContainsAllParametersFromRoleEntry(this, out collection))
			{
				filteredEntry = null;
			}
			else
			{
				filteredEntry = this.Clone(collection);
			}
			return true;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000287B4 File Offset: 0x000269B4
		internal RoleEntry FindAndRemoveMatchingParameters(RoleEntry[] availableEntries)
		{
			int num = Array.BinarySearch<RoleEntry>(availableEntries, this, RoleEntry.NameComparer);
			if (num < 0)
			{
				return this;
			}
			new List<string>(this.Parameters);
			RoleEntry roleEntry = availableEntries[num];
			ICollection<string> collection;
			if (roleEntry.ContainsAllParametersFromRoleEntry(this, out collection))
			{
				return null;
			}
			return this.Clone(collection);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000287F8 File Offset: 0x000269F8
		internal RoleEntry FindAndIntersectWithMatchingParameters(RoleEntry[] availableEntries)
		{
			int num = Array.BinarySearch<RoleEntry>(availableEntries, this, RoleEntry.NameComparer);
			if (num < 0)
			{
				return null;
			}
			new List<string>(this.Parameters);
			RoleEntry entry = availableEntries[num];
			return this.IntersectParameters(entry);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00028830 File Offset: 0x00026A30
		protected bool Equals(RoleEntry other)
		{
			if (other == null)
			{
				return false;
			}
			if (!this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase) || this.Parameters.Count != other.Parameters.Count)
			{
				return false;
			}
			for (int i = 0; i < this.parameters.Length; i++)
			{
				if (!this.parameters[i].Equals(other.parameters[i], StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002889E File Offset: 0x00026A9E
		public static bool operator ==(RoleEntry left, RoleEntry right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000288A7 File Offset: 0x00026AA7
		public static bool operator !=(RoleEntry left, RoleEntry right)
		{
			return !(left == right);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000288B3 File Offset: 0x00026AB3
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RoleEntry);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000288C1 File Offset: 0x00026AC1
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000288CE File Offset: 0x00026ACE
		internal int GetInstanceHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000288D8 File Offset: 0x00026AD8
		internal static int CompareRoleEntriesByName(RoleEntry a, RoleEntry b)
		{
			CmdletRoleEntry cmdletRoleEntry = a as CmdletRoleEntry;
			CmdletRoleEntry cmdletRoleEntry2 = b as CmdletRoleEntry;
			if (a == null || b == null)
			{
				if (a == null && b == null)
				{
					return 0;
				}
				if (!(a == null))
				{
					return 1;
				}
				return -1;
			}
			else if (cmdletRoleEntry == null != (cmdletRoleEntry2 == null))
			{
				if (!(cmdletRoleEntry == null))
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (cmdletRoleEntry != null)
				{
					return string.Compare(cmdletRoleEntry.FullName, cmdletRoleEntry2.FullName, StringComparison.OrdinalIgnoreCase);
				}
				return string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00028974 File Offset: 0x00026B74
		internal static int CompareRoleEntriesByNameAndInstanceHashCode(RoleEntry a, RoleEntry b)
		{
			int num = RoleEntry.CompareRoleEntriesByName(a, b);
			if (num != 0 || (a == null && b == null))
			{
				return num;
			}
			return a.GetInstanceHashCode().CompareTo(b.GetInstanceHashCode());
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x000289B4 File Offset: 0x00026BB4
		protected string ToString(string snapin)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			if (snapin != null)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(snapin);
				stringBuilder.Append(") ");
			}
			stringBuilder.Append(this.Name);
			foreach (string value in this.Parameters)
			{
				stringBuilder.Append(" -");
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00028A50 File Offset: 0x00026C50
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00028A5C File Offset: 0x00026C5C
		protected string ToADString(char typeHint, string snapin, string versionInfo)
		{
			StringBuilder stringBuilder = new StringBuilder(128);
			stringBuilder.Append(typeHint);
			stringBuilder.Append(",");
			stringBuilder.Append(this.Name);
			if (versionInfo != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(versionInfo);
			}
			if (snapin != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(snapin);
			}
			foreach (string value in this.Parameters)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000D00 RID: 3328
		public abstract string ToADString();

		// Token: 0x06000D01 RID: 3329 RVA: 0x00028B18 File Offset: 0x00026D18
		internal RoleEntry Clone(ICollection<string> parameters)
		{
			return this.Clone(parameters, true, null);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00028B23 File Offset: 0x00026D23
		internal RoleEntry Clone(ICollection<string> parameters, string newName)
		{
			return this.Clone(parameters, true, newName);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00028B30 File Offset: 0x00026D30
		private RoleEntry Clone(ICollection<string> parameters, bool performValidation, string newName = null)
		{
			RoleEntry roleEntry;
			if (this is CmdletRoleEntry)
			{
				roleEntry = new CmdletRoleEntry((CmdletRoleEntry)this, newName);
			}
			else if (this is ScriptRoleEntry)
			{
				roleEntry = new ScriptRoleEntry();
			}
			else if (this is ApplicationPermissionRoleEntry)
			{
				roleEntry = new ApplicationPermissionRoleEntry();
			}
			else if (this is WebServiceRoleEntry)
			{
				roleEntry = new WebServiceRoleEntry();
			}
			else
			{
				roleEntry = new UnknownRoleEntry();
			}
			if (string.IsNullOrWhiteSpace(newName))
			{
				roleEntry.name = this.Name;
			}
			else
			{
				roleEntry.name = newName;
			}
			roleEntry.SetParameters(parameters, performValidation);
			return roleEntry;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00028BB0 File Offset: 0x00026DB0
		protected static bool ContainsInvalidChars(string valueToCheck)
		{
			return RoleEntry.ContainsInvalidChars(valueToCheck, 0, valueToCheck.Length);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00028BBF File Offset: 0x00026DBF
		protected static bool ContainsInvalidChars(string valueToCheck, int startIndex, int count)
		{
			return -1 != valueToCheck.IndexOfAny(RoleEntry.invalidCharacters, startIndex, count);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00028BD4 File Offset: 0x00026DD4
		internal static void ValidateName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new FormatException(DataStrings.RoleEntryNameInvalidException(name ?? string.Empty));
			}
			if (RoleEntry.ContainsInvalidChars(name))
			{
				throw new FormatException(DataStrings.RoleEntryNameInvalidException(name));
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00028C14 File Offset: 0x00026E14
		internal RoleEntry MapToPreviousVersion()
		{
			return this;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00028C24 File Offset: 0x00026E24
		internal string MapParameterToPreviousVersion(string newParameter)
		{
			return newParameter;
		}

		// Token: 0x040007DD RID: 2013
		internal const char Separator = ',';

		// Token: 0x040007DE RID: 2014
		internal const char VersionPrefix = 'v';

		// Token: 0x040007DF RID: 2015
		internal const string SeparatorString = ",";

		// Token: 0x040007E0 RID: 2016
		private static ReadOnlyCollection<string> noParameters = new ReadOnlyCollection<string>(new string[0]);

		// Token: 0x040007E1 RID: 2017
		private static string[] emptyArray = new string[0];

		// Token: 0x040007E2 RID: 2018
		private static MruDictionaryCache<string, RoleEntry> roleEntryCache = new MruDictionaryCache<string, RoleEntry>(5000, 1440);

		// Token: 0x040007E3 RID: 2019
		private string name;

		// Token: 0x040007E4 RID: 2020
		private ReadOnlyCollection<string> parameterCollection;

		// Token: 0x040007E5 RID: 2021
		private string[] parameters;

		// Token: 0x040007E6 RID: 2022
		[NonSerialized]
		private SessionStateCommandEntry sessionStateCommandEntry;

		// Token: 0x040007E7 RID: 2023
		private int cachedIssEntryParameterCount;

		// Token: 0x040007E8 RID: 2024
		private static readonly char[] invalidCharacters = new char[]
		{
			','
		};

		// Token: 0x040007E9 RID: 2025
		internal static readonly IComparer<RoleEntry> NameComparer = new RoleEntry.NameRoleEntryComparer();

		// Token: 0x02000191 RID: 401
		private class NameRoleEntryComparer : IComparer<RoleEntry>
		{
			// Token: 0x06000D0A RID: 3338 RVA: 0x00028C80 File Offset: 0x00026E80
			public int Compare(RoleEntry a, RoleEntry b)
			{
				return RoleEntry.CompareRoleEntriesByName(a, b);
			}
		}
	}
}
