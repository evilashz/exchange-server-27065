using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Security.RightsManagement.StructuredStorage;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000982 RID: 2434
	internal abstract class EncryptedEmailMessage
	{
		// Token: 0x060034CB RID: 13515 RVA: 0x00085154 File Offset: 0x00083354
		public EncryptedEmailMessage()
		{
			EncryptedEmailMessage.Tracer.TraceDebug((long)this.GetHashCode(), "Creating EncryptedEmailMessage for load");
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x00085172 File Offset: 0x00083372
		public EncryptedEmailMessage(Stream bodyStream)
		{
			if (bodyStream == null)
			{
				throw new ArgumentNullException("bodyStream");
			}
			EncryptedEmailMessage.Tracer.TraceDebug((long)this.GetHashCode(), "Creating EncryptedEmailMessage to save");
			this.bodyStream = bodyStream;
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000851A5 File Offset: 0x000833A5
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x000851AD File Offset: 0x000833AD
		public Stream BodyStream
		{
			get
			{
				return this.bodyStream;
			}
			protected set
			{
				if (this.bodyStream != null)
				{
					this.bodyStream.Close();
				}
				this.bodyStream = value;
			}
		}

		// Token: 0x060034CF RID: 13519
		public abstract void Load(IStorage rootStorage, CreateStreamCallbackDelegate createBodyStreamCallback, CreateStreamCallbackDelegate createAttachmentStreamCallback);

		// Token: 0x060034D0 RID: 13520
		public abstract void Save(IStorage rootStorage, EncryptedEmailMessageBinding messageBinding);

		// Token: 0x060034D1 RID: 13521 RVA: 0x000851C9 File Offset: 0x000833C9
		public virtual void Close()
		{
			if (this.bodyStream != null)
			{
				this.bodyStream.Close();
				this.bodyStream = null;
			}
			EncryptedEmailMessage.Tracer.TraceDebug((long)this.GetHashCode(), "Disposed EncryptedEmailMessage");
		}

		// Token: 0x04002CEC RID: 11500
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04002CED RID: 11501
		private Stream bodyStream;
	}
}
