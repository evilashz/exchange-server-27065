using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Ceres.Common.Utils.SeekableTextReader;
using Microsoft.Ceres.DataLossPrevention.Crawl;
using Microsoft.Ceres.Diagnostics;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Flighting;
using Microsoft.Ceres.NlpBase.RichTypes;
using Microsoft.Office.CompliancePolicy.Classification;

namespace Microsoft.Ceres.DataLossPrevention.Common
{
	// Token: 0x02000004 RID: 4
	internal class FASTClassificationItem : IClassificationItem, IDisposable
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002640 File Offset: 0x00000840
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002648 File Offset: 0x00000848
		public bool IsExcelAlternateFeedUsed { get; private set; }

		// Token: 0x06000015 RID: 21 RVA: 0x00002654 File Offset: 0x00000854
		internal FASTClassificationItem(FASTClassificationStore ruleStore, DLPClassificationOperator op, IRecordTypeDescriptor recordTypeDescriptor)
		{
			this.ruleStore = ruleStore;
			this.lastScanPropertyName = op.LastScanProperty;
			this.countPropertyName = op.CountProperty;
			this.confidencePropertyName = op.ConfidenceProperty;
			this.typePropertyName = op.TypeProperty;
			this.managedPropertiesPosition = recordTypeDescriptor[op.ManagedPropertiesField].Position;
			this.contentPosition = recordTypeDescriptor[op.ContentField].Position;
			this.propertiesPosition = recordTypeDescriptor[op.PropertiesField].Position;
			this.streamWrapper = new FASTClassificationItem.SeekableTextStreamWrapper();
			this.IsExcelAlternateFeedUsed = false;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000026F8 File Offset: 0x000008F8
		public Stream Content
		{
			get
			{
				if (VariantConfiguration.IsFeatureEnabled(38))
				{
					IBucketField bucketField = this.record[this.propertiesPosition] as IBucketField;
					if (bucketField != null)
					{
						IStringField stringField = bucketField["64AE120F-487D-445A-8D5A-5258F99CB970:XLTextForDLPClassification"] as IStringField;
						if (stringField != null)
						{
							this.IsExcelAlternateFeedUsed = true;
							SeekableStringReader reader = new SeekableStringReader(stringField.StringValue);
							this.streamWrapper.Reset(reader, this.abortCallback);
							return this.streamWrapper;
						}
					}
				}
				ITextualField textualField = this.record[this.contentPosition] as ITextualField;
				if (textualField != null)
				{
					this.streamWrapper.Reset(textualField.ContentReader, this.abortCallback);
					return this.streamWrapper;
				}
				return null;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027A4 File Offset: 0x000009A4
		public void SetClassificationResults(ICAClassificationResultCollection results)
		{
			if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
			{
				ULS.SendTraceTag(4850008U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationItem.SetClassificationResults :: saving results for [{0}]", new object[]
				{
					this.ItemId
				});
			}
			IUpdateableBucketField updateableBucketField = this.record[this.managedPropertiesPosition] as IUpdateableBucketField;
			if (updateableBucketField != null)
			{
				if (this.persistClassificationData)
				{
					updateableBucketField.AddField(this.lastScanPropertyName, StandardFields.GetStandardDateTimeField(new DateTime?(DateTime.UtcNow)), BuiltInTypes.DateTimeType);
				}
				ICollection<long?> collection = new List<long?>();
				ICollection<long?> collection2 = new List<long?>();
				ICollection<string> collection3 = new List<string>();
				HashSet<long> hashSet = new HashSet<long>();
				if (results != null && results.Count > 0)
				{
					this.resultCount = results.Count;
					for (int i = 0; i < this.resultCount; i++)
					{
						ICAClassificationResult icaclassificationResult = results[i + 1];
						if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
						{
							ULS.SendTraceTag(4850009U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationItem.SetClassificationResults :: Results Found :: package={0} ruleId={1}", new object[]
							{
								icaclassificationResult.RulePackageID,
								icaclassificationResult.ID
							});
						}
						long? resultBase = this.ruleStore.GetResultBase(icaclassificationResult.RulePackageID, icaclassificationResult.ID);
						if (resultBase == null)
						{
							ULS.SendTraceTag(6038295U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "FASTClassificationItem.SetClassificationResults :: Unkwown rule ID in result (should not happen). :: package={0} ruleId={1}", new object[]
							{
								icaclassificationResult.RulePackageID,
								icaclassificationResult.ID
							});
						}
						else
						{
							long value = resultBase.Value;
							if (!hashSet.Add(value))
							{
								ULS.SendTraceTag(5833436U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 50, "FASTClassificationItem.SetClassificationResults :: Duplicate rule entry is being ignored.  Duplicate rule entries should not happen. :: package={0} ruleId={1}", new object[]
								{
									icaclassificationResult.RulePackageID,
									icaclassificationResult.ID
								});
							}
							else
							{
								long value2 = value + (long)((int)icaclassificationResult.GetAttributeValue("BD770258-EA9C-4162-B79C-7AD408EC7CD5"));
								long value3 = value + (long)((int)icaclassificationResult.GetAttributeValue("AFF85B32-1BA9-4EDE-9286-F08A7EE5A421"));
								collection.Add(new long?(value2));
								collection2.Add(new long?(value3));
								collection3.Add(icaclassificationResult.ID);
							}
						}
					}
					if (this.persistClassificationData && this.resultCount > 0)
					{
						if (ULS.ShouldTrace(ULSCat.msoulscat_SEARCH_DataLossPrevention, 100))
						{
							ULS.SendTraceTag(4850010U, ULSCat.msoulscat_SEARCH_DataLossPrevention, 100, "FASTClassificationItem.SetClassificationResults :: Updating managed properties");
						}
						IUpdateableListField<long?> updateableListField = (IUpdateableListField<long?>)StandardFields.ListDescriptor<long?>(BuiltInTypes.Int64Type).CreateField();
						updateableListField.Value = collection;
						updateableBucketField.AddField(this.countPropertyName, updateableListField, BuiltInTypes.ListType(BuiltInTypes.Int64Type));
						updateableListField = (IUpdateableListField<long?>)StandardFields.ListDescriptor<long?>(BuiltInTypes.Int64Type).CreateField();
						updateableListField.Value = collection2;
						updateableBucketField.AddField(this.confidencePropertyName, updateableListField, BuiltInTypes.ListType(BuiltInTypes.Int64Type));
						IUpdateableListField<string> updateableListField2 = (IUpdateableListField<string>)StandardFields.ListDescriptor<string>(BuiltInTypes.StringType).CreateField();
						updateableListField2.Value = collection3;
						updateableBucketField.AddField(this.typePropertyName, updateableListField2, BuiltInTypes.ListType(BuiltInTypes.StringType));
					}
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002AAD File Offset: 0x00000CAD
		public string ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002AB5 File Offset: 0x00000CB5
		internal int ResultCount
		{
			get
			{
				return this.resultCount;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002ABD File Offset: 0x00000CBD
		internal long CharactersProcessed
		{
			get
			{
				return this.streamWrapper.CharactersProcessed;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002ACA File Offset: 0x00000CCA
		internal long ContentLength
		{
			get
			{
				return this.streamWrapper.Length;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002AD7 File Offset: 0x00000CD7
		internal void ReleaseObjects()
		{
			this.record = null;
			this.itemId = null;
			this.abortCallback = null;
			this.streamWrapper.ReleaseObjects();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002AF9 File Offset: 0x00000CF9
		internal void Reset(IRecord record, string path, bool persistClassificationData, FASTClassificationItem.AbortCallback abortCallback)
		{
			this.record = record;
			this.itemId = path;
			this.persistClassificationData = persistClassificationData;
			this.resultCount = 0;
			this.IsExcelAlternateFeedUsed = false;
			this.abortCallback = abortCallback;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002B26 File Offset: 0x00000D26
		public void Dispose()
		{
			this.streamWrapper.Dispose();
		}

		// Token: 0x04000013 RID: 19
		private IRecord record;

		// Token: 0x04000014 RID: 20
		private FASTClassificationStore ruleStore;

		// Token: 0x04000015 RID: 21
		private bool persistClassificationData;

		// Token: 0x04000016 RID: 22
		private string itemId;

		// Token: 0x04000017 RID: 23
		private int resultCount;

		// Token: 0x04000018 RID: 24
		private FASTClassificationItem.SeekableTextStreamWrapper streamWrapper;

		// Token: 0x04000019 RID: 25
		private int managedPropertiesPosition;

		// Token: 0x0400001A RID: 26
		private int contentPosition;

		// Token: 0x0400001B RID: 27
		private int propertiesPosition;

		// Token: 0x0400001C RID: 28
		private string lastScanPropertyName;

		// Token: 0x0400001D RID: 29
		private string countPropertyName;

		// Token: 0x0400001E RID: 30
		private string confidencePropertyName;

		// Token: 0x0400001F RID: 31
		private string typePropertyName;

		// Token: 0x04000020 RID: 32
		private FASTClassificationItem.AbortCallback abortCallback;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000020 RID: 32
		public delegate bool AbortCallback();

		// Token: 0x02000006 RID: 6
		private class SeekableTextStreamWrapper : Stream
		{
			// Token: 0x06000023 RID: 35 RVA: 0x00002B33 File Offset: 0x00000D33
			internal void ReleaseObjects()
			{
				this.reader.Dispose();
				this.reader = null;
				this.abortCallback = null;
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00002B4E File Offset: 0x00000D4E
			internal void Reset(SeekableTextReader reader, FASTClassificationItem.AbortCallback abortCallback)
			{
				this.charactersProcessed = 0L;
				this.reader = reader;
				this.abortCallback = abortCallback;
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000025 RID: 37 RVA: 0x00002B66 File Offset: 0x00000D66
			internal long CharactersProcessed
			{
				get
				{
					return this.charactersProcessed;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000026 RID: 38 RVA: 0x00002B6E File Offset: 0x00000D6E
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002B74 File Offset: 0x00000D74
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num = count / 2;
				int num2 = 0;
				while (num > 0 && !this.abortCallback())
				{
					int count2 = Math.Min(num, this.charBuffer.Length);
					int num3 = this.reader.Read(this.charBuffer, 0, count2);
					if (num3 == 0)
					{
						break;
					}
					num2 += Encoding.Unicode.GetBytes(this.charBuffer, 0, num3, buffer, num2);
					num -= num3;
					this.charactersProcessed += (long)num3;
				}
				return num2;
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000028 RID: 40 RVA: 0x00002BEB File Offset: 0x00000DEB
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BEE File Offset: 0x00000DEE
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600002A RID: 42 RVA: 0x00002BF1 File Offset: 0x00000DF1
			public override void Flush()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600002B RID: 43 RVA: 0x00002BF8 File Offset: 0x00000DF8
			public override long Length
			{
				get
				{
					return this.reader.Length;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600002C RID: 44 RVA: 0x00002C05 File Offset: 0x00000E05
			// (set) Token: 0x0600002D RID: 45 RVA: 0x00002C0C File Offset: 0x00000E0C
			public override long Position
			{
				get
				{
					throw new NotSupportedException();
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002C13 File Offset: 0x00000E13
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002C1A File Offset: 0x00000E1A
			public override void SetLength(long value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002C21 File Offset: 0x00000E21
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04000022 RID: 34
			private FASTClassificationItem.AbortCallback abortCallback;

			// Token: 0x04000023 RID: 35
			private long charactersProcessed;

			// Token: 0x04000024 RID: 36
			private SeekableTextReader reader;

			// Token: 0x04000025 RID: 37
			private readonly char[] charBuffer = new char[32768];
		}
	}
}
