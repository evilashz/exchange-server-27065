using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000F2 RID: 242
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorReadingFileException : MigrationTransientException
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00010149 File Offset: 0x0000E349
		public ErrorReadingFileException(string file) : base(UpgradeHandlerStrings.ErrorReadingFile(file))
		{
			this.file = file;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001015E File Offset: 0x0000E35E
		public ErrorReadingFileException(string file, Exception innerException) : base(UpgradeHandlerStrings.ErrorReadingFile(file), innerException)
		{
			this.file = file;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x00010174 File Offset: 0x0000E374
		protected ErrorReadingFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001019E File Offset: 0x0000E39E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000101B9 File Offset: 0x0000E3B9
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x040003A3 RID: 931
		private readonly string file;
	}
}
