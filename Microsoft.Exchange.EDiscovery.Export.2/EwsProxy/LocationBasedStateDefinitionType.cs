using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002BB RID: 699
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class LocationBasedStateDefinitionType : BaseCalendarItemStateDefinitionType
	{
		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00027A30 File Offset: 0x00025C30
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x00027A38 File Offset: 0x00025C38
		public string OrganizerLocation
		{
			get
			{
				return this.organizerLocationField;
			}
			set
			{
				this.organizerLocationField = value;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00027A41 File Offset: 0x00025C41
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x00027A49 File Offset: 0x00025C49
		public string AttendeeLocation
		{
			get
			{
				return this.attendeeLocationField;
			}
			set
			{
				this.attendeeLocationField = value;
			}
		}

		// Token: 0x0400104E RID: 4174
		private string organizerLocationField;

		// Token: 0x0400104F RID: 4175
		private string attendeeLocationField;
	}
}
