using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x020000C8 RID: 200
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SerializedAccessTokenParserException : LocalizedException
	{
		// Token: 0x060004DF RID: 1247 RVA: 0x00012D45 File Offset: 0x00010F45
		public SerializedAccessTokenParserException(int lineNumber, int position, LocalizedString reason) : base(AuthorizationStrings.SerializedAccessTokenParserException(lineNumber, position, reason))
		{
			this.lineNumber = lineNumber;
			this.position = position;
			this.reason = reason;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012D6A File Offset: 0x00010F6A
		public SerializedAccessTokenParserException(int lineNumber, int position, LocalizedString reason, Exception innerException) : base(AuthorizationStrings.SerializedAccessTokenParserException(lineNumber, position, reason), innerException)
		{
			this.lineNumber = lineNumber;
			this.position = position;
			this.reason = reason;
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012D94 File Offset: 0x00010F94
		protected SerializedAccessTokenParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.position = (int)info.GetValue("position", typeof(int));
			this.reason = (LocalizedString)info.GetValue("reason", typeof(LocalizedString));
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00012E0C File Offset: 0x0001100C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("position", this.position);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00012E59 File Offset: 0x00011059
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00012E61 File Offset: 0x00011061
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00012E69 File Offset: 0x00011069
		public LocalizedString Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04000413 RID: 1043
		private readonly int lineNumber;

		// Token: 0x04000414 RID: 1044
		private readonly int position;

		// Token: 0x04000415 RID: 1045
		private readonly LocalizedString reason;
	}
}
