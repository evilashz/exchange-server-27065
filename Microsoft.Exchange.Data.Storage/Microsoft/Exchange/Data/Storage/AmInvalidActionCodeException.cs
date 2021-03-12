using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020000E8 RID: 232
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmInvalidActionCodeException : AmServerException
	{
		// Token: 0x06001313 RID: 4883 RVA: 0x00068B2F File Offset: 0x00066D2F
		public AmInvalidActionCodeException(int actionCode, string member, string value) : base(ServerStrings.AmInvalidActionCodeException(actionCode, member, value))
		{
			this.actionCode = actionCode;
			this.member = member;
			this.value = value;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00068B59 File Offset: 0x00066D59
		public AmInvalidActionCodeException(int actionCode, string member, string value, Exception innerException) : base(ServerStrings.AmInvalidActionCodeException(actionCode, member, value), innerException)
		{
			this.actionCode = actionCode;
			this.member = member;
			this.value = value;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00068B88 File Offset: 0x00066D88
		protected AmInvalidActionCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionCode = (int)info.GetValue("actionCode", typeof(int));
			this.member = (string)info.GetValue("member", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00068BFD File Offset: 0x00066DFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionCode", this.actionCode);
			info.AddValue("member", this.member);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x00068C3A File Offset: 0x00066E3A
		public int ActionCode
		{
			get
			{
				return this.actionCode;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x00068C42 File Offset: 0x00066E42
		public string Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001319 RID: 4889 RVA: 0x00068C4A File Offset: 0x00066E4A
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400097A RID: 2426
		private readonly int actionCode;

		// Token: 0x0400097B RID: 2427
		private readonly string member;

		// Token: 0x0400097C RID: 2428
		private readonly string value;
	}
}
