using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000032 RID: 50
	internal static class XsoUtil
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		internal static TReturnValue TranslateXsoExceptionsWithReturnValue<TReturnValue>(IDiagnosticsSession tracer, LocalizedString errorString, XsoUtil.XsoExceptionHandlingFlags flags, Func<TReturnValue> xsoCall)
		{
			TReturnValue result = default(TReturnValue);
			XsoUtil.TranslateXsoExceptions(tracer, errorString, flags, delegate()
			{
				result = xsoCall();
			});
			return result;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C728 File Offset: 0x0000A928
		internal static TReturnValue TranslateXsoExceptionsWithReturnValue<TReturnValue>(IDiagnosticsSession tracer, LocalizedString errorString, Func<TReturnValue> xsoCall)
		{
			return XsoUtil.TranslateXsoExceptionsWithReturnValue<TReturnValue>(tracer, errorString, XsoUtil.XsoExceptionHandlingFlags.None, xsoCall);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C733 File Offset: 0x0000A933
		internal static void TranslateXsoExceptions(IDiagnosticsSession tracer, LocalizedString errorString, Action xsoCall)
		{
			XsoUtil.TranslateXsoExceptions(tracer, errorString, XsoUtil.XsoExceptionHandlingFlags.None, xsoCall);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000C740 File Offset: 0x0000A940
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

		// Token: 0x060001A8 RID: 424 RVA: 0x0000CC28 File Offset: 0x0000AE28
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

		// Token: 0x060001A9 RID: 425 RVA: 0x0000CC54 File Offset: 0x0000AE54
		internal static ExchangePrincipal GetExchangePrincipal(ISearchServiceConfig config, MdbInfo mdbInfo, Guid mailboxGuid)
		{
			ExchangePrincipal result;
			if (config.ReadFromPassiveEnabled && !mdbInfo.IsLagCopy)
			{
				DatabaseLocationInfo databaseLocationInfo = new DatabaseLocationInfo(LocalServer.GetServer(), false);
				result = ExchangePrincipal.FromMailboxData(mailboxGuid, mdbInfo.Guid, null, Array<CultureInfo>.Empty, RemotingOptions.LocalConnectionsOnly, databaseLocationInfo);
			}
			else
			{
				result = ExchangePrincipal.FromMailboxData(mailboxGuid, mdbInfo.Guid, Array<CultureInfo>.Empty, RemotingOptions.AllowCrossSite);
			}
			return result;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000CCA8 File Offset: 0x0000AEA8
		internal static StoreSession GetStoreSession(ISearchServiceConfig config, ExchangePrincipal principal, bool isPublicFolderMailbox, string clientInfo)
		{
			if (isPublicFolderMailbox)
			{
				return PublicFolderSession.OpenAsSearch(principal, clientInfo, config.ReadFromPassiveEnabled);
			}
			return MailboxSession.OpenAsSystemService(principal, CultureInfo.InvariantCulture, clientInfo, config.ReadFromPassiveEnabled);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		internal static Folder GetRootFolder(StoreSession storeSession)
		{
			if (storeSession.IsPublicFolderSession)
			{
				PublicFolderSession publicFolderSession = (PublicFolderSession)storeSession;
				return Folder.Bind(publicFolderSession, publicFolderSession.GetPublicFolderRootId(), new PropertyDefinition[]
				{
					FolderSchema.Id
				});
			}
			return Folder.Bind(storeSession, DefaultFolderType.Configuration, new PropertyDefinition[]
			{
				FolderSchema.Id
			});
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000CD21 File Offset: 0x0000AF21
		internal static bool ShouldSkipMessageClass(string messageClass)
		{
			return XsoUtil.MessageClassesToSkip.Contains(messageClass);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		private static void TraceAndThrowTransientException(IDiagnosticsSession tracer, LocalizedString errorString, LocalizedException ex)
		{
			tracer.TraceError<LocalizedString, LocalizedException>("Error: {0}, exception: {1}", errorString, ex);
			throw new ComponentFailedTransientException(errorString, ex);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000CD44 File Offset: 0x0000AF44
		private static void TraceAndThrowPermanentException(IDiagnosticsSession tracer, LocalizedString errorString, LocalizedException ex)
		{
			tracer.TraceError<LocalizedString, LocalizedException>("Error: {0}, exception: {1}", errorString, ex);
			throw new ComponentFailedPermanentException(errorString, ex);
		}

		// Token: 0x04000110 RID: 272
		private static readonly HashSet<string> MessageClassesToSkip = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Exchange.ContentsSyncData",
			"IPM.Microsoft.WunderBar.Link",
			"IPM.Configuration.OWA.AutocompleteCache",
			"IPC.Microsoft Exchange 4.0.Deferred Action",
			"IPM.MS-Exchange.MailboxMoveHistory"
		};

		// Token: 0x02000033 RID: 51
		[Flags]
		internal enum XsoExceptionHandlingFlags
		{
			// Token: 0x04000112 RID: 274
			None = 0,
			// Token: 0x04000113 RID: 275
			DoNotExpectObjectNotFound = 1,
			// Token: 0x04000114 RID: 276
			DoNotExpectCorruptData = 2,
			// Token: 0x04000115 RID: 277
			RethrowUnexpectedExceptions = 4,
			// Token: 0x04000116 RID: 278
			RethrowAllExceptions = 7
		}
	}
}
