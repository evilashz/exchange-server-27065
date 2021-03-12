using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000AB RID: 171
	internal abstract class PipelineProcessor : IDataProcessor
	{
		// Token: 0x060005AF RID: 1455 RVA: 0x0001882C File Offset: 0x00016A2C
		protected PipelineProcessor(IDataProcessor next)
		{
			this.next = next;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001883C File Offset: 0x00016A3C
		public void Process(PropertyBag propertyBag)
		{
			Type type = base.GetType();
			string fullName = type.FullName;
			string text = string.Format("PipelineProcessor <{0}>", fullName);
			ADObjectId adobjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			ExTraceGlobals.BackSyncTracer.TraceDebug<string, string>((long)SyncConfiguration.TraceId, "{0} process {1} ...", text, adobjectId.ToString());
			ProcessorHelper.TracePropertBag(text, propertyBag);
			if (this.ProcessInternal(propertyBag))
			{
				this.next.Process(propertyBag);
			}
			else
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "{0} processing terminated", text);
			}
			ExTraceGlobals.BackSyncTracer.TraceDebug<string, string>((long)SyncConfiguration.TraceId, "{0} completed process {1}", text, adobjectId.ToString());
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000188E0 File Offset: 0x00016AE0
		public void Flush(Func<byte[]> getCookieDelegate, bool moreData)
		{
			this.ProcessRemainingEntries();
			this.next.Flush(getCookieDelegate, moreData);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000188F5 File Offset: 0x00016AF5
		protected virtual void ProcessRemainingEntries()
		{
		}

		// Token: 0x060005B3 RID: 1459
		protected abstract bool ProcessInternal(PropertyBag propertyBag);

		// Token: 0x040002CD RID: 717
		private readonly IDataProcessor next;
	}
}
