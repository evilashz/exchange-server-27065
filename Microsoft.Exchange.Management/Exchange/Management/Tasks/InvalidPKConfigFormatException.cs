using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001008 RID: 4104
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidPKConfigFormatException : LocalizedException
	{
		// Token: 0x0600AEE9 RID: 44777 RVA: 0x00293A9C File Offset: 0x00291C9C
		public InvalidPKConfigFormatException(string file) : base(Strings.InvalidPKConfigFormat(file))
		{
			this.file = file;
		}

		// Token: 0x0600AEEA RID: 44778 RVA: 0x00293AB1 File Offset: 0x00291CB1
		public InvalidPKConfigFormatException(string file, Exception innerException) : base(Strings.InvalidPKConfigFormat(file), innerException)
		{
			this.file = file;
		}

		// Token: 0x0600AEEB RID: 44779 RVA: 0x00293AC7 File Offset: 0x00291CC7
		protected InvalidPKConfigFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.file = (string)info.GetValue("file", typeof(string));
		}

		// Token: 0x0600AEEC RID: 44780 RVA: 0x00293AF1 File Offset: 0x00291CF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("file", this.file);
		}

		// Token: 0x170037E6 RID: 14310
		// (get) Token: 0x0600AEED RID: 44781 RVA: 0x00293B0C File Offset: 0x00291D0C
		public string File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x0400614C RID: 24908
		private readonly string file;
	}
}
