using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x02000143 RID: 323
	[XmlType(TypeName = "Filter", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Filter
	{
		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x0001C551 File Offset: 0x0001A751
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x0001C559 File Offset: 0x0001A759
		[XmlIgnore]
		public int ExecutionOrder
		{
			get
			{
				return this.internalExecutionOrder;
			}
			set
			{
				this.internalExecutionOrder = value;
				this.internalExecutionOrderSpecified = true;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x0001C569 File Offset: 0x0001A769
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001C571 File Offset: 0x0001A771
		[XmlIgnore]
		public BitType Enabled
		{
			get
			{
				return this.internalEnabled;
			}
			set
			{
				this.internalEnabled = value;
				this.internalEnabledSpecified = true;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001C581 File Offset: 0x0001A781
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0001C589 File Offset: 0x0001A789
		[XmlIgnore]
		public RunWhenType RunWhen
		{
			get
			{
				return this.internalRunWhen;
			}
			set
			{
				this.internalRunWhen = value;
				this.internalRunWhenSpecified = true;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x0001C599 File Offset: 0x0001A799
		// (set) Token: 0x0600094C RID: 2380 RVA: 0x0001C5B4 File Offset: 0x0001A7B4
		[XmlIgnore]
		public Condition Condition
		{
			get
			{
				if (this.internalCondition == null)
				{
					this.internalCondition = new Condition();
				}
				return this.internalCondition;
			}
			set
			{
				this.internalCondition = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0001C5BD File Offset: 0x0001A7BD
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		[XmlIgnore]
		public Actions Actions
		{
			get
			{
				if (this.internalActions == null)
				{
					this.internalActions = new Actions();
				}
				return this.internalActions;
			}
			set
			{
				this.internalActions = value;
			}
		}

		// Token: 0x04000520 RID: 1312
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "ExecutionOrder", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		public int internalExecutionOrder;

		// Token: 0x04000521 RID: 1313
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalExecutionOrderSpecified;

		// Token: 0x04000522 RID: 1314
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "Enabled", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public BitType internalEnabled;

		// Token: 0x04000523 RID: 1315
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalEnabledSpecified;

		// Token: 0x04000524 RID: 1316
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "RunWhen", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public RunWhenType internalRunWhen;

		// Token: 0x04000525 RID: 1317
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalRunWhenSpecified;

		// Token: 0x04000526 RID: 1318
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Condition), ElementName = "Condition", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Condition internalCondition;

		// Token: 0x04000527 RID: 1319
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Actions), ElementName = "Actions", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Actions internalActions;
	}
}
