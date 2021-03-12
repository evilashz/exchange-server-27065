using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.Search.Core.DocumentModel;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x0200000B RID: 11
	internal class Document : IDocument, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002827 File Offset: 0x00000A27
		public Document(IIdentity identity, DocumentOperation operation, IDocumentAdapter documentAdapter, bool initializeNestedDocuments) : this(identity, operation, documentAdapter)
		{
			if (initializeNestedDocuments)
			{
				this.internalNestedDocuments = new List<IDocument>();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002844 File Offset: 0x00000A44
		public Document(IIdentity identity, DocumentOperation operation, IDocumentAdapter documentAdapter)
		{
			Util.ThrowOnNullArgument(identity, "identity");
			this.propertyBag = new PropertyBag();
			this.documentAdapter = documentAdapter;
			this.propertyBag.SetProperty<IIdentity>(DocumentSchema.Identity, identity);
			this.propertyBag.SetProperty<DocumentOperation>(DocumentSchema.Operation, operation);
			this.diagnosticsSession = DiagnosticsSession.CreateDocumentDiagnosticsSession(identity, ExTraceGlobals.CoreDocumentModelTracer);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028B2 File Offset: 0x00000AB2
		public IIdentity Identity
		{
			get
			{
				return this.GetProperty<IIdentity>(DocumentSchema.Identity);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028BF File Offset: 0x00000ABF
		public DocumentOperation Operation
		{
			get
			{
				return this.GetProperty<DocumentOperation>(DocumentSchema.Operation);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000028CC File Offset: 0x00000ACC
		public IIdentity ParentIdentity
		{
			get
			{
				object obj = null;
				this.TryGetProperty(DocumentSchema.ParentIdentity, out obj);
				return (IIdentity)obj;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028EF File Offset: 0x00000AEF
		public ReadOnlyCollection<IDocument> NestedDocuments
		{
			get
			{
				if (this.internalNestedDocuments == null)
				{
					return null;
				}
				if (this.nestedDocuments == null)
				{
					this.nestedDocuments = new ReadOnlyCollection<IDocument>(this.internalNestedDocuments);
				}
				return this.nestedDocuments;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000291A File Offset: 0x00000B1A
		public ICollection<DocumentFailureDescription> Failures
		{
			get
			{
				return this.failures;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002922 File Offset: 0x00000B22
		protected IDiagnosticsSession DiagnosticSession
		{
			get
			{
				return this.diagnosticsSession;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000292A File Offset: 0x00000B2A
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002932 File Offset: 0x00000B32
		protected IDocumentAdapter DocumentAdapter
		{
			get
			{
				return this.documentAdapter;
			}
			set
			{
				this.documentAdapter = value;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000293C File Offset: 0x00000B3C
		public TPropertyValue GetProperty<TPropertyValue>(PropertyDefinition property)
		{
			object obj;
			if (!this.TryGetProperty(property, out obj))
			{
				throw new PropertyErrorException(property.Name);
			}
			if (!typeof(TPropertyValue).IsAssignableFrom(obj.GetType()))
			{
				throw new PropertyTypeErrorException(property.Name);
			}
			return (TPropertyValue)((object)obj);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000298C File Offset: 0x00000B8C
		public virtual bool TryGetProperty(PropertyDefinition property, out object value)
		{
			this.DiagnosticSession.TraceDebug<PropertyDefinition>("DocumentModel - GetProperty: {0}", property);
			bool result;
			if (this.documentAdapter != null && this.documentAdapter.ContainsPropertyMapping(property))
			{
				this.DiagnosticSession.TraceDebug<PropertyDefinition>("DocumentModel - Getting property {0} from adapter", property);
				result = this.documentAdapter.TryGetProperty(property, out value);
			}
			else
			{
				this.DiagnosticSession.TraceDebug<PropertyDefinition>("DocumentModel - Getting property {0} from property bag", property);
				result = this.propertyBag.TryGetProperty(property, out value);
			}
			this.DiagnosticSession.TraceDebug<PropertyDefinition, object>("DocumentModel - GotProperty: {0},{1}", property, value);
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A18 File Offset: 0x00000C18
		public virtual void SetProperty(PropertyDefinition property, object value)
		{
			this.DiagnosticSession.TraceDebug<PropertyDefinition, object>("DocumentModel - SetProperty: {0}, {1}", property, value);
			if (this.documentAdapter != null && this.documentAdapter.ContainsPropertyMapping(property))
			{
				this.DiagnosticSession.TraceDebug<PropertyDefinition>("DocumentModel - Setting property {0} through the adapter", property);
				this.documentAdapter.SetProperty(property, value);
				return;
			}
			if (this.DiagnosticSession.IsTraceEnabled(TraceType.DebugTrace))
			{
				object arg = null;
				this.TryGetProperty(property, out arg);
				this.DiagnosticSession.TraceDebug<PropertyDefinition, object>("DocumentModel - SetProperty: {0}, original value {1}:", property, arg);
			}
			this.DiagnosticSession.TraceDebug<PropertyDefinition>("DocumentModel - Setting property {0} onto the property bag", property);
			this.propertyBag.SetProperty(property, value);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002AB6 File Offset: 0x00000CB6
		public void AddDocument(IDocument document)
		{
			Util.ThrowOnNullArgument(document, "document");
			if (this.internalNestedDocuments == null)
			{
				this.internalNestedDocuments = new List<IDocument>();
			}
			this.internalNestedDocuments.Add(document);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AE2 File Offset: 0x00000CE2
		public int RemoveDocuments(ICollection<IDocument> documentsToRemove)
		{
			Util.ThrowOnNullOrEmptyArgument<IDocument>(documentsToRemove, "documentsToRemove");
			if (this.internalNestedDocuments == null)
			{
				return 0;
			}
			return this.internalNestedDocuments.RemoveAll(new Predicate<IDocument>(documentsToRemove.Contains));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B14 File Offset: 0x00000D14
		public void CopyPropertiesTo(Document document)
		{
			Util.ThrowOnNullArgument(document, "document");
			foreach (KeyValuePair<PropertyDefinition, object> keyValuePair in this.propertyBag.Values)
			{
				document.SetProperty(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B80 File Offset: 0x00000D80
		private void SetProperty<TPropertyValue>(PropertyDefinition property, TPropertyValue value)
		{
			this.propertyBag.SetProperty<TPropertyValue>(property, value);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B90 File Offset: 0x00000D90
		private bool TryGetProperty<TPropertyValue>(PropertyDefinition property, out object value)
		{
			bool flag = this.TryGetProperty(property, out value);
			if (flag)
			{
				Util.ThrowOnMismatchType<TPropertyValue>(value, property.Name);
			}
			return flag;
		}

		// Token: 0x0400001E RID: 30
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x0400001F RID: 31
		private readonly List<DocumentFailureDescription> failures = new List<DocumentFailureDescription>();

		// Token: 0x04000020 RID: 32
		private PropertyBag propertyBag;

		// Token: 0x04000021 RID: 33
		private IDocumentAdapter documentAdapter;

		// Token: 0x04000022 RID: 34
		private ReadOnlyCollection<IDocument> nestedDocuments;

		// Token: 0x04000023 RID: 35
		private List<IDocument> internalNestedDocuments;
	}
}
