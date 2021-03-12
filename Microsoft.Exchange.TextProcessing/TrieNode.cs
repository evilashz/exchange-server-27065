using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x02000035 RID: 53
	internal struct TrieNode
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000DF2F File Offset: 0x0000C12F
		public TrieNode(string value, int stringIndex = 0)
		{
			if (stringIndex > 32767)
			{
				throw new ArgumentException(Strings.TermExceedsMaximumLength);
			}
			this.keyword = value;
			this.stringIndex = (short)stringIndex;
			this.flags = 0;
			this.childrenMap = null;
			this.idList = null;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000DF6D File Offset: 0x0000C16D
		public TrieNode(char ch)
		{
			this = new TrieNode(string.Empty, 0);
			if (StringHelper.IsWhitespaceCharacter(ch))
			{
				this.keyword = " ";
				this.flags |= TrieNode.WhitespaceSpinnerFlag;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000DFA1 File Offset: 0x0000C1A1
		public short StringIndex
		{
			get
			{
				return this.stringIndex;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
		public List<long> TerminalIDs
		{
			get
			{
				return this.idList;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000DFB1 File Offset: 0x0000C1B1
		public string Keyword
		{
			get
			{
				return this.keyword;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000DFB9 File Offset: 0x0000C1B9
		private List<long> AllocatedIdList
		{
			get
			{
				if (this.idList == null)
				{
					this.idList = new List<long>();
				}
				return this.idList;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		public bool Transition(char ch, int position, RopeList<TrieNode> nodes, ref int nodeId, ref int newPosition)
		{
			if (StringHelper.IsWhitespaceCharacter(ch))
			{
				ch = ' ';
			}
			if (' ' == ch && (this.IsWhitespaceSpinnerNode() || (position > 0 && ' ' == this.keyword[position + (int)this.stringIndex - 1])))
			{
				newPosition = position;
				return true;
			}
			if (this.IsWhitespaceSpinnerNode() || position + (int)this.stringIndex >= this.keyword.Length)
			{
				if (this.childrenMap != null)
				{
					uint num = (uint)StringHelper.FindMask(ch);
					if ((num & (uint)this.flags) == num)
					{
						newPosition = 0;
						this.childrenMap.TryGetValue(ch, out nodeId);
						return nodeId != 0;
					}
				}
			}
			else if (this.keyword[position + (int)this.stringIndex] == ch)
			{
				newPosition = position + 1;
				return true;
			}
			return false;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E090 File Offset: 0x0000C290
		public void AddTerminalId(long id)
		{
			this.flags |= TrieNode.TerminalFlag;
			this.AllocatedIdList.Add(id);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E0B1 File Offset: 0x0000C2B1
		public void AddTerminalIds(List<long> addedIds)
		{
			this.flags |= TrieNode.TerminalFlag;
			this.AllocatedIdList.AddRange(addedIds);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E0D2 File Offset: 0x0000C2D2
		public bool IsTerminal(int position)
		{
			return (this.flags & TrieNode.TerminalFlag) != 0 && this.keyword.Length == position + (int)this.stringIndex;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E0F9 File Offset: 0x0000C2F9
		public int AddChild(char ch, int childIndex)
		{
			if (this.childrenMap == null)
			{
				this.childrenMap = new SortedList<char, int>();
			}
			this.childrenMap[ch] = childIndex;
			this.flags |= StringHelper.FindMask(ch);
			return childIndex;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E130 File Offset: 0x0000C330
		public int GetChild(char ch, RopeList<TrieNode> nodes)
		{
			int result;
			if (this.childrenMap == null || !this.childrenMap.TryGetValue(ch, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000E158 File Offset: 0x0000C358
		public bool IsMultiNode()
		{
			return this.keyword.Length - (int)this.stringIndex > 0 && !this.IsWhitespaceSpinnerNode();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000E17A File Offset: 0x0000C37A
		private bool IsWhitespaceSpinnerNode()
		{
			return (this.flags & TrieNode.WhitespaceSpinnerFlag) == TrieNode.WhitespaceSpinnerFlag;
		}

		// Token: 0x04000129 RID: 297
		public static readonly TrieNode Default = new TrieNode("Default", 0);

		// Token: 0x0400012A RID: 298
		private static readonly ushort WhitespaceSpinnerFlag = 1;

		// Token: 0x0400012B RID: 299
		private static readonly ushort TerminalFlag = 2;

		// Token: 0x0400012C RID: 300
		private readonly string keyword;

		// Token: 0x0400012D RID: 301
		private List<long> idList;

		// Token: 0x0400012E RID: 302
		private SortedList<char, int> childrenMap;

		// Token: 0x0400012F RID: 303
		private short stringIndex;

		// Token: 0x04000130 RID: 304
		private ushort flags;
	}
}
