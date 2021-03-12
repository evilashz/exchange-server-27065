using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003A1 RID: 929
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidRcrConfigOnNonMailboxException : TransientException
	{
		// Token: 0x06002772 RID: 10098 RVA: 0x000B5F3A File Offset: 0x000B413A
		public InvalidRcrConfigOnNonMailboxException(string nodeName) : base(ReplayStrings.InvalidRcrConfigOnNonMailboxException(nodeName))
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000B5F4F File Offset: 0x000B414F
		public InvalidRcrConfigOnNonMailboxException(string nodeName, Exception innerException) : base(ReplayStrings.InvalidRcrConfigOnNonMailboxException(nodeName), innerException)
		{
			this.nodeName = nodeName;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x000B5F65 File Offset: 0x000B4165
		protected InvalidRcrConfigOnNonMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000B5F8F File Offset: 0x000B418F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002776 RID: 10102 RVA: 0x000B5FAA File Offset: 0x000B41AA
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x04001395 RID: 5013
		private readonly string nodeName;
	}
}
