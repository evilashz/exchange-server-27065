using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005E6 RID: 1510
	[ComVisible(true)]
	public class MethodBody
	{
		// Token: 0x060046E5 RID: 18149 RVA: 0x0010163C File Offset: 0x000FF83C
		protected MethodBody()
		{
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060046E6 RID: 18150 RVA: 0x00101644 File Offset: 0x000FF844
		public virtual int LocalSignatureMetadataToken
		{
			get
			{
				return this.m_localSignatureMetadataToken;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060046E7 RID: 18151 RVA: 0x0010164C File Offset: 0x000FF84C
		public virtual IList<LocalVariableInfo> LocalVariables
		{
			get
			{
				return Array.AsReadOnly<LocalVariableInfo>(this.m_localVariables);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060046E8 RID: 18152 RVA: 0x00101659 File Offset: 0x000FF859
		public virtual int MaxStackSize
		{
			get
			{
				return this.m_maxStackSize;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x060046E9 RID: 18153 RVA: 0x00101661 File Offset: 0x000FF861
		public virtual bool InitLocals
		{
			get
			{
				return this.m_initLocals;
			}
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x00101669 File Offset: 0x000FF869
		public virtual byte[] GetILAsByteArray()
		{
			return this.m_IL;
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x060046EB RID: 18155 RVA: 0x00101671 File Offset: 0x000FF871
		public virtual IList<ExceptionHandlingClause> ExceptionHandlingClauses
		{
			get
			{
				return Array.AsReadOnly<ExceptionHandlingClause>(this.m_exceptionHandlingClauses);
			}
		}

		// Token: 0x04001D1B RID: 7451
		private byte[] m_IL;

		// Token: 0x04001D1C RID: 7452
		private ExceptionHandlingClause[] m_exceptionHandlingClauses;

		// Token: 0x04001D1D RID: 7453
		private LocalVariableInfo[] m_localVariables;

		// Token: 0x04001D1E RID: 7454
		internal MethodBase m_methodBase;

		// Token: 0x04001D1F RID: 7455
		private int m_localSignatureMetadataToken;

		// Token: 0x04001D20 RID: 7456
		private int m_maxStackSize;

		// Token: 0x04001D21 RID: 7457
		private bool m_initLocals;
	}
}
