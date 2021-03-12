using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ContentTypes.Tnef;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.TransportOperators
{
	// Token: 0x02000006 RID: 6
	internal class TransportRetrieverProducer : ExchangeProducerBase<TransportRetrieverOperator>, IAttachmentRetrieverProducer
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022F4 File Offset: 0x000004F4
		public TransportRetrieverProducer(TransportRetrieverOperator retrieverOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(retrieverOperator, inputType, context, ExTraceGlobals.TransportOperatorTracer, TransportRetrieverProducer.TransportOperatorGuid, "Microsoft.Exchange.Transport")
		{
			this.contentForLanguageDetection = new StringBuilder(base.Config.LanguageDetectionMaximumCharacters);
			this.needLanguageDetectionLogging = base.Snapshot.Search.LanguageDetection.EnableLanguageDetectionLogging;
			this.languageDetectionLoggingSamplingFrequency = base.Snapshot.Search.LanguageDetection.SamplingFrequency;
			this.languageSelectionEnabled = base.Snapshot.Search.LanguageDetection.EnableLanguageSelection;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002389 File Offset: 0x00000589
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002391 File Offset: 0x00000591
		public IUpdateableRecord Holder { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000239A File Offset: 0x0000059A
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000023A2 File Offset: 0x000005A2
		public int ErrorCodeIndex { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023AB File Offset: 0x000005AB
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000023B3 File Offset: 0x000005B3
		public int LastAttemptTimeIndex { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023BC File Offset: 0x000005BC
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000023C4 File Offset: 0x000005C4
		public int AttachmentsIndex { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023CD File Offset: 0x000005CD
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000023D5 File Offset: 0x000005D5
		public int AttachmentFileNamesIndex { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023DE File Offset: 0x000005DE
		protected override bool StartOfFlow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E4 File Offset: 0x000005E4
		public override void InternalProcessRecord(IRecord record)
		{
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			string stringValue = ((IStringField)this.Holder[this.contextIdIndex]).StringValue;
			Guid guid = Guid.Parse(stringValue);
			int int32Value = ((IInt32Field)this.Holder[this.portIndex]).Int32Value;
			this.PromoteMessageFlags();
			IUpdateableBooleanField updateableBooleanField = (IUpdateableBooleanField)this.Holder[this.hasAttachmentsIndex];
			((IUpdateableListField<string>)this.Holder[this.AttachmentFileNamesIndex]).Clear();
			((IUpdateableListField<Uri>)this.Holder[this.AttachmentsIndex]).Clear();
			((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = 0;
			Guid primitiveGuidValue = ((IGuidField)this.Holder[this.correlationIdIndex]).PrimitiveGuidValue;
			base.Tracer.TraceDebug<Guid, int>((long)base.TracingContext, "Connecting to port {1} for context id: {0}.", guid, int32Value);
			Stream stream = TransportStreamManager.Instance.Connect(int32Value, guid);
			try
			{
				base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Creating EmailMessage from stream for context id: {0}.", guid);
				IRetrieverItem retrieverItem = null;
				EmailMessage emailMessage = null;
				try
				{
					emailMessage = EmailMessage.Create(stream);
					this.PromoteEmailMessageProperties(emailMessage, guid);
					this.UpdateLanguageDetectionLoggingFields(emailMessage);
					base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting HasAttachments for context id: {0}.", guid);
					if (emailMessage.Attachments.Count > 0)
					{
						updateableBooleanField.BooleanValue = true;
						retrieverItem = TransportItemHandlerAdaptor.WrapTransportItem(emailMessage);
						emailMessage = null;
						if (this.attachmentRetrieverHandler == null)
						{
							this.attachmentRetrieverHandler = new AttachmentRetrieverHandler(this, base.Config, base.Tracer, base.TracingContext, true);
						}
						string currentPath = stringValue;
						this.attachmentRetrieverHandler.ProcessItemForAttachments(primitiveGuidValue, Guid.Empty, Guid.Empty, retrieverItem, false, currentPath);
					}
					else
					{
						updateableBooleanField.BooleanValue = false;
					}
				}
				finally
				{
					if (emailMessage != null)
					{
						emailMessage.Dispose();
						emailMessage = null;
					}
					if (retrieverItem != null)
					{
						retrieverItem.Dispose();
						retrieverItem = null;
					}
					this.contentForLanguageDetection.Clear();
				}
			}
			catch (ExchangeDataException arg)
			{
				base.Tracer.TraceError<Guid, ExchangeDataException>((long)base.TracingContext, "Got exception for context id {0}, error:{1}.", guid, arg);
				((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = 1;
			}
			catch (IOException arg2)
			{
				base.Tracer.TraceError<Guid, IOException>((long)base.TracingContext, "Got exception for context id {0}, error:{1}.", guid, arg2);
				((IUpdateableInt32Field)this.Holder[this.ErrorCodeIndex]).Int32Value = 1;
			}
			finally
			{
				base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Returning stream for context id: {0}.", guid);
				TransportStreamManager.Instance.CheckIn(stream);
			}
			base.SetNextRecord(this.Holder);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026F4 File Offset: 0x000008F4
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<TransportRetrieverProducer>(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026FC File Offset: 0x000008FC
		protected override void Initialize()
		{
			base.Initialize();
			IRecordImplDescriptor recordImplDescriptor = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = recordImplDescriptor.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, recordImplDescriptor);
			this.ErrorCodeIndex = base.InputType.RecordType.Position(base.Operator.ErrorCode);
			this.LastAttemptTimeIndex = -1;
			this.contextIdIndex = base.InputType.RecordType.Position(base.OperatorInstance.ContextId);
			this.portIndex = base.InputType.RecordType.Position(base.OperatorInstance.Port);
			this.messageFlagsIndex = base.InputType.RecordType.Position(base.Operator.MessageFlags);
			this.bodyIndex = base.OutputType.RecordType.Position(base.OperatorInstance.Body);
			this.originalBodyIndex = base.OutputType.RecordType.Position(base.OperatorInstance.OriginalBody);
			this.subjectIndex = base.OutputType.RecordType.Position(base.OperatorInstance.Subject);
			this.fromIndex = base.OutputType.RecordType.Position(base.OperatorInstance.From);
			this.toIndex = base.OutputType.RecordType.Position(base.OperatorInstance.To);
			this.ccIndex = base.OutputType.RecordType.Position(base.OperatorInstance.Cc);
			this.sentTimeIndex = base.OutputType.RecordType.Position(base.OperatorInstance.SentTime);
			this.hasAttachmentsIndex = base.OutputType.RecordType.Position(base.OperatorInstance.HasAttachments);
			this.AttachmentsIndex = base.InputType.RecordType.Position(base.Operator.AttachmentsField);
			this.AttachmentFileNamesIndex = base.InputType.RecordType.Position(base.Operator.AttachmentFileNamesField);
			this.correlationIdIndex = base.InputType.RecordType.Position(base.Operator.CorrelationId);
			this.transportLanguageDetectionTextIndex = base.InputType.RecordType.Position(base.Operator.TransportLanguageDetectionText);
			this.messageCodePageIndex = base.InputType.RecordType.Position(base.Operator.MessageCodePage);
			this.messageLocaleIDIndex = base.InputType.RecordType.Position(base.Operator.MessageLocaleID);
			this.internetCPIDIndex = base.InputType.RecordType.Position(base.Operator.InternetCPID);
			this.needLanguageDetectionLoggingIndex = base.InputType.RecordType.Position(base.Operator.NeedLanguageDetectionLogging);
			this.shouldBypassMdmIndex = base.OutputType.RecordType.Position(base.OperatorInstance.ShouldBypassMdm);
			this.shouldBypassNlgIndex = base.OutputType.RecordType.Position(base.OperatorInstance.ShouldBypassNlg);
			this.shouldBypassTokenSerializerIndex = base.OutputType.RecordType.Position(base.OperatorInstance.ShouldBypassTokenSerializer);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A42 File Offset: 0x00000C42
		protected override void ReleaseManagedResources()
		{
			ItemDepot.Instance.CleanupFlowInstance(base.FlowIdentifier);
			base.ReleaseManagedResources();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002A5A File Offset: 0x00000C5A
		private static string EmailRecipientToString(EmailRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			return string.Format("{0}, {1}", recipient.DisplayName, recipient.SmtpAddress);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002A80 File Offset: 0x00000C80
		private void PromoteMessageFlags()
		{
			TransportFlowMessageFlags int32Value = (TransportFlowMessageFlags)((IInt32Field)this.Holder[this.messageFlagsIndex]).Int32Value;
			((IUpdateableBooleanField)this.Holder[this.shouldBypassNlgIndex]).Value = ((int32Value & TransportFlowMessageFlags.ShouldBypassNlg) == TransportFlowMessageFlags.ShouldBypassNlg);
			((IUpdateableBooleanField)this.Holder[this.shouldBypassMdmIndex]).Value = ((int32Value & TransportFlowMessageFlags.SkipMdmGeneration) == TransportFlowMessageFlags.SkipMdmGeneration);
			((IUpdateableBooleanField)this.Holder[this.shouldBypassTokenSerializerIndex]).Value = ((int32Value & TransportFlowMessageFlags.SkipTokenInfoGeneration) == TransportFlowMessageFlags.SkipTokenInfoGeneration);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B1C File Offset: 0x00000D1C
		private void PromoteEmailMessageProperties(EmailMessage emailMessage, Guid contextId)
		{
			IUpdateableStringField updateableField = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.transportLanguageDetectionTextIndex);
			IUpdateableStringField updateableField2 = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.subjectIndex);
			IUpdateableStringField updateableField3 = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.fromIndex);
			IUpdateableListField<string> updateableField4 = base.GetUpdateableField<IUpdateableListField<string>>(this.Holder, this.toIndex);
			IUpdateableListField<string> updateableField5 = base.GetUpdateableField<IUpdateableListField<string>>(this.Holder, this.ccIndex);
			IUpdateableDateTimeField updateableField6 = base.GetUpdateableField<IUpdateableDateTimeField>(this.Holder, this.sentTimeIndex);
			this.PromoteMessageBody(emailMessage, contextId);
			base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting Subject string for context id: {0}.", contextId);
			updateableField2.StringValue = emailMessage.Subject;
			if (emailMessage.From != null)
			{
				base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting From string for context id: {0}.", contextId);
				updateableField3.StringValue = TransportRetrieverProducer.EmailRecipientToString(emailMessage.From);
				this.ProcessStringForLanguageDetection(updateableField, new string[]
				{
					emailMessage.Subject,
					emailMessage.From.DisplayName
				});
			}
			else
			{
				updateableField3.StringValue = string.Empty;
				this.ProcessStringForLanguageDetection(updateableField, new string[]
				{
					emailMessage.Subject
				});
			}
			if (emailMessage.To != null)
			{
				base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting To list of string for context id: {0}.", contextId);
				foreach (EmailRecipient emailRecipient in emailMessage.To)
				{
					updateableField4.Add(TransportRetrieverProducer.EmailRecipientToString(emailRecipient));
					this.ProcessStringForLanguageDetection(updateableField, new string[]
					{
						emailRecipient.DisplayName
					});
				}
			}
			if (emailMessage.Cc != null)
			{
				base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting CC list of string for context id: {0}.", contextId);
				foreach (EmailRecipient emailRecipient2 in emailMessage.Cc)
				{
					updateableField5.Add(TransportRetrieverProducer.EmailRecipientToString(emailRecipient2));
					this.ProcessStringForLanguageDetection(updateableField, new string[]
					{
						emailRecipient2.DisplayName
					});
				}
			}
			if (this.contentForLanguageDetection.Length > 0)
			{
				IUpdateableStringField updateableStringField = updateableField;
				updateableStringField.StringValue += this.contentForLanguageDetection.ToString();
			}
			base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting Sent Time for context id: {0}.", contextId);
			updateableField6.DateTimeValue = emailMessage.Date;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002DAC File Offset: 0x00000FAC
		private void PromoteMessageBody(EmailMessage emailMessage, Guid contextId)
		{
			IUpdateableStringField updateableField = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.bodyIndex);
			IUpdateableStringField updateableField2 = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.originalBodyIndex);
			using (MessageBody messageBody = MessageBody.Create(emailMessage.Body))
			{
				if (messageBody != null)
				{
					base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Setting Body field for context id: {0}.", contextId);
					updateableField.StringValue = messageBody.ToString();
				}
			}
			updateableField2.StringValue = string.Empty;
			if (!((IUpdateableBooleanField)this.Holder[this.shouldBypassNlgIndex]).BooleanValue)
			{
				updateableField2.StringValue = updateableField.StringValue;
			}
			this.contentForLanguageDetection.Clear();
			IUpdateableStringField updateableField3 = base.GetUpdateableField<IUpdateableStringField>(this.Holder, this.transportLanguageDetectionTextIndex);
			updateableField3.StringValue = updateableField.StringValue;
			if (updateableField3.StringValue == null)
			{
				updateableField3.StringValue = string.Empty;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002EA0 File Offset: 0x000010A0
		private void ProcessStringForLanguageDetection(IUpdateableStringField transportLanguageDetectionTextField, params string[] values)
		{
			if (values == null || values.Length == 0)
			{
				return;
			}
			if (transportLanguageDetectionTextField.StringValue.Length + this.contentForLanguageDetection.Length < base.Config.LanguageDetectionMaximumCharacters)
			{
				foreach (string value in values)
				{
					if (!string.IsNullOrEmpty(value))
					{
						this.contentForLanguageDetection.Append(" ");
						this.contentForLanguageDetection.Append(value);
					}
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F14 File Offset: 0x00001114
		private void UpdateLanguageDetectionLoggingFields(EmailMessage emailMessage)
		{
			IUpdateableInt32Field updateableField = base.GetUpdateableField<IUpdateableInt32Field>(this.Holder, this.messageCodePageIndex);
			IUpdateableInt32Field updateableField2 = base.GetUpdateableField<IUpdateableInt32Field>(this.Holder, this.messageLocaleIDIndex);
			IUpdateableInt32Field updateableField3 = base.GetUpdateableField<IUpdateableInt32Field>(this.Holder, this.internetCPIDIndex);
			IUpdateableBooleanField updateableField4 = base.GetUpdateableField<IUpdateableBooleanField>(this.Holder, this.needLanguageDetectionLoggingIndex);
			updateableField4.BooleanValue = false;
			bool flag = false;
			if (this.needLanguageDetectionLogging)
			{
				flag = (Interlocked.Increment(ref TransportRetrieverProducer.counterForLanguageDetectionLogging) % this.languageDetectionLoggingSamplingFrequency == 0);
				if (flag)
				{
					updateableField4.BooleanValue = true;
				}
				else
				{
					base.Tracer.TraceDebug<int>((long)base.TracingContext, "Current counter: {0}", TransportRetrieverProducer.counterForLanguageDetectionLogging);
				}
			}
			if (flag || this.languageSelectionEnabled)
			{
				int num;
				if (emailMessage.TryGetMapiProperty<int>(TnefPropertyTag.MessageCodepage, out num))
				{
					base.Tracer.TraceDebug<int>((long)base.TracingContext, "messageCodePage: {0}.", num);
					updateableField.Int32Value = num;
				}
				int num2;
				if (emailMessage.TryGetMapiProperty<int>(TnefPropertyTag.MessageLocaleID, out num2))
				{
					base.Tracer.TraceDebug<int>((long)base.TracingContext, "messageLocaleID: {0}.", num2);
					updateableField2.Int32Value = num2;
				}
				int num3;
				if (emailMessage.TryGetMapiProperty<int>(TnefPropertyTag.InternetCPID, out num3))
				{
					base.Tracer.TraceDebug<int>((long)base.TracingContext, "internetCPID: {0}.", num3);
					updateableField3.Int32Value = num3;
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000306E File Offset: 0x0000126E
		string IAttachmentRetrieverProducer.get_FlowIdentifier()
		{
			return base.FlowIdentifier;
		}

		// Token: 0x04000005 RID: 5
		private const string TransportOperatorName = "Microsoft.Exchange.Transport";

		// Token: 0x04000006 RID: 6
		private static readonly Guid TransportOperatorGuid = Guid.Parse("74b6db88-f2a1-427d-9799-561ebeda27ae");

		// Token: 0x04000007 RID: 7
		private static int counterForLanguageDetectionLogging;

		// Token: 0x04000008 RID: 8
		private readonly bool needLanguageDetectionLogging;

		// Token: 0x04000009 RID: 9
		private readonly int languageDetectionLoggingSamplingFrequency;

		// Token: 0x0400000A RID: 10
		private readonly bool languageSelectionEnabled;

		// Token: 0x0400000B RID: 11
		private int contextIdIndex;

		// Token: 0x0400000C RID: 12
		private int portIndex;

		// Token: 0x0400000D RID: 13
		private int bodyIndex;

		// Token: 0x0400000E RID: 14
		private int originalBodyIndex;

		// Token: 0x0400000F RID: 15
		private int subjectIndex;

		// Token: 0x04000010 RID: 16
		private int fromIndex;

		// Token: 0x04000011 RID: 17
		private int toIndex;

		// Token: 0x04000012 RID: 18
		private int ccIndex;

		// Token: 0x04000013 RID: 19
		private int sentTimeIndex;

		// Token: 0x04000014 RID: 20
		private int hasAttachmentsIndex;

		// Token: 0x04000015 RID: 21
		private int correlationIdIndex;

		// Token: 0x04000016 RID: 22
		private int transportLanguageDetectionTextIndex;

		// Token: 0x04000017 RID: 23
		private int messageCodePageIndex;

		// Token: 0x04000018 RID: 24
		private int messageFlagsIndex;

		// Token: 0x04000019 RID: 25
		private int shouldBypassNlgIndex;

		// Token: 0x0400001A RID: 26
		private int shouldBypassTokenSerializerIndex;

		// Token: 0x0400001B RID: 27
		private int shouldBypassMdmIndex;

		// Token: 0x0400001C RID: 28
		private int messageLocaleIDIndex;

		// Token: 0x0400001D RID: 29
		private int internetCPIDIndex;

		// Token: 0x0400001E RID: 30
		private int needLanguageDetectionLoggingIndex;

		// Token: 0x0400001F RID: 31
		private AttachmentRetrieverHandler attachmentRetrieverHandler;

		// Token: 0x04000020 RID: 32
		private StringBuilder contentForLanguageDetection;
	}
}
