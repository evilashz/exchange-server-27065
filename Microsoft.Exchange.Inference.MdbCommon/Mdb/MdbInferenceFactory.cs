using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000012 RID: 18
	internal class MdbInferenceFactory
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003D95 File Offset: 0x00001F95
		protected MdbInferenceFactory()
		{
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003D9D File Offset: 0x00001F9D
		public static MdbInferenceFactory Current
		{
			get
			{
				return MdbInferenceFactory.instance.Value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003DA9 File Offset: 0x00001FA9
		internal static Hookable<MdbInferenceFactory> Instance
		{
			get
			{
				return MdbInferenceFactory.instance;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003DB0 File Offset: 0x00001FB0
		internal IDocument CreateTrainingSubDocument(IEnumerable<MdbCompositeItemIdentity> itemIterator, int maxDocumentCount, Guid mailboxGuid, Guid mdbGuid)
		{
			return this.CreateTrainingSubDocument(itemIterator, maxDocumentCount, mailboxGuid, mdbGuid, null);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003DC0 File Offset: 0x00001FC0
		internal IDocument CreateTrainingSubDocument(IEnumerable<MdbCompositeItemIdentity> itemIterator, int maxDocumentCount, Guid mailboxGuid, Guid mdbGuid, IEnumerable<Tuple<PropertyDefinition, object>> propertiesForAllNestedDocuments)
		{
			DocumentFactory documentFactory = DocumentFactory.Current;
			IDocument document = (Document)documentFactory.CreateDocument(new SimpleIdentity<Guid>(mailboxGuid), DocumentOperation.Insert, null, true);
			foreach (MdbCompositeItemIdentity mdbCompositeItemIdentity in itemIterator)
			{
				Document document2 = (Document)documentFactory.CreateDocument(new MdbCompositeItemIdentity(mdbGuid, mailboxGuid, mdbCompositeItemIdentity.ItemId, 1), DocumentOperation.Insert);
				if (propertiesForAllNestedDocuments != null)
				{
					foreach (Tuple<PropertyDefinition, object> tuple in propertiesForAllNestedDocuments)
					{
						document2.SetProperty(tuple.Item1, tuple.Item2);
					}
				}
				document.AddDocument(document2);
				if (document.NestedDocuments.Count == maxDocumentCount)
				{
					break;
				}
			}
			return document;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal Document CreateDocument(Guid mailboxGuid)
		{
			return this.CreateDocument(mailboxGuid, false);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003EB4 File Offset: 0x000020B4
		internal Document CreateDocument(Guid mailboxGuid, bool initializeNestedDocuments)
		{
			Util.ThrowOnNullArgument(mailboxGuid, "mailboxGuid");
			DocumentFactory documentFactory = DocumentFactory.Current;
			return (Document)documentFactory.CreateDocument(new SimpleIdentity<Guid>(mailboxGuid), DocumentOperation.Insert, null, initializeNestedDocuments);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003EF0 File Offset: 0x000020F0
		internal MdbDocument CreateFullDocument(IDocument miniDocument, DocumentProcessingContext documentProcessingContext, MdbPropertyMap propertyMap, PropertyDefinition[] propertySet)
		{
			Util.ThrowOnNullArgument(miniDocument, "miniDocument");
			Util.ThrowOnMismatchType<Document>(miniDocument, "miniDocument");
			Util.ThrowOnNullArgument(miniDocument.Identity, "miniDocument.Identity");
			Util.ThrowOnMismatchType<MdbCompositeItemIdentity>(miniDocument.Identity, "miniDocument.Identity");
			Util.ThrowOnNullArgument(documentProcessingContext, "documentProcessingContext");
			Util.ThrowOnNullArgument(documentProcessingContext.Session, "documentProcessingContext.Session");
			MdbDocument mdbDocument = new MdbDocument((MdbCompositeItemIdentity)miniDocument.Identity, propertySet, documentProcessingContext.Session, propertyMap, miniDocument.Operation);
			((Document)miniDocument).CopyPropertiesTo(mdbDocument);
			return mdbDocument;
		}

		// Token: 0x04000040 RID: 64
		private static readonly Hookable<MdbInferenceFactory> instance = Hookable<MdbInferenceFactory>.Create(true, new MdbInferenceFactory());
	}
}
