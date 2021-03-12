using System;
using System.Collections;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001A8 RID: 424
	internal class PlayBackContext
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x000361D1 File Offset: 0x000343D1
		internal PlayBackContext()
		{
			this.committed = new PlayBackContext.ResumeInfo();
			this.potential = new PlayBackContext.ResumeInfo();
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x000361EF File Offset: 0x000343EF
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x000361FC File Offset: 0x000343FC
		internal ArrayList Prompts
		{
			get
			{
				return this.committed.Prompts;
			}
			set
			{
				this.committed.Prompts = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0003620A File Offset: 0x0003440A
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00036217 File Offset: 0x00034417
		internal TimeSpan Offset
		{
			get
			{
				return this.committed.Offset;
			}
			set
			{
				this.committed.Offset = value;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00036225 File Offset: 0x00034425
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00036232 File Offset: 0x00034432
		internal string Id
		{
			get
			{
				return this.committed.Id;
			}
			set
			{
				this.committed.Id = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00036240 File Offset: 0x00034440
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0003624D File Offset: 0x0003444D
		internal int LastPrompt
		{
			get
			{
				return this.committed.LastPrompt;
			}
			set
			{
				this.committed.LastPrompt = value;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0003625B File Offset: 0x0003445B
		internal void Reset()
		{
			this.committed.Reset();
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00036268 File Offset: 0x00034468
		internal void Commit()
		{
			this.committed = (PlayBackContext.ResumeInfo)this.potential.Clone();
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00036280 File Offset: 0x00034480
		internal void Update(ArrayList prompts)
		{
			this.potential.Prompts = prompts;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0003628E File Offset: 0x0003448E
		internal void Update(string id, int lastPrompt, TimeSpan offset)
		{
			this.potential.Id = id;
			this.potential.LastPrompt = lastPrompt;
			this.potential.Offset = offset;
		}

		// Token: 0x04000A33 RID: 2611
		private PlayBackContext.ResumeInfo committed;

		// Token: 0x04000A34 RID: 2612
		private PlayBackContext.ResumeInfo potential;

		// Token: 0x020001A9 RID: 425
		internal class ResumeInfo : ICloneable
		{
			// Token: 0x06000C7E RID: 3198 RVA: 0x000362B4 File Offset: 0x000344B4
			internal ResumeInfo()
			{
				this.Reset();
			}

			// Token: 0x17000336 RID: 822
			// (get) Token: 0x06000C7F RID: 3199 RVA: 0x000362C2 File Offset: 0x000344C2
			// (set) Token: 0x06000C80 RID: 3200 RVA: 0x000362CA File Offset: 0x000344CA
			internal ArrayList Prompts
			{
				get
				{
					return this.prompts;
				}
				set
				{
					this.prompts = value;
				}
			}

			// Token: 0x17000337 RID: 823
			// (get) Token: 0x06000C81 RID: 3201 RVA: 0x000362D3 File Offset: 0x000344D3
			// (set) Token: 0x06000C82 RID: 3202 RVA: 0x000362DB File Offset: 0x000344DB
			internal TimeSpan Offset
			{
				get
				{
					return this.offset;
				}
				set
				{
					this.offset = value;
				}
			}

			// Token: 0x17000338 RID: 824
			// (get) Token: 0x06000C83 RID: 3203 RVA: 0x000362E4 File Offset: 0x000344E4
			// (set) Token: 0x06000C84 RID: 3204 RVA: 0x000362EC File Offset: 0x000344EC
			internal int LastPrompt
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

			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06000C85 RID: 3205 RVA: 0x000362F5 File Offset: 0x000344F5
			// (set) Token: 0x06000C86 RID: 3206 RVA: 0x000362FD File Offset: 0x000344FD
			internal string Id
			{
				get
				{
					return this.id;
				}
				set
				{
					this.id = value;
				}
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x00036308 File Offset: 0x00034508
			public object Clone()
			{
				return new PlayBackContext.ResumeInfo
				{
					id = this.id,
					lastPrompt = this.lastPrompt,
					offset = this.offset,
					prompts = this.prompts
				};
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x0003634C File Offset: 0x0003454C
			internal void Reset()
			{
				this.id = string.Empty;
				this.lastPrompt = 0;
				this.offset = TimeSpan.Zero;
				this.prompts = new ArrayList();
			}

			// Token: 0x04000A35 RID: 2613
			private string id;

			// Token: 0x04000A36 RID: 2614
			private int lastPrompt;

			// Token: 0x04000A37 RID: 2615
			private TimeSpan offset;

			// Token: 0x04000A38 RID: 2616
			private ArrayList prompts;
		}
	}
}
