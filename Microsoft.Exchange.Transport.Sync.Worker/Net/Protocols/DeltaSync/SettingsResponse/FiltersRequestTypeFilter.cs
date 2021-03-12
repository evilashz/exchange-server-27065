using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsResponse
{
	// Token: 0x0200014D RID: 333
	[XmlType(TypeName = "FiltersRequestTypeFilter", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilter
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001C9BD File Offset: 0x0001ABBD
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0001C9C5 File Offset: 0x0001ABC5
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001C9D5 File Offset: 0x0001ABD5
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0001C9DD File Offset: 0x0001ABDD
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

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001C9ED File Offset: 0x0001ABED
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0001C9F5 File Offset: 0x0001ABF5
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

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001CA05 File Offset: 0x0001AC05
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0001CA20 File Offset: 0x0001AC20
		[XmlIgnore]
		public FiltersRequestTypeFilterCondition Condition
		{
			get
			{
				if (this.internalCondition == null)
				{
					this.internalCondition = new FiltersRequestTypeFilterCondition();
				}
				return this.internalCondition;
			}
			set
			{
				this.internalCondition = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001CA29 File Offset: 0x0001AC29
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0001CA44 File Offset: 0x0001AC44
		[XmlIgnore]
		public FiltersRequestTypeFilterActions Actions
		{
			get
			{
				if (this.internalActions == null)
				{
					this.internalActions = new FiltersRequestTypeFilterActions();
				}
				return this.internalActions;
			}
			set
			{
				this.internalActions = value;
			}
		}

		// Token: 0x04000553 RID: 1363
		[XmlElement(ElementName = "ExecutionOrder", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalExecutionOrder;

		// Token: 0x04000554 RID: 1364
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalExecutionOrderSpecified;

		// Token: 0x04000555 RID: 1365
		[XmlElement(ElementName = "Enabled", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalEnabled;

		// Token: 0x04000556 RID: 1366
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalEnabledSpecified;

		// Token: 0x04000557 RID: 1367
		[XmlElement(ElementName = "RunWhen", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RunWhenType internalRunWhen;

		// Token: 0x04000558 RID: 1368
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalRunWhenSpecified;

		// Token: 0x04000559 RID: 1369
		[XmlElement(Type = typeof(FiltersRequestTypeFilterCondition), ElementName = "Condition", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FiltersRequestTypeFilterCondition internalCondition;

		// Token: 0x0400055A RID: 1370
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(FiltersRequestTypeFilterActions), ElementName = "Actions", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public FiltersRequestTypeFilterActions internalActions;
	}
}
