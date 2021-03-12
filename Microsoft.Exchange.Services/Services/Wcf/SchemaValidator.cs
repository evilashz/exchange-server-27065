using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C95 RID: 3221
	internal class SchemaValidator
	{
		// Token: 0x0600574C RID: 22348 RVA: 0x00112F7A File Offset: 0x0011117A
		public SchemaValidator(SchemaValidator.ThrowSchemaValidationFaultDelegate faultDelegate)
		{
			this.faultDelegate = faultDelegate;
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x00112F8C File Offset: 0x0011118C
		private static XmlSchemaSet LoadSchemaSet(XmlSchemaSetPreprocessDelegate preprocessDelegate, params string[] schemaNames)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			try
			{
				SchemaValidator.LoadSchemaFromResourceFile(xmlSchemaSet, "SOAP1.1.xsd");
				foreach (string resourceName in schemaNames)
				{
					SchemaValidator.LoadSchemaFromResourceFile(xmlSchemaSet, resourceName);
				}
			}
			catch (Exception exception)
			{
				throw ExceptionHandlerBase.HandleInternalServerError(null, exception);
			}
			if (preprocessDelegate != null)
			{
				preprocessDelegate(xmlSchemaSet);
			}
			try
			{
				xmlSchemaSet.Compile();
			}
			catch (XmlSchemaException ex)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<XmlSchemaException>(0L, "Failed to compile schema set. Error: {0}.", ex);
				throw ExceptionHandlerBase.HandleInternalServerError(null, ex);
			}
			return xmlSchemaSet;
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x00113024 File Offset: 0x00111224
		protected static void LoadSchemaFromResourceFile(XmlSchemaSet schemaSet, string resourceName)
		{
			using (Stream manifestResourceStream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
			{
				XmlSchema schema = SafeXmlSchema.Read(manifestResourceStream, null);
				schemaSet.Add(schema);
			}
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x0011306C File Offset: 0x0011126C
		private static XmlSchema FixupExchangeSchema(XmlSchemaSet xmlSchemaSet, string schemaId, Func<XmlSchema, XmlSchema> fixupFunc)
		{
			foreach (object obj in xmlSchemaSet.Schemas())
			{
				XmlSchema xmlSchema = (XmlSchema)obj;
				if (xmlSchema.Id == schemaId)
				{
					return fixupFunc(xmlSchema);
				}
			}
			return null;
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x001130E5 File Offset: 0x001112E5
		private static void FixupExchange2010SchemaSet(XmlSchemaSet xmlSchemaSet)
		{
			SchemaValidator.FixupExchangeSchema(xmlSchemaSet, "types", (XmlSchema schema) => SchemaValidator.AddServerVersionSupportToSchema(ExchangeVersionType.Exchange2009, schema));
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x00113119 File Offset: 0x00111319
		private static void FixupExchange2013SchemaSet(XmlSchemaSet xmlSchemaSet)
		{
			SchemaValidator.FixupExchangeSchema(xmlSchemaSet, "types", (XmlSchema schema) => SchemaValidator.AddServerVersionSupportToSchema(ExchangeVersionType.Exchange2012, schema));
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x00113144 File Offset: 0x00111344
		private static void FixupLatestExchangeSchemaSet(XmlSchemaSet xmlSchemaSet)
		{
			SchemaValidator.FixupExchange2013SchemaSet(xmlSchemaSet);
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x00113160 File Offset: 0x00111360
		private static XmlSchema AddServerVersionSupportToSchema(ExchangeVersionType version, XmlSchema schema)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = schema.Items.OfType<XmlSchemaSimpleType>().First((XmlSchemaSimpleType type) => type.Name == "ExchangeVersionType");
			XmlSchemaSimpleTypeRestriction xmlSchemaSimpleTypeRestriction = (XmlSchemaSimpleTypeRestriction)xmlSchemaSimpleType.Content;
			XmlSchemaEnumerationFacet xmlSchemaEnumerationFacet = new XmlSchemaEnumerationFacet();
			xmlSchemaEnumerationFacet.Value = version.ToString();
			xmlSchemaSimpleTypeRestriction.Facets.Add(xmlSchemaEnumerationFacet);
			SchemaValidator.RemoveFixedAttributeFromRequestServerVersion(schema);
			return schema;
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x001131F8 File Offset: 0x001113F8
		private static XmlSchema RemoveFixedAttributeFromRequestServerVersion(XmlSchema schema)
		{
			XmlSchemaElement xmlSchemaElement = schema.Items.OfType<XmlSchemaElement>().First((XmlSchemaElement elem) => elem.Name == "RequestServerVersion");
			XmlSchemaComplexType xmlSchemaComplexType = (XmlSchemaComplexType)xmlSchemaElement.SchemaType;
			XmlSchemaAttribute xmlSchemaAttribute = xmlSchemaComplexType.Attributes.OfType<XmlSchemaAttribute>().First((XmlSchemaAttribute attr) => attr.Name == "Version");
			xmlSchemaAttribute.FixedValue = null;
			return schema;
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x00113278 File Offset: 0x00111478
		internal static XmlSchemaSet GetSchemaSetForVersion(ExchangeVersion version)
		{
			if (version.Version > ExchangeVersionType.Exchange2013)
			{
				version = ExchangeVersion.Exchange2013;
			}
			LazyMember<XmlSchemaSet> lazyMember;
			if (!SchemaValidator.cachedSchemaSets.Member.TryGetValue(version, out lazyMember))
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<ExchangeVersion>(0L, "[SchemaValidator.GetSchemaSetForVersion] Schema version is not loaded: {0}", version);
				throw FaultExceptionUtilities.CreateFault(new SchemaValidationException(new ArgumentException("Unsupported schema version"), 0, 0, "Unsupported request version"), FaultParty.Receiver);
			}
			return lazyMember.Member;
		}

		// Token: 0x06005756 RID: 22358 RVA: 0x001132E0 File Offset: 0x001114E0
		private XmlReaderSettings GetValidatingReaderSettings(ExchangeVersion version, bool treatWarningsAsErrors, bool checkCharacters)
		{
			XmlSchemaSet schemaSetForVersion = SchemaValidator.GetSchemaSetForVersion(version);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
			{
				CheckCharacters = checkCharacters,
				ValidationType = ValidationType.Schema,
				IgnoreComments = true,
				IgnoreWhitespace = true,
				IgnoreProcessingInstructions = true,
				CloseInput = true
			};
			if (treatWarningsAsErrors)
			{
				xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			}
			xmlReaderSettings.Schemas = schemaSetForVersion;
			xmlReaderSettings.ValidationEventHandler += this.HandleValidationEvent;
			return xmlReaderSettings;
		}

		// Token: 0x06005757 RID: 22359 RVA: 0x00113354 File Offset: 0x00111554
		internal SoapSavvyReader GetValidatingReader(Stream streamToValidate, ExchangeVersion version, bool treatWarningsAsErrors, bool checkCharacters)
		{
			this.soapSection = SoapSavvyReader.SoapSection.Unknown;
			XmlReaderSettings validatingReaderSettings = this.GetValidatingReaderSettings(version, treatWarningsAsErrors, checkCharacters);
			XmlReader innerReader = SafeXmlFactory.CreateSafeXmlReader(streamToValidate, validatingReaderSettings);
			return new SoapSavvyReader(innerReader, new SoapSavvyReader.OnSoapSectionChange(this.HandleSoapSectionChange));
		}

		// Token: 0x06005758 RID: 22360 RVA: 0x0011338D File Offset: 0x0011158D
		private void HandleSoapSectionChange(SoapSavvyReader reader, SoapSavvyReader.SoapSection section)
		{
			this.soapSection = section;
		}

		// Token: 0x06005759 RID: 22361 RVA: 0x00113398 File Offset: 0x00111598
		internal void ValidateMessage(Stream streamToValidate, ExchangeVersion version, bool treatWarningsAsErrors, bool checkCharacters)
		{
			XmlReader validatingReader = this.GetValidatingReader(streamToValidate, version, treatWarningsAsErrors, checkCharacters);
			try
			{
				this.ValidateMessage(validatingReader);
			}
			catch (XmlException ex)
			{
				XmlSchemaException exception = new XmlSchemaException(ex.Message, ex, ex.LineNumber, ex.LinePosition);
				this.faultDelegate(exception, this.soapSection);
			}
			finally
			{
				validatingReader.Close();
			}
		}

		// Token: 0x0600575A RID: 22362 RVA: 0x00113410 File Offset: 0x00111610
		private void ValidateMessage(XmlReader validatingReader)
		{
			while (validatingReader.Read())
			{
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x0011341A File Offset: 0x0011161A
		internal SoapSavvyReader.SoapSection SoapSection
		{
			get
			{
				return this.soapSection;
			}
		}

		// Token: 0x0600575C RID: 22364 RVA: 0x00113424 File Offset: 0x00111624
		private void HandleValidationEvent(object sender, ValidationEventArgs e)
		{
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>((long)this.GetHashCode(), "[SchemaValidator::HandleValidationEvent] Validation event received: {0}", e.Message);
			if (e.Exception != null && (this.soapSection == SoapSavvyReader.SoapSection.Body || e.Severity == XmlSeverityType.Error))
			{
				this.faultDelegate(e.Exception, this.soapSection);
			}
		}

		// Token: 0x04003013 RID: 12307
		private static LazyMember<XmlSchemaSet> e12SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(null, new string[]
		{
			"types-e12.xsd",
			"messages-e12.xsd"
		}));

		// Token: 0x04003014 RID: 12308
		private static LazyMember<XmlSchemaSet> e12SP1SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(null, new string[]
		{
			"types-e12sp1.xsd",
			"messages-e12sp1.xsd"
		}));

		// Token: 0x04003015 RID: 12309
		private static LazyMember<XmlSchemaSet> e14SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(new XmlSchemaSetPreprocessDelegate(SchemaValidator.FixupExchange2010SchemaSet), new string[]
		{
			"types-e14.xsd",
			"messages-e14.xsd"
		}));

		// Token: 0x04003016 RID: 12310
		private static LazyMember<XmlSchemaSet> e14SP1SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(null, new string[]
		{
			"types-e14sp1.xsd",
			"messages-e14sp1.xsd"
		}));

		// Token: 0x04003017 RID: 12311
		private static LazyMember<XmlSchemaSet> e14SP2SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(null, new string[]
		{
			"types-e14sp2.xsd",
			"messages-e14sp2.xsd"
		}));

		// Token: 0x04003018 RID: 12312
		private static LazyMember<XmlSchemaSet> e15SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(new XmlSchemaSetPreprocessDelegate(SchemaValidator.FixupExchange2013SchemaSet), new string[]
		{
			"types-e15.xsd",
			"messages-e15.xsd"
		}));

		// Token: 0x04003019 RID: 12313
		private static LazyMember<XmlSchemaSet> e15SP1SchemaSet = new LazyMember<XmlSchemaSet>(() => SchemaValidator.LoadSchemaSet(new XmlSchemaSetPreprocessDelegate(SchemaValidator.FixupLatestExchangeSchemaSet), new string[]
		{
			"types.xsd",
			"messages.xsd"
		}));

		// Token: 0x0400301A RID: 12314
		private static LazyMember<Dictionary<ExchangeVersion, LazyMember<XmlSchemaSet>>> cachedSchemaSets = new LazyMember<Dictionary<ExchangeVersion, LazyMember<XmlSchemaSet>>>(() => new Dictionary<ExchangeVersion, LazyMember<XmlSchemaSet>>
		{
			{
				ExchangeVersion.Exchange2007,
				SchemaValidator.e12SchemaSet
			},
			{
				ExchangeVersion.Exchange2007SP1,
				SchemaValidator.e12SP1SchemaSet
			},
			{
				ExchangeVersion.Exchange2010,
				SchemaValidator.e14SchemaSet
			},
			{
				ExchangeVersion.Exchange2010SP1,
				SchemaValidator.e14SP1SchemaSet
			},
			{
				ExchangeVersion.Exchange2010SP2,
				SchemaValidator.e14SP2SchemaSet
			},
			{
				ExchangeVersion.Exchange2013,
				SchemaValidator.e15SchemaSet
			}
		});

		// Token: 0x0400301B RID: 12315
		private SchemaValidator.ThrowSchemaValidationFaultDelegate faultDelegate;

		// Token: 0x0400301C RID: 12316
		private SoapSavvyReader.SoapSection soapSection;

		// Token: 0x02000C96 RID: 3222
		// (Invoke) Token: 0x0600576C RID: 22380
		internal delegate void ThrowSchemaValidationFaultDelegate(XmlSchemaException exception, SoapSavvyReader.SoapSection soapSection);
	}
}
