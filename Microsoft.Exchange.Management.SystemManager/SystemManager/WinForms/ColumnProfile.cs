using System;
using System.ComponentModel;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000B8 RID: 184
	[DDIPropertyExistInDataObject]
	public class ColumnProfile
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00016154 File Offset: 0x00014354
		public ColumnProfile()
		{
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00016163 File Offset: 0x00014363
		public ColumnProfile(string name, string dataObjectName, string mappingProperty)
		{
			this.name = name;
			this.dataObjectName = dataObjectName;
			this.mappingProperty = (string.IsNullOrEmpty(mappingProperty) ? name : mappingProperty);
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00016192 File Offset: 0x00014392
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0001619A File Offset: 0x0001439A
		[DDIMandatoryValue]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000161A3 File Offset: 0x000143A3
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x000161AB File Offset: 0x000143AB
		[DefaultValue(true)]
		public bool SupportBulkEdit
		{
			get
			{
				return this.supportBulkEdit;
			}
			set
			{
				this.supportBulkEdit = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000161B4 File Offset: 0x000143B4
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x000161BC File Offset: 0x000143BC
		[DefaultValue(false)]
		public bool IgnoreChangeTracking { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x000161C5 File Offset: 0x000143C5
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x000161CD File Offset: 0x000143CD
		[DefaultValue(null)]
		public ICustomTextConverter TextConverter
		{
			get
			{
				return this.converter;
			}
			set
			{
				this.converter = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x000161D6 File Offset: 0x000143D6
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x000161DE File Offset: 0x000143DE
		[DefaultValue(null)]
		public IPropertySetter PropertySetter
		{
			get
			{
				return this.propertySetter;
			}
			set
			{
				this.propertySetter = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000161E7 File Offset: 0x000143E7
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x000161EF File Offset: 0x000143EF
		[DefaultValue(false)]
		public bool PersistWholeObject { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000161F8 File Offset: 0x000143F8
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00016200 File Offset: 0x00014400
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		[DefaultValue(null)]
		public Type Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
				this.isTypeSpecified = true;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00016210 File Offset: 0x00014410
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x00016218 File Offset: 0x00014418
		[DDIValidLambdaExpression]
		[DefaultValue(null)]
		public string LambdaExpression { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00016221 File Offset: 0x00014421
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x00016229 File Offset: 0x00014429
		[DDIValidLambdaExpression]
		[DefaultValue(null)]
		public string OnceLambdaExpression { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00016232 File Offset: 0x00014432
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0001623A File Offset: 0x0001443A
		[DefaultValue(null)]
		public string DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
			set
			{
				this.defaultValue = value;
				this.isDefaultValueSpecified = true;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001624A File Offset: 0x0001444A
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x00016266 File Offset: 0x00014466
		public string MappingProperty
		{
			get
			{
				if (!string.IsNullOrEmpty(this.mappingProperty))
				{
					return this.mappingProperty;
				}
				return this.Name;
			}
			set
			{
				this.mappingProperty = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001626F File Offset: 0x0001446F
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x00016277 File Offset: 0x00014477
		[DefaultValue(null)]
		[DDIDataObjectNameExist]
		public string DataObjectName
		{
			get
			{
				return this.dataObjectName;
			}
			set
			{
				this.dataObjectName = value;
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00016280 File Offset: 0x00014480
		public void Retrieve(ref Type type, ref object defaultValue)
		{
			if (this.isTypeSpecified)
			{
				type = this.type;
			}
			if (this.isDefaultValueSpecified)
			{
				if (this.TextConverter != null)
				{
					defaultValue = this.TextConverter.Parse(type, this.defaultValue, null);
					return;
				}
				if (type != null && type.IsEnum)
				{
					defaultValue = Enum.Parse(type, this.defaultValue);
					return;
				}
				defaultValue = this.defaultValue;
			}
		}

		// Token: 0x040001F4 RID: 500
		private string name;

		// Token: 0x040001F5 RID: 501
		private Type type;

		// Token: 0x040001F6 RID: 502
		private string defaultValue;

		// Token: 0x040001F7 RID: 503
		private string dataObjectName;

		// Token: 0x040001F8 RID: 504
		private string mappingProperty;

		// Token: 0x040001F9 RID: 505
		private bool isTypeSpecified;

		// Token: 0x040001FA RID: 506
		private bool isDefaultValueSpecified;

		// Token: 0x040001FB RID: 507
		private ICustomTextConverter converter;

		// Token: 0x040001FC RID: 508
		private IPropertySetter propertySetter;

		// Token: 0x040001FD RID: 509
		private bool supportBulkEdit = true;
	}
}
