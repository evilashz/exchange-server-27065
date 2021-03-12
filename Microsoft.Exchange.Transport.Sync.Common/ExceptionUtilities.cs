using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExceptionUtilities
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x000078CA File Offset: 0x00005ACA
		public static bool IndicatesDatabaseDismount(Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("exception", exception);
			return ExceptionUtilities.ApplyCheckToExceptionAndInnerException(exception, new Func<Exception, bool>(ExceptionUtilities.CheckExceptionTypeForDatabaseDismount));
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000078FB File Offset: 0x00005AFB
		public static bool IndicatesUserNotFound(Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("exception", exception);
			return ExceptionUtilities.ApplyCheckToExceptionAndInnerException(exception, (Exception e) => ExceptionUtilities.IsMapiExceptionIndicatingUserNotFound(e) || ExceptionUtilities.IsStorageExceptionIndicatingUserNotFound(e));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007944 File Offset: 0x00005B44
		public static bool ShouldPermanentMapiOrXsoExceptionBeTreatedAsTransient(SyncLogSession syncLogSession, Exception exception)
		{
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("exception", exception);
			return ExceptionUtilities.ApplyCheckToExceptionAndInnerException(exception, (Exception e) => ExceptionUtilities.CheckPermanentMapiOrXsoExceptionTypeShouldBeTreatedAsTransient(syncLogSession, e));
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000798B File Offset: 0x00005B8B
		private static bool ApplyCheckToExceptionAndInnerException(Exception exception, Func<Exception, bool> function)
		{
			return function(exception) || (exception.InnerException != null && function(exception.InnerException));
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000079AE File Offset: 0x00005BAE
		private static bool CheckPermanentMapiOrXsoExceptionTypeShouldBeTreatedAsTransient(SyncLogSession syncLogSession, Exception exception)
		{
			return ExceptionUtilities.CheckExceptionTypeForDatabaseDismount(exception) || ExceptionUtilities.IsMapiPermanentExceptionToBeTreatedAsTransient(exception) || ExceptionUtilities.IsStoragePermanentExceptionToBeTreatedAsTransient(exception);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x000079CA File Offset: 0x00005BCA
		private static bool CheckExceptionTypeForDatabaseDismount(Exception exception)
		{
			return exception is MapiExceptionExiting || exception is MapiExceptionMdbOffline || exception is SessionDeadException || exception is MapiExceptionServerPaused;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x000079F0 File Offset: 0x00005BF0
		private static bool IsMapiPermanentExceptionToBeTreatedAsTransient(Exception exception)
		{
			return exception is MapiExceptionCallFailed || exception is MapiExceptionJetErrorFileNotFound || exception is MapiExceptionJetErrorInvalidSesid || exception is MapiExceptionJetErrorLogDiskFull || exception is MapiExceptionJetErrorReadVerifyFailure || exception is MapiExceptionMaxObjsExceeded || exception is MapiExceptionUnknownMailbox || exception is MapiExceptionDuplicateObject;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00007A40 File Offset: 0x00005C40
		private static bool IsStoragePermanentExceptionToBeTreatedAsTransient(Exception exception)
		{
			return exception is ConnectionFailedPermanentException || exception is MailboxUnavailableException || exception is AccountDisabledException;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00007A5D File Offset: 0x00005C5D
		private static bool IsMapiExceptionIndicatingUserNotFound(Exception exception)
		{
			return exception is MapiExceptionUnknownUser;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007A68 File Offset: 0x00005C68
		private static bool IsStorageExceptionIndicatingUserNotFound(Exception exception)
		{
			return exception is ObjectNotFoundException || exception is WrongServerException || exception is MailboxUnavailableException || exception is CannotResolveExternalDirectoryOrganizationIdException;
		}
	}
}
