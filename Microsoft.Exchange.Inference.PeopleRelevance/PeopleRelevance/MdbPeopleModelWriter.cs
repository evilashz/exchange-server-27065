using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Inference.MdbCommon;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x0200000B RID: 11
	internal sealed class MdbPeopleModelWriter : InferenceBaseModelWriter<MdbPeopleModelDataBinder, PeopleModelItem>
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002CA2 File Offset: 0x00000EA2
		internal MdbPeopleModelWriter(IPipelineComponentConfig config, IPipelineContext context) : base(config, context)
		{
			this.DiagnosticsSession.ComponentName = "MdbPeopleModelWriter";
			this.DiagnosticsSession.Tracer = ExTraceGlobals.MdbInferenceModelWriterTracer;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002CCC File Offset: 0x00000ECC
		public override string Description
		{
			get
			{
				return "MdbPeopleModelWriter is responsible for persisting the people inference model item to the Mdb.";
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002CD3 File Offset: 0x00000ED3
		public override string Name
		{
			get
			{
				return "MdbPeopleModelWriter";
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002CE4 File Offset: 0x00000EE4
		protected override void PrepareModelItem(IDocument document, PeopleModelItem modelItem)
		{
			object obj;
			if (document.TryGetProperty(PeopleRelevanceSchema.ContactList, out obj))
			{
				List<IInferenceRecipient> list = (obj as IDictionary<string, IInferenceRecipient>).Values.ToList<IInferenceRecipient>();
				modelItem.ContactList = list;
				if (list.Count<IInferenceRecipient>() > 0)
				{
					DateTime dateTime = list.Max((IInferenceRecipient x) => x.LastSentTime);
					if (dateTime > modelItem.LastProcessedMessageSentTime)
					{
						modelItem.LastProcessedMessageSentTime = dateTime;
					}
				}
			}
			if (document.TryGetProperty(PeopleRelevanceSchema.CurrentTimeWindowStartTime, out obj))
			{
				modelItem.CurrentTimeWindowStartTime = ((ExDateTime)obj).UniversalTime;
				modelItem.CurrentTimeWindowNumber = document.GetProperty<long>(PeopleRelevanceSchema.CurrentTimeWindowNumber);
			}
			if (document.TryGetProperty(PeopleRelevanceSchema.LastRecipientCacheValidationTime, out obj))
			{
				modelItem.LastRecipientCacheValidationTime = ((ExDateTime)obj).UniversalTime;
			}
			document.SetProperty(PeopleRelevanceSchema.PeopleModelVersion, modelItem.Version);
			modelItem.IsDefaultModel = false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002DCC File Offset: 0x00000FCC
		protected override MdbPeopleModelDataBinder GetModelDataBinder(object context)
		{
			DocumentProcessingContext documentProcessingContext = context as DocumentProcessingContext;
			if (documentProcessingContext != null)
			{
				return MdbPeopleModelDataBinderFactory.Current.CreateInstance(documentProcessingContext.Session);
			}
			throw new NullDocumentProcessingContextException();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002DF9 File Offset: 0x00000FF9
		protected override void WriteModelItem(MdbPeopleModelDataBinder dataBinder, PeopleModelItem modelItem)
		{
			MdbModelUtils.WriteModelItem<PeopleModelItem, MdbPeopleModelDataBinder>(dataBinder, modelItem);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E02 File Offset: 0x00001002
		protected override PeopleModelItem GetModelItem(MdbPeopleModelDataBinder dataBinder)
		{
			return MdbModelUtils.GetModelItem<PeopleModelItem, MdbPeopleModelDataBinder>(dataBinder);
		}

		// Token: 0x04000025 RID: 37
		private const string ComponentName = "MdbPeopleModelWriter";

		// Token: 0x04000026 RID: 38
		private const string ComponentDescription = "MdbPeopleModelWriter is responsible for persisting the people inference model item to the Mdb.";
	}
}
