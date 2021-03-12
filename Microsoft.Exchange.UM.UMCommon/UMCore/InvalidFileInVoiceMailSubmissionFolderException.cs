using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F0 RID: 496
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidFileInVoiceMailSubmissionFolderException : LocalizedException
	{
		// Token: 0x06001077 RID: 4215 RVA: 0x00038D4D File Offset: 0x00036F4D
		public InvalidFileInVoiceMailSubmissionFolderException(string file, string error) : base(Strings.InvalidFileInVoiceMailSubmissionFolder(file, error))
		{
			this.file = file;
			this.error = error;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x00038D6A File Offset: 0x00036F6A
		public InvalidFileInVoiceMailSubmissionFolderException(string file, string error, Exception innerException) : base(Strings.InvalidFileInVoiceMailSubmissionFolder(file, error), innerException)
		{
			this.file = file;
			this.error = error;
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x00038D88 File Offset: 0x00036F88
		protected InvalidFileInVoiceMailSubmissionFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00038DDD File Offset: 0x00036FDD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
			info.AddValue("error", this.error);
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00038E09 File Offset: 0x00037009
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00038E11 File Offset: 0x00037011
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000875 RID: 2165
		private readonly string file;

		// Token: 0x04000876 RID: 2166
		private readonly string error;
	}
}
