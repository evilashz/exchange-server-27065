using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200035A RID: 858
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReferenceAttachment : StreamAttachmentBase, IReferenceAttachment, IAttachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06002635 RID: 9781 RVA: 0x0009900C File Offset: 0x0009720C
		internal ReferenceAttachment(CoreAttachment coreAttachment) : base(coreAttachment)
		{
		}

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x00099015 File Offset: 0x00097215
		public override AttachmentType AttachmentType
		{
			get
			{
				base.CheckDisposed("AttachmentType::get");
				return AttachmentType.Reference;
			}
		}

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00099023 File Offset: 0x00097223
		// (set) Token: 0x06002638 RID: 9784 RVA: 0x0009903C File Offset: 0x0009723C
		public string AttachLongPathName
		{
			get
			{
				base.CheckDisposed("AttachLongPathName::get");
				return base.GetValueOrDefault<string>(InternalSchema.AttachLongPathName, null);
			}
			set
			{
				base.CheckDisposed("AttachLongPathName::set");
				base[InternalSchema.AttachLongPathName] = value;
			}
		}

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00099055 File Offset: 0x00097255
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x0009906E File Offset: 0x0009726E
		public string ProviderEndpointUrl
		{
			get
			{
				base.CheckDisposed("ProviderEndpointUrl::get");
				return base.GetValueOrDefault<string>(InternalSchema.AttachmentProviderEndpointUrl, null);
			}
			set
			{
				base.CheckDisposed("ProviderEndpointUrl::set");
				base[InternalSchema.AttachmentProviderEndpointUrl] = value;
			}
		}

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00099087 File Offset: 0x00097287
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x000990A0 File Offset: 0x000972A0
		public string ProviderType
		{
			get
			{
				base.CheckDisposed("ProviderType::get");
				return base.GetValueOrDefault<string>(InternalSchema.AttachmentProviderType, null);
			}
			set
			{
				base.CheckDisposed("ProviderType::set");
				base[InternalSchema.AttachmentProviderType] = value;
			}
		}

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600263D RID: 9789 RVA: 0x000990B9 File Offset: 0x000972B9
		protected override Schema Schema
		{
			get
			{
				return ReferenceAttachmentSchema.Instance;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000990C0 File Offset: 0x000972C0
		protected override PropertyTagPropertyDefinition ContentStreamProperty
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000990C4 File Offset: 0x000972C4
		internal static void CoreObjectUpdateReferenceAttachmentName(CoreAttachment coreAttachment)
		{
			ICorePropertyBag propertyBag = coreAttachment.PropertyBag;
			string text = propertyBag.TryGetProperty(InternalSchema.DisplayName) as string;
			string text2 = propertyBag.TryGetProperty(InternalSchema.AttachExtension) as string;
			if (!string.IsNullOrEmpty(text))
			{
				text = Attachment.TrimFilename(text);
				propertyBag[InternalSchema.DisplayName] = text;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 = '.' + Attachment.TrimFilename(text2);
				propertyBag[InternalSchema.AttachExtension] = (text2 ?? string.Empty);
			}
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x00099145 File Offset: 0x00097345
		public override Stream GetContentStream()
		{
			base.CheckDisposed("GetContentStream");
			return new MemoryStream();
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x00099157 File Offset: 0x00097357
		public override Stream GetContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("GetContentStream");
			return new MemoryStream();
		}

		// Token: 0x06002642 RID: 9794 RVA: 0x00099169 File Offset: 0x00097369
		public override Stream TryGetContentStream(PropertyOpenMode openMode)
		{
			base.CheckDisposed("TryGetContentStream");
			return new MemoryStream();
		}

		// Token: 0x06002643 RID: 9795 RVA: 0x0009917B File Offset: 0x0009737B
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ReferenceAttachment>(this);
		}

		// Token: 0x06002644 RID: 9796 RVA: 0x00099183 File Offset: 0x00097383
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			this.OnBeforeSaveUpdateAttachMethod();
			this.OnBeforeSaveUpdateExtraProperties();
		}

		// Token: 0x06002645 RID: 9797 RVA: 0x00099198 File Offset: 0x00097398
		protected override void OnBeforeSaveUpdateAttachSize()
		{
			if (base.MapiAttach != null)
			{
				return;
			}
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.AttachSize
			});
			base[InternalSchema.AttachSize] = 0;
		}

		// Token: 0x06002646 RID: 9798 RVA: 0x000991D8 File Offset: 0x000973D8
		private void OnBeforeSaveUpdateAttachMethod()
		{
			object obj = base.PropertyBag.TryGetProperty(InternalSchema.AttachMethod);
			if (obj is PropertyError)
			{
				obj = 7;
			}
			else
			{
				int num = (int)obj;
				if (num != 3 && num != 2 && num != 4)
				{
					num = 7;
				}
				obj = num;
			}
			base.PropertyBag[InternalSchema.AttachMethod] = obj;
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x00099234 File Offset: 0x00097434
		private void OnBeforeSaveUpdateExtraProperties()
		{
			base.PropertyBag[InternalSchema.AttachCalendarHidden] = true;
		}

		// Token: 0x040016ED RID: 5869
		internal const int AttachMethod = 7;
	}
}
