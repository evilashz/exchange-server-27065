using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200063E RID: 1598
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PolicyTag
	{
		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00117CDE File Offset: 0x00115EDE
		// (set) Token: 0x060041CF RID: 16847 RVA: 0x00117CE6 File Offset: 0x00115EE6
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x00117CEF File Offset: 0x00115EEF
		// (set) Token: 0x060041D1 RID: 16849 RVA: 0x00117CF7 File Offset: 0x00115EF7
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x00117D00 File Offset: 0x00115F00
		// (set) Token: 0x060041D3 RID: 16851 RVA: 0x00117D08 File Offset: 0x00115F08
		public EnhancedTimeSpan TimeSpanForRetention
		{
			get
			{
				return this.timeSpanForRetention;
			}
			set
			{
				this.timeSpanForRetention = value;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x00117D11 File Offset: 0x00115F11
		// (set) Token: 0x060041D5 RID: 16853 RVA: 0x00117D19 File Offset: 0x00115F19
		public Guid PolicyGuid
		{
			get
			{
				return this.policyGuid;
			}
			set
			{
				this.policyGuid = value;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x00117D22 File Offset: 0x00115F22
		// (set) Token: 0x060041D7 RID: 16855 RVA: 0x00117D2A File Offset: 0x00115F2A
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}
			set
			{
				this.isVisible = value;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x00117D33 File Offset: 0x00115F33
		// (set) Token: 0x060041D9 RID: 16857 RVA: 0x00117D3B File Offset: 0x00115F3B
		public bool OptedInto
		{
			get
			{
				return this.optedInto;
			}
			set
			{
				this.optedInto = value;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x00117D44 File Offset: 0x00115F44
		// (set) Token: 0x060041DB RID: 16859 RVA: 0x00117D4C File Offset: 0x00115F4C
		public bool IsArchive
		{
			get
			{
				return this.isArchive;
			}
			set
			{
				this.isArchive = value;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x00117D55 File Offset: 0x00115F55
		// (set) Token: 0x060041DD RID: 16861 RVA: 0x00117D5D File Offset: 0x00115F5D
		public ElcFolderType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<ElcFolderType>(value, "ElcFolderType");
				this.type = value;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x00117D71 File Offset: 0x00115F71
		// (set) Token: 0x060041DF RID: 16863 RVA: 0x00117D79 File Offset: 0x00115F79
		public RetentionActionType RetentionAction
		{
			get
			{
				return this.retentionAction;
			}
			set
			{
				EnumValidator.ThrowIfInvalid<RetentionActionType>(value, "RetentionActionType");
				this.retentionAction = value;
			}
		}

		// Token: 0x0400243F RID: 9279
		private string name;

		// Token: 0x04002440 RID: 9280
		private string description;

		// Token: 0x04002441 RID: 9281
		private EnhancedTimeSpan timeSpanForRetention;

		// Token: 0x04002442 RID: 9282
		private Guid policyGuid = Guid.Empty;

		// Token: 0x04002443 RID: 9283
		private bool isVisible;

		// Token: 0x04002444 RID: 9284
		private bool optedInto;

		// Token: 0x04002445 RID: 9285
		private bool isArchive;

		// Token: 0x04002446 RID: 9286
		private ElcFolderType type;

		// Token: 0x04002447 RID: 9287
		private RetentionActionType retentionAction;
	}
}
