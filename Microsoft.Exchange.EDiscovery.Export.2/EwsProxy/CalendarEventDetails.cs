using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200027D RID: 637
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CalendarEventDetails
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000276C9 File Offset: 0x000258C9
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x000276D1 File Offset: 0x000258D1
		public string ID
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

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x000276DA File Offset: 0x000258DA
		// (set) Token: 0x06001797 RID: 6039 RVA: 0x000276E2 File Offset: 0x000258E2
		public string Subject
		{
			get
			{
				return this.subjectField;
			}
			set
			{
				this.subjectField = value;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x000276EB File Offset: 0x000258EB
		// (set) Token: 0x06001799 RID: 6041 RVA: 0x000276F3 File Offset: 0x000258F3
		public string Location
		{
			get
			{
				return this.locationField;
			}
			set
			{
				this.locationField = value;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x000276FC File Offset: 0x000258FC
		// (set) Token: 0x0600179B RID: 6043 RVA: 0x00027704 File Offset: 0x00025904
		public bool IsMeeting
		{
			get
			{
				return this.isMeetingField;
			}
			set
			{
				this.isMeetingField = value;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0002770D File Offset: 0x0002590D
		// (set) Token: 0x0600179D RID: 6045 RVA: 0x00027715 File Offset: 0x00025915
		public bool IsRecurring
		{
			get
			{
				return this.isRecurringField;
			}
			set
			{
				this.isRecurringField = value;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0002771E File Offset: 0x0002591E
		// (set) Token: 0x0600179F RID: 6047 RVA: 0x00027726 File Offset: 0x00025926
		public bool IsException
		{
			get
			{
				return this.isExceptionField;
			}
			set
			{
				this.isExceptionField = value;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0002772F File Offset: 0x0002592F
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x00027737 File Offset: 0x00025937
		public bool IsReminderSet
		{
			get
			{
				return this.isReminderSetField;
			}
			set
			{
				this.isReminderSetField = value;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00027740 File Offset: 0x00025940
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00027748 File Offset: 0x00025948
		public bool IsPrivate
		{
			get
			{
				return this.isPrivateField;
			}
			set
			{
				this.isPrivateField = value;
			}
		}

		// Token: 0x04000FE6 RID: 4070
		private string idField;

		// Token: 0x04000FE7 RID: 4071
		private string subjectField;

		// Token: 0x04000FE8 RID: 4072
		private string locationField;

		// Token: 0x04000FE9 RID: 4073
		private bool isMeetingField;

		// Token: 0x04000FEA RID: 4074
		private bool isRecurringField;

		// Token: 0x04000FEB RID: 4075
		private bool isExceptionField;

		// Token: 0x04000FEC RID: 4076
		private bool isReminderSetField;

		// Token: 0x04000FED RID: 4077
		private bool isPrivateField;
	}
}
