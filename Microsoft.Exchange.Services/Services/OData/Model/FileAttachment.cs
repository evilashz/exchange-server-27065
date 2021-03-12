using System;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E5C RID: 3676
	internal class FileAttachment : Attachment
	{
		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06005F10 RID: 24336 RVA: 0x00128AFC File Offset: 0x00126CFC
		// (set) Token: 0x06005F11 RID: 24337 RVA: 0x00128B0E File Offset: 0x00126D0E
		public string ContentId
		{
			get
			{
				return (string)base[FileAttachmentSchema.ContentId];
			}
			set
			{
				base[FileAttachmentSchema.ContentId] = value;
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06005F12 RID: 24338 RVA: 0x00128B1C File Offset: 0x00126D1C
		// (set) Token: 0x06005F13 RID: 24339 RVA: 0x00128B2E File Offset: 0x00126D2E
		public string ContentLocation
		{
			get
			{
				return (string)base[FileAttachmentSchema.ContentLocation];
			}
			set
			{
				base[FileAttachmentSchema.ContentLocation] = value;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06005F14 RID: 24340 RVA: 0x00128B3C File Offset: 0x00126D3C
		// (set) Token: 0x06005F15 RID: 24341 RVA: 0x00128B4E File Offset: 0x00126D4E
		public bool IsContactPhoto
		{
			get
			{
				return (bool)base[FileAttachmentSchema.IsContactPhoto];
			}
			set
			{
				base[FileAttachmentSchema.IsContactPhoto] = value;
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06005F16 RID: 24342 RVA: 0x00128B61 File Offset: 0x00126D61
		// (set) Token: 0x06005F17 RID: 24343 RVA: 0x00128B73 File Offset: 0x00126D73
		public byte[] ContentBytes
		{
			get
			{
				return (byte[])base[FileAttachmentSchema.ContentBytes];
			}
			set
			{
				base[FileAttachmentSchema.ContentBytes] = value;
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06005F18 RID: 24344 RVA: 0x00128B81 File Offset: 0x00126D81
		internal override EntitySchema Schema
		{
			get
			{
				return FileAttachmentSchema.SchemaInstance;
			}
		}

		// Token: 0x04003387 RID: 13191
		internal new static readonly EdmEntityType EdmEntityType = new EdmEntityType(typeof(FileAttachment).Namespace, typeof(FileAttachment).Name, Attachment.EdmEntityType);
	}
}
