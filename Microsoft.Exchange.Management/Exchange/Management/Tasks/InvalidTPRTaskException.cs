using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200103C RID: 4156
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidTPRTaskException : LocalizedException
	{
		// Token: 0x0600AFE5 RID: 45029 RVA: 0x002950F1 File Offset: 0x002932F1
		public InvalidTPRTaskException(string taskName) : base(Strings.InvalidTPRTask(taskName))
		{
			this.taskName = taskName;
		}

		// Token: 0x0600AFE6 RID: 45030 RVA: 0x00295106 File Offset: 0x00293306
		public InvalidTPRTaskException(string taskName, Exception innerException) : base(Strings.InvalidTPRTask(taskName), innerException)
		{
			this.taskName = taskName;
		}

		// Token: 0x0600AFE7 RID: 45031 RVA: 0x0029511C File Offset: 0x0029331C
		protected InvalidTPRTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.taskName = (string)info.GetValue("taskName", typeof(string));
		}

		// Token: 0x0600AFE8 RID: 45032 RVA: 0x00295146 File Offset: 0x00293346
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("taskName", this.taskName);
		}

		// Token: 0x17003812 RID: 14354
		// (get) Token: 0x0600AFE9 RID: 45033 RVA: 0x00295161 File Offset: 0x00293361
		public string TaskName
		{
			get
			{
				return this.taskName;
			}
		}

		// Token: 0x04006178 RID: 24952
		private readonly string taskName;
	}
}
