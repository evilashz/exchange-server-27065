using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000301 RID: 769
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ArrayOfTransitionsType
	{
		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x00028776 File Offset: 0x00026976
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x0002877E File Offset: 0x0002697E
		[XmlElement("RecurringDateTransition", typeof(RecurringDateTransitionType))]
		[XmlElement("Transition", typeof(TransitionType))]
		[XmlElement("AbsoluteDateTransition", typeof(AbsoluteDateTransitionType))]
		[XmlElement("RecurringDayTransition", typeof(RecurringDayTransitionType))]
		public TransitionType[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00028787 File Offset: 0x00026987
		// (set) Token: 0x06001994 RID: 6548 RVA: 0x0002878F File Offset: 0x0002698F
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x0400113D RID: 4413
		private TransitionType[] itemsField;

		// Token: 0x0400113E RID: 4414
		private string idField;
	}
}
