using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000038 RID: 56
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DirectoryNotExistException : LocalizedException
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00008FA2 File Offset: 0x000071A2
		public DirectoryNotExistException(string dirName) : base(MigrationMonitorStrings.ErrorDirectoryNotExist(dirName))
		{
			this.dirName = dirName;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008FB7 File Offset: 0x000071B7
		public DirectoryNotExistException(string dirName, Exception innerException) : base(MigrationMonitorStrings.ErrorDirectoryNotExist(dirName), innerException)
		{
			this.dirName = dirName;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008FCD File Offset: 0x000071CD
		protected DirectoryNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dirName = (string)info.GetValue("dirName", typeof(string));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008FF7 File Offset: 0x000071F7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dirName", this.dirName);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009012 File Offset: 0x00007212
		public string DirName
		{
			get
			{
				return this.dirName;
			}
		}

		// Token: 0x0400015D RID: 349
		private readonly string dirName;
	}
}
