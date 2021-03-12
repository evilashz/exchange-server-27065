using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200050D RID: 1293
	[DataContract]
	public class MailboxPlanResolverRow : AdObjectResolverRow
	{
		// Token: 0x06003E0A RID: 15882 RVA: 0x000BA64E File Offset: 0x000B884E
		public MailboxPlanResolverRow(ADRawEntry aDRawEntry) : base(aDRawEntry)
		{
		}

		// Token: 0x17002459 RID: 9305
		// (get) Token: 0x06003E0B RID: 15883 RVA: 0x000BA658 File Offset: 0x000B8858
		public override string DisplayName
		{
			get
			{
				string text = (string)base.ADRawEntry[ADRecipientSchema.DisplayName];
				if (string.IsNullOrEmpty(text))
				{
					text = base.DisplayName;
				}
				return text;
			}
		}

		// Token: 0x0400284E RID: 10318
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ADRecipientSchema.DisplayName
		}.ToArray();
	}
}
