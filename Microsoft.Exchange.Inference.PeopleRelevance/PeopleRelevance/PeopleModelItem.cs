using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Inference.Common;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000012 RID: 18
	[DataContract(Name = "PeopleModelData")]
	[KnownType(typeof(InferenceRecipient))]
	[Serializable]
	internal class PeopleModelItem : InferenceBaseModelItem
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000035D0 File Offset: 0x000017D0
		public PeopleModelItem()
		{
			base.Version = PeopleModelItem.CurrentVersion;
			this.LastProcessedMessageSentTime = DateTime.MinValue;
			this.IsDefaultModel = true;
			this.ContactList = new List<IInferenceRecipient>();
			this.CurrentTimeWindowStartTime = DateTime.MinValue;
			this.LastRecipientCacheValidationTime = DateTime.MinValue;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003621 File Offset: 0x00001821
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003629 File Offset: 0x00001829
		[DataMember(Name = "ContactList")]
		public List<IInferenceRecipient> ContactList { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003632 File Offset: 0x00001832
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000363A File Offset: 0x0000183A
		[DataMember]
		public DateTime LastProcessedMessageSentTime { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003643 File Offset: 0x00001843
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000364B File Offset: 0x0000184B
		[DataMember]
		public DateTime CurrentTimeWindowStartTime { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003654 File Offset: 0x00001854
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000365C File Offset: 0x0000185C
		[DataMember]
		public long CurrentTimeWindowNumber { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003665 File Offset: 0x00001865
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000366D File Offset: 0x0000186D
		[DataMember]
		public DateTime LastRecipientCacheValidationTime { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003676 File Offset: 0x00001876
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000367E File Offset: 0x0000187E
		[DataMember]
		internal bool IsDefaultModel { get; set; }

		// Token: 0x06000095 RID: 149 RVA: 0x00003688 File Offset: 0x00001888
		public IDictionary<string, IInferenceRecipient> CreateContactDictionary()
		{
			IDictionary<string, IInferenceRecipient> dictionary = new Dictionary<string, IInferenceRecipient>();
			foreach (IInferenceRecipient inferenceRecipient in this.ContactList)
			{
				string text = inferenceRecipient.SmtpAddress;
				if (!string.IsNullOrEmpty(text))
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					dictionary.Add(text, inferenceRecipient);
				}
			}
			return dictionary;
		}

		// Token: 0x04000030 RID: 48
		public const string ModelDataFAIName = "Inference.PeopleModel";

		// Token: 0x04000031 RID: 49
		public static readonly Version MinimumSupportedVersion = new Version(1, 5);

		// Token: 0x04000032 RID: 50
		public static readonly Version CurrentVersion = new Version(1, 5);
	}
}
