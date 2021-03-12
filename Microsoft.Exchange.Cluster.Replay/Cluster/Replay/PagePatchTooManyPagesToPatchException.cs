using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200049A RID: 1178
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PagePatchTooManyPagesToPatchException : PagePatchApiFailedException
	{
		// Token: 0x06002CB5 RID: 11445 RVA: 0x000BFDF1 File Offset: 0x000BDFF1
		public PagePatchTooManyPagesToPatchException(int numPages, int maxSupported) : base(ReplayStrings.PagePatchTooManyPagesToPatchException(numPages, maxSupported))
		{
			this.numPages = numPages;
			this.maxSupported = maxSupported;
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x000BFE13 File Offset: 0x000BE013
		public PagePatchTooManyPagesToPatchException(int numPages, int maxSupported, Exception innerException) : base(ReplayStrings.PagePatchTooManyPagesToPatchException(numPages, maxSupported), innerException)
		{
			this.numPages = numPages;
			this.maxSupported = maxSupported;
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x000BFE38 File Offset: 0x000BE038
		protected PagePatchTooManyPagesToPatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.numPages = (int)info.GetValue("numPages", typeof(int));
			this.maxSupported = (int)info.GetValue("maxSupported", typeof(int));
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000BFE8D File Offset: 0x000BE08D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("numPages", this.numPages);
			info.AddValue("maxSupported", this.maxSupported);
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002CB9 RID: 11449 RVA: 0x000BFEB9 File Offset: 0x000BE0B9
		public int NumPages
		{
			get
			{
				return this.numPages;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002CBA RID: 11450 RVA: 0x000BFEC1 File Offset: 0x000BE0C1
		public int MaxSupported
		{
			get
			{
				return this.maxSupported;
			}
		}

		// Token: 0x040014F4 RID: 5364
		private readonly int numPages;

		// Token: 0x040014F5 RID: 5365
		private readonly int maxSupported;
	}
}
