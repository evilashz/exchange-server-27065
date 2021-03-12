using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LogFileLoadException : LocalizedException
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000901A File Offset: 0x0000721A
		public LogFileLoadException(string dirName) : base(MigrationMonitorStrings.ErrorLogFileLoad(dirName))
		{
			this.dirName = dirName;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000902F File Offset: 0x0000722F
		public LogFileLoadException(string dirName, Exception innerException) : base(MigrationMonitorStrings.ErrorLogFileLoad(dirName), innerException)
		{
			this.dirName = dirName;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009045 File Offset: 0x00007245
		protected LogFileLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dirName = (string)info.GetValue("dirName", typeof(string));
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000906F File Offset: 0x0000726F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dirName", this.dirName);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000908A File Offset: 0x0000728A
		public string DirName
		{
			get
			{
				return this.dirName;
			}
		}

		// Token: 0x0400015E RID: 350
		private readonly string dirName;
	}
}
