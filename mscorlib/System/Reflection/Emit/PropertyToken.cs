using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000631 RID: 1585
	[ComVisible(true)]
	[Serializable]
	public struct PropertyToken
	{
		// Token: 0x06004BD3 RID: 19411 RVA: 0x001128EC File Offset: 0x00110AEC
		internal PropertyToken(int str)
		{
			this.m_property = str;
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x001128F5 File Offset: 0x00110AF5
		public int Token
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x001128FD File Offset: 0x00110AFD
		public override int GetHashCode()
		{
			return this.m_property;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00112905 File Offset: 0x00110B05
		public override bool Equals(object obj)
		{
			return obj is PropertyToken && this.Equals((PropertyToken)obj);
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x0011291D File Offset: 0x00110B1D
		public bool Equals(PropertyToken obj)
		{
			return obj.m_property == this.m_property;
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x0011292D File Offset: 0x00110B2D
		public static bool operator ==(PropertyToken a, PropertyToken b)
		{
			return a.Equals(b);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x00112937 File Offset: 0x00110B37
		public static bool operator !=(PropertyToken a, PropertyToken b)
		{
			return !(a == b);
		}

		// Token: 0x040020E0 RID: 8416
		public static readonly PropertyToken Empty;

		// Token: 0x040020E1 RID: 8417
		internal int m_property;
	}
}
