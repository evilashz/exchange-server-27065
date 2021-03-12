using System;
using Microsoft.Ceres.DataLossPrevention.Common;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;

namespace Microsoft.Ceres.DataLossPrevention.Query
{
	// Token: 0x02000011 RID: 17
	internal class ClassificationDecoding
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003C75 File Offset: 0x00001E75
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003C7D File Offset: 0x00001E7D
		public long ConfidenceMaximum { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003C86 File Offset: 0x00001E86
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003C8E File Offset: 0x00001E8E
		public long ConfidenceMinimum { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003C97 File Offset: 0x00001E97
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003C9F File Offset: 0x00001E9F
		public long CountMaximum { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003CA8 File Offset: 0x00001EA8
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003CB0 File Offset: 0x00001EB0
		public long CountMinimum { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003CB9 File Offset: 0x00001EB9
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003CC1 File Offset: 0x00001EC1
		private string RuleId { get; set; }

		// Token: 0x0600008F RID: 143 RVA: 0x00003CCA File Offset: 0x00001ECA
		public ClassificationDecoding(string encoding, FASTClassificationStore store) : this(encoding.Split(ClassificationDecoding.Delimiters), store)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003CE0 File Offset: 0x00001EE0
		public ClassificationDecoding(string[] tokens, FASTClassificationStore store)
		{
			if (tokens.Length > 3)
			{
				throw new ArgumentException("Illegal number of tokens.  There must be only one, two, or three tokens.  Number of tokens: " + tokens.Length);
			}
			string text = tokens[0].Trim();
			this.RuleId = store.RuleNameToRuleId(text);
			if (this.RuleId == null)
			{
				throw new ArgumentException("Illegal token at position 0 (must be a rule identifier), token: " + text);
			}
			this.Initialize(tokens, store);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003D48 File Offset: 0x00001F48
		public ClassificationDecoding(string ruleId, string[] tokens, FASTClassificationStore store)
		{
			if (tokens.Length > 3)
			{
				throw new ArgumentException("Illegal number of tokens.  There must be only one, two, or three tokens.  Number of tokens: " + tokens.Length);
			}
			this.RuleId = ruleId;
			this.Initialize(tokens, store);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003D80 File Offset: 0x00001F80
		private void Initialize(string[] tokens, FASTClassificationStore store)
		{
			long? num = store.RuleIdToPrefixCode(this.RuleId);
			if (num == null)
			{
				throw new ArgumentException("Illegal token at position 1 (rule identifier exists but doesn't have a code), token: " + tokens[0]);
			}
			if (tokens.Length == 1)
			{
				this.isDefaultCountRange = true;
				this.isDefaultConfidenceRange = true;
				this.CountMinimum = ClassificationDecoding.TranslateToInternalValue(num.Value, 1U);
				this.CountMaximum = ClassificationDecoding.TranslateToInternalValue(num.Value, uint.MaxValue);
				return;
			}
			uint num2;
			uint num3;
			ClassificationDecoding.TranslateRangeToken(tokens[1], uint.MaxValue, out num2, out num3);
			if (num2 == 0U || num3 == 0U)
			{
				throw new ArgumentException("Classification does not allow zero in the count range.  Range: " + tokens[1]);
			}
			this.isDefaultCountRange = (num2 == 1U && num3 == uint.MaxValue);
			this.CountMinimum = ClassificationDecoding.TranslateToInternalValue(num.Value, num2);
			this.CountMaximum = ClassificationDecoding.TranslateToInternalValue(num.Value, num3);
			if (tokens.Length != 3)
			{
				this.isDefaultConfidenceRange = true;
				return;
			}
			ClassificationDecoding.TranslateRangeToken(tokens[2], 100U, out num2, out num3);
			if (num2 == 0U || num3 == 0U || num2 > 100U || num3 > 100U)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Classification only allows a confidence range between 1 and ",
					100U,
					" inclusive.  Range: ",
					tokens[2]
				}));
			}
			this.isDefaultConfidenceRange = (num2 == 1U && num3 == 100U);
			this.ConfidenceMinimum = ClassificationDecoding.TranslateToInternalValue(num.Value, num2);
			this.ConfidenceMaximum = ClassificationDecoding.TranslateToInternalValue(num.Value, num3);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003EE8 File Offset: 0x000020E8
		public TreeNode CreateSubtree()
		{
			ScopeNode scopeNode = null;
			RangeNode<long> rangeNode = new RangeNode<long>();
			rangeNode.StartInclusive = new bool?(true);
			rangeNode.Start = this.CountMinimum;
			rangeNode.EndInclusive = new bool?(true);
			rangeNode.End = this.CountMaximum;
			ScopeNode scopeNode2 = new ScopeNode("ClassificationCount");
			scopeNode2.FirstChild = rangeNode;
			if (!this.isDefaultConfidenceRange)
			{
				RangeNode<long> rangeNode2 = new RangeNode<long>();
				rangeNode2.StartInclusive = new bool?(true);
				rangeNode2.Start = this.ConfidenceMinimum;
				rangeNode2.EndInclusive = new bool?(true);
				rangeNode2.End = this.ConfidenceMaximum;
				scopeNode = new ScopeNode("ClassificationConfidence");
				scopeNode.FirstChild = rangeNode2;
			}
			TreeNode treeNode;
			if (this.isDefaultConfidenceRange)
			{
				treeNode = scopeNode2;
			}
			else if (this.isDefaultCountRange)
			{
				treeNode = scopeNode;
			}
			else
			{
				AndNode andNode = new AndNode();
				andNode.AddNode(scopeNode2);
				andNode.AddNode(scopeNode);
				treeNode = andNode;
			}
			if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
			{
				ULS.SendTraceTag(6121180U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "ClassificationDecoding.CreateSubtree :: subtree={0}; Result of transforming the query.", new object[]
				{
					treeNode
				});
			}
			return treeNode;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004000 File Offset: 0x00002200
		private static void TranslateRangeToken(string rangeToken, uint maximum, out uint firstNumber, out uint secondNumber)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = -1;
			int i = 0;
			bool flag = false;
			char[] array = rangeToken.ToCharArray();
			while (i < array.Length && char.IsWhiteSpace(array[i]))
			{
				i++;
			}
			if (i >= array.Length)
			{
				firstNumber = 1U;
				secondNumber = maximum;
				return;
			}
			if (array[i] == '*')
			{
				for (i++; i < array.Length; i++)
				{
					if (!char.IsWhiteSpace(array[i]))
					{
						throw new ArgumentException("The wildcard character must be alone (except for whitespace).  Token: " + rangeToken);
					}
				}
				firstNumber = 1U;
				secondNumber = maximum;
				return;
			}
			if (i < array.Length && char.IsDigit(array[i]))
			{
				num = i;
				do
				{
					i++;
				}
				while (i < array.Length && char.IsDigit(array[i]));
				num2 = i - 1;
			}
			while (i < array.Length && char.IsWhiteSpace(array[i]))
			{
				i++;
			}
			if (i < array.Length && object.Equals('.', array[i]))
			{
				flag = true;
				i++;
				if (i >= array.Length || !object.Equals('.', array[i]))
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Expected the end of '..' at index ",
						i,
						" of ",
						rangeToken
					}));
				}
				i++;
			}
			while (i < array.Length && char.IsWhiteSpace(array[i]))
			{
				i++;
			}
			if (i < array.Length && char.IsDigit(array[i]))
			{
				if (!flag)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Expected '..' between the two numbers at index ",
						i,
						" of ",
						rangeToken
					}));
				}
				num3 = i;
				while (i < array.Length && char.IsDigit(array[i]))
				{
					i++;
				}
				num4 = i - 1;
			}
			while (i < array.Length && char.IsWhiteSpace(array[i]))
			{
				i++;
			}
			if (i < array.Length)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Unexpected characters in the token, starting at index ",
					i,
					", range: ",
					rangeToken
				}));
			}
			ULS.SendTraceTag(5884048U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "ClassificationDecoding.ctor :: rangeToken={0}, firstNumberStart={1}, firstNumberEnd={2}, secondNumberStart={3}, secondNumberEnd={4}", new object[]
			{
				rangeToken,
				num,
				num2,
				num3,
				num4
			});
			if (num2 >= 0)
			{
				if (flag)
				{
					if (num4 < 0)
					{
						if (!uint.TryParse(new string(array, num, 1 + num2 - num), out firstNumber))
						{
							throw new ArgumentException("Failed to parse first number as a 32-bit unsigned integer.");
						}
						secondNumber = maximum;
						return;
					}
					else
					{
						if (!uint.TryParse(new string(array, num, 1 + num2 - num), out firstNumber))
						{
							throw new ArgumentException("Failed to parse first number as a 32-bit unsigned integer.  Range: " + rangeToken);
						}
						if (!uint.TryParse(new string(array, num3, 1 + num4 - num3), out secondNumber))
						{
							throw new ArgumentException("Failed to parse second number as a 32-bit unsigned integer.  Range: " + rangeToken);
						}
						if (firstNumber > secondNumber)
						{
							throw new ArgumentException("Illegal range.  The first number is larger than the second.  Range: " + rangeToken);
						}
					}
				}
				else
				{
					if (!uint.TryParse(new string(array, num, 1 + num2 - num), out firstNumber))
					{
						throw new ArgumentException("Failed to parse the number as a 32-bit unsigned integer.");
					}
					secondNumber = firstNumber;
				}
				return;
			}
			if (num4 < 0)
			{
				throw new ArgumentException("A range must have at least one number.  Range: " + rangeToken);
			}
			if (!uint.TryParse(new string(array, num3, 1 + num4 - num3), out secondNumber))
			{
				throw new ArgumentException("Failed to parse the number as a 32-bit unsigned integer.");
			}
			firstNumber = 1U;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004396 File Offset: 0x00002596
		private static long TranslateToInternalValue(long prefixCode, uint value)
		{
			return prefixCode | (long)((ulong)value);
		}

		// Token: 0x04000057 RID: 87
		private const uint MaximumConfidence = 100U;

		// Token: 0x04000058 RID: 88
		internal const char Wildcard = '*';

		// Token: 0x04000059 RID: 89
		internal static readonly char[] Delimiters = new char[]
		{
			'|'
		};

		// Token: 0x0400005A RID: 90
		private bool isDefaultCountRange;

		// Token: 0x0400005B RID: 91
		private bool isDefaultConfidenceRange;
	}
}
