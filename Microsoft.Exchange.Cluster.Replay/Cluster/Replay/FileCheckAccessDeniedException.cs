using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C7 RID: 967
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckAccessDeniedException : FileCheckException
	{
		// Token: 0x06002846 RID: 10310 RVA: 0x000B7930 File Offset: 0x000B5B30
		public FileCheckAccessDeniedException(string file) : base(ReplayStrings.FileCheckAccessDenied(file))
		{
			this.file = file;
		}

		// Token: 0x06002847 RID: 10311 RVA: 0x000B794A File Offset: 0x000B5B4A
		public FileCheckAccessDeniedException(string file, Exception innerException) : base(ReplayStrings.FileCheckAccessDenied(file), innerException)
		{
			this.file = file;
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000B7965 File Offset: 0x000B5B65
		protected FileCheckAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000B798F File Offset: 0x000B5B8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x000B79AA File Offset: 0x000B5BAA
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x040013D1 RID: 5073
		private readonly string file;
	}
}
