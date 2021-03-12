using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Data.ApplicationLogic.UM
{
	// Token: 0x020001C0 RID: 448
	internal abstract class VoiceMailPreviewSchema
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x00047500 File Offset: 0x00045700
		public static XmlSchemaSet SchemaSet
		{
			get
			{
				if (VoiceMailPreviewSchema.schemaSet == null)
				{
					lock (VoiceMailPreviewSchema.staticLock)
					{
						if (VoiceMailPreviewSchema.schemaSet == null)
						{
							VoiceMailPreviewSchema.Tracer.TraceDebug(0L, "VoiceMailPreviewSchema: Creating schema set...");
							Assembly executingAssembly = Assembly.GetExecutingAssembly();
							XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
							using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("VoiceMailPreviewSchema.xsd"))
							{
								xmlSchemaSet.Add(SafeXmlSchema.Read(manifestResourceStream, null));
								xmlSchemaSet.Compile();
							}
							VoiceMailPreviewSchema.schemaSet = xmlSchemaSet;
							VoiceMailPreviewSchema.Tracer.TraceDebug(0L, "VoiceMailPreviewSchema: Schema set created successfully.");
						}
					}
				}
				return VoiceMailPreviewSchema.schemaSet;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000475F4 File Offset: 0x000457F4
		internal static bool IsValidTranscription(Stream inputStream)
		{
			VoiceMailPreviewSchema.Tracer.TraceDebug(0L, "VoiceMailPreviewSchema:IsValidTranscription() -> Begin...");
			bool isValid = true;
			try
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ValidationType = ValidationType.Schema;
				xmlReaderSettings.IgnoreComments = true;
				xmlReaderSettings.IgnoreWhitespace = true;
				xmlReaderSettings.IgnoreProcessingInstructions = true;
				xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
				xmlReaderSettings.Schemas = VoiceMailPreviewSchema.SchemaSet;
				xmlReaderSettings.ValidationEventHandler += delegate(object sender, ValidationEventArgs e)
				{
					isValid = false;
					VoiceMailPreviewSchema.Tracer.TraceWarning<string, XmlSeverityType, XmlSchemaException>(0L, "VoiceMailPreviewSchema:ValidationEventHandler: {0}, {1}, {2}.", e.Message, e.Severity, e.Exception);
				};
				inputStream.Position = 0L;
				using (XmlReader xmlReader = XmlReader.Create(inputStream, xmlReaderSettings))
				{
					while (isValid && xmlReader.Read())
					{
					}
				}
			}
			catch (XmlException arg)
			{
				isValid = false;
				VoiceMailPreviewSchema.Tracer.TraceWarning<XmlException>(0L, "VoiceMailPreviewSchema:IsValidTranscription() -> Error:{0}", arg);
			}
			catch (XmlSchemaException arg2)
			{
				isValid = false;
				VoiceMailPreviewSchema.Tracer.TraceWarning<XmlSchemaException>(0L, "VoiceMailPreviewSchema:IsValidTranscription() -> Error:{0}", arg2);
			}
			catch (SystemException arg3)
			{
				isValid = false;
				VoiceMailPreviewSchema.Tracer.TraceWarning<SystemException>(0L, "VoiceMailPreviewSchema:IsValidTranscription() -> Error:{0}", arg3);
			}
			VoiceMailPreviewSchema.Tracer.TraceDebug<bool>(0L, "VoiceMailPreviewSchema:IsValidTranscription() -> Completed. IsValid:{0}", isValid);
			return isValid;
		}

		// Token: 0x04000918 RID: 2328
		public const string MsExchangeEvmProductId = "925712";

		// Token: 0x04000919 RID: 2329
		public const string XmlNamespace = "http://schemas.microsoft.com/exchange/um/2010/evm";

		// Token: 0x0400091A RID: 2330
		public const string ResourceName = "VoiceMailPreviewSchema.xsd";

		// Token: 0x0400091B RID: 2331
		private static readonly Trace Tracer = ExTraceGlobals.UMPartnerMessageTracer;

		// Token: 0x0400091C RID: 2332
		private static object staticLock = new object();

		// Token: 0x0400091D RID: 2333
		private static XmlSchemaSet schemaSet;

		// Token: 0x020001C1 RID: 449
		internal abstract class XPath
		{
			// Token: 0x0400091E RID: 2334
			public const string Prefix = "evm";

			// Token: 0x0400091F RID: 2335
			public const string SelectLanguage = "//evm:ASR/@lang";

			// Token: 0x04000920 RID: 2336
			public const string SelectConfidence = "//evm:ASR/@confidence";

			// Token: 0x04000921 RID: 2337
			public const string SelectRecoResult = "//evm:ASR/@recognitionResult";

			// Token: 0x04000922 RID: 2338
			public const string SelectRecoError = "//evm:ASR/@recognitionError";

			// Token: 0x04000923 RID: 2339
			public const string SelectErrorInfo = "//evm:ASR/evm:ErrorInformation";
		}

		// Token: 0x020001C2 RID: 450
		internal abstract class InternalXml
		{
			// Token: 0x0600113F RID: 4415 RVA: 0x00047780 File Offset: 0x00045980
			private static string GetTranscriptionSkippedXml(string transcriptionError)
			{
				return string.Format(CultureInfo.InvariantCulture, "<?xml version=\"1.0\" encoding=\"utf-8\"?><ASR lang=\"en-US\" confidence=\"0.0\" recognitionResult=\"skipped\" recognitionError=\"{0}\" schemaVersion=\"1.0.0.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/exchange/um/2010/evm\"></ASR>", new object[]
				{
					transcriptionError
				});
			}

			// Token: 0x04000924 RID: 2340
			public const string TimeoutStatus = "504 Timeout";

			// Token: 0x04000925 RID: 2341
			private const string TranscriptionSkippedTemplate = "<?xml version=\"1.0\" encoding=\"utf-8\"?><ASR lang=\"en-US\" confidence=\"0.0\" recognitionResult=\"skipped\" recognitionError=\"{0}\" schemaVersion=\"1.0.0.0\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.microsoft.com/exchange/um/2010/evm\"></ASR>";

			// Token: 0x04000926 RID: 2342
			public static readonly string TimeoutTranscription = VoiceMailPreviewSchema.InternalXml.GetTranscriptionSkippedXml("timeout");

			// Token: 0x04000927 RID: 2343
			public static readonly string MessageTooLongTranscription = VoiceMailPreviewSchema.InternalXml.GetTranscriptionSkippedXml("messagetoolong");

			// Token: 0x04000928 RID: 2344
			public static readonly string ProtectedVoiceMailTranscription = VoiceMailPreviewSchema.InternalXml.GetTranscriptionSkippedXml("protectedvoicemail");

			// Token: 0x04000929 RID: 2345
			public static readonly string ErrorReadingSettingsTranscription = VoiceMailPreviewSchema.InternalXml.GetTranscriptionSkippedXml("errorreadingsettings");
		}
	}
}
