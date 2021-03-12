using System;
using Microsoft.Exchange.Diagnostics.Components.SenderId;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.SenderId
{
	// Token: 0x0200001C RID: 28
	internal sealed class ExpSpfModifier
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003EF4 File Offset: 0x000020F4
		public ExpSpfModifier(SenderIdValidationContext context, MacroTermSpfNode domainSpec)
		{
			this.context = context;
			this.domainSpec = domainSpec;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003F0A File Offset: 0x0000210A
		public IAsyncResult BeginGetExplanation(AsyncCallback asyncCallback, object asyncState)
		{
			this.asyncResult = new SenderIdAsyncResult(asyncCallback, asyncState);
			SpfMacro.BeginExpandDomainSpec(this.context, this.domainSpec, new AsyncCallback(this.ExpandDomainSpecCallback), null);
			return this.asyncResult;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003F40 File Offset: 0x00002140
		private void ExpandDomainSpecCallback(IAsyncResult ar)
		{
			SpfMacro.ExpandedMacro expandedMacro = SpfMacro.EndExpandDomainSpec(ar);
			if (!expandedMacro.IsValid)
			{
				ExTraceGlobals.ValidationTracer.TraceError((long)this.GetHashCode(), "Could not expand DomainSpec for exp modifier");
				this.context.ValidationCompleted(SenderIdStatus.PermError);
				return;
			}
			string value = expandedMacro.Value;
			ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Using expanded domain for TXT lookup: {0}", value);
			if (!Util.AsyncDns.IsValidName(value))
			{
				this.asyncResult.InvokeCompleted(string.Empty);
				return;
			}
			Util.AsyncDns.BeginTxtRecordQuery(value, new AsyncCallback(this.TxtCallback), null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003FCC File Offset: 0x000021CC
		private void TxtCallback(IAsyncResult ar)
		{
			string[] array;
			if (Util.AsyncDns.EndTxtRecordQuery(ar, out array) == DnsStatus.Success && array.Length == 1 && array[0].Length > 0)
			{
				SpfMacro.BeginExpandExp(this.context, array[0], new AsyncCallback(this.ExpandExpCallback), null);
				return;
			}
			this.asyncResult.InvokeCompleted(string.Empty);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004024 File Offset: 0x00002224
		private void ExpandExpCallback(IAsyncResult ar)
		{
			SpfMacro.ExpandedMacro expandedMacro = SpfMacro.EndExpandExp(ar);
			if (expandedMacro.IsValid)
			{
				ExTraceGlobals.ValidationTracer.TraceDebug<string>((long)this.GetHashCode(), "Using expanded EXP string: {0}", expandedMacro.Value);
				this.asyncResult.InvokeCompleted(expandedMacro.Value);
				return;
			}
			ExTraceGlobals.ValidationTracer.TraceError((long)this.GetHashCode(), "Could not expand EXP string");
			this.asyncResult.InvokeCompleted(string.Empty);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004094 File Offset: 0x00002294
		public string EndGetExplanation(IAsyncResult ar)
		{
			return (string)((SenderIdAsyncResult)ar).GetResult();
		}

		// Token: 0x0400004A RID: 74
		private MacroTermSpfNode domainSpec;

		// Token: 0x0400004B RID: 75
		private SenderIdValidationContext context;

		// Token: 0x0400004C RID: 76
		private SenderIdAsyncResult asyncResult;
	}
}
