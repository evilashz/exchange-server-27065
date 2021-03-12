using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ELC;
using Microsoft.Exchange.InfoWorker.Common;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200006F RID: 111
	internal sealed class TagEnforcerManager
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0001C573 File Offset: 0x0001A773
		internal TagEnforcerManager(ElcTagSubAssistant elcTagSubAssistant)
		{
			this.elcTagSubAssistant = elcTagSubAssistant;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001C582 File Offset: 0x0001A782
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "Tag EnforcerManager manager for " + this.elcTagSubAssistant.DatabaseInfo.ToString();
			}
			return this.toString;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		internal void Invoke(MailboxDataForTags mailboxDataForTags)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			try
			{
				this.InvokeInternal(mailboxDataForTags);
			}
			finally
			{
				mailboxDataForTags.StatisticsLogEntry.TagEnforcerProcessingTime = stopwatch.ElapsedMilliseconds;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		private void InvokeInternal(MailboxDataForTags mailboxDataForTags)
		{
			TagEnforcerManager.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Invoke called.", new object[]
			{
				TraceContext.Get()
			});
			TagEnforcerManager.TracerPfd.TracePfd<int, object>((long)this.GetHashCode(), "PFD IWE {0} {1}: Invoke called.", 32023, TraceContext.Get());
			TagEnforcerBase[] array = new TagEnforcerBase[]
			{
				new AutocopyTagEnforcer(mailboxDataForTags, this.elcTagSubAssistant),
				new ExpirationTagEnforcer(mailboxDataForTags, this.elcTagSubAssistant)
			};
			foreach (TagEnforcerBase tagEnforcerBase in array)
			{
				if (tagEnforcerBase.IsEnabled())
				{
					tagEnforcerBase.Invoke();
				}
			}
		}

		// Token: 0x0400032A RID: 810
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.CommonTagEnforcerOperationsTracer;

		// Token: 0x0400032B RID: 811
		private static readonly Microsoft.Exchange.Diagnostics.Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400032C RID: 812
		private ElcTagSubAssistant elcTagSubAssistant;

		// Token: 0x0400032D RID: 813
		private string toString;
	}
}
