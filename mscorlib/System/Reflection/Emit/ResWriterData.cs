using System;
using System.IO;
using System.Resources;

namespace System.Reflection.Emit
{
	// Token: 0x020005FF RID: 1535
	internal class ResWriterData
	{
		// Token: 0x0600486C RID: 18540 RVA: 0x00105E81 File Offset: 0x00104081
		internal ResWriterData(ResourceWriter resWriter, Stream memoryStream, string strName, string strFileName, string strFullFileName, ResourceAttributes attribute)
		{
			this.m_resWriter = resWriter;
			this.m_memoryStream = memoryStream;
			this.m_strName = strName;
			this.m_strFileName = strFileName;
			this.m_strFullFileName = strFullFileName;
			this.m_nextResWriter = null;
			this.m_attribute = attribute;
		}

		// Token: 0x04001DB1 RID: 7601
		internal ResourceWriter m_resWriter;

		// Token: 0x04001DB2 RID: 7602
		internal string m_strName;

		// Token: 0x04001DB3 RID: 7603
		internal string m_strFileName;

		// Token: 0x04001DB4 RID: 7604
		internal string m_strFullFileName;

		// Token: 0x04001DB5 RID: 7605
		internal Stream m_memoryStream;

		// Token: 0x04001DB6 RID: 7606
		internal ResWriterData m_nextResWriter;

		// Token: 0x04001DB7 RID: 7607
		internal ResourceAttributes m_attribute;
	}
}
