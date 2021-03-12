using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000873 RID: 2163
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MasterCategoryListSerializer
	{
		// Token: 0x06005184 RID: 20868 RVA: 0x00153C0C File Offset: 0x00151E0C
		internal MasterCategoryListSerializer(XmlReader xmlReader)
		{
			this.xmlReader = xmlReader;
			this.PopulateNameTable();
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x00153C21 File Offset: 0x00151E21
		internal bool HasFaults
		{
			get
			{
				return this.faultCount > 0;
			}
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x00153CA4 File Offset: 0x00151EA4
		internal static void Serialize(MasterCategoryList mcl, XmlWriter xmlWriter)
		{
			MasterCategoryListSerializer.CatchSerializationException(delegate
			{
				xmlWriter.WriteStartElement("categories", "CategoryList.xsd");
				IEnumerable<PropValue> properties = MasterCategoryListSerializer.GetProperties(new MasterCategoryListSerializer.TryGetPropertyDelegate(mcl.TryGetProperty), MasterCategoryListSchema.Instance.AllProperties);
				MasterCategoryListSerializer.WriteAttributesForProperties(xmlWriter, MasterCategoryListSerializer.PropValuesToAttributeNameValues(properties));
				MasterCategoryListSerializer.WriteCategoryElements(xmlWriter, mcl);
				xmlWriter.WriteEndElement();
			});
		}

		// Token: 0x06005187 RID: 20871 RVA: 0x00153DF0 File Offset: 0x00151FF0
		internal void Deserialize(MasterCategoryList mcl)
		{
			MasterCategoryListSerializer.CatchSerializationException(delegate
			{
				bool flag = false;
				this.xmlReader.Read();
				while (!this.xmlReader.EOF)
				{
					if (this.xmlReader.NodeType == XmlNodeType.Element && this.IsFromCategoriesNamespace())
					{
						this.EnsureLocationIsExpectedForKnownElements(flag);
						string localName;
						if ((localName = this.xmlReader.LocalName) != null)
						{
							if (localName == "categories")
							{
								mcl.SetProperties(this.ReadAttributes(MasterCategoryListSchema.Instance));
								flag = true;
								this.xmlReader.Read();
								continue;
							}
							if (localName == "category")
							{
								this.LoadCategory(mcl);
								continue;
							}
						}
						this.xmlReader.Skip();
					}
					else
					{
						this.xmlReader.Skip();
					}
				}
				if (!flag)
				{
					this.ReportFault();
				}
			});
		}

		// Token: 0x06005188 RID: 20872 RVA: 0x0015418C File Offset: 0x0015238C
		internal void SerializeUsingSource(MasterCategoryList mcl, XmlWriter xmlWriter)
		{
			MasterCategoryListSerializer.CatchSerializationException(delegate
			{
				bool flag = false;
				HashSet<Category> hashSet = new HashSet<Category>(mcl.Count);
				Util.AddRange<Category, Category>(hashSet, mcl);
				this.xmlReader.Read();
				while (!this.xmlReader.EOF)
				{
					if (this.xmlReader.NodeType == XmlNodeType.Element && this.IsFromCategoriesNamespace())
					{
						this.EnsureLocationIsExpectedForKnownElements(flag);
						string localName;
						if ((localName = this.xmlReader.LocalName) != null)
						{
							if (localName == "categories")
							{
								xmlWriter.WriteStartElement(this.xmlReader.Prefix, this.xmlReader.LocalName, this.xmlReader.NamespaceURI);
								this.CopyOrOverrideAttributes(xmlWriter, MasterCategoryListSerializer.GetProperties(new MasterCategoryListSerializer.TryGetPropertyDelegate(mcl.TryGetProperty), MasterCategoryListSchema.Instance.AllProperties));
								flag = true;
								if (this.xmlReader.IsEmptyElement)
								{
									MasterCategoryListSerializer.WriteCategoryElements(xmlWriter, hashSet);
									xmlWriter.WriteEndElement();
								}
								this.xmlReader.Read();
								continue;
							}
							if (localName == "category")
							{
								Category category = this.FindMatchingCategory(mcl);
								if (category != null && hashSet.Contains(category))
								{
									xmlWriter.WriteStartElement(this.xmlReader.Prefix, this.xmlReader.LocalName, this.xmlReader.NamespaceURI);
									this.CopyOrOverrideAttributes(xmlWriter, MasterCategoryListSerializer.GetProperties(new MasterCategoryListSerializer.TryGetPropertyDelegate(category.TryGetProperty), CategorySchema.Instance.AllProperties));
									if (this.xmlReader.IsEmptyElement)
									{
										xmlWriter.WriteEndElement();
										this.xmlReader.Skip();
									}
									else
									{
										this.xmlReader.Read();
										int depth = this.xmlReader.Depth;
										while (this.xmlReader.Depth >= depth)
										{
											xmlWriter.WriteNode(this.xmlReader, false);
										}
									}
									hashSet.Remove(category);
									continue;
								}
								this.xmlReader.Skip();
								continue;
							}
						}
						xmlWriter.WriteNode(this.xmlReader, false);
					}
					else if (this.xmlReader.NodeType == XmlNodeType.EndElement && this.xmlReader.LocalName == "categories")
					{
						MasterCategoryListSerializer.WriteCategoryElements(xmlWriter, hashSet);
						xmlWriter.WriteNode(this.xmlReader, false);
					}
					else
					{
						xmlWriter.WriteNode(this.xmlReader, false);
					}
				}
				if (!flag)
				{
					if (hashSet.Count > 0)
					{
						ExTraceGlobals.StorageTracer.TraceDebug((long)mcl.GetHashCode(), "The source XML didn't contain the root element we expect. Reverting to source-less serialization.");
						throw new CorruptDataException(ServerStrings.ExInvalidMclXml);
					}
					this.ReportFault();
				}
			});
		}

		// Token: 0x06005189 RID: 20873 RVA: 0x001541C8 File Offset: 0x001523C8
		private static void CatchSerializationException(MasterCategoryListSerializer.MethodBody methodBody)
		{
			Exception ex = null;
			try
			{
				methodBody();
			}
			catch (XmlException ex2)
			{
				ex = ex2;
			}
			catch (PropertyValidationException ex3)
			{
				ex = ex3;
			}
			catch (ObjectValidationException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "MasterCategoriesList XML is not well-formed: \"{0}\"", ex.Message);
				throw new CorruptDataException(ServerStrings.ExInvalidMclXml, ex);
			}
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0015423C File Offset: 0x0015243C
		private static string ConvertToXmlString(object value)
		{
			if (value is Guid)
			{
				return ((Guid)value).ToString("B");
			}
			if (value is string)
			{
				return (string)value;
			}
			if (value is ExDateTime)
			{
				ExDateTime exDateTime = (ExDateTime)value;
				if (exDateTime.TimeZone != ExTimeZone.UtcTimeZone)
				{
					throw new NotSupportedException("MasterCategoryListSerializer cannot seriazlie non-UTC datetimes");
				}
				return ConvertUtils.GetXmlFromDateTime((DateTime)exDateTime);
			}
			else
			{
				if (value is int)
				{
					return XmlConvert.ToString((int)value);
				}
				if (value is bool)
				{
					return XmlConvert.ToString(((bool)value) ? 1 : 0);
				}
				throw new NotSupportedException("The type \"{0}\" is not supported for serialization by MasterCategoryListSerializer");
			}
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x00154490 File Offset: 0x00152690
		private static IEnumerable<PropValue> GetProperties(MasterCategoryListSerializer.TryGetPropertyDelegate tryGetProperty, IEnumerable<PropertyDefinition> propertyDefinitions)
		{
			foreach (PropertyDefinition propertyDefinition in propertyDefinitions)
			{
				yield return new PropValue((StorePropertyDefinition)propertyDefinition, tryGetProperty(propertyDefinition));
			}
			yield break;
		}

		// Token: 0x0600518C RID: 20876 RVA: 0x00154678 File Offset: 0x00152878
		private static IEnumerable<KeyValuePair<string, object>> PropValuesToAttributeNameValues(IEnumerable<PropValue> propValues)
		{
			foreach (PropValue propValue in propValues)
			{
				PropValue propValue2 = propValue;
				XmlAttributePropertyDefinition xmlAttrPropDef = propValue2.Property as XmlAttributePropertyDefinition;
				if (xmlAttrPropDef != null)
				{
					string xmlAttributeName = xmlAttrPropDef.XmlAttributeName;
					PropValue propValue3 = propValue;
					yield return new KeyValuePair<string, object>(xmlAttributeName, propValue3.Value);
				}
			}
			yield break;
		}

		// Token: 0x0600518D RID: 20877 RVA: 0x00154698 File Offset: 0x00152898
		private static void WriteCategoryElements(XmlWriter xmlWriter, IEnumerable<Category> categoriesToWrite)
		{
			string prefix = xmlWriter.LookupPrefix("CategoryList.xsd");
			foreach (Category @object in categoriesToWrite)
			{
				xmlWriter.WriteStartElement(prefix, "category", "CategoryList.xsd");
				IEnumerable<PropValue> properties = MasterCategoryListSerializer.GetProperties(new MasterCategoryListSerializer.TryGetPropertyDelegate(@object.TryGetProperty), CategorySchema.Instance.AllProperties);
				MasterCategoryListSerializer.WriteAttributesForProperties(xmlWriter, MasterCategoryListSerializer.PropValuesToAttributeNameValues(properties));
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x0600518E RID: 20878 RVA: 0x00154724 File Offset: 0x00152924
		private static void WriteAttributeForProperty(XmlWriter xmlWriter, string localName, object value)
		{
			if (!PropertyError.IsPropertyNotFound(value))
			{
				xmlWriter.WriteAttributeString(localName, MasterCategoryListSerializer.ConvertToXmlString(value));
			}
		}

		// Token: 0x0600518F RID: 20879 RVA: 0x0015473C File Offset: 0x0015293C
		private static void WriteAttributesForProperties(XmlWriter xmlWriter, IEnumerable<KeyValuePair<string, object>> attrNameToValues)
		{
			foreach (KeyValuePair<string, object> keyValuePair in attrNameToValues)
			{
				MasterCategoryListSerializer.WriteAttributeForProperty(xmlWriter, keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06005190 RID: 20880 RVA: 0x00154794 File Offset: 0x00152994
		private void AddResolvingDuplicates(MasterCategoryList mcl, Category categoryToLoad)
		{
			Category category = mcl.FindMatch(categoryToLoad);
			if (category == null)
			{
				mcl.Add(categoryToLoad);
				return;
			}
			this.ReportFault();
			ExTraceGlobals.StorageTracer.TraceDebug((long)this.GetHashCode(), "Duplicate Category found while deserializing a MasterCategoriesList: Name = \"{0}\" ({1}), Guid = \"{2}\" ({3})", new object[]
			{
				categoryToLoad.Name,
				(categoryToLoad.Name == category.Name) ? "same" : "different",
				categoryToLoad.Guid,
				(categoryToLoad.Guid == category.Guid) ? "same" : "different"
			});
			Category item = Category.Resolve(categoryToLoad, category, null);
			mcl.Remove(category.Name);
			mcl.Add(item);
		}

		// Token: 0x06005191 RID: 20881 RVA: 0x00154850 File Offset: 0x00152A50
		private void CopyOrOverrideAttributes(XmlWriter xmlWriter, IEnumerable<PropValue> propValueOverrides)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Util.AddRange<KeyValuePair<string, object>, KeyValuePair<string, object>>(dictionary, MasterCategoryListSerializer.PropValuesToAttributeNameValues(propValueOverrides));
			while (this.xmlReader.MoveToNextAttribute())
			{
				if (this.IsFromCategoriesNamespace())
				{
					object value;
					if (dictionary.TryGetValue(this.xmlReader.LocalName, out value))
					{
						dictionary.Remove(this.xmlReader.LocalName);
						MasterCategoryListSerializer.WriteAttributeForProperty(xmlWriter, this.xmlReader.LocalName, value);
					}
					else
					{
						xmlWriter.WriteAttributeString(this.xmlReader.LocalName, this.xmlReader.Value);
					}
				}
				else
				{
					xmlWriter.WriteAttributeString(this.xmlReader.Prefix, this.xmlReader.LocalName, this.xmlReader.NamespaceURI, this.xmlReader.Value);
				}
			}
			MasterCategoryListSerializer.WriteAttributesForProperties(xmlWriter, dictionary);
			this.xmlReader.MoveToElement();
		}

		// Token: 0x06005192 RID: 20882 RVA: 0x0015492C File Offset: 0x00152B2C
		private void EnsureLocationIsExpectedForKnownElements(bool readRootMcl)
		{
			if ((!readRootMcl && this.xmlReader.LocalName == "category") || (readRootMcl && this.xmlReader.LocalName == "categories"))
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Unexpected element while reading a MasterCategoriesList: {0}", this.xmlReader.LocalName);
				this.ReportFault();
				this.xmlReader.Skip();
			}
		}

		// Token: 0x06005193 RID: 20883 RVA: 0x001549A0 File Offset: 0x00152BA0
		private Category FindMatchingCategory(MasterCategoryList mcl)
		{
			string text = (string)this.ReadAttributeForProperty(InternalSchema.CategoryName);
			Guid? guid = (Guid?)this.ReadAttributeForProperty(InternalSchema.CategoryGuid);
			if (text == null || guid == null)
			{
				return null;
			}
			return mcl[guid.Value] ?? mcl[text];
		}

		// Token: 0x06005194 RID: 20884 RVA: 0x001549F8 File Offset: 0x00152BF8
		private bool IsFromCategoriesNamespace()
		{
			return this.xmlReader.NamespaceURI == "CategoryList.xsd" || (this.xmlReader.NodeType == XmlNodeType.Attribute && string.IsNullOrEmpty(this.xmlReader.Prefix) && string.IsNullOrEmpty(this.xmlReader.NamespaceURI));
		}

		// Token: 0x06005195 RID: 20885 RVA: 0x00154A50 File Offset: 0x00152C50
		private void LoadCategory(MasterCategoryList mcl)
		{
			Category category = null;
			try
			{
				category = Category.Load(this.ReadAttributes(CategorySchema.Instance));
			}
			catch (CorruptDataException ex)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, "Data for 1 category is malformed and cannot be corrected. Skipping. Error: {0}", ex.Message);
				this.ReportFault();
			}
			if (category != null)
			{
				this.AddResolvingDuplicates(mcl, category);
			}
			this.xmlReader.Skip();
		}

		// Token: 0x06005196 RID: 20886 RVA: 0x00154AB8 File Offset: 0x00152CB8
		private void PopulateNameTable()
		{
			XmlNameTable nameTable = this.xmlReader.NameTable;
			nameTable.Add("CategoryList.xsd");
			nameTable.Add("categories");
			nameTable.Add("category");
		}

		// Token: 0x06005197 RID: 20887 RVA: 0x00154AF8 File Offset: 0x00152CF8
		private List<PropValue> ReadAttributes(Schema schema)
		{
			List<PropValue> list = new List<PropValue>();
			foreach (PropertyDefinition propertyDefinition in schema.AllProperties)
			{
				XmlAttributePropertyDefinition xmlAttributePropertyDefinition = propertyDefinition as XmlAttributePropertyDefinition;
				if (xmlAttributePropertyDefinition != null)
				{
					object obj = this.ReadAttributeForProperty(xmlAttributePropertyDefinition);
					if (obj != null)
					{
						list.Add(new PropValue(xmlAttributePropertyDefinition, obj));
					}
				}
			}
			return list;
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x00154B6C File Offset: 0x00152D6C
		private object ReadAttributeForProperty(XmlAttributePropertyDefinition xmlAttrPropDef)
		{
			if (!this.xmlReader.MoveToAttribute(xmlAttrPropDef.XmlAttributeName) && !this.xmlReader.MoveToAttribute(xmlAttrPropDef.XmlAttributeName, "CategoryList.xsd"))
			{
				return null;
			}
			Exception ex2;
			try
			{
				if (xmlAttrPropDef.Type == typeof(Guid))
				{
					return new Guid(this.xmlReader.Value);
				}
				if (xmlAttrPropDef.Type == typeof(ExDateTime))
				{
					return ConvertUtils.GetDateTimeFromXml(this.xmlReader.Value);
				}
				return this.xmlReader.ReadContentAs(xmlAttrPropDef.Type, null);
			}
			catch (XmlException ex)
			{
				ex2 = ex;
			}
			catch (FormatException ex3)
			{
				ex2 = ex3;
			}
			catch (OverflowException ex4)
			{
				ex2 = ex4;
			}
			catch (InvalidCastException ex5)
			{
				ex2 = ex5;
			}
			finally
			{
				this.xmlReader.MoveToElement();
			}
			ExTraceGlobals.StorageTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Couldn't parse a value for the MasterCategoriesList property {0}: {1}", xmlAttrPropDef.Name, ex2.Message);
			this.ReportFault();
			return null;
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x00154CB0 File Offset: 0x00152EB0
		private void ReportFault()
		{
			if (this.faultCount < 200)
			{
				this.faultCount++;
				return;
			}
			throw new CorruptDataException(ServerStrings.ExInvalidMclXml);
		}

		// Token: 0x04002C63 RID: 11363
		private const string CategoriesElementName = "categories";

		// Token: 0x04002C64 RID: 11364
		private const string CategoryElementName = "category";

		// Token: 0x04002C65 RID: 11365
		private const string CategoriesURI = "CategoryList.xsd";

		// Token: 0x04002C66 RID: 11366
		private const int MaxFaultCount = 200;

		// Token: 0x04002C67 RID: 11367
		private readonly XmlReader xmlReader;

		// Token: 0x04002C68 RID: 11368
		private int faultCount;

		// Token: 0x02000874 RID: 2164
		// (Invoke) Token: 0x0600519B RID: 20891
		private delegate void MethodBody();

		// Token: 0x02000875 RID: 2165
		// (Invoke) Token: 0x0600519F RID: 20895
		private delegate object TryGetPropertyDelegate(PropertyDefinition propertyDefinition);
	}
}
