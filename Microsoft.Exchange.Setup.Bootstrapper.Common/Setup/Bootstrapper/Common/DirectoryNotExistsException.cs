using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DirectoryNotExistsException : LocalizedException
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00005091 File Offset: 0x00003291
		public DirectoryNotExistsException(string dirName) : base(Strings.DirectoryNotFound(dirName))
		{
			this.dirName = dirName;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000050A6 File Offset: 0x000032A6
		public DirectoryNotExistsException(string dirName, Exception innerException) : base(Strings.DirectoryNotFound(dirName), innerException)
		{
			this.dirName = dirName;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000050BC File Offset: 0x000032BC
		protected DirectoryNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dirName = (string)info.GetValue("dirName", typeof(string));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000050E6 File Offset: 0x000032E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dirName", this.dirName);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005101 File Offset: 0x00003301
		public string DirName
		{
			get
			{
				return this.dirName;
			}
		}

		// Token: 0x040000C0 RID: 192
		private readonly string dirName;
	}
}
