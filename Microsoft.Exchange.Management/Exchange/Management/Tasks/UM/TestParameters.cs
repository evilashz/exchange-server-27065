using System;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D74 RID: 3444
	internal struct TestParameters
	{
		// Token: 0x06008417 RID: 33815 RVA: 0x0021C8F4 File Offset: 0x0021AAF4
		internal TestParameters(string uri, string pin, string phone, string dpname, bool momTest)
		{
			this.remoteUri = uri;
			this.pin = pin;
			this.phone = phone;
			this.dpname = dpname;
			this.isMOMTest = momTest;
			this.diagInitialSilenceInMilisecs = 0;
			this.diagDtmfSequence = string.Empty;
			this.diagInterDtmfGapInMilisecs = 0;
			this.diagDtmfDurationInMilisecs = 0;
			this.diagInterDtmfGapDiffInMilisecs = string.Empty;
		}

		// Token: 0x1700290D RID: 10509
		// (get) Token: 0x06008418 RID: 33816 RVA: 0x0021C951 File Offset: 0x0021AB51
		internal string RemoteUri
		{
			get
			{
				return this.remoteUri;
			}
		}

		// Token: 0x1700290E RID: 10510
		// (get) Token: 0x06008419 RID: 33817 RVA: 0x0021C959 File Offset: 0x0021AB59
		internal bool IsMOMTest
		{
			get
			{
				return this.isMOMTest;
			}
		}

		// Token: 0x1700290F RID: 10511
		// (get) Token: 0x0600841A RID: 33818 RVA: 0x0021C961 File Offset: 0x0021AB61
		// (set) Token: 0x0600841B RID: 33819 RVA: 0x0021C969 File Offset: 0x0021AB69
		internal string PIN
		{
			get
			{
				return this.pin;
			}
			set
			{
				this.pin = value;
			}
		}

		// Token: 0x17002910 RID: 10512
		// (get) Token: 0x0600841C RID: 33820 RVA: 0x0021C972 File Offset: 0x0021AB72
		// (set) Token: 0x0600841D RID: 33821 RVA: 0x0021C97A File Offset: 0x0021AB7A
		internal string Phone
		{
			get
			{
				return this.phone;
			}
			set
			{
				this.phone = value;
			}
		}

		// Token: 0x17002911 RID: 10513
		// (get) Token: 0x0600841E RID: 33822 RVA: 0x0021C983 File Offset: 0x0021AB83
		// (set) Token: 0x0600841F RID: 33823 RVA: 0x0021C98B File Offset: 0x0021AB8B
		internal string DpName
		{
			get
			{
				return this.dpname;
			}
			set
			{
				this.dpname = value;
			}
		}

		// Token: 0x17002912 RID: 10514
		// (get) Token: 0x06008420 RID: 33824 RVA: 0x0021C994 File Offset: 0x0021AB94
		// (set) Token: 0x06008421 RID: 33825 RVA: 0x0021C99C File Offset: 0x0021AB9C
		internal string DiagDtmfSequence
		{
			get
			{
				return this.diagDtmfSequence;
			}
			set
			{
				this.diagDtmfSequence = value;
			}
		}

		// Token: 0x17002913 RID: 10515
		// (get) Token: 0x06008422 RID: 33826 RVA: 0x0021C9A5 File Offset: 0x0021ABA5
		// (set) Token: 0x06008423 RID: 33827 RVA: 0x0021C9AD File Offset: 0x0021ABAD
		internal int DiagInitialSilenceInMilisecs
		{
			get
			{
				return this.diagInitialSilenceInMilisecs;
			}
			set
			{
				this.diagInitialSilenceInMilisecs = value;
			}
		}

		// Token: 0x17002914 RID: 10516
		// (get) Token: 0x06008424 RID: 33828 RVA: 0x0021C9B6 File Offset: 0x0021ABB6
		// (set) Token: 0x06008425 RID: 33829 RVA: 0x0021C9BE File Offset: 0x0021ABBE
		internal int DiagInterDtmfGapInMilisecs
		{
			get
			{
				return this.diagInterDtmfGapInMilisecs;
			}
			set
			{
				this.diagInterDtmfGapInMilisecs = value;
			}
		}

		// Token: 0x17002915 RID: 10517
		// (get) Token: 0x06008426 RID: 33830 RVA: 0x0021C9C7 File Offset: 0x0021ABC7
		// (set) Token: 0x06008427 RID: 33831 RVA: 0x0021C9CF File Offset: 0x0021ABCF
		internal int DiagDtmfDurationInMilisecs
		{
			get
			{
				return this.diagDtmfDurationInMilisecs;
			}
			set
			{
				this.diagDtmfDurationInMilisecs = value;
			}
		}

		// Token: 0x17002916 RID: 10518
		// (get) Token: 0x06008428 RID: 33832 RVA: 0x0021C9D8 File Offset: 0x0021ABD8
		// (set) Token: 0x06008429 RID: 33833 RVA: 0x0021C9E0 File Offset: 0x0021ABE0
		internal string DiagInterDtmfGapDiffInMilisecs
		{
			get
			{
				return this.diagInterDtmfGapDiffInMilisecs;
			}
			set
			{
				this.diagInterDtmfGapDiffInMilisecs = value;
			}
		}

		// Token: 0x04004003 RID: 16387
		private string remoteUri;

		// Token: 0x04004004 RID: 16388
		private string pin;

		// Token: 0x04004005 RID: 16389
		private string phone;

		// Token: 0x04004006 RID: 16390
		private string dpname;

		// Token: 0x04004007 RID: 16391
		private bool isMOMTest;

		// Token: 0x04004008 RID: 16392
		private int diagInitialSilenceInMilisecs;

		// Token: 0x04004009 RID: 16393
		private string diagDtmfSequence;

		// Token: 0x0400400A RID: 16394
		private int diagInterDtmfGapInMilisecs;

		// Token: 0x0400400B RID: 16395
		private int diagDtmfDurationInMilisecs;

		// Token: 0x0400400C RID: 16396
		private string diagInterDtmfGapDiffInMilisecs;
	}
}
