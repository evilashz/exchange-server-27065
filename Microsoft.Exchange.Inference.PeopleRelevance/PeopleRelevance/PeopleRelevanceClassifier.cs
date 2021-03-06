using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleRelevanceClassifier : BaseComponent
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00003B9C File Offset: 0x00001D9C
		internal PeopleRelevanceClassifier()
		{
			this.DiagnosticsSession.ComponentName = "PeopleRelevanceClassifier";
			this.DiagnosticsSession.Tracer = ExTraceGlobals.PeopleRelevanceClassifierTracer;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public override string Description
		{
			get
			{
				return "The PeopleRelevanceClassifier determines recipients relevance.";
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003BCB File Offset: 0x00001DCB
		public override string Name
		{
			get
			{
				return "PeopleRelevanceClassifier";
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003BD4 File Offset: 0x00001DD4
		protected override void InternalProcessDocument(DocumentContext data)
		{
			this.DiagnosticsSession.TraceDebug<IIdentity>("Processing document - {0}", data.Document.Identity);
			DocumentProcessingContext processingContext = (DocumentProcessingContext)data.AsyncResult.AsyncState;
			IDocument document = data.Document;
			object obj;
			if (document.TryGetProperty(PeopleRelevanceSchema.IsBasedOnRecipientInfoData, out obj) && (bool)obj)
			{
				object obj2;
				if (!document.TryGetProperty(PeopleRelevanceSchema.RecipientInfoEnumerable, out obj2))
				{
					this.DiagnosticsSession.TraceDebug("RecipientInfo list doesn't exist. Nothing to process", new object[0]);
					return;
				}
				IEnumerable<IRecipientInfo> enumerable = obj2 as IEnumerable<IRecipientInfo>;
				if (enumerable != null && !enumerable.Any<IRecipientInfo>())
				{
					this.DiagnosticsSession.TraceDebug("RecipientInfo list is empty. Nothing to process", new object[0]);
					return;
				}
			}
			else
			{
				IDocument property = document.GetProperty<IDocument>(PeopleRelevanceSchema.SentItemsTrainingSubDocument);
				Util.ThrowOnConditionFailed(property.NestedDocuments != null, "Invalid training document. NestedDocuments is null.");
				if (property.NestedDocuments.Count == 0)
				{
					this.DiagnosticsSession.TraceDebug("NestedDocuments is empty. Nothing to process", new object[0]);
					return;
				}
			}
			IDictionary<string, IInferenceRecipient> dictionary = null;
			if (!data.Document.TryGetProperty(PeopleRelevanceSchema.ContactList, out obj))
			{
				this.DiagnosticsSession.TraceDebug("Contact list is not present. Nothing to process", new object[0]);
				return;
			}
			dictionary = (obj as IDictionary<string, IInferenceRecipient>);
			if (dictionary.Count == 0)
			{
				this.DiagnosticsSession.TraceDebug("Contact list is empty. Nothing to process", new object[0]);
				return;
			}
			this.FilterMaskedContacts(processingContext, dictionary);
			long property2 = data.Document.GetProperty<long>(PeopleRelevanceSchema.CurrentTimeWindowNumber);
			long timeWindowNumberAtLastRun = 0L;
			if (data.Document.TryGetProperty(PeopleRelevanceSchema.TimeWindowNumberAtLastRun, out obj))
			{
				timeWindowNumberAtLastRun = (long)obj;
			}
			List<string> list = new List<string>();
			foreach (IInferenceRecipient inferenceRecipient in dictionary.Values)
			{
				PeopleRelevanceClassifier.ApplyDecay(inferenceRecipient, property2, timeWindowNumberAtLastRun);
				if (inferenceRecipient.RawRecipientWeight > 0.0)
				{
					if (!string.IsNullOrEmpty(inferenceRecipient.SmtpAddress))
					{
						list.Add(inferenceRecipient.SmtpAddress.ToLower(CultureInfo.InvariantCulture));
					}
					else
					{
						this.DiagnosticsSession.TraceDebug<string>("Found Contact without SMTP: {0}", inferenceRecipient.Identity.ToString());
					}
				}
			}
			PeopleRelevanceClassifier.RankRecipients(dictionary, list);
			foreach (IInferenceRecipient recipient in dictionary.Values)
			{
				this.DetermineCaptureFlag(recipient, processingContext);
			}
			this.DiagnosticsSession.TraceDebug("Store updated recipient properties in the document", new object[0]);
			data.Document.SetProperty(PeopleRelevanceSchema.ContactList, dictionary);
			data.Document.SetProperty(PeopleRelevanceSchema.TopRankedContacts, list);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E8C File Offset: 0x0000208C
		private static void ApplyDecay(IInferenceRecipient recipient, long currentTimeWindowNumber, long timeWindowNumberAtLastRun)
		{
			if (recipient.RawRecipientWeight == 0.0)
			{
				Util.ThrowOnConditionFailed(recipient.RecipientRank == int.MaxValue, "Unexpected rank for unused recipient");
				return;
			}
			Util.ThrowOnConditionFailed(recipient.LastUsedInTimeWindow > 0L && recipient.LastUsedInTimeWindow <= currentTimeWindowNumber, string.Format("Unexpected LastUsedInTimeWindow value: {0}. CurrentTimeWindowNumber: {1}.", recipient.LastUsedInTimeWindow, currentTimeWindowNumber));
			long num = currentTimeWindowNumber - recipient.LastUsedInTimeWindow;
			long num2 = timeWindowNumberAtLastRun - recipient.LastUsedInTimeWindow;
			foreach (PeopleRelevanceClassifier.Decay decay in PeopleRelevanceClassifier.DecayConfig)
			{
				if (num >= decay.TimeWindowTreshold && num2 < decay.TimeWindowTreshold)
				{
					double val = Math.Max(recipient.RawRecipientWeight * decay.PercentDecay, (double)decay.MinDecay);
					double num3 = Math.Min(recipient.RawRecipientWeight, val);
					recipient.RawRecipientWeight -= num3;
				}
				if (recipient.RawRecipientWeight == 0.0)
				{
					recipient.RecipientRank = int.MaxValue;
					return;
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003FE4 File Offset: 0x000021E4
		private static void RankRecipients(IDictionary<string, IInferenceRecipient> contactList, List<string> topRanked)
		{
			if (topRanked.Count == 0)
			{
				return;
			}
			int num = Math.Min(PeopleRelevanceConfig.Instance.MaxRelevantRecipientsCount, topRanked.Count);
			topRanked.Sort((string a, string b) => contactList[b].RawRecipientWeight.CompareTo(contactList[a].RawRecipientWeight));
			int num2 = 0;
			foreach (string key in topRanked)
			{
				contactList[key].RecipientRank = ((++num2 <= num) ? num2 : int.MaxValue);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004090 File Offset: 0x00002290
		private void FilterMaskedContacts(DocumentProcessingContext processingContext, IDictionary<string, IInferenceRecipient> contactList)
		{
			if (processingContext != null && processingContext.Session != null)
			{
				MdbMaskedPeopleModelDataBinder mdbMaskedPeopleModelDataBinder = MdbMaskedPeopleModelDataBinderFactory.Current.CreateInstance(processingContext.Session);
				MaskedPeopleModelItem modelData = mdbMaskedPeopleModelDataBinder.GetModelData();
				if (modelData == null || modelData.ContactList.Count <= 0)
				{
					this.DiagnosticsSession.TraceDebug("Deleted items model is null or empty. Skipping removal step.", new object[0]);
					return;
				}
				IDictionary<string, MaskedRecipient> dictionary = modelData.CreateDictionary();
				List<MaskedRecipient> list = new List<MaskedRecipient>(10);
				foreach (KeyValuePair<string, MaskedRecipient> keyValuePair in dictionary)
				{
					if (contactList.ContainsKey(keyValuePair.Key))
					{
						if (contactList[keyValuePair.Key].LastSentTime > keyValuePair.Value.LastMaskedFromAutoCompleteTimeUtc)
						{
							list.Add(keyValuePair.Value);
						}
						else
						{
							contactList.Remove(keyValuePair.Key);
						}
					}
				}
				if (list.Count > 0)
				{
					foreach (MaskedRecipient item in list)
					{
						modelData.ContactList.Remove(item);
					}
					this.DiagnosticsSession.TraceDebug<int>("Removed {0} out-of-date contacts from the masked model item", list.Count);
					MdbModelUtils.WriteModelItem<MaskedPeopleModelItem, MdbMaskedPeopleModelDataBinder>(mdbMaskedPeopleModelDataBinder, modelData);
					return;
				}
			}
			else
			{
				this.DiagnosticsSession.TraceDebug("Context or Session is null. Skipping removal step.", new object[0]);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004214 File Offset: 0x00002414
		private void DetermineCaptureFlag(IInferenceRecipient recipient, DocumentProcessingContext processingContext)
		{
			int relevanceCategory = recipient.RelevanceCategory;
			int captureFlag = recipient.CaptureFlag;
			int num = 0;
			if (relevanceCategory == 2147483647)
			{
				if (recipient.RelevanceCategoryAtLastCapture != 2147483647)
				{
					num = 4;
				}
			}
			else if (recipient.RelevanceCategoryAtLastCapture == 2147483647)
			{
				num = 1;
			}
			else if (relevanceCategory != recipient.RelevanceCategoryAtLastCapture)
			{
				num = 2;
			}
			recipient.CaptureFlag = num;
			if (captureFlag != num)
			{
				this.DiagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, string.Format("U={0} - Recipient cache entry {1} capture flag change: CF={2} CFB={3} R={4} W={5} RCC={6}", new object[]
				{
					(processingContext != null) ? processingContext.Session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() : string.Empty,
					recipient.SmtpAddress,
					num,
					captureFlag,
					recipient.RecipientRank,
					recipient.RawRecipientWeight,
					recipient.RelevanceCategoryAtLastCapture
				}), new object[0]);
			}
		}

		// Token: 0x0400003E RID: 62
		private const string ComponentDescription = "The PeopleRelevanceClassifier determines recipients relevance.";

		// Token: 0x0400003F RID: 63
		private const string ComponentName = "PeopleRelevanceClassifier";

		// Token: 0x04000040 RID: 64
		private static readonly PeopleRelevanceClassifier.Decay[] DecayConfig = new PeopleRelevanceClassifier.Decay[]
		{
			new PeopleRelevanceClassifier.Decay(12L, 0.0, 1),
			new PeopleRelevanceClassifier.Decay(24L, 0.0, 1),
			new PeopleRelevanceClassifier.Decay(36L, 0.0, 1),
			new PeopleRelevanceClassifier.Decay(48L, 0.25, 2),
			new PeopleRelevanceClassifier.Decay(68L, 0.25, 3),
			new PeopleRelevanceClassifier.Decay(92L, 0.5, 4),
			new PeopleRelevanceClassifier.Decay(104L, 0.75, 5),
			new PeopleRelevanceClassifier.Decay(124L, 1.0, 0)
		};

		// Token: 0x02000016 RID: 22
		private struct Decay
		{
			// Token: 0x060000A9 RID: 169 RVA: 0x0000441A File Offset: 0x0000261A
			internal Decay(long timeWindowTreshold, double percentDecay, int minDecay)
			{
				this.TimeWindowTreshold = timeWindowTreshold;
				this.PercentDecay = percentDecay;
				this.MinDecay = minDecay;
			}

			// Token: 0x04000041 RID: 65
			public readonly long TimeWindowTreshold;

			// Token: 0x04000042 RID: 66
			public readonly double PercentDecay;

			// Token: 0x04000043 RID: 67
			internal readonly int MinDecay;
		}
	}
}
