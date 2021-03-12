using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000057 RID: 87
	internal class MapiMessageSubmitter : IMapiMessageSubmitter
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000C034 File Offset: 0x0000A234
		private MapiMessageSubmitter()
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C03C File Offset: 0x0000A23C
		public static MapiMessageSubmitter CreateInstance()
		{
			return new MapiMessageSubmitter();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C043 File Offset: 0x0000A243
		public void SendMapiMessage(string lamNotificationId, SendMapiMailDefinition mapiMailDefinition, out string entryId, out string internetMessageId, out Guid senderMbxGuid)
		{
			this.SendMapiMessageHelper(true, lamNotificationId, mapiMailDefinition, true, out entryId, out internetMessageId, out senderMbxGuid);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C054 File Offset: 0x0000A254
		public void SendMapiMessage(SendMapiMailDefinition mapiMailDefinition, out string entryId, out string internetMessageId, out Guid senderMbxGuid)
		{
			this.SendMapiMessageHelper(false, null, mapiMailDefinition, true, out entryId, out internetMessageId, out senderMbxGuid);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C064 File Offset: 0x0000A264
		public DeletionResult DeleteMessageFromOutbox(DeleteMapiMailDefinition deleteMapiMailDefinition)
		{
			return this.DeleteMessageFromOutboxHelper(deleteMapiMailDefinition);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C070 File Offset: 0x0000A270
		private static string EntryIdString(byte[] entryId)
		{
			string result = null;
			if (entryId != null)
			{
				StringBuilder stringBuilder = new StringBuilder(2 * entryId.Length);
				foreach (byte b in entryId)
				{
					stringBuilder.AppendFormat("{0:X} ", b);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		private void SendMapiMessageHelper(bool setLamNotificationId, string lamNotificationId, SendMapiMailDefinition mapiMailDefinition, bool returnOutParameters, out string entryId, out string internetMessageId, out Guid senderMbxGuid)
		{
			entryId = string.Empty;
			internetMessageId = string.Empty;
			senderMbxGuid = Guid.Empty;
			ExchangePrincipal mailboxOwner;
			if (mapiMailDefinition.SenderMbxGuid != Guid.Empty)
			{
				mailboxOwner = ExchangePrincipal.FromMailboxData(mapiMailDefinition.SenderMbxGuid, mapiMailDefinition.SenderMdbGuid, new List<CultureInfo>(0));
			}
			else
			{
				string domainPartOfEmailAddress = MapiMessageSubmitter.GetDomainPartOfEmailAddress(mapiMailDefinition.SenderEmailAddress);
				mailboxOwner = ExchangePrincipal.FromProxyAddress(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domainPartOfEmailAddress), mapiMailDefinition.SenderEmailAddress);
			}
			using (MailboxSession mailboxSession = MailboxSession.OpenAsTransport(mailboxOwner, "Client=Monitoring;Action=MapiSubmitLAMProbe"))
			{
				using (MessageItem messageItem = MessageItem.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.Outbox)))
				{
					messageItem.ClassName = mapiMailDefinition.MessageClass;
					if (setLamNotificationId)
					{
						messageItem[MessageItemSchema.MapiSubmitLamNotificationId] = lamNotificationId;
						messageItem[MessageItemSchema.XLAMNotificationId] = lamNotificationId;
					}
					messageItem[MessageItemSchema.DoNotDeliver] = (mapiMailDefinition.DoNotDeliver ? "Supress" : "Deliver");
					if (mapiMailDefinition.DropMessageInHub)
					{
						messageItem[MessageItemSchema.DropMessageInHub] = "FrontEnd-CAT-250";
						messageItem[MessageItemSchema.SystemProbeDrop] = "OnEndOfHeaders";
					}
					messageItem.Subject = mapiMailDefinition.MessageSubject;
					BodyWriteConfiguration configuration = new BodyWriteConfiguration(BodyFormat.TextPlain);
					using (TextWriter textWriter = messageItem.Body.OpenTextWriter(configuration))
					{
						textWriter.Write(mapiMailDefinition.MessageBody);
					}
					messageItem.Recipients.Add(new Participant(mapiMailDefinition.RecipientEmailAddress, mapiMailDefinition.RecipientEmailAddress, "SMTP"), RecipientItemType.To);
					if (mapiMailDefinition.DeleteAfterSubmit)
					{
						messageItem.SendWithoutSavingMessage();
					}
					else
					{
						messageItem.Send();
					}
					if (returnOutParameters)
					{
						senderMbxGuid = mailboxSession.MailboxGuid;
						messageItem.Load(new PropertyDefinition[]
						{
							StoreObjectSchema.EntryId,
							ItemSchema.InternetMessageId
						});
						object obj = messageItem.TryGetProperty(StoreObjectSchema.EntryId);
						byte[] entryId2 = (byte[])obj;
						entryId = MapiMessageSubmitter.EntryIdString(entryId2);
						object obj2 = messageItem.TryGetProperty(ItemSchema.InternetMessageId);
						internetMessageId = obj2.ToString();
					}
				}
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000C304 File Offset: 0x0000A504
		private DeletionResult DeleteMessageFromOutboxHelper(DeleteMapiMailDefinition deleteMapiMailDefinition)
		{
			DeletionResult result = DeletionResult.Fail;
			string domainPartOfEmailAddress = MapiMessageSubmitter.GetDomainPartOfEmailAddress(deleteMapiMailDefinition.SenderEmailAddress);
			ExchangePrincipal mailboxOwner = ExchangePrincipal.FromProxyAddress(ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domainPartOfEmailAddress), deleteMapiMailDefinition.SenderEmailAddress);
			QueryFilter queryFilter = new AndFilter(new List<QueryFilter>(2)
			{
				new TextFilter(ItemSchema.InternetMessageId, deleteMapiMailDefinition.InternetMessageId, MatchOptions.FullString, MatchFlags.IgnoreCase),
				new TextFilter(StoreObjectSchema.ItemClass, deleteMapiMailDefinition.MessageClass, MatchOptions.FullString, MatchFlags.IgnoreCase)
			}.ToArray());
			using (MailboxSession mailboxSession = MailboxSession.OpenAsTransport(mailboxOwner, "Client=Monitoring;Action=MapiSubmitLAMProbe"))
			{
				using (Folder folder = Folder.Bind(mailboxSession, DefaultFolderType.Outbox))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, new PropertyDefinition[]
					{
						ItemSchema.Id
					}))
					{
						object[][] rows = queryResult.GetRows(1);
						if (rows == null || rows.Length == 0)
						{
							result = DeletionResult.NoMatchingMessage;
						}
						else
						{
							mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
							{
								((VersionedId)rows[0][0]).ObjectId
							});
							result = DeletionResult.Success;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000C444 File Offset: 0x0000A644
		private static string GetDomainPartOfEmailAddress(string emailAddress)
		{
			SmtpAddress smtpAddress = new SmtpAddress(emailAddress);
			if (!smtpAddress.IsValidAddress)
			{
				throw new ArgumentException(string.Format("Email address: {0} is invalid while trying to extract the domain part", emailAddress));
			}
			return smtpAddress.Domain;
		}
	}
}
