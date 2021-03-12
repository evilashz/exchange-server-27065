using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200103F RID: 4159
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskAddClusterNodeUnexpectedlyFailedException : LocalizedException
	{
		// Token: 0x0600AFF5 RID: 45045 RVA: 0x002952AD File Offset: 0x002934AD
		public DagTaskAddClusterNodeUnexpectedlyFailedException(string nodeName, string dagName) : base(Strings.DagTaskAddClusterNodeUnexpectedlyFailedException(nodeName, dagName))
		{
			this.nodeName = nodeName;
			this.dagName = dagName;
		}

		// Token: 0x0600AFF6 RID: 45046 RVA: 0x002952CA File Offset: 0x002934CA
		public DagTaskAddClusterNodeUnexpectedlyFailedException(string nodeName, string dagName, Exception innerException) : base(Strings.DagTaskAddClusterNodeUnexpectedlyFailedException(nodeName, dagName), innerException)
		{
			this.nodeName = nodeName;
			this.dagName = dagName;
		}

		// Token: 0x0600AFF7 RID: 45047 RVA: 0x002952E8 File Offset: 0x002934E8
		protected DagTaskAddClusterNodeUnexpectedlyFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.nodeName = (string)info.GetValue("nodeName", typeof(string));
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600AFF8 RID: 45048 RVA: 0x0029533D File Offset: 0x0029353D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("nodeName", this.nodeName);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003816 RID: 14358
		// (get) Token: 0x0600AFF9 RID: 45049 RVA: 0x00295369 File Offset: 0x00293569
		public string NodeName
		{
			get
			{
				return this.nodeName;
			}
		}

		// Token: 0x17003817 RID: 14359
		// (get) Token: 0x0600AFFA RID: 45050 RVA: 0x00295371 File Offset: 0x00293571
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x0400617C RID: 24956
		private readonly string nodeName;

		// Token: 0x0400617D RID: 24957
		private readonly string dagName;
	}
}
