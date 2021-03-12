using System;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000175 RID: 373
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PublicFolderHierarchySyncProcessor : PublicFolderProcessor
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x00058899 File Offset: 0x00056A99
		public PublicFolderHierarchySyncProcessor(PublicFolderSession publicFolderSession, Trace tracer) : base(publicFolderSession, tracer)
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000588A4 File Offset: 0x00056AA4
		public override void Invoke()
		{
			this.tracer.TraceDebug<string>((long)this.GetHashCode(), "PublicFolderHierarchySyncProcessor::Invoke::{0} - Begin processing", this.publicFolderSession.DisplayAddress);
			try
			{
				if (!this.publicFolderSession.IsPrimaryHierarchySession)
				{
					this.tracer.TraceDebug<string>((long)this.GetHashCode(), "PublicFolderHierarchySyncProcessor::Invoke::{0} - Processing for public folder content-only mailbox", this.publicFolderSession.DisplayAddress);
					PublicFolderSyncJobState publicFolderSyncJobState = PublicFolderSyncJobRpc.StartSyncHierarchy(this.publicFolderSession.MailboxPrincipal, true);
					string text = null;
					ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(2235968829U, ref text);
					if (text != null)
					{
						while (publicFolderSyncJobState.JobStatus == PublicFolderSyncJobState.Status.Queued)
						{
							Thread.Sleep(1000);
							publicFolderSyncJobState = PublicFolderSyncJobRpc.QueryStatusSyncHierarchy(this.publicFolderSession.MailboxPrincipal);
						}
					}
				}
			}
			catch (StoragePermanentException arg)
			{
				this.tracer.TraceError<string, StoragePermanentException>((long)this.GetHashCode(), "PublicFolderHierarchySyncProcessor::Invoke::{0} - Unable to process {1}", this.publicFolderSession.DisplayAddress, arg);
			}
			catch (StorageTransientException arg2)
			{
				this.tracer.TraceError<string, StorageTransientException>((long)this.GetHashCode(), "PublicFolderHierarchySyncProcessor::Invoke::{0} - Unable to process {1}", this.publicFolderSession.DisplayAddress, arg2);
			}
		}
	}
}
