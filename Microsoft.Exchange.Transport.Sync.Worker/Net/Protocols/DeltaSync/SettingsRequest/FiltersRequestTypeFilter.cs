using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000117 RID: 279
	[XmlType(TypeName = "FiltersRequestTypeFilter", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class FiltersRequestTypeFilter
	{
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x0600083E RID: 2110 RVA: 0x0001B799 File Offset: 0x00019999
		// (set) Token: 0x0600083F RID: 2111 RVA: 0x0001B7A1 File Offset: 0x000199A1
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

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0001B7B1 File Offset: 0x000199B1
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0001B7B9 File Offset: 0x000199B9
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

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0001B7C9 File Offset: 0x000199C9
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0001B7D1 File Offset: 0x000199D1
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

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001B7E1 File Offset: 0x000199E1
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0001B7FC File Offset: 0x000199FC
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001B805 File Offset: 0x00019A05
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0001B820 File Offset: 0x00019A20
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

		// Token: 0x0400047A RID: 1146
		[XmlElement(ElementName = "ExecutionOrder", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalExecutionOrder;

		// Token: 0x0400047B RID: 1147
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalExecutionOrderSpecified;

		// Token: 0x0400047C RID: 1148
		[XmlElement(ElementName = "Enabled", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalEnabled;

		// Token: 0x0400047D RID: 1149
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalEnabledSpecified;

		// Token: 0x0400047E RID: 1150
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "RunWhen", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public RunWhenType internalRunWhen;

		// Token: 0x0400047F RID: 1151
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalRunWhenSpecified;

		// Token: 0x04000480 RID: 1152
		[XmlElement(Type = typeof(FiltersRequestTypeFilterCondition), ElementName = "Condition", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FiltersRequestTypeFilterCondition internalCondition;

		// Token: 0x04000481 RID: 1153
		[XmlElement(Type = typeof(FiltersRequestTypeFilterActions), ElementName = "Actions", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FiltersRequestTypeFilterActions internalActions;
	}
}
