using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B4 RID: 948
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkRemoteException : LocalizedException
	{
		// Token: 0x060027D9 RID: 10201 RVA: 0x000B6B4A File Offset: 0x000B4D4A
		public NetworkRemoteException(string nodeName, string messageText) : base(ReplayStrings.NetworkRemoteError(nodeName, messageText))
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000B6B67 File Offset: 0x000B4D67
		public NetworkRemoteException(string nodeName, string messageText, Exception innerException) : base(ReplayStrings.NetworkRemoteError(nodeName, messageText), innerException)
		{
			this.nodeName = nodeName;
			this.messageText = messageText;
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x000B6B88 File Offset: 0x000B4D88
		protected NetworkRemoteException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.messageText = (string)info.GetValue("messageText", typeof(string));
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000B6BDD File Offset: 0x000B4DDD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("messageText", this.messageText);
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060027DD RID: 10205 RVA: 0x000B6C09 File Offset: 0x000B4E09
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060027DE RID: 10206 RVA: 0x000B6C11 File Offset: 0x000B4E11
		public string MessageText
		{
			get
			{
				return this.messageText;
			}
		}

		// Token: 0x040013B0 RID: 5040
		private readonly string nodeName;

		// Token: 0x040013B1 RID: 5041
		private readonly string messageText;
	}
}
