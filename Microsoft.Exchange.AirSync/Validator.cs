using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200025F RID: 607
	internal class Validator
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x000872E2 File Offset: 0x000854E2
		internal Validator(int version, bool hasExtensions)
		{
			this.versionIdx = Validator.GetVersionIndexFromVersion(version, hasExtensions);
			this.version = version;
			this.validationErrors = new List<Validator.XmlValidationError>();
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x00087309 File Offset: 0x00085509
		internal int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x00087311 File Offset: 0x00085511
		internal List<Validator.XmlValidationError> ValidationErrors
		{
			get
			{
				return this.validationErrors;
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0008731C File Offset: 0x0008551C
		internal virtual bool ValidateXml(XmlElement rootNode, string rootNodeName)
		{
			bool result;
			using (Command.CurrentCommand.Context.Tracker.Start(TimeId.ValidatorValidateXML))
			{
				this.ValidationErrors.Clear();
				if (!GlobalSettings.Validate)
				{
					result = true;
				}
				else if (this.versionIdx < 0 || this.versionIdx >= Validator.airsyncVersion.Length)
				{
					this.ValidationErrors.Add(new Validator.XmlValidationError("Request contained unknown version: {0}.{1}", new object[]
					{
						this.Version / 10,
						this.Version % 10
					}));
					result = false;
				}
				else
				{
					if (rootNode == null)
					{
						throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.First140Error, null, false);
					}
					if (rootNode.LocalName != rootNodeName)
					{
						if (AirSyncDiagnostics.IsTraceEnabled(TraceType.ErrorTrace, ExTraceGlobals.ValidationTracer))
						{
							AirSyncDiagnostics.TraceError<XmlDocument, string, string>(ExTraceGlobals.ValidationTracer, null, "Validaton Failure InvalidRootNode {0} ExpectedNode={1} FoundNode={2}", rootNode.OwnerDocument, rootNodeName, rootNode.LocalName);
						}
						this.ValidationErrors.Add(new Validator.XmlValidationError("Invalid Root Node. Expected: '{0}' Found: '{1}'", new object[]
						{
							rootNodeName,
							rootNode.LocalName
						}));
						result = false;
					}
					else
					{
						result = this.ValidateXmlNode(rootNode, rootNode.NamespaceURI);
					}
				}
			}
			return result;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0008745C File Offset: 0x0008565C
		protected bool ValidateXmlNode(XmlNode xmlNode, string targetNamespace)
		{
			bool result;
			using (Command.CurrentCommand.Context.Tracker.Start(TimeId.ValidatorValidateXMLNode))
			{
				if (!Validator.schemasCreated)
				{
					Validator.CreateSchemaSet();
				}
				xmlNode.OwnerDocument.Schemas = Validator.schemas[this.versionIdx];
				ICollection collection = xmlNode.OwnerDocument.Schemas.Schemas(targetNamespace);
				if (collection == null || collection.Count == 0)
				{
					if (AirSyncDiagnostics.IsTraceEnabled(TraceType.ErrorTrace, ExTraceGlobals.ValidationTracer))
					{
						AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.ValidationTracer, null, "Validaton Failure No Schema specified for {0} TargetNamespace={1}", xmlNode.OwnerDocument.LocalName, targetNamespace);
					}
					this.ValidationErrors.Add(new Validator.XmlValidationError("Unknown Namespace: '{0}'", new object[]
					{
						targetNamespace
					}));
					result = false;
				}
				else
				{
					this.failed = false;
					xmlNode.OwnerDocument.Validate(new ValidationEventHandler(this.AirSyncValidationEventHandler), xmlNode);
					result = !this.failed;
				}
			}
			return result;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00087554 File Offset: 0x00085754
		private static int GetVersionIndexFromVersion(int version, bool hasExtensions)
		{
			if (version <= 25)
			{
				switch (version)
				{
				case 20:
					return 0;
				case 21:
					return 1;
				default:
					if (version == 25)
					{
						return 2;
					}
					break;
				}
			}
			else
			{
				switch (version)
				{
				case 120:
					return 3;
				case 121:
					return 4;
				default:
					switch (version)
					{
					case 140:
						if (!hasExtensions)
						{
							return 5;
						}
						return 6;
					case 141:
						return 7;
					default:
						if (version == 160)
						{
							return 8;
						}
						break;
					}
					break;
				}
			}
			return -1;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x000875C8 File Offset: 0x000857C8
		private static void CreateSchemaSet()
		{
			using (Command.CurrentCommand.Context.Tracker.Start(TimeId.ValidatorCreateSchemaSet))
			{
				if (GlobalSettings.Validate)
				{
					lock (Validator.schemas)
					{
						if (!Validator.schemasCreated)
						{
							DirectoryInfo directoryInfo = null;
							try
							{
								directoryInfo = new DirectoryInfo(GlobalSettings.SchemaDirectory);
							}
							catch (Exception ex)
							{
								Validator.HandleMissingSchemaDirectory(ex);
							}
							if (!directoryInfo.Exists)
							{
								Validator.HandleMissingSchemaDirectory(null);
							}
							for (int i = 0; i < Validator.airsyncVersion.Length; i++)
							{
								Validator.CreateSchemaSet(i, directoryInfo);
							}
							Validator.schemasCreated = true;
						}
					}
				}
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00087698 File Offset: 0x00085898
		private static void CreateSchemaSet(int idx, DirectoryInfo dir)
		{
			using (Command.CurrentCommand.Context.Tracker.Start(TimeId.ValidatorCreateSchemaSetParam))
			{
				DirectoryInfo directoryInfo = null;
				string text = dir.FullName + "\\v" + Validator.airsyncVersion[idx];
				try
				{
					directoryInfo = new DirectoryInfo(text);
				}
				catch (Exception ex)
				{
					Validator.HandleMissingSchemaVersionDirectory(ex, text);
				}
				if (!directoryInfo.Exists)
				{
					Validator.HandleMissingSchemaVersionDirectory(null, text);
				}
				Validator.schemas[idx] = new XmlSchemaSet();
				foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.xsd"))
				{
					try
					{
						Validator.schemas[idx].Add(null, fileInfo.FullName);
					}
					catch (Exception ex2)
					{
						Validator.HandleBadSchemaFile(ex2, text);
					}
				}
				try
				{
					Validator.schemas[idx].Compile();
				}
				catch (Exception ex3)
				{
					Validator.HandleBadSchemaFile(ex3, text);
				}
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000877AC File Offset: 0x000859AC
		private static void HandleMissingSchemaDirectory(Exception ex)
		{
			bool watsonReportEnabled = true;
			if (ex == null || ex is ArgumentNullException || ex is SecurityException || ex is ArgumentException || ex is PathTooLongException)
			{
				watsonReportEnabled = false;
			}
			AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, null, "SchemaDirectory is not specified in the web.config or it is not accessible. Exception '{0}' ", (ex == null) ? string.Empty : ex.ToString());
			throw new AirSyncFatalException(EASServerStrings.SchemaDirectoryNotAccessible, "SchemaDirectoryNotAccessible", watsonReportEnabled, ex);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00087814 File Offset: 0x00085A14
		private static void HandleMissingSchemaVersionDirectory(Exception ex, string directoryPath)
		{
			bool watsonReportEnabled = true;
			if (ex == null || ex is ArgumentNullException || ex is SecurityException || ex is ArgumentException || ex is PathTooLongException)
			{
				watsonReportEnabled = false;
			}
			AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, null, "SchemaDirectory with path '{0}' is missing or  not accessible. Exception '{1}' ", directoryPath, (ex == null) ? string.Empty : ex.ToString());
			throw new AirSyncFatalException(EASServerStrings.SchemaDirectoryVersionNotAccessible(directoryPath), "SchemaDirectoryVersionNotAccessible", watsonReportEnabled, ex);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0008787C File Offset: 0x00085A7C
		private static void HandleBadSchemaFile(Exception ex, string dirPath)
		{
			if (ex is XmlSchemaException)
			{
				XmlSchemaException ex2 = (XmlSchemaException)ex;
				string text = (ex2.SourceUri != null) ? ex2.SourceUri : string.Empty;
				AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, null, "Schema file with path '{0}' is corrupted. Exception '{1}'", text, ex2.ToString());
				throw new AirSyncFatalException(EASServerStrings.SchemaFileCorrupted(text), "SchemaFileCorrupted", false, ex2);
			}
			AirSyncDiagnostics.TraceError<string, string>(ExTraceGlobals.RequestsTracer, null, "Unable to compile the XSD SchemaSet under the folder '{0}'. Exception '{1}' ", dirPath, (ex == null) ? string.Empty : ex.ToString());
			throw new AirSyncFatalException(EASServerStrings.SchemaUnknownCompilationError(dirPath), "SchemaUnknownCompilationError", true, ex);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0008790C File Offset: 0x00085B0C
		private void AirSyncValidationEventHandler(object sender, ValidationEventArgs args)
		{
			this.failed = true;
			this.ValidationErrors.Add(new Validator.XmlValidationError(args));
			if (AirSyncDiagnostics.IsTraceEnabled(TraceType.ErrorTrace, ExTraceGlobals.ValidationTracer))
			{
				StringBuilder stringBuilder = new StringBuilder();
				XmlSchemaValidationException ex = args.Exception as XmlSchemaValidationException;
				if (ex != null && ex.SourceObject != null)
				{
					XmlElement xmlElement = ex.SourceObject as XmlElement;
					if (xmlElement != null)
					{
						stringBuilder.Append("Source=").Append(xmlElement.InnerXml);
					}
					if (args.Exception.LineNumber != 0)
					{
						stringBuilder.Append("Line=").Append(args.Exception.LineNumber);
					}
					if (args.Exception.StackTrace != null)
					{
						stringBuilder.Append("Stack=").Append(args.Exception.StackTrace);
					}
				}
				AirSyncDiagnostics.TraceError<XmlSeverityType, string, StringBuilder>(ExTraceGlobals.ValidationTracer, null, "Validaton Failure Severity={0} Message={1} {2}", args.Severity, args.Message, stringBuilder);
			}
		}

		// Token: 0x04000DE5 RID: 3557
		private const string VersionDirPrefix = "\\v";

		// Token: 0x04000DE6 RID: 3558
		private static readonly string[] airsyncVersion = new string[]
		{
			"2.0",
			"2.1",
			"2.5",
			"12.0",
			"12.1",
			"14.0",
			"14.0-1",
			"14.1",
			"16.0"
		};

		// Token: 0x04000DE7 RID: 3559
		private static XmlSchemaSet[] schemas = new XmlSchemaSet[Validator.airsyncVersion.Length];

		// Token: 0x04000DE8 RID: 3560
		private static bool schemasCreated;

		// Token: 0x04000DE9 RID: 3561
		private readonly List<Validator.XmlValidationError> validationErrors;

		// Token: 0x04000DEA RID: 3562
		private int versionIdx;

		// Token: 0x04000DEB RID: 3563
		private int version;

		// Token: 0x04000DEC RID: 3564
		private bool failed;

		// Token: 0x02000260 RID: 608
		internal class XmlValidationError
		{
			// Token: 0x06001664 RID: 5732 RVA: 0x00087A6C File Offset: 0x00085C6C
			public XmlValidationError(string message, XmlSeverityType severity, int lineNumber, int linePosition)
			{
				this.Message = message;
				this.Severity = severity;
				this.LineNumber = lineNumber;
				this.LinePosition = linePosition;
			}

			// Token: 0x06001665 RID: 5733 RVA: 0x00087A91 File Offset: 0x00085C91
			public XmlValidationError(string messagePattern, params object[] args) : this(string.Format(messagePattern, args), XmlSeverityType.Error, 0, 0)
			{
			}

			// Token: 0x06001666 RID: 5734 RVA: 0x00087AA3 File Offset: 0x00085CA3
			public XmlValidationError(ValidationEventArgs args) : this(args.Message, args.Severity, args.Exception.LineNumber, args.Exception.LinePosition)
			{
			}

			// Token: 0x170007AC RID: 1964
			// (get) Token: 0x06001667 RID: 5735 RVA: 0x00087ACD File Offset: 0x00085CCD
			// (set) Token: 0x06001668 RID: 5736 RVA: 0x00087AD5 File Offset: 0x00085CD5
			public string Message { get; private set; }

			// Token: 0x170007AD RID: 1965
			// (get) Token: 0x06001669 RID: 5737 RVA: 0x00087ADE File Offset: 0x00085CDE
			// (set) Token: 0x0600166A RID: 5738 RVA: 0x00087AE6 File Offset: 0x00085CE6
			public XmlSeverityType Severity { get; private set; }

			// Token: 0x170007AE RID: 1966
			// (get) Token: 0x0600166B RID: 5739 RVA: 0x00087AEF File Offset: 0x00085CEF
			// (set) Token: 0x0600166C RID: 5740 RVA: 0x00087AF7 File Offset: 0x00085CF7
			public int LineNumber { get; private set; }

			// Token: 0x170007AF RID: 1967
			// (get) Token: 0x0600166D RID: 5741 RVA: 0x00087B00 File Offset: 0x00085D00
			// (set) Token: 0x0600166E RID: 5742 RVA: 0x00087B08 File Offset: 0x00085D08
			public int LinePosition { get; private set; }

			// Token: 0x0600166F RID: 5743 RVA: 0x00087B14 File Offset: 0x00085D14
			public override string ToString()
			{
				if (this.LineNumber > 0)
				{
					return string.Format("Message = {0}; Severity = {1}; LineNumber = {2}; LinePosition = {3}", new object[]
					{
						this.Message,
						this.Severity,
						this.LineNumber,
						this.LinePosition
					});
				}
				return string.Format("Message = {0}; Severity = {1}", this.Message, this.Severity);
			}
		}
	}
}
