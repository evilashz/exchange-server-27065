using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000009 RID: 9
	public class StoreException : Exception
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00004376 File Offset: 0x00002576
		public StoreException(LID lid, ErrorCodeValue errorCode, string message, Exception innerException) : base(StoreException.FormatMessage(lid, errorCode, message), innerException)
		{
			DiagnosticContext.TraceStoreError(lid, (uint)errorCode);
			this.errorCode = errorCode;
			this.lid = lid.Value;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000043A3 File Offset: 0x000025A3
		public StoreException(LID lid, ErrorCodeValue errorCode, string message) : this(lid, errorCode, message, null)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000043AF File Offset: 0x000025AF
		public StoreException(LID lid, ErrorCodeValue errorCode) : this(lid, errorCode, null, null)
		{
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000043BB File Offset: 0x000025BB
		public ErrorCodeValue Error
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000043C3 File Offset: 0x000025C3
		public uint Lid
		{
			get
			{
				return this.lid;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000043CC File Offset: 0x000025CC
		private static string FormatMessage(LID lid, ErrorCodeValue errorCode, string message)
		{
			return string.Format("ErrorCode: {0}, LID: {1}{2}{3}", new object[]
			{
				errorCode,
				lid.Value,
				string.IsNullOrEmpty(message) ? string.Empty : " - ",
				message ?? string.Empty
			});
		}

		// Token: 0x040002C5 RID: 709
		private const string ErrorCodeSerializationLabel = "errorCode";

		// Token: 0x040002C6 RID: 710
		private const string LidSerializationLabel = "lid";

		// Token: 0x040002C7 RID: 711
		private readonly ErrorCodeValue errorCode;

		// Token: 0x040002C8 RID: 712
		private readonly uint lid;
	}
}
