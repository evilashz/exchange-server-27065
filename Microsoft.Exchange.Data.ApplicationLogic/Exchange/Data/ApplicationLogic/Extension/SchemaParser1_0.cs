using System;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.ApplicationLogic.Extension
{
	// Token: 0x02000132 RID: 306
	internal class SchemaParser1_0 : SchemaParser
	{
		// Token: 0x06000C83 RID: 3203 RVA: 0x00033CBD File Offset: 0x00031EBD
		public SchemaParser1_0(SafeXmlDocument xmlDoc, ExtensionInstallScope extensionInstallScope) : base(xmlDoc, extensionInstallScope)
		{
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00033CC7 File Offset: 0x00031EC7
		public override Version SchemaVersion
		{
			get
			{
				return SchemaConstants.SchemaVersion1_0;
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00033CCE File Offset: 0x00031ECE
		public override Version GetMinApiVersion()
		{
			return SchemaConstants.Exchange2013RtmApiVersion;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00033CD5 File Offset: 0x00031ED5
		protected override string GetOweNamespacePrefix()
		{
			return "owe1_0";
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00033CDC File Offset: 0x00031EDC
		protected override string GetOweNamespaceUri()
		{
			return "http://schemas.microsoft.com/office/appforoffice/1.0";
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x00033CE3 File Offset: 0x00031EE3
		protected override XmlNode GetFormSettingsParentNode(FormSettings.FormSettingsType formSettingsType)
		{
			if (formSettingsType == FormSettings.FormSettingsType.ItemRead)
			{
				return this.xmlDoc.ChildNodes[0];
			}
			return null;
		}
	}
}
