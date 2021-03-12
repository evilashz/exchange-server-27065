using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F9 RID: 505
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedSwitchValueException : LocalizedException
	{
		// Token: 0x060010A4 RID: 4260 RVA: 0x00039190 File Offset: 0x00037390
		public UnexpectedSwitchValueException(string enumValue) : base(Strings.UnexpectedSwitchValueException(enumValue))
		{
			this.enumValue = enumValue;
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000391A5 File Offset: 0x000373A5
		public UnexpectedSwitchValueException(string enumValue, Exception innerException) : base(Strings.UnexpectedSwitchValueException(enumValue), innerException)
		{
			this.enumValue = enumValue;
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x000391BB File Offset: 0x000373BB
		protected UnexpectedSwitchValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.enumValue = (string)info.GetValue("enumValue", typeof(string));
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000391E5 File Offset: 0x000373E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("enumValue", this.enumValue);
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00039200 File Offset: 0x00037400
		public string EnumValue
		{
			get
			{
				return this.enumValue;
			}
		}

		// Token: 0x0400087E RID: 2174
		private readonly string enumValue;
	}
}
