using System;
using System.Reflection;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x02000996 RID: 2454
	internal class EventItfInfo
	{
		// Token: 0x06006295 RID: 25237 RVA: 0x0014F9DF File Offset: 0x0014DBDF
		public EventItfInfo(string strEventItfName, string strSrcItfName, string strEventProviderName, RuntimeAssembly asmImport, RuntimeAssembly asmSrcItf)
		{
			this.m_strEventItfName = strEventItfName;
			this.m_strSrcItfName = strSrcItfName;
			this.m_strEventProviderName = strEventProviderName;
			this.m_asmImport = asmImport;
			this.m_asmSrcItf = asmSrcItf;
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x0014FA0C File Offset: 0x0014DC0C
		public Type GetEventItfType()
		{
			Type type = this.m_asmImport.GetType(this.m_strEventItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x0014FA44 File Offset: 0x0014DC44
		public Type GetSrcItfType()
		{
			Type type = this.m_asmSrcItf.GetType(this.m_strSrcItfName, true, false);
			if (type != null && !type.IsVisible)
			{
				type = null;
			}
			return type;
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x0014FA79 File Offset: 0x0014DC79
		public string GetEventProviderName()
		{
			return this.m_strEventProviderName;
		}

		// Token: 0x04002C15 RID: 11285
		private string m_strEventItfName;

		// Token: 0x04002C16 RID: 11286
		private string m_strSrcItfName;

		// Token: 0x04002C17 RID: 11287
		private string m_strEventProviderName;

		// Token: 0x04002C18 RID: 11288
		private RuntimeAssembly m_asmImport;

		// Token: 0x04002C19 RID: 11289
		private RuntimeAssembly m_asmSrcItf;
	}
}
