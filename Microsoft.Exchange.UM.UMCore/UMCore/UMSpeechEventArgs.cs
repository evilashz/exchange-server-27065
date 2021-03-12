using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200022E RID: 558
	internal sealed class UMSpeechEventArgs : EventArgs, IPlaybackEventArgs
	{
		// Token: 0x06001037 RID: 4151 RVA: 0x000486A0 File Offset: 0x000468A0
		internal UMSpeechEventArgs()
		{
			this.elapsedTime = TimeSpan.Zero;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x000486B3 File Offset: 0x000468B3
		// (set) Token: 0x06001039 RID: 4153 RVA: 0x000486BB File Offset: 0x000468BB
		public TimeSpan PlayTime
		{
			get
			{
				return this.elapsedTime;
			}
			set
			{
				this.elapsedTime = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x000486C4 File Offset: 0x000468C4
		// (set) Token: 0x0600103B RID: 4155 RVA: 0x000486CC File Offset: 0x000468CC
		public int LastPrompt
		{
			get
			{
				return this.lastPrompt;
			}
			set
			{
				this.lastPrompt = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x000486D5 File Offset: 0x000468D5
		// (set) Token: 0x0600103D RID: 4157 RVA: 0x000486DD File Offset: 0x000468DD
		public bool WasPlaybackStopped
		{
			get
			{
				return this.wasPlaybackStopped;
			}
			set
			{
				this.wasPlaybackStopped = value;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x000486E6 File Offset: 0x000468E6
		// (set) Token: 0x0600103F RID: 4159 RVA: 0x000486EE File Offset: 0x000468EE
		public string BookmarkReached
		{
			get
			{
				return this.bookmark;
			}
			set
			{
				this.bookmark = value;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x000486F7 File Offset: 0x000468F7
		// (set) Token: 0x06001041 RID: 4161 RVA: 0x000486FF File Offset: 0x000468FF
		internal IUMRecognitionResult Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x04000B89 RID: 2953
		private IUMRecognitionResult result;

		// Token: 0x04000B8A RID: 2954
		private TimeSpan elapsedTime;

		// Token: 0x04000B8B RID: 2955
		private int lastPrompt;

		// Token: 0x04000B8C RID: 2956
		private bool wasPlaybackStopped;

		// Token: 0x04000B8D RID: 2957
		private string bookmark;
	}
}
