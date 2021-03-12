using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000232 RID: 562
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ModifiedEventType : BaseObjectChangedEventType
	{
		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000264B8 File Offset: 0x000246B8
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x000264C0 File Offset: 0x000246C0
		public int UnreadCount
		{
			get
			{
				return this.unreadCountField;
			}
			set
			{
				this.unreadCountField = value;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000264C9 File Offset: 0x000246C9
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x000264D1 File Offset: 0x000246D1
		[XmlIgnore]
		public bool UnreadCountSpecified
		{
			get
			{
				return this.unreadCountFieldSpecified;
			}
			set
			{
				this.unreadCountFieldSpecified = value;
			}
		}

		// Token: 0x04000EBD RID: 3773
		private int unreadCountField;

		// Token: 0x04000EBE RID: 3774
		private bool unreadCountFieldSpecified;
	}
}
