using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200015C RID: 348
	internal sealed class InternetMessageHeadersProperty : ComplexPropertyBase, IToXmlCommand, IToXmlForPropertyBagCommand, IToServiceObjectCommand, IPropertyCommand
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0002F164 File Offset: 0x0002D364
		public InternetMessageHeadersProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002F16D File Offset: 0x0002D36D
		public static InternetMessageHeadersProperty CreateCommand(CommandContext commandContext)
		{
			return new InternetMessageHeadersProperty(commandContext);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002F178 File Offset: 0x0002D378
		private Dictionary<string, List<string>> ParseHeaders(string internetMessageHeaders)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "InternetMessageHeadersProperty.ParseHeaders:  internetMessageHeaders: '{0}'", internetMessageHeaders);
			Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(internetMessageHeaders)))
			{
				using (MimeReader mimeReader = new MimeReader(memoryStream))
				{
					if (!mimeReader.ReadNextPart())
					{
						return dictionary;
					}
					MimeHeaderReader headerReader = mimeReader.HeaderReader;
					while (headerReader.ReadNextHeader())
					{
						string key;
						string text;
						this.GetHeaderInformation(headerReader, out key, out text);
						if (!string.IsNullOrEmpty(text))
						{
							List<string> list = null;
							if (!dictionary.TryGetValue(key, out list))
							{
								list = new List<string>(1);
								dictionary.Add(key, list);
							}
							list.Add(text);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0002F24C File Offset: 0x0002D44C
		private void GetHeaderInformation(MimeHeaderReader headerReader, out string headerName, out string headerValue)
		{
			try
			{
				headerValue = headerReader.Value;
				headerName = headerReader.Name;
			}
			catch (ExchangeDataException arg)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<ExchangeDataException>(0L, "InternetMessageHeadersProperty.GetHeaderInformation:  Exception encountered parsing headers: '{0}'", arg);
				headerValue = null;
				headerName = null;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0002F298 File Offset: 0x0002D498
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			ServiceObject serviceObject = commandSettings.ServiceObject;
			StoreObject storeObject = commandSettings.StoreObject;
			if (!PropertyCommand.StorePropertyExists(storeObject, this.propertyDefinitions[0]))
			{
				return;
			}
			string internetMessageHeaders = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.propertyDefinitions[0]);
			Dictionary<string, List<string>> dictionary = this.ParseHeaders(internetMessageHeaders);
			if (dictionary.Count > 0)
			{
				DictionaryPropertyUri dictionaryPropertyUri = commandSettings.PropertyPath as DictionaryPropertyUri;
				if (dictionaryPropertyUri == null)
				{
					List<InternetHeaderType> list = new List<InternetHeaderType>();
					foreach (KeyValuePair<string, List<string>> keyValuePair in dictionary)
					{
						foreach (string value in keyValuePair.Value)
						{
							InternetHeaderType item = new InternetHeaderType
							{
								HeaderName = keyValuePair.Key,
								Value = value
							};
							list.Add(item);
						}
					}
					serviceObject[ItemSchema.InternetMessageHeaders] = list.ToArray();
					return;
				}
				if (dictionary.ContainsKey(dictionaryPropertyUri.Key))
				{
					InternetHeaderType[] valueOrDefault = serviceObject.GetValueOrDefault<InternetHeaderType[]>(ItemSchema.InternetMessageHeaders);
					List<InternetHeaderType> list2 = (valueOrDefault == null) ? new List<InternetHeaderType>() : valueOrDefault.ToList<InternetHeaderType>();
					foreach (InternetHeaderType internetHeaderType in list2)
					{
						if (internetHeaderType.HeaderName.Equals(dictionaryPropertyUri.Key, StringComparison.Ordinal))
						{
							return;
						}
					}
					foreach (string value2 in dictionary[dictionaryPropertyUri.Key])
					{
						InternetHeaderType item2 = new InternetHeaderType
						{
							HeaderName = dictionaryPropertyUri.Key,
							Value = value2
						};
						list2.Add(item2);
					}
					serviceObject[ItemSchema.InternetMessageHeaders] = list2.ToArray();
				}
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
		public void ToXml()
		{
			ToXmlCommandSettings commandSettings = base.GetCommandSettings<ToXmlCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			XmlElement serviceItem = commandSettings.ServiceItem;
			if (!PropertyCommand.StorePropertyExists(storeObject, this.propertyDefinitions[0]))
			{
				return;
			}
			string internetMessageHeaders = (string)PropertyCommand.GetPropertyValueFromStoreObject(storeObject, this.propertyDefinitions[0]);
			Dictionary<string, List<string>> dictionary = this.ParseHeaders(internetMessageHeaders);
			if (dictionary.Count > 0)
			{
				DictionaryPropertyUri dictionaryPropertyUri = commandSettings.PropertyPath as DictionaryPropertyUri;
				if (dictionaryPropertyUri == null)
				{
					this.ProcessAllHeaders(dictionary, serviceItem);
					return;
				}
				if (this.HeaderExists(serviceItem, dictionaryPropertyUri.Key))
				{
					return;
				}
				this.ProcessSingleHeader(dictionary, dictionaryPropertyUri.Key, serviceItem);
			}
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0002F56C File Offset: 0x0002D76C
		public void ToXmlForPropertyBag()
		{
			ToXmlForPropertyBagCommandSettings commandSettings = base.GetCommandSettings<ToXmlForPropertyBagCommandSettings>();
			DictionaryPropertyUri dictionaryPropertyUri = (DictionaryPropertyUri)commandSettings.PropertyPath;
			if (ExchangeVersion.Current != ExchangeVersion.Exchange2007)
			{
				throw new InvalidPropertyForOperationException(dictionaryPropertyUri);
			}
			IDictionary<PropertyDefinition, object> propertyBag = commandSettings.PropertyBag;
			XmlElement serviceItem = commandSettings.ServiceItem;
			if (this.HeaderExists(serviceItem, dictionaryPropertyUri.Key))
			{
				return;
			}
			PropertyDefinition key = this.propertyDefinitions[1];
			string empty = string.Empty;
			if (PropertyCommand.TryGetValueFromPropertyBag<string>(propertyBag, key, out empty))
			{
				XmlElement xmlElement = serviceItem[this.xmlLocalName, this.xmlNamespaceUri];
				if (xmlElement == null)
				{
					xmlElement = base.CreateXmlElement(serviceItem, this.xmlLocalName);
				}
				this.GenerateSingleHeaderXml(xmlElement, dictionaryPropertyUri.Key, empty);
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0002F618 File Offset: 0x0002D818
		private void ProcessAllHeaders(Dictionary<string, List<string>> parsedHeaders, XmlElement serviceItem)
		{
			XmlElement orCreateServiceProperty = this.GetOrCreateServiceProperty(serviceItem);
			if (orCreateServiceProperty.ChildNodes.Count > 0)
			{
				orCreateServiceProperty.RemoveAll();
			}
			foreach (KeyValuePair<string, List<string>> keyValuePair in parsedHeaders)
			{
				foreach (string headerValue in keyValuePair.Value)
				{
					this.GenerateSingleHeaderXml(orCreateServiceProperty, keyValuePair.Key, headerValue);
				}
			}
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
		private void ProcessSingleHeader(Dictionary<string, List<string>> parsedHeaders, string headerOfInterest, XmlElement serviceItem)
		{
			XmlElement xmlElement = null;
			foreach (KeyValuePair<string, List<string>> keyValuePair in parsedHeaders)
			{
				if (string.Compare(keyValuePair.Key, headerOfInterest, StringComparison.Ordinal) == 0)
				{
					if (xmlElement == null)
					{
						xmlElement = this.GetOrCreateServiceProperty(serviceItem);
					}
					using (List<string>.Enumerator enumerator2 = keyValuePair.Value.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string headerValue = enumerator2.Current;
							this.GenerateSingleHeaderXml(xmlElement, keyValuePair.Key, headerValue);
						}
						break;
					}
				}
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002F778 File Offset: 0x0002D978
		private XmlElement GetOrCreateServiceProperty(XmlElement serviceItem)
		{
			XmlElement xmlElement = serviceItem[this.xmlLocalName, this.xmlNamespaceUri];
			if (xmlElement == null)
			{
				xmlElement = base.CreateXmlElement(serviceItem, this.xmlLocalName);
			}
			return xmlElement;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0002F7AC File Offset: 0x0002D9AC
		private bool HeaderExists(XmlElement serviceItem, string headerName)
		{
			XmlElement xmlElement = serviceItem[this.xmlLocalName, this.xmlNamespaceUri];
			if (xmlElement == null)
			{
				return false;
			}
			foreach (object obj in xmlElement.ChildNodes)
			{
				XmlElement xmlElement2 = (XmlElement)obj;
				XmlAttribute xmlAttribute = (XmlAttribute)xmlElement2.Attributes.GetNamedItem("HeaderName");
				if (string.Compare(xmlAttribute.Value, headerName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0002F84C File Offset: 0x0002DA4C
		private void GenerateSingleHeaderXml(XmlElement serviceProperty, string headerName, string headerValue)
		{
			XmlElement parentElement = base.CreateXmlTextElement(serviceProperty, "InternetMessageHeader", headerValue);
			PropertyCommand.CreateXmlAttribute(parentElement, "HeaderName", headerName);
		}
	}
}
