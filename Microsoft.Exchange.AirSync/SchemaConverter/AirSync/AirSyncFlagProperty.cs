using System;
using System.Collections;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	internal class AirSyncFlagProperty : AirSyncNestedProperty
	{
		// Token: 0x06000FEE RID: 4078 RVA: 0x0005AB5C File Offset: 0x00058D5C
		public AirSyncFlagProperty(string xmlNodeNamespace, string airSyncTagName, bool requiresClientSupport) : base(xmlNodeNamespace, airSyncTagName, new FlagData(), requiresClientSupport)
		{
			base.ClientChangeTracked = true;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0005AB74 File Offset: 0x00058D74
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			INestedProperty nestedProperty = srcProperty as INestedProperty;
			if (nestedProperty == null)
			{
				throw new UnexpectedTypeException("INestedProperty", srcProperty);
			}
			if (PropertyState.Modified != srcProperty.State)
			{
				throw new ConversionException("Property only supports conversion from Modified property state");
			}
			INestedData nestedData = nestedProperty.NestedData;
			if (nestedData == null)
			{
				throw new ConversionException("nestedData is NULL");
			}
			base.XmlNode = base.XmlParentNode.OwnerDocument.CreateElement(base.AirSyncTagNames[0], base.Namespace);
			IDictionaryEnumerator enumerator = nestedData.SubProperties.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string text = enumerator.Key as string;
				string text2 = enumerator.Value as string;
				if (text2 != null)
				{
					string namespaceURI = FlagData.IsTaskProperty(text) ? "Tasks:" : base.Namespace;
					XmlNode newChild = base.XmlParentNode.OwnerDocument.CreateTextNode(text2);
					XmlNode xmlNode = base.XmlParentNode.OwnerDocument.CreateElement(text, namespaceURI);
					xmlNode.AppendChild(newChild);
					base.XmlNode.AppendChild(xmlNode);
				}
			}
			base.XmlParentNode.AppendChild(base.XmlNode);
		}
	}
}
