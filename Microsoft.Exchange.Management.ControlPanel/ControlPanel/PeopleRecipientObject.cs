using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000348 RID: 840
	public class PeopleRecipientObject : RecipientObjectResolverRow
	{
		// Token: 0x06002F5F RID: 12127 RVA: 0x00090871 File Offset: 0x0008EA71
		public PeopleRecipientObject(ADRawEntry entry) : base(entry)
		{
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x06002F60 RID: 12128 RVA: 0x0009087A File Offset: 0x0008EA7A
		public string LegacyExchangeDN
		{
			get
			{
				return (string)base.ADRawEntry[ADRecipientSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x06002F61 RID: 12129 RVA: 0x00090891 File Offset: 0x0008EA91
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)base.ADRawEntry[ADRecipientSchema.RecipientType];
			}
		}

		// Token: 0x04002302 RID: 8962
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(RecipientObjectResolverRow.Properties)
		{
			ADRecipientSchema.LegacyExchangeDN,
			ADRecipientSchema.RecipientType
		}.ToArray();
	}
}
