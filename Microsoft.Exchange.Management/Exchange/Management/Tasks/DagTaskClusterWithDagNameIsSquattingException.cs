using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001080 RID: 4224
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskClusterWithDagNameIsSquattingException : LocalizedException
	{
		// Token: 0x0600B16B RID: 45419 RVA: 0x002980FA File Offset: 0x002962FA
		public DagTaskClusterWithDagNameIsSquattingException(string dagName) : base(Strings.DagTaskClusterWithDagNameIsSquattingException(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B16C RID: 45420 RVA: 0x0029810F File Offset: 0x0029630F
		public DagTaskClusterWithDagNameIsSquattingException(string dagName, Exception innerException) : base(Strings.DagTaskClusterWithDagNameIsSquattingException(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B16D RID: 45421 RVA: 0x00298125 File Offset: 0x00296325
		protected DagTaskClusterWithDagNameIsSquattingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B16E RID: 45422 RVA: 0x0029814F File Offset: 0x0029634F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003888 RID: 14472
		// (get) Token: 0x0600B16F RID: 45423 RVA: 0x0029816A File Offset: 0x0029636A
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061EE RID: 25070
		private readonly string dagName;
	}
}
