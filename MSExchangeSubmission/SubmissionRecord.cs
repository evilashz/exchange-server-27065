using System;
using System.Globalization;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000009 RID: 9
	internal abstract class SubmissionRecord
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00004202 File Offset: 0x00002402
		protected SubmissionRecord(object submissionData)
		{
			if (submissionData == null)
			{
				throw new ArgumentNullException("submissionData");
			}
			this.submissionData = submissionData;
			this.recordTime = DateTime.UtcNow;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000422C File Offset: 0x0000242C
		public static void Drop(Breadcrumbs<SubmissionRecord.Success> successHistory, Breadcrumbs<SubmissionRecord.Failure> failureHistory, object submissionData, MailSubmissionResult result)
		{
			if (result.ErrorCode == 0U)
			{
				successHistory.Drop(new SubmissionRecord.Success(submissionData, result.Sender, result.MessageId, result.Subject));
				return;
			}
			failureHistory.Drop(new SubmissionRecord.Failure(submissionData, HResult.GetStringForErrorCode(result.ErrorCode), result.DiagnosticInfo));
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004280 File Offset: 0x00002480
		public override string ToString()
		{
			MapiEvent mapiEvent = this.submissionData as MapiEvent;
			return string.Format("Created: {0}, Submitted: {1}, Counter: {2}, Submitter: {3}, ObjectClass: {4}, ClientType: {5}", new object[]
			{
				this.recordTime,
				mapiEvent.CreateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo),
				mapiEvent.EventCounter,
				mapiEvent.MailboxGuid,
				mapiEvent.ObjectClass,
				Int32EnumFormatter<MapiEventClientTypes>.Format((int)mapiEvent.ClientType)
			});
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00004308 File Offset: 0x00002508
		protected void AddDiagnosticInfo(XElement recordElement)
		{
			MapiEvent mapiEvent = this.submissionData as MapiEvent;
			recordElement.Add(new object[]
			{
				new XElement("created", this.recordTime),
				new XElement("submitted", mapiEvent.CreateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo)),
				new XElement("counter", mapiEvent.EventCounter),
				new XElement("submitterMailboxGuid", mapiEvent.MailboxGuid),
				new XElement("objectClass", mapiEvent.ObjectClass),
				new XElement("clientType", Int32EnumFormatter<MapiEventClientTypes>.Format((int)mapiEvent.ClientType))
			});
		}

		// Token: 0x04000058 RID: 88
		private readonly DateTime recordTime;

		// Token: 0x04000059 RID: 89
		private object submissionData;

		// Token: 0x0200000A RID: 10
		internal class Success : SubmissionRecord
		{
			// Token: 0x06000052 RID: 82 RVA: 0x000043E5 File Offset: 0x000025E5
			public Success(object submissionData, string sender, string messageId, string subject) : base(submissionData)
			{
				this.sender = sender;
				this.subject = subject;
				this.messageId = messageId;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00004404 File Offset: 0x00002604
			public override string ToString()
			{
				return string.Format("{0}, Sender: {1}, MessageId: {2}, Subject: {3}", new object[]
				{
					base.ToString(),
					this.sender,
					this.messageId,
					this.subject
				});
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00004448 File Offset: 0x00002648
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement("Submission");
				xelement.SetAttributeValue("messageId", this.messageId);
				xelement.Add(new object[]
				{
					new XElement("sender", this.sender),
					new XElement("subject", this.subject)
				});
				base.AddDiagnosticInfo(xelement);
				return xelement;
			}

			// Token: 0x0400005A RID: 90
			private readonly string sender;

			// Token: 0x0400005B RID: 91
			private readonly string subject;

			// Token: 0x0400005C RID: 92
			private readonly string messageId;
		}

		// Token: 0x0200000B RID: 11
		internal class Failure : SubmissionRecord
		{
			// Token: 0x06000055 RID: 85 RVA: 0x000044C1 File Offset: 0x000026C1
			public Failure(object submissionData, string errorCode, string diagnosticInfo) : base(submissionData)
			{
				this.errorCode = errorCode;
				this.diagnosticInfo = diagnosticInfo;
			}

			// Token: 0x06000056 RID: 86 RVA: 0x000044D8 File Offset: 0x000026D8
			public override string ToString()
			{
				return string.Format("{0}, ErrorCode: {1}, DiagnosticInfo: {2}", base.ToString(), this.errorCode, this.diagnosticInfo);
			}

			// Token: 0x06000057 RID: 87 RVA: 0x000044F8 File Offset: 0x000026F8
			public XElement GetDiagnosticInfo()
			{
				XElement xelement = new XElement("Failure");
				base.AddDiagnosticInfo(xelement);
				xelement.Add(new object[]
				{
					new XElement("errorCode", this.errorCode),
					new XElement("diagnosticInfo", this.diagnosticInfo)
				});
				return xelement;
			}

			// Token: 0x0400005D RID: 93
			private readonly string errorCode;

			// Token: 0x0400005E RID: 94
			private readonly string diagnosticInfo;
		}
	}
}
