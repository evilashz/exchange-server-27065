using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.E4E;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.ApplicationLogic;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002D6 RID: 726
	internal static class EncryptionConfigurationHelper
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x00064CE8 File Offset: 0x00062EE8
		private static bool NewMessageItem(MailboxSession mailboxSession, string xml)
		{
			StoreId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			bool result;
			using (MessageItem messageItem = MessageItem.Create(mailboxSession, defaultFolderId))
			{
				messageItem.Subject = "Encryption Configuration";
				using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextPlain))
				{
					textWriter.Write(xml);
				}
				messageItem.IsDraft = false;
				messageItem[ItemSchema.ReceivedTime] = DateTime.UtcNow;
				messageItem[StoreObjectSchema.ItemClass] = "Encryption Configuration";
				ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult.SaveStatus == SaveResult.Success || conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution)
				{
					result = true;
				}
				else
				{
					EncryptionConfigurationHelper.Tracer.TraceError<SaveResult>(0L, "In NewMessageItem, messageItem.Save failed. Status: {0}", conflictResolutionResult.SaveStatus);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00064DC0 File Offset: 0x00062FC0
		private static MessageItem GetMessageItem(MailboxSession mailboxSession)
		{
			StoreId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Inbox);
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(defaultFolderId);
			MessageItem result;
			using (Folder folder = Folder.Bind(mailboxSession, storeObjectId))
			{
				ComparisonFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "Encryption Configuration");
				PropertyDefinition[] dataColumns = new PropertyDefinition[]
				{
					ItemSchema.Id
				};
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, queryFilter, null, dataColumns))
				{
					object[][] rows = queryResult.GetRows(2);
					if (rows.Length != 1)
					{
						if (rows.Length == 0)
						{
							EncryptionConfigurationHelper.Tracer.TraceDebug<string>(0L, "Retrieved zero messages with item class: {0}.", "Encryption Configuration");
						}
						else
						{
							EncryptionConfigurationHelper.Tracer.TraceError<int, string>(0L, "Retrieved {0} messages with item class: {1}. Only one expected.", rows.Length, "Encryption Configuration");
						}
						result = null;
					}
					else
					{
						StoreObjectId objectId = ((VersionedId)rows[0][0]).ObjectId;
						MessageItem messageItem = Item.BindAsMessage(mailboxSession, objectId);
						PropertyDefinition[] properties = new PropertyDefinition[]
						{
							ItemSchema.TextBody
						};
						messageItem.Load(properties);
						result = messageItem;
					}
				}
			}
			return result;
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00064EDC File Offset: 0x000630DC
		private static string GetExceptionMessages(Exception e)
		{
			StringBuilder stringBuilder = new StringBuilder(2 * e.Message.Length);
			do
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("|");
				}
				stringBuilder.Append(e.Message);
				e = e.InnerException;
			}
			while (e != null);
			return stringBuilder.ToString();
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00064F30 File Offset: 0x00063130
		internal static EncryptionConfigurationData GetEncryptionConfigurationData(MailboxSession mailboxSession)
		{
			MessageItem messageItem = EncryptionConfigurationHelper.GetMessageItem(mailboxSession);
			if (messageItem != null)
			{
				using (TextReader textReader = messageItem.Body.OpenTextReader(BodyFormat.TextPlain))
				{
					string serializedXML = textReader.ReadToEnd();
					messageItem.Dispose();
					return EncryptionConfigurationData.Deserialize(serializedXML);
				}
			}
			return new EncryptionConfigurationData();
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00064F8C File Offset: 0x0006318C
		internal static bool SetMessageItem(MailboxSession mailboxSession, string xml)
		{
			bool result;
			using (MessageItem messageItem = EncryptionConfigurationHelper.GetMessageItem(mailboxSession))
			{
				if (messageItem == null)
				{
					result = EncryptionConfigurationHelper.NewMessageItem(mailboxSession, xml);
				}
				else
				{
					messageItem.OpenAsReadWrite();
					using (TextWriter textWriter = messageItem.Body.OpenTextWriter(BodyFormat.TextPlain))
					{
						textWriter.Write(xml);
					}
					messageItem.IsDraft = false;
					messageItem[ItemSchema.ReceivedTime] = DateTime.UtcNow;
					messageItem[StoreObjectSchema.ItemClass] = "Encryption Configuration";
					ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
					if (conflictResolutionResult.SaveStatus == SaveResult.Success || conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution)
					{
						result = true;
					}
					else
					{
						EncryptionConfigurationHelper.Tracer.TraceError<SaveResult>(0L, "In SetMessageItem, messageItem.Save failed. Status: {0}", conflictResolutionResult.SaveStatus);
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00065064 File Offset: 0x00063264
		internal static ServiceError GetServiceError(Exception e)
		{
			ResponseCodeType messageKey = (e is TransientException) ? ResponseCodeType.ErrorInternalServerTransientError : ResponseCodeType.ErrorInternalServerError;
			string exceptionMessages = EncryptionConfigurationHelper.GetExceptionMessages(e);
			return new ServiceError(exceptionMessages, messageKey, 0, ExchangeVersion.Exchange2012);
		}

		// Token: 0x04000D89 RID: 3465
		private const string MessageSubject = "Encryption Configuration";

		// Token: 0x04000D8A RID: 3466
		private const string ItemClass = "Encryption Configuration";

		// Token: 0x04000D8B RID: 3467
		private static readonly Trace Tracer = ExTraceGlobals.E4ETracer;
	}
}
