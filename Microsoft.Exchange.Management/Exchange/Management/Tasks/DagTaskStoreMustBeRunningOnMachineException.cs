using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001082 RID: 4226
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagTaskStoreMustBeRunningOnMachineException : LocalizedException
	{
		// Token: 0x0600B176 RID: 45430 RVA: 0x00298241 File Offset: 0x00296441
		public DagTaskStoreMustBeRunningOnMachineException(string machineName) : base(Strings.DagTaskStoreMustBeRunningOnMachineException(machineName))
		{
			this.machineName = machineName;
		}

		// Token: 0x0600B177 RID: 45431 RVA: 0x00298256 File Offset: 0x00296456
		public DagTaskStoreMustBeRunningOnMachineException(string machineName, Exception innerException) : base(Strings.DagTaskStoreMustBeRunningOnMachineException(machineName), innerException)
		{
			this.machineName = machineName;
		}

		// Token: 0x0600B178 RID: 45432 RVA: 0x0029826C File Offset: 0x0029646C
		protected DagTaskStoreMustBeRunningOnMachineException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.machineName = (string)info.GetValue("machineName", typeof(string));
		}

		// Token: 0x0600B179 RID: 45433 RVA: 0x00298296 File Offset: 0x00296496
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("machineName", this.machineName);
		}

		// Token: 0x1700388B RID: 14475
		// (get) Token: 0x0600B17A RID: 45434 RVA: 0x002982B1 File Offset: 0x002964B1
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		// Token: 0x040061F1 RID: 25073
		private readonly string machineName;
	}
}
