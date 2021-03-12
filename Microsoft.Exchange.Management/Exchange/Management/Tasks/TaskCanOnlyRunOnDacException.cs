using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200105D RID: 4189
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TaskCanOnlyRunOnDacException : LocalizedException
	{
		// Token: 0x0600B0A1 RID: 45217 RVA: 0x002967E0 File Offset: 0x002949E0
		public TaskCanOnlyRunOnDacException(string dag) : base(Strings.TaskCanOnlyRunOnDac(dag))
		{
			this.dag = dag;
		}

		// Token: 0x0600B0A2 RID: 45218 RVA: 0x002967F5 File Offset: 0x002949F5
		public TaskCanOnlyRunOnDacException(string dag, Exception innerException) : base(Strings.TaskCanOnlyRunOnDac(dag), innerException)
		{
			this.dag = dag;
		}

		// Token: 0x0600B0A3 RID: 45219 RVA: 0x0029680B File Offset: 0x00294A0B
		protected TaskCanOnlyRunOnDacException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dag = (string)info.GetValue("dag", typeof(string));
		}

		// Token: 0x0600B0A4 RID: 45220 RVA: 0x00296835 File Offset: 0x00294A35
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dag", this.dag);
		}

		// Token: 0x1700384A RID: 14410
		// (get) Token: 0x0600B0A5 RID: 45221 RVA: 0x00296850 File Offset: 0x00294A50
		public string Dag
		{
			get
			{
				return this.dag;
			}
		}

		// Token: 0x040061B0 RID: 25008
		private readonly string dag;
	}
}
