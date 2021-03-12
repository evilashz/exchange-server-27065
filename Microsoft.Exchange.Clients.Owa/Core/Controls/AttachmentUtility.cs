using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Premium;
using Microsoft.Exchange.Common.Sniff;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Data.TextConverters.Internal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002A9 RID: 681
	internal static class AttachmentUtility
	{
		// Token: 0x06001A29 RID: 6697 RVA: 0x0009756C File Offset: 0x0009576C
		private static uint[] GenerateCrc32Table()
		{
			int num = 256;
			uint[] array = new uint[num];
			for (int i = 0; i < num; i++)
			{
				uint num2 = (uint)i;
				for (int j = 0; j < 8; j++)
				{
					if ((num2 & 1U) != 0U)
					{
						num2 = (3988292384U ^ num2 >> 1);
					}
					else
					{
						num2 >>= 1;
					}
				}
				array[i] = num2;
			}
			return array;
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x000975C0 File Offset: 0x000957C0
		public static AttachmentAddResult AddAttachment(Item item, HttpFileCollection files, UserContext userContext)
		{
			List<SanitizedHtmlString> list;
			return AttachmentUtility.AddAttachment(item, files, userContext, false, null, out list);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x000975DC File Offset: 0x000957DC
		public static AttachmentAddResult AddAttachment(Item item, HttpFileCollection files, UserContext userContext, bool isInlineImage, string bodyMarkup)
		{
			List<SanitizedHtmlString> list;
			return AttachmentUtility.AddAttachment(item, files, userContext, isInlineImage, bodyMarkup, out list);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x000975F8 File Offset: 0x000957F8
		public static AttachmentAddResult AddAttachment(Item item, HttpFileCollection files, UserContext userContext, bool isInlineImage, string bodyMarkup, out List<SanitizedHtmlString> attachmentLinks)
		{
			attachmentLinks = new List<SanitizedHtmlString>(files.Count);
			List<AttachmentId> list = new List<AttachmentId>(files.Count);
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (files == null)
			{
				throw new ArgumentNullException("files");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			Utilities.MakeModifiedCalendarItemOccurrence(item);
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, userContext);
			int num = AttachmentUtility.GetTotalAttachmentSize(attachmentCollection);
			int maximumFileSize = AttachmentUtility.GetMaximumFileSize(userContext);
			int num2 = AttachmentUtility.ToByteSize(maximumFileSize);
			int num3 = 0;
			StringBuilder stringBuilder = null;
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = null;
			bool flag = false;
			for (int i = 0; i < files.Count; i++)
			{
				if (!string.IsNullOrEmpty(files[i].FileName))
				{
					string text = AttachmentUtility.AttachGetFileName(files[i].FileName);
					if (!string.IsNullOrEmpty(text))
					{
						if (num + files[i].ContentLength > num2 && !string.IsNullOrEmpty(bodyMarkup) && !flag)
						{
							AttachmentUtility.RemoveUnlinkedInlineAttachments(item, bodyMarkup);
							num = AttachmentUtility.GetTotalAttachmentSize(attachmentCollection);
							flag = true;
						}
						num += files[i].ContentLength;
						if (num > num2)
						{
							num -= files[i].ContentLength;
							if (stringBuilder == null)
							{
								stringBuilder = new StringBuilder(text);
							}
							else
							{
								stringBuilder.Append(", ");
								stringBuilder.Append(text);
							}
						}
						else if (isInlineImage && !AttachmentUtility.IsSupportedImageContentType(files[i]))
						{
							if (sanitizingStringBuilder == null)
							{
								sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>(text);
							}
							else
							{
								sanitizingStringBuilder.Append(", ");
								sanitizingStringBuilder.Append(text);
							}
							num3++;
						}
						else
						{
							int num4;
							AttachmentId item2;
							SanitizedHtmlString item3;
							AttachmentAddResult attachmentAddResult = AttachmentUtility.AddAttachmentFromStream(item, text, files[i].ContentType, files[i].InputStream, userContext, isInlineImage, out num4, out item2, out item3);
							if (attachmentAddResult.ResultCode != AttachmentAddResultCode.NoError)
							{
								return attachmentAddResult;
							}
							list.Add(item2);
							attachmentLinks.Add(item3);
						}
					}
				}
			}
			AttachmentAddResult noError = AttachmentAddResult.NoError;
			ConflictResolutionResult conflictResolutionResult = null;
			bool flag2 = false;
			try
			{
				conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
				item.Load();
				if (!userContext.IsBasicExperience && userContext.IsIrmEnabled)
				{
					Utilities.IrmDecryptIfRestricted(item, userContext, true);
				}
			}
			catch (MessageTooBigException)
			{
				flag2 = true;
			}
			bool flag3 = false;
			if (conflictResolutionResult != null && conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				noError.SetResult(AttachmentAddResultCode.IrresolvableConflictWhenSaving, SanitizedHtmlString.FromStringId(-482397486));
				flag3 = true;
			}
			if (num3 != 0)
			{
				SanitizedHtmlString message = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1293887935), new object[]
				{
					sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>()
				});
				noError.SetResult(AttachmentAddResultCode.InsertingNonImageAttachment, message);
			}
			if (noError.ResultCode == AttachmentAddResultCode.NoError && stringBuilder != null)
			{
				SanitizedHtmlString message2 = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-178989031), new object[]
				{
					stringBuilder.ToString(),
					maximumFileSize
				});
				noError.SetResult(AttachmentAddResultCode.AttachmentExcceedsSizeLimit, message2);
			}
			else if (flag2)
			{
				noError.SetResult(AttachmentAddResultCode.ItemExcceedsSizeLimit, SanitizedHtmlString.FromStringId(-124437133));
			}
			if (flag3)
			{
				foreach (AttachmentId attachmentId in list)
				{
					attachmentCollection.Remove(attachmentId);
				}
			}
			return noError;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00097928 File Offset: 0x00095B28
		private static string AttachGetFileName(string userFileName)
		{
			int num = userFileName.LastIndexOfAny(AttachmentUtility.directorySeparatorCharacters);
			if (num == -1)
			{
				return userFileName;
			}
			return userFileName.Substring(num + 1);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00097950 File Offset: 0x00095B50
		public static AttachmentAddResult AddAttachmentFromStream(Item item, string attachmentFileName, string contentType, Stream inputStream, UserContext userContext, out int sizeInBytes)
		{
			AttachmentId attachmentId;
			SanitizedHtmlString sanitizedHtmlString;
			return AttachmentUtility.AddAttachmentFromStream(item, attachmentFileName, contentType, inputStream, userContext, false, out sizeInBytes, out attachmentId, out sanitizedHtmlString);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x00097970 File Offset: 0x00095B70
		public static AttachmentAddResult AddAttachmentFromStream(Item item, string attachmentFileName, string contentType, Stream inputStream, UserContext userContext, bool isInlineImage, out int sizeInBytes, out AttachmentId attachmentId, out SanitizedHtmlString attachmentLinkUrl)
		{
			AttachmentAddResult noError = AttachmentAddResult.NoError;
			attachmentLinkUrl = null;
			attachmentId = null;
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (string.IsNullOrEmpty(attachmentFileName))
			{
				throw new ArgumentNullException("attachmentFileName");
			}
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, userContext);
			using (StreamAttachment streamAttachment = (StreamAttachment)attachmentCollection.Create(AttachmentType.Stream))
			{
				streamAttachment.FileName = attachmentFileName;
				streamAttachment[AttachmentSchema.DisplayName] = attachmentFileName;
				streamAttachment.ContentType = contentType;
				if (isInlineImage)
				{
					streamAttachment.IsInline = isInlineImage;
					streamAttachment.ContentId = Guid.NewGuid().ToString();
				}
				sizeInBytes = 0;
				using (Stream contentStream = streamAttachment.GetContentStream())
				{
					byte[] array = new byte[32768];
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					int num;
					while ((num = inputStream.Read(array, 0, array.Length)) > 0)
					{
						contentStream.Write(array, 0, num);
						sizeInBytes += num;
						UserContextManager.TouchUserContext(userContext, stopwatch);
					}
					contentStream.Close();
				}
				try
				{
					streamAttachment.Save();
					streamAttachment.Load();
					attachmentLinkUrl = AttachmentUtility.GetInlineAttachmentLink(streamAttachment, item);
					attachmentId = streamAttachment.Id;
				}
				catch (ObjectNotFoundException)
				{
					if (ExTraceGlobals.DocumentsTracer.IsTraceEnabled(TraceType.DebugTrace) && item.Id != null)
					{
						ExTraceGlobals.DocumentsTracer.TraceDebug<string>(0L, "Attachment was not saved in item.  ItemId = {0}  ", item.Id.ToBase64String());
					}
					noError.SetResult(AttachmentAddResultCode.GeneralErrorWhenSaving, SanitizedHtmlString.FromStringId(-2102593951));
				}
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.AttachmentsUploaded.Increment();
			}
			return noError;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00097B5C File Offset: 0x00095D5C
		public static SanitizedHtmlString AddExistingItems(Item item, List<OwaStoreObjectId> itemsToAttachIds, UserContext userContext)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (itemsToAttachIds == null)
			{
				throw new ArgumentNullException("itemsToAttachIds");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			int maximumFileSize = AttachmentUtility.GetMaximumFileSize(userContext);
			OwaStoreObjectId obj = OwaStoreObjectId.CreateFromStoreObject(item);
			Item item2 = null;
			ItemAttachment itemAttachment = null;
			List<AttachmentId> list = new List<AttachmentId>(itemsToAttachIds.Count);
			SanitizedHtmlString sanitizedHtmlString = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Utilities.MakeModifiedCalendarItemOccurrence(item);
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, userContext);
			int count = attachmentCollection.Count;
			if (count + itemsToAttachIds.Count > 499)
			{
				return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1025276934), new object[]
				{
					499
				});
			}
			long num = (long)AttachmentUtility.GetTotalAttachmentSize(attachmentCollection);
			int i = 0;
			while (i < itemsToAttachIds.Count)
			{
				if (itemsToAttachIds[i].Equals(obj))
				{
					item2 = Item.CloneItem(userContext.MailboxSession, userContext.TryGetMyDefaultFolderId(DefaultFolderType.Drafts), item, false, false, null);
					Utilities.SaveItem(item2);
					item2.Load();
					flag3 = true;
					goto IL_111;
				}
				try
				{
					item2 = Utilities.GetItem<Item>(userContext, itemsToAttachIds[i], new PropertyDefinition[0]);
				}
				catch (ObjectNotFoundException)
				{
					flag2 = true;
					goto IL_1D1;
				}
				goto IL_111;
				IL_1D1:
				i++;
				continue;
				IL_111:
				using (item2)
				{
					num += item2.Size();
					if (num > (long)AttachmentUtility.ToByteSize(maximumFileSize))
					{
						flag = true;
					}
					else if (ObjectClass.IsOfClass(item2.ClassName, "IPM.Note.Microsoft.Approval.Request"))
					{
						flag2 = true;
					}
					else
					{
						try
						{
							itemAttachment = attachmentCollection.AddExistingItem(item2);
						}
						catch (ObjectNotFoundException)
						{
							flag2 = true;
							goto IL_1D1;
						}
						catch (StoragePermanentException)
						{
							flag2 = true;
							goto IL_1D1;
						}
						catch (StorageTransientException)
						{
							flag2 = true;
							goto IL_1D1;
						}
						finally
						{
							if (itemAttachment == null)
							{
								flag2 = true;
							}
							else
							{
								itemAttachment.Save();
								itemAttachment.Load();
								list.Add(itemAttachment.Id);
								itemAttachment.Dispose();
								itemAttachment = null;
							}
						}
						if (flag3)
						{
							Utilities.Delete(userContext, DeleteItemFlags.HardDelete, new OwaStoreObjectId[]
							{
								(item2.Id == null) ? null : OwaStoreObjectId.CreateFromStoreObject(item2)
							});
							flag3 = false;
						}
					}
				}
				goto IL_1D1;
			}
			ConflictResolutionResult conflictResolutionResult = null;
			bool flag4 = false;
			try
			{
				conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
			}
			catch (MessageTooBigException)
			{
				flag4 = true;
			}
			bool flag5 = false;
			if (conflictResolutionResult != null && conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				sanitizedHtmlString = SanitizedHtmlString.FromStringId(-482397486);
				flag5 = true;
			}
			if (SanitizedStringBase<OwaHtml>.IsNullOrEmpty(sanitizedHtmlString) && flag4)
			{
				flag5 = true;
				sanitizedHtmlString = SanitizedHtmlString.FromStringId(543046074);
			}
			else if (flag)
			{
				sanitizedHtmlString = SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1099394719), new object[]
				{
					maximumFileSize
				});
			}
			else if (flag2)
			{
				sanitizedHtmlString = SanitizedHtmlString.FromStringId(445087080);
			}
			if (flag5)
			{
				foreach (AttachmentId attachmentId in list)
				{
					attachmentCollection.Remove(attachmentId);
				}
			}
			return sanitizedHtmlString;
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00097E80 File Offset: 0x00096080
		public static void RemoveAttachment(Item item, ArrayList attachmentId)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (attachmentId == null)
			{
				throw new ArgumentNullException("attachmentId");
			}
			if (attachmentId.Count > 0)
			{
				Utilities.MakeModifiedCalendarItemOccurrence(item);
			}
			for (int i = 0; i < attachmentId.Count; i++)
			{
				AttachmentId attachmentId2 = (AttachmentId)attachmentId[i];
				AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, OwaContext.Current.UserContext);
				attachmentCollection.Remove(attachmentId2);
			}
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00097EF4 File Offset: 0x000960F4
		public static void RemoveAttachment(Item item, AttachmentId attachmentId)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (attachmentId == null)
			{
				throw new ArgumentNullException("attachmentId");
			}
			Utilities.MakeModifiedCalendarItemOccurrence(item);
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, OwaContext.Current.UserContext);
			attachmentCollection.Remove(attachmentId);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x00097F40 File Offset: 0x00096140
		internal static bool RemoveUnlinkedInlineAttachments(Item item, string bodyMarkup)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (bodyMarkup == null)
			{
				throw new ArgumentNullException("bodyMarkup");
			}
			StoreObjectType storeObjectType = StoreObjectType.Unknown;
			if (ObjectClass.IsMessage(item.ClassName, false))
			{
				storeObjectType = StoreObjectType.Message;
			}
			else if (ObjectClass.IsMeetingMessage(item.ClassName))
			{
				storeObjectType = StoreObjectType.MeetingMessage;
			}
			else if (ObjectClass.IsCalendarItemOrOccurrence(item.ClassName))
			{
				storeObjectType = StoreObjectType.CalendarItem;
			}
			return storeObjectType != StoreObjectType.Unknown && BodyConversionUtilities.SetBody(item, bodyMarkup, Markup.Html, storeObjectType, OwaContext.Current.UserContext);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x00097FB8 File Offset: 0x000961B8
		public static ArrayList GetAttachmentList(Item item, IList<AttachmentLink> attachmentLinks, bool isLoggedOnFromPublicComputer, bool isEmbeddedItem, bool discardInline)
		{
			return AttachmentUtility.GetAttachmentList(item, attachmentLinks, isLoggedOnFromPublicComputer, isEmbeddedItem, discardInline, false);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00097FC8 File Offset: 0x000961C8
		public static ArrayList GetAttachmentList(Item item, IList<AttachmentLink> attachmentLinks, bool isLoggedOnFromPublicComputer, bool isEmbeddedItem, bool discardInline, bool forceEnableItemLink)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (item is ReportMessage || ObjectClass.IsSmsMessage(item.ClassName))
			{
				return null;
			}
			UserContext userContext = UserContextManager.GetUserContext();
			bool isJunkOrPhishing = JunkEmailUtilities.IsJunkOrPhishing(item, isEmbeddedItem, forceEnableItemLink, userContext);
			bool isSharingMessage = AttachmentUtility.IsSharingMessage(item.ClassName);
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, true, userContext);
			ArrayList result = new ArrayList();
			if (attachmentLinks != null)
			{
				result = AttachmentUtility.CreateAttachmentList(attachmentLinks, attachmentCollection, isLoggedOnFromPublicComputer, isJunkOrPhishing, discardInline, isSharingMessage);
			}
			else
			{
				result = AttachmentUtility.CreateAttachmentList(attachmentCollection, isLoggedOnFromPublicComputer, isJunkOrPhishing, discardInline, isSharingMessage);
			}
			return result;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0009804C File Offset: 0x0009624C
		internal static ArrayList GetAttachmentListForZip(Item item, bool discardInline, UserContext userContext)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (item is ReportMessage || ObjectClass.IsSmsMessage(item.ClassName))
			{
				return null;
			}
			bool isSharingMessage = AttachmentUtility.IsSharingMessage(item.ClassName);
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, true, userContext);
			return AttachmentUtility.CreateAttachmentListForZip(attachmentCollection, discardInline, isSharingMessage, userContext);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x000980B0 File Offset: 0x000962B0
		private static ArrayList CreateAttachmentListForZip(AttachmentCollection attachmentCollection, bool discardInline, bool isSharingMessage, UserContext userContext)
		{
			ArrayList arrayList = new ArrayList();
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					if (!discardInline || (!(attachment is OleAttachment) && !attachment.IsInline))
					{
						if (!isSharingMessage || !AttachmentUtility.IsSharingMessageAttachment(attachment.ContentType, attachment.DisplayName))
						{
							AttachmentPolicy.Level attachmentLevel = AttachmentLevelLookup.GetAttachmentLevel(attachment, userContext);
							if (attachmentLevel != AttachmentPolicy.Level.Block)
							{
								string contentType = AttachmentUtility.CalculateContentType(attachment);
								if (!AttachmentUtility.IsMhtmlAttachment(contentType, attachment.FileExtension))
								{
									if (!AttachmentUtility.IsSmimeAttachment(contentType, attachment.FileName))
									{
										AttachmentWellInfo value = AttachmentUtility.CreateAttachmentInfoObject(attachmentCollection, attachment, false, false);
										arrayList.Add(value);
									}
								}
							}
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0009819C File Offset: 0x0009639C
		public static int GetCountForDownloadAttachments(ArrayList attachmentWellInfos)
		{
			int num = 0;
			foreach (object obj in attachmentWellInfos)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
				if (attachmentWellInfo.AttachmentLevel != AttachmentPolicy.Level.Block && !AttachmentUtility.IsMhtmlAttachment(attachmentWellInfo.MimeType, attachmentWellInfo.FileExtension))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0009820C File Offset: 0x0009640C
		private static ArrayList CreateAttachmentList(AttachmentCollection attachmentCollection, bool isLoggedOnFromPublicComputer, bool isJunkOrPhishing, bool discardInline, bool isSharingMessage)
		{
			ArrayList arrayList = new ArrayList();
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					if (!discardInline || (!(attachment is OleAttachment) && !attachment.IsInline))
					{
						if (!isSharingMessage || !AttachmentUtility.IsSharingMessageAttachment(attachment.ContentType, attachment.DisplayName))
						{
							AttachmentWellInfo value = AttachmentUtility.CreateAttachmentInfoObject(attachmentCollection, attachment, isLoggedOnFromPublicComputer, isJunkOrPhishing);
							arrayList.Add(value);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x000982BC File Offset: 0x000964BC
		private static ArrayList CreateAttachmentList(IList<AttachmentLink> attachmentLinks, AttachmentCollection attachmentCollection, bool isLoggedOnFromPublicComputer, bool isJunkOrPhishing, bool discardInline, bool isSharingMessage)
		{
			ArrayList arrayList = new ArrayList();
			foreach (AttachmentLink attachmentLink in attachmentLinks)
			{
				if (attachmentCollection.Contains(attachmentLink.AttachmentId) && (!discardInline || (attachmentLink.AttachmentType != AttachmentType.Ole && !(attachmentLink.IsMarkedInline == true))) && (!isSharingMessage || !AttachmentUtility.IsSharingMessageAttachment(attachmentLink.ContentType, attachmentLink.DisplayName)))
				{
					AttachmentWellInfo value = AttachmentUtility.CreateAttachmentInfoObject(attachmentCollection, attachmentLink, isLoggedOnFromPublicComputer, isJunkOrPhishing);
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00098368 File Offset: 0x00096568
		public static bool IsSharingMessage(string className)
		{
			return ObjectClass.IsOfClass(className, "IPM.Sharing");
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00098375 File Offset: 0x00096575
		private static bool IsSharingMessageAttachment(string contentType, string displayName)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(contentType, "application/x-sharing-metadata-xml") == 0 && 0 == StringComparer.OrdinalIgnoreCase.Compare(displayName, "sharing_metadata.xml");
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0009839E File Offset: 0x0009659E
		private static bool IsSmimeAttachment(string contentType, string fileName)
		{
			return StringComparer.OrdinalIgnoreCase.Compare(fileName, "smime.p7m") == 0 || 0 == StringComparer.OrdinalIgnoreCase.Compare(contentType, "multipart/signed");
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000983C8 File Offset: 0x000965C8
		public static ArrayList GetAttachmentList(OwaStoreObjectId owaConversationId, ItemPart itemPart, bool isLoggedOnFromPublicComputer, bool isEmbeddedItem, bool discardInline, bool forceEnableItemLink)
		{
			if (owaConversationId == null)
			{
				throw new ArgumentNullException("owaConversationId");
			}
			if (itemPart == null)
			{
				throw new ArgumentNullException("itemPart");
			}
			if (!owaConversationId.IsConversationId)
			{
				throw new ArgumentException("owaConversationId");
			}
			string text = string.Empty;
			object obj = itemPart.StorePropertyBag.TryGetProperty(StoreObjectSchema.ItemClass);
			if (!(obj is PropertyError))
			{
				text = (string)obj;
			}
			if (ObjectClass.IsReport(text) || ObjectClass.IsSmsMessage(text))
			{
				return null;
			}
			ArrayList arrayList = null;
			bool isJunkOrPhishing = JunkEmailUtilities.IsJunkOrPhishing(itemPart.StorePropertyBag, isEmbeddedItem, forceEnableItemLink, UserContextManager.GetUserContext());
			bool flag = AttachmentUtility.IsSharingMessage(text);
			arrayList = new ArrayList();
			foreach (AttachmentInfo attachmentInfo in itemPart.Attachments)
			{
				if ((!discardInline || (attachmentInfo.AttachmentType != AttachmentType.Ole && !attachmentInfo.IsInline)) && (!flag || !AttachmentUtility.IsSharingMessageAttachment(attachmentInfo.ContentType, attachmentInfo.DisplayName)))
				{
					AttachmentWellInfo value = AttachmentUtility.CreateAttachmentInfoObject(owaConversationId, attachmentInfo, isLoggedOnFromPublicComputer, isJunkOrPhishing);
					arrayList.Add(value);
				}
			}
			return arrayList;
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000984E8 File Offset: 0x000966E8
		internal static string CalculateContentType(Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}
			if (!string.IsNullOrEmpty(attachment.ContentType))
			{
				return attachment.ContentType;
			}
			return attachment.CalculatedContentType;
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x00098514 File Offset: 0x00096714
		internal static void SetResponseHeadersForZipAttachments(HttpContext httpContext, string fileName)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				throw new OwaInvalidInputException("Argument fileName may not be null or empty string");
			}
			httpContext.Response.ContentType = "application/zip; authoritative=true;";
			AttachmentUtility.SetZipContentDispositionResponseHeader(httpContext, fileName);
			httpContext.Response.Cache.SetExpires(AttachmentUtility.GetAttachmentExpiryDate());
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x00098570 File Offset: 0x00096770
		private static void SetZipContentDispositionResponseHeader(HttpContext httpContext, string fileName)
		{
			string str = AttachmentUtility.ToHexString(fileName);
			httpContext.Response.AppendHeader("Content-Disposition", "filename=" + str);
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000985A0 File Offset: 0x000967A0
		internal static void SetContentDispositionResponseHeader(HttpContext httpContext, string fileName, bool isInline)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (string.IsNullOrEmpty(fileName))
			{
				throw new OwaInvalidInputException("Argument fileName may not be null or empty string");
			}
			string text = AttachmentUtility.ToHexString(fileName);
			string value = string.Empty;
			BrowserType browserType = Utilities.GetBrowserType(httpContext.Request.UserAgent);
			if (browserType == BrowserType.Firefox)
			{
				value = string.Format(CultureInfo.InvariantCulture, "{0}; filename*=\"{1}\"", new object[]
				{
					isInline ? "inline" : "attachment",
					text
				});
			}
			else
			{
				value = string.Format(CultureInfo.InvariantCulture, "{0}; filename=\"{1}\"", new object[]
				{
					isInline ? "inline" : "attachment",
					text
				});
			}
			httpContext.Response.AppendHeader("Content-Disposition", value);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x00098664 File Offset: 0x00096864
		internal static DateTime GetAttachmentExpiryDate()
		{
			ExDateTime exDateTime = DateTimeUtilities.GetLocalTime().IncrementDays(-1);
			return (DateTime)exDateTime;
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x00098688 File Offset: 0x00096888
		private static string ToHexString(string fileName)
		{
			StringBuilder stringBuilder = new StringBuilder(fileName.Length);
			byte[] bytes = Encoding.UTF8.GetBytes(fileName);
			for (int i = 0; i < bytes.Length; i++)
			{
				if (bytes[i] >= 0 && bytes[i] <= 127 && !AttachmentUtility.IsMIMEAttributeSpecialChar((char)bytes[i]))
				{
					stringBuilder.Append((char)bytes[i]);
				}
				else
				{
					stringBuilder.AppendFormat("%{0}", Convert.ToString(bytes[i], 16));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000986FC File Offset: 0x000968FC
		private static bool IsMIMEAttributeSpecialChar(char c)
		{
			if (char.IsControl(c))
			{
				return true;
			}
			switch (c)
			{
			case ' ':
			case '"':
			case '%':
			case '\'':
			case '(':
			case ')':
			case '*':
			case ',':
			case '/':
				break;
			case '!':
			case '#':
			case '$':
			case '&':
			case '+':
			case '-':
			case '.':
				return false;
			default:
				switch (c)
				{
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					break;
				default:
					switch (c)
					{
					case '[':
					case '\\':
					case ']':
						break;
					default:
						return false;
					}
					break;
				}
				break;
			}
			return true;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000987A0 File Offset: 0x000969A0
		internal static void UpdateAcceptEncodingHeader(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (!string.IsNullOrEmpty(httpContext.Request.Headers["Accept-Encoding"]))
			{
				httpContext.Request.Headers["Accept-Encoding"] = string.Empty;
			}
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000987F4 File Offset: 0x000969F4
		internal static BlockStatus GetItemBlockStatus(Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			BlockStatus result = BlockStatus.DontKnow;
			object obj = item.TryGetProperty(ItemSchema.BlockStatus);
			if (obj is BlockStatus)
			{
				result = (BlockStatus)obj;
			}
			return result;
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x00098830 File Offset: 0x00096A30
		internal static bool IsMhtmlAttachment(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return contentType.ToLowerInvariant().Contains("multipart/related") || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".mht") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".mhtml") == 0;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x00098884 File Offset: 0x00096A84
		internal static bool IsXmlAttachment(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return contentType.ToLowerInvariant().Contains("text/xml") || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".xml") == 0;
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000988BC File Offset: 0x00096ABC
		internal static bool IsHtmlAttachment(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return contentType.ToLowerInvariant().Contains("text/html") || contentType.ToLowerInvariant().Contains("application/xhtml+xml") || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".htm") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".html") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".xhtml") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".xht") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".shtml") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".shtm") == 0 || StringComparer.OrdinalIgnoreCase.Compare(fileExtension, ".stm") == 0;
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x00098980 File Offset: 0x00096B80
		internal static bool DoNeedToFilterHtml(string contentType, string fileExtension, AttachmentPolicy.Level level, UserContext userContext)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			bool flag = AttachmentUtility.IsHtmlAttachment(contentType, fileExtension);
			bool flag2 = AttachmentPolicy.Level.ForceSave == level;
			bool result = false;
			bool flag3 = userContext.IsFeatureEnabled(Feature.ForceSaveAttachmentFiltering);
			if (flag)
			{
				result = (!flag2 || (flag2 && flag3));
			}
			return result;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000989DC File Offset: 0x00096BDC
		internal static void UpdateContentTypeForNeedToFilter(out string contentType, Charset charset)
		{
			Encoding encoding = null;
			if (charset != null && charset.TryGetEncoding(out encoding))
			{
				contentType = "text/html; charset=" + charset.Name;
				return;
			}
			contentType = Utilities.GetContentTypeString(OwaEventContentType.Html);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00098A14 File Offset: 0x00096C14
		internal static StreamAttachmentBase GetStreamAttachment(Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment");
			}
			return attachment as StreamAttachmentBase;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x00098A38 File Offset: 0x00096C38
		internal static Stream GetStream(StreamAttachmentBase streamAttachment)
		{
			if (streamAttachment == null)
			{
				throw new ArgumentNullException("streamAttachment");
			}
			OleAttachment oleAttachment = streamAttachment as OleAttachment;
			Stream result;
			if (oleAttachment != null)
			{
				result = oleAttachment.TryConvertToImage(ImageFormat.Jpeg);
			}
			else
			{
				result = streamAttachment.GetContentStream(PropertyOpenMode.ReadOnly);
			}
			return result;
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x00098A72 File Offset: 0x00096C72
		internal static bool GetIsHtmlOrXml(string contentType, string fileExtension)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			return AttachmentUtility.IsXmlAttachment(contentType, fileExtension) || AttachmentUtility.IsHtmlAttachment(contentType, fileExtension);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x00098A94 File Offset: 0x00096C94
		internal static bool GetDoNotSniff(AttachmentPolicy.Level level, UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return AttachmentPolicy.Level.ForceSave == level && !userContext.IsFeatureEnabled(Feature.ForceSaveAttachmentFiltering);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x00098ABC File Offset: 0x00096CBC
		internal static uint WriteFilteredResponse(HttpContext httpContext, Stream stream, Charset charset, BlockStatus blockStatus)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			uint result = 0U;
			try
			{
				using (Stream filteredStream = AttachmentUtility.GetFilteredStream(httpContext, stream, charset, blockStatus))
				{
					result = AttachmentUtility.WriteOutputStream(httpContext.Response.OutputStream, filteredStream);
				}
			}
			catch (ExchangeDataException innerException)
			{
				throw new OwaBodyConversionFailedException("Sanitize Html Failed", innerException);
			}
			catch (StoragePermanentException innerException2)
			{
				throw new OwaBodyConversionFailedException("Safe Html Attachment Conversion Failed", innerException2);
			}
			catch (StorageTransientException innerException3)
			{
				throw new OwaBodyConversionFailedException("Safe Html Attachment Conversion Failed", innerException3);
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
			return result;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x00098B8C File Offset: 0x00096D8C
		internal static Stream GetFilteredStream(HttpContext httpContext, Stream inputStream, Charset charset, BlockStatus blockStatus)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			OwaContext owaContext = OwaContext.Get(httpContext);
			UserContext userContext = owaContext.UserContext;
			HtmlToHtml htmlToHtml = new HtmlToHtml();
			TextConvertersInternalHelpers.SetPreserveDisplayNoneStyle(htmlToHtml, true);
			WebBeaconFilterLevels filterWebBeaconsAndHtmlForms = userContext.Configuration.FilterWebBeaconsAndHtmlForms;
			OwaSafeHtmlOutboundCallbacks @object;
			if (filterWebBeaconsAndHtmlForms == WebBeaconFilterLevels.DisableFilter || blockStatus == BlockStatus.NoNeverAgain)
			{
				@object = new OwaSafeHtmlAllowWebBeaconCallbacks(owaContext, true);
			}
			else
			{
				@object = new OwaSafeHtmlOutboundCallbacks(owaContext, false);
			}
			Encoding encoding = null;
			if (charset != null && charset.TryGetEncoding(out encoding))
			{
				htmlToHtml.DetectEncodingFromMetaTag = false;
				htmlToHtml.InputEncoding = encoding;
				htmlToHtml.OutputEncoding = encoding;
			}
			else
			{
				htmlToHtml.DetectEncodingFromMetaTag = true;
				htmlToHtml.InputEncoding = Encoding.ASCII;
				htmlToHtml.OutputEncoding = null;
			}
			htmlToHtml.FilterHtml = true;
			htmlToHtml.HtmlTagCallback = new HtmlTagCallback(@object.ProcessTag);
			return new ConverterStream(inputStream, htmlToHtml, ConverterStreamAccess.Read);
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x00098C60 File Offset: 0x00096E60
		internal static uint WriteUnfilteredResponse(HttpContext httpContext, Stream stream, string fileName, bool isNotHtmlandNotXml, bool doNotSniff)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			uint num = 0U;
			byte[] array = new byte[512];
			int num2 = stream.Read(array, 0, 512);
			if (num2 == 0)
			{
				return 0U;
			}
			try
			{
				if (!doNotSniff && isNotHtmlandNotXml && AttachmentUtility.CheckShouldRemoveContents(array, num2))
				{
					if (ExTraceGlobals.AttachmentHandlingTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string, string>(0L, "AttachmentUtility.WriteUnfilteredResponse: Return contents removed for attachment {0}: mailbox {1}", fileName, AttachmentUtility.TryGetMailboxIdentityName(httpContext));
					}
					num = AttachmentUtility.WriteContentsRemoved(httpContext.Response.OutputStream);
				}
				else
				{
					if (ExTraceGlobals.AttachmentHandlingTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.AttachmentHandlingTracer.TraceDebug<string, string>(0L, "AttachmentUtility.WriteUnfilteredResponse: Skip data sniff for attachment {0}: mailbox {1}", fileName, AttachmentUtility.TryGetMailboxIdentityName(httpContext));
					}
					httpContext.Response.OutputStream.Write(array, 0, num2);
					num = AttachmentUtility.WriteOutputStream(httpContext.Response.OutputStream, stream);
					num += (uint)num2;
				}
			}
			finally
			{
				if (stream != null)
				{
					((IDisposable)stream).Dispose();
				}
			}
			return num;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x00098D60 File Offset: 0x00096F60
		internal static bool CheckShouldRemoveContents(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanSeek)
			{
				throw new OwaInvalidInputException("Stream is required to support Seek operations, and does not");
			}
			byte[] array = new byte[512];
			int bytesToRead = stream.Read(array, 0, 512);
			bool result = AttachmentUtility.CheckShouldRemoveContents(array, bytesToRead);
			stream.Seek(0L, SeekOrigin.Begin);
			return result;
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00098DBC File Offset: 0x00096FBC
		private static bool CheckShouldRemoveContents(byte[] bytesToSniff, int bytesToRead)
		{
			bool result;
			using (MemoryStream memoryStream = new MemoryStream(bytesToSniff, 0, bytesToRead))
			{
				DataSniff dataSniff = new DataSniff(256);
				string x = dataSniff.FindMimeFromData(memoryStream);
				result = (StringComparer.OrdinalIgnoreCase.Compare(x, "text/xml") == 0 || 0 == StringComparer.OrdinalIgnoreCase.Compare(x, "text/html"));
			}
			return result;
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x00098E2C File Offset: 0x0009702C
		private static uint WriteContentsRemoved(Stream outputStream)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(LocalizedStrings.GetNonEncoded(-1868113279));
			outputStream.Write(bytes, 0, bytes.Length);
			return (uint)bytes.Length;
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x00098E5C File Offset: 0x0009705C
		internal static uint WriteOutputStream(Stream outputStream, Stream inputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			uint num = 0U;
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(AttachmentUtility.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			try
			{
				int num2;
				while ((num2 = inputStream.Read(array, 0, array.Length)) > 0)
				{
					outputStream.Write(array, 0, num2);
					num += (uint)num2;
				}
			}
			finally
			{
				if (array != null)
				{
					bufferPool.Release(array);
				}
			}
			return num;
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x00098EE0 File Offset: 0x000970E0
		internal static uint[] CompressAndWriteOutputStream(Stream outputStream, Stream inputStream, bool doComputeCrc)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (inputStream == null)
			{
				throw new ArgumentNullException("inputStream");
			}
			uint num = 0U;
			int num2 = 0;
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(AttachmentUtility.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			uint num3 = 0U;
			using (Stream stream = Streams.CreateTemporaryStorageStream())
			{
				try
				{
					int num4;
					using (Stream stream2 = new DeflateStream(stream, CompressionMode.Compress, true))
					{
						while ((num4 = inputStream.Read(array, 0, array.Length)) > 0)
						{
							if (doComputeCrc)
							{
								num3 = AttachmentUtility.ComputeCrc32FromBytes(array, num4, num3);
							}
							num2 += num4;
							stream2.Write(array, 0, num4);
						}
						stream2.Flush();
					}
					stream.Seek(0L, SeekOrigin.Begin);
					while ((num4 = stream.Read(array, 0, array.Length)) > 0)
					{
						outputStream.Write(array, 0, num4);
						num += (uint)num4;
					}
				}
				finally
				{
					if (array != null)
					{
						bufferPool.Release(array);
					}
				}
			}
			return new uint[]
			{
				num,
				num3,
				(uint)num2
			};
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x0009900C File Offset: 0x0009720C
		internal static uint ComputeCrc32FromStream(Stream stream)
		{
			if (!stream.CanSeek)
			{
				throw new OwaInvalidInputException("Stream is required to support Seek operations, and does not");
			}
			BufferPool bufferPool = BufferPoolCollection.AutoCleanupCollection.Acquire(AttachmentUtility.CopyBufferSize);
			byte[] array = bufferPool.Acquire();
			uint num = 0U;
			try
			{
				int bytesToRead;
				while ((bytesToRead = stream.Read(array, 0, array.Length)) > 0)
				{
					num = AttachmentUtility.ComputeCrc32FromBytes(array, bytesToRead, num);
				}
			}
			finally
			{
				if (array != null)
				{
					bufferPool.Release(array);
				}
			}
			stream.Seek(0L, SeekOrigin.Begin);
			return num;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00099088 File Offset: 0x00097288
		internal static uint ComputeCrc32FromBytes(byte[] data, int bytesToRead, uint seed)
		{
			uint num = seed ^ uint.MaxValue;
			for (int i = 0; i < bytesToRead; i++)
			{
				num = (AttachmentUtility.CrcTable[(int)((UIntPtr)((num ^ (uint)data[i]) & 255U))] ^ num >> 8);
			}
			return num ^ uint.MaxValue;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000990C1 File Offset: 0x000972C1
		internal static string CalculateAttachmentName(string displayName, string filename)
		{
			if (!string.IsNullOrEmpty(displayName))
			{
				return displayName;
			}
			if (!string.IsNullOrEmpty(filename))
			{
				return filename;
			}
			return Strings.UntitledAttachment;
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000990DC File Offset: 0x000972DC
		internal static int GetEmbeddedItemNestingLevel(HttpRequest request)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(request, "attcnt");
			int num;
			if (!int.TryParse(queryStringParameter, out num))
			{
				throw new OwaInvalidRequestException("Invalid attachment count querystring parameter");
			}
			if (num > AttachmentPolicy.MaxEmbeddedDepth)
			{
				throw new OwaInvalidRequestException("Accessing embedded attachment beyond maximum embedded depth");
			}
			return num;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00099120 File Offset: 0x00097320
		internal static void RenderDownloadAllAttachmentsLink(SanitizingTextWriter<OwaHtml> sanitizingWriter, HttpRequest request, string urlEncodedItemId, bool isEmbeddedItemInNonSMimeItem, UserContext userContext, int count)
		{
			sanitizingWriter.Write("<div class=\"roWellWrap attZip\"><span class=\"fltBefore spnZipAtts\">");
			sanitizingWriter.Write(userContext.DirectionMark);
			sanitizingWriter.Write(LocalizedStrings.GetHtmlEncoded(6409762));
			sanitizingWriter.Write(count);
			sanitizingWriter.Write(LocalizedStrings.GetHtmlEncoded(-1023695022));
			sanitizingWriter.Write(userContext.DirectionMark);
			sanitizingWriter.Write("</span><a id=\"lnkZipAtts\" class=\"tbfz fltBefore tbfHz\" href=\"");
			AttachmentUtility.RenderDownloadAllAttachmentsLinkUrl(sanitizingWriter, request, urlEncodedItemId, isEmbeddedItemInNonSMimeItem);
			sanitizingWriter.Write("\" target=_self name=\"lnkZipAtts\">");
			sanitizingWriter.Write(SanitizedHtmlString.FromStringId(-792355597));
			sanitizingWriter.Write("</a></div>");
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000991B4 File Offset: 0x000973B4
		private static void RenderDownloadAllAttachmentsLinkUrl(SanitizingTextWriter<OwaHtml> sanitizingWriter, HttpRequest request, string urlEncodedItemId, bool isEmbeddedItemInNonSMimeItem)
		{
			sanitizingWriter.Write("attachment.ashx?id=");
			sanitizingWriter.Write(urlEncodedItemId);
			if (isEmbeddedItemInNonSMimeItem)
			{
				int embeddedItemNestingLevel = AttachmentUtility.GetEmbeddedItemNestingLevel(request);
				for (int i = 0; i < embeddedItemNestingLevel; i++)
				{
					string text = "attid" + i.ToString(CultureInfo.InvariantCulture);
					string queryStringParameter = Utilities.GetQueryStringParameter(request, text);
					sanitizingWriter.Write("&");
					sanitizingWriter.Write(text);
					sanitizingWriter.Write("=");
					sanitizingWriter.Write(queryStringParameter);
				}
				sanitizingWriter.Write("&attcnt=");
				sanitizingWriter.Write(embeddedItemNestingLevel);
			}
			sanitizingWriter.Write("&dla=1");
			if (isEmbeddedItemInNonSMimeItem)
			{
				sanitizingWriter.Write("&femb=1");
			}
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00099258 File Offset: 0x00097458
		internal static Item GetEmbeddedItem(Item parentItem, HttpRequest request, UserContext userContext)
		{
			if (parentItem == null)
			{
				throw new ArgumentNullException("parentItem");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			Item result = null;
			using (Attachment attachment = Utilities.GetAttachment(parentItem, request, userContext))
			{
				using (ItemAttachment itemAttachment = attachment as ItemAttachment)
				{
					if (itemAttachment == null)
					{
						return null;
					}
					result = itemAttachment.GetItem(null);
				}
			}
			return result;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000992E8 File Offset: 0x000974E8
		private static AttachmentWellInfo CreateAttachmentInfoObject(AttachmentCollection collection, Attachment attachment, bool isLoggedOnFromPublicComputer, bool isJunkOrPhishing)
		{
			return new AttachmentWellInfo(collection, attachment, isJunkOrPhishing);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000992F2 File Offset: 0x000974F2
		private static AttachmentWellInfo CreateAttachmentInfoObject(AttachmentCollection collection, AttachmentLink attachmentLink, bool isLoggedOnFromPublicComputer, bool isJunkOrPhishing)
		{
			return new AttachmentWellInfo(collection, attachmentLink, isJunkOrPhishing);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000992FC File Offset: 0x000974FC
		private static AttachmentWellInfo CreateAttachmentInfoObject(OwaStoreObjectId owaConversationId, AttachmentInfo attachmentInfo, bool isLoggedOnFromPublicComputer, bool isJunkOrPhishing)
		{
			return new AttachmentWellInfo(owaConversationId, attachmentInfo, isJunkOrPhishing);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00099308 File Offset: 0x00097508
		public static bool IsSupportedImageContentType(HttpPostedFile file)
		{
			string contentType = file.ContentType;
			return !string.IsNullOrEmpty(contentType) && (contentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/pjpeg", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/gif", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/bmp", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/png", StringComparison.OrdinalIgnoreCase) || contentType.Equals("image/x-png", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0009937C File Offset: 0x0009757C
		public static bool IsOutLine(ArrayList attachmentList)
		{
			if (attachmentList == null)
			{
				return false;
			}
			for (int i = 0; i < attachmentList.Count; i++)
			{
				AttachmentWellInfo attachmentWellInfo = attachmentList[i] as AttachmentWellInfo;
				if (attachmentWellInfo.AttachmentType != AttachmentType.Ole && !attachmentWellInfo.IsInline)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000993C0 File Offset: 0x000975C0
		public static bool IsLevelOneAndBlock(ArrayList attachmentList)
		{
			if (attachmentList == null)
			{
				return false;
			}
			int count = attachmentList.Count;
			if (count == 0)
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)attachmentList[i];
				if (attachmentWellInfo.AttachmentLevel == AttachmentPolicy.Level.Block && !AttachmentUtility.IsWebReadyDocument(attachmentWellInfo.FileExtension, attachmentWellInfo.MimeType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00099418 File Offset: 0x00097618
		public static bool IsLevelOneOnly(ArrayList attachmentList)
		{
			if (attachmentList == null)
			{
				return true;
			}
			foreach (object obj in attachmentList)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
				if (attachmentWellInfo.AttachmentLevel != AttachmentPolicy.Level.Block)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0009947C File Offset: 0x0009767C
		public static bool IsLevelOneAndBlockOnly(ArrayList attachmentList)
		{
			if (attachmentList == null)
			{
				return true;
			}
			foreach (object obj in attachmentList)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)obj;
				if (attachmentWellInfo.AttachmentLevel != AttachmentPolicy.Level.Block || AttachmentUtility.IsWebReadyDocument(attachmentWellInfo.FileExtension, attachmentWellInfo.MimeType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000994F4 File Offset: 0x000976F4
		public static bool IsWebReadyDocument(string fileExtension, string mimeType)
		{
			UserContext userContext = UserContextManager.GetUserContext();
			AttachmentPolicy attachmentPolicy = userContext.AttachmentPolicy;
			if (!attachmentPolicy.WebReadyDocumentViewingEnable)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(fileExtension))
			{
				if ((attachmentPolicy.WebReadyDocumentViewingForAllSupportedTypes || attachmentPolicy.Contains(fileExtension, AttachmentPolicy.LookupTypeSignifer.FileArray)) && attachmentPolicy.Contains(fileExtension, AttachmentPolicy.LookupTypeSignifer.SupportedFileArray))
				{
					ExTraceGlobals.TranscodingTracer.TraceDebug<string>(0L, "File extension: {0} passes.", fileExtension);
					return true;
				}
			}
			else if (!string.IsNullOrEmpty(mimeType) && (attachmentPolicy.WebReadyDocumentViewingForAllSupportedTypes || attachmentPolicy.Contains(mimeType, AttachmentPolicy.LookupTypeSignifer.MimeArray)) && attachmentPolicy.Contains(mimeType, AttachmentPolicy.LookupTypeSignifer.SupportedMimeArray))
			{
				ExTraceGlobals.TranscodingTracer.TraceDebug<string>(0L, "Mime type: {0} passes.", mimeType);
				return true;
			}
			return false;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0009958C File Offset: 0x0009778C
		public static AttachmentUtility.AttachmentLinkFlags GetAttachmentLinkFlag(AttachmentWellType wellType, AttachmentWellInfo attachmentInfoObject)
		{
			if (attachmentInfoObject == null)
			{
				throw new ArgumentNullException("attachmentInfoObject");
			}
			AttachmentUtility.AttachmentLinkFlags attachmentLinkFlags = AttachmentUtility.AttachmentLinkFlags.None;
			UserContext userContext = UserContextManager.GetUserContext();
			AttachmentPolicy attachmentPolicy = userContext.AttachmentPolicy;
			int embeddedDepth = Utilities.GetEmbeddedDepth(HttpContext.Current.Request);
			bool flag = AttachmentUtility.IsWebReadyDocument(attachmentInfoObject.FileExtension, attachmentInfoObject.MimeType);
			if (wellType == AttachmentWellType.ReadOnly && attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.Block && !flag)
			{
				return AttachmentUtility.AttachmentLinkFlags.Skip;
			}
			if (embeddedDepth < AttachmentPolicy.MaxEmbeddedDepth && (attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.ForceSave || attachmentInfoObject.AttachmentLevel == AttachmentPolicy.Level.Allow) && (!attachmentPolicy.ForceWebReadyDocumentViewingFirst || !flag))
			{
				attachmentLinkFlags |= AttachmentUtility.AttachmentLinkFlags.AttachmentClickLink;
			}
			if (flag && embeddedDepth < AttachmentPolicy.MaxEmbeddedDepth)
			{
				attachmentLinkFlags |= AttachmentUtility.AttachmentLinkFlags.OpenAsWebPageLink;
			}
			return attachmentLinkFlags;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00099628 File Offset: 0x00097828
		public static string GetEmbeddedAttachmentDisplayName(Item item)
		{
			string className = item.ClassName;
			string text;
			if (ObjectClass.IsContact(className) || ObjectClass.IsDistributionList(className))
			{
				text = ItemUtility.GetProperty<string>(item, StoreObjectSchema.DisplayName, string.Empty);
			}
			else
			{
				text = ItemUtility.GetProperty<string>(item, ItemSchema.Subject, string.Empty);
			}
			if (Utilities.WhiteSpaceOnlyOrNullEmpty(text))
			{
				text = LocalizedStrings.GetNonEncoded(1797976510);
			}
			return text;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00099688 File Offset: 0x00097888
		public static string TrimAttachmentDisplayName(string attachmentDisplayName, ArrayList previousAttachmentDisplayNames, bool isEmbeddedItem)
		{
			if (attachmentDisplayName == null)
			{
				throw new ArgumentNullException("attachmentDisplayName");
			}
			if (attachmentDisplayName.Length <= 32)
			{
				return attachmentDisplayName;
			}
			string text = "~";
			int num = 1;
			int num2 = attachmentDisplayName.LastIndexOf('.');
			string text2;
			string text3;
			if (num2 > 0)
			{
				text2 = attachmentDisplayName.Substring(num2);
				text3 = attachmentDisplayName.Substring(0, num2);
			}
			else
			{
				text3 = attachmentDisplayName;
				text2 = string.Empty;
			}
			int num3;
			if (isEmbeddedItem)
			{
				num3 = 29;
			}
			else if (previousAttachmentDisplayNames != null)
			{
				num3 = 32 - text2.Length - 2;
			}
			else
			{
				num3 = 32 - text2.Length - 3;
			}
			if (num3 > 0)
			{
				num3 = Math.Min(num3, text3.Length);
				text3 = text3.Substring(0, num3);
			}
			else
			{
				text3 = string.Empty;
			}
			if (isEmbeddedItem)
			{
				attachmentDisplayName = text3 + "...";
			}
			else if (previousAttachmentDisplayNames != null)
			{
				for (int i = 0; i < previousAttachmentDisplayNames.Count; i++)
				{
					if (previousAttachmentDisplayNames[i].Equals(text3))
					{
						num++;
					}
				}
				text += num.ToString();
				attachmentDisplayName = text3 + text + text2;
				previousAttachmentDisplayNames.Add(text3);
			}
			else
			{
				attachmentDisplayName = text3 + "..." + text2;
			}
			return attachmentDisplayName;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000997AC File Offset: 0x000979AC
		public static SanitizedHtmlString GetInlineAttachmentLink(Attachment attachment, Item item)
		{
			string format = "attachment.ashx?id={0}&attcnt=1&attid0={1}&attcid0={2}";
			return SanitizedHtmlString.Format(format, new object[]
			{
				Utilities.UrlEncode(Utilities.GetIdAsString(item)),
				Utilities.UrlEncode(attachment.Id.ToBase64String()),
				Utilities.UrlEncode(attachment.ContentId)
			});
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x00099800 File Offset: 0x00097A00
		public static bool PromoteInlineAttachments(Item item)
		{
			bool flag = false;
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(item, false, OwaContext.Current.UserContext);
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					if (attachment.IsInline || attachment.RenderingPosition != -1)
					{
						attachment.IsInline = false;
						attachment.RenderingPosition = -1;
						attachment.Save();
						flag = true;
					}
				}
			}
			if (flag)
			{
				Utilities.SaveItem(item, false);
			}
			return flag;
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000998BC File Offset: 0x00097ABC
		public static bool VerifyInlineAttachmentUrlValidity(string imageUrlEncoded, Item item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (string.IsNullOrEmpty(imageUrlEncoded))
			{
				return false;
			}
			string text = HttpUtility.UrlDecode(imageUrlEncoded);
			int num = imageUrlEncoded.IndexOf("attachment.ashx?id=", StringComparison.Ordinal);
			num += "attachment.ashx?id=".Length;
			if (num >= imageUrlEncoded.Length)
			{
				return false;
			}
			int num2 = imageUrlEncoded.IndexOf('&', num);
			if (num2 == -1 || num2 < num)
			{
				return false;
			}
			string str = imageUrlEncoded.Substring(num, num2 - num);
			if (string.CompareOrdinal(Utilities.GetIdAsString(item), HttpUtility.UrlDecode(str)) != 0)
			{
				return false;
			}
			int num3 = text.IndexOf("&attid0=", StringComparison.Ordinal);
			return num3 != -1 && num3 < text.Length;
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00099964 File Offset: 0x00097B64
		public static string ParseInlineAttachmentContentId(string imageUrl, out int contentIdIndex)
		{
			if (imageUrl == null)
			{
				throw new ArgumentNullException("imageUrl");
			}
			contentIdIndex = -1;
			int num = imageUrl.IndexOf("&attid0=", StringComparison.Ordinal);
			if (num != -1 && num < imageUrl.Length)
			{
				string empty = string.Empty;
				contentIdIndex = imageUrl.IndexOf("&attcid0=", num);
				if (contentIdIndex != -1 && contentIdIndex < imageUrl.Length)
				{
					return imageUrl.Substring(contentIdIndex + "&attcid0=".Length);
				}
			}
			return null;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000999D8 File Offset: 0x00097BD8
		public static string ParseInlineAttachmentIdString(string imageUrl, int contentIdIndex)
		{
			if (imageUrl == null)
			{
				throw new ArgumentNullException("imageUrl");
			}
			int num = imageUrl.IndexOf("&attid0=", StringComparison.Ordinal);
			int num2 = num + "&attid0=".Length;
			string result;
			if (contentIdIndex != -1)
			{
				result = imageUrl.Substring(num2, contentIdIndex - num2);
			}
			else
			{
				result = imageUrl.Substring(num2);
			}
			return result;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x00099A28 File Offset: 0x00097C28
		public static AttachmentLink GetAttachmentLink(string attachmentIdString, string contentId, Item item, ConversionCallbackBase converstionCallback)
		{
			if (attachmentIdString == null)
			{
				throw new ArgumentNullException("attachmentIdString");
			}
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (converstionCallback == null)
			{
				throw new ArgumentNullException("converstionCallback");
			}
			AttachmentId attachmentId = null;
			try
			{
				attachmentId = item.CreateAttachmentId(attachmentIdString);
			}
			catch (CorruptDataException)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "OwaSafeHtmlInboundCallbacks - failed to decipher attachment for URL = ({0})", attachmentIdString);
				return null;
			}
			return converstionCallback.FindAttachmentByIdOrContentId(attachmentId, contentId);
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00099A9C File Offset: 0x00097C9C
		public static string GetOrGenerateAttachContentId(AttachmentLink attachmentLink)
		{
			if (attachmentLink == null)
			{
				throw new ArgumentNullException("attachmentLink");
			}
			if (attachmentLink.AttachmentType == AttachmentType.Ole)
			{
				attachmentLink.ConvertToImage();
			}
			if (string.IsNullOrEmpty(attachmentLink.ContentId))
			{
				attachmentLink.ContentId = Guid.NewGuid().ToString();
			}
			attachmentLink.MarkInline(true);
			return attachmentLink.ContentId;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00099AFC File Offset: 0x00097CFC
		public static bool ApplyAttachmentsUpdates(Item item, ConversionCallbackBase converstionCallback)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (converstionCallback == null)
			{
				throw new ArgumentNullException("converstionCallback");
			}
			bool flag = false;
			item.OpenAsReadWrite();
			CalendarItemBase calendarItemBase = item as CalendarItemBase;
			if (calendarItemBase != null)
			{
				Utilities.ValidateCalendarItemBaseStoreObject(calendarItemBase);
			}
			try
			{
				flag = converstionCallback.SaveChanges();
				flag = true;
			}
			catch (AccessDeniedException)
			{
			}
			if (flag)
			{
				try
				{
					Utilities.SaveItem(item, false);
				}
				catch (AccessDeniedException)
				{
				}
			}
			return flag;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00099B78 File Offset: 0x00097D78
		public static void RemoveSmimeAttachment(ArrayList attachmentWellRenderObjects)
		{
			if (attachmentWellRenderObjects == null)
			{
				throw new ArgumentNullException("attachmentWellRenderObjects");
			}
			if (attachmentWellRenderObjects.Count == 0)
			{
				throw new ArgumentException("attachmentWellRenderObjects is empty.");
			}
			for (int i = 0; i < attachmentWellRenderObjects.Count; i++)
			{
				AttachmentWellInfo attachmentWellInfo = (AttachmentWellInfo)attachmentWellRenderObjects[i];
				if (string.Equals(attachmentWellInfo.FileName, "smime.p7m", StringComparison.OrdinalIgnoreCase) || string.Equals(attachmentWellInfo.MimeType, "multipart/signed", StringComparison.OrdinalIgnoreCase))
				{
					attachmentWellRenderObjects.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00099BF4 File Offset: 0x00097DF4
		public static int GetTotalAttachmentSize(AttachmentCollection attachmentCollection)
		{
			int num = 0;
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					num += (int)attachment.Size;
				}
			}
			return num;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00099C64 File Offset: 0x00097E64
		public static string TryGetMailboxIdentityName(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				throw new ArgumentNullException("httpContext");
			}
			string result = string.Empty;
			if (OwaContext.Get(httpContext).MailboxIdentity != null)
			{
				result = OwaContext.Get(httpContext).MailboxIdentity.SafeGetRenderableName();
			}
			return result;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00099CA4 File Offset: 0x00097EA4
		private static int GetMaximumFileSize(UserContext userContext)
		{
			int? maximumMessageSize = Utilities.GetMaximumMessageSize(userContext);
			if (maximumMessageSize == null)
			{
				return 5;
			}
			return maximumMessageSize.Value / 1024;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00099CD0 File Offset: 0x00097ED0
		private static int ToByteSize(int megaByteSize)
		{
			return megaByteSize * 1048576;
		}

		// Token: 0x040012E4 RID: 4836
		private const string DefaultSmimeAttachmentName = "smime.p7m";

		// Token: 0x040012E5 RID: 4837
		private const string MultiPartSigned = "multipart/signed";

		// Token: 0x040012E6 RID: 4838
		private const string ContentDispositionHeader = "Content-Disposition";

		// Token: 0x040012E7 RID: 4839
		public const string AttachmentPrefix = "&attid0=";

		// Token: 0x040012E8 RID: 4840
		public const string AttachmentContentIdPrefix = "&attcid0=";

		// Token: 0x040012E9 RID: 4841
		public const string AttachmentBaseUrl = "attachment.ashx?id=";

		// Token: 0x040012EA RID: 4842
		public const int AttachmentCopyBufferSize = 32768;

		// Token: 0x040012EB RID: 4843
		public const int DefaultMaxFileSize = 5;

		// Token: 0x040012EC RID: 4844
		public const int MaxAttachments = 499;

		// Token: 0x040012ED RID: 4845
		public const int EstimatedMessageOverheadExcludingAttachments = 65536;

		// Token: 0x040012EE RID: 4846
		public const string AuthoritativeTrueHeader = "; authoritative=true;";

		// Token: 0x040012EF RID: 4847
		public const string ContentType = "Content-Type";

		// Token: 0x040012F0 RID: 4848
		public const string XDownloadOptions = "X-Download-Options";

		// Token: 0x040012F1 RID: 4849
		public const string XDownloadOptionsNoOpen = "noopen";

		// Token: 0x040012F2 RID: 4850
		private const int DataSniffByteCount = 512;

		// Token: 0x040012F3 RID: 4851
		public static uint[] CrcTable = AttachmentUtility.GenerateCrc32Table();

		// Token: 0x040012F4 RID: 4852
		private static char[] directorySeparatorCharacters = new char[]
		{
			'\\',
			'/'
		};

		// Token: 0x040012F5 RID: 4853
		public static BufferPoolCollection.BufferSize CopyBufferSize = BufferPoolCollection.BufferSize.Size2K;

		// Token: 0x020002AA RID: 682
		[Flags]
		public enum AttachmentLinkFlags
		{
			// Token: 0x040012F7 RID: 4855
			None = 0,
			// Token: 0x040012F8 RID: 4856
			Skip = 1,
			// Token: 0x040012F9 RID: 4857
			OpenAsWebPageLink = 2,
			// Token: 0x040012FA RID: 4858
			AttachmentClickLink = 4
		}
	}
}
