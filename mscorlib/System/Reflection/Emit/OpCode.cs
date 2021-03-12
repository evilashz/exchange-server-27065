using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000629 RID: 1577
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct OpCode
	{
		// Token: 0x06004B86 RID: 19334 RVA: 0x0011210C File Offset: 0x0011030C
		internal OpCode(OpCodeValues value, int flags)
		{
			this.m_stringname = null;
			this.m_pop = (StackBehaviour)(flags >> 12 & 31);
			this.m_push = (StackBehaviour)(flags >> 17 & 31);
			this.m_operand = (OperandType)(flags & 31);
			this.m_type = (OpCodeType)(flags >> 9 & 7);
			this.m_size = (flags >> 22 & 3);
			this.m_s1 = (byte)(value >> 8);
			this.m_s2 = (byte)value;
			this.m_ctrl = (FlowControl)(flags >> 5 & 15);
			this.m_endsUncondJmpBlk = ((flags & 16777216) != 0);
			this.m_stackChange = flags >> 28;
		}

		// Token: 0x06004B87 RID: 19335 RVA: 0x00112194 File Offset: 0x00110394
		internal bool EndsUncondJmpBlk()
		{
			return this.m_endsUncondJmpBlk;
		}

		// Token: 0x06004B88 RID: 19336 RVA: 0x0011219C File Offset: 0x0011039C
		internal int StackChange()
		{
			return this.m_stackChange;
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004B89 RID: 19337 RVA: 0x001121A4 File Offset: 0x001103A4
		[__DynamicallyInvokable]
		public OperandType OperandType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_operand;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x001121AC File Offset: 0x001103AC
		[__DynamicallyInvokable]
		public FlowControl FlowControl
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_ctrl;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004B8B RID: 19339 RVA: 0x001121B4 File Offset: 0x001103B4
		[__DynamicallyInvokable]
		public OpCodeType OpCodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004B8C RID: 19340 RVA: 0x001121BC File Offset: 0x001103BC
		[__DynamicallyInvokable]
		public StackBehaviour StackBehaviourPop
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_pop;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x001121C4 File Offset: 0x001103C4
		[__DynamicallyInvokable]
		public StackBehaviour StackBehaviourPush
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_push;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004B8E RID: 19342 RVA: 0x001121CC File Offset: 0x001103CC
		[__DynamicallyInvokable]
		public int Size
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004B8F RID: 19343 RVA: 0x001121D4 File Offset: 0x001103D4
		[__DynamicallyInvokable]
		public short Value
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_size == 2)
				{
					return (short)((int)this.m_s1 << 8 | (int)this.m_s2);
				}
				return (short)this.m_s2;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x001121F8 File Offset: 0x001103F8
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.Size == 0)
				{
					return null;
				}
				string[] array = OpCode.g_nameCache;
				if (array == null)
				{
					array = new string[287];
					OpCode.g_nameCache = array;
				}
				OpCodeValues opCodeValues = (OpCodeValues)((ushort)this.Value);
				int num = (int)opCodeValues;
				if (num > 255)
				{
					if (num < 65024 || num > 65054)
					{
						return null;
					}
					num = 256 + (num - 65024);
				}
				string text = Volatile.Read<string>(ref array[num]);
				if (text != null)
				{
					return text;
				}
				text = Enum.GetName(typeof(OpCodeValues), opCodeValues).ToLowerInvariant().Replace("_", ".");
				Volatile.Write<string>(ref array[num], text);
				return text;
			}
		}

		// Token: 0x06004B91 RID: 19345 RVA: 0x001122AB File Offset: 0x001104AB
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is OpCode && this.Equals((OpCode)obj);
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x001122C3 File Offset: 0x001104C3
		[__DynamicallyInvokable]
		public bool Equals(OpCode obj)
		{
			return obj.Value == this.Value;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x001122D4 File Offset: 0x001104D4
		[__DynamicallyInvokable]
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x001122DE File Offset: 0x001104DE
		[__DynamicallyInvokable]
		public static bool operator !=(OpCode a, OpCode b)
		{
			return !(a == b);
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x001122EA File Offset: 0x001104EA
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x001122F2 File Offset: 0x001104F2
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04002075 RID: 8309
		internal const int OperandTypeMask = 31;

		// Token: 0x04002076 RID: 8310
		internal const int FlowControlShift = 5;

		// Token: 0x04002077 RID: 8311
		internal const int FlowControlMask = 15;

		// Token: 0x04002078 RID: 8312
		internal const int OpCodeTypeShift = 9;

		// Token: 0x04002079 RID: 8313
		internal const int OpCodeTypeMask = 7;

		// Token: 0x0400207A RID: 8314
		internal const int StackBehaviourPopShift = 12;

		// Token: 0x0400207B RID: 8315
		internal const int StackBehaviourPushShift = 17;

		// Token: 0x0400207C RID: 8316
		internal const int StackBehaviourMask = 31;

		// Token: 0x0400207D RID: 8317
		internal const int SizeShift = 22;

		// Token: 0x0400207E RID: 8318
		internal const int SizeMask = 3;

		// Token: 0x0400207F RID: 8319
		internal const int EndsUncondJmpBlkFlag = 16777216;

		// Token: 0x04002080 RID: 8320
		internal const int StackChangeShift = 28;

		// Token: 0x04002081 RID: 8321
		private string m_stringname;

		// Token: 0x04002082 RID: 8322
		private StackBehaviour m_pop;

		// Token: 0x04002083 RID: 8323
		private StackBehaviour m_push;

		// Token: 0x04002084 RID: 8324
		private OperandType m_operand;

		// Token: 0x04002085 RID: 8325
		private OpCodeType m_type;

		// Token: 0x04002086 RID: 8326
		private int m_size;

		// Token: 0x04002087 RID: 8327
		private byte m_s1;

		// Token: 0x04002088 RID: 8328
		private byte m_s2;

		// Token: 0x04002089 RID: 8329
		private FlowControl m_ctrl;

		// Token: 0x0400208A RID: 8330
		private bool m_endsUncondJmpBlk;

		// Token: 0x0400208B RID: 8331
		private int m_stackChange;

		// Token: 0x0400208C RID: 8332
		private static volatile string[] g_nameCache;
	}
}
