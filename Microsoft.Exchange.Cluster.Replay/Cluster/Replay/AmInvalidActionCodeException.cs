using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200047A RID: 1146
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmInvalidActionCodeException : AmCommonException
	{
		// Token: 0x06002BFD RID: 11261 RVA: 0x000BE65D File Offset: 0x000BC85D
		public AmInvalidActionCodeException(int actionCode, string member, string value) : base(ReplayStrings.AmInvalidActionCodeException(actionCode, member, value))
		{
			this.actionCode = actionCode;
			this.member = member;
			this.value = value;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000BE687 File Offset: 0x000BC887
		public AmInvalidActionCodeException(int actionCode, string member, string value, Exception innerException) : base(ReplayStrings.AmInvalidActionCodeException(actionCode, member, value), innerException)
		{
			this.actionCode = actionCode;
			this.member = member;
			this.value = value;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000BE6B4 File Offset: 0x000BC8B4
		protected AmInvalidActionCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.actionCode = (int)info.GetValue("actionCode", typeof(int));
			this.member = (string)info.GetValue("member", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000BE729 File Offset: 0x000BC929
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("actionCode", this.actionCode);
			info.AddValue("member", this.member);
			info.AddValue("value", this.value);
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000BE766 File Offset: 0x000BC966
		public int ActionCode
		{
			get
			{
				return this.actionCode;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000BE76E File Offset: 0x000BC96E
		public string Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000BE776 File Offset: 0x000BC976
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040014BC RID: 5308
		private readonly int actionCode;

		// Token: 0x040014BD RID: 5309
		private readonly string member;

		// Token: 0x040014BE RID: 5310
		private readonly string value;
	}
}
