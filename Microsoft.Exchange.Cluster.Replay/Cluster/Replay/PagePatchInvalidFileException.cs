using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200049E RID: 1182
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchInvalidFileException : PagePatchApiFailedException
	{
		// Token: 0x06002CCC RID: 11468 RVA: 0x000C00FB File Offset: 0x000BE2FB
		public PagePatchInvalidFileException(string patchFile) : base(ReplayStrings.PagePatchInvalidFileException(patchFile))
		{
			this.patchFile = patchFile;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x000C0115 File Offset: 0x000BE315
		public PagePatchInvalidFileException(string patchFile, Exception innerException) : base(ReplayStrings.PagePatchInvalidFileException(patchFile), innerException)
		{
			this.patchFile = patchFile;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x000C0130 File Offset: 0x000BE330
		protected PagePatchInvalidFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.patchFile = (string)info.GetValue("patchFile", typeof(string));
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x000C015A File Offset: 0x000BE35A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("patchFile", this.patchFile);
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002CD0 RID: 11472 RVA: 0x000C0175 File Offset: 0x000BE375
		public string PatchFile
		{
			get
			{
				return this.patchFile;
			}
		}

		// Token: 0x040014FB RID: 5371
		private readonly string patchFile;
	}
}
