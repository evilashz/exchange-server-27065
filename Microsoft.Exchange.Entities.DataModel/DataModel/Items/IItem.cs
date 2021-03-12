using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x02000044 RID: 68
	public interface IItem : IStorageEntity, IEntity, IPropertyChangeTracker<PropertyDefinition>, IVersioned
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000163 RID: 355
		// (set) Token: 0x06000164 RID: 356
		[NotMapped]
		List<IAttachment> Attachments { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000165 RID: 357
		// (set) Token: 0x06000166 RID: 358
		ItemBody Body { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000167 RID: 359
		// (set) Token: 0x06000168 RID: 360
		List<string> Categories { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000169 RID: 361
		// (set) Token: 0x0600016A RID: 362
		ExDateTime DateTimeCreated { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600016B RID: 363
		// (set) Token: 0x0600016C RID: 364
		bool HasAttachments { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600016D RID: 365
		// (set) Token: 0x0600016E RID: 366
		Importance Importance { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600016F RID: 367
		// (set) Token: 0x06000170 RID: 368
		ExDateTime LastModifiedTime { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000171 RID: 369
		// (set) Token: 0x06000172 RID: 370
		string Preview { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000173 RID: 371
		// (set) Token: 0x06000174 RID: 372
		ExDateTime ReceivedTime { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000175 RID: 373
		// (set) Token: 0x06000176 RID: 374
		Sensitivity Sensitivity { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000177 RID: 375
		// (set) Token: 0x06000178 RID: 376
		string Subject { get; set; }
	}
}
