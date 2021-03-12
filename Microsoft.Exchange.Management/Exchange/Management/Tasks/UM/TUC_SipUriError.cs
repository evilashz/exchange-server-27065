using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011FB RID: 4603
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_SipUriError : LocalizedException
	{
		// Token: 0x0600B9BE RID: 47550 RVA: 0x002A654E File Offset: 0x002A474E
		public TUC_SipUriError(string field) : base(Strings.SipUriError(field))
		{
			this.field = field;
		}

		// Token: 0x0600B9BF RID: 47551 RVA: 0x002A6563 File Offset: 0x002A4763
		public TUC_SipUriError(string field, Exception innerException) : base(Strings.SipUriError(field), innerException)
		{
			this.field = field;
		}

		// Token: 0x0600B9C0 RID: 47552 RVA: 0x002A6579 File Offset: 0x002A4779
		protected TUC_SipUriError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.field = (string)info.GetValue("field", typeof(string));
		}

		// Token: 0x0600B9C1 RID: 47553 RVA: 0x002A65A3 File Offset: 0x002A47A3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("field", this.field);
		}

		// Token: 0x17003A4F RID: 14927
		// (get) Token: 0x0600B9C2 RID: 47554 RVA: 0x002A65BE File Offset: 0x002A47BE
		public string Field
		{
			get
			{
				return this.field;
			}
		}

		// Token: 0x0400646A RID: 25706
		private readonly string field;
	}
}
