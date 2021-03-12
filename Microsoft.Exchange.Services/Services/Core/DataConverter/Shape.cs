using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000193 RID: 403
	internal abstract class Shape
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0003681C File Offset: 0x00034A1C
		public Shape(XmlElementInformation itemXmlElementInformation, Schema schema, Shape innerShape, IList<PropertyInformation> defaultProperties)
		{
			this.itemXmlElementInformation = itemXmlElementInformation;
			this.schema = schema;
			this.innerShape = innerShape;
			this.defaultProperties = defaultProperties;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00036844 File Offset: 0x00034A44
		public static bool IsGenericMessageOnly(MessageItem messageItem)
		{
			object obj = messageItem.TryGetProperty(StoreObjectSchema.ItemClass);
			PropertyError propertyError = obj as PropertyError;
			string text;
			if (propertyError != null)
			{
				if (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
				{
					using (Stream stream = messageItem.OpenPropertyStream(StoreObjectSchema.ItemClass, PropertyOpenMode.ReadOnly))
					{
						int num = Encoding.Unicode.GetBytes("IPM.Note").Length;
						if (stream.Length >= (long)num)
						{
							byte[] array = new byte[num];
							stream.Read(array, 0, num);
							text = Encoding.Unicode.GetString(array, 0, array.Length);
							return ObjectClass.IsGenericMessage(text) && !ObjectClass.IsMessage(text, false);
						}
						return true;
					}
					return true;
				}
				return true;
			}
			text = (string)obj;
			return string.IsNullOrEmpty(text) || (ObjectClass.IsGenericMessage(text) && !ObjectClass.IsMessage(text, false));
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00036928 File Offset: 0x00034B28
		public bool TryGetPropertyInformation(PropertyPath propertyPath, out PropertyInformation propertyInformation)
		{
			bool flag = Shape.TryGetPropertyInformation(this, propertyPath, out propertyInformation);
			if (!flag)
			{
				DictionaryPropertyUri dictionaryPropertyUri = propertyPath as DictionaryPropertyUri;
				if (dictionaryPropertyUri != null)
				{
					DictionaryPropertyUriBase dictionaryPropertyUriBase = dictionaryPropertyUri.GetDictionaryPropertyUriBase();
					flag = Shape.TryGetPropertyInformation(this, dictionaryPropertyUriBase, out propertyInformation);
				}
				else
				{
					ExtendedPropertyUri extendedPropertyUri = propertyPath as ExtendedPropertyUri;
					if (extendedPropertyUri != null)
					{
						flag = Shape.TryGetPropertyInformation(this, ExtendedPropertyUri.Placeholder, out propertyInformation);
					}
				}
			}
			return flag;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00036978 File Offset: 0x00034B78
		private static bool TryGetPropertyInformation(Shape shape, PropertyPath propertyPath, out PropertyInformation propertyInformation)
		{
			bool flag = false;
			propertyInformation = null;
			Shape shape2 = shape;
			while (!flag && shape2 != null)
			{
				flag = shape2.Schema.TryGetPropertyInformationByPath(propertyPath, out propertyInformation);
				if (!flag)
				{
					shape2 = shape2.innerShape;
				}
			}
			return flag;
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000369AD File Offset: 0x00034BAD
		public IList<PropertyInformation> DefaultProperties
		{
			get
			{
				return this.defaultProperties;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x000369B5 File Offset: 0x00034BB5
		public Schema Schema
		{
			get
			{
				return this.schema;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x000369BD File Offset: 0x00034BBD
		public Shape InnerShape
		{
			get
			{
				return this.innerShape;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000369C5 File Offset: 0x00034BC5
		public XmlElement CreateItemXmlElement(XmlDocument ownerDocument)
		{
			return ServiceXml.CreateElement(ownerDocument, this.itemXmlElementInformation.LocalName, this.itemXmlElementInformation.NamespaceUri);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000369E3 File Offset: 0x00034BE3
		public XmlElement CreateItemXmlElement(XmlElement parentElement)
		{
			return ServiceXml.CreateElement(parentElement, this.itemXmlElementInformation.LocalName, this.itemXmlElementInformation.NamespaceUri);
		}

		// Token: 0x04000820 RID: 2080
		private IList<PropertyInformation> defaultProperties;

		// Token: 0x04000821 RID: 2081
		private Schema schema;

		// Token: 0x04000822 RID: 2082
		private Shape innerShape;

		// Token: 0x04000823 RID: 2083
		private XmlElementInformation itemXmlElementInformation;

		// Token: 0x02000194 RID: 404
		// (Invoke) Token: 0x06000B64 RID: 2916
		public delegate Shape CreateShapeCallback();

		// Token: 0x02000195 RID: 405
		// (Invoke) Token: 0x06000B68 RID: 2920
		public delegate Shape CreateShapeForPropertyBagCallback();

		// Token: 0x02000196 RID: 406
		// (Invoke) Token: 0x06000B6C RID: 2924
		public delegate Shape CreateShapeForStoreObjectCallback(StoreObject storeObject);
	}
}
