using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.Engine
{
	// Token: 0x02000003 RID: 3
	internal class DocumentTracker : IDocumentTracker, IDiagnosable
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002430 File Offset: 0x00000630
		internal DocumentTracker()
		{
			this.tracingContext = this.GetHashCode();
			this.TrackedDocuments = new HashSet<DocumentTracker.DocumentInfo>();
			this.RetriablePoisonDocumentsToStamp = new HashSet<long>();
			this.PermanentPoisonDocumentsToStamp = new HashSet<long>();
			this.PermanentPoisonDocuments = new HashSet<long>();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002494 File Offset: 0x00000694
		public int PoisonDocumentsCount
		{
			get
			{
				int count;
				lock (this.lockObject)
				{
					count = this.PermanentPoisonDocuments.Count;
				}
				return count;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000024DC File Offset: 0x000006DC
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000024E4 File Offset: 0x000006E4
		internal HashSet<DocumentTracker.DocumentInfo> TrackedDocuments { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000024ED File Offset: 0x000006ED
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000024F5 File Offset: 0x000006F5
		internal HashSet<long> RetriablePoisonDocumentsToStamp { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024FE File Offset: 0x000006FE
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002506 File Offset: 0x00000706
		internal HashSet<long> PermanentPoisonDocumentsToStamp { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000250F File Offset: 0x0000070F
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002517 File Offset: 0x00000717
		internal HashSet<long> PermanentPoisonDocuments { get; private set; }

		// Token: 0x0600001E RID: 30 RVA: 0x00002520 File Offset: 0x00000720
		public void Initialize(IFailedItemStorage failedItemStorage)
		{
			if (!this.initialized)
			{
				lock (this.lockObject)
				{
					this.PermanentPoisonDocuments = new HashSet<long>(failedItemStorage.GetPoisonDocuments());
					this.initialized = true;
					this.tracer.TraceDebug<int>((long)this.tracingContext, "Permanent Poison Documents collection initialized with count: {0}", this.PermanentPoisonDocuments.Count);
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000259C File Offset: 0x0000079C
		public void RecordDocumentProcessing(Guid instanceId, Guid correlationId, long docId)
		{
			DocumentTracker.DocumentInfo item = new DocumentTracker.DocumentInfo(instanceId, correlationId, docId);
			lock (this.lockObject)
			{
				this.TrackedDocuments.Remove(item);
				this.TrackedDocuments.Add(item);
				this.tracer.TraceDebug<Guid>((long)this.tracingContext, "Tracking document with CorrelationId: {0}", correlationId);
				this.tracer.TraceDebug<int>((long)this.tracingContext, "Total number of documents tracked by this Tracker: {0}", this.TrackedDocuments.Count);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002634 File Offset: 0x00000834
		public void RecordDocumentProcessingComplete(Guid correlationId, long docId, bool isTrackedAsPoison)
		{
			lock (this.lockObject)
			{
				foreach (DocumentTracker.DocumentInfo documentInfo in this.TrackedDocuments)
				{
					if (documentInfo.CorrelationId.Equals(correlationId))
					{
						this.TrackedDocuments.Remove(documentInfo);
						break;
					}
				}
				if (isTrackedAsPoison)
				{
					this.RetriablePoisonDocumentsToStamp.Remove(docId);
					if (this.PermanentPoisonDocumentsToStamp.Remove(docId))
					{
						this.PermanentPoisonDocuments.Add(docId);
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026F8 File Offset: 0x000008F8
		public void MarkCurrentlyTrackedDocumentsAsPoison()
		{
			lock (this.lockObject)
			{
				foreach (DocumentTracker.DocumentInfo documentInfo in this.TrackedDocuments)
				{
					if (!this.PermanentPoisonDocumentsToStamp.Contains(documentInfo.DocId) && !this.PermanentPoisonDocuments.Contains(documentInfo.DocId))
					{
						this.RetriablePoisonDocumentsToStamp.Add(documentInfo.DocId);
						this.tracer.TraceDebug<Guid>((long)this.tracingContext, "Moving currently tracked document to be considered poisonous. CorrelationId: {0}", documentInfo.CorrelationId);
					}
				}
				this.TrackedDocuments.Clear();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027D0 File Offset: 0x000009D0
		public void MarkDocumentAsPoison(long docId)
		{
			if (IndexId.IsWatermarkIndexId(docId))
			{
				return;
			}
			lock (this.lockObject)
			{
				this.PermanentPoisonDocumentsToStamp.Add(docId);
				this.RetriablePoisonDocumentsToStamp.Remove(docId);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002830 File Offset: 0x00000A30
		public void MarkDocumentAsRetriablePoison(long docId)
		{
			if (IndexId.IsWatermarkIndexId(docId))
			{
				return;
			}
			lock (this.lockObject)
			{
				if (!this.PermanentPoisonDocumentsToStamp.Contains(docId))
				{
					this.RetriablePoisonDocumentsToStamp.Add(docId);
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002890 File Offset: 0x00000A90
		public int ShouldDocumentBeStampedWithError(long docId)
		{
			int result = 0;
			lock (this.lockObject)
			{
				if (this.PermanentPoisonDocumentsToStamp.Contains(docId))
				{
					result = EvaluationErrorsHelper.MakePermanentError(EvaluationErrors.PoisonDocument);
				}
				else if (this.RetriablePoisonDocumentsToStamp.Contains(docId))
				{
					result = EvaluationErrorsHelper.MakeRetriableError(EvaluationErrors.PoisonDocument);
				}
			}
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000028FC File Offset: 0x00000AFC
		public bool ShouldDocumentBeSkipped(long docId)
		{
			return this.PermanentPoisonDocuments.Contains(docId);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000290C File Offset: 0x00000B0C
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(this.GetDiagnosticComponentName());
			foreach (DocumentTracker.DocumentInfo documentInfo in this.TrackedDocuments)
			{
				int mailboxNumber = IndexId.GetMailboxNumber(documentInfo.DocId);
				int documentId = IndexId.GetDocumentId(documentInfo.DocId);
				XElement xelement2 = new XElement("DocumentInformation");
				xelement2.Add(new XElement("FlowInstance", documentInfo.InstanceId));
				xelement2.Add(new XElement("MailboxNumber", mailboxNumber));
				xelement2.Add(new XElement("DocumentId", documentId));
				xelement2.Add(new XElement("CorrelationId", documentInfo.CorrelationId));
				xelement2.Add(new XElement("TimeStamp", documentInfo.TimeStamp));
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A40 File Offset: 0x00000C40
		public string GetDiagnosticComponentName()
		{
			return base.GetType().Name;
		}

		// Token: 0x0400000A RID: 10
		private readonly Trace tracer = ExTraceGlobals.DocumentTrackerOperatorTracer;

		// Token: 0x0400000B RID: 11
		private readonly int tracingContext;

		// Token: 0x0400000C RID: 12
		private readonly object lockObject = new object();

		// Token: 0x0400000D RID: 13
		private bool initialized;

		// Token: 0x02000004 RID: 4
		public class DocumentInfo : IEquatable<DocumentTracker.DocumentInfo>
		{
			// Token: 0x06000028 RID: 40 RVA: 0x00002A4D File Offset: 0x00000C4D
			public DocumentInfo(Guid instanceId, Guid correlationId, long docId)
			{
				this.DocId = docId;
				this.CorrelationId = correlationId;
				this.InstanceId = instanceId;
				this.TimeStamp = DateTime.UtcNow;
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002A75 File Offset: 0x00000C75
			// (set) Token: 0x0600002A RID: 42 RVA: 0x00002A7D File Offset: 0x00000C7D
			public long DocId { get; private set; }

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002A86 File Offset: 0x00000C86
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002A8E File Offset: 0x00000C8E
			public Guid CorrelationId { get; private set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x0600002D RID: 45 RVA: 0x00002A97 File Offset: 0x00000C97
			// (set) Token: 0x0600002E RID: 46 RVA: 0x00002A9F File Offset: 0x00000C9F
			public Guid InstanceId { get; private set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x0600002F RID: 47 RVA: 0x00002AA8 File Offset: 0x00000CA8
			// (set) Token: 0x06000030 RID: 48 RVA: 0x00002AB0 File Offset: 0x00000CB0
			public DateTime TimeStamp { get; private set; }

			// Token: 0x06000031 RID: 49 RVA: 0x00002ABC File Offset: 0x00000CBC
			public bool Equals(DocumentTracker.DocumentInfo other)
			{
				return this.InstanceId.Equals(other.InstanceId);
			}

			// Token: 0x06000032 RID: 50 RVA: 0x00002AE0 File Offset: 0x00000CE0
			public override int GetHashCode()
			{
				return this.InstanceId.GetHashCode();
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00002B01 File Offset: 0x00000D01
			public override string ToString()
			{
				return string.Format("InstanceId: {0}, DocumentId: {1}, CorrelationId: {2},", this.InstanceId, this.DocId, this.CorrelationId);
			}
		}
	}
}
