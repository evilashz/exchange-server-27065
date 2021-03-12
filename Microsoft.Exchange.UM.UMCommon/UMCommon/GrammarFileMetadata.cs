using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x0200009B RID: 155
	internal class GrammarFileMetadata
	{
		// Token: 0x06000562 RID: 1378 RVA: 0x00015444 File Offset: 0x00013644
		public GrammarFileMetadata(string path, string hash, long size)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.UMGrammarGeneratorTracer, this, "GrammarFileMetadata constructor - path='{0}', hash='{1}', size='{2}'", new object[]
			{
				path,
				hash,
				size
			});
			this.Path = path;
			this.Hash = hash;
			this.Size = size;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00015495 File Offset: 0x00013695
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0001549D File Offset: 0x0001369D
		public string Path { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x000154A6 File Offset: 0x000136A6
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x000154AE File Offset: 0x000136AE
		public string Hash { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x000154B7 File Offset: 0x000136B7
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x000154BF File Offset: 0x000136BF
		public long Size { get; private set; }
	}
}
