using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000987 RID: 2439
	internal sealed class DrmEmailMessageContainer : EncryptedEmailMessageContainer<DrmEmailMessage>
	{
		// Token: 0x060034F7 RID: 13559 RVA: 0x0008650E File Offset: 0x0008470E
		public DrmEmailMessageContainer()
		{
			DrmEmailMessageContainer.Tracer.TraceDebug((long)this.GetHashCode(), "Creating DrmEmailMessageContainer to load encrypted message");
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x0008652C File Offset: 0x0008472C
		public DrmEmailMessageContainer(string publishLicense, DrmEmailMessage emailMessage) : base(emailMessage)
		{
			DrmEmailMessageContainer.Tracer.TraceDebug((long)this.GetHashCode(), "Creating DrmEmailMessageContainer to save encrypted message");
			if (publishLicense == null)
			{
				throw new ArgumentNullException("publishLicense");
			}
			this.publishLicense = publishLicense;
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x00086560 File Offset: 0x00084760
		public string PublishLicense
		{
			get
			{
				return this.publishLicense;
			}
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060034FA RID: 13562 RVA: 0x00086568 File Offset: 0x00084768
		protected override string EncryptedEmailMessageStreamName
		{
			get
			{
				return "\tDRMContent";
			}
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0008656F File Offset: 0x0008476F
		protected override void ReadBinding(IStorage rootStorage)
		{
			DrmEmailMessageContainer.Tracer.TraceDebug((long)this.GetHashCode(), "Reading the publish license from RMS message");
			this.publishLicense = DrmDataspaces.Read(rootStorage);
			DrmEmailMessageContainer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Read the publish license from RMS message: {0}", this.publishLicense);
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x000865AF File Offset: 0x000847AF
		protected override void WriteBinding(IStorage rootStorage)
		{
			DrmEmailMessageContainer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "Writing the publish license to RMS message: {0}", this.publishLicense);
			DrmDataspaces.Write(rootStorage, this.publishLicense);
			DrmEmailMessageContainer.Tracer.TraceDebug((long)this.GetHashCode(), "Wrote the publish license to RMS message");
		}

		// Token: 0x04002D00 RID: 11520
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04002D01 RID: 11521
		private string publishLicense;
	}
}
