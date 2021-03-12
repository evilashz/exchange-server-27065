using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200034D RID: 845
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetRemindersType : BaseRequestType
	{
		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x000296AD File Offset: 0x000278AD
		// (set) Token: 0x06001B60 RID: 7008 RVA: 0x000296B5 File Offset: 0x000278B5
		public DateTime BeginTime
		{
			get
			{
				return this.beginTimeField;
			}
			set
			{
				this.beginTimeField = value;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x000296BE File Offset: 0x000278BE
		// (set) Token: 0x06001B62 RID: 7010 RVA: 0x000296C6 File Offset: 0x000278C6
		[XmlIgnore]
		public bool BeginTimeSpecified
		{
			get
			{
				return this.beginTimeFieldSpecified;
			}
			set
			{
				this.beginTimeFieldSpecified = value;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000296CF File Offset: 0x000278CF
		// (set) Token: 0x06001B64 RID: 7012 RVA: 0x000296D7 File Offset: 0x000278D7
		public DateTime EndTime
		{
			get
			{
				return this.endTimeField;
			}
			set
			{
				this.endTimeField = value;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x000296E0 File Offset: 0x000278E0
		// (set) Token: 0x06001B66 RID: 7014 RVA: 0x000296E8 File Offset: 0x000278E8
		[XmlIgnore]
		public bool EndTimeSpecified
		{
			get
			{
				return this.endTimeFieldSpecified;
			}
			set
			{
				this.endTimeFieldSpecified = value;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000296F1 File Offset: 0x000278F1
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x000296F9 File Offset: 0x000278F9
		public int MaxItems
		{
			get
			{
				return this.maxItemsField;
			}
			set
			{
				this.maxItemsField = value;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x00029702 File Offset: 0x00027902
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0002970A File Offset: 0x0002790A
		[XmlIgnore]
		public bool MaxItemsSpecified
		{
			get
			{
				return this.maxItemsFieldSpecified;
			}
			set
			{
				this.maxItemsFieldSpecified = value;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x00029713 File Offset: 0x00027913
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0002971B File Offset: 0x0002791B
		public GetRemindersTypeReminderType ReminderType
		{
			get
			{
				return this.reminderTypeField;
			}
			set
			{
				this.reminderTypeField = value;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x00029724 File Offset: 0x00027924
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x0002972C File Offset: 0x0002792C
		[XmlIgnore]
		public bool ReminderTypeSpecified
		{
			get
			{
				return this.reminderTypeFieldSpecified;
			}
			set
			{
				this.reminderTypeFieldSpecified = value;
			}
		}

		// Token: 0x04001242 RID: 4674
		private DateTime beginTimeField;

		// Token: 0x04001243 RID: 4675
		private bool beginTimeFieldSpecified;

		// Token: 0x04001244 RID: 4676
		private DateTime endTimeField;

		// Token: 0x04001245 RID: 4677
		private bool endTimeFieldSpecified;

		// Token: 0x04001246 RID: 4678
		private int maxItemsField;

		// Token: 0x04001247 RID: 4679
		private bool maxItemsFieldSpecified;

		// Token: 0x04001248 RID: 4680
		private GetRemindersTypeReminderType reminderTypeField;

		// Token: 0x04001249 RID: 4681
		private bool reminderTypeFieldSpecified;
	}
}
