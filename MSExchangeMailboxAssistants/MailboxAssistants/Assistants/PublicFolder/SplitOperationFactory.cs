using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000180 RID: 384
	internal class SplitOperationFactory : ISplitOperationFactory
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x0005B979 File Offset: 0x00059B79
		public SplitOperationFactory(IPublicFolderSession publicFolderSession, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, IXSOFactory xsoFactory)
		{
			this.publicFolderSession = publicFolderSession;
			this.logger = logger;
			this.powershellFactory = powershellFactory;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0005B99E File Offset: 0x00059B9E
		public ISplitOperation CreateIdentifyTargetMailboxOperation(IPublicFolderSplitState splitState)
		{
			return new IdentifyTargetMailboxOperation(splitState, this.publicFolderSession, this.logger, this.powershellFactory);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0005B9B8 File Offset: 0x00059BB8
		public ISplitOperation CreatePrepareTargetMailboxOperation(IPublicFolderSplitState splitState)
		{
			return new PrepareTargetMailboxOperation(splitState, this.publicFolderSession, this.logger, this.powershellFactory, this.xsoFactory);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0005B9D8 File Offset: 0x00059BD8
		public ISplitOperation CreatePrepareSplitPlanOperation(IPublicFolderSplitState splitState)
		{
			return new PrepareSplitPlanOperation(splitState, this.publicFolderSession, this.logger, this.powershellFactory, SplitOperationFactory.SplitPlanScriptPath);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0005B9F7 File Offset: 0x00059BF7
		public ISplitOperation CreateMoveContentOperation(IPublicFolderSplitState splitState)
		{
			return new MoveContentOperation(splitState, this.publicFolderSession, this.logger, this.powershellFactory, this.xsoFactory);
		}

		// Token: 0x040009BC RID: 2492
		internal static string SplitPlanScriptPath = Path.Combine(ExchangeSetupContext.InstallPath, "Scripts\\Split-PublicFolderMailbox.ps1");

		// Token: 0x040009BD RID: 2493
		private readonly IPublicFolderSession publicFolderSession;

		// Token: 0x040009BE RID: 2494
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040009BF RID: 2495
		private readonly IPublicFolderMailboxLoggerBase logger;

		// Token: 0x040009C0 RID: 2496
		private readonly IAssistantRunspaceFactory powershellFactory;
	}
}
