using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000F7 RID: 247
	[DataContract]
	public class KeyMapping
	{
		// Token: 0x06001EAE RID: 7854 RVA: 0x0005C324 File Offset: 0x0005A524
		public KeyMapping()
		{
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0005C32C File Offset: 0x0005A52C
		public KeyMapping(KeyMapping taskKeyMapping)
		{
			this.Context = taskKeyMapping.Context;
			this.FindMeFirstNumber = taskKeyMapping.FindMeFirstNumber;
			this.FindMeFirstNumberDuration = taskKeyMapping.FindMeFirstNumberDuration;
			this.FindMeSecondNumber = taskKeyMapping.FindMeSecondNumber;
			this.FindMeSecondNumberDuration = taskKeyMapping.FindMeSecondNumberDuration;
			this.Key = taskKeyMapping.Key;
			this.KeyMappingType = taskKeyMapping.KeyMappingType.ToStringWithNull();
			this.TransferToGALContactLegacyDN = taskKeyMapping.TransferToGALContactLegacyDN;
			if (!string.IsNullOrEmpty(this.TransferToGALContactLegacyDN))
			{
				IEnumerable<ADRecipient> enumerable = RecipientObjectResolver.Instance.ResolveLegacyDNs(new string[]
				{
					this.TransferToGALContactLegacyDN
				});
				if (enumerable != null)
				{
					using (IEnumerator<ADRecipient> enumerator = enumerable.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ADRecipient adrecipient = enumerator.Current;
							this.TransferToGALContactDisplayName = adrecipient.DisplayName;
						}
					}
				}
			}
			this.TransferToNumber = taskKeyMapping.TransferToNumber;
		}

		// Token: 0x170019D7 RID: 6615
		// (get) Token: 0x06001EB0 RID: 7856 RVA: 0x0005C424 File Offset: 0x0005A624
		// (set) Token: 0x06001EB1 RID: 7857 RVA: 0x0005C42C File Offset: 0x0005A62C
		[DataMember]
		public string Context { get; set; }

		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x0005C435 File Offset: 0x0005A635
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x0005C43D File Offset: 0x0005A63D
		[DataMember]
		public string FindMeFirstNumber { get; set; }

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x0005C446 File Offset: 0x0005A646
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x0005C44E File Offset: 0x0005A64E
		[DataMember]
		public int FindMeFirstNumberDuration { get; set; }

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0005C457 File Offset: 0x0005A657
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0005C45F File Offset: 0x0005A65F
		[DataMember]
		public string FindMeSecondNumber { get; set; }

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0005C468 File Offset: 0x0005A668
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x0005C470 File Offset: 0x0005A670
		[DataMember]
		public int FindMeSecondNumberDuration { get; set; }

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0005C479 File Offset: 0x0005A679
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x0005C481 File Offset: 0x0005A681
		[DataMember]
		public int Key { get; set; }

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0005C48A File Offset: 0x0005A68A
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x0005C492 File Offset: 0x0005A692
		[DataMember]
		public string KeyMappingType { get; set; }

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0005C49B File Offset: 0x0005A69B
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0005C4A3 File Offset: 0x0005A6A3
		[DataMember]
		public string TransferToGALContactLegacyDN { get; set; }

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0005C4AC File Offset: 0x0005A6AC
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0005C4B4 File Offset: 0x0005A6B4
		[DataMember]
		public string TransferToNumber { get; set; }

		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0005C4BD File Offset: 0x0005A6BD
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x0005C4C5 File Offset: 0x0005A6C5
		[DataMember]
		public string TransferToGALContactDisplayName { get; set; }

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0005C4D0 File Offset: 0x0005A6D0
		public KeyMapping ToTaskObject()
		{
			string transferToGALContactLegacyDN = null;
			if (!string.IsNullOrEmpty(this.TransferToGALContactLegacyDN))
			{
				Guid guid;
				if (Guid.TryParse(this.TransferToGALContactLegacyDN, out guid))
				{
					ADObjectId adobjectId = ADObjectId.ParseDnOrGuid(this.TransferToGALContactLegacyDN);
					if (adobjectId == null)
					{
						goto IL_85;
					}
					IEnumerable<PeopleRecipientObject> enumerable = RecipientObjectResolver.Instance.ResolvePeople(new ADObjectId[]
					{
						adobjectId
					});
					if (enumerable == null)
					{
						goto IL_85;
					}
					using (IEnumerator<PeopleRecipientObject> enumerator = enumerable.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							PeopleRecipientObject peopleRecipientObject = enumerator.Current;
							transferToGALContactLegacyDN = peopleRecipientObject.LegacyExchangeDN;
						}
						goto IL_85;
					}
				}
				transferToGALContactLegacyDN = this.TransferToGALContactLegacyDN;
			}
			IL_85:
			return new KeyMapping((KeyMappingType)Enum.Parse(typeof(KeyMappingType), this.KeyMappingType), this.Key, this.Context, this.FindMeFirstNumber, this.FindMeFirstNumberDuration, this.FindMeSecondNumber, this.FindMeSecondNumberDuration, this.TransferToNumber, transferToGALContactLegacyDN);
		}
	}
}
