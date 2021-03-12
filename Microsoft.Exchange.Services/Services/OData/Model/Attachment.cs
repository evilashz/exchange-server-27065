using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E5A RID: 3674
	internal abstract class Attachment : Entity
	{
		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x001284DE File Offset: 0x001266DE
		// (set) Token: 0x06005EED RID: 24301 RVA: 0x001284F0 File Offset: 0x001266F0
		public string Name
		{
			get
			{
				return (string)base[AttachmentSchema.Name];
			}
			set
			{
				base[AttachmentSchema.Name] = value;
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x001284FE File Offset: 0x001266FE
		// (set) Token: 0x06005EEF RID: 24303 RVA: 0x00128510 File Offset: 0x00126710
		public string ContentType
		{
			get
			{
				return (string)base[AttachmentSchema.ContentType];
			}
			set
			{
				base[AttachmentSchema.ContentType] = value;
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x0012851E File Offset: 0x0012671E
		// (set) Token: 0x06005EF1 RID: 24305 RVA: 0x00128530 File Offset: 0x00126730
		public int Size
		{
			get
			{
				return (int)base[AttachmentSchema.Size];
			}
			set
			{
				base[AttachmentSchema.Size] = value;
			}
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06005EF2 RID: 24306 RVA: 0x00128543 File Offset: 0x00126743
		// (set) Token: 0x06005EF3 RID: 24307 RVA: 0x00128555 File Offset: 0x00126755
		public DateTimeOffset LastModifiedTime
		{
			get
			{
				return (DateTimeOffset)base[AttachmentSchema.LastModifiedTime];
			}
			set
			{
				base[AttachmentSchema.LastModifiedTime] = value;
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06005EF4 RID: 24308 RVA: 0x00128568 File Offset: 0x00126768
		// (set) Token: 0x06005EF5 RID: 24309 RVA: 0x0012857A File Offset: 0x0012677A
		public bool IsInline
		{
			get
			{
				return (bool)base[AttachmentSchema.IsInline];
			}
			set
			{
				base[AttachmentSchema.IsInline] = value;
			}
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06005EF6 RID: 24310 RVA: 0x0012858D File Offset: 0x0012678D
		internal override EntitySchema Schema
		{
			get
			{
				return AttachmentSchema.SchemaInstance;
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06005EF7 RID: 24311 RVA: 0x00128594 File Offset: 0x00126794
		// (set) Token: 0x06005EF8 RID: 24312 RVA: 0x0012859C File Offset: 0x0012679C
		internal string ParentItemNavigationName { get; set; }

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x001285A5 File Offset: 0x001267A5
		// (set) Token: 0x06005EFA RID: 24314 RVA: 0x001285AD File Offset: 0x001267AD
		internal string ParentItemId { get; set; }

		// Token: 0x06005EFB RID: 24315 RVA: 0x001285B8 File Offset: 0x001267B8
		internal override Uri GetWebUri(ODataContext odataContext)
		{
			ArgumentValidator.ThrowIfNull("odataContext", odataContext);
			string uriString = string.Format("{0}Users('{1}')/{2}('{3}')/Attachments('{4}')", new object[]
			{
				odataContext.HttpContext.GetServiceRootUri(),
				odataContext.TargetMailbox.PrimarySmtpAddress,
				this.ParentItemNavigationName,
				this.ParentItemId,
				base.Id
			});
			return new Uri(uriString);
		}

		// Token: 0x04003370 RID: 13168
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(Attachment).Namespace, typeof(Attachment).Name, Entity.EdmEntityType, true, false);
	}
}
