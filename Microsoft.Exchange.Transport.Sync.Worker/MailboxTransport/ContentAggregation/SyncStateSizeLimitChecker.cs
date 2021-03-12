using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200021D RID: 541
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncStateSizeLimitChecker
	{
		// Token: 0x06001347 RID: 4935 RVA: 0x0004164E File Offset: 0x0003F84E
		public SyncStateSizeLimitChecker(long maxLoadedSyncStateSizeInBytes)
		{
			SyncUtilities.ThrowIfArgumentLessThanZero("maxLoadedSyncStateSizeInBytes", maxLoadedSyncStateSizeInBytes);
			this.maxLoadedSyncStateSizeInBytes = maxLoadedSyncStateSizeInBytes;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00041668 File Offset: 0x0003F868
		public virtual void CheckUncompressedSyncStateWithinBounds(Guid subscriptionGuid, string syncStateId, long currentSizeOfLoadedSyncStateInBytes)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("syncStateId", syncStateId);
			SyncUtilities.ThrowIfArgumentLessThanZero("currentSizeOfLoadedSyncStateInBytes", currentSizeOfLoadedSyncStateInBytes);
			UncompressedSyncStateSizeExceededException ex;
			if (this.IsUncompressedSyncStateExceededBounds(subscriptionGuid, syncStateId, currentSizeOfLoadedSyncStateInBytes, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x000416A8 File Offset: 0x0003F8A8
		public virtual bool IsUncompressedSyncStateExceededBounds(Guid subscriptionGuid, string syncStateId, long currentSizeOfLoadedSyncStateInBytes, out UncompressedSyncStateSizeExceededException uncompressedSyncStateSizeExceededException)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("syncStateId", syncStateId);
			SyncUtilities.ThrowIfArgumentLessThanZero("currentSizeOfLoadedSyncStateInBytes", currentSizeOfLoadedSyncStateInBytes);
			uncompressedSyncStateSizeExceededException = null;
			if (currentSizeOfLoadedSyncStateInBytes >= this.maxLoadedSyncStateSizeInBytes)
			{
				uncompressedSyncStateSizeExceededException = new UncompressedSyncStateSizeExceededException(syncStateId, subscriptionGuid, ByteQuantifiedSize.FromBytes(Convert.ToUInt64(currentSizeOfLoadedSyncStateInBytes)), ByteQuantifiedSize.FromBytes(Convert.ToUInt64(this.maxLoadedSyncStateSizeInBytes)));
				return true;
			}
			return false;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0004170C File Offset: 0x0003F90C
		public virtual void CheckCompressedSyncStateWithinBounds(Guid subscriptionGuid, string syncStateId, StoragePermanentException storagePermanentException)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("syncStateId", syncStateId);
			SyncUtilities.ThrowIfArgumentNull("storagePermanentException", storagePermanentException);
			CompressedSyncStateSizeExceededException ex;
			if (this.IsCompressedSyncStateExceededBounds(subscriptionGuid, syncStateId, storagePermanentException, out ex))
			{
				throw ex;
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0004174C File Offset: 0x0003F94C
		public virtual bool IsCompressedSyncStateExceededBounds(Guid subscriptionGuid, string syncStateId, StoragePermanentException storagePermanentException, out CompressedSyncStateSizeExceededException compressedSyncStateSizeExceededException)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("syncStateId", syncStateId);
			SyncUtilities.ThrowIfArgumentNull("storagePermanentException", storagePermanentException);
			compressedSyncStateSizeExceededException = null;
			if (storagePermanentException.InnerException is MapiExceptionStreamSizeError)
			{
				compressedSyncStateSizeExceededException = new CompressedSyncStateSizeExceededException(syncStateId, subscriptionGuid, storagePermanentException);
				return true;
			}
			return false;
		}

		// Token: 0x04000A32 RID: 2610
		private readonly long maxLoadedSyncStateSizeInBytes;
	}
}
