using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Office.Compliance.Audit.Schema;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	public class AuditUploaderConfigSchema : IExtensibleDataObject, IVerifiable
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BE11 File Offset: 0x0000A011
		// (set) Token: 0x060002BF RID: 703 RVA: 0x0000BE19 File Offset: 0x0000A019
		[DataMember(IsRequired = true)]
		public List<ParsingRule> ParsingSection { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000BE22 File Offset: 0x0000A022
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000BE2A File Offset: 0x0000A02A
		[DataMember(IsRequired = true)]
		public List<FilteringRule> FilteringSection { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000BE33 File Offset: 0x0000A033
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x0000BE3B File Offset: 0x0000A03B
		ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

		// Token: 0x060002C4 RID: 708 RVA: 0x0000BE44 File Offset: 0x0000A044
		public virtual void Initialize()
		{
			if (this.ParsingSection != null)
			{
				foreach (ParsingRule parsingRule in this.ParsingSection)
				{
					parsingRule.Initialize();
				}
			}
			if (this.FilteringSection != null)
			{
				foreach (FilteringRule filteringRule in this.FilteringSection)
				{
					filteringRule.Initialize();
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		public virtual void Validate()
		{
			if (this.ParsingSection != null)
			{
				foreach (ParsingRule parsingRule in this.ParsingSection)
				{
					parsingRule.Validate();
				}
			}
			if (this.FilteringSection != null)
			{
				foreach (FilteringRule filteringRule in this.FilteringSection)
				{
					filteringRule.Validate();
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000BF8C File Offset: 0x0000A18C
		[OnDeserializing]
		private void OnDeserializing(StreamingContext context)
		{
			this.Initialize();
		}
	}
}
