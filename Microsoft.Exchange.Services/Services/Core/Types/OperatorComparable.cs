using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200076B RID: 1899
	internal abstract class OperatorComparable : IComparable<OperatorComparable>
	{
		// Token: 0x060038AD RID: 14509 RVA: 0x000C88B9 File Offset: 0x000C6AB9
		public static bool operator <(OperatorComparable obj1, OperatorComparable obj2)
		{
			return OperatorComparable.Compare(obj1, obj2) < 0;
		}

		// Token: 0x060038AE RID: 14510 RVA: 0x000C88C5 File Offset: 0x000C6AC5
		public static bool operator >(OperatorComparable obj1, OperatorComparable obj2)
		{
			return OperatorComparable.Compare(obj1, obj2) > 0;
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000C88D1 File Offset: 0x000C6AD1
		public static bool operator ==(OperatorComparable obj1, OperatorComparable obj2)
		{
			return OperatorComparable.Compare(obj1, obj2) == 0;
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000C88DD File Offset: 0x000C6ADD
		public static bool operator !=(OperatorComparable obj1, OperatorComparable obj2)
		{
			return OperatorComparable.Compare(obj1, obj2) != 0;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000C88EC File Offset: 0x000C6AEC
		public static bool operator <=(OperatorComparable obj1, OperatorComparable obj2)
		{
			return OperatorComparable.Compare(obj1, obj2) <= 0;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000C88FB File Offset: 0x000C6AFB
		public static bool operator >=(OperatorComparable obj1, OperatorComparable obj2)
		{
			return OperatorComparable.Compare(obj1, obj2) >= 0;
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000C890A File Offset: 0x000C6B0A
		public static int Compare(OperatorComparable obj1, OperatorComparable obj2)
		{
			if (object.ReferenceEquals(obj1, obj2))
			{
				return 0;
			}
			if (obj1 == null)
			{
				return -1;
			}
			if (obj2 == null)
			{
				return 1;
			}
			return obj1.CompareTo(obj2);
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000C8928 File Offset: 0x000C6B28
		public override bool Equals(object obj)
		{
			return obj is OperatorComparable && this == (OperatorComparable)obj;
		}

		// Token: 0x060038B5 RID: 14517
		public abstract int CompareTo(OperatorComparable obj);

		// Token: 0x060038B6 RID: 14518
		public abstract override int GetHashCode();
	}
}
