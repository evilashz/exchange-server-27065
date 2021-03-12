using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x0200010A RID: 266
	[Serializable]
	public abstract class AnchorLocalizedExceptionBase : LocalizedException
	{
		// Token: 0x060013BF RID: 5055 RVA: 0x00069B8A File Offset: 0x00067D8A
		protected AnchorLocalizedExceptionBase(LocalizedString localizedErrorMessage, string internalError, Exception ex) : base(localizedErrorMessage, ex)
		{
			this.InternalError = internalError;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00069B9B File Offset: 0x00067D9B
		protected AnchorLocalizedExceptionBase(LocalizedString localizedErrorMessage, string internalError) : base(localizedErrorMessage)
		{
			this.InternalError = internalError;
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00069BAB File Offset: 0x00067DAB
		protected AnchorLocalizedExceptionBase(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.InternalError = info.GetString("InternalError");
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00069BC6 File Offset: 0x00067DC6
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x00069BDD File Offset: 0x00067DDD
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

		// Token: 0x060013C4 RID: 5060 RVA: 0x00069BF0 File Offset: 0x00067DF0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InternalError", this.InternalError);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00069C0B File Offset: 0x00067E0B
		public override string ToString()
		{
			return "internal error:" + this.InternalError + base.ToString();
		}

		// Token: 0x04000999 RID: 2457
		private const string InternalErrorKey = "InternalError";
	}
}
