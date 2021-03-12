using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000084 RID: 132
	internal struct ValuePosition
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x0001DFCE File Offset: 0x0001C1CE
		public ValuePosition(int line, int offset)
		{
			this.Line = line;
			this.Offset = offset;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001DFDE File Offset: 0x0001C1DE
		public static bool operator ==(ValuePosition pos1, ValuePosition pos2)
		{
			return pos1.Line == pos2.Line && pos1.Offset == pos2.Offset;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001E002 File Offset: 0x0001C202
		public static bool operator !=(ValuePosition pos1, ValuePosition pos2)
		{
			return !(pos1 == pos2);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001E010 File Offset: 0x0001C210
		public override bool Equals(object rhs)
		{
			if (rhs is ValuePosition)
			{
				ValuePosition valuePosition = (ValuePosition)rhs;
				return this.Line == valuePosition.Line && this.Offset == valuePosition.Offset;
			}
			return false;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0001E04E File Offset: 0x0001C24E
		public override int GetHashCode()
		{
			return this.Line * 1000 + this.Offset;
		}

		// Token: 0x040003DC RID: 988
		public int Line;

		// Token: 0x040003DD RID: 989
		public int Offset;
	}
}
