using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002FD RID: 765
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(RecurringDayTransitionType))]
	[XmlInclude(typeof(RecurringDateTransitionType))]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public abstract class RecurringTimeTransitionType : TransitionType
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001981 RID: 6529 RVA: 0x000286F0 File Offset: 0x000268F0
		// (set) Token: 0x06001982 RID: 6530 RVA: 0x000286F8 File Offset: 0x000268F8
		[XmlElement(DataType = "duration")]
		public string TimeOffset
		{
			get
			{
				return this.timeOffsetField;
			}
			set
			{
				this.timeOffsetField = value;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x00028701 File Offset: 0x00026901
		// (set) Token: 0x06001984 RID: 6532 RVA: 0x00028709 File Offset: 0x00026909
		public int Month
		{
			get
			{
				return this.monthField;
			}
			set
			{
				this.monthField = value;
			}
		}

		// Token: 0x04001137 RID: 4407
		private string timeOffsetField;

		// Token: 0x04001138 RID: 4408
		private int monthField;
	}
}
