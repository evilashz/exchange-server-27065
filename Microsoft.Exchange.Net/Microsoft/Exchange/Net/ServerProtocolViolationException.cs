using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Net
{
	// Token: 0x020000D9 RID: 217
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerProtocolViolationException : LocalizedException
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x000141FC File Offset: 0x000123FC
		public ServerProtocolViolationException(string size) : base(HttpStrings.ServerProtocolViolationException(size))
		{
			this.size = size;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00014211 File Offset: 0x00012411
		public ServerProtocolViolationException(string size, Exception innerException) : base(HttpStrings.ServerProtocolViolationException(size), innerException)
		{
			this.size = size;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00014227 File Offset: 0x00012427
		protected ServerProtocolViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.size = (string)info.GetValue("size", typeof(string));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00014251 File Offset: 0x00012451
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("size", this.size);
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0001426C File Offset: 0x0001246C
		public string Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x04000472 RID: 1138
		private readonly string size;
	}
}
