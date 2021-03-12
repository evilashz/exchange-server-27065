using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200003A RID: 58
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LogFileReadException : LocalizedException
	{
		// Token: 0x060001CE RID: 462 RVA: 0x00009092 File Offset: 0x00007292
		public LogFileReadException(string fileName) : base(MigrationMonitorStrings.ErrorLogFileRead(fileName))
		{
			this.fileName = fileName;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000090A7 File Offset: 0x000072A7
		public LogFileReadException(string fileName, Exception innerException) : base(MigrationMonitorStrings.ErrorLogFileRead(fileName), innerException)
		{
			this.fileName = fileName;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000090BD File Offset: 0x000072BD
		protected LogFileReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000090E7 File Offset: 0x000072E7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00009102 File Offset: 0x00007302
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x0400015F RID: 351
		private readonly string fileName;
	}
}
