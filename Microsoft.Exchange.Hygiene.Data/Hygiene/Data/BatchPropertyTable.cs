using System;
using System.Data;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000065 RID: 101
	internal class BatchPropertyTable : PropertyTable
	{
		// Token: 0x060003FD RID: 1021 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		public BatchPropertyTable()
		{
			this.InitializeSchema();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public void AddPropertyValue(Guid identity, PropertyDefinition prop, object value)
		{
			if (prop == null)
			{
				throw new ArgumentNullException("prop");
			}
			if (prop.Type.Equals(typeof(DataTable)))
			{
				throw new ArgumentException("Cannot add a property of type DataTable");
			}
			this.rowIdentity = new Guid?(identity);
			base.AddPropertyValue(prop, value);
			this.rowIdentity = null;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000BA15 File Offset: 0x00009C15
		public void AddPropertyValue(ADObjectId identity, PropertyDefinition prop, object value)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.AddPropertyValue(identity.ObjectGuid, prop, value);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000BA33 File Offset: 0x00009C33
		public void AddPropertyValue(ConfigObjectId identity, PropertyDefinition prop, object value)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.AddPropertyValue(Guid.Parse(identity.ToString()), prop, value);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000BA5C File Offset: 0x00009C5C
		internal static BatchPropertyTable Deserialize(string tableXmlString)
		{
			BatchPropertyTable batchPropertyTable = new BatchPropertyTable();
			if (!string.IsNullOrWhiteSpace(tableXmlString))
			{
				using (XmlReader xmlReader = XmlReader.Create(new StringReader(tableXmlString), BatchPropertyTable.xrs))
				{
					while (xmlReader.Read())
					{
						if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "row")
						{
							throw new InvalidOperationException("XML data is invalid. Unable to deserialize xml string.");
						}
						DataRow dataRow = batchPropertyTable.NewRow();
						string value;
						if (BatchPropertyTable.GetAttributeValue(xmlReader, BatchPropertyTable.IdentityColumn, out value))
						{
							dataRow[BatchPropertyTable.IdentityColumn] = value;
						}
						if (BatchPropertyTable.GetAttributeValue(xmlReader, PropertyTable.PropertyNameCol, out value))
						{
							dataRow[PropertyTable.PropertyNameCol] = value;
						}
						if (BatchPropertyTable.GetAttributeValue(xmlReader, PropertyTable.PropertyIndexCol, out value))
						{
							dataRow[PropertyTable.PropertyIndexCol] = value;
						}
						foreach (string text in PropertyTable.PropertyColumnTypes.Keys)
						{
							if (BatchPropertyTable.GetAttributeValue(xmlReader, text, out value))
							{
								dataRow[text] = value;
							}
						}
						batchPropertyTable.Rows.Add(dataRow);
					}
				}
			}
			return batchPropertyTable;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000BB98 File Offset: 0x00009D98
		protected override DataRow AddRow(PropertyDefinition prop, object value, int propertyIndex = 0)
		{
			DataRow dataRow = base.AddRow(prop, value, propertyIndex);
			dataRow[BatchPropertyTable.IdentityColumn] = this.rowIdentity.Value;
			return dataRow;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000BBCC File Offset: 0x00009DCC
		private static bool GetAttributeValue(XmlReader reader, string attributeName, out string attributeValue)
		{
			bool result = false;
			string attribute = reader.GetAttribute(attributeName);
			attributeValue = null;
			if (!string.IsNullOrWhiteSpace(attribute))
			{
				attributeValue = attribute;
				result = true;
			}
			return result;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		private void InitializeSchema()
		{
			base.TableName = "BatchPropertyTable";
			DataColumn dataColumn = new DataColumn(BatchPropertyTable.IdentityColumn, typeof(Guid));
			base.Columns.Add(dataColumn);
			dataColumn.SetOrdinal(0);
		}

		// Token: 0x04000272 RID: 626
		internal static readonly string IdentityColumn = "id_Identity";

		// Token: 0x04000273 RID: 627
		private static readonly XmlReaderSettings xrs = new XmlReaderSettings
		{
			ConformanceLevel = ConformanceLevel.Fragment
		};

		// Token: 0x04000274 RID: 628
		private Guid? rowIdentity;
	}
}
