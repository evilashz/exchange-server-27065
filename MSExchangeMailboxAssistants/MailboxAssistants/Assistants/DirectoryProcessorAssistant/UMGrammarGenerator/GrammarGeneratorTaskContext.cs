using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001BC RID: 444
	internal class GrammarGeneratorTaskContext : DirectoryProcessorBaseTaskContext
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00064D78 File Offset: 0x00062F78
		// (set) Token: 0x06001140 RID: 4416 RVA: 0x00064D80 File Offset: 0x00062F80
		private CultureInfo[] GrammarCultures { get; set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x00064D89 File Offset: 0x00062F89
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x00064D91 File Offset: 0x00062F91
		private int CultureIdx { get; set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x00064D9A File Offset: 0x00062F9A
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x00064DA2 File Offset: 0x00062FA2
		private Logger Logger { get; set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00064DAB File Offset: 0x00062FAB
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x00064DB3 File Offset: 0x00062FB3
		public IGrammarGeneratorInterface GrammarGeneratorInstance { get; private set; }

		// Token: 0x06001147 RID: 4423 RVA: 0x00064DBC File Offset: 0x00062FBC
		public GrammarGeneratorTaskContext(MailboxData mailboxData, TimeBasedDatabaseJob job, Queue<TaskQueueItem> taskQueue, AssistantStep step, TaskStatus taskStatus, IGrammarGeneratorInterface grammarGeneratorInstance, CultureInfo[] grammarCultures, Logger logger, RunData runData, IList<DirectoryProcessorBaseTask> deferredFinalizeTasks) : base(mailboxData, job, taskQueue, step, taskStatus, runData, deferredFinalizeTasks)
		{
			ValidateArgument.NotNull(grammarGeneratorInstance, "grammarGeneratorInstance");
			ValidateArgument.NotNull(grammarCultures, "grammarCultures");
			ValidateArgument.NotNull(logger, "logger");
			this.GrammarCultures = grammarCultures;
			this.CultureIdx = 0;
			this.Logger = logger;
			this.GrammarGeneratorInstance = grammarGeneratorInstance;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00064E20 File Offset: 0x00063020
		public CultureInfo GetNextCultureToProcess()
		{
			CultureInfo cultureInfo = null;
			this.Logger.TraceDebug(this, "GrammarGenerator.GetNextCultureToProcess - current CultureIdx='{0}'", new object[]
			{
				this.CultureIdx
			});
			if (this.CultureIdx < this.GrammarCultures.Length)
			{
				cultureInfo = this.GrammarCultures[this.CultureIdx];
				this.CultureIdx++;
			}
			this.Logger.TraceDebug(this, "GrammarGenerator.GetNextCultureToProcess - Returning c='{0}'", new object[]
			{
				cultureInfo
			});
			return cultureInfo;
		}
	}
}
