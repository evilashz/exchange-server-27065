using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.PeopleRelevance;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Inference.PeopleRelevance;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PeopleRelevance
{
	// Token: 0x0200021D RID: 541
	internal sealed class PeopleRelevanceFeeder
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x00076AB8 File Offset: 0x00074CB8
		public PeopleRelevanceFeeder(IPipeline pipeline)
		{
			Util.ThrowOnNullArgument(pipeline, "pipeline");
			this.peopleRelevanceProcessingPipeline = pipeline;
			this.sentItemsGenerator = new SentItemsTrainingSubDocumentGenerator();
			this.recipientInfoGenerator = new RecipientInformationGenerator();
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00076AE8 File Offset: 0x00074CE8
		internal SentItemsTrainingSubDocumentGenerator SentItemsGenerator
		{
			get
			{
				return this.sentItemsGenerator;
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00076AF0 File Offset: 0x00074CF0
		internal void ProcessMailbox(MailboxSession session, IDiagnosticsSession diagnosticsSession, bool isRecipientInfoBasedRelevanceEnabled = false)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(diagnosticsSession, "diagnosticsSession");
			PeopleRelevanceFeeder.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Processing Mailbox {0}", session.MailboxGuid);
			if (string.IsNullOrEmpty(session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()))
			{
				diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Skipping mailbox {0} because owner's primary SMTP address is blank.", new object[]
				{
					session
				});
				return;
			}
			try
			{
				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					PeopleModelItem modelData = MdbPeopleModelDataBinderFactory.Current.CreateInstance(session).GetModelData();
					PeopleRelevanceFeeder.Tracer.TraceDebug<bool, Version, DateTime>((long)this.GetHashCode(), "Got people inference model item. Default Model = {0} Version = {1} LastModifiedTime = {2} ", modelData.IsDefaultModel, modelData.Version, modelData.LastModifiedTime);
					IDocument document = null;
					IEnumerable<IRecipientInfo> enumerable = null;
					int num = 0;
					int num2 = 0;
					int num3;
					if (isRecipientInfoBasedRelevanceEnabled)
					{
						PeopleRelevanceFeeder.Tracer.TraceDebug((long)this.GetHashCode(), "Querying recipients' usage metrics");
						enumerable = this.recipientInfoGenerator.RunTrainingQuery(session);
						num3 = enumerable.Count<IRecipientInfo>();
					}
					else
					{
						PeopleRelevanceFeeder.Tracer.TraceDebug((long)this.GetHashCode(), "Running the training query from SentItems");
						document = this.sentItemsGenerator.RunTrainingQuery(session, modelData);
						num3 = document.NestedDocuments.Count;
					}
					if (num3 > 0)
					{
						IDocument document2 = PeopleRelevanceDocumentFactory.Current.CreatePeopleRelevanceDocument(modelData, document, session.MailboxGuid, session.MailboxOwner.Alias);
						ExAssert.RetailAssert(document2 != null, "People Relevance document is null");
						if (isRecipientInfoBasedRelevanceEnabled)
						{
							document2.SetProperty(PeopleRelevanceSchema.RecipientInfoEnumerable, enumerable);
						}
						document2.SetProperty(PeopleRelevanceSchema.IsBasedOnRecipientInfoData, isRecipientInfoBasedRelevanceEnabled);
						document2.SetProperty(PeopleRelevanceSchema.MailboxOwner, new MdbRecipient(session.MailboxOwner, session.PreferedCulture));
						PeopleRelevanceFeeder.Tracer.TraceDebug<IIdentity>((long)this.GetHashCode(), "Created a people relevance document Identity - {0}", document2.Identity);
						this.peopleRelevanceProcessingPipeline.ProcessDocument(document2, new DocumentProcessingContext(session));
						stopwatch.Stop();
						if (isRecipientInfoBasedRelevanceEnabled)
						{
							object obj;
							if (document2.TryGetProperty(PeopleRelevanceSchema.ContactList, out obj))
							{
								num = (obj as IDictionary<string, IInferenceRecipient>).Count<KeyValuePair<string, IInferenceRecipient>>();
							}
						}
						else
						{
							num = document.NestedDocuments.Count;
						}
						num2 = num3 - num;
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ProcessingStatisticsForPeopleRelevanceFeeder, null, new object[]
						{
							session.MailboxOwner.LegacyDn,
							num3,
							num,
							num2,
							stopwatch.Elapsed.TotalMilliseconds
						});
					}
					else
					{
						stopwatch.Stop();
					}
					PeopleRelevanceFeeder.Tracer.TraceDebug((long)this.GetHashCode(), "The people and time feeder for mailbox {0} picked {1} {2} to process. Number of {2} successfully processed {3} and number of items failed to be processed {4}. Total time taken to process this mailbox {5}.", new object[]
					{
						session.MailboxOwner.LegacyDn,
						num3,
						isRecipientInfoBasedRelevanceEnabled ? "usage metrics" : "sent items",
						num,
						num2,
						stopwatch.Elapsed.TotalMilliseconds
					});
				}
				catch (MessageSubmissionExceededException ex)
				{
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Failed to save model because it's too large.  Exception: {0}", new object[]
					{
						ex
					});
				}
				catch (QuotaExceededException ex2)
				{
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Failed to reset model or to run training query because mailbox is full.  Exception: {0}", new object[]
					{
						ex2
					});
				}
				catch (NestedDocumentCountZeroException ex3)
				{
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Failed to process training document because it's corrupt.  Nested document count is ZERO.  Exception: {0}", new object[]
					{
						ex3
					});
					throw;
				}
				catch (ObjectNotFoundException ex4)
				{
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "Failed to validate recipient cache.  Exception: {0}", new object[]
					{
						ex4
					});
				}
				catch (StorageTransientException ex5)
				{
					if (!(ex5.InnerException is MapiExceptionTimeout))
					{
						if (ex5.InnerException is MapiExceptionNetworkError)
						{
							diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Failed to read model because of network error.", new object[0]);
						}
						throw;
					}
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Failed to read model because of I/O timeout.  Exception: {0}", new object[]
					{
						ex5
					});
				}
				catch (StoragePermanentException ex6)
				{
					if (ex6.InnerException is MapiExceptionDatabaseError)
					{
						diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Failed to read model because of database-level error.  Exception: {0}", new object[]
						{
							ex6
						});
						throw;
					}
					if (!(ex6.InnerException is MapiExceptionMaxObjsExceeded))
					{
						throw;
					}
					diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, "Failed to reset model because cannot create user configuration message.  Exception: {0}", new object[]
					{
						ex6
					});
				}
			}
			catch (ComponentException ex7)
			{
				if (ex7.InnerException == null)
				{
					throw;
				}
				if (ex7.InnerException is ObjectNotFoundException)
				{
					diagnosticsSession.TraceError<ComponentException>("Received ObjectNotFoundException from the main pipeline.  Exception: {0} ", ex7);
				}
				else if (ex7.InnerException is CorruptDataException)
				{
					diagnosticsSession.TraceError<ComponentException>("Received CorruptDataException from the main pipeline.  Exception: {0}", ex7);
				}
				else
				{
					if (typeof(StoragePermanentException).IsAssignableFrom(ex7.InnerException.GetType()) || typeof(StorageTransientException).IsAssignableFrom(ex7.InnerException.GetType()) || typeof(DataSourceTransientException).IsAssignableFrom(ex7.InnerException.GetType()))
					{
						diagnosticsSession.TraceError<Exception>("Received storage or data source exception from the main pipeline.  Exception: {0}", ex7.InnerException);
						throw ex7.InnerException;
					}
					throw;
				}
			}
		}

		// Token: 0x04000C6A RID: 3178
		private const string ComponentName = "PeopleRelevanceFeeder";

		// Token: 0x04000C6B RID: 3179
		private readonly IPipeline peopleRelevanceProcessingPipeline;

		// Token: 0x04000C6C RID: 3180
		private SentItemsTrainingSubDocumentGenerator sentItemsGenerator;

		// Token: 0x04000C6D RID: 3181
		private RecipientInformationGenerator recipientInfoGenerator;

		// Token: 0x04000C6E RID: 3182
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.GeneralTracer;
	}
}
