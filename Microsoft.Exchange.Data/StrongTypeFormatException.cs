using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000098 RID: 152
	[Serializable]
	public class StrongTypeFormatException : FormatException
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x0000FA43 File Offset: 0x0000DC43
		public StrongTypeFormatException(string message, string paramName) : base(message)
		{
			this.paramName = paramName;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000FA53 File Offset: 0x0000DC53
		public string ParamName
		{
			get
			{
				return this.paramName;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000FA5B File Offset: 0x0000DC5B
		protected StrongTypeFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.paramName = (string)info.GetValue("ParamName", typeof(string));
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000FA85 File Offset: 0x0000DC85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ParamName", this.ParamName);
		}

		// Token: 0x04000228 RID: 552
		private string paramName;
	}
}
