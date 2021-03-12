using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000484 RID: 1156
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmBcsSelectionException : AmBcsException
	{
		// Token: 0x06002C3C RID: 11324 RVA: 0x000BEFA3 File Offset: 0x000BD1A3
		public AmBcsSelectionException(string bcsMessage) : base(ReplayStrings.AmBcsSelectionException(bcsMessage))
		{
			this.bcsMessage = bcsMessage;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000BEFBD File Offset: 0x000BD1BD
		public AmBcsSelectionException(string bcsMessage, Exception innerException) : base(ReplayStrings.AmBcsSelectionException(bcsMessage), innerException)
		{
			this.bcsMessage = bcsMessage;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000BEFD8 File Offset: 0x000BD1D8
		protected AmBcsSelectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.bcsMessage = (string)info.GetValue("bcsMessage", typeof(string));
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000BF002 File Offset: 0x000BD202
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("bcsMessage", this.bcsMessage);
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000BF01D File Offset: 0x000BD21D
		public string BcsMessage
		{
			get
			{
				return this.bcsMessage;
			}
		}

		// Token: 0x040014D3 RID: 5331
		private readonly string bcsMessage;
	}
}
