using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x0200010B RID: 267
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ProgressResultFactory : StandardResultFactory
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		internal ProgressResultFactory() : base(RopId.Progress)
		{
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00010008 File Offset: 0x0000E208
		public static RopResult Parse(Reader reader)
		{
			RopId ropId = (RopId)reader.PeekByte(0L);
			ErrorCode errorCode = (ErrorCode)reader.PeekUInt32(2L);
			if (ropId != RopId.Progress)
			{
				RopId ropId2 = ropId;
				if (ropId2 <= RopId.EmptyFolder)
				{
					if (ropId2 == RopId.DeleteMessages)
					{
						return new DeleteMessagesResult(reader);
					}
					switch (ropId2)
					{
					case RopId.MoveCopyMessages:
						return new MoveCopyMessagesResult(reader);
					case RopId.AbortSubmit:
					case RopId.QueryColumnsAll:
					case RopId.Abort:
						break;
					case RopId.MoveFolder:
						return new MoveFolderResult(reader);
					case RopId.CopyFolder:
						return new CopyFolderResult(reader);
					case RopId.CopyTo:
						if (errorCode == ErrorCode.None)
						{
							return new SuccessfulCopyToResult(reader);
						}
						return new FailedCopyToResult(reader);
					default:
						if (ropId2 == RopId.EmptyFolder)
						{
							return new EmptyFolderResult(reader);
						}
						break;
					}
				}
				else if (ropId2 <= RopId.HardEmptyFolder)
				{
					switch (ropId2)
					{
					case RopId.SetReadFlags:
						return new SetReadFlagsResult(reader);
					case RopId.CopyProperties:
						if (errorCode == ErrorCode.None)
						{
							return new SuccessfulCopyPropertiesResult(reader);
						}
						return new FailedCopyPropertiesResult(reader);
					default:
						switch (ropId2)
						{
						case RopId.HardDeleteMessages:
							return new HardDeleteMessagesResult(reader);
						case RopId.HardEmptyFolder:
							return new HardEmptyFolderResult(reader);
						}
						break;
					}
				}
				else
				{
					if (ropId2 == RopId.MoveCopyMessagesExtended)
					{
						return new MoveCopyMessagesExtendedResult(reader);
					}
					if (ropId2 == RopId.MoveCopyMessagesExtendedWithEntryIds)
					{
						return new MoveCopyMessagesExtendedWithEntryIdsResult(reader);
					}
				}
				throw new BufferParseException(string.Format("Unexpected result RopId: {0}", ropId));
			}
			if (errorCode == ErrorCode.None)
			{
				return new SuccessfulProgressResult(reader);
			}
			return StandardRopResult.ParseFailResult(reader);
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00010139 File Offset: 0x0000E339
		public RopResult CreateSuccessfulResult(byte logonId, uint completedTaskCount, uint totalTaskCount)
		{
			return new SuccessfulProgressResult(logonId, completedTaskCount, totalTaskCount);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00010144 File Offset: 0x0000E344
		public RopResult CreateSuccessfulCopyFolderResult(object progressToken, bool isPartiallyCompleted)
		{
			CopyFolderResultFactory copyFolderResultFactory = new CopyFolderResultFactory(progressToken);
			return copyFolderResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00010160 File Offset: 0x0000E360
		public RopResult CreateFailedCopyFolderResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			CopyFolderResultFactory copyFolderResultFactory = new CopyFolderResultFactory(progressToken);
			return copyFolderResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001017C File Offset: 0x0000E37C
		public RopResult CreateSuccessfulCopyPropertiesResult(object progressToken, PropertyProblem[] propertyProblems)
		{
			CopyPropertiesResultFactory copyPropertiesResultFactory = new CopyPropertiesResultFactory(progressToken);
			return copyPropertiesResultFactory.CreateSuccessfulResult(propertyProblems);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00010198 File Offset: 0x0000E398
		public RopResult CreateFailedCopyPropertiesResult(object progressToken, ErrorCode errorCode)
		{
			CopyPropertiesResultFactory copyPropertiesResultFactory = new CopyPropertiesResultFactory(progressToken);
			return copyPropertiesResultFactory.CreateFailedResult(errorCode);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public RopResult CreateSuccessfulCopyToResult(object progressToken, PropertyProblem[] propertyProblems)
		{
			CopyToResultFactory copyToResultFactory = new CopyToResultFactory(progressToken);
			return copyToResultFactory.CreateSuccessfulResult(propertyProblems);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000101D0 File Offset: 0x0000E3D0
		public RopResult CreateFailedCopyToResult(object progressToken, ErrorCode errorCode)
		{
			CopyToResultFactory copyToResultFactory = new CopyToResultFactory(progressToken);
			return copyToResultFactory.CreateFailedResult(errorCode);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000101EC File Offset: 0x0000E3EC
		public RopResult CreateSuccessfulDeleteMessagesResult(object progressToken, bool isPartiallyCompleted)
		{
			DeleteMessagesResultFactory deleteMessagesResultFactory = new DeleteMessagesResultFactory(progressToken);
			return deleteMessagesResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00010208 File Offset: 0x0000E408
		public RopResult CreateFailedDeleteMessagesResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			DeleteMessagesResultFactory deleteMessagesResultFactory = new DeleteMessagesResultFactory(progressToken);
			return deleteMessagesResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00010224 File Offset: 0x0000E424
		public RopResult CreateSuccessfulEmptyFolderResult(object progressToken, bool isPartiallyCompleted)
		{
			EmptyFolderResultFactory emptyFolderResultFactory = new EmptyFolderResultFactory(progressToken);
			return emptyFolderResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00010240 File Offset: 0x0000E440
		public RopResult CreateFailedEmptyFolderResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			EmptyFolderResultFactory emptyFolderResultFactory = new EmptyFolderResultFactory(progressToken);
			return emptyFolderResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001025C File Offset: 0x0000E45C
		public RopResult CreateSuccessfulHardDeleteMessagesResult(object progressToken, bool isPartiallyCompleted)
		{
			HardDeleteMessagesResultFactory hardDeleteMessagesResultFactory = new HardDeleteMessagesResultFactory(progressToken);
			return hardDeleteMessagesResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00010278 File Offset: 0x0000E478
		public RopResult CreateFailedHardDeleteMessagesResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			HardDeleteMessagesResultFactory hardDeleteMessagesResultFactory = new HardDeleteMessagesResultFactory(progressToken);
			return hardDeleteMessagesResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00010294 File Offset: 0x0000E494
		public RopResult CreateSuccessfulHardEmptyFolderResult(object progressToken, bool isPartiallyCompleted)
		{
			HardEmptyFolderResultFactory hardEmptyFolderResultFactory = new HardEmptyFolderResultFactory(progressToken);
			return hardEmptyFolderResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000102B0 File Offset: 0x0000E4B0
		public RopResult CreateFailedHardEmptyFolderResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			HardEmptyFolderResultFactory hardEmptyFolderResultFactory = new HardEmptyFolderResultFactory(progressToken);
			return hardEmptyFolderResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000102CC File Offset: 0x0000E4CC
		public RopResult CreateSuccessfulMoveCopyMessagesResult(object progressToken, bool isPartiallyCompleted)
		{
			MoveCopyMessagesResultFactory moveCopyMessagesResultFactory = new MoveCopyMessagesResultFactory(progressToken);
			return moveCopyMessagesResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000102E8 File Offset: 0x0000E4E8
		public RopResult CreateFailedMoveCopyMessagesResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			MoveCopyMessagesResultFactory moveCopyMessagesResultFactory = new MoveCopyMessagesResultFactory(progressToken);
			return moveCopyMessagesResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00010304 File Offset: 0x0000E504
		public RopResult CreateSuccessfulMoveCopyMessagesExtendedResult(object progressToken, bool isPartiallyCompleted)
		{
			MoveCopyMessagesExtendedResultFactory moveCopyMessagesExtendedResultFactory = new MoveCopyMessagesExtendedResultFactory(progressToken);
			return moveCopyMessagesExtendedResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00010320 File Offset: 0x0000E520
		public RopResult CreateFailedMoveCopyMessagesExtendedResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			MoveCopyMessagesExtendedResultFactory moveCopyMessagesExtendedResultFactory = new MoveCopyMessagesExtendedResultFactory(progressToken);
			return moveCopyMessagesExtendedResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001033C File Offset: 0x0000E53C
		public RopResult CreateSuccessfulMoveCopyMessagesExtendedWithEntryIdsResult(object progressToken, bool isPartiallyCompleted, StoreId[] messageIds, ulong[] changeNumbers)
		{
			MoveCopyMessagesExtendedWithEntryIdsResultFactory moveCopyMessagesExtendedWithEntryIdsResultFactory = new MoveCopyMessagesExtendedWithEntryIdsResultFactory(progressToken);
			return moveCopyMessagesExtendedWithEntryIdsResultFactory.CreateSuccessfulResult(isPartiallyCompleted, messageIds, changeNumbers);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001035C File Offset: 0x0000E55C
		public RopResult CreateFailedMoveCopyMessagesExtendedWithEntryIdsResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			MoveCopyMessagesExtendedWithEntryIdsResultFactory moveCopyMessagesExtendedWithEntryIdsResultFactory = new MoveCopyMessagesExtendedWithEntryIdsResultFactory(progressToken);
			return moveCopyMessagesExtendedWithEntryIdsResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00010378 File Offset: 0x0000E578
		public RopResult CreateSuccessfulMoveFolderResult(object progressToken, bool isPartiallyCompleted)
		{
			MoveFolderResultFactory moveFolderResultFactory = new MoveFolderResultFactory(progressToken);
			return moveFolderResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00010394 File Offset: 0x0000E594
		public RopResult CreateFailedMoveFolderResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			MoveFolderResultFactory moveFolderResultFactory = new MoveFolderResultFactory(progressToken);
			return moveFolderResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000103B0 File Offset: 0x0000E5B0
		public RopResult CreateSuccessfulSetReadFlagsResult(object progressToken, bool isPartiallyCompleted)
		{
			SetReadFlagsResultFactory setReadFlagsResultFactory = new SetReadFlagsResultFactory(progressToken);
			return setReadFlagsResultFactory.CreateSuccessfulResult(isPartiallyCompleted);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000103CC File Offset: 0x0000E5CC
		public RopResult CreateFailedSetReadFlagsResult(object progressToken, ErrorCode errorCode, bool isPartiallyCompleted)
		{
			SetReadFlagsResultFactory setReadFlagsResultFactory = new SetReadFlagsResultFactory(progressToken);
			return setReadFlagsResultFactory.CreateFailedResult(errorCode, isPartiallyCompleted);
		}
	}
}
