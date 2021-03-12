using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001092 RID: 4242
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToEvictNodeException : LocalizedException
	{
		// Token: 0x0600B1D3 RID: 45523 RVA: 0x00298DEA File Offset: 0x00296FEA
		public FailedToEvictNodeException(string nodeName, string dagName, string error) : base(Strings.FailedToEvictNodeException(nodeName, dagName, error))
		{
			this.nodeName = nodeName;
			this.dagName = dagName;
			this.error = error;
		}

		// Token: 0x0600B1D4 RID: 45524 RVA: 0x00298E0F File Offset: 0x0029700F
		public FailedToEvictNodeException(string nodeName, string dagName, string error, Exception innerException) : base(Strings.FailedToEvictNodeException(nodeName, dagName, error), innerException)
		{
			this.nodeName = nodeName;
			this.dagName = dagName;
			this.error = error;
		}

		// Token: 0x0600B1D5 RID: 45525 RVA: 0x00298E38 File Offset: 0x00297038
		protected FailedToEvictNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B1D6 RID: 45526 RVA: 0x00298EAD File Offset: 0x002970AD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("dagName", this.dagName);
			info.AddValue("error", this.error);
		}

		// Token: 0x170038A8 RID: 14504
		// (get) Token: 0x0600B1D7 RID: 45527 RVA: 0x00298EEA File Offset: 0x002970EA
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x170038A9 RID: 14505
		// (get) Token: 0x0600B1D8 RID: 45528 RVA: 0x00298EF2 File Offset: 0x002970F2
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x170038AA RID: 14506
		// (get) Token: 0x0600B1D9 RID: 45529 RVA: 0x00298EFA File Offset: 0x002970FA
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400620E RID: 25102
		private readonly string nodeName;

		// Token: 0x0400620F RID: 25103
		private readonly string dagName;

		// Token: 0x04006210 RID: 25104
		private readonly string error;
	}
}
