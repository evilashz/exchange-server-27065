using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMTypes;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SettingsRequest
{
	// Token: 0x02000107 RID: 263
	[XmlType(TypeName = "Filter", Namespace = "HMSETTINGS:")]
	[Serializable]
	public class Filter
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0001B1BD File Offset: 0x000193BD
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0001B1C5 File Offset: 0x000193C5
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

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001B1D5 File Offset: 0x000193D5
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001B1DD File Offset: 0x000193DD
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

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001B1ED File Offset: 0x000193ED
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001B1F5 File Offset: 0x000193F5
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

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001B205 File Offset: 0x00019405
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001B220 File Offset: 0x00019420
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

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001B229 File Offset: 0x00019429
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001B244 File Offset: 0x00019444
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

		// Token: 0x04000441 RID: 1089
		[XmlElement(ElementName = "ExecutionOrder", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalExecutionOrder;

		// Token: 0x04000442 RID: 1090
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalExecutionOrderSpecified;

		// Token: 0x04000443 RID: 1091
		[XmlElement(ElementName = "Enabled", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BitType internalEnabled;

		// Token: 0x04000444 RID: 1092
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalEnabledSpecified;

		// Token: 0x04000445 RID: 1093
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "RunWhen", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public RunWhenType internalRunWhen;

		// Token: 0x04000446 RID: 1094
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool internalRunWhenSpecified;

		// Token: 0x04000447 RID: 1095
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Condition), ElementName = "Condition", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Condition internalCondition;

		// Token: 0x04000448 RID: 1096
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Actions), ElementName = "Actions", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSETTINGS:")]
		public Actions internalActions;
	}
}
