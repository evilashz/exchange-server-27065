using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.EntitySets.Commands
{
	// Token: 0x0200001D RID: 29
	[DataContract]
	public abstract class EntityCommand<TEntitySet, TResult> : Command<TResult>, IEntityCommand<TEntitySet, TResult>
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004074 File Offset: 0x00002274
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000407C File Offset: 0x0000227C
		public virtual TEntitySet Scope { get; set; }

		// Token: 0x060000B6 RID: 182 RVA: 0x00004085 File Offset: 0x00002285
		protected bool ShouldExpand(string propertyName)
		{
			return this.Context != null && this.Context.Expand != null && this.Context.Expand.Contains(propertyName, StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000040B4 File Offset: 0x000022B4
		protected void RestoreScope(TEntitySet scope)
		{
			this.Scope = scope;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000040C0 File Offset: 0x000022C0
		private void TraceExecution()
		{
			if (this.Trace.IsTraceEnabled(TraceType.DebugTrace))
			{
				this.Trace.TraceDebug((long)this.GetHashCode(), "{0}::Execute({1}{2}){3}", new object[]
				{
					base.GetType().Name,
					this.Scope,
					this.GetCommandTraceDetails(),
					this.Context
				});
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004128 File Offset: 0x00002328
		private void OnDeserialized()
		{
			base.RegisterOnBeforeExecute(new Action(this.TraceExecution));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000413C File Offset: 0x0000233C
		[OnDeserialized]
		private void OnDeserialized(StreamingContext streamingContext)
		{
			this.OnDeserialized();
		}
	}
}
