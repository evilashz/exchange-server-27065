using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D2D RID: 3373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class SearchStatus : SearchObjectBase
	{
		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x060074B1 RID: 29873 RVA: 0x002066B0 File Offset: 0x002048B0
		// (set) Token: 0x060074B2 RID: 29874 RVA: 0x002066C2 File Offset: 0x002048C2
		public SearchState Status
		{
			get
			{
				return (SearchState)this[SearchStatusSchema.Status];
			}
			set
			{
				this[SearchStatusSchema.Status] = value;
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x060074B3 RID: 29875 RVA: 0x002066D5 File Offset: 0x002048D5
		// (set) Token: 0x060074B4 RID: 29876 RVA: 0x002066E7 File Offset: 0x002048E7
		public ADObjectId LastRunBy
		{
			get
			{
				return (ADObjectId)this[SearchStatusSchema.LastRunBy];
			}
			set
			{
				this[SearchStatusSchema.LastRunBy] = value;
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x060074B5 RID: 29877 RVA: 0x002066F5 File Offset: 0x002048F5
		// (set) Token: 0x060074B6 RID: 29878 RVA: 0x00206707 File Offset: 0x00204907
		public string LastRunByEx
		{
			get
			{
				return (string)this[SearchStatusSchema.LastRunByEx];
			}
			set
			{
				this[SearchStatusSchema.LastRunByEx] = value;
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x00206715 File Offset: 0x00204915
		// (set) Token: 0x060074B8 RID: 29880 RVA: 0x00206727 File Offset: 0x00204927
		public ExDateTime? LastStartTime
		{
			get
			{
				return (ExDateTime?)this[SearchStatusSchema.LastStartTime];
			}
			set
			{
				this[SearchStatusSchema.LastStartTime] = value;
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x060074B9 RID: 29881 RVA: 0x0020673A File Offset: 0x0020493A
		// (set) Token: 0x060074BA RID: 29882 RVA: 0x0020674C File Offset: 0x0020494C
		public ExDateTime? LastEndTime
		{
			get
			{
				return (ExDateTime?)this[SearchStatusSchema.LastEndTime];
			}
			set
			{
				this[SearchStatusSchema.LastEndTime] = value;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x060074BB RID: 29883 RVA: 0x0020675F File Offset: 0x0020495F
		// (set) Token: 0x060074BC RID: 29884 RVA: 0x00206771 File Offset: 0x00204971
		public int NumberMailboxesToSearch
		{
			get
			{
				return (int)this[SearchStatusSchema.NumberMailboxesToSearch];
			}
			set
			{
				this[SearchStatusSchema.NumberMailboxesToSearch] = value;
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x060074BD RID: 29885 RVA: 0x00206784 File Offset: 0x00204984
		// (set) Token: 0x060074BE RID: 29886 RVA: 0x00206796 File Offset: 0x00204996
		public int PercentComplete
		{
			get
			{
				return (int)this[SearchStatusSchema.PercentComplete];
			}
			set
			{
				this[SearchStatusSchema.PercentComplete] = value;
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x060074BF RID: 29887 RVA: 0x002067A9 File Offset: 0x002049A9
		// (set) Token: 0x060074C0 RID: 29888 RVA: 0x002067BB File Offset: 0x002049BB
		public long ResultNumber
		{
			get
			{
				return (long)this[SearchStatusSchema.ResultNumber];
			}
			set
			{
				this[SearchStatusSchema.ResultNumber] = value;
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x060074C1 RID: 29889 RVA: 0x002067CE File Offset: 0x002049CE
		// (set) Token: 0x060074C2 RID: 29890 RVA: 0x002067E0 File Offset: 0x002049E0
		public long ResultNumberEstimate
		{
			get
			{
				return (long)this[SearchStatusSchema.ResultNumberEstimate];
			}
			set
			{
				this[SearchStatusSchema.ResultNumberEstimate] = value;
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x060074C3 RID: 29891 RVA: 0x002067F3 File Offset: 0x002049F3
		// (set) Token: 0x060074C4 RID: 29892 RVA: 0x00206805 File Offset: 0x00204A05
		public ByteQuantifiedSize ResultSize
		{
			get
			{
				return (ByteQuantifiedSize)this[SearchStatusSchema.ResultSize];
			}
			set
			{
				this[SearchStatusSchema.ResultSize] = value;
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x060074C5 RID: 29893 RVA: 0x00206818 File Offset: 0x00204A18
		// (set) Token: 0x060074C6 RID: 29894 RVA: 0x0020682A File Offset: 0x00204A2A
		public ByteQuantifiedSize ResultSizeEstimate
		{
			get
			{
				return (ByteQuantifiedSize)this[SearchStatusSchema.ResultSizeEstimate];
			}
			set
			{
				this[SearchStatusSchema.ResultSizeEstimate] = value;
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x060074C7 RID: 29895 RVA: 0x0020683D File Offset: 0x00204A3D
		// (set) Token: 0x060074C8 RID: 29896 RVA: 0x0020684F File Offset: 0x00204A4F
		public ByteQuantifiedSize ResultSizeCopied
		{
			get
			{
				return (ByteQuantifiedSize)this[SearchStatusSchema.ResultSizeCopied];
			}
			set
			{
				this[SearchStatusSchema.ResultSizeCopied] = value;
			}
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x060074C9 RID: 29897 RVA: 0x00206862 File Offset: 0x00204A62
		// (set) Token: 0x060074CA RID: 29898 RVA: 0x00206874 File Offset: 0x00204A74
		public string ResultsPath
		{
			get
			{
				return (string)this[SearchStatusSchema.ResultsPath];
			}
			set
			{
				this[SearchStatusSchema.ResultsPath] = value;
			}
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x060074CB RID: 29899 RVA: 0x00206882 File Offset: 0x00204A82
		// (set) Token: 0x060074CC RID: 29900 RVA: 0x00206894 File Offset: 0x00204A94
		public string ResultsLink
		{
			get
			{
				return (string)this[SearchStatusSchema.ResultsLink];
			}
			set
			{
				this[SearchStatusSchema.ResultsLink] = value;
			}
		}

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x060074CD RID: 29901 RVA: 0x002068A2 File Offset: 0x00204AA2
		// (set) Token: 0x060074CE RID: 29902 RVA: 0x002068B4 File Offset: 0x00204AB4
		public MultiValuedProperty<string> Errors
		{
			get
			{
				return (MultiValuedProperty<string>)this[SearchStatusSchema.Errors];
			}
			set
			{
				this[SearchStatusSchema.Errors] = value;
			}
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x060074CF RID: 29903 RVA: 0x002068C2 File Offset: 0x00204AC2
		// (set) Token: 0x060074D0 RID: 29904 RVA: 0x002068D4 File Offset: 0x00204AD4
		public MultiValuedProperty<KeywordHit> KeywordHits
		{
			get
			{
				return (MultiValuedProperty<KeywordHit>)this[SearchStatusSchema.KeywordHits];
			}
			set
			{
				this[SearchStatusSchema.KeywordHits] = value;
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x060074D1 RID: 29905 RVA: 0x002068E2 File Offset: 0x00204AE2
		// (set) Token: 0x060074D2 RID: 29906 RVA: 0x002068F4 File Offset: 0x00204AF4
		public MultiValuedProperty<string> CompletedMailboxes
		{
			get
			{
				return (MultiValuedProperty<string>)this[SearchStatusSchema.CompletedMailboxes];
			}
			set
			{
				this[SearchStatusSchema.CompletedMailboxes] = value;
			}
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x060074D3 RID: 29907 RVA: 0x00206902 File Offset: 0x00204B02
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.SearchStatus;
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x060074D4 RID: 29908 RVA: 0x00206905 File Offset: 0x00204B05
		internal override SearchObjectBaseSchema Schema
		{
			get
			{
				return SearchStatus.schema;
			}
		}

		// Token: 0x0400515D RID: 20829
		private static SearchStatusSchema schema = ObjectSchema.GetInstance<SearchStatusSchema>();
	}
}
