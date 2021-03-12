using System;
using System.ComponentModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemManager.WinForms;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000119 RID: 281
	public class DataObject : ICloneable
	{
		// Token: 0x06001FE5 RID: 8165 RVA: 0x0005FFCE File Offset: 0x0005E1CE
		public DataObject()
		{
			this.Retriever = new ExchangeConfigObjectInfoRetriever();
			this.MinSupportedVersion = ExchangeObjectVersion.Exchange2003;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0005FFEC File Offset: 0x0005E1EC
		public DataObject(string name, Type type, IDataObjectInfoRetriever retriever) : this(name, type, null, retriever)
		{
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0005FFF8 File Offset: 0x0005E1F8
		private DataObject(string name, Type type, object dataObject, IDataObjectInfoRetriever retriever)
		{
			this.Name = name;
			this.Type = type;
			this.Value = dataObject;
			this.Retriever = retriever;
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00060020 File Offset: 0x0005E220
		public object Clone()
		{
			return new DataObject(this.Name, this.Type, this.Retriever)
			{
				DataObjectCreator = this.DataObjectCreator,
				MinSupportedVersion = this.MinSupportedVersion
			};
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x0006005E File Offset: 0x0005E25E
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x00060066 File Offset: 0x0005E266
		[DDIMandatoryValue]
		public string Name { get; set; }

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0006006F File Offset: 0x0005E26F
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x00060077 File Offset: 0x0005E277
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		[DDIMandatoryValue]
		public Type Type { get; set; }

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x00060080 File Offset: 0x0005E280
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x00060088 File Offset: 0x0005E288
		[DefaultValue(null)]
		public IDataObjectCreator DataObjectCreator { get; set; }

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x00060091 File Offset: 0x0005E291
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x00060099 File Offset: 0x0005E299
		[DefaultValue(typeof(ExchangeConfigObjectInfoRetriever))]
		public IDataObjectInfoRetriever Retriever { get; set; }

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x000600A2 File Offset: 0x0005E2A2
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x000600AA File Offset: 0x0005E2AA
		[DefaultValue(typeof(ExchangeObjectVersion))]
		public ExchangeObjectVersion MinSupportedVersion { get; set; }

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x000600B3 File Offset: 0x0005E2B3
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x000600BB File Offset: 0x0005E2BB
		internal object Value { get; set; }

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000600C4 File Offset: 0x0005E2C4
		public void Retrieve(string propertyName, out Type type, out PropertyDefinition propertyDefinition)
		{
			type = null;
			propertyDefinition = null;
			if (this.Retriever != null)
			{
				this.Retriever.Retrieve(this.Type, propertyName, out type, out propertyDefinition);
			}
		}
	}
}
