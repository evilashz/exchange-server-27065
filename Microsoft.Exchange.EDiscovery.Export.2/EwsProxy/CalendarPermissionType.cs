using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000208 RID: 520
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class CalendarPermissionType : BasePermissionType
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00025F35 File Offset: 0x00024135
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x00025F3D File Offset: 0x0002413D
		public CalendarPermissionReadAccessType ReadItems
		{
			get
			{
				return this.readItemsField;
			}
			set
			{
				this.readItemsField = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00025F46 File Offset: 0x00024146
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00025F4E File Offset: 0x0002414E
		[XmlIgnore]
		public bool ReadItemsSpecified
		{
			get
			{
				return this.readItemsFieldSpecified;
			}
			set
			{
				this.readItemsFieldSpecified = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00025F57 File Offset: 0x00024157
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00025F5F File Offset: 0x0002415F
		public CalendarPermissionLevelType CalendarPermissionLevel
		{
			get
			{
				return this.calendarPermissionLevelField;
			}
			set
			{
				this.calendarPermissionLevelField = value;
			}
		}

		// Token: 0x04000E45 RID: 3653
		private CalendarPermissionReadAccessType readItemsField;

		// Token: 0x04000E46 RID: 3654
		private bool readItemsFieldSpecified;

		// Token: 0x04000E47 RID: 3655
		private CalendarPermissionLevelType calendarPermissionLevelField;
	}
}
