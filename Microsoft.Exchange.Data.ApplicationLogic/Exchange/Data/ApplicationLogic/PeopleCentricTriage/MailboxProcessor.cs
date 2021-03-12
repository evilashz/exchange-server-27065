﻿using System;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.ApplicationLogic.PeopleCentricTriage
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxProcessor
	{
		// Token: 0x06000E5F RID: 3679 RVA: 0x0003B9DC File Offset: 0x00039BDC
		public MailboxProcessor(PeopleCentricTriageConfiguration configuration, IPeopleIKnowPublisherFactory publisherFactory, IPerformanceDataLogger perfLogger, ITracer tracer)
		{
			ArgumentValidator.ThrowIfNull("configuration", configuration);
			ArgumentValidator.ThrowIfNull("publisherFactory", publisherFactory);
			ArgumentValidator.ThrowIfNull("perfLogger", perfLogger);
			ArgumentValidator.ThrowIfNull("tracer", tracer);
			this.configuration = configuration;
			this.publisherFactory = publisherFactory;
			this.perfLogger = perfLogger;
			this.tracer = tracer;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003BA3C File Offset: 0x00039C3C
		public bool IsInteresting(MailboxProcessorRequest request, DateTime utcNow)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			if (this.MailboxTypeNotInteresting(request))
			{
				this.tracer.TraceDebug<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: {0} has a type that is NOT interesting.", request);
				return false;
			}
			if (this.LastLogonTooLongAgo(request, utcNow))
			{
				this.tracer.TraceDebug<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: {0} is NOT interesting because it's never been logged on or last logon happened too long ago.", request);
				return false;
			}
			return true;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0003BA9C File Offset: 0x00039C9C
		public void Process(MailboxProcessorRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			if (!this.IsMailboxInterestingAdditionalTests(request))
			{
				return;
			}
			this.ComputeAndPublishPeopleIKnow(request);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003BABC File Offset: 0x00039CBC
		private bool IsMailboxInterestingAdditionalTests(MailboxProcessorRequest request)
		{
			if (request.MailboxSession == null)
			{
				this.tracer.TraceError<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: NOT processing {0} because session has not been initialized.", request);
				return false;
			}
			IMailboxSession mailboxSession = request.MailboxSession as IMailboxSession;
			if (mailboxSession == null)
			{
				this.tracer.TraceDebug<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: NOT processing {0} because it's not attached to a mailbox session.", request);
				return false;
			}
			if (mailboxSession.MailboxOwner == null)
			{
				this.tracer.TraceDebug<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: NOT processing {0} because owner has not been initialized.", request);
				return false;
			}
			if (!request.IsFlightEnabled)
			{
				this.tracer.TraceDebug<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: NOT processing {0} because it's not enabled in flight.", request);
				return false;
			}
			RecipientTypeDetails recipientTypeDetails = mailboxSession.MailboxOwner.RecipientTypeDetails;
			if (recipientTypeDetails <= RecipientTypeDetails.DiscoveryMailbox)
			{
				if (recipientTypeDetails <= RecipientTypeDetails.LinkedMailbox)
				{
					if (recipientTypeDetails < RecipientTypeDetails.UserMailbox)
					{
						goto IL_FC;
					}
					switch ((int)(recipientTypeDetails - RecipientTypeDetails.UserMailbox))
					{
					case 0:
					case 1:
						return true;
					}
				}
				if (recipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox && recipientTypeDetails != RecipientTypeDetails.DiscoveryMailbox)
				{
				}
			}
			else if (recipientTypeDetails != RecipientTypeDetails.MonitoringMailbox && recipientTypeDetails != RecipientTypeDetails.GroupMailbox && recipientTypeDetails != RecipientTypeDetails.AuditLogMailbox)
			{
			}
			IL_FC:
			this.tracer.TraceDebug<MailboxProcessorRequest, RecipientTypeDetails>((long)this.GetHashCode(), "MailboxProcessor: NOT processing {0} because {1} is not a supported type.", request, mailboxSession.MailboxOwner.RecipientTypeDetails);
			return false;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003BBEC File Offset: 0x00039DEC
		private bool LastLogonTooLongAgo(MailboxProcessorRequest request, DateTime utcNow)
		{
			return request.LastLogonTime == null || request.LastLogonTime < utcNow - this.configuration.SkipMailboxInactivityThreshold;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003BC42 File Offset: 0x00039E42
		private bool MailboxTypeNotInteresting(MailboxProcessorRequest request)
		{
			return request.IsGroupMailbox || request.IsPublicFolderMailbox || request.IsSharedMailbox || request.IsTeamSiteMailbox;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003BC64 File Offset: 0x00039E64
		private void ComputeAndPublishPeopleIKnow(MailboxProcessorRequest request)
		{
			this.tracer.TraceDebug<MailboxProcessorRequest>((long)this.GetHashCode(), "MailboxProcessor: computing and publishing people I know collection for {0}", request);
			using (new StopwatchPerformanceTracker("ComputeAndPublishPeopleIKnow", this.perfLogger))
			{
				using (new StorePerformanceTracker("ComputeAndPublishPeopleIKnow", this.perfLogger))
				{
					using (new CpuPerformanceTracker("ComputeAndPublishPeopleIKnow", this.perfLogger))
					{
						try
						{
							this.publisherFactory.Create().Publish((IMailboxSession)request.MailboxSession);
						}
						catch (DefaultFolderNameClashException arg)
						{
							this.tracer.TraceError<MailboxProcessorRequest, DefaultFolderNameClashException>((long)this.GetHashCode(), "MailboxProcessor: computation and/or publishing of people I know for {0} FAILED at initialization of a default folder.  Exception: {1}", request, arg);
						}
						catch (StoragePermanentException arg2)
						{
							this.tracer.TraceError<MailboxProcessorRequest, StoragePermanentException>((long)this.GetHashCode(), "MailboxProcessor: computation and/or publishing of people I know for {0} FAILED with a PERMANENT storage exception.  Exception: {1}", request, arg2);
							throw;
						}
						catch (StorageTransientException arg3)
						{
							this.tracer.TraceError<MailboxProcessorRequest, StorageTransientException>((long)this.GetHashCode(), "MailboxProcessor: computation and/or publishing of people I know for {0} FAILED with a TRANSIENT storage exception.  Exception: {1}", request, arg3);
							throw;
						}
					}
				}
			}
		}

		// Token: 0x040007AF RID: 1967
		private readonly PeopleCentricTriageConfiguration configuration;

		// Token: 0x040007B0 RID: 1968
		private readonly IPeopleIKnowPublisherFactory publisherFactory;

		// Token: 0x040007B1 RID: 1969
		private readonly IPerformanceDataLogger perfLogger;

		// Token: 0x040007B2 RID: 1970
		private readonly ITracer tracer;
	}
}
