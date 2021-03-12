using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management
{
	// Token: 0x02000DE3 RID: 3555
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CopyDirectoriesException : DataSourceOperationException
	{
		// Token: 0x0600A45D RID: 42077 RVA: 0x00284179 File Offset: 0x00282379
		public CopyDirectoriesException(string fromFolder, string toFolder, string error) : base(Strings.CopyDirectoriesException(fromFolder, toFolder, error))
		{
			this.fromFolder = fromFolder;
			this.toFolder = toFolder;
			this.error = error;
		}

		// Token: 0x0600A45E RID: 42078 RVA: 0x0028419E File Offset: 0x0028239E
		public CopyDirectoriesException(string fromFolder, string toFolder, string error, Exception innerException) : base(Strings.CopyDirectoriesException(fromFolder, toFolder, error), innerException)
		{
			this.fromFolder = fromFolder;
			this.toFolder = toFolder;
			this.error = error;
		}

		// Token: 0x0600A45F RID: 42079 RVA: 0x002841C8 File Offset: 0x002823C8
		protected CopyDirectoriesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fromFolder = (string)info.GetValue("fromFolder", typeof(string));
			this.toFolder = (string)info.GetValue("toFolder", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600A460 RID: 42080 RVA: 0x0028423D File Offset: 0x0028243D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fromFolder", this.fromFolder);
			info.AddValue("toFolder", this.toFolder);
			info.AddValue("error", this.error);
		}

		// Token: 0x170035EE RID: 13806
		// (get) Token: 0x0600A461 RID: 42081 RVA: 0x0028427A File Offset: 0x0028247A
		public string FromFolder
		{
			get
			{
				return this.fromFolder;
			}
		}

		// Token: 0x170035EF RID: 13807
		// (get) Token: 0x0600A462 RID: 42082 RVA: 0x00284282 File Offset: 0x00282482
		public string ToFolder
		{
			get
			{
				return this.toFolder;
			}
		}

		// Token: 0x170035F0 RID: 13808
		// (get) Token: 0x0600A463 RID: 42083 RVA: 0x0028428A File Offset: 0x0028248A
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04005F54 RID: 24404
		private readonly string fromFolder;

		// Token: 0x04005F55 RID: 24405
		private readonly string toFolder;

		// Token: 0x04005F56 RID: 24406
		private readonly string error;
	}
}
