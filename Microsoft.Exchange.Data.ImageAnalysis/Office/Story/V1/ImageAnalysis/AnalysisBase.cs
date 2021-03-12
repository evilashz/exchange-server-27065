using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x02000006 RID: 6
	[DataContract]
	[Serializable]
	internal abstract class AnalysisBase
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000217B File Offset: 0x0000037B
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002183 File Offset: 0x00000383
		[DataMember]
		public ImageAnalysisState State { get; protected set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002194 File Offset: 0x00000394
		[DataMember]
		public Exception Error { get; protected set; }

		// Token: 0x06000015 RID: 21 RVA: 0x000021A0 File Offset: 0x000003A0
		protected void PerformAnalysis()
		{
			this.State = ImageAnalysisState.NotProcessed;
			if (!this.CanAnalyze())
			{
				this.State = ImageAnalysisState.UnsatisfactorySubject;
				this.CreateDefaultResults();
				return;
			}
			try
			{
				this.AnalysisImplementation();
				this.State = ImageAnalysisState.SuccessfullyProcessed;
			}
			catch (Exception error)
			{
				this.Error = error;
				this.State = ImageAnalysisState.ErrorProcessing;
			}
			finally
			{
				this.Lock();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002210 File Offset: 0x00000410
		protected virtual void Lock()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002212 File Offset: 0x00000412
		protected virtual void CreateDefaultResults()
		{
		}

		// Token: 0x06000018 RID: 24
		protected abstract bool CanAnalyze();

		// Token: 0x06000019 RID: 25
		protected abstract void AnalysisImplementation();
	}
}
