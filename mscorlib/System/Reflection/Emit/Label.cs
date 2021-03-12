using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000617 RID: 1559
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x060049E6 RID: 18918 RVA: 0x0010B3E4 File Offset: 0x001095E4
		internal Label(int label)
		{
			this.m_label = label;
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0010B3ED File Offset: 0x001095ED
		internal int GetLabelValue()
		{
			return this.m_label;
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0010B3F5 File Offset: 0x001095F5
		public override int GetHashCode()
		{
			return this.m_label;
		}

		// Token: 0x060049E9 RID: 18921 RVA: 0x0010B3FD File Offset: 0x001095FD
		public override bool Equals(object obj)
		{
			return obj is Label && this.Equals((Label)obj);
		}

		// Token: 0x060049EA RID: 18922 RVA: 0x0010B415 File Offset: 0x00109615
		public bool Equals(Label obj)
		{
			return obj.m_label == this.m_label;
		}

		// Token: 0x060049EB RID: 18923 RVA: 0x0010B425 File Offset: 0x00109625
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		// Token: 0x060049EC RID: 18924 RVA: 0x0010B42F File Offset: 0x0010962F
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x04001E4A RID: 7754
		internal int m_label;
	}
}
