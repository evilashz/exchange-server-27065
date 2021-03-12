using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E36 RID: 3638
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OutputDirectoryNotExist : LocalizedException
	{
		// Token: 0x0600A618 RID: 42520 RVA: 0x00287231 File Offset: 0x00285431
		public OutputDirectoryNotExist(string directory) : base(Strings.OutputDirectoryNotExist(directory))
		{
			this.directory = directory;
		}

		// Token: 0x0600A619 RID: 42521 RVA: 0x00287246 File Offset: 0x00285446
		public OutputDirectoryNotExist(string directory, Exception innerException) : base(Strings.OutputDirectoryNotExist(directory), innerException)
		{
			this.directory = directory;
		}

		// Token: 0x0600A61A RID: 42522 RVA: 0x0028725C File Offset: 0x0028545C
		protected OutputDirectoryNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.directory = (string)info.GetValue("directory", typeof(string));
		}

		// Token: 0x0600A61B RID: 42523 RVA: 0x00287286 File Offset: 0x00285486
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("directory", this.directory);
		}

		// Token: 0x1700365D RID: 13917
		// (get) Token: 0x0600A61C RID: 42524 RVA: 0x002872A1 File Offset: 0x002854A1
		public string Directory
		{
			get
			{
				return this.directory;
			}
		}

		// Token: 0x04005FC3 RID: 24515
		private readonly string directory;
	}
}
