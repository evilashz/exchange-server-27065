using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A24 RID: 2596
	[Serializable]
	public class MigrationDataCorruptionException : LocalizedException
	{
		// Token: 0x06005F52 RID: 24402 RVA: 0x00192FF7 File Offset: 0x001911F7
		public MigrationDataCorruptionException(string internalDetails) : this(internalDetails, null)
		{
		}

		// Token: 0x06005F53 RID: 24403 RVA: 0x00193001 File Offset: 0x00191201
		public MigrationDataCorruptionException(string internalDetails, Exception innerException)
		{
			this.createdStack = new StackTrace();
			base..ctor(Strings.MigrationDataCorruptionError, innerException);
			this.InternalError = internalDetails;
		}

		// Token: 0x06005F54 RID: 24404 RVA: 0x00193021 File Offset: 0x00191221
		protected MigrationDataCorruptionException(SerializationInfo info, StreamingContext context)
		{
			this.createdStack = new StackTrace();
			base..ctor(info, context);
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x06005F55 RID: 24405 RVA: 0x00193036 File Offset: 0x00191236
		// (set) Token: 0x06005F56 RID: 24406 RVA: 0x0019304D File Offset: 0x0019124D
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

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x06005F57 RID: 24407 RVA: 0x00193060 File Offset: 0x00191260
		public override string StackTrace
		{
			get
			{
				if (!string.IsNullOrEmpty(base.StackTrace))
				{
					return base.StackTrace;
				}
				return this.createdStack.ToString();
			}
		}

		// Token: 0x06005F58 RID: 24408 RVA: 0x00193081 File Offset: 0x00191281
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("InternalError", this.InternalError);
		}

		// Token: 0x040035F9 RID: 13817
		private readonly StackTrace createdStack;
	}
}
