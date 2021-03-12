using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E56 RID: 3670
	internal abstract class Item : Entity
	{
		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06005E7E RID: 24190 RVA: 0x00126AED File Offset: 0x00124CED
		// (set) Token: 0x06005E7F RID: 24191 RVA: 0x00126AFF File Offset: 0x00124CFF
		public string ChangeKey
		{
			get
			{
				return (string)base[ItemSchema.ChangeKey];
			}
			set
			{
				base[ItemSchema.ChangeKey] = value;
			}
		}

		// Token: 0x17001580 RID: 5504
		// (get) Token: 0x06005E80 RID: 24192 RVA: 0x00126B0D File Offset: 0x00124D0D
		// (set) Token: 0x06005E81 RID: 24193 RVA: 0x00126B1F File Offset: 0x00124D1F
		public string ClassName
		{
			get
			{
				return (string)base[ItemSchema.ClassName];
			}
			set
			{
				base[ItemSchema.ClassName] = value;
			}
		}

		// Token: 0x17001581 RID: 5505
		// (get) Token: 0x06005E82 RID: 24194 RVA: 0x00126B2D File Offset: 0x00124D2D
		// (set) Token: 0x06005E83 RID: 24195 RVA: 0x00126B3F File Offset: 0x00124D3F
		public string Subject
		{
			get
			{
				return (string)base[ItemSchema.Subject];
			}
			set
			{
				base[ItemSchema.Subject] = value;
			}
		}

		// Token: 0x17001582 RID: 5506
		// (get) Token: 0x06005E84 RID: 24196 RVA: 0x00126B4D File Offset: 0x00124D4D
		// (set) Token: 0x06005E85 RID: 24197 RVA: 0x00126B5F File Offset: 0x00124D5F
		public ItemBody Body
		{
			get
			{
				return (ItemBody)base[ItemSchema.Body];
			}
			set
			{
				base[ItemSchema.Body] = value;
			}
		}

		// Token: 0x17001583 RID: 5507
		// (get) Token: 0x06005E86 RID: 24198 RVA: 0x00126B6D File Offset: 0x00124D6D
		// (set) Token: 0x06005E87 RID: 24199 RVA: 0x00126B7F File Offset: 0x00124D7F
		public string BodyPreview
		{
			get
			{
				return (string)base[ItemSchema.BodyPreview];
			}
			set
			{
				base[ItemSchema.BodyPreview] = value;
			}
		}

		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06005E88 RID: 24200 RVA: 0x00126B8D File Offset: 0x00124D8D
		// (set) Token: 0x06005E89 RID: 24201 RVA: 0x00126B9F File Offset: 0x00124D9F
		public bool HasAttachments
		{
			get
			{
				return (bool)base[ItemSchema.HasAttachments];
			}
			set
			{
				base[ItemSchema.HasAttachments] = value;
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06005E8A RID: 24202 RVA: 0x00126BB2 File Offset: 0x00124DB2
		// (set) Token: 0x06005E8B RID: 24203 RVA: 0x00126BC4 File Offset: 0x00124DC4
		public string[] Categories
		{
			get
			{
				return (string[])base[ItemSchema.Categories];
			}
			set
			{
				base[ItemSchema.Categories] = value;
			}
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06005E8C RID: 24204 RVA: 0x00126BD2 File Offset: 0x00124DD2
		// (set) Token: 0x06005E8D RID: 24205 RVA: 0x00126BE4 File Offset: 0x00124DE4
		public DateTimeOffset DateTimeCreated
		{
			get
			{
				return (DateTimeOffset)base[ItemSchema.DateTimeCreated];
			}
			set
			{
				base[ItemSchema.DateTimeCreated] = value;
			}
		}

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06005E8E RID: 24206 RVA: 0x00126BF7 File Offset: 0x00124DF7
		// (set) Token: 0x06005E8F RID: 24207 RVA: 0x00126C09 File Offset: 0x00124E09
		public DateTimeOffset LastModifiedTime
		{
			get
			{
				return (DateTimeOffset)base[ItemSchema.LastModifiedTime];
			}
			set
			{
				base[ItemSchema.LastModifiedTime] = value;
			}
		}

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06005E90 RID: 24208 RVA: 0x00126C1C File Offset: 0x00124E1C
		// (set) Token: 0x06005E91 RID: 24209 RVA: 0x00126C2E File Offset: 0x00124E2E
		public Importance Importance
		{
			get
			{
				return (Importance)base[ItemSchema.Importance];
			}
			set
			{
				base[ItemSchema.Importance] = value;
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06005E92 RID: 24210 RVA: 0x00126C41 File Offset: 0x00124E41
		// (set) Token: 0x06005E93 RID: 24211 RVA: 0x00126C53 File Offset: 0x00124E53
		public IEnumerable<Attachment> Attachments
		{
			get
			{
				return (IEnumerable<Attachment>)base[ItemSchema.Attachments];
			}
			set
			{
				base[ItemSchema.Attachments] = value;
			}
		}

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06005E94 RID: 24212 RVA: 0x00126C61 File Offset: 0x00124E61
		internal override EntitySchema Schema
		{
			get
			{
				return ItemSchema.SchemaInstance;
			}
		}

		// Token: 0x0400332A RID: 13098
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Item).Namespace, typeof(Item).Name, Entity.EdmEntityType, true, false);
	}
}
