using System;
using Microsoft.Exchange.Data.ApplicationLogic.FreeBusy;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Data.ApplicationLogic.UM
{
	// Token: 0x020001BF RID: 447
	internal abstract class UMStagingFolder
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x00047367 File Offset: 0x00045567
		internal static Folder OpenOrCreateUMStagingFolder(MailboxSession session)
		{
			return UMStagingFolder.OpenOrCreate(session, "UM Staging");
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00047374 File Offset: 0x00045574
		internal static bool TryOpenUMReportingFolder(MailboxSession session, out Folder umReportingFolder)
		{
			return UMStagingFolder.TryOpenFolder(session, "UMReportingData", out umReportingFolder);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00047382 File Offset: 0x00045582
		internal static bool TryOpenUMGrammarsFolder(MailboxSession session, out Folder umGrammarsFolder)
		{
			return UMStagingFolder.TryOpenFolder(session, "UMGrammars", out umGrammarsFolder);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00047390 File Offset: 0x00045590
		private static bool TryOpenFolder(MailboxSession session, string folderName, out Folder folder)
		{
			folder = null;
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			bool result;
			using (Folder folder2 = Folder.Bind(session, DefaultFolderType.Configuration))
			{
				StoreObjectId storeObjectId = QueryChildFolderByName.Query(folder2, folderName);
				if (storeObjectId == null)
				{
					result = false;
				}
				else
				{
					folder = Folder.Bind(session, storeObjectId);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000473F0 File Offset: 0x000455F0
		internal static Folder OpenOrCreateUMReportingFolder(MailboxSession session)
		{
			return UMStagingFolder.OpenOrCreate(session, "UMReportingData");
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000473FD File Offset: 0x000455FD
		internal static Folder OpenOrCreateUMGrammarsFolder(MailboxSession session)
		{
			return UMStagingFolder.OpenOrCreate(session, "UMGrammars");
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0004740C File Offset: 0x0004560C
		private static Folder OpenOrCreate(MailboxSession session, string folderName)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			UMStagingFolder.Tracer.TraceDebug<string>(0L, "Attempting to open or create UM Staging folder for {0}", session.MailboxOwnerLegacyDN);
			Folder result;
			using (Folder folder = Folder.Bind(session, DefaultFolderType.Configuration))
			{
				Folder folder2 = null;
				bool flag = false;
				try
				{
					folder2 = Folder.Create(session, folder.Id, StoreObjectType.Folder, folderName, CreateMode.OpenIfExists);
					folder2.Save();
					folder2.Load(new PropertyDefinition[]
					{
						ItemSchema.Id,
						StoreObjectSchema.DisplayName
					});
					UMStagingFolder.Tracer.TraceDebug<string, string>(0L, "UM Staging folder opened successfully Id:{0}, Owner:{1}", folder2.Id.ObjectId.ToBase64String(), session.MailboxOwnerLegacyDN);
					flag = true;
				}
				finally
				{
					if (folder2 != null && !flag)
					{
						folder2.Dispose();
						folder2 = null;
					}
				}
				result = folder2;
			}
			return result;
		}

		// Token: 0x04000914 RID: 2324
		internal const string StagingFolderName = "UM Staging";

		// Token: 0x04000915 RID: 2325
		internal const string UMReportingFolderName = "UMReportingData";

		// Token: 0x04000916 RID: 2326
		internal const string UMGrammarsFolderName = "UMGrammars";

		// Token: 0x04000917 RID: 2327
		private static readonly Trace Tracer = ExTraceGlobals.UMPartnerMessageTracer;
	}
}
