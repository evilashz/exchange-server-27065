using System;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications
{
	// Token: 0x02000003 RID: 3
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.PushNotifications.Wcf")]
	internal abstract class BasicNotification : BasicDataContract
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021AE File Offset: 0x000003AE
		public BasicNotification(string identifier = null)
		{
			this.Identifier = (identifier ?? BasicNotification.GetNextId());
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021C6 File Offset: 0x000003C6
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000021CE File Offset: 0x000003CE
		[DataMember(Name = "id", EmitDefaultValue = false)]
		public string Identifier { get; private set; }

		// Token: 0x0600000C RID: 12 RVA: 0x000021D7 File Offset: 0x000003D7
		public sealed override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = "id:" + this.Identifier;
			}
			return this.toString;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FD File Offset: 0x000003FD
		[OnDeserialized]
		internal void OnDeserialized(StreamingContext context)
		{
			if (this.Identifier == null)
			{
				this.Identifier = BasicNotification.GetNextId();
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002214 File Offset: 0x00000414
		protected override void InternalToFullString(StringBuilder sb)
		{
			base.InternalToFullString(sb);
			sb.Append("id:").Append(this.Identifier).Append("; ");
			sb.Append("type:").Append(base.GetType().ToString()).Append("; ");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002270 File Offset: 0x00000470
		private static string GetNextId()
		{
			int num = Interlocked.Increment(ref BasicNotification.idCounter);
			return string.Format(BasicNotification.IdTemplate, num);
		}

		// Token: 0x04000004 RID: 4
		private static readonly string IdTemplate = ExDateTime.UtcNow.ToString("yyyyMMdd-HHmmss-{0}");

		// Token: 0x04000005 RID: 5
		private static int idCounter;

		// Token: 0x04000006 RID: 6
		private string toString;
	}
}
