using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator
{
	// Token: 0x020001BA RID: 442
	internal class GrammarGenerationLog : DisposableBase
	{
		// Token: 0x06001122 RID: 4386 RVA: 0x000639F8 File Offset: 0x00061BF8
		public GrammarGenerationLog(string grammarFileFolderPath, Logger tracer)
		{
			ValidateArgument.NotNullOrEmpty(grammarFileFolderPath, "grammarFileFolderPath");
			ValidateArgument.NotNull(tracer, "tracer");
			this.tracer = tracer;
			this.tracer.TraceDebug(this, "Entering GrammarGenerationLog constructor grammarFileFolderPath='{0}'", new object[]
			{
				grammarFileFolderPath
			});
			string path = Path.Combine(grammarFileFolderPath, "GrammarGeneration.Log");
			this.logWriter = new StreamWriter(path, true);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00063A60 File Offset: 0x00061C60
		public void WriteLine(string s)
		{
			try
			{
				this.logWriter.WriteLine(s);
			}
			catch (IOException ex)
			{
				this.tracer.TraceError(this, "Failed to write to grammar generation log. Error:'{0}'", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00063AAC File Offset: 0x00061CAC
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.tracer.TraceDebug(this, "Entering GrammarGenerationLog.InternalDispose", new object[0]);
				this.logWriter.Dispose();
				this.logWriter = null;
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00063ADA File Offset: 0x00061CDA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<GrammarGenerationLog>(this);
		}

		// Token: 0x04000ABA RID: 2746
		private const string LogFileName = "GrammarGeneration.Log";

		// Token: 0x04000ABB RID: 2747
		private StreamWriter logWriter;

		// Token: 0x04000ABC RID: 2748
		private Logger tracer;
	}
}
