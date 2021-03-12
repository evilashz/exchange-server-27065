using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMatching
{
	// Token: 0x0200004B RID: 75
	internal sealed class RegexTreeNode
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00009326 File Offset: 0x00007526
		public RegexTreeNode(char ch, int stateid) : this(RegexTreeNode.NodeType.Leaf, stateid)
		{
			this.charClass = new RegexCharacterClass(ch);
			if (ch == RegexParser.EOS)
			{
				this.end = true;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000934B File Offset: 0x0000754B
		public RegexTreeNode(RegexCharacterClass.ValueType charClass, int stateid) : this(RegexTreeNode.NodeType.Leaf, stateid)
		{
			this.charClass = new RegexCharacterClass(charClass);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009361 File Offset: 0x00007561
		public RegexTreeNode(RegexTreeNode.NodeType nodeType, int stateid)
		{
			this.nodeType = nodeType;
			this.stateid = stateid;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009377 File Offset: 0x00007577
		public int State
		{
			get
			{
				return this.stateid;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000937F File Offset: 0x0000757F
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00009387 File Offset: 0x00007587
		public RegexTreeNode Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009390 File Offset: 0x00007590
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00009398 File Offset: 0x00007598
		public RegexTreeNode Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000093A1 File Offset: 0x000075A1
		public RegexTreeNode.NodeType Type
		{
			get
			{
				return this.nodeType;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000093A9 File Offset: 0x000075A9
		public RegexCharacterClass CharClass
		{
			get
			{
				return this.charClass;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000093B1 File Offset: 0x000075B1
		public List<RegexTreeNode> FirstPos
		{
			get
			{
				return this.firstPos;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000093B9 File Offset: 0x000075B9
		public List<RegexTreeNode> FollowPos
		{
			get
			{
				return this.followPos;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000212 RID: 530 RVA: 0x000093C1 File Offset: 0x000075C1
		public bool End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000093C9 File Offset: 0x000075C9
		public void ComputeNFL()
		{
			this.ComputeNFL(this);
			this.ComputeFollowPos(this);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000093DC File Offset: 0x000075DC
		private static List<RegexTreeNode> CreateList(RegexTreeNode node)
		{
			return new List<RegexTreeNode>(1)
			{
				node
			};
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000093F8 File Offset: 0x000075F8
		private static List<RegexTreeNode> CopyList(List<RegexTreeNode> list)
		{
			List<RegexTreeNode> result = null;
			if (list != null)
			{
				result = new List<RegexTreeNode>(list);
			}
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009414 File Offset: 0x00007614
		private static List<RegexTreeNode> CombineList(List<RegexTreeNode> list1, List<RegexTreeNode> list2)
		{
			if (list1 != null)
			{
				if (list2 == null)
				{
					return new List<RegexTreeNode>(list1);
				}
				List<RegexTreeNode> list3 = new List<RegexTreeNode>(list1.Count + list2.Count);
				list3.AddRange(list1);
				list3.AddRange(list2);
				return list3;
			}
			else
			{
				if (list2 != null)
				{
					return new List<RegexTreeNode>(list2);
				}
				return null;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000945C File Offset: 0x0000765C
		private void ComputeNFL(RegexTreeNode node)
		{
			if (node != null)
			{
				this.ComputeNFL(node.Left);
				this.ComputeNFL(node.Right);
				switch (node.nodeType)
				{
				case RegexTreeNode.NodeType.Leaf:
					node.nullable = false;
					node.firstPos = RegexTreeNode.CreateList(node);
					node.lastPos = RegexTreeNode.CreateList(node);
					return;
				case RegexTreeNode.NodeType.Bar:
					if (node.Left.nullable || node.Right.nullable)
					{
						node.nullable = true;
					}
					node.firstPos = RegexTreeNode.CombineList(node.Left.firstPos, node.Right.firstPos);
					node.lastPos = RegexTreeNode.CombineList(node.Left.lastPos, node.Right.lastPos);
					return;
				case RegexTreeNode.NodeType.Cat:
					if (node.Left.nullable && node.Right.nullable)
					{
						node.nullable = true;
					}
					if (node.Left.nullable)
					{
						node.firstPos = RegexTreeNode.CombineList(node.Left.firstPos, node.Right.firstPos);
					}
					else
					{
						node.firstPos = RegexTreeNode.CopyList(node.Left.firstPos);
					}
					if (node.Right.nullable)
					{
						node.lastPos = RegexTreeNode.CombineList(node.Left.lastPos, node.Right.lastPos);
						return;
					}
					node.lastPos = RegexTreeNode.CopyList(node.Right.lastPos);
					return;
				case RegexTreeNode.NodeType.Star:
					node.nullable = true;
					if (node.Left != null)
					{
						node.firstPos = RegexTreeNode.CopyList(node.Left.firstPos);
						node.lastPos = RegexTreeNode.CopyList(node.Left.firstPos);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009614 File Offset: 0x00007814
		private void ComputeFollowPos(RegexTreeNode node)
		{
			if (node != null)
			{
				this.ComputeFollowPos(node.Left);
				if (node.nodeType == RegexTreeNode.NodeType.Cat)
				{
					if (node.Left.lastPos == null)
					{
						goto IL_D2;
					}
					using (List<RegexTreeNode>.Enumerator enumerator = node.Left.lastPos.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							RegexTreeNode regexTreeNode = enumerator.Current;
							regexTreeNode.followPos = RegexTreeNode.CombineList(regexTreeNode.followPos, node.Right.firstPos);
						}
						goto IL_D2;
					}
				}
				if (node.nodeType == RegexTreeNode.NodeType.Star && node.lastPos != null)
				{
					foreach (RegexTreeNode regexTreeNode2 in node.lastPos)
					{
						regexTreeNode2.followPos = RegexTreeNode.CombineList(regexTreeNode2.followPos, node.firstPos);
					}
				}
				IL_D2:
				this.ComputeFollowPos(node.Right);
			}
		}

		// Token: 0x040000E6 RID: 230
		private int stateid;

		// Token: 0x040000E7 RID: 231
		private bool end;

		// Token: 0x040000E8 RID: 232
		private List<RegexTreeNode> firstPos;

		// Token: 0x040000E9 RID: 233
		private List<RegexTreeNode> lastPos;

		// Token: 0x040000EA RID: 234
		private List<RegexTreeNode> followPos;

		// Token: 0x040000EB RID: 235
		private RegexTreeNode.NodeType nodeType;

		// Token: 0x040000EC RID: 236
		private RegexCharacterClass charClass;

		// Token: 0x040000ED RID: 237
		private RegexTreeNode left;

		// Token: 0x040000EE RID: 238
		private RegexTreeNode right;

		// Token: 0x040000EF RID: 239
		private bool nullable;

		// Token: 0x0200004C RID: 76
		internal enum NodeType
		{
			// Token: 0x040000F1 RID: 241
			Leaf,
			// Token: 0x040000F2 RID: 242
			Bar,
			// Token: 0x040000F3 RID: 243
			Cat,
			// Token: 0x040000F4 RID: 244
			Star,
			// Token: 0x040000F5 RID: 245
			LeftParen,
			// Token: 0x040000F6 RID: 246
			RightParen
		}
	}
}
