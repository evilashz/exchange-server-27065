using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RecipientExtractor : BaseComponent
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00006800 File Offset: 0x00004A00
		internal RecipientExtractor()
		{
			this.DiagnosticsSession.ComponentName = "RecipientExtractor";
			this.DiagnosticsSession.Tracer = ExTraceGlobals.RecipientExtractorTracer;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006828 File Offset: 0x00004A28
		public override string Description
		{
			get
			{
				return "The RecipientExtractor extracts recipient related properties.";
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000682F File Offset: 0x00004A2F
		public override string Name
		{
			get
			{
				return "RecipientExtractor";
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006838 File Offset: 0x00004A38
		protected override void InternalProcessDocument(DocumentContext data)
		{
			this.DiagnosticsSession.TraceDebug<IIdentity>("Processing document - {0}", data.Document.Identity);
			object obj;
			IDictionary<string, IInferenceRecipient> dictionary;
			if (data.Document.TryGetProperty(PeopleRelevanceSchema.ContactList, out obj))
			{
				dictionary = (obj as IDictionary<string, IInferenceRecipient>);
			}
			else
			{
				dictionary = new Dictionary<string, IInferenceRecipient>();
			}
			ExDateTime property = data.Document.GetProperty<ExDateTime>(PeopleRelevanceSchema.SentTime);
			bool isReply = false;
			if (data.Document.TryGetProperty(PeopleRelevanceSchema.IsReply, out obj))
			{
				isReply = (bool)obj;
			}
			long num;
			if (!data.Document.TryGetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, out obj))
			{
				num = 1L;
				data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowStartTime, property);
				data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, num);
			}
			else
			{
				num = (long)obj;
				TimeSpan t = property - data.Document.GetProperty<ExDateTime>(PeopleRelevanceSchema.CurrentTimeWindowStartTime);
				if (t >= RecipientExtractor.TimeWindowLength)
				{
					num += 1L;
					data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowStartTime, property);
					data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, num);
				}
			}
			data.Document.TryGetProperty(PeopleRelevanceSchema.RecipientsTo, out obj);
			if (obj != null)
			{
				IList<IMessageRecipient> recipients = (IList<IMessageRecipient>)obj;
				this.ProcessRecipients(recipients, dictionary, property, isReply, num);
			}
			data.Document.TryGetProperty(PeopleRelevanceSchema.RecipientsCc, out obj);
			if (obj != null)
			{
				IList<IMessageRecipient> recipients2 = (IList<IMessageRecipient>)obj;
				this.ProcessRecipients(recipients2, dictionary, property, isReply, num);
			}
			data.Document.SetProperty(PeopleRelevanceSchema.ContactList, dictionary);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000069C4 File Offset: 0x00004BC4
		private void ProcessRecipients(IList<IMessageRecipient> recipients, IDictionary<string, IInferenceRecipient> contactList, ExDateTime sentTime, bool isReply, long currentTimeWindowNumber)
		{
			foreach (IMessageRecipient recipient in recipients)
			{
				IInferenceRecipient inferenceRecipient = new InferenceRecipient(recipient);
				string key = inferenceRecipient.SmtpAddress.ToLower(CultureInfo.InvariantCulture);
				if (!contactList.ContainsKey(key))
				{
					contactList.Add(key, inferenceRecipient);
				}
				else
				{
					contactList[key].UpdateFromRecipient(inferenceRecipient);
					inferenceRecipient = contactList[key];
				}
				if (inferenceRecipient.TotalSentCount == 0L)
				{
					int num = isReply ? 2 : 6;
					inferenceRecipient.FirstSentTime = sentTime.UniversalTime;
					inferenceRecipient.RawRecipientWeight = (double)(num - 1);
					inferenceRecipient.RecipientRank = int.MaxValue;
				}
				inferenceRecipient.TotalSentCount += 1L;
				inferenceRecipient.LastSentTime = sentTime.UniversalTime;
				inferenceRecipient.LastUsedInTimeWindow = currentTimeWindowNumber;
				inferenceRecipient.RawRecipientWeight += 1.0;
			}
		}

		// Token: 0x04000083 RID: 131
		public const int InitialRecipientWeight = 6;

		// Token: 0x04000084 RID: 132
		public const int InitialRecipientWeightForReplies = 2;

		// Token: 0x04000085 RID: 133
		private const string ComponentDescription = "The RecipientExtractor extracts recipient related properties.";

		// Token: 0x04000086 RID: 134
		private const string ComponentName = "RecipientExtractor";

		// Token: 0x04000087 RID: 135
		public static readonly TimeSpan TimeWindowLength = TimeSpan.FromHours(8.0);
	}
}
