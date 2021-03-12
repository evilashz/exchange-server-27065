using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200061B RID: 1563
	[ComVisible(false)]
	public struct ExceptionHandler : IEquatable<ExceptionHandler>
	{
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x0010CCA7 File Offset: 0x0010AEA7
		public int ExceptionTypeToken
		{
			get
			{
				return this.m_exceptionClass;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06004A54 RID: 19028 RVA: 0x0010CCAF File Offset: 0x0010AEAF
		public int TryOffset
		{
			get
			{
				return this.m_tryStartOffset;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06004A55 RID: 19029 RVA: 0x0010CCB7 File Offset: 0x0010AEB7
		public int TryLength
		{
			get
			{
				return this.m_tryEndOffset - this.m_tryStartOffset;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06004A56 RID: 19030 RVA: 0x0010CCC6 File Offset: 0x0010AEC6
		public int FilterOffset
		{
			get
			{
				return this.m_filterOffset;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06004A57 RID: 19031 RVA: 0x0010CCCE File Offset: 0x0010AECE
		public int HandlerOffset
		{
			get
			{
				return this.m_handlerStartOffset;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06004A58 RID: 19032 RVA: 0x0010CCD6 File Offset: 0x0010AED6
		public int HandlerLength
		{
			get
			{
				return this.m_handlerEndOffset - this.m_handlerStartOffset;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06004A59 RID: 19033 RVA: 0x0010CCE5 File Offset: 0x0010AEE5
		public ExceptionHandlingClauseOptions Kind
		{
			get
			{
				return this.m_kind;
			}
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x0010CCF0 File Offset: 0x0010AEF0
		public ExceptionHandler(int tryOffset, int tryLength, int filterOffset, int handlerOffset, int handlerLength, ExceptionHandlingClauseOptions kind, int exceptionTypeToken)
		{
			if (tryOffset < 0)
			{
				throw new ArgumentOutOfRangeException("tryOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (tryLength < 0)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (filterOffset < 0)
			{
				throw new ArgumentOutOfRangeException("filterOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (handlerOffset < 0)
			{
				throw new ArgumentOutOfRangeException("handlerOffset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (handlerLength < 0)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if ((long)tryOffset + (long)tryLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("tryLength", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					int.MaxValue - tryOffset
				}));
			}
			if ((long)handlerOffset + (long)handlerLength > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("handlerLength", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					0,
					int.MaxValue - handlerOffset
				}));
			}
			if (kind == ExceptionHandlingClauseOptions.Clause && (exceptionTypeToken & 16777215) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeToken", new object[]
				{
					exceptionTypeToken
				}), "exceptionTypeToken");
			}
			if (!ExceptionHandler.IsValidKind(kind))
			{
				throw new ArgumentOutOfRangeException("kind", Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			this.m_tryStartOffset = tryOffset;
			this.m_tryEndOffset = tryOffset + tryLength;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerOffset;
			this.m_handlerEndOffset = handlerOffset + handlerLength;
			this.m_kind = kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x0010CE8A File Offset: 0x0010B08A
		internal ExceptionHandler(int tryStartOffset, int tryEndOffset, int filterOffset, int handlerStartOffset, int handlerEndOffset, int kind, int exceptionTypeToken)
		{
			this.m_tryStartOffset = tryStartOffset;
			this.m_tryEndOffset = tryEndOffset;
			this.m_filterOffset = filterOffset;
			this.m_handlerStartOffset = handlerStartOffset;
			this.m_handlerEndOffset = handlerEndOffset;
			this.m_kind = (ExceptionHandlingClauseOptions)kind;
			this.m_exceptionClass = exceptionTypeToken;
		}

		// Token: 0x06004A5C RID: 19036 RVA: 0x0010CEC1 File Offset: 0x0010B0C1
		private static bool IsValidKind(ExceptionHandlingClauseOptions kind)
		{
			return kind <= ExceptionHandlingClauseOptions.Finally || kind == ExceptionHandlingClauseOptions.Fault;
		}

		// Token: 0x06004A5D RID: 19037 RVA: 0x0010CECE File Offset: 0x0010B0CE
		public override int GetHashCode()
		{
			return this.m_exceptionClass ^ this.m_tryStartOffset ^ this.m_tryEndOffset ^ this.m_filterOffset ^ this.m_handlerStartOffset ^ this.m_handlerEndOffset ^ (int)this.m_kind;
		}

		// Token: 0x06004A5E RID: 19038 RVA: 0x0010CF00 File Offset: 0x0010B100
		public override bool Equals(object obj)
		{
			return obj is ExceptionHandler && this.Equals((ExceptionHandler)obj);
		}

		// Token: 0x06004A5F RID: 19039 RVA: 0x0010CF18 File Offset: 0x0010B118
		public bool Equals(ExceptionHandler other)
		{
			return other.m_exceptionClass == this.m_exceptionClass && other.m_tryStartOffset == this.m_tryStartOffset && other.m_tryEndOffset == this.m_tryEndOffset && other.m_filterOffset == this.m_filterOffset && other.m_handlerStartOffset == this.m_handlerStartOffset && other.m_handlerEndOffset == this.m_handlerEndOffset && other.m_kind == this.m_kind;
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x0010CF89 File Offset: 0x0010B189
		public static bool operator ==(ExceptionHandler left, ExceptionHandler right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004A61 RID: 19041 RVA: 0x0010CF93 File Offset: 0x0010B193
		public static bool operator !=(ExceptionHandler left, ExceptionHandler right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001E77 RID: 7799
		internal readonly int m_exceptionClass;

		// Token: 0x04001E78 RID: 7800
		internal readonly int m_tryStartOffset;

		// Token: 0x04001E79 RID: 7801
		internal readonly int m_tryEndOffset;

		// Token: 0x04001E7A RID: 7802
		internal readonly int m_filterOffset;

		// Token: 0x04001E7B RID: 7803
		internal readonly int m_handlerStartOffset;

		// Token: 0x04001E7C RID: 7804
		internal readonly int m_handlerEndOffset;

		// Token: 0x04001E7D RID: 7805
		internal readonly ExceptionHandlingClauseOptions m_kind;
	}
}
