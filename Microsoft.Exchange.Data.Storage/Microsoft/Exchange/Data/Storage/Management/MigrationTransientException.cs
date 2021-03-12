using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public class MigrationTransientException : TransientException
	{
		// Token: 0x060013AC RID: 5036 RVA: 0x000699C4 File Offset: 0x00067BC4
		public MigrationTransientException(LocalizedString localizedErrorMessage) : base(localizedErrorMessage)
		{
			this.InternalError = string.Empty;
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000699D8 File Offset: 0x00067BD8
		public MigrationTransientException(LocalizedString localizedErrorMessage, Exception innerException) : base(localizedErrorMessage, innerException)
		{
			this.InternalError = string.Empty;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000699ED File Offset: 0x00067BED
		public MigrationTransientException(LocalizedString localizedErrorMessage, string internalError, Exception ex) : base(localizedErrorMessage, ex)
		{
			this.InternalError = internalError;
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x000699FE File Offset: 0x00067BFE
		public MigrationTransientException(LocalizedString localizedErrorMessage, string internalError) : base(localizedErrorMessage)
		{
			this.InternalError = internalError;
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x00069A0E File Offset: 0x00067C0E
		protected MigrationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.InternalError = info.GetString("InternalError");
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x00069A29 File Offset: 0x00067C29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InternalError", this.InternalError);
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00069A44 File Offset: 0x00067C44
		// (set) Token: 0x060013B3 RID: 5043 RVA: 0x00069A5B File Offset: 0x00067C5B
		public string InternalError
		{
			get
			{
				return (string)this.Data["InternalError"];
			}
			internal set
			{
				this.Data["InternalError"] = value;
			}
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00069A6E File Offset: 0x00067C6E
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.InternalError))
			{
				return "internal error:" + this.InternalError + base.ToString();
			}
			return base.ToString();
		}
	}
}
