using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200002B RID: 43
	internal class ApnsFeedbackMetadata : ApnsFeedbackFileBase
	{
		// Token: 0x060001B2 RID: 434 RVA: 0x00006CBF File Offset: 0x00004EBF
		internal ApnsFeedbackMetadata(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO) : this(identifier, fileIO, ExTraceGlobals.PublisherManagerTracer)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006CCE File Offset: 0x00004ECE
		internal ApnsFeedbackMetadata(ApnsFeedbackFileId identifier, ApnsFeedbackFileIO fileIO, ITracer tracer) : base(identifier, fileIO, tracer)
		{
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00006CD9 File Offset: 0x00004ED9
		public override bool IsLoaded
		{
			get
			{
				return this.isLoaded;
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006CE1 File Offset: 0x00004EE1
		public override void Load()
		{
			base.Tracer.TraceDebug<ApnsFeedbackFileId>((long)this.GetHashCode(), "[Load] Loading APNs Feedback Metadata from '{0}'", base.Identifier);
			this.isLoaded = true;
		}

		// Token: 0x040000A8 RID: 168
		private bool isLoaded;
	}
}
