using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x020000AD RID: 173
	public static class Quota
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x00050430 File Offset: 0x0004E630
		internal static IDisposable SetFolderCheckHook(Quota.FolderCheckDelegate hook)
		{
			return Quota.hookableFolderCheck.SetTestHook(hook);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0005043D File Offset: 0x0004E63D
		internal static IDisposable SetMailboxCheckHook(Quota.MailboxCheckDelegate hook)
		{
			return Quota.hookableMailboxCheck.SetTestHook(hook);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0005044C File Offset: 0x0004E64C
		public static void Enforce(LID lid, Context context, Folder parentFolder, QuotaType quotaType, bool forReportDelivery)
		{
			QuotaInfo quotaInfo;
			long num;
			ErrorCode errorCode = Quota.hookableFolderCheck.Value(context, parentFolder, quotaType, forReportDelivery, out quotaInfo, out num);
			if (errorCode != ErrorCode.NoError)
			{
				DiagnosticContext.TraceDword(lid, (uint)quotaType);
				throw new StoreException((LID)29884U, errorCode);
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0005049C File Offset: 0x0004E69C
		public static void Enforce(LID lid, Context context, Mailbox mailbox, QuotaType quotaType, bool forReportDelivery)
		{
			QuotaInfo quotaInfo;
			long num;
			ErrorCode errorCode = Quota.hookableMailboxCheck.Value(context, mailbox, quotaType, forReportDelivery, out quotaInfo, out num);
			if (errorCode != ErrorCode.NoError)
			{
				DiagnosticContext.TraceDword(lid, (uint)quotaType);
				throw new StoreException((LID)29888U, errorCode);
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000504EC File Offset: 0x0004E6EC
		public static ErrorCode CheckForOverQuota(Context context, Folder parentFolder, QuotaType quotaType, bool forReportDelivery, out QuotaInfo quotaInfo, out long valueSize)
		{
			string moniker = string.Format("{0}-{1}", parentFolder.Mailbox.MailboxGuid, parentFolder.GetId(context));
			long valueSize2;
			switch (quotaType)
			{
			case QuotaType.StorageWarningLimit:
			case QuotaType.StorageOverQuotaLimit:
			case QuotaType.StorageShutoff:
			case QuotaType.DumpsterWarningLimit:
			case QuotaType.DumpsterShutoff:
				quotaInfo = parentFolder.GetQuotaInfo(context);
				valueSize = parentFolder.GetMessageSize(context) + parentFolder.GetHiddenItemSize(context);
				valueSize2 = valueSize;
				break;
			case QuotaType.MailboxMessagesPerFolderCountWarningQuota:
			case QuotaType.MailboxMessagesPerFolderCountReceiveQuota:
			case QuotaType.DumpsterMessagesPerFolderCountWarningQuota:
			case QuotaType.DumpsterMessagesPerFolderCountReceiveQuota:
				quotaInfo = parentFolder.Mailbox.QuotaInfo;
				valueSize = parentFolder.GetMessageCount(context) + parentFolder.GetHiddenItemCount(context);
				valueSize2 = valueSize + 1L;
				break;
			case QuotaType.FolderHierarchyChildrenCountWarningQuota:
			case QuotaType.FolderHierarchyChildrenCountReceiveQuota:
				quotaInfo = parentFolder.Mailbox.QuotaInfo;
				valueSize = parentFolder.GetFolderCount(context);
				valueSize2 = valueSize + 1L;
				break;
			case QuotaType.FolderHierarchyDepthWarningQuota:
			case QuotaType.FolderHierarchyDepthReceiveQuota:
			{
				quotaInfo = parentFolder.Mailbox.QuotaInfo;
				FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, parentFolder.Mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
				valueSize = folderHierarchy.GetFolderHierarchyDepth(parentFolder.GetId(context).ToExchangeShortId());
				valueSize2 = valueSize + 1L;
				break;
			}
			default:
				quotaInfo = QuotaInfo.Unlimited;
				valueSize = 0L;
				return ErrorCode.CreateInvalidParameter((LID)33600U);
			}
			return Quota.Check(context, moniker, quotaInfo, quotaType, valueSize2, forReportDelivery).Propagate((LID)29876U);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00050658 File Offset: 0x0004E858
		public static ErrorCode CheckForOverQuota(Context context, Mailbox mailbox, QuotaType quotaType, bool forReportDelivery, out QuotaInfo quotaInfo, out long valueSize)
		{
			quotaInfo = mailbox.QuotaInfo;
			string moniker = mailbox.MailboxGuid.ToString();
			switch (quotaType)
			{
			case QuotaType.StorageWarningLimit:
			case QuotaType.StorageOverQuotaLimit:
			case QuotaType.StorageShutoff:
				valueSize = mailbox.GetMessageSize(context) + mailbox.GetHiddenMessageSize(context);
				break;
			case QuotaType.DumpsterWarningLimit:
			case QuotaType.DumpsterShutoff:
				valueSize = mailbox.GetDeletedMessageSize(context) + mailbox.GetHiddenDeletedMessageSize(context);
				break;
			default:
				switch (quotaType)
				{
				case QuotaType.FoldersCountWarningQuota:
				case QuotaType.FoldersCountReceiveQuota:
				{
					FolderHierarchy folderHierarchy = FolderHierarchy.GetFolderHierarchy(context, mailbox, ExchangeShortId.Zero, FolderInformationType.Basic);
					valueSize = (long)(folderHierarchy.TotalFolderCount + 1);
					break;
				}
				default:
					quotaInfo = QuotaInfo.Unlimited;
					valueSize = 0L;
					return ErrorCode.CreateInvalidParameter((LID)49984U);
				}
				break;
			}
			return Quota.Check(context, moniker, quotaInfo, quotaType, valueSize, forReportDelivery).Propagate((LID)29880U);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00050738 File Offset: 0x0004E938
		private static ErrorCode Check(Context context, string moniker, QuotaInfo quotaInfo, QuotaType quotaType, long valueSize, bool forReportDelivery)
		{
			long num;
			Unlimited<long> unlimited;
			ErrorCodeValue errorCodeValue;
			switch (quotaType)
			{
			default:
				num = 52428800L;
				unlimited = quotaInfo.MailboxWarningQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.StorageOverQuotaLimit:
				num = 52428800L;
				unlimited = quotaInfo.MailboxSendQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.StorageShutoff:
				num = 52428800L;
				unlimited = quotaInfo.MailboxShutoffQuota;
				errorCodeValue = ErrorCodeValue.ShutoffQuotaExceeded;
				break;
			case QuotaType.DumpsterWarningLimit:
				num = 52428800L;
				unlimited = quotaInfo.DumpsterWarningQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.DumpsterShutoff:
				num = 52428800L;
				unlimited = quotaInfo.DumpsterShutoffQuota;
				errorCodeValue = ErrorCodeValue.ShutoffQuotaExceeded;
				break;
			case QuotaType.MailboxMessagesPerFolderCountWarningQuota:
				num = 1024L;
				unlimited = quotaInfo.MailboxMessagesPerFolderCountWarningQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.MailboxMessagesPerFolderCountReceiveQuota:
				num = 1024L;
				unlimited = quotaInfo.MailboxMessagesPerFolderCountReceiveQuota;
				errorCodeValue = ErrorCodeValue.MessagePerFolderCountReceiveQuotaExceeded;
				break;
			case QuotaType.DumpsterMessagesPerFolderCountWarningQuota:
				num = 1024L;
				unlimited = quotaInfo.DumpsterMessagesPerFolderCountWarningQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.DumpsterMessagesPerFolderCountReceiveQuota:
				num = 1024L;
				unlimited = quotaInfo.DumpsterMessagesPerFolderCountReceiveQuota;
				errorCodeValue = ErrorCodeValue.MessagePerFolderCountReceiveQuotaExceeded;
				break;
			case QuotaType.FolderHierarchyChildrenCountWarningQuota:
				num = 0L;
				unlimited = quotaInfo.FolderHierarchyChildrenCountWarningQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.FolderHierarchyChildrenCountReceiveQuota:
				num = 0L;
				unlimited = quotaInfo.FolderHierarchyChildrenCountReceiveQuota;
				errorCodeValue = ErrorCodeValue.FolderHierarchyChildrenCountReceiveQuotaExceeded;
				break;
			case QuotaType.FolderHierarchyDepthWarningQuota:
				num = 0L;
				unlimited = quotaInfo.FolderHierarchyDepthWarningQuota;
				errorCodeValue = ErrorCodeValue.QuotaExceeded;
				break;
			case QuotaType.FolderHierarchyDepthReceiveQuota:
				num = 0L;
				unlimited = quotaInfo.FolderHierarchyDepthReceiveQuota;
				errorCodeValue = ErrorCodeValue.FolderHierarchyDepthReceiveQuotaExceeded;
				break;
			case QuotaType.FoldersCountWarningQuota:
				num = 0L;
				unlimited = quotaInfo.FoldersCountWarningQuota;
				errorCodeValue = ErrorCodeValue.FolderHierarchySizeReceiveQuotaExceeded;
				break;
			case QuotaType.FoldersCountReceiveQuota:
				num = 0L;
				unlimited = quotaInfo.FoldersCountReceiveQuota;
				errorCodeValue = ErrorCodeValue.FolderHierarchySizeReceiveQuotaExceeded;
				break;
			}
			if (forReportDelivery && !unlimited.IsUnlimited)
			{
				long num2 = unlimited.Value * 110L / 100L;
				if (num2 - unlimited.Value < num)
				{
					num2 = unlimited.Value + num;
				}
				unlimited = new Unlimited<long>(num2);
			}
			if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.QuotaTracer.TraceDebug(29976L, "Quota.Check: container={0}, container size={1}, container quota={2}, quotaType={3}", new object[]
				{
					moniker,
					valueSize,
					unlimited,
					quotaType
				});
			}
			if (!unlimited.IsUnlimited && valueSize > unlimited.Value)
			{
				DiagnosticContext.TraceDwordAndString((LID)29818U, forReportDelivery ? 1U : 0U, moniker);
				DiagnosticContext.TraceDword((LID)29920U, (uint)quotaType);
				DiagnosticContext.TraceLong((LID)29828U, (ulong)valueSize);
				DiagnosticContext.TraceLong((LID)29832U, (ulong)unlimited.Value);
				return ErrorCode.CreateWithLid((LID)45884U, errorCodeValue);
			}
			return ErrorCode.NoError;
		}

		// Token: 0x040004B4 RID: 1204
		private static Hookable<Quota.FolderCheckDelegate> hookableFolderCheck = Hookable<Quota.FolderCheckDelegate>.Create(true, new Quota.FolderCheckDelegate(Quota.CheckForOverQuota));

		// Token: 0x040004B5 RID: 1205
		private static Hookable<Quota.MailboxCheckDelegate> hookableMailboxCheck = Hookable<Quota.MailboxCheckDelegate>.Create(true, new Quota.MailboxCheckDelegate(Quota.CheckForOverQuota));

		// Token: 0x020000AE RID: 174
		// (Invoke) Token: 0x060009E5 RID: 2533
		public delegate ErrorCode FolderCheckDelegate(Context context, Folder parentFolder, QuotaType quotaType, bool forReportDelivery, out QuotaInfo quotaInfo, out long currentContainerSize);

		// Token: 0x020000AF RID: 175
		// (Invoke) Token: 0x060009E9 RID: 2537
		public delegate ErrorCode MailboxCheckDelegate(Context context, Mailbox mailbox, QuotaType quotaType, bool forReportDelivery, out QuotaInfo quotaInfo, out long currentContainerSize);
	}
}
