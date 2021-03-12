using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000016 RID: 22
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FileCopyException : LocalizedException
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004F4B File Offset: 0x0000314B
		public FileCopyException(string srcDir, string dstDir) : base(Strings.FileCopyFailed(srcDir, dstDir))
		{
			this.srcDir = srcDir;
			this.dstDir = dstDir;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004F68 File Offset: 0x00003168
		public FileCopyException(string srcDir, string dstDir, Exception innerException) : base(Strings.FileCopyFailed(srcDir, dstDir), innerException)
		{
			this.srcDir = srcDir;
			this.dstDir = dstDir;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004F88 File Offset: 0x00003188
		protected FileCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.srcDir = (string)info.GetValue("srcDir", typeof(string));
			this.dstDir = (string)info.GetValue("dstDir", typeof(string));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004FDD File Offset: 0x000031DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("srcDir", this.srcDir);
			info.AddValue("dstDir", this.dstDir);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00005009 File Offset: 0x00003209
		public string SrcDir
		{
			get
			{
				return this.srcDir;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005011 File Offset: 0x00003211
		public string DstDir
		{
			get
			{
				return this.dstDir;
			}
		}

		// Token: 0x040000BD RID: 189
		private readonly string srcDir;

		// Token: 0x040000BE RID: 190
		private readonly string dstDir;
	}
}
