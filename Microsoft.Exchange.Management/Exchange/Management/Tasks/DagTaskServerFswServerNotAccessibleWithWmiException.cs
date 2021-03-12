using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200107C RID: 4220
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskServerFswServerNotAccessibleWithWmiException : LocalizedException
	{
		// Token: 0x0600B152 RID: 45394 RVA: 0x00297D85 File Offset: 0x00295F85
		public DagTaskServerFswServerNotAccessibleWithWmiException(string fswServer) : base(Strings.DagTaskServerFswServerNotAccessibleWithWmi(fswServer))
		{
			this.fswServer = fswServer;
		}

		// Token: 0x0600B153 RID: 45395 RVA: 0x00297D9A File Offset: 0x00295F9A
		public DagTaskServerFswServerNotAccessibleWithWmiException(string fswServer, Exception innerException) : base(Strings.DagTaskServerFswServerNotAccessibleWithWmi(fswServer), innerException)
		{
			this.fswServer = fswServer;
		}

		// Token: 0x0600B154 RID: 45396 RVA: 0x00297DB0 File Offset: 0x00295FB0
		protected DagTaskServerFswServerNotAccessibleWithWmiException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fswServer = (string)info.GetValue("fswServer", typeof(string));
		}

		// Token: 0x0600B155 RID: 45397 RVA: 0x00297DDA File Offset: 0x00295FDA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fswServer", this.fswServer);
		}

		// Token: 0x1700387F RID: 14463
		// (get) Token: 0x0600B156 RID: 45398 RVA: 0x00297DF5 File Offset: 0x00295FF5
		public string FswServer
		{
			get
			{
				return this.fswServer;
			}
		}

		// Token: 0x040061E5 RID: 25061
		private readonly string fswServer;
	}
}
