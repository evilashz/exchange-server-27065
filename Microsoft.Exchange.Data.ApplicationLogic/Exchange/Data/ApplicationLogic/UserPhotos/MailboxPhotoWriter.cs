using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.ApplicationLogic.UserPhotos
{
	// Token: 0x020001EC RID: 492
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxPhotoWriter : IMailboxPhotoWriter
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0004C5BF File Offset: 0x0004A7BF
		public MailboxPhotoWriter(IMailboxSession session, ITracer upstreamTracer) : this(session, new XSOFactory(), upstreamTracer)
		{
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0004C5D0 File Offset: 0x0004A7D0
		internal MailboxPhotoWriter(IMailboxSession session, IXSOFactory xsoFactory, ITracer upstreamTracer)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (xsoFactory == null)
			{
				throw new ArgumentNullException("xsoFactory");
			}
			if (upstreamTracer == null)
			{
				throw new ArgumentNullException("upstreamTracer");
			}
			this.tracer = ExTraceGlobals.UserPhotosTracer.Compose(upstreamTracer);
			this.upstreamTracer = upstreamTracer;
			this.session = session;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0004C63E File Offset: 0x0004A83E
		public void UploadPreview(int thumbprint, IDictionary<UserPhotoSize, byte[]> photos)
		{
			if (photos == null || photos.Count == 0)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Mailbox photo writer: uploaded photo is empty.");
				return;
			}
			this.DeleteAllPreviewItems();
			this.CreatePreviewItem(photos);
			this.StorePreviewThumbprint(thumbprint);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0004C678 File Offset: 0x0004A878
		public void Save()
		{
			VersionedId previewId = this.FindPreviewItem();
			int previewThumbprint = this.ReadPreviewThumbprint();
			this.DeleteAllActualPhotoItems();
			this.PromotePreviewToActualPhoto(previewId, previewThumbprint);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0004C6A1 File Offset: 0x0004A8A1
		public void Clear()
		{
			this.DeleteAllActualPhotoItems();
			this.StoreThumbprint(MailboxPhotoWriter.UserPhotoCacheIdProperty, 0);
			this.EnsureDeletedNotificationItem();
			this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: photo has been cleared.");
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0004C6D2 File Offset: 0x0004A8D2
		public void ClearPreview()
		{
			this.DeleteAllPreviewItems();
			this.DeletePropertyFromMailboxTableAndSaveChanges(MailboxPhotoWriter.UserPhotoPreviewCacheIdProperty);
			this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: PREVIEW photo has been cleared.");
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0004C6FC File Offset: 0x0004A8FC
		private void PromotePreviewToActualPhoto(VersionedId previewId, int previewThumbprint)
		{
			this.tracer.TraceDebug<VersionedId, int>((long)this.GetHashCode(), "Mailbox photo writer: promoting preview photo to actual photo.  Preview photo has id = {0} and thumbprint = {1:X8}", previewId, previewThumbprint);
			using (Item item = Item.Bind((MailboxSession)this.session, previewId, MailboxPhotoWriter.AllPhotoSizeProperties))
			{
				using (IItem item2 = Item.CloneItem((MailboxSession)this.session, this.GetPhotoStoreId(), item, false, false, MailboxPhotoWriter.AllPhotoSizeProperties))
				{
					item2.ClassName = "IPM.UserPhoto";
					item2.Save(SaveMode.ResolveConflicts);
					this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: preview photo item has been promoted to actual photo item.");
				}
			}
			this.session.Delete(DeleteItemFlags.HardDelete, new StoreId[]
			{
				previewId
			});
			this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: deleted preview item.");
			this.StoreThumbprint(MailboxPhotoWriter.UserPhotoCacheIdProperty, previewThumbprint);
			this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: thumbprint of preview photo has been promoted to thumbprint of actual photo.");
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0004C824 File Offset: 0x0004AA24
		private VersionedId FindPreviewItem()
		{
			List<VersionedId> list = (from photo in new UserPhotoEnumerator(this.session, this.GetPhotoStoreId(), "IPM.UserPhoto.Preview", this.xsoFactory, this.upstreamTracer)
			select photo.GetValueOrDefault<VersionedId>(ItemSchema.Id, null) into id
			where id != null
			select id).ToList<VersionedId>();
			if (list.Count == 0)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Mailbox photo writer: no preview photo item.");
				throw new ObjectNotFoundException(Strings.UserPhotoNotFound);
			}
			if (list.Count > 1)
			{
				this.tracer.TraceError((long)this.GetHashCode(), "Mailbox photo writer: too many preview photo items.");
				throw new ObjectNotFoundException(Strings.UserPhotoTooManyItems("IPM.UserPhoto.Preview"));
			}
			return list[0];
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0004C8FE File Offset: 0x0004AAFE
		private void DeleteAllActualPhotoItems()
		{
			this.DeleteAllPhotoItems("IPM.UserPhoto");
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0004C90B File Offset: 0x0004AB0B
		private void DeleteAllPreviewItems()
		{
			this.DeleteAllPhotoItems("IPM.UserPhoto.Preview");
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0004C918 File Offset: 0x0004AB18
		private void EnsureDeletedNotificationItem()
		{
			this.DeleteAllPhotoItems("IPM.UserPhoto.DeletedNotification");
			this.CreateDeletedNotificationItem();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0004C944 File Offset: 0x0004AB44
		private void DeleteAllPhotoItems(string itemClass)
		{
			VersionedId[] array = (from photo in new UserPhotoEnumerator(this.session, this.GetPhotoStoreId(), itemClass, this.xsoFactory, this.upstreamTracer)
			select photo.GetValueOrDefault<VersionedId>(ItemSchema.Id, null) into id
			where id != null
			select id).ToArray<VersionedId>();
			if (array.Length == 0)
			{
				return;
			}
			this.session.Delete(DeleteItemFlags.HardDelete, array);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0004C9D0 File Offset: 0x0004ABD0
		private void CreateDeletedNotificationItem()
		{
			using (IItem item = Item.Create((MailboxSession)this.session, "IPM.UserPhoto.DeletedNotification", this.GetPhotoStoreId()))
			{
				item.Save(SaveMode.ResolveConflicts);
				this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: dummy deleted notification item created successfully.");
			}
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0004CA34 File Offset: 0x0004AC34
		private void CreatePreviewItem(IDictionary<UserPhotoSize, byte[]> photos)
		{
			using (IItem item = Item.Create((MailboxSession)this.session, "IPM.UserPhoto.Preview", this.GetPhotoStoreId()))
			{
				foreach (UserPhotoSize userPhotoSize in MailboxPhotoWriter.AllPhotoSizes)
				{
					byte[] buffer;
					if (!photos.TryGetValue(userPhotoSize, out buffer))
					{
						this.tracer.TraceDebug<UserPhotoSize>((long)this.GetHashCode(), "Mailbox photo writer: photo of size {0} not available in preview (input).  Skipped.", userPhotoSize);
					}
					else
					{
						this.tracer.TraceDebug<UserPhotoSize>((long)this.GetHashCode(), "Mailbox photo writer: storing photo of size {0} onto preview item.", userPhotoSize);
						using (MemoryStream memoryStream = new MemoryStream(buffer))
						{
							this.StorePhotoOfSpecificSize(item, userPhotoSize, MailboxPhotoReader.GetPropertyDefinitionForSize(userPhotoSize), memoryStream);
						}
					}
				}
				item.Save(SaveMode.ResolveConflicts);
				this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: preview item created successfully.");
			}
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0004CB24 File Offset: 0x0004AD24
		private void StorePhotoOfSpecificSize(IItem photoItem, UserPhotoSize size, PropertyDefinition specificSizeProperty, MemoryStream photo)
		{
			if (photo == null || photo.Length == 0L)
			{
				this.tracer.TraceDebug<UserPhotoSize>((long)this.GetHashCode(), "Mailbox photo writer: photo of size {0} not available and will not be stored onto preview item.", size);
				return;
			}
			using (Stream stream = photoItem.OpenPropertyStream(specificSizeProperty, PropertyOpenMode.Create))
			{
				photo.Seek(0L, SeekOrigin.Begin);
				photo.CopyTo(stream);
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0004CB94 File Offset: 0x0004AD94
		private void StorePreviewThumbprint(int thumbprint)
		{
			this.StoreThumbprint(MailboxPhotoWriter.UserPhotoPreviewCacheIdProperty, thumbprint);
			this.tracer.TraceDebug<int>((long)this.GetHashCode(), "Mailbox photo writer: stored preview thumbprint = {0:X8}", thumbprint);
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0004CBBC File Offset: 0x0004ADBC
		private int ReadPreviewThumbprint()
		{
			Mailbox mailbox = ((MailboxSession)this.session).Mailbox;
			object obj = mailbox.TryGetProperty(MailboxPhotoWriter.UserPhotoPreviewCacheIdProperty);
			if (obj is int)
			{
				this.tracer.TraceDebug((long)this.GetHashCode(), "Mailbox photo writer: read thumbprint of preview photo: {0:X8}", new object[]
				{
					obj
				});
				return (int)obj;
			}
			this.tracer.TraceError((long)this.GetHashCode(), "Mailbox photo reader: thumbprint of preview photo doesn't exist.");
			throw new ObjectNotFoundException(Strings.UserPhotoThumbprintNotFound(true));
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0004CC3A File Offset: 0x0004AE3A
		private StoreObjectId GetPhotoStoreId()
		{
			return this.session.GetDefaultFolderId(DefaultFolderType.Configuration);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0004CC4C File Offset: 0x0004AE4C
		private void DeletePropertyFromMailboxTableAndSaveChanges(PropertyDefinition property)
		{
			Mailbox mailbox = ((MailboxSession)this.session).Mailbox;
			mailbox.Delete(property);
			mailbox.Save();
			mailbox.Load();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004CC80 File Offset: 0x0004AE80
		private void StoreThumbprint(PropertyDefinition thumbprintProperty, int thumbprint)
		{
			Mailbox mailbox = ((MailboxSession)this.session).Mailbox;
			mailbox[thumbprintProperty] = thumbprint;
			mailbox.Save();
			mailbox.Load();
		}

		// Token: 0x040009A1 RID: 2465
		private static readonly PropertyDefinition UserPhotoCacheIdProperty = MailboxSchema.UserPhotoCacheId;

		// Token: 0x040009A2 RID: 2466
		private static readonly PropertyDefinition UserPhotoPreviewCacheIdProperty = MailboxSchema.UserPhotoPreviewCacheId;

		// Token: 0x040009A3 RID: 2467
		private static readonly UserPhotoSize[] AllPhotoSizes = MailboxPhotoReader.AllPhotoSizes;

		// Token: 0x040009A4 RID: 2468
		private static readonly ICollection<PropertyDefinition> AllPhotoSizeProperties = new PropertyDefinition[]
		{
			UserPhotoSchema.UserPhotoHR648x648,
			UserPhotoSchema.UserPhotoHR504x504,
			UserPhotoSchema.UserPhotoHR432x432,
			UserPhotoSchema.UserPhotoHR360x360,
			UserPhotoSchema.UserPhotoHR240x240,
			UserPhotoSchema.UserPhotoHR120x120,
			UserPhotoSchema.UserPhotoHR96x96,
			UserPhotoSchema.UserPhotoHR64x64,
			UserPhotoSchema.UserPhotoHR48x48
		};

		// Token: 0x040009A5 RID: 2469
		private readonly IMailboxSession session;

		// Token: 0x040009A6 RID: 2470
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040009A7 RID: 2471
		private readonly ITracer tracer = ExTraceGlobals.UserPhotosTracer;

		// Token: 0x040009A8 RID: 2472
		private readonly ITracer upstreamTracer;
	}
}
