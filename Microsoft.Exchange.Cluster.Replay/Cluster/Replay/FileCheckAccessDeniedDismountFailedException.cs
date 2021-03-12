using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C8 RID: 968
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FileCheckAccessDeniedDismountFailedException : FileCheckException
	{
		// Token: 0x0600284B RID: 10315 RVA: 0x000B79B2 File Offset: 0x000B5BB2
		public FileCheckAccessDeniedDismountFailedException(string file, string dismountError) : base(ReplayStrings.FileCheckAccessDeniedDismountFailedException(file, dismountError))
		{
			this.file = file;
			this.dismountError = dismountError;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000B79D4 File Offset: 0x000B5BD4
		public FileCheckAccessDeniedDismountFailedException(string file, string dismountError, Exception innerException) : base(ReplayStrings.FileCheckAccessDeniedDismountFailedException(file, dismountError), innerException)
		{
			this.file = file;
			this.dismountError = dismountError;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000B79F8 File Offset: 0x000B5BF8
		protected FileCheckAccessDeniedDismountFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
			this.dismountError = (string)info.GetValue("dismountError", typeof(string));
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000B7A4D File Offset: 0x000B5C4D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
			info.AddValue("dismountError", this.dismountError);
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x000B7A79 File Offset: 0x000B5C79
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x000B7A81 File Offset: 0x000B5C81
		public string DismountError
		{
			get
			{
				return this.dismountError;
			}
		}

		// Token: 0x040013D2 RID: 5074
		private readonly string file;

		// Token: 0x040013D3 RID: 5075
		private readonly string dismountError;
	}
}
