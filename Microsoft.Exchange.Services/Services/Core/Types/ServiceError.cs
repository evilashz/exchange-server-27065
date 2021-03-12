using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000880 RID: 2176
	internal class ServiceError
	{
		// Token: 0x06003E5F RID: 15967 RVA: 0x000D85D0 File Offset: 0x000D67D0
		private void InitializeServiceError(string messageText, ResponseCodeType messageKey, int descriptiveLinkKey, ExchangeVersion currentExchangeVersion, ExchangeVersion effectiveVersion, bool stopsBatchProcessing)
		{
			if (!currentExchangeVersion.Supports(effectiveVersion))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string, ExchangeVersion, ExchangeVersion>(0L, "ServiceError.InitializeServiceError: Error, '{0}', not supported by request version. Internal server error used.  Error effective version: '{1}' Request version: '{2}'.", messageKey.ToString(), effectiveVersion, currentExchangeVersion);
				this.innerError = new Dictionary<string, string>();
				this.innerError.Add(ServiceError.InnerErrorMessageTextKey, messageText);
				this.innerError.Add(ServiceError.InnerErrorResponseCodeKey, messageKey.ToString());
				this.innerError.Add(ServiceError.InnerErrorDescriptiveLinkKey, descriptiveLinkKey.ToString());
				messageKey = ServiceError.InternalServerError.ResponseCode;
				descriptiveLinkKey = 0;
				messageText = this.ConvertToString(ServiceError.InternalServerError.MessageText);
			}
			this.messageKey = messageKey;
			this.messageText = messageText;
			this.descriptiveLinkKey = descriptiveLinkKey;
			this.stopsBatchProcessing = stopsBatchProcessing;
		}

		// Token: 0x06003E60 RID: 15968 RVA: 0x000D8698 File Offset: 0x000D6898
		private string ConvertToString(LocalizedString localizedString)
		{
			string result = localizedString;
			CallContext callContext = CallContext.Current;
			if (callContext != null)
			{
				result = localizedString.ToString(callContext.ServerCulture);
			}
			return result;
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000D86C4 File Offset: 0x000D68C4
		public ServiceError(string messageText, ResponseCodeType messageKey, int descriptiveLinkKey, ExchangeVersion effectiveVersion)
		{
			this.InitializeServiceError(messageText, messageKey, descriptiveLinkKey, ExchangeVersion.Current, effectiveVersion, false);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000D86DD File Offset: 0x000D68DD
		public ServiceError(string messageText, ResponseCodeType messageKey, int descriptiveLinkKey, ExchangeVersion effectiveVersion, bool stopsBatchProcessing)
		{
			this.InitializeServiceError(messageText, messageKey, descriptiveLinkKey, ExchangeVersion.Current, effectiveVersion, stopsBatchProcessing);
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000D86F7 File Offset: 0x000D68F7
		public ServiceError(Enum messageId, ResponseCodeType responseCode, int descriptiveLinkKey, ExchangeVersion exchangeVersion) : this(CoreResources.GetLocalizedString((CoreResources.IDs)messageId), responseCode, descriptiveLinkKey, exchangeVersion)
		{
		}

		// Token: 0x06003E64 RID: 15972 RVA: 0x000D8713 File Offset: 0x000D6913
		public ServiceError(LocalizedException localizedException, LocalizedString localizedString, ResponseCodeType messageKey, int descriptiveLinkKey, ExchangeVersion currentExchangeVersion, ExchangeVersion effectiveVersion, bool stopBatchProcessing, PropertyPath[] propertyPaths, IDictionary<string, string> constantValues)
		{
			this.InitializeServiceError(this.ConvertToString(localizedString), messageKey, descriptiveLinkKey, currentExchangeVersion, effectiveVersion, stopBatchProcessing);
			this.propertyPaths = propertyPaths;
			this.constantValues = constantValues;
			this.LocalizedException = localizedException;
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000D8748 File Offset: 0x000D6948
		public ServiceError(Enum messageId, ResponseCodeType responseCode, int descriptiveLinkKey, ExchangeVersion exchangeVersion, IDictionary<string, string> constantValues) : this(CoreResources.GetLocalizedString((CoreResources.IDs)messageId), responseCode, descriptiveLinkKey, exchangeVersion)
		{
			this.constantValues = constantValues;
		}

		// Token: 0x06003E66 RID: 15974 RVA: 0x000D876C File Offset: 0x000D696C
		public static ServiceError CreateBatchProcessingStoppedError()
		{
			return new ServiceError(CoreResources.GetLocalizedString(CoreResources.IDs.ErrorBatchProcessingStopped), ResponseCodeType.ErrorBatchProcessingStopped, 0, ExchangeVersion.Exchange2007, true);
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000D878B File Offset: 0x000D698B
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06003E68 RID: 15976 RVA: 0x000D8793 File Offset: 0x000D6993
		public ResponseCodeType MessageKey
		{
			get
			{
				return this.messageKey;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06003E69 RID: 15977 RVA: 0x000D879B File Offset: 0x000D699B
		// (set) Token: 0x06003E6A RID: 15978 RVA: 0x000D87A3 File Offset: 0x000D69A3
		internal LocalizedException LocalizedException { get; private set; }

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06003E6B RID: 15979 RVA: 0x000D87AC File Offset: 0x000D69AC
		// (set) Token: 0x06003E6C RID: 15980 RVA: 0x000D87B4 File Offset: 0x000D69B4
		internal bool IsTransient { get; set; }

		// Token: 0x06003E6D RID: 15981 RVA: 0x000D87BD File Offset: 0x000D69BD
		internal void AddConstantValueProperty(string key, string value)
		{
			if (this.constantValues == null)
			{
				this.constantValues = new Dictionary<string, string>();
			}
			this.constantValues.Add(key, value);
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000D87DF File Offset: 0x000D69DF
		public int DescriptiveLinkId
		{
			get
			{
				return this.descriptiveLinkKey;
			}
		}

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000D87E7 File Offset: 0x000D69E7
		public bool StopsBatchProcessing
		{
			get
			{
				return this.stopsBatchProcessing;
			}
		}

		// Token: 0x06003E70 RID: 15984 RVA: 0x000D87F0 File Offset: 0x000D69F0
		public override bool Equals(object obj)
		{
			ServiceError serviceError = obj as ServiceError;
			return serviceError != null && this.messageKey == serviceError.messageKey && this.messageText.Equals(serviceError.messageText);
		}

		// Token: 0x06003E71 RID: 15985 RVA: 0x000D8828 File Offset: 0x000D6A28
		public override int GetHashCode()
		{
			return this.messageKey.GetHashCode() ^ this.messageText.GetHashCode();
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000D8848 File Offset: 0x000D6A48
		public ServiceError(LocalizedException localizedException, LocalizedString localizedString, ResponseCodeType messageKey, int descriptiveLinkKey, ExchangeVersion effectiveVersion, bool stopBatchProcessing, PropertyPath[] propertyPaths, IDictionary<string, string> constantValues, XmlNodeArray xmlNodeArray) : this(localizedException, localizedString, messageKey, descriptiveLinkKey, ExchangeVersion.Current, effectiveVersion, stopBatchProcessing, propertyPaths, constantValues)
		{
			this.xmlNodeArray = xmlNodeArray;
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000D8875 File Offset: 0x000D6A75
		public ServiceError(LocalizedString localizedString, ResponseCodeType messageKey, int descriptiveLinkKey, ExchangeVersion currentExchangeVersion, ExchangeVersion effectiveVersion, bool stopsBatchProcessing, PropertyPath[] propertyPaths, IDictionary<string, string> constantValues, XmlNodeArray xmlNodeArray)
		{
			this.InitializeServiceError(this.ConvertToString(localizedString), messageKey, descriptiveLinkKey, currentExchangeVersion, effectiveVersion, stopsBatchProcessing);
			this.propertyPaths = propertyPaths;
			this.constantValues = constantValues;
			this.xmlNodeArray = xmlNodeArray;
		}

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x000D88AC File Offset: 0x000D6AAC
		public XmlNodeArray MessageXml
		{
			get
			{
				if ((this.propertyPaths == null || this.propertyPaths.Length == 0) && (this.constantValues == null || this.constantValues.Count == 0) && (this.innerError == null || this.innerError.Count == 0) && this.xmlNodeArray == null)
				{
					return null;
				}
				if (this.xmlNodeArray != null)
				{
					return this.xmlNodeArray;
				}
				XmlNodeArray result = new XmlNodeArray();
				SafeXmlDocument xmlDocument = new SafeXmlDocument();
				XmlElement messageXmlElement = ServiceXml.CreateElement(xmlDocument, ServiceError.MessageXmlElementName, "http://schemas.microsoft.com/exchange/services/2006/types");
				this.WriteConstantValueElements(this.innerError, result, messageXmlElement);
				this.WritePropertyPathElements(result, messageXmlElement);
				this.WriteConstantValueElements(this.constantValues, result, messageXmlElement);
				return result;
			}
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000D8950 File Offset: 0x000D6B50
		private void WritePropertyPathElements(XmlNodeArray xmlNodeArray, XmlElement messageXmlElement)
		{
			if (this.propertyPaths != null)
			{
				foreach (PropertyPath propertyPath in this.propertyPaths)
				{
					if (propertyPath != null)
					{
						try
						{
							XmlElement item = propertyPath.ToXml(messageXmlElement);
							xmlNodeArray.Nodes.Add(item);
						}
						catch (InvalidExtendedPropertyException)
						{
						}
					}
				}
			}
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000D89AC File Offset: 0x000D6BAC
		private void WriteConstantValueElements(IDictionary<string, string> errorDetails, XmlNodeArray xmlNodeArray, XmlElement messageXmlElement)
		{
			if (errorDetails != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in errorDetails)
				{
					XmlElement xmlElement = ServiceXml.CreateTextElement(messageXmlElement, ServiceError.ConstantValueElementName, keyValuePair.Value, "http://schemas.microsoft.com/exchange/services/2006/types");
					ServiceXml.CreateAttribute(xmlElement, ServiceError.ConstantValueAttributeName, keyValuePair.Key);
					xmlNodeArray.Nodes.Add(xmlElement);
				}
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000D8A28 File Offset: 0x000D6C28
		internal string GetAsXmlString()
		{
			XmlDocument xmlDocument;
			if (this.MessageXml != null)
			{
				xmlDocument = this.MessageXml.Nodes[0].OwnerDocument;
			}
			else
			{
				xmlDocument = new SafeXmlDocument();
			}
			XmlElement xmlElement = ServiceXml.CreateElement(xmlDocument, SchemaValidationException.DummyElementName, "http://schemas.microsoft.com/exchange/services/2006/errors");
			ServiceXml.CreateTextElement(xmlElement, ServiceError.ResponseCodeElementName, this.MessageKey.ToString());
			ServiceXml.CreateTextElement(xmlElement, ServiceError.MessageElementName, this.MessageText);
			if (this.MessageXml != null)
			{
				XmlElement xmlElement2 = ServiceXml.CreateElement(xmlElement, ServiceError.MessageXmlElementName, "http://schemas.microsoft.com/exchange/services/2006/types");
				foreach (XmlNode node in this.MessageXml.Nodes)
				{
					xmlElement2.AppendChild(xmlDocument.ImportNode(node, true));
				}
			}
			return xmlElement.InnerXml;
		}

		// Token: 0x040023CA RID: 9162
		public const int DefaultDescriptiveLinkKey = 0;

		// Token: 0x040023CB RID: 9163
		private static readonly string InnerErrorMessageTextKey = "InnerErrorMessageText";

		// Token: 0x040023CC RID: 9164
		private static readonly string InnerErrorResponseCodeKey = "InnerErrorResponseCode";

		// Token: 0x040023CD RID: 9165
		private static readonly string InnerErrorDescriptiveLinkKey = "InnerErrorDescriptiveLinkKey";

		// Token: 0x040023CE RID: 9166
		private static readonly StaticExceptionMapping InternalServerError = new StaticExceptionMapping(typeof(LocalizedException), ResponseCodeType.ErrorInternalServerError, CoreResources.IDs.ErrorInternalServerError);

		// Token: 0x040023CF RID: 9167
		private IDictionary<string, string> innerError;

		// Token: 0x040023D0 RID: 9168
		private PropertyPath[] propertyPaths;

		// Token: 0x040023D1 RID: 9169
		private IDictionary<string, string> constantValues;

		// Token: 0x040023D2 RID: 9170
		private string messageText;

		// Token: 0x040023D3 RID: 9171
		private int descriptiveLinkKey;

		// Token: 0x040023D4 RID: 9172
		private ResponseCodeType messageKey;

		// Token: 0x040023D5 RID: 9173
		private bool stopsBatchProcessing;

		// Token: 0x040023D6 RID: 9174
		private static readonly string ConstantValueElementName = "Value";

		// Token: 0x040023D7 RID: 9175
		private static readonly string ConstantValueAttributeName = "Name";

		// Token: 0x040023D8 RID: 9176
		private static readonly string MessageXmlElementName = "MessageXml";

		// Token: 0x040023D9 RID: 9177
		private static readonly string ResponseCodeElementName = "ResponseCode";

		// Token: 0x040023DA RID: 9178
		private static readonly string MessageElementName = "Message";

		// Token: 0x040023DB RID: 9179
		private XmlNodeArray xmlNodeArray;
	}
}
