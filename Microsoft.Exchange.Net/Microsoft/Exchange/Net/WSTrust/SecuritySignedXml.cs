using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Xml;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B6D RID: 2925
	internal sealed class SecuritySignedXml : SignedXml
	{
		// Token: 0x06003E96 RID: 16022 RVA: 0x000A363C File Offset: 0x000A183C
		public SecuritySignedXml(XmlDocument xmlDocument, XmlElement[] elementsToSign) : base(xmlDocument)
		{
			base.SignedInfo.CanonicalizationMethod = "http://www.w3.org/2001/10/xml-exc-c14n#";
			this.elementsToSign = new Dictionary<string, XmlElement>(elementsToSign.Length);
			foreach (XmlElement xmlElement in elementsToSign)
			{
				string attributeValue = WSSecurityUtility.Id.GetAttributeValue(xmlElement);
				this.elementsToSign.Add(attributeValue, xmlElement);
			}
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000A369B File Offset: 0x000A189B
		public override XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return this.elementsToSign[idValue];
		}

		// Token: 0x04003684 RID: 13956
		private Dictionary<string, XmlElement> elementsToSign;
	}
}
