using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.NlpBase.RichTypes;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000015 RID: 21
	internal class TransportWriterProducer : ExchangeWriterBase<TransportWriterOperator>
	{
		// Token: 0x0600005F RID: 95 RVA: 0x000035DA File Offset: 0x000017DA
		public TransportWriterProducer(IEvaluationContext context, bool forward) : base(context, forward, ExTraceGlobals.TransportOperatorTracer)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000035E9 File Offset: 0x000017E9
		protected override bool UsesAutoFlush
		{
			get
			{
				return base.CallbackInterval > 1;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000035F4 File Offset: 0x000017F4
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TransportWriterProducer>(this);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000035FC File Offset: 0x000017FC
		protected override bool FlushWriter()
		{
			return true;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000035FF File Offset: 0x000017FF
		protected override void CloseWriter()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003604 File Offset: 0x00001804
		protected override void InternalWrite(IRecord record)
		{
			IStringField stringField = (IStringField)record[this.contextIdIndex];
			Guid guid = Guid.Parse(stringField.StringValue);
			List<ISerializableProperty> list = new List<ISerializableProperty>();
			EvaluationErrors int32Value = (EvaluationErrors)((IUpdateableInt32Field)record[this.errorCodeIndex]).Int32Value;
			string text = "unknown";
			if (int32Value == EvaluationErrors.None)
			{
				bool flag = true;
				if (this.languageIdentificationFailedIndex != -1)
				{
					IBooleanField booleanField = (IBooleanField)record[this.languageIdentificationFailedIndex];
					flag = booleanField.BooleanValue;
				}
				if (flag)
				{
					base.Tracer.TraceDebug((long)base.TracingContext, "LanguageIdentification failure detected. Skip output of the AnnotationToken and Language properties.");
				}
				if (this.annotationTokenIndex != -1 && !flag)
				{
					IBlobField blobField = (IBlobField)record[this.annotationTokenIndex];
					byte[] blobValue = blobField.BlobValue;
					if (blobValue != null && blobValue.Length > 0)
					{
						base.Tracer.TraceDebug<Guid, int>((long)base.TracingContext, "Annotation token ({1} bytes) for context id: {0}.", guid, blobValue.Length);
						SerializableStreamProperty item = new SerializableStreamProperty(SerializablePropertyId.AnnotationToken, blobValue);
						list.Add(item);
					}
				}
				if (this.tasksXmlIndex != -1)
				{
					IStringField stringField2 = (IStringField)record[this.tasksXmlIndex];
					string stringValue = stringField2.StringValue;
					if (!string.IsNullOrEmpty(stringValue))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "TaskXml for context id: {0}.", guid);
						SerializableStringProperty item2 = new SerializableStringProperty(SerializablePropertyId.Tasks, stringValue);
						list.Add(item2);
					}
				}
				if (this.meetingsXmlIndex != -1)
				{
					IStringField stringField3 = (IStringField)record[this.meetingsXmlIndex];
					string stringValue2 = stringField3.StringValue;
					if (!string.IsNullOrEmpty(stringValue2))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "MeetingsXml for context id: {0}.", guid);
						SerializableStringProperty item3 = new SerializableStringProperty(SerializablePropertyId.Meetings, stringValue2);
						list.Add(item3);
					}
				}
				if (this.addressesXmlIndex != -1)
				{
					IStringField stringField4 = (IStringField)record[this.addressesXmlIndex];
					string stringValue3 = stringField4.StringValue;
					if (!string.IsNullOrEmpty(stringValue3))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "AddressesXml for context id: {0}.", guid);
						SerializableStringProperty item4 = new SerializableStringProperty(SerializablePropertyId.Addresses, stringValue3);
						list.Add(item4);
					}
				}
				if (this.keywordsXmlIndex != -1)
				{
					IStringField stringField5 = (IStringField)record[this.keywordsXmlIndex];
					string stringValue4 = stringField5.StringValue;
					if (!string.IsNullOrEmpty(stringValue4))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "KeywordsXml for context id: {0}.", guid);
						SerializableStringProperty item5 = new SerializableStringProperty(SerializablePropertyId.Keywords, stringValue4);
						list.Add(item5);
					}
				}
				if (this.phonesXmlIndex != -1)
				{
					IStringField stringField6 = (IStringField)record[this.phonesXmlIndex];
					string stringValue5 = stringField6.StringValue;
					if (!string.IsNullOrEmpty(stringValue5))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "PhonesXml for context id: {0}.", guid);
						SerializableStringProperty item6 = new SerializableStringProperty(SerializablePropertyId.Phones, stringValue5);
						list.Add(item6);
					}
				}
				if (this.emailsXmlIndex != -1)
				{
					IStringField stringField7 = (IStringField)record[this.emailsXmlIndex];
					string stringValue6 = stringField7.StringValue;
					if (!string.IsNullOrEmpty(stringValue6))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "EmailsXml for context id: {0}.", guid);
						SerializableStringProperty item7 = new SerializableStringProperty(SerializablePropertyId.Emails, stringValue6);
						list.Add(item7);
					}
				}
				if (this.urlsXmlIndex != -1)
				{
					IStringField stringField8 = (IStringField)record[this.urlsXmlIndex];
					string stringValue7 = stringField8.StringValue;
					if (!string.IsNullOrEmpty(stringValue7))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "UrlsXml for context id: {0}.", guid);
						SerializableStringProperty item8 = new SerializableStringProperty(SerializablePropertyId.Urls, stringValue7);
						list.Add(item8);
					}
				}
				if (this.contactsXmlIndex != -1)
				{
					IStringField stringField9 = (IStringField)record[this.contactsXmlIndex];
					string stringValue8 = stringField9.StringValue;
					if (!string.IsNullOrEmpty(stringValue8))
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "ContactsXml for context id: {0}.", guid);
						SerializableStringProperty item9 = new SerializableStringProperty(SerializablePropertyId.Contacts, stringValue8);
						list.Add(item9);
					}
				}
				if (this.languageIndex != -1 && !flag)
				{
					IStringField stringField10 = (IStringField)record[this.languageIndex];
					text = stringField10.StringValue;
					if (!string.IsNullOrEmpty(text))
					{
						base.Tracer.TraceDebug<Guid, string>((long)base.TracingContext, "Language for context id: {0}, {1}.", guid, text);
						SerializableStringProperty item10 = new SerializableStringProperty(SerializablePropertyId.Language, text);
						list.Add(item10);
					}
				}
			}
			string operatorTimings = base.Diagnostics.GetOperatorTimings(TransportFlowOperatorTimings.TimingEntryNames);
			if (!string.IsNullOrEmpty(operatorTimings))
			{
				base.Tracer.TracePerformance<Guid, string>((long)base.TracingContext, "Timing data for {0}: {1}", guid, operatorTimings);
				SerializableStringProperty item11 = new SerializableStringProperty(SerializablePropertyId.OperatorTiming, operatorTimings);
				list.Add(item11);
			}
			if (((IUpdateableBooleanField)record[this.needLanguageDetectionLoggingIndex]).BooleanValue)
			{
				long languageDetectorTime = -1L;
				long wordBreakerTime = -1L;
				OperatorTimingEntry operatorTimingEntry;
				if (base.Diagnostics.GetOperatorTimingEntry("PostTransportLanguageIdentifierDiagnosticOperator", OperatorLocation.BeginProcessRecord, out operatorTimingEntry))
				{
					languageDetectorTime = operatorTimingEntry.Elapsed;
				}
				if (base.Diagnostics.GetOperatorTimingEntry("PostWordBreakerDiagnosticOperator", OperatorLocation.BeginProcessRecord, out operatorTimingEntry))
				{
					wordBreakerTime = operatorTimingEntry.Elapsed;
				}
				base.Diagnostics.LogLanguageDetection(guid, text, languageDetectorTime, wordBreakerTime, ((ITextualField)record[this.transportLanguageDetectionTextIndex]).ContentSize, (((IInt32Field)record[this.messageCodePageIndex]).NullableInt32Value != null) ? ((IInt32Field)record[this.messageCodePageIndex]).NullableInt32Value.Value.ToString() : string.Empty, (((IInt32Field)record[this.messageLocaleIDIndex]).NullableInt32Value != null) ? ((IInt32Field)record[this.messageLocaleIDIndex]).NullableInt32Value.Value.ToString() : string.Empty, (((IInt32Field)record[this.internetCPIDIndex]).NullableInt32Value != null) ? ((IInt32Field)record[this.internetCPIDIndex]).NullableInt32Value.Value.ToString() : string.Empty);
			}
			base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Checking out stream for context id: {0}.", guid);
			using (Stream stream = TransportStreamManager.Instance.CheckOut(guid))
			{
				if (stream != null)
				{
					try
					{
						if (list.Count > 0)
						{
							base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Serializing results to stream for context id: {0}.", guid);
							SerializableProperties serializableProperties = new SerializableProperties(list.ToArray());
							serializableProperties.SerializeTo(stream);
						}
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Flushing stream for context id: {0}.", guid);
						stream.Flush();
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Reading single byte from stream for context id: {0}.", guid);
						stream.ReadByte();
					}
					catch (SocketException)
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Remote socket exception for context id: {0}.", guid);
					}
					catch (IOException)
					{
						base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Remote socket exception for context id: {0}.", guid);
					}
				}
				if (record.Metadata.SequenceId % (long)base.Operator.CallbackInterval == 0L)
				{
					base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Procedure callback for context id: {0}.", guid);
					base.ProduceCallbackSequence(record.Metadata.SequenceId, 0);
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003D38 File Offset: 0x00001F38
		protected override void Open()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003D3C File Offset: 0x00001F3C
		protected override void Initialize()
		{
			IRecordTypeDescriptor recordType = base.InputProperties.RecordSetType.RecordType;
			this.contextIdIndex = recordType.Position(base.Operator.ContextId);
			this.annotationTokenIndex = recordType.Position(base.Operator.AnnotationToken);
			this.tasksXmlIndex = recordType.Position(base.Operator.TasksXml);
			this.meetingsXmlIndex = recordType.Position(base.Operator.MeetingsXml);
			this.addressesXmlIndex = recordType.Position(base.Operator.AddressesXml);
			this.keywordsXmlIndex = recordType.Position(base.Operator.KeywordsXml);
			this.phonesXmlIndex = recordType.Position(base.Operator.PhonesXml);
			this.emailsXmlIndex = recordType.Position(base.Operator.EmailsXml);
			this.urlsXmlIndex = recordType.Position(base.Operator.UrlsXml);
			this.contactsXmlIndex = recordType.Position(base.Operator.ContactsXml);
			this.languageIndex = recordType.Position(base.Operator.Language);
			this.languageIdentificationFailedIndex = recordType.Position(base.Operator.LanguageIdentificationFailed);
			this.errorCodeIndex = recordType.Position(base.Operator.ErrorCode);
			this.messageCodePageIndex = recordType.Position(base.Operator.MessageCodePage);
			this.messageLocaleIDIndex = recordType.Position(base.Operator.MessageLocaleID);
			this.internetCPIDIndex = recordType.Position(base.Operator.InternetCPID);
			this.needLanguageDetectionLoggingIndex = recordType.Position(base.Operator.NeedLanguageDetectionLogging);
			this.transportLanguageDetectionTextIndex = recordType.Position(base.Operator.TransportLanguageDetectionText);
			base.Initialize();
		}

		// Token: 0x0400003A RID: 58
		private const string PostTransportLanguageIdentifierDiagnosticOperator = "PostTransportLanguageIdentifierDiagnosticOperator";

		// Token: 0x0400003B RID: 59
		private const string PostWordBreakerDiagnosticOperator = "PostWordBreakerDiagnosticOperator";

		// Token: 0x0400003C RID: 60
		private int contextIdIndex;

		// Token: 0x0400003D RID: 61
		private int annotationTokenIndex;

		// Token: 0x0400003E RID: 62
		private int tasksXmlIndex;

		// Token: 0x0400003F RID: 63
		private int meetingsXmlIndex;

		// Token: 0x04000040 RID: 64
		private int addressesXmlIndex;

		// Token: 0x04000041 RID: 65
		private int keywordsXmlIndex;

		// Token: 0x04000042 RID: 66
		private int phonesXmlIndex;

		// Token: 0x04000043 RID: 67
		private int emailsXmlIndex;

		// Token: 0x04000044 RID: 68
		private int urlsXmlIndex;

		// Token: 0x04000045 RID: 69
		private int contactsXmlIndex;

		// Token: 0x04000046 RID: 70
		private int languageIndex;

		// Token: 0x04000047 RID: 71
		private int languageIdentificationFailedIndex;

		// Token: 0x04000048 RID: 72
		private int errorCodeIndex;

		// Token: 0x04000049 RID: 73
		private int messageCodePageIndex;

		// Token: 0x0400004A RID: 74
		private int messageLocaleIDIndex;

		// Token: 0x0400004B RID: 75
		private int internetCPIDIndex;

		// Token: 0x0400004C RID: 76
		private int needLanguageDetectionLoggingIndex;

		// Token: 0x0400004D RID: 77
		private int transportLanguageDetectionTextIndex;
	}
}
