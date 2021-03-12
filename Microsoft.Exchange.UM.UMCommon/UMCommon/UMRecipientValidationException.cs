using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001AB RID: 427
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UMRecipientValidationException : LocalizedException
	{
		// Token: 0x06000E80 RID: 3712 RVA: 0x00035193 File Offset: 0x00033393
		public UMRecipientValidationException(string recipient, string fieldName) : base(Strings.UMRecipientValidation(recipient, fieldName))
		{
			this.recipient = recipient;
			this.fieldName = fieldName;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000351B0 File Offset: 0x000333B0
		public UMRecipientValidationException(string recipient, string fieldName, Exception innerException) : base(Strings.UMRecipientValidation(recipient, fieldName), innerException)
		{
			this.recipient = recipient;
			this.fieldName = fieldName;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000351D0 File Offset: 0x000333D0
		protected UMRecipientValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.recipient = (string)info.GetValue("recipient", typeof(string));
			this.fieldName = (string)info.GetValue("fieldName", typeof(string));
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00035225 File Offset: 0x00033425
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("recipient", this.recipient);
			info.AddValue("fieldName", this.fieldName);
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00035251 File Offset: 0x00033451
		public string Recipient
		{
			get
			{
				return this.recipient;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00035259 File Offset: 0x00033459
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x04000784 RID: 1924
		private readonly string recipient;

		// Token: 0x04000785 RID: 1925
		private readonly string fieldName;
	}
}
