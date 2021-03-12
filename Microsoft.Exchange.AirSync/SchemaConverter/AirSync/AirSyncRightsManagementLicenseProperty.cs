using System;
using System.Collections;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000161 RID: 353
	internal class AirSyncRightsManagementLicenseProperty : AirSyncNestedProperty
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x0005B339 File Offset: 0x00059539
		public AirSyncRightsManagementLicenseProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, new RightsManagementLicenseData(), requiresClientSupport)
		{
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005B34C File Offset: 0x0005954C
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			INestedProperty nestedProperty = srcProperty as INestedProperty;
			if (nestedProperty == null)
			{
				throw new UnexpectedTypeException("INestedProperty", srcProperty);
			}
			INestedData nestedData = nestedProperty.NestedData;
			if (nestedData == null)
			{
				throw new ConversionException("nestedData is NULL");
			}
			if (PropertyState.SetToDefault == srcProperty.State)
			{
				return;
			}
			if (nestedData.SubProperties.Count == 0)
			{
				return;
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			IDictionaryEnumerator enumerator = nestedData.SubProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string qualifiedName = enumerator.Key as string;
				string text = enumerator.Value as string;
				if (text != null)
				{
					XmlNode newChild = base.XmlParentNode.OwnerDocument.CreateTextNode(text);
					XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement(qualifiedName, base.Namespace);
					xmlNode.AppendChild(newChild);
					base.XmlNode.AppendChild(xmlNode);
				}
			}
			base.XmlParentNode.AppendChild(base.XmlNode);
		}
	}
}
