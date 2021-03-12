using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Ceres.ContentEngine.DataModel.RecordSerializer;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x02000010 RID: 16
	public class IndexableRecordValidation
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004E0C File Offset: 0x0000300C
		private IndexableRecordValidation()
		{
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004E14 File Offset: 0x00003014
		internal static IndexableRecordValidation Instance
		{
			get
			{
				return IndexableRecordValidation.instance;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004E1C File Offset: 0x0000301C
		public void ValidateIndexableRecord(IRecord record, string flowIdentifier, IRecordTypeDescriptor recordTypeDescriptor, bool isWatermark)
		{
			if (!SearchConfig.Instance.ValidateIndexableRecordEnabled)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			new StringBuilder();
			long num = -1L;
			try
			{
				num = ((IInt64Field)record["indexid"]).Int64Value;
				flag = (IndexId.IsWatermarkIndexId(num) != isWatermark);
			}
			catch (SchemaException)
			{
				flag = true;
			}
			Guid guid = Guid.Empty;
			try
			{
				guid = ((IGuidField)record["tenantid"]).GuidValue.GetValueOrDefault();
				flag2 = (guid == WatermarkStorageId.FastWatermarkTenantId != isWatermark);
			}
			catch (SchemaException)
			{
				flag2 = true;
			}
			try
			{
				if (isWatermark)
				{
					flag3 = (((IInt64Field)record[FastIndexSystemSchema.Watermark.Name]).NullableInt64Value == null);
				}
			}
			catch (SchemaException)
			{
				flag3 = true;
			}
			if (flag2 || flag || flag3)
			{
				Guid guid2 = Guid.Empty;
				try
				{
					guid2 = ((IGuidField)record["CorrelationId"]).GuidValue.GetValueOrDefault();
				}
				catch (SchemaException)
				{
				}
				string text = string.Empty;
				try
				{
					text = ((IStringField)record["compositeitemid"]).StringValue;
				}
				catch (SchemaException)
				{
				}
				foreach (string text2 in IndexableRecordValidation.PropertiesToNull)
				{
					try
					{
						((IUpdateableField)record[text2]).Value = null;
					}
					catch (SchemaException)
					{
					}
				}
				MemoryStream memoryStream = new MemoryStream(1024);
				XmlSerializer xmlSerializer = new XmlSerializer
				{
					DocumentElementName = "Records",
					FieldElementName = "Field",
					RecordElementName = "Record",
					NameOutputMode = 0,
					ValueOutputMode = 0
				};
				xmlSerializer.Configure(record.Descriptor.Type);
				xmlSerializer.Start(memoryStream, null);
				xmlSerializer.WriteRecord(record, memoryStream);
				xmlSerializer.End();
				memoryStream.Flush();
				memoryStream.Position = 0L;
				StreamReader streamReader = new StreamReader(memoryStream);
				throw new InvalidOperationException(string.Format("InvalidRecordDetected. Flow Instance: {0}, CorrelationId: {1}, DocumentId: {2}, DocumentId Validation: {3}, TenantId: {4}, TenantId Validation: {5}, WatermarkValue Validation: {6}, CompositeItemId: {7}, RecordOutput:{8}{9}", new object[]
				{
					flowIdentifier,
					guid2,
					num,
					flag ? "Failed" : "Passed",
					guid,
					flag2 ? "Failed" : "Passed",
					flag3 ? "Failed" : "Passed",
					text,
					Environment.NewLine,
					streamReader.ReadToEnd()
				}));
			}
		}

		// Token: 0x0400005C RID: 92
		private static readonly IndexableRecordValidation instance = new IndexableRecordValidation();

		// Token: 0x0400005D RID: 93
		private static readonly List<string> PropertiesToNull = new List<string>
		{
			"tempbody",
			"mailboxlanguagedetectiontext",
			"bodypreview",
			"newbody",
			"body"
		};
	}
}
