using System;

namespace System.Security
{
	// Token: 0x020001BE RID: 446
	[Serializable]
	internal sealed class SecurityDocumentElement : ISecurityElementFactory
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x000606A5 File Offset: 0x0005E8A5
		internal SecurityDocumentElement(SecurityDocument document, int position)
		{
			this.m_document = document;
			this.m_position = position;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000606BB File Offset: 0x0005E8BB
		SecurityElement ISecurityElementFactory.CreateSecurityElement()
		{
			return this.m_document.GetElement(this.m_position, true);
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000606CF File Offset: 0x0005E8CF
		object ISecurityElementFactory.Copy()
		{
			return new SecurityDocumentElement(this.m_document, this.m_position);
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x000606E2 File Offset: 0x0005E8E2
		string ISecurityElementFactory.GetTag()
		{
			return this.m_document.GetTagForElement(this.m_position);
		}

		// Token: 0x06001C02 RID: 7170 RVA: 0x000606F5 File Offset: 0x0005E8F5
		string ISecurityElementFactory.Attribute(string attributeName)
		{
			return this.m_document.GetAttributeForElement(this.m_position, attributeName);
		}

		// Token: 0x040009B6 RID: 2486
		private int m_position;

		// Token: 0x040009B7 RID: 2487
		private SecurityDocument m_document;
	}
}
