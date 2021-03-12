using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200005C RID: 92
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class LoadBalanceDiagnosableArgumentBase : DiagnosableArgument
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00009C84 File Offset: 0x00007E84
		public bool Verbose
		{
			get
			{
				return base.HasArgument("verbose");
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00009C91 File Offset: 0x00007E91
		public bool TraceEnabled
		{
			get
			{
				return base.HasArgument("trace");
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00009C9E File Offset: 0x00007E9E
		protected override bool FailOnMissingArgument
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00009CA1 File Offset: 0x00007EA1
		protected override void InitializeSchema(Dictionary<string, Type> schema)
		{
			schema["verbose"] = typeof(bool);
			schema["trace"] = typeof(bool);
			this.ExtendSchema(schema);
		}

		// Token: 0x06000334 RID: 820
		protected abstract void ExtendSchema(Dictionary<string, Type> schema);

		// Token: 0x040000ED RID: 237
		internal const string VerboseArgument = "verbose";

		// Token: 0x040000EE RID: 238
		internal const string TraceArgument = "trace";
	}
}
