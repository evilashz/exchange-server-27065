using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001093 RID: 4243
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FailedToStartNodeException : LocalizedException
	{
		// Token: 0x0600B1DA RID: 45530 RVA: 0x00298F02 File Offset: 0x00297102
		public FailedToStartNodeException(string nodeNames, string dagName) : base(Strings.FailedToStartNodeException(nodeNames, dagName))
		{
			this.nodeNames = nodeNames;
			this.dagName = dagName;
		}

		// Token: 0x0600B1DB RID: 45531 RVA: 0x00298F1F File Offset: 0x0029711F
		public FailedToStartNodeException(string nodeNames, string dagName, Exception innerException) : base(Strings.FailedToStartNodeException(nodeNames, dagName), innerException)
		{
			this.nodeNames = nodeNames;
			this.dagName = dagName;
		}

		// Token: 0x0600B1DC RID: 45532 RVA: 0x00298F40 File Offset: 0x00297140
		protected FailedToStartNodeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeNames = (string)info.GetValue("nodeNames", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B1DD RID: 45533 RVA: 0x00298F95 File Offset: 0x00297195
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeNames", this.nodeNames);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x170038AB RID: 14507
		// (get) Token: 0x0600B1DE RID: 45534 RVA: 0x00298FC1 File Offset: 0x002971C1
		public string NodeNames
		{
			get
			{
				return this.nodeNames;
			}
		}

		// Token: 0x170038AC RID: 14508
		// (get) Token: 0x0600B1DF RID: 45535 RVA: 0x00298FC9 File Offset: 0x002971C9
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x04006211 RID: 25105
		private readonly string nodeNames;

		// Token: 0x04006212 RID: 25106
		private readonly string dagName;
	}
}
