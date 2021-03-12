using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000499 RID: 1177
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchFileDeletionException : PagePatchApiFailedException
	{
		// Token: 0x06002CAF RID: 11439 RVA: 0x000BFD1A File Offset: 0x000BDF1A
		public PagePatchFileDeletionException(string file, string error) : base(ReplayStrings.PagePatchFileDeletionException(file, error))
		{
			this.file = file;
			this.error = error;
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000BFD3C File Offset: 0x000BDF3C
		public PagePatchFileDeletionException(string file, string error, Exception innerException) : base(ReplayStrings.PagePatchFileDeletionException(file, error), innerException)
		{
			this.file = file;
			this.error = error;
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000BFD60 File Offset: 0x000BDF60
		protected PagePatchFileDeletionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000BFDB5 File Offset: 0x000BDFB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000BFDE1 File Offset: 0x000BDFE1
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002CB4 RID: 11444 RVA: 0x000BFDE9 File Offset: 0x000BDFE9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040014F2 RID: 5362
		private readonly string file;

		// Token: 0x040014F3 RID: 5363
		private readonly string error;
	}
}
