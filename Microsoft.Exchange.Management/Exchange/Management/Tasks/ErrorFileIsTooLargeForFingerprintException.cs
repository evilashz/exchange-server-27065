using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FF6 RID: 4086
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorFileIsTooLargeForFingerprintException : LocalizedException
	{
		// Token: 0x0600AE90 RID: 44688 RVA: 0x00293223 File Offset: 0x00291423
		public ErrorFileIsTooLargeForFingerprintException(int fileSize, int max) : base(Strings.ErrorFileIsTooLargeForFingerprint(fileSize, max))
		{
			this.fileSize = fileSize;
			this.max = max;
		}

		// Token: 0x0600AE91 RID: 44689 RVA: 0x00293240 File Offset: 0x00291440
		public ErrorFileIsTooLargeForFingerprintException(int fileSize, int max, Exception innerException) : base(Strings.ErrorFileIsTooLargeForFingerprint(fileSize, max), innerException)
		{
			this.fileSize = fileSize;
			this.max = max;
		}

		// Token: 0x0600AE92 RID: 44690 RVA: 0x00293260 File Offset: 0x00291460
		protected ErrorFileIsTooLargeForFingerprintException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileSize = (int)info.GetValue("fileSize", typeof(int));
			this.max = (int)info.GetValue("max", typeof(int));
		}

		// Token: 0x0600AE93 RID: 44691 RVA: 0x002932B5 File Offset: 0x002914B5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileSize", this.fileSize);
			info.AddValue("max", this.max);
		}

		// Token: 0x170037D5 RID: 14293
		// (get) Token: 0x0600AE94 RID: 44692 RVA: 0x002932E1 File Offset: 0x002914E1
		public int FileSize
		{
			get
			{
				return this.fileSize;
			}
		}

		// Token: 0x170037D6 RID: 14294
		// (get) Token: 0x0600AE95 RID: 44693 RVA: 0x002932E9 File Offset: 0x002914E9
		public int Max
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x0400613B RID: 24891
		private readonly int fileSize;

		// Token: 0x0400613C RID: 24892
		private readonly int max;
	}
}
