using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000033 RID: 51
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FileCopierReadFileException : LocalizedException
	{
		// Token: 0x06000276 RID: 630 RVA: 0x0000D32D File Offset: 0x0000B52D
		public FileCopierReadFileException(string sourceDir) : base(Strings.NoFilesToCopy(sourceDir))
		{
			this.sourceDir = sourceDir;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D342 File Offset: 0x0000B542
		public FileCopierReadFileException(string sourceDir, Exception innerException) : base(Strings.NoFilesToCopy(sourceDir), innerException)
		{
			this.sourceDir = sourceDir;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D358 File Offset: 0x0000B558
		protected FileCopierReadFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.sourceDir = (string)info.GetValue("sourceDir", typeof(string));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D382 File Offset: 0x0000B582
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("sourceDir", this.sourceDir);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000D39D File Offset: 0x0000B59D
		public string SourceDir
		{
			get
			{
				return this.sourceDir;
			}
		}

		// Token: 0x04000188 RID: 392
		private readonly string sourceDir;
	}
}
