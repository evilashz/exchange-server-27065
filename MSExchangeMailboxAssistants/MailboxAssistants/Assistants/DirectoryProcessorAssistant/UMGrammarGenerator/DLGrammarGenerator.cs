using System;
using System.Collections.Generic;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001B8 RID: 440
	internal class DLGrammarGenerator : IGrammarGeneratorInterface
	{
		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x000637F3 File Offset: 0x000619F3
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x000637FB File Offset: 0x000619FB
		private Logger Logger { get; set; }

		// Token: 0x0600111B RID: 4379 RVA: 0x00063804 File Offset: 0x00061A04
		public DLGrammarGenerator(Logger logger)
		{
			this.Logger = logger;
			this.Logger.TraceDebug(this, "Entering DLGrammarGenerator constructor", new object[0]);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0006382C File Offset: 0x00061A2C
		public List<DirectoryGrammar> GetGrammarList()
		{
			this.Logger.TraceDebug(this, "Entering DLGrammarGenerator.GetGrammarList", new object[0]);
			List<DirectoryGrammar> list = new List<DirectoryGrammar>();
			this.Logger.TraceDebug(this, "Adding Distribution List grammar", new object[0]);
			list.Add(new DistributionListGrammar());
			return list;
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00063879 File Offset: 0x00061A79
		public string ADEntriesFileName
		{
			get
			{
				return "DistributionList";
			}
		}
	}
}
