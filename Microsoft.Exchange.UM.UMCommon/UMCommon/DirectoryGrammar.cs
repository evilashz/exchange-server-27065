using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200008E RID: 142
	internal abstract class DirectoryGrammar
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00013BD0 File Offset: 0x00011DD0
		public string FilePath
		{
			get
			{
				return this.filePath;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00013BD8 File Offset: 0x00011DD8
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00013BE0 File Offset: 0x00011DE0
		public bool MaxEntriesExceeded { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00013BE9 File Offset: 0x00011DE9
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x00013BF1 File Offset: 0x00011DF1
		public int NumEntries { get; private set; }

		// Token: 0x0600050F RID: 1295 RVA: 0x00013BFC File Offset: 0x00011DFC
		public void InitializeGrammar(string path, CultureInfo c)
		{
			ValidateArgument.NotNullOrEmpty(path, "path");
			ValidateArgument.NotNull(c, "c");
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "DirectoryGrammar.InitializeGrammar - path='{0}', culture='{1}'", new object[]
			{
				path,
				c
			});
			this.filePath = path;
			this.grammarFile = new GrammarFile(this.filePath, c);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00013C58 File Offset: 0x00011E58
		public void WriteADEntry(ADEntry entry)
		{
			ValidateArgument.NotNull(entry, "entry");
			PIIMessage data = PIIMessage.Create(PIIType._SmtpAddress, entry.SmtpAddress);
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, data, "DirectoryGrammar.WriteADEntry - Processing entry=_SmtpAddress for '{0}'", new object[]
			{
				this.filePath
			});
			if (this.MaxEntriesExceeded)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "Max entries exceeded - numEntries={0}, filePath={1}", new object[]
				{
					this.NumEntries,
					this.filePath
				});
				return;
			}
			if (this.ShouldAcceptGrammarEntry(entry))
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, data, "DirectoryGrammar.WriteADEntry - Writing entry=_SmtpAddress to '{0}'", new object[]
				{
					this.filePath
				});
				this.grammarFile.WriteEntry(entry);
				this.NumEntries += entry.Names.Count;
				if (this.NumEntries > 250000)
				{
					this.MaxEntriesExceeded = true;
				}
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00013D3C File Offset: 0x00011F3C
		public string CompleteGrammar()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "DirectoryGrammar.CompleteGrammar - '{0}'", new object[]
			{
				this.filePath
			});
			this.grammarFile.Close();
			this.grammarFile = null;
			return this.filePath;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000512 RID: 1298
		public abstract string FileName { get; }

		// Token: 0x06000513 RID: 1299 RVA: 0x00013D82 File Offset: 0x00011F82
		protected virtual bool ShouldAcceptGrammarEntry(ADEntry entry)
		{
			return true;
		}

		// Token: 0x0400031D RID: 797
		private const int MaxEntries = 250000;

		// Token: 0x0400031E RID: 798
		private GrammarFile grammarFile;

		// Token: 0x0400031F RID: 799
		private string filePath;
	}
}
