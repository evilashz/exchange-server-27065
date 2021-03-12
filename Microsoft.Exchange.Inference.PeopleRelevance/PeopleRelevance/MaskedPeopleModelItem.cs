using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Inference.Common;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000025 RID: 37
	[DataContract(Name = "MaskedPeopleModelItem")]
	[KnownType(typeof(InferenceRecipient))]
	[Serializable]
	internal class MaskedPeopleModelItem : InferenceBaseModelItem
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00006F76 File Offset: 0x00005176
		public MaskedPeopleModelItem()
		{
			base.Version = PeopleModelItem.CurrentVersion;
			this.ContactList = new List<MaskedRecipient>(10);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00006F96 File Offset: 0x00005196
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00006F9E File Offset: 0x0000519E
		[DataMember]
		public List<MaskedRecipient> ContactList { get; set; }

		// Token: 0x06000146 RID: 326 RVA: 0x00006FAF File Offset: 0x000051AF
		public IDictionary<string, MaskedRecipient> CreateDictionary()
		{
			return this.ContactList.ToDictionary((MaskedRecipient x) => x.EmailAddress, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000099 RID: 153
		public const string ModelDataFAIName = "Inference.MaskedPeopleModel";

		// Token: 0x0400009A RID: 154
		public static readonly Version MinimumSupportedVersion = new Version(1, 0);

		// Token: 0x0400009B RID: 155
		public static readonly Version CurrentVersion = new Version(1, 0);
	}
}
