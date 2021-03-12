using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000481 RID: 1153
	[OwaEventNamespace("Attachments")]
	internal sealed class AttachmentEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002C57 RID: 11351 RVA: 0x000F6C88 File Offset: 0x000F4E88
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(AttachmentEventHandler));
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000F6C9C File Offset: 0x000F4E9C
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("FId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("IT", typeof(StoreObjectType))]
		[OwaEvent("CreateImplicitDraftItem")]
		public void CreateImplicitDraftItem()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "AttachmentEventHandler.CreateImplicitDraftItem");
			StoreObjectType itemType = (StoreObjectType)base.GetParameter("IT");
			OwaStoreObjectId folderId = null;
			if (base.IsParameterSet("FId"))
			{
				folderId = (OwaStoreObjectId)base.GetParameter("FId");
			}
			Item item = this.CreateImplicitDraftItemHelper(itemType, folderId);
			base.WriteNewItemId(item);
			base.WriteChangeKey(item);
			item.Dispose();
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000F6D10 File Offset: 0x000F4F10
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("CleanupUnlinkedAttachments")]
		[OwaEventParameter("BodyMarkup", typeof(string))]
		public void CleanupUnlinkedAttachments()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "AttachmentEventHandler.CleanupUnlinkedAttachments");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string changeKey = (string)base.GetParameter("CK");
			string bodyMarkup = (string)base.GetParameter("BodyMarkup");
			using (Item item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, changeKey, new PropertyDefinition[0]))
			{
				if (base.UserContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(item, base.UserContext, true);
				}
				item.OpenAsReadWrite();
				AttachmentUtility.RemoveUnlinkedInlineAttachments(item, bodyMarkup);
				ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new OwaEventHandlerException("Could not save item due to conflict resolution failure", LocalizedStrings.GetNonEncoded(-482397486), OwaEventHandlerErrorCode.ConflictResolution);
				}
				item.Load();
				this.RenderTotalAttachmentSize(item);
				base.WriteChangeKey(item);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000F6DFC File Offset: 0x000F4FFC
		[OwaEvent("RefreshWell")]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		public void RefreshWell()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "AttachmentEventHandler.RefreshWell");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			this.RenderAttachments(owaStoreObjectId);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000F6E38 File Offset: 0x000F5038
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("Delete")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId))]
		[OwaEventParameter("CK", typeof(string))]
		[OwaEventParameter("AttId", typeof(string))]
		public void Delete()
		{
			Item item = null;
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "AttachmentEventHandler.Delete");
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string changeKey = (string)base.GetParameter("CK");
			try
			{
				item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, changeKey, new PropertyDefinition[0]);
				if (base.UserContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(item, base.UserContext, true);
				}
				AttachmentId attachmentId = item.CreateAttachmentId((string)base.GetParameter("AttId"));
				AttachmentUtility.RemoveAttachment(item, attachmentId);
				ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new OwaEventHandlerException("Could not save item due to conflict resolution failure", LocalizedStrings.GetNonEncoded(-482397486), OwaEventHandlerErrorCode.ConflictResolution);
				}
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsUpdated.Increment();
				}
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
					item = null;
					this.RenderAttachments(owaStoreObjectId);
				}
			}
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000F6F2C File Offset: 0x000F512C
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEvent("RenderImage")]
		[OwaEventParameter("AttId", typeof(string), false, true)]
		[OwaEventParameter("em", typeof(string), false, true)]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void RenderImage()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Attachments.RenderImage");
			OwaStoreObjectId storeId = null;
			string attId = string.Empty;
			string email = string.Empty;
			if (base.IsParameterSet("Id"))
			{
				storeId = (OwaStoreObjectId)base.GetParameter("Id");
			}
			if (base.IsParameterSet("AttId"))
			{
				attId = (string)base.GetParameter("AttId");
			}
			if (base.IsParameterSet("em"))
			{
				email = (string)base.GetParameter("em");
			}
			string empty = string.Empty;
			using (Stream contactPictureStream = this.GetContactPictureStream(storeId, attId, email, out empty))
			{
				this.OutputImage(contactPictureStream, empty);
			}
			Utilities.MakePageCacheable(this.HttpContext.Response, new int?(3));
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000F7008 File Offset: 0x000F5208
		[OwaEvent("ClearCachePic")]
		public void ClearCachedPicture()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Attachments.ClearCachedPicture");
			base.UserContext.UploadedDisplayPicture = null;
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000F702C File Offset: 0x000F522C
		[OwaEventParameter("FC", typeof(string))]
		[OwaEventParameter("Dpc", typeof(string))]
		[OwaEventParameter("em", typeof(string))]
		[OwaEventParameter("rt", typeof(string))]
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("RenderADPhoto")]
		public void RenderADPhoto()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Attachments.RenderADPhoto");
			bool flag = base.IsParameterSet("FC") && !string.IsNullOrEmpty((string)base.GetParameter("FC"));
			string email = string.Empty;
			string routingType = string.Empty;
			if (!flag)
			{
				if (base.IsParameterSet("em"))
				{
					email = (string)base.GetParameter("em");
				}
				if (base.IsParameterSet("rt"))
				{
					routingType = (string)base.GetParameter("rt");
				}
			}
			using (Stream pictureStream = this.GetPictureStream(flag, email, routingType))
			{
				this.OutputImage(pictureStream, "image/jpeg");
			}
			if (!flag)
			{
				Utilities.MakePageCacheable(this.HttpContext.Response, new int?(3));
				return;
			}
			Utilities.MakePageNoCacheNoStore(this.HttpContext.Response);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000F7120 File Offset: 0x000F5320
		private static string GetContentType(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return string.Empty;
			}
			string extension = Path.GetExtension(path);
			if (string.Equals(extension, ".bmp", StringComparison.OrdinalIgnoreCase))
			{
				return "image/bmp";
			}
			if (string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase))
			{
				return "image/gif";
			}
			if (string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase))
			{
				return "image/jpeg";
			}
			if (string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase))
			{
				return "image/png";
			}
			return string.Empty;
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000F7198 File Offset: 0x000F5398
		private Stream GetContactPictureStream(OwaStoreObjectId storeId, string attId, string email, out string contentType)
		{
			contentType = string.Empty;
			if (storeId != null)
			{
				using (Item item = Utilities.GetItem<Item>(base.UserContext, storeId, new PropertyDefinition[0]))
				{
					return this.GetContactPictureStream(item, attId, out contentType);
				}
			}
			using (ContactsFolder contactsFolder = ContactsFolder.Bind(base.UserContext.MailboxSession, DefaultFolderType.Contacts))
			{
				using (FindInfo<Contact> findInfo = contactsFolder.FindByEmailAddress(email, new PropertyDefinition[0]))
				{
					if (findInfo.FindStatus == FindStatus.Found)
					{
						return this.GetContactPictureStream(findInfo.Result, attId, out contentType);
					}
				}
			}
			return new MemoryStream();
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000F7264 File Offset: 0x000F5464
		private Stream GetContactPictureStream(Item item, string attId, out string contentType)
		{
			contentType = string.Empty;
			if (item == null)
			{
				return new MemoryStream();
			}
			if (string.IsNullOrEmpty(attId))
			{
				attId = RenderingUtilities.GetContactPictureAttachmentId(item);
			}
			if (string.IsNullOrEmpty(attId))
			{
				return new MemoryStream();
			}
			AttachmentId id = item.CreateAttachmentId(attId);
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, true, base.UserContext);
			Stream result;
			using (StreamAttachment streamAttachment = attachmentCollection.Open(id) as StreamAttachment)
			{
				if (streamAttachment == null)
				{
					throw new OwaInvalidRequestException("Attachment is not a stream attachment");
				}
				AttachmentPolicy.Level attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(streamAttachment, base.UserContext);
				if (attachmentLevel == AttachmentPolicy.Level.Block)
				{
					result = new MemoryStream();
				}
				else
				{
					contentType = AttachmentEventHandler.GetContentType(streamAttachment.FileName);
					if (contentType.Length == 0)
					{
						ExTraceGlobals.ContactsTracer.TraceDebug<string>((long)this.GetHashCode(), "Cannot determine image type for file: {0}", streamAttachment.FileName);
						result = new MemoryStream();
					}
					else
					{
						result = streamAttachment.GetContentStream();
					}
				}
			}
			return result;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000F734C File Offset: 0x000F554C
		private Stream GetPictureStream(bool fromCache, string email, string routingType)
		{
			Stream result;
			if (fromCache)
			{
				byte[] uploadedDisplayPicture = base.UserContext.UploadedDisplayPicture;
				if (uploadedDisplayPicture == null)
				{
					throw new OwaInvalidRequestException("Object not found");
				}
				result = new MemoryStream(uploadedDisplayPicture);
			}
			else
			{
				result = this.GetADPictureStream(email, routingType);
			}
			return result;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000F738C File Offset: 0x000F558C
		private Stream GetADPictureStream(string email, string routingType)
		{
			IRecipientSession recipientSession = Utilities.CreateADRecipientSession(ConsistencyMode.IgnoreInvalid, base.UserContext);
			byte[] array = null;
			bool flag = string.Equals(email, base.UserContext.ExchangePrincipal.LegacyDn, StringComparison.OrdinalIgnoreCase);
			string stringHash = Utilities.GetStringHash(email);
			bool flag2 = DisplayPictureUtility.IsInRecipientsNegativeCache(stringHash);
			if (!flag2 || flag)
			{
				ProxyAddress proxyAddress = null;
				try
				{
					if (string.Equals(routingType, "EX", StringComparison.Ordinal))
					{
						proxyAddress = new CustomProxyAddress((CustomProxyAddressPrefix)ProxyAddressPrefix.LegacyDN, email, true);
					}
					else if (string.Equals(routingType, "SMTP", StringComparison.Ordinal))
					{
						proxyAddress = new SmtpProxyAddress(email, true);
					}
					if (proxyAddress != null)
					{
						if (Globals.ArePerfCountersEnabled)
						{
							OwaSingleCounters.SenderPhotosTotalLDAPCalls.Increment();
						}
						ADRawEntry adrawEntry = recipientSession.FindByProxyAddress(proxyAddress, new PropertyDefinition[]
						{
							ADRecipientSchema.ThumbnailPhoto
						});
						if (adrawEntry != null)
						{
							array = (adrawEntry[ADRecipientSchema.ThumbnailPhoto] as byte[]);
						}
					}
					goto IL_10F;
				}
				catch (NonUniqueRecipientException ex)
				{
					ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "GetADPictureStream: NonUniqueRecipientException was thrown by FindByProxyAddress: {0}", ex.Message);
					throw new OwaEventHandlerException("Unable to retrieve recipient data.", LocalizedStrings.GetNonEncoded(-1953304495));
				}
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.SenderPhotosDataFromNegativeCacheCount.Increment();
			}
			IL_10F:
			bool flag3 = array != null && array.Length > 0;
			if (flag)
			{
				base.UserContext.HasPicture = new bool?(flag3);
			}
			if (flag3)
			{
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.SenderPhotosTotalLDAPCallsWithPicture.Increment();
				}
				if (flag2)
				{
					DisplayPictureUtility.RecipientsNegativeCache.Remove(stringHash);
				}
				return new MemoryStream(array);
			}
			if (!flag2)
			{
				int num = DisplayPictureUtility.RecipientsNegativeCache.AddAndCount(stringHash, DateTime.UtcNow);
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.SenderPhotosNegativeCacheCount.RawValue = (long)num;
				}
			}
			return new MemoryStream();
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000F7538 File Offset: 0x000F5738
		private Item CreateImplicitDraftItemHelper(StoreObjectType itemType, OwaStoreObjectId folderId)
		{
			if (itemType < StoreObjectType.Message)
			{
				throw new OwaInvalidRequestException("Item type provided is for a folder type.");
			}
			if (folderId == null && itemType == StoreObjectType.Post)
			{
				folderId = OwaStoreObjectId.CreateFromMailboxFolderId(base.UserContext.TryGetMyDefaultFolderId(DefaultFolderType.Drafts));
			}
			Item item = Utilities.CreateImplicitDraftItem(itemType, folderId);
			item.Save(SaveMode.ResolveConflicts);
			item.Load();
			return item;
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000F758C File Offset: 0x000F578C
		private void RenderTotalAttachmentSize(Item item)
		{
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, base.UserContext);
			int totalAttachmentSize = AttachmentUtility.GetTotalAttachmentSize(attachmentCollection);
			this.SanitizingWriter.Write("<div id=attSz>");
			this.SanitizingWriter.Write(totalAttachmentSize);
			this.SanitizingWriter.Write("</div>");
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000F75DC File Offset: 0x000F57DC
		private void RenderAttachments(OwaStoreObjectId owaStoreObjectId)
		{
			Item item = null;
			try
			{
				item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, new PropertyDefinition[0]);
				if (base.UserContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(item, base.UserContext, true);
				}
				this.Writer.Write("<div id=attWD>");
				ArrayList attachmentInformation = AttachmentWell.GetAttachmentInformation(item, null, base.UserContext.IsPublicLogon);
				AttachmentWell.RenderAttachments(this.Writer, AttachmentWellType.ReadWrite, attachmentInformation, base.UserContext);
				this.Writer.Write("</div>");
				this.Writer.Write("<div id=iBData>");
				AttachmentWell.RenderInfobar(this.Writer, attachmentInformation, base.UserContext);
				this.Writer.Write("</div>");
				base.WriteChangeKey(item);
			}
			finally
			{
				if (item != null)
				{
					item.Dispose();
					item = null;
				}
			}
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000F76B8 File Offset: 0x000F58B8
		private void OutputImage(Stream inputStream, string contentType)
		{
			HttpWriter httpWriter = this.Writer as HttpWriter;
			Stream outputStream = httpWriter.OutputStream;
			this.HttpContext.Response.ContentType = contentType;
			byte[] array = new byte[32768];
			int count;
			while ((count = inputStream.Read(array, 0, array.Length)) > 0)
			{
				outputStream.Write(array, 0, count);
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000F7710 File Offset: 0x000F5910
		[OwaEventParameter("CK", typeof(string), false, true)]
		[OwaEventParameter("ExIds", typeof(OwaStoreObjectId), true, true)]
		[OwaEvent("AttachItems")]
		[OwaEventParameter("Id", typeof(OwaStoreObjectId), false, true)]
		[OwaEventParameter("IT", typeof(StoreObjectType))]
		public void AttachItems()
		{
			OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)base.GetParameter("Id");
			string text = (string)base.GetParameter("CK");
			bool flag = false;
			Item item;
			if (!string.IsNullOrEmpty(text) && owaStoreObjectId != null)
			{
				item = Utilities.GetItem<Item>(base.UserContext, owaStoreObjectId, text, new PropertyDefinition[0]);
				if (base.UserContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(item, base.UserContext, true);
				}
			}
			else
			{
				flag = true;
				StoreObjectType itemType = (StoreObjectType)base.GetParameter("IT");
				item = this.CreateImplicitDraftItemHelper(itemType, null);
			}
			List<OwaStoreObjectId> itemsToAttachIds = this.GetItemsToAttachIds();
			SanitizedHtmlString errorInAttachments = AttachmentUtility.AddExistingItems(item, itemsToAttachIds, base.UserContext);
			item.Load();
			if (base.UserContext.IsIrmEnabled)
			{
				Utilities.IrmDecryptIfRestricted(item, base.UserContext, true);
			}
			ArrayList attachmentInformation = AttachmentWell.GetAttachmentInformation(item, null, base.UserContext.IsPublicLogon);
			RenderingUtilities.RenderAttachmentItems(this.SanitizingWriter, attachmentInformation, base.UserContext);
			this.SanitizingWriter.Write("<div id=attIB>");
			AttachmentWell.RenderInfobar(this.SanitizingWriter, attachmentInformation, errorInAttachments, base.UserContext);
			this.SanitizingWriter.Write("</div>");
			if (flag)
			{
				base.WriteNewItemId(item);
			}
			this.RenderTotalAttachmentSize(item);
			base.WriteChangeKey(item);
			if (item != null)
			{
				item.Dispose();
			}
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000F7858 File Offset: 0x000F5A58
		private List<OwaStoreObjectId> GetItemsToAttachIds()
		{
			OwaStoreObjectId[] array = (OwaStoreObjectId[])base.GetParameter("ExIds");
			if (ConversationUtilities.ContainsConversationItem(base.UserContext, array))
			{
				OwaStoreObjectId localFolderId = (OwaStoreObjectId)base.GetParameter("FId");
				array = ConversationUtilities.GetLocalItemIds(base.UserContext, array, localFolderId);
			}
			if (array != null)
			{
				return new List<OwaStoreObjectId>(array);
			}
			return null;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000F78B2 File Offset: 0x000F5AB2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AttachmentEventHandler>(this);
		}

		// Token: 0x04001D34 RID: 7476
		public const string EventNamespace = "Attachments";

		// Token: 0x04001D35 RID: 7477
		public const string MethodAttachItems = "AttachItems";

		// Token: 0x04001D36 RID: 7478
		public const string MethodCreateImplicitDraftItem = "CreateImplicitDraftItem";

		// Token: 0x04001D37 RID: 7479
		public const string MethodCleanupUnlinkedAttachments = "CleanupUnlinkedAttachments";

		// Token: 0x04001D38 RID: 7480
		public const string MethodRefreshWell = "RefreshWell";

		// Token: 0x04001D39 RID: 7481
		public const string MethodDelete = "Delete";

		// Token: 0x04001D3A RID: 7482
		public const string MethodRenderImage = "RenderImage";

		// Token: 0x04001D3B RID: 7483
		public const string MethodRenderADPhoto = "RenderADPhoto";

		// Token: 0x04001D3C RID: 7484
		public const string MethodClearCachedPicture = "ClearCachePic";

		// Token: 0x04001D3D RID: 7485
		public const string Email = "em";

		// Token: 0x04001D3E RID: 7486
		public const string RoutingType = "rt";

		// Token: 0x04001D3F RID: 7487
		public const string Id = "Id";

		// Token: 0x04001D40 RID: 7488
		public const string Ck = "CK";

		// Token: 0x04001D41 RID: 7489
		public const string ItemType = "IT";

		// Token: 0x04001D42 RID: 7490
		public const string BodyMarkup = "BodyMarkup";

		// Token: 0x04001D43 RID: 7491
		public const string AttId = "AttId";

		// Token: 0x04001D44 RID: 7492
		public const string ExIds = "ExIds";

		// Token: 0x04001D45 RID: 7493
		public const string FolderId = "FId";

		// Token: 0x04001D46 RID: 7494
		public const string DisplayPictureCanary = "Dpc";

		// Token: 0x04001D47 RID: 7495
		public const string FromCache = "FC";
	}
}
