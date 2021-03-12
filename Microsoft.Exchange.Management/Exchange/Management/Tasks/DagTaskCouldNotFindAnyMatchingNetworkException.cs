using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001083 RID: 4227
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskCouldNotFindAnyMatchingNetworkException : LocalizedException
	{
		// Token: 0x0600B17B RID: 45435 RVA: 0x002982B9 File Offset: 0x002964B9
		public DagTaskCouldNotFindAnyMatchingNetworkException(string machineName) : base(Strings.DagTaskCouldNotFindAnyMatchingNetworkException(machineName))
		{
			this.machineName = machineName;
		}

		// Token: 0x0600B17C RID: 45436 RVA: 0x002982CE File Offset: 0x002964CE
		public DagTaskCouldNotFindAnyMatchingNetworkException(string machineName, Exception innerException) : base(Strings.DagTaskCouldNotFindAnyMatchingNetworkException(machineName), innerException)
		{
			this.machineName = machineName;
		}

		// Token: 0x0600B17D RID: 45437 RVA: 0x002982E4 File Offset: 0x002964E4
		protected DagTaskCouldNotFindAnyMatchingNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
		}

		// Token: 0x0600B17E RID: 45438 RVA: 0x0029830E File Offset: 0x0029650E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
		}

		// Token: 0x1700388C RID: 14476
		// (get) Token: 0x0600B17F RID: 45439 RVA: 0x00298329 File Offset: 0x00296529
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x040061F2 RID: 25074
		private readonly string machineName;
	}
}
