using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002F1 RID: 753
	[Serializable]
	internal sealed class RecipientEventData
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x000673A0 File Offset: 0x000655A0
		public RecipientEventData(List<RecipientTrackingEvent> mainResult)
		{
			this.mainResult = mainResult;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x000673AF File Offset: 0x000655AF
		public RecipientEventData(List<RecipientTrackingEvent> mainResult, List<List<RecipientTrackingEvent>> handoffPaths) : this(mainResult)
		{
			this.handoffPaths = handoffPaths;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x000673BF File Offset: 0x000655BF
		public List<RecipientTrackingEvent> Events
		{
			get
			{
				return this.mainResult;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x000673C7 File Offset: 0x000655C7
		public List<List<RecipientTrackingEvent>> HandoffPaths
		{
			get
			{
				return this.handoffPaths;
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x000673D0 File Offset: 0x000655D0
		public static Dictionary<string, RecipientEventData> DeserializeMultiple(List<RecipientTrackingEvent> combinedReports)
		{
			Dictionary<string, List<RecipientTrackingEvent>> dictionary = new Dictionary<string, List<RecipientTrackingEvent>>();
			foreach (RecipientTrackingEvent recipientTrackingEvent in combinedReports)
			{
				string key = recipientTrackingEvent.ExtendedProperties.MessageTrackingReportId ?? string.Empty;
				List<RecipientTrackingEvent> list;
				if (!dictionary.TryGetValue(key, out list))
				{
					list = new List<RecipientTrackingEvent>();
					dictionary[key] = list;
				}
				list.Add(recipientTrackingEvent);
			}
			Dictionary<string, RecipientEventData> dictionary2 = new Dictionary<string, RecipientEventData>(dictionary.Count);
			foreach (string key2 in dictionary.Keys)
			{
				dictionary2[key2] = RecipientEventData.Deserialize(dictionary[key2]);
			}
			return dictionary2;
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x000674B8 File Offset: 0x000656B8
		public List<RecipientTrackingEvent> Serialize()
		{
			if (this.mainResult != null)
			{
				foreach (RecipientTrackingEvent recipientTrackingEvent in this.mainResult)
				{
					recipientTrackingEvent.UniquePathId = "0";
				}
				return this.mainResult;
			}
			if (this.handoffPaths == null)
			{
				return null;
			}
			int num = 0;
			foreach (List<RecipientTrackingEvent> list in this.handoffPaths)
			{
				num += list.Count;
			}
			List<RecipientTrackingEvent> list2 = new List<RecipientTrackingEvent>(num);
			int num2 = 1;
			foreach (List<RecipientTrackingEvent> list3 in this.handoffPaths)
			{
				foreach (RecipientTrackingEvent recipientTrackingEvent2 in list3)
				{
					recipientTrackingEvent2.UniquePathId = num2.ToString();
				}
				list2.AddRange(list3);
				num2++;
			}
			return list2;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00067610 File Offset: 0x00065810
		private static RecipientEventData Deserialize(List<RecipientTrackingEvent> combinedPaths)
		{
			Dictionary<string, List<RecipientTrackingEvent>> dictionary = new Dictionary<string, List<RecipientTrackingEvent>>();
			foreach (RecipientTrackingEvent recipientTrackingEvent in combinedPaths)
			{
				List<RecipientTrackingEvent> list = null;
				if (!dictionary.TryGetValue(recipientTrackingEvent.UniquePathId, out list))
				{
					list = new List<RecipientTrackingEvent>();
					dictionary[recipientTrackingEvent.UniquePathId] = list;
				}
				list.Add(recipientTrackingEvent);
			}
			if (dictionary.Keys.Count == 1)
			{
				using (Dictionary<string, List<RecipientTrackingEvent>>.KeyCollection.Enumerator enumerator2 = dictionary.Keys.GetEnumerator())
				{
					enumerator2.MoveNext();
					if (enumerator2.Current.Equals("0"))
					{
						return new RecipientEventData(dictionary[enumerator2.Current]);
					}
				}
			}
			List<List<RecipientTrackingEvent>> list2 = new List<List<RecipientTrackingEvent>>();
			foreach (KeyValuePair<string, List<RecipientTrackingEvent>> keyValuePair in dictionary)
			{
				list2.Add(keyValuePair.Value);
			}
			return new RecipientEventData(null, list2);
		}

		// Token: 0x04000E69 RID: 3689
		private const string MainResultUniquePathId = "0";

		// Token: 0x04000E6A RID: 3690
		private List<RecipientTrackingEvent> mainResult;

		// Token: 0x04000E6B RID: 3691
		private List<List<RecipientTrackingEvent>> handoffPaths;
	}
}
