using System;
using System.Collections.Generic;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001AB RID: 427
	internal class SchemaState
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x00062CF0 File Offset: 0x00060EF0
		protected SchemaState()
		{
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00062CFF File Offset: 0x00060EFF
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x00062D07 File Offset: 0x00060F07
		protected List<IProperty>[] ConversionTable
		{
			get
			{
				return this.conversionTable;
			}
			set
			{
				this.conversionTable = value;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00062D10 File Offset: 0x00060F10
		protected int LinkedSchemas
		{
			get
			{
				return this.linkedSchemas;
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00062D18 File Offset: 0x00060F18
		public void AddProperty(IProperty[] linkedPropertyList)
		{
			if (linkedPropertyList == null)
			{
				throw new ArgumentNullException("linkedPropertyList");
			}
			if (this.linkedSchemas != linkedPropertyList.Length)
			{
				throw new ArgumentException("linkedPropertyList supports a const number of linked schemas");
			}
			int count = this.conversionTable[0].Count;
			for (int i = 0; i < linkedPropertyList.Length; i++)
			{
				linkedPropertyList[i].SchemaLinkId = count;
				this.conversionTable[i].Add(linkedPropertyList[i]);
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00062D7E File Offset: 0x00060F7E
		public List<IProperty> GetSchema(int schemaNumber)
		{
			return this.conversionTable[schemaNumber];
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00062D88 File Offset: 0x00060F88
		protected void InitConversionTable(int linkedSchemas)
		{
			if (linkedSchemas <= 0)
			{
				throw new ArgumentException("cLinkedSchemas must be > 0");
			}
			this.linkedSchemas = linkedSchemas;
			this.conversionTable = new List<IProperty>[this.linkedSchemas];
			for (int i = 0; i < this.linkedSchemas; i++)
			{
				this.conversionTable[i] = new List<IProperty>();
			}
		}

		// Token: 0x04000B4C RID: 2892
		private List<IProperty>[] conversionTable;

		// Token: 0x04000B4D RID: 2893
		private int linkedSchemas = -1;
	}
}
