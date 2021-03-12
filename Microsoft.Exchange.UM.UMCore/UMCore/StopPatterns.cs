using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001E8 RID: 488
	internal class StopPatterns : IEnumerable<string>, IEnumerable
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		static StopPatterns()
		{
			StopPatterns.anyKeyOnly.Add("anyKey", true);
			StopPatterns.empty = new StopPatterns();
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00040B0E File Offset: 0x0003ED0E
		public StopPatterns()
		{
			this.patternBargeinMap = new Dictionary<string, bool>();
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00040B21 File Offset: 0x0003ED21
		public StopPatterns(int initialCapacity)
		{
			this.patternBargeinMap = new Dictionary<string, bool>(initialCapacity);
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00040B35 File Offset: 0x0003ED35
		public static StopPatterns AnyKeyOnly
		{
			get
			{
				return StopPatterns.anyKeyOnly;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00040B3C File Offset: 0x0003ED3C
		public static StopPatterns Empty
		{
			get
			{
				return StopPatterns.empty;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x00040B43 File Offset: 0x0003ED43
		public int Count
		{
			get
			{
				return this.patternBargeinMap.Count;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x00040B50 File Offset: 0x0003ED50
		public bool ContainsAnyKey
		{
			get
			{
				return this.containsAnyKey;
			}
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00040B58 File Offset: 0x0003ED58
		public static bool IsAnyKeyInput(ReadOnlyCollection<byte> digits)
		{
			bool result = false;
			if (digits.Count > 0)
			{
				result = true;
				foreach (byte b in digits)
				{
					if (b == 35 || b == 42)
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00040BB4 File Offset: 0x0003EDB4
		public void Add(string pattern, bool bargeIn)
		{
			this.patternBargeinMap[pattern] = bargeIn;
			this.containsAnyKey |= pattern.Equals("anyKey", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00040BDC File Offset: 0x0003EDDC
		public void Add(StopPatterns patterns)
		{
			foreach (KeyValuePair<string, bool> keyValuePair in patterns.patternBargeinMap)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00040C3C File Offset: 0x0003EE3C
		public IEnumerator<string> GetEnumerator()
		{
			return this.patternBargeinMap.Keys.GetEnumerator();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00040C53 File Offset: 0x0003EE53
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.patternBargeinMap.Keys.GetEnumerator();
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00040C6C File Offset: 0x0003EE6C
		public void CountMatches(ReadOnlyCollection<byte> input, out int partialMatches, out int completeMatches)
		{
			partialMatches = (completeMatches = 0);
			foreach (string stopPattern in this)
			{
				bool flag;
				bool flag2;
				StopPatterns.ComputeMatch(input, stopPattern, out flag, out flag2);
				if (flag)
				{
					partialMatches++;
				}
				if (flag2)
				{
					completeMatches++;
				}
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00040CD8 File Offset: 0x0003EED8
		public bool IsBargeInPattern(ReadOnlyCollection<byte> input)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			foreach (KeyValuePair<string, bool> keyValuePair in this.patternBargeinMap)
			{
				bool flag4;
				bool flag5;
				StopPatterns.ComputeMatch(input, keyValuePair.Key, out flag4, out flag5);
				if (flag4)
				{
					flag2 = true;
					flag3 &= keyValuePair.Value;
					if (flag5)
					{
						flag = keyValuePair.Value;
						break;
					}
				}
			}
			return flag || (flag2 && flag3);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00040D68 File Offset: 0x0003EF68
		private static void ComputeMatch(ReadOnlyCollection<byte> input, string stopPattern, out bool partial, out bool complete)
		{
			partial = (complete = false);
			if (StopPatterns.IsAnyKeyPattern(stopPattern) && StopPatterns.IsAnyKeyInput(input))
			{
				partial = true;
				complete = false;
			}
			else if (StopPatterns.IsDiagnosticSequence(input))
			{
				partial = true;
				complete = true;
			}
			else if (input.Count <= stopPattern.Length)
			{
				partial = true;
				complete = (input.Count == stopPattern.Length);
				for (int i = 0; i < input.Count; i++)
				{
					if ((char)input[i] != stopPattern[i])
					{
						partial = (complete = false);
						break;
					}
				}
			}
			ExAssert.RetailAssert(!complete || partial, "A sequence cannot be a complete match without also being a partial match!");
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00040E04 File Offset: 0x0003F004
		private static bool IsAnyKeyPattern(string stopPattern)
		{
			return stopPattern != null && stopPattern.Equals("anyKey", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00040E28 File Offset: 0x0003F028
		private static bool IsDiagnosticSequence(ReadOnlyCollection<byte> digits)
		{
			bool result = false;
			if (digits.Count > 0)
			{
				switch (digits[0])
				{
				case 65:
					result = true;
					break;
				case 66:
					result = true;
					break;
				case 67:
					result = true;
					break;
				case 68:
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x04000AE8 RID: 2792
		private static StopPatterns anyKeyOnly = new StopPatterns();

		// Token: 0x04000AE9 RID: 2793
		private static StopPatterns empty;

		// Token: 0x04000AEA RID: 2794
		private Dictionary<string, bool> patternBargeinMap;

		// Token: 0x04000AEB RID: 2795
		private bool containsAnyKey;
	}
}
