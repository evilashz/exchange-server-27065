using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002DD RID: 733
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RBACContextParserException : LocalizedException
	{
		// Token: 0x060019B1 RID: 6577 RVA: 0x0005D657 File Offset: 0x0005B857
		public RBACContextParserException(int lineNumber, int position, string reason) : base(Strings.RBACContextParserException(lineNumber, position, reason))
		{
			this.lineNumber = lineNumber;
			this.position = position;
			this.reason = reason;
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0005D67C File Offset: 0x0005B87C
		public RBACContextParserException(int lineNumber, int position, string reason, Exception innerException) : base(Strings.RBACContextParserException(lineNumber, position, reason), innerException)
		{
			this.lineNumber = lineNumber;
			this.position = position;
			this.reason = reason;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0005D6A4 File Offset: 0x0005B8A4
		protected RBACContextParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.position = (int)info.GetValue("position", typeof(int));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0005D719 File Offset: 0x0005B919
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("position", this.position);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0005D756 File Offset: 0x0005B956
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0005D75E File Offset: 0x0005B95E
		public int Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0005D766 File Offset: 0x0005B966
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040009A2 RID: 2466
		private readonly int lineNumber;

		// Token: 0x040009A3 RID: 2467
		private readonly int position;

		// Token: 0x040009A4 RID: 2468
		private readonly string reason;
	}
}
