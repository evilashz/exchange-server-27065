using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public abstract class XMLSerializableBase
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0001B9D1 File Offset: 0x00019BD1
		public XMLSerializableBase()
		{
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001B9D9 File Offset: 0x00019BD9
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0001B9E1 File Offset: 0x00019BE1
		[XmlAnyElement]
		public XmlElement[] UnknownElements
		{
			get
			{
				return this.unknownElements;
			}
			set
			{
				this.unknownElements = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001B9EA File Offset: 0x00019BEA
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001B9F2 File Offset: 0x00019BF2
		[XmlAnyAttribute]
		public XmlAttribute[] UnknownAttributes
		{
			get
			{
				return this.unknownAttributes;
			}
			set
			{
				this.unknownAttributes = value;
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001BA1C File Offset: 0x00019C1C
		public static T Deserialize<T>(string serializedXML, bool throwOnError = true) where T : class
		{
			return XMLSerializableBase.DeserializeFromStringInternal<T>(serializedXML, delegate(Exception e)
			{
				if (throwOnError)
				{
					throw new UnableToDeserializeXMLException(e.Message, e);
				}
			});
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001BA68 File Offset: 0x00019C68
		public static T Deserialize<T>(XmlReader xmlReader, bool throwOnError = true) where T : class
		{
			return XMLSerializableBase.DeserializeFromXmlReaderInternal<T>(xmlReader, delegate(Exception e)
			{
				if (throwOnError)
				{
					throw new UnableToDeserializeXMLException(e.Message, e);
				}
			});
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001BAB4 File Offset: 0x00019CB4
		public static T Deserialize<T>(Stream stream, bool throwOnError = true) where T : class
		{
			return XMLSerializableBase.DeserializeFromStreamInternal<T>(stream, delegate(Exception e)
			{
				if (throwOnError)
				{
					throw new UnableToDeserializeXMLException(e.Message, e);
				}
			});
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001BAF8 File Offset: 0x00019CF8
		public static string Serialize(object objectToSerialize, bool indent = false)
		{
			if (objectToSerialize == null)
			{
				return null;
			}
			StringBuilder sbuilder = new StringBuilder();
			XMLSerializableBase.SerializeToNewXmlWriterInternal(objectToSerialize, (XmlWriterSettings xmlws) => XmlWriter.Create(sbuilder, xmlws), indent);
			return sbuilder.ToString();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001BB50 File Offset: 0x00019D50
		public static void SerializeToStream(object objectToSerialize, Stream stream, bool indent = false)
		{
			XMLSerializableBase.SerializeToNewXmlWriterInternal(objectToSerialize, (XmlWriterSettings xmlws) => XmlWriter.Create(stream, xmlws), indent);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001BB7D File Offset: 0x00019D7D
		public string Serialize(bool indent = false)
		{
			return XMLSerializableBase.Serialize(this, indent);
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001BB86 File Offset: 0x00019D86
		public XElement ToDiagnosticInfo(string elementName = null)
		{
			return XElement.Parse(this.Serialize(false));
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001BB94 File Offset: 0x00019D94
		public override string ToString()
		{
			return this.Serialize(false);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		internal static ADPropertyDefinition ConfigurationXmlRawProperty()
		{
			return new ADPropertyDefinition("ConfigurationXMLRaw", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchConfigurationXML", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		internal static ADPropertyDefinition ConfigurationXmlProperty<T>(ADPropertyDefinition configXmlRawProperty) where T : XMLSerializableBase
		{
			return new ADPropertyDefinition("ConfigurationXML", ExchangeObjectVersion.Exchange2003, typeof(T), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				configXmlRawProperty
			}, null, XMLSerializableBase.ConfigurationXMLGetterDelegate<T>(configXmlRawProperty), XMLSerializableBase.ConfigurationXMLSetterDelegate<T>(configXmlRawProperty), null, null);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001BC30 File Offset: 0x00019E30
		internal static ADPropertyDefinition ConfigXmlProperty<T, ValueT>(string propertyName, ExchangeObjectVersion propertyVersion, ADPropertyDefinition configXmlProperty, ValueT defaultValue, Func<T, ValueT> getterDelegate, Action<T, ValueT> setterDelegate, SimpleProviderPropertyDefinition mservPropertyDefinition = null, SimpleProviderPropertyDefinition mbxPropertyDefinition = null) where T : XMLSerializableBase, new()
		{
			return XMLSerializableBase.ConfigXmlProperty<T, ValueT>(propertyName, propertyVersion, configXmlProperty, defaultValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, getterDelegate, setterDelegate, mservPropertyDefinition, mbxPropertyDefinition);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001BC58 File Offset: 0x00019E58
		internal static ADPropertyDefinition ConfigXmlProperty<T, ValueT>(string propertyName, ExchangeObjectVersion propertyVersion, ADPropertyDefinition configXmlProperty, ValueT defaultValue, PropertyDefinitionConstraint[] readConstraints, PropertyDefinitionConstraint[] writeConstraints, Func<T, ValueT> getterDelegate, Action<T, ValueT> setterDelegate, SimpleProviderPropertyDefinition mservPropertyDefinition = null, SimpleProviderPropertyDefinition mbxPropertyDefinition = null) where T : XMLSerializableBase, new()
		{
			ADPropertyDefinitionFlags adpropertyDefinitionFlags = ADPropertyDefinitionFlags.Calculated;
			if (setterDelegate == null)
			{
				adpropertyDefinitionFlags |= ADPropertyDefinitionFlags.ReadOnly;
			}
			return new ADPropertyDefinition(propertyName, propertyVersion, typeof(ValueT), null, adpropertyDefinitionFlags, defaultValue, readConstraints, writeConstraints, configXmlProperty.SupportingProperties.ToArray<ProviderPropertyDefinition>(), null, XMLSerializableBase.XmlElementGetterDelegate<T, ValueT>(getterDelegate, configXmlProperty, defaultValue), (setterDelegate != null) ? XMLSerializableBase.XmlElementSetterDelegate<T, ValueT>(setterDelegate, configXmlProperty) : null, mservPropertyDefinition, mbxPropertyDefinition);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001BCB4 File Offset: 0x00019EB4
		internal static Unlimited<ByteQuantifiedSize> UlongToUnlimitedSize(ulong rawValue)
		{
			if (rawValue == 18446744073709551615UL)
			{
				return Unlimited<ByteQuantifiedSize>.UnlimitedValue;
			}
			return new Unlimited<ByteQuantifiedSize>(new ByteQuantifiedSize(rawValue));
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0001BCCC File Offset: 0x00019ECC
		internal static ulong UnlimitedSizeToUlong(Unlimited<ByteQuantifiedSize> value)
		{
			if (!value.IsUnlimited)
			{
				return value.Value.ToBytes();
			}
			return ulong.MaxValue;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001BD2C File Offset: 0x00019F2C
		internal static T Deserialize<T>(string serializedXML, PropertyDefinition configXmlRawProperty) where T : class
		{
			return XMLSerializableBase.DeserializeFromStringInternal<T>(serializedXML, delegate(Exception e)
			{
				throw new DataValidationException(new PropertyValidationError(DirectoryStrings.CannotCalculateProperty(configXmlRawProperty.Name, e.Message), configXmlRawProperty, serializedXML), e);
			});
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001BD64 File Offset: 0x00019F64
		internal static string GetNullableSerializationValue<T>(T? value) where T : struct
		{
			if (value != null)
			{
				T value2 = value.Value;
				return value2.ToString();
			}
			return null;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001BD94 File Offset: 0x00019F94
		internal static T? GetNullableAttribute<T>(string value, XMLSerializableBase.TryParseDelegate<T> parseFunc) where T : struct
		{
			T value2;
			if (!parseFunc(value, out value2))
			{
				return null;
			}
			if (!string.IsNullOrWhiteSpace(value))
			{
				return new T?(value2);
			}
			return null;
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001BDCE File Offset: 0x00019FCE
		protected virtual void OnDeserialized()
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001BDD0 File Offset: 0x00019FD0
		private static T DeserializeFromStringInternal<T>(string serializedXML, Action<Exception> failureAction) where T : class
		{
			if (string.IsNullOrWhiteSpace(serializedXML))
			{
				return default(T);
			}
			T result;
			using (StringReader stringReader = new StringReader(serializedXML))
			{
				result = XMLSerializableBase.DeserializeFromTextReaderInternal<T>(stringReader, failureAction);
			}
			return result;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001BE1C File Offset: 0x0001A01C
		private static T DeserializeFromStreamInternal<T>(Stream stream, Action<Exception> failureAction) where T : class
		{
			T result;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				result = XMLSerializableBase.DeserializeFromTextReaderInternal<T>(streamReader, failureAction);
			}
			return result;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001BE58 File Offset: 0x0001A058
		private static T DeserializeFromTextReaderInternal<T>(TextReader textReader, Action<Exception> failureAction) where T : class
		{
			XmlReaderSettings settings = new XmlReaderSettings
			{
				CheckCharacters = false
			};
			T result;
			using (XmlReader xmlReader = XmlReader.Create(textReader, settings))
			{
				result = XMLSerializableBase.DeserializeFromXmlReaderInternal<T>(xmlReader, failureAction);
			}
			return result;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001BED0 File Offset: 0x0001A0D0
		private static T DeserializeFromXmlReaderInternal<T>(XmlReader xmlReader, Action<Exception> failureAction) where T : class
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			T result = default(T);
			try
			{
				XMLSerializableBase.PerformSerializationOperation(delegate
				{
					result = (serializer.Deserialize(xmlReader) as T);
				});
			}
			catch (InvalidOperationException obj)
			{
				failureAction(obj);
			}
			catch (FormatException obj2)
			{
				failureAction(obj2);
			}
			XMLSerializableBase xmlserializableBase = result as XMLSerializableBase;
			if (xmlserializableBase != null)
			{
				xmlserializableBase.OnDeserialized();
			}
			return result;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001BF7C File Offset: 0x0001A17C
		private static void SerializeToNewXmlWriterInternal(object objectToSerialize, Func<XmlWriterSettings, XmlWriter> createWriter, bool indent)
		{
			XmlWriterSettings arg = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				Indent = indent,
				CheckCharacters = false
			};
			using (XmlWriter xmlWriter = createWriter(arg))
			{
				XMLSerializableBase.SerializeToXmlWriterInternal(objectToSerialize, xmlWriter);
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001C008 File Offset: 0x0001A208
		private static void SerializeToXmlWriterInternal(object objectToSerialize, XmlWriter writer)
		{
			XmlSerializer serializer = new XmlSerializer(objectToSerialize.GetType());
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add(string.Empty, string.Empty);
			XMLSerializableBase.PerformSerializationOperation(delegate
			{
				serializer.Serialize(writer, objectToSerialize, ns);
				writer.Flush();
			});
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001C070 File Offset: 0x0001A270
		private static void PerformSerializationOperation(Action operation)
		{
			try
			{
				operation();
			}
			catch (InvalidOperationException ex)
			{
				LocalizedException ex2 = ex.InnerException as LocalizedException;
				if (ex2 != null)
				{
					ex2.PreserveExceptionStack();
					throw ex2;
				}
				throw;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		private static GetterDelegate ConfigurationXMLGetterDelegate<T>(ADPropertyDefinition configXmlRawProperty) where T : XMLSerializableBase
		{
			return delegate(IPropertyBag bag)
			{
				string serializedXML = (string)bag[configXmlRawProperty];
				return XMLSerializableBase.Deserialize<T>(serializedXML, configXmlRawProperty);
			};
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001C148 File Offset: 0x0001A348
		private static SetterDelegate ConfigurationXMLSetterDelegate<T>(ADPropertyDefinition configXmlRawProperty) where T : XMLSerializableBase
		{
			return delegate(object value, IPropertyBag bag)
			{
				XMLSerializableBase xmlserializableBase = value as XMLSerializableBase;
				string value2 = null;
				if (xmlserializableBase != null)
				{
					value2 = xmlserializableBase.Serialize(false);
				}
				bag[configXmlRawProperty] = value2;
			};
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001C1BC File Offset: 0x0001A3BC
		private static GetterDelegate XmlElementGetterDelegate<T, ValueT>(Func<T, ValueT> getDelegate, ProviderPropertyDefinition configXmlPropertyDefinition, ValueT defaultValue) where T : XMLSerializableBase
		{
			return delegate(IPropertyBag bag)
			{
				T t = (T)((object)bag[configXmlPropertyDefinition]);
				if (t == null)
				{
					return defaultValue;
				}
				return getDelegate(t);
			};
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001C24C File Offset: 0x0001A44C
		private static SetterDelegate XmlElementSetterDelegate<T, ValueT>(Action<T, ValueT> setDelegate, ProviderPropertyDefinition configXmlPropertyDefinition) where T : XMLSerializableBase, new()
		{
			return delegate(object value, IPropertyBag bag)
			{
				T t = (T)((object)bag[configXmlPropertyDefinition]);
				if (t == null)
				{
					t = Activator.CreateInstance<T>();
				}
				setDelegate(t, (ValueT)((object)value));
				bag[configXmlPropertyDefinition] = t;
			};
		}

		// Token: 0x04000217 RID: 535
		public const string ConfigXMLRawPropertyName = "ConfigurationXMLRaw";

		// Token: 0x04000218 RID: 536
		private const string ConfigXMLPropertyName = "ConfigurationXML";

		// Token: 0x04000219 RID: 537
		[NonSerialized]
		private XmlElement[] unknownElements;

		// Token: 0x0400021A RID: 538
		[NonSerialized]
		private XmlAttribute[] unknownAttributes;

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x060004E2 RID: 1250
		internal delegate bool TryParseDelegate<T>(string value, out T result);
	}
}
