using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000206 RID: 518
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class CalendarPermissionSetType
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00025E04 File Offset: 0x00024004
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x00025E0C File Offset: 0x0002400C
		[XmlArrayItem("CalendarPermission", IsNullable = false)]
		public CalendarPermissionType[] CalendarPermissions
		{
			get
			{
				return this.calendarPermissionsField;
			}
			set
			{
				this.calendarPermissionsField = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00025E15 File Offset: 0x00024015
		// (set) Token: 0x060014A7 RID: 5287 RVA: 0x00025E1D File Offset: 0x0002401D
		[XmlArrayItem("UnknownEntry", IsNullable = false)]
		public string[] UnknownEntries
		{
			get
			{
				return this.unknownEntriesField;
			}
			set
			{
				this.unknownEntriesField = value;
			}
		}

		// Token: 0x04000E34 RID: 3636
		private CalendarPermissionType[] calendarPermissionsField;

		// Token: 0x04000E35 RID: 3637
		private string[] unknownEntriesField;
	}
}
