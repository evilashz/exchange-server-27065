using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F65 RID: 3941
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemPartLoader
	{
		// Token: 0x060086D4 RID: 34516 RVA: 0x0024F4BD File Offset: 0x0024D6BD
		public ItemPartLoader(IXSOFactory xsofactory, IMailboxSession mailboxSession, bool canOpenIrmMessage, PropertyDefinition[] queriedPropertyDefinitions)
		{
			this.canOpenIrmMessage = canOpenIrmMessage;
			this.queriedPropertyDefinitions = queriedPropertyDefinitions;
			this.xsoFactory = xsofactory;
			this.mailboxSession = mailboxSession;
		}

		// Token: 0x060086D5 RID: 34517 RVA: 0x0024F4E2 File Offset: 0x0024D6E2
		public ItemPart Create(StoreObjectId itemId, IStorePropertyBag propertyBagFromTree, FragmentInfo uniqueBodyFragment, FragmentInfo disclaimerFragment, ParticipantTable recipients, IList<Participant> replyToParticipants)
		{
			return new ItemPart(itemId, (propertyBagFromTree.TryGetProperty(ItemSchema.Subject) as string) ?? string.Empty, uniqueBodyFragment, disclaimerFragment, recipients, replyToParticipants, propertyBagFromTree, this.queriedPropertyDefinitions);
		}

		// Token: 0x060086D6 RID: 34518 RVA: 0x0024F514 File Offset: 0x0024D714
		public LoadedItemPart Load(StoreId objectId, IStorePropertyBag propertyBagFromTree, HtmlStreamOptionCallback htmlCallback, PropertyDefinition[] propertyDefinitions, long bytesLoadedForConversation, bool isSmimeSupported, string domainName)
		{
			LoadedItemPart result;
			using (IItem item = this.xsoFactory.BindToItem(this.mailboxSession, objectId, propertyDefinitions))
			{
				ItemPartIrmInfo itemPartIrmInfo = this.GetItemPartIrmInfo(item);
				bool isIrmEnabled = itemPartIrmInfo != ItemPartIrmInfo.NotRestricted && this.canOpenIrmMessage;
				result = this.CreateLoadedItemPart(item, propertyBagFromTree, htmlCallback, propertyDefinitions, itemPartIrmInfo, isIrmEnabled, bytesLoadedForConversation, isSmimeSupported, domainName);
			}
			return result;
		}

		// Token: 0x060086D7 RID: 34519 RVA: 0x0024F584 File Offset: 0x0024D784
		private ItemPartIrmInfo GetItemPartIrmInfo(IItem item)
		{
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem == null || !rightsManagedMessageItem.IsRestricted)
			{
				return ItemPartIrmInfo.NotRestricted;
			}
			ItemPartIrmInfo result = null;
			if (!this.canOpenIrmMessage || !rightsManagedMessageItem.CanDecode)
			{
				if (!this.canOpenIrmMessage)
				{
					result = ItemPartIrmInfo.CreateForIrmDisabled();
				}
				else if (!rightsManagedMessageItem.CanDecode)
				{
					result = ItemPartIrmInfo.CreateForUnsupportedScenario();
				}
			}
			else
			{
				try
				{
					OutboundConversionOptions options = new OutboundConversionOptions(item.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), string.Empty);
					rightsManagedMessageItem.Decode(options, true);
					result = ItemPartIrmInfo.Create(rightsManagedMessageItem.UsageRights, rightsManagedMessageItem.Restriction.Name, rightsManagedMessageItem.Restriction.Description, rightsManagedMessageItem.ConversationOwner.EmailAddress, rightsManagedMessageItem.UserLicenseExpiryTime, rightsManagedMessageItem.Restriction.RequiresRepublishingWhenRecipientsChange, rightsManagedMessageItem.CanRepublish);
				}
				catch (RightsManagementPermanentException exception)
				{
					result = ItemPartIrmInfo.CreateForDecryptionFailure(exception);
				}
				catch (RightsManagementTransientException exception2)
				{
					result = ItemPartIrmInfo.CreateForDecryptionFailure(exception2);
				}
			}
			return result;
		}

		// Token: 0x060086D8 RID: 34520 RVA: 0x0024F67C File Offset: 0x0024D87C
		private LoadedItemPart CreateLoadedItemPart(IItem item, IStorePropertyBag propertyBagFromTree, HtmlStreamOptionCallback htmlCallback, PropertyDefinition[] additionalPropertyDefinitions, ItemPartIrmInfo itemPartIrmInfo, bool isIrmEnabled, long bytesLoadedForConversation, bool isSmimeSupported, string domainName)
		{
			ConversationBodyScanner bodyScanner = null;
			long bytesLoaded = 0L;
			PropertyDefinition[] loadedProperties = InternalSchema.Combine<PropertyDefinition>(this.queriedPropertyDefinitions, additionalPropertyDefinitions).ToArray<PropertyDefinition>();
			IStorePropertyBag propertyBag = this.CalculatePropertyBag(propertyBagFromTree, item, additionalPropertyDefinitions);
			BodyFragmentInfo bodyFragmentInfo = null;
			bool didLoadSucceed = false;
			if (this.TryLoadBodyScanner(item, htmlCallback, bytesLoadedForConversation, isIrmEnabled, out bodyScanner, out bytesLoaded))
			{
				bodyFragmentInfo = new BodyFragmentInfo(bodyScanner);
				didLoadSucceed = true;
			}
			AttachmentCollection attachmentCollection = item.AttachmentCollection;
			if (isIrmEnabled)
			{
				this.InitializeIrmInfo(item, itemPartIrmInfo, out attachmentCollection);
			}
			string itemClass = item.TryGetProperty(StoreObjectSchema.ItemClass) as string;
			if (isSmimeSupported && ObjectClass.IsSmimeClearSigned(itemClass))
			{
				this.InitializeSmimeInfo(item, domainName, out attachmentCollection);
			}
			return new LoadedItemPart(item, propertyBag, bodyFragmentInfo, loadedProperties, itemPartIrmInfo, didLoadSucceed, bytesLoaded, attachmentCollection);
		}

		// Token: 0x060086D9 RID: 34521 RVA: 0x0024F724 File Offset: 0x0024D924
		private void InitializeIrmInfo(IItem item, ItemPartIrmInfo itemPartIrmInfo, out AttachmentCollection attachmentCollection)
		{
			attachmentCollection = item.AttachmentCollection;
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted)
			{
				if (itemPartIrmInfo.DecryptionStatus.Failed || !rightsManagedMessageItem.IsDecoded)
				{
					attachmentCollection = null;
					return;
				}
				attachmentCollection = rightsManagedMessageItem.ProtectedAttachmentCollection;
				itemPartIrmInfo.BodyFormat = rightsManagedMessageItem.ProtectedBody.Format;
			}
		}

		// Token: 0x060086DA RID: 34522 RVA: 0x0024F780 File Offset: 0x0024D980
		private void InitializeSmimeInfo(IItem item, string domainName, out AttachmentCollection attachmentCollection)
		{
			attachmentCollection = item.AttachmentCollection;
			MessageItem messageItem = item as MessageItem;
			Item item2;
			if (messageItem != null && ItemConversion.TryOpenSMimeContent(messageItem, domainName, out item2))
			{
				attachmentCollection = item2.AttachmentCollection;
			}
		}

		// Token: 0x060086DB RID: 34523 RVA: 0x0024F7B4 File Offset: 0x0024D9B4
		private Body GetBodyFromRightsManagedMessageItem(IItem item)
		{
			Body result = null;
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			if (rightsManagedMessageItem != null && rightsManagedMessageItem.IsDecoded)
			{
				result = rightsManagedMessageItem.ProtectedBody;
			}
			return result;
		}

		// Token: 0x060086DC RID: 34524 RVA: 0x0024F7E0 File Offset: 0x0024D9E0
		private IStorePropertyBag CalculatePropertyBag(IStorePropertyBag queriedPropertyBag, IItem item, PropertyDefinition[] additionalPropertyDefinitions)
		{
			if (additionalPropertyDefinitions != null && additionalPropertyDefinitions.Length > 0)
			{
				ICollection<NativeStorePropertyDefinition> nativePropertyDefinitions = StorePropertyDefinition.GetNativePropertyDefinitions<PropertyDefinition>(PropertyDependencyType.NeedForRead, additionalPropertyDefinitions);
				PropertyDefinition[] array = nativePropertyDefinitions.ToArray<NativeStorePropertyDefinition>();
				QueryResultPropertyBag queryResultPropertyBag;
				if (queriedPropertyBag != null)
				{
					queryResultPropertyBag = new QueryResultPropertyBag(queriedPropertyBag, array, item.GetProperties(array));
				}
				else
				{
					queryResultPropertyBag = new QueryResultPropertyBag((StoreSession)item.Session, array);
					queryResultPropertyBag.SetQueryResultRow(item.GetProperties(array));
				}
				return queryResultPropertyBag.AsIStorePropertyBag();
			}
			if (queriedPropertyBag != null)
			{
				return queriedPropertyBag;
			}
			return null;
		}

		// Token: 0x060086DD RID: 34525 RVA: 0x0024F848 File Offset: 0x0024DA48
		private bool TryLoadBodyScanner(IItem item, HtmlStreamOptionCallback callback, long bytesLoadedForConversation, bool isIrmEnabled, out ConversationBodyScanner bodyScanner, out long bytesRead)
		{
			bodyScanner = null;
			Item item2 = null;
			bytesRead = 0L;
			Body body = item.Body;
			if (body == null)
			{
				ExTraceGlobals.ConversationTracer.TraceError(0L, "ConversationDataExtractor::LoadBodyScanner: ConversationItem has no body");
				return false;
			}
			try
			{
				if (ObjectClass.IsSmime(item.ClassName) && !ObjectClass.IsSmimeClearSigned(item.ClassName) && item is MessageItem)
				{
					if (ItemConversion.TryOpenSMimeContent((MessageItem)item, ConvertUtils.GetInboundConversionOptions(), out item2))
					{
						body = item2.Body;
					}
				}
				else if (isIrmEnabled)
				{
					body = this.GetBodyFromRightsManagedMessageItem(item);
				}
				if (body != null)
				{
					if (callback != null)
					{
						KeyValuePair<HtmlStreamingFlags, HtmlCallbackBase> keyValuePair = callback((Item)item);
						bodyScanner = body.GetConversationBodyScanner(keyValuePair.Value, ConversationDataExtractor.MaxBytesForConversation, bytesLoadedForConversation, true, keyValuePair.Key == HtmlStreamingFlags.FilterHtml, out bytesRead);
					}
					else
					{
						bodyScanner = body.GetConversationBodyScanner(null, ConversationDataExtractor.MaxBytesForConversation, bytesLoadedForConversation, true, true, out bytesRead);
					}
				}
				else
				{
					ExTraceGlobals.ConversationTracer.TraceError(0L, "ConversationDataExtractor::LoadBodyScanner: ConversationItem has no body");
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.ConversationTracer.TraceError(0L, "ObjectNotFoundException thrown from ConversationDataExtractor::LoadBodyScanner");
				bodyScanner = null;
				bytesRead = 0L;
			}
			catch (TextConvertersException)
			{
				ExTraceGlobals.ConversationTracer.TraceError(0L, "TextConvertersException thrown from Body::GetConversationBodyScanner");
				bodyScanner = null;
				bytesRead = 0L;
			}
			catch (MessageLoadFailedInConversationException)
			{
				ExTraceGlobals.ConversationTracer.TraceError(0L, "MessageLoadFailedInConversationException thrown from Body::GetConversationBodyScanner");
				bodyScanner = null;
				bytesRead = 0L;
			}
			finally
			{
				if (item2 != null)
				{
					item2.Dispose();
					item2 = null;
				}
			}
			return bodyScanner != null;
		}

		// Token: 0x04005A25 RID: 23077
		private readonly bool canOpenIrmMessage;

		// Token: 0x04005A26 RID: 23078
		private readonly PropertyDefinition[] queriedPropertyDefinitions;

		// Token: 0x04005A27 RID: 23079
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04005A28 RID: 23080
		private readonly IMailboxSession mailboxSession;
	}
}
