using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001088 RID: 4232
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskNoServersAreStartedException : LocalizedException
	{
		// Token: 0x0600B199 RID: 45465 RVA: 0x002986B1 File Offset: 0x002968B1
		public DagTaskNoServersAreStartedException(string dagName) : base(Strings.DagTaskNoServersAreStartedException(dagName))
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B19A RID: 45466 RVA: 0x002986C6 File Offset: 0x002968C6
		public DagTaskNoServersAreStartedException(string dagName, Exception innerException) : base(Strings.DagTaskNoServersAreStartedException(dagName), innerException)
		{
			this.dagName = dagName;
		}

		// Token: 0x0600B19B RID: 45467 RVA: 0x002986DC File Offset: 0x002968DC
		protected DagTaskNoServersAreStartedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
		}

		// Token: 0x0600B19C RID: 45468 RVA: 0x00298706 File Offset: 0x00296906
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
		}

		// Token: 0x17003896 RID: 14486
		// (get) Token: 0x0600B19D RID: 45469 RVA: 0x00298721 File Offset: 0x00296921
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x040061FC RID: 25084
		private readonly string dagName;
	}
}
