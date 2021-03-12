using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200103B RID: 4155
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InconsistentADException : LocalizedException
	{
		// Token: 0x0600AFE0 RID: 45024 RVA: 0x00295079 File Offset: 0x00293279
		public InconsistentADException(string reason) : base(Strings.InconsistentADError(reason))
		{
			this.reason = reason;
		}

		// Token: 0x0600AFE1 RID: 45025 RVA: 0x0029508E File Offset: 0x0029328E
		public InconsistentADException(string reason, Exception innerException) : base(Strings.InconsistentADError(reason), innerException)
		{
			this.reason = reason;
		}

		// Token: 0x0600AFE2 RID: 45026 RVA: 0x002950A4 File Offset: 0x002932A4
		protected InconsistentADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x0600AFE3 RID: 45027 RVA: 0x002950CE File Offset: 0x002932CE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17003811 RID: 14353
		// (get) Token: 0x0600AFE4 RID: 45028 RVA: 0x002950E9 File Offset: 0x002932E9
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04006177 RID: 24951
		private readonly string reason;
	}
}
