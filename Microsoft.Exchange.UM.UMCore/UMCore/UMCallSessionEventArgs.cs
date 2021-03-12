using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000206 RID: 518
	internal class UMCallSessionEventArgs : EventArgs, IPlaybackEventArgs
	{
		// Token: 0x06000F13 RID: 3859 RVA: 0x000443CA File Offset: 0x000425CA
		internal UMCallSessionEventArgs()
		{
			this.Reset();
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000443E4 File Offset: 0x000425E4
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000443EC File Offset: 0x000425EC
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

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000443F5 File Offset: 0x000425F5
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x000443FD File Offset: 0x000425FD
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

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00044406 File Offset: 0x00042606
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x0004440E File Offset: 0x0004260E
		internal Exception Error
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00044417 File Offset: 0x00042617
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0004441F File Offset: 0x0004261F
		internal bool WasPlaybackStopped
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

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00044428 File Offset: 0x00042628
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00044430 File Offset: 0x00042630
		internal IPEndPoint PimgEndpoint
		{
			get
			{
				return this.pimgEndpoint;
			}
			set
			{
				this.pimgEndpoint = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x00044439 File Offset: 0x00042639
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00044441 File Offset: 0x00042641
		internal Socket UMEndSocket
		{
			get
			{
				return this.umEndSocket;
			}
			set
			{
				this.umEndSocket = value;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0004444A File Offset: 0x0004264A
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00044452 File Offset: 0x00042652
		internal byte[] DtmfDigits
		{
			get
			{
				return this.dtmfDigits;
			}
			set
			{
				Array.Clear(this.dtmfDigits, 0, this.dtmfDigits.Length);
				this.dtmfDigits = (value ?? new byte[0]);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00044479 File Offset: 0x00042679
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00044481 File Offset: 0x00042681
		internal TimeSpan RecordTime
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0004448A File Offset: 0x0004268A
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00044492 File Offset: 0x00042692
		internal bool SendDtmfCompleted
		{
			get
			{
				return this.sendDtmfCompleted;
			}
			set
			{
				this.sendDtmfCompleted = value;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0004449B File Offset: 0x0004269B
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x000444A3 File Offset: 0x000426A3
		internal TimeSpan TotalRecordTime
		{
			get
			{
				return this.totalRecordTime;
			}
			set
			{
				this.totalRecordTime = value;
			}
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000444AC File Offset: 0x000426AC
		internal void Reset(int maxDigits)
		{
			if (this.umEndSocket != null)
			{
				this.umEndSocket.Close();
				this.umEndSocket = null;
			}
			this.pimgEndpoint = null;
			this.DtmfDigits = new byte[maxDigits];
			this.elapsedTime = TimeSpan.Zero;
			this.totalRecordTime = TimeSpan.Zero;
			this.lastPrompt = 0;
			this.error = null;
			this.sendDtmfCompleted = false;
			this.wasPlaybackStopped = false;
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x00044518 File Offset: 0x00042718
		internal void Reset()
		{
			this.Reset(0);
		}

		// Token: 0x04000B33 RID: 2867
		private Socket umEndSocket;

		// Token: 0x04000B34 RID: 2868
		private IPEndPoint pimgEndpoint;

		// Token: 0x04000B35 RID: 2869
		private byte[] dtmfDigits = new byte[0];

		// Token: 0x04000B36 RID: 2870
		private TimeSpan elapsedTime;

		// Token: 0x04000B37 RID: 2871
		private TimeSpan totalRecordTime;

		// Token: 0x04000B38 RID: 2872
		private int lastPrompt;

		// Token: 0x04000B39 RID: 2873
		private Exception error;

		// Token: 0x04000B3A RID: 2874
		private bool sendDtmfCompleted;

		// Token: 0x04000B3B RID: 2875
		private bool wasPlaybackStopped;
	}
}
