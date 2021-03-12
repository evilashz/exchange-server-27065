using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000F3 RID: 243
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ErrorGettingFilesException : MigrationTransientException
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x000101C1 File Offset: 0x0000E3C1
		public ErrorGettingFilesException(string directory) : base(UpgradeHandlerStrings.ErrorGettingFiles(directory))
		{
			this.directory = directory;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000101D6 File Offset: 0x0000E3D6
		public ErrorGettingFilesException(string directory, Exception innerException) : base(UpgradeHandlerStrings.ErrorGettingFiles(directory), innerException)
		{
			this.directory = directory;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000101EC File Offset: 0x0000E3EC
		protected ErrorGettingFilesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.directory = (string)info.GetValue("directory", typeof(string));
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00010216 File Offset: 0x0000E416
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("directory", this.directory);
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x00010231 File Offset: 0x0000E431
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x040003A4 RID: 932
		private readonly string directory;
	}
}
