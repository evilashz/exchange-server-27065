using System;
using System.IO;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Internal;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Conversations;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Conversations
{
	// Token: 0x02000029 RID: 41
	internal static class LicensingProcessor
	{
		// Token: 0x06000129 RID: 297 RVA: 0x000073FB File Offset: 0x000055FB
		public static bool IsEventInteresting(MapiEvent mapiEvent, MailboxData mailboxData)
		{
			return LicensingProcessor.IsMessageCreatedInSentItems(mapiEvent, mailboxData);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007404 File Offset: 0x00005604
		public static void HandleEventInternal(MailboxSession session, StoreObject storeItem)
		{
			LicensingProcessor.Tracer.TraceDebug(0L, "{0}: Calling LicensingProcessor.HandleEventInternal", new object[]
			{
				TraceContext.Get()
			});
			LicensingProcessor.ProcessSentItemMessage(session, storeItem);
			LicensingProcessor.Tracer.TraceDebug(0L, "{0}: Done LicensingProcessor.HandleEventInternal", new object[]
			{
				TraceContext.Get()
			});
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000745C File Offset: 0x0000565C
		private static bool IsMessageCreatedInSentItems(MapiEvent mapiEvent, MailboxData mailboxData)
		{
			if (mapiEvent.ItemType != ObjectType.MAPI_MESSAGE)
			{
				return false;
			}
			if ((mapiEvent.EventFlags & MapiEventFlags.FolderAssociated) != MapiEventFlags.None)
			{
				return false;
			}
			if (!mapiEvent.ExtendedEventFlags.Contains(MapiExtendedEventFlags.IrmRestrictedItem))
			{
				return false;
			}
			if (mapiEvent.ItemEntryId == null || mapiEvent.ParentEntryId == null)
			{
				LicensingProcessor.Tracer.TraceError(0L, "{0}: Found item without entry Id or parent entry Id", new object[]
				{
					TraceContext.Get()
				});
				return false;
			}
			if (!ObjectClass.IsOfClass(mapiEvent.ObjectClass, "IPM.Note"))
			{
				return false;
			}
			if (mapiEvent.ClientType == MapiEventClientTypes.Transport)
			{
				if ((mapiEvent.EventMask & MapiEventTypeFlags.ObjectCreated) != (MapiEventTypeFlags)0)
				{
					if (mailboxData == null)
					{
						return true;
					}
					DefaultFolderType defaultFolderType = mailboxData.MatchCachedDefaultFolderType(mapiEvent.ParentEntryId);
					if (defaultFolderType == DefaultFolderType.SentItems)
					{
						LicensingProcessor.Tracer.TraceDebug(0L, "{0}: Found item created in sent items by Transport", new object[]
						{
							TraceContext.Get()
						});
						return true;
					}
				}
			}
			else if ((mapiEvent.ClientType == MapiEventClientTypes.MOMT || mapiEvent.ClientType == MapiEventClientTypes.User || mapiEvent.ClientType == MapiEventClientTypes.RpcHttp) && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectMoved) != (MapiEventTypeFlags)0)
			{
				if (mailboxData == null)
				{
					return true;
				}
				DefaultFolderType defaultFolderType2 = mailboxData.MatchCachedDefaultFolderType(mapiEvent.ParentEntryId);
				DefaultFolderType defaultFolderType3 = mailboxData.MatchCachedDefaultFolderType(mapiEvent.OldParentEntryId);
				if (defaultFolderType2 == DefaultFolderType.SentItems && defaultFolderType3 == DefaultFolderType.Outbox)
				{
					LicensingProcessor.Tracer.TraceDebug(0L, "{0}: Found item moved to sent items by Outlook", new object[]
					{
						TraceContext.Get()
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000075AC File Offset: 0x000057AC
		private static void ProcessSentItemMessage(MailboxSession session, StoreObject storeItem)
		{
			MessageItem messageItem = storeItem as MessageItem;
			if (messageItem == null)
			{
				LicensingProcessor.Tracer.TraceError(0L, "{0}: HandleEventInternal: item is not MessageItem", new object[]
				{
					TraceContext.Get()
				});
				return;
			}
			if (LicensingProcessor.ShouldLicenseMessage(messageItem) && session.MailboxOwner != null && LicensingProcessor.ShouldLicenseForOrganization(session.MailboxOwner.MailboxInfo.OrganizationId))
			{
				LicensingProcessor.Tracer.TraceDebug<object, VersionedId>(0L, "{0}: Licensing message.{1}", TraceContext.Get(), messageItem.Id);
				bool flag = false;
				try
				{
					LicensingProcessor.LicenseMessage(session.MailboxOwner, messageItem);
					flag = true;
					LicensingProcessor.Tracer.TraceDebug(0L, "{0}: End Licensing message.", new object[]
					{
						TraceContext.Get()
					});
				}
				catch (ObjectNotFoundException)
				{
				}
				finally
				{
					if (!flag)
					{
						LicensingProcessor.Tracer.TraceError(0L, "{0}: LicensingProcessor: SentItems: Failed to license message.", new object[]
						{
							TraceContext.Get()
						});
					}
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000076AC File Offset: 0x000058AC
		private static bool ShouldLicenseForOrganization(OrganizationId orgId)
		{
			bool result;
			try
			{
				result = (RmsClientManager.IRMConfig.IsClientAccessServerEnabledForTenant(orgId) || RmsClientManager.IRMConfig.IsSearchEnabledForTenant(orgId));
			}
			catch (ExchangeConfigurationException innerException)
			{
				throw new StorageTransientException(Strings.FailedToReadIRMConfig, innerException);
			}
			return result;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000076F8 File Offset: 0x000058F8
		private static bool ShouldLicenseMessage(MessageItem item)
		{
			return item.IsRestricted && (PropertyError.IsPropertyNotFound(item.TryGetProperty(MessageItemSchema.DRMRights)) || PropertyError.IsPropertyNotFound(item.TryGetProperty(MessageItemSchema.DRMExpiryTime)));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007734 File Offset: 0x00005934
		private static void LicenseMessage(IExchangePrincipal userPrincipal, MessageItem item)
		{
			string publishLicense;
			using (Attachment attachment = item.AttachmentCollection.Open(item.AttachmentCollection.GetHandles()[0]))
			{
				using (Stream contentStream = ((StreamAttachmentBase)attachment).GetContentStream(PropertyOpenMode.ReadOnly))
				{
					using (DrmEmailMessageContainer drmEmailMessageContainer = new DrmEmailMessageContainer())
					{
						try
						{
							drmEmailMessageContainer.Load(contentStream, (object param0) => Streams.CreateTemporaryStorageStream());
							publishLicense = drmEmailMessageContainer.PublishLicense;
						}
						catch (InvalidRpmsgFormatException arg)
						{
							LicensingProcessor.Tracer.TraceError<object, InvalidRpmsgFormatException>(0L, "{0}: LicensingProcessor:Failed to load RPMSG. {1}", TraceContext.Get(), arg);
							return;
						}
					}
				}
			}
			UseLicenseAndUsageRights useLicenseAndUsageRights;
			try
			{
				useLicenseAndUsageRights = RmsClientManager.AcquireUseLicenseAndUsageRights(new RmsClientManagerContext(userPrincipal.MailboxInfo.OrganizationId, RmsClientManagerContext.ContextId.MessageId, item.InternetMessageId, publishLicense), publishLicense, userPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), userPrincipal.Sid, userPrincipal.RecipientTypeDetails);
			}
			catch (RightsManagementException ex)
			{
				LicensingProcessor.Tracer.TraceError<object, RightsManagementException>(0L, "{0}: LicensingProcessor:Failed to license message. {1}", TraceContext.Get(), ex);
				if (!ex.IsPermanent)
				{
					throw new StorageTransientException(Strings.FailedToAcquireUseLicense, ex);
				}
				return;
			}
			catch (ExchangeConfigurationException ex2)
			{
				LicensingProcessor.Tracer.TraceError<object, ExchangeConfigurationException>(0L, "{0}: LicensingProcessor:Failed to license message. {1}", TraceContext.Get(), ex2);
				throw new StorageTransientException(Strings.FailedToReadIRMConfig, ex2);
			}
			LicensingProcessor.Tracer.TraceDebug(0L, "{0}: LicensingProcesssor: Saving licenses", new object[]
			{
				TraceContext.Get()
			});
			item.OpenAsReadWrite();
			item[MessageItemSchema.DRMRights] = useLicenseAndUsageRights.UsageRights;
			item[MessageItemSchema.DRMExpiryTime] = useLicenseAndUsageRights.ExpiryTime;
			if (!DrmClientUtils.IsCachingOfLicenseDisabled(useLicenseAndUsageRights.UseLicense))
			{
				using (Stream stream = item.OpenPropertyStream(MessageItemSchema.DRMServerLicenseCompressed, PropertyOpenMode.Create))
				{
					DrmEmailCompression.CompressUseLicense(useLicenseAndUsageRights.UseLicense, stream);
				}
			}
			item[MessageItemSchema.DRMPropsSignature] = useLicenseAndUsageRights.DRMPropsSignature;
			item.Save(SaveMode.ResolveConflicts);
			LicensingProcessor.Tracer.TraceDebug(0L, "{0}: LicensingProcesssor: message was saved successfully", new object[]
			{
				TraceContext.Get()
			});
		}

		// Token: 0x04000122 RID: 290
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
