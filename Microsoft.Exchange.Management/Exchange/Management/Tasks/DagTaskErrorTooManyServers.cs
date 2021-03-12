using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200108E RID: 4238
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskErrorTooManyServers : LocalizedException
	{
		// Token: 0x0600B1B9 RID: 45497 RVA: 0x00298A22 File Offset: 0x00296C22
		public DagTaskErrorTooManyServers(string dagName, int max) : base(Strings.DagTaskErrorTooManyServers(dagName, max))
		{
			this.dagName = dagName;
			this.max = max;
		}

		// Token: 0x0600B1BA RID: 45498 RVA: 0x00298A3F File Offset: 0x00296C3F
		public DagTaskErrorTooManyServers(string dagName, int max, Exception innerException) : base(Strings.DagTaskErrorTooManyServers(dagName, max), innerException)
		{
			this.dagName = dagName;
			this.max = max;
		}

		// Token: 0x0600B1BB RID: 45499 RVA: 0x00298A60 File Offset: 0x00296C60
		protected DagTaskErrorTooManyServers(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dagName = (string)info.GetValue("dagName", typeof(string));
			this.max = (int)info.GetValue("max", typeof(int));
		}

		// Token: 0x0600B1BC RID: 45500 RVA: 0x00298AB5 File Offset: 0x00296CB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dagName", this.dagName);
			info.AddValue("max", this.max);
		}

		// Token: 0x1700389E RID: 14494
		// (get) Token: 0x0600B1BD RID: 45501 RVA: 0x00298AE1 File Offset: 0x00296CE1
		public string DagName
		{
			get
			{
				return this.dagName;
			}
		}

		// Token: 0x1700389F RID: 14495
		// (get) Token: 0x0600B1BE RID: 45502 RVA: 0x00298AE9 File Offset: 0x00296CE9
		public int Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x04006204 RID: 25092
		private readonly string dagName;

		// Token: 0x04006205 RID: 25093
		private readonly int max;
	}
}
