using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200108D RID: 4237
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskErrorServerWrongVersion : LocalizedException
	{
		// Token: 0x0600B1B4 RID: 45492 RVA: 0x002989AA File Offset: 0x00296BAA
		public DagTaskErrorServerWrongVersion(string serverName) : base(Strings.DagTaskErrorServerWrongVersion(serverName))
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B1B5 RID: 45493 RVA: 0x002989BF File Offset: 0x00296BBF
		public DagTaskErrorServerWrongVersion(string serverName, Exception innerException) : base(Strings.DagTaskErrorServerWrongVersion(serverName), innerException)
		{
			this.serverName = serverName;
		}

		// Token: 0x0600B1B6 RID: 45494 RVA: 0x002989D5 File Offset: 0x00296BD5
		protected DagTaskErrorServerWrongVersion(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x0600B1B7 RID: 45495 RVA: 0x002989FF File Offset: 0x00296BFF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x1700389D RID: 14493
		// (get) Token: 0x0600B1B8 RID: 45496 RVA: 0x00298A1A File Offset: 0x00296C1A
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04006203 RID: 25091
		private readonly string serverName;
	}
}
