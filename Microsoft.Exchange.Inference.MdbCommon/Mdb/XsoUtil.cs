using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x0200001E RID: 30
	internal static class XsoUtil
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004454 File Offset: 0x00002654
		internal static string GetDefaultFolderName(MailboxSession session, StoreObjectId folderId)
		{
			DefaultFolderType defaultFolderType = session.IsDefaultFolderType(folderId);
			return XsoUtil.GetDefaultFolderName(defaultFolderType);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004470 File Offset: 0x00002670
		internal static string GetDefaultFolderName(DefaultFolderType defaultFolderType)
		{
			switch (defaultFolderType)
			{
			case DefaultFolderType.DeletedItems:
				return ClientStrings.DeletedItems;
			case DefaultFolderType.Drafts:
				return ClientStrings.Drafts;
			case DefaultFolderType.Inbox:
				return ClientStrings.Inbox;
			case DefaultFolderType.JunkEmail:
				return ClientStrings.JunkEmail;
			case DefaultFolderType.Journal:
			case DefaultFolderType.Notes:
			case DefaultFolderType.Tasks:
			case DefaultFolderType.Reminders:
				break;
			case DefaultFolderType.Outbox:
				return ClientStrings.Outbox;
			case DefaultFolderType.SentItems:
				return ClientStrings.SentItems;
			case DefaultFolderType.Conflicts:
				return ClientStrings.Conflicts;
			default:
				if (defaultFolderType == DefaultFolderType.RecoverableItemsDeletions)
				{
					return ClientStrings.DeletedItems;
				}
				break;
			}
			return "Other";
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004518 File Offset: 0x00002718
		internal static AttachmentType ConvertFromXSOAttachmentType(AttachmentType attachmentType)
		{
			switch (attachmentType)
			{
			case AttachmentType.NoAttachment:
				return AttachmentType.NoAttachment;
			case AttachmentType.Stream:
				return AttachmentType.Stream;
			case AttachmentType.EmbeddedMessage:
				return AttachmentType.EmbeddedMessage;
			case AttachmentType.Ole:
				return AttachmentType.Ole;
			default:
				return AttachmentType.Unknown;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004548 File Offset: 0x00002748
		internal static bool CheckResponseBasedOnType(object lastVerb, ItemResponseType responseType)
		{
			if (lastVerb is PropertyError)
			{
				return false;
			}
			int num = (int)lastVerb;
			if (responseType == ItemResponseType.RepliedTo)
			{
				return num == 103 || num == 102 || num == 108 || (num >= 1 && num <= 31);
			}
			if (responseType == ItemResponseType.Forwarded)
			{
				return num == 104;
			}
			throw new ArgumentException("Invalid responseType.");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000045A0 File Offset: 0x000027A0
		internal static TResult MapXsoExceptions<TResult>(Func<TResult> call)
		{
			Exception innerException = null;
			try
			{
				return call();
			}
			catch (StorageTransientException ex)
			{
				innerException = ex;
			}
			catch (StoragePermanentException ex2)
			{
				innerException = ex2;
			}
			catch (TextConvertersException ex3)
			{
				innerException = ex3;
			}
			throw new OperationFailedException(innerException);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000045FC File Offset: 0x000027FC
		internal static void MapXsoExceptions(Action call)
		{
			Exception ex = null;
			try
			{
				call();
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			catch (TextConvertersException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				throw new OperationFailedException(ex);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004654 File Offset: 0x00002854
		internal static UserConfiguration ResetModel(string userConfigurationName, UserConfigurationTypes userConfigType, MailboxSession session, bool deleteOld, IDiagnosticsSession diagnosticSession)
		{
			StoreId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Inbox);
			OperationResult operationResult = OperationResult.Succeeded;
			Exception ex = null;
			if (deleteOld)
			{
				try
				{
					operationResult = session.UserConfigurationManager.DeleteFolderConfigurations(defaultFolderId, new string[]
					{
						userConfigurationName
					});
				}
				catch (ObjectNotFoundException ex2)
				{
					ex = ex2;
					if (diagnosticSession != null)
					{
						diagnosticSession.TraceDebug<string, ObjectNotFoundException>("FAI message '{0}' is missing. Exception: {1}", userConfigurationName, ex2);
					}
				}
				if (operationResult != OperationResult.Succeeded && ex == null)
				{
					if (diagnosticSession != null)
					{
						diagnosticSession.TraceError(string.Format("Deletion of user configuration (userConfiguration Name = {0}) failed. OperationResult = {1}. ObjectNotFoundException = {2}", userConfigurationName, operationResult.ToString(), ex), new object[0]);
					}
					throw new DeleteItemsException(string.Format("Deletion of user configuration (userConfiguration Name = {0}) failed. OperationResult = {1}. ObjectNotFoundException = {2}", userConfigurationName, operationResult.ToString(), ex));
				}
			}
			UserConfiguration userConfiguration = null;
			try
			{
				userConfiguration = session.UserConfigurationManager.CreateFolderConfiguration(userConfigurationName, userConfigType, defaultFolderId);
				userConfiguration.Save();
			}
			catch (Exception ex3)
			{
				if (diagnosticSession != null && !(ex3 is QuotaExceededException))
				{
					if (ex3 is StoragePermanentException)
					{
						diagnosticSession.SendInformationalWatsonReport(ex3, string.Format("Creation of user configuration failed (userConfiguration Name = {0}) deleteOld flag was {1}. Result of deletion of user configuration OperationResult = {2}. ObjectNotFoundException = {3}", new object[]
						{
							userConfigurationName,
							deleteOld,
							deleteOld ? operationResult.ToString() : "Not Applicable",
							ex
						}));
					}
					else
					{
						diagnosticSession.TraceError("Creation of user configuration failed (userConfiguration Name = {0}) deleteOld flag was {1}. Result of deletion of user configuration OperationResult = {2}. ObjectNotFoundException = {3}. Exception = {4}", new object[]
						{
							userConfigurationName,
							deleteOld,
							deleteOld ? operationResult.ToString() : "Not Applicable",
							ex,
							ex3.ToString()
						});
					}
				}
				if (userConfiguration != null)
				{
					userConfiguration.Dispose();
				}
				throw;
			}
			return userConfiguration;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004814 File Offset: 0x00002A14
		internal static TReturnValue TranslateXsoExceptionsWithReturnValue<TReturnValue>(IDiagnosticsSession tracer, LocalizedString errorString, XsoUtil.XsoExceptionHandlingFlags flags, Func<TReturnValue> xsoCall)
		{
			TReturnValue result = default(TReturnValue);
			XsoUtil.TranslateXsoExceptions(tracer, errorString, flags, delegate()
			{
				result = xsoCall();
			});
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004854 File Offset: 0x00002A54
		internal static TReturnValue TranslateXsoExceptionsWithReturnValue<TReturnValue>(IDiagnosticsSession tracer, LocalizedString errorString, Func<TReturnValue> xsoCall)
		{
			return XsoUtil.TranslateXsoExceptionsWithReturnValue<TReturnValue>(tracer, errorString, XsoUtil.XsoExceptionHandlingFlags.None, xsoCall);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000485F File Offset: 0x00002A5F
		internal static void TranslateXsoExceptions(IDiagnosticsSession tracer, LocalizedString errorString, Action xsoCall)
		{
			XsoUtil.TranslateXsoExceptions(tracer, errorString, XsoUtil.XsoExceptionHandlingFlags.None, xsoCall);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000486C File Offset: 0x00002A6C
		internal static void TranslateXsoExceptions(IDiagnosticsSession tracer, LocalizedString errorString, XsoUtil.XsoExceptionHandlingFlags flags, Action xsoCall)
		{
			try
			{
				xsoCall();
			}
			catch (ConnectionFailedTransientException ex)
			{
				XsoUtil.TraceAndThrowTransientException(tracer, errorString, ex);
			}
			catch (ConnectionFailedPermanentException ex2)
			{
				XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex2);
			}
			catch (MailboxUnavailableException ex3)
			{
				XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex3);
			}
			catch (ObjectNotFoundException ex4)
			{
				if ((flags & XsoUtil.XsoExceptionHandlingFlags.DoNotExpectObjectNotFound) == XsoUtil.XsoExceptionHandlingFlags.DoNotExpectObjectNotFound)
				{
					tracer.SendInformationalWatsonReport(ex4, null);
					if ((flags & XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions) == XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions)
					{
						XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex4);
					}
				}
				else
				{
					tracer.TraceDebug<LocalizedString, ObjectNotFoundException>("Error: {0}, exception: {1}", errorString, ex4);
				}
			}
			catch (CorruptDataException ex5)
			{
				if ((flags & XsoUtil.XsoExceptionHandlingFlags.DoNotExpectCorruptData) == XsoUtil.XsoExceptionHandlingFlags.DoNotExpectCorruptData)
				{
					tracer.SendInformationalWatsonReport(ex5, null);
					if ((flags & XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions) == XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions)
					{
						XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex5);
					}
				}
				else
				{
					tracer.TraceDebug<LocalizedString, CorruptDataException>("Error: {0}, exception: {1}", errorString, ex5);
				}
			}
			catch (AccessDeniedException ex6)
			{
				tracer.SendWatsonReport(ex6);
				XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex6);
			}
			catch (StoragePermanentException ex7)
			{
				if (ex7.GetType() != typeof(StoragePermanentException))
				{
					tracer.SendInformationalWatsonReport(ex7, null);
				}
				if ((flags & XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions) == XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions)
				{
					XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex7);
				}
			}
			catch (StorageTransientException ex8)
			{
				if (ex8.GetType() != typeof(StorageTransientException))
				{
					tracer.SendInformationalWatsonReport(ex8, null);
				}
				if ((flags & XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions) == XsoUtil.XsoExceptionHandlingFlags.RethrowUnexpectedExceptions)
				{
					XsoUtil.TraceAndThrowPermanentException(tracer, errorString, ex8);
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000049F4 File Offset: 0x00002BF4
		private static void TraceAndThrowTransientException(IDiagnosticsSession tracer, LocalizedString errorString, LocalizedException ex)
		{
			tracer.TraceError<LocalizedString, LocalizedException>("Error: {0}, exception: {1}", errorString, ex);
			throw new ComponentFailedTransientException(errorString, ex);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004A0A File Offset: 0x00002C0A
		private static void TraceAndThrowPermanentException(IDiagnosticsSession tracer, LocalizedString errorString, LocalizedException ex)
		{
			tracer.TraceError<LocalizedString, LocalizedException>("Error: {0}, exception: {1}", errorString, ex);
			throw new ComponentFailedPermanentException(errorString, ex);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004D80 File Offset: 0x00002F80
		internal static IEnumerable<StoreObjectId> GetSubfolders(IDiagnosticsSession tracer, Folder parentFolder, QueryFilter filter)
		{
			XsoUtil.<>c__DisplayClass6 CS$<>8__locals1 = new XsoUtil.<>c__DisplayClass6();
			CS$<>8__locals1.parentFolder = parentFolder;
			CS$<>8__locals1.filter = filter;
			Util.ThrowOnNullArgument(CS$<>8__locals1.parentFolder, "parentFolder");
			Guid mailboxGuid = CS$<>8__locals1.parentFolder.Session.MailboxGuid;
			using (QueryResult queryResult = XsoUtil.TranslateXsoExceptionsWithReturnValue<QueryResult>(tracer, Strings.ConnectionToMailboxFailed(mailboxGuid), () => CS$<>8__locals1.parentFolder.FolderQuery(FolderQueryFlags.DeepTraversal, CS$<>8__locals1.filter, null, new PropertyDefinition[]
			{
				FolderSchema.Id
			})))
			{
				for (;;)
				{
					object[][] folders = XsoUtil.TranslateXsoExceptionsWithReturnValue<object[][]>(tracer, Strings.ConnectionToMailboxFailed(mailboxGuid), () => queryResult.GetRows(10000));
					if (folders == null || folders.Length == 0)
					{
						break;
					}
					foreach (object[] folderProps in folders)
					{
						if (folderProps[0] != null && !PropertyError.IsPropertyError(folderProps[0]))
						{
							yield return StoreId.GetStoreObjectId((StoreId)folderProps[0]);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0200001F RID: 31
		[Flags]
		internal enum XsoExceptionHandlingFlags
		{
			// Token: 0x0400004A RID: 74
			None = 0,
			// Token: 0x0400004B RID: 75
			DoNotExpectObjectNotFound = 1,
			// Token: 0x0400004C RID: 76
			DoNotExpectCorruptData = 2,
			// Token: 0x0400004D RID: 77
			RethrowUnexpectedExceptions = 4,
			// Token: 0x0400004E RID: 78
			RethrowAllExceptions = 7
		}
	}
}
