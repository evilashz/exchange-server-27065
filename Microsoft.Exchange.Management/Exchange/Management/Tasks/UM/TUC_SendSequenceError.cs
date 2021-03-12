using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F1 RID: 4593
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_SendSequenceError : LocalizedException
	{
		// Token: 0x0600B992 RID: 47506 RVA: 0x002A6249 File Offset: 0x002A4449
		public TUC_SendSequenceError(string error) : base(Strings.SendSequenceError(error))
		{
			this.error = error;
		}

		// Token: 0x0600B993 RID: 47507 RVA: 0x002A625E File Offset: 0x002A445E
		public TUC_SendSequenceError(string error, Exception innerException) : base(Strings.SendSequenceError(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x0600B994 RID: 47508 RVA: 0x002A6274 File Offset: 0x002A4474
		protected TUC_SendSequenceError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x0600B995 RID: 47509 RVA: 0x002A629E File Offset: 0x002A449E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17003A4B RID: 14923
		// (get) Token: 0x0600B996 RID: 47510 RVA: 0x002A62B9 File Offset: 0x002A44B9
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04006466 RID: 25702
		private readonly string error;
	}
}
