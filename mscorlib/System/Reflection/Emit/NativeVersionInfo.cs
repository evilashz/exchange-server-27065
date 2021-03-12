using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000600 RID: 1536
	internal class NativeVersionInfo
	{
		// Token: 0x0600486D RID: 18541 RVA: 0x00105EC0 File Offset: 0x001040C0
		internal NativeVersionInfo()
		{
			this.m_strDescription = null;
			this.m_strCompany = null;
			this.m_strTitle = null;
			this.m_strCopyright = null;
			this.m_strTrademark = null;
			this.m_strProduct = null;
			this.m_strProductVersion = null;
			this.m_strFileVersion = null;
			this.m_lcid = -1;
		}

		// Token: 0x04001DB8 RID: 7608
		internal string m_strDescription;

		// Token: 0x04001DB9 RID: 7609
		internal string m_strCompany;

		// Token: 0x04001DBA RID: 7610
		internal string m_strTitle;

		// Token: 0x04001DBB RID: 7611
		internal string m_strCopyright;

		// Token: 0x04001DBC RID: 7612
		internal string m_strTrademark;

		// Token: 0x04001DBD RID: 7613
		internal string m_strProduct;

		// Token: 0x04001DBE RID: 7614
		internal string m_strProductVersion;

		// Token: 0x04001DBF RID: 7615
		internal string m_strFileVersion;

		// Token: 0x04001DC0 RID: 7616
		internal int m_lcid;
	}
}
