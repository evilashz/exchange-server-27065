using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001EA RID: 490
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class DelegateUserType
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x000258BF File Offset: 0x00023ABF
		// (set) Token: 0x06001405 RID: 5125 RVA: 0x000258C7 File Offset: 0x00023AC7
		public UserIdType UserId
		{
			get
			{
				return this.userIdField;
			}
			set
			{
				this.userIdField = value;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000258D0 File Offset: 0x00023AD0
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x000258D8 File Offset: 0x00023AD8
		public DelegatePermissionsType DelegatePermissions
		{
			get
			{
				return this.delegatePermissionsField;
			}
			set
			{
				this.delegatePermissionsField = value;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x000258E1 File Offset: 0x00023AE1
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x000258E9 File Offset: 0x00023AE9
		public bool ReceiveCopiesOfMeetingMessages
		{
			get
			{
				return this.receiveCopiesOfMeetingMessagesField;
			}
			set
			{
				this.receiveCopiesOfMeetingMessagesField = value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000258F2 File Offset: 0x00023AF2
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x000258FA File Offset: 0x00023AFA
		[XmlIgnore]
		public bool ReceiveCopiesOfMeetingMessagesSpecified
		{
			get
			{
				return this.receiveCopiesOfMeetingMessagesFieldSpecified;
			}
			set
			{
				this.receiveCopiesOfMeetingMessagesFieldSpecified = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00025903 File Offset: 0x00023B03
		// (set) Token: 0x0600140D RID: 5133 RVA: 0x0002590B File Offset: 0x00023B0B
		public bool ViewPrivateItems
		{
			get
			{
				return this.viewPrivateItemsField;
			}
			set
			{
				this.viewPrivateItemsField = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00025914 File Offset: 0x00023B14
		// (set) Token: 0x0600140F RID: 5135 RVA: 0x0002591C File Offset: 0x00023B1C
		[XmlIgnore]
		public bool ViewPrivateItemsSpecified
		{
			get
			{
				return this.viewPrivateItemsFieldSpecified;
			}
			set
			{
				this.viewPrivateItemsFieldSpecified = value;
			}
		}

		// Token: 0x04000DD0 RID: 3536
		private UserIdType userIdField;

		// Token: 0x04000DD1 RID: 3537
		private DelegatePermissionsType delegatePermissionsField;

		// Token: 0x04000DD2 RID: 3538
		private bool receiveCopiesOfMeetingMessagesField;

		// Token: 0x04000DD3 RID: 3539
		private bool receiveCopiesOfMeetingMessagesFieldSpecified;

		// Token: 0x04000DD4 RID: 3540
		private bool viewPrivateItemsField;

		// Token: 0x04000DD5 RID: 3541
		private bool viewPrivateItemsFieldSpecified;
	}
}
