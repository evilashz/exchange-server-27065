using System;
using System.ComponentModel;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B7 RID: 183
	[Serializable]
	public class DataObjectProfile : ICloneable
	{
		// Token: 0x060005F2 RID: 1522 RVA: 0x00015FAE File Offset: 0x000141AE
		public DataObjectProfile()
		{
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00015FB6 File Offset: 0x000141B6
		private DataObjectProfile(string name, Type type, object dataObject, IDataObjectInfoRetriever retriever, IDataObjectValidator validator)
		{
			this.Name = name;
			this.Type = type;
			this.DataObject = dataObject;
			this.Retriever = retriever;
			this.Validator = validator;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00015FE3 File Offset: 0x000141E3
		public DataObjectProfile(string name, Type type, IDataObjectInfoRetriever retriever, IDataObjectValidator validator) : this(name, type, null, retriever, validator)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00015FF1 File Offset: 0x000141F1
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00015FF9 File Offset: 0x000141F9
		public string Name { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00016002 File Offset: 0x00014202
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0001600A File Offset: 0x0001420A
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		public Type Type { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00016013 File Offset: 0x00014213
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0001601B File Offset: 0x0001421B
		internal object DataObject { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00016024 File Offset: 0x00014224
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0001602C File Offset: 0x0001422C
		[DefaultValue(null)]
		public IDataObjectCreator DataObjectCreator { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00016035 File Offset: 0x00014235
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0001603D File Offset: 0x0001423D
		[DefaultValue(null)]
		public IDataObjectInfoRetriever Retriever { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00016046 File Offset: 0x00014246
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0001604E File Offset: 0x0001424E
		[DefaultValue(null)]
		public IDataObjectValidator Validator { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00016057 File Offset: 0x00014257
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0001605F File Offset: 0x0001425F
		internal bool HasReportCorrupted { get; set; }

		// Token: 0x06000603 RID: 1539 RVA: 0x00016068 File Offset: 0x00014268
		public object Clone()
		{
			DataObjectProfile dataObjectProfile = new DataObjectProfile(this.Name, this.Type, this.Retriever, this.Validator);
			if (PSConnectionInfoSingleton.GetInstance().Type != OrganizationType.Cloud)
			{
				dataObjectProfile.DataObject = ((this.DataObject is ICloneable) ? ((ICloneable)this.DataObject).Clone() : this.DataObject);
			}
			else if (this.DataObject != null)
			{
				ConfigurableObject configurableObject = this.DataObject as ConfigurableObject;
				if (configurableObject != null)
				{
					ConfigurableObject configurableObject2 = MockObjectInformation.CreateDummyObject(configurableObject.GetType()) as ConfigurableObject;
					configurableObject2.propertyBag = (configurableObject.propertyBag.Clone() as PropertyBag);
					dataObjectProfile.DataObject = configurableObject2;
				}
				else
				{
					dataObjectProfile.DataObject = this.DataObject;
				}
			}
			return dataObjectProfile;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00016121 File Offset: 0x00014321
		public void Retrieve(string propertyName, out Type type)
		{
			type = null;
			if (this.Retriever != null)
			{
				this.Retriever.Retrieve(this.Type, propertyName, out type);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00016141 File Offset: 0x00014341
		public ValidationError[] Validate()
		{
			return this.Validator.Validate(this.DataObject);
		}
	}
}
