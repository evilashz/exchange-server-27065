using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000555 RID: 1365
	internal class MessageInfo
	{
		// Token: 0x060030A6 RID: 12454 RVA: 0x000C4A80 File Offset: 0x000C2C80
		public MessageInfo(string checkTitle, string instanceIdentity, string message, bool isException) : this(checkTitle, instanceIdentity, message, isException, null)
		{
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000C4AA1 File Offset: 0x000C2CA1
		public MessageInfo(string checkTitle, string instanceIdentity, string message, bool isException, uint? dbFailureEventId) : this(checkTitle, instanceIdentity, message, isException, dbFailureEventId, true)
		{
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C4AB1 File Offset: 0x000C2CB1
		public MessageInfo(string checkTitle, string instanceIdentity, string message, bool isException, uint? dbFailureEventId, bool isTransitioningState)
		{
			this.m_checkTitle = checkTitle;
			this.m_instanceIdentity = instanceIdentity;
			this.m_message = message;
			this.m_isException = isException;
			this.m_dbFailureEventId = dbFailureEventId;
			this.m_isTransitioningState = isTransitioningState;
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000C4AE6 File Offset: 0x000C2CE6
		public string InstanceIdentity
		{
			get
			{
				return this.m_instanceIdentity;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000C4AEE File Offset: 0x000C2CEE
		public string Message
		{
			get
			{
				return this.m_message;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000C4AF6 File Offset: 0x000C2CF6
		public bool IsException
		{
			get
			{
				return this.m_isException;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000C4AFE File Offset: 0x000C2CFE
		public uint? DbFailureEventId
		{
			get
			{
				return this.m_dbFailureEventId;
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000C4B06 File Offset: 0x000C2D06
		public string CheckTitle
		{
			get
			{
				return this.m_checkTitle;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000C4B0E File Offset: 0x000C2D0E
		public bool IsTransitioningState
		{
			get
			{
				return this.m_isTransitioningState;
			}
		}

		// Token: 0x04002288 RID: 8840
		private readonly string m_message;

		// Token: 0x04002289 RID: 8841
		private readonly bool m_isException;

		// Token: 0x0400228A RID: 8842
		private readonly string m_instanceIdentity;

		// Token: 0x0400228B RID: 8843
		private readonly uint? m_dbFailureEventId;

		// Token: 0x0400228C RID: 8844
		private readonly string m_checkTitle;

		// Token: 0x0400228D RID: 8845
		private readonly bool m_isTransitioningState;
	}
}
