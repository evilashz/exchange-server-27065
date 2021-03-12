using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x02000005 RID: 5
	internal abstract class SpfTerm
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002366 File Offset: 0x00000566
		public SpfTerm(SenderIdValidationContext context)
		{
			this.context = context;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002375 File Offset: 0x00000575
		public SpfTerm Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002380 File Offset: 0x00000580
		public virtual SpfTerm Append(SpfTerm newTerm)
		{
			SpfTerm spfTerm = this;
			while (spfTerm.next != null)
			{
				spfTerm = spfTerm.next;
			}
			spfTerm.next = newTerm;
			return newTerm;
		}

		// Token: 0x0600000F RID: 15
		public abstract void Process();

		// Token: 0x06000010 RID: 16 RVA: 0x000023A8 File Offset: 0x000005A8
		protected void ProcessNextTerm()
		{
			if (this.next == null)
			{
				throw new InvalidOperationException("Next pointer cannot be null.");
			}
			ExTraceGlobals.ValidationTracer.TraceDebug((long)this.GetHashCode(), "No match, processing next term");
			this.next.Process();
		}

		// Token: 0x04000009 RID: 9
		private SpfTerm next;

		// Token: 0x0400000A RID: 10
		protected SenderIdValidationContext context;
	}
}
