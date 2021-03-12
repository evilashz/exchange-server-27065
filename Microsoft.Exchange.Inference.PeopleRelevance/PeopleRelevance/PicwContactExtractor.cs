using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PicwContactExtractor : BaseComponent
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000371A File Offset: 0x0000191A
		internal PicwContactExtractor()
		{
			this.DiagnosticsSession.ComponentName = "PicwContactExtractor";
			this.DiagnosticsSession.Tracer = ExTraceGlobals.RecipientExtractorTracer;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003742 File Offset: 0x00001942
		public override string Description
		{
			get
			{
				return "The PicwContactExtractor extracts recipient related properties.";
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003749 File Offset: 0x00001949
		public override string Name
		{
			get
			{
				return "PicwContactExtractor";
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003750 File Offset: 0x00001950
		protected override void InternalProcessDocument(DocumentContext data)
		{
			object obj;
			if (data.Document.TryGetProperty(PeopleRelevanceSchema.IsBasedOnRecipientInfoData, out obj) && (bool)obj)
			{
				this.DiagnosticsSession.TraceDebug<IIdentity>("Processing PICW data - {0}", data.Document.Identity);
				IEnumerable<IRecipientInfo> recipientList;
				if (data.Document.TryGetProperty(PeopleRelevanceSchema.RecipientInfoEnumerable, out obj))
				{
					recipientList = (obj as IEnumerable<IRecipientInfo>);
				}
				else
				{
					recipientList = new List<IRecipientInfo>();
				}
				IDictionary<string, IInferenceRecipient> dictionary;
				if (data.Document.TryGetProperty(PeopleRelevanceSchema.ContactList, out obj))
				{
					dictionary = (obj as IDictionary<string, IInferenceRecipient>);
				}
				else
				{
					dictionary = new Dictionary<string, IInferenceRecipient>();
				}
				this.BuildRecipientList(data, recipientList, dictionary);
				data.Document.SetProperty(PeopleRelevanceSchema.ContactList, dictionary);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000037F8 File Offset: 0x000019F8
		private void BuildRecipientList(DocumentContext data, IEnumerable<IRecipientInfo> recipientList, IDictionary<string, IInferenceRecipient> contactList)
		{
			if (recipientList == null || !recipientList.Any<IRecipientInfo>())
			{
				return;
			}
			ExDateTime property = data.Document.GetProperty<ExDateTime>(PeopleRelevanceSchema.LastProcessedMessageSentTime);
			foreach (IRecipientInfo recipientInfo in recipientList)
			{
				if (recipientInfo.SentCount != 0U)
				{
					if (string.IsNullOrEmpty(recipientInfo.Address))
					{
						this.DiagnosticsSession.TraceDebug<IIdentity>("{0}: missing Address", data.Document.Identity);
					}
					else if (recipientInfo.FirstSentTimeUtc == null || recipientInfo.LastSentTimeUtc == null)
					{
						this.DiagnosticsSession.TraceDebug<IIdentity, string>("{0}: {1}: empty FirstSentTime or LastSentTime", data.Document.Identity, recipientInfo.Address);
					}
					else if (recipientInfo.LastSentTimeUtc < recipientInfo.FirstSentTimeUtc)
					{
						this.DiagnosticsSession.TraceDebug("{0}: {1}: LastSentTime {2} < FirstSentTime {3}", new object[]
						{
							data.Document.Identity,
							recipientInfo.Address,
							recipientInfo.LastSentTimeUtc.Value,
							recipientInfo.FirstSentTimeUtc.Value
						});
					}
					else if (!(recipientInfo.LastSentTimeUtc.Value.UniversalTime < property.UniversalTime))
					{
						object obj;
						long num;
						if (!data.Document.TryGetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, out obj))
						{
							num = 1L;
							data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowStartTime, recipientInfo.LastSentTimeUtc);
							data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, num);
						}
						else
						{
							num = (long)obj;
							TimeSpan t = recipientInfo.LastSentTimeUtc.Value.UniversalTime - data.Document.GetProperty<ExDateTime>(PeopleRelevanceSchema.CurrentTimeWindowStartTime).UniversalTime;
							if (t >= PicwContactExtractor.TimeWindowLength)
							{
								num += 1L;
								data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowStartTime, recipientInfo.LastSentTimeUtc);
								data.Document.SetProperty(PeopleRelevanceSchema.CurrentTimeWindowNumber, num);
							}
						}
						string text = recipientInfo.Address.ToLower(CultureInfo.InvariantCulture);
						IInferenceRecipient inferenceRecipient;
						if (!contactList.ContainsKey(text))
						{
							inferenceRecipient = new InferenceRecipient(new MdbRecipient(text, text, string.Empty));
							inferenceRecipient.RawRecipientWeight = 5.0;
							inferenceRecipient.RecipientRank = int.MaxValue;
							contactList.Add(text, inferenceRecipient);
						}
						else
						{
							inferenceRecipient = contactList[text];
						}
						if (inferenceRecipient.TotalSentCount != (long)((ulong)recipientInfo.SentCount))
						{
							inferenceRecipient.FirstSentTime = recipientInfo.FirstSentTimeUtc.Value.UniversalTime;
							inferenceRecipient.LastSentTime = recipientInfo.LastSentTimeUtc.Value.UniversalTime;
							inferenceRecipient.LastUsedInTimeWindow = num;
							inferenceRecipient.RawRecipientWeight += (double)((ulong)recipientInfo.SentCount - (ulong)inferenceRecipient.TotalSentCount);
							inferenceRecipient.TotalSentCount = (long)((ulong)recipientInfo.SentCount);
						}
					}
				}
			}
		}

		// Token: 0x04000039 RID: 57
		public const int InitialRecipientWeight = 6;

		// Token: 0x0400003A RID: 58
		public const int InitialRecipientWeightForReplies = 2;

		// Token: 0x0400003B RID: 59
		private const string ComponentDescription = "The PicwContactExtractor extracts recipient related properties.";

		// Token: 0x0400003C RID: 60
		private const string ComponentName = "PicwContactExtractor";

		// Token: 0x0400003D RID: 61
		public static readonly TimeSpan TimeWindowLength = TimeSpan.FromHours(8.0);
	}
}
