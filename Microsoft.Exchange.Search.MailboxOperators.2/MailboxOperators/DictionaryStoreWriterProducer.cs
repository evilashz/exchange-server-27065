using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Ceres.NlpBase.DictionaryInterface.Resource;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.Query;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000003 RID: 3
	internal class DictionaryStoreWriterProducer : ExchangeWriterBase<DictionaryStoreWriterOperator>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		public DictionaryStoreWriterProducer(IEvaluationContext context, bool forward) : base(context, forward, ExTraceGlobals.RetrieverOperatorTracer)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020F0 File Offset: 0x000002F0
		protected override bool UsesAutoFlush
		{
			get
			{
				return base.CallbackInterval > 1;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020FB File Offset: 0x000002FB
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DictionaryStoreWriterProducer>(this);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002103 File Offset: 0x00000303
		protected override bool FlushWriter()
		{
			return true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002106 File Offset: 0x00000306
		protected override void CloseWriter()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002108 File Offset: 0x00000308
		protected override void Open()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000210C File Offset: 0x0000030C
		protected override void InternalWrite(IRecord record)
		{
			Guid mailboxGuid = (Guid)base.Context.PopProperty("MailboxGuid");
			Guid mdbGuid = (Guid)base.Context.PopProperty("DatabaseGuid");
			MdbItemIdentity identity = new MdbItemIdentity(null, mdbGuid, mailboxGuid, 0, StoreObjectId.DummyId, 0, false);
			Dictionary<string, string> dictionaryAssemblyProperties = this.GetDictionaryAssemblyProperties(record);
			using (LohFriendlyStream lohFriendlyStream = new LohFriendlyStream(0))
			{
				Dictionary<string, byte[]> dictionary = new Dictionary<string, byte[]>
				{
					{
						"Dictionary",
						record["Dictionary"].Value as byte[]
					},
					{
						"DictionaryManipulationPipeline",
						record["DictionaryManipulationPipeline"].Value as byte[]
					}
				};
				ZipFileResourceCreator.BuildZipResource("Search.TopN", DictionaryStoreWriterProducer.CurrentVersion, lohFriendlyStream, dictionaryAssemblyProperties, dictionary);
				lohFriendlyStream.Position = 0L;
				if (lohFriendlyStream.Length <= 0L)
				{
					throw new ArgumentException("Dictionary Stream is empty.");
				}
				StoreSession storeSession = null;
				bool discard = true;
				try
				{
					storeSession = StoreSessionManager.GetWritableStoreSessionFromCache(identity, false, true);
					using (UserConfiguration searchDictionaryItem = SearchDictionary.GetSearchDictionaryItem((MailboxSession)storeSession, "Search.TopN"))
					{
						using (Stream stream = searchDictionaryItem.GetStream())
						{
							SearchDictionary.SerializeTo(stream, lohFriendlyStream, 1);
							searchDictionaryItem.Save(SaveMode.NoConflictResolutionForceSave);
							discard = false;
						}
					}
				}
				finally
				{
					if (storeSession != null)
					{
						StoreSessionManager.ReturnStoreSessionToCache(ref storeSession, discard);
					}
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002298 File Offset: 0x00000498
		private Dictionary<string, string> GetDictionaryAssemblyProperties(IRecord record)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			IBucketField bucketField = (IBucketField)record["DictionaryProperties"];
			for (int i = 0; i < bucketField.FieldCount; i++)
			{
				object value = bucketField[i].Value;
				if (value != null)
				{
					string key = bucketField.Name(i);
					string value2 = value.ToString();
					dictionary.Add(key, value2);
				}
			}
			return dictionary;
		}

		// Token: 0x04000001 RID: 1
		private static readonly string CurrentVersion = new Version(1, 0, 0, 1).ToString();
	}
}
