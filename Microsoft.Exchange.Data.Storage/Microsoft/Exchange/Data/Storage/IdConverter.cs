using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IdConverter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal IdConverter(StoreSession session)
		{
			this.session = session;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020E0 File Offset: 0x000002E0
		public static bool IsFromPublicStore(byte[] entryId)
		{
			Util.ThrowOnNullArgument(entryId, "entryId");
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			bool result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					result = MapiStore.IsPublicEntryId(entryId);
				}
				catch (MapiExceptionInvalidEntryId innerException)
				{
					throw new ArgumentException("Id is invalid.", innerException);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetParentEntryId, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to check if entry id is from public store.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetParentEntryId, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to check if entry id is from public store.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002224 File Offset: 0x00000424
		public static bool IsFromPublicStore(StoreObjectId id)
		{
			Util.ThrowOnNullArgument(id, "id");
			return IdConverter.IsFromPublicStore(id.ProviderLevelItemId);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000223C File Offset: 0x0000043C
		public static void ExpandIdSet(IdSet idset, Action<byte[]> action)
		{
			foreach (GuidGlobCountSet guidGlobCountSet in idset)
			{
				foreach (GlobCountRange globCountRange in guidGlobCountSet.GlobCountSet)
				{
					for (ulong num = globCountRange.LowBound; num <= globCountRange.HighBound; num += 1UL)
					{
						byte[] array = new byte[22];
						int dstOffset = ExBitConverter.Write(guidGlobCountSet.Guid, array, 0);
						Buffer.BlockCopy(IdConverter.GlobcntIntoByteArray(num), 0, array, dstOffset, 6);
						action(array);
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002308 File Offset: 0x00000508
		public static GuidGlobCount GuidGlobCountFromEntryId(byte[] entryId)
		{
			return IdConverter.ExtractGuidGlobCountFromEntryId(entryId, 22);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002312 File Offset: 0x00000512
		public static GuidGlobCount MessageGuidGlobCountFromEntryId(byte[] entryId)
		{
			if (!IdConverter.IsMessageId(entryId))
			{
				throw new CorruptDataException(ServerStrings.MapiInvalidId, new ArgumentException("Invalid message id size.", "entryId"));
			}
			return IdConverter.ExtractGuidGlobCountFromEntryId(entryId, 46);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002340 File Offset: 0x00000540
		public static bool IsValidMessageEntryId(byte[] entryId)
		{
			if (!IdConverter.IsMessageId(entryId))
			{
				return false;
			}
			for (int i = 62; i < 68; i++)
			{
				if (entryId[i] != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000236D File Offset: 0x0000056D
		public static bool IsMessageId(StoreObjectId storeObjectId)
		{
			Util.ThrowOnNullArgument(storeObjectId, "storeObjectId");
			return storeObjectId.IsMessageId;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002380 File Offset: 0x00000580
		public static bool IsMessageId(byte[] entryId)
		{
			return entryId != null && entryId.Length == 70;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000238E File Offset: 0x0000058E
		public static bool IsFolderId(StoreObjectId storeObjectId)
		{
			Util.ThrowOnNullArgument(storeObjectId, "storeObjectId");
			return storeObjectId.IsFolderId;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023A1 File Offset: 0x000005A1
		public static bool IsFolderId(byte[] entryId)
		{
			return entryId != null && entryId.Length == 46;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023B0 File Offset: 0x000005B0
		public static byte[] GetParentEntryIdFromMessageEntryId(byte[] messageEntryId)
		{
			Util.ThrowOnNullArgument(messageEntryId, "messageEntryId");
			if (!IdConverter.IsMessageId(messageEntryId))
			{
				throw new ArgumentException(ServerStrings.ExOnlyMessagesHaveParent);
			}
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			byte[] folderEntryIdFromMessageEntryId;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					folderEntryIdFromMessageEntryId = MapiStore.GetFolderEntryIdFromMessageEntryId(messageEntryId);
				}
				catch (MapiExceptionInvalidEntryId innerException)
				{
					throw new ArgumentException("Id is invalid.", innerException);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetParentEntryId, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get the parent entry id for the message.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotGetParentEntryId, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get the parent entry id for the message.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return folderEntryIdFromMessageEntryId;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000250C File Offset: 0x0000070C
		public static StoreObjectId GetParentIdFromMessageId(StoreObjectId messageId)
		{
			Util.ThrowOnNullArgument(messageId, "messageId");
			return StoreObjectId.FromProviderSpecificId(IdConverter.GetParentEntryIdFromMessageEntryId(messageId.ProviderLevelItemId));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000252C File Offset: 0x0000072C
		public static byte[] CreateEntryIdFromForeignServerId(byte[] serverId)
		{
			if (serverId == null)
			{
				throw new ArgumentNullException("serverId");
			}
			if (serverId.Length > 1 && serverId[0] == 0)
			{
				byte[] array = new byte[serverId.Length - 1];
				Array.Copy(serverId, 1, array, 0, serverId.Length - 1);
				return array;
			}
			throw new CorruptDataException(ServerStrings.MapiInvalidId, new ArgumentException("Invalid foreign serverId, the first byte should be 0.", "serverId"));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002588 File Offset: 0x00000788
		public StoreObjectId CreateMessageIdFromSvrEId(byte[] svrEId)
		{
			long num = 0L;
			long num2 = 0L;
			int num3 = 0;
			if (IdConverter.ParseOursServerEntryId(svrEId, out num, out num2, out num3) && num != 0L && num2 != 0L && num3 == 0)
			{
				return this.CreateMessageId(num, num2);
			}
			throw new CorruptDataException(ServerStrings.MapiInvalidId, new ArgumentException("Invalid SvrEid format, which should be 21 bytes, the first byte should be 1 and the last four bytes should be 0.", "svrEId"));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000025DC File Offset: 0x000007DC
		public StoreObjectId CreateMessageId(long parentFolderFid, long messageMid)
		{
			byte[] entryId = null;
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				entryId = this.session.Mailbox.MapiStore.CreateEntryId(parentFolderFid, messageMid);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a message entry id from short term ids. ParentFolderFid = {0}. MessageMid = {1}.", parentFolderFid, messageMid),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a message entry id from short term ids. ParentFolderFid = {0}. MessageMid = {1}.", parentFolderFid, messageMid),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return StoreObjectId.FromProviderSpecificId(entryId);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002724 File Offset: 0x00000924
		public StoreObjectId CreateFolderId(long folderFid)
		{
			byte[] entryId = null;
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				entryId = this.session.Mailbox.MapiStore.CreateEntryId(folderFid);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a folder entry id from short term id. FolderFid = {0}.", folderFid),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a folder entry id from short term id. FolderFid = {0}.", folderFid),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return StoreObjectId.FromProviderSpecificId(entryId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000285C File Offset: 0x00000A5C
		public StoreObjectId CreatePublicMessageId(long parentFolderFid, long messageMid)
		{
			byte[] entryId = null;
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				entryId = this.session.Mailbox.MapiStore.CreatePublicEntryId(parentFolderFid, messageMid);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a public message entry id from short term ids. ParentFolderFid = {0}. MessageMid = {1}.", parentFolderFid, messageMid),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a public message entry id from short term ids. ParentFolderFid = {0}. MessageMid = {1}.", parentFolderFid, messageMid),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return StoreObjectId.FromProviderSpecificId(entryId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000029A4 File Offset: 0x00000BA4
		public StoreObjectId GetSessionSpecificId(StoreObjectId storeObjectId)
		{
			if (this.session is PublicFolderSession)
			{
				if (IdConverter.IsMessageId(storeObjectId))
				{
					return this.CreateMessageId(this.GetFidFromId(storeObjectId), this.GetMidFromMessageId(storeObjectId));
				}
				if (IdConverter.IsFolderId(storeObjectId))
				{
					return this.CreateFolderId(this.GetFidFromId(storeObjectId));
				}
			}
			return storeObjectId;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000029F4 File Offset: 0x00000BF4
		public StoreObjectId CreatePublicFolderId(long folderFid)
		{
			byte[] entryId = null;
			StoreSession storeSession = this.session;
			bool flag = false;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				entryId = this.session.Mailbox.MapiStore.CreatePublicEntryId(folderFid);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a public folder entry id from short term id. FolderFid = {0}.", folderFid),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiCannotCreateEntryIdFromShortTermId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to create a public folder entry id from short term id. FolderFid = {0}.", folderFid),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return StoreObjectId.FromProviderSpecificId(entryId);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B2C File Offset: 0x00000D2C
		public long GetFidFromId(StoreObjectId storeObjectId)
		{
			Util.ThrowOnNullArgument(storeObjectId, "storeObjectId");
			StoreSession storeSession = this.session;
			bool flag = false;
			long fidFromEntryId;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					fidFromEntryId = this.session.Mailbox.MapiStore.GetFidFromEntryId(storeObjectId.ProviderLevelItemId);
				}
				catch (MapiExceptionInvalidEntryId innerException)
				{
					throw new CorruptDataException(ServerStrings.MapiInvalidId, innerException);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get the FID from the entry ID.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get the FID from the entry ID.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return fidFromEntryId;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C88 File Offset: 0x00000E88
		public long GetMidFromMessageId(StoreObjectId messageStoreObjectId)
		{
			Util.ThrowOnNullArgument(messageStoreObjectId, "messageStoreObjectId");
			if (!IdConverter.IsMessageId(messageStoreObjectId))
			{
				throw new ArgumentException("The argument messageStoreObjectId is not an id of a message.");
			}
			StoreSession storeSession = this.session;
			bool flag = false;
			long midFromMessageEntryId;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					midFromMessageEntryId = this.session.Mailbox.MapiStore.GetMidFromMessageEntryId(messageStoreObjectId.ProviderLevelItemId);
				}
				catch (MapiExceptionInvalidEntryId innerException)
				{
					throw new CorruptDataException(ServerStrings.MapiInvalidId, innerException);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get the MID from the entry ID.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get the MID from the entry ID.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return midFromMessageEntryId;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public KeyValuePair<ushort, Guid> GetMdbIdMapping()
		{
			StoreSession storeSession = this.session;
			bool flag = false;
			KeyValuePair<ushort, Guid> result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				ushort key;
				Guid value;
				this.session.Mailbox.MapiStore.GetMdbIdMapping(out key, out value);
				result = new KeyValuePair<ushort, Guid>(key, value);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get Mdb Id mapping", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to get Mdb Id mapping", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F38 File Offset: 0x00001138
		public byte[] GetLongTermIdFromId(long id)
		{
			StoreSession storeSession = this.session;
			bool flag = false;
			byte[] result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				result = this.session.Mailbox.MapiStore.GlobalIdFromId(id);
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetLongTermIdFromId(id), ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::GetLongTermIdFromId failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetLongTermIdFromId(id), ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::GetLongTermIdFromId failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000306C File Offset: 0x0000126C
		public long GetIdFromLongTermId(byte[] longTermId)
		{
			Util.ThrowOnNullArgument(longTermId, "The longTermId cannot be null.");
			StoreSession storeSession = this.session;
			bool flag = false;
			long result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					result = this.session.Mailbox.MapiStore.IdFromGlobalId(longTermId);
				}
				catch (MapiExceptionInvalidEntryId innerException)
				{
					throw new CorruptDataException(ServerStrings.MapiInvalidId, innerException);
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetIdFromLongTermId(Convert.ToBase64String(longTermId)), ex, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::GetIdFromLongTermId failed.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.CannotGetIdFromLongTermId(Convert.ToBase64String(longTermId)), ex2, storeSession, this, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("StoreSession::GetIdFromLongTermId failed.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000031D0 File Offset: 0x000013D0
		public string GetLegacyPopUid(StoreObjectId storeId)
		{
			Util.ThrowOnNullArgument(storeId, "storeId");
			byte[] longTermIdFromId = this.GetLongTermIdFromId(storeId);
			if (longTermIdFromId.Length == 22)
			{
				Array.Resize<byte>(ref longTermIdFromId, 24);
				longTermIdFromId[longTermIdFromId.Length - 1] = 0;
				longTermIdFromId[longTermIdFromId.Length - 2] = 0;
			}
			int num = 4 * longTermIdFromId.Length / 3;
			if (num % 4 != 0)
			{
				num += 4 - num % 4;
			}
			char[] array = new char[num];
			Convert.ToBase64CharArray(longTermIdFromId, 0, longTermIdFromId.Length, array, 0);
			Array.Reverse(array);
			return new string(array);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003245 File Offset: 0x00001445
		public byte[] GetLongTermIdFromId(StoreObjectId folderOrMessageId)
		{
			if (IdConverter.IsFolderId(folderOrMessageId))
			{
				return this.GetLongTermIdFromId(this.GetFidFromId(folderOrMessageId));
			}
			if (IdConverter.IsMessageId(folderOrMessageId))
			{
				return this.GetLongTermIdFromId(this.GetMidFromMessageId(folderOrMessageId));
			}
			throw new ArgumentException("Not a valid folder or message ID", "folderOrMessageId");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003284 File Offset: 0x00001484
		internal byte[][] GetSourceKeys(ICollection<StoreObjectId> objectIds, Predicate<StoreObjectId> isIdValid)
		{
			byte[][] array = new byte[objectIds.Count][];
			int num = 0;
			foreach (StoreObjectId storeObjectId in objectIds)
			{
				if (!isIdValid(storeObjectId))
				{
					throw new ArgumentException(ServerStrings.ExInvalidItemId);
				}
				array[num++] = this.GetLongTermIdFromId(storeObjectId);
			}
			return array;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003300 File Offset: 0x00001500
		private static bool MapiIsFolderId(byte[] entryId)
		{
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			bool result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					result = MapiStore.IsFolderEntryId(entryId);
				}
				catch (MapiExceptionInvalidEntryId)
				{
					result = false;
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to determine if the id is a folder id.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to determine if the id is a folder id.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003430 File Offset: 0x00001630
		private static bool MapiIsMessageId(byte[] entryId)
		{
			StoreSession storeSession = null;
			object thisObject = null;
			bool flag = false;
			bool result;
			try
			{
				if (storeSession != null)
				{
					storeSession.BeginMapiCall();
					storeSession.BeginServerHealthCall();
					flag = true;
				}
				if (StorageGlobals.MapiTestHookBeforeCall != null)
				{
					StorageGlobals.MapiTestHookBeforeCall(MethodBase.GetCurrentMethod());
				}
				try
				{
					result = MapiStore.IsMessageEntryId(entryId);
				}
				catch (MapiExceptionInvalidEntryId)
				{
					result = false;
				}
			}
			catch (MapiPermanentException ex)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to determine if the id is a message id.", new object[0]),
					ex
				});
			}
			catch (MapiRetryableException ex2)
			{
				throw StorageGlobals.TranslateMapiException(ServerStrings.MapiErrorParsingId, ex2, storeSession, thisObject, "{0}. MapiException = {1}.", new object[]
				{
					string.Format("Unable to determine if the id is a message id.", new object[0]),
					ex2
				});
			}
			finally
			{
				try
				{
					if (storeSession != null)
					{
						storeSession.EndMapiCall();
						if (flag)
						{
							storeSession.EndServerHealthCall();
						}
					}
				}
				finally
				{
					if (StorageGlobals.MapiTestHookAfterCall != null)
					{
						StorageGlobals.MapiTestHookAfterCall(MethodBase.GetCurrentMethod());
					}
				}
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003560 File Offset: 0x00001760
		private static byte[] GlobcntIntoByteArray(ulong globCnt)
		{
			return new byte[]
			{
				(byte)(globCnt >> 40),
				(byte)(globCnt >> 32),
				(byte)(globCnt >> 24),
				(byte)(globCnt >> 16),
				(byte)(globCnt >> 8),
				(byte)globCnt
			};
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000035A1 File Offset: 0x000017A1
		private static bool ParseOursServerEntryId(byte[] entryId, out long fid, out long mid, out int instanceNum)
		{
			if (entryId == null || entryId.Length != 21 || entryId[0] != 1)
			{
				fid = 0L;
				mid = 0L;
				instanceNum = 0;
				return false;
			}
			fid = ParseSerialize.ParseInt64(entryId, 1);
			mid = ParseSerialize.ParseInt64(entryId, 9);
			instanceNum = ParseSerialize.ParseInt32(entryId, 17);
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000035E0 File Offset: 0x000017E0
		private static GuidGlobCount ExtractGuidGlobCountFromEntryId(byte[] entryId, int begIndex)
		{
			if (entryId.Length - 24 < begIndex)
			{
				string message = string.Format("Invalid message id size. need {0} bytes but had {1}", begIndex + 24, entryId.Length);
				throw new CorruptDataException(ServerStrings.MapiInvalidId, new ArgumentException(message, "entryId"));
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(entryId, begIndex, array, 0, array.Length);
			byte[] array2 = new byte[8];
			Buffer.BlockCopy(entryId, begIndex + array.Length, array2, 0, array2.Length);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(array2, 0, 6);
			}
			return new GuidGlobCount(new Guid(array), BitConverter.ToUInt64(array2, 0));
		}

		// Token: 0x04000001 RID: 1
		private const int MessageEntryIdSize = 70;

		// Token: 0x04000002 RID: 2
		private const int FolderEntryIdSize = 46;

		// Token: 0x04000003 RID: 3
		private readonly StoreSession session;
	}
}
